using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections.ObjectModel;
using VHMS;

namespace VHMS.DataAccess.Discharge
{
    public class DischargeEntry
    {
        public static Collection<Entity.Discharge.DischargeEntry> GetDischargeEntry()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Discharge.DischargeEntry> objList = new Collection<Entity.Discharge.DischargeEntry>();
            Entity.Discharge.DischargeEntry objDischargeEntry = new Entity.Discharge.DischargeEntry();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DISCHARGEENTRY);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDischargeEntry = new Entity.Discharge.DischargeEntry();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objDischargeEntry.DischargeEntryID = Convert.ToInt32(drData["PK_DischargeEntryID"]);
                        objDischargeEntry.DischargeDateTime = Convert.ToDateTime(drData["DischargeDateTime"]);
                        objDischargeEntry.CoConsultantID = Convert.ToString(drData["CoConsultantID"]);
                        objDischargeEntry.Registrar = Convert.ToString(drData["Registrar"]);
                        objDischargeEntry.ExternalDoctor = Convert.ToString(drData["ExternalDoctor"]);
                        objDischargeEntry.DrugAllergy = Convert.ToString(drData["DrugAllergy"]);
                        objDischargeEntry.Diagnosis = Convert.ToString(drData["Diagnosis"]);
                        objDischargeEntry.CourseDuringStay = Convert.ToString(drData["CourseDuringStay"]);
                        objDischargeEntry.Investigation = Convert.ToString(drData["Investigation"]);
                        objDischargeEntry.PastHistory = Convert.ToString(drData["PastHistory"]);
                        objDischargeEntry.GeneralExamination = Convert.ToString(drData["GeneralExamination"]);
                        objDischargeEntry.LocalExamination = Convert.ToString(drData["LocalExamination"]);
                        objDischargeEntry.AdviseonDischarge = Convert.ToString(drData["AdviseonDischarge"]);
                        objDischargeEntry.ReviewAppointmentDateTime = Convert.ToDateTime(drData["ReviewAppointmentDateTime"]);

                        //Added on 29-072017
                        objDischargeEntry.SurgeryDate = Convert.ToDateTime(drData["SurgeryDate"]);
                        if (objDischargeEntry.SurgeryDate.ToString("dd/MM/yyyy") != "01/01/1900")
                            objDischargeEntry.sSurgeryDate = objDischargeEntry.SurgeryDate.ToString("dd/MM/yyyy");

                        //Added on 01-08-2017
                        objDischargeEntry.SummaryTypeID = Convert.ToInt16(drData["SummaryTypeID"]);
                        objDischargeEntry.RegistrarID = Convert.ToString(drData["RegistrarID"]);
                        //Added on 04-09-2017
                        objDischargeEntry.CauseofDeath = Convert.ToString(drData["CauseofDeath"]);
                        //Added on 05-09-2017
                        objDischargeEntry.WrittenBy = Convert.ToString(drData["WrittenBy"]);
                        objDischargeEntry.CheckedBy = Convert.ToInt32(drData["FK_CheckedBy"]);
                        objDischargeEntry.CheckedByName = Convert.ToString(drData["CheckedByName"]);
                        objDischargeEntry.WeekDays = Convert.ToString(drData["WeekDays"]);
                        objList.Add(objDischargeEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.DischargeEntry GetDischargeEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Discharge.DischargeEntry GetDischargeEntryByID(int iDischargeEntryID, int AdmissionID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Discharge.DischargeEntry objDischargeEntry = new Entity.Discharge.DischargeEntry();
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DISCHARGEENTRY);
                db.AddInParameter(cmd, "@PK_DischargeEntryID", DbType.Int32, iDischargeEntryID);
                db.AddInParameter(cmd, "@FK_AdmissionID", DbType.Int32, AdmissionID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDischargeEntry = new Entity.Discharge.DischargeEntry();

                        objDischargeEntry.DischargeEntryID = Convert.ToInt32(drData["PK_DischargeEntryID"]);
                        objDischargeEntry.DischargeDateTime = Convert.ToDateTime(drData["DischargeDateTime"]);
                        if (objDischargeEntry.DischargeDateTime.ToString("dd/MM/yyyy") != "01/01/1900")
                        {
                            objDischargeEntry.sDischargeDate = objDischargeEntry.DischargeDateTime.ToString("dd/MM/yyyy");
                            objDischargeEntry.sDischargeTime = objDischargeEntry.DischargeDateTime.ToString("HH:mm");
                        }
                        objDischargeEntry.CoConsultantID = Convert.ToString(drData["CoConsultantID"]);
                        objDischargeEntry.Registrar = Convert.ToString(drData["Registrar"]);
                        objDischargeEntry.ExternalDoctor = Convert.ToString(drData["ExternalDoctor"]);
                        objDischargeEntry.DrugAllergy = Convert.ToString(drData["DrugAllergy"]);
                        objDischargeEntry.Diagnosis = Convert.ToString(drData["Diagnosis"]);
                        objDischargeEntry.CourseDuringStay = Convert.ToString(drData["CourseDuringStay"]);
                        objDischargeEntry.Investigation = Convert.ToString(drData["Investigation"]);
                        objDischargeEntry.PastHistory = Convert.ToString(drData["PastHistory"]);
                        objDischargeEntry.GeneralExamination = Convert.ToString(drData["GeneralExamination"]);
                        objDischargeEntry.LocalExamination = Convert.ToString(drData["LocalExamination"]);
                        objDischargeEntry.AdviseonDischarge = Convert.ToString(drData["AdviseonDischarge"]);
                        objDischargeEntry.ReviewAppointmentDateTime = Convert.ToDateTime(drData["ReviewAppointmentDateTime"]);
                        if (objDischargeEntry.ReviewAppointmentDateTime.ToString("dd/MM/yyyy") != "01/01/1900")
                        {
                            objDischargeEntry.sReviewAppointmentDate = objDischargeEntry.ReviewAppointmentDateTime.ToString("dd/MM/yyyy");
                            objDischargeEntry.sReviewAppointmentTime = objDischargeEntry.ReviewAppointmentDateTime.ToString("HH:mm");
                        }
                        else
                        { objDischargeEntry.sReviewAppointmentDate = ""; objDischargeEntry.sReviewAppointmentTime = ""; }

                        objDischargeEntry.Admission = Admission.GetAdmissionByID(Convert.ToInt32(drData["FK_AdmissionID"]));
                        objDischargeEntry.HipReplacement = HipReplacement.GetHipReplacement(objDischargeEntry.DischargeEntryID);
                        objDischargeEntry.KneeReplacement = KneeReplacement.GetKneeReplacement(objDischargeEntry.DischargeEntryID);
                        objDischargeEntry.OtherSurgery = OtherSurgery.GetOtherSurgery(objDischargeEntry.DischargeEntryID);
                        objDischargeEntry.Prescription = Prescription.GetPrescription(objDischargeEntry.DischargeEntryID);

                        //Added on 29-072017
                        objDischargeEntry.SurgeryDate = Convert.ToDateTime(drData["SurgeryDate"]);
                        if (objDischargeEntry.SurgeryDate.ToString("dd/MM/yyyy") != "01/01/1900") objDischargeEntry.sSurgeryDate = objDischargeEntry.SurgeryDate.ToString("dd/MM/yyyy");
                        objDischargeEntry.RegistrarID = Convert.ToString(drData["RegistrarID"]);

                        //Added on 01-08-2017
                        objDischargeEntry.SummaryTypeID = Convert.ToInt16(drData["SummaryTypeID"]);
                        //Added on 04-09-2017
                        objDischargeEntry.CauseofDeath = Convert.ToString(drData["CauseofDeath"]);
                        //Added on 05-09-2017
                        objDischargeEntry.WrittenBy = Convert.ToString(drData["WrittenBy"]);
                        objDischargeEntry.CheckedBy = Convert.ToInt32(drData["FK_CheckedBy"]);
                        objDischargeEntry.CheckedByName = Convert.ToString(drData["CheckedByName"]);
                        objDischargeEntry.WeekDays = Convert.ToString(drData["WeekDays"]);

                        //Added on 08-09-2017
                        objDischargeEntry.PatientOwnDrug = PatientOwnDrug.GetPatientOwnDrug(objDischargeEntry.DischargeEntryID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.DischargeEntry GetDischargeEntryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objDischargeEntry;
        }
        public static int AddDischargeEntry(Entity.Discharge.DischargeEntry objDischargeEntry)
        {
            bool IsInserted = false;
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddDischargeEntry(oDb, objDischargeEntry, oTrans);
                    oTrans.Commit();

                    if (ID > 0)
                        Framework.InsertAuditLog("tDischargeEntry", "PK_DischargeEntryID", objDischargeEntry.DischargeEntryID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objDischargeEntry.CreatedBy.UserID);

                    if (ID > 0)
                    {
                        //HipReplacement Insert                        
                        foreach (Entity.Discharge.HipReplacement objtoHipReplacement in objDischargeEntry.HipReplacement)
                        {
                            objtoHipReplacement.DischargeEntryID = ID;
                            objtoHipReplacement.CreatedBy = objDischargeEntry.CreatedBy;

                            if (objtoHipReplacement.StatusFlag == "I")
                                IsInserted = HipReplacement.AddHipReplacement(objtoHipReplacement);
                        }
                        //KneeReplacement
                        foreach (Entity.Discharge.KneeReplacement objtoKneeReplacement in objDischargeEntry.KneeReplacement)
                        {
                            objtoKneeReplacement.DischargeEntryID = ID;
                            objtoKneeReplacement.CreatedBy = objDischargeEntry.CreatedBy;

                            if (objtoKneeReplacement.StatusFlag == "I")
                                IsInserted = KneeReplacement.AddKneeReplacement(objtoKneeReplacement);
                        }
                        //OtherSurgery                        
                        foreach (Entity.Discharge.OtherSurgery objtoOtherSurgery in objDischargeEntry.OtherSurgery)
                        {
                            objtoOtherSurgery.DischargeEntryID = ID;
                            objtoOtherSurgery.CreatedBy = objDischargeEntry.CreatedBy;

                            if (objtoOtherSurgery.StatusFlag == "I")
                                IsInserted = OtherSurgery.AddOtherSurgery(objtoOtherSurgery);
                        }
                        //Prescription Insert                        
                        foreach (Entity.Discharge.Prescription objtoPrescription in objDischargeEntry.Prescription)
                        {
                            objtoPrescription.DischargeEntryID = ID;
                            objtoPrescription.CreatedBy = objDischargeEntry.CreatedBy;

                            if (objtoPrescription.StatusFlag == "I") IsInserted = Prescription.AddPrescription(objtoPrescription);
                        }

                        //Added on 08-09-2017
                        //PatientOwnDrug Insert                        
                        foreach (Entity.Discharge.PatientOwnDrug objtoPatientOwnDrug in objDischargeEntry.PatientOwnDrug)
                        {
                            objtoPatientOwnDrug.DischargeEntryID = ID;
                            objtoPatientOwnDrug.CreatedBy = objDischargeEntry.CreatedBy;

                            if (objtoPatientOwnDrug.StatusFlag == "I") IsInserted = PatientOwnDrug.AddPatientOwnDrug(objtoPatientOwnDrug);
                        }
                    }
                }
                catch (Exception ex)
                {
                    oTrans.Rollback();
                    throw ex;
                }
                finally
                {
                    oDbCon.Close();
                }
            }
            return ID;
        }
        private static int AddDischargeEntry(Database oDb, Entity.Discharge.DischargeEntry objDischargeEntry, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_DISCHARGEENTRY);
                oDb.AddOutParameter(cmd, "@PK_DischargeEntryID", DbType.Int32, objDischargeEntry.DischargeEntryID);
                oDb.AddInParameter(cmd, "@DischargeDateTime", DbType.String, objDischargeEntry.sDischargeDate);
                oDb.AddInParameter(cmd, "@CoConsultantID", DbType.String, objDischargeEntry.CoConsultantID);
                oDb.AddInParameter(cmd, "@RegistrarID", DbType.String, objDischargeEntry.RegistrarID);
                oDb.AddInParameter(cmd, "@ExternalDoctor", DbType.String, objDischargeEntry.ExternalDoctor);
                oDb.AddInParameter(cmd, "@DrugAllergy", DbType.String, objDischargeEntry.DrugAllergy.CapitalizeFirst());
                oDb.AddInParameter(cmd, "@Diagnosis", DbType.String, objDischargeEntry.Diagnosis.CapitalizeFirst());
                oDb.AddInParameter(cmd, "@CourseDuringStay", DbType.String, objDischargeEntry.CourseDuringStay.CapitalizeFirst());
                oDb.AddInParameter(cmd, "@Investigation", DbType.String, objDischargeEntry.Investigation.CapitalizeFirst());
                oDb.AddInParameter(cmd, "@PastHistory", DbType.String, objDischargeEntry.PastHistory.CapitalizeFirst());
                oDb.AddInParameter(cmd, "@GeneralExamination", DbType.String, objDischargeEntry.GeneralExamination.CapitalizeFirst());
                oDb.AddInParameter(cmd, "@LocalExamination", DbType.String, objDischargeEntry.LocalExamination.CapitalizeFirst());
                oDb.AddInParameter(cmd, "@AdviseonDischarge", DbType.String, objDischargeEntry.AdviseonDischarge.CapitalizeFirst());
                oDb.AddInParameter(cmd, "@ReviewAppointmentDateTime", DbType.String, objDischargeEntry.sReviewAppointmentDate);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objDischargeEntry.CreatedBy.UserID);
                //Added on 13-07-2017
                oDb.AddInParameter(cmd, "@FK_AdmissionID", DbType.Int32, objDischargeEntry.Admission.AdmissionID);
                //Added on 29-07-2017
                oDb.AddInParameter(cmd, "@SurgeryDate", DbType.String, objDischargeEntry.sSurgeryDate);
                //Added on 01-08-2017
                oDb.AddInParameter(cmd, "@SummaryTypeID", DbType.Int16, objDischargeEntry.SummaryTypeID);
                //Added on 04-09-2017
                oDb.AddInParameter(cmd, "@CauseofDeath", DbType.String, objDischargeEntry.CauseofDeath.CapitalizeFirst());
                //Added on 05-09-2017
                oDb.AddInParameter(cmd, "@WrittenBy", DbType.String, objDischargeEntry.WrittenBy);
                oDb.AddInParameter(cmd, "@FK_CheckedBy", DbType.Int32, objDischargeEntry.CheckedBy);
                oDb.AddInParameter(cmd, "@WeekDays", DbType.String, objDischargeEntry.WeekDays);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_DischargeEntryID"));
                    objDischargeEntry.DischargeEntryID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.DischargeEntry AddDischargeEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateDischargeEntry(Entity.Discharge.DischargeEntry objDischargeEntry)
        {
            bool IsUpdated = false, IsInserted = false, IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateDischargeEntry(oDb, objDischargeEntry, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tDischargeEntry", "PK_DischargeEntryID", objDischargeEntry.DischargeEntryID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objDischargeEntry.ModifiedBy.UserID);

                    if (IsUpdated)
                    {

                        //HipReplacement                        
                        foreach (Entity.Discharge.HipReplacement objtoHipReplacement in objDischargeEntry.HipReplacement)
                        {
                            objtoHipReplacement.CreatedBy = objDischargeEntry.ModifiedBy;
                            objtoHipReplacement.ModifiedBy = objDischargeEntry.ModifiedBy;
                            if (objtoHipReplacement.StatusFlag == "I")
                            {
                                objtoHipReplacement.DischargeEntryID = objDischargeEntry.DischargeEntryID;
                                IsInserted = HipReplacement.AddHipReplacement(objtoHipReplacement);
                            }
                            else if (objtoHipReplacement.StatusFlag == "U")
                                IsUpdated = HipReplacement.UpdateHipReplacement(objtoHipReplacement);
                            else if (objtoHipReplacement.StatusFlag == "D")
                                IsDeleted = HipReplacement.DeleteHipReplacement(objtoHipReplacement.HipReplacementID, objtoHipReplacement.ModifiedBy.UserID);
                        }
                        //KneeReplacement                        
                        foreach (Entity.Discharge.KneeReplacement objtoKneeReplacement in objDischargeEntry.KneeReplacement)
                        {
                            objtoKneeReplacement.CreatedBy = objDischargeEntry.ModifiedBy;
                            objtoKneeReplacement.ModifiedBy = objDischargeEntry.ModifiedBy;
                            if (objtoKneeReplacement.StatusFlag == "I")
                            {
                                objtoKneeReplacement.DischargeEntryID = objDischargeEntry.DischargeEntryID;
                                IsInserted = KneeReplacement.AddKneeReplacement(objtoKneeReplacement);
                            }
                            else if (objtoKneeReplacement.StatusFlag == "U")
                                IsUpdated = KneeReplacement.UpdateKneeReplacement(objtoKneeReplacement);
                            else if (objtoKneeReplacement.StatusFlag == "D")
                                IsDeleted = KneeReplacement.DeleteKneeReplacement(objtoKneeReplacement.KneeReplacementID, objtoKneeReplacement.ModifiedBy.UserID);
                        }
                        //OtherSurgery                        
                        foreach (Entity.Discharge.OtherSurgery objtoOtherSurgery in objDischargeEntry.OtherSurgery)
                        {
                            objtoOtherSurgery.CreatedBy = objDischargeEntry.ModifiedBy;
                            objtoOtherSurgery.ModifiedBy = objDischargeEntry.ModifiedBy;
                            if (objtoOtherSurgery.StatusFlag == "I")
                            {
                                objtoOtherSurgery.DischargeEntryID = objDischargeEntry.DischargeEntryID;
                                IsInserted = OtherSurgery.AddOtherSurgery(objtoOtherSurgery);
                            }
                            else if (objtoOtherSurgery.StatusFlag == "U")
                                IsUpdated = OtherSurgery.UpdateOtherSurgery(objtoOtherSurgery);
                            else if (objtoOtherSurgery.StatusFlag == "D")
                                IsDeleted = OtherSurgery.DeleteOtherSurgery(objtoOtherSurgery.OtherSurgeryID, objtoOtherSurgery.ModifiedBy.UserID);

                        }
                        //Prescription
                        Prescription oPrescription = new Prescription();
                        foreach (Entity.Discharge.Prescription objtoPrescription in objDischargeEntry.Prescription)
                        {
                            objtoPrescription.CreatedBy = objDischargeEntry.ModifiedBy;
                            objtoPrescription.ModifiedBy = objDischargeEntry.ModifiedBy;
                            if (objtoPrescription.StatusFlag == "I")
                            {
                                objtoPrescription.DischargeEntryID = objDischargeEntry.DischargeEntryID;
                                IsInserted = Prescription.AddPrescription(objtoPrescription);
                            }
                            else if (objtoPrescription.StatusFlag == "U")
                                IsUpdated = Prescription.UpdatePrescription(objtoPrescription);
                            else if (objtoPrescription.StatusFlag == "D")
                                IsDeleted = Prescription.DeletePrescription(objtoPrescription.PrescriptionID, objtoPrescription.ModifiedBy.UserID);
                        }

                        //PatientOwnDrug
                        PatientOwnDrug oPatientOwnDrug = new PatientOwnDrug();
                        foreach (Entity.Discharge.PatientOwnDrug objtoPatientOwnDrug in objDischargeEntry.PatientOwnDrug)
                        {
                            objtoPatientOwnDrug.CreatedBy = objDischargeEntry.ModifiedBy;
                            objtoPatientOwnDrug.ModifiedBy = objDischargeEntry.ModifiedBy;
                            if (objtoPatientOwnDrug.StatusFlag == "I")
                            {
                                objtoPatientOwnDrug.DischargeEntryID = objDischargeEntry.DischargeEntryID;
                                IsInserted = PatientOwnDrug.AddPatientOwnDrug(objtoPatientOwnDrug);
                            }
                            else if (objtoPatientOwnDrug.StatusFlag == "U")
                                IsUpdated = PatientOwnDrug.UpdatePatientOwnDrug(objtoPatientOwnDrug);
                            else if (objtoPatientOwnDrug.StatusFlag == "D")
                                IsDeleted = PatientOwnDrug.DeletePatientOwnDrug(objtoPatientOwnDrug.PatientOwnDrugID, objtoPatientOwnDrug.ModifiedBy.UserID);
                        }
                    }
                }
                catch (Exception ex)
                {
                    oTrans.Rollback();
                    throw ex;
                }
                finally
                {
                    oDbCon.Close();
                }
            }
            return IsUpdated;
        }
        private static bool UpdateDischargeEntry(Database oDb, Entity.Discharge.DischargeEntry objDischargeEntry, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_DISCHARGEENTRY);
                oDb.AddInParameter(cmd, "@PK_DischargeEntryID", DbType.Int32, objDischargeEntry.DischargeEntryID);
                oDb.AddInParameter(cmd, "@DischargeDateTime", DbType.String, objDischargeEntry.sDischargeDate);
                oDb.AddInParameter(cmd, "@CoConsultantID", DbType.String, objDischargeEntry.CoConsultantID);
                oDb.AddInParameter(cmd, "@RegistrarID", DbType.String, objDischargeEntry.RegistrarID);
                oDb.AddInParameter(cmd, "@ExternalDoctor", DbType.String, objDischargeEntry.ExternalDoctor);
                oDb.AddInParameter(cmd, "@DrugAllergy", DbType.String, objDischargeEntry.DrugAllergy.CapitalizeFirst());
                oDb.AddInParameter(cmd, "@Diagnosis", DbType.String, objDischargeEntry.Diagnosis.CapitalizeFirst());
                oDb.AddInParameter(cmd, "@CourseDuringStay", DbType.String, objDischargeEntry.CourseDuringStay.CapitalizeFirst());
                oDb.AddInParameter(cmd, "@Investigation", DbType.String, objDischargeEntry.Investigation.CapitalizeFirst());
                oDb.AddInParameter(cmd, "@PastHistory", DbType.String, objDischargeEntry.PastHistory.CapitalizeFirst());
                oDb.AddInParameter(cmd, "@GeneralExamination", DbType.String, objDischargeEntry.GeneralExamination.CapitalizeFirst());
                oDb.AddInParameter(cmd, "@LocalExamination", DbType.String, objDischargeEntry.LocalExamination.CapitalizeFirst());
                oDb.AddInParameter(cmd, "@AdviseonDischarge", DbType.String, objDischargeEntry.AdviseonDischarge.CapitalizeFirst());
                oDb.AddInParameter(cmd, "@ReviewAppointmentDateTime", DbType.String, objDischargeEntry.sReviewAppointmentDate);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objDischargeEntry.ModifiedBy.UserID);
                //Added on 13-07-2017
                oDb.AddInParameter(cmd, "@FK_AdmissionID", DbType.Int32, objDischargeEntry.Admission.AdmissionID);
                //Added on 29-07-2017
                oDb.AddInParameter(cmd, "@SurgeryDate", DbType.String, objDischargeEntry.sSurgeryDate);
                //Added on 01-08-2017
                oDb.AddInParameter(cmd, "@SummaryTypeID", DbType.Int16, objDischargeEntry.SummaryTypeID);
                //Added on 04-09-2017
                oDb.AddInParameter(cmd, "@CauseofDeath", DbType.String, objDischargeEntry.CauseofDeath.CapitalizeFirst());
                //Added on 05-09-2017
                oDb.AddInParameter(cmd, "@WrittenBy", DbType.String, objDischargeEntry.WrittenBy);
                oDb.AddInParameter(cmd, "@FK_CheckedBy", DbType.Int32, objDischargeEntry.CheckedBy);
                oDb.AddInParameter(cmd, "@WeekDays", DbType.String, objDischargeEntry.WeekDays);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.DischargeEntry UpdateDischargeEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteDischargeEntry(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteDischargeEntry(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tDischargeEntry", "PK_DischargeEntryID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
                }
                catch (Exception ex)
                {
                    oTrans.Rollback();
                    throw ex;
                }
                finally
                {
                    oDbCon.Close();
                }
            }
            return IsDeleted;
        }
        private static bool DeleteDischargeEntry(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_DISCHARGEENTRY);
                oDb.AddInParameter(cmd, "@PK_DischargeEntryID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.DischargeEntry DeleteDischargeEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
