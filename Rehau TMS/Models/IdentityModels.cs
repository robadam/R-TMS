using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Rehau_TMS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        [Required]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Status")]
        public bool IsActive { get; set; }

        public string FullName
        {
            get { return Name + " " + Surname; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("FullName", this.FullName));
            return userIdentity;
        }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DatabaseContext", throwIfV1Schema: false)
        {
        }

        //DB Sets STARTS
        public DbSet<Article> Article { get; set; }
        public DbSet<Options> Options { get; set; }
        public DbSet<OptionsAdditional> OptionsAdditional { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Tool> Tool { get; set; }
        public DbSet<ToolStatus> ToolStatus { get; set; }
        public DbSet<WorkType> WorkType { get; set; }

        //DB Sets ENDS

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}