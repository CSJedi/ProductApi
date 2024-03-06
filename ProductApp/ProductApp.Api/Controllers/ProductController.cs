using Microsoft.AspNetCore.Mvc;
using ProductApp.Core.Interfaces;
using ProductApp.DAL.Models;
using ProductApp.Infrastructure.Logging.Interfaces;

namespace ProductApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private readonly IProductService _productService;

        public ProductController(IProductService productService, ILoggingService loggingService)
        {
            _productService = productService;
            _loggingService = loggingService;
        }

        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            _loggingService.Log("GetAllProducts method called");
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("GetProduct")]
        public IActionResult GetProduct(int id)
        {
            _loggingService.Log($"GetProduct method called with id: {id}");
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                _loggingService.Log($"Product with id {id} not found");
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromBody] Product product)
        {
            _loggingService.Log($"AddProduct method called with product: {product}");
            _productService.AddProduct(product);
            return CreatedAtAction(nameof(AddProduct), new { id = product.Id }, product);
        }

        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            _loggingService.Log($"UpdateProduct method called with id: {id} and product: {product}");
            var existingProduct = _productService.GetProductById(id);
            if (existingProduct == null)
            {
                _loggingService.Log($"Product with id {id} not found");
                return NotFound();
            }
            _productService.UpdateProduct(product);
            return NoContent();
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(int id)
        {
            _loggingService.Log($"DeleteProduct method called with id: {id}");
            var existingProduct = _productService.GetProductById(id);
            if (existingProduct == null)
            {
                _loggingService.Log($"Product with id {id} not found");
                return NotFound();
            }
            _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}
