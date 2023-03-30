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
    public partial class viewStudents: Form
    {

        string path = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EMSDB;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        string search = "";

        public viewStudents()
        {
            InitializeComponent();
            con = new SqlConnection(path);
            dataGridView1.AllowUserToAddRows = false;
        }

        private void getStudents()
        {
            search = textBox1.Text + "%";
            if (search == "%")
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("select UserId,Username,Email,DOB,Gender,Dept_name from EMS_User E,Department D where E.DeptId=D.DeptId AND RoleId=3 ", con);
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
                    cmd = new SqlCommand("select UserId,Username,Email,DOB,Gender,Dept_name from EMS_User E,Department D where E.DeptId=D.DeptId AND RoleId=3 AND Username like '"+search+"' ", con);
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

        private void addButton()
        {
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Insert(6, buttonColumn);
            buttonColumn.HeaderText = "Delete Row";
            buttonColumn.Width = 100;
            buttonColumn.Text = "Delete";
            buttonColumn.UseColumnTextForButtonValue = true;
        }

        private void viewTeachers_Load(object sender, EventArgs e)
        {
            getStudents();
            addButton();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Delete Row")
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["UserId"].Value);
                string name = dataGridView1.Rows[e.RowIndex].Cells["Username"].Value.ToString();
                if (MessageBox.Show(string.Format("Do you want to delete User: "+ name+" with Id: "+id+" ?"), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        con.Open();
                        cmd = new SqlCommand("Delete from EMS_User where UserId='" + id + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        getStudents();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            getStudents();
        }
    }
}
