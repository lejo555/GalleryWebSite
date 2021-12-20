using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Web.Helpers;
using GalleryWebSite.Models.BO;
using GalleryWebSite.Models.BLL;
using System.Text.RegularExpressions;
using System.Collections;
using System.Configuration;

namespace GalleryWebSite.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        POABLL bll = new POABLL();

        public ActionResult Index()
        {
            ViewBag.vbData = "home";
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.vbData = "contact";
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            ViewBag.vbData = "contact";
            string errorMessage = string.Empty;
            try
            {
                // Validation logic
                ViewBag.successMessage = "failure";
                if (string.IsNullOrEmpty(contact.Name))
                    ModelState.AddModelError("Name", "נא להזין שם");
                if (string.IsNullOrEmpty(contact.Email) && string.IsNullOrEmpty(contact.Phone) && string.IsNullOrEmpty(contact.Mobile))
                {
                    ModelState.AddModelError("Email", "נא להזין לפחות אחד מהשלושה: דואר אלקטרוני, טלפון או טלפון נייד");
                }
                else
                {

                    if (!string.IsNullOrEmpty(contact.Email) && !Regex.IsMatch(contact.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                        ModelState.AddModelError("Email", "כתובת דואר לא תקינה");

                    if (!string.IsNullOrEmpty(contact.Phone) && !Regex.IsMatch(contact.Phone, @"^0([23489]|7[1-46-9])-?\d{7}$"))
                        //@"(\(\d{2}\) |(\d{3}-))?\d{3}-\d{4}")
                        ModelState.AddModelError("Phone", "מספר טלפון לא תקין");

                    if (!string.IsNullOrEmpty(contact.Mobile) && !Regex.IsMatch(contact.Mobile, @"^05[0-58]-?\d{7}$"))
                        ModelState.AddModelError("Mobile", "מספר טלפון נייד לא תקין");
                }
                if (string.IsNullOrEmpty(contact.Message))
                    ModelState.AddModelError("Message", "נא להזין הודעה");

                if (!ModelState.IsValid)
                    return View("Contact");


                string HtmlBody =
                    "<table width='100%' align='right' DIR='RTL'>" +
                    "<tr><td style='font-weight:bold'>שם השולח:</td><td>" + contact.Name + "</td></tr>" +
                    "<tr><td style='font-weight:bold'>כתובת דואר אלקטרוני:</td><td>" + contact.Email + "</td></tr>" +
                    "<tr><td style='font-weight:bold'>טלפון:</td><td>" + contact.Phone + "</td></tr>" +
                    "<tr><td style='font-weight:bold'>טלפון נייד:</td><td>" + contact.Mobile + "</td></tr>" +
                    "<tr><td style='font-weight:bold'>גוף ההודעה:</td></tr>" +
                    "<tr><td>" + contact.Message + "</td></tr>" +
                    "</table>";


                // Specify the from and to email address
                MailMessage mailMessage = new MailMessage
                    ("lejo555@gmail.com", "meital255@gmail.com");
                // Specify the email body
                mailMessage.Body = HtmlBody;
                mailMessage.IsBodyHtml = true;
                // Specify the email Subject
                mailMessage.Subject = "דואר נכנס עבור הגלריה של דניאלה";
                // No need to specify the SMTP settings as these 
                // are already specified in web.config
                SmtpClient smtpClient = new SmtpClient();
                // Finally send the email message using Send() method
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message; // for logging purposes
                ModelState.AddModelError("", "התרחשה תקלה בשליחת ההודעה , נסו שנית");
                return View("Contact");
            }
            //return RedirectToAction("Contact");
            ModelState.Clear();
            ViewBag.successMessage = "success";
            return View("Contact");
        }

        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// ajax method
        /// for further explanation see dal documentation
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPoaCount()
        {
            return Json(bll.GetPoaCount(), JsonRequestBehavior.AllowGet);
        }


    }
}
