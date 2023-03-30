using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Linq;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace DBMSProject
{
    public partial class Signup : Form
    {
        string path = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EMSDB;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        public Signup()
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

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login lg = new Login();
            lg.Show();
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

        private void label3_MouseHover(object sender, EventArgs e)
        {
            label3.Font = new Font(label3.Font.Name, 9, FontStyle.Underline);
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.Font = new Font(label3.Font.Name, 9, FontStyle.Regular);
        }

        private void buttonSignup_Click(object sender, EventArgs e)
        {
            if(textBoxName.Text==""|| textBoxEmail.Text == "" || textBoxPassword.Text == ""||dateTimePickerDOB.Text=="")
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
            else if (textBoxPassword.Text!=textBoxRPassword.Text)
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
                    if (radioButtonMale.Checked)
                        gender = "Male";
                    else
                        gender = "Female";
                    con.Open();
                    string uEmail = textBoxEmail.Text;
                    string uName = "";
                    cmd = new SqlCommand("select Username from EMS_User where Email='" + textBoxEmail.Text + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        uName= dt.Rows[0]["Username"].ToString();
                        MessageBox.Show("Email: "+uEmail+" is already registered with name "+uName);
                    }
                    else
                    {
                        cmd = new SqlCommand("insert into EMS_User(Username,Email,Password,DOB,Gender,RoleId)  values ('" + textBoxName.Text + "','" + textBoxEmail.Text + "','" + textBoxPassword.Text + "','" + dateTimePickerDOB.Value + "','" + gender + "','" + 2 + "')", con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("registered successfully!");
                        reset();
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

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
    }
}
