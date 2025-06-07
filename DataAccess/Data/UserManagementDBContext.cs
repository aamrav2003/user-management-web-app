using Microsoft.EntityFrameworkCore;
using IAB_251_Assessment2.BusinessLogic.Entities;
using System.Collections.Generic;

namespace IAB_251_Assessment2.DataAccess.Data
{
    public class UserManagementDBContext : DbContext
    {
        private readonly ILogger<UserManagementDBContext> _logger;

        public UserManagementDBContext(DbContextOptions<UserManagementDBContext> options, ILogger<UserManagementDBContext> logger)
            : base(options)
        {
            _logger = logger;
        }

        public override int SaveChanges()
        {
            try
            {
                _logger.LogInformation("Saving changes to database");
                var result = base.SaveChanges();
                _logger.LogInformation($"Successfully saved {result} changes to database");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving changes to database: {ex.Message}");
                _logger.LogError($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Quotation> Quotations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Quotation>().ToTable("Quotations");
        }
    }
}
