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

public partial class frmPaidReports : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            //LoadCustomer();
            //LoadBranch();
            //txtDOB.Text = Convert.ToDateTime(DateTime.Now.AddDays(-7)).ToString("dd-MM-yyyy");
            //txtDOR.Text = Convert.ToDateTime(DateTime.Now).ToString("dd-MM-yyyy");

        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {

        //Response.ClearContent();
        //Response.AppendHeader("content-disposition", "attachment; filename=SalesDetails.xls");
        //Response.ContentType = "application/excel";

        //StringWriter stringwriter = new StringWriter();
        //HtmlTextWriter htmltextwriter = new HtmlTextWriter(stringwriter);

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
        //divPdf.RenderControl(htmltextwriter);
        //Response.Write(stringwriter.ToString());
        //Response.End();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        //DateTime fdate = Convert.ToDateTime(txtDOB.Text);
        //DateTime tdate = Convert.ToDateTime(txtDOR.Text);
        string display = "please Enter Correct Register No and Mobile No....";
        if (txtRegisterNo.Text.Length >= 1 || txtMobileNo.Text.Length >= 10)
            LoadReport();
        else
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
        //gvSales_RowDataBound(sender,e);
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
        Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
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
        //DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        //ReportDataSet dsReportData = new ReportDataSet();

        //dsData = VHMS.DataAccess.VHMSReports.PrintRenewal(0, txtDOB.Text, txtDOR.Text, ddlCustomerName.SelectedValue, ddlBranch.SelectedValue);
        //try
        //{
        //    if (dsData.Tables.Count > 0)
        //    {
        //        dsData.Tables[0].TableName = "tRenewal";
        //        foreach (DataRow drow in dsData.Tables[0].Rows)
        //            dsReportData.tRenewal.ImportRow(drow);

        //        gvSales.DataSource = dsData.Tables[0];
        //        gvSales.DataBind();
        //    }
        //  DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;

        ReportDataSet dsReportData = new ReportDataSet();

        dsData = VHMS.DataAccess.VHMSReports.PrintSchemeLedgerReport(txtMobileNo.Text, txtRegisterNo.Text);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tRenewal";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tRenewal.ImportRow(drow);

                dsData.Tables[1].TableName = "tRegister";
                foreach (DataRow drow in dsData.Tables[1].Rows)
                    dsReportData.tRegister.ImportRow(drow);

                dsData.Tables[2].TableName = "tCompany";
                foreach (DataRow drow in dsData.Tables[2].Rows)
                    dsReportData.tCompany.ImportRow(drow);
            }
            if (dsReportData.tRegister.Rows.Count > 0)
            {
                string TableHTML = ""; string CompanyName = "";
                foreach (ReportDataSet.tRegisterRow drtJobCardRow in dsReportData.tRegister)
                {
                    if (dsReportData.tCompany.Rows.Count > 0)
                    {
                        TableHTML += "<table cellpadding='0' cellspacing='0'>";
                        foreach (ReportDataSet.tCompanyRow drtBranchRow in dsReportData.tCompany)
                        {
                            CompanyName = drtBranchRow.CompanyName.ToString();
                            TableHTML += "<tr valign='middle'><th rowspan='3' align='right' style='padding-left:25px;width:20%'><img src='images/smslogo.jpg' width='100%' alt=''/></th><th style='font-size:19px;text-align:center;'>" + drtBranchRow.CompanyName + "</th></tr>";
                            TableHTML += "<tr><th style='text-align:center;'>" + drtBranchRow.CompanyAddress + "</th></tr>";
                            if (drtBranchRow.PhoneNo2.Length > 2)
                                TableHTML += "<tr><th style='text-align:center;'> Phone :" + drtBranchRow.PhoneNo1 + "/" + drtBranchRow.PhoneNo2 + "</th></tr>";
                            else
                                TableHTML += "<tr><th style='text-align:center;'> Phone :" + drtBranchRow.PhoneNo1 + "</th></tr>";
                        }

                        TableHTML += "</table>";

                    }
                    TableHTML += "<table cellpadding='0' cellspacing='0' ><thead><tr  colspan='12'><th  style='text-align:center;'>Customer Ledger</th></tr></thead></table>";
                    TableHTML += "<table cellpadding='0' cellspacing='0'><thead>";
                    TableHTML += "<tr><td><b>Mr./Ms." + drtJobCardRow.CustomerName + "</b></td><td><b>Account No.: " + drtJobCardRow.AccountNo + "</td></tr>";
                    TableHTML += "<tr><td> " + drtJobCardRow.Address + "</td><td><b>Installment Amount:Rs." + drtJobCardRow.InstallmentAmount + "</td></tr>";
                    TableHTML += "<tr><td><b>Mobile No.: " + drtJobCardRow.MobileNo + "</td><td><b>Joining Dt.: " + drtJobCardRow.RegisterDate.ToString("dd-MM-yyyy") + "</td></tr>";
                    TableHTML += "<tr><td><b>Salesman Code: " + drtJobCardRow.EmployeeCode + "</td><td><b>Maturity Dt.: " + drtJobCardRow.RegisterDate.AddMonths(drtJobCardRow.Duration).ToString("dd-MM-yyyy") + "</td></tr>";
                    TableHTML += "<tr><td><b>Scheme: " + drtJobCardRow.ChitName + "</b></td><td><b>Branch: " + drtJobCardRow.BranchName + "</b></td></tr>";
                    TableHTML += "<br/>";
                    TableHTML += "</thead></table>";
                    TableHTML += "<table cellpadding='0' cellspacing='0' class='border_details' ><thead>";
                    TableHTML += "<tr><th style='width:5%;'><b>&nbsp S.No</b></th><th style='width:10%;'><b>Receipt Dt.</b></th></th><th   style='width:10%;text-align:right'><b>Receipt No.</b></th><th   style='width:10%;text-align:right'><b>Amount</b></th><th   style='width:10%;text-align:right'><b>Total Amount</b></th></tr></thead>";
                    decimal TAmount = 0;
                    if (dsReportData.tRenewal.Rows.Count > 0)
                    {
                        int sno = 1;

                        foreach (ReportDataSet.tRenewalRow drtJobCardComplaintsRow in dsReportData.tRenewal)
                        {
                            if (drtJobCardComplaintsRow.FK_RegisterID == drtJobCardRow.PK_RegisterID)
                            {
                                TAmount += drtJobCardComplaintsRow.AmountPaid;
                                TableHTML += "<tr><td>&nbsp &nbsp" + sno + "</td><td>" + drtJobCardComplaintsRow.RenewalDate.ToString("dd-MM-yyyy") + "</td><td  style='text-align:right'> " + drtJobCardComplaintsRow.RenewalNo + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.AmountPaid + "</td><td  style='text-align:right'>" + TAmount + "</td></tr>";
                                sno++;
                            }
                        }
                    }

                    TableHTML += "<tr style='text-align:right'><td colspan='3'></td><td style='text-align:right'><b>Total Amount  </b></td><td style='text-align:right'><b> " + TAmount + "</b></td></tr>";
                    TableHTML += "</table>";
                    //if(drtJobCardRow.Status!= "Open")
                    //TableHTML += "<tr style='text-align:right'><td colspan='3'></td><td style='text-align:right'><b>Status = Closed   </b></td><td style='text-align:right'><b> " + drtJobCardRow.CancelledDate.ToString("dd-MM-yyyy") + "</b></td></tr>";
                    if (drtJobCardRow.Status != "Open")
                    {
                        TableHTML += "<table cellpadding='0' cellspacing='0'><thead>";
                        TableHTML += "<tr><td><b style='font-size: 18px;font-weight: bold;color: red;'>A/C Closed</b></td><td><b  style='font-size: 18px;font-weight: bold;color: red;'>Closed Date:" + drtJobCardRow.CancelledDate.ToString("dd-MM-yyyy") + "</b></td></tr>";
                        TableHTML += "</thead></table>";
                    }
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
    //public void LoadCustomer()
    //{
    //    Collection<VHMS.Entity.Customer> ObjList = new Collection<VHMS.Entity.Customer>();
    //    ObjList = VHMS.DataAccess.Customer.GetCustomer();

    //    ddlCustomerName.DataSource = ObjList;
    //    ddlCustomerName.DataTextField = "CustomerName";
    //    ddlCustomerName.DataValueField = "CustomerID";
    //    ddlCustomerName.DataBind();
    //    ddlCustomerName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    //}
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
