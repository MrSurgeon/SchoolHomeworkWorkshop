using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCompany.School.HomeworkDemo.Areas.AdminAuthorities.Models.PersonModel;
using MyCompany.School.HomeworkDemo.Data;

namespace MyCompany.School.HomeworkDemo.Areas.Admins.Controllers
{
    [Area("AdminAuthorities")]
    public class PersonController : Controller
    {
        private SchoolDataDbContext _schoolDataDbContext;

        public PersonController(SchoolDataDbContext schoolDataDbContext)
        {
            _schoolDataDbContext = schoolDataDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        // GET:Person List
        public async Task<IActionResult> List()
        {
            var result = await _schoolDataDbContext.Persons.ToListAsync();
            var personList = new PersonListViewModel()
            {
                People = result
            };
            if (personList == null)
            {
                ModelState.AddModelError("ListHata", "Model hatası");
                return RedirectToAction("Index");
            }
            return View(personList);
        }

        // POST: Person List
        [HttpPost]
        public async Task<IActionResult> List(string Key)
        {
            if (Key == null)
            {
                ModelState.AddModelError(string.Empty, "Doğru bir arama değeri girmediniz!!");
                return View();
            }

            // Bu kısımda input değeri değiştikçe arama değerinin değişmesini sağlayan bir method tanımla

            var model = new PersonListViewModel()
            {
                People = await _schoolDataDbContext.Persons.Where(a => a.Name.Contains(Key)).ToListAsync<Person>()
            };
            if (model == null)
            {
                ModelState.AddModelError(string.Empty, "Can't Find Any People");
                return View();
            }
            return View(model);
        }
        // GET: Person Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: Person Add
        [HttpPost]
        public async Task<IActionResult> Add([Bind("")] PersonAddModel personAddModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Your information is don't valid");
                return View("Add");
            }

            personAddModel.Person.IsPersonActive = true;
            // IsActivePerson'a özel güzel bir süreç oluştur.

            try
            {
                var result = await _schoolDataDbContext.AddAsync(personAddModel.Person);
                await _schoolDataDbContext.SaveChangesAsync();
                return RedirectToAction("List");
            }
            catch (DbUpdateException error)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes" + "\n Message:" + error.Message.ToString() + "Inner Exception:" + error.InnerException.ToString());
            }
            return View(personAddModel);
        }

        // GET: Person Edit
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "Id is not have a value");
            }
            var person = new PersonAddModel()
            {
                Person = await _schoolDataDbContext.Persons.FirstOrDefaultAsync(s => s.Id == int.Parse(id))
            };

            if (person == default)
            {
                ModelState.AddModelError(String.Empty, "Person Update Error!!");
                return RedirectToAction("List");
            }
            return View(person);
        }

        // GET: Person Details
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "Id don't have a value");
                return RedirectToAction("List");
            }
            var person = new PersonAddModel
            { Person = await _schoolDataDbContext.Persons.FirstOrDefaultAsync(s => s.Id == int.Parse(id)) };

            if (person == null)
            {
                ModelState.AddModelError(string.Empty, "Role is not avaliable");
                return RedirectToAction("List");
            }

            return View(person);
        }

        // GET: Person Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "Id is not have a value");
                return RedirectToAction("List");
            }

            var person = new PersonAddModel()
            {
                Person = await _schoolDataDbContext.Persons.FirstOrDefaultAsync(s => s.Id == int.Parse(id))
            };

            if (person == null)
            {
                ModelState.AddModelError(string.Empty, "Can't Find Person Anything");
                return RedirectToAction("List");
            }
            return View(person);
        }

        //POST: Person Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, PersonAddModel personAddModel)
        {

            if (int.Parse(id) != personAddModel.Person.Id)
            {
                ModelState.AddModelError("", "Id is Not Belongs the Role");
                return View(personAddModel);
            }

            //personAddModel.Person.IsPersonActive=true;

            if (ModelState.IsValid)
            {
                var result = _schoolDataDbContext.Persons.Update(personAddModel.Person);

                if (result == null)
                {
                    ModelState.AddModelError(string.Empty, "Model is not updated");
                    return View(personAddModel);
                }
                await _schoolDataDbContext.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View(personAddModel);
        }

        // POST: Person Delete
        [HttpPost]
        public async Task<IActionResult> Delete(string id, bool isPostMethodValidation = true)
        {
            if (id == null)
            {
                ModelState.AddModelError(String.Empty, "Id is not have a value");
            }

            var person = await _schoolDataDbContext.Persons.FirstOrDefaultAsync(s => s.Id == int.Parse(id));

            if (person == null)
            {
                ModelState.AddModelError(String.Empty, "Person is not found");
                return View("List");
            }

            person.IsPersonActive = false;
            try
            {
                _schoolDataDbContext.Persons.Update(person);
                var result = await _schoolDataDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("List");
        }

        public async Task<IActionResult> Search(string Key)
        {
            if (string.IsNullOrEmpty(Key))
            {
                ModelState.AddModelError(string.Empty, "Id is not unreachable");
                return View("List", Key);
            }

            var people = new PersonListViewModel()
            {
                //Bu kısımda bir açılır kutu görünümü sağlayıp kişilerin aramayı 
                //farklı tercihlere göre gerçekleştirebilmesini sağlayabiliriz.

                People = await (from m in _schoolDataDbContext.Persons
                                where m.Name == Key
                                select m
                               ).ToListAsync()
            };
            return View("List", people);
        }

    }
}
