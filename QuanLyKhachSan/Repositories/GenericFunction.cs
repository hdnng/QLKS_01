using Microsoft.EntityFrameworkCore;
using QuanLyKhachSan.Data; // Đảm bảo import DbContext của bạn
using QuanLyKhachSan.Models; // Import các model của bạn
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace QuanLyKhachSan.Repositories
{
    //Khai Báo GenericRepository: T: Đây là kiểu tổng quát (generic type)
    //where T : class: Điều kiện này yêu cầu kiểu T phải là một class (lớp dữ liệu)
    public class GenericFunction<T> where T : class
    {
        //_context: Đây là đối tượng DbContext, chịu trách nhiệm kết nối và quản lý các đối tượng trong cơ sở dữ liệu
        private readonly ApplicationDbContext _context;

        //_dbSet: Là một thuộc tính kiểu DbSet<T>, dùng để thao tác với các bảng trong cơ sở dữ liệu
        //DbSet<T> đại diện cho một tập hợp các đối tượng của kiểu T trong cơ sở dữ liệu, ví dụ: DbSet<User>, DbSet<Room>, v.v.
        private readonly DbSet<T> _dbSet;

        private readonly ILogger<GenericFunction<T>> _logger; // Thêm ILogger vào class


        //Constructor này nhận một đối tượng ApplicationDbContext để khởi tạo GenericRepository
        //thiết lập _dbSet bằng cách sử dụng phương thức Set<T>() của DbContext, cho phép thao tác với bảng tương ứng với kiểu T
        public GenericFunction(ApplicationDbContext context, ILogger<GenericFunction<T>> logger)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _logger = logger;  // Khởi tạo logger
        }

        public static class Constants
        {
            public const string PhoneNumberPattern = @"^0(32|33|34|35|36|37|38|39|56|58|59|70|76|77|78|79|81|82|83|84|85|86|87|88|89|90|92|93|96|97|98|99)\d{7}$";
            public const string GmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        }

        // Thêm mới
        public async Task AddAsync(T ob)
        {
            await _dbSet.AddAsync(ob);
            await _context.SaveChangesAsync();
        }

        // Cập nhật
        public async Task UpdateAsync(T ob)
        {
            _dbSet.Update(ob);
            await _context.SaveChangesAsync();
        }

        // Xóa
        public async Task DeleteAsync(T ob)
        {
            _dbSet.Remove(ob);
            await _context.SaveChangesAsync();
        }


        // Tìm kiếm tất cả
        //public async Task<IEnumerable<T>> GetAllAsync()
        //{
        //    return await _dbSet.ToListAsync();
        //}

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                var result = await _dbSet.ToListAsync();

                // Log kết quả trước khi trả về
                _logger.LogInformation($"Đã lấy {result.Count()} bản ghi từ bảng {_dbSet.EntityType.Name}");

                if (result == null || !result.Any())
                {
                    throw new Exception("KO");
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi lấy dữ liệu: {ex.Message}");
                return Enumerable.Empty<T>(); // Trả về danh sách rỗng nếu có lỗi
            }
        }




        // Kiểm tra tồn tại (Check if record exists based on condition)
        public async Task<bool> ExistsAsync(Func<T, bool> predicate)
        {
            return await Task.FromResult(_dbSet.Any(predicate));
        }


        // Hàm kiểm tra giá trị theo biểu thức chính quy (regex) truyền vào
        public bool IsValidFormat(string pattern, string value)
        {
            // Kiểm tra nếu biểu thức chính quy hợp lệ và giá trị khớp với pattern
            if (string.IsNullOrEmpty(pattern) || string.IsNullOrEmpty(value))
            {
                return false;
            }

            var regex = new Regex(pattern);
            return regex.IsMatch(value);
        }

        public bool Comparison(string value1, string value2, string character)
        {
            int index = value1.LastIndexOf(character);
            if (index > -1)
            {
                string comparativeValue = value1.Substring(index);
                return comparativeValue.Equals(value2, StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                return false;
            }
        }

        public async Task<string> GetLastUserIdAsync(string prefix)
        {
            var lastUserId = await _context.tblUser
                .Where(u => u.UserID.StartsWith(prefix))
                .OrderByDescending(u => u.UserID)
                .Select(u => u.UserID)
                .FirstOrDefaultAsync();

            return lastUserId;
        }

        public async Task<string> GetLastObIdAsync(string tableName)
        {
            // Lấy DbSet theo tên bảng
            var dbSet = _context.GetType()
                .GetProperty(tableName)
                .GetValue(_context);

            // Sử dụng reflection để lấy kiểu của DbSet
            var entityType = dbSet.GetType().GenericTypeArguments[0];

            // Lấy phương thức OrderByDescending thông qua reflection
            var orderByMethod = typeof(Enumerable)
                .GetMethods()
                .First(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2)
                .MakeGenericMethod(entityType, typeof(string));  // Dự đoán kiểu string cho ID

            // Gọi phương thức OrderByDescending với tiêu chí sắp xếp theo ID
            var result = orderByMethod.Invoke(null, new[] { dbSet, (Func<object, string>)(entity => (string)entity.GetType().GetProperty("UserID").GetValue(entity)) })
                .GetType()
                .GetMethod("FirstOrDefault")
                .Invoke(null, null); // Lấy đối tượng đầu tiên trong danh sách đã sắp xếp

            // Trả về ID nếu tìm thấy
            return result?.ToString();
        }

        public async Task<TResult> FindValueByCondition<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector)
        {
            // Sử dụng DbSet<T> có sẵn trong _context
            if (_dbSet == null)
            {
                throw new InvalidOperationException("DbSet could not be found for the specified type.");
            }

            // Thực hiện truy vấn LINQ để tìm giá trị dựa trên điều kiện và chọn cột cần thiết
            var query = _dbSet.Where(predicate).Select(selector);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }
    }
}
