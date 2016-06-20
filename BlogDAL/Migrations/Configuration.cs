namespace BlogDAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogDAL.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(BlogDAL.DBContext context)
        {
            addUser(context);
            addThemeOptions(context);
        }

        private void addUser(BlogDAL.DBContext context)
        {
            var user = new BlogDAL.DAL.User()
            {
                date = DateTime.Now,
                password = "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", //Pass 1234
                username = "Admin"
            };

            context.Users.Add(user);
            context.SaveChanges();
        }

        private void addThemeOptions(BlogDAL.DBContext context)
        {
            var themeOptions = new BlogDAL.DAL.ThemeOptions()
            {
                BlogBossName="SameOne",
                BlogFooterText="Footer",
                BlogBossTitle= "Developer"                
            };

            context.ThemeOptions.Add(themeOptions);
            context.SaveChanges();
        }
    }
}
