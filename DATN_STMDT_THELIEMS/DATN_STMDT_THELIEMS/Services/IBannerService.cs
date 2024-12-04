using DATN_STMDT_THELIEMS.Models;

namespace DATN_STMDT_THELIEMS.Services
{
    public interface IBannerService
    {
        Task<List<Banners>> GetAllBannersAsync();
        Task<Banners> GetBannerByIdAsync(int id);
    }
}
