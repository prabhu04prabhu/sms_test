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
    public class Dosage
    {
        public static Collection<Entity.Discharge.Dosage> GetDosage()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Discharge.Dosage> objList = new Collection<Entity.Discharge.Dosage>();
            Entity.Discharge.Dosage objDosage = new Entity.Discharge.Dosage();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DOSAGE);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDosage = new Entity.Discharge.Dosage();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objDosage.DosageID = Convert.ToInt32(drData["PK_DosageID"]);
                        objDosage.DosageName = Convert.ToString(drData["DosageName"]);
                        objDosage.DosageCode = Convert.ToString(drData["DosageCode"]);
                        objDosage.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objDosage);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Dosage GetDosage | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Discharge.Dosage GetDosageByID(int iDosageID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Discharge.Dosage objDosage = new Entity.Discharge.Dosage();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DOSAGE);
                db.AddInParameter(cmd, "@PK_DosageID", DbType.Int32, iDosageID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDosage = new Entity.Discharge.Dosage();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objDosage.DosageID = Convert.ToInt32(drData["PK_DosageID"]);
                        objDosage.DosageName = Convert.ToString(drData["DosageName"]);
                        objDosage.DosageCode = Convert.ToString(drData["DosageCode"]);
                        objDosage.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Dosage GetDosageByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objDosage;
        }
        public static int AddDosage(Entity.Discharge.Dosage objDosage)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddDosage(oDb, objDosage, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tDosage", "PK_DosageID", objDosage.DosageID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objDosage.CreatedBy.UserID);
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
        private static int AddDosage(Database oDb, Entity.Discharge.Dosage objDosage, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_DOSAGE);
                oDb.AddOutParameter(cmd, "@PK_DosageID", DbType.Int32, objDosage.DosageID);
                oDb.AddInParameter(cmd, "@DosageName", DbType.String, objDosage.DosageName);
                oDb.AddInParameter(cmd, "@DosageCode", DbType.String, objDosage.DosageCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objDosage.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objDosage.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_DosageID"));
                    objDosage.DosageID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Dosage AddDosage | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateDosage(Entity.Discharge.Dosage objDosage)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateDosage(oDb, objDosage, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tDosage", "PK_DosageID", objDosage.DosageID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objDosage.ModifiedBy.UserID);
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
        private static bool UpdateDosage(Database oDb, Entity.Discharge.Dosage objDosage, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_DOSAGE);
                oDb.AddInParameter(cmd, "@PK_DosageID", DbType.Int32, objDosage.DosageID);
                oDb.AddInParameter(cmd, "@DosageName", DbType.String, objDosage.DosageName);
                oDb.AddInParameter(cmd, "@DosageCode", DbType.String, objDosage.DosageCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objDosage.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objDosage.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Dosage UpdateDosage | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteDosage(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteDosage(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tDosage", "PK_DosageID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteDosage(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_DOSAGE);
                oDb.AddInParameter(cmd, "@PK_DosageID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Dosage DeleteDosage | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static DataSet GetSearchDosage(string sKey)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsTags = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_DOSAGE);
                db.AddInParameter(cmd, "@key", DbType.String, sKey);
                dsTags = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "Dosage GetSearchDosage | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsTags;
        }
    }
}
