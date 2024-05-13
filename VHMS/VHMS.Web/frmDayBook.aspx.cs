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
using System.Globalization;

public partial class frmDayBook : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            txtDOB.Text = fromDate(Convert.ToInt32(Session["FK_FinancialYearID"]));

            if (FinancialYearStatus(Convert.ToInt32(Session["FK_FinancialYearID"])) == "Closed")
                txtDOR.Text = TODate(Convert.ToInt32(Session["FK_FinancialYearID"]));
            else
                txtDOR.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
    }
    private static string fromDate(Int32 firstName)
    {
        VHMS.Entity.FinancialYear objQuotation = new VHMS.Entity.FinancialYear();
        objQuotation = VHMS.DataAccess.FinancialYear.GetFinancialYearByID(firstName);
        string result = objQuotation.sYearFrom;
        return result;
    }
    private static string FinancialYearStatus(Int32 firstName)
    {
        VHMS.Entity.FinancialYear objQuotation = new VHMS.Entity.FinancialYear();
        objQuotation = VHMS.DataAccess.FinancialYear.GetFinancialYearByID(firstName);
        string result = objQuotation.Status;
        return result;
    }
    private static string TODate(Int32 firstName)
    {
        VHMS.Entity.FinancialYear objQuotation = new VHMS.Entity.FinancialYear();
        objQuotation = VHMS.DataAccess.FinancialYear.GetFinancialYearByID(firstName);
        string result = objQuotation.sYearTo;
        return result;
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {

        Response.ClearContent();
        Response.AppendHeader("content-disposition", "attachment; filename=Daybook.xls");
        Response.ContentType = "application/excel";

        StringWriter stringwriter = new StringWriter();
        HtmlTextWriter htmltextwriter = new HtmlTextWriter(stringwriter);

        //divPdf.HeaderRow.Style.Add("background-color", "#FFFFFF");

        //foreach (TableCell tableCell in divPdf.HeaderRow.Cells)
        //{
        //    tableCell.Style["background-color"] = "#d8d4d4";
        //}

        //foreach (GridViewRow gridViewRow in divPdf.Rows)
        //{
        //    gridViewRow.BackColor = System.Drawing.Color.White;
        //    foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
        //    {
        //        gridViewRowTableCell.Style["background-color"] = "#FFFFFF";
        //    }
        //}
        divPdf.RenderControl(htmltextwriter);
        Response.Write(stringwriter.ToString());
        Response.End();
    }

    
    protected void btnPDF_Click(object sender, EventArgs e)
    {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Daybook.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            divPdf.RenderControl(htmlTextWriter);
            StringReader stringReader = new StringReader(stringWriter.ToString());
            Document Doc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(Doc);
            PdfWriter.GetInstance(Doc, Response.OutputStream);
            Doc.Open();
            htmlparser.Parse(stringReader);
            Doc.Close();
            Response.Write(Doc);
            Response.End();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadReport();
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
        var stylecss = @"<link href='css/print.css' rel='stylesheet type='text/css' /><link href='css/fonts/font-awesome.min.css' rel='stylesheet type='text/css' />";
        htmlWriter.Write(javascript);
        htmlWriter.Write(stylecss);
        divPdf.RenderControl(htmlWriter);
        streamWriter.Write(strBuilder.ToString());
        streamWriter.Flush();
        Response.End();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=BankStatement.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter stringWriter = new StringWriter();
        HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
        divPdf.RenderControl(htmlTextWriter);
        StringReader stringReader = new StringReader(stringWriter.ToString());
        Document Doc = new Document(PageSize.A4);
        HTMLWorker htmlparser = new HTMLWorker(Doc);
        PdfWriter.GetInstance(Doc, Response.OutputStream);
        Doc.Open();
        htmlparser.Parse(stringReader);
        Doc.Close();
        Response.Write(Doc);
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    private void LoadReport()
    {
        ReportDataSet dsReportData = new ReportDataSet();

        dsData = VHMS.DataAccess.VHMSReports.PrintDayBook(txtDOB.Text, txtDOR.Text, ddlCustomer.SelectedValue);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tBankStatement";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tBankStatement.ImportRow(drow);

                dsData.Tables[1].TableName = "tLedger";
                foreach (DataRow drow in dsData.Tables[1].Rows)
                    dsReportData.tLedger.ImportRow(drow);

                dsData.Tables[2].TableName = "tCompany";
                foreach (DataRow drow in dsData.Tables[2].Rows)
                    dsReportData.tCompany.ImportRow(drow);
            }
            if (dsReportData.tLedger.Rows.Count > 0)
            {
                string TableHTML = ""; string CompanyName = "";
                foreach (ReportDataSet.tLedgerRow drtJobCardRow in dsReportData.tLedger)
                {
                    if (dsReportData.tCompany.Rows.Count > 0)
                    {
                        TableHTML += "<table cellpadding='0' cellspacing='0'>";
                        foreach (ReportDataSet.tCompanyRow drtBranchRow in dsReportData.tCompany)
                        {
                            CompanyName = drtBranchRow.CompanyName.ToString();
                            TableHTML += "<tr valign='middle'><th style='font-size:19px;text-align:center;'Colspan='5'>" + drtBranchRow.CompanyName + "</th></tr>";
                            TableHTML += "<tr><th style='text-align:center;'Colspan='5'>" + drtBranchRow.CompanyAddress + "</th></tr>";
                            if (drtBranchRow.PhoneNo2.Length > 2)
                                TableHTML += "<tr><th style='text-align:center;'Colspan='5'> Phone :" + drtBranchRow.PhoneNo1 + "/" + drtBranchRow.PhoneNo2 + "</th></tr>";
                            else
                                TableHTML += "<tr><th style='text-align:center;'Colspan='5'> Phone :" + drtBranchRow.PhoneNo1 + "</th></tr>";
                        }

                        TableHTML += "</table>";

                    }
                  
                    TableHTML += "<table cellpadding='0' cellspacing='0' class='border_details' ><thead>";
                    TableHTML += "<tr><th style='width:10%;'><b> Date</b></th><th style='width:20%;'><b>Voucher No</b></th><th style='width:10%;'><b>Type</b></th><th style='width:30%;'><b>Particulars</b></th></th><th style='width:10%;text-align:right'><b>Debit</b></th><th style='width:10%;text-align:right'><b>Credit</b></th><th style='width:10%;text-align:right'><b>Balance</b></th></tr></thead>";
                    decimal CAmount = 0, DAmount = 0;
                    string BalanceType = "";
                    if (dsReportData.tBankStatement.Rows.Count > 0)
                    {
                        int sno = 1;

                        foreach (ReportDataSet.tBankStatementRow drtJobCardComplaintsRow in dsReportData.tBankStatement)
                        {
                            CAmount += drtJobCardComplaintsRow.Credit;
                            DAmount += drtJobCardComplaintsRow.Debit;
                            decimal TotalBalance = CAmount - DAmount;
                            if (TotalBalance > 0)
                                BalanceType = TotalBalance + "  Cr ";
                            else
                            {
                                TotalBalance = DAmount - CAmount;
                                BalanceType = TotalBalance + "  Dr ";
                            }

                            string VDate = "", vNo = "";
                            if (!drtJobCardComplaintsRow.IsVoucherDateNull())
                                VDate = drtJobCardComplaintsRow.VoucherDate.ToString("dd-MM-yyyy");
                            if (!drtJobCardComplaintsRow.IsVoucherNoNull())
                                vNo = drtJobCardComplaintsRow.VoucherNo;

                            TableHTML += "<tr><td style='width:10%;'>" + VDate + "</td><td>" + vNo + "</td><td>" + drtJobCardComplaintsRow.VoucherType + "</td><td>" + drtJobCardComplaintsRow.Particulars + "</td><td  style='text-align:right'> " + drtJobCardComplaintsRow.Debit + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.Credit + "</td><td  style='text-align:right'>" + BalanceType + "</td></tr>";
                            sno++;
                        }
                    }

                    TableHTML += "<tr style='text-align:right'><td colspan='3'></td><td style='text-align:right'><b>Total Amount  </b></td><td style='text-align:right'><b> " + DAmount + "</b></td><td style='text-align:right'><b> " + CAmount + "</b></td></tr>";
                    if (CAmount > DAmount)
                        TableHTML += "<tr style='text-align:right'><td colspan='3'></td><td style='text-align:right'><b>Closing Balance</b></td><td style='text-align:right'></td><td style='text-align:right'><b> " + (CAmount - DAmount).ToString("0.00") + "</b></td></tr>";
                    else
                        TableHTML += "<tr style='text-align:right'><td colspan='3'></td><td style='text-align:right'><b>Closing Balance</b></td><td style='text-align:right'><b> " + (DAmount - CAmount).ToString("0.00") + "</b></td><td style='text-align:right'></td></tr>";

                    TableHTML += "</table>";
                    TableHTML += "<br/>";
                    TableHTML += "<br/>";
                    TableHTML += "<br/>";
                    TableHTML += "<br/>";
                }

                divOPInvoice1.InnerHtml = TableHTML;
            }
        }

        catch (Exception ex)
        {
            sException = "frmPrintJobCard | " + ex.ToString();
            Log.Write(sException);
        }
    }


    // int totalItems = 0;
    //  decimal percent = 0M;
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    InvoiceAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "InvoiceAmount"));
        //}
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    (e.Row.FindControl("lblTotalAmount") as Label).Text = InvoiceAmount.ToString();
        //}

    }

    //public void LoadBranch()
    //{
    //    Collection<VHMS.Entity.Branch> ObjList = new Collection<VHMS.Entity.Branch>();
    //    ObjList = VHMS.DataAccess.Branch.GetBranch();

    //    ddlBranch.DataSource = ObjList;
    //    ddlBranch.DataTextField = "BranchName";
    //    ddlBranch.DataValueField = "BranchID";
    //    ddlBranch.DataBind();
    //    ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    //}
}
