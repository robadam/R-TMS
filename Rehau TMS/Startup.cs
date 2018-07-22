using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Rehau_TMS.Models;
using Owin;
using System.Security.Claims;


[assembly: OwinStartupAttribute(typeof(Rehau_TMS.Startup))]
namespace Rehau_TMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // Creating default role and user for admin  
            if (!roleManager.RoleExists("Admin"))
            {  
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
                            
                var user = new ApplicationUser();
                user.UserName = "Admin";
                user.Name = "Admin";
                user.Surname = "Admin";
                user.IsActive = true;

                string UserPswd = "3156Twe@45masdf";

                var chkUser = UserManager.Create(user, UserPswd);

                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }


            //Creating  roles Pracownik and Moderator
            if (!roleManager.RoleExists("Pracownik"))
            {
                var role = new IdentityRole();
                role.Name = "Pracownik";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Moderator"))
            { 
                var role = new IdentityRole();
                role.Name = "Moderator";
                roleManager.Create(role);
            }
        }
    }
}
