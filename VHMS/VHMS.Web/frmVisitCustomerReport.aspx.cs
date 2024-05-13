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

public partial class frmVisitCustomerReport : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    decimal InstallmentAmount = 0;
    int iRegionID = 0;
    int iZoneID = 0;
    string empid = "0";
    int iRoleID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            empid = Request.QueryString["id"];
            LoadBranch();

            if (empid != null && Convert.ToInt32(empid) > 0)
            {
                VHMS.Entity.User ObjList = new VHMS.Entity.User();
                ObjList = VHMS.DataAccess.User.GetUserByID(Convert.ToInt32(empid));
                iRoleID = ObjList.RoleID;
                if (iRoleID == 7)
                {
                    iRegionID = ObjList.Region.RegionID;
                    LoadBranchService();
                }
                else if (iRoleID == 9)
                {
                    iZoneID = ObjList.Zone.ZoneID;
                    LoadBranchService();
                }
                else
                {
                    if (ObjList.Branch.BranchID > 0)
                        ddlBranch.SelectedValue = ObjList.Branch.BranchID.ToString();
                    ddlBranch.Enabled = false;
                    LoadEmployeeCode();
                }
            }
            //txtDOB.Text = Convert.ToDateTime(DateTime.Now.AddDays(-7)).ToString("dd/MM/yyyy");
            txtDOB.Text = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy");
            txtDOR.Text = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy");
        }
    }
    protected void ddlPayment_SelectedIndexChanged(object sender, EventArgs e)
    { LoadEmployeeCode(); }
    private void LoadReport()
    {
        int RegionID = 0;
        int ZoneID = 0;
        ReportDataSet dsReportData = new ReportDataSet();
        empid = Request.QueryString["id"];
        if (empid != null && Convert.ToInt32(empid) > 0)
        {
            VHMS.Entity.User ObjList = new VHMS.Entity.User();
            ObjList = VHMS.DataAccess.User.GetUserByID(Convert.ToInt32(empid));
            iRoleID = ObjList.RoleID;
            if (iRoleID == 7)
                RegionID = ObjList.Region.RegionID;
            else if (iRoleID == 9)
                ZoneID = ObjList.Zone.ZoneID;

            Session["ReportName"] = "";
            Session["ReportFromDate"] = "";
            Session["ReportToDate"] = "";
            Session["ReportMobileNo"] = "";
            Session["ReportSchemeID"] = "";
            Session["ReportBranchID"] = "";
            Session["ReportEmployeeID"] = "";
            Session["ReportRegionID"] = "";
            Session["ReportZoneID"] = "";

            Session["ReportName"] = "Register";
            Session["ReportFromDate"] = txtDOB.Text;
            Session["ReportToDate"] = txtDOR.Text;
            Session["ReportBranchID"] = ddlBranch.SelectedValue;
            Session["ReportEmployeeID"] = ddlEmployeeCode.SelectedValue;
            Session["ReportRegionID"] = RegionID;
            Session["ReportZoneID"] = ZoneID;
            Response.Redirect("frmVisitCustomerReport.aspx?mobileview=yes&id=10");
        }
        else
        {
            if (Convert.ToInt32(ddlBranch.SelectedValue) == 0)
            {
                int RoleID = Convert.ToInt32(HttpContext.Current.Session["RoleID"]);
                if (RoleID == 7)
                    RegionID = Convert.ToInt32(HttpContext.Current.Session["RegionID"]);
                else if (RoleID == 9)
                    ZoneID = Convert.ToInt32(HttpContext.Current.Session["ZoneID"]);
            }
        }
        dsData = VHMS.DataAccess.VHMSReports.PrintVisitCustomer(txtDOB.Text, txtDOR.Text,  ddlBranch.SelectedValue, ddlEmployeeCode.SelectedValue, RegionID, ZoneID);
        try
        {

            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tCustomer";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tCustomer.ImportRow(drow);

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
    private void cal()
    {

    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=Register.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gvRegister.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
        gvRegister.AllowPaging = true;
        gvRegister.DataBind();
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AppendHeader("content-disposition", "attachment; filename=Register.xls");
        Response.ContentType = "application/excel";

        StringWriter stringwriter = new StringWriter();
        HtmlTextWriter htmltextwriter = new HtmlTextWriter(stringwriter);

        gvRegister.HeaderRow.Style.Add("background-color", "#FFFFFF");

        foreach (TableCell tableCell in gvRegister.HeaderRow.Cells)
        {
            tableCell.Style["background-color"] = "#d8d4d4";
        }

        foreach (GridViewRow gridViewRow in gvRegister.Rows)
        {
            gridViewRow.BackColor = System.Drawing.Color.White;
            foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
            {
                gridViewRowTableCell.Style["background-color"] = "#FFFFFF";
            }
        }
        gvRegister.RenderControl(htmltextwriter);
        Response.Write(stringwriter.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //
    }
    public void LoadBranch()
    {
        int RoleID = Convert.ToInt32(HttpContext.Current.Session["RoleID"]);
        if (RoleID == 7)
            iRegionID = Convert.ToInt32(HttpContext.Current.Session["RegionID"]);
        else if (RoleID == 9)
            iZoneID = Convert.ToInt32(HttpContext.Current.Session["ZoneID"]);
        Collection<VHMS.Entity.Branch> ObjList = new Collection<VHMS.Entity.Branch>();
        ObjList = VHMS.DataAccess.Branch.GetBranch(iRegionID, iZoneID);

        ddlBranch.DataSource = ObjList;
        ddlBranch.DataTextField = "BranchName";
        ddlBranch.DataValueField = "BranchID";
        ddlBranch.DataBind();
        ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    public void LoadBranchService()
    {

        Collection<VHMS.Entity.Branch> ObjList = new Collection<VHMS.Entity.Branch>();
        ObjList = VHMS.DataAccess.Branch.GetBranch(iRegionID, iZoneID);

        ddlBranch.DataSource = ObjList;
        ddlBranch.DataTextField = "BranchName";
        ddlBranch.DataValueField = "BranchID";
        ddlBranch.DataBind();
        ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    public void LoadEmployeeCode()
    {
        Collection<VHMS.Entity.User> ObjList = new Collection<VHMS.Entity.User>();
        ObjList = VHMS.DataAccess.User.GetBranchUser(Convert.ToInt32(ddlBranch.SelectedValue));

        ddlEmployeeCode.DataSource = ObjList;
        ddlEmployeeCode.DataTextField = "EmployeeCode";
        ddlEmployeeCode.DataValueField = "UserID";
        ddlEmployeeCode.DataBind();
        ddlEmployeeCode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadReport();
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
       // lblTotal.Text = "Total Amount: " + Convert.ToDecimal(InstallmentAmount);
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    InstallmentAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "InstallmentAmount"));
        //}
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    e.Row.Cells[0].ColumnSpan = 5;
        //    e.Row.Cells.RemoveAt(1);
        //    e.Row.Cells.RemoveAt(2);
        //    e.Row.Cells.RemoveAt(3);
        //    e.Row.Cells.RemoveAt(4);

        //    e.Row.Cells[3].Text = "Total Amount :";
        //    e.Row.Cells[4].Text = Convert.ToDecimal(InstallmentAmount).ToString();
        //    e.Row.Cells[3].Font.Bold = true;
        //    e.Row.Cells[4].Font.Bold = true;
        //    e.Row.Cells[3].Font.Size = 14;
        //    e.Row.Cells[4].Font.Size = 14;
        //}

    }
}