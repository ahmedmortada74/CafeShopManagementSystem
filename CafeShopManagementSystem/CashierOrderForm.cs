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
    }
}
