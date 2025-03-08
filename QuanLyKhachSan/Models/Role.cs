using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhachSan.Models
{
    public class Role
    {
        [Key]
        [Column("sRoleID")]
        [StringLength(3)] // Tương ứng với CHAR(3)
        public string RoleID { get; set; }

        [Required] // Tương ứng với NOT NULL
        [StringLength(100)] // Tương ứng với NVARCHAR(100)
        [Column("sRoleName")]
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
