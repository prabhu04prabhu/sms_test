using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

public partial class frmPatientDetailsReport : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
        }
        LoadReport();
    }

    protected void gvProductMas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProductMas.PageIndex = e.NewPageIndex;
        LoadReport();
    }

    protected void ddlPayment_SelectedIndexChanged(object sender, EventArgs e)
    { LoadReport(); }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AppendHeader("content-disposition","attachment; filename=PatientDetails.xls");
        Response.ContentType = "application/excel";

        StringWriter stringwriter = new StringWriter();
        HtmlTextWriter htmltextwriter = new HtmlTextWriter(stringwriter);

        gvProductMas.HeaderRow.Style.Add("background-color", "#FFFFFF");

        foreach (TableCell tableCell in gvProductMas.HeaderRow.Cells)
        {
            tableCell.Style["background-color"] = "#d8d4d4";
        }

        foreach (GridViewRow gridViewRow in gvProductMas.Rows)
        {
            gridViewRow.BackColor = System.Drawing.Color.White;
            foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
            {
                gridViewRowTableCell.Style["background-color"] = "#FFFFFF";
            }
        }
        gvProductMas.RenderControl(htmltextwriter);
        Response.Write(stringwriter.ToString());
        Response.End();
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=PatientDetails.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gvProductMas.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
        gvProductMas.AllowPaging = true;
        gvProductMas.DataBind();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    private void LoadReport()
    {
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        ReportDataSet dsReportData = new ReportDataSet();

        dsData = VHMS.DataAccess.VHMSReports.PrintPatientDetails(0, txtDOB.Text, txtDOR.Text, ddlCategory.SelectedItem.Text, ddlHRefer.SelectedItem.Text);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tPatient";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tPatient.ImportRow(drow);

                gvProductMas.DataSource = dsData.Tables[0];
                gvProductMas.DataBind();
            }

        }
        catch (Exception ex)
        {
            sException = "frmPrintPatient | " + ex.ToString();
            Log.Write(sException);
        }
    }
}