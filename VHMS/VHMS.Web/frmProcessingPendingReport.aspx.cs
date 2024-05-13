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
public partial class frmProcessingPendingReport : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    decimal InvoiceAmount = 0, NetAmount = 0, DiscountAmount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            LoadCatagory();

            LoadSubCategory();
            LoadSupplier();
            LoadProduct();
        }
    }
    protected void ddlPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        // LoadReport();
    }


    public void LoadSupplier()
    {
        Collection<VHMS.Entity.Billing.Vendor> ObjList = new Collection<VHMS.Entity.Billing.Vendor>();
        ObjList = VHMS.DataAccess.Billing.Vendor.GetActiveVendor();

        ddlVendor.DataSource = ObjList;
        ddlVendor.DataTextField = "VendorName";
        ddlVendor.DataValueField = "VendorID";
        ddlVendor.DataBind();
        ddlVendor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    { LoadProduct(); }
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
    public void LoadProduct()
    {
        Collection<VHMS.Entity.Master.Product> ObjList = new Collection<VHMS.Entity.Master.Product>();
        ObjList = VHMS.DataAccess.Master.Product.GetActiveProductID(0, Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlSubCategory.SelectedValue), 0);

        ddlProduct.DataSource = ObjList;
        ddlProduct.DataTextField = "ProductName";
        ddlProduct.DataValueField = "ProductID";
        ddlProduct.DataBind();
        ddlProduct.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
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
                Response.AddHeader("content-disposition", "attachment;filename=ProcessingPendingReports.xls");
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
            Response.AddHeader("content-disposition", "attachment;filename=ProcessingPendingReports.pdf");
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

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSubCategory();
        LoadProduct();
    }

    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    { LoadProduct(); }
    private void LoadReport()
    {
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        ReportDataSet dsReportData = new ReportDataSet();

        dsData = VHMS.DataAccess.VHMSReports.PrintProcessingPendingReport(ddlProduct.SelectedValue, ddlCategory.SelectedValue, ddlSubCategory.SelectedValue, ddlVendor.SelectedValue);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tProcessingInwardTrans";
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
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    InvoiceAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetAmount"));
        //}
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    e.Row.Cells[0].ColumnSpan = 5;
        //    e.Row.Cells.RemoveAt(1);
        //    e.Row.Cells.RemoveAt(2);
        //    e.Row.Cells.RemoveAt(3);
        //    e.Row.Cells.RemoveAt(4);

        //    e.Row.Cells[1].Text = "Total Amount :";
        //    e.Row.Cells[3].Text = Convert.ToDecimal(NetAmount).ToString();
        //    e.Row.Cells[4].Text = Convert.ToDecimal(InvoiceAmount).ToString();
        //    e.Row.Cells[2].Text = Convert.ToDecimal(DiscountAmount).ToString();
        //    e.Row.Cells[1].Font.Bold = true;
        //    e.Row.Cells[2].Font.Bold = true;
        //    e.Row.Cells[3].Font.Bold = true;
        //    e.Row.Cells[4].Font.Bold = true;
        //    e.Row.Cells[1].Font.Size = 14;
        //    e.Row.Cells[2].Font.Size = 14;
        //    e.Row.Cells[3].Font.Size = 14;
        //    e.Row.Cells[4].Font.Size = 14;
        //}
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
        //GVSummary.Visible = false;
    }
}

