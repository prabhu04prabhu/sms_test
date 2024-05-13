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

public partial class PrintExchangeInvoice : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            if (HttpContext.Current.Session["ExchangeID"] != null)
                hdnBillNo.Value = HttpContext.Current.Session["ExchangeID"].ToString();
            else
                hdnBillNo.Value = "-1";
        }
        LoadReport();
        btnPrint_Click(sender, e);
    }

    private void LoadReport()
    {
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        ReportDataSet dsReportData = new ReportDataSet();

        dsData = VHMS.DataAccess.VHMSReports.PrintExchangeInvoice(Convert.ToInt32(hdnBillNo.Value), null);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tExchange";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tExchange.ImportRow(drow);

                dsData.Tables[1].TableName = "tExchangeTrans";
                foreach (DataRow drow in dsData.Tables[1].Rows)
                    dsReportData.tExchangeTrans.ImportRow(drow);

                dsData.Tables[2].TableName = "tCustomer";
                foreach (DataRow drow in dsData.Tables[2].Rows)
                    dsReportData.tCustomer.ImportRow(drow);

                dsData.Tables[3].TableName = "tCompany";
                foreach (DataRow drow in dsData.Tables[3].Rows)
                    dsReportData.tCompany.ImportRow(drow);

            }
            if (dsReportData.tExchange.Rows.Count > 0)
            {
                string TableHTML = ""; string CompanyName = ""; decimal iExchangeWeight = 0;
                foreach (ReportDataSet.tExchangeRow drtJobCardRow in dsReportData.tExchange)
                {
                    if (dsReportData.tCompany.Rows.Count > 0)
                    {
                        //TableHTML += "<table cellpadding='0' cellspacing='0' style='margin-left: 113px;'>";
                        TableHTML += "<table cellpadding='0' cellspacing='0' style='margin-top: 195px;'>";
                        foreach (ReportDataSet.tCompanyRow drtBranchRow in dsReportData.tCompany)
                        {
                            CompanyName = drtBranchRow.CompanyName.ToString();
                            //TableHTML += "<tr valign='middle'><th rowspan='3' align='right' style='padding-left:25px;width:20%'><img src='images/Dummy.jpg' width='100%' alt=''/></th><th style='font-size:19px;text-align:center;'>" + drtBranchRow.CompanyName + "</th></tr>";
                            //TableHTML += "<tr><th style='text-align:center;'>" + drtBranchRow.CompanyAddress + "</th></tr>";
                            //if (drtBranchRow.PhoneNo2.Length > 2)
                            //    TableHTML += "<tr><th style='text-align:center;'> Phone :" + drtBranchRow.PhoneNo1 + "/" + drtBranchRow.PhoneNo2 + "</th></tr>";
                            //else
                            //    TableHTML += "<tr><th style='text-align:center;'> Phone :" + drtBranchRow.PhoneNo1 + "</th></tr>";
                            //TableHTML += "<tr><th style='text-align:center;'> TIN No :" + drtBranchRow.TINNo + "</th></tr>";
                            //TableHTML += "<tr><th style='text-align:center;'> CST No :" + drtBranchRow.CSTNo + "</th></tr>";
                        }
                        TableHTML += "</table>";

                    }

                    foreach (ReportDataSet.tExchangeTransRow drtETransRow in dsReportData.tExchangeTrans)
                    {
                        iExchangeWeight = iExchangeWeight + drtETransRow.GrossWeight;
                    }
                    TableHTML += "<table cellpadding='0' cellspacing='0' style='width: 65%;'><thead><tr><th style='text-align:center;'>Exchange Invoice</th></tr></table>";
                    if (dsReportData.tCustomer.Rows.Count > 0)
                    {
                        TableHTML += "<table cellpadding='0' cellspacing='0' style='width: 67%;'><thead>";

                        //foreach (ReportDataSet.tRateRow drtRate in dsReportData.tRate)
                        //{
                        //    iRate = " Gold= Rs." + drtRate.Gold_22Sales + "   Silver= Rs." + drtRate.SilverSales;

                        //}
                        foreach (ReportDataSet.tCustomerRow drtCustomerRow in dsReportData.tCustomer)
                        {
                            TableHTML += "<tr><td><b>Mr./Ms." + drtCustomerRow.CustomerName + "</b></td><td></td><td></td><td><b>Invoice No. </b></td><td> : " + drtJobCardRow.ExchangeNo + "</td></tr>";
                            TableHTML += "<tr><td> " + drtCustomerRow.Address + "</td><td></td><td></td><td><b> Date </b></td><td> : " + drtJobCardRow.ExchangeDate.ToString("dd/MM/yyyy") + "</td></tr>";
                            TableHTML += "<tr><td><b>Mobile No. : " + drtCustomerRow.MobileNo + "</b></td><td><b></b></td><td></td><td></td></tr>";

                        }
                        TableHTML += "</table>";
                    }

                    TableHTML += "<table cellpadding='0' cellspacing='0' class='border_details' style='width: 65%;font-size: 12px !important;'><thead>";
                    TableHTML += "<tr><th style='width:10px;'><b>&nbsp S.No</b></th><th><b>Description</b></th><th   style='text-align:right'><b>Net.Wt</b></th></th><th   style='text-align:right'><b>Grs.Wt</b></th><th   style='text-align:right'><b>Stn.Wt</b></th><th   style='text-align:right'><b>Melt.Wt</b></th><th   style='text-align:right'><b>Karat</b><th   style='text-align:right'><b>Rate</b></th><th   style='text-align:right'><b>Total</b></th></tr></thead>";

                    if (dsReportData.tExchangeTrans.Rows.Count > 0)
                    {
                        int sno = 1;

                        foreach (ReportDataSet.tExchangeTransRow drtJobCardComplaintsRow in dsReportData.tExchangeTrans)
                        {
                            TableHTML += "<tr><td>&nbsp &nbsp" + sno + "</td><td>" + drtJobCardComplaintsRow.CategoryName + "-" + drtJobCardComplaintsRow.ProductName + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.NetWeight + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.GrossWeight + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.StoneWeight + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.MeltingWeight + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.Karat + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.CurrentRate + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.Amount + "</td></tr>";
                            sno++;
                        }
                    }
                    TableHTML += "</table>";
                    TableHTML += "<table cellpadding='0' cellspacing='0' style='width: 65%;'>";
                    TableHTML += "<tr><td colspan='3' style='text-align:right'><b>Total Amount</b></td><td style='text-align:right'>" + drtJobCardRow.TotalAmount + "</td></tr>";
                    TableHTML += "<tr><td colspan='3' style='text-align:right'><b>CGST (" + drtJobCardRow.CGSTPercent + "%)</b></td><td style='text-align:right'>" + drtJobCardRow.CGSTAmount + "</td></tr>";
                    TableHTML += "<tr><td colspan='3' style='text-align:right'><b>SGST (" + drtJobCardRow.SGSTPercent + "%)</b></td><td style='text-align:right'>" + drtJobCardRow.SGSTAmount + "</td></tr>";
                    
                    TableHTML += "<tr><td colspan='3' style='text-align:right'><b>Net Amount</b></td><td style='text-align:right'>" + drtJobCardRow.NetAmount + "</td></tr>";
                    TableHTML += "</table>";
                    TableHTML += "<table cellpadding='0' cellspacing='0' style='width: 65%;'>";
                    TableHTML += "<tr><td><b>Rupees</b></td><td><b>" + AmountInWords(Convert.ToInt32(drtJobCardRow.NetAmount)) + "</b></td></tr>";
                    TableHTML += "<tr><th colspan='2' ></th></tr>";
                    TableHTML += "<tr><th colspan='3' ></th></tr>";
                    TableHTML += "<tr><td style='text-align:right' colspan='3'><b>Authorized By</b><br/><br/><br/></td></tr>";
                    //TableHTML += "<tr><td style='text-align:left' colspan='2'>Customer's Signature</td><td style='text-align:right;'><b>For " + CompanyName + "</b></td></tr></table>";
                    TableHTML += "<tr><td style='text-align:left' colspan='3'><table style='width:100% !important;margin:0% !important;'><tr><td style='text-align:left;width:60%;'>Customer's Signature</td><td style='text-align:right;width:40%;'><b>For " + CompanyName + "</b></td></tr></table></td></tr></table>";
                }

                divOPInvoice.InnerHtml = TableHTML;
            }

        }
        catch (Exception ex)
        {
            sException = "frmPrintJobCard | " + ex.ToString();
            Log.Write(sException);
        }
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.ContentType = "text/html";
        var strBuilder = new StringBuilder();
        var strWriter = new StringWriter(strBuilder);
        var htmlWriter = new HtmlTextWriter(strWriter);
        var streamWriter = new StreamWriter(Response.OutputStream);
        var javascript = @"<script type='text/javascript'>window.onload = function() {setTimeout(function () { window.print(); }, 2000);}</script>";
        var stylecss = @"<link href='css/Print.css' rel='stylesheet type='text/css' /><link href='css/fonts/font-awesome.min.css' rel='stylesheet type='text/css' />";
        htmlWriter.Write(javascript);
        htmlWriter.Write(stylecss);
        divPdf.RenderControl(htmlWriter);
        streamWriter.Write(strBuilder.ToString());
        streamWriter.Flush();
        Response.End();
    }
}