using Microsoft.AspNetCore.Identity;
using MyCompany.School.HomeworkDemo.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.School.HomeworkDemo.Areas.AdminAuthorities.Models.RoleModel
{
    public class RoleListViewModel
    {
        public List<SchoolRole> Roles { get; set; }
        public string Key { get; set; }
    }
}
