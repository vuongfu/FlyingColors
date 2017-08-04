﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutorOnline.Business.Repository;
using TutorOnline.Common;
using TutorOnline.DataAccess;
using TutorOnline.Web.Models;

namespace TutorOnline.Web.Controllers
{
    [Authorize(Roles = UserCommonString.Tutor)]
    public class TutorController : Controller
    {
        private TutorRepository TuRes = new TutorRepository();

        static int thisWeekNumber = DateAndWeekSelection.GetIso8601WeekOfYear(DateTime.Today);
        static DateTime firstDayOfWeek = DateAndWeekSelection.FirstDateOfWeek(DateTime.Today.Year, thisWeekNumber, CultureInfo.CurrentCulture);
        
        public ActionResult TutorBookSlot()
        {
            DateTime firstDayWeek1 = firstDayOfWeek;
            ViewBag.CurrentWeek = firstDayWeek1.ToShortDateString() + " - " + firstDayWeek1.AddDays(6).ToShortDateString();

            firstDayWeek1 = firstDayWeek1.AddDays(7);
            ViewBag.FirstWeek = firstDayWeek1.ToShortDateString() + " - " + firstDayWeek1.AddDays(6).ToShortDateString();

            firstDayWeek1 = firstDayWeek1.AddDays(7);
            ViewBag.SecondWeek = firstDayWeek1.ToShortDateString() + " - " + firstDayWeek1.AddDays(6).ToShortDateString();

            ViewBag.FirstDayOfWeek = firstDayOfWeek;
            ViewBag.Today = DateTime.Now.ToShortDateString();

            string Uid = "";
            if (Request.Cookies["UserInfo"] != null)
            {
                if (Request.Cookies["UserInfo"]["UserId"] != null)
                {
                    Uid = Request.Cookies["UserInfo"]["UserId"];
                }
            }
            int tutorId = int.Parse(Uid);
            var week0 = TuRes.GetAllSlotInTwoDates(firstDayOfWeek, firstDayOfWeek.AddDays(6), tutorId);
            var week1 = TuRes.GetAllSlotInTwoDates(firstDayOfWeek.AddDays(7), firstDayOfWeek.AddDays(13), tutorId);
            var week2 = TuRes.GetAllSlotInTwoDates(firstDayOfWeek.AddDays(14), firstDayOfWeek.AddDays(20), tutorId);

            List<string> SlotOfWeek0 = MapEntityToModel(week0, firstDayOfWeek);
            List<string> SlotOfWeek1 = MapEntityToModel(week1, firstDayOfWeek.AddDays(7));
            List<string> SlotOfWeek2 = MapEntityToModel(week2, firstDayOfWeek.AddDays(14));

            ViewBag.SlotBooked0 = SlotOfWeek0;
            ViewBag.SlotBooked1 = SlotOfWeek1;
            ViewBag.SlotBooked2 = SlotOfWeek2;

            return View();
        }

        private List<string> MapEntityToModel(IEnumerable<Schedule> ListData, DateTime StartDate)
        {
            List<string> returnList = new List<string>();
            string[] namesDays = DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames;
            
            foreach (var item in ListData)
            {
                string day = namesDays[(item.OrderDate - StartDate).Days];
                string temp = day + String.Format("{0:00}", item.OrderSlot);
                returnList.Add(temp);
            }
            return returnList; 
        }

        [HttpPost]
        public ActionResult SaveSlot(List<string> Week0, List<string> Week1, List<string> Week2)
        {
            List<TutorBookSlotViewModel> SlotBooked = new List<TutorBookSlotViewModel>();
            if (Week0 != null)
            {
                foreach (string item in Week0)
                {
                    TutorBookSlotViewModel tempData = MapDataFromView(item, firstDayOfWeek);
                    SlotBooked.Add(tempData);
                }
            }

            if (Week1 != null)
            {
                foreach (string item in Week1)
                {
                    TutorBookSlotViewModel tempData = MapDataFromView(item, firstDayOfWeek.AddDays(7));
                    SlotBooked.Add(tempData);
                }
            }

            if (Week2 != null)
            {
                foreach (string item in Week2)
                {
                    TutorBookSlotViewModel tempData = MapDataFromView(item, firstDayOfWeek.AddDays(14));
                    SlotBooked.Add(tempData);
                }
            }
                

            string Uid = "";
            if (Request.Cookies["UserInfo"] != null)
            {
                if (Request.Cookies["UserInfo"]["UserId"] != null)
                {
                    Uid = Request.Cookies["UserInfo"]["UserId"];
                }
            }
            int tutorId = int.Parse(Uid);
            var SlotBookedGetFromDB = TuRes.GetAllSlotInTwoDates(firstDayOfWeek, firstDayOfWeek.AddDays(20), tutorId);

            foreach(var item in SlotBooked)
            {
                var orderslot = int.Parse(item.OrderSlot);
                var Slot = SlotBookedGetFromDB.FirstOrDefault(x => x.OrderDate == item.OrderDate && x.OrderSlot == orderslot && x.TutorId == item.TutorId);
                if (Slot == null)
                {
                    Schedule data = new Schedule();
                    data.TutorId = item.TutorId;
                    data.OrderSlot = orderslot;
                    data.OrderDate = item.OrderDate;
                    data.Price = TuRes.GetPriceOfSlot(item.TutorId);
                    data.Status = TuRes.GetDefaultStatusIdForSlot();
                    TuRes.AddSlotBooked(data);
                }
            }

            List<int> DeleteList = new List<int>();
            foreach(var item in SlotBookedGetFromDB)
            {
                var orderslot = String.Format("{0:00}", item.OrderSlot);
                var check = SlotBooked.FirstOrDefault(x => x.OrderDate == item.OrderDate && x.OrderSlot == orderslot && x.TutorId == item.TutorId);
                if (check == null)
                {
                    DeleteList.Add(item.ScheduleId);
                }
            }

            foreach(int id in DeleteList)
            {
                TuRes.DeleteSlotBooked(id);
            }

            return Json("Lưu thành công", JsonRequestBehavior.AllowGet);
        }

        public TutorBookSlotViewModel MapDataFromView(string data, DateTime startDate)
        {
            TutorBookSlotViewModel temp = new TutorBookSlotViewModel();
            string Day = data.Substring(0, 3);
            string Slot = data.Substring(3);
            switch (Day)
            {
                case "Sun":
                    temp.OrderDate = startDate;
                    break;
                case "Mon":
                    temp.OrderDate = startDate.AddDays(1);
                    break;
                case "Tue":
                    temp.OrderDate = startDate.AddDays(2);
                    break;
                case "Wed":
                    temp.OrderDate = startDate.AddDays(3);
                    break;
                case "Thu":
                    temp.OrderDate = startDate.AddDays(4);
                    break;
                case "Fri":
                    temp.OrderDate = startDate.AddDays(5);
                    break;
                case "Sat":
                    temp.OrderDate = startDate.AddDays(6);
                    break;
                default:
                    break;
            }
            temp.OrderSlot = Slot;

            string Uid = "";
            if (Request.Cookies["UserInfo"] != null)
            {
                if (Request.Cookies["UserInfo"]["UserId"] != null)
                {
                    Uid = Request.Cookies["UserInfo"]["UserId"];
                }
            }

            temp.TutorId = int.Parse(Uid);
            return temp;
        }

        public ActionResult ViewSchedule()
        {

            DateTime firstDayWeek1 = firstDayOfWeek;
            ViewBag.CurrentWeek = firstDayWeek1.ToShortDateString() + " - " + firstDayWeek1.AddDays(6).ToShortDateString();

            firstDayWeek1 = firstDayWeek1.AddDays(7);
            ViewBag.FirstWeek = firstDayWeek1.ToShortDateString() + " - " + firstDayWeek1.AddDays(6).ToShortDateString();

            firstDayWeek1 = firstDayWeek1.AddDays(7);
            ViewBag.SecondWeek = firstDayWeek1.ToShortDateString() + " - " + firstDayWeek1.AddDays(6).ToShortDateString();

            ViewBag.FirstDayOfWeek = firstDayOfWeek;

            string Uid = "";
            if (Request.Cookies["UserInfo"] != null)
            {
                if (Request.Cookies["UserInfo"]["UserId"] != null)
                {
                    Uid = Request.Cookies["UserInfo"]["UserId"];
                }
            }

            int tutorId = int.Parse(Uid);

            ViewBag.tutorId = tutorId;

            return View();
        }

        [HttpPost]
        public ActionResult GetBookedSlotByStudent(DateTime startDate, DateTime endDate, int TutorId)
        {
            var week0 = TuRes.GetAllSlotBookedByStudent(startDate, endDate, TutorId);
            List<BookedSlotByStudent> returnData = new List<BookedSlotByStudent>();
            List<string> SlotOfWeek0 = MapEntityToModel(week0, startDate);

            for (int i = 0; i < week0.Count(); i++)
            {
                BookedSlotByStudent temp = new BookedSlotByStudent();
                temp.Status = week0.ElementAt(i).Status;
                temp.tableSlotId = SlotOfWeek0[i];
                if(temp.Status != 11)
                {
                    temp.ScheduleId = week0.ElementAt(i).ScheduleId;
                    temp.StudentName = week0.ElementAt(i).Student.FirstName;
                    temp.LessionName = week0.ElementAt(i).Lesson.LessonName;
                }                               
                returnData.Add(temp);
            }

            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewDetailSlotBooked(int id)
        {
            return View();
        }

    }
}