using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace PS.MAD.D4AS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly UseCases.SubmitNewTicket _submitNewTicket;
        private readonly UseCases.ProcessImage _imageProcessor;
        private readonly UseCases.ProcessVideo _videoProcessor;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TicketsController> _logger;

        public TicketsController(
            UseCases.SubmitNewTicket submitNewTicket,
            UseCases.ProcessImage imageProcessor,
            UseCases.ProcessVideo videoProcessor,
            IConfiguration configuration,
            ILogger<TicketsController> logger)
        {
            _submitNewTicket = submitNewTicket;
            _imageProcessor = imageProcessor;
            _videoProcessor = videoProcessor;
            _configuration = configuration;
            _logger = logger;
        }

        public ActionResult Post([FromBody]Model.NewTicketRequest request)
        {
            var connectionString = _configuration["ServiceBus:ConnectionString"];
            var destinationQueueName = _configuration["ServiceBus:OutputQueueName"];
            var queueClient = new Microsoft.Azure.ServiceBus.QueueClient(connectionString, destinationQueueName);

            var message = new Microsoft.Azure.ServiceBus.Message();
            message.Body = System.Text.Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    Description = request.Title
                }));
            message.Label = "New";
            message.UserProperties.Add("Status", "Created");
            message.UserProperties.Add("UserId", request.UserId);

            var ticketId = GetNewGuid();
            message.UserProperties.Add("TicketId", ticketId);

            queueClient.SendAsync(message);
            this._logger.LogInformation("Ticket with ID: {ID} has beed created", ticketId);

            var telemetryClient = new Microsoft.ApplicationInsights.TelemetryClient();
            var requestTelemetry = new Microsoft.ApplicationInsights.DataContracts.RequestTelemetry();
            telemetryClient.TrackRequest(requestTelemetry);

            return Ok();
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> UploadImage([FromForm]Model.UploadFileRequest request)
        {
            var image = new Entities.Image()
            {
                UserDescription = request.Description,
                Body = new byte[request.File.Length],
                Name = request.File.FileName
            };

            request.File.OpenReadStream().Read(image.Body, 0, image.Body.Length);

            _imageProcessor.ForTicket(
                new Entities.Ticket()
                {
                    Id = request.TicketId
                },
                image);

            return Ok();
        }

        [HttpPut("[action]")]
        public ActionResult UploadVideo([FromForm]Model.UploadFileRequest request)
        {
            var video = new Entities.Video()
            {
                UserDescription = request.Description,
                Body = new byte[request.File.Length],
                Name = request.File.FileName
            };

            request.File.OpenReadStream().Read(video.Body, 0, video.Body.Length);

            _videoProcessor.ForTicket(
                new Entities.Ticket()
                {
                    Id = request.TicketId
                },
                video);

            return Ok();
        }

        private Guid GetNewGuid()
        {
            return Guid.NewGuid();
        }
    }
}
