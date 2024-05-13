using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Text;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Drawing.Printing;
using VHMS.DataAccess;

public partial class PrintLetterPad : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    ReportDocument rprt = new ReportDocument();
    ReportDocument rprtDuplicate = new ReportDocument();
    ReportDocument rprtTransport = new ReportDocument();
    string sException = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            if (HttpContext.Current.Session["LetterPadID"] != null)
                hdnBillNo.Value = HttpContext.Current.Session["LetterPadID"].ToString();
            else
                hdnBillNo.Value = "-1";
        }
        LoadReport();
        // btnPrint_Click(sender, e);
    }
    protected void chkPartyCode_CheckedChanged(object sender, EventArgs e)
    {
        LoadReport();
    }
    private void Databind()
    {
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        decimal DiscountAmount = 0;

        dsData = VHMS.DataAccess.VHMSReports.PrintLetterPad(Convert.ToInt32(hdnBillNo.Value));
        ReportDataSet dsReportData = new ReportDataSet();
       
        try
        {
            dsReportData = LoadData();
            rprt = new ReportDocument();
            rprt.Load(Server.MapPath("~/Reports/rptLetterPadReport.rpt"));
            rprt.SetDataSource(dsReportData);
            this.CRDischargeSummaryReport.ReportSource = rprt;
            CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            CRDischargeSummaryReport.Zoom(100);
            this.CRDischargeSummaryReport.DataBind();
        }
        catch (Exception ex)
        {
            sException = "frmPrintJobCard | " + ex.ToString();
            Log.Write(sException);
        }
    }
    private void ExportReport()
    {
        ExportOptions exopt = default(ExportOptions);
        DiskFileDestinationOptions dfdopt = new DiskFileDestinationOptions();
        string fname = "Invoice.pdf";
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

    protected void btnExportReport_Click(object sender, EventArgs e)
    {
        try
        {
            ExportReport();
        }
        catch (Exception ex)
        { Log.Write("RepViewDischargeSummary btnExportReport_Click | " + ex.ToString()); }
    }

    private void LoadReport()
    {

        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        decimal DiscountAmount = 0;

        dsData = VHMS.DataAccess.VHMSReports.PrintLetterPad(Convert.ToInt32(hdnBillNo.Value));
        ReportDataSet dsReportData = new ReportDataSet();
        try
        {
            dsReportData = LoadData();
            rprt = new ReportDocument();
            rprt.Load(Server.MapPath("~/Reports/rptLetterPadReport.rpt"));
            rprt.SetDataSource(dsReportData);
            this.CRDischargeSummaryReport.ReportSource = rprt;
            CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            CRDischargeSummaryReport.Zoom(100);
            this.CRDischargeSummaryReport.DataBind();
        }
        catch (Exception ex)
        {
            sException = "frmPrintJobCard | " + ex.ToString();
            Log.Write(sException);
            string script1 = "alert(\"" + sException + "!\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script1, true);
        }
    }
    protected void Page_UnLoad()
    {
        if (rprt != null)
        {
            rprt.Dispose();
            rprt.Clone();
            rprt.Close();
            CRDischargeSummaryReport.Dispose();
        }
    }
    private ReportDataSet LoadData()
    {
        dsData = VHMS.DataAccess.VHMSReports.PrintLetterPad(Convert.ToInt32(hdnBillNo.Value));

        ReportDataSet dsReportData = new ReportDataSet();

        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "tLetterPad";
            foreach (DataRow drow in dsData.Tables[0].Rows) dsReportData.tLetterPad.ImportRow(drow);
            dsData.Tables[1].TableName = "tCompany";
            foreach (DataRow drow in dsData.Tables[1].Rows) dsReportData.tCompany.ImportRow(drow);
        }
        return dsReportData;
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        CRDischargeSummaryReport.ReportSource = rprt;
        PrinterSettings getprinterName = new PrinterSettings();
        rprt.PrintOptions.PrinterName = getprinterName.PrinterName;
        rprt.PrintToPrinter(1, false, 0, 0);
        this.CRDischargeSummaryReport.RefreshReport();
    }
}