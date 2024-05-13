﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmPrintRegister : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    decimal InstallmentAmount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadReport();
    }
    private void LoadReport()
    {
        int RegionID = 0;
        int ZoneID = 0;
        ReportDataSet dsReportData = new ReportDataSet();
        string txtDOB, txtDOR, txtMobile, ddlScheme, ddlBranch, ddlEmployeeCode, ReportName;

        ReportName = Session["ReportName"].ToString();
        txtDOB = Session["ReportFromDate"].ToString();
        txtDOR = Session["ReportToDate"].ToString();
        txtMobile = Session["ReportMobileNo"].ToString();
        ddlScheme = Session["ReportSchemeID"].ToString();
        ddlBranch = Session["ReportBranchID"].ToString();
        ddlEmployeeCode = Session["ReportEmployeeID"].ToString();
        RegionID = Convert.ToInt32(Session["ReportRegionID"]);
        ZoneID = Convert.ToInt32(Session["ReportZoneID"]);

        dsData = VHMS.DataAccess.VHMSReports.PrintRegister(txtDOB, txtDOR, txtMobile, ddlScheme, ddlBranch, ddlEmployeeCode, RegionID, ZoneID);
        try
        {

            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tRegister";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tRegister.ImportRow(drow);

                gvRegister.DataSource = dsData.Tables[0];
                gvRegister.DataBind();
            }

        }
        catch (Exception ex)
        {
            sException = "frmPrintClosed | " + ex.ToString();
            Log.Write(sException);
        }
    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        //InstallmentAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "InstallmentAmount"));
        lblTotal.Text = "Total Amount: " + Convert.ToDecimal(InstallmentAmount);
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            InstallmentAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "InstallmentAmount"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].ColumnSpan = 5;
            e.Row.Cells.RemoveAt(1);
            e.Row.Cells.RemoveAt(2);
            e.Row.Cells.RemoveAt(3);
            e.Row.Cells.RemoveAt(4);

            e.Row.Cells[3].Text = "Total Amount :";
            e.Row.Cells[4].Text = Convert.ToDecimal(InstallmentAmount).ToString();
            e.Row.Cells[3].Font.Bold = true;
            e.Row.Cells[4].Font.Bold = true;
            e.Row.Cells[3].Font.Size = 14;
            e.Row.Cells[4].Font.Size = 14;
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //
    }
}