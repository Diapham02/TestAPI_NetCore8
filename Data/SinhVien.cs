using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_API.Data
{
    [Table("SinhVien")]
    public class SinhVien
    {
        // Mã sinh viên (khóa chính)
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int MaSV { get; set; }

        // Tên sinh viên
        [Required(ErrorMessage = "Tên sinh viên là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên sinh viên không được vượt quá 100 ký tự")]
        public string TenSV { get; set; }

        // Ngày sinh của sinh viên
        public DateTime? NgaySinh { get; set; }

        // Giới tính của sinh viên
        [StringLength(10, ErrorMessage = "Giới tính không được vượt quá 10 ký tự")]
        public string GioiTinh { get; set; }

        // Mã khoa của sinh viên là 1 khoá ngoại 
        [Required(ErrorMessage = "Mã khoa là bắt buộc")]
        [StringLength(10, ErrorMessage = "Mã khoa không được vượt quá 10 ký tự")]
        public string? MaKhoa { get; set; }
        //FK 
        //[ForeignKey("MaKhoa")]
        //public Khoa Khoa { get; set; }
    }
}
