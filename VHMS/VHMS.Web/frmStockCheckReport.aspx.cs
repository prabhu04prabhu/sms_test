using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Data.SqlClient;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using System.Globalization;

public partial class frmStockCheckReport : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    ReportDocument rprt = new ReportDocument();
    ReportDocument rprt1 = new ReportDocument();
    string sException = string.Empty;
    decimal InvoiceAmount = 0, NetAmount = 0, DiscountAmount = 0, TotalQty = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnExportReport);
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            txtDOB.Text = fromDate(Convert.ToInt32(Session["FK_FinancialYearID"]));

            if (FinancialYearStatus(Convert.ToInt32(Session["FK_FinancialYearID"])) == "Closed")
                txtDOR.Text = TODate(Convert.ToInt32(Session["FK_FinancialYearID"]));
            else
                txtDOR.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        LoadReport();
    }

    private static string fromDate(Int32 firstName)
    {
        VHMS.Entity.FinancialYear objQuotation = new VHMS.Entity.FinancialYear();
        objQuotation = VHMS.DataAccess.FinancialYear.GetFinancialYearByID(firstName);
        string result = objQuotation.sYearFrom;
        return result;
    }
    private static string FinancialYearStatus(Int32 firstName)
    {
        VHMS.Entity.FinancialYear objQuotation = new VHMS.Entity.FinancialYear();
        objQuotation = VHMS.DataAccess.FinancialYear.GetFinancialYearByID(firstName);
        string result = objQuotation.Status;
        return result;
    }
    private static string TODate(Int32 firstName)
    {
        VHMS.Entity.FinancialYear objQuotation = new VHMS.Entity.FinancialYear();
        objQuotation = VHMS.DataAccess.FinancialYear.GetFinancialYearByID(firstName);
        string result = objQuotation.sYearTo;
        return result;
    }
    protected void ddlPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rprt.Close();
        rprt.Dispose();
        rprt1.Close();
        rprt1.Dispose();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadReport();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    private void LoadReport()
    {
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        try
        {
            if (ddluser.Text == "StockCheck")
            {
                ReportDatestNew dsReportData = new ReportDatestNew();
                dsReportData = LoadData();
                rprt = new ReportDocument();
                rprt.Load(Server.MapPath("~/Reports/rptStockCheckReport.rpt"));
                rprt.SetDataSource(dsReportData);
                this.CRStockCheckReport.ReportSource = rprt;
                CRStockCheckReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
                CRStockCheckReport.Zoom(100);
                this.CRStockCheckReport.DataBind();
            }
            else
            {
                ReportDatestNew dsReportData1 = new ReportDatestNew();
                dsReportData1 = LoadData1();
                rprt1 = new ReportDocument();
                rprt1.Load(Server.MapPath("~/Reports/rptStockCheckDetailedReport.rpt"));
                rprt1.SetDataSource(dsReportData1);
                this.CRStockCheckReport1.ReportSource = rprt1;
                CRStockCheckReport1.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
                CRStockCheckReport1.Zoom(100);
                this.CRStockCheckReport1.DataBind();
            }
        }
        catch (Exception ex)
        {
            sException = "frmPrintSales | " + ex.ToString();
            Log.Write(sException);
        }
    }
    protected void btnExportReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddluser.Text == "StockCheck")
                ExportReport();
            else
                ExportReport1();
        }
        catch (Exception ex)
        { Log.Write("RepViewDischargeSummary btnExportReport_Click | " + ex.ToString()); }
    }

    private ReportDatestNew LoadData()
    {
        dsData = VHMS.DataAccess.VHMSReports.PrintStockCheck(txtDOB.Text, txtDOR.Text, 0, ddluser.Text);
        ReportDatestNew dsReportData = new ReportDatestNew();

        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "tStockCheckTrans";
            foreach (DataRow drow in dsData.Tables[0].Rows) dsReportData.tStockCheckTrans.ImportRow(drow);
            dsData.Tables[1].TableName = "tCompany";
            foreach (DataRow drow in dsData.Tables[1].Rows) dsReportData.tCompany.ImportRow(drow);
        }
        return dsReportData;
    }

    private ReportDatestNew LoadData1()
    {
        dsData = VHMS.DataAccess.VHMSReports.PrintStockCheck(txtDOB.Text, txtDOR.Text, 0, ddluser.Text);
        ReportDatestNew dsReportData1 = new ReportDatestNew();

        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "tStock";
            foreach (DataRow drow in dsData.Tables[0].Rows) dsReportData1.tStock.ImportRow(drow);
            dsData.Tables[1].TableName = "tCompany";
            foreach (DataRow drow in dsData.Tables[1].Rows) dsReportData1.tCompany.ImportRow(drow);
        }
        return dsReportData1;
    }

    private void ExportReport()
    {
        ExportOptions exopt = default(ExportOptions);
        DiskFileDestinationOptions dfdopt = new DiskFileDestinationOptions();
        string fname = "StockCheck Report.pdf";
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
        string fname = "StockCheck Report.pdf";
        dfdopt.DiskFileName = Server.MapPath(fname);

        exopt = rprt1.ExportOptions;
        exopt.ExportDestinationType = ExportDestinationType.DiskFile;

        //for PDF select PortableDocFormat for excel select ExportFormatType.Excel    
        exopt.ExportFormatType = ExportFormatType.PortableDocFormat;
        exopt.DestinationOptions = dfdopt;

        //finally export your report document    
        rprt1.Export();

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
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddluser.Text == "StockCheck")
            {
                ReportDatestNew dsReportData = new ReportDatestNew();
                dsReportData = LoadData();
                rprt = new ReportDocument();
                rprt.Load(Server.MapPath("~/Reports/rptStockCheckReport.rpt"));
                rprt.SetDataSource(dsReportData);
                this.CRStockCheckReport.ReportSource = rprt;
                CRStockCheckReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
                CRStockCheckReport.Zoom(100);
                this.CRStockCheckReport.DataBind();
                rprt.ExportToHttpResponse
                (CrystalDecisions.Shared.ExportFormatType.Excel, Response, true, "StockCheck Report");
            }
            else
            {
                ReportDatestNew dsReportData1 = new ReportDatestNew();
                dsReportData1 = LoadData1();
                rprt1 = new ReportDocument();
                rprt1.Load(Server.MapPath("~/Reports/rptStockCheckDetailedReport.rpt"));
                rprt1.SetDataSource(dsReportData1);
                this.CRStockCheckReport.ReportSource = rprt1;
                CRStockCheckReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
                CRStockCheckReport.Zoom(100);
                this.CRStockCheckReport.DataBind();
                rprt1.ExportToHttpResponse
                (CrystalDecisions.Shared.ExportFormatType.Excel, Response, true, "StockCheck Report");
            }

        }
        catch (Exception ex)
        {
            ex.ToString();
            //return View();
        }
    }
}

