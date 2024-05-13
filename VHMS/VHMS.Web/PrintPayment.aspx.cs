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

public partial class PrintPayment : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            VHMSService.AddPageVisitLog();
            if (HttpContext.Current.Session["PrintPaymentID"] != null)
                hdnBillNo.Value = HttpContext.Current.Session["PrintPaymentID"].ToString();
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

        dsData = VHMS.DataAccess.VHMSReports.PrintPayment(Convert.ToInt32(hdnBillNo.Value));
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tPayment";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tPayment.ImportRow(drow);

                dsData.Tables[1].TableName = "tPaymentTrans";
                foreach (DataRow drow in dsData.Tables[1].Rows)
                    dsReportData.tPaymentTrans.ImportRow(drow);

                dsData.Tables[2].TableName = "tSupplier";
                foreach (DataRow drow in dsData.Tables[2].Rows)
                    dsReportData.tSupplier.ImportRow(drow);

                dsData.Tables[3].TableName = "tCompany";
                foreach (DataRow drow in dsData.Tables[3].Rows)
                    dsReportData.tCompany.ImportRow(drow);

                //dsData.Tables[4].TableName = "tTDSPaymentTrans";
                //foreach (DataRow drow in dsData.Tables[4].Rows)
                //    dsReportData.tTDSPaymentTrans.ImportRow(drow);
            }
            if (dsReportData.tPayment.Rows.Count > 0)
            {
                string TableHTML = ""; string CompanyName = ""; string iRate = ""; decimal amount = 0;
                foreach (ReportDataSet.tPaymentRow drtJobCardRow in dsReportData.tPayment)
                {
                    if (dsReportData.tCompany.Rows.Count > 0)
                    {
                        TableHTML += "<table cellpadding='0' cellspacing='0'>";
                        foreach (ReportDataSet.tCompanyRow drtBranchRow in dsReportData.tCompany)
                        {
                            CompanyName = drtBranchRow.CompanyName.ToString();
                            if (drtBranchRow.PhoneNo2.Length < 2)
                                TableHTML += "<tr valign='middle'><th rowspan='3' align='right' style='padding-left:25px;width:10%'><img src='images/meenakchi-amman-1.jpg' width='100%' alt=''/><th rowspan='3' align='right' style='padding-left:25px;width:15%'><img src='images/5.png' width='100%' alt=''/></th><th style='font-size:19px;text-align:center;width: 48%;height: 1%;'>" + drtBranchRow.CompanyName + "</th><th style='text-align:end;height: 1%;'> " + drtBranchRow.PhoneNo1 + "</th></tr>";
                            else
                                TableHTML += "<tr valign='middle'><th rowspan='3' align='right' style='padding-left:25px;width:10%'><img src='images/meenakchi-amman-1.jpg' width='100%' alt=''/><th rowspan='3' align='right' style='padding-left:25px;width:15%'><img src='images/5.png' width='100%' alt=''/></th><th style='font-size:19px;text-align:center;width: 48%;height: 1%;'>" + drtBranchRow.CompanyName + "</th><th style='text-align:end;height: 1%;'> " + drtBranchRow.PhoneNo1 + " / " + drtBranchRow.PhoneNo2 + "</th></tr>";

                            TableHTML += "<tr><th style='text-align:center;height: 1%;' >Pure Handloom Silk Sarees Manufacturer</th><th></th></tr>";

                            TableHTML += "<tr><th style='text-align:center;height: 1%;'>" + drtBranchRow.CompanyAddress + "</th><th style='text-align:end;height: 1%;'>" + drtBranchRow.CSTNo + " / State Code: 33 </th></tr>";
                            TableHTML += "<tr><th style='text-align:center;'Colspan='4'> Email : " + drtBranchRow.Email + "</th></tr>";
                        }
                        TableHTML += "</table>";

                    }

                    if (dsReportData.tSupplier.Rows.Count > 0)
                    {
                        TableHTML += "<table cellpadding='0' cellspacing='0' style='width: 80%;'><thead>";
                        foreach (ReportDataSet.tSupplierRow drtSupplierRow in dsReportData.tSupplier)
                        {
                            string Inv_No = drtJobCardRow.VoucherNo;

                            TableHTML += "<tr><td><b>Voucher No.:" + Inv_No + "</b></td><td style='width: 20%;'></td><td> Date:" + drtJobCardRow.VoucherDate.ToString("dd/MM/yyyy") + "</td></tr>";
                            TableHTML += "<tr><td><b>Mr./Ms." + drtSupplierRow.SupplierName + "</b></td><td style='width: 20%;'></td><td><b>Mobile No. : " + drtSupplierRow.PhoneNo1 + "</b></td></tr>";
                            TableHTML += "<tr><td style='width: 1px;'> " + drtSupplierRow.SupplierAddress + "</td><td></td></tr>";
                        }
                        TableHTML += "</table>";
                    }

                    TableHTML += "<table cellpadding='0' cellspacing='0' class='border_details' style='width: 80%;font-size: 12px !important;'><thead>";
                    TableHTML += "<tr><th style='width:10px;'><b>&nbsp S.No</b></th><th><b>Bill No.</b></th><th><b>Bill Date</b></th><th style='text-align:right'><b>Net Amount</b></th><th style='text-align:right'><b>Return Amount</b></th><th style='text-align:right'><b>TDS Amount</b></th><th style='text-align:right'><b>Amount Paid</b></th></tr></thead>";

                    if (dsReportData.tPaymentTrans.Rows.Count > 0)
                    {
                        int sno = 1;

                        foreach (ReportDataSet.tPaymentTransRow drtJobCardComplaintsRow in dsReportData.tPaymentTrans)
                        {
                            TableHTML += "<tr><td>&nbsp &nbsp" + sno + "</td><td>" + drtJobCardComplaintsRow.PurchaseNo + "</td><td>" + drtJobCardComplaintsRow.PurchaseDate.ToString("dd/MM/yyyy") + "</td><td style='text-align:right'>" + drtJobCardComplaintsRow.NetAmount + "</td><td style='text-align:right'>" + drtJobCardComplaintsRow.ReturnAmount + "</td><td style='text-align:right'>" + drtJobCardComplaintsRow.TDSAmount + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.Amount + "</td></tr>";
                            sno++;
                        }
                    }
                    TableHTML += "</table>";
                    TableHTML += "<table cellpadding='0' cellspacing='0' style='width: 80%;'>";
                    TableHTML += "<tr><td colspan='3' style='text-align:right'><b>Total Discount Amount :</b></td><td style='text-align:right'>" + drtJobCardRow.Discount_Amount + "</td></tr>";
                    TableHTML += "<tr><td colspan='3' style='text-align:right'><b>Courier/Other Charges :</b></td><td style='text-align:right'>" + drtJobCardRow.OtherDiscount + "</td></tr>";
                  //  TableHTML += "<tr><td colspan='3' style='text-align:right'><b>TDS Amount :</b></td><td style='text-align:right'>" + drtJobCardRow.TDSAmount + "</td></tr>";

                    TableHTML += "<tr><td colspan='3' style='text-align:right'><b>Total Amount Paid :</b></td><td style='text-align:right'>" + drtJobCardRow.Amount + "</td></tr>";
                    //decimal Total=
                    TableHTML += "</table>";

                    TableHTML += "<table cellpadding='0' cellspacing='0'style='width: 80%;'>";
                    if (drtJobCardRow.PaymentMode == "Cash")
                        TableHTML += "<tr><td><b>Paymend Mode : " + drtJobCardRow.PaymentMode + "</b></td><td</td></tr>";
                    else
                        TableHTML += "<tr><td><b>Payment Mode : " + drtJobCardRow.PaymentMode + " (" + drtJobCardRow.ChequeNo + ")</b></td><td</td></tr>";
                    TableHTML += "<tr><td><b>Collection Date : " + drtJobCardRow.CollectionDate.ToString("dd-MM-yyyy") + "</b></td><td</td></tr>";
                    TableHTML += "<tr><td><b>Rupees " + AmountInWords(Convert.ToInt32(drtJobCardRow.Amount)) + "</b></td><td</td></tr></table>";
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