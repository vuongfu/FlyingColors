using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorOnline.DataAccess;
using System.Data.Entity;

namespace TutorOnline.Business.Repository
{
    public class CriteriaRepository : BaseRepository
    {
        public List<Criterion> GetAllCriteriaInLes(int? id)
        {
            var criteria = _dbContext.Criteria.Include(x => x.Lesson).Include(x => x.Role).Where(x => x.LessonId == id).ToList();
            return criteria;
        }
        public Criterion FindCriteria(int? id)
        {
            Criterion criteria = _dbContext.Criteria.Find(id);
            return criteria;
        }
        public void AddCriteria(Criterion criteria)
        {
            criteria.isActived = true;
            _dbContext.Criteria.Add(criteria);
            _dbContext.SaveChanges();
        }
        public void EditCriteria(Criterion criteria)
        {
            criteria.isActived = true;
            _dbContext.Entry(criteria).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public void DeleteCriteria(int id)
        {
            _dbContext.Criteria.Where(x => x.CriteriaId == id).ToList().ForEach(x => x.isActived = false);
            _dbContext.SaveChanges();
        }

        public bool isExistsCriteriaName(string name, int lesId)
        {
            var criteria = _dbContext.Criteria.Where(x => x.LessonId == lesId).FirstOrDefault(x => x.CriteriaName == name);
            if (criteria == null)
                return false;
            else
                return true;
        }

        public bool isUsedCriteriaInTuFeDetail(int criId, int lesId)
        {
            var criteria = _dbContext.TutorFeedbackDetails.Where(x => x.CriteriaId == criId && x.TutorFeedback.LessonId == lesId).FirstOrDefault();
            if (criteria == null)
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
