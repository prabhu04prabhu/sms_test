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
using CrystalDecisions.CrystalReports.Engine;
using System.Globalization;

public partial class frmStockLedger : System.Web.UI.Page
{
    ReportDocument rprt = new ReportDocument();
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            LoadProduct();
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
    public void LoadProduct()
    {
        Collection<VHMS.Entity.Master.Product> ObjList = new Collection<VHMS.Entity.Master.Product>();
        ObjList = VHMS.DataAccess.Master.Product.GetProductList();

        ddlCustomer.DataSource = ObjList;
        ddlCustomer.DataTextField = "ProductName";
        ddlCustomer.DataValueField = "ProductID";
        ddlCustomer.DataBind();
        ddlCustomer.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {

        Response.ClearContent();
        Response.AppendHeader("content-disposition", "attachment; filename=StockLedger.xls");
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

    protected void btnView_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlCustomer.SelectedValue) > 0)
        {
            divPdf.Visible = true;
            LoadReport();
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Select Product or enter SMSCode');", true);
            txtCode.Focus();
        }
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
        Response.AddHeader("content-disposition", "attachment;filename=StockLedger.pdf");
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
    protected void tbAccount_TextChanged(object sender, EventArgs e)
    {
        if (txtCode.Text != "")
        {
            //string text = "";

            //text = txtCode.Text;

            //string[] arr = text.Split('|');

            //txtCode.Text = arr[0];
            Collection<VHMS.Entity.Master.Product> ObjList = new Collection<VHMS.Entity.Master.Product>();
            ObjList = VHMS.DataAccess.Master.Product.GetProductByCodeByID(txtCode.Text, false);

            ddlCustomer.DataSource = ObjList;
            ddlCustomer.DataTextField = "ProductName";
            ddlCustomer.DataValueField = "ProductID";
            ddlCustomer.DataBind();
        }
        else
        {
            LoadProduct();
        }
        divPdf.Visible = false;
        //ddlCustomer.SelectedValue = 0;
    }
    private void LoadReport()
    {
        ReportDatestNew dsReportData = new ReportDatestNew();

        dsData = VHMS.DataAccess.VHMSReports.PrintStockLedgerReport(txtDOB.Text, txtDOR.Text, ddlCustomer.SelectedValue);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tStockDetailed";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tStockDetailed.ImportRow(drow);

                dsData.Tables[1].TableName = "tProduct";
                foreach (DataRow drow in dsData.Tables[1].Rows)
                    dsReportData.tProduct.ImportRow(drow);

                dsData.Tables[2].TableName = "tCompany";
                foreach (DataRow drow in dsData.Tables[2].Rows)
                    dsReportData.tCompany.ImportRow(drow);
            }
            if (dsReportData.tProduct.Rows.Count > 0)
            {
                string TableHTML = ""; string CompanyName = ""; string iRate = ""; decimal gRate = 0; string date = "";
                foreach (ReportDatestNew.tProductRow drtJobCardRow in dsReportData.tProduct)
                {
                    if (dsReportData.tCompany.Rows.Count > 0)
                    {
                        TableHTML += "<table cellpadding='0' cellspacing='0'>";
                        foreach (ReportDatestNew.tCompanyRow drtBranchRow in dsReportData.tCompany)
                        {
                            CompanyName = drtBranchRow.CompanyName.ToString();
                            if (drtBranchRow.PhoneNo2.Length <= 0)
                                TableHTML += "<tr valign='middle'><th rowspan='3' align='right' style='padding-left:25px;width:10%'><img src='images/meenakchi-amman-1.jpg' width='100%' alt=''/><th rowspan='3' align='right' style='padding-left:25px;width:15%'><img src='images/5.png' width='100%' alt=''/></th><th style='font-size:19px;text-align:center;width: 48%;height: 1%;'>" + drtBranchRow.CompanyName + "</th><th style='text-align:end;height: 1%;'> " + drtBranchRow.PhoneNo1 + "</th></tr>";
                            else
                                TableHTML += "<tr valign='middle'><th rowspan='3' align='right' style='padding-left:25px;width:10%'><img src='images/meenakchi-amman-1.jpg' width='100%' alt=''/><th rowspan='3' align='right' style='padding-left:25px;width:15%'><img src='images/5.png' width='100%' alt=''/></th><th style='font-size:19px;text-align:center;width: 48%;height: 1%;'>" + drtBranchRow.CompanyName + "</th><th style='text-align:end;height: 1%;'> " + drtBranchRow.PhoneNo1 + " / " + drtBranchRow.PhoneNo2 + "</th></tr>";

                            TableHTML += "<tr><th style='text-align:center;height: 1%;' >Pure Handloom Silk Sarees Manufacturer</th><th></th></tr>";

                            TableHTML += "<tr><th style='text-align:center;height: 1%;'>" + drtBranchRow.CompanyAddress + "</th><th style='text-align:end;height: 1%;'>" + drtBranchRow.CSTNo + " / State Code: 33 </th></tr>";
                            TableHTML += "<tr><th style='text-align:center;'Colspan='4'> Email : " + drtBranchRow.Email + "</th></tr>";

                        }
                        TableHTML += "</table>";
                    }
                    TableHTML += "<table cellpadding='0' cellspacing='0' ><thead><tr><th  style='text-align:center;'Colspan='5'>Stock Ledger</th></tr></thead></table>";
                    TableHTML += "<table cellpadding='0' cellspacing='0'><thead>";
                    // TableHTML += "<tr><td colspan='3'><b>" + drtJobCardRow.ProductName + "</b></td><td colspan='3' style='text-align:center;'><b>SMS Code.: " + drtJobCardRow.SMSCode + "</td></tr>";
                    TableHTML += "<tr><td colspan='3'><b>SMS Code.: " + drtJobCardRow.SMSCode + "</td><td colspan='3' style='text-align:right;'><b>Party Code.: " + drtJobCardRow.ProductCode + "</td></tr>";

                    TableHTML += "<br/>";
                    TableHTML += "</thead></table>";
                    TableHTML += "<table cellpadding='0' cellspacing='0' class='border_details' ><thead>";
                    TableHTML += "<tr><th style='width:10%;'><b> Bill No</b></th><th style='width:10%;'><b>Date</b></th><th style='width:20%;'><b>Type</b></th><th style='width:50%;'><b>Particulars</b></th></th><th style='width:25%;text-align:right'><b>InQty</b></th><th style='width:25%;text-align:right'><b>OutQty</b></th><th style='width:25%;text-align:right'><b>Bal.Qty</b></th></tr></thead>";
                    decimal CAmount = 0, DAmount = 0, TotalBalance = 0;
                    string BalanceType = "";
                    if (dsReportData.tStockDetailed.Rows.Count > 0)
                    {
                        int sno = 1;

                        foreach (ReportDatestNew.tStockDetailedRow drtJobCardComplaintsRow in dsReportData.tStockDetailed)
                        {
                            if (drtJobCardComplaintsRow.VoucherType == "Stock Adujst")
                            {
                                CAmount = drtJobCardComplaintsRow.InQty;
                                DAmount = 0;
                            }
                            else
                            {
                                CAmount += drtJobCardComplaintsRow.InQty;
                                DAmount += drtJobCardComplaintsRow.OutQty;
                            }
                            TotalBalance = CAmount - DAmount;

                            if (TotalBalance > 0)
                                BalanceType = TotalBalance.ToString();
                            else
                            {
                                BalanceType = TotalBalance.ToString();
                            }

                            string VDate = "";
                            if (!drtJobCardComplaintsRow.IsVoucherDateNull())
                                VDate = drtJobCardComplaintsRow.VoucherDate.ToString("dd-MM-yyyy");
                            TableHTML += "<tr><td style='width:10%;'>" + drtJobCardComplaintsRow.VoucherNo + "</td><td style='width:10%;'>" + VDate + "</td><td style='width:20%;'>" + drtJobCardComplaintsRow.VoucherType + "</td><td style='width:50%;'>" + drtJobCardComplaintsRow.Particulars + "</td><td  style='text-align:right;width:25%;'> " + drtJobCardComplaintsRow.InQty + "</td><td  style='text-align:right;width:25%;'>" + drtJobCardComplaintsRow.OutQty + "</td><td  style='text-align:right;width:25%;'>" + BalanceType + "</td></tr>";
                            sno++;
                        }
                    }
                    if (TotalBalance > 0)
                        TableHTML += "<tr style='text-align:right'><td colspan='3'></td><td style='text-align:right'><b>Available Quantity</b></td><td style='text-align:right'><b> " + BalanceType.ToString() + "</b></td><td style='text-align:right'></td></tr>";
                    else
                        TableHTML += "<tr style='text-align:right'><td colspan='3'></td><td style='text-align:right'><b>Available Quantity</b></td><td style='text-align:right'></td><td style='text-align:right'><b> " + BalanceType.ToString() + "</b></td></tr>";

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

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=BuyerLedger.pdf");
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
}
