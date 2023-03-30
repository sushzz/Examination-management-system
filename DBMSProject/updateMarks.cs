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
    public partial class updateMarks : Form
    {
        string path = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EMSDB;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        DataTable dtCopy = new DataTable();
        int userId = Convert.ToInt32(Login.LoginInfo.UserID);
        int deptId = Convert.ToInt32(Login.LoginInfo.DeptId);
        int sessionId = Convert.ToInt32(Login.LoginInfo.SessionID);
        string deptName = "";
        public updateMarks()
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
                cmd = new SqlCommand("select Dept_name from Department where DeptId="+deptId+"", con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                deptName = dt1.Rows[0]["Dept_name"].ToString();
                cmd = new SqlCommand("select * from StudentReport where DepartmentName='"+deptName+"' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                
                da.Fill(dt);
                da.Fill(dtCopy);
                dataGridView1.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        private void updateMarks_Load(object sender, EventArgs e)
        {
            getReport();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            facultyDashboard fd = new facultyDashboard();
            fd.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                con.Open();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataRow dataRow in dtCopy.Rows)
                    {
                        if (row.Cells["MarksId"].Value.ToString() == dataRow["MarksId"].ToString())
                        {
                            if (row.Cells["Score"].Value.ToString() != dataRow["Score"].ToString())
                            {
                                cmd = new SqlCommand("Update StudentReport set Score="+ row.Cells["Score"].Value + ",LastUpdatedBy="+userId+"where MarksId="+ row.Cells["MarksId"].Value + "", con);
                                cmd.ExecuteNonQuery();

                            }
                        }
                    }
                }
                con.Close();
                getReport();
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            facultyDashboard fd = new facultyDashboard();
            fd.Show();
        }
    }
}
