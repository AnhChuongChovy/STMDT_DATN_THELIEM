using System.Collections.Generic;
using System.Linq;
using DATN_STMDT_THELIEMS.Models;
using DATN_STMDT_THELIEMS.DATA;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Data.SqlClient;

namespace DATN_STMDT_THELIEMS.Services
{
    public class HomeService : IHomeService
    {
        private readonly AppDBContext _context;

        public HomeService(AppDBContext context)
        {
            _context = context;
        }

        // phương thức đổ dữ liệu tất cả sp

        public async Task<IEnumerable<Products>> GetAllProducts()
        {
            return await _context.PRODUCTS.ToListAsync();

        }

        // Tính năng tìm kiếm sp 
        public async Task<IEnumerable<Products>> GetProductsBySearchAsync(string searchString, string sortOrder = null, int page = 1, int pageSize = 20)
        {
            IQueryable<Products> query = _context.PRODUCTS.Where(p => p.Status == 1); // Filter by active status

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => EF.Functions.Like(p.Name, $"%{searchString}%") || p.Name.StartsWith(searchString));

                query = query
                    .OrderByDescending(p => p.Name.StartsWith(searchString))  // Prioritize exact matches
                    .ThenByDescending(p => EF.Functions.Like(p.Name, $"{searchString}%"))  // Partial matches at start
                    .ThenBy(p => p.Name);  // Alphabetical order for remaining
            }
            else
            {
                query = query.OrderBy(p => p.Name); // Default sort when no search term is provided
            }

            // Tính năng lọc
            query = sortOrder switch
            {
                "priceAsc" => query.OrderBy(p => p.Price),
                "priceDesc" => query.OrderByDescending(p => p.Price),
                _ => query
            };

            // Apply pagination
            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }


        // Thêm đổ banner lên carousel

        public async Task<IEnumerable<Banners>> GetAllBanners()
        {
            return await _context.BANNERS.ToListAsync();

        }

        public async Task<IEnumerable<Brands>> GetAllBrands()
        {
            return await _context.BRANDS.ToListAsync();

        }
        public async Task<IEnumerable<Categories>> GetAllCategories()
        {
            return await _context.CATEGORIES.ToListAsync();

        }

    }
}
    