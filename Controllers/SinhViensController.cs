using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_API.Data;

namespace Test_API.Controllers
{
    [Route("api/[controller]")] // Định tuyến URL đến controller 
    [ApiController] //API controller, tự động xác thực model và lỗi
    public class SinhViensController : ControllerBase
    {
        private readonly SinhVienContext _context;

        //Contructor nhận tham số là SinhVienContext gán vào _context 
        public SinhViensController(SinhVienContext context)
        {
            _context = context; // Khai báo class member thuộc SinhVienContext
        }

        // GET: api/SinhViens
        //Lấy toàn bộ danh sách sinh viên
        [HttpGet("Get List SinhVien")]
        public async Task<ActionResult<PaginationList>> GetSinhViens(int PageIndex = 1, int PageSize = 10)
        {
            // Nếu DbContext không có dữ liệu thì trả về NotFound
            if (_context.SinhViens == null)
            {
                return NotFound();
            }
            var query = _context.SinhViens.AsQueryable();// tạo truy vấn IQueryable để có thể áp dụng các thao tác LINQ sau này
            // Phân trang
            if (PageIndex < 1)
            {
                PageIndex = 1;
            }

            // Tổng số mục trong truy vấn
            int TotalItem = await query.CountAsync();

            // Tổng số trang cần thiết, làm tròn lên
            int TotalPage = (int)Math.Ceiling(TotalItem / (double)PageSize);

            // Áp dụng phân trang bằng cách bỏ qua các mục ở các trang trước và lấy số mục cần thiết
            query = query.Skip((PageIndex - 1) * PageSize).Take(PageSize);

            // Lấy danh sách kết quả sau khi áp dụng phân trang
            var result = await query.ToListAsync();

            // Tạo đối tượng PaginationList để chứa thông tin phân trang và danh sách kết quả
            var paginationList = new PaginationList(TotalPage, PageIndex, result);

            // Trả về kết quả với đối tượng PaginationList
            return Ok(paginationList);
        }


        // GET: api/SinhViens/5
        // Search sinh viên theo mã SV
        [HttpGet("Search SinhVien theo MaSV")]
        public async Task<ActionResult<IEnumerable<SinhVien>>> GetSinhVien(int id) // trả về dữ liệu dưới dạng một danh sách các object
        {
            var sinhVien = await _context.SinhViens
                .Where(sv => sv.MaSV == id) // Lọc id và chọn các thuộc tính cần thiết  
                .Select(sv => new
                {
                    sv.MaSV,
                    sv.TenSV,
                    sv.GioiTinh,
                    sv.NgaySinh,
                    sv.Khoa.TenKhoa // Lấy tên khoa
                })
                .ToListAsync();// Chuyển kết quả thành danh sách và lấy từ db
            if (sinhVien == null || !sinhVien.Any()) // Nếu rỗng hoặc sai mã sv thì trả về not found
            {
                return NotFound();
            }
            return Ok(sinhVien);
        }


        // GET: api/SinhViens/5
        // Search sinh viên bằng nhiều trường đầu vào 
        [HttpGet("Search SinhVien theo Keyword")]
        public async Task<ActionResult<IEnumerable<SinhVien>>> Searching(string keyword, int PageIndex, int PageSize)
        {
            var query = _context.SinhViens.AsQueryable(); // 
            //Lấy key để tìm
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(sv =>
                sv.MaSV.ToString().Contains(keyword) ||
                sv.TenSV.ToLower().Contains(keyword) ||
                sv.KhoaId.ToString().Contains(keyword) ||
                sv.GioiTinh.ToString().Contains(keyword) ||
                sv.Khoa.TenKhoa.ToLower().ToString().Contains(keyword));
            }

            //Phan trang
            if (PageIndex <= 0)
            {
                PageIndex = 0;
            }

            int TotalItem = await query.CountAsync();
            int TotalPage = (int)Math.Ceiling(TotalItem / (double)PageSize);// Làm tròn lên 

            query = query.Skip((PageIndex - 1) * PageSize).Take(PageSize);

            //Ket qua hien thi
            var result = await query.ToListAsync();

            //Nếu đầu vào không có gì thì NotFound
            if (result.Count == 0)
            {
                return NotFound();
            }

            var paginationList = new PaginationList(TotalPage, PageIndex, result);

            return Ok(paginationList);
        }

        // PUT: api/SinhViens/5
        //Chinh sua theo MaSV
        [HttpPut("Edit SinhVien theo MaSV")]
        public async Task<IActionResult> PutSinhVien(SinhVien sinhVien)
        {
            try
            {
                var Exist = await _context.SinhViens.FindAsync(sinhVien.MaSV);
                //Check exist db
                if (Exist is null)
                {
                    return BadRequest();
                }

                // Thay doi thong tin sv
                Exist.TenSV = sinhVien.TenSV;
                Exist.NgaySinh = sinhVien.NgaySinh;
                Exist.GioiTinh = sinhVien.GioiTinh;
                //Save data va push db
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) //Bắt Exception
            {
                throw;
            }
            return NoContent();
        }

        // POST: api/SinhViens
        // Them sinh vien
        [HttpPost("Add SinhVien")]
        public async Task<ActionResult<SinhVien>> ThemSinhVien(SinhVien sinhVien)
        {
            //Check xem Khoa nhập vào có trong db chưa
            var khoaExists = await _context.Khoas.AnyAsync(k => k.KhoaId == sinhVien.KhoaId);
            if (!khoaExists)
            {
                return BadRequest("KhoaId không hợp lệ.");
            }
            //Add vào 
            _context.SinhViens.Add(sinhVien); // Thêm 1 đối tượng sinhVien vào db
            await _context.SaveChangesAsync(); // Lưu bất đồng bộ 
            return CreatedAtAction("ThemSinhVien", new { maSV = sinhVien.MaSV }, sinhVien); // Trả về phản hồi sinhvien vừa thêm vào
        }


        // DELETE: api/SinhViens/5
        // Xoá 1 sinh viên hoặc nhiều sinh viên theo mã sinh viên
        [HttpDelete("Xoá hoặc xoá nhiều sinh viên theo MaSV")] // Nhận mã sinh viên dưới dạng chuỗi
        public async Task<IActionResult> DeleteSinhViens(string MaSV)
        {
            // Tách chuỗi mã sinh viên thành mảng các ID
            var idStrings = MaSV.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries); //Loại bỏ rỗng trong kết quả
            var idsToDelete = idStrings.Select(int.Parse).ToList(); // Chuyển đổi thành List<int>
            // Tìm kiếm và xóa sinh viên
            var sinhViensToDelete = await _context.SinhViens
                .Where(sv => idsToDelete.Contains(sv.MaSV))
                .ToListAsync();
            // Nếu không được nhập vào
            if (sinhViensToDelete.Count == 0)
            {
                return NotFound();
            }
            //Xoá một loạt đối tượng 
            _context.SinhViens.RemoveRange(sinhViensToDelete);
            //Lưu vào Db
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
