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
public partial class frmAuditorBillReports : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
  
    int iRegionID = 0;
    int iZoneID = 0;
    string empid = "0";
    int iRoleID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            string empid = Request.QueryString["id"];

            LoadBranch();
            GetPreviousBillNo();
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
                }
            }

            txtDOB.Text = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy");
            txtDOR.Text = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy");
        }
    }
    protected void ddlPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetPreviousBillNo();
    }

    protected void SelectChkBox_CheckChanged(object sender, EventArgs e)
    {
        decimal TotalAmt = 0;
        foreach (GridViewRow row in gvRenewal.Rows)
        {
            if ((row.FindControl("SelectChkBox") as CheckBox).Checked)
            {
                //row.Cells[1].Text = GenerateInvoiceNo(row.Cells[8].Text);
                TotalAmt += Convert.ToDecimal(row.Cells[6].Text);
            }
        }
        lblTotal.Text = " Total Amount : " + TotalAmt.ToString("0.00");
    }
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
            Session["ReportFromDate"] = txtDOB.Text;
            Session["ReportToDate"] = txtDOR.Text;
            Session["ReportBranchID"] = ddlBranch.SelectedValue;
            Session["ReportRegionID"] = RegionID;
            Session["ReportZoneID"] = ZoneID;
            Response.Redirect("frmPrintPage.aspx?mobileview=yes&id=10");
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

        dsData = VHMS.DataAccess.VHMSReports.PrintSales(txtDOB.Text, txtDOR.Text, ddlBranch.SelectedValue);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tSales";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tSales.ImportRow(drow);

                gvRenewal.DataSource = dsData.Tables[0];
                gvRenewal.DataBind();
            }
        }
        catch (Exception ex)
        {
            sException = "frmPrintClosed | " + ex.ToString();
            Log.Write(sException);
        }
    }
    public void LoadBranchService()
    {

        //int RoleID = Convert.ToInt32(HttpContext.Current.Session["RoleID"]);
        //if (RoleID == 7)
        //    iRegionID = Convert.ToInt32(HttpContext.Current.Session["RegionID"]);
        //else if (RoleID == 9)
        //    iZoneID = Convert.ToInt32(HttpContext.Current.Session["ZoneID"]);
        Collection<VHMS.Entity.Branch> ObjList = new Collection<VHMS.Entity.Branch>();
        ObjList = VHMS.DataAccess.Branch.GetMainBranch();

        ddlBranch.DataSource = ObjList;
        ddlBranch.DataTextField = "BranchName";
        ddlBranch.DataValueField = "BranchID";
        ddlBranch.DataBind();
    }

    private void GetPreviousBillNo()
    {
        VHMS.Entity.Billing.Sales ObjList = new VHMS.Entity.Billing.Sales();
        ObjList = VHMS.DataAccess.Billing.Sales.GetLastBillNo(Convert.ToInt32(ddlBranch.SelectedValue));
        txtLastBillNo.Text = ObjList.AuditBillNo;
        OriginalBillNo.Text = "Invoice No. : " + ObjList.InvoiceNo;
        OriginalBillDate.Text = "Invoice No. : " + ObjList.sInvoiceDate;
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        if (txtLastBillNo.Text.Length <= 0)
        {
            string display = "Last Bill Number should not be empty";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
            return;
        }
        int Count = 0;
        int cnt = 1;
        foreach (GridViewRow row in gvRenewal.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                //Hide the Row if CheckBox is not checked
                row.Visible = (row.FindControl("SelectChkBox") as CheckBox).Checked;
                if (row.Visible == true)
                {
                    row.Cells[0].Text = cnt++.ToString();
                    row.Cells[1].Text = GenerateInvoiceNo(row.Cells[8].Text);
                    Count = 1;
                }
            }
        }
        if (Count == 1)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=AuditorBill.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvRenewal.Columns[7].Visible = false;
            gvRenewal.Columns[8].Visible = false;

            gvRenewal.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
            gvRenewal.AllowPaging = true;
            gvRenewal.DataBind();
        }
        else
        {
            string display = "Please select CheckBox";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
            LoadReport();
        }
    }

    protected void btnSelectAll_Click(object sender, EventArgs e)
    {

        foreach (GridViewRow row in gvRenewal.Rows)
        {
            CheckBox chkcheck = (CheckBox)row.FindControl("SelectChkBox");
            chkcheck.Checked = true;
        }
    }

    protected void btnUnSelect_Click(object sender, EventArgs e)
    {

        foreach (GridViewRow row in gvRenewal.Rows)
        {
            CheckBox chkcheck = (CheckBox)row.FindControl("SelectChkBox");
            chkcheck.Checked = false;
        }
    }

    private string GenerateInvoiceNo(string SalesID)
    {
        VHMS.Entity.Billing.Sales ObjList = new VHMS.Entity.Billing.Sales();
        ObjList = VHMS.DataAccess.Billing.Sales.GetAuditBillNo(txtLastBillNo.Text, Convert.ToInt32(SalesID));
        txtLastBillNo.Text = ObjList.AuditBillNo;
        OriginalBillNo.Text = "Invoice No. : " + ObjList.InvoiceNo;
        OriginalBillDate.Text = "Invoice No. : " + ObjList.sInvoiceDate;
        return ObjList.AuditBillNo;
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (txtLastBillNo.Text.Length <= 0)
        {
            string display = "Last Bill Number should not be empty";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
            return;
        }
        int Counts = 0, cnt = 1;

        foreach (GridViewRow row in gvRenewal.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                //Hide the Row if CheckBox is not checked
                row.Visible = (row.FindControl("SelectChkBox") as CheckBox).Checked;
                if (row.Visible == true)
                {
                    row.Cells[0].Text = cnt++.ToString();
                    row.Cells[1].Text = GenerateInvoiceNo(row.Cells[8].Text);
                    Counts = 1;
                }

            }
        }

        foreach (GridViewRow row in gvRenewal.Rows)
        {
            // int rowss = gvRenewal.Rows.Count;
        }
        if (Counts == 1)
        {
            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment; filename=AuditorBill.xls");
            Response.ContentType = "application/excel";

            StringWriter stringwriter = new StringWriter();
            HtmlTextWriter htmltextwriter = new HtmlTextWriter(stringwriter);
            gvRenewal.Columns[7].Visible = false;
            gvRenewal.Columns[8].Visible = false;

            gvRenewal.HeaderRow.Style.Add("background-color", "#FFFFFF");

            foreach (TableCell tableCell in gvRenewal.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#d8d4d4";
            }

            foreach (GridViewRow gridViewRow in gvRenewal.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";
                }
            }
            gvRenewal.RenderControl(htmltextwriter);
            Response.Write(stringwriter.ToString());
            Response.End();
        }
        else
        {
            string display = "Please select CheckBox";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
            LoadReport();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //
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
        ObjList = VHMS.DataAccess.Branch.GetMainBranch();

        ddlBranch.DataSource = ObjList;
        ddlBranch.DataTextField = "BranchName";
        ddlBranch.DataValueField = "BranchID";
        ddlBranch.DataBind();
        //ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        //string display = "Please select  Employee Code";

        // if (ddlEmployeeCode.SelectedIndex >=0)
        LoadReport();
        //else
        //  ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);

    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        //InstallmentAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "InstallmentAmount"));
        // lblTotal.Text = "Total Amount: " + Convert.ToDecimal(AmountPaid);
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow Row = e.Row;
            CheckBox SelectChkBox = (CheckBox)Row.FindControl("SelectChkBox");
            Label IsGSTBill = (Label)Row.FindControl("IsGSTBill");
            if (Convert.ToBoolean(IsGSTBill.Text) == true)
                SelectChkBox.Checked = true;
        }
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    e.Row.Cells[0].ColumnSpan = 5;
        //    e.Row.Cells.RemoveAt(1);
        //    e.Row.Cells.RemoveAt(2);
        //    e.Row.Cells.RemoveAt(3);
        //    e.Row.Cells.RemoveAt(4);
        //   // e.Row.Cells.RemoveAt(5);

        //    e.Row.Cells[3].Text = "Total :";
        //    e.Row.Cells[5].Text = Convert.ToDecimal(AmountPaid).ToString();
        //    e.Row.Cells[3].Font.Bold = true;
        //    e.Row.Cells[5].Font.Bold = true;
        //    e.Row.Cells[3].Font.Size = 14;
        //    e.Row.Cells[5].Font.Size = 14;
        //}
        //gvRenewal.Columns[4].HeaderText = "Gst Tax" + CGSTPercent;
        //gvRenewal.Columns[5].HeaderText = "Gst Tax" + SGSTPercent;
    }
}