using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class SampleContext : IdentityDbContext<User>
    {

        public SampleContext(DbContextOptions<SampleContext> DbConnection) : base(DbConnection)
        {
            Database.EnsureCreated();
        }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<ElectedProject> ElectedProjects { get; set; }

        public DbSet<DonationHistory> DonationHistories { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Rating> Ratings { get; set; }

    }

}
