namespace DATN_STMDT_THELIEMS.Models
{
    public class ProductReviewViewModel
    {
        public int ReviewId { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int LikeCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> MediaUrls { get; set; }
    }
}
