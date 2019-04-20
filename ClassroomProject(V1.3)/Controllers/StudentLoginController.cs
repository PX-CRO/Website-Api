using ClassroomProject_V1._3_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClassroomProject_V1._3_.Controllers
{
    public class StudentLoginController : Controller
    {
        // GET: StudentLogin
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Autherize(Student student)
        {
            using (DBClassroomEntities db = new DBClassroomEntities())
            {
                var q1 = db.Students.Where(x => x.eMail == student.eMail && x.Password == student.Password).FirstOrDefault();
                if (q1 == null)
                {
                    student.LoginErrorMessage = "Yanlış E-Posta veya Şifre, Lütfen tekrar deneyiniz.";
                    return View("Index", student);
                }
                else
                {
                    Session["StudentUserID"] = q1.Id;
                    Session["UserInf"] = "Hoşgeldiniz, " + q1.ParentName + " " + q1.LName;
                    return RedirectToAction("Index", "StudentHome", new { id = q1.Id});
                }

            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "StudentLogin");
        }
    }
}