using EcommerceBackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registration")]
        public Response register(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EComCS").ToString());
            return dal.register(users, connection);
        }

        [HttpPost]
        [Route("login")]
        public Response login(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EComCS").ToString());
            return dal.login(users, connection);
        }

        [HttpPost]
        [Route("updateProfile")]
        public Response updateProfile(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EComCS").ToString());
            return dal.updateProfile(users, connection);
        }

        [HttpPost]
        [Route("addAddress")]
        public Response addAddress(UserAddress userAddress)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EComCS").ToString());
            return dal.AddUpdateAddress(userAddress, connection, "ADD");
        }

        [HttpPost]
        [Route("updateAddress")]
        public Response updateAddress(UserAddress userAddress)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EComCS").ToString());
            return dal.AddUpdateAddress(userAddress, connection, "UPDATE");
        }

        [HttpPost]
        [Route("addToCart")]
        public Response addToCart(Cart cart)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EComCS").ToString());
            Response response = dal.addToCart(cart, connection, "ADD");
            return response;
        }

        [HttpPost]
        [Route("removeFromCart")]
        public Response removeFromCart(Cart cart)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EComCS").ToString());
            Response response = dal.addToCart(cart, connection, "REMOVE");
            return response;
        }

        [HttpPost]
        [Route("addToWishlist")]
        public Response addToWishlist(WishList wishList)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EComCS").ToString());
            Response response = dal.addToWishList(wishList, connection, "ADD");
            return response;
        }

        [HttpPost]
        [Route("removeFromWishlist")]
        public Response removeFromWishlist(WishList wishList)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EComCS").ToString());
            Response response = dal.addToWishList(wishList, connection, "REMOVE");
            return response;
        }
    }
}
