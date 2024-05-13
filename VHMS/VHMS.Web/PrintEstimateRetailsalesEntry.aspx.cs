using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using System.Data;
using System.IO;
using System.Text;
using System.Drawing.Printing;

public partial class PrintEstimateRetailsalesEntry : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    ReportDocument rprt = new ReportDocument();
    ReportDocument rprtDuplicate = new ReportDocument();
    string sException = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.btnExportReport);

        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            VHMSService.AddPageVisitLog();
            if (HttpContext.Current.Session["EstimateSalesEntryID"] != null)
                hdnPurchaseNo.Value = HttpContext.Current.Session["EstimateSalesEntryID"].ToString();
            else
                hdnPurchaseNo.Value = "-1";
        }
        //txtDOR.Text = Convert.ToDateTime(DateTime.Now).ToString("MMMM-yyyy");
        LoadReport();
        // ExportReport();
    }
    private void LoadReport()
    {
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        decimal DiscountAmount = 0;

        // dsData = VHMS.DataAccess.VHMSReports.PrintPurchaseEntryInvoice(Convert.ToInt32(hdnPurchaseNo.Value));
        string sTemp = string.Empty;
        try
        {

            ReportDataSet dsReportData = new ReportDataSet();
            dsReportData = LoadData();
            if (dsReportData.tEstimateSalesEntry.Rows.Count > 0)
            {

                foreach (ReportDataSet.tEstimateSalesEntryRow drtBranchRow in dsReportData.tEstimateSalesEntry)
                {
                    DiscountAmount = drtBranchRow.DiscountAmount;
                }
            }
            if (ddluser.Text != "EstimateMEMO")
            {
                rprt = new ReportDocument();

                if (DiscountAmount > 0)
                    rprt.Load(Server.MapPath("~/Reports/rpteEstimateSalesEntryA5.rpt"));
                else
                    rprt.Load(Server.MapPath("~/Reports/rptEstimateSalesEntryWithoutDiscountAmtA5.rpt"));
                rprt.SetDataSource(dsReportData);
                this.CRDischargeSummaryReport.ReportSource = rprt;
                CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
                CRDischargeSummaryReport.Zoom(100);
                this.CRDischargeSummaryReport.DataBind();
            }
            else
            {
                rprtDuplicate = new ReportDocument();
                if (DiscountAmount > 0)
                    rprtDuplicate.Load(Server.MapPath("~/Reports/rptEstimateSalesEntryMEMOA5.rpt"));
                else
                    rprtDuplicate.Load(Server.MapPath("~/Reports/rptEstimateSalesEntryMEMOWithoutDiscountA5.rpt"));
                rprtDuplicate.SetDataSource(dsReportData);
                this.CRDuplicate.ReportSource = rprtDuplicate;
                CRDuplicate.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
                CRDuplicate.Zoom(100);
                this.CRDuplicate.DataBind();
            }

        }

        catch (Exception ex)
        {
            sException = "frmPrintJobCard | " + ex.ToString();
            Log.Write(sException);
        }
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rprt.Close();
        rprt.Dispose();
        rprtDuplicate.Close();
        rprtDuplicate.Dispose();
    }
    private ReportDataSet LoadData()
    {
        dsData = VHMS.DataAccess.VHMSReports.PrintEstimateRetailsSalesInvoice(Convert.ToInt32(hdnPurchaseNo.Value));
        ReportDataSet dsReportData = new ReportDataSet();

        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "tEstimateSalesEntry";
            foreach (DataRow drow in dsData.Tables[0].Rows) dsReportData.tEstimateSalesEntry.ImportRow(drow);
            dsData.Tables[1].TableName = "tEstimateSalesEntryTrans";
            foreach (DataRow drow in dsData.Tables[1].Rows) dsReportData.tEstimateSalesEntryTrans.ImportRow(drow);
            dsData.Tables[2].TableName = "tCustomer";
            foreach (DataRow drow in dsData.Tables[2].Rows) dsReportData.tCustomer.ImportRow(drow);
            dsData.Tables[3].TableName = "tCompany";
            foreach (DataRow drow in dsData.Tables[3].Rows) dsReportData.tCompany.ImportRow(drow);

        }
        return dsReportData;
    }
    //private void ExportReport()
    //{
    //    ExportOptions exopt = default(ExportOptions);
    //    DiskFileDestinationOptions dfdopt = new DiskFileDestinationOptions();
    //    string fname = "Invoice.pdf";
    //    dfdopt.DiskFileName = Server.MapPath(fname);

    //    exopt = rprt.ExportOptions;
    //    exopt.ExportDestinationType = ExportDestinationType.DiskFile;

    //    //for PDF select PortableDocFormat for excel select ExportFormatType.Excel    
    //    exopt.ExportFormatType = ExportFormatType.PortableDocFormat;
    //    exopt.DestinationOptions = dfdopt;

    //    //finally export your report document    
    //    rprt.Export();

    //    //To open your PDF after save it from crystal report    

    //    string Path = Server.MapPath(fname);
    //    FileInfo file = new FileInfo(Path);
    //    Response.ClearContent();

    //    Response.AddHeader("Content-Disposition", "inline; filename=" + file.Name);
    //    Response.AddHeader("Content-Length", file.Length.ToString());
    //    Response.ContentType = "application/pdf";
    //    Response.TransmitFile(file.FullName);
    //    Response.Flush();
    //}
    protected void ddlPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadReport();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (chkOriginal.Checked)
            if (ddluser.Text != "EstimateMEMO")
            {
                //CRDischargeSummaryReport.ReportSource = rprt;
                //PrinterSettings getprinterName = new PrinterSettings();
                //rprt.PrintOptions.PrinterName = getprinterName.PrinterName;
                //rprtDuplicate.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)System.Drawing.Printing.PaperKind.A5;
                //rprt.PrintToPrinter(1, false, 0, 0);
                //this.CRDischargeSummaryReport.RefreshReport();
                ExportReport();
            }
            else
            //if (chkDuplicate.Checked)
            {
                //CRDuplicate.ReportSource = rprtDuplicate;
                //PrinterSettings getprinterName1 = new PrinterSettings();
                //rprtDuplicate.PrintOptions.PrinterName = getprinterName1.PrinterName;
                //rprtDuplicate.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)System.Drawing.Printing.PaperKind.A5;
                //rprtDuplicate.PrintToPrinter(1, false, 0, 0);
                //this.CRDuplicate.RefreshReport();
                ExportReport1();
            }
        
    }

   
    private void ExportReport()
    {
        ExportOptions exopt = default(ExportOptions);
        DiskFileDestinationOptions dfdopt = new DiskFileDestinationOptions();
        string fname = "Billing.pdf";
        dfdopt.DiskFileName = Server.MapPath(fname);

        exopt = rprt.ExportOptions;
        exopt.ExportDestinationType = ExportDestinationType.DiskFile;

        //for PDF select PortableDocFormat for excel select ExportFormatType.Excel    
        exopt.ExportFormatType = ExportFormatType.PortableDocFormat;
        exopt.DestinationOptions = dfdopt;

        //finally export your report document    
        rprt.Export();

        //To open your PDF after save it from crystal report    

        string Path = Server.MapPath(fname);
        FileInfo file = new FileInfo(Path);
        Response.ClearContent();

        Response.AddHeader("Content-Disposition", "inline; filename=" + file.Name);
        Response.AddHeader("Content-Length", file.Length.ToString());
        Response.ContentType = "application/pdf";
        Response.TransmitFile(file.FullName);
        Response.Flush();
    }
    private void ExportReport1()
    {
        ExportOptions exopt = default(ExportOptions);
        DiskFileDestinationOptions dfdopt = new DiskFileDestinationOptions();
        string fname = "Billing.pdf";
        dfdopt.DiskFileName = Server.MapPath(fname);

        exopt = rprtDuplicate.ExportOptions;
        exopt.ExportDestinationType = ExportDestinationType.DiskFile;

        //for PDF select PortableDocFormat for excel select ExportFormatType.Excel    
        exopt.ExportFormatType = ExportFormatType.PortableDocFormat;
        exopt.DestinationOptions = dfdopt;

        //finally export your report document    
        rprtDuplicate.Export();

        //To open your PDF after save it from crystal report    

        string Path = Server.MapPath(fname);
        FileInfo file = new FileInfo(Path);
        Response.ClearContent();

        Response.AddHeader("Content-Disposition", "inline; filename=" + file.Name);
        Response.AddHeader("Content-Length", file.Length.ToString());
        Response.ContentType = "application/pdf";
        Response.TransmitFile(file.FullName);
        Response.Flush();
    }
    public string AmountInWords(int numbers)
    {
        int number = numbers;

        if (number == 0) return "Zero";
        if (number == -2147483648) return "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight";
        int[] num = new int[4];
        int first = 0;
        int u, h, t;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        if (number < 0)
        {
            sb.Append("Minus ");
            number = -number;
        }
        string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
"Five " ,"Six ", "Seven ", "Eight ", "Nine "};
        string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
"Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
        string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
"Seventy ","Eighty ", "Ninety "};
        string[] words3 = { "Thousand ", "Lakh ", "Crore " };
        num[0] = number % 1000; // units
        num[1] = number / 1000;
        num[2] = number / 100000;
        num[1] = num[1] - 100 * num[2]; // thousands
        num[3] = number / 10000000; // crores
        num[2] = num[2] - 100 * num[3]; // lakhs
        for (int i = 3; i > 0; i--)
        {
            if (num[i] != 0)
            {
                first = i;
                break;
            }
        }
        for (int i = first; i >= 0; i--)
        {
            if (num[i] == 0) continue;
            u = num[i] % 10; // ones
            t = num[i] / 10;
            h = num[i] / 100; // hundreds
            t = t - 10 * h; // tens
            if (h > 0) sb.Append(words0[h] + "Hundred ");
            if (u > 0 || t > 0)
            {
                if (h > 0 || i == 0) sb.Append("and ");
                if (t == 0)
                    sb.Append(words0[u]);
                else if (t == 1)
                    sb.Append(words1[u]);
                else
                    sb.Append(words2[t - 2] + words0[u]);
            }
            if (i != 0) sb.Append(words3[i - 1]);
        }
        return sb.ToString().TrimEnd();
    }
}