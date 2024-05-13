using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using System.Data;
using System.IO;
using System.Globalization;

public partial class RepSalesMarginReport : System.Web.UI.Page
{
    ReportDocument rprt = new ReportDocument();
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnExportReport);

        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            LoadSupplier();
            LoadCustomer();
            LoadCategory();
            LoadSubcategory();
            LoadProduct();
            txtDOB.Text = Convert.ToDateTime(DateTime.Now).AddDays(-120).ToString("dd/MM/yyyy");
            txtDOR.Text = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy");
        }
        LoadReport();
    }
    //private static string fromDate(Int32 firstName)
    //{
    //    VHMS.Entity.FinancialYear objQuotation = new VHMS.Entity.FinancialYear();
    //    objQuotation = VHMS.DataAccess.FinancialYear.GetFinancialYearByID(firstName);
    //    string result = objQuotation.sYearFrom;
    //    return result;
    //}
    //private static string FinancialYearStatus(Int32 firstName)
    //{
    //    VHMS.Entity.FinancialYear objQuotation = new VHMS.Entity.FinancialYear();
    //    objQuotation = VHMS.DataAccess.FinancialYear.GetFinancialYearByID(firstName);
    //    string result = objQuotation.Status;
    //    return result;
    //}
    //private static string TODate(Int32 firstName)
    //{
    //    VHMS.Entity.FinancialYear objQuotation = new VHMS.Entity.FinancialYear();
    //    objQuotation = VHMS.DataAccess.FinancialYear.GetFinancialYearByID(firstName);
    //    string result = objQuotation.sYearTo;
    //    return result;
    //}
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

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rprt.Close();
        rprt.Dispose();
    }

    public void LoadCategory()
    {
        Collection<VHMS.Entity.Billing.Category> ObjList = new Collection<VHMS.Entity.Billing.Category>();
        ObjList = VHMS.DataAccess.Billing.Category.GetActiveCategory();

        ddlCategory.DataSource = ObjList;
        ddlCategory.DataTextField = "CategoryName";
        ddlCategory.DataValueField = "CategoryID";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }

    public void LoadSubcategory()
    {
        Collection<VHMS.Entity.SubCategory> ObjList = new Collection<VHMS.Entity.SubCategory>();
        ObjList = VHMS.DataAccess.SubCategory.GetActiveSubCategoryByCategoryID(Convert.ToInt32(ddlCategory.SelectedValue));

        ddlSubcategory.DataSource = ObjList;
        ddlSubcategory.DataTextField = "SubCategoryName";
        ddlSubcategory.DataValueField = "SubCategoryID";
        ddlSubcategory.DataBind();
        ddlSubcategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }

    public void LoadProduct()
    {
        Collection<VHMS.Entity.Master.Product> ObjList = new Collection<VHMS.Entity.Master.Product>();
        ObjList = VHMS.DataAccess.Master.Product.GetActiveProductID(0, Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlSubcategory.SelectedValue), Convert.ToInt32(ddlSupplier.SelectedValue));

        ddlProduct.DataSource = ObjList;
        ddlProduct.DataTextField = "ProductName";
        ddlProduct.DataValueField = "ProductID";
        ddlProduct.DataBind();
        ddlProduct.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }

    public void LoadCustomer()
    {
        Collection<VHMS.Entity.Customer> ObjList = new Collection<VHMS.Entity.Customer>();
        ObjList = VHMS.DataAccess.Customer.GetActiveCustomer();

        ddlCustomer.DataSource = ObjList;
        ddlCustomer.DataTextField = "CustomerName";
        ddlCustomer.DataValueField = "CustomerID";
        ddlCustomer.DataBind();
        ddlCustomer.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSubcategory();
        LoadProduct();
    }

    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    { LoadProduct(); }

    protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    { LoadProduct(); }
    
    protected void btnSearchReport_Click(object sender, EventArgs e)
    { LoadReport(); }

    protected void btnExportReport_Click(object sender, EventArgs e)
    {
        try
        {
            ExportReport();
        }
        catch (Exception ex)
        { Log.Write("RepViewDischargeSummary btnExportReport_Click | " + ex.ToString()); }
    }

    private void LoadReport()
    {
        string sTemp = string.Empty;
        try
        {
            ReportDataSet dsReportData = new ReportDataSet();
            dsReportData = LoadData();
            rprt = new ReportDocument();
            //if (!chkPartyCode.Checked)
            //{
           if (ddluser.Text == "BillWise")
                rprt.Load(Server.MapPath("~/Reports/rptSalesBillWiseMarginReport.rpt"));
            else
                rprt.Load(Server.MapPath("~/Reports/rptSalesAbstractYearWiseMarginReport.rpt"));
            // }
            //else
            //{
            //     rprt.Load(Server.MapPath("~/Reports/rptAbstractMarginMonthWiseReport.rpt"));
            //}
            rprt.SetDataSource(dsReportData);
            this.CRDischargeSummaryReport.ReportSource = rprt;
            CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            CRDischargeSummaryReport.Zoom(100);
            this.CRDischargeSummaryReport.DataBind();
        }
        catch (Exception ex)
        {
            sException = "ReportPage_RepSalesTaxReturn | " + ex.ToString();
            Log.Write(sException);
        }
    }
    private ReportDataSet LoadData()
    {
       // if (!chkPartyCode.Checked)
          //  dsData = VHMS.DataAccess.VHMSReports.MarginReport(Convert.ToInt32(ddlSupplier.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlSubcategory.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(ddlCustomer.SelectedValue), txtDOB.Text, txtDOR.Text, ddlType.SelectedValue);
        //else
            dsData = VHMS.DataAccess.VHMSReports.SalesMarginReport1(Convert.ToInt32(ddlSupplier.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlSubcategory.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(ddlCustomer.SelectedValue), txtDOB.Text, txtDOR.Text, ddlType.SelectedValue);
        ReportDataSet dsReportData = new ReportDataSet();

        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "tSalesEntryTrans";
            foreach (DataRow drow in dsData.Tables[0].Rows) dsReportData.tSalesEntryTrans.ImportRow(drow);
            dsData.Tables[1].TableName = "tCompany";
            foreach (DataRow drow in dsData.Tables[1].Rows) dsReportData.tCompany.ImportRow(drow);
        }
        return dsReportData;
    }
    private void ExportReport()
    {
        ExportOptions exopt = default(ExportOptions);
        DiskFileDestinationOptions dfdopt = new DiskFileDestinationOptions();
        string fname = "SalesMarginWithReturnReport.pdf";
        dfdopt.DiskFileName = Server.MapPath(fname);

        exopt = rprt.ExportOptions;
        exopt.ExportDestinationType = ExportDestinationType.DiskFile;

        //for PDF select PortableDocFormat for excel select ExportFormatType.Excel    
        exopt.ExportFormatType = ExportFormatType.PortableDocFormat;
        exopt.DestinationOptions = dfdopt;

        //finally export your report document    
        rprt.Export();

        //To open your PDF after save it from crystal report    

        string Path = Server.MapPath(fname);
        FileInfo file = new FileInfo(Path);
        Response.ClearContent();

        Response.AddHeader("Content-Disposition", "inline; filename=" + file.Name);
        Response.AddHeader("Content-Length", file.Length.ToString());
        Response.ContentType = "application/pdf";
        Response.TransmitFile(file.FullName);
        Response.Flush();
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