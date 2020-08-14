using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.School.HomeworkDemo.Models.Security
{
    public class LoginViewModel
    {
        [Required]
        public String UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
