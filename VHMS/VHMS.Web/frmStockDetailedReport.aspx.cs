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

public partial class frmStockDetailedReport : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    decimal TotalWeight = 0 ,Waste=0, NetWeight=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            LoadCatagory();
            LoadBranch();
            LoadProduct();
            LoadCatagory1();
            LoadBranch1();
            LoadProduct1();
        }
    }
    private void LoadReport()
    {
        ReportDataSet dsReportData = new ReportDataSet();
        dsData = VHMS.DataAccess.VHMSReports.PrintStockDetails(0, ddlCategory.SelectedValue, ddlProduct.SelectedValue, ddlBranch.SelectedValue, ddlStatus.SelectedValue);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tStock";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tStock.ImportRow(drow);

                gvProductMas.DataSource = dsData.Tables[0];
                gvProductMas.DataBind();
            }

        }
        catch (Exception ex)
        {
            sException = "frmPrintStock | " + ex.ToString();
            Log.Write(sException);
        }
    }
    
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=StockDetailed.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gvProductMas.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
        gvProductMas.AllowPaging = true;
        gvProductMas.DataBind();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AppendHeader("content-disposition", "attachment; filename=StockDetailed.xls");
        Response.ContentType = "application/excel";

        StringWriter stringwriter = new StringWriter();
        HtmlTextWriter htmltextwriter = new HtmlTextWriter(stringwriter);

        gvProductMas.HeaderRow.Style.Add("background-color", "#FFFFFF");

        foreach (TableCell tableCell in gvProductMas.HeaderRow.Cells)
        {
            tableCell.Style["background-color"] = "#d8d4d4";
        }

        foreach (GridViewRow gridViewRow in gvProductMas.Rows)
        {
            gridViewRow.BackColor = System.Drawing.Color.White;
            foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
            {
                gridViewRowTableCell.Style["background-color"] = "#FFFFFF";
            }
        }
        gvProductMas.RenderControl(htmltextwriter);
        Response.Write(stringwriter.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    public void LoadCatagory()
    {
        Collection<VHMS.Entity.Billing.Category> ObjList = new Collection<VHMS.Entity.Billing.Category>();
        ObjList = VHMS.DataAccess.Billing.Category.GetCategory();

        ddlCategory.DataSource = ObjList;
        ddlCategory.DataTextField = "CategoryName";
        ddlCategory.DataValueField = "CategoryID";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    public void LoadBranch()
    {
        Collection<VHMS.Entity.Branch> ObjList = new Collection<VHMS.Entity.Branch>();
        ObjList = VHMS.DataAccess.Branch.GetBranch();

        ddlBranch.DataSource = ObjList;
        ddlBranch.DataTextField = "BranchName";
        ddlBranch.DataValueField = "BranchID";
        ddlBranch.DataBind();
        ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    public void LoadProduct()
    {
        Collection<VHMS.Entity.Master.Product> ObjList = new Collection<VHMS.Entity.Master.Product>();
        ObjList = VHMS.DataAccess.Master.Product.GetProduct();

        ddlProduct.DataSource = ObjList;
        ddlProduct.DataTextField = "ProductName";
        ddlProduct.DataValueField = "ProductID";
        ddlProduct.DataBind();
        ddlProduct.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadReport();
    }

    private void LoadReport1()
    {
       
        //ReportDataSet dsReportData = new ReportDataSet();

        //dsData = VHMS.DataAccess.VHMSReports.PrintStockDetails1(ddlCategory1 .SelectedValue, ddlProduct1.SelectedValue, ddlBranch1.SelectedValue);
        //try
        //{
        //    if (dsData.Tables.Count > 0)
        //    {
        //        dsData.Tables[0].TableName = "tStock";
        //        foreach (DataRow drow in dsData.Tables[0].Rows)
        //            dsReportData.tStock.ImportRow(drow);

        //        GVSummary.DataSource = dsData.Tables[0];
        //        GVSummary.DataBind();
        //    }

        //}
        //catch (Exception ex)
        //{
        //    sException = "frmPrintStock | " + ex.ToString();
        //    Log.Write(sException);
        //}
    }
   
    protected void btnPDF1_Click(object sender, EventArgs e)
    {
        //Response.ContentType = "application/pdf";
        //Response.AddHeader("content-disposition", "attachment;filename=StockDetailed.pdf");
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);
        //GVSummary.RenderControl(hw);
        //StringReader sr = new StringReader(sw.ToString());
        //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //pdfDoc.Open();
        //htmlparser.Parse(sr);
        //pdfDoc.Close();
        //Response.Write(pdfDoc);
        //Response.End();
        //GVSummary.AllowPaging = true;
        //GVSummary.DataBind();
    }
    protected void ddlProduct1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadReport();
    }
    protected void btnAddNew1_Click(object sender, EventArgs e)
    {
        //Response.ClearContent();
        //Response.AppendHeader("content-disposition", "attachment; filename=StockDetailed.xls");
        //Response.ContentType = "application/excel";

        //StringWriter stringwriter = new StringWriter();
        //HtmlTextWriter htmltextwriter = new HtmlTextWriter(stringwriter);

        //GVSummary.HeaderRow.Style.Add("background-color", "#FFFFFF");

        //foreach (TableCell tableCell in GVSummary.HeaderRow.Cells)
        //{
        //    tableCell.Style["background-color"] = "#d8d4d4";
        //}

        //foreach (GridViewRow gridViewRow in GVSummary.Rows)
        //{
        //    gridViewRow.BackColor = System.Drawing.Color.White;
        //    foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
        //    {
        //        gridViewRowTableCell.Style["background-color"] = "#FFFFFF";
        //    }
        //}
        //GVSummary.RenderControl(htmltextwriter);
        //Response.Write(stringwriter.ToString());
        //Response.End();
    }
    public void LoadCatagory1()
    {
        //Collection<VHMS.Entity.Billing.Category> ObjList = new Collection<VHMS.Entity.Billing.Category>();
        //ObjList = VHMS.DataAccess.Billing.Category.GetCategory();

        //ddlCategory1.DataSource = ObjList;
        //ddlCategory1.DataTextField = "CategoryName";
        //ddlCategory1.DataValueField = "CategoryID";
        //ddlCategory1.DataBind();
        //ddlCategory1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    public void LoadBranch1()
    {
        //Collection<VHMS.Entity.Branch> ObjList = new Collection<VHMS.Entity.Branch>();
        //ObjList = VHMS.DataAccess.Branch.GetBranch();

        //ddlBranch1.DataSource = ObjList;
        //ddlBranch1.DataTextField = "BranchName";
        //ddlBranch1.DataValueField = "BranchID";
        //ddlBranch1.DataBind();
        //ddlBranch1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    public void LoadProduct1()
    {
        ////Collection<VHMS.Entity.Master.Product> ObjList = new Collection<VHMS.Entity.Master.Product>();
        ////ObjList = VHMS.DataAccess.Master.Product.GetProduct();

        ////ddlProduct1.DataSource = ObjList;
        ////ddlProduct1.DataTextField = "ProductName";
        ////ddlProduct1.DataValueField = "ProductID";
        ////ddlProduct1.DataBind();
        ////ddlProduct1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    protected void btnView1_Click(object sender, EventArgs e)
    {
       // LoadReport1();
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalWeight += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWeight"));
            Waste += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Wastage"));
            NetWeight += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetWeight"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].ColumnSpan = 4;
            e.Row.Cells.RemoveAt(1);
            e.Row.Cells.RemoveAt(2);
            e.Row.Cells.RemoveAt(3);

            e.Row.Cells[1].Text = "Total Amount :";
            e.Row.Cells[4].Text = Convert.ToDecimal(TotalWeight).ToString();
            e.Row.Cells[3].Text = Convert.ToDecimal(Waste).ToString();
            e.Row.Cells[2].Text = Convert.ToDecimal(NetWeight).ToString();
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