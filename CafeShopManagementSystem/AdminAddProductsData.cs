using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace CafeShopManagementSystem
{
    internal class AdminAddProductsData
    {

        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\AIA\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");
         
       public int ID { set; get; }
        public string ProductID { set; get; }
        public string ProductName { set; get; }
        public string Type { set; get; }
        public string Stock { set; get; }
        public string Price { set; get; }
        public string Status { set; get; }
        public string Image { set; get; }
        public string DateInsert { set; get; }
        public string DateUpdate { set; get; }

        public List<AdminAddProductsData> productsListData()
        {

           List<AdminAddProductsData> listData = new List<AdminAddProductsData>();

            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT * FROM products WHERE  date_delete IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            AdminAddProductsData apd = new AdminAddProductsData();
                            apd.ID = (int)reader["id"];
                            apd.ProductID =reader["prod_id"].ToString();
                            apd.ProductName = reader["prod_name"].ToString();
                            apd.Type = reader["prod_type"].ToString();
                            apd.Stock = reader["prod_stock"].ToString();
                            apd.Price = reader["prod_price"].ToString();
                            apd.Status = reader["prod_status"].ToString();
                            apd.Image = reader["prod_image"].ToString();
                            apd.DateInsert = reader["date_insert"].ToString();
                            apd.DateUpdate = reader["date_update"].ToString();
                        
                            listData.Add(apd);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failds :" + ex);
                }
                finally
                {
                    connect.Close();
                }
            }
            return listData;
        }



        public List<AdminAddProductsData> availablProductsData()
        {

            List<AdminAddProductsData> listData = new List<AdminAddProductsData>();

            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT * FROM products WHERE status =@stats ";
  
                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@stats", "Available");
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            AdminAddProductsData apd = new AdminAddProductsData();
                            apd.ID = (int)reader["id"];
                            apd.ProductID = reader["prod_id"].ToString();
                            apd.ProductName = reader["prod_name"].ToString();
                            apd.Type = reader["prod_type"].ToString();
                            apd.Stock = reader["prod_stock"].ToString();
                            apd.Price = reader["prod_price"].ToString();

                            listData.Add(apd);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failds :" + ex);
                }
                finally
                {
                    connect.Close();
                }
            }
            return listData;
        }

    }
}
