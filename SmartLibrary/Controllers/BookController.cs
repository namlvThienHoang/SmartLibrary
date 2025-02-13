using AutoMapper;
using SmartLibrary.Utilities.Helpers;
using SmartLibrary.Models;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Models.ViewModels;
using SmartLibrary.Models.ViewModels.Book;
using SmartLibrary.Models.ViewModels.Category;
using SmartLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace SmartLibrary.Controllers
{
    [Authorize]
    [AutoLogAndToast]
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;

        public BookController(IAuditLogService auditLogService, ApplicationUserManager userManager, IBookService bookService)
            : base(auditLogService, userManager)
        {
            _bookService = bookService;
        }

        // GET: Books
        public async Task<ActionResult> Index(string searchString, string sortOrder, int? pageNumber, int pageSize = 10)
        {
            // Thiết lập thứ tự sắp xếp
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParam = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.PublisherSortParam = sortOrder == "publisher" ? "publisher_desc" : "publisher";
            ViewBag.PublishedDateSortParam = sortOrder == "publishedDate" ? "publishedDate_desc" : "publishedDate";

            // Thiết lập trang hiện tại
            pageNumber = pageNumber ?? 1;

            // Lấy dữ liệu từ service
            var model = await _bookService.GetBooks(searchString, sortOrder, pageNumber.Value, pageSize);

            return View(model);
        }

        // GET: Book/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var book = await _bookService.GetBookById(id);

            if (book == null)
                return HttpNotFound();

            return View(book);
        }

        // GET: Book/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _bookService.GetCategoriesAsync(), "Id", "Name");
            ViewBag.Authors = new SelectList(await _bookService.GetAuthorsAsync(), "Id", "AuthorName");
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateBookViewModel model, HttpPostedFileBase coverImage)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    // Upload ảnh bìa
                    if (coverImage != null && coverImage.ContentLength > 0)
                    {
                        string uploadFolderPath = Server.MapPath("~/Uploads/Books");
                        model.CoverImage = FileHelper.UploadFile(coverImage, uploadFolderPath, "/Uploads/Books");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Vui lòng chọn ảnh bìa.");
                        return View(model);
                    }

                    await _bookService.CreateBook(model);
                    SetToast("Thành công", "Thêm mới sách thành công!", Commons.ToastType.Success);
                    await LogActionAsync("Thêm mới", "Sách", $"Đã tạo sách có tiêu đề: {model.Title}");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi upload ảnh: " + ex.Message);
                    return View(model);
                }
            }
            // Nếu ModelState không hợp lệ, load lại danh sách Category
            ViewBag.Categories = new SelectList(await _bookService.GetCategoriesAsync(), "Id", "Name");
            ViewBag.Authors = new SelectList(await _bookService.GetAuthorsAsync(), "Id", "AuthorName");
            return View(model);
        }

        // GET: Book/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var book = await _bookService.GetBookEditById(id.Value);

            if (book == null)
                return HttpNotFound();
            ViewBag.Categories = new SelectList(await _bookService.GetCategoriesAsync(), "Id", "Name", book.CategoryIds);
            ViewBag.Authors = new SelectList(await _bookService.GetAuthorsAsync(), "Id", "AuthorName", book.AuthorIds);
            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditBookViewModel model, HttpPostedFileBase CoverImage)
        {
            if (!ModelState.IsValid)
            {
                SetToast("Thất bại", "Dữ liệu không hợp lệ!", Commons.ToastType.Error);
                // Trả lại view với thông báo lỗi nếu ModelState không hợp lệ
                return View(model);
            }

            // Tìm sách trong cơ sở dữ liệu
            var book = await _bookService.GetBookEditById(model.Id);

            if (book == null)
            {
                return HttpNotFound();
            }

            try
            {
                // Nếu có ảnh mới được upload
                if (CoverImage != null && CoverImage.ContentLength > 0)
                {
                    // Xóa ảnh cũ nếu tồn tại
                    if (!string.IsNullOrEmpty(book.CoverImage))
                    {
                        FileHelper.DeleteFile(book.CoverImage);
                    }

                    // Upload ảnh mới
                    string uploadFolderPath = Server.MapPath("~/Uploads/Books");
                    model.CoverImage = FileHelper.UploadFile(CoverImage, uploadFolderPath, "/Uploads/Books");
                }

                // Cập nhật thông tin sách
                // Lưu thay đổi vào cơ sở dữ liệu
                await _bookService.EditBook(model);
                SetToast("Thành công", "Chỉnh sửa sách thành công!", Commons.ToastType.Success);
                await LogActionAsync("Chỉnh sửa", "Sách", $"Đã chỉnh sửa sách có tiêu đề: {book.Title}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi khi cập nhật: " + ex.Message);
                ViewBag.Categories = new SelectList(await _bookService.GetCategoriesAsync(), "Id", "Name", book.CategoryIds);
                ViewBag.Authors = new SelectList(await _bookService.GetAuthorsAsync(), "Id", "AuthorName", book.AuthorIds);
                return View(book);
            }
        }

        // GET: Book/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var book = await _bookService.GetBookById(id);

            if (book == null)
                return HttpNotFound();

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var book = await _bookService.GetBookById(id);

            if (book == null)
                return HttpNotFound();

            await _bookService.DeleteBook(id);
            SetToast("Thành công", "Xóa thành công!", Commons.ToastType.Success);
            await LogActionAsync("Xóa", "Sách", $"Đã xóa sách có tiêu đề: {book.Title}");
            return RedirectToAction(nameof(Index));
        }
    }
}