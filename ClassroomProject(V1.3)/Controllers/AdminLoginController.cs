using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassroomProject_V1._3_.Models;

namespace ClassroomProject_V1._3_.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: AdminLogin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(Management management)
        {
            using (DBClassroomEntities db = new DBClassroomEntities())
            {
                var q1 = db.Managements.Where(x => x.Email == management.Email && x.Password == management.Password).FirstOrDefault();
                if (q1 == null)
                {
                    management.LoginErrorMessage = "Yanlış E-Posta veya Şifre, Lütfen tekrar deneyiniz.";
                    return View("Index", management);
                }
                else
                {
                    Session["UserID"] = management.Id;
                    Session["UserEmail"] = management.Email;
                    return RedirectToAction("Index", "Home");
                }

            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "AdminLogin");
        }
    }
}