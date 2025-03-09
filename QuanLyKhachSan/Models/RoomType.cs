using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhachSan.Models
{
    public class RoomType
    {
        [Key]
        [Column("sRoomTypeID", TypeName = "CHAR(3)")]
        public string RoomTypeID { get; set; }

        [Column("sRoomTypeName", TypeName = "VARCHAR(50)")]
        public string RoomTypeName { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }
}
