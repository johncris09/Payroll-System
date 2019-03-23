namespace PayrollSystem.Reports
{
    partial class frmSalaryReports
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.employeeSalaryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.jCMPayrollSystemDataSet = new PayrollSystem.JCMPayrollSystemDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.employeeSalaryTableAdapter = new PayrollSystem.JCMPayrollSystemDataSetTableAdapters.employeeSalaryTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.employeeSalaryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jCMPayrollSystemDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // employeeSalaryBindingSource
            // 
            this.employeeSalaryBindingSource.DataMember = "employeeSalary";
            this.employeeSalaryBindingSource.DataSource = this.jCMPayrollSystemDataSet;
            // 
            // jCMPayrollSystemDataSet
            // 
            this.jCMPayrollSystemDataSet.DataSetName = "JCMPayrollSystemDataSet";
            this.jCMPayrollSystemDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.employeeSalaryBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PayrollSystem.Reports.Report2.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(863, 471);
            this.reportViewer1.TabIndex = 0;
            // 
            // employeeSalaryTableAdapter
            // 
            this.employeeSalaryTableAdapter.ClearBeforeFill = true;
            // 
            // frmSalaryReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(863, 471);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSalaryReports";
            this.Text = "Employee Salary";
            this.Load += new System.EventHandler(this.frmSalaryReports_Load);
            ((System.ComponentModel.ISupportInitialize)(this.employeeSalaryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jCMPayrollSystemDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private JCMPayrollSystemDataSet jCMPayrollSystemDataSet;
        private System.Windows.Forms.BindingSource employeeSalaryBindingSource;
        private JCMPayrollSystemDataSetTableAdapters.employeeSalaryTableAdapter employeeSalaryTableAdapter;
    }
}