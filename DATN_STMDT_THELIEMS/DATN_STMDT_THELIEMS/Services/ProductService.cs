using DATN_STMDT_THELIEMS.DATA;
using DATN_STMDT_THELIEMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DATN_STMDT_THELIEMS.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDBContext _context;

        public ProductService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Products> GetProductsByIdAsync(int id)
        {
            return await _context.PRODUCTS
                .Include(p => p.Brands)
                .Include(p => p.Product_Variants)
                    .ThenInclude(v => v.Product_Images)
                .Include(p => p.Product_Variants)
                    .ThenInclude(v => v.Product_Variant_Options)
                        .ThenInclude(o => o.variant_values)
                        .ThenInclude(i => i.Variant_Options)
                .Include(p => p.Product_Parts)
                    .ThenInclude(pp => pp.Product_Part_Images)
                .Include(p => p.product_Attributes)
                    .ThenInclude(a => a.Attributes)
                    .ThenInclude(a => a.Category)
                .Include(s => s.Shops)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Products>> GetSimilarProductsAsync(int productId, int categoryId, int limit = 40)
        {
            return await _context.PRODUCTS
                .Where(p => p.Id != productId && p.Category_id == categoryId)
                .OrderBy(p => p.Id)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<List<Products>> GetDiscountedProductsAsync(int limit = 40)
        {
            return await _context.PRODUCTS
                .Where(p => p.Percent_Decrease.HasValue && p.Percent_Decrease > 0)
                .OrderByDescending(p => p.Percent_Decrease)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<List<Products>> GetBestSellingProductsAsync(int limit = 40)
        {
            return await _context.PRODUCTS
                .Where(p => p.Sold_count.HasValue)
                .OrderByDescending(p => p.Sold_count)
                .Take(limit)
                .ToListAsync();
        }

        public List<Products> GetProducts(int offset, int limit)
        {
            return _context.PRODUCTS
                           .OrderBy(p => p.Id)
                           .Skip(offset)
                           .Take(limit)
                           .ToList();
        }

        public async Task<Delivery_address> GetDefaultAddressByUserIdAsync(int id)
        {
            return await _context.DELIVERY_ADDRESSES
                .FirstOrDefaultAsync(a => a.User_id == id);
        }

        public async Task<List<ProductReviewViewModel>> GetProductReviewsAsync(int productId, bool onlyWithImages = false)
        {
            return await _context.PRODUCT_REVIEWS
                .Where(r => r.Order_details.Product_variants.Product_id == productId)
                .Include(r => r.User) // Lấy thông tin người dùng
                .Include(r => r.Review_Medias) // Lấy thông tin hình ảnh đánh giá
                .Include(r => r.Order_details) // Lấy thông tin chi tiết đơn hàng
                .Select(r => new ProductReviewViewModel
                {
                    ReviewId = r.Id,
                    UserName = r.User.Full_name,
                    UserImage = r.User.Image,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.Created_at,
                    LikeCount = r.Like_count,
                    MediaUrls = r.Review_Medias.Select(m => m.Media).ToList()
                })
                .ToListAsync();
        }

        public async Task<RatingSummaryViewModel> GetRatingSummaryAsync(int productId)
        {
            var reviews = await _context.PRODUCT_REVIEWS
                .Where(r => r.Order_details.Product_variants.Product_id == productId)
                .ToListAsync();

            var summary = new RatingSummaryViewModel
            {
                TotalReviews = reviews.Count,
                AverageRating = reviews.Count > 0 ? reviews.Average(r => r.Rating) : 0
            };

            // Tính số lượng đánh giá theo từng mức sao
            var ratingGroups = reviews
                .GroupBy(r => r.Rating)
                .ToDictionary(g => g.Key, g => g.Count());

            foreach (var ratingGroup in ratingGroups)
            {
                summary.RatingCounts[ratingGroup.Key] = ratingGroup.Value;
            }

            return summary;
        }


        public async Task<List<ProductReviewViewModel>> GetFilteredReviewsAsync(int productId, string filter)
        {
            var query = _context.PRODUCT_REVIEWS
                .Where(r => r.Order_details.Product_variants.Product_id == productId)
                .Include(r => r.User)
                .Include(r => r.Review_Medias)
                .AsQueryable();

            if (filter == "newest")
            {
                query = query.OrderByDescending(r => r.Created_at);
            }
            else if (filter == "hasImages")
            {
                query = query.Where(r => r.Review_Medias.Any());
            }

            return await query.Select(r => new ProductReviewViewModel
            {
                ReviewId = r.Id,
                UserName = r.User.Full_name,
                UserImage = r.User.Image,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.Created_at,
                LikeCount = r.Like_count,
                MediaUrls = r.Review_Medias.Select(m => m.Media).ToList()
            }).ToListAsync();
        }
    }
}
