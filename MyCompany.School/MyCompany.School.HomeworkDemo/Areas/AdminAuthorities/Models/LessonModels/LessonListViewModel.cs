using MyCompany.School.HomeworkDemo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.School.HomeworkDemo.Areas.AdminAuthorities.Models.LessonModels
{
    public class LessonListViewModel
    {
        public List<Lesson> Lessons { get; set; }
        public string Key { get; set; }
    }
}
