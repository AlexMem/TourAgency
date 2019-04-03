using Microsoft.EntityFrameworkCore;
using TourAgency.Models;

namespace TourAgency.Contexts {
    public class TourAgencyDbContext : DbContext {
        private static string _connectionString;
        public DbSet<Order> orders { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Tour> tours { get; set; }
        public DbSet<Type> types { get; set; }
        public DbSet<User> users { get; set; }

        public TourAgencyDbContext() {}
        
        public TourAgencyDbContext(string connectionString) {
            if (_connectionString == null) {
                _connectionString = connectionString;
            }
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySQL(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity => {
                entity.HasKey(e => e.userId);
                entity.HasOne(e => e.role)
                      .WithMany(e => e.users);
                entity.Property(e => e.isRegular)
                      .HasConversion<int>();
            });
            
            modelBuilder.Entity<Order>(entity => {
                entity.HasKey(e => e.orderId);
                entity.HasOne(e => e.user);
                entity.HasOne(e => e.tour);
            });
            
            modelBuilder.Entity<Tour>(entity => {
                entity.HasKey(e => e.tourId);
                entity.HasOne(e => e.type)
                      .WithMany(e=>e.tours);
                entity.HasOne(e => e.user);
                entity.Property(e => e.isActive)
                      .HasConversion<int>();
                entity.Property(e => e.isHot)
                      .HasConversion<int>();
            });
            
            modelBuilder.Entity<Type>(entity => {
                entity.HasKey(e => e.typeId);
            });
            
            modelBuilder.Entity<Role>(entity => {
                entity.HasKey(e => e.roleId);
            });
        }
    }
}