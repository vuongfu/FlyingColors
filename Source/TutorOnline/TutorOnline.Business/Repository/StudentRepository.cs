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
                var students = _dbContext.Students.Include(x => x.Role).Where(x => x.isActived == true && x.Country != "Vietnam");
                return students;
            }

        }
        public IEnumerable<Student> GetStuInMonth()
        {
            var students = _dbContext.Students.Include(x => x.Role).Where(x => x.RegisterDate.Month == DateTime.Now.Month);
            return students;
        }

        public IEnumerable<Schedule> GetAllSlotBookedByStudent(DateTime StartDay, DateTime EndDay, int StudentId)
        {
            var slot = _dbContext.Schedules.Where(x => x.OrderDate.Date >= StartDay.Date && x.OrderDate.Date <= EndDay.Date && x.StudentId == StudentId && (x.Status == 4 || x.Status == 3 || x.Status == 5));
            return slot;
        }

        public IEnumerable<Schedule> GetAllSlotBookedToday(int StudentId)
        {
            var slot = _dbContext.Schedules.Where(x => x.OrderDate.Date ==  DateTime.Today.Date && x.StudentId == StudentId && (x.Status == 4 || x.Status == 3 || x.Status == 5));
            return slot;
        }


        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
