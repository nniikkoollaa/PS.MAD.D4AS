using PS.MAD.D4AS.UseCases.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        private Entities.Image GetMonochromeCopy(Entities.Image image)
        {
            var monochromeImage = new Entities.Image();
            var imageParts = image.Split(4).ToArray();
            var monochromeParts = new ConcurrentBag<Entities.Image>();

            Parallel.For(
                0,
                imageParts.Length,
                (index) =>
                {
                    var monochromePart = imageParts[index].Monochrome();
                    monochromeParts.Add(monochromePart);
                });

            monochromeImage = Entities.Image.Combine(monochromeParts);

            return monochromeImage;
        }

        private IEnumerable<Entities.Image> GetImageVariations(Entities.Image image)
        {
            // create monochrome
            // create high contrast
            // create high brightness
            var imageVariations = new Entities.Image[3];

            var makeMonochromeTask = new Task<Entities.Image>(() =>
            {
                return image.Monochrome();
            });

            makeMonochromeTask.Start();

            var makeHighContrastTask = Task.Run(() =>
            {
                imageVariations[1] = image.HighContrast();
            });

            var makeHighBrightnessTask = Task.Factory.StartNew((state) =>
            {
                var percentage = (int)state;
                imageVariations[2] = image.HighBrightness(percentage);
            },
            50);

            // makeMonochromeTask.Wait();
            imageVariations[0] = makeMonochromeTask.Result;

            Task.WaitAll(makeMonochromeTask, makeHighContrastTask, makeHighBrightnessTask);
            Task.WaitAny(makeMonochromeTask, makeHighContrastTask, makeHighBrightnessTask);

            return imageVariations;
        }
    }
}
