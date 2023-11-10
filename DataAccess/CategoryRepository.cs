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
    public class CategoryRepository: ICategoryRepository
    {
        private readonly DbContext _context;

        public CategoryRepository(DbContext context)
        {
            _context = context;
        }

        public IList<Category> GetAllCategories()
        {
            return _context.Set<Category>().ToList();
        }
    }
}
