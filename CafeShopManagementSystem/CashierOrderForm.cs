using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace CafeShopManagementSystem
{
    public partial class CashierOrderForm : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\AIA\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");

        public CashierOrderForm()
        {
            InitializeComponent();
            displayAvailableProds();
        }

        public void displayAvailableProds()
        {
            CashierOrderFormProdData allProds = new CashierOrderFormProdData();

            List<CashierOrderFormProdData> listData = allProds.availablProductsData();
            cashierOrderForm_menuTable.DataSource = listData;
        }

        private void cashierOrderForm_addBtn_Click(object sender, EventArgs e)
        {

        }


        private void cashierOrderForm_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            cashierOrderForm_productID.SelectedIndex = -1;
            cashierOrderForm_productID.Items.Clear();
            cashierOrderForm_productName.Text = "";
            cashierOrderForm_price.Text = "";

            string selectValue = cashierOrderForm_type.SelectedItem as string;


            if (selectValue != null)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\AIA\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30"))
                    {
                        connect.Open();
                        string selectData = $"SELECT * FROM products WHERE prod_type ='{selectValue}'AND prod_status =@status AND date_delete IS NULL";
                        using (SqlCommand cmd = new SqlCommand(selectData, connect))
                        {
                            cmd.Parameters.AddWithValue("@status", "Available");
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string value = reader["prod_id"].ToString();
                                    cashierOrderForm_productID.Items.Add(value);
                                }
                            }
                        }
                    }
                }

                catch (Exception exx)
                {
                    MessageBox.Show("Error" + exx, "Error Message ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }






        }

        private void cashierOrderForm_productID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectValue = cashierOrderForm_productID.SelectedItem as string;

            if (selectValue != null)
            {
                using (SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\AIA\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    connect.Open();
                    string selectData = $"SELECT * FROM products WHERE prod_id ='{selectValue}'AND prod_status =@status AND date_delete IS NULL";
                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@status", "Available");
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string prodName = reader["prod_name"].ToString();
                                string price = reader["prod_price"].ToString() ;
                                cashierOrderForm_productName.Text = prodName;
                                cashierOrderForm_price.Text = price;
                            }
                        }
                    }
                }
            }
        }
    }
}
