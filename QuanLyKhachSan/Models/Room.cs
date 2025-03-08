using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhachSan.Models
{
    public class Room
    {
        [Key] // Ánh xạ thuộc tính này là khóa chính
        [Column("sRoomID", TypeName = "CHAR(5)")] // Tên cột trong bảng SQL và kiểu dữ liệu
        public string RoomID { get; set; }

        [ForeignKey("RoomType")] // Ràng buộc khóa ngoại với bảng RoomType
        [Column("sRoomTypeID", TypeName = "CHAR(3)")] // Tên cột trong bảng SQL và kiểu dữ liệu
        public string RoomTypeID { get; set; }

        [Column("sDescription", TypeName = "TEXT")] // Tên cột trong bảng SQL và kiểu dữ liệu
        public string Description { get; set; }

        [Required] // Chỉ định rằng trường này không thể null
        [Column("iCapacity")]
        public int Capacity { get; set; }

        [Column("sIMGUrl", TypeName = "VARCHAR(255)")] // Tên cột trong bảng SQL và kiểu dữ liệu
        public string IMGUrl { get; set; }

        [Required] // Chỉ định rằng trường này không thể null
        [Column("iStatus")]
        [Range(0, 1)] // Gỉới hạn giá trị 0 hoặc 1
        public int Status { get; set; }

        [Required] // Chỉ định rằng trường này không thể null
        [Column("fPricePerDay", TypeName = "FLOAT(53)")] // Tên cột trong bảng SQL và kiểu dữ liệu
        public float PricePerDay { get; set; }

        // Ràng buộc khóa ngoại
        public RoomType RoomType { get; set; }
    }
}
