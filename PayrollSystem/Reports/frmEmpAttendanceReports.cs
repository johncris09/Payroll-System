using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PayrollSystem.Reports
{
    public partial class frmEmpAttendanceReports : Form
    {
        public frmEmpAttendanceReports()
        {
            InitializeComponent();
        }

        private void frmEmpAttendance_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'jCMPayrollSystemDataSet2.attendance' table. You can move, or remove it, as needed.
            this.attendanceTableAdapter.Fill(this.jCMPayrollSystemDataSet2.attendance);
            // TODO: This line of code loads data into the 'jCMPayrollSystemDataSet1.empAttendance' table. You can move, or remove it, as needed.
            this.empAttendanceTableAdapter.Fill(this.jCMPayrollSystemDataSet1.empAttendance);
            // TODO: This line of code loads data into the 'jCMPayrollSystemDataSet.empAttendance' table. You can move, or remove it, as needed.
            this.empAttendanceTableAdapter.Fill(this.jCMPayrollSystemDataSet.empAttendance);

            this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
