using DATN_STMDT_THELIEMS.DATA;
using DATN_STMDT_THELIEMS.Models;
using Microsoft.EntityFrameworkCore;

namespace DATN_STMDT_THELIEMS.Services
{
    public class BannerService : IBannerService
    {
        private readonly AppDBContext _context;

        public BannerService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Banners>> GetAllBannersAsync()
        {
            return await _context.BANNERS.ToListAsync(); // Lấy toàn bộ banner từ bảng Banners
        }

        public async Task<Banners> GetBannerByIdAsync(int id)
        {
            return await _context.BANNERS.FirstOrDefaultAsync(b => b.Id == id); // Lấy banner theo Id
        }
    }
}
