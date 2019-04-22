using ClassroomProject_V1._3_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ClassroomProject_V1._3_.Controllers
{
    public class ForgetPasswordController : Controller
    {
        // GET: ForgetPassword
        public ActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> CheckIt(ForgetPasswordClass forgetp)
        {
            using (DBClassroomEntities db = new DBClassroomEntities())
            {
                var q1 = db.Managements.Where(x => x.Email == forgetp.eMail).FirstOrDefault();
                var q2 = db.Teachers.Where(x => x.eMail == forgetp.eMail).FirstOrDefault();
                var q3 = db.Students.Where(x => x.eMail == forgetp.eMail).FirstOrDefault();

                if (q1 == null && q2 == null && q3 == null)
                {
                    forgetp.Message = "Yanlış E-Posta, Lütfen tekrar deneyiniz.";
                    return View("Index", forgetp);
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var body = "<p>Kimden E-Posta: {0} ({1})</p><p>Mesaj:</p><p>{2}</p><p>{3}</p><p>{4}</p>";
                        var message = new MailMessage();
                        message.From = new MailAddress("pexaks@outlook.com");
                        message.Subject = "Pexax Eğitim Merkezi || Şifremi Unuttum";
                        if (q1 != null)
                        {
                            message.To.Add(new MailAddress(q1.Email));
                            message.Body = string.Format(body, "Pexax Eğitim Merkezi", "pexaks@outlook.com", "Şifreniz : " + q1.Password + " dir.","Değiştirmek istiyorsanız Lütfen yönetim ile iletişime geçiniz.","Teşekkürler, İyi Günler...");
                        }
                        else if (q2 != null)
                        {
                            message.To.Add(new MailAddress(q2.eMail));
                            message.Body = string.Format(body, "Pexax Eğitim Merkezi", "pexaks@outlook.com", "Şifreniz : " + q2.Password + " dir." ,"Değiştirmek istiyorsanız Lütfen yönetim ile iletişime geçiniz.", "Teşekkürler, İyi Günler...");
                        }
                        else if (q3 != null)
                        {
                            message.To.Add(new MailAddress(q3.eMail));
                            message.Body = string.Format(body, "Pexax Eğitim Merkezi", "pexaks@outlook.com", "Şifreniz : " + q3.Password + " dir. ","Değiştirmek istiyorsanız Lütfen yönetim ile iletişime geçiniz.", "Teşekkürler, İyi Günler...");
                        }
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
                    forgetp.Message = "E-Postanıza bir posta gönderdik. Lütfen kontrol edin.";
                    return View("Index", forgetp);
                }

            }
        }
    }
}