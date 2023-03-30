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
    public partial class viewAddExam : Form
    {
        string path = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EMSDB;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        public viewAddExam()
        {
            InitializeComponent();
            con = new SqlConnection(path);
            dataGridView1.AllowUserToAddRows = false;
        }

        private void getExam()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("select ExamId,ExamName,ExamDate from Examination ", con);
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

        private void reset()
        {
            textBoxName.Text = "";
            dateTimePicker1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text == "")
            {
                MessageBox.Show("Enter Exam Name");
                textBoxName.Focus();
            }
            else
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("insert into Examination(ExamName,ExamDate,ReportGenerated)  values ('" + textBoxName.Text + "','" + dateTimePicker1.Value + "','No')", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("registered successfully!");
                    reset();
                    con.Close();
                    getExam();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void viewAddExam_Load(object sender, EventArgs e)
        {
            getExam();
        }
    }
}
