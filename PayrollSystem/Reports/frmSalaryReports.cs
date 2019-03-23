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
    public partial class frmSalaryReports : Form
    {
        public frmSalaryReports()
        {
            InitializeComponent();
        }

        private void frmSalaryReports_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'jCMPayrollSystemDataSet.employeeSalary' table. You can move, or remove it, as needed.
            this.employeeSalaryTableAdapter.Fill(this.jCMPayrollSystemDataSet.employeeSalary);

            this.reportViewer1.RefreshReport();
        }
    }
}
