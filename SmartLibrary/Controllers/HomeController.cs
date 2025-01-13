using SmartLibrary.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartLibrary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // GET: Contact
        public ActionResult Contact()
        {
            return View(new ContactViewModel());
        }

        // POST: Contact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Xử lý logic liên hệ (ví dụ: gửi email)
                TempData["SuccessMessage"] = "Cảm ơn bạn đã liên hệ với chúng tôi! Chúng tôi sẽ phản hồi sớm.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}