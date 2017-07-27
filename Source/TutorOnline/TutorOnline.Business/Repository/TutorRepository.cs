using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorOnline.DataAccess;
using TutorOnline.Common;
using System.Data.Entity;

namespace TutorOnline.Business.Repository
{
    public class TutorRepository : BaseRepository
    {
        public IEnumerable<Tutor> GetAllTutor()
        {
            var tutors = _dbContext.Tutors.Include(x => x.Role).Where(x => x.isActived == true);
            return tutors;
        }
        public IEnumerable<Tutor> GetTuByCountry(string country)
        {
            if (country == new ManagerStringCommon().vn.ToString())
            {
                var tutors = _dbContext.Tutors.Include(x => x.Role).Where(x => x.isActived == true && x.Country == country);
                return tutors;
            }
            else
            {
                var tutors = _dbContext.Tutors.Include(x => x.Role).Where(x => x.isActived == true && x.Country != "Vietnam");
                return tutors;
            }
            
        }
        public IEnumerable<Tutor> GetAllPretutor()
        {
            var tutors = _dbContext.Tutors.Include(x => x.Role).Where(x => x.RoleId == 8);
            return tutors;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void DeleteSlotBooked(int id)
        {
            TeachSchedule temp = _dbContext.TeachSchedules.Find(id);
            _dbContext.TeachSchedules.Remove(temp);
            _dbContext.SaveChanges();
        }

        public void AddSlotBooked(TeachSchedule slot)
        {
            _dbContext.TeachSchedules.Add(slot);
            _dbContext.SaveChanges();
        }

        public IEnumerable<TeachSchedule> GetAllSlotInTwoDates(DateTime StartDay, DateTime EndDay, int TutorId)
        {
            var slot = _dbContext.TeachSchedules.Where(x => x.OrderDate >= StartDay && x.OrderDate <= EndDay && x.TutorId == TutorId);
            return slot;
        }
    }
}
