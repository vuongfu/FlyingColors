using HappyCakeStore.Domain.Abstract;
using HappyCakeStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyCakeStore.Domain.Business
{
    public class EFCategoryRepository : ICategoriesRepository 
    {
        private readonly EFDbContext context = new EFDbContext();

        public IEnumerable<Category> Categories
        {
            get
            {
                return context.Categories;
            }
        }
    }
}
