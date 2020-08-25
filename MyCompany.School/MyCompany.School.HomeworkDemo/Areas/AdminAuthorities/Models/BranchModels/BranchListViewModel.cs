using System.Collections.Generic;
using MyCompany.School.HomeworkDemo.Data;

namespace MyCompany.School.HomeworkDemo.Areas.AdminAuthorities.Models.BranchModels
{
    public class BranchListViewModel
    {
        public BranchListViewModel()
        {
        }

        public List<Branch> Branches { get; set; }
        public string Key { get; set; }
    }
}