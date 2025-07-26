using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Threading;
using System.IO;

namespace MALL
{
    public partial class frmReportForm : Form
    {
        string qry = "", Path = "",expath="",Heading="";

        public frmReportForm(string Path, string qry, string expath)
        {
            InitializeComponent();
            this.Path = Path; this.qry = qry; this.expath = expath;
        }
        public frmReportForm(string Path, string qry, string expath,string Heading)
        {
            InitializeComponent();
            this.Path = Path; this.qry = qry; this.expath = expath;
            this.Heading = Heading;
        }
        ReportDocument cryRpt = null;
        public frmReportForm(ReportDocument cryRpt)
        {
            InitializeComponent();
            this.cryRpt = cryRpt;
            crystalReportViewer1.ReportSource = cryRpt;
            cryRpt.Refresh();
            crystalReportViewer1.ShowPrintButton = true;
            
            crystalReportViewer1.ShowRefreshButton = true;
            crystalReportViewer1.ShowCloseButton = true;
            crystalReportViewer1.DisplayGroupTree = true;
            crystalReportViewer1.Focus();
            crystalReportViewer1.HorizontalScroll.Visible = true;
            crystalReportViewer1.VerticalScroll.Visible = true;
            crystalReportViewer1.Zoom(1);
            //crystalReportViewer1.Zoom= Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            //this.crystalReportViewer1.ZOO .reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            crystalReportViewer1.Refresh();
           
        }
        public frmReportForm(ReportDocument cryRpt,bool HideExport)
        {
            InitializeComponent();
            this.cryRpt = cryRpt;
            crystalReportViewer1.ReportSource = cryRpt;
            cryRpt.Refresh();
            crystalReportViewer1.ShowPrintButton = true;
            crystalReportViewer1.ShowExportButton = !HideExport;
            crystalReportViewer1.ShowRefreshButton = true;
            crystalReportViewer1.ShowCloseButton = true;
            crystalReportViewer1.DisplayGroupTree = false ;
            
            crystalReportViewer1.Focus();
            crystalReportViewer1.HorizontalScroll.Visible = true;
            crystalReportViewer1.VerticalScroll.Visible = true;
            crystalReportViewer1.Refresh();

        }
        public frmReportForm(bool Load)
        {
            //string A = System.IO.Path.GetTempPath();
            //string temp = Environment.GetEnvironmentVariable("TEMP");
            //string[] K = System.IO.Directory.GetFiles(temp);
            //int i ;
            //for (i= 0;i<K.Length;i++)
            //{
            //    if (K[i].Contains(".rpt"))
            //    {
            //       // System.IO.File.Delete(K[i]);//
            //    }
            //}
            Thread th = new Thread(new ThreadStart(Load_));
            th.Start();
        }
        private void  Load_()
        {
            InitializeComponent();
            ReportDocument cryRpt = new ReportDocument();
            cryRpt.Load(Defaults.Rep_Path + "BlankReport.rpt");
            foreach (CrystalDecisions.Shared.IConnectionInfo connection in cryRpt.DataSourceConnections)
            {
                connection.SetConnection("mall", "mall", "mall", "mall");
                connection.SetLogon("mall", "mall");
            }

            if (qry != "") cryRpt.RecordSelectionFormula = qry;

            if (!(Heading.Equals("")))
            {
                cryRpt.DataDefinition.FormulaFields["test"].Text = "'" + Heading + "';";
                // cryRpt.DataDefinition.FormulaFields["test"].Text = Heading;
            }

            crystalReportViewer1.ReportSource = cryRpt;
            cryRpt.Refresh();
            crystalReportViewer1.Refresh();
            cryRpt.Close();
            cryRpt.Dispose();
            this.Dispose();
        }

        private void frmReportForm_Load(object sender, EventArgs e)
        {
            /*ReportDocument cryRpt = new ReportDocument();
            cryRpt.Load(Path);
            foreach (CrystalDecisions.Shared.IConnectionInfo connection in cryRpt.DataSourceConnections)
            {
                connection.SetConnection("mall", "mall", "mall", "mall");
                connection.SetLogon("mall", "mall");
            }
           
            if (qry != "") cryRpt.RecordSelectionFormula = qry;

            if (!(Heading.Equals("")))
            {
                cryRpt.DataDefinition.FormulaFields["test"].Text ="'"+ Heading +"';" ;
               // cryRpt.DataDefinition.FormulaFields["test"].Text = Heading;
            }

            crystalReportViewer1.ReportSource = cryRpt;
            cryRpt.Refresh();
            crystalReportViewer1.ShowPrintButton = true;
            crystalReportViewer1.ShowRefreshButton = true;
            crystalReportViewer1.ShowCloseButton = true;
            crystalReportViewer1.DisplayGroupTree = false;
            //
            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            ExcelFormatOptions CrFormatTypeOptions = new ExcelFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = Defaults.Exp_Path + "STKReport.xls";
            CrExportOptions = cryRpt.ExportOptions;
            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            CrExportOptions.ExportFormatType = ExportFormatType.Excel;
            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            CrExportOptions.FormatOptions = CrFormatTypeOptions;
            cryRpt.Export();//excel commended for testing only

            //
            //ExportOptions CrExportOptions;
            //DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions1 = new PdfRtfWordFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = expath;
            CrExportOptions = cryRpt.ExportOptions;
            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            CrExportOptions.FormatOptions = CrFormatTypeOptions1;
            cryRpt.Export();
            crystalReportViewer1.Refresh();
            crystalReportViewer1.ShowFirstPage();
            crystalReportViewer1.ShowLastPage();
            crystalReportViewer1.ShowNextPage();
            crystalReportViewer1.ShowPreviousPage();*/

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void crystalReportViewer1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                crystalReportViewer1.ShowNextPage();
            }
            else if (e.KeyCode == Keys.Right)
            {
                crystalReportViewer1.ShowPreviousPage();
            }

        }

        private void frmReportForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                crystalReportViewer1.ShowNextPage();
            }
            else if (e.KeyCode == Keys.Right)
            {
                crystalReportViewer1.ShowPreviousPage();
            }
            else if (e.KeyCode == Keys.Down)
            { 
            }
            else if (e.KeyCode == Keys.Up)
            { 
            }
        }


        private void frmReportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.crystalReportViewer1.Dispose();
                this.crystalReportViewer1 = null;
                cryRpt.Close();
                cryRpt.Dispose();

                 GC.Collect();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error In Report Form Closing" + ex);
            }

        }
    }
}
