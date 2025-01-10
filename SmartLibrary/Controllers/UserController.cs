using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartLibrary.Models;
using SmartLibrary.Models.ViewModels.User;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmartLibrary.Controllers
{
    public class UserController : Controller
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
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    // Add any additional properties if needed
                };
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
        public ActionResult Index()
        {
            var users = UserManager.Users.ToList();
            var userViewModels = users.Select(user => Mapper.Map<UserViewModel>(user)).ToList();

            return View(userViewModels);
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

            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                // Add other properties as needed
            };

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
            return View(user);
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