using System.ComponentModel.DataAnnotations;

namespace DATN_STMDT_THELIEMS.Models
{
    public class Product_attribute
    {
        [Key]
        public int Id { get; set; }
		public int? Attribute_value_id { get; set; }
		public Attribute_value? attribute_Value { get; set; }
        public int? Product_id { get; set; }
		public Products? Products { get; set; }
		public DateTime? Create_at { get; set; }
		public DateTime? Update_at { get; set; }
    }
}
