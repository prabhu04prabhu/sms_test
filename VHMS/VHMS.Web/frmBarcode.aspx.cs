using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Text;
using System.Drawing;
using System.Globalization;
using KeepDynamic.Barcode.Generator;
using BarcodeLib.Barcode.ASP.NET;
using BarcodeLib.Barcode.CrystalReports;
using BarcodeLib.Barcode;
using Zen.Barcode;
using TarCode.Barcode.Control;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using System.Web.Services;

public partial class frmBarcode : BaseConfig
{
    string iBarcode = "";
    string formName = "StockAdjuest";
    DataSet dsData = new DataSet();
    DataSet dsPurchase = new DataSet();
    ReportDocument rprt = new ReportDocument();
    string sException = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            if (HttpContext.Current.Session["Barcode"] != null)
                txtBarcode.Text = HttpContext.Current.Session["Barcode"].ToString();
            else
                txtBarcode.Text = "";
            if (HttpContext.Current.Session["BarcodeQty"] != null)
                txtCount.Text = HttpContext.Current.Session["BarcodeQty"].ToString();
            else
                txtCount.Text = "1";
            if (HttpContext.Current.Session["ScreenName"] != null)
                formName = HttpContext.Current.Session["ScreenName"].ToString();


            if (HttpContext.Current.Session["BarcodePurchaseID"] != null)
            {
                if (formName == "Purchase")
                {
                    GetPurchaseDetails(HttpContext.Current.Session["BarcodePurchaseID"].ToString());
                    btnPrintAll.Visible = true;
                }
                else
                    btnPrintAll.Visible = false;
            }
            else
            {
                btnPrintAll.Visible = false;
                txtCount.Text = "1";
            }

            Session["Barcode"] = null;
            Session["BarcodePurchaseID"] = null;
            Session["BarcodeQty"] = null;
            Session["ScreenName"] = "";
            btnGenrate.BackColor = Color.LightBlue;
            btnPrint.BackColor = Color.LightBlue;
            btnPrintAll.BackColor = Color.LightBlue;
        }

    }

    public void GetPurchaseDetails(string ID)
    {
        dsPurchase = VHMS.DataAccess.VHMSReports.PrintPurchaseByID(Convert.ToInt32(ID));
        if (dsPurchase.Tables.Count > 0 && dsPurchase.Tables[1].Rows.Count > 0)
        {
            GridView1.DataSource = dsPurchase.Tables[1];
            GridView1.DataBind();
        }
    }


    [WebMethod]
    public static string[] GetBarcode(string prefix)
    {
        Collection<VHMS.Entity.Master.Product> ObjList = new Collection<VHMS.Entity.Master.Product>();
        List<string> customers = new List<string>();
        customers = VHMS.DataAccess.Master.Product.GetProductList(prefix);
        return customers.ToArray();
    }

    [WebMethod]
    public static string[] GetBarcodeList(string prefix)
    {
        Collection<VHMS.Entity.Master.Product> ObjList = new Collection<VHMS.Entity.Master.Product>();
        List<string> customers = new List<string>();
        customers = VHMS.DataAccess.Master.Product.GetBarcodeList(prefix);
        return customers.ToArray();
    }

   
    [WebMethod]
    public static string[] GetBarcodeCode(string prefix, int SupplierID, string IsAll)
    {
        Collection<VHMS.Entity.Master.Product> ObjList = new Collection<VHMS.Entity.Master.Product>();
        List<string> customers = new List<string>();
        if (IsAll == "A")
            SupplierID = 0;
        customers = VHMS.DataAccess.Master.Product.GetProductCodeList(prefix, SupplierID);
        return customers.ToArray();
    }

    [WebMethod]
    public static string[] GetBarcodeCodeYarn(string prefix, int SupplierID, string IsAll)
    {
        Collection<VHMS.Entity.Master.Product> ObjList = new Collection<VHMS.Entity.Master.Product>();
        List<string> customers = new List<string>();
        if (IsAll == "A")
            SupplierID = 0;
        customers = VHMS.DataAccess.Master.Product.GetProductCodeListYarn(prefix, SupplierID);
        return customers.ToArray();
    }

    [WebMethod]
    public static string[] GetSubCategoryNameList(string prefix, int CategoryID)
    {
        Collection<VHMS.Entity.SubCategory> ObjList = new Collection<VHMS.Entity.SubCategory>();
        List<string> customers = new List<string>();
        customers = VHMS.DataAccess.SubCategory.GetSubCategoryNameList(prefix, CategoryID);
        return customers.ToArray();
    }


    [WebMethod]
    public static string[] GetRetailSalesCustomer(string prefix)
    {
        Collection<VHMS.Entity.Customer> ObjList = new Collection<VHMS.Entity.Customer>();
        List<string> customers = new List<string>();
        customers = VHMS.DataAccess.Customer.GetRetailSalesCustomer(prefix);
        return customers.ToArray();
    }

    [WebMethod]
    public static string[] GetSMSCode(string prefix, int SupplierID, string IsAll)
    {
        Collection<VHMS.Entity.Master.Product> ObjList = new Collection<VHMS.Entity.Master.Product>();
        List<string> customers = new List<string>();
        if (IsAll == "A")
            SupplierID = 0;
        customers = VHMS.DataAccess.Master.Product.GetProductSMSCodeList(prefix, SupplierID);
        return customers.ToArray();
    }

    [WebMethod]
    public static string[] GetWholeSalesCustomer(string prefix)
    {
        Collection<VHMS.Entity.Customer> ObjList = new Collection<VHMS.Entity.Customer>();
        List<string> customers = new List<string>();
        customers = VHMS.DataAccess.Customer.GetWholeSalesCustomer(prefix);
        return customers.ToArray();
    }


    public void bindReport()
    {
        if (txtBarcode.Text.Length > 0)
        {

            if (txtCount.Text.Length > 0)
            {
                string barCode = txtBarcode.Text;
                var drawObject = BarcodeDrawFactory.GetSymbology(Zen.Barcode.BarcodeSymbology.Code128);
                var metrics = drawObject.GetDefaultMetrics(30);
                metrics.Scale = 2;
                System.Drawing.Image BackgroundImage = drawObject.Draw(Convert.ToString(txtBarcode.Text.Trim()), metrics);

                ImageConverter converter = new ImageConverter();
                byte[] byteImage;
                byteImage = (byte[])converter.ConvertTo(BackgroundImage, typeof(byte[]));

                try
                {
                    ReportDataSet dsReportData = new ReportDataSet();
                    dsData = VHMS.DataAccess.VHMSReports.PrintBarcode(txtBarcode.Text.Trim());

                    if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
                    {
                        dsData.Tables[0].TableName = "tProduct";
                        ReportDataSet.tBarcodeRow drTb_BarcodeRow = null;
                        int BarcodeCount = Convert.ToInt32(txtCount.Text);
                        foreach (DataRow drow in dsData.Tables[0].Rows)
                        {
                            for (int i = 0; i < BarcodeCount; i++)
                            {
                                drTb_BarcodeRow = dsReportData.tBarcode.NewtBarcodeRow();
                                drTb_BarcodeRow.MRPPrice = Convert.ToDecimal(drow["MRP"]);
                                if (ddlRate.SelectedValue == "WholeSale")
                                    drTb_BarcodeRow.SellingPrice = Convert.ToDecimal(drow["WholeSalePrice"]);
                                else
                                    drTb_BarcodeRow.SellingPrice = Convert.ToDecimal(drow["RetailPrice"]);
                                if (drow["PurchaseDate"] != "01/01/1900")
                                    drTb_BarcodeRow.PurchaseDate = Convert.ToDateTime(drow["PurchaseDate"]);
                                else
                                    drTb_BarcodeRow.PurchaseDate = Convert.ToDateTime(DateTime.Now);
                                drTb_BarcodeRow.Code = "SMS";
                                drTb_BarcodeRow.Barcode = byteImage;
                                drTb_BarcodeRow.Barcodelabel = barCode;

                                if (drTb_BarcodeRow.RowState == System.Data.DataRowState.Detached)
                                    dsReportData.tBarcode.Rows.Add(drTb_BarcodeRow);
                            }
                        }
                    }

                    rprt = new ReportDocument();
                    if (ddlType.SelectedValue == "SMSCodeOnly")
                        rprt.Load(Server.MapPath("~/Reports/rptSMSOnlyBarcode.rpt"));
                    else
                        rprt.Load(Server.MapPath("~/Reports/rptSMSreaderBarcode.rpt"));

                    rprt.SetDataSource(dsReportData);
                    this.CRDischargeSummaryReport.ReportSource = rprt;
                    CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
                    CRDischargeSummaryReport.Zoom(100);
                    this.CRDischargeSummaryReport.DataBind();

                }

                catch (Exception ex)
                {
                    sException = "frmPrintJobCard | " + ex.ToString();
                    Log.Write(sException);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "clientScript", "alert('Enter Print Count');", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "clientScript", "alert('Enter Barcode or SMS Code');", true);
        }
    }
    protected void btnGenrate_Click(object sender, EventArgs e)
    {
        if (txtBarcode.Text.Length > 0 || txtSMSCode.Text.Length > 0)
        {
            if (txtCount.Text.Length > 0)
            {
                try
                {
                    ReportDataSet dsReportData = new ReportDataSet();
                    if (txtBarcode.Text.Length > 0)
                        dsData = VHMS.DataAccess.VHMSReports.PrintBarcode(txtBarcode.Text.Trim());
                    else
                        dsData = VHMS.DataAccess.VHMSReports.PrintSMSCode(txtSMSCode.Text.Trim());

                    if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
                    {


                        dsData.Tables[0].TableName = "tProduct";
                        ReportDataSet.tBarcodeRow drTb_BarcodeRow = null;
                        decimal Count = Convert.ToDecimal(txtCount.Text);
                        int BarcodeCount = Convert.ToInt32(Count);
                        foreach (DataRow drow in dsData.Tables[0].Rows)
                        {
                            if (txtBarcode.Text.Length <= 0)
                                txtBarcode.Text = drow["TextCode"].ToString();

                            string barCode = "";
                            string TextCodes = "";
                            //if (ddlType.SelectedValue == "BarReader")
                            //{
                            //    if (txtBarcode.Text.Length > 0)
                            //        barCode = txtBarcode.Text.Trim();
                            //    else
                            //        barCode = txtSMSCode.Text.Trim();
                            //}
                            //else if (ddlType.SelectedValue == "SMSCodeOnly")
                            //{
                            //    barCode = drow["SMSCode"].ToString();
                            //}
                            //else
                            //{
                            //    if (txtBarcode.Text.Length > 0)
                            //        barCode = txtBarcode.Text.Trim();
                            //    else
                            //        barCode = txtSMSCode.Text.Trim();
                            //}


                            if (ddlType.SelectedValue == "BarReader")
                            {
                                if (txtBarcode.Text.Length > 0)
                                {
                                    TextCodes = drow["SMSCode"].ToString();
                                    barCode = txtBarcode.Text.Trim();
                                }
                                else
                                {
                                    TextCodes = drow["TextCode"].ToString();
                                    barCode = drow["SMSCode"].ToString();
                                }
                            }
                            else if (ddlType.SelectedValue == "SMSreader")
                            {
                                //if (txtBarcode.Text.Length > 0)
                                //{
                                TextCodes = txtBarcode.Text.Trim();
                                barCode = drow["SMSCode"].ToString();
                                // }

                                //if (txtSMSCode.Text.Length > 0)
                                //{
                                //    TextCodes = drow["SMSCode"].ToString();
                                //    barCode = drow["TextCode"].ToString();
                                //}
                            }
                            else
                            {
                                TextCodes = drow["SMSCode"].ToString();
                                barCode = drow["SMSCode"].ToString();
                            }

                            var drawObject = BarcodeDrawFactory.GetSymbology(Zen.Barcode.BarcodeSymbology.Code128);
                            var metrics = drawObject.GetDefaultMetrics(30);
                            metrics.Scale = 2;
                            System.Drawing.Image BackgroundImage = drawObject.Draw(Convert.ToString(barCode), metrics);

                            ImageConverter converter = new ImageConverter();
                            byte[] byteImage;
                            byteImage = (byte[])converter.ConvertTo(BackgroundImage, typeof(byte[]));

                            for (int i = 0; i < BarcodeCount; i++)
                            {
                                drTb_BarcodeRow = dsReportData.tBarcode.NewtBarcodeRow();
                                drTb_BarcodeRow.MRPPrice = Convert.ToDecimal(drow["MRP"]);
                                if (ddlRate.SelectedValue == "WholeSale")
                                    drTb_BarcodeRow.SellingPrice = Convert.ToDecimal(drow["WholeSalePrice"]);
                                else
                                    drTb_BarcodeRow.SellingPrice = Convert.ToDecimal(drow["RetailPrice"]);
                                if (!string.IsNullOrEmpty(drow["PurchaseDate"].ToString()))
                                    drTb_BarcodeRow.PurchaseDate = Convert.ToDateTime(drow["PurchaseDate"]);
                                else
                                    drTb_BarcodeRow.PurchaseDate = Convert.ToDateTime(drow["CreatedOn"]);
                                drTb_BarcodeRow.Code = "SMS";
                                drTb_BarcodeRow.Barcode = byteImage;
                                drTb_BarcodeRow.Barcodelabel = barCode;
                                if (ddlType.SelectedValue != "SMSCodeOnly")
                                {
                                    drTb_BarcodeRow.TextCode = TextCodes;
                                    drTb_BarcodeRow.PrintName = Convert.ToString(drow["CategoryName"]) + "/" + Convert.ToString(drow["SubCategoryName"]);
                                    drTb_BarcodeRow.Minimum_DiscountPercentage = Convert.ToInt32(drow["Minimum_DiscountPercentage"]) + "-" + Convert.ToInt32(drow["Maximum_DiscountPercentage"]);
                                    drTb_BarcodeRow.Maximum_DiscountPercentage = 0;
                                }

                                if (drTb_BarcodeRow.RowState == System.Data.DataRowState.Detached)
                                    dsReportData.tBarcode.Rows.Add(drTb_BarcodeRow);
                            }
                        }
                    }

                    rprt = new ReportDocument();
                    if (ddlType.SelectedValue == "SMSCodeOnly")
                        rprt.Load(Server.MapPath("~/Reports/rptSMSOnlyBarcode.rpt"));
                    else
                        rprt.Load(Server.MapPath("~/Reports/rptSMSreaderBarcode.rpt"));

                    rprt.SetDataSource(dsReportData);
                    this.CRDischargeSummaryReport.ReportSource = rprt;
                    CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
                    CRDischargeSummaryReport.Zoom(100);
                    this.CRDischargeSummaryReport.DataBind();

                }

                catch (Exception ex)
                {
                    sException = "frmPrintJobCard | " + ex.ToString();
                    Log.Write(sException);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "clientScript", "alert('Enter Print Count');", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "clientScript", "alert('Enter Barcode or SMS Code');", true);
        }

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btnGenrate_Click(sender, e);
        ExportReport();
    }
    protected void btnPrintAll_Click(object sender, EventArgs e)
    {
        try
        {
            ReportDataSet dsReportData = new ReportDataSet();
            foreach (GridViewRow row in GridView1.Rows)
            {

                txtBarcode.Text = (row.FindControl("txtBarcode") as TextBox).Text.Trim();
                //}
                //else
                //{
                txtSMSCode.Text = (row.FindControl("txtSMSCode") as TextBox).Text.Trim();
                txtCount.Text = (row.FindControl("txtQuantity") as TextBox).Text;

                dsData = VHMS.DataAccess.VHMSReports.PrintBarcode(txtBarcode.Text);

                if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
                {
                    dsData.Tables[0].TableName = "tProduct";
                    ReportDataSet.tBarcodeRow drTb_BarcodeRow = null;
                    decimal Count = Convert.ToDecimal(txtCount.Text);
                    int BarcodeCount = Convert.ToInt32(Count);
                    foreach (DataRow drow in dsData.Tables[0].Rows)
                    {
                        string barCode;
                        string TextCodes;
                        //if (ddlType.SelectedValue == "BarReader")
                        //{
                        //    if (txtBarcode.Text.Length > 0)
                        //        barCode = txtBarcode.Text.Trim();
                        //    else
                        //        barCode = txtSMSCode.Text.Trim();
                        //}
                        //else
                        //    barCode = drow["SMSCode"].ToString();

                        //if (ddlType.SelectedValue != "BarReader")
                        //    TextCode = drow["TextCode"].ToString();

                        //else
                        //    TextCode = drow["SMSCode"].ToString();
                        if (ddlType.SelectedValue == "BarReader")
                        {
                            if (txtBarcode.Text.Length > 0)
                            {
                                TextCodes = drow["SMSCode"].ToString();
                                barCode = txtBarcode.Text.Trim();
                            }
                            else
                            {
                                TextCodes = drow["TextCode"].ToString();
                                barCode = drow["SMSCode"].ToString();
                            }
                        }
                        else if (ddlType.SelectedValue == "SMSreader")
                        {
                            //if (txtBarcode.Text.Length > 0)
                            //{
                            TextCodes = txtBarcode.Text.Trim();
                            barCode = drow["SMSCode"].ToString();
                            // }

                            //if (txtSMSCode.Text.Length > 0)
                            //{
                            //    TextCodes = drow["SMSCode"].ToString();
                            //    barCode = drow["TextCode"].ToString();
                            //}
                        }
                        else
                        {
                            TextCodes = drow["SMSCode"].ToString();
                            barCode = drow["SMSCode"].ToString();
                        }

                        var drawObject = BarcodeDrawFactory.GetSymbology(Zen.Barcode.BarcodeSymbology.Code128);
                        var metrics = drawObject.GetDefaultMetrics(30);
                        metrics.Scale = 2;
                        System.Drawing.Image BackgroundImage = drawObject.Draw(Convert.ToString(barCode), metrics);

                        ImageConverter converter = new ImageConverter();
                        byte[] byteImage;
                        byteImage = (byte[])converter.ConvertTo(BackgroundImage, typeof(byte[]));

                        for (int i = 0; i < BarcodeCount; i++)
                        {
                            drTb_BarcodeRow = dsReportData.tBarcode.NewtBarcodeRow();
                            drTb_BarcodeRow.MRPPrice = Convert.ToDecimal(drow["MRP"]);
                            if (ddlRate.SelectedValue == "WholeSale")
                                drTb_BarcodeRow.SellingPrice = Convert.ToDecimal(drow["WholeSalePrice"]);
                            else
                                drTb_BarcodeRow.SellingPrice = Convert.ToDecimal(drow["RetailPrice"]);

                            if (!string.IsNullOrEmpty(drow["PurchaseDate"].ToString()))
                                drTb_BarcodeRow.PurchaseDate = Convert.ToDateTime(drow["PurchaseDate"]);
                            else
                                drTb_BarcodeRow.PurchaseDate = Convert.ToDateTime("01/03/2021");

                            drTb_BarcodeRow.Code = "SMS";
                            drTb_BarcodeRow.Barcode = byteImage;
                            drTb_BarcodeRow.Barcodelabel = barCode;
                            if (ddlType.SelectedValue != "SMSCodeOnly")
                            {
                                drTb_BarcodeRow.TextCode = TextCodes;
                                drTb_BarcodeRow.PrintName = Convert.ToString(drow["CategoryName"]) + "/" + Convert.ToString(drow["SubCategoryName"]);
                                drTb_BarcodeRow.Minimum_DiscountPercentage = Convert.ToInt32(drow["Minimum_DiscountPercentage"]) + "-" + Convert.ToInt32(drow["Maximum_DiscountPercentage"]);
                                drTb_BarcodeRow.Maximum_DiscountPercentage = 0;
                            }

                            if (drTb_BarcodeRow.RowState == System.Data.DataRowState.Detached)
                                dsReportData.tBarcode.Rows.Add(drTb_BarcodeRow);
                        }
                    }
                }
            }
            rprt = new ReportDocument();
            if (ddlType.SelectedValue == "SMSCodeOnly")
                rprt.Load(Server.MapPath("~/Reports/rptSMSOnlyBarcode.rpt"));
            else
                rprt.Load(Server.MapPath("~/Reports/rptSMSreaderBarcode.rpt"));

            rprt.SetDataSource(dsReportData);
            this.CRDischargeSummaryReport.ReportSource = rprt;
            CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            CRDischargeSummaryReport.Zoom(100);
            this.CRDischargeSummaryReport.DataBind();
            ExportReport();
        }
        catch (Exception ex)
        {
            sException = "frmPrintJobCard | " + ex.ToString();
            Log.Write(sException);
        }
    }

    private void ExportReport()
    {
        ExportOptions exopt = default(ExportOptions);
        DiskFileDestinationOptions dfdopt = new DiskFileDestinationOptions();
        string fname = "barcode.pdf";
        dfdopt.DiskFileName = Server.MapPath(fname);

        exopt = rprt.ExportOptions;
        exopt.ExportDestinationType = ExportDestinationType.DiskFile;

        exopt.ExportFormatType = ExportFormatType.PortableDocFormat;
        exopt.DestinationOptions = dfdopt;

        rprt.Export();

        string Path = Server.MapPath(fname);
        Session["myFilePath"] = Path;
        Response.Write("<script>window.open ('ViewPDF.aspx?reportFile=" + Path + "','_blank');</script>");
    }

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow) { }
    }

    protected void RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
        txtBarcode.Text = (row.FindControl("txtBarcode") as TextBox).Text;
        txtSMSCode.Text = (row.FindControl("txtSMSCode") as TextBox).Text;

        txtCount.Text = (row.FindControl("txtQuantity") as TextBox).Text;
        btnGenrate_Click(sender, e);
        ExportReport();
    }

}