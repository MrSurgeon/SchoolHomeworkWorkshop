using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCompany.School.HomeworkDemo.Data;

namespace MyCompany.School.HomeworkDemo.Models.Homework
{
    public class HomeworkListViewModel
    {
        public List<HomeworkDescription> HomeworkList { get; set; }

        public string Key { get; set; }
    }
}
