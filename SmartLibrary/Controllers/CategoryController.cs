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
using SmartLibrary.Models.ViewModels.Category;
using SmartLibrary.Helpers;
using SmartLibrary.Models.ViewModels;

namespace SmartLibrary.Controllers
{
    public class CategoryController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Category
        public async Task<ActionResult> Index(string search, string sortOrder, int page = 1, int pageSize = 10)
        {
            // Lấy dữ liệu ban đầu
            var query = db.Categories.AsQueryable();


            // Áp dụng tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(b =>
                    b.CategoryName.Contains(search));
            }

            // Lấy tổng số lượng sách
            int totalBooks = await query.CountAsync();

            // Áp dụng sắp xếp
            query = PaginationHelper.ApplySorting(query, sortOrder, (item, order) =>
            {
                switch (order)
                {
                    case "name_desc":
                        return item.OrderByDescending(b => b.CategoryName);
                    default:
                        return item.OrderBy(b => b.CategoryName);
                }
            });

            // Áp dụng phân trang
            var books = PaginationHelper.ApplyPagination(query, page, pageSize);

            // Tạo ViewModel chứa dữ liệu
            var viewModel = new PagedResult<CategoryViewModel>
            {
                Items = Mapper.Map<List<CategoryViewModel>>(await books.ToListAsync()),
                Pagination = new PaginationInfo
                {
                    PageNumber = page,
                    PageSize = pageSize,
                    TotalItems = totalBooks
                }
            };

            return View(viewModel);
        }

        // GET: Category/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<CategoryViewModel>(category));
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateCategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var category = Mapper.Map<Category>(categoryViewModel);
                db.Categories.Add(category);
                await db.SaveChangesAsync();
                SetToast("Thành công", "Thêm mới loại sách thành công!", Commons.ToastType.Success);
                return RedirectToAction("Index");
            }

            return View(categoryViewModel);
        }

        // GET: Category/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<CategoryViewModel>(category));
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditCategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var category = Mapper.Map<Category>(categoryViewModel);
                db.Entry(category).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(categoryViewModel);
        }

        // GET: Category/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<CategoryViewModel>(category));
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Category category = await db.Categories.FindAsync(id);
            db.Categories.Remove(category);
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
