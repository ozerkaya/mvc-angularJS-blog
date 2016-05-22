using BlogDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BlogDAL
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))] 
    public class DBContext : System.Data.Entity.DbContext
    {
        static DBContext()
        {
            DbConfiguration.SetConfiguration(new MySql.Data.Entity.MySqlEFConfiguration());
        }
        public DbSet<User> Users { get; set; }

        public DbSet<ThemeOptions> ThemeOptions { get; set; }

        public DbSet<Posts> Posts { get; set; }
    }

    
}