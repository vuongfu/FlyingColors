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

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
