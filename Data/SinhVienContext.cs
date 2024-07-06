using Microsoft.EntityFrameworkCore;

namespace Test_API.Data
{
    public class SinhVienContext : DbContext
    {
        public SinhVienContext(DbContextOptions<SinhVienContext> options)
            : base(options)
        {
        }

        public DbSet<Khoa> Khoas { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SinhVien>()
                .HasOne(s => s.Khoa)
                .WithMany(k => k.SinhViens) // Một khoa có nhiều sinh viên
                .HasForeignKey(sv => sv.KhoaId) // Khóa ngoại trong SinhVien
                .OnDelete(DeleteBehavior.Restrict); // Xóa sinh viên khi khoa bị xóa

            base.OnModelCreating(modelBuilder);
        }
    }
}
