namespace TestioProject.DAL.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using TestioProject.DAL.Models;
    
    public class TestioDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<WrittenLetter> WrittenLetters { get; set; }
        public DbSet<Restricted> Restricteds { get; set; }
        public DbSet<UserAvatar> UserAvatars { get; set; }

        public TestioDbContext(DbContextOptions<TestioDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
