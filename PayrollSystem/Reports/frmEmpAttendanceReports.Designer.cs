namespace PayrollSystem.Reports
{
    partial class frmEmpAttendanceReports
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.attendanceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.jCMPayrollSystemDataSet2 = new PayrollSystem.JCMPayrollSystemDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.empAttendanceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.jCMPayrollSystemDataSet = new PayrollSystem.JCMPayrollSystemDataSet();
            this.empAttendanceTableAdapter = new PayrollSystem.JCMPayrollSystemDataSetTableAdapters.empAttendanceTableAdapter();
            this.jCMPayrollSystemDataSet1 = new PayrollSystem.JCMPayrollSystemDataSet();
            this.empAttendanceBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.attendanceTableAdapter = new PayrollSystem.JCMPayrollSystemDataSetTableAdapters.attendanceTableAdapter();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.attendanceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jCMPayrollSystemDataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empAttendanceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jCMPayrollSystemDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jCMPayrollSystemDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empAttendanceBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // attendanceBindingSource
            // 
            this.attendanceBindingSource.DataMember = "attendance";
            this.attendanceBindingSource.DataSource = this.jCMPayrollSystemDataSet2;
            // 
            // jCMPayrollSystemDataSet2
            // 
            this.jCMPayrollSystemDataSet2.DataSetName = "JCMPayrollSystemDataSet";
            this.jCMPayrollSystemDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.ForeColor = System.Drawing.Color.Red;
            reportDataSource3.Name = "Emp_Attendance";
            reportDataSource3.Value = this.attendanceBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PayrollSystem.Reports.attendance.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(873, 471);
            this.reportViewer1.TabIndex = 0;
            // 
            // empAttendanceBindingSource
            // 
            this.empAttendanceBindingSource.DataMember = "empAttendance";
            this.empAttendanceBindingSource.DataSource = this.jCMPayrollSystemDataSet;
            // 
            // jCMPayrollSystemDataSet
            // 
            this.jCMPayrollSystemDataSet.DataSetName = "JCMPayrollSystemDataSet";
            this.jCMPayrollSystemDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // empAttendanceTableAdapter
            // 
            this.empAttendanceTableAdapter.ClearBeforeFill = true;
            // 
            // jCMPayrollSystemDataSet1
            // 
            this.jCMPayrollSystemDataSet1.DataSetName = "JCMPayrollSystemDataSet";
            this.jCMPayrollSystemDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // empAttendanceBindingSource1
            // 
            this.empAttendanceBindingSource1.DataMember = "empAttendance";
            this.empAttendanceBindingSource1.DataSource = this.jCMPayrollSystemDataSet1;
            // 
            // attendanceTableAdapter
            // 
            this.attendanceTableAdapter.ClearBeforeFill = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(847, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 24);
            this.button1.TabIndex = 49;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmEmpAttendanceReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.ClientSize = new System.Drawing.Size(873, 471);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmEmpAttendanceReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmEmpAttendance";
            this.Load += new System.EventHandler(this.frmEmpAttendance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.attendanceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jCMPayrollSystemDataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empAttendanceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jCMPayrollSystemDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jCMPayrollSystemDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empAttendanceBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource empAttendanceBindingSource;
        private JCMPayrollSystemDataSetTableAdapters.empAttendanceTableAdapter empAttendanceTableAdapter;
        private JCMPayrollSystemDataSet jCMPayrollSystemDataSet1;
        private System.Windows.Forms.BindingSource empAttendanceBindingSource1;
        private JCMPayrollSystemDataSet jCMPayrollSystemDataSet;
        private JCMPayrollSystemDataSet jCMPayrollSystemDataSet2;
        private System.Windows.Forms.BindingSource attendanceBindingSource;
        private JCMPayrollSystemDataSetTableAdapters.attendanceTableAdapter attendanceTableAdapter;
        private System.Windows.Forms.Button button1;
    }
}