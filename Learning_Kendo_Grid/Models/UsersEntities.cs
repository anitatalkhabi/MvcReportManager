using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Models
{
    public class UsersEntities : DbContext
    {
        public UsersEntities() 
        {

        }
        public DbSet<User> Users { get; set; }
    }
}