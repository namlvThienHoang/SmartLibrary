using SmartLibrary.Models.ViewModels.LibrarySetting;
using SmartLibrary.Services.Implementations;
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
    [CustomAuthorize(Roles = "Admin")]
    public class SettingController : BaseController
    {
        private readonly ILibrarySettingService _service;

        public SettingController(IAuditLogService auditLogService, ApplicationUserManager userManager, ILibrarySettingService service)
        : base(auditLogService, userManager) 
        {
            _service = service;
        }

        // Danh sách cấu hình
        public async Task<ActionResult> Index()
        {
            var settings = await _service.GetAllAsync();
            return View(settings);
        }

        // Thêm mới
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LibrarySettingViewModel model)
        {
            if (!ModelState.IsValid && model.DataType != "image")
            {
                return View(model);
                
            }
            if (model.DataType == "image" && model.FileUpload != null && model.FileUpload.ContentLength > 0)
            {
                string uploadFolderPath = Server.MapPath("~/Uploads/Settings");
                model.SettingValue = FileHelper.UploadFile(model.FileUpload, uploadFolderPath, "/Uploads/Settings");
            }

            await _service.AddAsync(model);
            return RedirectToAction("Index");
        }

        // Chỉnh sửa
        public async Task<ActionResult> Edit(int id)
        {
            var setting = await _service.GetByIdAsync(id);
            if (setting == null) return HttpNotFound();

            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LibrarySettingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var setting = await _service.GetByIdAsync(model.LibrarySettingId);
            if (setting == null) return HttpNotFound();

            // Nếu có ảnh mới được upload
            if (model.DataType == "image" && model.FileUpload != null && model.FileUpload.ContentLength > 0)
            {
                // Xóa ảnh cũ nếu tồn tại
                if (!string.IsNullOrEmpty(setting.SettingValue))
                {
                    FileHelper.DeleteFile(setting.SettingValue);
                }

                // Upload ảnh mới
                string uploadFolderPath = Server.MapPath("~/Uploads/Settings");
                model.SettingValue = FileHelper.UploadFile(model.FileUpload, uploadFolderPath, "/Uploads/Settings");
            }
            await _service.UpdateAsync(model);
            return RedirectToAction("Index");
        }

        // Xóa
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}