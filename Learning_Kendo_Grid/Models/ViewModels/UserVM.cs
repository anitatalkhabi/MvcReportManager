using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Helpers;
namespace ViewModels
{
    public class UserVM
    {
        public int Id { get; set; }
        [Display(Name = "نام")] 
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }
        [Display(Name = "کدملی")]
        public string NationalCode { get; set; }
        [Display(Name = "جنسیت")]
        public bool Gender { get; set; } 
        public List<int> Degree { get; set; }
        [Display(Name = "تحصیلات")]
        public string  StrDegree { get; set; }

    }
 
}