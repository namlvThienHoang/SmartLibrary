using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartLibrary.Models;
using SmartLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SmartLibrary.Controllers
{
    public class BaseController : Controller
    {
        private readonly IAuditLogService _auditLogService;
        private readonly ApplicationUserManager _userManager;

        public BaseController()
        {
            // Manually create instances if not using DI
            _auditLogService = new AuditLogService(new ApplicationDbContext());
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            _userManager = new ApplicationUserManager(userStore);
        }

        // Method to log actions
        protected async Task LogActionAsync(string action, string entity, string details)
        {
            var userId = User.Identity.GetUserId();
            await _auditLogService.LogActionAsync(action, entity, details, userId);
        }

        // Handle common error response or actions
        protected ActionResult HandleError(string errorMessage)
        {
            // Log the error or handle it as needed
            return View("Error", new { message = errorMessage });
        }
    }

}