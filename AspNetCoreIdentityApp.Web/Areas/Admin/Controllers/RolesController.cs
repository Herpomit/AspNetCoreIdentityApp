using AspNetCoreIdentityApp.Web.Areas.Admin.Models;
using AspNetCoreIdentityApp.Web.Extansions;
using AspNetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentityApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RolesController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Authorize(Roles = "admin,role-action")]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.Select(x => new RoleViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();

            return View(roles);
        }

        [Authorize(Roles = "role-action")]
        public IActionResult RoleCreate()
        {
            return View();
        }

        [Authorize(Roles = "role-action")]
        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleCreateViewModel request)
        {
            var result = await _roleManager.CreateAsync(new AppRole() { Name = request.Name });
            if (!result.Succeeded)
            {
                ModelState.AddModelErrorList(result.Errors);
                return View();
            }

            TempData["SuccessMessage"] = "Rol Eklenmiştir.";

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "role-action")]
        public async Task<IActionResult> RoleUpdate(int id)
        {
            var roleToUpdate = await _roleManager.FindByIdAsync(id.ToString());

            if (roleToUpdate == null)
            {
                throw new Exception("Güncellenecek Rol Bulunamamıştır.");
            }

            return View(new RoleUpdateViewModel() { Id = roleToUpdate.Id, Name = roleToUpdate.Name });
        }

        [Authorize(Roles = "role-action")]
        [HttpPost]
        public async Task<IActionResult> RoleUpdate(RoleUpdateViewModel request)
        {
            var roleToUpdate = await _roleManager.FindByIdAsync(request.Id.ToString());

            if (roleToUpdate == null)
            {
                throw new Exception("Güncellenecek Rol Bulunamamıştır.");
            }

            roleToUpdate.Name = request.Name;

            await _roleManager.UpdateAsync(roleToUpdate);

            ViewData["SuccessMessage"] = "Rol Bilgisi Başarı ile güncellendi!";

            return View();
        }

        [Authorize(Roles = "role-action")]
        public async Task<IActionResult> RoleDelete(int id)
        {
            var roleToDelete = await _roleManager.FindByIdAsync(id.ToString());
            if (roleToDelete == null)
            {
                throw new Exception("Silinecek Rol bulunamadı");
            }

            var result = await _roleManager.DeleteAsync(roleToDelete);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.Select(x => x.Description).First());
            }

            TempData["SuccessMessage"] = "Rol Silinmiştir.";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AssignRoleToUser(int id)
        {
            var curentUser = await _userManager.FindByIdAsync(id.ToString());
            ViewBag.userId = id;

            var roles = await _roleManager.Roles.ToListAsync();
            
            var userRoles = await _userManager.GetRolesAsync(curentUser);

            var roleViewModelList = new List<AssignRoleToUserViewModel>();


            foreach (var item in roles)
            {
                var assignRoleToUserViewModel = new AssignRoleToUserViewModel() { Id = item.Id, Name = item.Name };

                if (userRoles.Contains(item.Name))
                {
                    assignRoleToUserViewModel.Exist = true;
                }
                roleViewModelList.Add(assignRoleToUserViewModel);
            }


            return View(roleViewModelList);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(int userId, List<AssignRoleToUserViewModel> requestList)
        {
            var userToAssignRoles = await _userManager.FindByIdAsync(userId.ToString());


            foreach (var role in requestList)
            {
                if (role.Exist)
                {
                    await _userManager.AddToRoleAsync(userToAssignRoles,role.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(userToAssignRoles,role.Name);
                }
            }
            return RedirectToAction(nameof(HomeController.UserList),"Home");
        }
    }
}
