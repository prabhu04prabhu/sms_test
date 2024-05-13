using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using System.Data;

public partial class RepDischargeSummary : System.Web.UI.Page
{
    ReportDocument rprt = new ReportDocument();
    DataSet dsData = new DataSet();
    string sException = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnSearchReport);

        if (!IsPostBack)
            VHMSService.AddPageVisitLog();

        LoadReport();
    }
    protected void btnSearchReport_Click(object sender, EventArgs e)
    {
        LoadReport();
    }    
    private void LoadReport()
    {
        string sTemp = string.Empty;
        int AdmissionID = 0;
        try
        {
            if (!string.IsNullOrEmpty(hdnAdmissionID.Value)) AdmissionID = Convert.ToInt32(hdnAdmissionID.Value);
            ReportDataSet dsReportData = new ReportDataSet();

            dsReportData = LoadData(AdmissionID);
            rprt = new ReportDocument();
            rprt.Load(Server.MapPath("~/Reports/rptDischargeSummary.rpt"));
            rprt.SetDataSource(dsReportData);
            this.CRDischargeSummaryReport.ReportSource = rprt;
            CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
            CRDischargeSummaryReport.Zoom(100);
            this.CRDischargeSummaryReport.DataBind();
        }
        catch (Exception ex)
        {
            sException = "ReportPage_RepDischargeSummary | " + ex.ToString();
            Log.Write(sException);
        }
    }
    private ReportDataSet LoadData(int AdmissionID)
    {
        dsData = VHMS.DataAccess.Discharge.DischargeReport.GetDischargeSummary(AdmissionID);
        ReportDataSet dsReportData = new ReportDataSet();

        if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].TableName = "tAdmission";
            foreach (DataRow drow in dsData.Tables[0].Rows) dsReportData.tAdmission.ImportRow(drow);
            if (dsReportData.tAdmission.Rows.Count > 0)
            {
                ReportDataSet.tAdmissionRow drtAdmissionRow = (ReportDataSet.tAdmissionRow)dsReportData.tAdmission.Rows[0];
                drtAdmissionRow.PrimaryConsultant = drtAdmissionRow.PrimaryConsultant.Replace(", ", Environment.NewLine);
                if (drtAdmissionRow.DateofAdmission.ToString("dd/MM/yyyy") == "01/01/1900") drtAdmissionRow.sDateofAdmission = "";             
            }

            dsData.Tables[1].TableName = "tDischargeEntry";
            foreach (DataRow drow in dsData.Tables[1].Rows) dsReportData.tDischargeEntry.ImportRow(drow);
            if (dsReportData.tAdmission.Rows.Count > 0)
            {
                ReportDataSet.tDischargeEntryRow drtDischargeEntryRow = (ReportDataSet.tDischargeEntryRow)dsReportData.tDischargeEntry.Rows[0];
                drtDischargeEntryRow.CoConsultant = drtDischargeEntryRow.CoConsultant.Replace(", ", Environment.NewLine);
                drtDischargeEntryRow.Registrar = drtDischargeEntryRow.Registrar.Replace(", ", Environment.NewLine);
                //Added on 05-09-2017
                drtDischargeEntryRow.WeekDays = drtDischargeEntryRow.WeekDays.Replace("1", "Monday");
                drtDischargeEntryRow.WeekDays = drtDischargeEntryRow.WeekDays.Replace("2", "Tuesday");
                drtDischargeEntryRow.WeekDays = drtDischargeEntryRow.WeekDays.Replace("3", "Wednesday");
                drtDischargeEntryRow.WeekDays = drtDischargeEntryRow.WeekDays.Replace("4", "Thursday");
                drtDischargeEntryRow.WeekDays = drtDischargeEntryRow.WeekDays.Replace("5", "Friday");
                drtDischargeEntryRow.WeekDays = drtDischargeEntryRow.WeekDays.Replace("6", "Saturday");
                drtDischargeEntryRow.WeekDays = drtDischargeEntryRow.WeekDays.Replace("7", "Sunday");
                if (drtDischargeEntryRow.DischargeDateTime.ToString("dd/MM/yyyy") == "01/01/1900") drtDischargeEntryRow.sDischargeDateTime = "";
                if (drtDischargeEntryRow.SurgeryDate.ToString("dd/MM/yyyy") == "01/01/1900") drtDischargeEntryRow.sSurgeryDate = "";

                //Added on 22-09-2017
                if (drtDischargeEntryRow.ReviewAppointmentDateTime.ToString("dd/MM/yyyy") != "01/01/1900")
                    drtDischargeEntryRow.sReviewAppointmentDateTime = "Review Appointment on " + drtDischargeEntryRow.ReviewAppointmentDateTime.ToString("dd/MM/yyyy hh:mm tt");
                else
                    drtDischargeEntryRow.sReviewAppointmentDateTime = "";
            }

            dsData.Tables[2].TableName = "tPrescription";
            foreach (DataRow drow in dsData.Tables[2].Rows) dsReportData.tPrescription.ImportRow(drow);

            //Added on 05-09-2017
            dsData.Tables[3].TableName = "tOtherSurgery";
            //Added on 22-09-2017
            string sSurgeryDate = string.Empty;
            int OSurgeryRowCount = 1;
            foreach (DataRow drow in dsData.Tables[3].Rows)
            {
                dsReportData.tOtherSurgery.ImportRow(drow);
                sSurgeryDate = sSurgeryDate + drow["sSurgeryDate"].ToString();
                if (OSurgeryRowCount != dsData.Tables[3].Rows.Count) sSurgeryDate += "," + Environment.NewLine + "\t\t\t  ";
                OSurgeryRowCount++;
            }
            if (!string.IsNullOrEmpty(sSurgeryDate))
            {
                //sSurgeryDate = sSurgeryDate.Substring(0, sSurgeryDate.Length - 1);
                ReportDataSet.tDischargeEntryRow drtDischargeEntryRow = (ReportDataSet.tDischargeEntryRow)dsReportData.tDischargeEntry.Rows[0];
                drtDischargeEntryRow.sSurgeryDate = sSurgeryDate;
            }

            dsData.Tables[4].TableName = "tHipReplacement";
            foreach (DataRow drow in dsData.Tables[4].Rows) dsReportData.tHipReplacement.ImportRow(drow);

            //dsReportData.EnforceConstraints = false;
            DataTable dtTempHipReplacement = new DataTable();
            dtTempHipReplacement = dsReportData.tHipReplacementData.Clone();

            //For Hip Replacement
            foreach (DataRow drow in dsReportData.tHipReplacement.Rows)
            {
                DataRow drRow = dtTempHipReplacement.NewRow();
                if (Convert.ToInt16(drow["HipSurgeryTypeID"]) == 3) //BOTH
                {
                    drRow = dtTempHipReplacement.NewRow();
                    drRow["PK_HipReplacementID"] = drow["PK_HipReplacementID"];
                    drRow["FK_DischargeEntryID"] = drow["FK_DischargeEntryID"];
                    drRow["HipSurgeryName"] = drow["HipSurgeryName"];
                    drRow["HipSurgeryDate"] = drow["HipSurgeryDate"];
                    drRow["LHipImplant"] = drow["LHipImplant"];
                    drRow["LAcetabulumCup"] = drow["LAcetabulumCup"];
                    drRow["LLiner"] = drow["LLiner"];
                    drRow["LFemoralHead"] = drow["LFemoralHead"];
                    drRow["LFemoralStem"] = drow["LFemoralStem"];
                    drRow["FK_CreatedBy"] = drow["FK_CreatedBy"];
                    drRow["CreatedOn"] = drow["CreatedOn"];
                    //Added on 16-09-2017
                    drRow["LAcetabulumCupTitle"] = drow["LAcetabulumCupTitle"];
                    drRow["LLinerTitle"] = drow["LLinerTitle"];
                    drRow["LFemoralHeadTitle"] = drow["LFemoralHeadTitle"];
                    drRow["LFemoralStemTitle"] = drow["LFemoralStemTitle"];
                    dtTempHipReplacement.Rows.Add(drRow);

                    drRow = dtTempHipReplacement.NewRow();
                    drRow["PK_HipReplacementID"] = drow["PK_HipReplacementID"];
                    drRow["FK_DischargeEntryID"] = drow["FK_DischargeEntryID"];
                    drRow["HipSurgeryName"] = drow["HipSurgeryName"];
                    drRow["HipSurgeryDate"] = drow["HipSurgeryDate"];
                    drRow["LHipImplant"] = drow["RHipImplant"];
                    drRow["LAcetabulumCup"] = drow["RAcetabulumCup"];
                    drRow["LLiner"] = drow["RLiner"];
                    drRow["LFemoralHead"] = drow["RFemoralHead"];
                    drRow["LFemoralStem"] = drow["RFemoralStem"];
                    drRow["FK_CreatedBy"] = drow["FK_CreatedBy"];
                    drRow["CreatedOn"] = drow["CreatedOn"];
                    //Added on 16-09-2017
                    drRow["LAcetabulumCupTitle"] = drow["RAcetabulumCupTitle"];
                    drRow["LLinerTitle"] = drow["RLinerTitle"];
                    drRow["LFemoralHeadTitle"] = drow["RFemoralHeadTitle"];
                    drRow["LFemoralStemTitle"] = drow["RFemoralStemTitle"];
                    dtTempHipReplacement.Rows.Add(drRow);
                }
                else if (Convert.ToInt16(drow["HipSurgeryTypeID"]) == 1) //LEFT
                {
                    drRow = dtTempHipReplacement.NewRow();
                    drRow["PK_HipReplacementID"] = drow["PK_HipReplacementID"];
                    drRow["FK_DischargeEntryID"] = drow["FK_DischargeEntryID"];
                    drRow["HipSurgeryName"] = drow["HipSurgeryName"];
                    drRow["HipSurgeryDate"] = drow["HipSurgeryDate"];
                    drRow["LHipImplant"] = drow["LHipImplant"];
                    drRow["LAcetabulumCup"] = drow["LAcetabulumCup"];
                    drRow["LLiner"] = drow["LLiner"];
                    drRow["LFemoralHead"] = drow["LFemoralHead"];
                    drRow["LFemoralStem"] = drow["LFemoralStem"];
                    drRow["FK_CreatedBy"] = drow["FK_CreatedBy"];
                    drRow["CreatedOn"] = drow["CreatedOn"];
                    //Added on 16-09-2017
                    drRow["LAcetabulumCupTitle"] = drow["LAcetabulumCupTitle"];
                    drRow["LLinerTitle"] = drow["LLinerTitle"];
                    drRow["LFemoralHeadTitle"] = drow["LFemoralHeadTitle"];
                    drRow["LFemoralStemTitle"] = drow["LFemoralStemTitle"];
                    dtTempHipReplacement.Rows.Add(drRow);
                }
                else if (Convert.ToInt16(drow["HipSurgeryTypeID"]) == 2) //RIGHT
                {
                    drRow = dtTempHipReplacement.NewRow();
                    drRow["PK_HipReplacementID"] = drow["PK_HipReplacementID"];
                    drRow["FK_DischargeEntryID"] = drow["FK_DischargeEntryID"];
                    drRow["HipSurgeryName"] = drow["HipSurgeryName"];
                    drRow["HipSurgeryDate"] = drow["HipSurgeryDate"];
                    drRow["LHipImplant"] = drow["RHipImplant"];
                    drRow["LAcetabulumCup"] = drow["RAcetabulumCup"];
                    drRow["LLiner"] = drow["RLiner"];
                    drRow["LFemoralHead"] = drow["RFemoralHead"];
                    drRow["LFemoralStem"] = drow["RFemoralStem"];
                    drRow["FK_CreatedBy"] = drow["FK_CreatedBy"];
                    drRow["CreatedOn"] = drow["CreatedOn"];
                    //Added on 16-09-2017
                    drRow["LAcetabulumCupTitle"] = drow["RAcetabulumCupTitle"];
                    drRow["LLinerTitle"] = drow["RLinerTitle"];
                    drRow["LFemoralHeadTitle"] = drow["RFemoralHeadTitle"];
                    drRow["LFemoralStemTitle"] = drow["RFemoralStemTitle"];
                    dtTempHipReplacement.Rows.Add(drRow);
                }
            }
            foreach (DataRow drow in dtTempHipReplacement.Rows) dsReportData.tHipReplacementData.ImportRow(drow);

            dsData.Tables[5].TableName = "tKneeReplacement";
            foreach (DataRow drow in dsData.Tables[5].Rows) dsReportData.tKneeReplacement.ImportRow(drow);
            DataTable dtTempKneeReplacement = new DataTable();
            dtTempKneeReplacement = dsReportData.tKneeReplacementData.Clone();

            //For Knee Replacement
            foreach (DataRow drow in dsReportData.tKneeReplacement.Rows)
            {
                DataRow drRow = dtTempKneeReplacement.NewRow();
                if (Convert.ToInt16(drow["KneeSurgeryTypeID"]) == 3) //BOTH
                {
                    drRow = dtTempKneeReplacement.NewRow();
                    drRow["PK_KneeReplacementID"] = drow["PK_KneeReplacementID"];
                    drRow["FK_DischargeEntryID"] = drow["FK_DischargeEntryID"];
                    drRow["KneeSurgeryName"] = drow["KneeSurgeryName"];
                    drRow["KneeSurgeryDate"] = drow["KneeSurgeryDate"];
                    drRow["KneeSurgeryTypeID"] = drow["KneeSurgeryTypeID"];
                    drRow["LKneeImplant"] = drow["LKneeImplant"];
                    drRow["LFemur"] = drow["LFemur"];
                    drRow["LTibia"] = drow["LTibia"];
                    drRow["LPoly"] = drow["LPoly"];
                    drRow["LStem"] = drow["LStem"];
                    drRow["FK_CreatedBy"] = drow["FK_CreatedBy"];
                    drRow["CreatedOn"] = drow["CreatedOn"];
                    //Added on 16-09-2017
                    drRow["LFemurTitle"] = drow["LFemurTitle"];
                    drRow["LTibiaTitle"] = drow["LTibiaTitle"];
                    drRow["LPolyTitle"] = drow["LPolyTitle"];
                    drRow["LStemTitle"] = drow["LStemTitle"];
                    dtTempKneeReplacement.Rows.Add(drRow);

                    drRow = dtTempKneeReplacement.NewRow();
                    drRow["PK_KneeReplacementID"] = drow["PK_KneeReplacementID"];
                    drRow["FK_DischargeEntryID"] = drow["FK_DischargeEntryID"];
                    drRow["KneeSurgeryName"] = drow["KneeSurgeryName"];
                    drRow["KneeSurgeryDate"] = drow["KneeSurgeryDate"];
                    drRow["KneeSurgeryTypeID"] = drow["KneeSurgeryTypeID"];
                    drRow["LKneeImplant"] = drow["RKneeImplant"];
                    drRow["LFemur"] = drow["RFemur"];
                    drRow["LTibia"] = drow["RTibia"];
                    drRow["LPoly"] = drow["RPoly"];
                    drRow["LStem"] = drow["RStem"];
                    drRow["FK_CreatedBy"] = drow["FK_CreatedBy"];
                    drRow["CreatedOn"] = drow["CreatedOn"];
                    //Added on 16-09-2017
                    drRow["LFemurTitle"] = drow["RFemurTitle"];
                    drRow["LTibiaTitle"] = drow["RTibiaTitle"];
                    drRow["LPolyTitle"] = drow["RPolyTitle"];
                    drRow["LStemTitle"] = drow["RStemTitle"];
                    dtTempKneeReplacement.Rows.Add(drRow);
                }
                else if (Convert.ToInt16(drow["KneeSurgeryTypeID"]) == 1) //LEFT
                {
                    drRow = dtTempKneeReplacement.NewRow();
                    drRow["PK_KneeReplacementID"] = drow["PK_KneeReplacementID"];
                    drRow["FK_DischargeEntryID"] = drow["FK_DischargeEntryID"];
                    drRow["KneeSurgeryName"] = drow["KneeSurgeryName"];
                    drRow["KneeSurgeryDate"] = drow["KneeSurgeryDate"];
                    drRow["KneeSurgeryTypeID"] = drow["KneeSurgeryTypeID"];
                    drRow["LKneeImplant"] = drow["LKneeImplant"];
                    drRow["LFemur"] = drow["LFemur"];
                    drRow["LTibia"] = drow["LTibia"];
                    drRow["LPoly"] = drow["LPoly"];
                    drRow["LStem"] = drow["LStem"];
                    drRow["FK_CreatedBy"] = drow["FK_CreatedBy"];
                    drRow["CreatedOn"] = drow["CreatedOn"];
                    //Added on 16-09-2017
                    drRow["LFemurTitle"] = drow["LFemurTitle"];
                    drRow["LTibiaTitle"] = drow["LTibiaTitle"];
                    drRow["LPolyTitle"] = drow["LPolyTitle"];
                    drRow["LStemTitle"] = drow["LStemTitle"];
                    dtTempKneeReplacement.Rows.Add(drRow);
                }
                else if (Convert.ToInt16(drow["KneeSurgeryTypeID"]) == 2) //RIGHT
                {
                    drRow = dtTempHipReplacement.NewRow();
                    drRow["PK_KneeReplacementID"] = drow["PK_KneeReplacementID"];
                    drRow["FK_DischargeEntryID"] = drow["FK_DischargeEntryID"];
                    drRow["KneeSurgeryName"] = drow["KneeSurgeryName"];
                    drRow["KneeSurgeryDate"] = drow["KneeSurgeryDate"];
                    drRow["KneeSurgeryTypeID"] = drow["KneeSurgeryTypeID"];
                    drRow["LKneeImplant"] = drow["RKneeImplant"];
                    drRow["LFemur"] = drow["RFemur"];
                    drRow["LTibia"] = drow["RTibia"];
                    drRow["LPoly"] = drow["RPoly"];
                    drRow["LStem"] = drow["RStem"];
                    drRow["FK_CreatedBy"] = drow["FK_CreatedBy"];
                    drRow["CreatedOn"] = drow["CreatedOn"];
                    //Added on 16-09-2017
                    drRow["LFemurTitle"] = drow["RFemurTitle"];
                    drRow["LTibiaTitle"] = drow["RTibiaTitle"];
                    drRow["LPolyTitle"] = drow["RPolyTitle"];
                    drRow["LStemTitle"] = drow["RStemTitle"];
                    dtTempKneeReplacement.Rows.Add(drRow);
                }
            }
            foreach (DataRow drow in dtTempKneeReplacement.Rows) dsReportData.tKneeReplacementData.ImportRow(drow);

            //Added on 11-09-2017
            dsData.Tables[6].TableName = "tPatientOwnDrug";
            foreach (DataRow drow in dsData.Tables[6].Rows) dsReportData.tPatientOwnDrug.ImportRow(drow);
        }
        return dsReportData;
    }
}