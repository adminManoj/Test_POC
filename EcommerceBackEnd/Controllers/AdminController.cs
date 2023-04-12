using EcommerceBackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("addUpdateProduct")]
        public Response addUpdateProduct([FromForm] Product product)
        {
            string path = Path.Combine(@"D:\Youtube Channel\Student Projects\Jyoti Testu\", product.FormFile.FileName);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                product.FormFile.CopyTo(stream);
                product.Image = product.FormFile.FileName;
            }
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EComCS").ToString());
            Response response = dal.addUpdateProduct(product, connection);
            return response;
        }

        [HttpGet]
        [Route("listProduct")]
        public Response listProduct()
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EComCS").ToString());
            Response response = dal.listProduct(connection);
            return response;
        }

    }
}
