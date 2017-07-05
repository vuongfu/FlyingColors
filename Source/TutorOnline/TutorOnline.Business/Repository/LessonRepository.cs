using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorOnline.DataAccess;

namespace TutorOnline.Business.Repository
{
    public class LessonRepository : BaseRepository
    {
        public IEnumerable<Lesson> GetAllLessons()
        {
            var lessons = _dbContext.Lessons.Include(x => x.Subject);
            return lessons;
        }
        public IEnumerable<Lesson> GetLesCategory(int? id)
        {
            var lessons = _dbContext.Lessons.Include(x => x.Subject).Where(x => x.Subject.CategoryId == id);
            return lessons;
        }
        public Lesson FindLesson(int? id)
        {
            Lesson lesson = _dbContext.Lessons.Find(id);
            return lesson;
        }
        public List<Lesson> FindLessons(int? id)
        {
            List<Lesson> listLesson = GetAllLessons().Where(x => x.SubjectId == id).ToList();
            return listLesson;
        }
        public void AddLesson(Lesson lesson)
        {
            lesson.isActived = true;
            _dbContext.Lessons.Add(lesson);
            _dbContext.SaveChanges();
        }
        public void EditLesson(Lesson lesson)
        {
            lesson.isActived = true;
            _dbContext.Entry(lesson).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public void DeleteLesson(int id)
        {
            _dbContext.Lessons.Where(x => x.LessonId == id).ToList().ForEach(x => x.isActived = false);
            _dbContext.SaveChanges();
        }

        public bool isExistsMaterialName(string name)
        {
            var material = _dbContext.LearningMaterials.FirstOrDefault(x => x.MaterialUrl == name);
            if (material == null)
                return false;
            else
                return true;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
