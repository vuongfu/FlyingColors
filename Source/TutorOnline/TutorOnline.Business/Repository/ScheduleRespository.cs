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
        public void DeleteSlotBookedOfTutor(int id)
        {
            Schedule temp = _dbContext.Schedules.Find(id);
            _dbContext.Schedules.Remove(temp);
            _dbContext.SaveChanges();
        }
        public void AddSlotBookedByTutor(Schedule slot)
        {
            _dbContext.Schedules.Add(slot);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Schedule> GetAllSlotInTwoDates(DateTime StartDay, DateTime EndDay, int TutorId)
        {
            var slot = _dbContext.Schedules.Where(x => x.OrderDate >= StartDay && x.OrderDate <= EndDay && x.TutorId == TutorId && x.Status == 11);
            return slot;
        }

        public IEnumerable<Schedule> GetAllSlotBookedByStudentNotStart(DateTime StartDay, DateTime EndDay, int TutorId)
        {
            var slot = _dbContext.Schedules.Where(x => x.OrderDate >= StartDay && x.OrderDate <= EndDay && x.Status == 4 && x.TutorId == TutorId);
            return slot;
        }

        public void CancelSlot(int tutorId, int OrderSlot, DateTime OrderDate, string reason)
        {
            Schedule slot = _dbContext.Schedules.FirstOrDefault(x => x.TutorId == tutorId && x.OrderSlot == OrderSlot && x.OrderDate == OrderDate);
            if (slot != null)
            {
                slot.Status = 5;
                slot.CanReason = reason;
                _dbContext.Entry(slot).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
        }

        public Schedule getSlotOfTutor(int tutorId, int OrderSlot, DateTime OrderDate)
        {
            Schedule slot = _dbContext.Schedules.FirstOrDefault(x => x.TutorId == tutorId && x.OrderSlot == OrderSlot && x.OrderDate == OrderDate);
            return slot;
        }

        public void UpdateSlot(Schedule slot)
        {
            _dbContext.Entry(slot).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public IEnumerable<Schedule> GetAllSlotBookedByStudent(DateTime StartDay, DateTime EndDay, int TutorId)
        {
            var slot = _dbContext.Schedules.Where(x => x.OrderDate >= StartDay && x.OrderDate <= EndDay && x.TutorId == TutorId && (x.Status == 4 || x.Status == 3 || x.Status == 5 || x.Status == 11));
            return slot;
        }

        public Schedule getSlotById(int id)
        {
            var temp = _dbContext.Schedules.Find(id);
            return temp;
        }
    }
}
