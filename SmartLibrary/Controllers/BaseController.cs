using Microsoft.AspNet.Identity;
using SmartLibrary.Utilities.Helpers;
using SmartLibrary.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SmartLibrary.Services.Interfaces;

namespace SmartLibrary.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IAuditLogService _auditLogService;
        protected readonly ApplicationUserManager _userManager;

        public BaseController(IAuditLogService auditLogService, ApplicationUserManager userManager)
        {
            _auditLogService = auditLogService;
            _userManager = userManager;
        }

        // Method to log actions
        protected async Task LogActionAsync(string action, string entity, string details)
        {
            var userId = User.Identity.GetUserId();
            await _auditLogService.LogActionAsync(action, entity, details, userId);
        }

        public async Task LogSuccessAsync(string action, string entity, string details)
        {
            await LogActionAsync(action, entity, $"Thành công: {details}");
        }

        public async Task LogErrorAsync(string action, string entity, string errorMessage)
        {
            await LogActionAsync(action, entity, $"Thất bại: {errorMessage}");
        }

        // Handle common error response or actions
        protected ActionResult HandleError(string errorMessage)
        {
            // Log the error or handle it as needed
            return View("Error", new { message = errorMessage });
        }

        // Hàm thiết lập thông báo Toast
        protected void SetToast(string title, string message, string type)
        {
            TempData["ToastTitle"] = HttpUtility.HtmlEncode(title);    // Mã hóa nội dung
            TempData["ToastMessage"] = HttpUtility.HtmlEncode(message);
            TempData["ToastType"] = type;
        }

        public void SetSuccessToast(string message)
        {
            SetToast("Thành công", message, "success");
        }

        public void SetErrorToast(string message)
        {
            SetToast("Thất bại", message, "error");
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            // Lấy tên Controller và Action
            var controllerName = filterContext.RouteData.Values["controller"]?.ToString();
            var actionName = filterContext.RouteData.Values["action"]?.ToString();

            // Kiểm tra nếu là trang chủ (Controller là "Home" và Action là "Index")
            if (controllerName == "Home" && actionName == "Index")
            {
                // Không thiết lập Breadcrumbs cho trang chủ
                return;
            }

            var key = $"{controllerName}/{actionName}";

            // Kiểm tra và thiết lập Breadcrumbs
            if (BreadcrumbConfiguration.Breadcrumbs.TryGetValue(key, out var breadcrumbs))
            {
                ViewData["Breadcrumbs"] = breadcrumbs;
            }
            else
            {
                // Breadcrumbs mặc định nếu không tìm thấy
                ViewData["Breadcrumbs"] = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Không xác định", Url = "#", IsActive = true }
                };
            }
        }

    }

}