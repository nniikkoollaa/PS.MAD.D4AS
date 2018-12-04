using System;
using System.Collections.Generic;
using System.Linq;

namespace PS.MAD.D4AS.Entities
{
    public class Ticket : BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DateOfAccident { get; set; }
        public string Description { get; set; }
        public string AnalysedDescription { get; set; }
        public PriorityEnum Priority { get; set; }
        public IList<Image> Images { get; set; }
        public IList<Video> Videos { get; set; }

        public Ticket()
        {
            Images = new List<Image>();
            Videos = new List<Video>();
        }

        public void AddImage(Image image)
        {
            Images.Add(image);
            UpdatePriority();
        }

        public void AddVideo(Video video)
        {
            Videos.Add(video);
            UpdatePriority();
        }

        protected void UpdatePriority()
        {
            var averageImagePriority = Images.Select(i => (int)i.Priority).DefaultIfEmpty(0).Average();
            var averageVideoPriority = Videos.Select(v => (int)v.Priority).DefaultIfEmpty(0).Average();
            Priority = (PriorityEnum)((averageImagePriority + averageVideoPriority) / 2);
        }
    }
}
