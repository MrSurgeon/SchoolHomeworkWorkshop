using System;
using System.Collections.Generic;

namespace MyCompany.School.HomeworkDemo.Data
{
    public partial class HomeworkDescriptions
    {
        public HomeworkDescriptions()
        {
            StudentPersonHomeworks = new HashSet<StudentPersonHomeworks>();
        }

        public int Id { get; set; }
        public int? PersonLessonId { get; set; }
        public string HomeworkDescription { get; set; }
        public DateTime LoadDate { get; set; }

        public virtual PersonLessons PersonLesson { get; set; }
        public virtual ICollection<StudentPersonHomeworks> StudentPersonHomeworks { get; set; }
    }
}
