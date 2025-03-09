using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
namespace QuanLyKhachSan.Controllers
{
    public class AdminController : Controller
    {
        // GET: /Admin/CustomerList
        public IActionResult CustomerList()
        {
            return View();
        }
    }
}