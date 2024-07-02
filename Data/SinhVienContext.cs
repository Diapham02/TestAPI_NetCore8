using Microsoft.EntityFrameworkCore;

namespace Test_API.Data
{
    public class SinhVienContext : DbContext
    {
        public SinhVienContext(DbContextOptions<SinhVienContext> opt) : base(opt)
        {

        }

        #region DbSet
        public DbSet<SinhVien>? SinhViens { get; set; }
        #endregion 
    }


}
