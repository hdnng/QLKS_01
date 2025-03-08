using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhachSan.Models
{
    [Table("tblRoomType", Schema = "dbo")]
    public class RoomType
    {
        [Key]
        [Column("sRoomTypeID", TypeName = "CHAR(3)")]
        public string RoomTypeID { get; set; }

        [Required]
        [Column("sRoomTypeName", TypeName = "NVARCHAR(100)")]
        public string RoomTypeName { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }
}
