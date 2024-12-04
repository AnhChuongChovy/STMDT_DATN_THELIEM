using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace DATN_STMDT_THELIEMS.Models
{
    public class Users
    {
        public int Id { get; set; }
        public int Role_id { get; set; }
        public Role? Role { get; set; }
        public byte? Is_seller { get; set; }
        [StringLength(25, ErrorMessage = "Password không được quá 25 ký tự")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống tên của bạn.")]
        public string? Full_name { get; set; }
        public string? Gender { get; set; }
        public string? Birthday { get; set; }
        [Required(ErrorMessage = "Email không được bỏ trống.")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        [StringLength(50, ErrorMessage = "Email không được quá 50 ký tự")]
        public string? Email { get; set; }
        [StringLength(10, ErrorMessage = "Số điện thoại phải có 10 số"),MinLength(10)]
        public string? Phone { get; set; }
		public byte? Status { get; set; }
		public string? Image { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public Shops? Shops { get; set; }
        public ICollection<Orders>? Orders { get; set; }
        public ICollection<Voucher>? Vouchers { get; set; }

        public ICollection<Delivery_address>? Delivery_Addresses { get; set; }
        public ICollection<User_shop_follow>? User_Shop_Follows { get; set; }
        public ICollection<User_shop_rating>? User_Shop_Ratings { get; set; }
        public ICollection<Product_review>? Product_Reviews { get; set; }

        [NotMapped]
        public string? ConfirmPassword { get; set; }
        [NotMapped]
        public string? Erorr { get; set; }

        public bool IsValidPhoneNumber()
        {
            // Biểu thức chính quy kiểm tra số điện thoại Việt Nam
            string phonePattern = @"^(0[3|5|7|8|9])+([0-9]{8})$";

            return Regex.IsMatch(this.Phone ?? string.Empty, phonePattern);
        }
    }
}
