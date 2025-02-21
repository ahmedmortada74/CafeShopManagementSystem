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
    public partial class AdminAddProducts : UserControl
    {
           SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\AIA\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");
        public AdminAddProducts()
        {
            InitializeComponent();
        }

        public bool emptyFields()
        {
            if(adminAddProducts_id.Text == "" || adminAddProducts_name.Text == "" 
                || adminAddProducts_type.SelectedIndex == -1 || adminAddProducts_stock.Text ==""
                || adminAddProducts_price.Text =="" || adminAddProducts_status.SelectedIndex == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void adminAddProducts_addBtn_Click(object sender, EventArgs e)
        {
           if(emptyFields())
            {
                MessageBox.Show("All fields are required to bo filld", "Erroe Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if(connect.State == ConnectionState.Closed)
                {
                    try
                    {
                        connect.Open();
                       
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        connect.Close();
                    }
                }
             
            }
        }
    }
}
