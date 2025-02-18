using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartLibrary.Models;
using SmartLibrary.Models.EntityModels;
using AutoMapper;
using SmartLibrary.Models.ViewModels.AuditLog;
using SmartLibrary.Utilities.Helpers;
using SmartLibrary.Models.ViewModels;
using SmartLibrary.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SmartLibrary.Controllers
{
    public class AuditLogController : BaseController
    {
        private readonly ApplicationUserManager UserManager;
        public AuditLogController(IAuditLogService auditLogService, ApplicationUserManager userManager)
        : base(auditLogService, userManager) // Gọi constructor của BaseController
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            UserManager = new ApplicationUserManager(userStore);
        }

        // GET: AuditLog
        public async Task<ActionResult> Index(string searchString, string sortOrder, int? pageNumber, int pageSize = 10)
        {
            // Thiết lập thứ tự sắp xếp
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ActionSortParam = string.IsNullOrEmpty(sortOrder) ? "action_desc" : "";

            // Thiết lập trang hiện tại
            pageNumber = pageNumber ?? 1;
            var userId = User.Identity.GetUserId();
            var roles = await UserManager.GetRolesAsync(userId);
            if(roles.Contains(ModelCommons.Roles.Admin))
            {
                userId = null;
            }    
            // Lấy dữ liệu từ service
            var model = await _auditLogService.GetAuditLogs(userId, searchString, sortOrder, pageNumber.Value, pageSize);

            return View(model);
        }

        // GET: AuditLog/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var auditLog = await _auditLogService.GetAuditLogById(id);

            if (auditLog == null)
                return HttpNotFound();

            return View(auditLog);
        }
    }
}
