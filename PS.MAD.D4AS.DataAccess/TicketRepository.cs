using Microsoft.EntityFrameworkCore;
using PS.MAD.D4AS.Entities;
using System;
using System.Linq;
using PS.MAD.D4AS.DataAccess.Converters;

namespace PS.MAD.D4AS.DataAccess
{
    public class TicketRepository : UseCases.Contracts.ITicketRepository
    {
        private readonly Contracts.IStorage _storage;

        public TicketRepository(Contracts.IStorage storage)
        {
            _storage = storage;
        }

        public Guid Create(Entities.Ticket ticket)
        {
            var result = Guid.Empty;
            using (var context = new TicketingContext())
            {
                var dbEntity = ticket.FromCore();
                context.Tickets.Add(dbEntity);
                context.SaveChanges();

                result = dbEntity.Id;
            }

            return result;
        }

        public Entities.Ticket GetBy(Guid id)
        {
            Entities.Ticket result = null;
            using (var context = new TicketingContext())
            {
                var dbEntity = context.Tickets.Include(t => t.Images).FirstOrDefault(t => t.Id == id);
                if (dbEntity != null)
                {
                    result = dbEntity.ToCore(); 
                }
            }

            return result;
        }


        public Guid AttachImage(Entities.Ticket ticket, Entities.Image image)
        {
            var result = Guid.Empty;

            using (var context = new TicketingContext())
            {
                var dbEntity = image.FromCore();
                dbEntity.TicketId = ticket.Id;
                context.Images.Add(dbEntity);
                context.SaveChanges();

                result = dbEntity.Id;
                image.Id = dbEntity.Id;
            }

            _storage.StoreImage(image);

            return result;
        }

        public Guid AttachVideo(Entities.Ticket ticket, Entities.Video video)
        {
            var result = Guid.Empty;

            using (var context = new TicketingContext())
            {
                var dbEntity = video.FromCore();
                dbEntity.TicketId = ticket.Id;
                context.Videos.Add(dbEntity);
                context.SaveChanges();

                result = dbEntity.Id;
                video.Id = dbEntity.Id;
            }

            _storage.StoreVideo(video);

            return result;
        }

        public void UpdatePriority(Ticket ticket)
        {
            using (var context = new TicketingContext())
            {
                var dbEntity = context.Tickets.First(t=>t.Id==ticket.Id);
                dbEntity.Priority = (int)ticket.Priority;
                context.SaveChanges();
            }
        }
    }
}
