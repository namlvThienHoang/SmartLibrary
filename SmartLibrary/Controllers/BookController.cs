using SmartLibrary.Models;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Models.ViewModels;
using SmartLibrary.Models.ViewModels.Book;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartLibrary.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Book
        public ActionResult Index(int page = 1, int pageSize = 10)
        {

            // Lấy tổng số lượng sách
            int totalBooks = _context.Books.Count();

            // Lấy danh sách sách theo trang
            var books = _context.Books
                          .OrderBy(b => b.Title) // Sắp xếp theo tiêu chí
                          .Skip((page - 1) * pageSize) // Bỏ qua các mục trước trang hiện tại
                          .Take(pageSize) // Lấy số mục của trang hiện tại
                          .Select(b => new BookViewModel
                          {
                              Id = b.BookId,
                              Title = b.Title,
                              Description = b.Description,
                              Publisher = b.Publisher,
                              PublishedDate = b.PublishedDate,
                              ISBN = b.ISBN,
                              TotalCopies = b.TotalCopies,
                              AvailableCopies = b.AvailableCopies,
                              CoverImage = b.CoverImage
                          })
                          .ToList();

            // Tạo ViewModel chứa dữ liệu
            var viewModel = new PagedResult<BookViewModel>
            {
                Items = books,
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalBooks
            };
            

            return View(viewModel);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            var book = _context.Books
                .Where(b => b.BookId == id)
                .Select(b => new BookViewModel
                {
                    Id = b.BookId,
                    Title = b.Title,
                    Description = b.Description,
                    Publisher = b.Publisher,
                    PublishedDate = b.PublishedDate,
                    ISBN = b.ISBN,
                    TotalCopies = b.TotalCopies,
                    AvailableCopies = b.AvailableCopies,
                    CoverImage = b.CoverImage
                }).FirstOrDefault();

            if (book == null)
                return HttpNotFound();

            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    Title = model.Title,
                    Description = model.Description,
                    Publisher = model.Publisher,
                    PublishedDate = model.PublishedDate,
                    ISBN = model.ISBN,
                    TotalCopies = model.TotalCopies,
                    AvailableCopies = model.TotalCopies, // Initially, available copies = total copies
                    CoverImage = model.CoverImage
                };

                _context.Books.Add(book);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Book added successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            var book = _context.Books.Find(id);

            if (book == null)
                return HttpNotFound();

            var model = new EditBookViewModel
            {
                Id = book.BookId,
                Title = book.Title,
                Description = book.Description,
                Publisher = book.Publisher,
                PublishedDate = book.PublishedDate,
                ISBN = book.ISBN,
                TotalCopies = book.TotalCopies,
                CoverImage = book.CoverImage
            };

            return View(model);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var book = _context.Books.Find(model.Id);

                if (book == null)
                    return HttpNotFound();

                book.Title = model.Title;
                book.Description = model.Description;
                book.Publisher = model.Publisher;
                book.PublishedDate = model.PublishedDate;
                book.ISBN = model.ISBN;
                book.TotalCopies = model.TotalCopies;
                book.CoverImage = model.CoverImage;

                // Update available copies if total copies are reduced
                if (book.AvailableCopies > book.TotalCopies)
                    book.AvailableCopies = book.TotalCopies;

                _context.Entry(book).State = EntityState.Modified; // Đánh dấu đối tượng là "Modified"
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            var book = _context.Books.Find(id);

            if (book == null)
                return HttpNotFound();

            return View(new BookViewModel
            {
                Id = book.BookId,
                Title = book.Title,
                Description = book.Description,
                Publisher = book.Publisher,
                PublishedDate = book.PublishedDate,
                ISBN = book.ISBN,
                TotalCopies = book.TotalCopies,
                AvailableCopies = book.AvailableCopies,
                CoverImage = book.CoverImage
            });
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var book = _context.Books.Find(id);

            if (book == null)
                return HttpNotFound();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}