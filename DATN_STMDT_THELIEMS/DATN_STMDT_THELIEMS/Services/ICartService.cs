using DATN_STMDT_THELIEMS.Models;
using Microsoft.CodeAnalysis;
using System.Drawing;

namespace DATN_STMDT_THELIEMS.Services
{
    public interface ICartService
    {
        Task AddToCartAsync(int productId, int quantity, string color, string size);
        Task<List<(Products Product, int Quantity, string Color, string Size)>> GetCartItemsAsync(); // Update return type
        Task RemoveFromCartAsync(int productId);
    }
}
