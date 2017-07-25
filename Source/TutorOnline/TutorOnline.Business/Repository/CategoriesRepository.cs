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
            var categories = _dbContext.Categories.Where(x => x.isActived == true);
            return categories;
        }

        public Category FindCategory(int? id)
        {
            Category category = _dbContext.Categories.Find(id);
            return category;
        }

        public void AddCategory(Category category)
        {
            category.isActived = true;
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
        }

        public void EditCategory(Category category)
        {
            category.isActived = true;
            _dbContext.Entry(category).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            _dbContext.Categories.Where(x => x.CategoryId == id).ToList().ForEach(x => x.isActived = false);
            _dbContext.SaveChanges();
        }

        public bool isExistsCategoryName(string name)
        {
            var category = _dbContext.Categories.FirstOrDefault(x => x.CategoryName == name && x.isActived == true);
            if (category == null)
                return false;
            else
                return true;
        }
        public bool isExistedSubjectIn(int id)
        {
            var subject = _dbContext.Subjects.FirstOrDefault(x => x.CategoryId == id && x.isActived == true);
            if (subject == null)
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
