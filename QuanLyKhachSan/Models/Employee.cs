using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhachSan.Models
{
    public class Employee
    {
        [Key]
        [Column("sEmployeeID", TypeName = "CHAR(5)")]
        public string EmployeeID { get; set; }

        [Required]
        [Column("sUserID", TypeName = "CHAR(5)")]
        public string UserID { get; set; }

        [Required]
        [Column("sGmail", TypeName = "VARCHAR(255)")]
        public string Gmail { get; set; }

        [Phone]
        [Column("sPhoneNumber", TypeName = "VARCHAR(10)")]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        [Column("sFullName", TypeName = "NVARCHAR(100)")]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        [Column("dBirthday", TypeName = "DATE")]
        public DateTime Birthday { get; set; }

        [StringLength(12)]
        [Column("sCCCD", TypeName = "CHAR(12)")]
        public string CCCD { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
    }
}
