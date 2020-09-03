using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCompany.School.HomeworkDemo.Data;
using MyCompany.School.HomeworkDemo.Models.Student;

namespace MyCompany.School.HomeworkDemo.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly SchoolDataDbContext _schoolDataDbContext;

        public StudentController(SchoolDataDbContext schoolDataDbContext)
        {
            _schoolDataDbContext = schoolDataDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> HomeworkList(int id)
        {
            var homeworks = new StudentHomeworkListViewModel
            {
                Homeworks = await (from sp in _schoolDataDbContext.StudentPersons
                                   join sh in _schoolDataDbContext.StudentPersonHomeworks
                                                      on sp.StudentNo equals sh.StudentNo
                                   select new StudentHomeworkModel()
                                   {
                                       StudentNo = sh.StudentNo,
                                       HomeworkId = sh.HomeworkId,
                                       StudentAnswer = sh.StudentAnswer,
                                       Id = sp.PersonId
                                   }).Where(p => p.Id == id).ToListAsync()
            };

            if (homeworks.Homeworks == null)
            {
                ModelState.AddModelError(string.Empty, "Homeworks is not found !!");
                return View("Index");
            }

            return View(homeworks);
        }

        public async Task<IActionResult> HomeworkAnswer(int id, int homeworkId)
        {
            if (id == 0 && homeworkId == 0)
            {
                ModelState.AddModelError(string.Empty, "Id or Homework Id is not found");
                return View("Index");
            }

            var homework = new StudentAnswerViewModel()
            {
                HomeworkModel = await (from sp in _schoolDataDbContext.StudentPersons
                                       join sh in _schoolDataDbContext.StudentPersonHomeworks
                                           on sp.StudentNo equals sh.StudentNo
                                       select new StudentHomeworkModel()
                                       {
                                           StudentNo = sh.StudentNo,
                                           HomeworkId = sh.HomeworkId,
                                           StudentAnswer = sh.StudentAnswer,
                                           Id = sp.PersonId
                                       }).Where(p => p.Id == id && p.HomeworkId == homeworkId).FirstOrDefaultAsync()
            };
            if (homework.HomeworkModel == default)
            {
                ModelState.AddModelError(string.Empty, "Homework is not found");
                return View("HomeworkList");
            }

            return View();

        }

        public async Task<IActionResult> HomeworkAuthorList(int? id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "Id is null");
                return View("Index");
            }
            var list = await (from sp in _schoolDataDbContext.StudentPersons
                              join sh in _schoolDataDbContext.StudentPersonHomeworks on sp.StudentNo equals sh.StudentNo
                              join hd in _schoolDataDbContext.HomeworkDescriptions
                                  on sh.HomeworkId equals hd.HomeworkId
                              select new
                              {
                                  PersonLessonId = hd.PersonLessonId,
                                  LoadDate = hd.LoadDate,
                                  StudentNo = sp.StudentNo,
                                  HomeworkId = sh.HomeworkId
                              }).ToListAsync();

            if (list == null)
            {
                ModelState.AddModelError(string.Empty, "Problem is found");
                return View("Index");
            }

            return View(list);
        }

        public async Task<IActionResult> HomeworkLessonList(int? id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "Id is null !!");
                return View("Index");
            }
            var list = await (from sp in _schoolDataDbContext.StudentPersons
                              join sh in _schoolDataDbContext.StudentPersonHomeworks on sp.StudentNo equals sh.StudentNo
                              join hd in _schoolDataDbContext.HomeworkDescriptions on sh.HomeworkId equals hd.Id
                              join pl in _schoolDataDbContext.PersonLessons on hd.PersonLessonId equals pl.Id
                              group sh by sh.HomeworkId into g
                              select new
                              {
                                  g.Key
                              }).ToListAsync();
            if (list == null)
            {
                ModelState.AddModelError(string.Empty, "Object is unavailable");
                return View("Index");
            }

            return View();
        }

        //public async Task<IActionResult> EditStudent(int? id)
        //{


        //}



    }
}
