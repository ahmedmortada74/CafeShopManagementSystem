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
    public partial class Form1 : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\AIA\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");
        public Form1()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void login_registerBtn_Click(object sender, EventArgs e)
        {
            RegisterForm regForm = new RegisterForm(); 
            regForm.Show();
            this.Hide();
        }

        private void login_showPass_CheckedChanged(object sender, EventArgs e)
        {
            login_password.PasswordChar = login_showPass.Checked ? '\0' : '*';
        }

        public bool emptyFields()
        {
            if(login_username.Text =="" || login_password.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void login_btn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("All Fields are required to be filled.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State == ConnectionState.Closed)
                {

                    try
                    {
                        connect.Open();
                        string selectAccount = "SELECT * FROM users WHERE username = @usern AND password =@pass AND status =@status";

                        using (SqlCommand cmd = new SqlCommand(selectAccount, connect))
                        {
                            cmd.Parameters.AddWithValue("@usern",login_username.Text.Trim());
                            cmd.Parameters.AddWithValue("@pass",login_password.Text.Trim());
                            cmd.Parameters.AddWithValue("@status","Active");
                            
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            adapter.Fill(table);    
                            if (table.Rows.Count >= 1)
                            {
                                MessageBox.Show("Login Successflly!  ", "Information Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                                AdminMainForm adminMainForm = new AdminMainForm();
                                adminMainForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Incorrect Username/Passwoed or ther's no Admin's approval   ", "Erroe Message ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Connection faild :" + ex, "Error Message ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
