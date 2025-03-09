﻿using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Data;
using QuanLyKhachSan.Repositories;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Services;
using System;
using System.Threading.Tasks;
using System.Diagnostics;

namespace QuanLyKhachSan.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManagementService _userS;
        private readonly GenericFunction<User> _userR;
        private readonly CustomerManagementService _customerS;


        public AccountController(UserManagementService userS,
                                 GenericFunction<User> userR,
                                 CustomerManagementService customerS)
        {
            _userS = userS;
            _userR = userR;
            _customerS = customerS;
        }

        // Trang đăng ký (GET)
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Xử lý đăng ký (POST)
        [HttpPost]
        public async Task<IActionResult> SignUp(User modelU, Customer modelC)
        {

                Debug.WriteLine("User Model:");
                Debug.WriteLine($"Gmail: {modelU.Gmail}");
                Debug.WriteLine($"Password: {modelU.Password}");
                Debug.WriteLine($"RoleID: {modelU.RoleID}");
                Debug.WriteLine($"UserID: {modelU.UserID}");

                Debug.WriteLine("Customer Model:");
                Debug.WriteLine($"CustomerID: {modelC.CustomerID}");
                Debug.WriteLine($"UserID: {modelC.UserID}");
                Debug.WriteLine($"PhoneNumber: {modelC.PhoneNumber}");
                Debug.WriteLine($"Gmail: {modelC.Gmail}");
                Debug.WriteLine($"CCCD: {modelC.CCCD}");
                Debug.WriteLine($"FullName: {modelC.FullName}");
                Debug.WriteLine($"Birthday: {modelC.Birthday}");
            
                bool exist = await _userR.ExistsAsync(u => u.Gmail == modelU.Gmail);
                Debug.WriteLine($"Exist: {exist}");
                bool check1 = _userR.Comparison(modelU.Gmail, "@staff.hotel.com", "@");
                bool check2 = _userR.Comparison(modelU.Gmail, "@manager.hotel.com", "@");
                Debug.WriteLine($"Check1: {check1}");
                Debug.WriteLine($"Check2: {check2}");

            
            if (!exist)
                {
                    if (check1)
                    {
                        //Thêm user, role = 1
                        modelU.RoleID = "R01";
                        await _userS.AddUserAsync(modelU);

                    }
                    else if (check2)
                    {
                        //thêm user, role = 2
                       modelU.RoleID = "R02"; // Đặt role là quản lý (manager)
                        await _userS.AddUserAsync(modelU); // Thêm user vào hệ thống
                    }
                    else
                    {
                        //thêm user và thêm khách hàng, role = 0
                        modelU.RoleID = "R00";
                        modelU.UserID = await _userS.GenerateUserIdAsync(modelU.RoleID);
                        modelC.UserID = modelU.UserID;
                        await _userS.AddUserAsync(modelU); // Thêm user vào hệ thống

                        await _customerS.AddCustomerAsync(modelC);
                    }
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("Gmail", "Gmail đã được đăng ký từ trước.");
                    return View(modelU);
                }
            
        }
    

    

        [HttpPost]
        public async Task<IActionResult> Login(User modelU)
        {
            try
            {
                bool existG = await _userR.ExistsAsync(u => u.Gmail == modelU.Gmail);
                Debug.WriteLine(existG);
                if (existG)
                {
                    string pass = await _userR.FindValueByCondition<string>(
                        u => u.Gmail == modelU.Gmail,
                        u => u.Password);

                    Debug.WriteLine($"Password: {pass}");
                    string role = await _userR.FindValueByCondition<string>(
                        u => u.Gmail == modelU.Gmail,
                        u => u.RoleID);
                    Debug.WriteLine($"Role: {role}");
                    if (_userS.HashPassword(modelU.Password) == pass && (role == "R01" || role == "R02"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (_userS.HashPassword(modelU.Password) == pass && role == "R00")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // Incorrect password
                        ModelState.AddModelError(string.Empty, "Incorrect password.");
                        return View(modelU);
                    }
                }
                else
                {
                    // Gmail not found
                    ModelState.AddModelError(string.Empty, "Gmail not found.");
                    return View(modelU);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
                Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View(modelU);
            }
        }





    }
}
