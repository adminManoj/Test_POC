using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EcommerceBackEnd.Models
{
    public class DAL
    {
        public Response register(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", users.Username);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Type", "Users");
            cmd.Parameters.AddWithValue("@ActionType", "ADD");
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User registered successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User registration failed";
            }
            return response;
        }
        public Response login(Users users, SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("sp_login", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
            da.SelectCommand.Parameters.AddWithValue("@Password", users.Password);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.Username = Convert.ToString(dt.Rows[0]["Username"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                response.StatusCode = 200;
                response.StatusMessage = "User is valid";
                response.User = user;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User is invalid";
                response.User = null;
            }
            return response;
        }
        public Response updateProfile(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", users.ID);
            cmd.Parameters.AddWithValue("@Username", users.Username);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@ActionType", "UPDATE");
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Record updated successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Some error occured. Try after sometime";
            }

            return response;
        }
        public Response AddUpdateAddress(UserAddress users, SqlConnection connection, string type)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_address", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", users.ID);
            cmd.Parameters.AddWithValue("@Address", users.Address);
            cmd.Parameters.AddWithValue("@ActionType", type);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                if (type == "ADD")
                    response.StatusMessage = "Address added successfully";
                else
                    response.StatusMessage = "Record updated successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Some error occured. Try after sometime";
            }

            return response;
        }
        public Response addToCart(Cart cart, SqlConnection connection, string type)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddToCart", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", cart.ID);
            cmd.Parameters.AddWithValue("@UserID", cart.UserID);
            cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
            cmd.Parameters.AddWithValue("@ProductID", cart.ProductID);
            cmd.Parameters.AddWithValue("@ActionType", type);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                if (type == "ADD")
                    response.StatusMessage = "Item added successfully";
                else
                    response.StatusMessage = "Item removed successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Item could not be added";
            }
            return response;
        }
        public Response cartList(Cart cart, SqlConnection connection)
        {
            Response response = new Response();
            List<Cart> listCart = new List<Cart>();
            SqlDataAdapter da = new SqlDataAdapter("sp_CartList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@UserId", cart.UserID);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Cart obj = new Cart();
                    obj.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    obj.Name = Convert.ToString(dt.Rows[i]["Name"]);
                    obj.ProductID = Convert.ToInt32(dt.Rows[i]["ProductID"]);
                    obj.UserID = Convert.ToInt32(dt.Rows[i]["UserID"]);
                    obj.Image = Convert.ToString(dt.Rows[i]["Image"]);
                    obj.Price = Convert.ToDecimal(dt.Rows[i]["Price"]);
                    obj.Quantity = Convert.ToInt32(dt.Rows[i]["Quantity"]);
                    listCart.Add(obj);
                }
                if (listCart.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Cart details fetched";
                    response.listCart = listCart;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Cart details are not available";
                    response.listCart = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Cart details are not available";
                response.listCart = null;
            }
            return response;
        }
        public Response addToWishList(WishList list, SqlConnection connection, string type)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddToWishList", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", list.ID);
            cmd.Parameters.AddWithValue("@UserID", list.UserID);
            cmd.Parameters.AddWithValue("@ProductID", list.ProductID);
            cmd.Parameters.AddWithValue("@ActionType", type);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                if (type == "ADD")
                    response.StatusMessage = "Item added to wishlst successfully";
                else
                    response.StatusMessage = "Item removed from wishlst successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Item could not be added";
            }
            return response;
        }
        public Response wishList(WishList list, SqlConnection connection)
        {
            Response response = new Response();
            List<WishList> listWishList = new List<WishList>();
            SqlDataAdapter da = new SqlDataAdapter("sp_WishList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@UserId", list.UserID);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    WishList objWishList = new WishList();
                    objWishList.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    objWishList.Name = Convert.ToString(dt.Rows[i]["Name"]);
                    objWishList.ProductID = Convert.ToInt32(dt.Rows[i]["ProductID"]);
                    objWishList.UserID = Convert.ToInt32(dt.Rows[i]["UserID"]);
                    objWishList.Image = Convert.ToString(dt.Rows[i]["Image"]);
                    objWishList.Price = Convert.ToDecimal(dt.Rows[i]["Price"]);
                    listWishList.Add(objWishList);
                }
                if (listWishList.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "WishList details fetched";
                    response.listWishList = listWishList;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "WishList details are not available";
                    response.listWishList = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "WishList details are not available";
                response.listCart = null;
            }
            return response;
        }
        public Response addUpdateProduct(Product product, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_addUpdateProduct", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", product.ID);
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Image", product.Image);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
            cmd.Parameters.AddWithValue("@Type", "ADD");
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Product inserted successfully";
            }
            return response;
        }
        public Response listProduct(SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_GetProducts", connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            List<Product> listProduct = new List<Product>();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Product objProduct = new Product();
                    objProduct.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    objProduct.Name = Convert.ToString(dt.Rows[i]["Name"]);
                    objProduct.Image = Convert.ToString(dt.Rows[i]["Image"]);
                    objProduct.Price = Convert.ToDecimal(dt.Rows[i]["Price"]);
                    objProduct.Quantity = Convert.ToInt32(dt.Rows[i]["Quantity"]);
                    listProduct.Add(objProduct);
                }
                if (listProduct.Count > 0)
                {
                    response.StatusCode = 200;
                    response.listProduct = listProduct;
                }
                else
                {
                    response.StatusCode = 100;
                    response.listProduct = null;
                }

            }

            return response;
        }
    }
}
