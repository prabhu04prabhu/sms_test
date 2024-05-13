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

public partial class frmExpensePendingReport : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty; 
    ReportDocument rprt = new ReportDocument();
    decimal InvoiceAmount = 0, NetAmount = 0,DiscountAmount=0,TotalQty=0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnExportReport);
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            LoadCustomer();
        }
        LoadReport();
    }
 
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rprt.Close();
        rprt.Dispose();
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
            ReportDatestNew dsReportData = new ReportDatestNew();
            dsReportData = LoadData();
            rprt = new ReportDocument();
            rprt.Load(Server.MapPath("~/Reports/rptExpensePendingReport.rpt"));
            rprt.SetDataSource(dsReportData);
            this.CRDischargeSummaryReport.ReportSource = rprt;
            CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            CRDischargeSummaryReport.Zoom(100);
            this.CRDischargeSummaryReport.DataBind();
            //if (dsData.Tables.Count > 0)
            //{
            //    dsData.Tables[0].TableName = "tSalesEntry";
            //    foreach (DataRow drow in dsData.Tables[0].Rows)
            //        dsReportData.tSalesEntry.ImportRow(drow);

            //    gvSales.DataSource = dsData.Tables[0];
            //    gvSales.DataBind();
            //}

        }
        catch (Exception ex)
        {
            sException = "frmPrintSales | " + ex.ToString();
            Log.Write(sException);
        }
    }
    private ReportDatestNew LoadData()
    {
        // if (!chkPartyCode.Checked)
        //  dsData = VHMS.DataAccess.VHMSReports.MarginReport(Convert.ToInt32(ddlSupplier.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlSubcategory.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(ddlCustomer.SelectedValue), txtDOB.Text, txtDOR.Text, ddlType.SelectedValue);
        //else
        dsData = VHMS.DataAccess.VHMSReports.PrintExpensePending(ddlCustomer.SelectedValue);
        ReportDatestNew dsReportData = new ReportDatestNew();

        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "tExpense";
            foreach (DataRow drow in dsData.Tables[0].Rows) dsReportData.tExpense.ImportRow(drow);
            dsData.Tables[1].TableName = "tCompany";
            foreach (DataRow drow in dsData.Tables[1].Rows) dsReportData.tCompany.ImportRow(drow);
        }
        return dsReportData;
    }
    public void LoadCustomer()
    {
        Collection<VHMS.Entity.Ledger> ObjList = new Collection<VHMS.Entity.Ledger>();
        ObjList = VHMS.DataAccess.Ledger.GetLedger();
        ddlCustomer.DataSource = ObjList;
        ddlCustomer.DataTextField = "LedgerName";
        ddlCustomer.DataValueField = "LedgerID";
        ddlCustomer.DataBind();
        ddlCustomer.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    NetAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ReturnAmount"));
        //    InvoiceAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetAmount"));
        //    DiscountAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PaidAmount"));
        //    TotalQty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BalanceAmount")); 
        //}
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    e.Row.Cells[0].ColumnSpan = 5;
        //    e.Row.Cells.RemoveAt(1);
        //    e.Row.Cells.RemoveAt(2);
        //    e.Row.Cells.RemoveAt(3);
        //    e.Row.Cells.RemoveAt(4);
        //    e.Row.Cells[0].Text = "Total Amount :";
        //    e.Row.Cells[4].Text = Convert.ToDecimal(TotalQty).ToString();
        //    e.Row.Cells[3].Text = Convert.ToDecimal(DiscountAmount).ToString();
        //    e.Row.Cells[2].Text = Convert.ToDecimal(NetAmount).ToString();
        //    e.Row.Cells[1].Text = Convert.ToDecimal(InvoiceAmount).ToString();
        //    e.Row.Cells[1].Font.Bold = true;
        //    e.Row.Cells[2].Font.Bold = true;
        //    e.Row.Cells[3].Font.Bold = true;
        //    e.Row.Cells[4].Font.Bold = true;
        //    e.Row.Cells[0].Font.Bold = true;
        //    e.Row.Cells[1].Font.Size = 14;
        //    e.Row.Cells[2].Font.Size = 14;
        //    e.Row.Cells[3].Font.Size = 14;
        //    e.Row.Cells[4].Font.Size = 14;
        //    e.Row.Cells[0].Font.Size = 14;
        //}
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
    private void ExportReport()
    {
        ExportOptions exopt = default(ExportOptions);
        DiskFileDestinationOptions dfdopt = new DiskFileDestinationOptions();
        string fname = "PurchasePendingReport.pdf";
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
        //Response.Close();
        //Response.Redirect("frmSalesPendingReport.aspx");
        //Response.Headers.Clear();
    }
}

