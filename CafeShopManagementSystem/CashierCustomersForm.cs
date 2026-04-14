using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeShopManagementSystem
{
    public partial class CashierCustomersForm : UserControl
    {
        public CashierCustomersForm()
        {
            InitializeComponent();

            DisplayCustomersData();

        }
        public void DisplayCustomersData()
        {
            CustomersData cData = new CustomersData();
            List<CustomersData> listData = cData.allCustomersData();
            dataGridView1.DataSource = listData;


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
