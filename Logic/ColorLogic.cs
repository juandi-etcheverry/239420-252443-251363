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
    public class ColorLogic : IColorLogic
    {

        private readonly IColorRepository _colorRepository;

        public ColorLogic(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        public IList<Color> GetAllColors()
        {
            return _colorRepository.GetAllColors();
        }
    }
}
