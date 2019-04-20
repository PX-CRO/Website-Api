using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassroomProject_V1._3_.Models;

namespace ClassroomProject_V1._3_.Controllers
{
    public class TeacherController : Controller
    {
        private DBClassroomEntities db = new DBClassroomEntities();

        // GET: Teacher
        public ActionResult Index(string searching)
        {
            var teachers = db.Teachers.Where(x => x.FName.Contains(searching) || searching == null || x.TCno.Contains(searching) || x.LName.Contains(searching)).ToList();
            return View(teachers);
        }

        // GET: Teacher/Create
        public ActionResult Create()
        {
            ViewBag.Lesson_Id = new SelectList(db.Lessons, "Id", "Name");
            return View();
        }

        // POST: Teacher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Teacher teacher)
        {
            string FileName = Path.GetFileNameWithoutExtension(teacher.ImageFile.FileName);
            string Extension = Path.GetExtension(teacher.ImageFile.FileName);

            FileName = FileName + Extension;
            teacher.Photo = "~/Image/Teachers/" + FileName;
            FileName = Path.Combine(Server.MapPath("~/Image/Teachers/"), FileName);
            teacher.ImageFile.SaveAs(FileName);
            db.Teachers.Add(teacher);
            int a = db.SaveChanges();
            if (a > 0)
            {
                TempData["CreateMessage"] = "<script> alert('Başarıyla Eklendi.') </script>";
                ModelState.Clear();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["CreateMessage"] = "<script> alert('Bir hata ile karşılaştı. Lütfen tekrar deneyiniz.') </script>";
            }

            ViewBag.Lesson_Id = new SelectList(db.Lessons, "Id", "Name", teacher.Lesson_Id);
            return View(teacher);
        }

        // GET: Teacher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            Session["TeacherImagePath"] = teacher.Photo;
            if (teacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.Lesson_Id = new SelectList(db.Lessons, "Id", "Name", teacher.Lesson_Id);
            return View(teacher);
        }

        // POST: Teacher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                if (teacher.ImageFile != null)
                {
                    string FileName = Path.GetFileNameWithoutExtension(teacher.ImageFile.FileName);
                    string Extension = Path.GetExtension(teacher.ImageFile.FileName);

                    FileName = FileName + Extension;
                    teacher.Photo = "~/Image/Teachers/" + FileName;
                    FileName = Path.Combine(Server.MapPath("~/Image/Teachers/"), FileName);
                    teacher.ImageFile.SaveAs(FileName);
                    db.Entry(teacher).State = EntityState.Modified;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["UpdateMessage"] = "<script> alert('Başarıyla Güncellendi.') </script>";
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["UpdateMessage"] = "<script> alert('Bir hata oluştu') </script>";
                    }
                }
                else
                {
                    teacher.Photo = Session["TeacherImagePath"].ToString();
                    db.Entry(teacher).State = EntityState.Modified;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["UpdateMessage"] = "<script> alert('Başarıyla Güncellendi.') </script>";
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["UpdateMessage"] = "<script> alert('Bir hata oluştu') </script>";
                    }
                }
            }

            ViewBag.Lesson_Id = new SelectList(db.Lessons, "Id", "Name", teacher.Lesson_Id);
            return View(teacher);
        }

        // GET: Teacher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
