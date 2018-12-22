using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PS.MAD.D4AS.ErrorHandlers
{
    public class OperationWithBasicRetryAsync
    {
        private int retryCount = 3;
        private readonly TimeSpan delay = TimeSpan.FromSeconds(5);
        private Task Action;

        public OperationWithBasicRetryAsync(Task action, int retryCount = 3, double delayInSeconds = 5)
        {
            this.Action = action;
            this.retryCount = retryCount;
            this.delay = TimeSpan.FromSeconds(delayInSeconds);
        }

        public async Task Execute()
        {
            int currentRetry = 0;

            while (true)
            {
                try
                {
                    await this.Action;

                    break;
                }
                catch (Exception ex)
                {
                    // log the error
                    currentRetry++;

                    if (currentRetry > this.retryCount)
                    {
                        throw;
                    }
                }

                await Task.Delay(delay);
            }
        }
    }
}
