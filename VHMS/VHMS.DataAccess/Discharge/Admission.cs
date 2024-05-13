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
    public class Admission
    {
        public static Collection<Entity.Discharge.Admission> GetAdmission(string sKey, int RowCount = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Discharge.Admission> objList = new Collection<Entity.Discharge.Admission>();
            Entity.Discharge.Admission objAdmission = new Entity.Discharge.Admission();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ADMISSION);
                db.AddInParameter(cmd, "@SearchKey", DbType.String, sKey);
                db.AddInParameter(cmd, "@RowCount", DbType.String, RowCount);

                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objAdmission = new Entity.Discharge.Admission();
                        objAdmission.AdmissionID = Convert.ToInt32(drData["PK_AdmissionID"]);
                        objAdmission.AdmissionNo = Convert.ToString(drData["AdmissionNo"]);
                        objAdmission.UHIDNo = Convert.ToString(drData["UHIDNo"]);
                        objAdmission.RoomNo = Convert.ToString(drData["RoomNo"]);
                        objAdmission.ContactNo = Convert.ToString(drData["ContactNo"]);
                        objAdmission.PatientName = Convert.ToString(drData["PatientName"]);
                        objAdmission.PatientAge = Convert.ToInt16(drData["PatientAge"]);
                        objAdmission.PatientSex = Convert.ToInt16(drData["PatientSex"]);
                        objAdmission.DateofAdmission = Convert.ToDateTime(drData["DateofAdmission"]);
                        if (objAdmission.DateofAdmission.ToString("dd/MM/yyyy") != "01/01/1900")
                        {
                            objAdmission.sDateofAdmissionDate = objAdmission.DateofAdmission.ToString("dd/MM/yyyy");
                            objAdmission.sDateofAdmissionTime = objAdmission.DateofAdmission.ToString("HH:mm");
                        }
                        else
                        {
                            objAdmission.sDateofAdmissionDate = "";
                            objAdmission.sDateofAdmissionTime = "";
                        }
                        //Added on 28-07-2017
                        objAdmission.MLCNo = Convert.ToString(drData["MLCNo"]);
                        objAdmission.PatientAddress = Convert.ToString(drData["PatientAddress"]);

                        //Added on 29-07-2017
                        objAdmission.PrimaryConsultantID = Convert.ToString(drData["PrimaryConsultantID"]);
                        objAdmission.PrimaryConsultant = Convert.ToString(drData["PrimaryConsultant"]);
                        //Added on 17-09-2017
                        objAdmission.SummaryType = Convert.ToString(drData["SummaryType"]);
                        objList.Add(objAdmission);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Admission GetAdmission | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Discharge.Admission GetAdmissionByID(int iAdmissionID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Discharge.Admission objAdmission = new Entity.Discharge.Admission();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ADMISSION);
                db.AddInParameter(cmd, "@PK_AdmissionID", DbType.Int32, iAdmissionID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objAdmission = new Entity.Discharge.Admission();
                        objAdmission.AdmissionID = Convert.ToInt32(drData["PK_AdmissionID"]);
                        objAdmission.AdmissionNo = Convert.ToString(drData["AdmissionNo"]);
                        objAdmission.UHIDNo = Convert.ToString(drData["UHIDNo"]);
                        objAdmission.RoomNo = Convert.ToString(drData["RoomNo"]);
                        objAdmission.ContactNo = Convert.ToString(drData["ContactNo"]);
                        objAdmission.PatientName = Convert.ToString(drData["PatientName"]);
                        objAdmission.PatientAge = Convert.ToInt16(drData["PatientAge"]);
                        objAdmission.PatientSex = Convert.ToInt16(drData["PatientSex"]);
                        objAdmission.DateofAdmission = Convert.ToDateTime(drData["DateofAdmission"]);
                        if (objAdmission.DateofAdmission.ToString("dd/MM/yyyy") != "01/01/1900")
                        {
                            objAdmission.sDateofAdmissionDate = objAdmission.DateofAdmission.ToString("dd/MM/yyyy");
                            objAdmission.sDateofAdmissionTime = objAdmission.DateofAdmission.ToString("HH:mm");
                        }
                        //Added on 28-07-2017
                        objAdmission.MLCNo = Convert.ToString(drData["MLCNo"]);
                        objAdmission.PatientAddress = Convert.ToString(drData["PatientAddress"]);

                        //Added on 29-07-2017
                        objAdmission.PrimaryConsultantID = Convert.ToString(drData["PrimaryConsultantID"]);
                        objAdmission.PrimaryConsultant = Convert.ToString(drData["PrimaryConsultant"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Admission GetAdmissionByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objAdmission;
        }
        public static int AddAdmission(Entity.Discharge.Admission objAdmission)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddAdmission(oDb, objAdmission, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tAdmission", "PK_AdmissionID", objAdmission.AdmissionID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objAdmission.CreatedBy.UserID);
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
        private static int AddAdmission(Database oDb, Entity.Discharge.Admission objAdmission, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_ADMISSION);
                oDb.AddOutParameter(cmd, "@PK_AdmissionID", DbType.Int32, objAdmission.AdmissionID);
                oDb.AddInParameter(cmd, "@AdmissionNo", DbType.String, objAdmission.AdmissionNo);
                oDb.AddInParameter(cmd, "@UHIDNo", DbType.String, objAdmission.UHIDNo);
                oDb.AddInParameter(cmd, "@RoomNo", DbType.String, objAdmission.RoomNo);
                oDb.AddInParameter(cmd, "@ContactNo", DbType.String, objAdmission.ContactNo);
                oDb.AddInParameter(cmd, "@PatientName", DbType.String, objAdmission.PatientName);
                oDb.AddInParameter(cmd, "@PatientAge", DbType.Int16, objAdmission.PatientAge);
                oDb.AddInParameter(cmd, "@PatientSex", DbType.Int16, objAdmission.PatientSex);
                oDb.AddInParameter(cmd, "@PrimaryConsultantID", DbType.String, objAdmission.PrimaryConsultantID);
                oDb.AddInParameter(cmd, "@DateofAdmission", DbType.String, objAdmission.sDateofAdmissionDate);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objAdmission.CreatedBy.UserID);
                //Added on 28-07-2017
                oDb.AddInParameter(cmd, "@MLCNo", DbType.String, objAdmission.MLCNo);
                //Modified on 20-10-2017
                oDb.AddInParameter(cmd, "@PatientAddress", DbType.String, objAdmission.PatientAddress.CapitalizeFirst());
                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_AdmissionID"));
                    objAdmission.AdmissionID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Admission AddAdmission | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateAdmission(Entity.Discharge.Admission objAdmission)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateAdmission(oDb, objAdmission, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tAdmission", "PK_AdmissionID", objAdmission.AdmissionID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objAdmission.ModifiedBy.UserID);
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
        private static bool UpdateAdmission(Database oDb, Entity.Discharge.Admission objAdmission, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_ADMISSION);
                oDb.AddInParameter(cmd, "@PK_AdmissionID", DbType.Int32, objAdmission.AdmissionID);
                oDb.AddInParameter(cmd, "@AdmissionNo", DbType.String, objAdmission.AdmissionNo);
                oDb.AddInParameter(cmd, "@UHIDNo", DbType.String, objAdmission.UHIDNo);
                oDb.AddInParameter(cmd, "@RoomNo", DbType.String, objAdmission.RoomNo);
                oDb.AddInParameter(cmd, "@ContactNo", DbType.String, objAdmission.ContactNo);
                oDb.AddInParameter(cmd, "@PatientName", DbType.String, objAdmission.PatientName);
                oDb.AddInParameter(cmd, "@PatientAge", DbType.Int16, objAdmission.PatientAge);
                oDb.AddInParameter(cmd, "@PatientSex", DbType.Int16, objAdmission.PatientSex);
                oDb.AddInParameter(cmd, "@PrimaryConsultantID", DbType.String, objAdmission.PrimaryConsultantID);
                oDb.AddInParameter(cmd, "@DateofAdmission", DbType.String, objAdmission.sDateofAdmissionDate);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objAdmission.ModifiedBy.UserID);
                //Added on 28-07-2017
                oDb.AddInParameter(cmd, "@MLCNo", DbType.String, objAdmission.MLCNo);
                //Modified on 20-10-2017
                oDb.AddInParameter(cmd, "@PatientAddress", DbType.String, objAdmission.PatientAddress.CapitalizeFirst());
                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Admission UpdateAdmission | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteAdmission(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteAdmission(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tAdmission", "PK_AdmissionID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteAdmission(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_ADMISSION);
                oDb.AddInParameter(cmd, "@PK_AdmissionID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Admission DeleteAdmission | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static DataSet SearchAdmissionNo(string sKey)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsTags = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ADMISSION);
                db.AddInParameter(cmd, "@SearchKey", DbType.String, sKey);
                db.AddInParameter(cmd, "@RowCount", DbType.String, 100);
                dsTags = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "Admission GetAdmissionDetails | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsTags;
        }
    }
}
