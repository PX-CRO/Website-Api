using ClassroomProject_V1._3_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClassroomProject_V1._3_.Controllers
{
    public class TeacherLoginController : Controller
    {
        // GET: TeacherLogin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(Teacher teacher)
        {
            using (DBClassroomEntities db = new DBClassroomEntities())
            {
                var q1 = db.Teachers.Where(x => x.eMail == teacher.eMail && x.Password == teacher.Password).FirstOrDefault();
                if (q1 == null)
                {
                    teacher.LoginErrorMessage = "Yanlış E-Posta veya Şifre, Lütfen tekrar deneyiniz.";
                    return View("Index", teacher);
                }
                else
                {
                    Session["TeacherUserID"] = q1.Id;
                    Session["UserInf"] = "Hoşgeldiniz, " + q1.FName + " " + q1.LName;
                    return RedirectToAction("Index", "TeacherHome", new { id = q1.Id });
                }

            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "TeacherLogin");
        }
    }
}