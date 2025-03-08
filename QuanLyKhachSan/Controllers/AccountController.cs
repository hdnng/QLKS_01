using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Data;
using QuanLyKhachSan.Repositories;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Services;
using System;
using System.Threading.Tasks;
namespace QuanLyKhachSan.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManagementService _userS;
        private readonly GenericFunction<User> _userR;
        private readonly CustomerManagementService _customerS;
        private readonly GenericFunction<Customer> _customerR;
        private readonly GenericFunction<Employee> _employeeR;

        public AccountController(UserManagementService userS,
                                 GenericFunction<User> userR,
                                 CustomerManagementService customerS,
                                 GenericFunction<Customer> customerR,
                                 GenericFunction<Employee> employeeR)
        {
            _userS = userS;
            _userR = userR;
            _customerS = customerS;
            _customerR = customerR;
            _employeeR = employeeR;
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
            if (ModelState.IsValid)
            {
                bool exist = await _userR.ExistsAsync(u => u.Gmail == modelU.Gmail);
                bool check1 = _userR.Comparison(modelU.Gmail, "@staff.hotel.com", "@");
                bool check2 = _userR.Comparison(modelU.Gmail, "@manager.hotel.com", "@");
                if (!exist && (check1 || check2))
                {
                    if (check1 && await _userR.ExistsAsync(e => e.Gmail == modelU.Gmail))
                    {
                        //Thêm user, role = 1
                        modelU.RoleID = "R01";
                        await _userS.AddUserAsync(modelU);

                    }
                    else if (check2 && await _employeeR.ExistsAsync(e => e.Gmail == modelU.Gmail))
                    {
                        //thêm user, role = 2
                        modelU.RoleID = "R02"; // Đặt role là quản lý (manager)
                        await _userS.AddUserAsync(modelU); // Thêm user vào hệ thống
                    }
                    else
                    {
                        //thêm user và thêm khách hàng, role = 0
                        modelU.RoleID = "R00";
                        await _userS.AddUserAsync(modelU); // Thêm user vào hệ thống
                        modelC.UserID = modelU.UserID;
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
            else
            {
                //return RedirectToAction("Login");
                return View(modelU);
            }
            //return RedirectToAction("Login");

            //return View(modelU);
        }
    }
}
