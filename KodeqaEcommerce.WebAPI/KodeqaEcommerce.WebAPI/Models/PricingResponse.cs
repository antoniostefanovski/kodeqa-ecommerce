namespace KodeqaEcommerce.WebAPI.Models
{
    public class PricingResponse
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Country { get; set; } = string.Empty;
        public decimal SubTotal { get; set; }
        public Discount? Discount { get; set; }
        public decimal SubTotalAfterDiscount { get; set; }
        public Tax? Tax { get; set; }
        public decimal FinalPrice { get; set; }
    }
}
