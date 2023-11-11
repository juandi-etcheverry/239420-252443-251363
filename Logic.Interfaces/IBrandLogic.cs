using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Logic.Interfaces
{
    public interface IBrandLogic
    {
        public IList<Brand> GetAllBrands();
    }
}
