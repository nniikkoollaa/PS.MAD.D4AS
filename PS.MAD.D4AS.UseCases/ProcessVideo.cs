using PS.MAD.D4AS.UseCases.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PS.MAD.D4AS.UseCases
{
    public class ProcessVideo
    {
        private readonly Contracts.ITicketRepository _repository;
        private readonly Random _random;

        public ProcessVideo(Contracts.ITicketRepository repository)
        {
            this._repository = repository;
            _random = new Random();
        }

        public void ForTicket(Entities.Ticket ticket, Entities.Video video)
        {
            // 1. validate image - it pictures a damaged vehicle
            Validate(video);

            // 2. analyse the image - use AI tool to estimate damage and set priority
            AssessDamageAndPriority(video);

            // 3. crop and compress the image 
            CropAndCompress(video);

            // 4. update ticket
            UpdateTicketWithImageAndPriority(ticket, video);
        }

        private void Validate(Entities.Video video)
        {
            // validation logic goes here...
            System.Threading.Thread.Sleep(_random.Next(800, 1200));
            var isValid = true;

            if (!isValid)
            {
                throw new VideoNotValidException();
            }
        }

        private void AssessDamageAndPriority(Entities.Video video)
        {
            // AI logic goes here...
            video.Priority = Entities.PriorityEnum.Medium;
            video.AnalyserDescription = "Description";
            System.Threading.Thread.Sleep(_random.Next(800, 1200));
        }

        private void CropAndCompress(Entities.Video video)
        {
            // crop and compress algorithm goes here...
            System.Threading.Thread.Sleep(_random.Next(400, 600));
        }

        private void UpdateTicketWithImageAndPriority(Entities.Ticket ticket, Entities.Video video)
        {
            var existingTicket = _repository.GetBy(ticket.Id);
            existingTicket.AddVideo(video);
            _repository.UpdatePriority(existingTicket);
            _repository.AttachVideo(ticket, video);
        }
    }
}
