using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhachSan.Models
{
    public class Booking
    {
        [Key]
        [Column("sBookingID", TypeName = "CHAR(10)")]
        public string BookingID { get; set; }

        [ForeignKey("Employee")]
        [Column("sEmployeeID", TypeName = "CHAR(5)")]
        public string EmployeeID { get; set; }

        [ForeignKey("Customer")]
        [Column("sCustomerID", TypeName = "CHAR(5)")]
        public string CustomerID { get; set; }

        [Column("iNumGuests")]
        public int NumGuests { get; set; }

        [Column("dtBookingTime")]
        public DateTime BookingTime { get; set; } = DateTime.Now; // Đặt giá trị mặc định là ngày giờ hiện tại

        [Column("iStatus")]
        public int Status { get; set; } = 0; // Giá trị mặc định là 0

        [Column("fTotalPayment")]
        public float TotalPayment { get; set; }

        [Column("dtCancelBookingTime")]
        public DateTime? CancelBookingTime { get; set; }

        // Các liên kết với các bảng khác
        public Employee Employee { get; set; }
        public Customer Customer { get; set; }
        public ICollection<BookingDetail> BookingDetails { get; set; }
    }
}
