﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Text;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Drawing.Printing;
using VHMS.DataAccess;


public partial class PrintPurchaseReturn : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    ReportDocument rprt = new ReportDocument();
    ReportDocument rprtTransport = new ReportDocument();
    string sException = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.btnExportReport);

        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            VHMSService.AddPageVisitLog();
            if (HttpContext.Current.Session["PurchaseReturnID"] != null)
                hdnPurchaseReturn.Value = HttpContext.Current.Session["PurchaseReturnID"].ToString();
            else
                hdnPurchaseReturn.Value = "-1";
        }
        //txtDOR.Text = Convert.ToDateTime(DateTime.Now).ToString("MMMM-yyyy");
        LoadReport();

    }
    private void LoadReport()
    {
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        Boolean IsDiscount = false;

        // dsData = VHMS.DataAccess.VHMSReports.PrintPurchaseEntryInvoice(Convert.ToInt32(hdnPurchaseNo.Value));
        string sTemp = string.Empty;
        try
        {

            ReportDataSet dsReportData = new ReportDataSet();
            dsReportData = LoadData();

            if (dsReportData.tPurchaseReturn.Rows.Count > 0)
            {

                foreach (ReportDataSet.tPurchaseReturnRow drtBranchRow in dsReportData.tPurchaseReturn)
                {
                    IsDiscount = drtBranchRow.IsDiscount;
                }
            }
            rprt = new ReportDocument();
            if (IsDiscount == false)
            {
                if (!chkPartyCode.Checked)
                    rprt.Load(Server.MapPath("~/Reports/rptPurchaseReturnWithOutPartycode.rpt"));
                else
                    rprt.Load(Server.MapPath("~/Reports/rptPurchaseReturnwithPartycode.rpt"));
            }
            else
            {
                if (!chkPartyCode.Checked)
                    rprt.Load(Server.MapPath("~/Reports/rptPurchaseReturnWithOutPartycodeIsDiscount.rpt"));
                else
                    rprt.Load(Server.MapPath("~/Reports/rptPurchaseReturnwithPartycodeIsDiscount.rpt"));
            }
            rprt.SetDataSource(dsReportData);
            this.CRDischargeSummaryReport.ReportSource = rprt;
            CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            CRDischargeSummaryReport.Zoom(100);
            this.CRDischargeSummaryReport.DataBind();
            // ExportReport();
               rprtTransport = new ReportDocument();
            rprtTransport.Load(Server.MapPath("~/Reports/rptPurchaseReturnTransportcopy.rpt"));
            rprtTransport.SetDataSource(dsReportData);
            this.CRTransport.ReportSource = rprtTransport;
            CRTransport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            CRTransport.Zoom(100);
            this.CRTransport.DataBind();
        }

        catch (Exception ex)
        {
            sException = "frmPrintJobCard | " + ex.ToString();
            Log.Write(sException);
        }
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rprt.Close();
        rprt.Dispose();
    }
    private ReportDataSet LoadData()
    {
        dsData = VHMS.DataAccess.VHMSReports.PrintPurchaseReturnInvoice(Convert.ToInt32(hdnPurchaseReturn.Value));
        ReportDataSet dsReportData = new ReportDataSet();

        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "tPurchaseReturn";
            foreach (DataRow drow in dsData.Tables[0].Rows) dsReportData.tPurchaseReturn.ImportRow(drow);
            dsData.Tables[1].TableName = "tPurchaseReturnTrans";
            foreach (DataRow drow in dsData.Tables[1].Rows) dsReportData.tPurchaseReturnTrans.ImportRow(drow);
            dsData.Tables[2].TableName = "tCompany";
            foreach (DataRow drow in dsData.Tables[2].Rows) dsReportData.tCompany.ImportRow(drow);

        }
        return dsReportData;
    }

    protected void btnExportReport_Click(object sender, EventArgs e)
    {
        try
        {
            ExportReport();
        }
        catch (Exception ex)
        { Log.Write("RepViewDischargeSummary btnExportReport_Click | " + ex.ToString()); }
    }

    private void ExportReport()
    {
        ExportOptions exopt = default(ExportOptions);
        DiskFileDestinationOptions dfdopt = new DiskFileDestinationOptions();
        string fname = "PurchaseReturn.pdf";
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
    protected void chkPartyCode_CheckedChanged(object sender, EventArgs e)
    {
        LoadReport();
    }
    protected void Page_UnLoad()
    {
        if (rprt != null)
        {
            rprt.Dispose();
            rprt.Clone();
            rprt.Close();
            CRDischargeSummaryReport.Dispose();
        }
        if (rprtTransport != null)
        {
            rprtTransport.Dispose();
            rprtTransport.Clone();
            rprtTransport.Close();
            CRTransport.Dispose();
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

        //if (chkOriginal.Checked)
        //{
        //    CRDischargeSummaryReport.ReportSource = rprt;
        //    PrinterSettings getprinterName = new PrinterSettings();
        //    rprt.PrintOptions.PrinterName = getprinterName.PrinterName;
        //    rprt.PrintToPrinter(1, false, 0, 0);
        //    this.CRDischargeSummaryReport.RefreshReport();
        //}
        //if (chkTransport.Checked)
        //{
        //    CRTransport.ReportSource = rprtTransport;
        //    PrinterSettings getprinterName2 = new PrinterSettings();
        //    rprtTransport.PrintOptions.PrinterName = getprinterName2.PrinterName;
        //    rprtTransport.PrintToPrinter(1, false, 0, 0);
        //    this.CRTransport.RefreshReport();
        }

    }
