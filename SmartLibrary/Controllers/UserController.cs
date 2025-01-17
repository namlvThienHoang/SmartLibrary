using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartLibrary.Helpers;
using SmartLibrary.Models;
using SmartLibrary.Models.ViewModels;
using SmartLibrary.Models.ViewModels.User;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmartLibrary.Controllers
{
    public class UserController : BaseController
    {
        private ApplicationUserManager UserManager;
        public UserController()
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            UserManager = new ApplicationUserManager(userStore);
        }

        // CREATE: /User/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<ApplicationUser>(model);
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Optionally assign default role to the user after creation
                    await UserManager.AddToRoleAsync(user.Id, "User");
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }
            return View(model);
        }

        // READ: /User/Index (List of users)
        public async Task<ActionResult> Index(string search, string sortOrder, int page = 1, int pageSize = 10)
        {
            // Lấy dữ liệu ban đầu
            var query = UserManager.Users.AsQueryable();


            // Áp dụng tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(b =>
                    b.UserName.Contains(search));
            }

            // Lấy tổng số lượng sách
            int totalUsers = await query.CountAsync();

            // Áp dụng sắp xếp
            query = PaginationHelper.ApplySorting(query, sortOrder, (item, order) =>
            {
                switch (order)
                {
                    case "username_desc":
                        return item.OrderByDescending(b => b.UserName);
                    default:
                        return item.OrderBy(b => b.UserName);
                }
            });

            // Áp dụng phân trang
            var users = PaginationHelper.ApplyPagination(query, page, pageSize);

            // Tạo ViewModel chứa dữ liệu
            var viewModel = new PagedResult<UserViewModel>
            {
                Items = Mapper.Map<List<UserViewModel>>(await users.ToListAsync()),
                Pagination = new PaginationInfo
                {
                    PageNumber = page,
                    PageSize = pageSize,
                    TotalItems = totalUsers
                }
            };

            return View(viewModel);
        }

        // READ: /User/Details/{id}
        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var userViewModel = Mapper.Map<UserViewModel>(user);
            return View(userViewModel);
        }

        // UPDATE: /User/Edit/{id}
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<EditUserViewModel>(user);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.UserName = model.UserName;
                user.Email = model.Email;
                user.FullName = model.FullName;
                user.DateOfBirth = model.DateOfBirth;
                user.Address = model.Address;
                user.PhoneNumber = model.PhoneNumber;
                user.Status = model.Status;
                // Update other properties as needed

                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }
            return View(model);
        }

        // DELETE: /User/Delete/{id}
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<UserViewModel>(user));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            AddErrors(result);
            return View(user);
        }

        // Helper function to handle errors
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}