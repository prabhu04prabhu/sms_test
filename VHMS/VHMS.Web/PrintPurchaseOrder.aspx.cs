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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class PrintPurchaseOrder : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    ReportDocument rprt = new ReportDocument();
    string sException = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            if (HttpContext.Current.Session["PurchaseOrderID"] != null)
                hdnBillNo.Value = HttpContext.Current.Session["PurchaseOrderID"].ToString();
            else
                hdnBillNo.Value = "-1";
        }
        LoadReport();
        ExportReport();
        //btnPrint_Click(sender, e);
    }
    private void ExportReport()
    {
        ExportOptions exopt = default(ExportOptions);
        DiskFileDestinationOptions dfdopt = new DiskFileDestinationOptions();
        string fname = "PurchaseOrderce.pdf";
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
    private void LoadReport()
    {
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        try
        {
            ReportDataSet dsReportData = new ReportDataSet();
            dsReportData = LoadData();
            rprt = new ReportDocument();
            rprt.Load(Server.MapPath("~/Reports/rptPurchaseOrdeReport.rpt"));

            rprt.SetDataSource(dsReportData);
            this.CRDischargeSummaryReport.ReportSource = rprt;
            CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            CRDischargeSummaryReport.Zoom(100);
            this.CRDischargeSummaryReport.DataBind();
            //    if (dsData.Tables.Count > 0)
            //    {
            //        dsData.Tables[0].TableName = "tPurchaseOrder";
            //        foreach (DataRow drow in dsData.Tables[0].Rows)
            //            dsReportData.tPurchaseOrder.ImportRow(drow);

            //        dsData.Tables[1].TableName = "tPurchaseOrderTrans";
            //        foreach (DataRow drow in dsData.Tables[1].Rows)
            //            dsReportData.tPurchaseOrderTrans.ImportRow(drow);

            //        dsData.Tables[2].TableName = "tSupplier";
            //        foreach (DataRow drow in dsData.Tables[2].Rows)
            //            dsReportData.tSupplier.ImportRow(drow);

            //        dsData.Tables[3].TableName = "tCompany";
            //        foreach (DataRow drow in dsData.Tables[3].Rows)
            //            dsReportData.tCompany.ImportRow(drow);
            //    }
            //    if (dsReportData.tPurchaseOrder.Rows.Count > 0)
            //    {
            //        string TableHTML = ""; string CompanyName = ""; decimal gRate = 0; decimal SRate = 0; decimal amount = 0;
            //        foreach (ReportDataSet.tPurchaseOrderRow drtJobCardRow in dsReportData.tPurchaseOrder)
            //        {
            //            if (dsReportData.tCompany.Rows.Count > 0)
            //            {
            //                TableHTML += "<table cellpadding='0' cellspacing='0'>";
            //                foreach (ReportDataSet.tCompanyRow drtBranchRow in dsReportData.tCompany)
            //                {
            //                    CompanyName = drtBranchRow.CompanyName;
            //                    TableHTML += "<tr valign='middle'><th rowspan='3' align='right' style='padding-left:25px;width:20%'><img src='images/aks.png' width='100%' alt=''/></th><th style='font-size:19px;text-align:center;'>" + drtBranchRow.CompanyName + "</th></tr>";
            //                    TableHTML += "<tr><th style='text-align:center;'>" + drtBranchRow.CompanyAddress + "</th></tr>";
            //                    if (drtBranchRow.PhoneNo2.Length > 2)
            //                        TableHTML += "<tr><th style='text-align:center;'> Phone :" + drtBranchRow.PhoneNo1 + "/" + drtBranchRow.PhoneNo2 + "</th></tr>";
            //                    else
            //                        TableHTML += "<tr><th style='text-align:center;'> Phone :" + drtBranchRow.PhoneNo1 + "</th></tr>";
            //                }
            //                TableHTML += "</table>";

            //            }

            //            if (dsReportData.tSupplier.Rows.Count > 0)
            //            {
            //                TableHTML += "<table cellpadding='0' cellspacing='0' style='width: 67%;'><thead>";

            //                foreach (ReportDataSet.tSupplierRow drtSupplierRow in dsReportData.tSupplier)
            //                {
            //                    TableHTML += "<tr><td><b>Mr./Ms." + drtSupplierRow.SupplierName + "</b></td><td></td><td><td style='text-align:right;'><b>PO.No :" + drtJobCardRow.PurchaseOrderNo + "</b></td></tr>";
            //                    TableHTML += "<tr><td><b>" + drtSupplierRow.PhoneNo1 + "</b></td><td></td><td></td><td style='text-align:right;'><b>Date  :" + drtJobCardRow.PurchaseOrderDate.ToString("dd/MM/yyyy") + "</b></td></tr>";
            //                    if (!String.IsNullOrEmpty(drtSupplierRow.Fax) && drtSupplierRow.Fax.Length > 9)
            //                        TableHTML += "<tr><td><b>" + drtSupplierRow.SupplierAddress + "</b></td><td></td><td><b>GST No. : " + drtSupplierRow.Fax + "</b></td></tr>";
            //                   else
            //                        TableHTML += "<tr><td><b>" + drtSupplierRow.SupplierAddress + "</b></td><td></td><td></td></tr>";

            //                }
            //                TableHTML += "</table>";
            //            }

            //            TableHTML += "<table cellpadding='0' cellspacing='0' class='border_details' style='width: 85%;font-size: 12px !important;'><thead>";
            //            TableHTML += "<tr><th style='width:10px;'><b>&nbsp S.No</b></th><th><b>Description</b></th></th><th><b>Code</b></th><th style='text-align:right'><b>Qty</b></th><th   style='text-align:right'><b>Rate</b></th><th   style='text-align:right'><b>Subtotal</b></th></tr></thead>";
            //            decimal Subtotal = 0;
            //            if (dsReportData.tPurchaseOrderTrans.Rows.Count > 0)
            //            {
            //                int sno = 1;

            //                foreach (ReportDataSet.tPurchaseOrderTransRow drtJobCardComplaintsRow in dsReportData.tPurchaseOrderTrans)
            //                {
            //                    TableHTML += "<tr><td>&nbsp &nbsp" + sno + "</td><td>" + drtJobCardComplaintsRow.SupplierProductName  + "</td><td>" + drtJobCardComplaintsRow.ProductCode + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.Quantity + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.Rate + "</td><td  style='text-align:right'>" + (drtJobCardComplaintsRow.Rate * drtJobCardComplaintsRow.Quantity).ToString("0.00") + "</td></tr>";
            //                    Subtotal += drtJobCardComplaintsRow.Rate * drtJobCardComplaintsRow.Quantity;
            //                    sno++;
            //                }
            //            }
            //            TableHTML += "</table>";
            //            TableHTML += "<table cellpadding='0' cellspacing='0' style='width: 85%;'>";
            //            TableHTML += "<tr><td colspan='3' style='text-align:right'><b></b></td><td style='text-align:right'>Total Amount  :" + Subtotal + "</td></tr>";
            //            TableHTML += "<tr><td></td></tr>";
            //            TableHTML += "<tr><td></td><td style='text-align:right' colspan='3'><b>Authorized By</b></td></tr>";
            //            TableHTML += "<tr><td></td></tr>";
            //            TableHTML += "<tr><td></td></tr>";
            //            TableHTML += "<tr><td></td></tr>";
            //            TableHTML += "<tr><td></td><td style='text-align:right;width:40%;' colspan='3'><b>For " + CompanyName + "</b></td></tr>";
            //            TableHTML += "</table>";

            //        }

            //        divOPInvoice.InnerHtml = TableHTML;
            //    }

        }
        catch (Exception ex)
        {
            sException = "frmPrintJobCard | " + ex.ToString();
            Log.Write(sException);
        }
    }
    private ReportDataSet LoadData()
    {
        dsData = VHMS.DataAccess.VHMSReports.PrintPurchaseOrder(Convert.ToInt32(hdnBillNo.Value));
        ReportDataSet dsReportData = new ReportDataSet();

        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "tPurchaseOrder";
            foreach (DataRow drow in dsData.Tables[0].Rows) dsReportData.tPurchaseOrder.ImportRow(drow);
            dsData.Tables[1].TableName = "tPurchaseOrderTrans";
            foreach (DataRow drow in dsData.Tables[1].Rows) dsReportData.tPurchaseOrderTrans.ImportRow(drow);
            dsData.Tables[2].TableName = "tSupplier";
            foreach (DataRow drow in dsData.Tables[2].Rows) dsReportData.tSupplier.ImportRow(drow);
            dsData.Tables[3].TableName = "tCompany";
            foreach (DataRow drow in dsData.Tables[3].Rows) dsReportData.tCompany.ImportRow(drow);

        }
        return dsReportData;
    }

    public string AmountInWords(int numbers)
    {
        int number = numbers;

        if (number == 0) return "Zero";
        if (number == -2147483648) return "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight";
        int[] num = new int[4];
        int first = 0;
        int u, h, t;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        if (number < 0)
        {
            sb.Append("Minus ");
            number = -number;
        }
        string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
"Five " ,"Six ", "Seven ", "Eight ", "Nine "};
        string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
"Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
        string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
"Seventy ","Eighty ", "Ninety "};
        string[] words3 = { "Thousand ", "Lakh ", "Crore " };
        num[0] = number % 1000; // units
        num[1] = number / 1000;
        num[2] = number / 100000;
        num[1] = num[1] - 100 * num[2]; // thousands
        num[3] = number / 10000000; // crores
        num[2] = num[2] - 100 * num[3]; // lakhs
        for (int i = 3; i > 0; i--)
        {
            if (num[i] != 0)
            {
                first = i;
                break;
            }
        }
        for (int i = first; i >= 0; i--)
        {
            if (num[i] == 0) continue;
            u = num[i] % 10; // ones
            t = num[i] / 10;
            h = num[i] / 100; // hundreds
            t = t - 10 * h; // tens
            if (h > 0) sb.Append(words0[h] + "Hundred ");
            if (u > 0 || t > 0)
            {
                if (h > 0 || i == 0) sb.Append("and ");
                if (t == 0)
                    sb.Append(words0[u]);
                else if (t == 1)
                    sb.Append(words1[u]);
                else
                    sb.Append(words2[t - 2] + words0[u]);
            }
            if (i != 0) sb.Append(words3[i - 1]);
        }
        return sb.ToString().TrimEnd();
    }
    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    Response.Clear();
    //    Response.ContentType = "text/html";
    //    var strBuilder = new StringBuilder();
    //    var strWriter = new StringWriter(strBuilder);
    //    var htmlWriter = new HtmlTextWriter(strWriter);
    //    var streamWriter = new StreamWriter(Response.OutputStream);
    //    var javascript = @"<script type='text/javascript'>window.onload = function() {setTimeout(function () { window.print(); }, 2000);}</script>";
    //    var stylecss = @"<link href='css/Print.css' rel='stylesheet type='text/css' /><link href='css/fonts/font-awesome.min.css' rel='stylesheet type='text/css' />";
    //    htmlWriter.Write(javascript);
    //    htmlWriter.Write(stylecss);
    //    divPdf.RenderControl(htmlWriter);
    //    streamWriter.Write(strBuilder.ToString());
    //    streamWriter.Flush();
    //    Response.End();
    //}
}