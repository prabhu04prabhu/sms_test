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
    public class Branch
    {
        public static Collection<Entity.Branch> GetBranch(int iRegisterID=0, int iZonalID=0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Branch> objList = new Collection<Entity.Branch>();
            Entity.Branch objBranch = new Entity.Branch();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            Entity.Region objRegion;
            Entity.Zone objZone;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_BRANCH);
                db.AddInParameter(cmd, "@FK_ZoneID", DbType.Int32, iZonalID);
                db.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, iRegisterID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objBranch = new Entity.Branch();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();
                        objRegion = new Entity.Region();
                        objZone = new Entity.Zone();

                        objBranch.BranchID = Convert.ToInt32(drData["PK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objBranch.BranchCode = Convert.ToString(drData["BranchCode"]);
                        objBranch.Address = Convert.ToString(drData["Address"]);
                        objBranch.ContactPerson = Convert.ToString(drData["ContactPerson"]);
                        objBranch.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objBranch.Landline = Convert.ToString(drData["Landline"]);
                        objBranch.Email = Convert.ToString(drData["Email"]);
                        objBranch.Website = Convert.ToString(drData["Website"]);

                        objRegion.RegionID = Convert.ToInt32(drData["FK_RegionID"]);
                        objRegion.RegionName = Convert.ToString(drData["RegionName"]);
                        objRegion.RegionCode = Convert.ToString(drData["RegionCode"]);
                        objBranch.Region = objRegion;

                        objZone.ZoneID = Convert.ToInt32(drData["FK_ZoneID"]);
                        objZone.ZoneName = Convert.ToString(drData["ZoneName"]);
                        objZone.ZoneCode = Convert.ToString(drData["ZoneCode"]);
                        objBranch.Zone = objZone;

                        objBranch.HeadOfficeFlag = Convert.ToBoolean(drData["HeadOfficeFlag"]);
                        objBranch.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objBranch);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Branch GetBranch | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }



        public static Collection<Entity.Branch> GetMainBranch()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Branch> objList = new Collection<Entity.Branch>();
            Entity.Branch objBranch = new Entity.Branch();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            Entity.Region objRegion;
            Entity.Zone objZone;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_MAINBRANCH);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objBranch = new Entity.Branch();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();
                        objRegion = new Entity.Region();
                        objZone = new Entity.Zone();

                        objBranch.BranchID = Convert.ToInt32(drData["PK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objBranch.BranchCode = Convert.ToString(drData["BranchCode"]);
                        objBranch.Address = Convert.ToString(drData["Address"]);
                        objBranch.ContactPerson = Convert.ToString(drData["ContactPerson"]);
                        objBranch.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objBranch.Landline = Convert.ToString(drData["Landline"]);
                        objBranch.Email = Convert.ToString(drData["Email"]);
                        objBranch.Website = Convert.ToString(drData["Website"]);
                        objBranch.HeadOfficeFlag = Convert.ToBoolean(drData["HeadOfficeFlag"]);
                        objBranch.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objRegion.RegionID = Convert.ToInt32(drData["FK_RegionID"]);
                        objRegion.RegionName = Convert.ToString(drData["RegionName"]);
                        objRegion.RegionCode = Convert.ToString(drData["RegionCode"]);
                        objBranch.Region = objRegion;

                        objZone.ZoneID = Convert.ToInt32(drData["FK_ZoneID"]);
                        objZone.ZoneName = Convert.ToString(drData["ZoneName"]);
                        objZone.ZoneCode = Convert.ToString(drData["ZoneCode"]);
                        objBranch.Zone = objZone;

                        objList.Add(objBranch);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Branch GetBranch | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Branch GetBranchByID(int iBranchID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Branch objBranch = new Entity.Branch();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            Entity.Region objRegion;
            Entity.Zone objZone;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_BRANCH);
                db.AddInParameter(cmd, "@PK_BranchID", DbType.Int32, iBranchID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objBranch = new Entity.Branch();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();
                        objRegion = new Entity.Region();
                        objZone = new Entity.Zone();

                        objBranch.BranchID = Convert.ToInt32(drData["PK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objBranch.BranchCode = Convert.ToString(drData["BranchCode"]);
                        objBranch.Address = Convert.ToString(drData["Address"]);
                        objBranch.ContactPerson = Convert.ToString(drData["ContactPerson"]);
                        objBranch.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objBranch.Landline = Convert.ToString(drData["Landline"]);
                        objBranch.Email = Convert.ToString(drData["Email"]);
                        objBranch.Website = Convert.ToString(drData["Website"]);
                        objBranch.HeadOfficeFlag = Convert.ToBoolean(drData["HeadOfficeFlag"]);
                        objBranch.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objRegion.RegionID = Convert.ToInt32(drData["FK_RegionID"]);
                        objRegion.RegionName = Convert.ToString(drData["RegionName"]);
                        objRegion.RegionCode = Convert.ToString(drData["RegionCode"]);
                        objBranch.Region = objRegion;

                        objZone.ZoneID = Convert.ToInt32(drData["FK_ZoneID"]);
                        objZone.ZoneName = Convert.ToString(drData["ZoneName"]);
                        objZone.ZoneCode = Convert.ToString(drData["ZoneCode"]);
                        objBranch.Zone = objZone;
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Branch GetBranchByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objBranch;
        }
        public static int AddBranch(Entity.Branch objBranch)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddBranch(oDb, objBranch, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tBranch", "PK_BranchID", objBranch.BranchID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objBranch.CreatedBy.UserID);
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
        private static int AddBranch(Database oDb, Entity.Branch objBranch, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_BRANCH);
                oDb.AddOutParameter(cmd, "@PK_BranchID", DbType.Int32, objBranch.BranchID);
                oDb.AddInParameter(cmd, "@BranchName", DbType.String, objBranch.BranchName);
                oDb.AddInParameter(cmd, "@BranchCode", DbType.String, objBranch.BranchCode);
                oDb.AddInParameter(cmd, "@Address", DbType.String, objBranch.Address);
                oDb.AddInParameter(cmd, "@MobileNo", DbType.String, objBranch.MobileNo);
                oDb.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, objBranch.Region.RegionID);
                oDb.AddInParameter(cmd, "@FK_ZoneID", DbType.Int32, objBranch.Zone.ZoneID);
                oDb.AddInParameter(cmd, "@ContactPerson", DbType.String, objBranch.ContactPerson);
                oDb.AddInParameter(cmd, "@Landline", DbType.String, objBranch.Landline);
                oDb.AddInParameter(cmd, "@Email", DbType.String, objBranch.Email);
                oDb.AddInParameter(cmd, "@Website", DbType.String, objBranch.Website);
                oDb.AddInParameter(cmd, "@HeadOfficeFlag", DbType.Boolean, objBranch.HeadOfficeFlag);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objBranch.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objBranch.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_BranchID"));
                    objBranch.BranchID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Branch AddBranch | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateBranch(Entity.Branch objBranch)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateBranch(oDb, objBranch, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tBranch", "PK_BranchID", objBranch.BranchID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objBranch.ModifiedBy.UserID);
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
        private static bool UpdateBranch(Database oDb, Entity.Branch objBranch, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_BRANCH);
                oDb.AddInParameter(cmd, "@PK_BranchID", DbType.Int32, objBranch.BranchID);
                oDb.AddInParameter(cmd, "@BranchName", DbType.String, objBranch.BranchName);
                oDb.AddInParameter(cmd, "@BranchCode", DbType.String, objBranch.BranchCode);
                oDb.AddInParameter(cmd, "@Address", DbType.String, objBranch.Address);
                oDb.AddInParameter(cmd, "@MobileNo", DbType.String, objBranch.MobileNo);
                oDb.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, objBranch.Region.RegionID);
                oDb.AddInParameter(cmd, "@FK_ZoneID", DbType.Int32, objBranch.Zone.ZoneID);
                oDb.AddInParameter(cmd, "@ContactPerson", DbType.String, objBranch.ContactPerson);
                oDb.AddInParameter(cmd, "@Landline", DbType.String, objBranch.Landline);
                oDb.AddInParameter(cmd, "@Email", DbType.String, objBranch.Email);
                oDb.AddInParameter(cmd, "@Website", DbType.String, objBranch.Website);
                oDb.AddInParameter(cmd, "@HeadOfficeFlag", DbType.Boolean, objBranch.HeadOfficeFlag);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objBranch.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objBranch.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Branch UpdateBranch | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteBranch(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteBranch(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tBranch", "PK_BranchID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteBranch(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_BRANCH);
                oDb.AddInParameter(cmd, "@PK_BranchID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Branch DeleteBranch | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
