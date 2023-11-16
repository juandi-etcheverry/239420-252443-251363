using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ColorRepository: IColorRepository
    {
        private readonly DbContext _context;

        public ColorRepository(DbContext context)
        {
            _context = context;
        }

        public IList<Color> GetAllColors()
        {
            return _context.Set<Color>().ToList();
        }
    }
}
