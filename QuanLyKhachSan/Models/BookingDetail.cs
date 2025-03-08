using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhachSan.Models
{
    public class BookingDetail
    {
        [Key]
        [Column("sBookingID", TypeName = "CHAR(10)")]
        public string BookingID { get; set; }

        [Column("sRoomID", TypeName = "CHAR(5)")]
        public string RoomID { get; set; }

        [Required]
        [Column("dtCheckInDate", TypeName = "DATETIME")]
        public DateTime CheckInDate { get; set; }

        [Required]
        [Column("dtCheckOutDate", TypeName = "DATETIME")]
        public DateTime CheckOutDate { get; set; }

        [Required]
        [Column("fTotalPrice", TypeName = "FLOAT")]
        public float TotalPrice { get; set; }

        // Foreign Key references to Booking and Room
        [ForeignKey("BookingID")]
        public Booking Booking { get; set; }

        [ForeignKey("RoomID")]
        public Room Room { get; set; }
    }
}
