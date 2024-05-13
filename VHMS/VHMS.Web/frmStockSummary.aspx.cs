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

public partial class frmStockSummary : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    decimal TotalQty = 0, TotalAmount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            LoadCatagory();
            LoadSubCategory();
            LoadSupplier();
            LoadProduct();
            LoadProductType();
            LoadUnit();
        }
    }
    private void LoadReport()
    {

        //ReportDataSet dsReportData = new ReportDataSet();

        //dsData = VHMS.DataAccess.VHMSReports.PrintStockDetails(0, ddlCategory.SelectedValue, ddlProduct.SelectedValue, ddlBranch.SelectedValue, ddlStatus.SelectedValue);
        //try
        //{
        //    if (dsData.Tables.Count > 0)
        //    {
        //        dsData.Tables[0].TableName = "tStock";
        //        foreach (DataRow drow in dsData.Tables[0].Rows)
        //            dsReportData.tStock.ImportRow(drow);

        //        gvProductMas.DataSource = dsData.Tables[0];
        //        gvProductMas.DataBind();
        //    }

        //}
        //catch (Exception ex)
        //{
        //    sException = "frmPrintStock | " + ex.ToString();
        //    Log.Write(sException);
        //}
    }

    public void LoadCatagory()
    {
        Collection<VHMS.Entity.Billing.Category> ObjList = new Collection<VHMS.Entity.Billing.Category>();
        ObjList = VHMS.DataAccess.Billing.Category.GetActiveCategory();

        ddlCategory.DataSource = ObjList;
        ddlCategory.DataTextField = "CategoryName";
        ddlCategory.DataValueField = "CategoryID";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
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

    public void LoadUnit()
    {
        Collection<VHMS.Entity.Billing.Unit> ObjList = new Collection<VHMS.Entity.Billing.Unit>();
        ObjList = VHMS.DataAccess.Billing.Unit.GetUnit();

        ddlUnit.DataSource = ObjList;
        ddlUnit.DataTextField = "UnitName";
        ddlUnit.DataValueField = "UnitID";
        ddlUnit.DataBind();
        ddlUnit.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    public void LoadSubCategory()
    {

        Collection<VHMS.Entity.SubCategory> ObjList = new Collection<VHMS.Entity.SubCategory>();
        ObjList = VHMS.DataAccess.SubCategory.GetActiveSubCategoryByCategoryID(Convert.ToInt32(ddlCategory.SelectedValue));

        ddlSubCategory.DataSource = ObjList;
        ddlSubCategory.DataTextField = "SubCategoryName";
        ddlSubCategory.DataValueField = "SubCategoryID";
        ddlSubCategory.DataBind();
        ddlSubCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }

    public void LoadProductType()
    {

        Collection<VHMS.Entity.Billing.ProductType> ObjList = new Collection<VHMS.Entity.Billing.ProductType>();
        ObjList = VHMS.DataAccess.Billing.ProductType.GetActiveProductType();

        ddlProductType.DataSource = ObjList;
        ddlProductType.DataTextField = "ProductTypeName";
        ddlProductType.DataValueField = "ProductTypeID";
        ddlProductType.DataBind();
        ddlProductType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        //Response.ContentType = "application/pdf";
        //Response.AddHeader("content-disposition", "attachment;filename=StockDetailed.pdf");
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);
        //gvProductMas.RenderControl(hw);
        //StringReader sr = new StringReader(sw.ToString());
        //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //pdfDoc.Open();
        //htmlparser.Parse(sr);
        //pdfDoc.Close();
        //Response.Write(pdfDoc);
        //Response.End();
        //gvProductMas.AllowPaging = true;
        //gvProductMas.DataBind();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        //Response.ClearContent();
        //Response.AppendHeader("content-disposition", "attachment; filename=StockDetailed.xls");
        //Response.ContentType = "application/excel";

        //StringWriter stringwriter = new StringWriter();
        //HtmlTextWriter htmltextwriter = new HtmlTextWriter(stringwriter);

        //gvProductMas.HeaderRow.Style.Add("background-color", "#FFFFFF");

        //foreach (TableCell tableCell in gvProductMas.HeaderRow.Cells)
        //{
        //    tableCell.Style["background-color"] = "#d8d4d4";
        //}

        //foreach (GridViewRow gridViewRow in gvProductMas.Rows)
        //{
        //    gridViewRow.BackColor = System.Drawing.Color.White;
        //    foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
        //    {
        //        gridViewRowTableCell.Style["background-color"] = "#FFFFFF";
        //    }
        //}
        //gvProductMas.RenderControl(htmltextwriter);
        //Response.Write(stringwriter.ToString());
        //Response.End();
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSubCategory();
        LoadProduct();
    }
    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    { LoadProduct(); }
    protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    { LoadProduct(); }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    public void LoadProduct()
    {
        Collection<VHMS.Entity.Master.Product> ObjList = new Collection<VHMS.Entity.Master.Product>();
        ObjList = VHMS.DataAccess.Master.Product.GetActiveProductID(0, Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlSubCategory.SelectedValue), Convert.ToInt32(ddlSupplier.SelectedValue));

        ddlProduct.DataSource = ObjList;
        ddlProduct.DataTextField = "ProductName";
        ddlProduct.DataValueField = "ProductID";
        ddlProduct.DataBind();
        ddlProduct.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        //    LoadReport();
    }

    private void LoadReport1()
    {
        ReportDataSet dsReportData = new ReportDataSet();

        dsData = VHMS.DataAccess.VHMSReports.PrintStockReport(ddlCategory.SelectedValue, ddlSubCategory.SelectedValue, ddlProduct.SelectedValue, ddlSupplier.SelectedValue, ddlProductType.SelectedValue, ddluser.Text, txtCode.Text, ddlUnit.SelectedValue);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tStock";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tStock.ImportRow(drow);

                GVSummary.DataSource = dsData.Tables[0];
                GVSummary.DataBind();
            }

        }
        catch (Exception ex)
        {
            sException = "frmPrintStock | " + ex.ToString();
            Log.Write(sException);
        }
    }

    protected void btnPDF1_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=StockSummary.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        GVSummary.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
        GVSummary.AllowPaging = true;
        GVSummary.DataBind();
    }
    protected void ddlProduct1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadReport();
    }
    protected void btnAddNew1_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AppendHeader("content-disposition", "attachment; filename=StockSummary.xls");
        Response.ContentType = "application/excel";

        StringWriter stringwriter = new StringWriter();
        HtmlTextWriter htmltextwriter = new HtmlTextWriter(stringwriter);

        GVSummary.HeaderRow.Style.Add("background-color", "#FFFFFF");

        foreach (TableCell tableCell in GVSummary.HeaderRow.Cells)
        {
            tableCell.Style["background-color"] = "#d8d4d4";
        }

        foreach (GridViewRow gridViewRow in GVSummary.Rows)
        {
            gridViewRow.BackColor = System.Drawing.Color.White;
            foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
            {
                gridViewRowTableCell.Style["background-color"] = "#FFFFFF";
            }
        }
        GVSummary.RenderControl(htmltextwriter);
        Response.Write(stringwriter.ToString());
        Response.End();
    }
    protected void btnView1_Click(object sender, EventArgs e)
    {
        GVSummary.Visible = true;

        LoadReport1();
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalQty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Quantity"));
            TotalAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalvalue"));
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

            e.Row.Cells[3].Text = "Total Qty:";
            e.Row.Cells[4].Text = Convert.ToDecimal(TotalQty).ToString();
            e.Row.Cells[6].Text = Convert.ToDecimal(TotalAmount).ToString();
            e.Row.Cells[3].Font.Bold = true;
            e.Row.Cells[4].Font.Bold = true;
            e.Row.Cells[3].Font.Size = 14;
            e.Row.Cells[6].Font.Bold = true;
            e.Row.Cells[6].Font.Size = 14;
            e.Row.Cells[4].Font.Size = 14;
        }
    }
    protected void tbAccount_TextChanged(object sender, EventArgs e)
    {
        if (txtCode.Text != "")
        {
            Collection<VHMS.Entity.Master.Product> ObjList = new Collection<VHMS.Entity.Master.Product>();
            ObjList = VHMS.DataAccess.Master.Product.GetProductByCodeByID(txtCode.Text, false);

            ddlProduct.DataSource = ObjList;
            ddlProduct.DataTextField = "ProductName";
            ddlProduct.DataValueField = "ProductID";
            ddlProduct.DataBind();
        }
        else
        {
            LoadProduct();
        }
        GVSummary.Visible = false;
    }
}