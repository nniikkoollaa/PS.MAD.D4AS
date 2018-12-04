using System;
using System.Collections.Generic;
using System.Text;

namespace PS.MAD.D4AS.DataAccess.Converters
{
    public static class VideoConverter
    {
        public static Entities.Video ToCore(this Model.Video video)
        {
            Entities.Video result = null;

            if (video != null)
            {
                result = new Entities.Video()
                {
                    Id = video.Id,
                    Name = video.Name,
                    UserDescription = video.UserDescription,
                    AnalyserDescription = video.AnalyserDescription,
                    Priority = (Entities.PriorityEnum)video.Priority
                };
            }

            return result;
        }

        public static Model.Video FromCore(this Entities.Video video)
        {
            Model.Video result = null;

            if (video != null)
            {
                result = new Model.Video()
                {
                    Id = video.Id,
                    Name = video.Name,
                    UserDescription = video.UserDescription,
                    AnalyserDescription = video.AnalyserDescription,
                    Priority = (int)video.Priority
                };
            }

            return result;
        }
    }
}
