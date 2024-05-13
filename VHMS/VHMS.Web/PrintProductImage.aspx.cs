using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.IO;
using System.Text;

public partial class PrintProductImage : System.Web.UI.Page
{
    ReportDocument rprt = new ReportDocument();
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            LoadCategory();
            LoadSubCategory();
            LoadSupplier();
            LoadProduct();
            LoadProductType();
        }
        else
        {
            bindReport();
        }
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
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rprt.Close();
        rprt.Dispose();
    }
    public void bindReport()
    {
        ReportDataSet dsReportData = new ReportDataSet();
        dsReportData = LoadData();
        rprt = new ReportDocument();
        if (chkPartyCode.Checked)
            rprt.Load(Server.MapPath("~/Reports/rptProductImage.rpt"));
        else
            rprt.Load(Server.MapPath("~/Reports/rptProductImageWithoutPartyCode.rpt"));
        rprt.SetDataSource(dsReportData);
        Session.Add("rptdoc", rprt);
        this.CRDischargeSummaryReport.ReportSource = rprt;
        CRDischargeSummaryReport.RefreshReport();
        CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
        CRDischargeSummaryReport.Zoom(100);
        this.CRDischargeSummaryReport.DataBind();
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
    public void LoadSubCategory()
    {

        Collection<VHMS.Entity.SubCategory> ObjList = new Collection<VHMS.Entity.SubCategory>();
        ObjList = VHMS.DataAccess.SubCategory.GetSubCategoryByCategoryID(Convert.ToInt32(ddlCategory.SelectedValue));

        ddlSubCategory.DataSource = ObjList;
        ddlSubCategory.DataTextField = "SubCategoryName";
        ddlSubCategory.DataValueField = "SubCategoryID";
        ddlSubCategory.DataBind();
        ddlSubCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadReport();
    }

    private void LoadReport()
    {
        string sTemp = string.Empty;
        try
        {
            ReportDataSet dsReportData = new ReportDataSet();
            dsReportData = LoadData();
            rprt = new ReportDocument();
            if (chkPartyCode.Checked)
                rprt.Load(Server.MapPath("~/Reports/rptProductImage.rpt"));
            else
                rprt.Load(Server.MapPath("~/Reports/rptProductImageWithoutPartyCode.rpt"));

            rprt.SetDataSource(dsReportData);
            Session.Add("rptdoc", rprt);
            this.CRDischargeSummaryReport.ReportSource = rprt;
            CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            CRDischargeSummaryReport.Zoom(100);
            this.CRDischargeSummaryReport.DataBind();
        }
        catch (Exception ex)
        {
            sException = "ReportPage_RepCheckPrint | " + ex.ToString();
            Log.Write(sException);
        }
    }

    private ReportDataSet LoadData()
    {
        dsData = VHMS.DataAccess.VHMSReports.PrintProductImage(Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlSubCategory.SelectedValue), Convert.ToInt32(ddlSupplier.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue),Convert.ToInt32(ddlProductType.SelectedValue),ddlOrder.Text);

        ReportDataSet dsReportData = new ReportDataSet();

        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "tProductImages";
            foreach (DataRow drow in dsData.Tables[0].Rows)
                dsReportData.tProductImages.ImportRow(drow);
            foreach (DataRow dr in dsReportData.tProductImages.Rows)
            {
                if (dr["Filepath"].ToString() != null && dr["Filepath"].ToString().Length > 10)
                    dr["Image"] = File.ReadAllBytes(Server.MapPath(dr["Filepath"].ToString()));
            }

            dsData.Tables[1].TableName = "tCompany";
            foreach (DataRow drow in dsData.Tables[1].Rows) dsReportData.tCompany.ImportRow(drow);
        }
        return dsReportData;
    }

    private void ExportReport()
    {
        ExportOptions exopt = default(ExportOptions);
        DiskFileDestinationOptions dfdopt = new DiskFileDestinationOptions();
        string fname = "ProductImageReport.pdf";
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //CRDischargeSummaryReport.ReportSource = rprt;
        //PrinterSettings getprinterName = new PrinterSettings();
        //rprt.PrintOptions.PrinterName = getprinterName.PrinterName;
        //rprtDuplicate.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)System.Drawing.Printing.PaperKind.A5;
        //rprt.PrintToPrinter(1, false, 0, 0);
        //this.CRDischargeSummaryReport.RefreshReport();
        ExportReport();
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
    }
}