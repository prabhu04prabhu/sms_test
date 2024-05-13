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
public partial class frmSalesBill : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    DataSet dsSales = new DataSet();
    DataSet dsCustomer = new DataSet();
    string sException = string.Empty;
    decimal InvoiceAmount = 0, NetAmount = 0, DiscountAmount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            // LoadCustomer();
            LoadBranch();
            txtDOB.Text = Convert.ToDateTime(DateTime.Now.AddDays(-7)).ToString("dd/MM/yyyy");
            txtDOR.Text = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy");
        }
    }
    protected void ddlPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        // LoadReport();
    }
    //protected void BindData()
    //{
    //    DataTable result = (DataTable)Session["tSales"];
    //    if (result.Rows.Count > 0)
    //    {
    //        gvSales.DataSource = result;
    //        gvSales.DataBind();
    ////    }
    //  }

    //protected void btnExcel_Click(object sender, EventArgs e)
    //{
    //    if (gvSales.Rows.Count < 1)
    //    {
    //        string display = "no Records Found.";
    //        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
    //    }
    //    else
    //    {
    //        //Response.ClearContent();
    //        //Response.AppendHeader("content-disposition", "attachment; filename=SalesDetails.xls");
    //        //Response.ContentType = "application/excel";

    //        //StringWriter stringwriter = new StringWriter();
    //        //HtmlTextWriter htmltextwriter = new HtmlTextWriter(stringwriter);

    //        //gvSales.HeaderRow.Style.Add("background-color", "#FFFFFF");

    //        //foreach (TableCell tableCell in gvSales.HeaderRow.Cells)
    //        //{
    //        //    tableCell.Style["background-color"] = "#d8d4d4";
    //        //}

    //        //foreach (GridViewRow gridViewRow in gvSales.Rows)
    //        //{
    //        //    gridViewRow.BackColor = System.Drawing.Color.White;
    //        //    foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
    //        //    {
    //        //        gridViewRowTableCell.Style["background-color"] = "#FFFFFF";
    //        //    }
    //        //}
    //        //gvSales.RenderControl(htmltextwriter);
    //        //Response.Write(stringwriter.ToString());
    //        //Response.End();
    //        try
    //        {
    //            this.gvSales.AllowPaging = false;
    //            this.gvSales.AllowSorting = false;
    //            this.gvSales.EditIndex = -1;

    //            // Let's bind data to GridView
    //           // this.BindData();

    //            // Let's output HTML of GridView
    //            Response.Clear();
    //            Response.ContentType = "application/vnd.xls";
    //            Response.AddHeader("content-disposition","attachment;filename=SalesReports.xls");
    //            Response.Charset = "";
    //            StringWriter swriter = new StringWriter();
    //            HtmlTextWriter hwriter = new HtmlTextWriter(swriter);
    //            gvSales.RenderControl(hwriter);
    //            Response.Write(swriter.ToString());
    //            Response.End();
    //        }
    //        catch (Exception exe)
    //        {
    //            throw exe;
    //        }
    //    }
    //}
    protected void btnView_Click(object sender, EventArgs e)
    {

       
        LoadReport();

    }
    //protected void btnPDF_Click(object sender, EventArgs e)
    //{
    //    if (gvSales.Rows.Count < 1)
    //    {
    //        string display = "no Records Found.";
    //        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
    //    }
    //    else
    //    {
    //        Response.ContentType = "application/pdf";
    //        Response.AddHeader("content-disposition", "attachment;filename=SalesReports.pdf");
    //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //        StringWriter sw = new StringWriter();
    //        HtmlTextWriter hw = new HtmlTextWriter(sw);
    //       gvSales.RenderControl(hw);
    //        StringReader sr = new StringReader(sw.ToString());
    //        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
    //        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
    //        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
    //        pdfDoc.Open();
    //        htmlparser.Parse(sr);
    //        pdfDoc.Close();
    //        Response.Write(pdfDoc);
    //        Response.End();
    //        gvSales.AllowPaging = true;
    //        gvSales.DataBind();
    //    }
    //}
    public override void VerifyRenderingInServerForm(Control control)
    {
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

    private void LoadReport()

    {
        int Sales_ID = 0, Customer_ID = 0, CusID = 0, TransID = 0, ID = 0;
        string TableHTML = "", CompanyName = ""; ;
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        ReportDataSet dsReportData = new ReportDataSet();
        ReportDataSet dsReportData1 = new ReportDataSet();

        dsSales = VHMS.DataAccess.VHMSReports.PrintSales(txtDOB.Text, txtDOR.Text, ddlBranch.SelectedValue);
        try
        {
            dsSales.Tables[0].TableName = "tSales";
            foreach (DataRow drow in dsSales.Tables[0].Rows)
                dsReportData1.tSales.ImportRow(drow);

            dsSales.Tables[1].TableName = "tCompany";
            foreach (DataRow drowss in dsSales.Tables[1].Rows)
               dsReportData1.tCompany.ImportRow(drowss);

            for (int i = 0; i < dsSales.Tables[0].Rows.Count; i++)
            {
                //for (int j = 0; j <= dsSales.Tables[0].Rows.Count; j++)
                //{
                Sales_ID = Convert.ToInt32(dsReportData1.tSales[i][0]);
                Customer_ID = Convert.ToInt32(dsReportData1.tSales[i][3]);

                dsData = VHMS.DataAccess.VHMSReports.PrintSalesTrans(Sales_ID);
                if (TransID != Sales_ID)
                {
                    dsData.Tables[0].TableName = "VSalesTrans";
                    foreach (DataRow dsRow in dsData.Tables[0].Rows)
                        dsReportData.VSalesTrans.ImportRow(dsRow);
                }
                TransID = Sales_ID;
                if (dsReportData.tRate.Count == 0)
                {
                    dsData.Tables[1].TableName = "tRate";
                    foreach (DataRow darow in dsData.Tables[1].Rows)
                        dsReportData.tRate.ImportRow(darow);
                }
                dsCustomer = VHMS.DataAccess.VHMSReports.PrintCustomerSales(Customer_ID);
                if (CusID != Customer_ID)
                {
                    dsCustomer.Tables[0].TableName = "tCustomer";
                    foreach (DataRow drows in dsCustomer.Tables[0].Rows)
                        dsReportData.tCustomer.ImportRow(drows);
                    CusID = Customer_ID;
                }


                if (dsReportData1.tSales.Rows.Count > 0)
                {
                     string iRate = ""; decimal gRate = 0; decimal SRate = 0; decimal amount = 0;

                    foreach (ReportDataSet.tSalesRow drtJobCardRow in dsReportData1.tSales)
                    {
                        if (drtJobCardRow.PK_SalesID == Sales_ID)
                        {
                            if (dsReportData1.tCompany.Rows.Count > 0)
                            {
                                TableHTML += "<table cellpadding='0' cellspacing='0' style='margin-top: 195px;'>";
                                //CompanyName = drtBranchRow.CompanyName.ToString();
                                foreach (ReportDataSet.tCompanyRow drtBranchRow in dsReportData1.tCompany)
                                {
                                    CompanyName = drtBranchRow.CompanyName.ToString();
                                    //TableHTML += "<tr valign='middle'><th rowspan='3' align='right' style='padding-left:25px;width:20%'><img src='images/Dummy.jpg' width='100%' alt=''/></th><th style='font-size:19px;text-align:center;'>" + drtBranchRow.CompanyName + "</th></tr>";
                                    //TableHTML += "<tr><th style='text-align:center;'>" + drtBranchRow.CompanyAddress + "</th></tr>";
                                    //if (drtBranchRow.PhoneNo2.Length > 2)
                                    //    TableHTML += "<tr><th style='text-align:center;'> Phone :" + drtBranchRow.PhoneNo1 + "/" + drtBranchRow.PhoneNo2 + "</th></tr>";
                                    //else
                                    //    TableHTML += "<tr><th style='text-align:center;'> Phone :" + drtBranchRow.PhoneNo1 + "</th></tr>";
                                }
                                TableHTML += "</table>";
                            }
                            ID = Sales_ID;

                            // TableHTML += "<table cellpadding='0' cellspacing='0'><thead><tr><th style='text-align:center;'>Tax Invoice</th></tr></table>";
                            if (dsReportData.tCustomer.Rows.Count > 0)
                            {
                                TableHTML += "<table cellpadding='0' cellspacing='0' style='width: 67%;'><thead>";

                                foreach (ReportDataSet.tRateRow drtRate in dsReportData.tRate)
                                {
                                    iRate = " Gold= Rs." + drtRate.Gold_22Sales + "   Silver= Rs." + drtRate.SilverSales;
                                    gRate = drtRate.Gold_22Sales;
                                    SRate = drtRate.SilverSales;
                                }
                                foreach (ReportDataSet.tCustomerRow drtCustomerRow in dsReportData.tCustomer)
                                {
                                   
                                    TableHTML += "<tr><td><b>Invoice No.:" + drtJobCardRow.InvoiceNo + "</b></td><td><b>Date</b></td><td> :" + drtJobCardRow.InvoiceDate.ToString("dd/MM/yyyy") + "</td></tr>";
                                    TableHTML += "<tr><td><b>Mr./Ms." + drtCustomerRow.CustomerName + "</b></td><td><b> Gold   </b></td><td> : RS. " + gRate + "</td></tr>";
                                    TableHTML += "<tr><td style='width: 1px;'> " + drtCustomerRow.Address + "</td><td><b>  Silver  </b></td><td> : RS. " + SRate + "</td></tr>";
                                    TableHTML += "<tr><td><b>Mobile No. : " + drtCustomerRow.MobileNo + "</b><b></b></td><td><b> Salesman code </b></td><td> : " + drtJobCardRow.EmployeeCode + "</td></tr>";

                                }
                                TableHTML += "</table>";
                            }

                            TableHTML += "<table cellpadding='0' cellspacing='0' class='border_details' style='width: 65%;font-size: 12px !important;'><thead>";
                            TableHTML += "<tr><th style='width:10px;'><b>&nbsp S.No</b></th><th><b>Description</b></th></th><th   style='text-align:right'><b>Qty</b></th><th   style='text-align:right'><b>Grs.Wt</b></th><th   style='text-align:right'><b>Stn.Wt</b></th><th   style='text-align:right'><b>Stn.Amt</b></th><th   style='text-align:right'><b>Waste%</b><th   style='text-align:right'><b>Net.Wt</b></th><th   style='text-align:right'><b>Total</b></th></tr></thead>";

                            if (dsReportData.VSalesTrans.Rows.Count > 0)
                            {
                                int sno = 1;

                                foreach (ReportDataSet.VSalesTransRow drtJobCardComplaintsRow in dsReportData.VSalesTrans)
                                {
                                    TableHTML += "<tr><td>&nbsp &nbsp" + sno + "</td><td>" + drtJobCardComplaintsRow.CategoryName + "-" + drtJobCardComplaintsRow.ProductName + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.Quantity + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.NetWeight + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.StoneWeight + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.StonePrice + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.WastagePercent + "</td><td  style='text-align:right'>" + (Convert.ToDecimal(drtJobCardComplaintsRow.NetWeight) + Convert.ToDecimal(drtJobCardComplaintsRow.StoneWeight)) + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.Subtotal + "</td></tr>";
                                    sno++;
                                }
                            }
                            decimal TotalAmount = 0, NetAmount = 0, TaxAmount = 0; 
                            TableHTML += "</table>";
                            TableHTML += "<table cellpadding='0' cellspacing='0' style='width: 65%;'>";
                            TableHTML += "<tr><td colspan='3' style='text-align:right; font-size: large;'><b>Total Amount  :</b></td><td style='text-align:right'>" + drtJobCardRow.TotalAmount + "</td></tr>";
                         
                            TableHTML += "<tr><td colspan='3' style='text-align:right'><b>Discount Amount  :</b></td><td style='text-align:right'>" + drtJobCardRow.DiscountAmount + "</td></tr>";
                            TotalAmount = drtJobCardRow.TotalAmount - drtJobCardRow.DiscountAmount;
                            TableHTML += "<tr><td colspan='3' style='text-align:right'><b>Total Amount  :</b></td><td style='text-align:right'>" + TotalAmount + "</td></tr>";
                            //TableHTML += "<tr><td colspan='3' style='text-align:right'><b>Tax (" + drtJobCardRow.TaxPercent + "%)</b></td><td style='text-align:right'>" + drtJobCardRow.TaxAmount + "</td></tr>";
                            TableHTML += "<tr><td colspan='3' style='text-align:right'><b>CGST (" + drtJobCardRow.CGSTPercent + "%) :</b></td><td style='text-align:right'>" + drtJobCardRow.CGSTAmount + "</td></tr>";
                            TableHTML += "<tr><td colspan='3' style='text-align:right'><b>SGST (" + drtJobCardRow.SGSTPercent + "%) :</b></td><td style='text-align:right'>" + drtJobCardRow.SGSTAmount + "</td></tr>";
                            //if (iExchangeWeight > 0)
                            //{
                            //    TableHTML += "<tr><td colspan='3' style='text-align:right'><b>Exchange (" + iExchangeWeight + " Gm)</b></td><td style='text-align:right'>" + drtJobCardRow.ExchangeAmount + "</td></tr>";
                            //}
                            TaxAmount = drtJobCardRow.CGSTAmount + drtJobCardRow.SGSTAmount;
                            NetAmount = TotalAmount + TaxAmount;
                            if (drtJobCardRow.ReturnAmount > 0)
                            {
                                TableHTML += "<tr><td colspan='3' style='text-align:right'><b>Return Amount :</b></td><td style='text-align:right'>" + drtJobCardRow.ReturnAmount + "</td></tr>";
                            }
                            //if (drtJobCardRow.SchemeAmount > 0)
                            //{
                            //    TableHTML += "<tr><td colspan='3' style='text-align:right'><b>Net Amount :</b></td><td style='text-align:right'>" + NetAmount + "</td></tr>";
                            //}
                            //decimal Tamt = Convert.ToDecimal(drtJobCardRow.InvoiceAmount) - Convert.ToDecimal(drtJobCardRow.ExchangeAmount);
                            TableHTML += "<tr><td colspan='3' style='text-align:right'><b>Net Amount :</td><td style='text-align:right'>" + NetAmount + "</b></td></tr>";
                            TableHTML += "</table>";
                            if ((drtJobCardRow.InvoiceAmount != drtJobCardRow.CardAmount) && drtJobCardRow.CardAmount != 0)
                            {
                                amount = Convert.ToDecimal(drtJobCardRow.InvoiceAmount) - Convert.ToDecimal(drtJobCardRow.CardAmount);
                                TableHTML += "<table cellpadding='0' cellspacing='0' style='width: 65%;'>";
                                TableHTML += "<tr><td style='width:12%'><b>Pay Mode :</b><td><b>Cash :&nbsp;" + amount + "&nbsp;&nbsp;</b>(&nbsp;" + AmountInWords(Convert.ToInt32(amount)) + "&nbsp;)</td></b></td><td ><b>Card :&nbsp;" + drtJobCardRow.CardAmount + "&nbsp;&nbsp;</b>(&nbsp;" + AmountInWords(Convert.ToInt32(drtJobCardRow.CardAmount)) + "&nbsp;) </b></td></td><td></tr>";
                                TableHTML += "<tr><td><b>Rupees :</b></td><td style='width: 40%'><b>" + AmountInWords(Convert.ToInt32(drtJobCardRow.InvoiceAmount)) + "</b></td></tr>";
                                //TableHTML += "<tr><th colspan='3' ></th></tr>";
                                //TableHTML += "<tr><td style='text-align:right' colspan='3'><b>Authorized By</b><br/><br/><br/></td></tr>";
                                //TableHTML += "<tr><td style='text-align:left' colspan='2'>Customer's Signature</td><td style='text-align:right' ><b>For " + CompanyName + "</b></td></tr></table>";
                            }
                            else
                            {
                                TableHTML += "<table cellpadding='0' cellspacing='0'style='width: 66%;'>";
                                TableHTML += "<tr><td style='width:15%'><b>Pay Mode </b></td><td><b>" + drtJobCardRow.PaymentMode + "</b></td></tr>";
                                TableHTML += "<tr><td><b>Rupees </b></td><td><b>" + AmountInWords(Convert.ToInt32(drtJobCardRow.InvoiceAmount)) + "</b></td></tr>";
                                //TableHTML += "<tr><th colspan='3' ></th></tr>";
                                //TableHTML += "<tr><td colspan='3'><b>Authorized By</b><br/><br/><br/></td></tr>";
                                //TableHTML += "<tr><td style='text-align:left'>Customer's Signature</td><td style='text-align:right'><b>For " + CompanyName + "</b></td></tr></table>";
                            }
                            //for(int br = 0;br < 20;br++)
                            //TableHTML += "<br/>";
                            TableHTML += "<tr><th colspan='3' ></th></tr>";
                            TableHTML += "<tr><td style='text-align:right' colspan='3'><b>Authorized By</b><br/><br/><br/></td></tr>";
                            //TableHTML += "<tr><td style='text-align:left' colspan='2'>Customer's Signature</td><td style='text-align:right;'><b>For " + CompanyName + "</b></td></tr></table>";
                            TableHTML += "<tr><td style='text-align:left' colspan='3'><table  style='page-break-before: always;width:100% !important;margin:0% !important;'><tr><td style='text-align:left;width:60%;'>Customer's Signature</td><td style='text-align:right;width:40%;'><b>For " + CompanyName + "</b></td></tr></table></td></tr></table><div style='break-after:page'></div>";
                        }
                    }
                    divOPInvoice.InnerHtml = TableHTML;
                    dsReportData.Clear();
                }
                

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

    public void LoadCustomer()
    {
        Collection<VHMS.Entity.Customer> ObjList = new Collection<VHMS.Entity.Customer>();
        ObjList = VHMS.DataAccess.Customer.GetCustomer();

        //ddlCustomerName.DataSource = ObjList;
        //ddlCustomerName.DataTextField = "CustomerName";
        //ddlCustomerName.DataValueField = "CustomerID";
        //ddlCustomerName.DataBind();
        //ddlCustomerName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    public void LoadBranch()
    {
        Collection<VHMS.Entity.Branch> ObjList = new Collection<VHMS.Entity.Branch>();
        ObjList = VHMS.DataAccess.Branch.GetBranch();

        ddlBranch.DataSource = ObjList;
        ddlBranch.DataTextField = "BranchName";
        ddlBranch.DataValueField = "BranchID";
        ddlBranch.DataBind();
        ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NetAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TaxAmount"));
            InvoiceAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "InvoiceAmount"));
            DiscountAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DiscountAmount"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].ColumnSpan = 5;
            e.Row.Cells.RemoveAt(1);
            e.Row.Cells.RemoveAt(2);
            e.Row.Cells.RemoveAt(3);
            e.Row.Cells.RemoveAt(4);

            e.Row.Cells[1].Text = "Total Amount :";
            e.Row.Cells[3].Text = Convert.ToDecimal(NetAmount).ToString();
            e.Row.Cells[4].Text = Convert.ToDecimal(InvoiceAmount).ToString();
            e.Row.Cells[2].Text = Convert.ToDecimal(DiscountAmount).ToString();
            e.Row.Cells[1].Font.Bold = true;
            e.Row.Cells[2].Font.Bold = true;
            e.Row.Cells[3].Font.Bold = true;
            e.Row.Cells[4].Font.Bold = true;
            e.Row.Cells[1].Font.Size = 14;
            e.Row.Cells[2].Font.Size = 14;
            e.Row.Cells[3].Font.Size = 14;
            e.Row.Cells[4].Font.Size = 14;
        }
    }
}

