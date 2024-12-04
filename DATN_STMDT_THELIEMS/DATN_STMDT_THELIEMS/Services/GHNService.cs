namespace DATN_STMDT_THELIEMS.Services
{
    public class GHNService : IGHNService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GHNService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<Province>> GetProvincesAsync()
        {
            var client = _httpClientFactory.CreateClient("GHN");
            var response = await client.GetAsync("master-data/province");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<Province>>>();
            return result?.Data ?? new List<Province>();
        }

        public async Task<List<District>> GetDistrictsAsync(int provinceId)
        {
            var client = _httpClientFactory.CreateClient("GHN");
            var response = await client.PostAsJsonAsync("master-data/district", new { province_id = provinceId });
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<District>>>();
            return result?.Data ?? new List<District>();
        }

        public async Task<List<Ward>> GetWardsAsync(int districtId)
        {
            var client = _httpClientFactory.CreateClient("GHN");
            var response = await client.PostAsJsonAsync("master-data/ward", new { district_id = districtId });
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<Ward>>>();
            return result?.Data ?? new List<Ward>();
        }
    }

    // Các lớp mô hình phản hồi từ GHN API
    public class ApiResponse<T>
    {
        public int Code { get; set; }
        public T Data { get; set; }
    }

    public class Province
    {
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
    }

    public class District
    {
        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
    }

    public class Ward
    {
        public string WardCode { get; set; }
        public string WardName { get; set; }
    }
}
