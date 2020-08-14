using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyCompany.School.HomeworkDemo.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MyCompany.School.HomeworkDemo.Data
{
    public partial class Person
    {
        public Person()
        {
            PersonLessons = new HashSet<PersonLessons>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        [DisplayName("Person Active")]
        [BindNever]
        public bool? IsPersonActive { get; set; }

        public virtual StudentPersons StudentPersons { get; set; }
        public virtual TeacherPersons TeacherPersons { get; set; }
        public virtual ICollection<PersonLessons> PersonLessons { get; set; }
    }
}
