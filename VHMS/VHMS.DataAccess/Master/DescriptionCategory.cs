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
    public class DescriptionCategory
    {
        public static Collection<Entity.DescriptionCategory> GetDescriptionCategory()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.DescriptionCategory> objList = new Collection<Entity.DescriptionCategory>();
            Entity.DescriptionCategory objDescriptionCategory = new Entity.DescriptionCategory();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DESCRIPTIONCATEGORY);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDescriptionCategory = new Entity.DescriptionCategory();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objDescriptionCategory.DescriptionCategoryID = Convert.ToInt32(drData["PK_DescriptionCategoryID"]);
                        objDescriptionCategory.DescriptionCategoryName = Convert.ToString(drData["DescriptionCategoryName"]);
                        objDescriptionCategory.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objDescriptionCategory);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.DescriptionCategory GetDescriptionCategory | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.DescriptionCategory GetDescriptionCategoryByID(int iDescriptionCategoryID)
        {
            string sException = string.Empty;
            Database db;
            Entity.DescriptionCategory objDescriptionCategory = new Entity.DescriptionCategory();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DESCRIPTIONCATEGORY);
                db.AddInParameter(cmd, "@PK_DescriptionCategoryID", DbType.Int32, iDescriptionCategoryID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDescriptionCategory = new Entity.DescriptionCategory();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objDescriptionCategory.DescriptionCategoryID = Convert.ToInt32(drData["PK_DescriptionCategoryID"]);
                        objDescriptionCategory.DescriptionCategoryName = Convert.ToString(drData["DescriptionCategoryName"]);
                        objDescriptionCategory.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.DescriptionCategory GetDescriptionCategoryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objDescriptionCategory;
        }
        public static int AddDescriptionCategory(Entity.DescriptionCategory objDescriptionCategory)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddDescriptionCategory(oDb, objDescriptionCategory, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tDescriptionCategory", "PK_DescriptionCategoryID", objDescriptionCategory.DescriptionCategoryID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objDescriptionCategory.CreatedBy.UserID);
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
        private static int AddDescriptionCategory(Database oDb, Entity.DescriptionCategory objDescriptionCategory, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_DESCRIPTIONCATEGORY);
                oDb.AddOutParameter(cmd, "@PK_DescriptionCategoryID", DbType.Int32, objDescriptionCategory.DescriptionCategoryID);
                oDb.AddInParameter(cmd, "@DescriptionCategoryName", DbType.String, objDescriptionCategory.DescriptionCategoryName);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objDescriptionCategory.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objDescriptionCategory.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_DescriptionCategoryID"));
                    objDescriptionCategory.DescriptionCategoryID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.DescriptionCategory AddDescriptionCategory | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateDescriptionCategory(Entity.DescriptionCategory objDescriptionCategory)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateDescriptionCategory(oDb, objDescriptionCategory, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tDescriptionCategory", "PK_DescriptionCategoryID", objDescriptionCategory.DescriptionCategoryID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objDescriptionCategory.ModifiedBy.UserID);
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
        private static bool UpdateDescriptionCategory(Database oDb, Entity.DescriptionCategory objDescriptionCategory, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_DESCRIPTIONCATEGORY);
                oDb.AddInParameter(cmd, "@PK_DescriptionCategoryID", DbType.Int32, objDescriptionCategory.DescriptionCategoryID);
                oDb.AddInParameter(cmd, "@DescriptionCategoryName", DbType.String, objDescriptionCategory.DescriptionCategoryName);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objDescriptionCategory.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objDescriptionCategory.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.DescriptionCategory UpdateDescriptionCategory | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteDescriptionCategory(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteDescriptionCategory(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tDescriptionCategory", "PK_DescriptionCategoryID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteDescriptionCategory(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_DESCRIPTIONCATEGORY);
                oDb.AddInParameter(cmd, "@PK_DescriptionCategoryID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.DescriptionCategory DeleteDescriptionCategory | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
