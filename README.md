
# Kodeqa Ecommerce API

**Author:** @antoniostefanovski `stefanovsky.antonio@gmail.com`

### How to run the application
`dotnet build`
`dotnet run --project KodeqaEcommerce.WebAPI\KodeqaEcommerce.WebAPI.csproj`

### Calculated Results from test cases
- test case 1: Input: productId=PROD-001, quantity=55, country=MK
`finalPrice: 700.92`
- test case 2: productId=PROD-001, quantity=100, country=DE
`finalPrice: 1224.00`
- test case 3: productId=PROD-001, quantity=25, country=USA
`finalPrice: 330.00`

### Implementation Description
Bugs fixed (by the given business rules):
- Fixed discount threshold (check >=500 instead of > 500)
- Fixed incorrect tax calculation
- Corrected discount tier boundaries (by the given rules from the table)
- Corrected discount tier ordering (instead of overriding the value)
- Added SubTotalAfterDiscount since it was missing
Additional Improvements:
- Replaced multiple method parameters with a single DTO to simplify the response-building logic.
- Made the discount threshold configurable via application settings.
- Implemented all missing business rules as defined in the assignment.
- Applied consistent rounding to two decimal places for all monetary values.
- Added request validation using data annotations and automatic model validation via `[ApiController]`.
- Introduced null checks to handle potential invalid or missing data safely.
- Added models based on response and data needs, including `Discount` and `Tax` as complex types, as well as `Product` and `ProductCatalog`.
- Introduced abstractions for the repository and service layers and wired them using dependency injection.