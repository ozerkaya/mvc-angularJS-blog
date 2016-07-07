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

        public DbSet<Pages> Pages { get; set; }

        public DbSet<Labels> Labels { get; set; }

        public DbSet<LabelTypes> LabelTypes { get; set; }

        public DbSet<SocialContacts> SocialContacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Labels>()
           .HasRequired(p => p.Post)
           .WithMany(p => p.Label)
           .HasForeignKey(p => p.Post_ID)
           .WillCascadeOnDelete();


            modelBuilder.Entity<Labels>()
                .HasRequired(a => a.LabelTypes)
                .WithMany()
                .HasForeignKey(u => u.LabelTypes_ID);

            base.OnModelCreating(modelBuilder);
        }

    }




}