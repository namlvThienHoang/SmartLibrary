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
using Microsoft.AspNet.Identity;
using SmartLibrary.Hubs;
using Microsoft.AspNet.SignalR;

namespace SmartLibrary.Controllers
{
    [CustomAuthorize]
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
        [CustomAuthorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _bookService.GetCategoriesAsync(), "Id", "Name");
            ViewBag.Authors = new SelectList(await _bookService.GetAuthorsAsync(), "Id", "AuthorName");
            return View();
        }

        // POST: Book/Create
        [CustomAuthorize(Roles = "Admin")]
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
        [CustomAuthorize(Roles = "Admin")]
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
        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditBookViewModel model, HttpPostedFileBase CoverImage)
        {
            if (!ModelState.IsValid)
            {
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
        [CustomAuthorize(Roles = "Admin")]
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
        [CustomAuthorize(Roles = "Admin")]
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

        [HttpPost]
        public async Task<JsonResult> AddReview(int bookId, int rating, string comment)
        {
            if (rating < 1 || rating > 5 || string.IsNullOrWhiteSpace(comment))
            {
                return Json(new { success = false, message = "Vui lòng nhập đánh giá hợp lệ." });
            }
            var userId = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập để đánh giá." });
            }
            var userName = User.Identity.Name;
            var reviewId = await _bookService.AddReview(userId, bookId, rating, comment);
            var createdAt = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            var context = GlobalHost.ConnectionManager.GetHubContext<ReviewHub>();
            context.Clients.All.newReview(userName, rating, comment, createdAt);
            return Json(new { success = true, reviewId, userName, rating, comment, createdAt });
        }


        [HttpPost]
        public async Task<ActionResult> EditReview(int reviewId, int rating, string comment)
        {
            var review = await _bookService.GetBookReviewById(reviewId);
            if (review == null || review.UserName != User.Identity.Name)
            {
                return Json(new { success = false, message = "Không tìm thấy đánh giá hoặc bạn không có quyền chỉnh sửa!" });
            }
            review.Rating = rating;
            review.Review = comment;
            review.ReviewDate = DateTime.Now;
            await _bookService.EditReview(review);

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ReviewHub>();
            hubContext.Clients.All.updateReview(review.Id, review.UserName, review.Rating, review.Review, review.ReviewDate.ToString("dd/MM/yyyy HH:mm"));

            return Json(new
            {
                success = true,
                reviewId = review.Id,
                userName = review.UserName,
                rating = review.Rating,
                comment = review.Review,
                createdAt = review.ReviewDate.ToString("dd/MM/yyyy HH:mm")
            });
        }

        [HttpPost]
        public async Task<ActionResult> DeleteReview(int reviewId)
        {
            var review = await _bookService.GetBookReviewById(reviewId);
            if (review == null || (review.UserName != User.Identity.Name && !User.IsInRole("Admin")))
            {
                return Json(new { success = false, message = "Bạn không có quyền xóa đánh giá này!" });
            }

            await _bookService.DeleteReview(reviewId);

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ReviewHub>();
            hubContext.Clients.All.deleteReview(reviewId);

            return Json(new { success = true, reviewId });
        }


        public ActionResult Chat()
            {
                return View();
            }
        }
}