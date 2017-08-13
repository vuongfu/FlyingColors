using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorOnline.DataAccess;

namespace TutorOnline.Business.Repository
{
    public class TutorSubjectRepository : BaseRepository
    {
        public List<int> GetListSubjectIdOfTutor(int tutorId)
        {
            var data = _dbContext.TutorSubjects.Where(x => x.TutorId == tutorId).ToList();
            List<int> returnData = new List<int>();
            foreach(var item in data)
            {
                returnData.Add(item.SubjectId);
            }
            return returnData;
        }

        public void AddTutorSubject(TutorSubject data)
        {
            _dbContext.TutorSubjects.Add(data);
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
