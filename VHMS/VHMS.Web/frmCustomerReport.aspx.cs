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
using System.Drawing.Printing;
using System.Diagnostics;
using CrystalDecisions.CrystalReports.Engine;

public partial class frmCustomerReport : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    //private PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
        }

    }
    protected void ddlProduct1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadReport();
    }
    //public void LoadCustomer()
    //{
    //    Collection<VHMS.Entity.Customer> ObjList = new Collection<VHMS.Entity.Customer>();
    //    ObjList = VHMS.DataAccess.Customer.GetCustomer();

    //    ddlCustomer.DataSource = ObjList;
    //    ddlCustomer.DataTextField = "CustomerName";
    //    ddlCustomer.DataValueField = "CustomerID";
    //    ddlCustomer.DataBind();
    //    ddlCustomer.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    //}
   
    private void LoadReport()
    {

        ReportDataSet dsReportData = new ReportDataSet();

        dsData = VHMS.DataAccess.VHMSReports.PrintCustomer(0, ddlCustomer.SelectedValue);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tCustomer";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tCustomer.ImportRow(drow);

                gvProductMas.DataSource = dsData.Tables[0];
                gvProductMas.DataBind();
            }

        }
        catch (Exception ex)
        {
            sException = "frmPrintCustomer | " + ex.ToString();
            Log.Write(sException);
        }
    }

    protected void btnPDf_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=Customer.pdf");
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
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadReport();
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AppendHeader("content-disposition", "attachment; filename=Customer.xls");
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
    protected void btnprint_click(object sender, EventArgs e)
    {
       
        //printPreviewDialog1.Document = printDocument1;
        //printPreviewDialog1.ShowDialog();


    }

}