using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;


public partial class frmPatientDetails : System.Web.UI.Page
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

    private void LoadReport()
    {
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        ReportDataSet dsReportData = new ReportDataSet();
         
        dsData = VHMS.DataAccess.VHMSReports.PrintPatientDetails(0, "01/03/2019", "01/04/2019", "All", "All");
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