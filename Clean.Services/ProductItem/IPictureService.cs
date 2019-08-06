using Clean.Core.Data;
using Clean.Core.Domain.ProductItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Services.ProductItem
{
    public interface IPictureService
    {

        IEnumerable<Picture> TakePictures(int amount);

        IEnumerable<Picture> GetAllPictures();

        void InsertPicture(Picture picture);

        IEnumerable<Picture> GetPicturesByUserIdAsc(object id, int amount = -1, int? startFrom = null);

        IEnumerable<Picture> GetPicturesByUserIdDesc(object id, int amount = -1, int? startFrom = null);

    }
}
