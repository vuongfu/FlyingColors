using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorOnline.DataAccess;
using System.Data.Entity;

namespace TutorOnline.Business.Repository
{
    public class ManagerRepository : BaseRepository
    {
        public IEnumerable<User> GetAllUser()
        {
            var users = _dbContext.Users.Include(x => x.Role);
            return users;
        }

        public IEnumerable<Role> GetAllRole()
        {
            return _dbContext.Roles;
        }

        public User Find(int? id)
        {
            User user = _dbContext.Users.Find(id);
            return user;
        }

        public void Edit(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            var categories = _dbContext.Categories;
            return categories;
        }
        public IEnumerable<Subject> GetAllSubject()
        {
            var subjects = _dbContext.Subjects.Include(x => x.Category);
            return subjects;
        }
        public Category FindCategory(int? id)
        {
            Category category = _dbContext.Categories.Find(id);
            return category;
        }
        /// <summary>
        /// This function find all Subject which have CategoriesID same.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>subjects</returns>
        public List<Subject> FindSubjects(int? id)
        {
            List<Subject> listSub = GetAllSubject().Where(x => x.CategoryID == id).ToList();
            return listSub;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
