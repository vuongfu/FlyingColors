﻿using System;
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
            var lessons = _dbContext.Lessons.Include(x => x.Subject).Where(x => x.isActived == true);
            return lessons;
        }
        public IEnumerable<Lesson> GetLesCategory(int? id)
        {
            var lessons = _dbContext.Lessons.Include(x => x.Subject).Where(x => x.Subject.CategoryId == id && x.isActived == true);
            return lessons;
        }

        public IEnumerable<Lesson> GetLesInSub(int? id)
        {
            var lessons = _dbContext.Lessons.Include(x => x.Subject).Where(x => x.SubjectId == id && x.isActived == true);
            return lessons;
        }
        public Lesson FindLesson(int? id)
        {
            Lesson lesson = _dbContext.Lessons.Where(x => x.isActived == true && x.LessonId == id).FirstOrDefault();
            return lesson;
        }
        public List<Lesson> FindLessons(int? id)
        {
            List<Lesson> listLesson = GetAllLessons().Where(x => x.SubjectId == id && x.isActived == true).ToList();
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
        public bool isExistedMaterialIn(int id)
        {
            var material = _dbContext.LearningMaterials.FirstOrDefault(x => x.LessonId == id && x.isActived == true);
            if (material == null)
                return false;
            else
                return true;
        }
        public bool isExistedLessonInSchedule(int id)
        {
            var schedule = _dbContext.Schedules.FirstOrDefault(x => x.LessonId == id && x.OrderDate >= DateTime.Now);
            if (schedule == null)
                return false;
            else
                return true;
        }
        public bool isExistedQuestionIn(int id)
        {
            var question = _dbContext.Questions.FirstOrDefault(x => x.LessonId == id && x.isActived == true);
            if (question == null)
                return false;
            else
                return true;
        }
        public bool isExistedCriteriaIn(int id)
        {
            var criteria = _dbContext.Criteria.FirstOrDefault(x => x.LessonId == id && x.isActived == true);
            if (criteria == null)
                return false;
            else
                return true;
        }
        public void DeleteLesson(int id)
        {
            _dbContext.Lessons.Where(x => x.LessonId == id && x.isActived == true).ToList().ForEach(x => x.isActived = false);
            _dbContext.SaveChanges();
        }

        public bool isExistsLessonName(string name, int id)
        {
            var lesson = _dbContext.Lessons.Where(x => x.SubjectId == id && x.isActived == true).FirstOrDefault(x => x.LessonName == name);
            if (lesson == null)
                return false;
            else
                return true;
        }

        public bool isExistsLessonNameEdit(string name, int id)
        {
            var curLes = FindLesson(id);
            //Get anotherCategory
            var lesson = _dbContext.Lessons.Where(x => x.SubjectId == curLes.SubjectId && x.LessonId != id && x.isActived == true);
            //Check isExistCategoryName
            foreach (var item in lesson)
            {
                if (name != null)
                {
                    if (name.Trim() == item.LessonName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
