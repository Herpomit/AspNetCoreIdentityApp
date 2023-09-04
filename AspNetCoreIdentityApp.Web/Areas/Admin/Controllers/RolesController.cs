using AspNetCoreIdentityApp.Web.Areas.Admin.Models;
using AspNetCoreIdentityApp.Web.Extansions;
using AspNetCoreIdentityApp.Web.Models;
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

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.Select(x => new RoleViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();

            return View(roles);
        }

        public IActionResult RoleCreate()
        {
            return View();
        }

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

        public async Task<IActionResult> RoleUpdate(int id)
        {
            var roleToUpdate = await _roleManager.FindByIdAsync(id.ToString());

            if (roleToUpdate == null)
            {
                throw new Exception("Güncellenecek Rol Bulunamamıştır.");
            }

            return View(new RoleUpdateViewModel() { Id = roleToUpdate.Id, Name = roleToUpdate.Name });
        }

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
    }
}
