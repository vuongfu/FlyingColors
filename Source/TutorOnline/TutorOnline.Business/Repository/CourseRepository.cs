using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorOnline.Core.Entity;
using TutorOnline.DataAccess;

namespace TutorOnline.Business.Repository
{
    public class CourseRepository : BaseRepository
    {
        
        public IEnumerable<Course> GetAll()
        {
            return _dbContext.Courses;
        }

        public void AddOrUpdate(Course course)
        {
            if(course.CourseID == -1)
            {
                _dbContext.Courses.Add(course);
            }
            else
            {
                Course dbEntry = _dbContext.Courses.FirstOrDefault(x => x.CourseID == course.CourseID);
                if(dbEntry != null)
                {
                    dbEntry.CourseName = course.CourseName;
                    dbEntry.Description = course.Description;
                    dbEntry.EndDate = course.EndDate;
                    dbEntry.Photo = course.Photo;
                    dbEntry.Price = course.Price;
                    dbEntry.Purpose = course.Purpose;
                    dbEntry.Requirement = course.Requirement;
                    dbEntry.StartDate = course.StartDate;
                    dbEntry.SubjectID = course.SubjectID;
                    dbEntry.Title = course.Title;
                }
            }
            _dbContext.SaveChanges();
        }

        public Course DeleteCourse(int id)
        {
            Course dbEntry = _dbContext.Courses.FirstOrDefault(x => x.CourseID == id);
            if(dbEntry != null)
            {
                _dbContext.Courses.Remove(dbEntry);
                _dbContext.SaveChanges();
            }

            return dbEntry;
        }

        public Course GetCourse(int id)
        {
            return _dbContext.Courses.FirstOrDefault(x => x.CourseID == id);
        }
    }
}
