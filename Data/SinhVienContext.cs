using Microsoft.EntityFrameworkCore;

namespace Test_API.Data
{
    public class SinhVienContext : DbContext
    {
        //Định nghĩa Contructor SinhVienContext kế thừa từ Dbcontext
        public SinhVienContext(DbContextOptions<SinhVienContext> options)
            : base(options)
        {
        }
        //Collection Khoa, Sinhvien
        public DbSet<Khoa> Khoas { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }

        //Cấu hình mối quan hệ giữa Sinhvien và Khoa
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SinhVien>()
                .HasOne(s => s.Khoa)// Sinh viên chỉ có 1 Khoa
                .WithMany(k => k.SinhViens) // Một khoa có nhiều sinh viên
                .HasForeignKey(sv => sv.KhoaId) // Khóa ngoại trong SinhVien
                .OnDelete(DeleteBehavior.Restrict); // Xóa sinh viên khi khoa bị xóa
            base.OnModelCreating(modelBuilder);
        }
    }
}
