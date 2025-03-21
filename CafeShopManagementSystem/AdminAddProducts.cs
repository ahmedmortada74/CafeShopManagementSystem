﻿using System;
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
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace CafeShopManagementSystem
{
    public partial class AdminAddProducts : UserControl
    {
           SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\AIA\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");
        public AdminAddProducts()
        {
            InitializeComponent();
            displayProductsData();
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
        public void displayProductsData()
        {
            AdminAddProductsData prodData = new AdminAddProductsData();
            List<AdminAddProductsData> listData = prodData.productsListData();
            dataGridView1.DataSource= listData;
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

                        string selectProdID = "SELECT * FROM products WHERE prod_id = @prodID";

                        using (SqlCommand selectPID = new SqlCommand(selectProdID, connect))
                        {
                            selectPID.Parameters.AddWithValue("@prodID", adminAddProducts_id.Text.Trim());

                            SqlDataAdapter adapter = new SqlDataAdapter(selectPID);
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            if(table.Rows.Count >= 1)
                            {
                                MessageBox.Show("Product ID:" +adminAddProducts_id.Text.Trim()+ " Is Already Taken", "Error Message"
                                    , MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            else
                            {
                                string insertData = "INSERT INTO products (prod_id, prod_name, prod_type, prod_stock," +
                                    " prod_price, prod_status, prod_image, date_insert) VALUES" +
                                    "(@prodID,@prodName, @prodType, @prodStock, @prodPrice, @prodStatus, @prodImage, @dataInsert)";
                                 DateTime today = DateTime.Now;

                                string path = Path.Combine(@"E:\projects\CafeShopManagementSystem\CafeShopManagementSystem\Product_Directory\"
                                                        ,adminAddProducts_id.Text.Trim()+ ".jpg");

                                string directoryPath = Path.GetDirectoryName(path);

                                if (!Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }

                                File.Copy(adminAddProducts_imageView.ImageLocation, path, true);

                                using(SqlCommand cmd = new SqlCommand(insertData, connect))
                                {
                                    cmd.Parameters.AddWithValue("@prodID", adminAddProducts_id.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodName", adminAddProducts_name.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodType", adminAddProducts_type.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodStock", adminAddProducts_stock.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodPrice", adminAddProducts_price.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodStatus", adminAddProducts_status.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodImage", path);
                                    cmd.Parameters.AddWithValue("@dataInsert", today);

                                    cmd.ExecuteNonQuery();
                                    clearFields();
                                    MessageBox.Show("Added Successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    displayProductsData();
                                }
                            }
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
        }

        private void adminAddProducts_importBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg; &*.png|*.jpg; &*.png)";
                string imagePath = "";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    adminAddProducts_imageView.ImageLocation = imagePath;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void clearFields()
        {
            adminAddProducts_id.Text = "";
            adminAddProducts_name.Text = "";
            adminAddProducts_type.SelectedIndex = -1;
            adminAddProducts_stock.Text = "";
            adminAddProducts_price.Text = "";
            adminAddProducts_status.SelectedIndex = -1;
            adminAddProducts_imageView.Image = null;
        }

        private void adminAddProducts_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex !=-1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                adminAddProducts_id.Text = row.Cells[1].Value.ToString();
                adminAddProducts_name.Text = row.Cells[2].Value.ToString();
                adminAddProducts_type.Text = row.Cells[3].Value.ToString();
                adminAddProducts_stock.Text = row.Cells[4].Value.ToString();
                adminAddProducts_price.Text = row.Cells[5].Value.ToString();
                adminAddProducts_status.Text = row.Cells[6].Value.ToString();

                string imagePath = row.Cells[7].Value.ToString();
                try
                {
                    if (imagePath != null)
                    {
                        adminAddProducts_imageView.Image = Image.FromFile(imagePath);
                    }
                    else
                    {
                        adminAddProducts_imageView.Image = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No Image :3", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void adminAddProducts_apdateBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("All fields are required to bo filld", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult check = MessageBox.Show("Are you sure you want to Update Product ID:" + adminAddProducts_id.Text.Trim()
                  + "?", "Confrirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (check == DialogResult.Yes)
                {

                    if (connect.State != ConnectionState.Open)
                    {
                        try

                        {
                            connect.Open();
                            string updateData = "UPDATE products SET prod_name =@prodName, prod_type =@prodType ,prod_stock =@prodStock," +
                                " prod_price=@prodPrice,date_update=@dateUpdate WHERE prod_id=@prodID ";
                            DateTime today = DateTime.Now;
                            using (SqlCommand cmd = new SqlCommand(updateData, connect))
                            {
                                cmd.Parameters.AddWithValue("@prodName", adminAddProducts_name.Text.Trim());
                                cmd.Parameters.AddWithValue("@prodType", adminAddProducts_type.Text.Trim());
                                cmd.Parameters.AddWithValue("@prodStock", adminAddProducts_stock.Text.Trim());
                                cmd.Parameters.AddWithValue("@prodPrice", adminAddProducts_price.Text.Trim());
                                cmd.Parameters.AddWithValue("@prodStatus", adminAddProducts_status.Text.Trim());
                                //cmd.Parameters.AddWithValue("@prodImage", path);
                                cmd.Parameters.AddWithValue("@dateUpdate", today);
                                cmd.Parameters.AddWithValue("@prodID", adminAddProducts_id.Text.Trim());

                                cmd.ExecuteNonQuery();
                                clearFields();
                                MessageBox.Show("Updated Successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                displayProductsData();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Connection failed:" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            connect.Close();
                        }
                    }
                }
            }
        }

        private void adminAddProducts_deleteBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("All fields are required to bo filld", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult check = MessageBox.Show("Are you sure you want to Delete Product ID:" + adminAddProducts_id.Text.Trim()
                  + "?", "Confrirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (check == DialogResult.Yes)
                {

                    if (connect.State != ConnectionState.Open)
                    {
                        try

                        {
                            connect.Open();
                            string updateData = "UPDATE products SET  date_delete=@dateDpdate WHERE prod_id=@prodID";
                            DateTime today = DateTime.Now;
                            using (SqlCommand cmd = new SqlCommand(updateData, connect))
                            {
                                
                                cmd.Parameters.AddWithValue("@dateDpdate", today);
                                cmd.Parameters.AddWithValue("@prodID", adminAddProducts_id.Text.Trim());

                                cmd.ExecuteNonQuery();
                                clearFields();
                                MessageBox.Show("Removed Successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                displayProductsData();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Connection failed:" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
}
