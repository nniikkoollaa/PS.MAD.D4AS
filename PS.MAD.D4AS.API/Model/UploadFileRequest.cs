using System;

namespace PS.MAD.D4AS.API.Model
{
    public class UploadFileRequest
    {
        public Guid TicketId { get; set; }
        public string Description { get; set; }
        public Microsoft.AspNetCore.Http.IFormFile File { get; set; }
    }
}