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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using System.Globalization;

public partial class frmAttendancelogReport : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    int months = 0;
    string Query = "Active=1";
    ReportDocument rprt = new ReportDocument();
    decimal InvoiceAmount = 0, NetAmount = 0, DiscountAmount = 0, TotalQty = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.btnExportReport);
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            ddlMonth.Text = DateTime.Now.ToString("MMMM");
            ddlyear.Text = DateTime.Now.Year.ToString();
            LoadCustomer();
            MonthList();
            Load_Year();
            Load_MonthValues();
        }
        // LoadReport();
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rprt.Close();
        rprt.Dispose();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadReport();
    }
    private ReportDatestNew LoadEmployee()
    {
        dsData = VHMS.DataAccess.VHMSReports.GetEmployee(Convert.ToInt32(ddlCustomer.SelectedValue));
        ReportDatestNew dsReportData = new ReportDatestNew();

        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "tEmployee";
            foreach (DataRow drow in dsData.Tables[0].Rows) dsReportData.tEmployee.ImportRow(drow);
        }
        return dsReportData;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    private void LoadReport()
    {
        Load_MonthValues();
        ReportDatestNew dsLog = null;
        ReportDatestNew dsAttendance = null;
        ReportDatestNew dsEmployee = null;
        rprt.Dispose();
        rprt.Clone();
        ReportDatestNew.V_AttendanceLogFinalRow drGSTAnnexureI = null;
        ReportDatestNew dsAttendanceLogs = null;
        rprt.Close();
        rprt = new ReportDocument();
        ReportDatestNew.V_AttendanceLogFinalRow drGSTAnnexureII = null;
        try
        {
            dsLog = new ReportDatestNew();
            dsAttendance = new ReportDatestNew();
            dsEmployee = new ReportDatestNew();
            dsAttendanceLogs = new ReportDatestNew();
            if (Convert.ToInt32(months) > 0)
                Query = months.ToString();
            dsLog = LoadData();
            foreach (DateTime dates in AllDatesInMonth(Convert.ToInt32(ddlyear.Text), Convert.ToInt32(months)))
            {
                string From_date = dates.ToString("dd/MM/yyyy");

                string date = DateTime.ParseExact(From_date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

                drGSTAnnexureII = (ReportDatestNew.V_AttendanceLogFinalRow)dsLog.V_AttendanceLogFinal.NewV_AttendanceLogFinalRow();
                ReportDatestNew.Tb_AttendanceLogFinalRow drtbGSTAnnexureII = null;

                ReportDatestNew.tAttendanceLogFinalRow[] drTb_AttendanceLogRow = (ReportDatestNew.tAttendanceLogFinalRow[])dsLog.tAttendanceLogFinal.Select("PunchDate ='" + date + "'");
                if (Convert.ToInt32(ddlCustomer.SelectedValue) > 0)
                {
                    dsEmployee = LoadEmployee();
                    foreach (ReportDatestNew.tEmployeeRow drtEmployeeRow in dsEmployee.tEmployee.Rows)
                    {
                        if (drTb_AttendanceLogRow.Length > 0)
                        {
                            dsAttendanceLogs = LoadAttendance(dates.ToString("yyyy-MM-dd"), Convert.ToInt32(ddlCustomer.SelectedValue));
                            foreach (ReportDatestNew.V_AttendanceLogFinalRow drRows in dsAttendanceLogs.V_AttendanceLogFinal.Rows)
                            {
                                drGSTAnnexureI = (ReportDatestNew.V_AttendanceLogFinalRow)dsLog.V_AttendanceLogFinal.NewV_AttendanceLogFinalRow();
                                drGSTAnnexureI.Status = "";
                                drGSTAnnexureI.SpecialStatus = drRows.SpecialStatus;
                                drGSTAnnexureI.Duration = drRows.Duration + " Hours";
                                drGSTAnnexureI.FK_EmployeeID = drRows.FK_EmployeeID;
                                drGSTAnnexureI.PunchDate = drRows.PunchDate;
                                drGSTAnnexureI.PunchInTime = drRows.PunchInTime;
                                drGSTAnnexureI.PunchOutTime = drRows.PunchOutTime;
                                drGSTAnnexureI.DeductionAmt = drRows.DeductionAmt;
                                drGSTAnnexureI.LateMinutes = drRows.LateMinutes;
                                drGSTAnnexureI.EmployeeName = ddlCustomer.Text;
                                drGSTAnnexureI.EmployeeCode = drtEmployeeRow.EmployeeCode;
                                drGSTAnnexureI.PhoneNo1 = drtEmployeeRow.PhoneNo1;
                                drGSTAnnexureI.FK_CreatedBy = drRows.FK_CreatedBy;
                                drGSTAnnexureI.Active = true;
                                if (drGSTAnnexureI.RowState == System.Data.DataRowState.Detached)
                                    dsLog.V_AttendanceLogFinal.Rows.Add(drGSTAnnexureI);
                            }
                        }
                        else
                        {
                            drGSTAnnexureII.FK_EmployeeID = Convert.ToInt32(ddlCustomer.SelectedValue);
                            drGSTAnnexureII.PunchDate = dates;
                            drGSTAnnexureII.PunchInTime = "-";
                            drGSTAnnexureII.PunchOutTime = "-";
                            drGSTAnnexureII.Duration = "-";
                            drGSTAnnexureII.DeductionAmt = 0;
                            drGSTAnnexureII.LateMinutes = 0;
                            drGSTAnnexureII.EmployeeCode = drtEmployeeRow.EmployeeCode;
                            drGSTAnnexureII.PhoneNo1 = drtEmployeeRow.PhoneNo1;
                            drGSTAnnexureII.EmployeeName = ddlCustomer.SelectedItem.ToString();
                            drGSTAnnexureII.FK_CreatedBy = 1008;
                            drGSTAnnexureII.Active = true;
                            if (drGSTAnnexureII.RowState == System.Data.DataRowState.Detached)
                                dsLog.V_AttendanceLogFinal.Rows.Add(drGSTAnnexureII);
                        }
                    }
                }
            }
            rprt = new ReportDocument();
            rprt.Load(Server.MapPath("~/Reports/rptAttendanceLog.rpt"));
            TextObject t1 = (TextObject)rprt.ReportDefinition.Sections["Section1"].ReportObjects["txtFilter"];
            t1.Text = "Report generated in the month of " + ddlMonth.SelectedItem.ToString() + " - " + ddlyear.SelectedItem.ToString() + "";
            rprt.SetDataSource(dsLog);
            this.CRDischargeSummaryReport.ReportSource = rprt;
            CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            CRDischargeSummaryReport.Zoom(100);
            this.CRDischargeSummaryReport.DataBind();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    public static IEnumerable<DateTime> AllDatesInMonth(int year, int month)
    {
        int days = DateTime.DaysInMonth(year, month);
        for (int day = 1; day <= days; day++)
        {
            yield return new DateTime(year, month, day);
        }
    }
    private void Load_Year()
    {
        string i = Convert.ToString(Convert.ToInt16((DateTime.Now.Year)));
        int date1 = Convert.ToInt32(i);
        for (int CurrentYear = date1; CurrentYear > date1 - 3; CurrentYear--)
        {
            //date = Convert.ToString(Convert.ToInt16((DateTime.Now.Year)) + i);
            ddlyear.Items.Add(CurrentYear.ToString());

        }
    }
    private void MonthList()
    {
        var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
        for (int i = 0; i < 12; i++)
        {
            ddlMonth.Items.Add(new System.Web.UI.WebControls.ListItem(months[i], i.ToString()));
        }

    }

    private void Load_MonthValues()
    {
        if (ddlMonth.SelectedItem.ToString() == "January")
            months = DateTime.ParseExact(ddlMonth.SelectedItem.ToString(), "MMMM", CultureInfo.InvariantCulture).Month;
        else if (ddlMonth.SelectedItem.ToString() == "February")
            months = DateTime.ParseExact(ddlMonth.SelectedItem.ToString(), "MMMM", CultureInfo.InvariantCulture).Month;
        else if (ddlMonth.SelectedItem.ToString() == "March")
            months = DateTime.ParseExact(ddlMonth.SelectedItem.ToString(), "MMMM", CultureInfo.InvariantCulture).Month;
        else if (ddlMonth.SelectedItem.ToString() == "April")
            months = DateTime.ParseExact(ddlMonth.SelectedItem.ToString(), "MMMM", CultureInfo.InvariantCulture).Month;
        else if (ddlMonth.SelectedItem.ToString() == "May")
            months = DateTime.ParseExact(ddlMonth.SelectedItem.ToString(), "MMMM", CultureInfo.InvariantCulture).Month;
        else if (ddlMonth.SelectedItem.ToString() == "June")
            months = DateTime.ParseExact(ddlMonth.SelectedItem.ToString(), "MMMM", CultureInfo.InvariantCulture).Month;
        else if (ddlMonth.SelectedItem.ToString() == "July")
            months = DateTime.ParseExact(ddlMonth.SelectedItem.ToString(), "MMMM", CultureInfo.InvariantCulture).Month;
        else if (ddlMonth.SelectedItem.ToString() == "August")
            months = DateTime.ParseExact(ddlMonth.SelectedItem.ToString(), "MMMM", CultureInfo.InvariantCulture).Month;
        else if (ddlMonth.SelectedItem.ToString() == "September")
            months = DateTime.ParseExact(ddlMonth.SelectedItem.ToString(), "MMMM", CultureInfo.InvariantCulture).Month;
        else if (ddlMonth.SelectedItem.ToString() == "October")
            months = DateTime.ParseExact(ddlMonth.SelectedItem.ToString(), "MMMM", CultureInfo.InvariantCulture).Month;
        else if (ddlMonth.SelectedItem.ToString() == "November")
            months = DateTime.ParseExact(ddlMonth.SelectedItem.ToString(), "MMMM", CultureInfo.InvariantCulture).Month;
        else
            months = DateTime.ParseExact(ddlMonth.SelectedItem.ToString(), "MMMM", CultureInfo.InvariantCulture).Month;
    }
    private ReportDatestNew LoadData()
    {
        // if (!chkPartyCode.Checked)
        //  dsData = VHMS.DataAccess.VHMSReports.MarginReport(Convert.ToInt32(ddlSupplier.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlSubcategory.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(ddlCustomer.SelectedValue), txtDOB.Text, txtDOR.Text, ddlType.SelectedValue);
        //else
        dsData = VHMS.DataAccess.VHMSReports.PrintAttendance(Convert.ToInt32(ddlCustomer.SelectedValue), Query, ddlyear.Text);
        ReportDatestNew dsReportData = new ReportDatestNew();

        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "tAttendanceLogFinal";
            foreach (DataRow drow in dsData.Tables[0].Rows) dsReportData.tAttendanceLogFinal.ImportRow(drow);
        }
        return dsReportData;
    }


    private ReportDatestNew LoadAttendance(string date, int ID)
    {
        // if (!chkPartyCode.Checked)
        //  dsData = VHMS.DataAccess.VHMSReports.MarginReport(Convert.ToInt32(ddlSupplier.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlSubcategory.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(ddlCustomer.SelectedValue), txtDOB.Text, txtDOR.Text, ddlType.SelectedValue);
        //else
        dsData = VHMS.DataAccess.VHMSReports.PrintAttendanceBYID(ID, date);
        ReportDatestNew dsReportData = new ReportDatestNew();

        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "V_AttendanceLogFinal";
            foreach (DataRow drow in dsData.Tables[0].Rows) dsReportData.V_AttendanceLogFinal.ImportRow(drow);
        }
        return dsReportData;
    }
    public void LoadCustomer()
    {
        Collection<VHMS.Entity.Employee> ObjList = new Collection<VHMS.Entity.Employee>();
        ObjList = VHMS.DataAccess.Employee.GetEmployee(0);

        ddlCustomer.DataSource = ObjList;
        ddlCustomer.DataTextField = "EmployeeName";
        ddlCustomer.DataValueField = "EmployeeID";
        ddlCustomer.DataBind();
        ddlCustomer.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
    }
    protected void btnExportReport_Click(object sender, EventArgs e)
    {
        try
        {
            ExportReport();
        }
        catch (Exception ex)
        { Log.Write("RepViewDischargeSummary btnExportReport_Click | " + ex.ToString()); }
    }
    private void ExportReport()
    {
        LoadReport();
        ExportOptions exopt = default(ExportOptions);
        DiskFileDestinationOptions dfdopt = new DiskFileDestinationOptions();
        string fname = "AttendanceReport.pdf";
        dfdopt.DiskFileName = Server.MapPath(fname);

        exopt = rprt.ExportOptions;
        exopt.ExportDestinationType = ExportDestinationType.DiskFile;

        //for PDF select PortableDocFormat for excel select ExportFormatType.Excel    
        exopt.ExportFormatType = ExportFormatType.PortableDocFormat;
        exopt.DestinationOptions = dfdopt;

        //finally export your report document    
        rprt.Export();

        //To open your PDF after save it from crystal report    

        string Path = Server.MapPath(fname);
        FileInfo file = new FileInfo(Path);
        Response.ClearContent();

        Response.AddHeader("Content-Disposition", "inline; filename=" + file.Name);
        Response.AddHeader("Content-Length", file.Length.ToString());
        Response.ContentType = "application/pdf";
        Response.TransmitFile(file.FullName);
        Response.Flush();
        //Response.Close();
        //Response.Redirect("frmSalesPendingReport.aspx");
        //Response.Headers.Clear();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {

       // LoadReport();
       // string FileName = DateTime.Now.ToShortDateString().ToString().Replace("/", "_");
       // string PdfFilePath = "D:" ;
       // FileName += "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString();
       // rprt.ExportToDisk(ExportFormatType.Excel, PdfFilePath + "\\AttendanceLog" + FileName + ".xls");
       ////ScriptManager.RegisterClientScriptBlock(this.GetType(), "myalert", "alert('"+PdfFilePath + "\\AttendanceLog" + FileName + ".xls');", true);
       // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);

    }
}


