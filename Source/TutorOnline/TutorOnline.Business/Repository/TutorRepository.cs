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
    public class TutorRepository : BaseRepository
    {
        public IEnumerable<Tutor> GetAllTutor()
        {
            var tutors = _dbContext.Tutors.Include(x => x.Role).Where(x => x.isActived == true);
            return tutors;
        }
        public IEnumerable<Tutor> GetTuByCountry(string country)
        {
            if (country == new ManagerStringCommon().vn.ToString())
            {
                var tutors = _dbContext.Tutors.Include(x => x.Role).Where(x => x.isActived == true && x.Country == country);
                return tutors;
            }
            else
            {
                var tutors = _dbContext.Tutors.Include(x => x.Role).Where(x => x.isActived == true && x.Country != "Việt nam");
                return tutors;
            }
            
        }
        public IEnumerable<Tutor> GetAllPretutor()
        {
            var tutors = _dbContext.Tutors.Include(x => x.Role).Where(x => x.RoleId == 8);
            return tutors;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
