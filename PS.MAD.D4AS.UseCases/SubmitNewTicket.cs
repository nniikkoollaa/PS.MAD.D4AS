using PS.MAD.D4AS.UseCases.Exceptions;
using System;

namespace PS.MAD.D4AS.UseCases
{
    public class SubmitNewTicket
    {
        private readonly Contracts.ITicketRepository _repository;
        private readonly Random _random;

        public SubmitNewTicket(Contracts.ITicketRepository repository)
        {
            this._repository = repository;
            _random = new Random();
        }

        public Guid ForUser(Entities.User user, Entities.Ticket ticket)
        {
            // 1. Validate
            this.Validate(user);

            // 2. Save
            var ticketId = this._repository.Create(ticket);

            return ticketId;
        }

        private void Validate(Entities.User user)
        {
            System.Threading.Thread.Sleep(_random.Next(800, 1200));
            var isValid = true;

            if (!isValid)
            {
                throw new UserNotValidException();
            }
        }
    }
}
