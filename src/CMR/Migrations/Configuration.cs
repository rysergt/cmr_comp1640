namespace CMR.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CMR.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
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
                SeedAdmin(context);
                SeedCourseLeader(context);
            }
        }

        public void SeedAdmin(ApplicationDbContext context)
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);

            ApplicationUser[] admins =
            {
                new ApplicationUser { UserName = "admin", Email = "admin@test.com" }
            };

            foreach (ApplicationUser admin in admins)
            {
                var result = manager.Create(admin, "password");
                if (result.Succeeded)
                {
                    manager.AddToRole(admin.Id, "Administrator");
                }
            }
        }

        public void SeedCourseLeader(ApplicationDbContext context)
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);

            ApplicationUser[] courseLeaders =
            {
                new ApplicationUser { UserName = "leader", Email = "leader@test.com" }
            };

            foreach (ApplicationUser courseLeader in courseLeaders)
            {
                var result = manager.Create(courseLeader, "password");
                if (result.Succeeded)
                {
                    manager.AddToRole(courseLeader.Id, "Course Leader");
                }
            }
        }
    }
}
