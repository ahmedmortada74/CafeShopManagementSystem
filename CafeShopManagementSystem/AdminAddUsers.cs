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
    public partial class AdminAddUsers : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\AIA\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");

        public AdminAddUsers()
        {
            InitializeComponent();
            displayAddUsersData();
        }

        public void displayAddUsersData()
        {
            AdminAddUaersData uaersData = new AdminAddUaersData();
            List<AdminAddUaersData> listData = uaersData.usersListData();
            dataGridView1.DataSource = listData;
        }
        private void AdminAddUsers_Load(object sender, EventArgs e)
        {

        }

        public bool emptFields()
        {
            if(adminAddUsers_username.Text =="" || adminAddUsers_password.Text ==""
                ||adminAddUsers_role.Text==""||adminAddUsers_status.Text =="")
            {
                return true;
            }
            else
            {
                return false;   
            }
        }
        private void adminAddUsers_addBtn_Click(object sender, EventArgs e)
        {
            if (emptFields())
            {
                MessageBox.Show("All fields are required to bo ");
            }
        }
    }
}
