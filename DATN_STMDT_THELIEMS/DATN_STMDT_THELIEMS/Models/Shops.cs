using System.ComponentModel.DataAnnotations;

namespace DATN_STMDT_THELIEMS.Models
{
    public class Shops
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public Users? Users { get; set; }
        [Required(ErrorMessage = "Tên shop không được bỏ trống.")]
        [StringLength(50, ErrorMessage = "Tên shop không được vượt quá 50 ký tự")]
        public string? Name { get; set; }
        public string? Image { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [StringLength(10, ErrorMessage = "Số điện thoại không được vượt quá 10 ký tự")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [StringLength(50, ErrorMessage = "Email không được vượt quá 50 ký tự")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
        [StringLength(150, ErrorMessage = "Địa chỉ không được vượt quá 150 ký tự")]
        public string? Address { get; set; }
        [StringLength(1000, ErrorMessage = "Địa chỉ không được vượt quá 1000 ký tự")]
        public string? Description { get; set; }
        public byte Status { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public ICollection<User_shop_follow>? User_Shop_Follows { get; set; }
        public ICollection<User_shop_rating>? User_Shop_Ratings { get; set; }
        public ICollection<Products>? Products { get; set; }
        public ICollection<Orders>? Orders { get; set; }

    }
}
