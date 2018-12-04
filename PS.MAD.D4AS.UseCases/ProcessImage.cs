using PS.MAD.D4AS.UseCases.Exceptions;
using System;

namespace PS.MAD.D4AS.UseCases
{
    public class ProcessImage
    {
        private readonly Contracts.ITicketRepository _repository;
        private readonly Random _random;

        public ProcessImage(Contracts.ITicketRepository repository)
        {
            _repository = repository;
            _random = new Random();
        }

        public void ForTicket(Entities.Ticket ticket, Entities.Image image)
        {
            // 1. validate image - it pictures a damaged vehicle
            Validate(image);

            // 2. analyse the image - use AI tool to estimate damage and set priority
            AssessDamageAndPriority(image);

            // 3. crop and compress the image 
            CropAndCompress(image);

            // 4. update ticket
            UpdateTicketWithImageAndPriority(ticket, image);
        }

        private void Validate(Entities.Image image)
        {
            // validation logic goes here...
            System.Threading.Thread.Sleep(_random.Next(800, 1200));
            var isValid = true;

            if (!isValid)
            {
                throw new ImageNotValidException();
            }
        }

        private void AssessDamageAndPriority(Entities.Image image)
        {
            // AI logic goes here...
            image.Priority = Entities.PriorityEnum.Medium;
            image.AnalyserDescription = "Description";
            System.Threading.Thread.Sleep(_random.Next(800, 1200));
        }

        private void CropAndCompress(Entities.Image image)
        {
            // crop and compress algorithm goes here...
            System.Threading.Thread.Sleep(_random.Next(400, 600));
        }

        private void UpdateTicketWithImageAndPriority(Entities.Ticket ticket, Entities.Image image)
        {
            var existingTicket = _repository.GetBy(ticket.Id);
            existingTicket.AddImage(image);
            _repository.UpdatePriority(existingTicket);
            _repository.AttachImage(ticket, image);
        }
    }
}
