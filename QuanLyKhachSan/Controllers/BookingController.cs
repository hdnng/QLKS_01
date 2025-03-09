using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Repositories;
using QuanLyKhachSan.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace QuanLyKhachSan.Controllers
{
    public class BookingController : Controller
    {
        private readonly BookingManagementService _bookingS;
        private readonly GenericFunction<Booking> _bookingR;
        private readonly BookingDetailManagementService _bookingdS;
        private readonly GenericFunction<BookingDetail> _bookingdR;



        public BookingController(BookingManagementService bookingS,
                                 GenericFunction<Booking> bookingR,
                                 BookingDetailManagementService bookingdS,
                                 GenericFunction<BookingDetail> bookingdR)
        {
            _bookingS = bookingS;
            _bookingR = bookingR;
            _bookingdS = bookingdS;
            _bookingdR = bookingdR;

        }

        // GET: /User/BookingDetail?id={id}
        public IActionResult BookingDetail()
        {
            return View();
        }

        // GET: /User/BookingList
        public IActionResult BookingList()
        {
            return View();
        }



    }
}