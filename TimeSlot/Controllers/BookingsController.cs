﻿using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeSlot.Models;
using TimeSlot.Persistence;
using TimeSlot.Services;
using TimeSlot.ViewModels;


namespace TimeSlot.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
       
        private readonly IRoomRepository _roomRepository;
        //private readonly IBookingRepository _bookingRepository;
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService, IRoomRepository roomRepository)
        {
            _bookingService = bookingService;
            _roomRepository = roomRepository;
        }
        
        public IActionResult Index()
        {
            var bookings = _bookingService.GetAll();
            return View(bookings);
        }

        public IActionResult Add(int? id)
        {
            ViewBag.Action = "add";

            var bookingVM = new BookingViewModel
            {
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
            if (!ModelState.IsValid)
            {
                bookingVM.Rooms = _roomRepository.GetAll();
                ViewBag.Action = "add";

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
            _bookingService.Add(bookingVM.Booking);
            return RedirectToAction("Index");
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
