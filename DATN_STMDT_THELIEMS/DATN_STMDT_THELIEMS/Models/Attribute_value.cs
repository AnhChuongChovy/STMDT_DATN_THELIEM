using System.ComponentModel.DataAnnotations;

namespace DATN_STMDT_THELIEMS.Models
{
    public class Attribute_value
    {
        [Key]
        public int Id { get; set; }
        public int Attribute_id {  get; set; }
        public Attributes? Attributes { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public string? Name { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public ICollection<Product_attribute>? product_Attributes { get; set; }

    }
}
