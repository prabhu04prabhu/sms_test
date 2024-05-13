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
    public class LedgerType
    {
        public static Collection<Entity.LedgerType> GetLedgerType()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.LedgerType> objList = new Collection<Entity.LedgerType>();
            Entity.LedgerType objLedgerType = new Entity.LedgerType();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_LEDGERTYPE);
                dsList = db.ExecuteDataSet(cmd);
               
                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objLedgerType = new Entity.LedgerType();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objLedgerType.LedgerTypeID = Convert.ToInt32(drData["PK_LedgerTypeID"]);
                        objLedgerType.LedgerTypeName = Convert.ToString(drData["LedgerTypeName"]);
                        objLedgerType.RecordType = Convert.ToString(drData["RecordType"]);
                        objLedgerType.IsSubGroup = Convert.ToBoolean(drData["IsSubGroup"]);
                        objLedgerType.IsDefaultRecord = Convert.ToBoolean(drData["IsDefaultRecord"]);
                        objLedgerType.PrimaryGroupID = Convert.ToInt32(drData["PrimaryGroupID"]);
                        objLedgerType.PrimaryGroupName = Convert.ToString(drData["PrimaryGroupName"]);
                        objLedgerType.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objLedgerType);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.LedgerType GetLedgerType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.LedgerType GetLedgerTypeByID(int iLedgerTypeID)
        {
            string sException = string.Empty;
            Database db;
            Entity.LedgerType objLedgerType = new Entity.LedgerType();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_LEDGERTYPE);
                db.AddInParameter(cmd, "@PK_LedgerTypeID", DbType.Int32, iLedgerTypeID);
                DataSet dsList = db.ExecuteDataSet(cmd);
               
                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objLedgerType = new Entity.LedgerType();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objLedgerType.LedgerTypeID = Convert.ToInt32(drData["PK_LedgerTypeID"]);
                        objLedgerType.LedgerTypeName = Convert.ToString(drData["LedgerTypeName"]);
                        objLedgerType.RecordType = Convert.ToString(drData["RecordType"]);
                        objLedgerType.IsSubGroup = Convert.ToBoolean(drData["IsSubGroup"]);
                        objLedgerType.IsDefaultRecord = Convert.ToBoolean(drData["IsDefaultRecord"]);
                        objLedgerType.PrimaryGroupID = Convert.ToInt32(drData["PrimaryGroupID"]);
                        objLedgerType.PrimaryGroupName = Convert.ToString(drData["PrimaryGroupName"]);
                        objLedgerType.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.LedgerType GetLedgerTypeByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objLedgerType;
        }

        public static Collection<Entity.LedgerType> GetLedgerTypeByCategoryID(int iLedgerTypeID)
        {
            string sException = string.Empty;
            Database db;
            Collection<Entity.LedgerType> objList = new Collection<Entity.LedgerType>();
            Entity.LedgerType objLedgerType = new Entity.LedgerType();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_LEDGERTYPE);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, iLedgerTypeID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objLedgerType = new Entity.LedgerType();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objLedgerType.LedgerTypeName = Convert.ToString(drData["LedgerTypeName"]);
                        objLedgerType.RecordType = Convert.ToString(drData["RecordType"]);
                        objLedgerType.IsSubGroup = Convert.ToBoolean(drData["IsSubGroup"]);
                        objLedgerType.IsDefaultRecord = Convert.ToBoolean(drData["IsDefaultRecord"]);
                        objLedgerType.PrimaryGroupID = Convert.ToInt32(drData["PrimaryGroupID"]);
                        objLedgerType.PrimaryGroupName = Convert.ToString(drData["PrimaryGroupName"]);
                        objLedgerType.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objLedgerType);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.LedgerType GetLedgerTypeByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static int AddLedgerType(Entity.LedgerType objLedgerType)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddLedgerType(oDb, objLedgerType, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tLedgerType", "PK_LedgerTypeID", objLedgerType.LedgerTypeID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objLedgerType.CreatedBy.UserID);
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
        private static int AddLedgerType(Database oDb, Entity.LedgerType objLedgerType, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_LEDGERTYPE);
                oDb.AddOutParameter(cmd, "@PK_LedgerTypeID", DbType.Int32, objLedgerType.LedgerTypeID);
                oDb.AddInParameter(cmd, "@LedgerTypeName", DbType.String, objLedgerType.LedgerTypeName);
                oDb.AddInParameter(cmd, "@RecordType", DbType.String, objLedgerType.RecordType);
                oDb.AddInParameter(cmd, "@IsDefaultRecord", DbType.Boolean, objLedgerType.IsDefaultRecord);
                oDb.AddInParameter(cmd, "@IsSubGroup", DbType.Boolean, objLedgerType.IsSubGroup);
                oDb.AddInParameter(cmd, "@PrimaryGroupID", DbType.Int32, objLedgerType.PrimaryGroupID);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objLedgerType.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objLedgerType.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_LedgerTypeID"));
                    objLedgerType.LedgerTypeID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.LedgerType AddLedgerType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateLedgerType(Entity.LedgerType objLedgerType)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateLedgerType(oDb, objLedgerType, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tLedgerType", "PK_LedgerTypeID", objLedgerType.LedgerTypeID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objLedgerType.ModifiedBy.UserID);
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
        private static bool UpdateLedgerType(Database oDb, Entity.LedgerType objLedgerType, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_LEDGERTYPE);
                oDb.AddInParameter(cmd, "@PK_LedgerTypeID", DbType.Int32, objLedgerType.LedgerTypeID);
                oDb.AddInParameter(cmd, "@LedgerTypeName", DbType.String, objLedgerType.LedgerTypeName);
                oDb.AddInParameter(cmd, "@RecordType", DbType.String, objLedgerType.RecordType);
                oDb.AddInParameter(cmd, "@IsDefaultRecord", DbType.Boolean, objLedgerType.IsDefaultRecord);
                oDb.AddInParameter(cmd, "@IsSubGroup", DbType.Boolean, objLedgerType.IsSubGroup);
                oDb.AddInParameter(cmd, "@PrimaryGroupID", DbType.Int32, objLedgerType.PrimaryGroupID);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objLedgerType.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objLedgerType.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.LedgerType UpdateLedgerType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteLedgerType(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteLedgerType(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tLedgerType", "PK_LedgerTypeID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteLedgerType(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_LEDGERTYPE);
                oDb.AddInParameter(cmd, "@PK_LedgerTypeID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.LedgerType DeleteLedgerType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
