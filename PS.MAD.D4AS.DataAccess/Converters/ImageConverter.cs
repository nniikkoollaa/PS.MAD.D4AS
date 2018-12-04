using System;
using System.Collections.Generic;
using System.Text;

namespace PS.MAD.D4AS.DataAccess.Converters
{
    public static class ImageConverter
    {
        public static Entities.Image ToCore(this Model.Image image)
        {
            Entities.Image result = null;

            if (image != null)
            {
                result = new Entities.Image()
                {
                    Id = image.Id,
                    Name = image.Name,
                    UserDescription = image.UserDescription,
                    AnalyserDescription = image.AnalyserDescription,
                    Priority = (Entities.PriorityEnum)image.Priority
                };
            }

            return result;
        }

        public static Model.Image FromCore(this Entities.Image image)
        {
            Model.Image result = null;

            if (image != null)
            {
                result = new Model.Image()
                {
                    Id = image.Id,
                    Name = image.Name,
                    UserDescription = image.UserDescription,
                    AnalyserDescription = image.AnalyserDescription,
                    Priority = (int)image.Priority
                };
            }

            return result;
        }
    }
}
