using System;
using System.Collections.Generic;

namespace MyCompany.School.HomeworkDemo.Data
{
    public partial class TeacherPersons
    {
        public int PersonId { get; set; }
        public int? TeacherNo { get; set; }
        public int? BranchId { get; set; }

        public virtual Branchs Branch { get; set; }
        public virtual Person Person { get; set; }
    }
}
