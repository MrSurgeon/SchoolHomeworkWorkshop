using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCompany.School.HomeworkDemo.Areas.AdminAuthorities.Models.BranchModels;
using MyCompany.School.HomeworkDemo.Data;

namespace MyCompany.School.HomeworkDemo.Areas.Admins.Controllers
{
    [Area("AdminAuthorities")]
    public class BranchController : Controller
    {

        private readonly SchoolDataDbContext _schoolDataDbContext;

        public BranchController(SchoolDataDbContext schoolDataDbContext)
        {
            _schoolDataDbContext = schoolDataDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET:Branch List
        public async Task<IActionResult> List()
        {
            var result = await _schoolDataDbContext.Branchs.ToListAsync();
            var branchList = new BranchListViewModel()
            {
                Branches = result
            };
            if (branchList == null)
            {
                ModelState.AddModelError("ListHata", "Model hatası");
                return RedirectToAction("Index");
            }
            return View(branchList);
        }

        // POST: Branch List
        [HttpPost]
        public async Task<IActionResult> List(string Key)
        {
            if (string.IsNullOrEmpty(Key))
            {
                ModelState.AddModelError(string.Empty, "Doğru bir arama değeri girmediniz!!");
                return View();
            }

            // Bu kısımda input değeri değiştikçe arama değerinin değişmesini sağlayan bir method tanımla

            var model = new BranchListViewModel()
            {
                Branches = await _schoolDataDbContext.Branchs.Where(a => a.BranchName.Contains(Key)).ToListAsync()
            };
            if (model == null)
            {
                ModelState.AddModelError(string.Empty, "Can't Find Any People");
                return View();
            }
            return View(model);
        }

        // GET: Branch Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: Branch Add
        [HttpPost]
        public async Task<IActionResult> Add([Bind("Branch")] BranchManagerModel branchManagerModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Your information is don't valid");
                return View("Add");
            }
            try
            {
                var result = await _schoolDataDbContext.AddAsync(branchManagerModel.Branch);
                await _schoolDataDbContext.SaveChangesAsync();
                return RedirectToAction("List");
            }
            catch (DbUpdateException error)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes" + "\n Message:"
                    + error.Message.ToString() + "Inner Exception:" + error.InnerException.ToString());
            }
            return View(branchManagerModel);
        }

        // GET: Branch Edit
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "Id is not have a value");
            }
            var branch = new BranchManagerModel()
            {
                Branch = await _schoolDataDbContext.Branchs.FirstOrDefaultAsync(s => s.Id == int.Parse(id))
            };

            if (branch == default)
            {
                ModelState.AddModelError(String.Empty, "Branch Update Error!!");
                return RedirectToAction("List");
            }
            return View(branch);
        }

        // GET: Branch Details
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "Id don't have a value");
                return RedirectToAction("List");
            }
            var branch = new BranchManagerModel()
            {
                Branch = await _schoolDataDbContext.Branchs.FirstOrDefaultAsync(s => s.Id == int.Parse(id))
            };

            if (branch == null)
            {
                ModelState.AddModelError(string.Empty, "Role is not avaliable");
                return RedirectToAction("List");
            }

            return View(branch);
        }

        // GET: Branch Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "Id is not have a value");
                return RedirectToAction("List");
            }

            var branch = new BranchManagerModel()
            {
                Branch = await _schoolDataDbContext.Branchs.FirstOrDefaultAsync(s => s.Id == int.Parse(id))
            };

            if (branch == null)
            {
                ModelState.AddModelError(string.Empty, "Can't Find Person Anything");
                return RedirectToAction("List");
            }
            return View(branch);
        }

        //POST: Branch Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, BranchManagerModel branchManagerModel)
        {

            if (int.Parse(id) != branchManagerModel.Branch.Id)
            {
                ModelState.AddModelError("", "Id is Not Belongs the Role");
                return View(branchManagerModel);
            }

            if (ModelState.IsValid)
            {
                var result = _schoolDataDbContext.Branchs.Update(branchManagerModel.Branch);

                if (result == null)
                {
                    ModelState.AddModelError(string.Empty, "Model is not updated");
                    return View(branchManagerModel);
                }
                await _schoolDataDbContext.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View(branchManagerModel);
        }

        // POST: Branch Delete
        [HttpPost]
        public async Task<IActionResult> Delete(string id, bool isPostMethodValidation = true)
        {
            if (id == null)
            {
                ModelState.AddModelError(String.Empty, "Id is not have a value");
                return View("List");
            }

            var branch = await _schoolDataDbContext.Branchs.FirstOrDefaultAsync(s => s.Id == int.Parse(id));

            if (branch == null)
            {
                ModelState.AddModelError(String.Empty, "Branch is not found");
                return View("List");
            }

            try
            {
                _schoolDataDbContext.Branchs.Remove(branch);
                await _schoolDataDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction("List");
        }

        //Post : Search
        public async Task<IActionResult> Search(string Key)
        {
            if (string.IsNullOrEmpty(Key))
            {
                ModelState.AddModelError(string.Empty, "Id is not unreachable");
                return View("List");
            }

            var requireBranches = new BranchListViewModel()
            {
                //Bu kısımda bir açılır kutu görünümü sağlayıp kişilerin aramayı 
                //farklı tercihlere göre gerçekleştirebilmesini sağlayabiliriz.

                Branches = await (from m in _schoolDataDbContext.Branchs
                        where m.BranchName == Key
                        select m
                    ).ToListAsync()
            };
            return View("List", requireBranches);
        }
    }

}
