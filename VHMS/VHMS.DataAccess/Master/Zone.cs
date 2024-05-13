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
    public class Zone
    {
        public static Collection<Entity.Zone> GetZone(int iZoneid=0, int iRegionid = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Zone> objList = new Collection<Entity.Zone>();
            Entity.Zone objZone = new Entity.Zone();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            Entity.Region objRegion;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ZONE);
                db.AddInParameter(cmd, "@PK_ZoneID", DbType.Int32, iZoneid);
                db.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, iRegionid);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objZone = new Entity.Zone();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();
                        objRegion = new Entity.Region();

                        objZone.ZoneID = Convert.ToInt32(drData["PK_ZoneID"]);
                        objZone.ZoneName = Convert.ToString(drData["ZoneName"]);
                        objZone.ZoneCode = Convert.ToString(drData["ZoneCode"]);

                        objRegion.RegionID = Convert.ToInt32(drData["FK_RegionID"]);
                        objRegion.RegionName = Convert.ToString(drData["RegionName"]);
                        objRegion.RegionCode = Convert.ToString(drData["RegionCode"]);
                        objZone.Region = objRegion;
                        objZone.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objList.Add(objZone);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Zone GetZone | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Zone GetZoneByID(int iZoneID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Zone objZone = new Entity.Zone();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            Entity.Region objRegion;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ZONE);
                db.AddInParameter(cmd, "@PK_ZoneID", DbType.Int32, iZoneID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objZone = new Entity.Zone();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();
                        objRegion = new Entity.Region();

                        objZone.ZoneID = Convert.ToInt32(drData["PK_ZoneID"]);
                        objZone.ZoneName = Convert.ToString(drData["ZoneName"]);
                        objZone.ZoneCode = Convert.ToString(drData["ZoneCode"]);
                        objRegion.RegionID = Convert.ToInt32(drData["FK_RegionID"]);
                        objRegion.RegionName = Convert.ToString(drData["RegionName"]);
                        objRegion.RegionCode = Convert.ToString(drData["RegionCode"]);
                        objZone.Region = objRegion;
                        objZone.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Zone GetZoneByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objZone;
        }
        public static int AddZone(Entity.Zone objZone)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddZone(oDb, objZone, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tZone", "PK_ZoneID", objZone.ZoneID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objZone.CreatedBy.UserID);
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
        private static int AddZone(Database oDb, Entity.Zone objZone, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_ZONE);
                oDb.AddOutParameter(cmd, "@PK_ZoneID", DbType.Int32, objZone.ZoneID);
                oDb.AddInParameter(cmd, "@ZoneName", DbType.String, objZone.ZoneName);
                oDb.AddInParameter(cmd, "@ZoneCode", DbType.String, objZone.ZoneCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objZone.IsActive);
                oDb.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, objZone.Region.RegionID);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objZone.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_ZoneID"));
                    objZone.ZoneID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Zone AddZone | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateZone(Entity.Zone objZone)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateZone(oDb, objZone, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tZone", "PK_ZoneID", objZone.ZoneID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objZone.ModifiedBy.UserID);
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
        private static bool UpdateZone(Database oDb, Entity.Zone objZone, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_ZONE);
                oDb.AddInParameter(cmd, "@PK_ZoneID", DbType.Int32, objZone.ZoneID);
                oDb.AddInParameter(cmd, "@ZoneName", DbType.String, objZone.ZoneName);
                oDb.AddInParameter(cmd, "@ZoneCode", DbType.String, objZone.ZoneCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objZone.IsActive);
                oDb.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, objZone.Region.RegionID);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objZone.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Zone UpdateZone | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteZone(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteZone(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tZone", "PK_ZoneID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteZone(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_ZONE);
                oDb.AddInParameter(cmd, "@PK_ZoneID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Zone DeleteZone | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
