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
using SmartLibrary.Models.ViewModels.Reservation;
using Microsoft.AspNet.Identity;
using SmartLibrary.Services.Interfaces;
using SmartLibrary.Utilities.Helpers;

namespace SmartLibrary.Controllers
{
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
        public ActionResult Create()
        {
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
                SetToast("Thành công", "Đặt sách thành công!", Commons.ToastType.Success);
                return RedirectToAction("Index");
            }

            return View(reservationVM);
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
            SetToast("Thành công", "Xóa đặt sách thành công!", Commons.ToastType.Success);
            await LogActionAsync("Xóa", "Đặt sách", $"Đã xóa đặt sách có ID: {id}");
            return RedirectToAction(nameof(Index));
        }

    }
}
