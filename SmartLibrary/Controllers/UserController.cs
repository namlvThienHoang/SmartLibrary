using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartLibrary.Utilities.Helpers;
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
        public async Task<ActionResult> Index(string searchString, string sortOrder, int? pageNumber, int pageSize = 10)
        {
            // Thiết lập thứ tự sắp xếp
            ViewBag.CurrentSort = sortOrder;
            ViewBag.UserNameSortParam = string.IsNullOrEmpty(sortOrder) ? "username_desc" : "";

            // Lấy dữ liệu ban đầu
            var query = UserManager.Users.AsQueryable();


            // Áp dụng tìm kiếm
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(b =>
                    b.UserName.Contains(searchString));
            }

            // Lấy tổng số lượng sách
            int totalUsers = await query.CountAsync();

            // Áp dụng sắp xếp
            // Sắp xếp
            switch (sortOrder)
            {
                case "username_desc":
                    query = query.OrderByDescending(b => b.UserName);
                    break;
                default:
                    query = query.OrderBy(b => b.UserName);
                    break;
            }

            // Thiết lập trang hiện tại
            pageNumber = pageNumber ?? 1;

            // Phân trang
            var users = await query.Skip((pageNumber.Value - 1) * pageSize).Take(pageSize).ToListAsync();

            var userViewModels = Mapper.Map<List<UserViewModel>>(users);
            // Tạo ViewModel chứa dữ liệu
            var viewModel = new PagedResult<UserViewModel>
            {
                Items = userViewModels,
                Pagination = new PaginationInfo
                {
                    PageNumber = pageNumber.Value,
                    PageSize = pageSize,
                    TotalItems = totalUsers
                },
                SearchString = searchString,
                SortOrder = sortOrder
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