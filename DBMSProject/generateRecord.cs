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
    public partial class generateRecord : Form
    {
        string path = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EMSDB;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        public generateRecord()
        {
            InitializeComponent();
            con = new SqlConnection(path);
            pictureBox1.Visible = false;
            label1.Visible = false;
        }

        private void getExam()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("select ExamName from Examination where ReportGenerated='No' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.ExecuteNonQuery();
                con.Close();
                comboBox1.DisplayMember = "ExamName";
                comboBox1.ValueMember = "ExamName";
                comboBox1.DataSource = ds.Tables[0];

                comboBox1.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        private void generateRecord_Load(object sender, EventArgs e)
        {
            getExam();
            pictureBox1.Visible = false;
            label1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Visible = true;
                label1.Visible = true;
                string cmbBox = "";
                cmbBox = comboBox1.Text;
                con.Open();
                cmd = new SqlCommand("SELECT UserId,Dept_name,SubjectName FROM EMS_User E,Subjects S,Department D where RoleId=3 AND E.DeptId=D.DeptId AND S.DeptId=D.DeptId", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach(DataRow row in dt.Rows)
                {
                    cmd = new SqlCommand("insert into StudentReport(SubjectName,StudentId,DepartmentName,ExamName)  values ('" + row["SubjectName"].ToString() + "','" + row["UserId"].ToString() + "','" + row["Dept_name"].ToString() + "','" + cmbBox + "')", con);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Successful");
                cmd = new SqlCommand("Update Examination set ReportGenerated='Yes' where ExamName='"+cmbBox+"'",con);
                cmd.ExecuteNonQuery();
                con.Close();
                pictureBox1.Visible = false;
                label1.Visible = false;
                getExam();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                pictureBox1.Visible = false;
                label1.Visible = false;
            }
        }
    }
}
