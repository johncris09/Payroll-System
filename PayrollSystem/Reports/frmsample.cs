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
    public partial class frmsample : Form
    {
        public frmsample()
        {
            InitializeComponent();
        }

        private void frmsample_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
