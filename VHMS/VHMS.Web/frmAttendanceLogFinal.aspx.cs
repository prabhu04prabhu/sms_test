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
using System.Globalization;
using System.Web.Services;

public partial class frmAttendanceLogFinal : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    public static decimal iDeductionAmount = 0;
    public static int iLateMinutes = 0;
    public static int UserID = 0;
    public static int iiLateMinutes = 0;
    public static int iWorkingMinutes = 0;
    public static string iInTime;
    public static string iOutTime;
    public static decimal perdaysalary = 0;
    public static int TotalDays = 0;
    public static int HolidayCount = 0;
    // HolidaysMasterSystem oHolidaysMasterSystem = null;
    string sException = string.Empty;
    decimal InvoiceAmount = 0, NetAmount = 0, DiscountAmount = 0, TotalQty = 0, TotalAmount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["UserID"] != null) UserID = Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString());

        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();


            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

        }
    }
   
    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadReport();
        LoadDisplay();
    }
    private void LoadDisplay()
    {
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        ReportDataSet dsReportData = new ReportDataSet();

        dsData = VHMS.DataAccess.VHMSReports.GetAttendanceLogByDynamic(txtFromDate.Text,0);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tAttendanceLogFinal";

                gvPurchase.DataSource = dsData.Tables[0];
                gvPurchase.DataBind();
            }

        }
        catch (Exception ex)
        {
            sException = "frmPrintPurchase | " + ex.ToString();
            Log.Write(sException);
        }
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        if (gvPurchase.Rows.Count < 1)
        {
            string display = "no Records Found.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
        }
        else
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=PurchaseReports.pdf");
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
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    private ReportDatestNew Loadhift()
    {
        dsData = VHMS.DataAccess.VHMSReports.GetShift(0);
        ReportDatestNew dsReportData = new ReportDatestNew();

        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "tShift";
            foreach (DataRow drow in dsData.Tables[0].Rows) dsReportData.tShift.ImportRow(drow);
        }


        return dsReportData;
    }
    private ReportDatestNew LoadData(int EmployeeID)
    {
        dsData = VHMS.DataAccess.VHMSReports.GetAttendanceLogByDynamic(txtFromDate.Text, EmployeeID);
        ReportDatestNew dsReportData = new ReportDatestNew();

        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "tAttendanceLogFinal";
            foreach (DataRow drow in dsData.Tables[0].Rows) dsReportData.tAttendanceLogFinal.ImportRow(drow);
        }
        return dsReportData;
    }
    private ReportDatestNew LoadEmployee()
    {
        dsData = VHMS.DataAccess.VHMSReports.GetEmployee(0);
        ReportDatestNew dsReportData = new ReportDatestNew();

        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "tEmployee";
            foreach (DataRow drow in dsData.Tables[0].Rows) dsReportData.tEmployee.ImportRow(drow);
        }
        return dsReportData;
    }
    [WebMethod]
    protected void LoadReport()
    {
        VHMS.DataAccess.Employee oEmployeeMasSystem = null;
        ReportDatestNew dsEmployee = null;
        // VHMS.DataAccess.AttendanceLog oAttendanceLogMasterSystem = null;
        ReportDatestNew dsAttendanceLog = null;
        ReportDatestNew dsAttendanceLogFinal = null;
        VHMS.Entity.AttendanceLog objRegion = null;
        DataSet DataRow = new DataSet();
        VHMS.Entity.Employee objEmployee = null;
        VHMS.Entity.User objUser = null;
        int InID = 0, OutID = 0;

        string From_date = txtFromDate.Text;

        string date = DateTime.ParseExact(From_date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

        DateTime dts = Convert.ToDateTime(date);
        try
        {
            // oEmployeeMasSystem = new VHMS.DataAccess.Employee();
            dsEmployee = new ReportDatestNew();
            // dsAttendanceLogFinal = new ReportDatestNew();
            objRegion = new VHMS.Entity.AttendanceLog();
            objEmployee = new VHMS.Entity.Employee();
            dsEmployee = LoadEmployee();
            dsAttendanceLog = new ReportDatestNew();
            // oAttendanceLogMasterSystem = new VHMS.DataAccess.AttendanceLog();
            ReportDatestNew.tAttendanceLogFinalRow drGSTAnnexureII = null;
            objUser = new VHMS.Entity.User();

            foreach (ReportDatestNew.tEmployeeRow drtEmployeeRow in dsEmployee.tEmployee.Rows)
            {
                dsAttendanceLogFinal = LoadData(drtEmployeeRow.PK_EmployeeID);
                //  if (dsAttendanceLogFinal.tAttendanceLogFinal.Rows.Count == 0)
                //{
                ReportDatestNew.tAttendanceLogFinalRow[] drTb_AttendanceLogRow = (ReportDatestNew.tAttendanceLogFinalRow[])dsAttendanceLogFinal.tAttendanceLogFinal.Select("FK_EmployeeID=" + drtEmployeeRow.PK_EmployeeID);

                if (drTb_AttendanceLogRow.Length > 0)
                    drGSTAnnexureII = drTb_AttendanceLogRow[0];
                else
                    drGSTAnnexureII = (ReportDatestNew.tAttendanceLogFinalRow)dsAttendanceLogFinal.tAttendanceLogFinal.NewtAttendanceLogFinalRow();

                dsAttendanceLog = new ReportDatestNew();
                objRegion = new VHMS.Entity.AttendanceLog();
                dsData = VHMS.DataAccess.VHMSReports.GetInAttendanceLog("TOP 1 * ", "DeviceLogs_" + dts.Month + "_" + dts.Year, " UserId=" + drtEmployeeRow.PK_EmployeeID + " and CONVERT(DATE,LogDate,103) = CONVERT(DATE,'" + txtFromDate.Text + "',103) order by 1 Asc");

                if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
                {
                    dsData.Tables[0].TableName = "Tb_EmployeeAttendence";
                    foreach (DataRow drow in dsData.Tables[0].Rows) dsAttendanceLog.Tb_EmployeeAttendence.ImportRow(drow);
                }
                if (dsAttendanceLog.Tb_EmployeeAttendence.Rows.Count > 0)
                {
                    foreach (ReportDatestNew.Tb_EmployeeAttendenceRow drRows in dsAttendanceLog.Tb_EmployeeAttendence.Rows)
                    {

                        objUser.UserID = UserID;
                        InID = drRows.DeviceLogId;
                        objEmployee.EmployeeID = Convert.ToInt32(drRows.UserId);
                        objEmployee.EmployeeName = drtEmployeeRow.EmployeeName;
                        objRegion.Employee = objEmployee;
                        objRegion.sPunchDate = drRows.LogDate.ToString("dd/MM/yyyy");
                        objRegion.PunchInTime = drRows.LogDate.ToString("hh:mm tt");
                        objRegion.DeductionAmt = 0;
                        objRegion.LateMinutes = 0;
                        objRegion.SpecialStatus = "Present";
                        objRegion.Status = "IN";
                        objRegion.CreatedBy = objUser;
                        objRegion.ModifiedBy = objUser;
                        objRegion.Active = true;
                        objRegion.Edit = 0;
                    }
                }
                dsAttendanceLog = new ReportDatestNew();


                dsData = VHMS.DataAccess.VHMSReports.GetInAttendanceLog("TOP 1 * ", "DeviceLogs_" + dts.Month + "_" + dts.Year, " UserId=" + drtEmployeeRow.PK_EmployeeID + " and CONVERT(DATE,LogDate,103) = CONVERT(DATE,'" + txtFromDate.Text + "',103) order by 1 Desc");

                if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
                {
                    dsData.Tables[0].TableName = "Tb_EmployeeAttendence";
                    foreach (DataRow drow in dsData.Tables[0].Rows) dsAttendanceLog.Tb_EmployeeAttendence.ImportRow(drow);
                }

                if (dsAttendanceLog.Tb_EmployeeAttendence.Rows.Count > 0)
                {
                    foreach (ReportDatestNew.Tb_EmployeeAttendenceRow drRows in dsAttendanceLog.Tb_EmployeeAttendence.Rows)
                    {
                        double tMinutes = 0; double tWorkingMinutes = 0;
                        double LatedMinutes = 0;
                        OutID = drRows.DeviceLogId;
                        if (InID != OutID)
                        {
                            objRegion.PunchOutTime = drRows.LogDate.ToString("hh:mm tt");
                            objRegion.Status = "Out";
                        }
                        else
                        {
                            objRegion.PunchOutTime = objRegion.PunchInTime;
                            objRegion.Status = " No OutTime";
                        }

                        TimeSpan time1 = (DateTime.ParseExact(objRegion.PunchOutTime, "h:mm tt", CultureInfo.InvariantCulture)).TimeOfDay;
                        TimeSpan time2 = (DateTime.ParseExact(objRegion.PunchInTime, "h:mm tt", CultureInfo.InvariantCulture)).TimeOfDay;
                        TimeSpan duration = time1 - time2;
                        tMinutes = duration.TotalMinutes;
                        double TotalHours = Math.Floor(tMinutes / 60);
                        double TotalMinutes = (tMinutes % 60);
                        objRegion.Duration = (duration.ToString()).Substring(0, 5);

                        int intMonth = DateTime.Now.Month;
                        int intYear = DateTime.Now.Year;
                        objRegion.AttendanceLogID = drGSTAnnexureII.PK_AttendanceLogID;
                        int intDaysThisMonth = DateTime.DaysInMonth(intYear, intMonth);
                        TotalDays = intDaysThisMonth - HolidayCount;
                        perdaysalary = drtEmployeeRow.NetSalary / TotalDays;

                        //TimeSpan start = (DateTime.ParseExact("01:00 PM", "h:mm tt", CultureInfo.InvariantCulture)).TimeOfDay;
                        //if (start <= (DateTime.ParseExact(objRegion.PunchInTime, "h:mm tt", CultureInfo.InvariantCulture)).TimeOfDay)
                        //{
                        //    objRegion.SpecialStatus = "Half Day";
                        //    objRegion.DeductionAmt = perdaysalary / 2;
                        //}
                        //else if (objRegion.SpecialStatus == "Permission")
                        //{
                        //    objRegion.DeductionAmt = 0;
                        //}
                        //else
                        //{
                        objRegion.SpecialStatus = "Present";

                        TimeSpan OutTime = (DateTime.ParseExact(drtEmployeeRow.EmployeeOutTime, "h:mm tt", CultureInfo.InvariantCulture)).TimeOfDay;
                        TimeSpan InTime = (DateTime.ParseExact(drtEmployeeRow.EmployeeInTime, "h:mm tt", CultureInfo.InvariantCulture)).TimeOfDay;
                        TimeSpan Workingduration = OutTime - InTime;
                        tWorkingMinutes = Workingduration.TotalMinutes;

                        if (tWorkingMinutes > (Convert.ToInt32(tMinutes)))
                            objRegion.LateMinutes = Convert.ToInt32(tWorkingMinutes) - (Convert.ToInt32(tMinutes));
                        else
                            objRegion.LateMinutes = 0;

                        double Overtime = tMinutes + 60;
                        if (tWorkingMinutes < Overtime)
                            objRegion.OvertimeCount = 1;
                        else
                            objRegion.OvertimeCount = 0;

                        //iiLateMinutes = 30;
                        //int amt = 0;
                        //if (iiLateMinutes <= drGSTAnnexureII.LateMinutes)
                        //{
                        //    LatedMinutes = drGSTAnnexureII.LateMinutes - iiLateMinutes;
                        //    amt = 20;
                        //}
                        //decimal Latehours = Math.Round(Convert.ToDecimal(LatedMinutes) / 15);
                        //drGSTAnnexureII.DeductionAmt = Latehours * iDeductionAmount + amt;
                        //}

                        if (drGSTAnnexureII.RowState == System.Data.DataRowState.Detached)
                            dsAttendanceLogFinal.tAttendanceLogFinal.Rows.Add(drGSTAnnexureII);
                    }
                }
                else
                {
                    objUser.UserID = UserID;                  
                    objEmployee.EmployeeID = drtEmployeeRow.PK_EmployeeID;
                    objEmployee.EmployeeName = drtEmployeeRow.EmployeeName;
                    objRegion.Employee = objEmployee;
                    objRegion.sPunchDate = From_date;
                    objRegion.PunchInTime = "";
                    objRegion.DeductionAmt = 0;
                    objRegion.LateMinutes = 0;
                    objRegion.Status = "IN";
                    objRegion.CreatedBy = objUser;
                    objRegion.ModifiedBy = objUser;
                    objRegion.Active = true;
                    objRegion.PunchOutTime = "";
                    objRegion.Status = " No OutTime";                    
                    objRegion.Duration ="0";                    
                    objRegion.AttendanceLogID = drGSTAnnexureII.PK_AttendanceLogID;                    
                    objRegion.SpecialStatus = "Absent";
                    objRegion.OvertimeCount = 0;
                    objRegion.Edit = 0;
                    if (drtEmployeeRow.IsActive == true)
                    {
                        if (objRegion.AttendanceLogID > 0)
                            VHMS.DataAccess.AttendanceLog.UpdateAttendanceLog(objRegion);
                        else
                            VHMS.DataAccess.AttendanceLog.AddAttendanceLog(objRegion);
                    }
                  
                }
                if (dsAttendanceLog.Tb_EmployeeAttendence.Rows.Count > 0)
                {
                    if (objRegion.AttendanceLogID > 0)
                        VHMS.DataAccess.AttendanceLog.UpdateAttendanceLog(objRegion);
                    else
                        VHMS.DataAccess.AttendanceLog.AddAttendanceLog(objRegion);
                }
                // }
            }
            //gvPurchase.DataSource = dsAttendanceLogFinal.tAttendanceLogFinal;
            // gvPurchase.DataBind();

            // foreach (var Row in dsAttendanceLogFinal.Tables["1"])

        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", ex.ToString(), true);
            ex.ToString();
        }
    }
   


}

