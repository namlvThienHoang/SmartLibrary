using SmartLibrary.Models.ViewModels;
using SmartLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartLibrary.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IAuditLogService auditLogService, ApplicationUserManager userManager)
        : base(auditLogService, userManager) // Gọi constructor của BaseController
        {
        }
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

        public ActionResult AccessDenied()
        {
            return View();
        }

    }
}