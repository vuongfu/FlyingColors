using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorOnline.DataAccess;
using System.Data.Entity;

namespace TutorOnline.Business.Repository
{
    public class ScheduleRespository : BaseRepository
    {
        public List<Schedule> GetAllStudentSchedule(int? StudentId)
        {
            var schedule = _dbContext.Schedules.Where(x => x.StudentId == StudentId).ToList();
            return schedule;
        }
        public IEnumerable<Schedule> GetAllTutorSchedule(int? Tutorid)
        {
            var schedule = _dbContext.Schedules.Where(x => x.TutorId == Tutorid).ToList();
            return schedule;
        }
        public Schedule FindSchedule(int? id)
        {
            Schedule schedule = _dbContext.Schedules.Find(id);
            return schedule;
        }
        public void EditSchedule(Schedule schedule)
        {
            _dbContext.Entry(schedule).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public void DeleteSchedule(int id)
        {
            _dbContext.Schedules.Where(x => x.ScheduleId == id).ToList().ForEach(x => x.Status = 5);
            _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
        public List<Schedule> GetTutorSchedule(DateTime Date, int TutorId)
        {
                List<Schedule> slot = _dbContext.Schedules.Include(s => s.Tutor).Where(x => x.OrderDate == Date && x.TutorId == TutorId).OrderBy(s => s.OrderSlot).ToList();
                return slot;
        }

        public List<TutorSubject> GetTutorSubject(int SubjectId)
        {
            List<TutorSubject> TutorSubject = _dbContext.TutorSubjects.Where(x => x.SubjectId == SubjectId).ToList();
            return TutorSubject;
        }
    }
}
