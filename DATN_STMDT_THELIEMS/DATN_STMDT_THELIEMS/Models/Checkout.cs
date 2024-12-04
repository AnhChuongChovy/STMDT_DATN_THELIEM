namespace DATN_STMDT_THELIEMS.Models
{
    public class Checkout
    {
        public Products Product { get; set; }
        public int ProductId { get; set; }
        public int? Shop_id { get; set; }
        public Shops Shops { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int? Price { get; set; }
        public int Shipping_method { get; set; }
        public int? Percent_Decrease { get; set; }
        public int? TotalPrice { get; set; }
        public int? TotalQuantity { get; set; }
        public int? ShippingFee { get; set; }
        public int? ShippingDiscount { get; set; }
        public int? PromotionDiscount { get; set; }
        public int? TotalPayment { get; set; }
        public int? DiscountedPrice { get; set; }
        public string PaymentMethod { get; set; }
        public byte? Payment_status { get; set; }

        // Thông tin người nhận
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
    }

}
