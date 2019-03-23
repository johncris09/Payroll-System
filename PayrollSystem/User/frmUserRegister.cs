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
    public partial class User_Register : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path.GetFullPath("JCMPayrollSystem.accdb"));
        public User_Register()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Delete from [USER] where [username] = '"+txtUsername.Text+"'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                MessageBox.Show("Updated Successfully", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                con.Close();
                ClearData(); 
                LoadData();

                btnSave.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;

            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void User_Register_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtLastName;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            LoadData();

        }

        private void txtLastName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (txtLastName.Text.Length > 0)
                {
                    txtFirstName.Focus();
                }
                else 
                {
                    txtLastName.Focus();
                }
            }
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFirstName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtFirstName.Text.Length > 0)
                {
                    txtMiddleName.Focus();
                }
                else
                {
                    txtFirstName.Focus();
                }
            }
        }

        private void txtMiddleName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtMiddleName.Text.Length > 0)
                {
                    dtpBirthdate.Focus();
                }
                else
                {
                    txtMiddleName.Focus();
                }
            }

        }

        private void txtMiddleName_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpBirthdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtMiddleName.Text.Length > 0)
                {
                    dtpBirthdate.Focus();
                }
                else
                {
                    txtMiddleName.Focus();
                }
            }

        }

        private void ClearData()
        {
            txtLastName.Clear();
            txtFirstName.Clear();
            txtMiddleName.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            cboRole.SelectedIndex = -1;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            dtpBirthdate.Value = DateTime.Now;

        }

        private bool Validation() 
        {
            bool result = false;
            if(string.IsNullOrEmpty(txtLastName.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtLastName, "Last Name Required"); 
            }
            else if(string.IsNullOrEmpty(txtFirstName.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtFirstName, "First Name Required"); 
            }
            else if(string.IsNullOrEmpty(txtAddress.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtAddress,"Address is Required");
            }
            else if(string.IsNullOrEmpty(txtEmail.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtEmail,"Email is Required");
            }
            else if(cboRole.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cboRole,"Select Role");
            }
            else if(string.IsNullOrEmpty(txtUsername.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtUsername,"Username is Required");
            }
            else if(string.IsNullOrEmpty(txtPassword.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtPassword,"Password is Required");

            }
            else if (txtPassword.Text.Length < 6)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtPassword, "Password Minimum Character Required");
            }
            else 
            {
                errorProvider1.Clear();
                result = true;
            }
            return result;
        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {
           // txtAddress.Text = "INSERT INTO [USER](last_name, first_name, middle_name, email, username,password, role,dob,Address)VALUES('" + txtLastName.Text + "','" + txtFirstName.Text + "','" + txtMiddleName.Text + "','" + txtEmail.Text + "','" + txtUsername.Text + "','" + txtPassword.Text + "','" + cboRole.Text + "','" + dtpBirthdate.Value.ToString("MM/dd/yyyy") + "','" + txtAddress.Text + "')";
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "INSERT INTO [USER] ([last_name], [first_name], [middle_name], [email], [username],[password], [role],[dob],[Address])VALUES('" + txtLastName.Text + "','" + txtFirstName.Text + "','" + txtMiddleName.Text + "','" + txtEmail.Text + "','" + txtUsername.Text + "','" + txtPassword.Text + "','" + cboRole.Text + "','" + dtpBirthdate.Value.ToString("MM/dd/yyyy") + "','" + txtAddress.Text + "')";
            //cmd.CommandText = "Insert into [user] (last_name,dob) values (\""+txtLastName.Text+"\",\""+dtpBirthdate.Value.ToString("dd/MM/yyyy")+"\")";
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Saved!", "Saved", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            ClearData();
            con.Close();
            LoadData();
            
        }

        private void LoadData()
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM [USER]";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach(DataRow row in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells["user_id"].Value = n + 1;
                dataGridView1.Rows[n].Cells["last_name"].Value = row["last_name"].ToString();
                dataGridView1.Rows[n].Cells["first_name"].Value = row["first_name"].ToString();
                dataGridView1.Rows[n].Cells["middle_name"].Value = row["middle_name"].ToString();
                dataGridView1.Rows[n].Cells["birthdate"].Value =  Convert.ToDateTime(row["dob"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows[n].Cells["Email"].Value = row["email"].ToString();
                dataGridView1.Rows[n].Cells["username"].Value = row["username"].ToString();
                dataGridView1.Rows[n].Cells["address"].Value = row["address"].ToString();
                dataGridView1.Rows[n].Cells["role"].Value = row["role"].ToString();
                
            }
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtLastName.Text = dataGridView1.SelectedRows[0].Cells["last_name"].Value.ToString();
            txtFirstName.Text = dataGridView1.SelectedRows[0].Cells["first_name"].Value.ToString();
            txtMiddleName.Text = dataGridView1.SelectedRows[0].Cells["middle_name"].Value.ToString();
            dtpBirthdate.Text = dataGridView1.SelectedRows[0].Cells["birthdate"].Value.ToString();
           // txtMiddleName.Text = dataGridView1.SelectedRows[0].Cells["birthdate"].Value.ToString();
            txtEmail.Text = dataGridView1.SelectedRows[0].Cells["Email"].Value.ToString();
            txtAddress.Text = dataGridView1.SelectedRows[0].Cells["address"].Value.ToString();
            txtUsername.Text = dataGridView1.SelectedRows[0].Cells["username"].Value.ToString();
            cboRole.Text = dataGridView1.SelectedRows[0].Cells["Role"].Value.ToString();

            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to update?","Update",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Update [user] set [last_name]='"+txtLastName.Text+"', [first_name] = '"+txtFirstName.Text+"', [middle_name] = '"+txtMiddleName.Text+"', [email] = '"+txtEmail.Text+"',[role] = '"+cboRole.Text+"', [dob] = '"+dtpBirthdate.Value.ToString("dd/MM/yyyy")+"', [Address] = '"+txtAddress.Text +"' where [username] = '"+txtUsername.Text+"'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                MessageBox.Show("Updated Successfully","Message",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
               
                con.Close();
                ClearData();
                LoadData();

                btnSave.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
