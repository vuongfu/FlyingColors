﻿using System;
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
                var tutors = _dbContext.Tutors.Include(x => x.Role).Where(x => x.isActived == true && x.Country != "Vietnam");
                return tutors;
            }
            
        }

        public Tutor FindTutor(int? id)
        {
            Tutor tutor = _dbContext.Tutors.Where(x => x.isActived == true && x.TutorId == id).FirstOrDefault();
            return tutor;
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
    }
}
