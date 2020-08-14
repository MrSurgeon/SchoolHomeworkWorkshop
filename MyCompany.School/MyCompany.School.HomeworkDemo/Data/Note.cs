using System;
using System.Collections.Generic;

namespace MyCompany.School.HomeworkDemo.Data
{
    public partial class Note
    {
        public int Id { get; set; }
        public int? NoteResult { get; set; }
        public int? LessonsId { get; set; }
        public int? TeacherId { get; set; }

        public virtual StudentPersonNotes StudentPersonNotes { get; set; }
    }
}
