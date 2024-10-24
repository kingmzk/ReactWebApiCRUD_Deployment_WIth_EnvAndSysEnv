//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SampleCRUD.Models;

//namespace SampleCRUD.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProductsController : ControllerBase
//    {

//            private readonly ProductDbContext _context;

//            public ProductsController(ProductDbContext context)
//            {
//                _context = context;
//            }

//            // GET: api/products
//            [HttpGet]
//            public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
//            {
//                return await _context.Products.ToListAsync();
//            }

//            // GET: api/products/{id}
//            [HttpGet("{id}")]
//            public async Task<ActionResult<Product>> GetProduct(int id)
//            {
//                var product = await _context.Products.FindAsync(id);

//                if (product == null)
//                {
//                    return NotFound();
//                }

//                return product;
//            }

//            // POST: api/products
//            [HttpPost]
//            public async Task<ActionResult<Product>> PostProduct(Product product)
//            {
//                _context.Products.Add(product);
//                await _context.SaveChangesAsync();

//                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
//            }

//        // PUT: api/products/{id}
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateProduct(int id, Product updatedProduct)
//        {
//            // Check if the product exists
//            var product = await _context.Products.FindAsync(id);
//            if (product == null)
//            {
//                return NotFound(); // Send 404 if the product does not exist
//            }

//            // Update the product's properties
//            product.Name = updatedProduct.Name;
//            product.Price = updatedProduct.Price;
//            product.Stock = updatedProduct.Stock;

//            try
//            {
//                await _context.SaveChangesAsync(); // Save the changes to the database
//                return Ok(product); // Return the updated product
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ProductExists(id))
//                {
//                    return NotFound(); // Send 404 if the product was deleted before updating
//                }
//                else
//                {
//                    throw;
//                }
//            }
//        }

//        // Helper method to check if the product exists
//        private bool ProductExists(int id)
//        {
//            return _context.Products.Any(e => e.Id == id);
//        }


//        // DELETE: api/products/{id}
//        [HttpDelete("{id}")]
//            public async Task<IActionResult> DeleteProduct(int id)
//            {
//                var product = await _context.Products.FindAsync(id);
//                if (product == null)
//                {
//                    return NotFound();
//                }

//                _context.Products.Remove(product);
//                await _context.SaveChangesAsync();

//                return NoContent();
//            }
//        }
//    }



//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SampleCRUD.Models;
//using System;

//namespace SampleCRUD.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProductsController : ControllerBase
//    {
//        private readonly ProductDbContext _context;

//        public ProductsController(ProductDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/products
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
//        {
//            try
//            {
//                return await _context.Products.ToListAsync();
//            }
//            catch (Exception ex)
//            {
//                // Log the exception (optional)
//                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data from the database: {ex.Message}");
//            }
//        }

//        // GET: api/products/{id}
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Product>> GetProduct(int id)
//        {
//            try
//            {
//                var product = await _context.Products.FindAsync(id);

//                if (product == null)
//                {
//                    return NotFound();
//                }

//                return product;
//            }
//            catch (Exception ex)
//            {
//                // Log the exception (optional)
//                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving product with ID {id}: {ex.Message}");
//            }
//        }

//        // POST: api/products
//        [HttpPost]
//        public async Task<ActionResult<Product>> PostProduct(Product product)
//        {
//            try
//            {
//                _context.Products.Add(product);
//                await _context.SaveChangesAsync();

//                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
//            }
//            catch (Exception ex)
//            {
//                // Log the exception (optional)
//                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating new product: {ex.Message}");
//            }
//        }

//        // PUT: api/products/{id}
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateProduct(int id, Product updatedProduct)
//        {
//            try
//            {
//                // Check if the product exists
//                var product = await _context.Products.FindAsync(id);
//                if (product == null)
//                {
//                    return NotFound();
//                }

//                // Update the product's properties
//                product.Name = updatedProduct.Name;
//                product.Price = updatedProduct.Price;
//                product.Stock = updatedProduct.Stock;

//                await _context.SaveChangesAsync(); // Save the changes to the database
//                return Ok(product); // Return the updated product
//            }
//            catch (DbUpdateConcurrencyException ex)
//            {
//                if (!ProductExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    // Log the exception (optional)
//                    return StatusCode(StatusCodes.Status500InternalServerError, $"Concurrency error updating product with ID {id}: {ex.Message}");
//                }
//            }
//            catch (Exception ex)
//            {
//                // Log the exception (optional)
//                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating product with ID {id}: {ex.Message}");
//            }
//        }

//        // DELETE: api/products/{id}
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteProduct(int id)
//        {
//            try
//            {
//                var product = await _context.Products.FindAsync(id);
//                if (product == null)
//                {
//                    return NotFound();
//                }

//                _context.Products.Remove(product);
//                await _context.SaveChangesAsync();

//                return NoContent();
//            }
//            catch (Exception ex)
//            {
//                // Log the exception (optional)
//                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting product with ID {id}: {ex.Message}");
//            }
//        }

//        // Helper method to check if the product exists
//        private bool ProductExists(int id)
//        {
//            return _context.Products.Any(e => e.Id == id);
//        }
//    }
//}












using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SampleCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductDbContext _context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ProductDbContext context, ILogger<ProductsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                return Ok(products); // Return 200 OK with data
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving products from the database.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error. Please try again later.");
            }
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    _logger.LogWarning($"Product with ID {id} not found.");
                    return NotFound($"Product with ID {id} not found.");
                }

                return Ok(product); // Return 200 OK with product
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving product with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error. Please try again later.");
            }
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); // Return 400 Bad Request with validation errors
                }

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product); // Return 201 Created with product details
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new product.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error. Please try again later.");
            }
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            try
            {

                var existingProduct = await _context.Products.FindAsync(id);
                if (existingProduct == null)
                {
                    return NotFound($"Product with ID {id} not found.");
                }

                // Update properties
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.Stock = updatedProduct.Stock;

                _context.Products.Update(existingProduct);
                await _context.SaveChangesAsync();

                return Ok(existingProduct); // Return 200 OK with updated product
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProductExists(id))
                {
                    return NotFound($"Product with ID {id} not found.");
                }

                _logger.LogError(ex, $"Concurrency error while updating product with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Concurrency error. Please try again later.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating product with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error. Please try again later.");
            }
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound($"Product with ID {id} not found.");
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return NoContent(); // Return 204 No Content after successful deletion
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting product with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error. Please try again later.");
            }
        }

        // Helper method to check if the product exists
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
