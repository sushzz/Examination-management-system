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
    public partial class viewStudentsF : Form
    {
        string path = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EMSDB;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        int deptId = Convert.ToInt32(Login.LoginInfo.DeptId);
        int sessionId = Convert.ToInt32(Login.LoginInfo.SessionID);
        string search = "";
        public viewStudentsF()
        {
            InitializeComponent();
            con = new SqlConnection(path);
            dataGridView1.AllowUserToAddRows = false;
            getStudents();
        }

        private void getStudents()
        {
            search = textBox1.Text + "%";
            if (search =="%")
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("select UserId,Username,Email,DOB,Gender from EMS_User where RoleId=3 and DeptId=" + deptId + " ", con);
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
            else
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("select UserId,Username,Email,DOB,Gender from EMS_User where RoleId=3 and DeptId=" + deptId + " and Username like '"+search+"' ", con);
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
            
        }

        private void viewStudentsF_Load(object sender, EventArgs e)
        {
            getStudents();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            facultyDashboard fd = new facultyDashboard();
            fd.Show();
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

        private void button1_Click(object sender, EventArgs e)
        {
            getStudents();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            getStudents();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            facultyDashboard fd = new facultyDashboard();
            fd.Show();
        }
    }
}
