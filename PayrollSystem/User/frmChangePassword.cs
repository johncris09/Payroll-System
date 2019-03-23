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
namespace PayrollSystem.User
{
    public partial class frmChangePassword : Form
    {

        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path.GetFullPath("JCMPayrollSystem.accdb"));
        public frmChangePassword()
        {
            InitializeComponent();
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtUsername;
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (txtUsername.Text.Length > 0)
                {
                    txtOldPassword.Focus();

                }
                else 
                {
                    txtUsername.Focus();
                }
            }

        }
        private void txtOldPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtOldPassword.Text.Length > 0)
                {
                    txtNewPassword.Focus();

                }
                else
                {
                    txtOldPassword.Focus();
                }
            }
            
        }
        private void txtNewPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtNewPassword.Text.Length > 0)
                {
                    txtConfirmPassword.Focus();

                }
                else
                {
                    txtNewPassword.Focus();
                }
            }

        }
        private void txtConfirmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtConfirmPassword.Text.Length > 0)
                {
                    btnChange.Focus();

                }
                else
                {
                    txtConfirmPassword.Focus();
                }
            }
            
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to change Password?","Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {

                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT 1 FROM [USER] where [username] = '"+txtUsername.Text+"' and password = '"+txtOldPassword.Text+"'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (txtNewPassword.Text == txtConfirmPassword.Text)
                    {
                        if (txtNewPassword.Text.Length > 3)
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "Update [User] set [password] = '"+txtNewPassword.Text +"' where [username] ='"+txtUsername.Text+"' and [password] = '"+txtOldPassword.Text+"'";
                            
                            cmd.ExecuteNonQuery();
                            da.Fill(dt);
                            MessageBox.Show("Record Updated Successfully","Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);


                        }
                        else
                        {
                            errorProvider1.SetError(txtNewPassword,"Please Enter  4 character password");
                        }
                    }
                    else {
                        errorProvider1.SetError(txtNewPassword, "Password Mistake");
                        errorProvider1.SetError(txtConfirmPassword, "Password Mistake");
                    }

                }
                else {
                    errorProvider1.SetError(txtUsername,"Please Check Username and Password");
                    errorProvider1.SetError(txtOldPassword, "Please Check Username and Password");
                }
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
