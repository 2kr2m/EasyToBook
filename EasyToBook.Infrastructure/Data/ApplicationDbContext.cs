using EasyToBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyToBook.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Villa>().HasData(
                 new Villa
                 {
                     Id = 1,
                     Name = "Royal Villa",
                     Description = "Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa",
                     ImageUrl = "https://assets.palmspringslife.com/wp-content/uploads/2018/12/26160745/villa-royale.jpg",
                     Occupancy = 4,
                     Price = 200,
                     Sqft = 550,

                 },
                 new Villa
                 {
                     Id = 2,
                     Name = "Royal Villa",
                     Description = "Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa",
                     ImageUrl = "https://assets.palmspringslife.com/wp-content/uploads/2018/12/26160745/villa-royale.jpg",
                     Occupancy = 4,
                     Price = 200,
                     Sqft = 550,

                 },
                 new Villa
                 {
                     Id = 3,
                     Name = "Royal Villa",
                     Description = "Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa",
                     ImageUrl = "https://assets.palmspringslife.com/wp-content/uploads/2018/12/26160745/villa-royale.jpg",
                     Occupancy = 4,
                     Price = 200,
                     Sqft = 550,

                 }
            );
            modelBuilder.Entity<VillaNumber>().HasData(
                new VillaNumber
                {
                    Villa_Number = 201,
                    VillaId = 2,
                    
                },
                new VillaNumber
                {
                    Villa_Number = 202,
                    VillaId = 2,
                    
                },
                new VillaNumber
                {
                    Villa_Number = 203,
                    VillaId = 2,
                    
                },
                new VillaNumber
                {
                    Villa_Number = 301,
                    VillaId = 3,
                    
                },
                new VillaNumber
                {
                    Villa_Number = 302,
                    VillaId = 3,
                    
                }
                );
        }
    }
}
