using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ClassroomProject_V1._3_.Models;

namespace ClassroomProject_V1._3_.Controllers
{
    public class GradeController : Controller
    {
        private DBClassroomEntities db = new DBClassroomEntities();

        // GET: Grade
        public ActionResult Index()
        {
            var grades = db.Grades.Include(g => g.Student).Take(50);
            return View(grades.ToList());
        }



        // GET: Grade/Create
        public ActionResult Create()
        {
            var studentss = db.Students.Select(s => new
            {
                Text = s.FName + " " + s.LName + " | " + s.TCno + " | " + s.Id,
                Value = s.Id
            }).ToList();
            ViewBag.Student_Id = new SelectList(studentss, "Value", "Text");
            return View();
        }

        // POST: Grade/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Grade grade)
        {
            if (ModelState.IsValid)
            {

                db.Grades.Add(grade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Student_Id = new SelectList(db.Students, "Id", "TCno", grade.Student_Id);
            return View(grade);
        }

        // GET: Grade/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            var studentss = db.Students.Select(s => new
            {
                Text = s.FName + " " + s.LName + " | " + s.TCno + " | " + s.Id,
                Value = s.Id
            }).ToList();
            ViewBag.Student_Id = new SelectList(studentss, "Value", "Text", grade.Student_Id);
            return View(grade);
        }

        // POST: Grade/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Grade grade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Student_Id = new SelectList(db.Students, "Id", "TCno", grade.Student_Id);
            return View(grade);
        }

        // GET: Grade/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // POST: Grade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grade grade = db.Grades.Find(id);
            db.Grades.Remove(grade);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: Discontinuity/SendMail/5
        public async Task<ActionResult> SendMail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var body = "<h3>Sevgili {0},</h3><h4>{1} sınavında Puan ve Sıralaması şu şekildedir.</h4><p>Puan: {2}</p><p>Sıralama: {3}</p><p>{4}</p>";
                    var message = new MailMessage();
                    message.From = new MailAddress("pexaks@outlook.com");
                    message.Subject = "Pexax Eğitim Merkezi || Öğrenci Not Bilgisi";
                    message.To.Add(new MailAddress(grade.Student.eMail));
                    message.Body = string.Format(body, grade.Student.ParentName, grade.Name, grade.Mark ,grade.Ranking, "İyi Günler || Pexax Yönetim Merkezi");
                    message.IsBodyHtml = true;

                    using (var smtp = new SmtpClient())
                    {
                        smtp.Credentials = new NetworkCredential("eposta@gmail.com", "şifre");
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(message);
                        return RedirectToAction("Index");
                    }
                }
                return View("Index");
            }
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
