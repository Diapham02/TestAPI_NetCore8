using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Test_API.Data
{
    [Table("SINH_VIEN")]
    public class SinhVien
    {
        // Mã sinh viên (khóa chính)
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int MaSV { get; set; }

        // Tên sinh viên
        [Required(ErrorMessage = "Tên sinh viên là bắt buộc")]
        [StringLength(50, ErrorMessage = "Tên sinh viên không được vượt quá 50 ký tự")]
        public string TenSV { get; set; }

        // Ngày sinh của sinh viên
        public DateTime? NgaySinh { get; set; }

        // Giới tính của sinh viên
        [StringLength(10, ErrorMessage = "Giới tính không được vượt quá 10 ký tự")]
        [RegularExpression("^(Nam|Nữ|Khác)$", ErrorMessage = "Giới tính không hợp lệ")]
        public string GioiTinh { get; set; }

        //FK tham chiếu đến khoá chính
        public int KhoaId { get; set; }

        // Đối tượng Khoa tương ứng với KhoaId
        [JsonIgnore]
        public virtual Khoa? Khoa { get; set; }
    }
}
