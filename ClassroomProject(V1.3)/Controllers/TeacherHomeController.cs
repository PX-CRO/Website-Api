using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassroomProject_V1._3_.Models;

namespace ClassroomProject_V1._3_.Controllers
{
    public class TeacherHomeController : Controller
    {
        DBClassroomEntities db = new DBClassroomEntities();
        // GET: TeacherHome
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Students(string searching)
        {
            var students = db.Students.Where(x => x.FName.Contains(searching) || searching == null || x.TCno.Contains(searching) || x.LName.Contains(searching)).Take(50).ToList();
            return View(students);
        }

        public ActionResult Classes()
        {
            var ClassData = new ClassDTO()
            {
                ClassList = db.Classes.ToList()
            };
            return View(ClassData);
        }

       
        public ActionResult TeacherProfile()
        {
            return View();
        }
    }
}