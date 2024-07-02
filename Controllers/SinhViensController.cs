using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_API.Data;

namespace Test_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinhViensController : ControllerBase
    {
        private readonly SinhVienContext _context;

        public SinhViensController(SinhVienContext context)
        {
            _context = context;
        }

        // GET: api/SinhViens
        //Lấy toàn bộ danh sách sinh viên
        [HttpGet("Lấy list sinh viên")]
        public async Task<ActionResult<IEnumerable<SinhVien>>> GetSinhViens()
        {
            return await _context.SinhViens!.ToListAsync();
        }

        // GET: api/SinhViens/5
        // Hiển thị sinh viên theo mã SV
        [HttpGet("Search sinh viên theo MaSV")]
        public async Task<ActionResult<SinhVien>> GetSinhVien(int id)
        {
            var SvFinder = await _context.SinhViens!.FindAsync(id);
            //Neu null tra lai Ko thay
            if (SvFinder == null)
            {
                return NotFound();
            }
            return SvFinder;
        }

        // GET: api/SinhViens/5
        // Hiển thị sinh viên bằng nhiều trường đầu vào 
        [HttpGet("Search sinh viên theo TenSV, MaKhoa, NgaySinh")]
        public async Task<ActionResult<IEnumerable<SinhVien>>> Searching(string keyword)
        {
            var query = _context.SinhViens.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(sv => sv.TenSV.ToLower().ToString().Contains(keyword)
                || sv.MaKhoa.ToLower().ToString().Contains(keyword)
                || sv.NgaySinh.ToString().Contains(keyword));
            }

            var results = await query.ToListAsync();

            if (results.Count == 0)
            {
                return NotFound();
            }

            return results;
        }



        // PUT: api/SinhViens/5
        //Chinh sua theo MaSV
        [HttpPut("Chỉnh sửa sinh viên theo MaSV")]
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

                // thay doi tt
                Exist.TenSV = sinhVien.TenSV;
                Exist.NgaySinh = sinhVien.NgaySinh;
                Exist.MaKhoa = sinhVien.MaKhoa;
                Exist.GioiTinh = sinhVien.GioiTinh;
                //Save data and push db
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }

        // POST: api/SinhViens
        // Them sinh vien
        [HttpPost("Thêm sinh viên")]
        public async Task<ActionResult<SinhVien>> PostSinhVien(SinhVien sinhVien)
        {
            try
            {
                // Không cần gán MaSV nữa vì đã có IDENTITY
                sinhVien.MaSV = 0;

                _context.SinhViens.Add(sinhVien);
                await _context.SaveChangesAsync();

                // Lấy MaSV mới được tạo từ cơ sở dữ liệu
                int newMaSV = sinhVien.MaSV; // Entity Framework sẽ tự động cập nhật giá trị MaSV sau khi lưu

                return CreatedAtAction(nameof(GetSinhVien), new { id = newMaSV }, sinhVien);
            }
            catch (Exception) // Bắt các lỗi khác
            {
                // Ghi log lỗi hoặc xử lý tùy theo yêu cầu
                return StatusCode(500, "Lỗi trong quá trình thêm sinh viên.");
            }
        }




        // DELETE: api/SinhViens/5
        // Xoá 1 sinh viên hoặc nhiều sinh viên theo mã sinh viên
        [HttpDelete("Xoá hoặc xoá nhiều sinh viên theo MaSV")] // Nhận mã sinh viên dưới dạng chuỗi
        public async Task<IActionResult> DeleteSinhViens(string MaSV)
        {
            // Tách chuỗi mã sinh viên thành mảng các ID
            var idStrings = MaSV.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var idsToDelete = idStrings.Select(int.Parse).ToList(); // Chuyển đổi thành List<int>
            // Tìm kiếm và xóa sinh viên
            var sinhViensToDelete = await _context.SinhViens
                .Where(sv => idsToDelete.Contains(sv.MaSV))
                .ToListAsync();

            if (sinhViensToDelete.Count == 0)
            {
                return NotFound();
            }
            _context.SinhViens.RemoveRange(sinhViensToDelete);
            //Push lên Db
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
