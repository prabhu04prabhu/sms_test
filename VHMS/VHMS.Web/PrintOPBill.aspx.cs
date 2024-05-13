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

public partial class PrintOPBill : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            if (HttpContext.Current.Session["OPID"] != null)
                hdnBillNo.Value = HttpContext.Current.Session["OPID"].ToString();
            else
                hdnBillNo.Value = "-1";
        }
        LoadReport();
        btnPrint_Click(sender,e);
    }

    private void LoadReport()
    {
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        ReportDataSet dsReportData = new ReportDataSet();

        dsData = VHMS.DataAccess.VHMSReports.PrintOPBill(Convert.ToInt32(hdnBillNo.Value), null);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tOPBillingMaster";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tOPBillingMaster.ImportRow(drow);

                dsData.Tables[1].TableName = "tOPBillingTrans";
                foreach (DataRow drow in dsData.Tables[1].Rows)
                    dsReportData.tOPBillingTrans.ImportRow(drow);

                dsData.Tables[2].TableName = "tPatient";
                foreach (DataRow drow in dsData.Tables[2].Rows)
                    dsReportData.tPatient.ImportRow(drow);

                dsData.Tables[3].TableName = "tCompany";
                foreach (DataRow drow in dsData.Tables[3].Rows)
                    dsReportData.tCompany.ImportRow(drow);

            }
            if (dsReportData.tOPBillingMaster.Rows.Count > 0)
            {
                string TableHTML = "";
                foreach (ReportDataSet.tOPBillingMasterRow drtJobCardRow in dsReportData.tOPBillingMaster)
                {
                    if (dsReportData.tCompany.Rows.Count > 0)
                    {
                        TableHTML += "<table cellpadding='0' cellspacing='0'>";
                        foreach (ReportDataSet.tCompanyRow drtBranchRow in dsReportData.tCompany)
                        {                            
                            TableHTML += "<tr valign='middle'><th rowspan='3' align='right' style='padding-left:25px;width:20%'><img src='images/aks.png' width='100%' alt=''/></th><th style='font-size:19px;text-align:center;'>" + drtBranchRow.CompanyName + "</th></tr>";
                            TableHTML += "<tr><th style='text-align:center;'>" + drtBranchRow.CompanyAddress + "</th></tr>";
                            if(drtBranchRow.PhoneNo2.Length>2)
                            TableHTML += "<tr><th style='text-align:center;'> Phone :" + drtBranchRow.PhoneNo1 + "/" + drtBranchRow.PhoneNo2 + "</th></tr>";  
                            else
                                TableHTML += "<tr><th style='text-align:center;'> Phone :" + drtBranchRow.PhoneNo1 + "</th></tr>";
                        }
                        TableHTML += "</table>";

                    }
                    TableHTML += "<table cellpadding='0' cellspacing='0'><thead><tr><th style='text-align:center;'>Bill Receipt</th></tr></table>";
                    if (dsReportData.tPatient.Rows.Count > 0)
                    {
                        TableHTML += "<table cellpadding='0' cellspacing='0'><thead>";
                        foreach (ReportDataSet.tPatientRow drtCustomerRow in dsReportData.tPatient)
                        {
                            TableHTML += "<tr><td><b>OPD No</b></td><td> : " + drtCustomerRow.OPDNo + "</td><td><b> Date </b></td><td> : " + drtJobCardRow.BillDate.ToString("dd/MM/yyyy") + "</td></tr>";
                            TableHTML += "<tr><td><b>Bill No</b></td><td> : " + drtJobCardRow.BillNo + "</td><td><b> Doctor Name </b></td><td> : " + drtJobCardRow.DoctorName + "</td></tr>";
                            TableHTML += "<tr><td><b>Patient Name</b></td><td> : " + drtCustomerRow.WName + "</td><td><b>Specialization </b></td><td> : " + drtJobCardRow.Specialization + "</td></tr>";
                            //TableHTML += "<tr><td><b>Husband/Father Name</b></td><td> : " + drtCustomerRow.HName + "</td><td></td></tr>";
                        }
                        TableHTML += "</table>";
                    }
                    
                    TableHTML += "<table cellpadding='0' cellspacing='0' class='border_details'><thead>";
                    TableHTML += "<tr><th style='width:10px;'><b>&nbsp S.No</b></th><th><b>Description</b></th><th  style='text-align:right'><b>Charges</b></th><th  style='text-align:right'><b>Amount</b></th></tr></thead>";
                   
                    if (dsReportData.tOPBillingTrans.Rows.Count > 0)
                    {
                        int sno = 1;
                        
                        foreach (ReportDataSet.tOPBillingTransRow drtJobCardComplaintsRow in dsReportData.tOPBillingTrans)
                        {
                            TableHTML += "<tr><td>&nbsp &nbsp" + sno + "</td><td>" + drtJobCardComplaintsRow.DescriptionName + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.Charges + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.Subtotal + "</td></tr>";
                            sno++;
                        }
                    }
                    TableHTML += "</table>";
                    TableHTML += "<table cellpadding='0' cellspacing='0'>";
                    TableHTML += "<tr><td colspan='3' style='text-align:right'><b>Total Amount</b></td><td style='text-align:right'>" + drtJobCardRow.TotalAmount + "</td></tr>";
                    TableHTML += "<tr><td colspan='3' style='text-align:right'><b>Dicount Amount</b></td><td style='text-align:right'>" + drtJobCardRow.DiscountAmount + "</td></tr>";
                    TableHTML += "<tr><td colspan='3' style='text-align:right'><b>Net Amount</b></td><td style='text-align:right'>" + drtJobCardRow.NetAmount + "</td></tr>";
                    //TableHTML += "<tr><td colspan='3' style='text-align:right'><b>Paid Amount</b></td><td style='text-align:right'>" + drtJobCardRow.PaidAmount + "</td></tr>";
                    //TableHTML += "<tr><td colspan='3' style='text-align:right'><b>Balance Amount</b></td><td style='text-align:right'>" + drtJobCardRow.BalanceAmount + "</td></tr>";
                    TableHTML += "</table>";
                    TableHTML += "<table cellpadding='0' cellspacing='0'>";
                    TableHTML += "<tr><td style='width:30%'><b>Pay Mode</b></td><td><b>" + drtJobCardRow.PaymentMode + "</b></td></tr>";
                    TableHTML += "<tr><td><b>Rupees</b></td><td><b>" + AmountInWords(Convert.ToInt32(drtJobCardRow.NetAmount)) + "</b></td></tr>";
                    TableHTML += "<tr><th colspan='2' ></th></tr>";
                    TableHTML += "<tr><td colspan='2'><b>Authorized By</b><br/><br/><br/></td></tr>";
                    TableHTML += "<tr><td style='text-align:left'><b>Prepared by " + drtJobCardRow.CUserName + "</b></td><td style='text-align:right'><b>Print on " + DateTime.Now.ToString("dddd") +", "+ DateTime.Now + "</b></td></tr></table>";
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