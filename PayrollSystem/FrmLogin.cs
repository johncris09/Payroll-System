using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Globalization;

using System.IO;
namespace PayrollSystem
{
    public partial class FrmLogin : Form
    {

        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path.GetFullPath("JCMPayrollSystem.accdb"));
        
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

            txtusername.Focus(); 
            timer1.Enabled = true; // Enable the timer.
            timer1.Start();//Strart it
            timer1.Interval = 1;

            int duration = 100;//in milliseconds
            int steps = 100;
            Timer timer = new Timer();
            timer.Interval = duration / steps;

            int currentStep = 0;
            timer.Tick += (arg1, arg2) =>
            {

                Opacity = ((double)currentStep) / steps;
                currentStep++;

                if (currentStep >= steps)
                {
                    timer.Stop();
                    timer.Dispose();
                }
            };

            timer.Start();

        }
        bool close = true;
        private void btnCancel_Click(object sender, EventArgs e)
        {
            while(close)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    
                    Application.Exit();
                    close = false;
                }
                else
                {
                    close = false;
                    txtusername.Focus();
                }
            } 
            
            
        }
        public void LogIn() 
        {
            if (txtusername.Text == "" || txtpassword.Text == "")
            {
                MessageBox.Show("Invalid Username/Password", "Login", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from [user] where username = '" + txtusername.Text + "' and password = '" + txtpassword.Text + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    this.Hide();
                    frmSplash splash = new frmSplash();
                    splash.Show();

                }
                else
                {
                    MessageBox.Show("Invalid Username/Password", "Login", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }

                con.Close();

            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {

            LogIn();
        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtusername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                LogIn();
            }
        }

        private void txtpassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                LogIn();
            }
        }

        private void lblChangePassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }
    }
}
