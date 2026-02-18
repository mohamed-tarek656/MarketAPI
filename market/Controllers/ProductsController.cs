using Humanizer;
using market.dtos;
using market.Models;
using market.Services.internalinterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace market.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        // private readonly marketdBContext _context
        private readonly iproductservice<Product> _service;

        //public ProductsController(marketdBContext context)
        //{
        //    _context = context;
        //}
        public ProductsController(iproductservice<Product> service)
        {
           _service = service;
            
        }

        // GET: api/Products
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _service.getallasync());
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _service.getbyid(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok( product);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            //if (id != product.Id)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(product).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ProductExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
            var updated = await _service.updateasync(id, product);
            if (!updated)
                return BadRequest();

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<dtopostproduct>> PostProduct(dtopostproduct productpost)
        {
            //if (!ModelState.IsValid) return BadRequest(productpost);
            //var newproduct = new Product()
            //{
            //    Name = productpost.Name,
            //    Price = productpost.Price,
            //    Quantity = productpost.Quantity
            //};
            //_context.Products.Add(newproduct);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetProduct", new { id = productpost.Id }, productpost);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = new Product
            {
                Name = productpost.Name,
                Price = productpost.Price,
                Quantity = productpost.Quantity
            };

            await _service.createasync(product);

            return CreatedAtAction(("getproduct"),
                new { id = product.Id },
                productpost);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            //var product = await _context.Products.FindAsync(id);
            //if (product == null)
            //{
            //    return NotFound();
            //}

            //_context.Products.Remove(product);
            //await _context.SaveChangesAsync();

            //return NoContent();
            var deleted = await _service.deleteasync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        private async Task<bool> ProductExists(int id)
        {
            var product = await _service.getbyid(id);
            return product != null;
        }

    }
}
