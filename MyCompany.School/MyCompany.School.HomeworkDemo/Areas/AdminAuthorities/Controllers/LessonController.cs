using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCompany.School.HomeworkDemo.Areas.AdminAuthorities.Models.LessonModels;
using MyCompany.School.HomeworkDemo.Data;

namespace MyCompany.School.HomeworkDemo.Areas.AdminAuthorities.Controllers
{
    [Area("AdminAuthorities")]
    public class LessonController : Controller
    {
        private readonly SchoolDataDbContext _schoolDataDbContext;

        public LessonController(SchoolDataDbContext schoolDataDbContext)
        {
            _schoolDataDbContext = schoolDataDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET:Lesson List
        public async Task<IActionResult> List()
        {
            var result = await _schoolDataDbContext.Lessons.ToListAsync();
            var LessonList = new LessonListViewModel()
            {
                Lessons = result
            };
            if (LessonList == null)
            {
                ModelState.AddModelError("ListHata", "Model hatası");
                return RedirectToAction("Index");
            }
            return View(LessonList);
        }

        // POST: Lesson List
        [HttpPost]
        public async Task<IActionResult> List(string Key)
        {
            if (string.IsNullOrEmpty(Key))
            {
                ModelState.AddModelError(string.Empty, "Doğru bir arama değeri girmediniz!!");
                return View();
            }

            // Bu kısımda input değeri değiştikçe arama değerinin değişmesini sağlayan bir method tanımla

            var model = new LessonListViewModel()
            {
                Lessons = await _schoolDataDbContext.Lessons.Where(a => a.Name.Contains(Key)).ToListAsync<Lesson>()
            };
            if (model == null)
            {
                ModelState.AddModelError(string.Empty, "Can't Find Any People");
                return View();
            }
            return View(model);
        }

        // GET: Lesson Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: Lesson Add
        [HttpPost]
        public async Task<IActionResult> Add([Bind("Name")] LessonManagerModel lessonAddModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Your information is don't valid");
                return View("Add");
            }
            try
            {
                var result = await _schoolDataDbContext.AddAsync(lessonAddModel.Lesson);
                await _schoolDataDbContext.SaveChangesAsync();
                return RedirectToAction("List");
            }
            catch (DbUpdateException error)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes" + "\n Message:" + error.Message.ToString() + "Inner Exception:" + error.InnerException.ToString());
            }
            return View(lessonAddModel);
        }

        // GET: Lesson Edit
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "Id is not have a value");
            }
            var lesson = new LessonManagerModel()
            {
                Lesson = await _schoolDataDbContext.Lessons.FirstOrDefaultAsync(s => s.Id == int.Parse(id))
            };

            if (lesson == default)
            {
                ModelState.AddModelError(String.Empty, "Person Update Error!!");
                return RedirectToAction("List");
            }
            return View(lesson);
        }

        // GET: Lesson Details
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "Id don't have a value");
                return RedirectToAction("List");
            }
            var lesson = new LessonManagerModel
            {
                Lesson = await _schoolDataDbContext.Lessons.FirstOrDefaultAsync(s => s.Id == int.Parse(id))
            };

            if (lesson == null)
            {
                ModelState.AddModelError(string.Empty, "Role is not avaliable");
                return RedirectToAction("List");
            }

            return View(lesson);
        }

        // GET: Lesson Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "Id is not have a value");
                return RedirectToAction("List");
            }

            var lesson = new LessonManagerModel()
            {
                Lesson = await _schoolDataDbContext.Lessons.FirstOrDefaultAsync(s => s.Id == int.Parse(id))
            };

            if (lesson == null)
            {
                ModelState.AddModelError(string.Empty, "Can't Find Person Anything");
                return RedirectToAction("List");
            }
            return View(lesson);
        }

        //POST: Lesson Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, LessonManagerModel lessonAddModel)
        {

            if (int.Parse(id) != lessonAddModel.Lesson.Id)
            {
                ModelState.AddModelError("", "Id is Not Belongs the Role");
                return View(lessonAddModel);
            }

            if (ModelState.IsValid)
            {
                var result = _schoolDataDbContext.Lessons.Update(lessonAddModel.Lesson);

                if (result == null)
                {
                    ModelState.AddModelError(string.Empty, "Model is not updated");
                    return View(lessonAddModel);
                }
                await _schoolDataDbContext.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View(lessonAddModel);
        }

        // POST: Lesson Delete
        [HttpPost]
        public async Task<IActionResult> Delete(string id, bool isPostMethodValidation = true)
        {
            if (id == null)
            {
                ModelState.AddModelError(String.Empty, "Id is not have a value");
                return View("List");
            }

            var lesson = await _schoolDataDbContext.Lessons.FirstOrDefaultAsync(s => s.Id == int.Parse(id));

            if (lesson == null)
            {
                ModelState.AddModelError(String.Empty, "Person is not found");
                return View("List");
            }

            try
            {
                _schoolDataDbContext.Lessons.Remove(lesson);
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

            var requireLessons = new LessonListViewModel()
            {
                //Bu kısımda bir açılır kutu görünümü sağlayıp kişilerin aramayı 
                //farklı tercihlere göre gerçekleştirebilmesini sağlayabiliriz.

                Lessons = await (from m in _schoolDataDbContext.Lessons
                                 where  m.Name == Key
                                 select m
                                ).ToListAsync()
            };
            return View("List", requireLessons);
        }

    }
}
