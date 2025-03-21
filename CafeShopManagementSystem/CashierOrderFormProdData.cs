﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CafeShopManagementSystem
{
    internal class CashierOrderFormProdData
    {

        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\AIA\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");

        public int ID { set; get; }
        public string ProductID { set; get; }
        public string ProductName { set; get; }
        public string Type { set; get; }
        public string Stock { set; get; }
        public string Price { set; get; }
        public string Status { set; get; }



        public List<CashierOrderFormProdData> availablProductsData()
        {

            List<CashierOrderFormProdData> listData = new List<CashierOrderFormProdData>();

            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT * FROM products WHERE prod_status =@stats  AND date_delete IS NULL";


                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@stats", "Available");
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CashierOrderFormProdData apd = new CashierOrderFormProdData();
                            apd.ID = (int)reader["id"];
                            apd.ProductID = reader["prod_id"].ToString();
                            apd.ProductName = reader["prod_name"].ToString();
                            apd.Type = reader["prod_type"].ToString();
                            apd.Stock = reader["prod_stock"].ToString();
                            apd.Price = reader["prod_price"].ToString();
                            apd.Status = reader["prod_status"].ToString();
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
