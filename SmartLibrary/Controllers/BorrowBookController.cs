using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using SmartLibrary.Models.ViewModels.BorrowBook;
using SmartLibrary.Utilities.Helpers;
using SmartLibrary.Services.Interfaces;
using SmartLibrary.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using static SmartLibrary.Models.ModelCommons;

namespace SmartLibrary.Controllers
{
    [Authorize]
    [AutoLogAndToast]
    public class BorrowBookController : BaseController
    {
        private readonly IBorrowBookService _borrowBookService;

        public BorrowBookController(IAuditLogService auditLogService, ApplicationUserManager userManager, IBorrowBookService borrowBookService)
            : base(auditLogService, userManager)
        {
            _borrowBookService = borrowBookService;
        }

        // GET: BorrowBooks
        public async Task<ActionResult> Index(string searchString, string sortOrder, int? pageNumber, int pageSize = 10)
        {
            // Thiết lập thứ tự sắp xếp
            ViewBag.CurrentSort = sortOrder;
            ViewBag.BorrowDateSortParam = string.IsNullOrEmpty(sortOrder) ? "borrowDate_desc" : "";

            pageNumber = pageNumber ?? 1;

            var model = await _borrowBookService.GetBorrowedBooks(searchString, sortOrder, pageNumber.Value, pageSize);

            return View(model);
        }

        // GET: BorrowBooks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var borrowBook = await _borrowBookService.GetBorrowBookById(id.Value);

            if (borrowBook == null)
                return HttpNotFound();

            return View(borrowBook);
        }

        // GET: BorrowBooks/Create
        public async Task<ActionResult> Create()
        {
            using (var context = new ApplicationDbContext())
            {
                var users = await context.Users.ToListAsync();
                var books = await context.Books.ToListAsync();
                ViewBag.Users = new SelectList(users, "Id", "FullName");
                ViewBag.Books = new SelectList(books, "BookId", "Title");
            }

            return View();
        }

        // POST: BorrowBooks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateBorrowBookViewModel borrowBookVM)
        {
            if (ModelState.IsValid)
            {
                await _borrowBookService.CreateBorrowBook(borrowBookVM);
                SetToast("Thành công", "Mượn sách thành công!", Commons.ToastType.Success);
                return RedirectToAction("Index");
            }
            using (var context = new ApplicationDbContext())
            {
                var users = await context.Users.ToListAsync();
                var books = await context.Books.ToListAsync();
                ViewBag.Users = new SelectList(users, "Id", "FullName");
                ViewBag.Books = new SelectList(books, "BookId", "Title");
            }
            return View(borrowBookVM);
        }

        // GET: BorrowBook/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var borrowBook = await _borrowBookService.GetBorrowBookEditById(id.Value);

            if (borrowBook == null)
                return HttpNotFound();
            return View(borrowBook);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditBorrowBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Tìm sách trong cơ sở dữ liệu
            var borrowBook = await _borrowBookService.GetBorrowBookEditById(model.BorrowTransactionId);

            if (borrowBook == null)
            {
                return HttpNotFound();
            }

            try
            {
                borrowBook.ReturnDate = DateTime.Now;
                borrowBook.Status = BorrowBookStatus.DaTra;
                // Lưu thay đổi vào cơ sở dữ liệu
                await _borrowBookService.EditBorrowBook(borrowBook);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi khi cập nhật: " + ex.Message);
                return View(borrowBook);
            }
        }

        // GET: BorrowBooks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var borrowBook = await _borrowBookService.GetBorrowBookById(id.Value);

            if (borrowBook == null)
                return HttpNotFound();

            return View(borrowBook);
        }

        // POST: BorrowBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var borrowBook = await _borrowBookService.GetBorrowBookById(id);

            if (borrowBook == null)
                return HttpNotFound();

            await _borrowBookService.DeleteBorrowBook(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
