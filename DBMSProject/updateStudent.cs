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
    public partial class updateStudent : Form
    {
        string path = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EMSDB;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        DataTable dtCopy = new DataTable();
        public updateStudent()
        {
            InitializeComponent();
            con = new SqlConnection(path);
            dataGridView1.AllowUserToAddRows = false;
            getStudents();
        }

        private void getStudents()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("select UserId,Username,Email,DOB,Gender from EMS_User where RoleId=3 ", con);
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

        private void updateStudent_Load(object sender, EventArgs e)
        {
            getStudents();
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
                        if (row.Cells["UserId"].Value.ToString() == dataRow["UserId"].ToString())
                        {
                            if (row.Cells["Username"].Value.ToString() != dataRow["Username"].ToString()|| row.Cells["Email"].Value.ToString() != dataRow["Email"].ToString()|| row.Cells["DOB"].Value.ToString() != dataRow["DOB"].ToString()|| row.Cells["Gender"].Value.ToString() != dataRow["Gender"].ToString())
                            {
                                cmd = new SqlCommand("Update EMS_User set Username='" + row.Cells["Username"].Value + "',Email='" + row.Cells["Email"].Value + "',DOB='"+ row.Cells["DOB"].Value + "',Gender='"+ row.Cells["Gender"].Value + "'where UserId=" + row.Cells["UserId"].Value + "", con);
                                cmd.ExecuteNonQuery();

                            }
                        }
                    }
                }
                con.Close();
                getStudents();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                con.Close();
            }
        }
    }
}
