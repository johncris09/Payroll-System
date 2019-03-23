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
    public partial class frmEmpSalary : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path.GetFullPath("JCMPayrollSystem.accdb"));
        
        
        public frmEmpSalary()
        {
            InitializeComponent();
        }

        
        private void txtEmpID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT  [EmpID],[Last_Name],[First_Name],[MIddle_Name] FROM [Employee] where [EmpId]  like  '" + txtEmpID.Text + "%'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                dataGridView2.DataSource = dt;
                con.Close();
            }
            catch (Exception)
            {
                con.Close();
                LoadData();
            }
            finally
            {
                con.Close();
            }
            
        }

        private void txtEmpID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsNumber (e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtEmpID_KeyDown(object sender, KeyEventArgs e)
        {
             
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (dataGridView2.Rows.Count > 0)
                    {

                        txtEmpID.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                        txtLastName.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                        txtFirstName.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                        txtMiddleName.Text = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
                        dtpJoinDate.Focus();


                    }
                }
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("No Record for Employee Id '"+txtEmpID.Text+"'","Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                ClearData();
            }
            finally
            {
                con.Close();
            }
            
        }


        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT  [EmpID],[Last_Name],[First_Name],[MIddle_Name] FROM [Employee] where [last_name]  like  '" + txtLastName.Text + "%'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();

        }

        private void txtLastName_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dataGridView2.Rows.Count > 0)
                {
                    txtEmpID.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                    txtLastName.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                    txtFirstName.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                    txtMiddleName.Text = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
                    dtpJoinDate.Focus();

                }
            }
        }

        private void dtpJoinDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                txtSalary.Focus();
            }
        }

        private void txtSalary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if(txtSalary.Text.Length > 0)
                {
                    btnSave.Focus();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtSalary.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtSalary,"Salary is Required");
            }else{

                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from [empSalary] where [empID] = " + txtEmpID.Text ;
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Employee is Already Exist", "Messsage", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    try 
                    {
                        cmd.CommandText = "INSERT INTO empsalary  (EmpId, JoinDate, Salary) VALUES  ('" + txtEmpID.Text + "','" + dtpJoinDate.Value.ToString("MM/dd/yyyy") + "','" + txtSalary.Text + "')";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record Saved!", "Saved", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        ClearData(); 
                    }catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    
                }

                con.Close();



                /************/
                
            }
            
            
        }

        private void LoadData()
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT empsalary.*, Employee.Last_name, Employee.first_name, Employee.middle_name FROM Employee, empsalary where employee.EmpID = empSalary.EmpId ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                //dataGridView1.Rows[n].Cells["empID"].Value = n + 1;
                dataGridView1.Rows[n].Cells["empID"].Value = row["EmpID"].ToString();
                dataGridView1.Rows[n].Cells["last_name"].Value = row["last_name"].ToString();
                dataGridView1.Rows[n].Cells["first_name"].Value = row["first_name"].ToString();
                dataGridView1.Rows[n].Cells["middle_name"].Value = row["middle_name"].ToString();
                dataGridView1.Rows[n].Cells["join_date"].Value = Convert.ToDateTime(row["JoinDate"].ToString()).ToString("MM/dd/yyyy");
                dataGridView1.Rows[n].Cells["salary"].Value = row["salary"].ToString();
                /*dataGridView1.Rows[n].Cells["username"].Value = row["username"].ToString();
               dataGridView1.Rows[n].Cells["address"].Value = row["address"].ToString();
               dataGridView1.Rows[n].Cells["role"].Value = row["role"].ToString();*/

            }
            con.Close();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        
        
        }
        private void ClearData()
        {
            txtEmpID.Clear();
            txtLastName.Clear();
            txtFirstName.Clear();
            txtMiddleName.Clear();
            txtSalary.Clear();
            dtpJoinDate.Value = DateTime.Now;
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

        }
        private bool Validation()
        {

            bool result = false;

            if (string.IsNullOrEmpty(txtEmpID.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtLastName, "Emp Id  Required");
            }
            else if (string.IsNullOrEmpty(txtSalary.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtFirstName, "Salary Required");
            }
            else
            {
                errorProvider1.Clear();
                result = true;
            }
            return result;
        
        }
         
        private void frmEmpSalary_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtEmpID;
            LoadData();

        }


        private void txtSalary_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&(e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                txtLastName.Text = dataGridView1.SelectedRows[0].Cells["last_name"].Value.ToString();
                txtFirstName.Text = dataGridView1.SelectedRows[0].Cells["first_name"].Value.ToString();
                txtMiddleName.Text = dataGridView1.SelectedRows[0].Cells["middle_name"].Value.ToString();
                txtEmpID.Text = dataGridView1.SelectedRows[0].Cells["empId"].Value.ToString();
                dtpJoinDate.Text = dataGridView1.SelectedRows[0].Cells["join_date"].Value.ToString();
                txtSalary.Text = dataGridView1.SelectedRows[0].Cells["salary"].Value.ToString();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            


            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this data?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "Delete from [empsalary] where [EmpId] = " + txtEmpID.Text + " ";
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE [empsalary] SET JoinDate ='"+dtpJoinDate.Text+"', Salary ='"+ txtSalary.Text+"' where [empId] = " + txtEmpID.Text;
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
