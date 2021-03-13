using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PaymentAPI.Domain.Entities;

namespace PaymentAPI.Infrastructure.Data.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PaymentState>().HasData(
                new PaymentState { Id = 1, State = "Successful" },
                new PaymentState { Id = 2, State = "Failed" }
            );
        }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentState> PaymentStates { get; set; }

        public class AppDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            private readonly IConfiguration config;

            public AppDbContextFactory(IConfiguration config)
            {
                this.config = config;
            }
            public ApplicationDbContext CreateDbContext(string[] args)
            {

                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }

    }
}
