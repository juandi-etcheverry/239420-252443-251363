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
    public class CategoryLogic : ICategoryLogic
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryLogic(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IList<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }
    }
}
