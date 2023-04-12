using Microsoft.AspNetCore.Http;

namespace EcommerceBackEnd.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
