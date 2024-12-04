using System.ComponentModel.DataAnnotations;

namespace DATN_STMDT_THELIEMS.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public int? Parent_id { get; set; }
        public Categories? Parent { get; set; }
        [Required(ErrorMessage = "Tên danh mục không được bỏ trống.")]
        [MaxLength(50, ErrorMessage = "Tên danh mục không được vượt quá 50 ký tự.")]
        public string? Name { get; set; } // nvarchar(50)
        public string? Image { get; set; } // Text
        public byte? Status { get; set; } // Tinyint -> bool
        [Required(ErrorMessage = "Tittle không được bỏ trống.")]
        [MaxLength(50, ErrorMessage = "Title không được vượt quá 50 ký tự.")]
        public string? Title { get; set; } // nvarchar(50)
        [Required(ErrorMessage = "Mô tả không được bỏ trống.")]
        [MaxLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
        public string? Description { get; set; } // Text
        [Required(ErrorMessage = "Keyword không được bỏ trống.")]
        [MaxLength(30, ErrorMessage = "Keyword không được vượt quá 30 ký tự.")]
        public string? Keyword { get; set; } // nvarchar(30)
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public ICollection<Products>? Products { get; set; }
		public ICollection<Attributes>? Attribute { get; set; }
	}
}
