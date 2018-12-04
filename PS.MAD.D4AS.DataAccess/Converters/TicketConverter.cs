using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PS.MAD.D4AS.DataAccess.Converters;

namespace PS.MAD.D4AS.DataAccess.Converters
{
    public static class TicketConverter
    {
        public static Entities.Ticket ToCore(this Model.Ticket ticket)
        {
            Entities.Ticket result = null;

            if (ticket != null)
            {
                result = new Entities.Ticket()
                {
                    DateOfAccident = ticket.DateOfAccident,
                    Description = ticket.Description,
                    Priority = (Entities.PriorityEnum)ticket.Priority,
                    Id = ticket.Id
                };

                if (ticket.Images != null)
                {
                    foreach (var image in ticket.Images)
                    {
                        result.Images.Add(image.ToCore());
                    }
                }
            }

            return result;
        }

        public static Model.Ticket FromCore(this Entities.Ticket ticket)
        {
            Model.Ticket result = null;

            if (ticket != null)
            {
                result = new Model.Ticket()
                {
                    DateOfAccident = ticket.DateOfAccident,
                    Description = ticket.Description,
                    Priority = (int)ticket.Priority,
                    Id = ticket.Id
                };

                if (ticket.Images != null)
                {
                    var images = new List<Model.Image>();

                    foreach (var image in ticket.Images)
                    {
                        images.Add(image.FromCore());
                    }

                    result.Images = images;
                }
            }

            return result;
        }
    }
}
