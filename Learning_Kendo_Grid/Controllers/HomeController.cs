using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Helpers;

namespace Learning_Kendo_Grid.Controllers
{
    public class HomeController : Controller
    {
        // UsersEntities db = new UsersEntities();

        // GET: Home

        public ActionResult sample()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllData([DataSourceRequest] DataSourceRequest request)

        {

            using (var db = new UsersEntities())
            {
                var entities = db.Users.ToList();

                var res = entities.Select(e => new UserVM
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    NationalCode = e.NationalCode,
                    Gender = e.Gender,

                    StrDegree = ConvertIntToDegree(e.Degree)

                }).ToList();
                return Json(res.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Delete(int id)
        {
            using (var db = new UsersEntities())
            {
                var user = db.Users.SingleOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Degrees = GetDegrees();
                string degreeNames = ConvertIntToDegree(user.Degree);
                var lst = user.Degree.Split(',');

                ViewBag.UserDegrees = GetSelectItems(lst);
                return View(user); 
            }

              
        }

        
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            { 

            try
            {
                using (var db = new UsersEntities())
                {
                    var user = db.Users.SingleOrDefault(u => u.Id == id);
                    if (user == null)
                    {
                        return Json(new { success = false, message = "User not found." });
                    }

                    db.Users.Remove(user);
                    db.SaveChanges();
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }


        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        public string ConvertIntToDegree(string lst)
        {
            List<string> res = new List<string>();
            //var res = "";
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
            string final = string.Join(",", res);
            return final;
        }
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
    }
}