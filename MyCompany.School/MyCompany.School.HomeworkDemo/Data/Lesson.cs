using System;
using System.Collections.Generic;

namespace MyCompany.School.HomeworkDemo.Data
{
    public partial class Lesson
    {
        public Lesson()
        {
            PersonLessons = new HashSet<PersonLessons>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PersonLessons> PersonLessons { get; set; }
    }
}
