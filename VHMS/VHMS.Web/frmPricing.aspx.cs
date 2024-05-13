using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmPricing : BaseConfig
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) VHMSService.AddPageVisitLog();
    }
    //protected void btnExcel_Click(object sender, EventArgs e)
    //{
    //    DataTable dt = new DataTable();
    //    SqlConnection cnn;
    //    string sql = null;
    //    string strcon = ConfigurationManager.ConnectionStrings["SMSConnectionString"].ConnectionString;
    //    //connectionString = "data source=servername;initial catalog=databasename;user id=username;password=password;";
    //    cnn = new SqlConnection(strcon);
    //    cnn.Open();
    //    sql = "SELECT SMSCode,ProductName,ProductCode as PartyCode,PurchasePrice,MRP,WholeSaleMargin ,WholeSalePrice,RetailMargin,RetailPrice,WholeSalePriceA,WholeSalePriceB,WholeSalePriceC,RetailPriceA,RetailPriceB,RetailPriceC,Minimum_DiscountPercentage ,Maximum_DiscountPercentage FROM tPricing inner join tProduct on tProduct.PK_ProductID=tPricing.FK_ProductID";
    //    SqlDataAdapter dscmd = new SqlDataAdapter(sql, cnn);
    //    DataSet ds = new DataSet();
    //    dscmd.Fill(dt);
    //    //dt = dscmd;//your datatable
    //    string attachment = "attachment; filename=Pricing.xls";
    //    Response.ClearContent();
    //    Response.AddHeader("content-disposition", attachment);
    //    Response.ContentType = "application/vnd.ms-excel";
    //    string tab = "";
    //    foreach (DataColumn dc in dt.Columns)
    //    {
    //        Response.Write(tab + dc.ColumnName);
    //        tab = "\t";
    //    }

    //    Response.Write("\n");
    //    int i;
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        tab = "";
    //        for (i = 0; i < dt.Columns.Count; i++)
    //        {

    //            Response.Write(tab + dr[i].ToString());
    //            tab = "\t";
    //        }
    //        Response.Write("\n");
    //    }
    //    Response.End();
    //}
    //protected void btnExport_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        SqlConnection cnn;
    //        string sql = null;
    //        string strcon = ConfigurationManager.ConnectionStrings["SMSConnectionString"].ConnectionString;
    //        //connectionString = "data source=servername;initial catalog=databasename;user id=username;password=password;";
    //        cnn = new SqlConnection(strcon);
    //        cnn.Open();
    //        sql = "SELECT SMSCode,ProductCode as PartyCode,PurchasePrice,MRP,WholeSaleMargin as WSalesMargin ,WholeSalePrice as WSalesPrice,RetailMargin,RetailPrice,Minimum_DiscountPercentage as MinDiscPer,Maximum_DiscountPercentage as MaxDiscPer FROM tPricing inner join tProduct on tProduct.PK_ProductID=tPricing.FK_ProductID";
    //        SqlDataAdapter dscmd = new SqlDataAdapter(sql, cnn);
    //        DataSet ds = new DataSet();
    //        dscmd.Fill(dt);
    //        Response.ContentType = "application/pdf";
    //        Response.AddHeader("content-disposition", "attachment;filename=Pricing.pdf");
    //        //dt = dscmd;//your datatable
    //        string html = "<table border='1' >";
    //        //add header row
    //        html += "<tr>";
    //        for (int i = 0; i < dt.Columns.Count; i++)
    //            html += "<td Style='Font-Size:8px;'>" + dt.Columns[i].ColumnName + "</td>";
    //        html += "</tr>";
    //        //add rows
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            html += "<tr>";
    //            for (int j = 0; j < dt.Columns.Count; j++)
    //                html += "<td Style='Font-Size:7px;'>" + dt.Rows[i][j].ToString() + "</td>";
    //            html += "</tr>";
    //        }
    //        html += "</table>";
    //        StringReader sr = new StringReader(html.ToString());
    //        Document pdfDoc = new Document(PageSize.A4, 5f, 10f, 10f, 10f);




    //        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
    //        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
    //        pdfDoc.Open();
    //        htmlparser.Parse(sr);
    //        pdfDoc.Close();
    //        Response.Write(pdfDoc);
    //        Response.End();
    //    }
    //    catch (Exception ex)
    //    { ex.ToString(); }
    //}
    public override void VerifyRenderingInServerForm(Control control) { }

}
