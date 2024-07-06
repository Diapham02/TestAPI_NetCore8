using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_API.Data
{
    [Table("KHOA")]
    public class Khoa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int KhoaId { get; set; }

        [StringLength(10, ErrorMessage = "Mã khoa không được vượt quá 10 ký tự")]
        public string MaKhoa { get; set; }

        [StringLength(50, ErrorMessage = "Tên khoa không quá 50 ký tự")]
        public string TenKhoa { get; set; }

        // Danh sách sinh viên thuộc khoa này (mối quan hệ 1-nhiều)
        public virtual ICollection<SinhVien> SinhViens { get; set; }
    }
}
