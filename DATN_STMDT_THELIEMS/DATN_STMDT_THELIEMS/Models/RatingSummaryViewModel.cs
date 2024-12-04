namespace DATN_STMDT_THELIEMS.Models
{
    public class RatingSummaryViewModel
    {
        public double AverageRating { get; set; } // Điểm đánh giá trung bình
        public int TotalReviews { get; set; } // Tổng số đánh giá
        public Dictionary<int, int> RatingCounts { get; set; } // Số lượt đánh giá theo từng mức sao (1-5)

        public RatingSummaryViewModel()
        {
            RatingCounts = new Dictionary<int, int>
            {
                { 5, 0 },
                { 4, 0 },
                { 3, 0 },
                { 2, 0 },
                { 1, 0 }
            };
        }
    }
}
