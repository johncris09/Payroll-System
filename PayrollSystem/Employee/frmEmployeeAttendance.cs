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
    public partial class frmEmployeeAttendance : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path.GetFullPath("JCMPayrollSystem.accdb"));
        
        public frmEmployeeAttendance()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtEmpID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtTotalDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }


        private DataGridView dgView;
        private DataGridViewTextBoxColumn dgviewcol1;
        private DataGridViewTextBoxColumn dgviewcol2;

        void Search()
        {

            dgView = new DataGridView();
            dgviewcol1 = new DataGridViewTextBoxColumn();
            dgviewcol2 = new DataGridViewTextBoxColumn();
            this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]{this.dgviewcol1,dgviewcol2});
            this.dgView.Name = "dgView";
            dgView.Visible = false;
            this.dgviewcol1.Visible = false;
            this.dgviewcol2.Visible = false;
            this.dgView.AllowUserToAddRows = false;
            this.dgView.RowHeadersVisible = false;
            this.dgView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Controls.Add(dgView);
            this.dgView.ReadOnly = true;
            dgView.BringToFront();
        }

        void Search(int LX, int LY,int DW,int DH, string ColName, string ColSize) 
        {
            this.dgView.Location = new System.Drawing.Point(LX, LY);
            this.dgView.Size = new System.Drawing.Size(DW, DH);

            string[] clSize = ColSize.Split(',');
            //Size
            for (int i = 0; i < clSize.Length; i++ )
            {
                if (int.Parse(clSize[i]) != 0)
                {
                    dgView.Columns[i].Width = int.Parse(clSize[i]);
                }
                else
                {
                    dgView.Columns[i].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
                }
            }

            // Name

            string[] clName = ColName.Split(',');
            for (int i = 0; i < clName.Length; i++ )
            {
                this.dgView.Columns[i].HeaderText = clName[i];
                this.dgView.Columns[i].Visible = true;
            }


        
        
        }

        private void frmEmployeeAttendance_Load(object sender, EventArgs e)
        {
            Search();
            this.ActiveControl = txtEmpID;
            loadYear();
            loadMonth();
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;

        }

        private void loadMonth()
        {
            DateTime now = DateTime.Now;
            for (int i = 0; i < 12; i++)
            {
                cboMonth.Items.Add(now.ToString("MMMM"));
                now = now.AddMonths(1);
            } 
        }

        private void loadYear()
        {
            DateTime todaysDate = DateTime.Now.Date;
            int year = todaysDate.Year;
            for (int i = year; i >= 1900; i--)
            {
                cboYear.Items.Add(i.ToString());
            }
             
            
        }

        private void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtEmpID_TextChanged(object sender, EventArgs e)
        {

            if (txtEmpID.Text.Length > 0)
            {
                this.dgView.Visible = true;
                dgView.BringToFront();
                Search(90, 70, 400, 200, "Emp Id, Emp Name", "100,0");
                this.dgView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.employee_MouseDoubleClick);
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [Employee] where [EmpId]  like  '" + txtEmpID.Text + "%'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                dgView.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    int n = dgView.Rows.Add();
                    dgView.Rows[n].Cells[0].Value = row["EmpId"].ToString();
                    dgView.Rows[n].Cells[1].Value = row["Last_Name"].ToString() + " " + row["First_Name"].ToString() + " " + row["Middle_Name"].ToString(); ;

                }

                con.Close();
            }
            else
            {
                dgView.Visible = false;
            }
        }

        bool change = true; 

        private void employee_MouseDoubleClick(object sender, MouseEventArgs e)
        { 
            if(change)
            {
                change = false;
                txtEmpID.Text = dgView.SelectedRows[0].Cells[0].Value.ToString();
                txtName.Text = dgView.SelectedRows[0].Cells[1].Value.ToString();
                this.dgView.Visible = false;
                cboYear.Focus();
                change = true;
            }
        }

        private void txtEmpID_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (dgView.Rows.Count > 0)
                {
                    txtEmpID.Text = dgView.SelectedRows[0].Cells[0].Value.ToString();
                    txtName.Text = dgView.SelectedRows[0].Cells[1].Value.ToString();
                    this.dgView.Visible = false;
                    cboYear.Focus();
                }
                else
                {
                    this.dgView.Visible = true;
                }
            }
        }

        private void cboYear_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (cboYear.SelectedIndex != -1)
                {
                    cboMonth.Focus();

                }
                else
                {
                    cboYear.Focus();
                }
            }
        }

        private void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboMonth.SelectedIndex != -1)
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * from [empAttendance] where [emp_Id] = "+txtEmpID.Text+" and [Year_duty] = '"+cboYear.Text+"' and [month_duty] = '"+cboMonth.Text+"'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtTotalDays.Text = dt.Rows[0]["TotalDays"].ToString();
                    txtWorkingDays.Text = dt.Rows[0]["WorkingDays"].ToString();
                    txtPresentDays.Text = dt.Rows[0]["PresentDays"].ToString();
                    txtAbsentDays.Text = dt.Rows[0]["AbsentDays"].ToString();
                    txtLopDays.Text = dt.Rows[0]["LopDays"].ToString();
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;

                }
                else
                {

                    txtTotalDays.Clear();
                    txtWorkingDays.Clear(); 
                    txtPresentDays.Clear();  
                    txtAbsentDays.Clear(); 
                    txtLopDays.Clear(); 
                    btnSave.Enabled = true;
                    btnUpdate.Enabled = false;
                    btnDelete.Enabled = false;
                }
                 
                con.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text; 
            cmd.CommandText = "INSERT INTO empAttendance (emp_ID, Year_duty, Month_duty, TotalDays, WorkingDays, PresentDays, AbsentDays, LopDays) VALUES   ('"+txtEmpID.Text+"','"+cboYear.Text+"','"+cboMonth.Text+"','"+txtTotalDays.Text+"','"+txtWorkingDays.Text+"','"+txtPresentDays.Text+"','"+txtAbsentDays.Text+"','"+txtLopDays.Text+"')"; 
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Saved!", "Saved", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            ClearData();
            con.Close();
        }

        public void ClearData()
        {
            txtEmpID.Clear();
            txtName.Clear();
            cboYear.SelectedIndex = -1;
            cboMonth.SelectedIndex = -1;
            txtTotalDays.Clear();
            txtWorkingDays.Clear();
            txtPresentDays.Clear();
            txtAbsentDays.Clear();
            txtLopDays.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to update?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE empAttendance SET Year_duty ='"+cboYear.Text+"', Month_duty ='"+cboMonth.Text+"', TotalDays ='"+txtTotalDays.Text+"', WorkingDays ='"+txtWorkingDays.Text+"', PresentDays ='"+txtPresentDays.Text+"', AbsentDays ='"+txtAbsentDays.Text+"', LopDays ='"+txtLopDays.Text+"' where emp_id = "+txtEmpID.Text+" and year_duty = '"+cboYear.Text+"' and month_duty = '"+cboMonth.Text+"'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                MessageBox.Show("Updated Successfully", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                con.Close();
                ClearData();

                btnSave.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Delete from [empAttendance] where emp_id = " + txtEmpID.Text + " and year_duty = '" + cboYear.Text + "' and month_duty = '" + cboMonth.Text + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                con.Close();
                ClearData(); 

                btnSave.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                txtEmpID.Focus();

            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {

            Employee.frmAttendanceView ViewAttendance = new Employee.frmAttendanceView(); 
            ViewAttendance.StartPosition = FormStartPosition.CenterScreen;
            ViewAttendance.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
