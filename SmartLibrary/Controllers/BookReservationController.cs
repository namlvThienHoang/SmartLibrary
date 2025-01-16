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

namespace SmartLibrary.Controllers
{
    public class BookReservationController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BookReservation
        public async Task<ActionResult> Index()
        {
            var reservations = await db.Reservations.ToListAsync();
            return View(Mapper.Map<List<ReservationViewModel>>(reservations));
        }

        // GET: BookReservation/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = await db.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<ReservationViewModel>(reservation));
        }

        // GET: BookReservation/Create
        public ActionResult Create()
        {
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title");
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName");
            return View();
        }

        // POST: BookReservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateReservationViewModel reservationVM)
        {
            if (ModelState.IsValid)
            {
                var reservation = Mapper.Map<Reservation>(reservationVM);
                db.Reservations.Add(reservation);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", reservationVM.BookId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", reservationVM.UserId);
            return View(reservationVM);
        }

        // GET: BookReservation/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = await db.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", reservation.BookId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", reservation.UserId);
            return View(Mapper.Map<EditReservationViewModel>(reservation));
        }

        // POST: BookReservation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( EditReservationViewModel reservationVM)
        {
            if (ModelState.IsValid)
            {
                var reservation = Mapper.Map<Reservation>(reservationVM);
                db.Entry(reservation).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", reservationVM.BookId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", reservationVM.UserId);
            return View(reservationVM);
        }

        // GET: BookReservation/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = await db.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: BookReservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Reservation reservation = await db.Reservations.FindAsync(id);
            db.Reservations.Remove(reservation);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: BookReservation/Status
        public async Task<ActionResult> Status()
        {
            var userId = User.Identity.GetUserId();
            var reservations = await db.Reservations
                                       .Where(r => r.UserId == userId)
                                       .Include(r => r.Book)
                                       .ToListAsync();
            return View(reservations);
        }

        public async Task<ActionResult> UpdateStatus(int reservationId, string status)
        {
            var reservation = db.Reservations.Find(reservationId);
            if (reservation == null)
            {
                return HttpNotFound();
            }

            reservation.Status = status;
            await db.SaveChangesAsync();
            return RedirectToAction("Status");
        }

    }
}
