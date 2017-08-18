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
        public string Authenticate(string username, string password)
        {
            var CheckBackEndUser = _dbContext.BackendUsers.FirstOrDefault(a => a.UserName == username && a.isActived == true);
            var CheckTutor = _dbContext.Tutors.FirstOrDefault(a => a.UserName == username && a.isActived == true);
            var CheckStudent = _dbContext.Students.FirstOrDefault(a => a.UserName == username && a.isActived == true);
            var CheckParent = _dbContext.Parents.FirstOrDefault(a => a.UserName == username && a.isActived == true);
            if (CheckBackEndUser == null && CheckTutor == null && CheckStudent == null && CheckParent == null)
                return null;

            string returnRoleName = null;
            
            if (CheckBackEndUser != null)
            {
                string pass = CheckBackEndUser.Password;
                if (pass.Equals(password,StringComparison.CurrentCulture))
                    returnRoleName = CheckBackEndUser.Role.RoleName;
                
            }                
            if (CheckTutor != null) {
                string pass = CheckTutor.Password;
                if (pass.Equals(password, StringComparison.CurrentCulture))
                    returnRoleName = CheckTutor.Role.RoleName;
            }                
            if (CheckStudent != null)
            {
                string pass = CheckStudent.Password;
                if (pass.Equals(password, StringComparison.CurrentCulture))
                    returnRoleName = CheckStudent.Role.RoleName;
            }                
            if (CheckParent != null)
            {
                string pass = CheckParent.Password;
                if (pass.Equals(password, StringComparison.CurrentCulture))
                    returnRoleName = CheckParent.Role.RoleName;
            }
                

            return returnRoleName;
        }

        public BackendUser getCurrentUserTypeBackEnd(string Username)
        {
            return _dbContext.BackendUsers.FirstOrDefault(x => x.UserName == Username);
        }

        public Tutor getCurrentUserTypeTutor(string Username)
        {
            return _dbContext.Tutors.FirstOrDefault(x => x.UserName == Username);
        }

        public Student getCurrentUserTypeStudent(string Username)
        {
            return _dbContext.Students.FirstOrDefault(x => x.UserName == Username);
        }

        public Parent getCurrentUserTypeParent(string Username)
        {
            return _dbContext.Parents.FirstOrDefault(x => x.UserName == Username);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
