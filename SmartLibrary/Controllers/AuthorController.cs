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
using SmartLibrary.Models.ViewModels.Author;
using SmartLibrary.Utilities.Helpers;
using SmartLibrary.Models.ViewModels;
using SmartLibrary.Services.Interfaces;

namespace SmartLibrary.Controllers
{
    [AutoLogAndToast]
    [Authorize]
    public class AuthorController : BaseController
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuditLogService auditLogService, ApplicationUserManager userManager, IAuthorService authorService)
       : base(auditLogService, userManager) // Gọi constructor của BaseController
        {
            _authorService = authorService;
        }


        // GET: Authors
        public async Task<ActionResult> Index(string searchString, string sortOrder, int? pageNumber, int pageSize = 10)
        {
            // Thiết lập thứ tự sắp xếp
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            // Thiết lập trang hiện tại
            pageNumber = pageNumber ?? 1;

            // Lấy dữ liệu từ service
            var model = await _authorService.GetAuthors(searchString, sortOrder, pageNumber.Value, pageSize);

            return View(model);
        }

        // GET: Authors/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var author = await _authorService.GetAuthorById(id.Value);

            if (author == null)
                return HttpNotFound();

            return View(author);
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAuthorViewModel authorVM, HttpPostedFileBase AvatarImage)
        {
            if (!ModelState.IsValid)
            {
                return View(authorVM);
            }
            try
            {
                // Upload ảnh bìa
                if (AvatarImage != null && AvatarImage.ContentLength > 0)
                {
                    string uploadFolderPath = Server.MapPath("~/Uploads/Authors");
                    authorVM.AvatarImage = FileHelper.UploadFile(AvatarImage, uploadFolderPath, "/Uploads/Authors");
                }

                await _authorService.CreateAuthor(authorVM);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi khi upload ảnh: " + ex.Message);
                return View(authorVM);
            }
        }

        // GET: Authors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var author = await _authorService.GetAuthorEditById(id.Value);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditAuthorViewModel authorVM, HttpPostedFileBase AvatarImage)
        {
            if (!ModelState.IsValid)
            {
                // Trả lại view với thông báo lỗi nếu ModelState không hợp lệ
                return View(authorVM);
            }

            // Tìm sách trong cơ sở dữ liệu
            var author = await _authorService.GetAuthorById(authorVM.Id);
            if (author == null)
            {
                return HttpNotFound();
            }

            try
            {
                // Nếu có ảnh mới được upload
                if (AvatarImage != null && AvatarImage.ContentLength > 0)
                {
                    // Xóa ảnh cũ nếu tồn tại
                    if (!string.IsNullOrEmpty(author.AvatarImage))
                    {
                        FileHelper.DeleteFile(author.AvatarImage);
                    }

                    // Upload ảnh mới
                    string uploadFolderPath = Server.MapPath("~/Uploads/Authors");
                    authorVM.AvatarImage = FileHelper.UploadFile(AvatarImage, uploadFolderPath, "/Uploads/Authors");
                }

                // Cập nhật thông tin sách
                // Lưu thay đổi vào cơ sở dữ liệu
                await _authorService.EditAuthor(authorVM);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi khi cập nhật: " + ex.Message);
                return View(authorVM);
            }
        }

        // GET: Authors/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var author = await _authorService.GetAuthorById(id.Value);

            if (author == null)
                return HttpNotFound();

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var author = await _authorService.GetAuthorById(id);

            if (author == null)
                return HttpNotFound();

            await _authorService.DeleteAuthor(id);
            SetToast("Thành công", "Xóa thành công!", Commons.ToastType.Success);
            await LogActionAsync("Xóa", "Tác giả", $"Đã xóa tác giả có tên: {author.AuthorName}");
            return RedirectToAction(nameof(Index));
        }

    }
}
