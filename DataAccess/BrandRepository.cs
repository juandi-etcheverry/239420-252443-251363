using DataAccess.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class BrandRepository: IBrandRepository
    {
        private readonly DbContext _context;

        public BrandRepository(DbContext context)
        {
            _context = context;
        }

        public IList<Brand> GetAllBrands()
        {
            return _context.Set<Brand>().ToList();
        }
    }
}
