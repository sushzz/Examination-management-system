using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DBMSProject
{
    public partial class Contact : Form
    {
        string path = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EMSDB;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        public Contact()
        {
            InitializeComponent();
            con = new SqlConnection(path);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Homepage hp = new Homepage();
            hp.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Homepage hp = new Homepage();
            hp.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login lg = new Login();
            lg.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Signup sp = new Signup();
            sp.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            about ab = new about();
            ab.Show();
        }

        private void buttonSignup_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string email = textBoxEmail.Text;
                string comments = textBoxComments.Text;
                cmd = new SqlCommand("select UserId,Username from EMS_User where Email='" + email + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    cmd = new SqlCommand("Insert into Comments (Email,Comments) Values('"+email+"','"+comments+"')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Submitted Successfully!");
                    textBoxEmail.Text = "";
                    textBoxComments.Text = "";
                }
                else
                {
                    MessageBox.Show("Not a valid user");
                    con.Close();
                    textBoxEmail.Text = "";
                    textBoxComments.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }
    }
}
