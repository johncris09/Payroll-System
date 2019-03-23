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
    public partial class frmUserReports : Form
    {
        public frmUserReports()
        {
            InitializeComponent();
        }

        private void frmUserReports_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'jCMPayrollSystemDataSet.user' table. You can move, or remove it, as needed.
            this.userTableAdapter.Fill(this.jCMPayrollSystemDataSet.user);

            this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
