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
using System.Globalization;
public partial class frmSalesReturnReport : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    decimal ReturnAmount = 0, TaxAmount = 0,TotalAmount=0,TotalQty=0, Totaldis=0,Invoice = 0;
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
    private void LoadReport()
    {
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        ReportDataSet dsReportData = new ReportDataSet();

        dsData = VHMS.DataAccess.VHMSReports.PrintReturn(txtDOB.Text, txtDOR.Text, ddlCustomer.SelectedValue, ddlDatetype.Text);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tSalesReturn";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tSalesReturn.ImportRow(drow);

                gvSalesReturn.DataSource = dsData.Tables[0];
                gvSalesReturn.DataBind();
            }

        }
        catch (Exception ex)
        {
            sException = "frmPrintgvExchange | " + ex.ToString();
            Log.Write(sException);
        }
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        if (gvSalesReturn.Rows.Count < 1)
        {
            string display = "no Records Found.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
        }
        else
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=WholeSalesReturn.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvSalesReturn.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
            gvSalesReturn.AllowPaging = true;
            gvSalesReturn.DataBind();
        }
    }
    protected void ddlPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadReport();
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ReturnAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ReturnAmount"));
            TaxAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TaxAmount"));
            TotalQty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalQuantity"));
            Totaldis += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DiscountAmount"));
            Invoice += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total"));
            TotalAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalAmount"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //GVUserEntry.FooterRow.Cells(0).ColumnSpan = 7
            e.Row.Cells[0].ColumnSpan =7;
            e.Row.Cells.RemoveAt(1);
            e.Row.Cells.RemoveAt(2);
            e.Row.Cells.RemoveAt(3);
            e.Row.Cells.RemoveAt(4);
            e.Row.Cells.RemoveAt(5);
            e.Row.Cells.RemoveAt(6);

            e.Row.Cells[1].Text = "Total Amount :";
            e.Row.Cells[2].Text = Convert.ToDecimal(TotalQty).ToString();
            e.Row.Cells[3].Text = Convert.ToDecimal(TotalAmount).ToString();
            e.Row.Cells[4].Text = Convert.ToDecimal(Totaldis).ToString();
            e.Row.Cells[5].Text = Convert.ToDecimal(Invoice).ToString();
            e.Row.Cells[6].Text = Convert.ToDecimal(TaxAmount).ToString();
            e.Row.Cells[7].Text = Convert.ToDecimal(ReturnAmount).ToString();
            e.Row.Cells[1].Font.Bold = true;
            e.Row.Cells[2].Font.Bold = true;
            e.Row.Cells[3].Font.Bold = true;
            e.Row.Cells[4].Font.Bold = true;
            e.Row.Cells[5].Font.Bold = true;
            e.Row.Cells[6].Font.Bold = true;
            e.Row.Cells[7].Font.Bold = true;
            e.Row.Cells[1].Font.Size = 14;
            e.Row.Cells[2].Font.Size = 14;
            e.Row.Cells[3].Font.Size = 14;
            e.Row.Cells[4].Font.Size = 14;
            e.Row.Cells[5].Font.Size = 14;
            e.Row.Cells[6].Font.Size = 14;
            e.Row.Cells[7].Font.Size = 14;
        }


    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        // lblTotal.Text = "Total Rows: " + (gvSalesReturn.DataSource as DataTable).Rows.Count;
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvSalesReturn.Rows.Count < 1)
        {
            string display = "no Records Found.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
        }
        else
        {
            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment; filename=WholeSalesReturn.xls");
            Response.ContentType = "application/excel";

            StringWriter stringwriter = new StringWriter();
            HtmlTextWriter htmltextwriter = new HtmlTextWriter(stringwriter);

            gvSalesReturn.HeaderRow.Style.Add("background-color", "#FFFFFF");

            foreach (TableCell tableCell in gvSalesReturn.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#d8d4d4";
            }

            foreach (GridViewRow gridViewRow in gvSalesReturn.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";
                }
            }
            gvSalesReturn.RenderControl(htmltextwriter);
            Response.Write(stringwriter.ToString());
            Response.End();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //
    }
    public void LoadCustomer()
    {
        Collection<VHMS.Entity.Customer> ObjList = new Collection<VHMS.Entity.Customer>();
        ObjList = VHMS.DataAccess.Customer.GetActiveSalesReturnCustomer();

        ddlCustomer.DataSource = ObjList;
        ddlCustomer.DataTextField = "CustomerName";
        ddlCustomer.DataValueField = "CustomerID";
        ddlCustomer.DataBind();
        ddlCustomer.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadReport();
    }
}