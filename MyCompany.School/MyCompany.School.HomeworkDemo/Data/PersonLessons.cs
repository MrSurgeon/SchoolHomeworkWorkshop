using System;
using System.Collections.Generic;

namespace MyCompany.School.HomeworkDemo.Data
{
    public partial class PersonLessons
    {
        public PersonLessons()
        {
            HomeworkDescriptions = new HashSet<HomeworkDescription>();
        }
        public int Id { get; set; }
        public int? PersonNo { get; set; }
        public int? LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual Person PersonNoNavigation { get; set; }
        public virtual ICollection<HomeworkDescription> HomeworkDescriptions { get; set; }
    }
}
