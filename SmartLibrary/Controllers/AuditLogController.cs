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
using SmartLibrary.Models.ViewModels.AuditLog;
using SmartLibrary.Helpers;
using SmartLibrary.Models.ViewModels;

namespace SmartLibrary.Controllers
{
    public class AuditLogController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AuditLog
        public async Task<ActionResult> Index(string search, string sortOrder, int page = 1, int pageSize = 10)
        {
            // Lấy dữ liệu ban đầu
            var query = db.AuditLogs.AsQueryable();


            // Áp dụng tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(b =>
                    b.Action.Contains(search));
            }

            // Lấy tổng số lượng sách
            int totalBooks = await query.CountAsync();

            // Áp dụng sắp xếp
            query = PaginationHelper.ApplySorting(query, sortOrder, (item, order) =>
            {
                switch (order)
                {
                    case "action_desc":
                        return item.OrderByDescending(b => b.Action);
                    default:
                        return item.OrderBy(b => b.Action);
                }
            });

            // Áp dụng phân trang
            var books = PaginationHelper.ApplyPagination(query, page, pageSize);

            // Tạo ViewModel chứa dữ liệu
            var viewModel = new PagedResult<AuditLogViewModel>
            {
                Items = Mapper.Map<List<AuditLogViewModel>>(await books.ToListAsync()),
                Pagination = new PaginationInfo
                {
                    PageNumber = page,
                    PageSize = pageSize,
                    TotalItems = totalBooks
                }
            };

            return View(viewModel);
        }

        // GET: AuditLog/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditLog auditLog = await db.AuditLogs.FindAsync(id);
            if (auditLog == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<AuditLogViewModel>(auditLog));
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
