using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TimeSlot.Models;

namespace TimeSlot.Data
{
    public class TimeSlotContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public TimeSlotContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Room>()
            .HasMany(r => r.Bookings)
            .WithOne(b => b.Room)
            .HasForeignKey(b => b.RoomId);

            modelBuilder.Entity<Booking>()
            .HasOne<ApplicationUser>(r => r.User)
            .WithMany(b => b.Bookings)
            .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Room>().HasData(
            new Room { RoomId = 1, Name = "A1.01", Capacity = 10 },
            new Room { RoomId = 2, Name = "A1.02", Capacity = 5 },
            new Room { RoomId = 3, Name = "A1.03", Capacity = 4 },
            new Room { RoomId = 4, Name = "A1.04", Capacity = 6 });

            
        }

       
    }
}
