using TimeSlot.Models;

namespace TimeSlot.ViewModels
{
    public class BookingViewModel
    {
        public Booking Booking { get; set; } = new Booking();
        public List<Room> Rooms { get; set; } = new List<Room>();

        public BookingViewModel()
        {
            Booking = new Booking();
        }
    }
}
