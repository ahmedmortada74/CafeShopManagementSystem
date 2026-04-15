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
    public partial class CashierMainForm : Form
    {
        public CashierMainForm()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you Sure want to Exit ", "Confirmation Message",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure want sign out?","Confirmation Message",MessageBoxButtons.YesNo,MessageBoxIcon.Question); 
            if (check == DialogResult.Yes)
            {
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Hide();
            }
        }

        private void cashierOrderForm_amount_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dashboard_btn_Click(object sender, EventArgs e)
        {
            adminDashboardForm1.Visible = true;
            adminAddProducts1.Visible = false;
            cashierOrderForm1.Visible = false;
            cashierCustomersForm1.Visible = false;

            AdminDashboardForm dashboard = adminDashboardForm1 as AdminDashboardForm;
            if (dashboard != null)
            {
                dashboard.refreshData();
            }
        }


        private void addProducts_btn_Click(object sender, EventArgs e)
        {
            adminAddProducts1.Visible = true;
            adminDashboardForm1.Visible = false;
            cashierOrderForm1.Visible = false;
            cashierCustomersForm1.Visible = false;

            AdminAddProducts addProducts = adminAddProducts1 as AdminAddProducts;
            if (addProducts != null)
            {
                addProducts.refreshData();
            }
        }
        private void order_btn_Click(object sender, EventArgs e)
        {
            adminDashboardForm1.Visible = false;
            adminAddProducts1.Visible = false;
            cashierOrderForm1.Visible = true;
            cashierCustomersForm1.Visible = false;

            CashierOrderForm orderForm = cashierOrderForm1 as CashierOrderForm;
            if (orderForm != null)
            {
                orderForm.refreshData();
            }
        }
        private void customers_btn_Click(object sender, EventArgs e)
        {
            cashierCustomersForm1.Visible = true;
            adminDashboardForm1.Visible = false;
            adminAddProducts1.Visible = false;
            cashierOrderForm1.Visible = false;

            CashierCustomersForm customersForm = cashierCustomersForm1 as CashierCustomersForm;
            if (customersForm != null)
            {
                customersForm.refreshData();
            }
        }

    }
}
