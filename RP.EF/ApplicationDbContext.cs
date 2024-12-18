using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagement.EF;
using TaskManagement.Models;
using Task = TaskManagement.Models.Task;

namespace TaskManagement
{
    public class ApplicationDbContext :IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Task> Tasks { get; set; }
       public DbSet<Usery> Users { get; set; }
       public DbSet<Projecty> Projects { get; set; }
}
}
