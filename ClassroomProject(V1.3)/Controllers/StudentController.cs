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
    public class StudentController : Controller
    {
        private DBClassroomEntities db = new DBClassroomEntities();

        // GET: Student
        public ActionResult Index(string searching)
        {
            var students = db.Students.Where(x => x.FName.Contains(searching) || searching == null || x.TCno.Contains(searching) || x.LName.Contains(searching)).ToList();
            return View(students);
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            ViewBag.Class_Id = new SelectList(db.Classes, "Id", "Name");
            ViewBag.Group_Id = new SelectList(db.Groups, "Id", "Name");
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student s)
        {
            string FileName = Path.GetFileNameWithoutExtension(s.ImageFile.FileName);
            string Extension = Path.GetExtension(s.ImageFile.FileName);

            FileName = FileName + Extension;
            s.Photo = "~/Image/Students/" + FileName;
            FileName = Path.Combine(Server.MapPath("~/Image/Students/"), FileName);
            s.ImageFile.SaveAs(FileName);
            db.Students.Add(s);
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

            ViewBag.Class_Id = new SelectList(db.Classes, "Id", "Name", s.Class_Id);
            ViewBag.Group_Id = new SelectList(db.Groups, "Id", "Name", s.Group_Id);
            return View();
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            Session["StudentImagePath"] = student.Photo;
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.Class_Id = new SelectList(db.Classes, "Id", "Name", student.Class_Id);
            ViewBag.Group_Id = new SelectList(db.Groups, "Id", "Name", student.Group_Id);
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student s)
        {
            if (ModelState.IsValid)
            {
                if (s.ImageFile != null)
                {
                    string FileName = Path.GetFileNameWithoutExtension(s.ImageFile.FileName);
                    string Extension = Path.GetExtension(s.ImageFile.FileName);

                    FileName = FileName + Extension;
                    s.Photo = "~/Image/Students/" + FileName;
                    FileName = Path.Combine(Server.MapPath("~/Image/Students/"), FileName);
                    s.ImageFile.SaveAs(FileName);
                    db.Entry(s).State = EntityState.Modified;
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
                    s.Photo = Session["StudentImagePath"].ToString();
                    db.Entry(s).State = EntityState.Modified;
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

            ViewBag.Class_Id = new SelectList(db.Classes, "Id", "Name", s.Class_Id);
            ViewBag.Group_Id = new SelectList(db.Groups, "Id", "Name", s.Group_Id);
            return View();
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
