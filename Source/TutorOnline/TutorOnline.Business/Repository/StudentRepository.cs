using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorOnline.DataAccess;
using System.Data.Entity;
using TutorOnline.Common;

namespace TutorOnline.Business.Repository
{
    public class StudentRepository : BaseRepository
    {
        public IEnumerable<Student> GetAllStudent()
        {
            var students = _dbContext.Students.Include(x => x.Role).Where(x => x.isActived == true);
            return students;
        }
        public IEnumerable<Student> GetStuByCountry(string country)
        {
            if (country == new ManagerStringCommon().vn.ToString())
            {
                var students = _dbContext.Students.Include(x => x.Role).Where(x => x.isActived == true && x.Country == country);
                return students;
            }
            else
            {
                var students = _dbContext.Students.Include(x => x.Role).Where(x => x.isActived == true && x.Country != "Việt nam");
                return students;
            }

        }
        public IEnumerable<Student> GetStuInMonth()
        {
            var students = _dbContext.Students.Include(x => x.Role).Where(x => x.RegisterDate.Month == DateTime.Now.Month);
            return students;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
