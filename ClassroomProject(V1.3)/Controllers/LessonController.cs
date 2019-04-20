using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassroomProject_V1._3_.Models;

namespace ClassroomProject_V1._3_.Controllers
{
   
    public class LessonController : Controller
    {
        DBClassroomEntities db = new DBClassroomEntities();

        [HttpGet]
        public ActionResult Index()
        {
            var LesData = new LessonDTO()
            {
                LessonList = db.Lessons.ToList()
            };
            return View(LesData);
        }


        [HttpPost]
        public ActionResult Index(LessonDTO lesson)
        {
            if (lesson.LessonData.Id == 0)
            {
                db.Lessons.Add(lesson.LessonData);
                db.SaveChanges();
            }
            else
            {
                var dataLes = db.Lessons.FirstOrDefault(a => a.Id == lesson.LessonData.Id);

                dataLes.Name = lesson.LessonData.Name;
                dataLes.Status = lesson.LessonData.Status;
                dataLes.Description = lesson.LessonData.Description;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var dataForDelete = db.Lessons.FirstOrDefault(a => a.Id == id);
            db.Lessons.Remove(dataForDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var dataLes = new LessonDTO()
            {
                LessonList = db.Lessons.ToList(),
                LessonData = db.Lessons.FirstOrDefault(a => a.Id == id)
            };

            return View("Index", dataLes);
        }
    }
}