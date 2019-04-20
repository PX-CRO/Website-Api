using ClassroomProject_V1._3_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClassroomProject_V1._3_.Controllers
{
    public class ManagementController : Controller
    {



        DBClassroomEntities db = new DBClassroomEntities();

        [HttpGet]
        public ActionResult Index()
        {
            var ManagementData = new ManagementDTO()
            {
                ManagementList = db.Managements.ToList()
            };
            return View(ManagementData);
        }


        [HttpPost]
        public ActionResult Index(ManagementDTO manage)
        {
            if (manage.ManagementData.Id == 0)
            {
                db.Managements.Add(manage.ManagementData);
                db.SaveChanges();
            }
            else
            {
                var dataManage = db.Managements.FirstOrDefault(a => a.Id == manage.ManagementData.Id);
                dataManage.Email = manage.ManagementData.Email;
                dataManage.Password = manage.ManagementData.Password;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var dataForDelete = db.Managements.FirstOrDefault(a => a.Id == id);
            db.Managements.Remove(dataForDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var ManagementData = new ManagementDTO()
            {
                ManagementList = db.Managements.ToList(),
                ManagementData = db.Managements.FirstOrDefault(x => x.Id == id)
            };

            return View("Index", ManagementData);
        }

        public ActionResult SendMail(int id)
        {

            return View();
        }

    }
}