using Microsoft.AspNet.Identity.EntityFramework;
using SmartLibrary.Models;
using SmartLibrary.Models.ViewModels.Report;
using SmartLibrary.Services.Interfaces;
using SmartLibrary.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SmartLibrary.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class ReportController : BaseController
    {
        private readonly IReportService _service;


        private readonly ApplicationUserManager UserManager;
        private readonly ApplicationRoleManager RoleManager;

        public ReportController(IAuditLogService auditLogService,
            ApplicationUserManager userManager,
            IReportService service)
        : base(auditLogService, userManager) // Gọi constructor của BaseController
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());

            UserManager = new ApplicationUserManager(userStore);
            RoleManager = new ApplicationRoleManager(roleStore);
            _service = service;
        }
        // GET: Report
        public async Task<ActionResult> Index()
        {
            int year = DateTime.Now.Year;
            var report = new ReportViewModel
            {
                TotalBooks = _service.GetTotalBooks(),
                TotalBorrowedBooks = _service.GetTotalBorrowedBooks(),
                TotalReturnedBooks = _service.GetTotalReturnedBooks(),
                TotalMembers = await GetUsersWithUserRoleCreatedInLastWeek(),
                BookCategoryStats = await _service.GetBookCategoryStatsAsync(),
                BorrowedBooksByMonth = _service.GetBooksBorrowedByMonth(year)
            };

            return View(report);
        }

        #region helper
        public async Task<int> GetUsersWithUserRoleCreatedInLastWeek()
        {
            var userRole = await RoleManager.FindByNameAsync(ModelCommons.Roles.User);
            // Lấy danh sách các user có role "user"
            var users = UserManager.Users;

            // Lọc các user được tạo trong 1 tuần gần nhất
            var oneWeekAgo = DateTime.UtcNow.AddDays(-30);
            var totalUsers = users.Where(u => u.CreatedAt >= oneWeekAgo
            && u.Roles.Select(x => x.RoleId).Contains(userRole.Id)).Count();

            return totalUsers;
        }
        #endregion
    }
}