using Microsoft.AspNetCore.Mvc;
using krkit_Backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace krkit_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // Ürünleri tutan örnek bir liste
        private static List<Product> Products = new List<Product>
        {
            new Product { Name = "Ürün 1", Price = 100.00m },
            new Product { Name = "Ürün 2", Price = 200.00m }
        };

        // Ürünleri listeleme (GET)
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(Products);
        }

        // Ürün ekleme (POST)
        [HttpPost("add")]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if (product == null || string.IsNullOrEmpty(product.Name) || product.Price <= 0)
            {
                return BadRequest("Geçerli bir ürün adı ve fiyatı girin.");
            }

            // Aynı Id sahip bir ürün var mı kontrol edelim
            if (Products.Any(p => p.Id == product.Id))
            {
                return BadRequest("Bu isimde bir ürün zaten mevcut.");
            }

            // Yeni ürün ekleme
            Products.Add(product);
            return CreatedAtAction(nameof(GetProductByName), new { name = product.Name }, product);
        }

        // Belirli bir ürünü adına göre getirme (GET)
        [HttpGet("select")]
        public IActionResult GetProductByName()
        {
            var product = Products.FirstOrDefault();
            if (product == null)
                return NotFound("Ürün bulunamadı.");

            return Ok(product);
        }

        // Ürün güncelleme (PUT)
        [HttpPut("update")]
        public IActionResult UpdateProduct( [FromBody] Product updatedProduct)
        {
            var product = Products.FirstOrDefault();
            if (product == null)
                return NotFound("Ürün bulunamadı.");

            // Güncellenen bilgileri atama
            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            return Ok(product);
        }

        // Ürün silme (DELETE)
        [HttpDelete("delete")]
        public IActionResult DeleteProduct([FromBody] int id)
        {
            // İlgili ID'ye sahip ürünü bul
            var product = Products.FirstOrDefault(p => p.Id ==id);
            if (product == null)
                return NotFound("Ürün bulunamadı.");

            // Ürünü listeden silme
            Products.Remove(product);
            return NoContent();
        }

    }
}
