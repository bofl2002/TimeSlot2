using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TimeSlot.Models;
using TimeSlot.Persistence;
using TimeSlot.Services;
using TimeSlot.ViewModels;


namespace TimeSlot.Controllers
{
    [Authorize]
    //[Authorize(Roles ="Admin")]
    public class BookingsController : Controller
    {
       
        private readonly IRoomRepository _roomRepository;
        //private readonly IBookingRepository _bookingRepository;
        private readonly IBookingService _bookingService;

        private readonly UserManager<ApplicationUser> _userManager;

        public BookingsController(IBookingService bookingService, IRoomRepository roomRepository, UserManager<ApplicationUser> userManager)
        {
            _bookingService = bookingService;
            _roomRepository = roomRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            if (await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), "Admin"))
            {
                var allBookings = _bookingService.GetAll();
                return View(allBookings);
            }
            else
            {
                var bookings = _bookingService.GetAllByUserId(userId);
                return View(bookings);
            }
            
        }


        public IActionResult Add(int? id)
        {
            ViewBag.Action = "Add";

            var bookingVM = new BookingViewModel
            {
                Booking = new Booking(),
                Rooms = _roomRepository.GetAll()
            };

            var date = DateTime.Now;
            bookingVM.Booking.StartTime = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);
            bookingVM.Booking.EndTime = new DateTime(date.Year, date.Month, date.Day, date.Hour + 1, date.Minute, 0);

            if (id != null) bookingVM.Booking.RoomId = id.Value;

            return View(bookingVM);
        }

        [HttpPost]
        public IActionResult Add(BookingViewModel bookingVM)
        {
            ModelState.Remove("Booking.UserId");
            ModelState.Remove("Booking.User");

            if (!ModelState.IsValid)
            {
                bookingVM.Rooms = _roomRepository.GetAll();
                ViewBag.Action = "Add";
                return View(bookingVM);
            }
            try
            {
                var userId = _userManager.GetUserId(User);
                bookingVM .Booking.UserId = userId;
                _bookingService.Add(bookingVM.Booking);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                bookingVM.Rooms = _roomRepository.GetAll();
                ViewBag.Action = "Add";
                return View(bookingVM);
            }
        }

        public IActionResult Edit(int? id)
        {
            BookingViewModel bookingVM = new BookingViewModel
            {
                Booking = _bookingService.GetById(id ?? 0),
                Rooms = _roomRepository.GetAll()
            };

            ViewBag.Action = "edit";

            return View(bookingVM);
        }

        [HttpPost]
        public IActionResult Edit(BookingViewModel bookingVM)
        {
            if (!ModelState.IsValid)
            {
                bookingVM.Rooms = _roomRepository.GetAll();

                ViewBag.Action = "edit";

                return View(bookingVM);
            }
            try
            {
                _bookingService.Add(bookingVM.Booking);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                bookingVM.Rooms = _roomRepository.GetAll();
                ViewBag.Action = "add";
                return View(bookingVM);
            }

            _bookingService.Update(bookingVM.Booking);
            
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _bookingService.Delete(id);

            return RedirectToAction("Index");   
        }
    }
}
