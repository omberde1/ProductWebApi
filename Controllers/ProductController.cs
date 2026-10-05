using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductWebApi.Data.InMemory;
using ProductWebApi.DTOs.Product;
using ProductWebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProductWebApi.Controllers;

[Route("api/products")]
[ApiController]
public class ProductController : ControllerBase
{
    public ProductStore _store;
    public ProductController(ProductStore store)
    {
        _store = store;
    }

    [HttpGet]
    public IActionResult GetPaginatedProductsByFilters(ProductCategory? category, decimal? minPrice, decimal? maxPrice, bool? inStock, ProductSortByFilter? sortBy, int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            if(pageSize > 100) return BadRequest("Max pageSize value is 100");
            if (pageSize % 10 != 0) return BadRequest("pageSize must be a multiple of 10");

            var products = _store.Products.AsEnumerable().Skip((pageNumber - 1) * 10).Take(pageSize);
            if (category is not null) products = products.Where(p => p.Category == category);
            if (minPrice is not null) products =  products.Where(p => p.Price >= minPrice);
            if (maxPrice is not null) products = products.Where(p => p.Price <= maxPrice);
            if (inStock is not null) products = products.Where(p => inStock == true ? p.Stock > 0 : p.Stock == 0);

            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex}");
        }
    }
    [HttpGet("{id:int}")]
    public IActionResult GetProductById(int id)
    {
        try
        {
            if (id <= 0) return BadRequest("Id cannot be (lower than/equal to) 0.");

            var product = _store.Products.FirstOrDefault(p => p.Id == id);
            if (product is null) return NotFound("Product with supplied Id was not found.");

            return Ok(product);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex}");
        }
    }
    [Authorize]
    [HttpPost]
    public IActionResult AddProduct([FromBody] AddProductDto productRequest)
    {
        try
        {
            // #IMP# : After making Enum as string globally,
            // while adding/updating in request body we can pass either idx:int or name:string both without any hassle for Category
            // for now commented the below as model validation applied on DTO
            // if (!Enum.IsDefined<ProductCategory>(product.Category)) return BadRequest("Incorrect Category");

            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdStr is null) return Unauthorized();
            int userId = int.Parse(userIdStr);

            var newProduct = new Product
            {
                Id = _store.GetIncrementedId(),
                Name = productRequest.Name,
                Price = productRequest.Price,
                Stock = productRequest.Stock,
                Category = productRequest.Category,
                AddedBy = userId
            };
            _store.Products.Add(newProduct);

            return Created($"api/products/{newProduct.Id}", newProduct);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex}");
        }
    }
    [Authorize]
    [HttpPut("{id:int}")]
    public IActionResult EditProduct(int id, [FromBody] EditProductDto productRequest)
    {
        try
        {
            var existingProduct = _store.Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct is null) return NotFound("Product with supplied Id was not found.");

            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdStr is null) return Unauthorized();
            int userId = int.Parse(userIdStr);

            // #IMP# direct updates reflect automatically in the list
            existingProduct.Name = productRequest.Name;
            existingProduct.Price = productRequest.Price;
            existingProduct.Stock = productRequest.Stock;
            existingProduct.Category = productRequest.Category;
            existingProduct.LastUpdated = DateTime.UtcNow;
            existingProduct.LastUpdatedBy = userId;

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex}");
        }
    }
    [Authorize(Roles = "admin,product_admin")] // #IMP# Multiple roles
    [HttpDelete("{id:int}")]
    public IActionResult DeleteProduct(int id)
    {
        try
        {
            if (id <= 0) return BadRequest("Id cannot be (lower than/equal to) 0.");

            var product = _store.Products.FirstOrDefault(p => p.Id == id);
            if (product is null) return NotFound("Product with supplied Id was not found.");

            _store.Products.Remove(product);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex}");
        }
    }


}