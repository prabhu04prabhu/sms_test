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

public partial class frmExpenseReport : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    string PaymentMode = "";
    decimal TotalAmount = 0, DiscountAmount = 0, NetAmount = 0, Amount=0, Total=0, TotalQty=0, TaxAmount=0;

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
    protected void ddlPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        // LoadReport();
    }
    //protected void BindData()
    //{
    //    DataTable result = (DataTable)Session["tSales"];
    //    if (result.Rows.Count > 0)
    //    {
    //        gvSales.DataSource = result;
    //        gvSales.DataBind();
    ////    }
    //  }


    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvSales.Rows.Count < 1)
        {
            string display = "no Records Found.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
        }
        else
        {
            try
            {
                this.gvSales.AllowPaging = false;
                this.gvSales.AllowSorting = false;
                this.gvSales.EditIndex = -1;

                Response.Clear();
                Response.ContentType = "application/vnd.xls";
                Response.AddHeader("content-disposition", "attachment;filename=ExpenseReports.xls");
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
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadReport();
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        if (gvSales.Rows.Count < 1)
        {
            string display = "no Records Found.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
        }
        else
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ExpenseReports.pdf");
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
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    private void LoadReport()
    {
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        ReportDataSet dsReportData = new ReportDataSet();

        dsData = VHMS.DataAccess.VHMSReports.PrintExpenseReport(0, txtDOB.Text, txtDOR.Text, Convert.ToInt32(ddlSupplier.SelectedValue), ddluser.Text);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tExpense";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tExpense.ImportRow(drow);

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
    public void LoadCustomer()
    {
        Collection<VHMS.Entity.Ledger> ObjList = new Collection<VHMS.Entity.Ledger>();
        ObjList = VHMS.DataAccess.Ledger.GetLedger();

        ddlSupplier.DataSource = ObjList;
        ddlSupplier.DataTextField = "LedgerName";
        ddlSupplier.DataValueField = "LedgerID";
        ddlSupplier.DataBind();
        ddlSupplier.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TaxAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TaxAmount"));
            DiscountAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DiscountAmount"));
            TotalAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
            NetAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetAmount"));
            Total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total"));
            TotalQty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalQty"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].ColumnSpan = 5;
            e.Row.Cells.RemoveAt(1);
            e.Row.Cells.RemoveAt(2);
            e.Row.Cells.RemoveAt(3);
            e.Row.Cells.RemoveAt(4);

            e.Row.Cells[2].Text = "Total Amount :";
            e.Row.Cells[3].Text = Convert.ToDecimal(TotalQty).ToString();
            e.Row.Cells[4].Text = Convert.ToDecimal(TotalAmount).ToString();
            e.Row.Cells[5].Text = Convert.ToDecimal(DiscountAmount).ToString();
            e.Row.Cells[6].Text = Convert.ToDecimal(Total).ToString();
            e.Row.Cells[7].Text = Convert.ToDecimal(TaxAmount).ToString();
            e.Row.Cells[8].Text = Convert.ToDecimal(NetAmount).ToString();
            e.Row.Cells[2].Font.Bold = true;
            e.Row.Cells[3].Font.Bold = true;
            e.Row.Cells[4].Font.Bold = true;
            e.Row.Cells[5].Font.Bold = true;
            e.Row.Cells[6].Font.Bold = true;
            e.Row.Cells[7].Font.Bold = true;
            e.Row.Cells[8].Font.Bold = true;

            e.Row.Cells[2].Font.Size = 14;
            e.Row.Cells[3].Font.Size = 14;
            e.Row.Cells[4].Font.Size = 14;
            e.Row.Cells[5].Font.Size = 14;
            e.Row.Cells[6].Font.Size = 14;
            e.Row.Cells[7].Font.Size = 14;
            e.Row.Cells[8].Font.Size = 14;
        }
    }
}

