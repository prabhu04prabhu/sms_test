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

public partial class frmSalesBillLegder : System.Web.UI.Page
{
    ReportDocument rprt = new ReportDocument();
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    string GpayAmount = "", NetAmount="", TDSAmount="", DiscAmount = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            LoadSupplier();
            txtDOB.Text = fromDate(Convert.ToInt32(Session["FK_FinancialYearID"]));

            if (FinancialYearStatus(Convert.ToInt32(Session["FK_FinancialYearID"])) == "Closed")
                txtDOR.Text = TODate(Convert.ToInt32(Session["FK_FinancialYearID"]));
            else
                txtDOR.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

        }
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
    public void LoadSupplier()
    {
        Collection<VHMS.Entity.Customer> ObjList = new Collection<VHMS.Entity.Customer>();
        ObjList = VHMS.DataAccess.Customer.GetActiveCustomer();

        ddlSupplier.DataSource = ObjList;
        ddlSupplier.DataTextField = "CustomerName";
        ddlSupplier.DataValueField = "CustomerID";
        ddlSupplier.DataBind();
        ddlSupplier.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
            try
            {
                this.gvSales.AllowPaging = false;
                this.gvSales.AllowSorting = false;
                this.gvSales.EditIndex = -1;

                Response.Clear();
                Response.ContentType = "application/vnd.xls";
                Response.AddHeader("content-disposition", "attachment;filename=SalesBillLedger.xls");
                Response.Charset = "";
                StringWriter swriter = new StringWriter();
                HtmlTextWriter hwriter = new HtmlTextWriter(swriter);
                gvSales.RenderControl(hwriter);
                Response.Write(swriter.ToString());
                Response.End();
            }
            catch (Exception exe)
            {
                throw exe;
            }
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        LoadReport();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=SalesBillLedger.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvSales.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
            gvSales.AllowPaging = true;
            gvSales.DataBind();
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
        ReportDatestNew dsReportData = new ReportDatestNew();
        dsData = VHMS.DataAccess.VHMSReports.PrintSalesBillLedgerReport(txtDOB.Text, txtDOR.Text, ddlSupplier.SelectedValue);
        try
        {
            if (dsData.Tables.Count > 0)
            {               
                gvSales.DataSource = dsData.Tables[0];
                gvSales.DataBind();
            }
        }
        catch (Exception ex)
        {
            sException = "frmPrintJobCard | " + ex.ToString();
            Log.Write(sException);
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GpayAmount += DataBinder.Eval(e.Row.DataItem, "TotalPaidAmount");
            NetAmount += DataBinder.Eval(e.Row.DataItem, "TotalReturnAmount");
            TDSAmount += DataBinder.Eval(e.Row.DataItem, "TDSPaidAmount");
            DiscAmount += DataBinder.Eval(e.Row.DataItem, "TotalSalesDiscAmount");

            Label lbl = (Label)e.Row.FindControl("lblBillCount");
            lbl.Text = GpayAmount.Replace(",", "<br>");
            GpayAmount = "";

            Label lbl1 = (Label)e.Row.FindControl("lblTotalReturnCount");
            lbl1.Text = NetAmount.Replace(",", "<br>");
            NetAmount = "";

            Label lbl2 = (Label)e.Row.FindControl("lblTotalTDSCount");
            lbl2.Text = TDSAmount.Replace(",", "<br>");
            TDSAmount = "";

            Label lbl3 = (Label)e.Row.FindControl("lblTotalPurDiscAmount");
            lbl3.Text = DiscAmount.Replace(",", "<br>");
            DiscAmount = "";
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("lblBillCount");
            lbl.Text = GpayAmount.Replace(",", "<br>");

            Label lbl1 = (Label)e.Row.FindControl("lblTotalReturnCount");
            lbl1.Text = NetAmount.Replace(",", "<br>");

            Label lbl2 = (Label)e.Row.FindControl("lblTotalPurDiscAmount");
            lbl2.Text = DiscAmount.Replace(",", "<br>");
        }
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rprt.Close();
        rprt.Dispose();
    }
}
