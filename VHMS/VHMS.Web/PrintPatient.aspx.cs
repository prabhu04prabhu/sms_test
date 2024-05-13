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

public partial class PrintPatient : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            if (HttpContext.Current.Session["PatientID"] != null)
                hdnPatientID.Value = HttpContext.Current.Session["PatientID"].ToString();
            else
                hdnPatientID.Value = "-1";
            Session["PatientID"] = null;
        }
        LoadReport();
        btnPrint_Click(sender, e);
    }

    private void LoadReport()
    {
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        ReportDataSet dsReportData = new ReportDataSet();

        dsData = VHMS.DataAccess.VHMSReports.PrintPatient(Convert.ToInt32(hdnPatientID.Value));
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tPatient";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tPatient.ImportRow(drow);

                dsData.Tables[1].TableName = "tCompany";
                foreach (DataRow drow in dsData.Tables[1].Rows)
                    dsReportData.tCompany.ImportRow(drow);

            }
            if (dsReportData.tPatient.Rows.Count > 0)
            {
                string TableHTML = "";
                
                foreach (ReportDataSet.tPatientRow drtCustomerRow in dsReportData.tPatient)
                {
                    if (dsReportData.tCompany.Rows.Count > 0)
                    {
                        TableHTML += "<table cellpadding='0' cellspacing='0'>";
                        foreach (ReportDataSet.tCompanyRow drtBranchRow in dsReportData.tCompany)
                        {
                            TableHTML += "<tr valign='middle'><th rowspan='3' align='right' style='padding-left:25px;width:20%'><img src='images/aks.png' width='100%' alt=''/></th><th style='font-size:19px;text-align:center;'>" + drtBranchRow.CompanyName + "</th></tr>";
                            TableHTML += "<tr><th style='text-align:center;'>" + drtBranchRow.CompanyAddress + "</th></tr>";
                            TableHTML += "<tr><th style='text-align:center;'> Phone :" + drtBranchRow.PhoneNo1 + " </th></tr>";
                        }
                        TableHTML += "</table>";

                    }
                    TableHTML += "<table cellpadding='0' cellspacing='0'><thead><tr><th style='text-align:center;'>Welcome to Akshaya Fertility Centre hope to Happiness</th></tr></table>";
                    var today = DateTime.Today;                    
                    var HAge = today.Year - drtCustomerRow.HDOB.Year;
                    if (drtCustomerRow.HDOB > today.AddYears(-HAge)) HAge--;
                    var WAge = today.Year - drtCustomerRow.WDOB.Year;
                    if (drtCustomerRow.WDOB > today.AddYears(-WAge)) WAge--;
                    TableHTML += "<table cellpadding='0' cellspacing='0'><tr><td><b>Category</b></td><td> : " + drtCustomerRow.Category + "</td></tr>";
                    TableHTML += "<tr><td><b>OPD No.</b></td><td> : " + drtCustomerRow.OPDNo + "</td></tr></table>";
                    //int test = ((drtCustomerRow.HDOB.Year - drtCustomerRow.CreatedOn.Year) * 12) + drtCustomerRow.HDOB.Month - drtCustomerRow.CreatedOn.Month;
                    // double test1 = drtCustomerRow.HDOB.Subtract(drtCustomerRow.CreatedOn).Days / (365.25 / 12);

                    TableHTML += "<table cellpadding='0' cellspacing='0'><thead><tr><th style='text-align:center;'><br/><br/>Husband Details</th></tr></table>";
                   

                    TableHTML += "<table cellpadding='0' cellspacing='0'><thead>";
                    TableHTML += "<tr><td><b>Husband Name</b></td><td> : " + drtCustomerRow.HName + "</td><td><b>Referred By</b></td><td> : " + drtCustomerRow.HReferredBy + "  " + drtCustomerRow.HReferredDetails + "</td></tr>";
                    //TableHTML += "<tr><td><b>Date of Birth/ Age</b></td><td> : " + drtCustomerRow.HDOB.ToString("dd/MM/yyyy") +"," + HAge + "</td><td><b>Blood Group</b></td><td> : " + drtCustomerRow.HBloodGroup + "</td></tr>";
                    TableHTML += "<tr><td><b>Age</b></td><td> : " + drtCustomerRow.HAge + "</td><td><b>Blood Group</b></td><td> : " + drtCustomerRow.HBloodGroup + "</td></tr>";

                    TableHTML += "<tr><td><b>Address</b></td><td> : " + drtCustomerRow.HAddress + "</td><td><b>Pincode</b></td><td> : " + drtCustomerRow.HPincode + "</td></tr>";
                    TableHTML += "<tr><td><b>Nationality</b></td><td> : " + drtCustomerRow.HNationality + "</td><td><b>City/Country</b></td><td> : " + drtCustomerRow.HCity +"/"+ drtCustomerRow.HCountry + "</td></tr>";
                    TableHTML += "<tr><td><b>Email Address</b></td><td> : " + drtCustomerRow.HEmail + "</td><td><b>Mobile/Ph.No</b></td><td> : " + drtCustomerRow.HMobileNo + "</td></tr>";
                   // TableHTML += "<tr><td><b>Photo</b></td><td> : " + drtCustomerRow.HImage + "</td><td><b>Proof</b></td><td> : " + drtCustomerRow.HIDProof + "</td></tr>";
                    TableHTML += "<tr><td><b>Profession</b></td><td> : " + drtCustomerRow.HProfession + "</td><td><b>Photo</b></td><td rowspan='3' align='right' style='padding-left:25px;'><img src='" + drtCustomerRow.HImage + "' height='144' width='192' alt=''/></td></tr></table>";
                   
                    TableHTML += "<table cellpadding='0' cellspacing='0'><thead><tr><th style='text-align:center;'><br/><br/>Wife Details</th></tr></table>";

                    TableHTML += "<table cellpadding='0' cellspacing='0'><thead>";
                    TableHTML += "<tr><td><b>Wife Name</b></td><td> : " + drtCustomerRow.WName + "</td><td><b>Referred By</b></td><td> : " + drtCustomerRow.WReferredBy +"  " + drtCustomerRow.WReferredDetails + "</td></tr>";
                    //TableHTML += "<tr><td><b>Date of Birth/ Age</b></td><td> : " + drtCustomerRow.WDOB.ToString("dd/MM/yyyy") + "," + WAge + "</td><td><b>Blood Group</b></td><td> : " + drtCustomerRow.WBloodGroup + "</td></tr>";
                    TableHTML += "<tr><td><b>Age</b></td><td> : " + drtCustomerRow.WAge + "</td><td><b>Blood Group</b></td><td> : " + drtCustomerRow.WBloodGroup + "</td></tr>";
                    TableHTML += "<tr><td><b>Address</b></td><td> : " + drtCustomerRow.WAddress + "</td><td><b>Pincode</b></td><td> : " + drtCustomerRow.WPincode + "</td></tr>";
                    TableHTML += "<tr><td><b>Nationality</b></td><td> : " + drtCustomerRow.WNationality + "</td><td><b>City/Country</b></td><td> : " + drtCustomerRow.WCity + "/" + drtCustomerRow.WCountry + "</td></tr>";
                    TableHTML += "<tr><td><b>Email Address</b></td><td> : " + drtCustomerRow.WEmail + "</td><td><b>Mobile/Ph.No</b></td><td> : " + drtCustomerRow.WMobileNo + "</td></tr>";
                    TableHTML += "<tr><td><b>Profession</b></td><td> : " + drtCustomerRow.WProfession +"</td><td><b>Photo</b></td><td rowspan='3' align='right' style='padding-left:25px;'><img src='" + drtCustomerRow.WImage + "' height='144' width='192' alt=''/></td></tr></table>";

                    //TableHTML += "<table cellpadding='0' cellspacing='0' class='border_details'><thead>";
                    //TableHTML += "<tr><th><b>&nbsp S.No</b></th><th><b>Description</b></th><th  style='text-align:right'><b>Charges</b></th><th  style='text-align:right'><b>Amount</b></th></tr></thead>";

                    //if (dsReportData.tOPBillingTrans.Rows.Count > 0)
                    //{
                    //    int sno = 1;
                    //    foreach (ReportDataSet.tOPBillingTransRow drtJobCardComplaintsRow in dsReportData.tOPBillingTrans)
                    //    {
                    //        TableHTML += "<tr><td>&nbsp &nbsp" + sno + "</td><td>" + drtJobCardComplaintsRow.DescriptionName + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.Charges + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.Subtotal + "</td></tr>";
                    //        sno++;
                    //    }
                    //}

                    //TableHTML += "<tr><td colspan='3' style='text-align:right'><b>Total Amount</b></td><td style='text-align:right'>" + drtJobCardRow.TotalAmount + "</td></tr>";
                    //TableHTML += "<tr><td colspan='3' style='text-align:right'><b>Dicount Amount</b></td><td style='text-align:right'>" + drtJobCardRow.DiscountAmount + "</td></tr>";
                    //TableHTML += "<tr><td colspan='3' style='text-align:right'><b>Net Amount</b></td><td style='text-align:right'>" + drtJobCardRow.NetAmount + "</td></tr>";
                    //TableHTML += "<tr><td colspan='3' style='text-align:right'><b>Paid Amount</b></td><td style='text-align:right'>" + drtJobCardRow.PaidAmount + "</td></tr>";
                    //TableHTML += "<tr><td colspan='3' style='text-align:right'><b>Balance Amount</b></td><td style='text-align:right'>" + drtJobCardRow.BalanceAmount + "</td></tr>";

                    TableHTML += "<table cellpadding='0' cellspacing='0'>";
                    TableHTML += "<tr><td style='width:60%'><b>The above furnished informaton are true and correct</b><br/><br/></td></tr>";                   
                    TableHTML += "<tr><th colspan='2' ></th></tr>";
                    TableHTML += "<tr><td colspan='2'><b>Authorized By</b><br/><br/><br/></td></tr>";
                    TableHTML += "<tr><td style='text-align:left'><b>Date :" + drtCustomerRow.CreatedOn.ToString("dd/MM/yyyy") + "</b></td><td style='text-align:right'><b>Signature of Husband / Wife</b></td></tr></table>";
                }

                divOPInvoice.InnerHtml = TableHTML;
            }

        }
        catch (Exception ex)
        {
            sException = "frmPrintPatient | " + ex.ToString();
            Log.Write(sException);
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
        var stylecss = @"<link href='css/Print.css' rel='stylesheet type='text/css' /><link href='css/fonts/font-awesome.min.css' rel='stylesheet type='text/css' />";
        htmlWriter.Write(javascript);
        htmlWriter.Write(stylecss);
        divPdf.RenderControl(htmlWriter);
        streamWriter.Write(strBuilder.ToString());
        streamWriter.Flush();
        Response.End();
    }
}