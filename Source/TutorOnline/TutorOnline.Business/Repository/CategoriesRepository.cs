using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorOnline.DataAccess;

namespace TutorOnline.Business.Repository
{
    public class CategoriesRepository : BaseRepository
    {
        public IEnumerable<Category> GetAllCategories()
        {
            var categories = _dbContext.Categories;
            return categories;
        }

        public Category FindCategory(int? id)
        {
            Category category = _dbContext.Categories.Find(id);
            return category;
        }

        public void AddCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
        }

        public void EditCategory(Category category)
        {
            _dbContext.Entry(category).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            Category category = _dbContext.Categories.Find(id);
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
        }

        public bool isExistsCategoryName(string name)
        {
            var category = _dbContext.Categories.FirstOrDefault(x => x.CategoryName == name);
            if (category == null)
                return false;
            else
                return true;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
