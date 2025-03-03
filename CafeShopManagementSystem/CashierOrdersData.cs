using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CafeShopManagementSystem
{
    internal class CashierOrdersData
    {

        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\AIA\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");

        public int CID { set; get; }
        public string ProdID { set; get; }
        public string ProdName { set; get; }
        public string ProdType { set; get; }
        public int Qty { set; get; }
        public string Price { set; get; }

        public List<CashierOrdersData> ordersListData()
        {
            List<CashierOrdersData> listData = new List<CashierOrdersData>();

            if(connect.State== ConnectionState.Closed)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT * FROM orders";

                }catch(Exception ex)
                {

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
