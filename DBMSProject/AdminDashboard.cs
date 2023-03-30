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
    public partial class AdminDashboard : Form
    {
        string path = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EMSDB;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        private Form activeForm = null;
        int sessionId = Convert.ToInt32(Login.LoginInfo.SessionID);
        public AdminDashboard()
        {
            InitializeComponent();
            customizeDesign();
            con = new SqlConnection(path);
            getCount();
        }

        int teacherCount = 0;
        int studentCount = 0;
        int examCount = 0;

        private void getCount()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("Select Count(*) from EMS_User Where RoleId=2 ", con);
                teacherCount = (int)cmd.ExecuteScalar();
                cmd = new SqlCommand("Select Count(*) from EMS_User Where RoleId=3 ", con);
                studentCount = (int)cmd.ExecuteScalar();
                cmd = new SqlCommand("Select Count(*) from Examination", con);
                examCount = (int)cmd.ExecuteScalar();
                con.Close();
                label1.Text = "Teachers: " + teacherCount.ToString();
                label6.Text = "Students: " + studentCount.ToString();
                label7.Text = "Exams: " + examCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);  
            }
        }

        private void customizeDesign()
        {
            teachersPanel.Visible = false;
            studentsPanel.Visible = false;
            examPanel.Visible = false;
        }

        private void hideSubMenu()
        {
            if (teachersPanel.Visible == true)
                teachersPanel.Visible = false;
            if (studentsPanel.Visible == true)
                studentsPanel.Visible = false;
            if (examPanel.Visible == true)
                examPanel.Visible = false;
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            adminPanel.Controls.Add(childForm);
            adminPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showSubMenu(teachersPanel);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            showSubMenu(studentsPanel);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openChildForm(new viewTeachers());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openChildForm(new AddTeachers());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            openChildForm(new viewStudents());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            openChildForm(new AddStudents());
        }

        private void button13_Click(object sender, EventArgs e)
        {
            showSubMenu(examPanel);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            openChildForm(new viewAddExam());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openChildForm(new generateRecord());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
        }

        private void adminPanel_Paint(object sender, PaintEventArgs e)
        {
            getCount();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("Update Session_History Set Logout_DT=GETDATE() where SessionId="+sessionId+"", con);
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            openChildForm(new updateStudent());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openChildForm(new updateTeacher());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            openChildForm(new comments());
        }
    }
}
