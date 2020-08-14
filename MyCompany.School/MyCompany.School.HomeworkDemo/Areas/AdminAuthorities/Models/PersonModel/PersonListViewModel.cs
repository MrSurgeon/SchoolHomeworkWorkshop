using MyCompany.School.HomeworkDemo.Data;
using System.Collections.Generic;

namespace MyCompany.School.HomeworkDemo.Areas.AdminAuthorities.Models.PersonModel
{
    public class PersonListViewModel
    {
        public List<Person> People { get; set; }
        public string Key { get; set; }
    }
}