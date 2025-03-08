using Microsoft.EntityFrameworkCore;
//giúp làm việc với database mà không cần viết truy vấn SQL trực tiếp.
using QuanLyKhachSan.Models;
using System.Data;
namespace QuanLyKhachSan.Data
{
    //DbContext đóng vai trò là cầu nối giữa ứng dụng và cơ sở dữ liệu.
    public class ApplicationDbContext:DbContext
    {
        //Constructor này giúp ApplicationDbContext nhận các cấu hình database từ Program.cs.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> tblUser { get; set; }
        public DbSet<Role> tblRole { get; set; }
        public DbSet<Employee> tblEmployee { get; set; }
        public DbSet<Customer> tblCustomer { get; set; }
        public DbSet<RoomType> tblRoomType { get; set; }
        public DbSet<Room> tblRoom { get; set; }
        public DbSet<Booking> tblBooking { get; set; }
        public DbSet<BookingDetail> tblBookingDetail { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình tên bảng nếu bạn muốn bảng có tên khác
            modelBuilder.Entity<User>().ToTable("tblUser");
            modelBuilder.Entity<Role>().ToTable("tblRole");
            modelBuilder.Entity<Employee>().ToTable("tblEmployee");
            modelBuilder.Entity<Customer>().ToTable("tblCustomer");
            modelBuilder.Entity<RoomType>().ToTable("tblRoomType");
            modelBuilder.Entity<Room>().ToTable("tblRoom");
            modelBuilder.Entity<Booking>().ToTable("tblBooking");
            modelBuilder.Entity<BookingDetail>().ToTable("tblBookingDetail");
            

            // Thực hiện các cấu hình khác (nếu cần)
            base.OnModelCreating(modelBuilder);
        }

    }


}
