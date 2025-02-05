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
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateCategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategory(categoryViewModel);
                SetToast("Thành công", "Thêm mới loại sách thành công!", Commons.ToastType.Success);
                return RedirectToAction("Index");
            }

            return View(categoryViewModel);
        }

        // GET: Category/Edit/5
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
        public async Task<ActionResult> Edit(EditCategoryViewModel categoryViewModel)
        {

            if (!ModelState.IsValid)
            {
                SetToast("Thất bại", "Dữ liệu không hợp lệ!", Commons.ToastType.Error);
                // Trả lại view với thông báo lỗi nếu ModelState không hợp lệ
                return View(categoryViewModel);
            }

            // Tìm sách trong cơ sở dữ liệu
            var category = await _categoryService.GetCategoryById(categoryViewModel.Id);
            if (category == null)
            {
                return HttpNotFound();
            }

            await _categoryService.EditCategory(categoryViewModel);
            SetToast("Thành công", "Chỉnh sửa loại sách thành công!", Commons.ToastType.Success);
            await LogActionAsync("Chỉnh sửa", "Loại sách", $"Đã chỉnh sửa loại sách có tên: {category.Name}");
            return RedirectToAction(nameof(Index));
        }

        // GET: Category/Delete/5
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
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var category = await _categoryService.GetCategoryById(id);

            if (category == null)
                return HttpNotFound();

            await _categoryService.DeleteCategory(id);
            SetToast("Thành công", "Xóa thành công!", Commons.ToastType.Success);
            await LogActionAsync("Xóa", "Loại sách", $"Đã xóa loại sách có tên: {category.Name}");
            return RedirectToAction(nameof(Index));
        }
    }
}
