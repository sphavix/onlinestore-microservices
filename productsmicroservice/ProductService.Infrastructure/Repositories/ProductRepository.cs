using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using ProductService.Infrastructure.Persistence;
using ProductService.Infrastructure.Persistence.Models;
using System.Linq.Expressions;

namespace ProductService.Infrastructure.Repositories;

public class ProductRepository(ApplicationDbContext context) : IProductRepository
{
    private readonly ApplicationDbContext _context = context;
    public async Task<Product?> AddProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<bool> DeleteProductAsync(Guid productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductID == productId);

        if(product is null)
        {
            return false;
        }

        _context.Products.Remove(product);
        int rowCount = await _context.SaveChangesAsync();
        return rowCount > 0;
    }

    public async Task<Product?> GetProductByConditionAsync(Expression<Func<Product, bool>> expression)
    {
        return await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(expression);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Product?>> GetProductsByConditionAsync(Expression<Func<Product, bool>> expression)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(expression)
            .ToListAsync();
    }

    public async Task<Product?> UpdateProductAsync(Product product)
    {
        var existingProduct = await _context.Products
            .FirstOrDefaultAsync(x => x.ProductID == product.ProductID);

        if (existingProduct is null)
        {
            return null;
        }

        existingProduct.ProductName = product.ProductName;
        existingProduct.Category = product.Category;
        existingProduct.UnitPrice = product.UnitPrice;
        existingProduct.QuantityInStock = product.QuantityInStock;

        _context.Products.Update(existingProduct);
        await _context.SaveChangesAsync();
        return existingProduct;
    }
}
