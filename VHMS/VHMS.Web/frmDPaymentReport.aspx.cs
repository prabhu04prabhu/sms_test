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
using System.Globalization;

public partial class frmDPaymentReport : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    String GpayAmount = "";
    decimal InvoiceAmount = 0, NetAmount = 0, DiscountAmount = 0, TotalQty = 0, TotalAmount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            LoadSupplier();
            txtDOB.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            txtDOR.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
    }
    protected void ddlPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        // LoadReport();
    }
    //protected void BindData()
    //{
    //    DataTable result = (DataTable)Session["tPurchase"];
    //    if (result.Rows.Count > 0)
    //    {
    //        gvPurchase.DataSource = result;
    //        gvPurchase.DataBind();
    ////    }
    //  }


    protected void btnExcel_Click(object sender, EventArgs e)
    {
        LoadReport();
        try
        {
            this.gvPurchase.AllowPaging = false;
            this.gvPurchase.AllowSorting = false;
            this.gvPurchase.EditIndex = -1;

            Response.Clear();
            Response.ContentType = "application/vnd.xls";
            Response.AddHeader("content-disposition", "attachment;filename=PaymentReports.xls");
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
    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadReport();
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        LoadReport();
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=PaymentReports.pdf");
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
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    private void LoadReport()
    {
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        ReportDataSet dsReportData = new ReportDataSet();

        dsData = VHMS.DataAccess.VHMSReports.PrintPaymentReport(txtDOB.Text, txtDOR.Text, ddlSupplier.SelectedValue, ddluser.SelectedValue, 0);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tPayment";

                gvPurchase.DataSource = dsData.Tables[0];
                gvPurchase.DataBind();
            }

        }
        catch (Exception ex)
        {
            sException = "frmPrintPurchase | " + ex.ToString();
            Log.Write(sException);
        }
    }
    public void LoadSupplier()
    {
        Collection<VHMS.Entity.Billing.Supplier> ObjList = new Collection<VHMS.Entity.Billing.Supplier>();
        ObjList = VHMS.DataAccess.Billing.Supplier.GetActiveSupplier();

        ddlSupplier.DataSource = ObjList;
        ddlSupplier.DataTextField = "SupplierName";
        ddlSupplier.DataValueField = "SupplierID";
        ddlSupplier.DataBind();
        ddlSupplier.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            GpayAmount += DataBinder.Eval(e.Row.DataItem, "BillNos");
            NetAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
            DiscountAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Discount_Amount"));
            InvoiceAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Charges"));
            Label lbl = (Label)e.Row.FindControl("lblBillCount");
            lbl.Text = GpayAmount.Replace(",", "<br>");
            GpayAmount = "";
            // GpayAmount += DataBinder.Eval(e.Row.DataItem, "BillNos");
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].ColumnSpan = 6;
            e.Row.Cells.RemoveAt(1);
            e.Row.Cells.RemoveAt(2);
            e.Row.Cells.RemoveAt(3);
            e.Row.Cells.RemoveAt(4);
            e.Row.Cells.RemoveAt(5);
            // e.Row.Cells.RemoveAt(6);

            e.Row.Cells[1].Text = "Total Amount :";
            e.Row.Cells[2].Text = Convert.ToDecimal(InvoiceAmount).ToString();
            e.Row.Cells[3].Text = Convert.ToDecimal(DiscountAmount).ToString();
            e.Row.Cells[4].Text = Convert.ToDecimal(NetAmount).ToString();

            e.Row.Cells[1].Font.Bold = true;
            e.Row.Cells[2].Font.Bold = true;
            e.Row.Cells[3].Font.Bold = true;
            e.Row.Cells[4].Font.Bold = true;
            e.Row.Cells[1].Font.Size = 12;
            e.Row.Cells[2].Font.Size = 12;
            e.Row.Cells[3].Font.Size = 12;
            e.Row.Cells[4].Font.Size = 12;

            Label lbl = (Label)e.Row.FindControl("lblBillCount");
            lbl.Text = GpayAmount.Replace(",", "<br>");


        }
    }
}

