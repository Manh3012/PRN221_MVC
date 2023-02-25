using System;
using DAL.Config;
using System.Linq;
using System.Text;
using DAL.Entities;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DAL
{
    public class FRMDbContext : IdentityDbContext<User>
    {
        public FRMDbContext()
        {

        }

        public FRMDbContext(DbContextOptions<FRMDbContext> options) : base(options)
        {
        }


        public DbSet<Category> Category { get; set; }
        public DbSet<User> Customer { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }


        public DbSet<Orders> Orders { get; set; }

        public DbSet<Product> Product { get; set; }



        public DbSet<Transaction> TransactionShared { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration config = new ConfigurationBuilder()
                                            .SetBasePath(Directory.GetCurrentDirectory())
                                            .AddJsonFile("appsettings.json").Build();

                string connectionString = config["ConnectionStrings:DefaultConnection"];
                optionsBuilder.UseSqlServer(connectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfig());
            #region Orders
            modelBuilder.Entity<Orders>().Property(x => x.ID).HasDefaultValueSql("NEWID()");
            #endregion
            #region Transaction
            modelBuilder.Entity<Transaction>().Property(x => x.ID).HasDefaultValueSql("NEWID()");
            #endregion
          
        }


    }
}
