using FriendOrganizer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace FriendOrganizer.DataAccess
{
    public class FriendOrganizerDbContext : DbContext
    {
        private string connectionString;

        public FriendOrganizerDbContext() : base()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            connectionString = configuration.GetConnectionString("SQLConnection");
        }

        public DbSet<Friend> Friend { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var friends = new Friend[]
            {
                new Friend {Id = 1, FirstName = "Thomas", LastName = "Huber"},
                new Friend {Id = 2, FirstName = "Urs", LastName = "Meier"},
                new Friend {Id = 3, FirstName = "Erkan", LastName = "Egin"},
                new Friend {Id = 4, FirstName = "Sara", LastName = "Huber"},
            };

            modelBuilder.Entity<Friend>().HasData(friends);

            modelBuilder.Entity<Friend>()
                .Property(f => f.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            base.OnModelCreating(modelBuilder);            
        }
    }
}
