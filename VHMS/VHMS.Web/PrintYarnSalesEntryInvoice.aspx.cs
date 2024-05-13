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

public partial class PrintYarnSalesEntryInvoice : System.Web.UI.Page
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
            if (HttpContext.Current.Session["YarnSalesEntryID"] != null)
                hdnBillNo.Value = HttpContext.Current.Session["YarnSalesEntryID"].ToString();
            else
                hdnBillNo.Value = "-1";
        }
        LoadReport();
        btnPrint_Click(sender, e);
    }
    private void Databind()
    {
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        decimal DiscountAmount = 0;

        dsData = VHMS.DataAccess.VHMSReports.PrintYarnSalesEntryInvoice(Convert.ToInt32(hdnBillNo.Value));
        ReportDataSet dsReportData = new ReportDataSet();
        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "tYarnSalesEntryTrans";
            foreach (DataRow drow in dsData.Tables[0].Rows)
                dsReportData.tYarnSalesEntryTrans.ImportRow(drow);
        }
        if (dsReportData.tYarnSalesEntryTrans.Rows.Count > 0)
        {

            foreach (ReportDataSet.tYarnSalesEntryTransRow drtBranchRow in dsReportData.tYarnSalesEntryTrans)
            {
                DiscountAmount = DiscountAmount + drtBranchRow.DiscountAmount;
            }
        }
        try
        {
            dsReportData = LoadData();
            rprt = new ReportDocument();
            if (DiscountAmount > 0)
                rprt.Load(Server.MapPath("~/Reports/rptYarnSalesEntryReport.rpt"));
            else
                rprt.Load(Server.MapPath("~/Reports/rptYarnSalesEntryReportWithoutDiscountAmt.rpt"));
            rprt.SetDataSource(dsReportData);
            this.CRDischargeSummaryReport.ReportSource = rprt;
            CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            CRDischargeSummaryReport.Zoom(100);
            this.CRDischargeSummaryReport.DataBind();

            //rprtDuplicate = new ReportDocument();
            //if (DiscountAmount > 0)
            //    rprtDuplicate.Load(Server.MapPath("~/Reports/rptYarnSalesEntryDuplicateReport.rpt"));
            //else
            //    rprtDuplicate.Load(Server.MapPath("~/Reports/rptYarnSalesEntryDuplicateWithoutDiscountamtReport.rpt"));
            //rprtDuplicate.SetDataSource(dsReportData);
            //this.CRDuplicate.ReportSource = rprtDuplicate;
            //CRDuplicate.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            //CRDuplicate.Zoom(100);
            //this.CRDuplicate.DataBind();

            //rprtTransport = new ReportDocument();
            //rprtTransport.Load(Server.MapPath("~/Reports/rptYarnSalesEntryTransportReport.rpt"));
            //rprtTransport.SetDataSource(dsReportData);
            //this.CRTransport.ReportSource = rprtTransport;
            //CRTransport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            //CRTransport.Zoom(100);
            //this.CRTransport.DataBind();
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
        string fname = "YarnInvoice.pdf";
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

        dsData = VHMS.DataAccess.VHMSReports.PrintYarnSalesEntryInvoice(Convert.ToInt32(hdnBillNo.Value));
        ReportDataSet dsReportData = new ReportDataSet();
        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "tYarnSalesEntryTrans";
            foreach (DataRow drow in dsData.Tables[0].Rows)
                dsReportData.tYarnSalesEntryTrans.ImportRow(drow);
        }
        if (dsReportData.tYarnSalesEntryTrans.Rows.Count > 0)
        {

            foreach (ReportDataSet.tYarnSalesEntryTransRow drtBranchRow in dsReportData.tYarnSalesEntryTrans)
            {
                DiscountAmount = DiscountAmount + drtBranchRow.DiscountAmount;
            }
        }
        try
        {

            dsReportData = LoadData();
            rprt = new ReportDocument();
            if (DiscountAmount > 0)
                rprt.Load(Server.MapPath("~/Reports/rptYarnSalesEntryReport.rpt"));
            else
                rprt.Load(Server.MapPath("~/Reports/rptYarnSalesEntryReportWithoutDiscountAmt.rpt"));
            rprt.SetDataSource(dsReportData);
            this.CRDischargeSummaryReport.ReportSource = rprt;
            CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            CRDischargeSummaryReport.Zoom(100);
            this.CRDischargeSummaryReport.DataBind();

            //rprtDuplicate = new ReportDocument();
            //if (DiscountAmount > 0)
            //    rprtDuplicate.Load(Server.MapPath("~/Reports/rptYarnSalesEntryDuplicateReport.rpt"));
            //else
            //    rprtDuplicate.Load(Server.MapPath("~/Reports/rptYarnSalesEntryDuplicateWithoutDiscountamtReport.rpt"));
            //rprtDuplicate.SetDataSource(dsReportData);
            //this.CRDuplicate.ReportSource = rprtDuplicate;
            //CRDuplicate.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            //CRDuplicate.Zoom(100);
            //this.CRDuplicate.DataBind();

            //rprtTransport = new ReportDocument();
            //rprtTransport.Load(Server.MapPath("~/Reports/rptYarnSalesEntryTransportReport.rpt"));
            //rprtTransport.SetDataSource(dsReportData);
            //this.CRTransport.ReportSource = rprtTransport;
            //CRTransport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            //CRTransport.Zoom(100);
            //this.CRTransport.DataBind();
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
        //if ( rprtDuplicate != null)
        //{
        //    rprtDuplicate.Dispose();
        //    rprtDuplicate.Clone();
        //    rprtDuplicate.Close();
        //    CRDuplicate.Dispose();
        //}
        //if (rprtTransport != null)
        //{
        //    rprtTransport.Dispose();
        //    rprtTransport.Clone();
        //    rprtTransport.Close();
        //    CRTransport.Dispose();
        //}
    }
    private ReportDataSet LoadData()
    {
        dsData = VHMS.DataAccess.VHMSReports.PrintYarnSalesEntryInvoice(Convert.ToInt32(hdnBillNo.Value));

        ReportDataSet dsReportData = new ReportDataSet();

        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "tYarnSalesEntry";
            foreach (DataRow drow in dsData.Tables[0].Rows) dsReportData.tYarnSalesEntry.ImportRow(drow);
            dsData.Tables[1].TableName = "tYarnSalesEntryTrans";
            foreach (DataRow drow in dsData.Tables[1].Rows) dsReportData.tYarnSalesEntryTrans.ImportRow(drow);
            dsData.Tables[2].TableName = "tCustomer";
            foreach (DataRow drow in dsData.Tables[2].Rows) dsReportData.tCustomer.ImportRow(drow);
            dsData.Tables[3].TableName = "tCompany";
            foreach (DataRow drow in dsData.Tables[3].Rows) dsReportData.tCompany.ImportRow(drow);
        }
        return dsReportData;
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ExportReport();
        //if (chkOriginal.Checked)
        //{
        //    CRDischargeSummaryReport.ReportSource = rprt;
        //    PrinterSettings getprinterName = new PrinterSettings();
        //    rprt.PrintOptions.PrinterName = getprinterName.PrinterName;
        //    rprt.PrintToPrinter(1, false, 0, 0);
        //    this.CRDischargeSummaryReport.RefreshReport();
        //}
        //if (chkDuplicate.Checked)
        //{
        //    CRDuplicate.ReportSource = rprtDuplicate;
        //    PrinterSettings getprinterName1 = new PrinterSettings();
        //    rprtDuplicate.PrintOptions.PrinterName = getprinterName1.PrinterName;
        //    rprtDuplicate.PrintToPrinter(1, false, 0, 0);
        //    this.CRDuplicate.RefreshReport();
        //}
        //if (chkTransport.Checked)
        //{
        //    CRTransport.ReportSource = rprtTransport;
        //    PrinterSettings getprinterName2 = new PrinterSettings();
        //    rprtTransport.PrintOptions.PrinterName = getprinterName2.PrinterName;
        //    rprtTransport.PrintToPrinter(1, false, 0, 0);
        //    this.CRTransport.RefreshReport();
        //}

    }
}