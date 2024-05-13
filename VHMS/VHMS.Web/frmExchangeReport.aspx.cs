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

public partial class frmExchangeReport : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    decimal InvoiceAmount = 0 , TaxAmount=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            //LoadCatagory();
            //LoadBranch();
            //LoadProduct();
            //LoadCustomer();
            LoadBranch1();
            txtDOB.Text = Convert.ToDateTime(DateTime.Now.AddDays(-7)).ToString("dd-MM-yyyy");
            txtDOR.Text = Convert.ToDateTime(DateTime.Now).ToString("dd-MM-yyyy");
        }
    }
    private void LoadReport1()
    {
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        ReportDataSet dsReportData = new ReportDataSet();

        dsData = VHMS.DataAccess.VHMSReports.PrintExchange(0, txtDOB.Text, txtDOR.Text, txtMobile.Text, ddlBranch.SelectedValue);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tExchange";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tExchange.ImportRow(drow);

                gvExchange.DataSource = dsData.Tables[0];
                gvExchange.DataBind();
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
        if (gvExchange.Rows.Count < 1)
        {
            string display = "no Records Found.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
        }
        else
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ExchangeDetailed.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvExchange.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
            gvExchange.AllowPaging = true;
            gvExchange.DataBind();
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            InvoiceAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetAmount"));
            TaxAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TaxAmount"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].ColumnSpan = 5;
            e.Row.Cells.RemoveAt(1);
            e.Row.Cells.RemoveAt(2);
            e.Row.Cells.RemoveAt(3);
            e.Row.Cells.RemoveAt(4);

            e.Row.Cells[1].Text = "Total Amount :";
            e.Row.Cells[3].Text = Convert.ToDecimal(InvoiceAmount).ToString();
            e.Row.Cells[2].Text = Convert.ToDecimal(TaxAmount).ToString();
            //e.Row.Cells[4].Font.Bold = true;
            e.Row.Cells[1].Font.Bold = true;
            e.Row.Cells[2].Font.Bold = true;
            e.Row.Cells[3].Font.Bold = true;
           // e.Row.Cells[4].Font.Size = 14;
            e.Row.Cells[1].Font.Size = 14;
            e.Row.Cells[2].Font.Size = 14;
            e.Row.Cells[3].Font.Size = 14;
        }
    }
    protected void ddlPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadReport();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvExchange.Rows.Count < 1)
        {
            string display = "no Records Found.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
        }
        else
        {
            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment; filename=ExchangeDetailed.xls");
            Response.ContentType = "application/excel";

            StringWriter stringwriter = new StringWriter();
            HtmlTextWriter htmltextwriter = new HtmlTextWriter(stringwriter);

            gvExchange.HeaderRow.Style.Add("background-color", "#FFFFFF");

            foreach (TableCell tableCell in gvExchange.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#d8d4d4";
            }

            foreach (GridViewRow gridViewRow in gvExchange.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";
                }
            }
            gvExchange.RenderControl(htmltextwriter);
            Response.Write(stringwriter.ToString());
            Response.End();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //
    }
    public void LoadBranch1()
    {
        Collection<VHMS.Entity.Branch> ObjList = new Collection<VHMS.Entity.Branch>();
        ObjList = VHMS.DataAccess.Branch.GetBranch();

        ddlBranch.DataSource = ObjList;
        ddlBranch.DataTextField = "BranchName";
        ddlBranch.DataValueField = "BranchID";
        ddlBranch.DataBind();
        ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    public void LoadCustomer()
    {
        Collection<VHMS.Entity.Customer> ObjList = new Collection<VHMS.Entity.Customer>();
        ObjList = VHMS.DataAccess.Customer.GetCustomer();

        //ddlCustomerName.DataSource = ObjList;
        //ddlCustomerName.DataTextField = "CustomerName";
        //ddlCustomerName.DataValueField = "CustomerID";
        //ddlCustomerName.DataBind();
        //ddlCustomerName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        //DateTime fdate = Convert.ToDateTime(txtDOB.Text);
        //DateTime tdate = Convert.ToDateTime(txtDOR.Text);
        //string display = "From date must be less than To date.";
        //if (fdate >= tdate)
        //    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
        //else
            LoadReport1();
    }
}