using System;
using System.Collections.Generic;

namespace MyCompany.School.HomeworkDemo.Data
{
    public partial class Classes
    {
        public Classes()
        {
            StudentPersons = new HashSet<StudentPersons>();
        }

        public int Id { get; set; }
        public int? TeacherId { get; set; }
        public string ClassName { get; set; }

        public virtual ICollection<StudentPersons> StudentPersons { get; set; }
    }
}
