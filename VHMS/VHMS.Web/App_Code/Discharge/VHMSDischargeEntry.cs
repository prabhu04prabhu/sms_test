using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;
using System.Data;
using System.Web;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Globalization;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using VHMS.Entity;

public partial class VHMSService : IVHMSService
{
    #region "DischargeEntry"
    public string GetDischargeEntry()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                jsObject.MaxJsonLength = int.MaxValue;
                Collection<VHMS.Entity.Discharge.DischargeEntry> ObjList = new Collection<VHMS.Entity.Discharge.DischargeEntry>();
                ObjList = VHMS.DataAccess.Discharge.DischargeEntry.GetDischargeEntry();
                objResponse.Status = "Success";
                objResponse.Value = ObjList.Count > 0 ? jsObject.Serialize(ObjList) : "NoRecord";
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.DischargeEntry GetDischargeEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetDischargeEntryByID(int ID, int AdmissionID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            jsObject.MaxJsonLength = int.MaxValue;
            if (ValidateSession())
            {
                VHMS.Entity.Discharge.DischargeEntry objDischargeEntry = new VHMS.Entity.Discharge.DischargeEntry();
                objDischargeEntry = VHMS.DataAccess.Discharge.DischargeEntry.GetDischargeEntryByID(ID, AdmissionID);
                if (objDischargeEntry.DischargeEntryID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objDischargeEntry);
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "NoRecord";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.DischargeEntry GetDischargeEntryByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddDischargeEntry(VHMS.Entity.Discharge.DischargeEntry Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iDischargeEntryId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;

                //Added on 13-07-2017
                DateTime dtAdmissioinDate = DateTime.MinValue, dtAdmissionTime = DateTime.MinValue;
                if (!string.IsNullOrEmpty(Objdata.Admission.sDateofAdmissionDate)) dtAdmissioinDate = DateTime.ParseExact(Objdata.Admission.sDateofAdmissionDate, "dd/MM/yyyy", null);
                if (!string.IsNullOrEmpty(Objdata.Admission.sDateofAdmissionTime)) dtAdmissionTime = DateTime.ParseExact(Objdata.Admission.sDateofAdmissionTime, "HH:mm", null);
                Objdata.Admission.DateofAdmission = dtAdmissioinDate.AddHours(dtAdmissionTime.Hour).AddMinutes(dtAdmissionTime.Minute);
                if (Objdata.Admission.DateofAdmission != DateTime.MinValue) Objdata.Admission.sDateofAdmissionDate = Objdata.Admission.DateofAdmission.ToString("dd/MM/yyyy HH:mm");

                Objdata.Admission.CreatedBy = objUser;
                Objdata.Admission.AdmissionID = VHMS.DataAccess.Discharge.Admission.AddAdmission(Objdata.Admission);

                DateTime dtDischargeDate = DateTime.MinValue, dtDischargeTime = DateTime.MinValue;
                if (!string.IsNullOrEmpty(Objdata.sDischargeDate)) dtDischargeDate = DateTime.ParseExact(Objdata.sDischargeDate, "dd/MM/yyyy", null);
                if (!string.IsNullOrEmpty(Objdata.sDischargeTime)) dtDischargeTime = DateTime.ParseExact(Objdata.sDischargeTime, "HH:mm", null);
                Objdata.DischargeDateTime = dtDischargeDate.AddHours(dtDischargeTime.Hour).AddMinutes(dtDischargeTime.Minute);
                if (Objdata.DischargeDateTime != DateTime.MinValue) Objdata.sDischargeDate = Objdata.DischargeDateTime.ToString("dd/MM/yyyy HH:mm");

                DateTime dtReviewDate = DateTime.MinValue, dtReviewTime = DateTime.MinValue;
                if (!string.IsNullOrEmpty(Objdata.sReviewAppointmentDate)) dtReviewDate = DateTime.ParseExact(Objdata.sReviewAppointmentDate, "dd/MM/yyyy", null);
                if (!string.IsNullOrEmpty(Objdata.sReviewAppointmentTime)) dtReviewTime = DateTime.ParseExact(Objdata.sReviewAppointmentTime, "HH:mm", null);
                Objdata.ReviewAppointmentDateTime = dtReviewDate.AddHours(dtReviewTime.Hour).AddMinutes(dtReviewTime.Minute);
                if (Objdata.ReviewAppointmentDateTime != DateTime.MinValue) Objdata.sReviewAppointmentDate = Objdata.ReviewAppointmentDateTime.ToString("dd/MM/yyyy HH:mm");

                //Added on 17-09-2017
                DateTime dtSurgeryDate = DateTime.MinValue;
                if (!string.IsNullOrEmpty(Objdata.sSurgeryDate)) dtSurgeryDate = DateTime.ParseExact(Objdata.sSurgeryDate, "dd/MM/yyyy", null);
                Objdata.SurgeryDate = dtSurgeryDate;

                //InsertDosageandDuration(Objdata, objUser);
                //Added on 05-09-2017
                InsertOtherProcedure(Objdata, objUser);

                iDischargeEntryId = VHMS.DataAccess.Discharge.DischargeEntry.AddDischargeEntry(Objdata);
                if (iDischargeEntryId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iDischargeEntryId.ToString();
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.DischargeEntry AddDischargeEntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "DischargeEntry_U_01" || ex.Message.ToString() == "Admission_A_01" || ex.Message.ToString() == "Admission_U_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "Error";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    private void InsertDosageandDuration(VHMS.Entity.Discharge.DischargeEntry Objdata, VHMS.Entity.User oUser)
    {
        int ID = 0;
        VHMS.Entity.Discharge.Dosage oDosage;
        VHMS.Entity.Discharge.Duration oDuration;

        foreach (VHMS.Entity.Discharge.Prescription oPrescription in Objdata.Prescription)
        {
            if (oPrescription.DosageID == 0)
            {
                oDosage = new VHMS.Entity.Discharge.Dosage();
                oDosage.DosageID = 0;
                oDosage.DosageName = oPrescription.Dosage;
                oDosage.IsActive = true;
                oDosage.CreatedBy = oUser;
                ID = VHMS.DataAccess.Discharge.Dosage.AddDosage(oDosage);
            }
            if (oPrescription.DurationID == 0)
            {
                oDuration = new VHMS.Entity.Discharge.Duration();
                oDuration.DurationID = 0;
                oDuration.DurationName = oPrescription.Duration;
                oDuration.IsActive = true;
                oDuration.CreatedBy = oUser;
                ID = VHMS.DataAccess.Discharge.Duration.AddDuration(oDuration);
            }
        }
    }
    private void InsertOtherProcedure(VHMS.Entity.Discharge.DischargeEntry Objdata, VHMS.Entity.User oUser)
    {
        VHMS.Entity.Discharge.OtherProcedure oOtherProcedure;
        foreach (VHMS.Entity.Discharge.OtherSurgery oOtherSurgery in Objdata.OtherSurgery)
        {
            if (oOtherSurgery.OtherProcedure.OtherProcedureID == 0)
            {
                oOtherProcedure = new VHMS.Entity.Discharge.OtherProcedure();
                oOtherProcedure.OtherProcedureName = oOtherSurgery.OtherProcedure.OtherProcedureName;
                oOtherProcedure.OtherProcedureDescription = oOtherSurgery.OtherProcedure.OtherProcedureDescription;
                oOtherProcedure.IsActive = true;
                oOtherProcedure.CreatedBy = oUser;
                oOtherProcedure.OtherProcedureID = VHMS.DataAccess.Discharge.OtherProcedure.AddOtherProcedure(oOtherProcedure);
                oOtherSurgery.OtherProcedure = oOtherProcedure;
            }
        }
    }
    public string UpdateDischargeEntry(VHMS.Entity.Discharge.DischargeEntry Objdata)
    {
        string sDischargeEntryId = string.Empty;
        string sException = string.Empty;
        bool bDischargeEntry = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserID;
                Objdata.ModifiedBy = objUser;

                //Added on 13-07-2017
                DateTime dtAdmissioinDate = DateTime.MinValue, dtAdmissionTime = DateTime.MinValue;
                if (!string.IsNullOrEmpty(Objdata.Admission.sDateofAdmissionDate)) dtAdmissioinDate = DateTime.ParseExact(Objdata.Admission.sDateofAdmissionDate, "dd/MM/yyyy", null);
                if (!string.IsNullOrEmpty(Objdata.Admission.sDateofAdmissionTime)) dtAdmissionTime = DateTime.ParseExact(Objdata.Admission.sDateofAdmissionTime, "HH:mm", null);
                Objdata.Admission.DateofAdmission = dtAdmissioinDate.AddHours(dtAdmissionTime.Hour).AddMinutes(dtAdmissionTime.Minute);
                if (Objdata.Admission.DateofAdmission != DateTime.MinValue) Objdata.Admission.sDateofAdmissionDate = Objdata.Admission.DateofAdmission.ToString("dd/MM/yyyy HH:mm");

                Objdata.Admission.ModifiedBy = objUser;
                VHMS.DataAccess.Discharge.Admission.UpdateAdmission(Objdata.Admission);

                DateTime dtDischargeDate = DateTime.MinValue, dtDischargeTime = DateTime.MinValue;
                if (!string.IsNullOrEmpty(Objdata.sDischargeDate)) dtDischargeDate = DateTime.ParseExact(Objdata.sDischargeDate, "dd/MM/yyyy", null);
                if (!string.IsNullOrEmpty(Objdata.sDischargeTime)) dtDischargeTime = DateTime.ParseExact(Objdata.sDischargeTime, "HH:mm", null);
                Objdata.DischargeDateTime = dtDischargeDate.AddHours(dtDischargeTime.Hour).AddMinutes(dtDischargeTime.Minute);
                if (Objdata.DischargeDateTime != DateTime.MinValue) Objdata.sDischargeDate = Objdata.DischargeDateTime.ToString("dd/MM/yyyy HH:mm");

                DateTime dtReviewDate = DateTime.MinValue, dtReviewTime = DateTime.MinValue;
                if (!string.IsNullOrEmpty(Objdata.sReviewAppointmentDate)) dtReviewDate = DateTime.ParseExact(Objdata.sReviewAppointmentDate, "dd/MM/yyyy", null);
                if (!string.IsNullOrEmpty(Objdata.sReviewAppointmentTime)) dtReviewTime = DateTime.ParseExact(Objdata.sReviewAppointmentTime, "HH:mm", null);
                Objdata.ReviewAppointmentDateTime = dtReviewDate.AddHours(dtReviewTime.Hour).AddMinutes(dtReviewTime.Minute);
                if (Objdata.ReviewAppointmentDateTime != DateTime.MinValue) Objdata.sReviewAppointmentDate = Objdata.ReviewAppointmentDateTime.ToString("dd/MM/yyyy HH:mm");

                //Added on 17-09-2017
                DateTime dtSurgeryDate = DateTime.MinValue;
                if (!string.IsNullOrEmpty(Objdata.sSurgeryDate)) dtSurgeryDate = DateTime.ParseExact(Objdata.sSurgeryDate, "dd/MM/yyyy", null);
                Objdata.SurgeryDate = dtSurgeryDate;

                //InsertDosageandDuration(Objdata, objUser);
                //Added on 05-09-2017
                InsertOtherProcedure(Objdata, objUser);

                bDischargeEntry = VHMS.DataAccess.Discharge.DischargeEntry.UpdateDischargeEntry(Objdata);
                if (bDischargeEntry == true)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "1";
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "0";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.DischargeEntry UpdateDischargeEntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "DischargeEntry_U_01" || ex.Message.ToString() == "Admission_A_01" || ex.Message.ToString() == "Admission_U_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string DeleteDischargeEntry(int ID)
    {
        string sDischargeEntryId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bDischargeEntry = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bDischargeEntry = VHMS.DataAccess.Discharge.DischargeEntry.DeleteDischargeEntry(ID, UserID);
                if (bDischargeEntry == true)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "1";
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "0";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.DischargeEntry DeleteDischargeEntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "DischargeEntry_R_01" || ex.Message.ToString() == "DischargeEntry_D_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    //Added on 13-07-2017
    public string SearchAdmissionNo(string key)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        DataSet dsResult = null;
        try
        {
            jsObject.MaxJsonLength = Int32.MaxValue;
            if (ValidateSession())
            {
                dsResult = VHMS.DataAccess.Discharge.Admission.SearchAdmissionNo(key);
                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = GetDtJson(dsResult.Tables[0]);
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "NoRecord";
                }
            }
            else
                return "0";
        }
        catch (Exception ex)
        {
            sException = "VHMSService.DischargeEntry SearchAdmissionNo |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetAdmission(string key, int RowCount = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Discharge.Admission> ObjList = new Collection<VHMS.Entity.Discharge.Admission>();
                ObjList = VHMS.DataAccess.Discharge.Admission.GetAdmission(key, RowCount);
                objResponse.Status = "Success";
                objResponse.Value = ObjList.Count > 0 ? jsObject.Serialize(ObjList) : "NoRecord";
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.DischargeEntry GetAdmission |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    #endregion
}