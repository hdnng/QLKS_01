using Microsoft.AspNetCore.Mvc;

namespace QuanLyKhachSan.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult RoomDetail(int roomId)
        {
            ViewData["RoomId"] = roomId;
            return View();
        }

    }
}
