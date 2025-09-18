using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TimeSlot.Models;

namespace TimeSlot.Data
{
    public class TimeSlotContext : DbContext 
    {
        public DbSet<Room> rooms { get; set; }
        public DbSet<Booking> bookings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
            .HasMany(r => r.Bookings)
            .WithOne(b => b.Room)
            .HasForeignKey(b => b.RoomId);

            modelBuilder.Entity<Room>().HasData(
            new Room { RoomId = 1, Name = "A1.01", Capacity = 10 },
            new Room { RoomId = 2, Name = "A1.02", Capacity = 5 },
            new Room { RoomId = 3, Name = "A1.03", Capacity = 4 },
            new Room { RoomId = 4, Name = "A1.04", Capacity = 6 });

            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    BookingId = 1,
                    Title = "Vejledning m. Jens",
                    StartTime = new DateTime(2025, 9, 16, 10, 30, 0),
                    EndTime = new DateTime(2025, 9, 16, 11, 30, 0),
                    RoomId = 1
                },
            new Booking
            {
                BookingId = 2,
                Title = "Møde - Team 3",
                StartTime = new DateTime(2025, 9, 15, 13, 30, 0),
                EndTime = new DateTime(2025, 9, 15, 15, 30, 0),
                RoomId = 2
            },
            new Booking
            {
                BookingId = 3,
                Title = "Ledermøde",
                StartTime = new DateTime(2025, 9, 19, 8, 30, 0),
                EndTime = new DateTime(2025, 9, 19, 10, 30, 0),
                RoomId = 3
            });
        }
        public TimeSlotContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
       
    }
}
