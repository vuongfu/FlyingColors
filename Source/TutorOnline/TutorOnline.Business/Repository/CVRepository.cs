using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorOnline.DataAccess;

namespace TutorOnline.Business.Repository
{
    public class CVRepository : BaseRepository
    {
        public IEnumerable<CV> GetAllCV()
        {
            return _dbContext.CVs;
        }
        public IEnumerable<CV> GetCVByStatus(bool isRead, bool isApproved)
        {
            return _dbContext.CVs.Where(x => (x.isRead == isRead && x.isApproved == isApproved));
        }
        public IEnumerable<CV> GetCVByStatus(bool isRead)
        {
            return _dbContext.CVs.Where(x => x.isRead == isRead);
        }
        public void AddCV(CV cv)
        {
            cv.isRead = false;
            cv.isApproved = false;
            _dbContext.CVs.Add(cv);
            _dbContext.SaveChanges();
        }
        public CV FindCV(int? id)
        {
            CV cv = _dbContext.CVs.Find(id);
            return cv;
        }
        public void ChangeStatus(CV cv)
        {
            _dbContext.Entry(cv).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}