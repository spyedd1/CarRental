using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GymApp.Models;

namespace GymApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GymApp.Models.GymCustomer> GymCustomer { get; set; } = default!;
        public DbSet<GymApp.Models.GymPass> GymPass { get; set; } = default!;
    }
}
