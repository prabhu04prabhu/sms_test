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
    public class WareHouse
    {
        public static Collection<Entity.Billing.WareHouse> GetWareHouse()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.WareHouse> objList = new Collection<Entity.Billing.WareHouse>();
            Entity.Billing.WareHouse objWareHouse = new Entity.Billing.WareHouse();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_WAREHOUSE);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objWareHouse = new Entity.Billing.WareHouse();

                        objWareHouse.WareHouseID = Convert.ToInt32(drData["PK_WareHouseID"]);
                        objWareHouse.WareHouseName = Convert.ToString(drData["WareHouseName"]);
                        objWareHouse.WareHouseCode = Convert.ToString(drData["WareHouseCode"]);
                        objWareHouse.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objWareHouse);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.WareHouse GetWareHouse | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.WareHouse GetWareHouseByID(int iWareHouseID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.WareHouse objWareHouse = new Entity.Billing.WareHouse();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_WAREHOUSE);
                db.AddInParameter(cmd, "@PK_WareHouseID", DbType.Int32, iWareHouseID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objWareHouse = new Entity.Billing.WareHouse();
                        objWareHouse.WareHouseID = Convert.ToInt32(drData["PK_WareHouseID"]);
                        objWareHouse.WareHouseName = Convert.ToString(drData["WareHouseName"]);
                        objWareHouse.WareHouseCode = Convert.ToString(drData["WareHouseCode"]);
                        objWareHouse.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.WareHouse GetWareHouseByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objWareHouse;
        }
        public static int AddWareHouse(Entity.Billing.WareHouse objWareHouse)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddWareHouse(oDb, objWareHouse, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tWareHouse", "PK_WareHouseID", objWareHouse.WareHouseID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objWareHouse.CreatedBy.UserID);
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
        private static int AddWareHouse(Database oDb, Entity.Billing.WareHouse objWareHouse, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_WAREHOUSE);
                oDb.AddOutParameter(cmd, "@PK_WareHouseID", DbType.Int32, objWareHouse.WareHouseID);
                oDb.AddInParameter(cmd, "@WareHouseName", DbType.String, objWareHouse.WareHouseName);
                oDb.AddInParameter(cmd, "@WareHouseCode", DbType.String, objWareHouse.WareHouseCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objWareHouse.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objWareHouse.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_WareHouseID"));
                    objWareHouse.WareHouseID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.WareHouse AddWareHouse | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateWareHouse(Entity.Billing.WareHouse objWareHouse)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateWareHouse(oDb, objWareHouse, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tWareHouse", "PK_WareHouseID", objWareHouse.WareHouseID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objWareHouse.ModifiedBy.UserID);
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
        private static bool UpdateWareHouse(Database oDb, Entity.Billing.WareHouse objWareHouse, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_WAREHOUSE);
                oDb.AddInParameter(cmd, "@PK_WareHouseID", DbType.Int32, objWareHouse.WareHouseID);
                oDb.AddInParameter(cmd, "@WareHouseName", DbType.String, objWareHouse.WareHouseName);
                oDb.AddInParameter(cmd, "@WareHouseCode", DbType.String, objWareHouse.WareHouseCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objWareHouse.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objWareHouse.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.WareHouse UpdateWareHouse | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteWareHouse(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteWareHouse(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tWareHouse", "PK_WareHouseID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteWareHouse(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_WAREHOUSE);
                oDb.AddInParameter(cmd, "@PK_WareHouseID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.WareHouse DeleteWareHouse | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
