using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmPrintUnpaid : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    decimal InstallmentAmount = 0, Balance = 0;
    int iRegionID = 0;
    int iZoneID = 0;
    string empid = "0";
    int iRoleID = 0;
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
        //txtDOB = Session["ReportFromDate"].ToString();
        //txtDOR = Session["ReportToDate"].ToString();
        txtMobile = Session["ReportMobileNo"].ToString();
        ddlScheme = Session["ReportSchemeID"].ToString();
        ddlBranch = Session["ReportBranchID"].ToString();
        ddlEmployeeCode = Session["ReportEmployeeID"].ToString();
        RegionID = Convert.ToInt32(Session["ReportRegionID"]);
        ZoneID = Convert.ToInt32(Session["ReportZoneID"]);

        dsData = VHMS.DataAccess.VHMSReports.PrintUnPaid(txtMobile, ddlScheme, ddlBranch, ddlEmployeeCode, RegionID, ZoneID);
        try
        {

            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tRegister";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tRegister.ImportRow(drow);

                gvUnPaid.DataSource = dsData.Tables[0];
                gvUnPaid.DataBind();
            }

        }
        catch (Exception ex)
        {
            sException = "frmPrintClosed | " + ex.ToString();
            Log.Write(sException);
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            InstallmentAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "InstallmentAmount"));
            Balance += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Balance"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].ColumnSpan = 6;
            e.Row.Cells.RemoveAt(1);
            e.Row.Cells.RemoveAt(2);
            e.Row.Cells.RemoveAt(3);
            e.Row.Cells.RemoveAt(4);
            e.Row.Cells.RemoveAt(5);
            //e.Row.Cells.RemoveAt(6);

            e.Row.Cells[0].Text = "Total Amount :";
            e.Row.Cells[3].Text = Convert.ToDecimal(InstallmentAmount).ToString();
            e.Row.Cells[5].Text = Convert.ToDecimal(Balance).ToString();
            e.Row.Cells[0].Font.Bold = true;
            e.Row.Cells[3].Font.Bold = true;
            e.Row.Cells[5].Font.Bold = true;
            e.Row.Cells[0].Font.Size = 11;
            e.Row.Cells[3].Font.Size = 11;
            e.Row.Cells[5].Font.Size = 11;
        }
    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        //InstallmentAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "InstallmentAmount"));
        lblTotal.Text = "Total Amount : " + Convert.ToDecimal(InstallmentAmount);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //
    }
}