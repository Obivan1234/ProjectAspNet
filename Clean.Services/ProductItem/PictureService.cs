using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Clean.Core.Data;
using Clean.Core.Domain.ProductItem;

namespace Clean.Services.ProductItem
{
    public class PictureService : IPictureService
    {
        private readonly IRepository<Picture> _picRepository;

        public PictureService(IRepository<Picture> picRepository)
        {
            this._picRepository = picRepository;
        }

        public IEnumerable<Picture> GetAllPictures()
        {
            return _picRepository.Get();
        }

        public IEnumerable<Picture> GetPicturesByUserIdAsc(object id, int amount = -1, int? startFrom = null)
        {
            if (startFrom != null)
            {
                return _picRepository.Get(p => p.ApplicationUserMyId == (string)id && p.Id > startFrom, amount, ItemOrderBy.Asc);
            }
            else
            {
                return _picRepository.Get(p => p.ApplicationUserMyId == (string)id, amount, ItemOrderBy.Asc);
            }
        }

        public IEnumerable<Picture> GetPicturesByUserIdDesc(object id, int amount = -1, int? startFrom = null)
        {
            if (startFrom != null)
            {
                return _picRepository.Get(p => (p.ApplicationUserMyId == (string)id) && p.Id < startFrom, amount, ItemOrderBy.Desc);
            }
            else
            {
                return _picRepository.Get(p => p.ApplicationUserMyId == (string)id, amount, ItemOrderBy.Desc);
            }
        }

        public void InsertPicture(Picture picture)
        {
            _picRepository.Insert(picture);
        }

        public IEnumerable<Picture> TakePictures(int amount)
        {
            return _picRepository.Get(null, amount);
        }
    }
}
