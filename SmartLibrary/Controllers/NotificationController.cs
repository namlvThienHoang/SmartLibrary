using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartLibrary.Models;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Models.ViewModels.Notification;
using SmartLibrary.Services.Implementations;
using SmartLibrary.Services.Interfaces;
using SmartLibrary.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static SmartLibrary.Utilities.Helpers.Commons;

namespace SmartLibrary.Controllers
{
    [CustomAuthorize]
    public class NotificationController : BaseController
    {
        private readonly INotificationService _service;
        private readonly ApplicationUserManager UserManager;

        public NotificationController(IAuditLogService auditLogService,
            ApplicationUserManager userManager,
            INotificationService notificationService)
        : base(auditLogService, userManager)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            UserManager = new ApplicationUserManager(userStore);
            _service = notificationService;
        }

        public async Task<ActionResult> Index()
        {
            string userId = User.Identity.GetUserId();
            bool isAdmin = User.IsInRole("Admin");

            var notifications = await _service.GetAllAsync(userId, isAdmin);
            return View(notifications);
        }


        public async Task<ActionResult> Details(int id)
        {
            var notification = await _service.GetByIdAsync(id);
            if (notification == null) return HttpNotFound();
            return View(notification);
        }

        [CustomAuthorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            using (var context = new ApplicationDbContext())
            {
                var users = await context.Users.ToListAsync();
                ViewBag.Users = new SelectList(users, "Id", "FullName");
            }
            ViewBag.NotificationTypes = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = NotificationType.System, Text = NotificationType.System },
                new SelectListItem { Value = NotificationType.Reminder, Text = NotificationType.Reminder },
                new SelectListItem { Value = NotificationType.Security, Text = NotificationType.Security },
                new SelectListItem { Value = NotificationType.Message, Text = NotificationType.Message    }
            }, "Value", "Text");
            return View(new CreateNotificationViewModel());
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateNotificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(model);
                return RedirectToAction("Index");
            }
            using (var context = new ApplicationDbContext())
            {
                var users = await context.Users.ToListAsync();
                ViewBag.Users = new SelectList(users, "Id", "FullName");
            }
            ViewBag.NotificationTypes = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = NotificationType.System, Text = NotificationType.System },
                new SelectListItem { Value = NotificationType.Reminder, Text = NotificationType.Reminder },
                new SelectListItem { Value = NotificationType.Security, Text = NotificationType.Security },
                new SelectListItem { Value = NotificationType.Message, Text = NotificationType.Message    }
            }, "Value", "Text");
            return View(model);
        }

        [CustomAuthorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var notification = await _service.GetByIdAsync(id);
            if (notification == null) return HttpNotFound();

            var editModel = new EditNotificationViewModel
            {
                NotificationId = notification.NotificationId,
                Title = notification.Title,
                Message = notification.Message,
                NotificationType = notification.NotificationType,
                RedirectUrl = notification.RedirectUrl,
                IsRead = notification.IsRead,
                UserIds = notification.UserIds
            };
            using (var context = new ApplicationDbContext())
            {
                var users = await context.Users.ToListAsync();
                ViewBag.Users = new SelectList(users, "Id", "FullName", editModel.UserIds);
            }
            ViewBag.NotificationTypes = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = NotificationType.System, Text = NotificationType.System },
                new SelectListItem { Value = NotificationType.Reminder, Text = NotificationType.Reminder },
                new SelectListItem { Value = NotificationType.Security, Text = NotificationType.Security },
                new SelectListItem { Value = NotificationType.Message, Text = NotificationType.Message    }
            }, "Value", "Text");
            return View(editModel);
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditNotificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(model);
                return RedirectToAction("Index");
            }
            using (var context = new ApplicationDbContext())
            {
                var users = await context.Users.ToListAsync();
                ViewBag.Users = new SelectList(users, "Id", "FullName", model.UserIds);
            }
            ViewBag.NotificationTypes = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = NotificationType.System, Text = NotificationType.System },
                new SelectListItem { Value = NotificationType.Reminder, Text = NotificationType.Reminder },
                new SelectListItem { Value = NotificationType.Security, Text = NotificationType.Security },
                new SelectListItem { Value = NotificationType.Message, Text = NotificationType.Message    }
            }, "Value", "Text");
            return View(model);
        }

        [CustomAuthorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var notification = await _service.GetByIdAsync(id);
            if (notification == null) return HttpNotFound();
            return View(notification);
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmDelete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> MarkAsRead(int id)
        {
            await _service.MarkAsReadAsync(id);
            return RedirectToAction("Index");
        }
    }

}