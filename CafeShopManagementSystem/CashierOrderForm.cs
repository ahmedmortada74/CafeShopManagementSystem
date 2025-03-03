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
            IDGenerator();
            if (cashierOrderForm_type.SelectedIndex == -1 || cashierOrderForm_productID.SelectedIndex == -1
               || cashierOrderForm_prodName.Text == "" || cashierOrderForm_quantity.Value == 0
               || cashierOrderForm_price.Text == "")
            {
                MessageBox.Show("Please Select the Product first ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

            }
            if (connect.State == ConnectionState.Closed)
            {
           
                try
                {
                    connect.Open();
                    float getPrice = 0;


                    string selectOrder = "SELECT * FROM products WHERE prod_id =@prodID";

                    using(SqlCommand getOrder =new SqlCommand(selectOrder, connect))
                    {
                        getOrder.Parameters.AddWithValue("@prodID", cashierOrderForm_productID.Text.Trim());

                        using (SqlDataReader reader = getOrder.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                object rawValue = reader["prod_price"];
                                if(rawValue != DBNull.Value)
                                {
                                    getPrice = Convert.ToSingle(rawValue);
                                }
                            }
                        }
                        

                    }

                    string insertOrder = "INSERT INTO orders (customer_id, prod_id,prod_name, prod_type, qty, prod_price, order_date)" +
                        "VALUES(@customerID, @prodID,@prodName, @prodType, @qty, @prodPrice, @orderDate )";
                    DateTime today = DateTime.Today;

                    using (SqlCommand cmd = new SqlCommand(insertOrder,connect))
                    {
                        cmd.Parameters.AddWithValue("@customerID", idGen);
                        cmd.Parameters.AddWithValue("@prodID", cashierOrderForm_productID.Text.Trim());
                        cmd.Parameters.AddWithValue("@prodName", cashierOrderForm_prodName.Text);
                        cmd.Parameters.AddWithValue("@prodType", cashierOrderForm_type.Text.Trim());
                        float totalPrice =( getPrice * (int)cashierOrderForm_quantity.Value);
                        cmd.Parameters.AddWithValue("@qty", cashierOrderForm_quantity.Value);
                        cmd.Parameters.AddWithValue("@prodPrice",totalPrice);
                        cmd.Parameters.AddWithValue("@orderDate", today);

                        cmd.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed:" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        private int idGen = 0;
        public void IDGenerator()
        {
            using (SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\AIA\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                connect.Open();
                string selectID = "SELECT MAX(customer_id) FROM customers";
                using (SqlCommand cmd = new SqlCommand(selectID, connect))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {

                        int temp = Convert.ToInt32(result);
                        if (temp == 0)
                        {
                            idGen = 1;
                        }
                        else
                        {
                            idGen = temp + 1;
                        }

                    }
                    else
                    {
                        idGen = 1;
                    }
                }
            }
           
        }

        private void cashierOrderForm_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            cashierOrderForm_productID.SelectedIndex = -1;
            cashierOrderForm_productID.Items.Clear();
            cashierOrderForm_prodName.Text = "";
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
                try
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
                                    string price = reader["prod_price"].ToString();
                                    cashierOrderForm_prodName.Text = prodName;
                                    cashierOrderForm_price.Text = price;
                                }
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error" + ex, "Error Message ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
           
            }
        }
    }
}
