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
    public partial class AdminDashboardForm : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\AIA\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");

        public AdminDashboardForm()
        {
            InitializeComponent();

            displayTotalCashier();
            displayTotalCustomer();
            displayTotalIncome();
            displayTodaysIncome();
        }

        public void displayTotalCashier()
        {
            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT COUNT(*) FROM users WHERE role = @role AND status =@status";
                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@role", "Cashier");
                        cmd.Parameters.AddWithValue("@status", "Active");

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int Count = Convert.ToInt32(reader[0]);
                            dashbord_TC.Text = Count.ToString();


                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    connect.Close();
                }
            }

        }


        public void displayTotalCustomer()
        {
            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT COUNT(id) FROM Customers";
                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int Count = Convert.ToInt32(reader[0]);
                            dashbord_TCust.Text = Count.ToString();


                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    connect.Close();
                }
            }

        }
        public void displayTodaysIncome()
        {

            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT SUM(total_price) FROM Customers";
                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {


                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int Count = Convert.ToInt32(reader[0]);
                            dashbord_TI.Text = Count.ToString("0.00") + "L.E";


                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        public void displayTotalIncome()
        {
            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT SUM(total_price) FROM Customers WHERE date = @date";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        DateTime today = DateTime.Today;
                        string getToday = today.ToString("yyyy-MM-dd");

                        cmd.Parameters.AddWithValue("@date", getToday);

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            // ✅ Check for DBNull before converting
                            int count = reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0]);
                            dashbord_TIn.Text = count.ToString("0.00") + " L.E";
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

    }
}
