using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LojaAPI.Dao;
using LojaAPI.Models;
using Microsoft.AspNetCore.Http;

namespace LojaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;

        public ProductsController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutProduct(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var productUpdate = await _context.Products.FindAsync(id);
            if (!ModelState.IsValid) return BadRequest();

            if (String.IsNullOrEmpty(product.Image))
            {
                productUpdate.Image = productUpdate.Image;
            }
            else
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", productUpdate.Image);

                System.IO.File.Delete(filePath);

                productUpdate.Image = product.Image;

            }

            productUpdate.CategoryId = product.CategoryId;
            productUpdate.Name = product.Name;
            productUpdate.Description = product.Description;
            productUpdate.Price = product.Price;
            productUpdate.Brand = product.Brand;
            productUpdate.Model = product.Model;
            productUpdate.Amount = product.Amount;


            _context.Products.Update(productUpdate);
            await _context.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
           
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Product>> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            string imgNm = product.Image;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgNm);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        [RequestSizeLimit(40000000)]
        [HttpPost("imageUp")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
        {
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    await stream.FlushAsync();
                }

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }
    }
}
