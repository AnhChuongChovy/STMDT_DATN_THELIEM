namespace DATN_STMDT_THELIEMS.Services
{
    public interface IGHNService
    {
        Task<List<Province>> GetProvincesAsync();
        Task<List<District>> GetDistrictsAsync(int provinceId);
        Task<List<Ward>> GetWardsAsync(int districtId);
    }
}
