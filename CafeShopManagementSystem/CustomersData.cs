using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace CafeShopManagementSystem
{
    internal class CustomersData
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\AIA\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");

        public int CustomerID { set; get; }
        public string TotalPric { set; get; }
        public string Amount { set; get; }
        public string Change { set; get; }
        public string Date { set; get; }
        public List<CustomersData> allCustomersData()
        {
            List<CustomersData> listData = new List<CustomersData>();
            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT * FROM customers";
                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CustomersData cData = new CustomersData();
                                cData.CustomerID = Convert.ToInt32(reader["customer_id"]);
                                cData.TotalPric = reader["total_price"].ToString();
                                cData.Amount = reader["amount"].ToString();
                                cData.Change = reader["change"].ToString();
                                cData.Date = reader["date"].ToString();
                                listData.Add(cData);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
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
