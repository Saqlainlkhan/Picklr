using Microsoft.EntityFrameworkCore;

namespace Picklr.Models
{
    public class PicklrContext : DbContext
    {
        public PicklrContext(DbContextOptions<PicklrContext> options) : base(options) { }

        public DbSet<Club> Clubs { get; set; } = null!;
        public DbSet<PicklrProgram> Programs { get; set; } = null!;
        public DbSet<AppUser> Users { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One Club has many Programs
            modelBuilder.Entity<Club>()
                .HasMany(c => c.Programs)
                .WithOne(p => p.Club)
                .HasForeignKey(p => p.ClubID);

            // Seed Clubs
            modelBuilder.Entity<Club>().HasData(
                new Club
                {
                    ClubID = 1,
                    Name = "Picklr Downtown",
                    Location = "123 Main St, Chicago, IL",
                    Description = "Our flagship downtown club with 10 indoor courts."
                },
                new Club
                {
                    ClubID = 2,
                    Name = "Picklr Northside",
                    Location = "456 Oak Ave, Evanston, IL",
                    Description = "A vibrant outdoor facility with 8 courts and a pro shop."
                },
                new Club
                {
                    ClubID = 3,
                    Name = "Picklr New York",
                    Location = "789 Broadway, New York, NY",
                    Description = "Our newest club in the heart of the city."
                }
            );

            // Seed Programs
            modelBuilder.Entity<PicklrProgram>().HasData(
                new PicklrProgram
                {
                    ProgramID = 1,
                    Name = "Beginner Open Play",
                    Description = "Drop-in open play for new players. No experience needed.",
                    Fee = 10.00m,
                    AvailableDays = "Monday,Wednesday,Friday",
                    ClubID = 1
                },
                new PicklrProgram
                {
                    ProgramID = 2,
                    Name = "Intermediate Clinic",
                    Description = "Weekly skill-building clinic led by a certified coach.",
                    Fee = 25.00m,
                    AvailableDays = "Tuesday,Thursday",
                    ClubID = 1
                },
                new PicklrProgram
                {
                    ProgramID = 3,
                    Name = "Advanced Tournament",
                    Description = "Competitive round-robin tournament for rated players.",
                    Fee = 40.00m,
                    AvailableDays = "Saturday,Sunday",
                    ClubID = 2
                },
                new PicklrProgram
                {
                    ProgramID = 4,
                    Name = "Picklr Social",
                    Description = "Casual weekend social play, all levels welcome.",
                    Fee = 0.00m,
                    AvailableDays = "Saturday",
                    ClubID = 2
                },
                new PicklrProgram
                {
                    ProgramID = 5,
                    Name = "Picklr 101",
                    Description = "The program is designed for the beginners.",
                    Fee = 10.00m,
                    AvailableDays = "Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday",
                    ClubID = 3
                }
            );

            // Seed Users
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    UserID = 1,
                    FirstName = "Alice",
                    LastName = "Smith",
                    Email = "alice@picklr.com",
                    Role = "Admin"
                },
                new AppUser
                {
                    UserID = 2,
                    FirstName = "Bob",
                    LastName = "Jones",
                    Email = "bob@picklr.com",
                    Role = "Client"
                }
            );
        }
    }
}
