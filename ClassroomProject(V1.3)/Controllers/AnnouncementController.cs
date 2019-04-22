using ClassroomProject_V1._3_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClassroomProject_V1._3_.Controllers
{
    public class AnnouncementController : Controller
    {
        DBClassroomEntities db = new DBClassroomEntities();

        [HttpGet]
        public ActionResult Index()
        {
            var AnnouncementData = new AnnouncementDTO()
            {
                AnnouncementList = db.Announcements.OrderByDescending(x => x.Date).Take(50).ToList()
            };
            return View(AnnouncementData);
        }


        [HttpPost]
        public ActionResult Index(AnnouncementDTO anno)
        {
            if (anno.AnnouncementData.Id == 0)
            {
                db.Announcements.Add(new Announcement
                {
                    Date = DateTime.Now,
                    EntireContent = anno.AnnouncementData.EntireContent,
                    Title = anno.AnnouncementData.Title
                });
                db.SaveChanges();
            }
            else
            {
                var dataAnno = db.Announcements.FirstOrDefault(a => a.Id == anno.AnnouncementData.Id);
                dataAnno.Title = anno.AnnouncementData.Title;
                dataAnno.Date = anno.AnnouncementData.Date;
                dataAnno.EntireContent = anno.AnnouncementData.EntireContent;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var dataForDelete = db.Announcements.FirstOrDefault(a => a.Id == id);
            db.Announcements.Remove(dataForDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var dataAnno = new AnnouncementDTO()
            {
                AnnouncementList = db.Announcements.ToList(),
                AnnouncementData = db.Announcements.FirstOrDefault(x => x.Id == id)
            };

            return View("Index", dataAnno);
        }
    }
}