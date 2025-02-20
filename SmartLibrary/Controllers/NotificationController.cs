using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartLibrary.Models;
using SmartLibrary.Models.ViewModels.Notification;
using SmartLibrary.Services.Interfaces;
using SmartLibrary.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SmartLibrary.Controllers
{
    [CustomAuthorize]
    public class NotificationController : BaseController
    {
        private readonly INotificationService _notificationService;
        private readonly ApplicationUserManager UserManager;

        public NotificationController(IAuditLogService auditLogService,
            ApplicationUserManager userManager,
            INotificationService notificationService)
        : base(auditLogService, userManager)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            UserManager = new ApplicationUserManager(userStore);
            _notificationService = notificationService;
        }

        // Hiển thị danh sách thông báo
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();
            var notifications = await _notificationService.GetNotificationsByUserIdAsync(userId);
            var model = notifications.Select(n => new NotificationViewModel
            {
                NotificationId = n.NotificationId,
                Message = n.Message,
                CreatedDate = n.CreatedDate.ToString("dd/MM/yyyy HH:mm"),
                IsRead = n.IsRead
            }).ToList();

            return View(model);
        }

        // Hiển thị form tạo thông báo (chỉ Admin)
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // Xử lý tạo thông báo
        [HttpPost]
        [CustomAuthorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateNotificationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userIds = UserManager.Users.Select(x=>x.Id).ToList();

            await _notificationService.CreateNotificationForAllUsersAsync(model.Message, userIds);
            return RedirectToAction("Index");
        }

        // Hiển thị form chỉnh sửa thông báo (chỉ Admin)
        [CustomAuthorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var notification = await _notificationService.GetNotificationByIdAsync(id);
            if (notification == null)
            {
                return HttpNotFound();
            }

            var model = new EditNotificationViewModel
            {
                NotificationId = notification.NotificationId,
                Message = notification.Message
            };

            return View(model);
        }

        // Xử lý chỉnh sửa thông báo
        [HttpPost]
        [CustomAuthorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditNotificationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var notification = await _notificationService.GetNotificationByIdAsync(model.NotificationId);
            if (notification == null)
            {
                return HttpNotFound();
            }

            notification.Message = model.Message;
            await _notificationService.UpdateNotificationAsync(notification);

            return RedirectToAction("Index");
        }
        // Đánh dấu một thông báo là đã đọc
        [HttpPost]
        public async Task<JsonResult> MarkAsRead(int id)
        {
            await _notificationService.MarkNotificationAsReadAsync(id);
            return Json(new { success = true });
        }

        // Đánh dấu tất cả thông báo của admin là đã đọc
        [HttpPost]
        public async Task<JsonResult> MarkAllAsRead()
        {
            var adminId = User.Identity.GetUserId();
            await _notificationService.MarkAllNotificationsAsReadAsync(adminId);
            return Json(new { success = true });
        }

        // Xóa một thông báo
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _notificationService.DeleteNotificationAsync(id);
            return Json(new { success = true });
        }
    }

}