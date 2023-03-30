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
    public partial class facultyDashboard : Form
    {
        string path = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EMSDB;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        int sessionId = Convert.ToInt32(Login.LoginInfo.SessionID);
        public facultyDashboard()
        {
            InitializeComponent();
            con = new SqlConnection(path);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            viewStudentsF vsf = new viewStudentsF();
            vsf.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            updateMarks um = new updateMarks();
            um.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Hide();
            updateMarks um = new updateMarks();
            um.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            viewStudentsF vsf = new viewStudentsF();
            vsf.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("Update Session_History Set Logout_DT=GETDATE() where SessionId=" + sessionId + "", con);
                cmd.ExecuteNonQuery();
                con.Close();
                Login.LoginInfo.RoleID = "";
                Login.LoginInfo.UserID = "";
                Login.LoginInfo.SessionID = "";
                this.Hide();
                Homepage hp = new Homepage();
                hp.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
