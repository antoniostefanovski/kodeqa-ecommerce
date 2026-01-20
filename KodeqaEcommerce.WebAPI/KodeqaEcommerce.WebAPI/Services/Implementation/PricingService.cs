using KodeqaEcommerce.WebAPI.Configuration;
using KodeqaEcommerce.WebAPI.DataContext.Interface;
using KodeqaEcommerce.WebAPI.Models;
using KodeqaEcommerce.WebAPI.Services.Interface;
using Microsoft.Extensions.Options;

namespace KodeqaEcommerce.WebAPI.Services.Implementation
{
    public class PricingService
        : IPricingService
    {
        private readonly IProductRepository productRepository;
        private readonly PricingOptions pricingOptions;

        public PricingService(IProductRepository productRepository, IOptions<PricingOptions> pricingOptions)
        {
            this.productRepository = productRepository;
            this.pricingOptions = pricingOptions.Value;
        }

        public PricingResponse CalculatePrice(OrderRequest orderRequest)
        {
            if (orderRequest == null)
                throw new ArgumentNullException(nameof(orderRequest));

            var productId = orderRequest.ProductId;
            var quantity = orderRequest.Quantity;
            var country = orderRequest.Country;

            var product = GetProduct(productId);
            var subTotal = quantity * product.Price;

            var discountPct = CalculateDiscount(quantity, subTotal);
            var discountAmount = subTotal * discountPct;

            var subtotalAfterDiscount = subTotal - discountAmount;

            var taxRate = GetTaxRate(country);
            var taxAmount = subtotalAfterDiscount * taxRate;

            var finalPrice = subtotalAfterDiscount + taxAmount;

            var buildResponseDto = new BuildResponseDto
            {
                Product = product,
                OrderRequest = orderRequest,
                SubTotal = subTotal,
                SubTotalAfterDiscount = subtotalAfterDiscount,
                DiscountPct = discountPct,
                DiscountAmount = discountAmount,
                TaxRate = taxRate,
                TaxAmount = taxAmount,
                FinalPrice = finalPrice
            };

            var pricingResponse = BuildResponse(buildResponseDto);

            return pricingResponse;
        }

        private Product GetProduct(string productId)
        {
            var product = productRepository.Get(productId);

            return product;
        }

        private decimal GetTaxRate(string country)
        {
            return country switch
            {
                "MK" => 0.18m,
                "DE" => 0.20m,
                "FR" => 0.20m,
                "USA" => 0.10m,
                _ => throw new ArgumentException($"Unsupported country: {country}")
            };
        }

        private decimal CalculateDiscount(int quantity, decimal subtotal)
        {
            decimal discount = 0;

            if (subtotal >= pricingOptions.DiscountThreshold)
            {
                if (quantity >= 100)
                    discount = 0.15m;
                else if (quantity >= 50)
                    discount = 0.10m;
                else if (quantity >= 10)
                    discount = 0.05m;
            }

            return discount;
        }

        private PricingResponse BuildResponse(
            BuildResponseDto buildResponseDto)
        {
            var discount = new Discount
            {
                Amount = Math.Round(buildResponseDto.DiscountAmount, 2, MidpointRounding.AwayFromZero),
                Percentage = Math.Round(buildResponseDto.DiscountPct, 2, MidpointRounding.AwayFromZero)
            };

            var tax = new Tax
            {
                Amount = Math.Round(buildResponseDto.TaxAmount, 2, MidpointRounding.AwayFromZero),
                Rate = Math.Round(buildResponseDto.TaxRate, 2, MidpointRounding.AwayFromZero),
                Country = buildResponseDto.OrderRequest.Country
            };

            var response = new PricingResponse
            {
                ProductId = buildResponseDto.Product.Id,
                ProductName = buildResponseDto.Product.Name,
                Quantity = buildResponseDto.OrderRequest.Quantity,
                UnitPrice = Math.Round(buildResponseDto.Product.Price, 2, MidpointRounding.AwayFromZero),
                Country = buildResponseDto.OrderRequest.Country,
                SubTotal = Math.Round(buildResponseDto.SubTotal, 2, MidpointRounding.AwayFromZero),
                Discount = discount,
                SubTotalAfterDiscount = Math.Round(buildResponseDto.SubTotalAfterDiscount, 2, MidpointRounding.AwayFromZero),
                Tax = tax,
                FinalPrice = Math.Round(buildResponseDto.FinalPrice, 2, MidpointRounding.AwayFromZero)
            };

            return response;
        }
    }
}
