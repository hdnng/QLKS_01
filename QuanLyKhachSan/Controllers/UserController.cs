using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
namespace QuanLyKhachSan.Controllers
{
    public class UserController : Controller
    {
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