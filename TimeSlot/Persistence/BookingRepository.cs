using Microsoft.EntityFrameworkCore;
using TimeSlot.Data;
using TimeSlot.Models;

namespace TimeSlot.Persistence
{
    public class BookingRepository : IBookingRepository
    {

        private readonly TimeSlotContext _context;
        public BookingRepository(TimeSlotContext context)
        {
            _context = context;
        }

   
        public void Add(Booking booking)
        {
            if (booking != null)
            {
                _context.Add(booking);
                _context.SaveChanges();
            }
         
        }

        public void Delete(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking != null)
            {
                _context.Remove(booking);
                _context.SaveChanges();
            }
        }

        public List<Booking> GetAll()
        {
            return _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.User)
                .Where(b => b.StartTime >= DateTime.Now)
                .ToList();
        }

        public Booking? GetById(int id)
        {
            if (id != null)
            {
                var booking = _context.Bookings.Find(id);
                return (booking);
            }
            return null;
        }

        public void Update(Booking booking)
        {
            if (booking != null)
            {
                var bookings = _context.Bookings.Find(booking.BookingId);
                _context.Update(bookings);
                _context.SaveChanges();
            }
        }
    }

    
}
