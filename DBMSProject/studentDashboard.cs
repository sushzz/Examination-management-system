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
    public partial class studentDashboard : Form
    {
        string path = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EMSDB;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        int userId = Convert.ToInt32(Login.LoginInfo.UserID);
        int sessionId = Convert.ToInt32(Login.LoginInfo.SessionID);
        public studentDashboard()
        {
            InitializeComponent();
            con = new SqlConnection(path);
            dataGridView1.AllowUserToAddRows = false;
            getReport();
        }

        private void getReport()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("select ExamName,SubjectName,Score from StudentReport where StudentId=" + userId + "", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
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
