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
    public class OtherProcedure
    {
        public static Collection<Entity.Discharge.OtherProcedure> GetOtherProcedure()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Discharge.OtherProcedure> objList = new Collection<Entity.Discharge.OtherProcedure>();
            Entity.Discharge.OtherProcedure objOtherProcedure = new Entity.Discharge.OtherProcedure();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_OTHERPROCEDURE);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objOtherProcedure = new Entity.Discharge.OtherProcedure();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objOtherProcedure.OtherProcedureID = Convert.ToInt32(drData["PK_OtherProcedureID"]);
                        objOtherProcedure.OtherProcedureName = Convert.ToString(drData["OtherProcedureName"]);
                        objOtherProcedure.OtherProcedureDescription = Convert.ToString(drData["OtherProcedureDescription"]);
                        objOtherProcedure.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objOtherProcedure);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.OtherProcedure GetOtherProcedure | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Discharge.OtherProcedure GetOtherProcedureByID(int iOtherProcedureID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Discharge.OtherProcedure objOtherProcedure = new Entity.Discharge.OtherProcedure();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_OTHERPROCEDURE);
                db.AddInParameter(cmd, "@PK_OtherProcedureID", DbType.Int32, iOtherProcedureID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objOtherProcedure = new Entity.Discharge.OtherProcedure();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objOtherProcedure.OtherProcedureID = Convert.ToInt32(drData["PK_OtherProcedureID"]);
                        objOtherProcedure.OtherProcedureName = Convert.ToString(drData["OtherProcedureName"]);
                        objOtherProcedure.OtherProcedureDescription = Convert.ToString(drData["OtherProcedureDescription"]);
                        objOtherProcedure.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.OtherProcedure GetOtherProcedureByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objOtherProcedure;
        }
        public static int AddOtherProcedure(Entity.Discharge.OtherProcedure objOtherProcedure)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddOtherProcedure(oDb, objOtherProcedure, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tOtherProcedure", "PK_OtherProcedureID", objOtherProcedure.OtherProcedureID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objOtherProcedure.CreatedBy.UserID);
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
        private static int AddOtherProcedure(Database oDb, Entity.Discharge.OtherProcedure objOtherProcedure, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_OTHERPROCEDURE);
                oDb.AddOutParameter(cmd, "@PK_OtherProcedureID", DbType.Int32, objOtherProcedure.OtherProcedureID);
                oDb.AddInParameter(cmd, "@OtherProcedureName", DbType.String, objOtherProcedure.OtherProcedureName);
                oDb.AddInParameter(cmd, "@OtherProcedureDescription", DbType.String, objOtherProcedure.OtherProcedureDescription);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objOtherProcedure.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objOtherProcedure.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_OtherProcedureID"));
                    objOtherProcedure.OtherProcedureID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.OtherProcedure AddOtherProcedure | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateOtherProcedure(Entity.Discharge.OtherProcedure objOtherProcedure)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateOtherProcedure(oDb, objOtherProcedure, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tOtherProcedure", "PK_OtherProcedureID", objOtherProcedure.OtherProcedureID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objOtherProcedure.ModifiedBy.UserID);
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
        private static bool UpdateOtherProcedure(Database oDb, Entity.Discharge.OtherProcedure objOtherProcedure, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_OTHERPROCEDURE);
                oDb.AddInParameter(cmd, "@PK_OtherProcedureID", DbType.Int32, objOtherProcedure.OtherProcedureID);
                oDb.AddInParameter(cmd, "@OtherProcedureName", DbType.String, objOtherProcedure.OtherProcedureName);
                oDb.AddInParameter(cmd, "@OtherProcedureDescription", DbType.String, objOtherProcedure.OtherProcedureDescription);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objOtherProcedure.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objOtherProcedure.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.OtherProcedure UpdateOtherProcedure | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteOtherProcedure(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteOtherProcedure(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tOtherProcedure", "PK_OtherProcedureID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteOtherProcedure(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_OTHERPROCEDURE);
                oDb.AddInParameter(cmd, "@PK_OtherProcedureID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.OtherProcedure DeleteOtherProcedure | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
