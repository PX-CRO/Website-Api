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
    public class PaymentController : Controller
    {
        private DBClassroomEntities db = new DBClassroomEntities();

        // GET: Payment
        public ActionResult Index()
        {
            var payments = db.Payments.Include(p => p.Student).Take(50);
            return View(payments.ToList());
        }

        // GET: Payment/Create
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

        // POST: Payment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Payment payment)
        {
            if (ModelState.IsValid)
            {
                payment.Date = Convert.ToDateTime(DateTime.Now);
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Student_Id = new SelectList(db.Students, "Id", "TCno", payment.Student_Id);
            return View(payment);
        }

        // GET: Payment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            Session["PaymentDate"] = payment.Date;
            if (payment == null)
            {
                return HttpNotFound();
            }

            var studentss = db.Students.Select(s => new
            {
                Text = s.FName + " " + s.LName + " | " + s.TCno + " | " + s.Id,
                Value = s.Id
            }).ToList();
            ViewBag.Student_Id = new SelectList(studentss, "Value", "Text", payment.Student_Id);
            return View(payment);
        }

        // POST: Payment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Payment payment)
        {
            if (ModelState.IsValid)
            {
                payment.Date = Convert.ToDateTime(Session["PaymentDate"]);
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Student_Id = new SelectList(db.Students, "Id", "TCno", payment.Student_Id);
            return View(payment);
        }

        // GET: Payment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> SendMail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var body = "<h3>Sevgili {0},</h3><h4>{1} tarihinde {2} tarafından {3} TL ödenmiştir.</h4><p>{4}</p>";
                    var message = new MailMessage();
                    message.From = new MailAddress("pexaks@outlook.com");
                    message.Subject = "Pexax Eğitim Merkezi || Öğrenci Ödeme Bilgisi";
                    message.To.Add(new MailAddress(payment.Student.eMail));
                    message.Body = string.Format(body, payment.Student.ParentName, payment.Date, payment.PayerNameSurname, payment.Total, "İyi Günler || Pexax Yönetim Merkezi");
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
