using System;
using System.Collections.Generic;

namespace PS.MAD.D4AS.Entities
{
    public class Image : BaseEntity
    {
        public Guid Id { get; set; }
        public Ticket Ticket { get; set; }
        public byte[] Body { get; set; }
        public PriorityEnum Priority { get; set; }
        public string UserDescription { get; set; }
        public string AnalyserDescription { get; set; }
        public string Name { get; set; }

        public IEnumerable<Image> Split(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new Image();
            }
        }

        public Image Monochrome()
        {
            return new Image();
        }

        public Image HighContrast()
        {
            return new Image();
        }

        public Image HighBrightness(int percentage = 100)
        {
            return new Image();
        }


        public static Image Combine(IEnumerable<Image> parts)
        {
            return new Image();
        }
    }
}
