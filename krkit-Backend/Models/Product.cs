namespace krkit_Backend.Models
{
    public class Product
    {
        public int Id { get; set; }          // Ürün ID'si
        public string CompanyName { get; set; } // Firma ismi
        public string Description { get; set; } // Açıklama
        public decimal Price { get; set; }    // Ürün fiyatı
        public int Quantity { get; set; }     // Adet
        public string Barcode { get; set; }   // Barkod numarası
    }
}