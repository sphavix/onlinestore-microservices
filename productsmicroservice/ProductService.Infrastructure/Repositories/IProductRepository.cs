using ProductService.Infrastructure.Persistence.Models;
using System.Linq.Expressions;

namespace ProductService.Infrastructure.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<IEnumerable<Product?>> GetProductsByConditionAsync(Expression<Func<Product, bool>> expression);
    Task<Product?> GetProductByConditionAsync(Expression<Func<Product, bool>> expression);
    Task<Product?> AddProductAsync(Product product);
    Task<Product?> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(Guid productId);


}
