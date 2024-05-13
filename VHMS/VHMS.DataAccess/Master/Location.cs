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
    public class Location
    {
        public static Collection<Entity.Location> GetLocation()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Location> objList = new Collection<Entity.Location>();
            Entity.Location objLocation = new Entity.Location();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_LOCATION);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objLocation = new Entity.Location();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objLocation.LocationID = Convert.ToInt32(drData["PK_LocationID"]);
                        objLocation.LocationName = Convert.ToString(drData["LocationName"]);
                        objLocation.LocationCode = Convert.ToString(drData["LocationCode"]);
                        objLocation.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objLocation);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Location GetLocation | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Location GetLocationByID(int iLocationID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Location objLocation = new Entity.Location();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_LOCATION);
                db.AddInParameter(cmd, "@PK_LocationID", DbType.Int32, iLocationID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objLocation = new Entity.Location();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objLocation.LocationID = Convert.ToInt32(drData["PK_LocationID"]);
                        objLocation.LocationName = Convert.ToString(drData["LocationName"]);
                        objLocation.LocationCode = Convert.ToString(drData["LocationCode"]);
                        objLocation.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Location GetLocationByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objLocation;
        }
        public static int AddLocation(Entity.Location objLocation)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddLocation(oDb, objLocation, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tLocation", "PK_LocationID", objLocation.LocationID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objLocation.CreatedBy.UserID);
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
        private static int AddLocation(Database oDb, Entity.Location objLocation, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_LOCATION);
                oDb.AddOutParameter(cmd, "@PK_LocationID", DbType.Int32, objLocation.LocationID);
                oDb.AddInParameter(cmd, "@LocationName", DbType.String, objLocation.LocationName);
                oDb.AddInParameter(cmd, "@LocationCode", DbType.String, objLocation.LocationCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objLocation.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objLocation.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_LocationID"));
                    objLocation.LocationID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Location AddLocation | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateLocation(Entity.Location objLocation)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateLocation(oDb, objLocation, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tLocation", "PK_LocationID", objLocation.LocationID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objLocation.ModifiedBy.UserID);
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
        private static bool UpdateLocation(Database oDb, Entity.Location objLocation, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_LOCATION);
                oDb.AddInParameter(cmd, "@PK_LocationID", DbType.Int32, objLocation.LocationID);
                oDb.AddInParameter(cmd, "@LocationName", DbType.String, objLocation.LocationName);
                oDb.AddInParameter(cmd, "@LocationCode", DbType.String, objLocation.LocationCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objLocation.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objLocation.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Location UpdateLocation | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteLocation(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteLocation(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tLocation", "PK_LocationID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteLocation(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_LOCATION);
                oDb.AddInParameter(cmd, "@PK_LocationID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Location DeleteLocation | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
