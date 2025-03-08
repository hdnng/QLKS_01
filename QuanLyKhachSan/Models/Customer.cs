using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhachSan.Models
{
    public class Customer
    {
        // Ánh xạ với trường sCustomerID
        [Key]
        [Column("sCustomerID")]
        [StringLength(5)]
        public string CustomerID { get; set; }

        // Ánh xạ với trường sUserID và thiết lập là khóa ngoại
        [ForeignKey("User")]
        [Column("sUserID")]
        [StringLength(5)]
        public string UserID { get; set; }

        // Ánh xạ với trường sPhoneNumber
        [Column("sPhoneNumber")]
        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [Column("sGmail", TypeName = "VARCHAR(255)")]
        public string Gmail { get; set; }

        // Ánh xạ với trường sCCCD và thiết lập UNIQUE
        [Column("sCCCD")]
        [StringLength(12)]
        [Required] // Đảm bảo trường này không thể null
        public string CCCD { get; set; }

        // Ánh xạ với trường sFullName
        [Column("sFullName")]
        [StringLength(100)]
        public string FullName { get; set; }

        // Ánh xạ với trường dBirthday
        [Column("dBirthday")]
        public DateTime Birthday { get; set; }

        // Mối quan hệ với bảng User (1 Customer thuộc về 1 User)
        public User User { get; set; }
    }
}
