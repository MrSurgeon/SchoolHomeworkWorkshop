using System;
using System.Collections.Generic;

namespace MyCompany.School.HomeworkDemo.Data
{
    public partial class Branch
    {
        public Branch()
        {
            TeacherPersons = new HashSet<TeacherPersons>();
        }
        public int Id { get; set; }
        public string BranchName { get; set; }

        public virtual ICollection<TeacherPersons> TeacherPersons { get; set; }
    }
}
