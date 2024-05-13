﻿using System;
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
public partial class frmUnPaidReports : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            //LoadCatagory();
            //LoadBranch();
            //LoadProduct();
            // LoadCustomer();
            string empid = Request.QueryString["id"];
            LoadChitName();
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
            //LoadEmployeeCode();
            //txtDOB.Text = Convert.ToDateTime(DateTime.Now.AddDays(-7)).ToString("dd/MM/yyyy");
            // txtDOR.Text = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy");
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

            Session["ReportName"] = "Renewal";
            Session["ReportMobileNo"] = txtMobile.Text;
            Session["ReportSchemeID"] = ddlScheme.SelectedValue;
            Session["ReportBranchID"] = ddlBranch.SelectedValue;
            Session["ReportEmployeeID"] = ddlEmployeeCode.SelectedValue;
            Session["ReportRegionID"] = RegionID;
            Session["ReportZoneID"] = ZoneID;
            Response.Redirect("frmPrintUnpaid.aspx?mobileview=yes&id=10");

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
        dsData = VHMS.DataAccess.VHMSReports.PrintUnPaid(txtMobile.Text, ddlScheme.SelectedValue, ddlBranch.SelectedValue, ddlEmployeeCode.SelectedValue, RegionID, ZoneID);
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
    private void cal()
    {
       
    }
    
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=Unpaid.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gvUnPaid.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
        gvUnPaid.AllowPaging = true;
        gvUnPaid.DataBind();
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AppendHeader("content-disposition", "attachment; filename=Unpaid.xls");
        Response.ContentType = "application/excel";

        StringWriter stringwriter = new StringWriter();
        HtmlTextWriter htmltextwriter = new HtmlTextWriter(stringwriter);

        gvUnPaid.HeaderRow.Style.Add("background-color", "#FFFFFF");

        foreach (TableCell tableCell in gvUnPaid.HeaderRow.Cells)
        {
            tableCell.Style["background-color"] = "#d8d4d4";
        }

        foreach (GridViewRow gridViewRow in gvUnPaid.Rows)
        {
            gridViewRow.BackColor = System.Drawing.Color.White;
            foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
            {
                gridViewRowTableCell.Style["background-color"] = "#FFFFFF";
            }
        }
        gvUnPaid.RenderControl(htmltextwriter);
        Response.Write(stringwriter.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //
    }
    public void LoadChitName()
    {
        Collection<VHMS.Entity.Chit> ObjList = new Collection<VHMS.Entity.Chit>();
        ObjList = VHMS.DataAccess.Chit.GetChit();

        ddlScheme.DataSource = ObjList;
        ddlScheme.DataTextField = "ChitName";
        ddlScheme.DataValueField = "ChitID";
        ddlScheme.DataBind();
        ddlScheme.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    public void LoadCustomer()
    {
        //Collection<VHMS.Entity.Customer> ObjList = new Collection<VHMS.Entity.Customer>();
        //ObjList = VHMS.DataAccess.Customer.GetCustomer();

        //ddlCustomerName.DataSource = ObjList;
        //ddlCustomerName.DataTextField = "CustomerName";
        //ddlCustomerName.DataValueField = "CustomerID";
        //ddlCustomerName.DataBind();
        //ddlCustomerName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    public void LoadBranch()
    {
        int RegionID = 0;
        int ZoneID = 0;
        int RoleID = Convert.ToInt32(HttpContext.Current.Session["RoleID"]);
        if (RoleID == 7)
            RegionID = Convert.ToInt32(HttpContext.Current.Session["RegionID"]);
        else if (RoleID == 9)
            ZoneID = Convert.ToInt32(HttpContext.Current.Session["ZoneID"]);
        Collection<VHMS.Entity.Branch> ObjList = new Collection<VHMS.Entity.Branch>();
        ObjList = VHMS.DataAccess.Branch.GetBranch(RegionID, ZoneID);

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
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            InstallmentAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "InstallmentAmount"));
            Balance += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Balance"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].ColumnSpan =6;
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
            e.Row.Cells[0].Font.Size = 14;
            e.Row.Cells[3].Font.Size = 14;
            e.Row.Cells[5].Font.Size = 14;
        }
            }
    protected void OnDataBound(object sender, EventArgs e)
    {
        //InstallmentAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "InstallmentAmount"));
        lblTotal.Text = "Total Amount: " + Convert.ToDecimal(InstallmentAmount);
    }
}