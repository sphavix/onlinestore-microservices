using AutoMapper;
using FluentValidation;
using ProductService.Application.Dtos;
using ProductService.Infrastructure.Persistence.Models;
using ProductService.Infrastructure.Repositories;
using System.Linq.Expressions;

namespace ProductService.Application.Services;

public class ProductsService(
    IValidator<CreateProductRequest> createProductValidator,
    IValidator<UpdateProductRequest> updateProductValidator,
    IMapper mapper,
    IProductRepository productRepository) : IProductsService
{
    private readonly IValidator<CreateProductRequest> _createProductValidator = createProductValidator;
    private readonly IValidator<UpdateProductRequest> _updateProductValidator = updateProductValidator;
    private readonly IMapper _mapper = mapper;
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<ProductRespone?> AddProductAsync(CreateProductRequest productRequest)
    {
        if(productRequest is null)
        {
            throw new ArgumentNullException(nameof(productRequest));
        }

        // Validate the product request
        var validationResult = await _createProductValidator.ValidateAsync(productRequest);

        if(!validationResult.IsValid)
        {
            string errors = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)); // Collect all error messages

            throw new ArgumentException(errors); // Throw an exception with all error messages
        }

        // Map the request to the Product entity
        var product = _mapper.Map<Product>(productRequest);

        // Add the product to the repository
        var addedProduct = await _productRepository.AddProductAsync(product);

        if (addedProduct is null)
        {
            throw new InvalidOperationException("Failed to add product to the repository.");
        }

        // Map the added product back to the response DTO
        var productResponse = _mapper.Map<ProductRespone>(addedProduct);

        return productResponse;
    }

    public async Task<bool> DeleteProductAsync(Guid productId)
    {
        // Check if the productId is valid
        var product = await _productRepository.GetProductByConditionAsync(p => p.ProductID == productId);

        if (product is null)
        {
            return false; // Return false if the product does not exist
        }

        // Call the repository to delete the product
        var isDeleted = await _productRepository.DeleteProductAsync(productId);

        return isDeleted; // Return the result of the deletion operation
    }

    public async Task<ProductRespone?> GetProductByConditionAsync(Expression<Func<Product, bool>> expression)
    {
        // Call the repository to get the product by condition
        var product = await _productRepository.GetProductByConditionAsync(expression);

        if (product is null)
        {
            return null; // Return null if no product is found
        }

        // Map the product to the response DTO
        var productResponse = _mapper.Map<ProductRespone>(product);

        return productResponse;
    }

    public async Task<List<ProductRespone?>> GetProductsAsync()
    {
        var products = await _productRepository.GetProductsAsync();

        var productResponse = _mapper.Map<IEnumerable<ProductRespone?>>(products);

        return productResponse.ToList(); // Convert to List and return
    }

    public async Task<List<ProductRespone?>> GetProductsByConditionAsync(Expression<Func<Product, bool>> expression)
    {
        var products = await _productRepository.GetProductsByConditionAsync(expression);

        var productResponse = _mapper.Map<IEnumerable<ProductRespone?>>(products);

        return productResponse.ToList(); // Convert to List and return
    }

    public async Task<ProductRespone?> UpdateProductAsync(UpdateProductRequest productRequest)
    {
        // Check if the productRequest is null
        var product = await _productRepository.GetProductByConditionAsync(p => p.ProductID == productRequest.ProductID);

        if (product is null)
        {
            throw new ArgumentException($"Product with ID {productRequest.ProductID} does not exist.");
        }

        // Validate the product request
        var validationResult = await _updateProductValidator.ValidateAsync(productRequest);

        if (!validationResult.IsValid)
        {
            string errors = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)); // Collect all error messages
            throw new ArgumentException(errors); // Throw an exception with all error messages
        }

        // Map the request to the Product entity
        var updatedProduct = _mapper.Map<Product>(productRequest);

        // Update the product in the repository
        var productResult = await _productRepository.UpdateProductAsync(updatedProduct);

        // Map the updated product back to the response DTO
        var productResponse = _mapper.Map<ProductRespone>(productResult);

        return productResponse;
    }
}
