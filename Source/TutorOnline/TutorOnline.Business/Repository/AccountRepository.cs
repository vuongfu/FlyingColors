﻿using System;
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
            var CheckBackEndUser = _dbContext.BackendUsers.FirstOrDefault(a => a.UserName == username && a.Password == password);
            var CheckTutor = _dbContext.Tutors.FirstOrDefault(a => a.UserName == username && a.Password == password);
            var CheckStudent = _dbContext.Students.FirstOrDefault(a => a.UserName == username && a.Password == password);
            var CheckParent = _dbContext.Parents.FirstOrDefault(a => a.UserName == username && a.Password == password);
            if (CheckBackEndUser == null && CheckTutor == null && CheckStudent == null && CheckParent == null)
                return null;

            string returnRoleName = null;
            if (CheckBackEndUser != null)
                returnRoleName = CheckBackEndUser.Role.RoleName;
            if (CheckTutor != null)
                returnRoleName = CheckTutor.Role.RoleName;
            if (CheckStudent != null)
                returnRoleName = CheckStudent.Role.RoleName;
            if (CheckParent != null)
                returnRoleName = CheckParent.Role.RoleName;

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
    }
}
