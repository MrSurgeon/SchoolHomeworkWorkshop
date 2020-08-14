using System;
using System.Collections.Generic;

namespace MyCompany.School.HomeworkDemo.Data
{
    public partial class StudentPersonHomeworks
    {
        public int Id { get; set; }
        public int HomeworkId { get; set; }
        public int StudentNo { get; set; }
        public string StudentAnswer { get; set; }

        public virtual HomeworkDescriptions Homework { get; set; }
        public virtual StudentPersons StudentNoNavigation { get; set; }
        public virtual StudentPersonHomeworkFiles StudentPersonHomeworkFiles { get; set; }
    }
}
