using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using TourAgency.Models;

namespace TourAgency.Contexts {
    public class TourAgencyDbContext : DbContext {
        public DbSet<Order> orders { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Tour> tours { get; set; }
        public DbSet<Type> types { get; set; }
        public DbSet<User> users { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if(optionsBuilder.IsConfigured) return;
            optionsBuilder.UseMySQL(new MySqlConnection(@"Server=192.168.99.100;Database=tour_db;Uid=root;Pwd=admin;"));
        }
    }
}