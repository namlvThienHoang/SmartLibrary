using AutoMapper;
using SmartLibrary.Helpers;
using SmartLibrary.Models;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Models.ViewModels;
using SmartLibrary.Models.ViewModels.Book;
using SmartLibrary.Models.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SmartLibrary.Controllers
{
    public class BookController : BaseController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public async Task<ActionResult> Index(string search, string Title, string Publisher, string ISBN, int? AvailableCopies, string Status, string sortOrder, int page = 1, int pageSize = 10)
        {
            // Lấy dữ liệu ban đầu
            var query = _context.Books.AsQueryable();

            // Lọc dữ liệu theo điều kiện
            if (!string.IsNullOrEmpty(Title))
                query = query.Where(b => b.Title.Contains(Title));

            if (!string.IsNullOrEmpty(Publisher))
                query = query.Where(b => b.Publisher.Contains(Publisher));

            if (!string.IsNullOrEmpty(ISBN))
                query = query.Where(b => b.ISBN.Contains(ISBN));

            if (AvailableCopies.HasValue)
                query = query.Where(b => b.AvailableCopies == AvailableCopies.Value);

            if (!string.IsNullOrEmpty(Status))
            {
                if (Status == "Available")
                    query = query.Where(b => b.AvailableCopies > 0);
                else if (Status == "Unavailable")
                    query = query.Where(b => b.AvailableCopies == 0);
            }

            // Áp dụng tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(b =>
                    b.Title.Contains(search) ||
                    b.BookAuthors.Any(ba => ba.Author.AuthorName.Contains(search)));
            }

            // Lấy tổng số lượng sách
            int totalBooks = await query.CountAsync();

            // Áp dụng sắp xếp
            query = PaginationHelper.ApplySorting(query, sortOrder, (item, order) =>
            {
                switch (order)
                {
                    case "title_desc":
                        return item.OrderByDescending(b => b.Title);
                    default:
                        return item.OrderBy(b => b.Title);
                }
            });

            // Áp dụng phân trang
            var books = PaginationHelper.ApplyPagination(query, page, pageSize);

            // Tạo ViewModel chứa dữ liệu
            var viewModel = new PagedResult<BookViewModel>
            {
                Items = Mapper.Map<List<BookViewModel>>(await books.ToListAsync()),
                Pagination = new PaginationInfo
                {
                    PageNumber = page,
                    PageSize = pageSize,
                    TotalItems = totalBooks
                }
            };

            return View(viewModel);
        }


        // GET: Book/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return HttpNotFound();

            return View(Mapper.Map<BookViewModel>(book));
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewBag.Authors = new SelectList(_context.Authors, "AuthorId", "AuthorName");
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateBookViewModel model, HttpPostedFileBase CoverImage)
        {
            if (ModelState.IsValid)
            {
                // Sử dụng helper để lưu ảnh
                var savedImagePath = FileHelper.SaveFile(CoverImage, "~/Uploads/Books");

                var book = new Book
                {
                    Title = model.Title,
                    Description = model.Description,
                    Publisher = model.Publisher,
                    PublishedDate = model.PublishedDate,
                    ISBN = model.ISBN,
                    TotalCopies = model.TotalCopies,
                    AvailableCopies = model.TotalCopies, // Initially, available copies = total copies
                    CoverImage = savedImagePath,
                };

                // Xử lý quan hệ nhiều-nhiều với Author và Category
                if (model.AuthorIds != null && model.AuthorIds.Any())
                {
                    book.BookAuthors = model.AuthorIds
                        .Select(authorId => new BookAuthor { AuthorId = authorId })
                        .ToList();
                }

                if (model.CategoryIds != null && model.CategoryIds.Any())
                {
                    book.BookCategories = model.CategoryIds
                        .Select(categoryId => new BookCategory { CategoryId = categoryId })
                        .ToList();
                }

                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                SetToast("Thành công", "Thêm mới sách thành công!", Commons.ToastType.Success);
                // Log the action
                await LogActionAsync("Thêm mới", "Sách", $"Đã tạo sách có tiêu đề: {book.Title}");
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, load lại danh sách Category
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewBag.Authors = new SelectList(_context.Authors, "AuthorId", "AuthorName");
            return View(model);
        }

        // GET: Book/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return HttpNotFound();

            var bookVM = Mapper.Map<EditBookViewModel>(book);

            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName", bookVM.CategoryIds);
            ViewBag.Authors = new SelectList(_context.Authors, "AuthorId", "AuthorName", bookVM.AuthorIds);
            return View(bookVM);
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
            var book = await _context.Books
                .Include(b => b.BookAuthors)
                .Include(b => b.BookCategories)
                .FirstOrDefaultAsync(b => b.BookId == model.Id);

            if (book == null)
            {
                return HttpNotFound();
            }

            // Cập nhật thông tin cơ bản
            book.Title = model.Title;
            book.Description = model.Description;
            book.Publisher = model.Publisher;
            book.PublishedDate = model.PublishedDate;
            book.ISBN = model.ISBN;
            book.TotalCopies = model.TotalCopies;
            book.AvailableCopies = model.TotalCopies;

            // Cập nhật ảnh bìa nếu có
            if (CoverImage != null && CoverImage.ContentLength > 0)
            {
                var savedImagePath = FileHelper.SaveFile(CoverImage, "~/Uploads/Books");
                book.CoverImage = savedImagePath;
            }

            // Cập nhật số lượng có sẵn nếu tổng số lượng giảm
            if (book.AvailableCopies > book.TotalCopies)
            {
                book.AvailableCopies = book.TotalCopies;
            }

            // Cập nhật quan hệ nhiều-nhiều với Tác giả (Authors)
            if (model.AuthorIds != null)
            {
                // Xóa các tác giả hiện tại
                book.BookAuthors.Clear();

                // Thêm tác giả mới
                foreach (var authorId in model.AuthorIds)
                {
                    book.BookAuthors.Add(new BookAuthor { AuthorId = authorId, BookId = book.BookId });
                }
            }

            // Cập nhật quan hệ nhiều-nhiều với Thể loại (Categories)
            if (model.CategoryIds != null)
            {
                // Xóa các thể loại hiện tại
                book.BookCategories.Clear();

                // Thêm thể loại mới
                foreach (var categoryId in model.CategoryIds)
                {
                    book.BookCategories.Add(new BookCategory { CategoryId = categoryId, BookId = book.BookId });
                }
            }

            // Đánh dấu đối tượng đã sửa đổi
            _context.Entry(book).State = EntityState.Modified;

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();
            SetToast("Thành công", "Chỉnh sửa sách thành công!", Commons.ToastType.Success);
            await LogActionAsync("Chỉnh sửa", "Sách", $"Đã chỉnh sửa sách có tiêu đề: {book.Title}");
            return RedirectToAction(nameof(Index));
        }

        // GET: Book/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return HttpNotFound();

            return View(Mapper.Map<BookViewModel>(book));
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return HttpNotFound();

            _context.Books.Remove(book);
             await _context.SaveChangesAsync();
            SetToast("Thành công", "Xóa thành công!", Commons.ToastType.Success);
            await LogActionAsync("Xóa", "Sách", $"Đã xóa sách có tiêu đề: {book.Title}");
            return RedirectToAction(nameof(Index));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}