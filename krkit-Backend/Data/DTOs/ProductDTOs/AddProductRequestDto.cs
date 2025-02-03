namespace krkit_Backend.Data.DTOs.ProductDTOs
{
    public class AddProductRequestDto
    {
        public string CompanyName { get; set; } // Firma ismi
        public string Description { get; set; } // Açıklama
        public decimal Price { get; set; }    // Ürün fiyatı
        public int Quantity { get; set; }     // Adet
        public string Barcode { get; set; }   // Barkod numarası
    }
}
