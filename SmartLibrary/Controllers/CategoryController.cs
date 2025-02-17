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
using SmartLibrary.Models.ViewModels.Category;
using SmartLibrary.Utilities.Helpers;
using SmartLibrary.Models.ViewModels;
using SmartLibrary.Services.Interfaces;

namespace SmartLibrary.Controllers
{
    [CustomAuthorize]
    [AutoLogAndToast]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(IAuditLogService auditLogService, ApplicationUserManager userManager, ICategoryService categoryService)
        : base(auditLogService, userManager) // Gọi constructor của BaseController
        {
            _categoryService = categoryService;
        }

        // GET: Category
        public async Task<ActionResult> Index(string searchString, string sortOrder, int? pageNumber, int pageSize = 10)
        {
            // Thiết lập thứ tự sắp xếp
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            // Thiết lập trang hiện tại
            pageNumber = pageNumber ?? 1;

            // Lấy dữ liệu từ service
            var model = await _categoryService.GetCategories(searchString, sortOrder, pageNumber.Value, pageSize);

            return View(model);
        }

        // GET: Category/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = await _categoryService.GetCategoryById(id.Value);

            if (category == null)
                return HttpNotFound();

            return View(category);
        }

        // GET: Category/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateCategoryViewModel categoryViewModel, HttpPostedFileBase coverImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Upload ảnh bìa
                    if (coverImage != null && coverImage.ContentLength > 0)
                    {
                        string uploadFolderPath = Server.MapPath("~/Uploads/Categories");
                        categoryViewModel.CoverImage = FileHelper.UploadFile(coverImage, uploadFolderPath, "/Uploads/Categories");
                    }

                    await _categoryService.CreateCategory(categoryViewModel);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi upload ảnh: " + ex.Message);
                    return View(categoryViewModel);
                }
                
            }

            return View(categoryViewModel);
        }

        // GET: Category/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = await _categoryService.GetCategoryEditById(id.Value);
            
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditCategoryViewModel categoryViewModel, HttpPostedFileBase CoverImage)
        {

            if (!ModelState.IsValid)
            {
                return View(categoryViewModel);
            }

            // Tìm sách trong cơ sở dữ liệu
            var category = await _categoryService.GetCategoryById(categoryViewModel.Id);
            if (category == null)
            {
                return HttpNotFound();
            }

            try
            {
                // Nếu có ảnh mới được upload
                if (CoverImage != null && CoverImage.ContentLength > 0)
                {
                    // Xóa ảnh cũ nếu tồn tại
                    if (!string.IsNullOrEmpty(category.CoverImage))
                    {
                        FileHelper.DeleteFile(category.CoverImage);
                    }

                    // Upload ảnh mới
                    string uploadFolderPath = Server.MapPath("~/Uploads/Categories");
                    categoryViewModel.CoverImage = FileHelper.UploadFile(CoverImage, uploadFolderPath, "/Uploads/Categories");
                }

                // Cập nhật thông tin sách
                // Lưu thay đổi vào cơ sở dữ liệu
                await _categoryService.EditCategory(categoryViewModel);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi khi cập nhật: " + ex.Message);
                return View(categoryViewModel);
            }

            
        }

        // GET: Category/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = await _categoryService.GetCategoryById(id.Value);

            if (category == null)
                return HttpNotFound();

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var category = await _categoryService.GetCategoryById(id);

            if (category == null)
                return HttpNotFound();

            await _categoryService.DeleteCategory(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
