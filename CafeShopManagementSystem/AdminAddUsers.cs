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
                ||adminAddUsers_role.Text==""||adminAddUsers_status.Text ==""
                || adminAddUsers_imageView ==null)
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
                MessageBox.Show("All fields are required to bo filld","Erroe Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                if(connect.State == ConnectionState.Closed)
                {
                    try
                    {
                        connect.Open();
                        string selectUsern = "SELECT * FROM users WHERE username = @usern";

                        using (SqlCommand checkUsern =new SqlCommand(selectUsern, connect))
                        {
                            checkUsern.Parameters.AddWithValue("@usern", adminAddUsers_username.Text.Trim());

                            SqlDataAdapter adapter = new SqlDataAdapter(checkUsern);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            if(table.Rows.Count >= 1)
                            {
                                string usern = adminAddUsers_username.Text.Substring(0, 1).ToUpper() + adminAddUsers_username.Text.Substring(1);
                                 MessageBox.Show(usern+"is already taken","Error Message",MessageBoxButtons.OK,MessageBoxIcon.Error);

                            }
                            else
                            {
                                string insertData = "INSERT INTO users (username, password, profile_image, role, status, date_reg)" +
                                    "VALUES(@usern, @pass, @image, @role, @status, @date)";
                                DateTime today = DateTime.Now;

                                string path = Path.Combine(@"E:\projects\CafeShopManagementSystem\CafeShopManagementSystem\User_Directory\", adminAddUsers_username.Text.Trim() + ".jpg");

                                string directoryPath =Path.GetDirectoryName(path);

                                if (!Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }
                                File.Copy(adminAddUsers_imageView.ImageLocation, path, true);

                                using(SqlCommand cmd =new SqlCommand(insertData, connect))
                                {
                                    cmd.Parameters.AddWithValue("@usern", adminAddUsers_username.Text.Trim());
                                    cmd.Parameters.AddWithValue("@pass", adminAddUsers_password.Text.Trim());
                                    cmd.Parameters.AddWithValue("@image", path);
                                    cmd.Parameters.AddWithValue("@role", adminAddUsers_role.Text.Trim());
                                    cmd.Parameters.AddWithValue("@status", adminAddUsers_status.Text.Trim());
                                    cmd.Parameters.AddWithValue("@date",today);

                                    cmd.ExecuteNonQuery();

                                    MessageBox.Show("Added Successfully!","Information Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
                                    displayAddUsersData();
                                }  
                            }
                        } 
                    }catch (Exception ex)
                    {
                        MessageBox.Show("Connection failed:"+ex,"Error Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();

                    }
                }
            }
        }

        private void adminAddUsers_importBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg; &*.png|*.jpg; &*.png)";
                string imagePath = "";
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    adminAddUsers_imageView.ImageLocation = imagePath;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:"+ex, "Error Message" ,MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            //userData.ID = (int)reader["id"];
            //userData.Username = reader["username"].ToString();
            //userData.Password = reader["password"].ToString();
            //userData.Role = reader["role"].ToString();
            //userData.Status = reader["status"].ToString();
            //userData.Image = reader["profile_image"].ToString();
            //userData.DateRegistered = reader["date_reg"].ToString();
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            adminAddUsers_username.Text = row.Cells[1].Value.ToString();
            adminAddUsers_password.Text = row.Cells[2].Value.ToString();
            adminAddUsers_role.Text = row.Cells[3].Value.ToString();
            adminAddUsers_status.Text = row.Cells[4].Value.ToString();
             
            string imagePath = row.Cells[5].Value.ToString();   
            if(imagePath != null)
            {
                adminAddUsers_imageView.Image =Image.FromFile(imagePath);
            }
            else
            {
                adminAddUsers_imageView.Image = null;
            }
            
        }
    }
}
