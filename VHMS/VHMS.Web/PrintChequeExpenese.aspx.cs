using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.IO;
using System.Text;

public partial class PrintChequeExpenese : System.Web.UI.Page
{
    ReportDocument rprt = new ReportDocument();
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            if (HttpContext.Current.Session["PrintExpensePaymentID"] != null)
                hdnBillNo.Value = HttpContext.Current.Session["PrintExpensePaymentID"].ToString();
            else
                hdnBillNo.Value = "-1";
        }
        LoadReport();
          ExportReport();
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

    private void LoadReport()
    {
        string sTemp = string.Empty;
        try
        {
            ReportDataSet dsReportData = new ReportDataSet();
            dsReportData = LoadData();
            rprt = new ReportDocument();
            rprt.Load(Server.MapPath("~/Reports/rptChequeExpenese.rpt"));
            rprt.SetDataSource(dsReportData);
            this.CRDischargeSummaryReport.ReportSource = rprt;
            CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            CRDischargeSummaryReport.Zoom(100);
            this.CRDischargeSummaryReport.DataBind();
        }
        catch (Exception ex)
        {
            sException = "ReportPage_RepCheckPrint | " + ex.ToString();
            Log.Write(sException);
        }
    }

    private ReportDataSet LoadData()
    {

        dsData = VHMS.DataAccess.VHMSReports.PrintExpenese(Convert.ToInt32(hdnBillNo.Value));
        ReportDataSet dsReportData = new ReportDataSet();
        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "tSupplier";
            foreach (DataRow drow in dsData.Tables[0].Rows) dsReportData.tSupplier.ImportRow(drow);
            dsData.Tables[0].TableName = "tExpensePayment";
            foreach (DataRow drow in dsData.Tables[0].Rows) dsReportData.tExpensePayment.ImportRow(drow);
            dsData.Tables[1].TableName = "tLedger";
            foreach (DataRow drow in dsData.Tables[1].Rows) dsReportData.tLedger.ImportRow(drow);
        }
        return dsReportData;

    }

    private void ExportReport()
    {
        ExportOptions exopt = default(ExportOptions);
        DiskFileDestinationOptions dfdopt = new DiskFileDestinationOptions();
        string fname = "Payment.pdf";
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
}