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
            new Product { CompanyName = "Firma 1", Description = "Açıklama 1", Price = 100.00m, Quantity = 10, Barcode = "123456" },
            new Product { CompanyName = "Firma 2", Description = "Açıklama 2", Price = 200.00m, Quantity = 20, Barcode = "654321" }
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
            if (product == null || string.IsNullOrEmpty(product.CompanyName) || product.Price <= 0 || product.Quantity <= 0 || string.IsNullOrEmpty(product.Barcode))
            {
                return BadRequest("Geçerli firma ismi, açıklama, fiyat, adet ve barkod numarası girin.");
            }

            // Aynı Barkod numarasına sahip bir ürün var mı kontrol edelim
            if (Products.Any(p => p.Barcode == product.Barcode))
            {
                return BadRequest("Bu barkod numarasına sahip bir ürün zaten mevcut.");
            }

            // Yeni ürün ekleme
            Products.Add(product);
            return CreatedAtAction(nameof(GetProductByBarcode), new { barcode = product.Barcode }, product);
        }

        // Belirli bir ürünü barkod numarasına göre getirme (GET)
        [HttpGet("select")]
        public IActionResult GetProductByBarcode(string barcode)
        {
            var product = Products.FirstOrDefault(p => p.Barcode == barcode);
            if (product == null)
                return NotFound("Ürün bulunamadı.");

            return Ok(product);
        }

        // Ürün güncelleme (PUT)
        [HttpPut("update")]
        public IActionResult UpdateProduct([FromBody] Product updatedProduct)
        {
            var product = Products.FirstOrDefault(p => p.Barcode == updatedProduct.Barcode);
            if (product == null)
                return NotFound("Ürün bulunamadı.");

            // Güncellenen bilgileri atama
            product.CompanyName = updatedProduct.CompanyName;
            product.Description = updatedProduct.Description;
            product.Price = updatedProduct.Price;
            product.Quantity = updatedProduct.Quantity;
            return Ok(product);
        }

        // Ürün silme (DELETE)
        // Ürün silme (DELETE)
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            // İlgili ID'ye sahip ürünü bul
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound("Ürün bulunamadı.");

            // Ürünü listeden silme
            Products.Remove(product);
            return NoContent();
        }

    }
}