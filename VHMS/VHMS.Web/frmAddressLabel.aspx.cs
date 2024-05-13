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
using System.Text;
using System.Drawing;


public partial class frmAddressLabel : BaseConfig
{
    ReportDocument rprt = new ReportDocument();
    ReportDocument rprt1 = new ReportDocument();
    DataSet dsData = new DataSet();
    string iBarcode = ""; string iTable = ""; string fname = "AddressPrint.pdf";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            if (HttpContext.Current.Session["SalesID"] != null)
                iBarcode = HttpContext.Current.Session["SalesID"].ToString();
            else
                iBarcode = "0";

            Session["Barcode"] = null;

            if (HttpContext.Current.Session["Table"] != null)
                iTable = HttpContext.Current.Session["Table"].ToString();
            else
                iTable = "0";

            //Session["Table"] = null;
            btnGenrate.BackColor = Color.LightBlue;
            btnPrint.BackColor = Color.LightBlue;
            btnGenrate_Click(sender, e);
        }

        //test();
    }
    protected void btnGenrate_Click(object sender, EventArgs e)
    {
        //string barCode = txtAddressLabel.Text;

        //  VHMS.Entity.Customer objStock = new VHMS.Entity.Customer();
        //  objStock = VHMS.DataAccess.Customer.GetCustomerByCode(barCode);

        ////  decimal gross = objStock.NetWeight + objStock.StoneWeight;
        //lblName.Text = objStock.CustomerName + ",";
        //lblAddressLabel.Text =  objStock.Address;

        //VHMS.Entity.Company objCompany = new VHMS.Entity.Company();
        //objCompany = VHMS.DAL.Company.GetCompany();
        ////lblNetWt.Text = "NW:" + objStock.NetWeight.ToString("0.000");
        ////lblGrossWt.Text = "GW:" + gross.ToString("0.000");
        ////lblStoneWt.Text = "SW:" + objStock.StoneWeight.ToString("0.000");
        //lblCompanyName.Text = objCompany.CompanyName + ",";
        //lblCompanyAddress.Text = objCompany.CompanyAddress;

        if (iTable == "Customer")
        {
            ReportDatestNew dsReportData = new ReportDatestNew();
            VHMS.Entity.Customer objStock = new VHMS.Entity.Customer();
            objStock = VHMS.DataAccess.Customer.GetCustomerByID(Convert.ToInt32(iBarcode));

            txtAddressLabel.Text = objStock.MobileNo;

            dsData = VHMS.DataAccess.VHMSReports.PrintAddress(Convert.ToInt32(iBarcode));
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tCustomer";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tCustomer.ImportRow(drow);

                dsData.Tables[1].TableName = "tCompany";
                foreach (DataRow drow in dsData.Tables[1].Rows)
                    dsReportData.tCompany.ImportRow(drow);
            }
            rprt = new ReportDocument();
            if (!chkPartyCode.Checked)
                rprt.Load(Server.MapPath("~/Reports/RptCustomerAddressPrint.rpt"));
            else
                rprt.Load(Server.MapPath("~/Reports/RptCustomerAddresswithPhonenoPrint.rpt"));
            rprt.SetDataSource(dsReportData);
            this.CustomerAddress.ReportSource = rprt;
            CustomerAddress.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            CustomerAddress.Zoom(100);
            this.CustomerAddress.DataBind();

            ExportOptions exopt = default(ExportOptions);
            DiskFileDestinationOptions dfdopt = new DiskFileDestinationOptions();

            dfdopt.DiskFileName = Server.MapPath(fname);
            exopt = rprt.ExportOptions;
            exopt.ExportDestinationType = ExportDestinationType.DiskFile;
            exopt.ExportFormatType = ExportFormatType.PortableDocFormat;
            exopt.DestinationOptions = dfdopt;
            rprt.Export();
        }
        else
        {
            ReportDataSet dsReportData = new ReportDataSet();
            VHMS.Entity.Billing.Supplier objStock = new VHMS.Entity.Billing.Supplier();
            objStock = VHMS.DataAccess.Billing.Supplier.GetSupplierByID(Convert.ToInt32(iBarcode));

            txtAddressLabel.Text = objStock.PhoneNo1;

            dsData = VHMS.DataAccess.VHMSReports.PrintSupplierAddress(Convert.ToInt32(iBarcode));
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tSupplier";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tSupplier.ImportRow(drow);

                dsData.Tables[1].TableName = "tCompany";
                foreach (DataRow drow in dsData.Tables[1].Rows)
                    dsReportData.tCompany.ImportRow(drow);
            }
            rprt1 = new ReportDocument();
            if (!chkPartyCode.Checked)
                rprt1.Load(Server.MapPath("~/Reports/RptSupplierAddressPrint.rpt"));
            else
                rprt1.Load(Server.MapPath("~/Reports/RptSupplierAddressWithPhonenoPrint.rpt"));
            rprt1.SetDataSource(dsReportData);
            this.SupplierAddress.ReportSource = rprt1;
            SupplierAddress.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            SupplierAddress.Zoom(100);
            this.SupplierAddress.DataBind();
            ExportOptions exopt = default(ExportOptions);
            DiskFileDestinationOptions dfdopt = new DiskFileDestinationOptions();

            dfdopt.DiskFileName = Server.MapPath(fname);
            exopt = rprt1.ExportOptions;
            exopt.ExportDestinationType = ExportDestinationType.DiskFile;
            exopt.ExportFormatType = ExportFormatType.PortableDocFormat;
            exopt.DestinationOptions = dfdopt;
            rprt1.Export();
        }


        //rprt.ExportToHttpResponse
        //(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "PriceUpdateLogReport");
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["Table"] != null)
            iTable = HttpContext.Current.Session["Table"].ToString();
        else
            iTable = "0";

        if (iTable == "Customer")
                ExportReport();
        else
                ExportReport1();
           
    }
    private void ExportReport()
    {
        string Path = Server.MapPath(fname);
        Session["myFilePath"] = Path;
        Response.Write("<script>window.open ('ViewPDF.aspx?reportFile=" + Path + "','_blank');</script>");
    }
    private void ExportReport1()
    {
        string Path = Server.MapPath(fname);
        Session["myFilePath"] = Path;
        Response.Write("<script>window.open ('ViewPDF.aspx?reportFile=" + Path + "','_blank');</script>");
    }
    protected void chkPartyCode_CheckedChanged(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["SalesID"] != null)
            iBarcode = HttpContext.Current.Session["SalesID"].ToString();
        else
            iBarcode = "0";

        Session["Barcode"] = null;

        if (HttpContext.Current.Session["Table"] != null)
            iTable = HttpContext.Current.Session["Table"].ToString();
        else
            iTable = "0";
        btnGenrate_Click(sender, e);
    }
}