using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PayrollSystem
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void masterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        } 
        private void userRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {

            User.User_Register frmRegister = new User.User_Register();
            frmRegister.MdiParent = this;
            frmRegister.StartPosition = FormStartPosition.CenterScreen;
            frmRegister.Show();

            
        }

        bool close = true;
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(close)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    close = false;
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User.frmChangePassword changepassword = new User.frmChangePassword();
            changepassword.MdiParent = this;
            changepassword.StartPosition = FormStartPosition.CenterScreen;
            changepassword.Show();
        }

        private void employeeRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Employee.EmployeeRegister empRegister = new Employee.EmployeeRegister();
            empRegister.MdiParent = this;
            empRegister.StartPosition = FormStartPosition.CenterScreen;
            empRegister.Show();
        }

        private void employeeSalaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Employee.frmEmpSalary empSalary = new Employee.frmEmpSalary();
            empSalary.MdiParent = this;
            empSalary.StartPosition = FormStartPosition.CenterScreen;
            empSalary.Show();

        }

        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void employeeAttendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Employee.frmEmployeeAttendance empAttendance = new Employee.frmEmployeeAttendance();
            empAttendance.MdiParent = this;
            empAttendance.StartPosition = FormStartPosition.CenterScreen;
            empAttendance.Show();

        }

        private void attendanceViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Employee.frmAttendanceView ViewAttendance = new Employee.frmAttendanceView();
            ViewAttendance.MdiParent = this;
            ViewAttendance.StartPosition = FormStartPosition.CenterScreen;
            ViewAttendance.Show();
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void userReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.frmUserReports userReports = new Reports.frmUserReports();
            userReports.MdiParent = this;
            userReports.StartPosition = FormStartPosition.CenterScreen;
            userReports.Show();
        }

        private void employeeAttendanceReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.frmEmpAttendanceReports attendanceReports = new Reports.frmEmpAttendanceReports();
            attendanceReports.MdiParent = this;
            attendanceReports.StartPosition = FormStartPosition.CenterScreen;
            attendanceReports.Show();
        }

        private void employeeSalaryReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.frmSalaryReports salaryReports = new Reports.frmSalaryReports();
            salaryReports.MdiParent = this;
            salaryReports.StartPosition = FormStartPosition.CenterScreen;
            salaryReports.Show();
        }

        private void employeeReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.frmEmpReports empReports = new Reports.frmEmpReports();
            empReports.MdiParent = this;
            empReports.StartPosition = FormStartPosition.CenterScreen;
            empReports.Show();
        }

        private void payslipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.frmemppayslip payslip = new Reports.frmemppayslip();
            payslip.MdiParent = this;
            payslip.StartPosition = FormStartPosition.CenterScreen;
            payslip.Show();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Log Out? ","Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                this.Hide();
                FrmLogin login = new FrmLogin();
                login.Show();
            }
            
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
