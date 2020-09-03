using System;
using System.Collections.Generic;

namespace MyCompany.School.HomeworkDemo.Data
{
    public partial class StudentPersons
    {
        public StudentPersons()
        {
            StudentPersonHomeworks = new HashSet<StudentPersonHomework>();
        }

        public int PersonId { get; set; }
        public int? StudentNo { get; set; }
        public int? ClassId { get; set; }

        public virtual Classes Class { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<StudentPersonHomework> StudentPersonHomeworks { get; set; }
    }
}
