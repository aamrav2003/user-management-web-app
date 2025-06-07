using Microsoft.EntityFrameworkCore;
using IAB_251_Assessment2.BusinessLogic.Entities;
using System.Collections.Generic;

namespace IAB_251_Assessment2.DataAccess.Data
{
    public class UserManagementDBContext : DbContext
    {
        public UserManagementDBContext(DbContextOptions<UserManagementDBContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Quotation> Quoations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Quotation>().ToTable("Quotations");
        }
    }
}
