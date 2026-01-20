namespace KodeqaEcommerce.WebAPI.Models
{
    public class BuildResponseDto
    {
        public Product Product { get; set; }
        public OrderRequest OrderRequest { get; set; }
        public decimal SubTotal { get; set; }
        public decimal SubTotalAfterDiscount { get; set; }
        public decimal DiscountPct { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal FinalPrice { get; set; }
    }
}
