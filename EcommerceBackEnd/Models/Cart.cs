namespace EcommerceBackEnd.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public int Quantity { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
    }
}
