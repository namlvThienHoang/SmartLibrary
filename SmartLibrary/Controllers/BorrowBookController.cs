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
using SmartLibrary.Models.ViewModels.BorrowBook;
using SmartLibrary.Utilities.Helpers;
using SmartLibrary.Services.Interfaces;

namespace SmartLibrary.Controllers
{
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
        public ActionResult Create()
        {
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

            return View(borrowBookVM);
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
            SetToast("Thành công", "Xóa mượn sách thành công!", Commons.ToastType.Success);
            await LogActionAsync("Xóa", "Mượn sách", $"Đã xóa mượn sách có ID: {id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
