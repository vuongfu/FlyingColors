using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorOnline.DataAccess;

namespace TutorOnline.Business.Repository
{
    public class SubjectsRepository : BaseRepository
    {
        public IEnumerable<Subject> GetAllSubject()
        {
            var subjects = _dbContext.Subjects.Include(x => x.Category).Where(x => x.isActived == true);
            return subjects;
        }
        public IEnumerable<Subject> GetSubCategory(int? id)
        {
            var subjects = _dbContext.Subjects.Include(x => x.Category).Where(x => x.isActived == true && x.CategoryId == id);
            return subjects;
        }
        public Subject FindSubject(int? id)
        {
            Subject subject = _dbContext.Subjects.Find(id);
            return subject;
        }
        public List<Subject> FindSubjects(int? id)
        {
            List<Subject> listSub = GetAllSubject().Where(x => x.CategoryId == id).ToList();
            return listSub;
        }
        public void AddSubject(Subject subject)
        {
            subject.isActived = true;
            _dbContext.Subjects.Add(subject);
            _dbContext.SaveChanges();
        }
        public void EditSubject(Subject subject)
        {
            subject.isActived = true;
            _dbContext.Entry(subject).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public void DeleteSubject(int id)
        {
            _dbContext.Subjects.Where(x => x.SubjectId == id).ToList().ForEach(x => x.isActived = false);
            _dbContext.SaveChanges();
        }

        public bool isExistsSubjectName(string name)
        {
            var subject = _dbContext.Subjects.FirstOrDefault(x => x.SubjectName == name);
            if (subject == null)
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
