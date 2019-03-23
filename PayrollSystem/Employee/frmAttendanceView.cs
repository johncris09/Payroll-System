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
    public partial class frmAttendanceView : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path.GetFullPath("JCMPayrollSystem.accdb"));
        
        public frmAttendanceView()
        {
            InitializeComponent();
        }

        private void frmAttendanceView_Load(object sender, EventArgs e)
        {
            Search();
            txtSearch.Visible = false;
            cboSearchCategory.SelectedIndex = 0;
            loadMonth();
            loadYear();
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

        private void cboSearchCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboSearchCategory.SelectedIndex == 0)
            {
                txtSearch.Visible = false;
            }

            if (cboSearchCategory.SelectedIndex != 0)
            {
                txtSearch.Visible = true;
                txtSearch.Clear();

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
            this.dgView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dgviewcol1, dgviewcol2 });
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

        void Search(int LX, int LY, int DW, int DH, string ColName, string ColSize)
        {
            this.dgView.Location = new System.Drawing.Point(LX, LY);
            this.dgView.Size = new System.Drawing.Size(DW, DH);

            string[] clSize = ColSize.Split(',');
            //Size
            for (int i = 0; i < clSize.Length; i++)
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
            for (int i = 0; i < clName.Length; i++)
            {
                this.dgView.Columns[i].HeaderText = clName[i];
                this.dgView.Columns[i].Visible = true;
            }




        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if(txtSearch.Text.Length != 0  )
            {
                if(cboSearchCategory.SelectedIndex == 1)
                {
                    this.dgView.Visible = true;
                    dgView.BringToFront();
                    Search(120, 90, 400, 200, "Emp Id, Emp Name", "100,0");
                    this.dgView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.employee_MouseDoubleClick);
                    con.Open();
                    OleDbCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM [Employee] where [EmpId]  like  '" + txtSearch.Text + "%'";
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
            }
        }

        bool change = true;

        private void employee_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (change)
            {
                change = false;
                txtSearch.Text = dgView.SelectedRows[0].Cells[0].Value.ToString();
                this.dgView.Visible = false;
                cboYear.Focus();
                change = true;
            }
        }

        void LoadData(string condition )
        {


            
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT empAttendance.emp_ID, empAttendance.Year_duty, empAttendance.Month_duty, empAttendance.TotalDays, empAttendance.WorkingDays, empAttendance.PresentDays, empAttendance.AbsentDays, empAttendance.LopDays, Employee.Last_Name, Employee.First_Name, Employee.Middle_Name FROM (Employee INNER JOIN empAttendance ON Employee.EmpID = empAttendance.emp_ID) " + condition + "";
                // cmd.CommandText = "SELECT empAttendance.* from empAttendance  " + condition + "";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    int n = dgView.Rows.Add();
                    dataGridView1.Rows[n].Cells["empId"].Value = row["emp_ID"].ToString();
                    dataGridView1.Rows[n].Cells["name"].Value = row["last_name"].ToString() + ", " + row["first_name"].ToString() + " " + row["middle_name"].ToString();
                    dataGridView1.Rows[n].Cells["name"].Value = n + 1;
                    dataGridView1.Rows[n].Cells["year"].Value = row["year_duty"].ToString();
                    dataGridView1.Rows[n].Cells["month"].Value = row["month_duty"].ToString();
                    dataGridView1.Rows[n].Cells["totalDays"].Value = row["totalDays"].ToString();
                    dataGridView1.Rows[n].Cells["workingDays"].Value = row["workingDays"].ToString();
                    dataGridView1.Rows[n].Cells["presentDays"].Value = row["PresentDays"].ToString();
                    dataGridView1.Rows[n].Cells["absentDays"].Value = row["AbsentDays"].ToString();
                    dataGridView1.Rows[n].Cells["lopDays"].Value = row["LopDays"].ToString();
                }

                con.Close();
             

        }


        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgView.Rows.Count > 0)
                {
                    txtSearch.Text = dgView.SelectedRows[0].Cells[0].Value.ToString(); 
                    this.dgView.Visible = false;
                    cboYear.Focus();
                }
                else
                {
                    this.dgView.Visible = true;
                }
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            
            if(cboSearchCategory.SelectedIndex == 0)
            {

                //textBox1.Text =" SELECT empAttendance.emp_ID, empAttendance.Year_duty, empAttendance.Month_duty, empAttendance.TotalDays, empAttendance.WorkingDays, empAttendance.PresentDays, empAttendance.AbsentDays, empAttendance.LopDays, Employee.Last_Name, Employee.First_Name, Employee.Middle_Name FROM (Employee INNER JOIN empAttendance ON Employee.EmpID = empAttendance.emp_ID) where empAttendance.year_duty = '" + cboYear.Text + "' and empAttendance.Month_duty = '" + cboMonth.Text + "'";
                //LoadData("where empAttendance.year_duty = '"+cboYear.Text+"' and empAttendance.Month_duty = '"+cboMonth.Text+"'");
                con.Open(); 
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT empAttendance.emp_ID, empAttendance.Year_duty, empAttendance.Month_duty, empAttendance.TotalDays, empAttendance.WorkingDays, empAttendance.PresentDays, empAttendance.AbsentDays, empAttendance.LopDays, Employee.Last_Name, Employee.First_Name, Employee.Middle_Name FROM (Employee INNER JOIN empAttendance ON Employee.EmpID = empAttendance.emp_ID) where empAttendance.year_duty = '" + cboYear.Text + "' and empAttendance.Month_duty = '" + cboMonth.Text + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    int n = dgView.Rows.Add();
                    dataGridView1.Rows[n].Cells["empId"].Value = n + 1;
                     dataGridView1.Rows[n].Cells["name"].Value = row["last_name"].ToString() + ", " + row["first_name"].ToString() + " " + row["middle_name"].ToString();
                    dataGridView1.Rows[n].Cells["year"].Value = row["year_duty"].ToString();
                    dataGridView1.Rows[n].Cells["month"].Value = row["month_duty"].ToString();
                    dataGridView1.Rows[n].Cells["totalDays"].Value = row["totalDays"].ToString();
                    dataGridView1.Rows[n].Cells["workingDays"].Value = row["workingDays"].ToString();
                    dataGridView1.Rows[n].Cells["presentDays"].Value = row["PresentDays"].ToString();
                    dataGridView1.Rows[n].Cells["absentDays"].Value = row["AbsentDays"].ToString();
                    dataGridView1.Rows[n].Cells["lopDays"].Value = row["LopDays"].ToString();
                }
                con.Close(); 
                
            
            }
            else if (cboSearchCategory.SelectedIndex == 1)
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT empAttendance.emp_ID, empAttendance.Year_duty, empAttendance.Month_duty, empAttendance.TotalDays, empAttendance.WorkingDays, empAttendance.PresentDays, empAttendance.AbsentDays, empAttendance.LopDays, Employee.Last_Name, Employee.First_Name, Employee.Middle_Name FROM (Employee INNER JOIN empAttendance ON Employee.EmpID = empAttendance.emp_ID) where empAttendance.emp_id = " + txtSearch.Text + " and  empAttendance.year_duty = '" + cboYear.Text + "' and empAttendance.Month_duty = '" + cboMonth.Text + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.Rows[0].Cells["empId"].Value = dt.Rows[0]["emp_ID"].ToString();
                    dataGridView1.Rows[0].Cells["name"].Value = dt.Rows[0]["last_name"].ToString() + ", " + dt.Rows[0]["first_name"].ToString() + " " + dt.Rows[0]["middle_name"].ToString();
                    dataGridView1.Rows[0].Cells["year"].Value = dt.Rows[0]["year_duty"].ToString();
                    dataGridView1.Rows[0].Cells["month"].Value = dt.Rows[0]["month_duty"].ToString();
                    dataGridView1.Rows[0].Cells["totalDays"].Value = dt.Rows[0]["totalDays"].ToString();
                    dataGridView1.Rows[0].Cells["workingDays"].Value = dt.Rows[0]["workingDays"].ToString();
                    dataGridView1.Rows[0].Cells["presentDays"].Value = dt.Rows[0]["presentDays"].ToString();
                    dataGridView1.Rows[0].Cells["absentDays"].Value = dt.Rows[0]["absentDays"].ToString();
                    dataGridView1.Rows[0].Cells["lopDays"].Value = dt.Rows[0]["lopDays"].ToString();
                }
                else 
                
                {
                    MessageBox.Show("No Record yet", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                

                con.Close();
            }

           
        }


        private void searchEmpId()
        {
            
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
