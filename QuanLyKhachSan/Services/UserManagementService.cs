using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Repositories;
using System.Threading.Tasks;
namespace QuanLyKhachSan.Services
{
    public class UserManagementService
    {
        private readonly GenericFunction<User> _userR;
        private readonly ILogger<UserManagementService> _logger;


        public UserManagementService(GenericFunction<User> userR, ILogger<UserManagementService> logger)
        {
            _userR = userR;
            _logger = logger;  // Khởi tạo _logger
        }


        // Đăng ký người dùng mới
        public async Task AddUserAsync(User model)
        {
            // Tạo UserID mới cho người dùng dựa trên role
            var userId = await GenerateUserIdAsync(model.RoleID);

            var user = new User
            {
                UserID = userId,
                Gmail = model.Gmail, // Lấy thông tin từ model
                Password = HashPassword(model.Password), // Mã hóa mật khẩu
                RoleID = model.RoleID // Vai trò của người dùng
            };

            // Thêm người dùng vào cơ sở dữ liệu
            await _userR.AddAsync(user);
        }

        // Tạo UserID tự động dựa trên role
        public async Task<string> GenerateUserIdAsync(string role)
        {
            string prefix = role == "R00" ? "Ua" : "UA"; // Chọn prefix phù hợp với role
            var lastUserId = await _userR.GetLastUserIdAsync(prefix); // Lấy UserID cuối cùng từ cơ sở dữ liệu

            // Tạo ID mới dựa trên UserID cuối cùng
            string newUserId;

            if (lastUserId != null)
            {
                // Lấy số cuối cùng trong UserID và tăng lên
                var lastNumber = int.Parse(lastUserId.Substring(2));  // Cắt bỏ phần prefix (Ua hoặc UA)
                var newNumber = lastNumber + 1;
                newUserId = prefix + newNumber.ToString("D3"); // Đảm bảo số luôn có 3 chữ số
            }
            else
            {
                // Nếu chưa có UserID nào, bắt đầu từ Ua001 hoặc UA001
                newUserId = prefix + "001";
            }

            return newUserId;
        }

        // Hàm mã hóa mật khẩu (sử dụng kỹ thuật mã hóa như SHA256, bcrypt...)
        private string HashPassword(string password)
        {
            //phương thức mã hóa
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return System.Convert.ToBase64String(hashedBytes);
            }
        }

        
    }
}
