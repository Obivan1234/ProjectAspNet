using System;
using System.Collections.Generic;
using System.Linq;
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
