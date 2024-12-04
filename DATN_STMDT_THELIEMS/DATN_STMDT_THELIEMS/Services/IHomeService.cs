using DATN_STMDT_THELIEMS.Models;

namespace DATN_STMDT_THELIEMS.Services
{
    public interface IHomeService
    {
        Task<IEnumerable<Products>> GetAllProducts();
        Task<IEnumerable<Banners>> GetAllBanners();
        Task<IEnumerable<Brands>> GetAllBrands();
        Task<IEnumerable<Categories>> GetAllCategories();
        Task<IEnumerable<Products>> GetProductsBySearchAsync(string searchString, string sortOrder, int page = 1, int pageSize = 20);

    }

}
