using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Repositories;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace QuanLyKhachSan.Services
{
    public class CustomerManagementService
    {
        private readonly GenericFunction<Customer> _customerR;
        private readonly ILogger<CustomerManagementService> _logger;


        public CustomerManagementService(GenericFunction<Customer> customerR, ILogger<CustomerManagementService> logger)
        {
            _customerR = customerR;
            _logger = logger;  // Khởi tạo _logger
        }


        public async Task AddCustomerAsync(Customer model)
        {
            try
            {
                var customer = new Customer
                {
                    CustomerID = await GenerateCustomerIdAsync(),  // Tạo CustomerID
                    UserID = model.UserID,
                    FullName = model.FullName,
                    Birthday = model.Birthday,
                    Gmail = model.Gmail,
                    PhoneNumber = model.PhoneNumber
                };

                await _customerR.AddAsync(customer);  // Lưu Customer vào cơ sở dữ liệu
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu có
                _logger.LogError("Error adding customer: ", ex);
            }
        }
        


        // Tạo CustomerID tự động
        public async Task<string> GenerateCustomerIdAsync()
        {
            string prefix = "C";  // Mã bắt đầu với "C"
            string suffix = ""; // Tạo hậu tố với chữ cái từ 'a' đến 'z'
            int number = 1; // Số thứ tự bắt đầu từ 001

            // Lấy CustomerID cuối cùng từ cơ sở dữ liệu (Bảng tblCustomer)
            var lastCustomerId = await _customerR.GetLastObIdAsync("tblCustomer");

            if (lastCustomerId != null)
            {
                // Lấy phần hậu tố (chữ cái)
                suffix = lastCustomerId.Substring(1, 1); // Lấy chữ cái thứ 2 trong ID

                // Lấy số cuối cùng trong CustomerID và tăng lên
                var lastNumber = int.Parse(lastCustomerId.Substring(2));  // Cắt bỏ phần "C" và phần chữ cái
                number = lastNumber + 1;
            }
            else
            {
                // Nếu chưa có CustomerID nào, bắt đầu từ "Ca001"
                suffix = "a";
            }

            // Tạo CustomerID mới với số 3 chữ số
            string newCustomerId = prefix + suffix + number.ToString("D3"); // Đảm bảo số luôn có 3 chữ số

            // Nếu số thứ tự vượt quá 999, thay đổi chữ cái hậu tố
            if (number > 999)
            {
                suffix = ((char)(suffix[0] + 1)).ToString();  // Chuyển sang chữ cái tiếp theo
                newCustomerId = prefix + suffix + "001";  // Reset số thứ tự về 001
            }

            return newCustomerId;
        }

    }
}
