using SmartLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartLibrary.Controllers
{
    [Authorize]
    public class SettingController : BaseController
    {

        public SettingController(IAuditLogService auditLogService, ApplicationUserManager userManager)
        : base(auditLogService, userManager) // Gọi constructor của BaseController
        {
        }
        // GET: Setting
        public ActionResult Index()
        {
            return View();
        }
    }
}