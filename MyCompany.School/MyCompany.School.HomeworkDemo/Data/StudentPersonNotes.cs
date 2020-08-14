using System;
using System.Collections.Generic;

namespace MyCompany.School.HomeworkDemo.Data
{
    public partial class StudentPersonNotes
    {
        public int NoteId { get; set; }
        public int? StudentNo { get; set; }

        public virtual Note Note { get; set; }
    }
}
