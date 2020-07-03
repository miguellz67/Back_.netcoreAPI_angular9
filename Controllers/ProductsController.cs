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

            var produtoAtualizacao = await _context.Products.FindAsync(id);
            if (!ModelState.IsValid) return BadRequest();

            produtoAtualizacao.CategoryId = product.CategoryId;
            produtoAtualizacao.Name = product.Name;
            produtoAtualizacao.Description = product.Description;
            produtoAtualizacao.Price = product.Price;
            produtoAtualizacao.Brand = product.Brand;
            produtoAtualizacao.Model = product.Model;
            
            _context.Products.Update(produtoAtualizacao);
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
        public async Task<IActionResult> UploadImage(IFormFile arquivo, string imgNome)
        {
            if (arquivo.Length > 0)
            {
                try
                {
                    imgNome = Guid.NewGuid() + "_";
                    imgNome = Path.Combine(imgNome, arquivo.FileName);

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgNome);

                    if (System.IO.File.Exists(filePath))
                    {
                        return BadRequest();
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await arquivo.CopyToAsync(stream);
                        await stream.FlushAsync();
                        
                    }
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }

            }
            return Ok();
        }
    }
}
