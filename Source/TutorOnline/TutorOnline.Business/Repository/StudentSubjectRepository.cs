using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorOnline.DataAccess;

namespace TutorOnline.Business.Repository
{
    public class StudentSubjectRepository : BaseRepository
    {
        public IEnumerable<StudentSubject> GetAllSubject()
        {
            var StudentSubjects = _dbContext.StudentSubjects.Include(x => x.Subject).Where(x => x.Status == 1);
            return StudentSubjects;
        }
        public IEnumerable<StudentSubject> GetSubById(int? SubjectId, int? StudentId)
        {
            var StudentSubjects = _dbContext.StudentSubjects.Include(x => x.Subject).Where(x => x.Status == 1 && x.SubjectId == SubjectId && x.StudentId == StudentId);
            return StudentSubjects;
        }
        public StudentSubject FindSubject(int? id)
        {
            StudentSubject StudentSubjects = _dbContext.StudentSubjects.Find(id);
            return StudentSubjects;
        }
        public List<StudentSubject> FindSubjects(int? id)
        {
            List<StudentSubject> listSub = GetAllSubject().Where(x => x.SubjectId == id).ToList();
            return listSub;
        }
        public void AddSubject(StudentSubject StudentSubject)
        {
            StudentSubject.Status = 1;
            _dbContext.StudentSubjects.Add(StudentSubject);
            _dbContext.SaveChanges();
        }
        public void EditSubject(StudentSubject StudentSubject)
        {
            _dbContext.Entry(StudentSubject).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public void DeleteSubject(int id)
        {
            _dbContext.StudentSubjects.Where(x => x.SubjectId == id).ToList().ForEach(x => x.Status = 0);
            _dbContext.SaveChanges();
        }
    }
}
