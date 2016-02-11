using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using LangDetector.Domain;

namespace LangDetector.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("MyConnection")
        {
        }

        public DbSet<Request> Requests { get; set;}
        public DbSet<RequestInfo> RequestsInfo { get; set; }
    }
}