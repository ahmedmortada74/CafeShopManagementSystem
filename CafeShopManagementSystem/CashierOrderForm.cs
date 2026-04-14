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
using System.Globalization;

using System.Drawing.Printing;

namespace CafeShopManagementSystem
{
    public partial class CashierOrderForm : UserControl
    {
        public static int getCustID;

        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\AIA\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");

        public CashierOrderForm()
        {
            InitializeComponent();
            IDGenerator();
            displayAvailableProds();
            displayAllOrders();
            displayTotalPrice();
        }

        public void displayAvailableProds()
        {
            CashierOrderFormProdData allProds = new CashierOrderFormProdData();

            List<CashierOrderFormProdData> listData = allProds.availablProductsData();
            cashierOrderForm_menuTable.DataSource = listData;
        }

        public void displayAllOrders()
        {
            CashierOrdersData allOrders = new CashierOrdersData();
            List<CashierOrdersData> listData = allOrders.ordersListData();
            cashierOrderForm_orderTable.DataSource = listData;
        }


        private int totalPrice;

        public void displayTotalPrice()
        {
            IDGenerator();
            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT SUM(prod_price) FROM orders WHERE customer_id = @custID";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@custID", idGen);

                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            totalPrice = (int)Convert.ToSingle(result);
                            //totalPrice = (result != DBNull.Value && result != null) ? Convert.ToInt32(result) : 0;
                            cashierOrderForm_orderPrice.Text = totalPrice.ToString("0.00");

                        }
                        else
                        {
                            //totalPrice = 0;
                            //cashierOrderForm_orderPrice.Text = "0.00";

                        }
                        }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection failed: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
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

                    using (SqlCommand getOrder = new SqlCommand(selectOrder, connect))
                    {

                        getOrder.Parameters.AddWithValue("@prodID", cashierOrderForm_productID.Text.Trim());

                        using (SqlDataReader reader = getOrder.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                object rawValue = reader["prod_price"];

                                if (rawValue != DBNull.Value)
                                {
                                    getPrice = Convert.ToSingle(rawValue);
                                }
                            }
                        }


                    }
                    string insertOrder = "INSERT INTO orders (customer_id, prod_id, prod_name, prod_type, Qty, prod_price, order_date)" +
                                         "VALUES(@customerID, @prodID, @prodName, @prodType, @qty, @prodPrice, @orderDate)";

                    DateTime today = DateTime.Today;

                    using (SqlCommand cmd = new SqlCommand(insertOrder, connect))
                    {
                        cmd.Parameters.AddWithValue("@customerID", idGen);
                        cmd.Parameters.AddWithValue("@prodID", cashierOrderForm_productID.Text.Trim());
                        cmd.Parameters.AddWithValue("@prodName", cashierOrderForm_prodName.Text);
                        cmd.Parameters.AddWithValue("@prodType", cashierOrderForm_type.Text.Trim());
                        float totalPrice = (getPrice * (int)cashierOrderForm_quantity.Value);
                        cmd.Parameters.AddWithValue("@qty", cashierOrderForm_quantity.Value);
                        cmd.Parameters.AddWithValue("@prodPrice", totalPrice);
                        cmd.Parameters.AddWithValue("@orderDate", today);

                        cmd.ExecuteNonQuery();
                        displayAllOrders();
                        displayTotalPrice();
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
                    getCustID = idGen;
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
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex, "Error Message ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }




        private void cashierOrderForm_amount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (!decimal.TryParse(cashierOrderForm_amount.Text,  NumberStyles.Number, CultureInfo.InvariantCulture,  out decimal amount))
                {
                    MessageBox.Show("الرجاء إدخال قيمة صحيحة.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cashierOrderForm_amount.Clear();
                    return;
                }

                decimal total = (decimal)totalPrice;

                decimal change = amount - total;

                if (change < 0)
                {
                    MessageBox.Show("المبلغ المدفوع أقل من الإجمالي.", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cashierOrderForm_change.Text = "";
                }
                else
                {
                    cashierOrderForm_change.Text = change.ToString("0.00");
                }
            }
        }

        private void cashierOrderForm_payBtn_Click(object sender, EventArgs e)
        {
            if (cashierOrderForm_amount.Text == "" || cashierOrderForm_orderTable.Rows.Count < 0)
            {
                MessageBox.Show("Something went wrong.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                if (MessageBox.Show("Are You Sure for paying?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    if (connect.State == ConnectionState.Closed)
                    {
                        try
                        {
                            connect.Open();
                            IDGenerator();
                            displayTotalPrice();

                            string insertData = "INSERT INTO customers (customer_id ,total_price, amount, change, date)" +
                                "VALUES (@custID, @totalPrice, @amount, @change, @date)";
                            DateTime today = DateTime.Today;

                            using (SqlCommand cmd = new SqlCommand(insertData, connect))
                            {
                                cmd.Parameters.AddWithValue("@custID", idGen);
                                cmd.Parameters.AddWithValue("@totalPrice", totalPrice);
                                cmd.Parameters.AddWithValue("@amount", cashierOrderForm_amount.Text);
                                cmd.Parameters.AddWithValue("@change", cashierOrderForm_change.Text);
                                cmd.Parameters.AddWithValue("@date", today);

                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Paid Successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Connection Failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        finally
                        {
                            connect.Close();
                        }
                    }
                }
            }
        }

        private int rowIndex = 0;

      
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            rowIndex = 0;
        }




        private void cashierOrderForm_receiptBtn_Click(object sender, EventArgs e)
        {
            printDocument1.PrintPage -= printDocument1_PrintPage;
            printDocument1.BeginPrint -= printDocument1_BeginPrint;
            printDocument1.PrintPage += printDocument1_PrintPage;
            printDocument1.BeginPrint += printDocument1_BeginPrint;

            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            float pageWidth = e.MarginBounds.Width;
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;

            Font titleFont = new Font("Arial", 16, FontStyle.Bold);
            Font subFont = new Font("Arial", 9);
            Font boldFont = new Font("Arial", 10, FontStyle.Bold);
            Font cellFont = new Font("Arial", 9);
            Font totalFont = new Font("Arial", 11, FontStyle.Bold);
            Font smallFont = new Font("Arial", 8);

            StringFormat center = new StringFormat { Alignment = StringAlignment.Center };
            StringFormat rightFmt = new StringFormat { Alignment = StringAlignment.Far };
            StringFormat leftFmt = new StringFormat { Alignment = StringAlignment.Near };

            // ── Header ────────────────────────────────────────────
            g.FillRectangle(new SolidBrush(Color.FromArgb(26, 26, 46)),
                            new RectangleF(x, y, pageWidth, 65));
            g.DrawString("Mortadella Cafe Shop ", titleFont, Brushes.White,
                         new RectangleF(x, y + 8, pageWidth, 36), center);
            g.DrawString("Receipt", subFont, new SolidBrush(Color.FromArgb(180, 180, 180)),
                         new RectangleF(x, y + 44, pageWidth, 18), center);
            y += 75;

            // ── Date ──────────────────────────────────────────────
            g.DrawString(DateTime.Now.ToString("yyyy-MM-dd   HH:mm:ss"),
               new Font("Arial", 12, FontStyle.Bold), Brushes.Black,
               new RectangleF(x, y, pageWidth, 18), center);
            y += 24;

            // ── Table Header ──────────────────────────────────────
            DrawDashedLine(g, x, y, x + pageWidth, y);
            y += 8;

            float c0 = 25;
            float c1 = 70;
            float c2 = pageWidth - 25 - 70 - 105 - 70 - 40;
            float c3 = 105;
            float c4 = 70;
            float c5 = 40;
            float[] colWidths = { c0, c1, c2, c3, c4, c5 };
            string[] headers = { "#", "Prod ID", "Product Name", "Product Type", "Quantity", "Price" };

            float cx = x;
            for (int i = 0; i < headers.Length; i++)
            {
                g.DrawString(headers[i], boldFont, Brushes.Black,
                             new RectangleF(cx, y, colWidths[i], 18),
                             i == headers.Length - 1 ? rightFmt : leftFmt);
                cx += colWidths[i];
            }
            y += 20;
            DrawDashedLine(g, x, y, x + pageWidth, y);
            y += 6;

            // ── Table Rows ────────────────────────────────────────
            int rowNum = 1;
            while (rowIndex < cashierOrderForm_orderTable.Rows.Count)
            {
                DataGridViewRow row = cashierOrderForm_orderTable.Rows[rowIndex];

                string[] cells = {
            rowNum.ToString(),
            row.Cells["ProdID"].Value?.ToString()    ?? "",
            row.Cells["ProdName"].Value?.ToString()  ?? "",
            row.Cells["ProdType"].Value?.ToString()  ?? "",
            row.Cells["Qty"].Value?.ToString()        ?? "",
            row.Cells["Price"].Value?.ToString() ?? ""
        };

                if (rowNum % 2 == 0)
                    g.FillRectangle(new SolidBrush(Color.FromArgb(245, 245, 245)),
                                    new RectangleF(x, y, pageWidth, 18));

                cx = x;
                for (int i = 0; i < cells.Length; i++)
                {
                    g.DrawString(cells[i], cellFont, Brushes.Black,
                                 new RectangleF(cx, y, colWidths[i], 18),
                                 i == cells.Length - 1 ? rightFmt : leftFmt);
                    cx += colWidths[i];
                }

                y += 20;
                rowNum++;
                rowIndex++;

                if (y + 110 > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            // ── Totals ────────────────────────────────────────────
            y += 8;
            DrawDashedLine(g, x, y, x + pageWidth, y);
            y += 10;

            float labelW = pageWidth * 0.5f;
            float valueX = x + labelW;
            float valueW = pageWidth * 0.5f;

            decimal total = (decimal)totalPrice;
            decimal paid = decimal.TryParse(cashierOrderForm_amount.Text,
                                 NumberStyles.Number, CultureInfo.InvariantCulture,
                                 out decimal p) ? p : 0;
            decimal change = paid - total;

            g.DrawString("Total:", totalFont, Brushes.Black,
                         new RectangleF(x, y, labelW, 22), leftFmt);
            g.DrawString(total.ToString("0.00") + " L.E", totalFont, Brushes.Black,
                         new RectangleF(valueX, y, valueW, 22), rightFmt);
            y += 26;

            g.DrawString("Paid:", boldFont, Brushes.Black,
                         new RectangleF(x, y, labelW, 20), leftFmt);
            g.DrawString(paid.ToString("0.00") + " L.E", cellFont, Brushes.Black,
                         new RectangleF(valueX, y, valueW, 20), rightFmt);
            y += 24;

            g.DrawString("Change:", boldFont, Brushes.Black,
                         new RectangleF(x, y, labelW, 20), leftFmt);
            g.DrawString(change.ToString("0.00") + " L.E",
                         new Font("Arial", 12, FontStyle.Bold),
                         new SolidBrush(Color.FromArgb(0, 128, 80)),
                         new RectangleF(valueX, y, valueW, 20), rightFmt);
            y += 32;

            // ── Footer ────────────────────────────────────────────
            DrawDashedLine(g, x, y, x + pageWidth, y);
            y += 10;
            g.DrawString("Thank you for visiting! See you again :)",
                         smallFont, Brushes.Gray,
                         new RectangleF(x, y, pageWidth, 18), center);
        }

        private void DrawDashedLine(Graphics g, float x1, float y1, float x2, float y2)
        {
            using (Pen pen = new Pen(Color.LightGray, 1) { DashPattern = new float[] { 4, 4 } })
                g.DrawLine(pen, x1, y1, x2, y2);
        }
    }
}
