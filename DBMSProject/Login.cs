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
    public partial class Login : Form
    {
        string path = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EMSDB;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        public Login()
        {
            InitializeComponent();
            con = new SqlConnection(path);
            textBox2.PasswordChar = '*';
            textBox2.MaxLength = 16;
            pictureBox5.Visible = false;
        }

        public static class LoginInfo
        {
            public static string UserID;
            public static string RoleID;
            public static string SessionID;
            public static string DeptId;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            ForgotPassword fp = new ForgotPassword();
            fp.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
                pictureBox5.Visible = false;
            else
                pictureBox5.Visible = true;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Contact help = new Contact();
            help.Show();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

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
            Signup sp = new Signup();
            sp.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string userId = textBox1.Text;
                string password = textBox2.Text;
                string responseUserId = "";
                string userName = "";
                string RoleId = "";
                int sessionID = 0;
                cmd = new SqlCommand("select UserId,Username,RoleId,DeptId from EMS_User where Email='" + userId + "'and Password='" + password + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    userName= dt.Rows[0]["Username"].ToString();
                    RoleId= dt.Rows[0]["RoleId"].ToString();
                    responseUserId= dt.Rows[0]["UserId"].ToString();
                    LoginInfo.UserID = responseUserId;
                    LoginInfo.RoleID = RoleId;
                    LoginInfo.DeptId= dt.Rows[0]["DeptId"].ToString();
                    //MessageBox.Show("Login sucess Welcome "+userName+"Role:"+RoleId);
                    cmd = new SqlCommand("insert into Session_History (Login_DT,UserId)  values (GETDATE(),'" + responseUserId + "')SELECT SCOPE_IDENTITY()", con);
                    //cmd.ExecuteNonQuery();
                    sessionID = Convert.ToInt32(cmd.ExecuteScalar());
                    LoginInfo.SessionID = sessionID.ToString();
                    con.Close();
                    if (RoleId == "1")
                    {
                        this.Hide();
                        AdminDashboard ad = new AdminDashboard();
                        ad.Show();
                    }
                    else if (RoleId == "2")
                    {
                        this.Hide();
                        facultyDashboard fd = new facultyDashboard();
                        fd.Show();
                    }
                    else if (RoleId == "3")
                    {
                        this.Hide();
                        studentDashboard sd = new studentDashboard();
                        sd.Show();
                    }
                    else
                    {
                        MessageBox.Show("Server Busy");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Login please check username and password");
                    con.Close();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        private void pictureBox5_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.PasswordChar = '\0';
        }

        private void pictureBox5_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            about abt = new about();
            abt.Show();
        }
    }
}
