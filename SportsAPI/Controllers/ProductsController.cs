using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPlusSport.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsAPI.Classes;
using SportsAPI.Models;

namespace SportsAPI.Controllers
{
    [ApiVersion("1.0")]
    //[Route("v{v:apiversion}/products")]  //URL API Versioning
    //Use this https://localhost:44369/v1.0/Products when we use the above URL API Versioning 
    [Route("products")]//Header API Versioning 
    //Add key as X-API-Version and value as 1.0 in the header while the url is https://localhost:44369/Products
    [ApiController]
    public class ProductsV1_0Controller : ControllerBase
    {
        private readonly ShopContext _context;
        public ProductsV1_0Controller(ShopContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
        /*        [HttpGet]
                public IEnumerable<Product> GetProducts()
                {
                    return _context.Products.ToList();
                }*/

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryParameters queryParameters)
        {
            IQueryable<Product> products = _context.Products;

            if (queryParameters.minPrice != null && queryParameters.maxPrice != null)
            {
                products = products.Where(p => p.Price >= queryParameters.minPrice && p.Price <= queryParameters.maxPrice);
            }

            if (!string.IsNullOrEmpty(queryParameters.Sku))
            {
                products = products.Where(s => s.Sku == queryParameters.Sku);
            }

            if (!string.IsNullOrEmpty(queryParameters.Name))
            {
                products = products.Where(n => n.Name.ToLower().Contains(queryParameters.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(queryParameters.sortBy))
            {
                if (typeof(Product).GetProperty(queryParameters.sortBy) != null)
                {
                    products = products.OrderByCustom(queryParameters.sortBy, queryParameters.SortOrder);
                }
            }

            products = products.Skip(queryParameters.Size * (queryParameters.Page - 1)).Take(queryParameters.Size);
            return Ok(await products.ToArrayAsync());
        }
        //[HttpGet,Route("/products/{id}")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody]Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute]int id, [FromBody]Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Products.Find(id) == null)
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }
    }

    [ApiVersion("2.0")]
    //[Route("v{v:apiversion}/products")]  //URL API Versioning
    //Use this https://localhost:44369/v2.0/Products when we use the above URL API Versioning
    [Route("products")]//Header API Versioning 
    //Add key as X-API-Version and value as 2.0 in the header while the url is https://localhost:44369/Products
    [ApiController]
    public class ProductsV2_0Controller : ControllerBase
    {
        private readonly ShopContext _context;
        public ProductsV2_0Controller(ShopContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
        /*        [HttpGet]
                public IEnumerable<Product> GetProducts()
                {
                    return _context.Products.ToList();
                }*/

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryParameters queryParameters)
        {
            IQueryable<Product> products = _context.Products.Where(p=>p.IsAvailable==true);

            if (queryParameters.minPrice != null && queryParameters.maxPrice != null)
            {
                products = products.Where(p => p.Price >= queryParameters.minPrice && p.Price <= queryParameters.maxPrice);
            }

            if (!string.IsNullOrEmpty(queryParameters.Sku))
            {
                products = products.Where(s => s.Sku == queryParameters.Sku);
            }

            if (!string.IsNullOrEmpty(queryParameters.Name))
            {
                products = products.Where(n => n.Name.ToLower().Contains(queryParameters.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(queryParameters.sortBy))
            {
                if (typeof(Product).GetProperty(queryParameters.sortBy) != null)
                {
                    products = products.OrderByCustom(queryParameters.sortBy, queryParameters.SortOrder);
                }
            }

            products = products.Skip(queryParameters.Size * (queryParameters.Page - 1)).Take(queryParameters.Size);
            return Ok(await products.ToArrayAsync());
        }
        //[HttpGet,Route("/products/{id}")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody]Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute]int id, [FromBody]Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Products.Find(id) == null)
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }
    }
}