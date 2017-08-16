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

        public string GetRoleName(string username)
        {
            var CheckBackEndUser = _dbContext.BackendUsers.FirstOrDefault(a =>  a.UserName == username);
            var CheckTutor = _dbContext.Tutors.FirstOrDefault(a =>  a.UserName == username);
            var CheckStudent = _dbContext.Students.FirstOrDefault(a => a.UserName == username);
            var CheckParent = _dbContext.Parents.FirstOrDefault(a =>  a.UserName == username);
            if (CheckBackEndUser == null && CheckTutor == null && CheckStudent == null && CheckParent == null)
                return null;

            string returnRole = null;
            if (CheckBackEndUser != null)
                returnRole = CheckBackEndUser.Role.RoleName;
            if (CheckTutor != null)
                returnRole = CheckTutor.Role.RoleName;
            if (CheckStudent != null)
                returnRole = CheckStudent.Role.RoleName;
            if (CheckParent != null)
                returnRole = CheckParent.Role.RoleName;

            return returnRole;
        }

        public IEnumerable<Parent> GetAllParentUser()
        {
            var users = _dbContext.Parents.Include(x => x.Role);
            return users;
        }

        public IEnumerable<Subject> GetAllTutorSubject()
        {
            var subject = _dbContext.Subjects;
            return subject;
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

        public IEnumerable<ParentInfo> GetAllParent()
        {

            var users = _dbContext.Parents.Include(x => x.Role);
            List<ParentInfo> list = new List<ParentInfo>();
            foreach (var item in users)
            {
                ParentInfo temp = new ParentInfo();
                temp.ParentId = item.ParentId;
                temp.ParentName = item.LastName + " " + item.FirstName;
                list.Add(temp);
            }
            return list;
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
            user.RegisterDate = DateTime.Now;
            _dbContext.BackendUsers.Add(user);
            _dbContext.SaveChanges();
        }

        public void AddTutor(Tutor user)
        {
            user.RegisterDate = DateTime.Now;
            _dbContext.Tutors.Add(user);
            _dbContext.SaveChanges();
        }

        public void AddStudent(Student user)
        {
            user.RegisterDate = DateTime.Now;
            _dbContext.Students.Add(user);
            _dbContext.SaveChanges();
        }

        public void AddParent(Parent user)
        {
            user.RegisterDate = DateTime.Now;
            _dbContext.Parents.Add(user);
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

        public void EditParentUser(Parent user)
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

        public bool CheckDeleteParent(int id)
        {
            var temp = _dbContext.Students.FirstOrDefault(x => x.ParentId == id);
            if(temp != null)
            {
                return false;
            }
            return true;
        }

        public void DeleteParentUser(int id)
        {
            Parent user = _dbContext.Parents.Find(id);
            _dbContext.Parents.Remove(user);
            _dbContext.SaveChanges();
        }

        public void DeleteStudentUser(int id)
        {
            Student user = _dbContext.Students.Find(id);
            _dbContext.Students.Remove(user);
            _dbContext.SaveChanges();
        }

        public void DeleteTutorUser(int id)
        {
            Tutor user = _dbContext.Tutors.Find(id);
            _dbContext.Tutors.Remove(user);
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

        public bool checkPassword(int id, string pass, int userRole)
        {
            var ParentUser = _dbContext.Parents.FirstOrDefault(x => x.ParentId == id && x.Password == pass && x.RoleId == userRole);
            var BackEndUser = _dbContext.BackendUsers.FirstOrDefault(x => x.BackendUserId == id && x.Password == pass && x.RoleId == userRole);
            var StudentUser = _dbContext.Students.FirstOrDefault(x => x.StudentId == id && x.Password == pass && x.RoleId == userRole);
            var TutorUser = _dbContext.Tutors.FirstOrDefault(x => x.TutorId == id && x.Password == pass && x.RoleId == userRole);
            if (ParentUser == null && BackEndUser == null && StudentUser == null && TutorUser == null)
                return false;
            else
                return true;
        }

        public UserLoginInfo checkEmailLogin(string email)
        {
            var ParentUser = _dbContext.Parents.FirstOrDefault(x => x.Email == email && x.isActived == true);
            var BackEndUser = _dbContext.BackendUsers.FirstOrDefault(x => x.Email == email && x.isActived == true);
            var StudentUser = _dbContext.Students.FirstOrDefault(x => x.Email == email && x.isActived == true);
            var TutorUser = _dbContext.Tutors.FirstOrDefault(x => x.Email == email && x.isActived == true);
            if (ParentUser == null && BackEndUser == null && StudentUser == null && TutorUser == null)
                return null;
            UserLoginInfo returnResult;

            if (ParentUser != null)
            {
                returnResult = new UserLoginInfo(ParentUser);
            }
            else if (BackEndUser != null)
            {
                returnResult = new UserLoginInfo(BackEndUser);
            }
            else if (StudentUser != null)
            {
                returnResult = new UserLoginInfo(StudentUser);
            }
            else if (TutorUser != null)
            {
                returnResult = new UserLoginInfo(TutorUser);
            }
            else
            {
                return null;
            }

            return returnResult;
        }

    }

    public class ParentInfo
    {
        public int ParentId { get; set; }
        public string ParentName { get; set; }
    }

    public class UserLoginInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string email { get; set; }
        public string Photo { get; set; }

        public UserLoginInfo()
        {

        }

        public UserLoginInfo(Tutor data)
        {
            this.UserId = data.TutorId;
            this.UserName = data.UserName;
            this.RoleId = data.RoleId;
            this.RoleName = data.Role.RoleName;
            this.email = data.Email;
            this.Photo = (data.Photo == null ? "DefaultIcon.png" : data.Photo);
        }
        public UserLoginInfo(Parent data)
        {
            this.UserId = data.ParentId;
            this.UserName = data.UserName;
            this.RoleId = data.RoleId;
            this.RoleName = data.Role.RoleName;
            this.email = data.Email;
            this.Photo = (data.Photo == null ? "DefaultIcon.png" : data.Photo);

        }
        public UserLoginInfo(BackendUser data)
        {
            this.UserId = data.BackendUserId;
            this.UserName = data.UserName;
            this.RoleId = data.RoleId;
            this.RoleName = data.Role.RoleName;
            this.email = data.Email;
            this.Photo = (data.Photo == null ? "DefaultIcon.png" : data.Photo);
        }
        public UserLoginInfo(Student data)
        {
            this.UserId = data.StudentId;
            this.UserName = data.UserName;
            this.RoleId = data.RoleId;
            this.RoleName = data.Role.RoleName;
            this.email = data.Email;
            this.Photo = (data.Photo == null ? "DefaultIcon.png" : data.Photo);
        }
    }
}
