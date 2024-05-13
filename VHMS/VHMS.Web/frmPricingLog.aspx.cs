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
using CrystalDecisions.CrystalReports.Engine;
using System.Globalization;
using CrystalDecisions.Shared;

public partial class frmPricingLog : System.Web.UI.Page
{
    ReportDocument rprt = new ReportDocument();
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            LoadVendor(); 
            LoadSupplier();
            //LoadBranch();
            txtDOB.Text = fromDate(Convert.ToInt32(Session["FK_FinancialYearID"]));

            if (FinancialYearStatus(Convert.ToInt32(Session["FK_FinancialYearID"])) == "Closed")
                txtDOR.Text = TODate(Convert.ToInt32(Session["FK_FinancialYearID"]));
            else
                txtDOR.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

        }
        //LoadReport();
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
    //protected void btnExport1_Click(object sender, EventArgs e)
    //{

    //    ReportDataSet dsReportData = new ReportDataSet();

    //    dsData = VHMS.DataAccess.VHMSReports.PrintPricingLogReport(txtDOB.Text, txtDOR.Text, Convert.ToInt32(ddlVendor.SelectedValue), Convert.ToInt32(ddlSupplier.SelectedValue));
    //    try
    //    {
    //        if (dsData.Tables.Count > 0)
    //        {
    //            dsData.Tables[0].TableName = "tPricingLog";
    //            foreach (DataRow drow in dsData.Tables[0].Rows)
    //                dsReportData.tPricingLog.ImportRow(drow);

    //            dsData.Tables[1].TableName = "tCompany";
    //            foreach (DataRow drow in dsData.Tables[1].Rows)
    //                dsReportData.tCompany.ImportRow(drow);
    //        }
    //        //rprt = new ReportDocument();
    //        //rprt.Load(Server.MapPath("~/Reports/Rpt_pricingLog.rpt"));
    //        //rprt.SetDataSource(dsReportData);
    //        //this.CRDischargeSummaryReport.ReportSource = rprt;
    //        //CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
    //        //CRDischargeSummaryReport.Zoom(100);
    //        //this.CRDischargeSummaryReport.DataBind();
    //        //rprt.ExportToHttpResponse
    //        //(CrystalDecisions.Shared.ExportFormatType.Excel, Response, true, "PriceUpdateLogReport");

    //    }
    //    catch
    //    {
    //        throw;
    //        //return View();
    //    }
    //}
    public void LoadVendor()
    {
        Collection<VHMS.Entity.Master.Product> ObjList = new Collection<VHMS.Entity.Master.Product>();
        ObjList = VHMS.DataAccess.Master.Product.GetProduct();

        ddlVendor.DataSource = ObjList;
        ddlVendor.DataTextField = "ProductName";
        ddlVendor.DataValueField = "ProductID";
        ddlVendor.DataBind();
        ddlVendor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rprt.Close();
        rprt.Dispose();
    }
    public void LoadSupplier()
    {
        Collection<VHMS.Entity.Billing.Supplier> ObjList = new Collection<VHMS.Entity.Billing.Supplier>();
        ObjList = VHMS.DataAccess.Billing.Supplier.GetSupplier();

        ddlSupplier.DataSource = ObjList;
        ddlSupplier.DataTextField = "SupplierName";
        ddlSupplier.DataValueField = "SupplierID";
        ddlSupplier.DataBind();
        ddlSupplier.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(ddlVendor.SelectedValue) > 0)
            LoadReport();
      //  else
           // ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Select Vendor');", true);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ReportDataSet dsReportData = new ReportDataSet();

        dsData = VHMS.DataAccess.VHMSReports.PrintPricingLogReport(txtDOB.Text, txtDOR.Text, Convert.ToInt32(ddlVendor.SelectedValue), Convert.ToInt32(ddlSupplier.SelectedValue));
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tPricingLog";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tPricingLog.ImportRow(drow);

                dsData.Tables[1].TableName = "tCompany";
                foreach (DataRow drow in dsData.Tables[1].Rows)
                    dsReportData.tCompany.ImportRow(drow);
            }
            rprt = new ReportDocument();
            rprt.Load(Server.MapPath("~/Reports/Rpt_pricingLog.rpt"));
            rprt.SetDataSource(dsReportData);
            this.CRDischargeSummaryReport.ReportSource = rprt;
            CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            CRDischargeSummaryReport.Zoom(100);
            this.CRDischargeSummaryReport.DataBind();
            rprt.ExportToHttpResponse
            (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "PriceUpdateLogReport");
        }

        catch (Exception ex)
        {
            sException = "frmPrintJobCard | " + ex.ToString();
            Log.Write(sException);
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvPurchase.Rows.Count < 1)
        {
            string display = "no Records Found.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
        }
        else
        {
            try
            {
                this.gvPurchase.AllowPaging = false;
                this.gvPurchase.AllowSorting = false;
                this.gvPurchase.EditIndex = -1;

                Response.Clear();
                Response.ContentType = "application/vnd.xls";
                Response.AddHeader("content-disposition", "attachment;filename=PricingLogReports.xls");
                Response.Charset = "";
                StringWriter swriter = new StringWriter();
                HtmlTextWriter hwriter = new HtmlTextWriter(swriter);
                gvPurchase.RenderControl(hwriter);
                Response.Write(swriter.ToString());
                Response.End();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    private void LoadReport()
    {
        ReportDataSet dsReportData = new ReportDataSet();

        dsData = VHMS.DataAccess.VHMSReports.PrintPricingLogReport(txtDOB.Text, txtDOR.Text, Convert.ToInt32(ddlVendor.SelectedValue),Convert.ToInt32(ddlSupplier.SelectedValue));
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tPricingLog";
                //foreach (DataRow drow in dsData.Tables[0].Rows)
                //    dsReportData.tPricingLog.ImportRow(drow);

                //dsData.Tables[1].TableName = "tCompany";
                //foreach (DataRow drow in dsData.Tables[1].Rows)
                //    dsReportData.tCompany.ImportRow(drow);
                gvPurchase.DataSource = dsData.Tables[0];
                gvPurchase.DataBind();
            }
            //rprt = new ReportDocument();
            //rprt.Load(Server.MapPath("~/Reports/Rpt_pricingLog.rpt"));
            //rprt.SetDataSource(dsReportData);
            //this.CRDischargeSummaryReport.ReportSource = rprt;
            //CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            //CRDischargeSummaryReport.Zoom(100);
            //this.CRDischargeSummaryReport.DataBind();
        }

        catch (Exception ex)
        {
            sException = "frmPrintJobCard | " + ex.ToString();
            Log.Write(sException);
        }
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        if (gvPurchase.Rows.Count < 1)
        {
            string display = "no Records Found.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
        }
        else
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=PricingLogReports.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvPurchase.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
            gvPurchase.AllowPaging = true;
            gvPurchase.DataBind();
        }
    }

    // int totalItems = 0;
    //  decimal percent = 0M;
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    InvoiceAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "InvoiceAmount"));
        //}
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    (e.Row.FindControl("lblTotalAmount") as Label).Text = InvoiceAmount.ToString();
        //}

    }

    //public void LoadBranch()
    //{
    //    Collection<VHMS.Entity.Branch> ObjList = new Collection<VHMS.Entity.Branch>();
    //    ObjList = VHMS.DataAccess.Branch.GetBranch();

    //    ddlBranch.DataSource = ObjList;
    //    ddlBranch.DataTextField = "BranchName";
    //    ddlBranch.DataValueField = "BranchID";
    //    ddlBranch.DataBind();
    //    ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    //}
    protected void tbAccount_TextChanged(object sender, EventArgs e)
    {
        if (txtCode.Text != "")
        {
            Collection<VHMS.Entity.Master.Product> ObjList = new Collection<VHMS.Entity.Master.Product>();
            ObjList = VHMS.DataAccess.Master.Product.GetProductByCodeByID(txtCode.Text, false);

            ddlVendor.DataSource = ObjList;
            ddlVendor.DataTextField = "ProductName";
            ddlVendor.DataValueField = "ProductID";
            ddlVendor.DataBind();
        }
        else
        {
            LoadVendor();
        }
        //GVSummary.Visible = false;
    }
}
