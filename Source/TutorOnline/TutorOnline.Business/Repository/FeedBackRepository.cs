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
    public class FeedBackRepository : BaseRepository
    {
        public IEnumerable<TutorFeedback> GetAllTutorFeedBack()
        {
            var temp = _dbContext.TutorFeedbacks.Include(x => x.TutorFeedbackDetails);
            return temp;
        }

        public IEnumerable<TutorFeedback> GetTutorFeedBackByLesson( int LessonId, int StudentId)
        {
            var temp = _dbContext.TutorFeedbacks.Include(x => x.TutorFeedbackDetails).Where(x => x.LessonId == LessonId && x.StudentId == StudentId).OrderByDescending(x => x.ScheduleId);
            return temp;
        }
        public IEnumerable<TutorFeedback> GetTutorFeedBackBySchedule(int ScheduleId)
        {
            var temp = _dbContext.TutorFeedbacks.Include(x => x.TutorFeedbackDetails).Where(x => x.ScheduleId == ScheduleId);
            return temp;
        }

        public void AddStudentFeedback(StudentFeedback data)
        {
            _dbContext.StudentFeedbacks.Add(data);
            _dbContext.SaveChanges();
        }

        public StudentFeedback FindFeedbackForTutor(int scheduleId)
        {
            var data = _dbContext.StudentFeedbacks.FirstOrDefault(x => x.ScheduleId == scheduleId);
            return data;
        }

        public void EditTutorFeedBack(TutorFeedback data)
        {
            _dbContext.Entry(data).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
