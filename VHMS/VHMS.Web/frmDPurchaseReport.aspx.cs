﻿using System;
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
public partial class frmDPurchaseReport : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    decimal InvoiceAmount = 0, NetAmount = 0,DiscountAmount=0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            LoadSupplier();
            txtDOB.Text = Convert.ToDateTime(DateTime.Now.AddDays(-7)).ToString("dd/MM/yyyy");
            txtDOR.Text = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy");
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
                Response.AddHeader("content-disposition","attachment;filename=PurchaseReports.xls");
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
    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadReport();
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
            Response.AddHeader("content-disposition", "attachment;filename=PurchaseReports.pdf");
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
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    private void LoadReport()
    {
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        ReportDataSet dsReportData = new ReportDataSet();

        dsData = VHMS.DataAccess.VHMSReports.PrintPurchase( txtDOB.Text, txtDOR.Text,0, ddlSupplier.SelectedValue, 0);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tPurchase";

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
        ObjList = VHMS.DataAccess.Billing.Supplier.GetSupplier();

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
            NetAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TaxAmount"));
            InvoiceAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetAmount"));
            DiscountAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DiscountAmount"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].ColumnSpan = 5;
            e.Row.Cells.RemoveAt(1);
            e.Row.Cells.RemoveAt(2);
            e.Row.Cells.RemoveAt(3);
            e.Row.Cells.RemoveAt(4);

            e.Row.Cells[1].Text = "Total Amount :";
            e.Row.Cells[3].Text = Convert.ToDecimal(NetAmount).ToString();
            e.Row.Cells[4].Text = Convert.ToDecimal(InvoiceAmount).ToString();
            e.Row.Cells[2].Text = Convert.ToDecimal(DiscountAmount).ToString();
            e.Row.Cells[1].Font.Bold = true;
            e.Row.Cells[2].Font.Bold = true;
            e.Row.Cells[3].Font.Bold = true;
            e.Row.Cells[4].Font.Bold = true;
            e.Row.Cells[1].Font.Size = 14;
            e.Row.Cells[2].Font.Size = 14;
            e.Row.Cells[3].Font.Size = 14;
            e.Row.Cells[4].Font.Size = 14;
        }
    }
}

