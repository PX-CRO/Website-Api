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
            var students = db.Students.Where(x => x.FName.Contains(searching) || searching == null || x.TCno.Contains(searching) || x.LName.Contains(searching)).ToList();
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

        //[HttpGet]
        //public ActionResult Questions1()
        //{
        //    var QuestionData = new QuestionDTO()
        //    {
        //        QuestionList = db.Questions.ToList()
        //    };
        //    ViewBag.Lesson_Id = new SelectList(db.Lessons, "Id", "Name");
        //    return View(QuestionData);
        //}


        //[HttpPost]
        //public ActionResult Questions1(QuestionDTO ques)
        //{
        //    if (ques.QuestionData.Id == 0)
        //    {
        //        db.Questions.Add(new Question
        //        {
        //            Question1 = ques.QuestionData.Question1,
        //            A = ques.QuestionData.A,
        //            B = ques.QuestionData.B,
        //            C = ques.QuestionData.C,
        //            D = ques.QuestionData.D,
        //            E = ques.QuestionData.E,
        //            Answer = ques.QuestionData.Answer,
        //            LessonID = ques.QuestionData.LessonID,
        //            TeacherID = Convert.ToInt32(Session["TeacherUserID"].ToString())
        //        });

        //        db.SaveChanges();
        //    }
        //    else
        //    {
        //        var dataQues = db.Questions.FirstOrDefault(a => a.Id == ques.QuestionData.Id);
        //        dataQues.Question1 = ques.QuestionData.Question1;
        //        dataQues.A = ques.QuestionData.A;
        //        dataQues.B = ques.QuestionData.B;
        //        dataQues.C = ques.QuestionData.C;
        //        dataQues.D = ques.QuestionData.D;
        //        dataQues.E = ques.QuestionData.E;
        //        dataQues.Answer = ques.QuestionData.Answer;
        //        dataQues.LessonID = ques.QuestionData.LessonID;
        //        dataQues.TeacherID = ques.QuestionData.TeacherID;
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("Questions1");
        //}


        //public ActionResult Delete(int id)
        //{
        //    var dataForDelete = db.Questions.FirstOrDefault(a => a.Id == id);
        //    db.Questions.Remove(dataForDelete);
        //    db.SaveChanges();
        //    return RedirectToAction("Questions1");
        //}

        //public ActionResult Edit(int id)
        //{
        //    var dataAnno = new QuestionDTO()
        //    {
        //        QuestionList = db.Questions.ToList(),
        //        QuestionData = db.Questions.FirstOrDefault(x => x.Id == id)
        //    };

        //    return View("Questions1", dataAnno);
        //}










        public ActionResult TeacherProfile()
        {
            return View();
        }
    }
}