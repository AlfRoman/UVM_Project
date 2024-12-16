using UVM_Project.Models;
using UVM_Project.Utilities;
using Microsoft.EntityFrameworkCore;

namespace UVM_Project.DataAccess
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbConnection = $"Filename={DBConnection.PathBuilder("users.db")}";
            optionsBuilder.UseSqlite(dbConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(col => col.UserId);
                entity.Property(col => col.UserId).IsRequired().ValueGeneratedOnAdd();
            });
        }
    }
}
