using FluentValidation;
using ProductService.Application.Dtos;
using ProductService.Application.Services;

namespace ProductService.Api.ProductsEnpoints;

public static class ProductServiceEndpoints
{
    public static IEndpointRouteBuilder MapProductServiceEndpoints(this IEndpointRouteBuilder app)
    {
        // GET: /api/products
        app.MapGet("/api/products", async (IProductsService _productService) =>
        {
            var products = await _productService.GetProductsAsync();

            return Results.Ok(products);
        });

        // GET: /api/products/search/{productId}
        app.MapGet("/api/products/search/product-id/{productID:guid}", async (IProductsService _productService, Guid productID) =>
        {
            var product = await _productService.GetProductByConditionAsync(x => x.ProductID == productID);

            return Results.Ok(product);
        });

        // GET: /api/products/search/searchString
        app.MapGet("/api/products/search/{SearchString}", async (IProductsService _productService, string SearchString) =>
        {
            var productsByName = await _productService.GetProductsByConditionAsync(x => x.ProductName != null && 
                                    x.ProductName.Contains(SearchString, StringComparison.OrdinalIgnoreCase));

            var productsByCategory = await _productService.GetProductsByConditionAsync(x => x.Category != null &&
                                    x.Category.Contains(SearchString, StringComparison.OrdinalIgnoreCase));

            var products = productsByName.Union(productsByCategory).ToList();

            return Results.Ok(products);
        });

        // POST: /api/products
        app.MapPost("/api/products", async (IProductsService _productService, 
            IValidator<CreateProductRequest> _validator, 
            CreateProductRequest productRequest) =>
        {
            var validationResult = await _validator.ValidateAsync(productRequest);
            
            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errors = validationResult.Errors.GroupBy(x => x.PropertyName)
                        .ToDictionary(x => x.Key, x => x.Select(e => e.ErrorMessage).ToArray()); // Group errors by property name and create a dictionary

                return Results.ValidationProblem(errors);
            }

            var product = await _productService.AddProductAsync(productRequest);
            if (product != null)
            {
                return Results.Created($"/api/products/search/product-id/{product.ProductID}", product);
            }

            return Results.Problem("An error occurred while creating the product.");
        });

        // PUT: /api/products/{productId}
        app.MapPut("/api/products", async (IProductsService _productService,
            IValidator<UpdateProductRequest> _validator,
            UpdateProductRequest productRequest) =>
        {
            var validationResult = await _validator.ValidateAsync(productRequest);

            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errors = validationResult.Errors.GroupBy(x => x.PropertyName)
                        .ToDictionary(x => x.Key, x => x.Select(e => e.ErrorMessage).ToArray()); // Group errors by property name and create a dictionary

                return Results.ValidationProblem(errors);
            }

            var product = await _productService.UpdateProductAsync(productRequest);
            if (product != null)
            {
                return Results.Ok(product);
            }

            return Results.Problem("An error occurred while updating the product.");
        });

        // DELETE: /api/products/{productId}
        app.MapDelete("/api/products/{productId:guid}", async (IProductsService _productService, Guid productId) =>
        {
            var isDeleted = await _productService.DeleteProductAsync(productId);

            if (isDeleted)
            {
                return Results.NoContent();
            }
            return Results.Problem("An error occurred while deleting the product.");
        });


        return app;
    }
}
