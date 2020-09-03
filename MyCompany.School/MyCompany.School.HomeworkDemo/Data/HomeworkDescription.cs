using System;
using System.Collections.Generic;

namespace MyCompany.School.HomeworkDemo.Data
{
    public partial class HomeworkDescription
    {
        public HomeworkDescription()
        {
            StudentPersonHomeworks = new HashSet<StudentPersonHomework>();
        }

        public int Id { get; set; }
        public int? PersonLessonId { get; set; }
        public string HomeworkDetails { get; set; }
        public DateTime LoadDate { get; set; }

        public Homework Homework { get; set; }
        public int? HomeworkId { get; set; }
        public virtual PersonLessons PersonLesson { get; set; }
        public virtual ICollection<StudentPersonHomework> StudentPersonHomeworks { get; set; }
    }
}
