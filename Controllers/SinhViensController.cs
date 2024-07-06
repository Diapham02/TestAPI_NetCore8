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
            var sinhViens = await _context.SinhViens.Select(sv => new
            {
                sv.MaSV,
                sv.TenSV,
                sv.GioiTinh,
                sv.NgaySinh,
                sv.Khoa.TenKhoa,
            }).ToListAsync();
            return Ok(sinhViens);
        }

        // GET: api/SinhViens/5
        // Hiển thị sinh viên theo mã SV
        [HttpGet("Search sinh viên theo MaSV")]
        public async Task<ActionResult<IEnumerable<SinhVien>>> GetSinhVien(int id)
        {
            var sinhVien = await _context.SinhViens
                .Where(sv => sv.MaSV == id)
                .Select(sv => new
                {
                    sv.MaSV,
                    sv.TenSV,
                    sv.GioiTinh,
                    sv.NgaySinh,
                    sv.Khoa.TenKhoa // Lấy tên khoa
                })
                .ToListAsync();

            if (sinhVien == null || !sinhVien.Any())
            {
                return NotFound();
            }

            return Ok(sinhVien);
        }


        // GET: api/SinhViens/5
        // Hiển thị sinh viên bằng nhiều trường đầu vào 
        [HttpGet("Search sinh viên theo TenSV, MaKhoa")]
        public async Task<ActionResult<IEnumerable<SinhVien>>> Searching(string keyword)
        {
            var query = _context.SinhViens.AsQueryable();
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
            //Ket qua hien thi
            var results = await query.Select(sv => new
            {
                sv.MaSV,
                sv.TenSV,
                sv.GioiTinh,
                sv.NgaySinh,
                sv.Khoa.TenKhoa,
            }).ToListAsync();

            //NEU DAU VAO 0 CO J THI NOTFOUND
            if (results.Count == 0)
            {
                return NotFound();
            }

            return Ok(results);
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

                // Thay doi tt
                Exist.TenSV = sinhVien.TenSV;
                Exist.NgaySinh = sinhVien.NgaySinh;
                Exist.GioiTinh = sinhVien.GioiTinh;
                //Save data va push db
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
        public async Task<ActionResult<SinhVien>> ThemSinhVien(SinhVien sinhVien)
        {
            var khoaExists = await _context.Khoas.AnyAsync(k => k.KhoaId == sinhVien.KhoaId);
            if (!khoaExists)
            {
                return BadRequest("KhoaId không hợp lệ.");
            }

            _context.SinhViens.Add(sinhVien);
            await _context.SaveChangesAsync();

            return CreatedAtAction("ThemSinhVien", new { maSV = sinhVien.MaSV }, sinhVien);
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
