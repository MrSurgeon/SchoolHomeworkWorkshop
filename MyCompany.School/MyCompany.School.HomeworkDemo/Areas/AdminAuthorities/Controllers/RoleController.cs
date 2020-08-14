using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCompany.School.HomeworkDemo.Areas.AdminAuthorities.Models.RoleModel;
using MyCompany.School.HomeworkDemo.Security;

namespace MyCompany.School.HomeworkDemo.Areas.Admins.Controllers
{
    [Area("AdminAuthorities")]
    public class RoleController : Controller
    {
        private RoleManager<SchoolRole> _roleManager;
        public RoleController(RoleManager<SchoolRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET:Role List
        public async Task<IActionResult> RoleList()
        {
            var result = new RoleListViewModel()
            {
                Roles = await _roleManager.Roles.ToListAsync()
            };
            if (result == null)
            {
                ModelState.AddModelError("ListHata", "Model hatası");
                return RedirectToAction("Index");
            }
            return View(result);
        }

        // GET: RoleList
        [HttpPost]
        public async Task<IActionResult> RoleList(string Key)
        {
            if (Key == null)
            {
                ModelState.AddModelError("IdHata", "Doğru bir arama değeri girmediniz!!");
            }

            // Bu kısımda input değeri değiştikçe arama değerinin değişmesini sağlayan bir method tanımla

            var model = new RoleListViewModel()
            {
                Roles = await _roleManager.Roles.Where(a => a.Name.Contains(Key)).ToListAsync<SchoolRole>()
            };
            if (model == null)
            {
                ModelState.AddModelError("AnythingRoleFind", "Can't Any Find Roles");
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: RoleAdd
        public IActionResult RoleAdd()
        {
            return View();
        }

        // POST:RoleAdd
        [HttpPost]
        public async Task<IActionResult> RoleAdd(AdminRoleAddModel roleAdminAddModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("RoleAddModelError", "Your information is don't valid");
                return View("RoleAdd");
            }

            var result = await _roleManager.CreateAsync(roleAdminAddModel.Role);

            if (!result.Succeeded)
            {
                return View(roleAdminAddModel);
            }
            return RedirectToAction("RoleList");
        }

        // GET: Role Edit
        public async Task<IActionResult> RoleEdit(string id)
        {
            if (id == null)
            {
                ModelState.AddModelError("NotFoundValueOfId", "Id is not have a value");
            }
            var role = new AdminRoleAddModel()
            {
                Role = await _roleManager.FindByIdAsync(id)
            };

            if (role == null)
            {
                ModelState.AddModelError(String.Empty, "User Update Error! Not Found A Role");
            }
            return View(role);
        }
        // GET: Role Details
        public async Task<IActionResult> RoleDetails(string id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "Id don't have a value");
                return RedirectToAction("RoleList");
            }
            var role = new AdminRoleAddModel
            { Role = await _roleManager.FindByIdAsync(id) };

            if (role == null)
            {
                ModelState.AddModelError(string.Empty, "Role is not avaliable");
                return RedirectToAction("RoleList");
            }

            return View(role);
        }

        // GET: Role Delete
        [HttpGet]
        public async Task<IActionResult> RoleDelete(string id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "Id is not have a value");
                return RedirectToAction("RoleList");
            }


            var role = new AdminRoleAddModel()
            {
                Role = await _roleManager.FindByIdAsync(id)
            };

            if (role == null)
            {
                ModelState.AddModelError(string.Empty, "Can't Find Role Anything");
                return RedirectToAction("RoleList");
            }
            return View(role);
        }

        //POST: Role Edit
        [HttpPost]
        public async Task<IActionResult> RoleEdit(string id, AdminRoleAddModel roleAdminAddModel)
        {

            if (id != roleAdminAddModel.Role.Id)
            {
                ModelState.AddModelError("", "Id is Not Belongs the Role");
                return View(roleAdminAddModel);
            }

            if (ModelState.IsValid)
            {
                var result = await _roleManager.UpdateAsync(roleAdminAddModel.Role);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("RoleEditResultError", "Update is not successed!");
                    return View(roleAdminAddModel);
                }
            }
            return RedirectToAction("RoleList");
        }

        // POST: Role Delete
        [HttpPost]
        public async Task<IActionResult> RoleDelete(string id, bool isPostMethodValidation = true)
        {
            if (id == null)
            {
                ModelState.AddModelError(String.Empty, "Id is not have a value");
            }

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ModelState.AddModelError(String.Empty, "Role is not found");
                return View("RoleList");
            }

            var result = await _roleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Can't Delete Role");
                return RedirectToAction("RoleList");
            }
            return RedirectToAction("RoleList");
        }
    }
}
