using System;
using System.Collections;
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

public partial class frmPunchLogFinal : System.Web.UI.Page
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
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadReport();
        LoadDisplay();
        LoadReportLog();
    }
    private void LoadDisplay()
    {
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        ReportDataSet dsReportData = new ReportDataSet();

        dsData = VHMS.DataAccess.VHMSReports.GetPunchLogByDynamic(txtFromDate.Text, txtToDate.Text, 0);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tPunchLog";
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
        dsData = VHMS.DataAccess.VHMSReports.GetPunchLogByDynamic(txtFromDate.Text, txtToDate.Text, EmployeeID);
        ReportDatestNew dsReportData = new ReportDatestNew();

        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "tPunchLog";
            foreach (DataRow drow in dsData.Tables[0].Rows) dsReportData.tPunchLog.ImportRow(drow);
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
        ReportDatestNew dsPunchLog = null;
        ReportDatestNew dsPunchLogFinal = null;
        VHMS.Entity.PunchLog objRegion = null;
        DataSet DataRow = new DataSet();
        VHMS.Entity.Employee objEmployee = null;
        VHMS.Entity.User objUser = null;
        int InID = 0, OutID = 0;


        string From_date = txtFromDate.Text;

        string date = DateTime.ParseExact(From_date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

        DateTime dts = Convert.ToDateTime(date);
        try
        {
            dsEmployee = new ReportDatestNew();
            objRegion = new VHMS.Entity.PunchLog();
            objEmployee = new VHMS.Entity.Employee();
            dsEmployee = LoadEmployee();
            dsPunchLog = new ReportDatestNew();
            ReportDatestNew.tPunchLogRow drGSTAnnexureII = null;
            objUser = new VHMS.Entity.User();

            foreach (ReportDatestNew.tEmployeeRow drtEmployeeRow in dsEmployee.tEmployee.Rows)
            {
                dsPunchLogFinal = LoadData(drtEmployeeRow.PK_EmployeeID);
                ReportDatestNew.tPunchLogRow[] drTb_PunchLogRow = (ReportDatestNew.tPunchLogRow[])dsPunchLogFinal.tPunchLog.Select("FK_EmployeeID=" + drtEmployeeRow.PK_EmployeeID);

                if (drTb_PunchLogRow.Length > 0)
                    drGSTAnnexureII = drTb_PunchLogRow[0];
                else
                    drGSTAnnexureII = (ReportDatestNew.tPunchLogRow)dsPunchLogFinal.tPunchLog.NewtPunchLogRow();

                dsPunchLog = new ReportDatestNew();
                objRegion = new VHMS.Entity.PunchLog();
                dsData = VHMS.DataAccess.VHMSReports.GetInAttendanceLog(" * ", "DeviceLogs_" + dts.Month + "_" + dts.Year, " UserId=" + drtEmployeeRow.PK_EmployeeID + " and CONVERT(DATE,LogDate,103) BETWEEN  COALESCE (CONVERT(DATE,'" + txtFromDate.Text + "',103),LogDate) and  COALESCE (CONVERT(DATE,'" + txtToDate.Text + "',103),LogDate) order by DeviceLogId,LogDate,UserId asc ");
                if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
                {
                    dsData.Tables[0].TableName = "Tb_EmployeeAttendence";
                    foreach (DataRow drow in dsData.Tables[0].Rows) dsPunchLog.Tb_EmployeeAttendence.ImportRow(drow);
                }
                foreach (ReportDatestNew.Tb_EmployeeAttendenceRow drRows in dsPunchLog.Tb_EmployeeAttendence.Rows)
                {
                    objUser.UserID = UserID;
                    InID = drRows.DeviceLogId;
                    objEmployee.EmployeeID = Convert.ToInt32(drRows.UserId);
                    objEmployee.EmployeeName = drtEmployeeRow.EmployeeName;
                    objRegion.Employee = objEmployee;
                    objRegion.sLogDate = drRows.LogDate.ToString();
                    objRegion.LogTime = drRows.LogDate.ToString("hh:mm tt");
                    objRegion.CreatedBy = objUser;
                    objRegion.ModifiedBy = objUser;
                    VHMS.DataAccess.PunchLog.AddPunchLog(objRegion);
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", ex.ToString(), true);
            ex.ToString();
        }
    }

    public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
    {
        for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
            yield return day;
    }
    protected void LoadReportLog()
    {
        VHMS.DataAccess.Employee oEmployeeMasSystem = null;
        ReportDatestNew dsEmploye = null;
        ReportDatestNew dsAttendance = null;
        ReportDatestNew dsAttendanceFinal = null;
        VHMS.Entity.AttendanceLog objRegion = null;
        DataSet DataRow = new DataSet();
        VHMS.Entity.Employee objEmployee = null;
        VHMS.Entity.User objUser = null;
        DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null);
        string From_date = From.ToString("dd/MM/yyyy");
        DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null);
        string To_date = To.ToString("dd/MM/yyyy");
        double tMinutes = 0;
        double tWorkingMinutes = 0;
        try
        {
            dsEmploye = new ReportDatestNew();
            objRegion = new VHMS.Entity.AttendanceLog();
            objEmployee = new VHMS.Entity.Employee();
            dsEmploye = LoadEmployee();
            dsAttendance = new ReportDatestNew();
            ReportDatestNew.tAttendanceLogFinalRow drGSTAnnexureII = null;
            objUser = new VHMS.Entity.User();
            foreach (ReportDatestNew.tEmployeeRow drtEmployeeRow in dsEmploye.tEmployee.Rows)
            {
                dsAttendanceFinal = LoadData(drtEmployeeRow.ESLID);
                ReportDatestNew.tAttendanceLogFinalRow[] drTb_AttendanceLogRow = (ReportDatestNew.tAttendanceLogFinalRow[])dsAttendanceFinal.tAttendanceLogFinal.Select("FK_EmployeeID=" + drtEmployeeRow.PK_EmployeeID);
                if (drTb_AttendanceLogRow.Length > 0)
                    drGSTAnnexureII = drTb_AttendanceLogRow[0];
                else
                    drGSTAnnexureII = (ReportDatestNew.tAttendanceLogFinalRow)dsAttendanceFinal.tAttendanceLogFinal.NewtAttendanceLogFinalRow();

                dsAttendance = new ReportDatestNew();
                objRegion = new VHMS.Entity.AttendanceLog();

                dsData = VHMS.DataAccess.VHMSReports.GetpunchLog(txtFromDate.Text, txtToDate.Text, drtEmployeeRow.ESLID);

                if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
                {
                    dsData.Tables[0].TableName = "Tb_EmployeeLog";
                    foreach (DataRow drow in dsData.Tables[0].Rows) dsAttendance.Tb_EmployeeLog.ImportRow(drow);
                }

                DateTime[] dates_list = Enumerable.Range(0, 1 + To.Subtract(From).Days).Select(offset => From.AddDays(offset)).ToArray();

                if (dsAttendance.Tb_EmployeeLog.Rows.Count > 0)
                {
                    string EmployeeID_absent = "";

                    foreach (ReportDatestNew.Tb_EmployeeLogRow drRows in dsAttendance.Tb_EmployeeLog.Rows)
                    {
                        dates_list = dates_list.Where(val => val != drRows.LogDate).ToArray();

                        objUser.UserID = UserID;
                        objEmployee.EmployeeID = drtEmployeeRow.ESLID;
                        objEmployee.EmployeeID = Convert.ToInt32(drRows.FK_EmployeeID);
                        EmployeeID_absent = drRows.FK_EmployeeID;
                        objEmployee.EmployeeName = drtEmployeeRow.EmployeeName;
                        objRegion.Employee = objEmployee;
                        objRegion.sPunchDate = drRows.LogDate.ToString("dd/MM/yyyy");
                        objRegion.PunchInTime = drRows.Intime;
                        objRegion.PunchOutTime = drRows.Outtime;
                        objRegion.DeductionAmt = 0;
                        objRegion.LateMinutes = 0;
                        objRegion.CreatedBy = objUser;
                        objRegion.ModifiedBy = objUser;
                        objRegion.Active = true;
                        objRegion.Edit = 0;
                        //TotalMinutes
                        TimeSpan time1 = (DateTime.ParseExact(objRegion.PunchOutTime, "h:mm tt", CultureInfo.InvariantCulture)).TimeOfDay;
                        TimeSpan time2 = (DateTime.ParseExact(objRegion.PunchInTime, "h:mm tt", CultureInfo.InvariantCulture)).TimeOfDay;
                        TimeSpan duration = time1 - time2;
                        tMinutes = duration.TotalMinutes;
                        double TotalHours = Math.Floor(tMinutes / 60);
                        double TotalMinutes = (tMinutes % 60);
                        objRegion.Duration = (duration.ToString()).Substring(0, 5);
                        //LateMinutes
                        TimeSpan OutTime = (DateTime.ParseExact(drtEmployeeRow.EmployeeOutTime, "h:mm tt", CultureInfo.InvariantCulture)).TimeOfDay;
                        TimeSpan InTime = (DateTime.ParseExact(drtEmployeeRow.EmployeeInTime, "h:mm tt", CultureInfo.InvariantCulture)).TimeOfDay;
                        TimeSpan Workingduration = OutTime - InTime;
                        tWorkingMinutes = Workingduration.TotalMinutes;
                        if (tWorkingMinutes > (Convert.ToInt32(tMinutes)))
                            objRegion.LateMinutes = Convert.ToInt32(tWorkingMinutes) - (Convert.ToInt32(tMinutes));
                        else
                            objRegion.LateMinutes = 0;
                        //OvertimeCount
                        double Overtime = tMinutes + 60;
                        if (tWorkingMinutes < Overtime)
                            objRegion.OvertimeCount = 1;
                        else
                            objRegion.OvertimeCount = 0;
                        //Status
                        objRegion.Status = " No OutTime";

                        objRegion.AttendanceLogID = drGSTAnnexureII.PK_AttendanceLogID;
                        objRegion.SpecialStatus = "Present";
                        if (drtEmployeeRow.IsActive == true)
                        {
                            VHMS.DataAccess.AttendanceLog.AddAttendanceLog(objRegion);
                        }
                    }
                }
                else

                    foreach (var ab_date in dates_list)
                    {
                        objUser.UserID = UserID;
                        objEmployee.EmployeeID = drtEmployeeRow.ESLID;
                        objEmployee.EmployeeName = drtEmployeeRow.EmployeeName;
                        objRegion.Employee = objEmployee;
                        objRegion.sPunchDate = ab_date.ToString("dd/MM/yyyy"); ;
                        objRegion.PunchInTime = "";
                        objRegion.DeductionAmt = 0;
                        objRegion.LateMinutes = 0;
                        objRegion.Status = "IN";
                        objRegion.CreatedBy = objUser;
                        objRegion.ModifiedBy = objUser;
                        objRegion.Active = true;
                        objRegion.PunchOutTime = "";
                        objRegion.Status = " No OutTime";
                        objRegion.Duration = "0";
                        objRegion.AttendanceLogID = drGSTAnnexureII.PK_AttendanceLogID;
                        objRegion.SpecialStatus = "Absent";
                        objRegion.OvertimeCount = 0;
                        objRegion.Edit = 0;
                        if (drtEmployeeRow.IsActive == true)
                        {
                            if (objRegion.AttendanceLogID > 0)
                                VHMS.DataAccess.AttendanceLog.AddAttendanceLog(objRegion);
                        }
                    }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", ex.ToString(), true);
    ex.ToString();
        }
    }
}

