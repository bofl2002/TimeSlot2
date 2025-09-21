using TimeSlot.Models;

namespace TimeSlot.Services
{
    public interface IBookingService
    {
        void Add(Booking booking);
        void Delete(int id);
        List<Booking> GetAll();
        Booking? GetById(int id);
        void Update(Booking booking);
        List<Booking> GetAllByUserId(string userId);

    }
}
