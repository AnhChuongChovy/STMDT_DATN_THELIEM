using DATN_STMDT_THELIEMS.Models;

namespace DATN_STMDT_THELIEMS.Services
{
    public interface IProductService
    {
        Task<Products> GetProductsByIdAsync(int id);
        Task<List<Products>> GetSimilarProductsAsync(int productId, int categoryId, int limit = 40);
        Task<List<Products>> GetDiscountedProductsAsync(int limit = 40);
        Task<List<Products>> GetBestSellingProductsAsync(int limit = 40);
        List<Products> GetProducts(int offset, int limit);
        Task<Delivery_address> GetDefaultAddressByUserIdAsync(int id);

        Task<List<ProductReviewViewModel>> GetProductReviewsAsync(int productId, bool onlyWithImages = false);
        Task<RatingSummaryViewModel> GetRatingSummaryAsync(int productId);
        Task<List<ProductReviewViewModel>> GetFilteredReviewsAsync(int productId, string filter);
    }
}
