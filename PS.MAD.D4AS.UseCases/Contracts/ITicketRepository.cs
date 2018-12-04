using System;
using System.Collections.Generic;
using System.Text;

namespace PS.MAD.D4AS.UseCases.Contracts
{
    public interface ITicketRepository
    {
        Guid Create(Entities.Ticket ticket);
        Entities.Ticket GetBy(Guid id);
        Guid AttachImage(Entities.Ticket ticket, Entities.Image image);
        Guid AttachVideo(Entities.Ticket ticket, Entities.Video video);
        void UpdatePriority(Entities.Ticket ticket);
    }
}
