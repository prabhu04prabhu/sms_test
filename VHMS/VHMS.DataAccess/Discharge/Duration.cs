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
    public class Duration
    {
        public static Collection<Entity.Discharge.Duration> GetDuration()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Discharge.Duration> objList = new Collection<Entity.Discharge.Duration>();
            Entity.Discharge.Duration objDuration = new Entity.Discharge.Duration();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DURATION);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDuration = new Entity.Discharge.Duration();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objDuration.DurationID = Convert.ToInt32(drData["PK_DurationID"]);
                        objDuration.DurationName = Convert.ToString(drData["DurationName"]);
                        objDuration.DurationCode = Convert.ToString(drData["DurationCode"]);
                        objDuration.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objDuration);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Duration GetDuration | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Discharge.Duration GetDurationByID(int iDurationID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Discharge.Duration objDuration = new Entity.Discharge.Duration();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DURATION);
                db.AddInParameter(cmd, "@PK_DurationID", DbType.Int32, iDurationID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDuration = new Entity.Discharge.Duration();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objDuration.DurationID = Convert.ToInt32(drData["PK_DurationID"]);
                        objDuration.DurationName = Convert.ToString(drData["DurationName"]);
                        objDuration.DurationCode = Convert.ToString(drData["DurationCode"]);
                        objDuration.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Duration GetDurationByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objDuration;
        }
        public static int AddDuration(Entity.Discharge.Duration objDuration)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddDuration(oDb, objDuration, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tDuration", "PK_DurationID", objDuration.DurationID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objDuration.CreatedBy.UserID);
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
        private static int AddDuration(Database oDb, Entity.Discharge.Duration objDuration, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_DURATION);
                oDb.AddOutParameter(cmd, "@PK_DurationID", DbType.Int32, objDuration.DurationID);
                oDb.AddInParameter(cmd, "@DurationName", DbType.String, objDuration.DurationName);
                oDb.AddInParameter(cmd, "@DurationCode", DbType.String, objDuration.DurationCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objDuration.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objDuration.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_DurationID"));
                    objDuration.DurationID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Duration AddDuration | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateDuration(Entity.Discharge.Duration objDuration)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateDuration(oDb, objDuration, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tDuration", "PK_DurationID", objDuration.DurationID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objDuration.ModifiedBy.UserID);
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
        private static bool UpdateDuration(Database oDb, Entity.Discharge.Duration objDuration, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_DURATION);
                oDb.AddInParameter(cmd, "@PK_DurationID", DbType.Int32, objDuration.DurationID);
                oDb.AddInParameter(cmd, "@DurationName", DbType.String, objDuration.DurationName);
                oDb.AddInParameter(cmd, "@DurationCode", DbType.String, objDuration.DurationCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objDuration.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objDuration.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Duration UpdateDuration | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteDuration(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteDuration(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tDuration", "PK_DurationID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteDuration(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_DURATION);
                oDb.AddInParameter(cmd, "@PK_DurationID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Duration DeleteDuration | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static DataSet GetSearchDuration(string sKey)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsTags = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_DURATION);
                db.AddInParameter(cmd, "@key", DbType.String, sKey);
                dsTags = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "Duration GetSearchDuration | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsTags;
        }
    }
}
