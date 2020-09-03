using System;
using System.Collections.Generic;

namespace MyCompany.School.HomeworkDemo.Data
{
    public partial class StudentPersonHomeworkFiles
    {
        public int HomeworkId { get; set; }
        public int? FileId { get; set; }

        public virtual StudentPersonHomework Homework { get; set; }
    }
}
