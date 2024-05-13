using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections.ObjectModel;
using VHMS;

namespace VHMS.DataAccess.Billing
{
    public class HSNMaster
    {
        public static Collection<Entity.Billing.HSNMaster> GetHSNMaster()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.HSNMaster> objList = new Collection<Entity.Billing.HSNMaster>();
            Entity.Billing.HSNMaster objHSNMaster = new Entity.Billing.HSNMaster();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_HSNMASTER);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objHSNMaster = new Entity.Billing.HSNMaster();

                        objHSNMaster.HSNMasterID = Convert.ToInt32(drData["PK_HSNMasterID"]);
                        objHSNMaster.HSNCode = Convert.ToString(drData["HSNCode"]);
                        objHSNMaster.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objHSNMaster);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.HSNMaster GetHSNMaster | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.HSNMaster> GetActiveHSNMaster()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.HSNMaster> objList = new Collection<Entity.Billing.HSNMaster>();
            Entity.Billing.HSNMaster objHSNMaster = new Entity.Billing.HSNMaster();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_HSNMASTER);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        if (Convert.ToBoolean(drData["IsActive"]))
                        {
                            objHSNMaster = new Entity.Billing.HSNMaster();

                            objHSNMaster.HSNMasterID = Convert.ToInt32(drData["PK_HSNMasterID"]);
                            objHSNMaster.HSNCode = Convert.ToString(drData["HSNCode"]);
                            objHSNMaster.IsActive = Convert.ToBoolean(drData["IsActive"]);

                            objList.Add(objHSNMaster);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.HSNMaster GetHSNMaster | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.HSNMaster GetHSNMasterByID(int iHSNMasterID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.HSNMaster objHSNMaster = new Entity.Billing.HSNMaster();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_HSNMASTER);
                db.AddInParameter(cmd, "@PK_HSNMasterID", DbType.Int32, iHSNMasterID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objHSNMaster = new Entity.Billing.HSNMaster();
                        objHSNMaster.HSNMasterID = Convert.ToInt32(drData["PK_HSNMasterID"]);
                        objHSNMaster.HSNCode = Convert.ToString(drData["HSNCode"]);
                        objHSNMaster.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.HSNMaster GetHSNMasterByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objHSNMaster;
        }
        public static int AddHSNMaster(Entity.Billing.HSNMaster objHSNMaster)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddHSNMaster(oDb, objHSNMaster, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tHSNMaster", "PK_HSNMasterID", objHSNMaster.HSNMasterID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objHSNMaster.CreatedBy.UserID);
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
        private static int AddHSNMaster(Database oDb, Entity.Billing.HSNMaster objHSNMaster, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_HSNMASTER);
                oDb.AddOutParameter(cmd, "@PK_HSNMasterID", DbType.Int32, objHSNMaster.HSNMasterID);
                oDb.AddInParameter(cmd, "@HSNCode", DbType.String, objHSNMaster.HSNCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objHSNMaster.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objHSNMaster.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_HSNMasterID"));
                    objHSNMaster.HSNMasterID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.HSNMaster AddHSNMaster | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateHSNMaster(Entity.Billing.HSNMaster objHSNMaster)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateHSNMaster(oDb, objHSNMaster, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tHSNMaster", "PK_HSNMasterID", objHSNMaster.HSNMasterID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objHSNMaster.ModifiedBy.UserID);
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
        private static bool UpdateHSNMaster(Database oDb, Entity.Billing.HSNMaster objHSNMaster, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_HSNMASTER);
                oDb.AddInParameter(cmd, "@PK_HSNMasterID", DbType.Int32, objHSNMaster.HSNMasterID);
                oDb.AddInParameter(cmd, "@HSNCode", DbType.String, objHSNMaster.HSNCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objHSNMaster.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objHSNMaster.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.HSNMaster UpdateHSNMaster | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteHSNMaster(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteHSNMaster(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tHSNMaster", "PK_HSNMasterID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteHSNMaster(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_HSNMASTER);
                oDb.AddInParameter(cmd, "@PK_HSNMasterID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.HSNMaster DeleteHSNMaster | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
