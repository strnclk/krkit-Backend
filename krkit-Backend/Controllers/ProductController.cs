using Microsoft.AspNetCore.Mvc;
using krkit_Backend.Data.Models;
using krkit_Backend.Data.UnitOfWork;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using krkit_Backend.Data.DTOs.ProductDTOs;

namespace krkit_Backend.Controllers
{
    [Route("api/[controller]")]
     [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> GetAllProducts(GetProductByFilterRequestDto dto)
        {
            IEnumerable<Product> products;

            if (string.IsNullOrEmpty(dto.Barcode))
            {
                // Tüm ürünleri getir, ancak FKS ile başlayanları hariç tut
                products = await _unitOfWork.Products.FindAsync(p => !p.Barcode.StartsWith("FKS"));
            }
            else
            {
                // Eğer barkod filtresi varsa, FKS ile başlamayan ve filtrelenen barkoda uyanları getir
                products = await _unitOfWork.Products.FindAsync(p =>
                    p.Barcode == dto.Barcode && !p.Barcode.StartsWith("FKS"));
            }

            return Ok(products);
        }

        // Ürün ekleme (POST)
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequestDto productRequest)
        {
            if (productRequest == null || string.IsNullOrEmpty(productRequest.CompanyName) ||
                productRequest.Price <= 0 || productRequest.Quantity <= 0 || string.IsNullOrEmpty(productRequest.Barcode))
            {
                return BadRequest("Geçerli firma ismi, açıklama, fiyat, adet ve barkod numarası girin.");
            }

            // Aynı Barkod numarasına sahip bir ürün var mı kontrol edelim
            var existingProduct = await _unitOfWork.Products.FindAsync(p => p.Barcode == productRequest.Barcode);
            if (existingProduct.Any())
            {
                return BadRequest("Bu barkod numarasına sahip bir ürün zaten mevcut.");
            }

            // DTO'dan Entity'ye dönüşüm
            var newProduct = new Product
            {
                CompanyName = productRequest.CompanyName,
                Description = productRequest.Description,
                Price = productRequest.Price,
                Quantity = productRequest.Quantity,
                Barcode = productRequest.Barcode,
                IsDeleted = false // Varsayılan olarak silinmemiş
            };

            // Yeni ürün ekleme
            await _unitOfWork.Products.AddAsync(newProduct);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductByBarcode), new { barcode = newProduct.Barcode }, newProduct);
        }


        // Belirli bir ürünü barkod numarasına göre getirme (GET) CHATTEN
        [HttpGet("select")]
        public async Task<IActionResult> GetProductByBarcode(string barcode)
        {
            var product = await _unitOfWork.Products.FindAsync(p => p.Barcode == barcode);
            var productResult = product.FirstOrDefault();

            if (productResult == null)
                return NotFound("Ürün bulunamadı.");

            return Ok(productResult);
        }

        // Ürün güncelleme (PUT)
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct( UpdateProductRequestDto updateProductRequest)
        {
            // Veritabanından güncellenmek istenen ürünü alıyoruz
            var product = await _unitOfWork.Products.GetByIdAsync(updateProductRequest.Id);
            if (product == null)
                return NotFound("Ürün bulunamadı.");

            // DTO'dan gelen veriyi entity'e manuel olarak aktarıyoruz
            product.CompanyName = updateProductRequest.CompanyName;
            product.Description = updateProductRequest.Description;
            product.Price = updateProductRequest.Price;
            product.Quantity = updateProductRequest.Quantity;
            product.Barcode = updateProductRequest.Barcode;

            // Ürünü güncelliyoruz
            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return Ok(product); // Güncellenen ürünü döndürüyoruz
        }


        // Ürün silme (DELETE)
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                return NotFound("Ürün bulunamadı.");

            await _unitOfWork.Products.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
