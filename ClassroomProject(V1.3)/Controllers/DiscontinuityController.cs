﻿using System;
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
    public class DiscontinuityController : Controller
    {
        private DBClassroomEntities db = new DBClassroomEntities();

        // GET: Discontinuity
        public ActionResult Index()
        {
            var discontinuities = db.Discontinuities.Include(d => d.Student).OrderByDescending(x => x.Date).Take(50);
            return View(discontinuities.ToList());
        }

        // GET: Discontinuity/Create
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

        // POST: Discontinuity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Discontinuity discontinuity)
        {
            if (ModelState.IsValid)
            {
                db.Discontinuities.Add(discontinuity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Student_Id = new SelectList(db.Students, "Id", "Type", discontinuity.Student_Id);
            return View(discontinuity);
        }

        // GET: Discontinuity/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discontinuity discontinuity = db.Discontinuities.Find(id);
            if (discontinuity == null)
            {
                return HttpNotFound();
            }
            var studentss = db.Students.Select(s => new
            {
                Text = s.FName + " " + s.LName + " | " + s.TCno + " | " + s.Id,
                Value = s.Id
            }).ToList();
            ViewBag.Student_Id = new SelectList(studentss, "Value", "Text", discontinuity.Student_Id);
            return View(discontinuity);
        }

        // POST: Discontinuity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Discontinuity discontinuity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discontinuity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Student_Id = new SelectList(db.Students, "Id", "Type", discontinuity.Student_Id);
            return View(discontinuity);
        }

        // GET: Discontinuity/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discontinuity discontinuity = db.Discontinuities.Find(id);
            if (discontinuity == null)
            {
                return HttpNotFound();
            }
            return View(discontinuity);
        }

        // POST: Discontinuity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Discontinuity discontinuity = db.Discontinuities.Find(id);
            db.Discontinuities.Remove(discontinuity);
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
            Discontinuity discontinuity = db.Discontinuities.Find(id);
            if (discontinuity == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var body = "<h3>Sevgili {0},</h3><h4>{1} tarihinde devamsızlık yapmıştır.</h4><p>Açıklama:</p><p>{2}</p><p>{3}</p>";
                    var message = new MailMessage();
                    message.From = new MailAddress("pexaks@outlook.com");
                    message.Subject = "Pexax Eğitim Merkezi || Öğrenci Devamsızlık Bilgisi";
                    message.To.Add(new MailAddress(discontinuity.Student.eMail));
                    message.Body = string.Format(body, discontinuity.Student.ParentName , discontinuity.Date, discontinuity.Description, "İyi Günler || Pexax Yönetim Merkezi");
                    message.IsBodyHtml = true;

                    using (var smtp = new SmtpClient())
                    {
                        smtp.Credentials = new NetworkCredential("eposta@gmail.com ", "şifre");
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(message);
                        return RedirectToAction("Index");
                    }
                }
                TempData["msg"] = "<script>alert('E-Posta başarı ile Velisine gönderildi. ');</script>";
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
