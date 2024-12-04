using DATN_STMDT_THELIEMS.DATA;
using DATN_STMDT_THELIEMS.Models;
using DATN_STMDT_THELIEMS.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

public class CartService : ICartService
{
    private readonly AppDBContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string CartSessionKey = "CartItems";
    private ISession Session => _httpContextAccessor.HttpContext?.Session;

    public CartService(AppDBContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task AddToCartAsync(int productId, int quantity, string color, string size)
    {
        var product = await _context.PRODUCTS.FindAsync(productId);
        if (product == null) return;

        var cart = GetCartFromSession();
        var index = cart.FindIndex(ci => ci.Product.Id == productId && ci.Color == color && ci.Size == size);

        if (index != -1)
        {
            // Update the quantity for the existing product with the same color and size
            var existingItem = cart[index];
            cart[index] = (existingItem.Product, existingItem.Quantity + quantity, existingItem.Color, existingItem.Size);
        }
        else
        {
            // Add a new item to the cart with color and size
            cart.Add((product, quantity, color, size));
        }

        SaveCartToSession(cart);
    }

    public async Task<List<(Products Product, int Quantity, string Color, string Size)>> GetCartItemsAsync()
    {
        var cart = GetCartFromSession();

        // Get the product IDs from the cart
        var productIds = cart.Select(ci => ci.Product.Id).ToList();

        if (!productIds.Any())
            return new List<(Products Product, int Quantity, string Color, string Size)>();

        // Query the database for products with their Shops included
        var products = await _context.PRODUCTS
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
                                     .Where(p => productIds.Contains(p.Id))
                                     .ToListAsync();

        // Reconstruct the cart with product details, including color and size
        var detailedCart = cart.Select(ci =>
            (products.First(p => p.Id == ci.Product.Id), ci.Quantity, ci.Color, ci.Size))
            .ToList();

        return detailedCart;
    }

    public async Task RemoveFromCartAsync(int productId)
    {
        var cart = GetCartFromSession();
        cart.RemoveAll(ci => ci.Product.Id == productId);
        SaveCartToSession(cart);
    }


    private List<(Products Product, int Quantity, string Color, string Size)> GetCartFromSession()
    {
        var cartData = Session?.GetString(CartSessionKey);
        return string.IsNullOrEmpty(cartData)
            ? new List<(Products Product, int Quantity, string Color, string Size)>()
            : JsonConvert.DeserializeObject<List<(Products Product, int Quantity, string Color, string Size)>>(cartData);
    }

    private void SaveCartToSession(List<(Products Product, int Quantity, string Color, string Size)> cart)
    {
        var cartData = JsonConvert.SerializeObject(cart);
        Session?.SetString(CartSessionKey, cartData);
    }
}
