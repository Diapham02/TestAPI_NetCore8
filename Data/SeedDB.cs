using Microsoft.CodeAnalysis;

namespace Test_API.Data
{
    public class SeedDB
    {
        public static void Init(SinhVienContext context)
        {
            //Phương thức trong LINQ kiểm tra xem có phần tử nào bên trong Collection ko
            if (context.SinhViens.Any())
            {
                {
                    return;
                }
            }

            //Seed Khoa
            var khoas = new Khoa[]
            {
                new Khoa {KhoaId = 0 ,MaKhoa = "CNTT", TenKhoa = "Công nghệ thông tin" },
                new Khoa {KhoaId = 1 ,MaKhoa = "ĐTVT" , TenKhoa = "Điện tử viễn thông" },
            };
            //Them Khoa vao db
            context.Khoas.AddRange(khoas); // Thêm data vào bộ nhớ tạm của context trước khi gọi SaveChanges
            context.SaveChanges();


            //seed sinh vien 
            var sinhViens = new SinhVien[]
{
    new SinhVien{TenSV = "Phạm Long", NgaySinh = DateTime.Parse("2002/02/05"), GioiTinh = "Nam", KhoaId = 0 },
    new SinhVien{TenSV = "Nguyễn Thị Hoa", NgaySinh = DateTime.Parse("2001/04/10"), GioiTinh = "Nữ", KhoaId = 1 },
    new SinhVien{TenSV = "Trần Văn Bình", NgaySinh = DateTime.Parse("2000/11/25"), GioiTinh = "Nam", KhoaId = 1 },
    new SinhVien{TenSV = "Lê Minh Tuấn", NgaySinh = DateTime.Parse("2002/06/15"), GioiTinh = "Nam", KhoaId = 0 },
    new SinhVien{TenSV = "Đặng Thu Hà", NgaySinh = DateTime.Parse("2001/12/30"), GioiTinh = "Nữ", KhoaId = 1 },
    new SinhVien{TenSV = "Võ Hồng Phúc", NgaySinh = DateTime.Parse("2000/05/05"), GioiTinh = "Nam", KhoaId = 0 },
    new SinhVien{TenSV = "Phan Văn Dũng", NgaySinh = DateTime.Parse("2002/08/18"), GioiTinh = "Nam", KhoaId = 1 },
    new SinhVien{TenSV = "Ngô Thị Lan", NgaySinh = DateTime.Parse("2001/07/24"), GioiTinh = "Nữ", KhoaId = 1 },
    new SinhVien{TenSV = "Hoàng Minh Anh", NgaySinh = DateTime.Parse("2000/03/14"), GioiTinh = "Nam", KhoaId = 1 },
    new SinhVien{TenSV = "Bùi Thị Hương", NgaySinh = DateTime.Parse("2002/10/01"), GioiTinh = "Nữ", KhoaId = 1 },
    new SinhVien{TenSV = "Đinh Văn Toàn", NgaySinh = DateTime.Parse("2000/02/22"), GioiTinh = "Nam", KhoaId = 0 },
    new SinhVien{TenSV = "Trương Minh Tâm", NgaySinh = DateTime.Parse("2001/09/13"), GioiTinh = "Nam", KhoaId = 1 },
    new SinhVien{TenSV = "Lý Thị Tuyết", NgaySinh = DateTime.Parse("2002/04/20"), GioiTinh = "Nữ", KhoaId = 1 },
    new SinhVien{TenSV = "Phạm Văn Khoa", NgaySinh = DateTime.Parse("2000/07/11"), GioiTinh = "Nam", KhoaId = 0 },
    new SinhVien{TenSV = "Nguyễn Minh Quân", NgaySinh = DateTime.Parse("2001/05/09"), GioiTinh = "Nam", KhoaId = 1 },
    new SinhVien{TenSV = "Trần Thị Nhung", NgaySinh = DateTime.Parse("2000/11/02"), GioiTinh = "Nữ", KhoaId = 1 },
    new SinhVien{TenSV = "Lê Văn Đức", NgaySinh = DateTime.Parse("2002/06/07"), GioiTinh = "Nam", KhoaId = 1 },
    new SinhVien{TenSV = "Đặng Văn Hùng", NgaySinh = DateTime.Parse("2001/03/19"), GioiTinh = "Nam", KhoaId = 0 },
    new SinhVien{TenSV = "Phan Thị Mai", NgaySinh = DateTime.Parse("2000/08/26"), GioiTinh = "Nữ", KhoaId = 1 },
    new SinhVien{TenSV = "Hoàng Văn Khải", NgaySinh = DateTime.Parse("2002/12/15"), GioiTinh = "Nam", KhoaId = 0 }
};
            //Thêm Sv vào db
            context.SinhViens.AddRange(sinhViens);
            //Lưu thay đổi vào DB
            context.SaveChanges();
        }
    }
}

