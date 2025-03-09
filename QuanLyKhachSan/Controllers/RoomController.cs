using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyKhachSan.Data;
using QuanLyKhachSan.Models;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Controllers
{
    public class RoomController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> RoomDetail(string roomId)
        {
            if (roomId == null)
            {
                return NotFound();
            }

            var room = await _context.tblRoom
                .Include(r => r.RoomType)
                .FirstOrDefaultAsync(m => m.RoomID == roomId);

            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }
    }
}
