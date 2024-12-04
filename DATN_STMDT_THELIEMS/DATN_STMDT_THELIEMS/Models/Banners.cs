using System.ComponentModel.DataAnnotations;

namespace DATN_STMDT_THELIEMS.Models
{
    public class Banners
    {
        [Key]
        public int Id { get; set; }
        public string? Image { get; set; }
        public byte? Status { get; set; }
    }
}
