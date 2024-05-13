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

public partial class frmPurchaseBillLegder : System.Web.UI.Page
{
    ReportDocument rprt = new ReportDocument();
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    string GpayAmount = "", NetAmount = "", DiscAmount = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            LoadSupplier();
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
    public void LoadSupplier()
    {
        Collection<VHMS.Entity.Billing.Supplier> ObjList = new Collection<VHMS.Entity.Billing.Supplier>();
        ObjList = VHMS.DataAccess.Billing.Supplier.GetActiveSupplier();

        ddlSupplier.DataSource = ObjList;
        ddlSupplier.DataTextField = "SupplierName";
        ddlSupplier.DataValueField = "SupplierID";
        ddlSupplier.DataBind();
        ddlSupplier.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            LoadReport();
            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment; filename=PurchaseBillLedger.xls");
            Response.ContentType = "application/excel";

            StringWriter stringwriter = new StringWriter();
            HtmlTextWriter htmltextwriter = new HtmlTextWriter(stringwriter);

            gvPurchase.HeaderRow.Style.Add("background-color", "#FFFFFF");

            foreach (TableCell tableCell in gvPurchase.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#d8d4d4";
            }

            foreach (GridViewRow gridViewRow in gvPurchase.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";
                    gridViewRowTableCell.Style["border"] = "1px Solid";
                }
            }
            gvPurchase.RenderControl(htmltextwriter);
            Response.Write(stringwriter.ToString());
            Response.End();
        }
        catch (Exception exe)
        {
            throw exe;
        }
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        LoadReport();
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=PurchaseBillLedger.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gvPurchase.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
        gvPurchase.AllowPaging = true;
        gvPurchase.DataBind();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadReport();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    private void LoadReport()
    {
        ReportDatestNew dsReportData = new ReportDatestNew();
        dsData = VHMS.DataAccess.VHMSReports.PrintBillLedgerReport(txtDOB.Text, txtDOR.Text, ddlSupplier.SelectedValue);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                //dsData.Tables[0].TableName = "tPurchase";
                //foreach (DataRow drow in dsData.Tables[0].Rows)
                //    dsReportData.tPurchase.ImportRow(drow);

                //dsData.Tables[1].TableName = "tSupplier";
                //foreach (DataRow drow in dsData.Tables[1].Rows)
                //    dsReportData.tSupplier.ImportRow(drow);

                //dsData.Tables[2].TableName = "tCompany";
                //foreach (DataRow drow in dsData.Tables[2].Rows)
                //    dsReportData.tCompany.ImportRow(drow);
                dsData.Tables[0].TableName = "tPurchase";

                //foreach (DataRow drow in dsData.Tables[1].Rows)

                gvPurchase.DataSource = dsData.Tables[0];
                gvPurchase.DataBind();
            }
            //if (dsReportData.tSupplier.Rows.Count > 0)
            //{
            //    string TableHTML = ""; string CompanyName = ""; string iRate = ""; decimal gRate = 0; string date = "";
            //    foreach (ReportDatestNew.tSupplierRow drtJobCardRow in dsReportData.tSupplier)
            //    {
            //        if (dsReportData.tCompany.Rows.Count > 0)
            //        {
            //            TableHTML += "<table cellpadding='0' cellspacing='0'>";
            //            foreach (ReportDatestNew.tCompanyRow drtBranchRow in dsReportData.tCompany)
            //            {
            //                CompanyName = drtBranchRow.CompanyName.ToString();
            //                if (drtBranchRow.PhoneNo2.Length < 0)
            //                    TableHTML += "<tr valign='middle'><th rowspan='3' align='right' style='padding-left:25px;width:10%'><img src='images/meenakchi-amman-1.jpg' width='100%' alt=''/><th rowspan='3' align='right' style='padding-left:25px;width:15%'><img src='images/5.png' width='100%' alt=''/></th><th style='font-size:19px;text-align:center;width: 48%;height: 1%;'>" + drtBranchRow.CompanyName + "</th><th style='text-align:end;height: 1%;'> " + drtBranchRow.PhoneNo1 + "</th></tr>";
            //                else
            //                    TableHTML += "<tr valign='middle'><th rowspan='3' align='right' style='padding-left:25px;width:10%'><img src='images/meenakchi-amman-1.jpg' width='100%' alt=''/><th rowspan='3' align='right' style='padding-left:25px;width:15%'><img src='images/5.png' width='100%' alt=''/></th><th style='font-size:19px;text-align:center;width: 48%;height: 1%;'>" + drtBranchRow.CompanyName + "</th><th style='text-align:end;height: 1%;'> " + drtBranchRow.PhoneNo1 + " / " + drtBranchRow.PhoneNo2 + "</th></tr>";

            //                TableHTML += "<tr><th style='text-align:center;height: 1%;' >Pure Handloom Silk Sarees Manufacturer</th><th></th></tr>";

            //                TableHTML += "<tr><th style='text-align:center;height: 1%;'>" + drtBranchRow.CompanyAddress + "</th><th style='text-align:end;height: 1%;'>" + drtBranchRow.CSTNo + " / State Code: 33 </th></tr>";
            //                TableHTML += "<tr><th style='text-align:center;'Colspan='4'> Email : " + drtBranchRow.Email + "</th></tr>";
            //            }
            //            TableHTML += "</table>";
            //        }
            //        TableHTML += "<table cellpadding='0' cellspacing='0' ><thead><tr><th  style='text-align:center;' Colspan='5'>Supplier Ledger</th></tr></thead></table>";
            //        TableHTML += "<table cellpadding='0' cellspacing='0'><thead>";
            //        TableHTML += "<tr><td Colspan='7'><b>Mr./Ms." + drtJobCardRow.SupplierName + "</b></td><td Colspan='2'></td><td Colspan='3'><b>Phone No.: " + drtJobCardRow.PhoneNo1 + "</td></tr>";
            //        TableHTML += "<tr><td Colspan='5'> " + drtJobCardRow.City + "</td><td></td></tr>";
            //        TableHTML += "<br/>";
            //        TableHTML += "</thead></table>";
            //        TableHTML += "<table cellpadding='0' cellspacing='0' class='border_details' ><thead>";
            //        TableHTML += "<tr><th style='width:10%;'><b> Date</b></th><th style='width:50%;'><b>Particulars</b></th></th><th style='width:10%;text-align:right'><b>Amount</b></th><th style='width:10%;text-align:right'><b>Debit Amount</b></th></tr></thead>";
            //        decimal CAmount = 0, DAmount = 0;
            //        string BalanceType = "";
            //        if (dsReportData.tBillstatement.Rows.Count > 0)
            //        {
            //            int sno = 1;

            //            foreach (ReportDatestNew.tBillstatementRow drtJobCardComplaintsRow in dsReportData.tBillstatement)
            //            {
            //                string VDate = "";
            //                if (!drtJobCardComplaintsRow.IsVoucherDateNull())
            //                    VDate = drtJobCardComplaintsRow.VoucherDate.ToString("dd-MM-yyyy");
            //                TableHTML += "<tr><td>" + VDate + "</td><td>" + drtJobCardComplaintsRow.Particulars + "</td><td  style='text-align:right'> " + drtJobCardComplaintsRow.Amount + "</td><td  style='text-align:right'> " + drtJobCardComplaintsRow.DebitAmount + "</td></tr>";
            //                sno++;
            //            }
            //        }
            //        //TableHTML += "<tr style='text-align:right'><td colspan='1'></td><td style='text-align:right'><b>Total Amount  </b></td><td style='text-align:right'><b> " + DAmount + "</b></td><td style='text-align:right'><b> " + CAmount + "</b></td></tr>";
            //        //if (CAmount > DAmount)
            //        //    TableHTML += "<tr style='text-align:right'><td colspan='1'></td><td style='text-align:right'><b>Closing Balance</b></td><td style='text-align:right'></td><td style='text-align:right'><b> " + (CAmount - DAmount).ToString("0.00") + "</b></td></tr>";
            //        //else
            //        //    TableHTML += "<tr style='text-align:right'><td colspan='1'></td><td style='text-align:right'><b>Closing Balance</b></td><td style='text-align:right'><b> " + (DAmount - CAmount).ToString("0.00") + "</b></td><td style='text-align:right'></td></tr>";
            //        TableHTML += "</table>";
            //        TableHTML += "<br/>";
            //        TableHTML += "<br/>";
            //        TableHTML += "<br/>";
            //        TableHTML += "<br/>";
            //    }
            //    divOPInvoice1.InnerHtml = TableHTML;
            //}
        }
        catch (Exception ex)
        {
            sException = "frmPrintJobCard | " + ex.ToString();
            Log.Write(sException);
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GpayAmount += DataBinder.Eval(e.Row.DataItem, "TotalPaidAmount");
            NetAmount += DataBinder.Eval(e.Row.DataItem, "TotalReturnAmount");
            DiscAmount += DataBinder.Eval(e.Row.DataItem, "TotalPurDiscAmount");

            Label lbl = (Label)e.Row.FindControl("lblBillCount");
            lbl.Text = GpayAmount.Replace(",", "<br>");
            GpayAmount = "";

            Label lbl1 = (Label)e.Row.FindControl("lblTotalReturnCount");
            lbl1.Text = NetAmount.Replace(",", "<br>");
            NetAmount = "";

            Label lbl2 = (Label)e.Row.FindControl("lblTotalPurDiscAmount");
            lbl2.Text = DiscAmount.Replace(",", "<br>");
            DiscAmount = "";
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("lblBillCount");
            lbl.Text = GpayAmount.Replace(",", "<br>");

            Label lbl1 = (Label)e.Row.FindControl("lblTotalReturnCount");
            lbl1.Text = NetAmount.Replace(",", "<br>");

            Label lbl2 = (Label)e.Row.FindControl("lblTotalPurDiscAmount");
            lbl2.Text = DiscAmount.Replace(",", "<br>");
        }
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rprt.Close();
        rprt.Dispose();
    }
}
