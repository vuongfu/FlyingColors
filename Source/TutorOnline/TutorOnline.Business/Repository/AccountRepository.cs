using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorOnline.DataAccess;

namespace TutorOnline.Business.Repository
{
    public class AccountRepository : BaseRepository
    {
        public bool Authenticate(string username, string password)
        {
            var result = _dbContext.Users.FirstOrDefault(a => a.Username == username && a.Password == password);
            if (result == null)
                return false;
            return true;
        }

        public User getCurrentUser(string Username)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Username == Username);
        }
    }
}
