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

namespace PayrollSystem.Employee
{
    public partial class EmployeeRegister : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path.GetFullPath("JCMPayrollSystem.accdb"));
        
         public EmployeeRegister()
        {
            InitializeComponent();
        }

        private void EmployeeRegister_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtLastName;
            LoadData();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private bool ifEmployeeExist(string last_name,string first_name, string email)
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from [employee] where last_name = '" + txtLastName.Text + "' and [first_name] = '" + txtFirstName.Text+ "' and [email] = '"+txtEmail.Text+"'";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        private bool Validation() 
        {
            bool result = false;
            if(string.IsNullOrEmpty(txtLastName.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtLastName,"Last Name is Required");
            }
            else if(string.IsNullOrEmpty(txtFirstName.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtFirstName,"First Name is Required");
            }
            else if(string.IsNullOrEmpty(txtAddress.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtAddress,"Address is Required");
            }
            else if (string.IsNullOrEmpty(txtEmail.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtEmail, "Email is Required");
            }
            else 
            {
                errorProvider1.Clear();
                result = true;
            }
            return result;
        }
        
        

        

       

        private void txtLastName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFirstName.Focus();
            }
            else 
            {
                txtLastName.Focus();
            }

        }

        private void txtFirstName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtMiddleName.Focus();
            }
            else
            {
                txtFirstName.Focus();
            }

        }

        private void txtMiddleName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddress.Focus();
            }
            else
            {
                txtMiddleName.Focus();
            }
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtEmail.Focus();
            }
            else
            {
                txtAddress.Focus();
            }

        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtEmail.Text.Length > 0)
                {
                    txtMobile.Focus();

                }
                else
                {
                    txtEmail.Focus();
                }
            }

        }

        private void txtMobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpBirthdate.Focus();
            }
            else
            {
                txtMobile.Focus();
            }

        }

        private void dtpBirthdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBankDetails.Focus();
            }
            else
            {
                dtpBirthdate.Focus();
            }

        }

        private void txtBankDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
            else
            {
                txtBankDetails.Focus();
            }

        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
        /*    if(!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
         */
        }
        private void ClearData()
        {
            txtEmpID.Clear();
            txtLastName.Clear();
            txtFirstName.Clear();
            txtMiddleName.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtMobile.Clear();
            dtpBirthdate.Value = DateTime.Now;
            txtBankDetails.Clear();

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
             if(Validation())
            {
                if (ifEmployeeExist(txtLastName.Text, txtFirstName.Text, txtMiddleName.Text))
                {
                    MessageBox.Show("Employee Already Exist", "Exist", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    con.Open();
                    OleDbCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Insert into [Employee] ([Last_Name], [First_Name], [Middle_Name],[Address], [Email], [Mobile],[DOB],[Bank_Details]) values ('" + txtLastName.Text + "','" + txtFirstName.Text + "','" + txtMiddleName.Text + "','" + txtAddress.Text + "','" + txtEmail.Text + "','" + txtMobile.Text + "','" + dtpBirthdate.Value.ToString("MM/dd/yyyy") + "','" + txtBankDetails.Text + "') ";
                    MessageBox.Show("Successfully Saved","Message",MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ClearData();
                    LoadData();
                    txtLastName.Focus();
                }
            }
        }

        private void LoadData() 
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM [employee]";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells["empId"].Value = n + 1;
                dataGridView1.Rows[n].Cells["last_name"].Value = row["last_name"].ToString();
                dataGridView1.Rows[n].Cells["first_name"].Value = row["first_name"].ToString();
                dataGridView1.Rows[n].Cells["middle_name"].Value = row["middle_name"].ToString();
                dataGridView1.Rows[n].Cells["dob"].Value = Convert.ToDateTime(row["dob"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows[n].Cells["address"].Value = row["address"].ToString();
                dataGridView1.Rows[n].Cells["email"].Value = row["email"].ToString();
                dataGridView1.Rows[n].Cells["mobile"].Value = row["mobile"].ToString();
                dataGridView1.Rows[n].Cells["bank_details"].Value = row["bank_details"].ToString();
                
                
            }
            con.Close();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtEmpID.Text = dataGridView1.SelectedRows[0].Cells["EmpID"].Value.ToString();
            txtLastName.Text = dataGridView1.SelectedRows[0].Cells["last_name"].Value.ToString();
            txtLastName.Text = dataGridView1.SelectedRows[0].Cells["last_name"].Value.ToString();
            txtFirstName.Text = dataGridView1.SelectedRows[0].Cells["first_name"].Value.ToString();
            txtMiddleName.Text = dataGridView1.SelectedRows[0].Cells["middle_name"].Value.ToString();
            dtpBirthdate.Text = dataGridView1.SelectedRows[0].Cells["dob"].Value.ToString();
            txtEmail.Text = dataGridView1.SelectedRows[0].Cells["email"].Value.ToString();
            txtAddress.Text = dataGridView1.SelectedRows[0].Cells["address"].Value.ToString();
            txtMobile.Text = dataGridView1.SelectedRows[0].Cells["mobile"].Value.ToString();
            txtBankDetails.Text = dataGridView1.SelectedRows[0].Cells["bank_details"].Value.ToString();
            
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to update?","Message",MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(result == DialogResult.Yes)
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update [employee]  set [last_name] = '" + txtLastName.Text + "', [first_name] = '" + txtFirstName.Text + "', middle_name = '" + txtMiddleName.Text + "', [address] = '" + txtAddress.Text + "', [Email] = '" + txtEmail.Text + "', [mobile] = '" + txtEmail.Text + "', [dob] = '" + dtpBirthdate.Value.ToString("MM/dd/yyyy") + "', [bank_details] = '"+ txtBankDetails.Text+"' where empid = " + Convert.ToInt32(txtEmpID.Text) + "";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                MessageBox.Show("Updated Successfully", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                LoadData();
                ClearData();

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this data?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Delete from [employee] where [empID] = "+txtEmpID.Text+" ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                MessageBox.Show("Delete Successfully", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                LoadData();
                ClearData();

            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
