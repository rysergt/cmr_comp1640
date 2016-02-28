namespace CMR.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CMR.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CMR.Models.ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                string[] roles = { "Administrator", "Course Leader" };
                foreach (string role in roles)
                {
                    manager.Create(new IdentityRole(role));
                }
            }

            if (!context.Users.Any())
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var admin = new ApplicationUser { UserName = "admin", Email = "admin@test.com" };

                var result = manager.Create(admin, "password");
                if (result.Succeeded)
                {
                    manager.AddToRole(admin.Id, "Administrator");
                }
            }
        }
    }
}
