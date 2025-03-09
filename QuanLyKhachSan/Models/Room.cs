using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhachSan.Models
{
    public class Room
    {
        [Key]
        [Column("sRoomID", TypeName = "CHAR(5)")]
        public string RoomID { get; set; }

        [ForeignKey("RoomType")]
        [Column("sRoomTypeID", TypeName = "CHAR(3)")]
        public string RoomTypeID { get; set; }

        [Column("sDescription", TypeName = "VARCHAR(255)")]
        public string Description { get; set; }

        [Required]
        [Column("iCapacity")]
        public int Capacity { get; set; }

        [Column("sIMGUrl", TypeName = "VARCHAR(255)")]
        public string? IMGUrl { get; set; } 

        [Required]
        [Column("iStatus")]
        [Range(0, 1)]
        public int Status { get; set; }

        [Required]
        [Column("fPricePerDay", TypeName = "FLOAT(53)")]
        public float PricePerDay { get; set; }

        public RoomType RoomType { get; set; }
    }
}
