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
using CrystalDecisions.CrystalReports.Engine;
using System.Globalization;

public partial class frmDSupplierLedgerSummary : System.Web.UI.Page
{
    ReportDocument rprt = new ReportDocument();
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            LoadSupplier();
            if (DateTime.Now.Month > 3)
                txtDOB.Text = new DateTime(DateTime.Now.Year, 4, 1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            else
                txtDOB.Text = new DateTime(DateTime.Now.Year - 1, 4, 1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            txtDOR.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        LoadReport();
    }

    public void LoadSupplier()
    {
        Collection<VHMS.Entity.Billing.Supplier> ObjList = new Collection<VHMS.Entity.Billing.Supplier>();
        ObjList = VHMS.DataAccess.Billing.Supplier.GetActiveSupplier();

        ddlSupplier.DataSource = ObjList;
        ddlSupplier.DataTextField = "SupplierName";
        ddlSupplier.DataValueField = "SupplierID";
        ddlSupplier.DataBind();
        ddlSupplier.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
    }


    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadReport();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string sTemp = string.Empty;
        try
        {
            ReportDataSet dsReportData = new ReportDataSet();

            dsData = VHMS.DataAccess.VHMSReports.PrintPurchaseLedgerReport(txtDOB.Text, txtDOR.Text, ddlSupplier.SelectedValue, 0);

            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tStatement";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tStatement.ImportRow(drow);

                dsData.Tables[1].TableName = "tSupplier";
                foreach (DataRow drow in dsData.Tables[1].Rows)
                    dsReportData.tSupplier.ImportRow(drow);

                dsData.Tables[2].TableName = "tCompany";
                foreach (DataRow drow in dsData.Tables[2].Rows)
                    dsReportData.tCompany.ImportRow(drow);
            }
            //  dsReportData = LoadData();
            rprt = new ReportDocument();
            rprt.Load(Server.MapPath("~/Reports/Rpt_PurchaseStatementSummary.rpt"));
            rprt.SetDataSource(dsReportData);
            this.CRDischargeSummaryReport.ReportSource = rprt;
            CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            CRDischargeSummaryReport.Zoom(100);
            this.CRDischargeSummaryReport.DataBind();
            rprt.ExportToHttpResponse
            (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "Supplier Ledger");
        }
        catch (Exception ex)
        {
            sException = "ReportPage_RepDischargeSummary | " + ex.ToString();
            Log.Write(sException);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    private void LoadReport()
    {
        ReportDataSet dsReportData = new ReportDataSet();

        dsData = VHMS.DataAccess.VHMSReports.PrintPurchaseLedgerReport(txtDOB.Text, txtDOR.Text, ddlSupplier.SelectedValue, 0);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tStatement";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tStatement.ImportRow(drow);

                dsData.Tables[1].TableName = "tSupplier";
                foreach (DataRow drow in dsData.Tables[1].Rows)
                    dsReportData.tSupplier.ImportRow(drow);

                dsData.Tables[2].TableName = "tCompany";
                foreach (DataRow drow in dsData.Tables[2].Rows)
                    dsReportData.tCompany.ImportRow(drow);
            }

            btnprint.Visible = false;
            rprt = new ReportDocument();
            rprt.Load(Server.MapPath("~/Reports/Rpt_PurchaseStatementSummary.rpt"));
            rprt.SetDataSource(dsReportData);
            this.CRDischargeSummaryReport.ReportSource = rprt;
            CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            CRDischargeSummaryReport.Zoom(100);
            this.CRDischargeSummaryReport.DataBind();
        }

        catch (Exception ex)
        {
            sException = "frmPrintJobCard | " + ex.ToString();
            Log.Write(sException);
        }
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rprt.Close();
        rprt.Dispose();
    }
}
