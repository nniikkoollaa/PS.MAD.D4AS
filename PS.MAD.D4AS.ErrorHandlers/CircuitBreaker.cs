using System;
using System.Threading;

namespace PS.MAD.D4AS.ErrorHandlers
{
    public class CircuitBreaker
    {
        private enum CircuitBreakerStateEnum
        {
            Closed,
            Open,
            HalfOpen
        }

        private readonly object halfOpenSyncObject = new object();

        private CircuitBreakerStateEnum State { get; set; }
        private Exception LastException { get; set; }
        private DateTime LastStateChangedDateUtc { get; set; }
        private bool IsClosed { get; set; }
        private Action Action { get; set; }

        private TimeSpan OpenToHalfOpenWaitTime { get; set; }
        private int MaxiumumAllowedNumberOfFailedAttempts { get; set; }
        private int NeededNumberOfSuccessfulAttempts { get; set; }

        private int NumberOfFailedAttempts { get; set; }
        private int NumberOfSuccessfulAttempts { get; set; }

        public CircuitBreaker(Action actionToPerform)
        {
            // pass additional configuration such as desired initial state and threshold  values
            IsClosed = true;
            State = CircuitBreakerStateEnum.Closed;
            OpenToHalfOpenWaitTime = TimeSpan.FromSeconds(3);

            Action = actionToPerform;
        }

        public void Execute()
        {
            if (!IsClosed)
            {
                if (LastStateChangedDateUtc + OpenToHalfOpenWaitTime < DateTime.UtcNow)
                {
                    bool lockToken = false;
                    try
                    {
                        Monitor.TryEnter(halfOpenSyncObject, ref lockToken);
                        if (lockToken)
                        {
                            HalfOpen();
                            Action();
                            Close();
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Open(ex);
                        throw;
                    }
                    finally
                    {
                        if (lockToken)
                        {
                            Monitor.Exit(halfOpenSyncObject);
                        }
                    }
                }

                throw new CircuitBreakerOpenException(LastException);
            }

            try
            {
                Action();
            }
            catch (Exception ex)
            {
                TraceException(ex);

                throw;
            }
        }

        private void TraceException(Exception ex)
        {
            // examine the type of exception and update counters
            Open(ex);
        }

        private void Open(Exception ex)
        {
            LastException = ex;
        }

        private void Close()
        {

        }

        private void HalfOpen()
        {

        }
    }
}
