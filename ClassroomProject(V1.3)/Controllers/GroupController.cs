using ClassroomProject_V1._3_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClassroomProject_V1._3_.Controllers
{
    public class GroupController : Controller
    {
        DBClassroomEntities db = new DBClassroomEntities();

        [HttpGet]
        public ActionResult Index()
        {
            var GroupData = new GroupDTO()
            {
                GroupList = db.Groups.ToList()
            };
            return View(GroupData);
        }


        [HttpPost]
        public ActionResult Index(GroupDTO grup)
        {
            if (grup.GroupData.Id == 0)
            {
                db.Groups.Add(grup.GroupData);
                db.SaveChanges();
            }
            else
            {
                var dataGrup = db.Groups.FirstOrDefault(a => a.Id == grup.GroupData.Id);

                dataGrup.Name = grup.GroupData.Name;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var dataForDelete = db.Groups.FirstOrDefault(a => a.Id == id);
            db.Groups.Remove(dataForDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var dataGrup = new GroupDTO()
            {
                GroupList = db.Groups.ToList(),
                GroupData = db.Groups.FirstOrDefault(a => a.Id == id)
            };

            return View("Index", dataGrup);
        }
    }
}