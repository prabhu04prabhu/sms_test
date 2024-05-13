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

public partial class RepSalesTaxReturnReport : System.Web.UI.Page
{
    ReportDocument rprt = new ReportDocument();
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
       ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnExportReport);

        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
        }
        txtDOR.Text = Convert.ToDateTime(DateTime.Now).ToString("MMMM-yyyy");
      LoadReport();
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rprt.Close();
        rprt.Dispose();
    }
    protected void btnSearchReport_Click(object sender, EventArgs e)
    { LoadReport(); }
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
        string sTemp = string.Empty;
        try
        {
            ReportDataSet dsReportData = new ReportDataSet();
            dsReportData = LoadData();
            rprt = new ReportDocument();
            rprt.Load(Server.MapPath("~/Reports/rptSalesTaxReturn.rpt"));

            rprt.SetDataSource(dsReportData);
            this.CRDischargeSummaryReport.ReportSource = rprt;
            CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            CRDischargeSummaryReport.Zoom(100);
            this.CRDischargeSummaryReport.DataBind();
        }
        catch (Exception ex)
        {
            sException = "ReportPage_RepSalesTaxReturn | " + ex.ToString();
            Log.Write(sException);
        }
    }
    private ReportDataSet LoadData()
    {
        dsData = VHMS.DataAccess.VHMSReports.SalesTaxReturnReport(Convert.ToDateTime(txtDOR.Text).Month, Convert.ToDateTime(txtDOR.Text).Year);
        ReportDataSet dsReportData = new ReportDataSet();

        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "VPurchase";
            foreach (DataRow drow in dsData.Tables[0].Rows) dsReportData.VPurchase.ImportRow(drow);
            dsData.Tables[1].TableName = "VPurchaseReturn";
            foreach (DataRow drow in dsData.Tables[1].Rows) dsReportData.VPurchaseReturn.ImportRow(drow);
            dsData.Tables[2].TableName = "VSalesEntry";
            foreach (DataRow drow in dsData.Tables[2].Rows) dsReportData.VSalesEntry.ImportRow(drow);
            dsData.Tables[3].TableName = "VSalesReturn";
            foreach (DataRow drow in dsData.Tables[3].Rows) dsReportData.VSalesReturn.ImportRow(drow);
            dsData.Tables[4].TableName = "tExpense";
            foreach (DataRow drow in dsData.Tables[4].Rows) dsReportData.tExpense.ImportRow(drow);
            dsData.Tables[5].TableName = "tCompany";
            foreach (DataRow drow in dsData.Tables[5].Rows) dsReportData.tCompany.ImportRow(drow);
        }
        return dsReportData;
    }
    private void ExportReport()
    {
        ExportOptions exopt = default(ExportOptions);
        DiskFileDestinationOptions dfdopt = new DiskFileDestinationOptions();
        string fname = "SalesTaxReturn.pdf";
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