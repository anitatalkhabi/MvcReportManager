using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Helpers;
namespace Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public bool Gender { get; set; } 
        public string  Degree { get; set; }
    }
}