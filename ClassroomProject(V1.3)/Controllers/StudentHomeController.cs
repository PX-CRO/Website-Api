using ClassroomProject_V1._3_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ClassroomProject_V1._3_.Controllers
{
    public class StudentHomeController : Controller
    {
        private DBClassroomEntities db = new DBClassroomEntities();


        public ActionResult Index()
        {
            if (Session["StudentUserID"] != null)
            {
                int id = Convert.ToInt32(Session["StudentUserID"].ToString());
                Student student = db.Students.Find(id);
                if (student == null)
                {
                    return HttpNotFound();
                }
                return View(student);
            }
            else
            {
                return RedirectToAction("Index", "StudentLogin");
            }

        }

        public ActionResult Discontinuity()
        {
            if (Session["StudentUserID"] != null)
            {
                int id = Convert.ToInt32(Session["StudentUserID"].ToString());

                var Discon = db.Discontinuities.Where(x => x.Student_Id == id).OrderByDescending(x => x.Date).Take(50).ToList();
                if (Discon == null)
                {
                    return HttpNotFound();
                }
                return View(Discon);
            }
            else
            {
                return RedirectToAction("Index", "StudentLogin");
            }
        }

        public ActionResult Grade()
        {
            if (Session["StudentUserID"] != null)
            {
                int id = Convert.ToInt32(Session["StudentUserID"].ToString());

                var Grade = db.Grades.Where(x => x.Student_Id == id).Take(50).ToList();
                if (Grade == null)
                {
                    return HttpNotFound();
                }
                return View(Grade);
            }
            else
            {
                return RedirectToAction("Index", "StudentLogin");
            }
        }

        public ActionResult Payment()
        {
            if (Session["StudentUserID"] != null)
            {
                int id = Convert.ToInt32(Session["StudentUserID"].ToString());

                var Paym = db.Payments.Where(x => x.Student_Id == id).OrderByDescending(x => x.Date).Take(50).ToList();
                if (Paym == null)
                {
                    return HttpNotFound();
                }
                return View(Paym);
            }
            else
            {
                return RedirectToAction("Index", "StudentLogin");
            }
        }

        public ActionResult Teacher()
        {
            if (Session["StudentUserID"] != null)
            {
                int id = Convert.ToInt32(Session["StudentUserID"].ToString());

                var Teach = db.Teachers.ToList();
                if (Teach == null)
                {
                    return HttpNotFound();
                }
                return View(Teach);
            }
            else
            {
                return RedirectToAction("Index", "StudentLogin");
            }
        }
    }
}