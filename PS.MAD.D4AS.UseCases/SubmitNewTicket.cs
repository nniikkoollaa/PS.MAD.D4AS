using System;

namespace PS.MAD.D4AS.UseCases
{
    public class SubmitNewTicket
    {
        private readonly Contracts.ITicketRepository _repository;

        public SubmitNewTicket(Contracts.ITicketRepository repository)
        {
            this._repository = repository;
        }

        public Guid ForUser(Entities.User user, Entities.Ticket ticket)
        {
            // validate if user has a valid insurance policy
            return this._repository.Create(ticket);
        }
    }
}
