
using System.ComponentModel.DataAnnotations;

namespace DATN_STMDT_THELIEMS.Models
{
    public class Products
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn danh mục sản phẩm.")]
        public int? Category_id { get; set; }
        public Categories? Categories { get; set; }
        public int? Supplier_id { get; set; }
        public Supplier? Supplier { get; set; }
        public int? Shop_id { get; set; }
        public Shops? Shops { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn thương hiệu sản phẩm.")]
        public int? Brand_id { get; set; }
        public Brands? Brands { get; set; }
        public string? Product_link { get; set; }
        [Required(ErrorMessage = "SKU sản phẩm không được để trống.")]
        [MaxLength(20, ErrorMessage = "SKU sản phẩm không được vượt quá 20 ký tự.")]
        public string? Sku { get; set; }
        [Required(ErrorMessage = "Tên sản phẩm không được để trống.")]
        [MaxLength(120, ErrorMessage = "Tên sản phẩm không được vượt quá 120 ký tự.")]
        public string? Name { get; set; }
        public string? Image { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá sản phẩm.")]
        public int? Price { get; set; }
        public int? Percent_Decrease { get; set; }
        [Required(ErrorMessage = "Cân nặng sản phẩm không được để trống.")]
        public int? Weight { get; set; }
        [Required(ErrorMessage = "Chiều cao sản phẩm không được để trống.")]
        public int? Height { get; set; }
        [Required(ErrorMessage = "Chiều rộng sản phẩm không được để trống.")]
        public int? Width { get; set; }
        [Required(ErrorMessage = "Chiều dài sản phẩm không được để trống.")]
        public int? Long { get; set; }
        [MaxLength(500, ErrorMessage = "Mô tả sản phẩm không được vượt quá 500 ký tự.")]
        public string? Description { get; set; }
        public int? View_count { get; set; }
        public int? Sold_count { get; set; }
        public string? Meta_title { get; set; }
        public string? Meta_keyword { get; set; }
        public byte? Status { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public ICollection<Product_variants>? Product_Variants { get; set; }
        public ICollection<Product_parts>? Product_Parts { get; set; }
		public ICollection<Product_attribute>? product_Attributes { get; set; }

		public int TotalQuantity => Product_Variants?.Sum(v => v.Quantity) ?? 0;
        public decimal? DiscountedPrice
        {
            get
            {
                if (Price.HasValue && Percent_Decrease.HasValue)
                {
                    return Price.Value * (1 - Percent_Decrease.Value / 100m);
                }
                return null;
            }
        }
    }
}
