using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TimeSlot.Models;
using TimeSlot.Persistence;

namespace TimeSlot.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public void Update(Booking booking)
        {
            if (booking.EndTime <= booking.StartTime)
            {
                throw new ArgumentException("End time cannot be before start time.");

            }

            if (booking.StartTime <= DateTime.Now)
            {
                throw new ArgumentException("Start time cannot be in the past.");


            }
            var overlap = _bookingRepository.GetAll().Any(b =>
            b.RoomId == booking.RoomId &&
            booking.StartTime < b.EndTime &&
            booking.EndTime > b.StartTime);

            if (overlap)
            {
                throw new ArgumentException("Room is not available for the selected time");

            }
            _bookingRepository.Update(booking);
        }

        public List<Booking> GetAll()
        {
            return _bookingRepository.GetAll(); 
        }

        public Booking? GetById(int id)
        {
            return _bookingRepository.GetById(id);
        }

        public void Delete(int id)
        {
            var booking = _bookingRepository.GetById(id);
                if (booking != null)
            {
                _bookingRepository.Delete(id);
            }
            
        }



 

        public void Add(Booking booking)
        {
            if (booking.EndTime <= booking.StartTime)
            {
                throw new ArgumentException("Sluttidspunkt skal være senere end starttidspunkt.");
                
            }

            if (booking.StartTime <= DateTime.Now)
            {
                throw new ArgumentException("Startiden må ikke være tidligere end nu.");
               

            }
            var overlap = _bookingRepository.GetAll().Any(b =>
            b.RoomId == booking.RoomId &&
            booking.StartTime < b.EndTime &&
            booking.EndTime > b.StartTime);

            if (overlap)
            {
                throw new ArgumentException("Lokalet er allerede booket");
               
            }
            _bookingRepository.Add(booking);
        }


    }
}
