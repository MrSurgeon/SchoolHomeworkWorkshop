using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.School.HomeworkDemo.Data
{
    public class Homework
    {
        public int Id { get; set; }
        public String HomeworkDescription { get; set; }
        public DateTime LoadDate { get; set; }

        public List<HomeworkDescription> HomeworkDescriptionses { get; set; }
    }
}
