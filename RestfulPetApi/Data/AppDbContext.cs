using Microsoft.EntityFrameworkCore;
using RestfulPetApi.Models;

namespace RestfulPetApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<HealthStatus> HealthStatuses { get; set; }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Food> Foods { get; set; }

        public DbSet<Training> Trainings { get; set; }

        public DbSet<SocialInteraction> SocialInteractions { get; set; }    


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("PetApiDb"); // Your database connection string should go here
            }
        }
    }

}
