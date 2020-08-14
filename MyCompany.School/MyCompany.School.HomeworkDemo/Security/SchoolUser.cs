using Microsoft.AspNetCore.Identity;
using MyCompany.School.HomeworkDemo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.School.HomeworkDemo.Security
{
    public class SchoolUser : IdentityUser
    {
        public int PersonId { get; set; }

    }
}
