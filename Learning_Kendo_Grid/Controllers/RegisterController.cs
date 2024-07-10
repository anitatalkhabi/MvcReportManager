using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using Models;

namespace Learning_Kendo_Grid.Controllers
{
    public class RegisterController : Controller
    {
     //   UsersEntities db = new UsersEntities();
        //public ActionResult OverView_Gender()
        //{
        //    List<SelectListItem> genders = GetData();

        //    return Json(genders, JsonRequestBehavior.AllowGet);
        //}
        //private static List<SelectListItem> GetData()
        //{
        //    return new List<SelectListItem>()
        //    {
        //        new SelectListItem { Value = "1", Text = "زن"},
        //        new SelectListItem { Value = "2", Text = "مرد" },

        //    };
        //}
        public static List<SelectListItem> GetDegrees()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text="دیپلم"},
                new SelectListItem {Value = "2", Text="کاردانی"},
                new SelectListItem {Value = "3", Text="کارشناسی"},
                new SelectListItem {Value = "4", Text="کارشناسی ارشد"},
                new SelectListItem {Value = "5", Text="دکتری"}, 
            };
        }
        public ActionResult Register()
        {
            ViewBag.Degrees = GetDegrees();
            return View();
        }
       

        [HttpPost]
        public ActionResult Create(UserVM model)  
        {
            if(ModelState.IsValid)
            {
                using (var db = new UsersEntities())
                {


                    var newUser = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        NationalCode = model.NationalCode,
                        Gender = model.Gender,
                        Degree = string.Join(",", model.Degree)

                    };
                    db.Users.Add(newUser);
                    db.SaveChanges();
                }
            }
            return Redirect("~/Home/Index"); 
        }
        
        public ActionResult Edit(int id)
        {
            using (var db = new UsersEntities())
            {
                var user = db.Users.SingleOrDefault(u => u.Id == id);

                ViewBag.Degrees = GetDegrees();
                
                if (user == null)
                {
                    return HttpNotFound(); 
                }
                string degreeNames = ConvertIntToDegree(user.Degree);
                var lst = user.Degree.Split(',');

                ViewBag.UserDegrees = GetSelectItems(lst);
                return View(user); 
            }
        }
        public static List<SelectListItem> GetSelectItems(string[] strng) 
        {
            if (strng == null)
                return new List<SelectListItem>();
            return strng.Select(e => new SelectListItem()
            {
                Text = e,
                Value = e
            }).ToList();
        }
       [HttpPost]
        public ActionResult Edit(UserVM user, int id) 
        {
            if(ModelState.IsValid)
            {
                using (var db = new UsersEntities())
                {
                    var userInDb = db.Users.FirstOrDefault(u => u.Id == id); 

                    if (userInDb == null)
                    {
                        return HttpNotFound(); 
                    }

                   
                    userInDb.FirstName = user.FirstName;
                    userInDb.LastName = user.LastName;
                    userInDb.NationalCode = user.NationalCode;
                    userInDb.Gender = user.Gender;
                    userInDb.Degree = string.Join(",", user.Degree);



                    db.SaveChanges();
                    return Redirect("~/Home/Index");



                }
            }
            return View();

        }
        public string ConvertIntToDegree(string lst)
        {
            List<string> res = new List<string>();
            var Splited = lst.Split(',');
            foreach (var item in Splited)
            {
                switch (item)
                {
                    case "1":
                        res.Add("دیپلم");
                        break;
                    case "2":
                        res.Add("کاردانی");
                        break;
                    case "3":
                        res.Add("کارشناسی");
                        break;
                    case "4":
                        res.Add("کارشناسی ارشد");
                        break;
                    case "5":
                        res.Add("دکتری");
                        break;
                }
            }
            return string.Join(",", res);
        }
       
    }
}