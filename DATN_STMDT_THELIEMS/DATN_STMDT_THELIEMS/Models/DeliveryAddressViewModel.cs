namespace DATN_STMDT_THELIEMS.Models
{
    public class DeliveryAddressViewModel
    {
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int WardId { get; set; }
        public string WardName { get; set; }
        public string Note { get; set; }
    }
}
