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

public partial class frmTDSCustomerLedger : System.Web.UI.Page
{
    ReportDocument rprt = new ReportDocument();
    DataSet dsData = new DataSet();
    decimal InvoiceAmount = 0, NetAmount = 0, DiscountAmount = 0, TotalQty = 0, OtherCharges = 0;

    string sException = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            LoadCustomer();
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

    public void LoadCustomer()
    {
        Collection<VHMS.Entity.Customer> ObjList = new Collection<VHMS.Entity.Customer>();
        ObjList = VHMS.DataAccess.Customer.GetActiveCustomer();

        ddlCustomer.DataSource = ObjList;
        ddlCustomer.DataTextField = "CustomerName";
        ddlCustomer.DataValueField = "CustomerID";
        ddlCustomer.DataBind();
        ddlCustomer.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rprt.Close();
        rprt.Dispose();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        LoadReport();
        try
        {
            this.gvSales.AllowPaging = false;
            this.gvSales.AllowSorting = false;
            this.gvSales.EditIndex = -1;

            Response.Clear();
            Response.ContentType = "application/vnd.xls";
            Response.AddHeader("content-disposition", "attachment;filename=TDSPaymentReports.xls");
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

    private void LoadReport()
    {
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        ReportDataSet dsReportData = new ReportDataSet();

        dsData = VHMS.DataAccess.VHMSReports.PrintTDSSalesEntry(0, txtDOB.Text, txtDOR.Text, ddlCustomer.SelectedValue);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                //dsData.Tables[0].TableName = "tSalesEntry";
                //foreach (DataRow drow in dsData.Tables[0].Rows)
                //    dsReportData.tSalesEntry.ImportRow(drow);

                gvSales.DataSource = dsData.Tables[0];
                gvSales.DataBind();
            }

        }
        catch (Exception ex)
        {
            sException = "frmPrintSales | " + ex.ToString();
            Log.Write(sException);
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadReport();
        //if (Convert.ToInt32(ddlCustomer.SelectedValue) > 0)
           
        //else
        //    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Select Customer');", true);
    }


    protected void btnPDF_Click(object sender, EventArgs e)
    {
        LoadReport();
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=TDSPaymentReports.pdf");
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
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NetAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TDSAmount"));
            InvoiceAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetAmount"));
            TotalQty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Roundoff"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].ColumnSpan = 7;
            e.Row.Cells.RemoveAt(1);
            e.Row.Cells.RemoveAt(2);
            e.Row.Cells.RemoveAt(3);
            e.Row.Cells.RemoveAt(4);
            e.Row.Cells.RemoveAt(5);
            e.Row.Cells.RemoveAt(6);

            e.Row.Cells[2].Text = "Total Amount :";
            e.Row.Cells[6].Text = Convert.ToDecimal(NetAmount).ToString();
            e.Row.Cells[5].Text = Convert.ToDecimal(TotalQty).ToString();
            e.Row.Cells[3].Text = Convert.ToDecimal(InvoiceAmount).ToString();
            e.Row.Cells[2].Font.Bold = true;
            e.Row.Cells[3].Font.Bold = true;
            e.Row.Cells[5].Font.Bold = true;
            e.Row.Cells[6].Font.Bold = true;
            e.Row.Cells[2].Font.Size = 14;
            e.Row.Cells[3].Font.Size = 14;
            e.Row.Cells[5].Font.Size = 14;
            e.Row.Cells[6].Font.Size = 14;
        }
    }
}

