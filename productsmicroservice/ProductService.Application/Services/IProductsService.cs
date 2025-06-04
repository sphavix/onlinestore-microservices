using ProductService.Application.Dtos;
using ProductService.Infrastructure.Persistence.Models;
using System.Linq.Expressions;

namespace ProductService.Application.Services;
public interface IProductsService
{
    Task<List<ProductRespone?>> GetProductsAsync();
    Task<List<ProductRespone?>> GetProductsByConditionAsync(Expression<Func<Product, bool>> expression);
    Task<ProductRespone?> GetProductByConditionAsync(Expression<Func<Product, bool>> expression);
    Task<ProductRespone?> AddProductAsync(CreateProductRequest productRequest);
    Task<ProductRespone?> UpdateProductAsync(UpdateProductRequest productRequest);
    Task<bool> DeleteProductAsync(Guid productId);
}
