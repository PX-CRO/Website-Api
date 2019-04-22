using ClassroomProject_V1._3_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClassroomProject_V1._3_.Controllers
{
    public class ClassController : Controller
    {
        DBClassroomEntities db = new DBClassroomEntities();

        [HttpGet]
        public ActionResult Index()
        {
            var ClassData = new ClassDTO()
            {
                ClassList = db.Classes.Take(50).ToList()
            };
            return View(ClassData);
        }


        [HttpPost]
        public ActionResult Index(ClassDTO clas)
        {
            if (clas.ClassData.Id == 0)
            {
                db.Classes.Add(clas.ClassData);
                db.SaveChanges();
            }
            else
            {
                var dataCla = db.Classes.FirstOrDefault(a => a.Id == clas.ClassData.Id);

                dataCla.Name = clas.ClassData.Name;
                dataCla.Description = clas.ClassData.Description;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var dataForDelete = db.Classes.FirstOrDefault(a => a.Id == id);
            db.Classes.Remove(dataForDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var dataCla = new ClassDTO()
            {
                ClassList = db.Classes.ToList(),
                ClassData = db.Classes.FirstOrDefault(a => a.Id == id)
            };

            return View("Index", dataCla);
        }
    }
}