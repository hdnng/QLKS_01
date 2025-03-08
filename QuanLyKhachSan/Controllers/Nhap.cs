using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Repositories;
using Microsoft.Extensions.Logging;

namespace QuanLyKhachSan.Controllers
{
    public class NhapController : Controller
    {
        private readonly GenericFunction<User> _userR; // Lấy dữ liệu từ bảng User
        private readonly ILogger<NhapController> _logger;  // Thay đổi từ AccountController sang NhapController

        public NhapController(GenericFunction<User> userR, ILogger<NhapController> logger)
        {
            _userR = userR;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> DisplayData()
        {
            try
            {
                // Lấy tất cả dữ liệu User từ cơ sở dữ liệu
                var users = await _userR.GetAllAsync();

                // Kiểm tra và log xem có dữ liệu hay không
                if (users == null || !users.Any())
                {
                    _logger.LogWarning("Không có người dùng nào trong cơ sở dữ liệu.");
                    return View("Error");  // Trả về trang lỗi nếu không có dữ liệu
                }

                _logger.LogInformation($"Đã lấy {users.Count()} người dùng từ cơ sở dữ liệu.");

                // Trả về dữ liệu cho View
                return View(users);  // Trả về dữ liệu người dùng cho View
            }
            catch (Exception ex)
            {
                _logger.LogError("Lỗi khi lấy dữ liệu: {ErrorMessage}", ex.Message);
                return View("Error");  // Nếu có lỗi, hiển thị trang lỗi
            }
        }





    }
}
