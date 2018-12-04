using System;
using System.Collections.Generic;
using System.Text;

namespace PS.MAD.D4AS.DataAccess.Contracts
{
    public interface IStorage
    {
        void StoreImage(Entities.Image image);
        void StoreVideo(Entities.Video video);
    }
}
