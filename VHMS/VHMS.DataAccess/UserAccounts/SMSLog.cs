using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections.ObjectModel;
using VHMS;

namespace VHMS.DataAccess
{
    public class SMSLog
    {
        //public static Collection<Entity.SMSLog> GetSMSLog(int CountryID = 0)
        //{
        //    string sException = string.Empty;
        //    Database db;
        //    DataSet dsList = null;
        //    Collection<Entity.SMSLog> objList = new Collection<Entity.SMSLog>();
        //    Entity.SMSLog objSMSLog = new Entity.SMSLog();
        //    Entity.Country objCountry = null;
        //    Entity.User objCreatedUser;
        //    Entity.User objModifiedUser;

        //    try
        //    {
        //        db = Entity.DBConnection.dbCon;
        //        DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_STATE);
        //        db.AddInParameter(cmd, "@FK_CountryID", DbType.Int32, CountryID);
        //        dsList = db.ExecuteDataSet(cmd);

        //        if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow drData in dsList.Tables[0].Rows)
        //            {
        //                objSMSLog = new Entity.SMSLog();
        //                objCountry = new Entity.Country();
        //                objCreatedUser = new Entity.User();
        //                objModifiedUser = new Entity.User();

        //                objCountry.CountryID = Convert.ToInt32(drData["FK_CountryID"]);
        //                objCountry.CountryName = Convert.ToString(drData["CountryName"]);
        //                objSMSLog.Country = objCountry;

        //                objSMSLog.SMSLogID = Convert.ToInt32(drData["PK_SMSLogID"]);
        //                objSMSLog.SMSLogName = Convert.ToString(drData["SMSLogName"]);
        //                objSMSLog.SMSLogCode = Convert.ToString(drData["SMSLogCode"]);
        //                objSMSLog.IsActive = Convert.ToBoolean(drData["IsActive"]);

        //                objList.Add(objSMSLog);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "VHMS.DataAccess.SMSLog GetSMSLog | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return objList;
        //}
        //public static Entity.SMSLog GetSMSLogByID(int iSMSLogID)
        //{
        //    string sException = string.Empty;
        //    Database db;
        //    Entity.SMSLog objSMSLog = new Entity.SMSLog();
        //    Entity.User objCreatedUser;
        //    Entity.User objModifiedUser;
        //    Entity.Country objCountry = null;

        //    try
        //    {
        //        db = Entity.DBConnection.dbCon;
        //        DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_STATE);
        //        db.AddInParameter(cmd, "@PK_SMSLogID", DbType.Int32, iSMSLogID);
        //        DataSet dsList = db.ExecuteDataSet(cmd);

        //        if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow drData in dsList.Tables[0].Rows)
        //            {
        //                objSMSLog = new Entity.SMSLog();
        //                objCountry = new Entity.Country();
        //                objCreatedUser = new Entity.User();
        //                objModifiedUser = new Entity.User();

        //                objCountry.CountryID = Convert.ToInt32(drData["FK_CountryID"]);
        //                objCountry.CountryName = Convert.ToString(drData["CountryName"]);
        //                objSMSLog.Country = objCountry;

        //                objSMSLog.SMSLogID = Convert.ToInt32(drData["PK_SMSLogID"]);
        //                objSMSLog.SMSLogName = Convert.ToString(drData["SMSLogName"]);
        //                objSMSLog.SMSLogCode = Convert.ToString(drData["SMSLogCode"]);
        //                objSMSLog.IsActive = Convert.ToBoolean(drData["IsActive"]);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "VHMS.DataAccess.SMSLog GetSMSLogByID | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return objSMSLog;
        //}
        public static int AddSMSLog(Entity.SMSLog objSMSLog)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddSMSLog(oDb, objSMSLog, oTrans);
                    oTrans.Commit();
                    //if (ID > 0)
                    //    Framework.InsertAuditLog("tSMSLog", "PK_SMSLogID", objSMSLog.SMSLogID.ToString(), (char)Entity.Common.DatabaseAction.INSERT,1);
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
        private static int AddSMSLog(Database oDb, Entity.SMSLog objSMSLog, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_SMSLOG);
                oDb.AddOutParameter(cmd, "@PK_SMSLogID", DbType.Int32, objSMSLog.SMSLogID);
                oDb.AddInParameter(cmd, "@Message", DbType.String, objSMSLog.Message);
                oDb.AddInParameter(cmd, "@SendTo", DbType.String, objSMSLog.SendTo);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objSMSLog.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_SMSLogID"));
                    objSMSLog.SMSLogID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.SMSLog AddSMSLog | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        //public static bool UpdateSMSLog(Entity.SMSLog objSMSLog)
        //{
        //    bool IsUpdated = true;
        //    Database oDb = Entity.DBConnection.dbCon;
        //    using (DbConnection oDbCon = oDb.CreateConnection())
        //    {
        //        oDbCon.Open();
        //        DbTransaction oTrans = oDbCon.BeginTransaction();
        //        try
        //        {
        //            IsUpdated = UpdateSMSLog(oDb, objSMSLog, oTrans);
        //            oTrans.Commit();
        //            if (IsUpdated) Framework.InsertAuditLog("tSMSLog", "PK_SMSLogID", objSMSLog.SMSLogID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objSMSLog.ModifiedBy.UserID);
        //        }
        //        catch (Exception ex)
        //        {
        //            oTrans.Rollback();
        //            throw ex;
        //        }
        //        finally
        //        {
        //            oDbCon.Close();
        //        }
        //    }
        //    return IsUpdated;
        //}
        //private static bool UpdateSMSLog(Database oDb, Entity.SMSLog objSMSLog, DbTransaction oTrans)
        //{
        //    string sException = string.Empty;
        //    int iID = 0;
        //    bool bResult = false;
        //    try
        //    {
        //        DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_STATE);
        //        oDb.AddInParameter(cmd, "@PK_SMSLogID", DbType.Int32, objSMSLog.SMSLogID);
        //        oDb.AddInParameter(cmd, "@FK_CountryID", DbType.String, objSMSLog.Country.CountryID);
        //        oDb.AddInParameter(cmd, "@SMSLogName", DbType.String, objSMSLog.SMSLogName);
        //        oDb.AddInParameter(cmd, "@SMSLogCode", DbType.String, objSMSLog.SMSLogCode);
        //        oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objSMSLog.IsActive);
        //        oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objSMSLog.ModifiedBy.UserID);

        //        iID = oDb.ExecuteNonQuery(cmd);
        //        if (iID != 0) bResult = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "VHMS.DataAccess.SMSLog UpdateSMSLog | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return bResult;
        //}
        //public static bool DeleteSMSLog(int ID, int UserID)
        //{
        //    bool IsDeleted = false;
        //    Database oDb = Entity.DBConnection.dbCon;
        //    using (DbConnection oDbCon = oDb.CreateConnection())
        //    {
        //        oDbCon.Open();
        //        DbTransaction oTrans = oDbCon.BeginTransaction();
        //        try
        //        {
        //            IsDeleted = DeleteSMSLog(oDb, ID, UserID, oTrans);
        //            oTrans.Commit();

        //            if (IsDeleted) Framework.InsertAuditLog("tSMSLog", "PK_SMSLogID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
        //        }
        //        catch (Exception ex)
        //        {
        //            oTrans.Rollback();
        //            throw ex;
        //        }
        //        finally
        //        {
        //            oDbCon.Close();
        //        }
        //    }
        //    return IsDeleted;
        //}
        //private static bool DeleteSMSLog(Database oDb, int ID, int UserID, DbTransaction oTrans)
        //{
        //    string sException = string.Empty;
        //    int iRemoveId = 0;
        //    bool bResult = false;
        //    try
        //    {
        //        DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_STATE);
        //        oDb.AddInParameter(cmd, "@PK_SMSLogID", DbType.Int32, ID);
        //        iRemoveId = oDb.ExecuteNonQuery(cmd);
        //        if (iRemoveId != 0) bResult = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "VHMS.DataAccess.SMSLog DeleteSMSLog | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return bResult;
        //}
    }
}
