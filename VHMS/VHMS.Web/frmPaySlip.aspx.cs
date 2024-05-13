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

public partial class frmPaySlip : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    int months = 0;
    string Query = "Active=1";
    ReportDocument rprt = new ReportDocument();
    decimal InvoiceAmount = 0, NetAmount = 0, DiscountAmount = 0, TotalQty = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnExportReport);
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
         LoadReport();
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
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        try
        {
            ReportDatestNew dsReportData = new ReportDatestNew();
            dsReportData = LoadData();
            rprt = new ReportDocument();
            rprt.Load(Server.MapPath("~/Reports/rptSalary.rpt"));
            rprt.SetDataSource(dsReportData);
            this.PaySlip.ReportSource = rprt;
            PaySlip.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            PaySlip.Zoom(100);
            this.PaySlip.DataBind();
        }
        catch (Exception ex)
        {
            sException = "frmPrintSales | " + ex.ToString();
            Log.Write(sException);
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
        string monthYear = ddlMonth.SelectedItem.ToString() + "-" + ddlyear.Text;

        dsData = VHMS.DataAccess.VHMSReports.PrintPaysilp(Convert.ToInt32(ddlCustomer.SelectedValue), monthYear);
        ReportDatestNew dsReportData = new ReportDatestNew();

        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "tSalary";
            foreach (DataRow drow in dsData.Tables[0].Rows) dsReportData.tSalary.ImportRow(drow);

            dsData.Tables[1].TableName = "tCompany";
            foreach (DataRow drow in dsData.Tables[1].Rows) dsReportData.tCompany.ImportRow(drow);
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
        ddlCustomer.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
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
        string fname = "PaySlip.pdf";
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
}

