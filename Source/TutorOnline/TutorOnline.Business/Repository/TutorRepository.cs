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
            var tutors = _dbContext.Tutors.Include(x => x.Role).Where(x => x.isActived == true && x.RoleId == 7).ToList();
            return tutors;
        }
        public List<string> GetCateNameOfTutor(int tutorId)
        {
            List<string> lstCate = new List<string>();
            var objs = (from ts in _dbContext.TutorSubjects
                           join s in _dbContext.Subjects on ts.SubjectId equals s.SubjectId
                           join c in _dbContext.Categories on s.CategoryId equals c.CategoryId
                           where ts.TutorId == tutorId && ts.Status == 6
                           select c.CategoryName).Distinct().ToList();
            if(objs != null)
            {
                foreach (var item in objs)
                    lstCate.Add(item.ToString());
            }
            return lstCate;
        }
        public List<string> GetNewCateNameOfTutor(int tutorId)
        {
            List<string> lstCate = new List<string>();
            var objs = (from ts in _dbContext.TutorSubjects
                        join s in _dbContext.Subjects on ts.SubjectId equals s.SubjectId
                        join c in _dbContext.Categories on s.CategoryId equals c.CategoryId
                        where ts.TutorId == tutorId && ts.Status == 7
                        select c.CategoryName).Distinct().ToList();
            if (objs != null)
            {
                foreach (var item in objs)
                    lstCate.Add(item.ToString());
            }
            return lstCate;
        }

        public List<int> GetTutorIdWhoSignMoreSub()
        {
            List<int> lstTuId = new List<int>();
            var obj = (from ts in _dbContext.TutorSubjects
                       join t in _dbContext.Tutors on ts.TutorId equals t.TutorId
                       where t.isActived == true && ts.Status == 7 && t.RoleId == 7
                       group ts by ts.TutorId into g
                       select g.Key).ToList();
            if (obj != null)
            {
                foreach (var item in obj)
                    lstTuId.Add(item);
            }

            return lstTuId;
        }
        public IEnumerable<TutorSubject> GetTutorSubjects(int tutorId)
        {
            var tutorSub = _dbContext.TutorSubjects.Include(x => x.Subject).Where(x => x.TutorId == tutorId && x.Status == 6).ToList();
            return tutorSub;
        }
        public IEnumerable<TutorSubject> GetTutorNewSubjects(int tutorId)
        {
            var tutorSub = _dbContext.TutorSubjects.Include(x => x.Subject).Where(x => x.TutorId == tutorId && x.Status == 7).ToList();
            return tutorSub;
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
                var tutors = _dbContext.Tutors.Include(x => x.Role).Where(x => x.isActived == true && x.Country != "Vietnam");
                return tutors;
            }
            
        }
        public Tutor FindTutor(int? id)
        {
            Tutor tutor = _dbContext.Tutors.Where(x => x.isActived == true && x.TutorId == id && x.Role.RoleName == (UserCommonString.Tutor)).FirstOrDefault();
            return tutor;
        }
        public Tutor FindPreTutor(int? id)
        {
            Tutor tutor = _dbContext.Tutors.Where(x => x.isActived == true && x.TutorId == id && x.Role.RoleName == (UserCommonString.PreTutor)).FirstOrDefault();
            return tutor;
        }
        public IEnumerable<Tutor> GetAllPretutor()
        {
            var tutors = _dbContext.Tutors.Include(x => x.Role).Where(x => x.Role.RoleName == UserCommonString.PreTutor && x.isActived == true).ToList();
            return tutors;
        }
        public void ApprovedTutorSubject(int? id)
        {
            _dbContext.TutorSubjects.Where(x => x.TutorSubjectId == id).ToList().ForEach(x => x.Status = 6);
            _dbContext.SaveChanges();
        }
        public void RejectedTutorSubject(int? id)
        {
            TutorSubject ts = _dbContext.TutorSubjects.Find(id);
            _dbContext.TutorSubjects.Remove(ts);
            _dbContext.SaveChanges();
        }
        public void ApprovedPreTutor(List<int> tusubId, int? tuId)
        {
            _dbContext.Tutors.Where(x => x.TutorId == tuId).ToList().ForEach(x => x.RoleId = 7);
            _dbContext.SaveChanges();
            for(int i = 0; i < tusubId.Count(); i ++)
            {
                _dbContext.TutorSubjects.Where(x => x.TutorSubjectId == tusubId[i]).ToList().ForEach(x => x.Status = 6);
                _dbContext.SaveChanges();
            }
        }

        public void RejectedPreTutor(List<int> tusubId, int? tuId)
        {
            for (int i = 0; i < tusubId.Count(); i++)
            {
                TutorSubject ts = _dbContext.TutorSubjects.Find(tusubId[i]);
                _dbContext.TutorSubjects.Remove(ts);
                _dbContext.SaveChanges();
            }
            _dbContext.Tutors.Where(x => x.TutorId == tuId).ToList().ForEach(x => x.isActived = false);
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void DeleteSlotBooked(int id)
        {
            Schedule temp = _dbContext.Schedules.Find(id);
            _dbContext.Schedules.Remove(temp);
            _dbContext.SaveChanges();
        }

        public Decimal GetPriceOfSlot(int tutorId)
        {
            var temp = _dbContext.Tutors.FirstOrDefault(x => x.TutorId == tutorId);
            if(temp != null)
            {
                return ((Decimal)temp.Salary * 2);
            }
            return 0;
        }

        public int GetDefaultStatusIdForSlot()
        {
            var temp = _dbContext.Status.FirstOrDefault(x => x.Status1 == "schedule_available");
            if (temp != null)
                return temp.StatusId;
            return 0;
        }

        public void AddSlotBooked(Schedule slot)
        {
            _dbContext.Schedules.Add(slot);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Schedule> GetAllSlotInTwoDates(DateTime StartDay, DateTime EndDay, int TutorId)
        {
            var slot = _dbContext.Schedules.Where(x => x.OrderDate >= StartDay && x.OrderDate <= EndDay && x.TutorId == TutorId);
            return slot;
        }

        public IEnumerable<Schedule> GetAllSlotBookedByStudent(DateTime StartDay, DateTime EndDay, int TutorId)
        {
            var slot = _dbContext.Schedules.Where(x => x.OrderDate >= StartDay && x.OrderDate <= EndDay && x.TutorId == TutorId && (x.Status == 4 || x.Status == 3 || x.Status ==5 || x.Status == 11));
            return slot;
        }

        public void AddTutorSubject(TutorSubject data)
        {
            _dbContext.TutorSubjects.Add(data);
            _dbContext.SaveChanges();
        }

        public int getTutorIdByUsername(string username)
        {
            var temp = _dbContext.Tutors.FirstOrDefault(x => x.UserName == username);
            if (temp != null)
                return temp.TutorId;
            return -1;
        }
    }
}
