using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DBMSProject
{
    public partial class about : Form
    {
        public about()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Homepage hp = new Homepage();
            hp.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Homepage hp = new Homepage();
            hp.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login lg = new Login();
            lg.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Signup sp = new Signup();
            sp.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Contact hlp = new Contact();
            hlp.Show();
        }
    }
}
