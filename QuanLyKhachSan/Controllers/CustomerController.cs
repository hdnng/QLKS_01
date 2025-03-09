using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyKhachSan.Data;
using QuanLyKhachSan.Models;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1️⃣ Danh sách khách hàng
        public async Task<IActionResult> CustomerList(string search)
        {
            var customers = _context.tblCustomer.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                customers = customers.Where(c => c.FullName.Contains(search) || c.CCCD.Contains(search));
            }

            return View(await customers.ToListAsync());
        }

        // 2️⃣ Hiển thị form thêm khách hàng
        public IActionResult Create()
        {
            return View();
        }

        // 3️⃣ Xử lý thêm khách hàng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.tblCustomer.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
    }
}
