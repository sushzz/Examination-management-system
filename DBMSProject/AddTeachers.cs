using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DBMSProject
{
    public partial class AddTeachers : Form
    {
        string path = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EMSDB;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        //String strDateFormat = "yyyy-MM-dd";
        
        int deptId = 1;

        public AddTeachers()
        {
            InitializeComponent();
            con = new SqlConnection(path);
            dateTimePickerDOB.MaxDate = DateTime.Today;
            textBoxPassword.PasswordChar = '*';
            textBoxPassword.MaxLength = 16;
            textBoxRPassword.PasswordChar = '*';
            textBoxRPassword.MaxLength = 16;
        }

        private void reset()
        {
            textBoxName.Text = "";
            textBoxEmail.Text = "";
            textBoxPassword.Text = "";
            textBoxRPassword.Text = "";
            radioButtonMale.Checked = false;
            radioButtonFemale.Checked = false;
        }

        private void textBoxEmail_Leave(object sender, EventArgs e)
        {
            Regex mRegxExpression;
            if (textBoxEmail.Text.Trim() != string.Empty)

            {

                mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

                if (!mRegxExpression.IsMatch(textBoxEmail.Text.Trim()))

                {

                    MessageBox.Show("E-mail address format is not correct.");

                    textBoxEmail.Focus();

                }

            }
        }

        private void pictureBox5_MouseDown(object sender, MouseEventArgs e)
        {
            textBoxPassword.PasswordChar = '\0';
        }

        private void pictureBox5_MouseUp(object sender, MouseEventArgs e)
        {
            textBoxPassword.PasswordChar = '*';
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            textBoxRPassword.PasswordChar = '\0';
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            textBoxRPassword.PasswordChar = '*';
        }

        private void buttonSignup_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text == "" || textBoxEmail.Text == "" || textBoxPassword.Text == "" || dateTimePickerDOB.Text == "")
            {
                MessageBox.Show("All the fields are mandatory except gender!");
                if (textBoxName.Text == "")
                    textBoxName.Focus();
                else if (textBoxEmail.Text == "")
                    textBoxEmail.Focus();
                else if (textBoxPassword.Text == "")
                    textBoxPassword.Focus();
                else
                    dateTimePickerDOB.Focus();
            }
            else if (textBoxPassword.Text != textBoxRPassword.Text)
            {
                MessageBox.Show("Password doesn't match!");
                textBoxRPassword.Text = "";
                textBoxRPassword.Focus();
            }
            else
            {
                try
                {
                    string gender;
                    //DateTime datetime = DateTime.ParseExact(dateTimePickerDOB.Text, strDateFormat, CultureInfo.InvariantCulture);
                    if (radioButtonMale.Checked)
                        gender = "Male";
                    else
                        gender = "Female";
                    if (comboBox1.Text == "CS")
                        deptId = 1;
                    else if (comboBox1.Text == "IS")
                        deptId = 2;
                    else if (comboBox1.Text == "BT")
                        deptId = 3;
                    else if (comboBox1.Text == "EC")
                        deptId = 4;
                    else
                        deptId = 5;
                    con.Open();
                    string uEmail = textBoxEmail.Text;
                    string uName = "";
                    cmd = new SqlCommand("select Username from EMS_User where Email='" + textBoxEmail.Text + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        uName = dt.Rows[0]["Username"].ToString();
                        MessageBox.Show("Email: " + uEmail + " is already registered with name " + uName);
                        con.Close();
                    }
                    else
                    {
                        cmd = new SqlCommand("insert into EMS_User(Username,Email,Password,DOB,Gender,RoleId,DeptId)  values ('" + textBoxName.Text + "','" + textBoxEmail.Text + "','" + textBoxPassword.Text + "','" + dateTimePickerDOB.Text + "','" + gender + "','" + 2 + "',"+deptId+")", con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("registered successfully!");
                        con.Close();
                        reset();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    con.Close();
                }
            }
        }

        private void AddTeachers_Load(object sender, EventArgs e)
        {
            dateTimePickerDOB.CustomFormat = "MM-dd-yyyy";
            dateTimePickerDOB.Format = DateTimePickerFormat.Custom;
        }
    }
}
