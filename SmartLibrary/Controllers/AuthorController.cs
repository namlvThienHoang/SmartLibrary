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
using SmartLibrary.Models.ViewModels.Author;
using SmartLibrary.Helpers;
using SmartLibrary.Models.ViewModels;

namespace SmartLibrary.Controllers
{
    public class AuthorController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Authors
        public async Task<ActionResult> Index(string search, string sortOrder, int page = 1, int pageSize = 10)
        {
            // Lấy dữ liệu ban đầu
            var query = db.Authors.AsQueryable();

           
            // Áp dụng tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(b =>
                    b.AuthorName.Contains(search));
            }

            // Lấy tổng số lượng sách
            int totalBooks = await query.CountAsync();

            // Áp dụng sắp xếp
            query = PaginationHelper.ApplySorting(query, sortOrder, (item, order) =>
            {
                switch (order)
                {
                    case "name_desc":
                        return item.OrderByDescending(b => b.AuthorName);
                    default:
                        return item.OrderBy(b => b.AuthorName);
                }
            });

            // Áp dụng phân trang
            var books = PaginationHelper.ApplyPagination(query, page, pageSize);

            // Tạo ViewModel chứa dữ liệu
            var viewModel = new PagedResult<AuthorViewModel>
            {
                Items = Mapper.Map<List<AuthorViewModel>>(await books.ToListAsync()),
                Pagination = new PaginationInfo
                {
                    PageNumber = page,
                    PageSize = pageSize,
                    TotalItems = totalBooks
                }
            };

            return View(viewModel);
        }

        // GET: Authors/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = await db.Authors.FindAsync(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<AuthorViewModel>(author));
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAuthorViewModel authorVM)
        {
            if (ModelState.IsValid)
            {
                var author = Mapper.Map<Author>(authorVM);
                db.Authors.Add(author);
                await db.SaveChangesAsync();
                SetToast("Thành công", "Thêm mới tác giả thành công!", Commons.ToastType.Success);
                await LogActionAsync("Thêm mới", "Tác giả", $"Đã thêm tác giả có tên: {author.AuthorName}");
                return RedirectToAction("Index");
            }

            return View(authorVM);
        }

        // GET: Authors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = await db.Authors.FindAsync(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<EditAuthorViewModel>(author));
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditAuthorViewModel author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                await db.SaveChangesAsync();
                SetToast("Thành công", "Chỉnh sửa tác giả thành công!", Commons.ToastType.Success);
                await LogActionAsync("Chỉnh sửa", "Tác giả", $"Đã chỉnh sửa tác giả có tên: {author.AuthorName}");
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = await db.Authors.FindAsync(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<AuthorViewModel>(author));
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Author author = await db.Authors.FindAsync(id);
            db.Authors.Remove(author);
            await db.SaveChangesAsync();
            SetToast("Thành công", "Xóa tác giả thành công!", Commons.ToastType.Success);
            await LogActionAsync("Xóa", "Tác giả", $"Đã xóa tác giả có tên: {author.AuthorName}");
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
