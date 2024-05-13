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
    public class Region
    {
        public static Collection<Entity.Region> GetRegion()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Region> objList = new Collection<Entity.Region>();
            Entity.Region objRegion = new Entity.Region();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_REGION);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRegion = new Entity.Region();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objRegion.RegionID = Convert.ToInt32(drData["PK_RegionID"]);
                        objRegion.RegionName = Convert.ToString(drData["RegionName"]);
                        objRegion.RegionCode = Convert.ToString(drData["RegionCode"]);
                        objRegion.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objList.Add(objRegion);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Region GetRegion | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Region GetRegionByID(int iRegionID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Region objRegion = new Entity.Region();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_REGION);
                db.AddInParameter(cmd, "@PK_RegionID", DbType.Int32, iRegionID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRegion = new Entity.Region();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objRegion.RegionID = Convert.ToInt32(drData["PK_RegionID"]);
                        objRegion.RegionName = Convert.ToString(drData["RegionName"]);
                        objRegion.RegionCode = Convert.ToString(drData["RegionCode"]);
                        objRegion.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Region GetRegionByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objRegion;
        }
        public static int AddRegion(Entity.Region objRegion)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddRegion(oDb, objRegion, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tRegion", "PK_RegionID", objRegion.RegionID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objRegion.CreatedBy.UserID);
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
        private static int AddRegion(Database oDb, Entity.Region objRegion, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_REGION);
                oDb.AddOutParameter(cmd, "@PK_RegionID", DbType.Int32, objRegion.RegionID);
                oDb.AddInParameter(cmd, "@RegionName", DbType.String, objRegion.RegionName);
                oDb.AddInParameter(cmd, "@RegionCode", DbType.String, objRegion.RegionCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objRegion.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objRegion.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_RegionID"));
                    objRegion.RegionID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Region AddRegion | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateRegion(Entity.Region objRegion)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateRegion(oDb, objRegion, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tRegion", "PK_RegionID", objRegion.RegionID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objRegion.ModifiedBy.UserID);
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
        private static bool UpdateRegion(Database oDb, Entity.Region objRegion, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_REGION);
                oDb.AddInParameter(cmd, "@PK_RegionID", DbType.Int32, objRegion.RegionID);
                oDb.AddInParameter(cmd, "@RegionName", DbType.String, objRegion.RegionName);
                oDb.AddInParameter(cmd, "@RegionCode", DbType.String, objRegion.RegionCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objRegion.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objRegion.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Region UpdateRegion | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteRegion(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteRegion(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tRegion", "PK_RegionID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteRegion(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_REGION);
                oDb.AddInParameter(cmd, "@PK_RegionID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Region DeleteRegion | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
