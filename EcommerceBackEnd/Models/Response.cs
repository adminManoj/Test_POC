using System.Collections.Generic;

namespace EcommerceBackEnd.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public Users User { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        public WishList WishList { get; set; }
        public UserAddress UserAddress { get; set; }
        public List<Product> listProducts { get; set; }
        public List<WishList> listWishList { get; set; }
        public List<Cart> listCart { get; set; }
        public List<Product> listProduct { get; set; }

    }
}
