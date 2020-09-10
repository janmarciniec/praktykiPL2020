using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Praktyki.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Hour> Hours { get; set; }
        public DbSet<Day> Days { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hour>().HasData(
                new Hour { Id = 1, Hours = "8-10" },
                new Hour { Id = 2, Hours = "10-12" },
                new Hour { Id = 3, Hours = "12-14" },
                new Hour { Id = 4, Hours = "14-16" },
                new Hour { Id = 5, Hours = "16-18" },
                new Hour { Id = 6, Hours = "18-20" }
            );

            modelBuilder.Entity<Day>().HasData(
                 new Day { Id = 1, Name = "Poniedziałek" },
                 new Day { Id = 2, Name = "Wtorek" },
                 new Day { Id = 3, Name = "Środa" },
                 new Day { Id = 4, Name = "Czwartek" },
                 new Day { Id = 5, Name = "Piątek" }        
            );

            modelBuilder.Entity<IdentityRole>().HasData(
               new IdentityRole
               {
                   Name = "Admin",
                   NormalizedName = "Admin".ToUpper()
               },
               new IdentityRole
               {
                   Name = "User",
                   NormalizedName = "User".ToUpper()
               }
           );
        }
    }
}
