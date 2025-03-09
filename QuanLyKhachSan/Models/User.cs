using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhachSan.Models
{
    public class User
    {
        [Key]
        [Column("sUserID")]
        //[StringLength(5)] // CHAR(5)
        public string UserID { get; set; }

        [Column("sGmail")]
        [Required] // Không được phép null
        public string Gmail { get; set; }

        [Column("sPassword")]
        [Required] // Không được phép null
        public string Password { get; set; }

        [Column("sRoleID")]
        [Required] // Không được phép null
        public string RoleID { get; set; }

        [ForeignKey("RoleID")]
        public Role Role { get; set; }
    }
}
