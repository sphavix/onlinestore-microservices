namespace ProductService.Application.Dtos;

public record ProductRespone(
    Guid ProductID,
    string ProductName,
    CategoryOptions Category,
    double? UnitPrice,
    int? QuantityInStock)
{
    public ProductRespone()
        : this(default, default, default, default, default)
    {
    }
}
