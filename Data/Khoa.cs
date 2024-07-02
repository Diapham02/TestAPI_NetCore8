using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_API.Data
{
    [Table("Khoa")]
    public class Khoa
    {
        [Key]
        [Required(ErrorMessage = "Mã khoa là bắt buộc")]
        [StringLength(10, ErrorMessage = "Mã khoa không được vượt quá 10 ký tự")]
        public string MaKhoa { get; set; }
        [Required(ErrorMessage = "Tên khoa là bắt buộc")]
        [StringLength(50, ErrorMessage = "Tên khoa không quá 50 ký tự")]
        public string TenKhoa { get; set; }
    }
}
