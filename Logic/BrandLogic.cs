using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Domain;
using Logic.Interfaces;

namespace Logic
{
    public class BrandLogic: IBrandLogic
    {
        private readonly IBrandRepository _brandRepository;

        public BrandLogic(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public IList<Brand> GetAllBrands()
        {
            return _brandRepository.GetAllBrands();
        }
    }
}
