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

namespace PayrollSystem.Reports
{

    public partial class frmemppayslip : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Via Marie\Desktop\PayrollSystem\PayrollSystem\JCMPayrollSystem.accdb");
        
        public frmemppayslip()
        {
            InitializeComponent();
        }
        

        private void txtEmpID_KeyDown(object sender, KeyEventArgs e)
        {
             
        }

        private void frmemppayslip_Load(object sender, EventArgs e)
        {
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
            cboMonth.SelectedIndex = 0;
        }

        private void loadYear()
        {
            DateTime todaysDate = DateTime.Now.Date;
            int year = todaysDate.Year;
            for (int i = year; i >= 1950; i--)
            {
                cboYear.Items.Add(i.ToString());
            }
            cboYear.SelectedIndex = 0;

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmpID_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtEmpID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }


        float salary = 0;
        float workingDays = 0;
        float present = 0;
        float lop = 0;
        float income = 0;
        float deduction = 0;
        float netsalary = 0;
        float perday = 0;


        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if(txtEmpID.Text.Length > 0)
            {
            
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Employee.EmpID, Employee.Last_Name, Employee.First_Name, Employee.Middle_Name, empAttendance.Year_duty, empAttendance.Month_duty, empAttendance.WorkingDays, empAttendance.PresentDays, empAttendance.LopDays, empsalary.Salary FROM ((empAttendance INNER JOIN Employee ON empAttendance.emp_ID = Employee.EmpID) INNER JOIN empsalary ON Employee.EmpID = empsalary.EmpId) WHERE (empAttendance.Year_duty = '" + cboYear.Text + "') AND (empAttendance.Month_duty = '" + cboMonth.Text + "') AND (Employee.EmpID = " + txtEmpID.Text + ") ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    salary = float.Parse(dt.Rows[0]["salary"].ToString());
                    workingDays = float.Parse(dt.Rows[0]["workingDays"].ToString());
                    present = float.Parse(dt.Rows[0]["presentDays"].ToString());
                    lop = float.Parse(dt.Rows[0]["lopDays"].ToString());
                    perday = (salary / 12) / workingDays;
                    income = perday * present;
                    deduction = perday * lop;
                    netsalary = income - deduction;


                    txtDisplayEmpId.Text = dt.Rows[0]["EmpId"].ToString();
                    txtName.Text = dt.Rows[0]["Last_Name"].ToString() + ", " + dt.Rows[0]["First_Name"].ToString() + " " + dt.Rows[0]["Middle_name"].ToString(); ;
                    txtYear.Text = dt.Rows[0]["Year_duty"].ToString();
                    txtMonth.Text = dt.Rows[0]["Month_duty"].ToString();
                    txtIncome.Text = income.ToString();
                    txtDeduction.Text = deduction.ToString();
                    txtNetSalary.Text = netsalary.ToString();
                }
                else
                {
                    MessageBox.Show("No record");
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
