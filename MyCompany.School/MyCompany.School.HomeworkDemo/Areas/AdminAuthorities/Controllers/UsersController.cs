﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyCompany.School.HomeworkDemo.Areas.Admins.Controllers
{
     [Area("AdminAuthorities")]
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
