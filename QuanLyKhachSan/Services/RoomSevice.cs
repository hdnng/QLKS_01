using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanLyKhachSan.Data;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Services
{
    public class RoomService
    {
        private readonly ApplicationDbContext _context;

        public RoomService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Room> GetRooms()
        {
            // Get room info and room type name by joining the two tables
            return _context.tblRoom.Include(r => r.RoomType).ToList();
        }
    }
}
