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

namespace PayrollSystem
{
    public partial class frmsample : Form
    { 
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path.GetFullPath("JCMPayrollSystem.accdb"));
        public frmsample()
        {
            InitializeComponent();
        }

        private void frmsample_Load(object sender, EventArgs e)
        {

            con.Open();
            con.Close();
        }
    }
}
