using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorOnline.Business;
using System.Web;
using TutorOnline.DataAccess;
using System.Data.Entity;

namespace TutorOnline.Business.Repository
{
    public class UsersRepository : BaseRepository
    {
        public IEnumerable<BackendUser> GetAllBackEndUser()
        {
            var users = _dbContext.BackendUsers.Include(x => x.Role);
            return users;
        }

        public IEnumerable<Tutor> GetAllTutorUser()
        {
            var users = _dbContext.Tutors.Include(x => x.Role);
            return users;
        }

        public int GetRoleId(int? id, string username)
        {
            var CheckBackEndUser = _dbContext.BackendUsers.FirstOrDefault(a => a.BackendUserId == id && a.UserName == username);
            var CheckTutor = _dbContext.Tutors.FirstOrDefault(a => a.TutorId == id && a.UserName == username);
            var CheckStudent = _dbContext.Students.FirstOrDefault(a => a.StudentId == id && a.UserName == username);
            var CheckParent = _dbContext.Parents.FirstOrDefault(a => a.ParentId == id && a.UserName == username);
            if (CheckBackEndUser == null && CheckTutor == null && CheckStudent == null && CheckParent == null)
                return -1;

            int returnRoleId = -1;
            if (CheckBackEndUser != null)
                returnRoleId = CheckBackEndUser.RoleId;
            if (CheckTutor != null)
                returnRoleId = CheckTutor.RoleId;
            if (CheckStudent != null)
                returnRoleId = CheckStudent.RoleId;
            if (CheckParent != null)
                returnRoleId = CheckParent.RoleId;

            return returnRoleId;
        }

        public IEnumerable<Parent> GetAllParentUser()
        {
            var users = _dbContext.Parents.Include(x => x.Role);
            return users;
        }

        public IEnumerable<Student> GetAllStudentUser()
        {
            var users = _dbContext.Students.Include(x => x.Role);
            return users;
        }

        //public IEnumerable<BackendUsers> GetTutor(bool isActive)
        //{
        //    var user =  _dbContext.BackendUsers.Where(x => x.isActived == isActive);
        //    return user;
        //}

        public IEnumerable<Role> GetAllRole()
        {
            return _dbContext.Roles;
        }

        public BackendUser FindBackEndUser(int? id)
        {
            BackendUser user = _dbContext.BackendUsers.Find(id);
            return user;
        }

        public Student FindStudentUser(int? id)
        {
            Student user = _dbContext.Students.Find(id);
            return user;
        }

        public Parent FindParentUser(int? id)
        {
            Parent user = _dbContext.Parents.Find(id);
            return user;
        }

        public Tutor FindTutorUser(int? id)
        {
            Tutor user = _dbContext.Tutors.Find(id);
            return user;
        }

        public void AddBackEndUser(BackendUser user)
        {
            _dbContext.BackendUsers.Add(user);
            _dbContext.SaveChanges();
        }

        public void AddTutor(Tutor user)
        {
            _dbContext.Tutors.Add(user);
            _dbContext.SaveChanges();
        }

        public void AddStudent(Student user)
        {
            _dbContext.Students.Add(user);
            _dbContext.SaveChanges();
        }

        public void EditBackEndUser(BackendUser user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void EditTutorUser(Tutor user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void EditStudentUser(Student user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeleteBackEndUser(int id)
        {
            BackendUser user = _dbContext.BackendUsers.Find(id);
            _dbContext.BackendUsers.Remove(user);
            _dbContext.SaveChanges();
        }

        public void DeleteTutorUser(int id)
        {
            Tutor user = _dbContext.Tutors.Find(id);
            _dbContext.Tutors.Remove(user);
            _dbContext.SaveChanges();
        }

        public void DeleteStudentUser(int id)
        {
            Student user = _dbContext.Students.Find(id);
            _dbContext.Students.Remove(user);
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public bool isExistsUsername(string name)
        {
            var user = _dbContext.BackendUsers.FirstOrDefault(x => x.UserName == name);
            var Tutor = _dbContext.Tutors.FirstOrDefault(a => a.UserName == name);
            var Student = _dbContext.Students.FirstOrDefault(a => a.UserName == name);
            if (user == null && Tutor == null && Student == null)
                return false;
            else
                return true;
        }

        public bool checkPassword(int id, string pass)
        {
            var user = _dbContext.BackendUsers.FirstOrDefault(x => x.BackendUserId == id && x.Password == pass);
            if (user == null)
                return false;
            else
                return true;
        }

    }
}
