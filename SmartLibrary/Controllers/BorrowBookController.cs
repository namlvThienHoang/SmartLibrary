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

namespace SmartLibrary.Controllers
{
    public class BorrowBookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BorrowBook
        public async Task<ActionResult> Index()
        {
            var borrowTransactions = db.BorrowTransactions.Include(b => b.Book).Include(b => b.User);
            return View(await borrowTransactions.ToListAsync());
        }

        // GET: BorrowBook/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowTransaction borrowTransaction = await db.BorrowTransactions.FindAsync(id);
            if (borrowTransaction == null)
            {
                return HttpNotFound();
            }
            return View(borrowTransaction);
        }

        // GET: BorrowBook/Create
        public ActionResult Create()
        {
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title");
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName");
            return View();
        }

        // POST: BorrowBook/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BorrowTransactionId,UserId,BookId,BorrowDate,DueDate,ReturnDate,Status")] BorrowTransaction borrowTransaction)
        {
            if (ModelState.IsValid)
            {
                db.BorrowTransactions.Add(borrowTransaction);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", borrowTransaction.BookId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", borrowTransaction.UserId) ;
            return View(borrowTransaction);
        }

        // GET: BorrowBook/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowTransaction borrowTransaction = await db.BorrowTransactions.FindAsync(id);
            if (borrowTransaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", borrowTransaction.BookId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", borrowTransaction.UserId);
            return View(borrowTransaction);
        }

        // POST: BorrowBook/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BorrowTransactionId,UserId,BookId,BorrowDate,DueDate,ReturnDate,Status")] BorrowTransaction borrowTransaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(borrowTransaction).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", borrowTransaction.BookId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", borrowTransaction.UserId);
            return View(borrowTransaction);
        }

        // GET: BorrowBook/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowTransaction borrowTransaction = await db.BorrowTransactions.FindAsync(id);
            if (borrowTransaction == null)
            {
                return HttpNotFound();
            }
            return View(borrowTransaction);
        }

        // POST: BorrowBook/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BorrowTransaction borrowTransaction = await db.BorrowTransactions.FindAsync(id);
            db.BorrowTransactions.Remove(borrowTransaction);
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
    }
}
