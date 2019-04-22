using ClassroomProject_V1._3_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClassroomProject_V1._3_.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        DBClassroomEntities db = new DBClassroomEntities();
        public ActionResult Index()
        {
            var AnnouncementData = new AnnouncementDTO()
            {
                AnnouncementList = db.Announcements.OrderByDescending(x => x.Date).Take(10).ToList()
            };
            return View(AnnouncementData);
        }
    }
}