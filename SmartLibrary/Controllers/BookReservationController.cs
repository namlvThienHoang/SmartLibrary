using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using SmartLibrary.Models.ViewModels.Reservation;
using SmartLibrary.Services.Interfaces;
using SmartLibrary.Utilities.Helpers;
using SmartLibrary.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System;
using static SmartLibrary.Models.ModelCommons;

namespace SmartLibrary.Controllers
{
    [Authorize]
    [AutoLogAndToast]
    public class BookReservationController : BaseController
    {
        public BookReservationController(IAuditLogService auditLogService, ApplicationUserManager userManager)
        : base(auditLogService, userManager) // Gọi constructor của BaseController
        {
        }

        private readonly IBookReservationService _bookReservationService;

        public BookReservationController(IAuditLogService auditLogService, ApplicationUserManager userManager, IBookReservationService bookReservationService)
            : base(auditLogService, userManager)
        {
            _bookReservationService = bookReservationService;
        }

        // GET: BookReservations
        public async Task<ActionResult> Index(string searchString, string sortOrder, int? pageNumber, int pageSize = 10)
        {
            // Thiết lập thứ tự sắp xếp
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ReservationDateSortParam = string.IsNullOrEmpty(sortOrder) ? "reservationDate_desc" : "";

            pageNumber = pageNumber ?? 1;

            var model = await _bookReservationService.GetReservations(searchString, sortOrder, pageNumber.Value, pageSize);

            return View(model);
        }

        // GET: BookReservations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reservation = await _bookReservationService.GetReservationById(id.Value);

            if (reservation == null)
                return HttpNotFound();

            return View(reservation);
        }

        // GET: BookReservations/Create
        public async  Task<ActionResult> Create()
        {
            using (var context = new ApplicationDbContext())
            {
                var users = await context.Users.ToListAsync();
                var books = await context.Books.ToListAsync();
                ViewBag.Users = new SelectList(users, "Id", "FullName");
                ViewBag.Books = new SelectList(books, "BookId", "Title");
            }
            var statuses = new List<string>
                {
                    ModelCommons.ReservationStatus.DangCho,
                    ModelCommons.ReservationStatus.DaHuy,
                    ModelCommons.ReservationStatus.HoanTat
                };

            ViewBag.Statuses = new SelectList(statuses);
            return View();
        }

        // POST: BookReservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateReservationViewModel reservationVM)
        {
            if (ModelState.IsValid)
            {
                await _bookReservationService.CreateReservation(reservationVM);
                return RedirectToAction("Index");
            }
            using (var context = new ApplicationDbContext())
            {
                var users = await context.Users.ToListAsync();
                var books = await context.Books.ToListAsync();
                ViewBag.Users = new SelectList(users, "Id", "FullName");
                ViewBag.Books = new SelectList(books, "BookId", "Title");
            }
            return View(reservationVM);
        }


        // GET: BookReservations/HuyDatTruoc/5
        public async Task<ActionResult> HuyDatTruoc(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reservation = await _bookReservationService.GetReservationEditById(id.Value);

            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: BookReservations/HuyDatTruoc/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> HuyDatTruoc(EditReservationViewModel reservationViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(reservationViewModel);
            }

            // Tìm sách trong cơ sở dữ liệu
            var category = await _bookReservationService.GetReservationEditById(reservationViewModel.ReservationId);
            if (category == null)
            {
                return HttpNotFound();
            }

            try
            {
                if(reservationViewModel.CancelDate.HasValue)
                {
                    reservationViewModel.Status = ReservationStatus.DaHuy;
                }
                // Lưu thay đổi vào cơ sở dữ liệu
                await _bookReservationService.EditReservation(reservationViewModel);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi khi cập nhật: " + ex.Message);
                return View(reservationViewModel);
            }
        }

        // GET: BookReservations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reservation = await _bookReservationService.GetReservationById(id.Value);

            if (reservation == null)
                return HttpNotFound();

            return View(reservation);
        }

        // POST: BookReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _bookReservationService.GetReservationById(id);

            if (reservation == null)
                return HttpNotFound();

            await _bookReservationService.DeleteReservation(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
