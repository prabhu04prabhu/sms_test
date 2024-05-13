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
    public class Frequency
    {
        public static Collection<Entity.Discharge.Frequency> GetFrequency()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Discharge.Frequency> objList = new Collection<Entity.Discharge.Frequency>();
            Entity.Discharge.Frequency objFrequency = new Entity.Discharge.Frequency();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_FREQUENCY);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objFrequency = new Entity.Discharge.Frequency();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objFrequency.FrequencyID = Convert.ToInt32(drData["PK_FrequencyID"]);
                        objFrequency.FrequencyName = Convert.ToString(drData["FrequencyName"]);
                        objFrequency.FrequencyCode = Convert.ToString(drData["FrequencyCode"]);
                        objFrequency.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objFrequency);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Frequency GetFrequency | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Discharge.Frequency GetFrequencyByID(int iFrequencyID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Discharge.Frequency objFrequency = new Entity.Discharge.Frequency();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_FREQUENCY);
                db.AddInParameter(cmd, "@PK_FrequencyID", DbType.Int32, iFrequencyID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objFrequency = new Entity.Discharge.Frequency();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objFrequency.FrequencyID = Convert.ToInt32(drData["PK_FrequencyID"]);
                        objFrequency.FrequencyName = Convert.ToString(drData["FrequencyName"]);
                        objFrequency.FrequencyCode = Convert.ToString(drData["FrequencyCode"]);
                        objFrequency.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Frequency GetFrequencyByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objFrequency;
        }
        public static int AddFrequency(Entity.Discharge.Frequency objFrequency)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddFrequency(oDb, objFrequency, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tFrequency", "PK_FrequencyID", objFrequency.FrequencyID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objFrequency.CreatedBy.UserID);
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
        private static int AddFrequency(Database oDb, Entity.Discharge.Frequency objFrequency, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_FREQUENCY);
                oDb.AddOutParameter(cmd, "@PK_FrequencyID", DbType.Int32, objFrequency.FrequencyID);
                oDb.AddInParameter(cmd, "@FrequencyName", DbType.String, objFrequency.FrequencyName);
                oDb.AddInParameter(cmd, "@FrequencyCode", DbType.String, objFrequency.FrequencyCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objFrequency.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objFrequency.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_FrequencyID"));
                    objFrequency.FrequencyID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Frequency AddFrequency | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateFrequency(Entity.Discharge.Frequency objFrequency)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateFrequency(oDb, objFrequency, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tFrequency", "PK_FrequencyID", objFrequency.FrequencyID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objFrequency.ModifiedBy.UserID);
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
        private static bool UpdateFrequency(Database oDb, Entity.Discharge.Frequency objFrequency, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_FREQUENCY);
                oDb.AddInParameter(cmd, "@PK_FrequencyID", DbType.Int32, objFrequency.FrequencyID);
                oDb.AddInParameter(cmd, "@FrequencyName", DbType.String, objFrequency.FrequencyName);
                oDb.AddInParameter(cmd, "@FrequencyCode", DbType.String, objFrequency.FrequencyCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objFrequency.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objFrequency.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Frequency UpdateFrequency | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteFrequency(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteFrequency(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tFrequency", "PK_FrequencyID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteFrequency(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_FREQUENCY);
                oDb.AddInParameter(cmd, "@PK_FrequencyID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Frequency DeleteFrequency | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static DataSet GetSearchFrequency(string sKey)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsTags = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_FREQUENCY);
                db.AddInParameter(cmd, "@key", DbType.String, sKey);
                dsTags = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "Frequency GetSearchFrequency | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsTags;
        }
    }
}
