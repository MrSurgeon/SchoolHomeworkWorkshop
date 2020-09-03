using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MyCompany.School.HomeworkDemo.Data;
using MyCompany.School.HomeworkDemo.Models.Homework;

namespace MyCompany.School.HomeworkDemo.Controllers
{

    public class HomeworkController : Controller
    {
        private readonly SchoolDataDbContext _schoolDataDbContext;

        public HomeworkController(SchoolDataDbContext schoolDataDbContext)
        {
            _schoolDataDbContext = schoolDataDbContext;
        }

        //GET: Homework Index
        public IActionResult Index()
        {
            return View();
        }
        // GET : Homework Add
        public IActionResult Add()
        {
            return View();
        }
        // GET : Homework List
        public async Task<IActionResult> List()
        {
            var homeworkList = new HomeworkListViewModel()
            {
                HomeworkList = await _schoolDataDbContext.HomeworkDescriptions.ToListAsync()
            };

            if (homeworkList == null)
            {
                ModelState.AddModelError(string.Empty, "List has null values");
                return View("Index");
            }

            return View(homeworkList);
        }
        // GET: Homework Edit
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ModelState.AddModelError(string.Empty, "Identification error");
                return View("List");
            }

            var homework = new HomeworkManagerModel()
            {
                Homework = await _schoolDataDbContext.HomeworkDescriptions.FirstOrDefaultAsync(s => s.Id == int.Parse(id))
            };

            if (homework == default)
            {
                ModelState.AddModelError(string.Empty, "Homework isn't found !!");
                return View("List");
            }
            return View(homework);
        }
        // GET: Homework Details
        public async Task<IActionResult> Details(string id)
        {
            var homework = new HomeworkManagerModel()
            {
                Homework = await _schoolDataDbContext.HomeworkDescriptions.FirstOrDefaultAsync(s => s.Id == int.Parse(id))
            };
            if (homework.Homework == default)
            {
                ModelState.AddModelError(string.Empty, "Homework isn't found !!");
                return View("List");
            }
            return View(homework);

        }
        // GET: Homework Delete
        public async Task<IActionResult> Delete(string id)
        {
            var homework = new HomeworkManagerModel()
            {
                Homework = await _schoolDataDbContext.HomeworkDescriptions.FirstOrDefaultAsync(s => s.Id == int.Parse(id))
            };
            if (homework.Homework == default)
            {
                ModelState.AddModelError(string.Empty, "Homework isn't found !!");
                return View("List");
            }
            return View();
        }
        [HttpPost]
        // POST: Homework List
        public async Task<IActionResult> List(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                ModelState.AddModelError(string.Empty, "Key is null");
                return View("List");
            }

            var result = new HomeworkListViewModel()
            {
                HomeworkList = await _schoolDataDbContext.HomeworkDescriptions.Where(s => s.HomeworkDetails.Contains(key)).ToListAsync()
            };

            if (result == null)
            {
                ModelState.AddModelError(string.Empty, "Model is not have any value");
                return View("List");
            }

            return View("List", result);
        }
        // POST: Homework Add
        [HttpPost]
        public async Task<IActionResult> Add(HomeworkManagerModel homeworkManagerModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Validation is not reasonable");
                return View(homeworkManagerModel);
            }

            try
            {
                await _schoolDataDbContext.HomeworkDescriptions.AddAsync(homeworkManagerModel.Homework);

                await _schoolDataDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction("List");
        }
        // POST: Homework Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, [Bind("")] HomeworkManagerModel homeworkManagerModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Model is not valid !!");
                return View(homeworkManagerModel);
            }
            if (id != homeworkManagerModel.Homework.Id)
            {
                ModelState.AddModelError(string.Empty, "Id is not equal to required information");
                return View("List");
            }

            try
            {
                _schoolDataDbContext.HomeworkDescriptions.Update(homeworkManagerModel.Homework);
                await _schoolDataDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View("List");

            }
            return RedirectToAction("List");
        }
        // POST: Homework Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "Id is null !!");
                return View("List");
            }

            try
            {
                _schoolDataDbContext.HomeworkDescriptions.Remove
               (
                               await (from m in _schoolDataDbContext.HomeworkDescriptions
                                      where m.Id == id
                                      select m)
                                   .FirstOrDefaultAsync());
                await _schoolDataDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction("List");

        }

    }
}
