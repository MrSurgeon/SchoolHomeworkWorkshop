using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCompany.School.HomeworkDemo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.School.HomeworkDemo.Security
{
    public class SchoolIdentityDbContext : IdentityDbContext<SchoolUser, SchoolRole, string>
    {
        
        public SchoolIdentityDbContext(DbContextOptions<SchoolIdentityDbContext> options) : base(options)
        {

        }
    }
}
