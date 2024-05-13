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
    public class Category
    {
        public static Collection<Entity.Billing.Category> GetCategory()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Category> objList = new Collection<Entity.Billing.Category>();
            Entity.Billing.Category objCategory = new Entity.Billing.Category();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CATEGORY);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCategory = new Entity.Billing.Category();

                        objCategory.CategoryID = Convert.ToInt32(drData["PK_CategoryID"]);
                        objCategory.CategoryName = Convert.ToString(drData["CategoryName"]);
                        objCategory.CategoryCode = Convert.ToString(drData["CategoryCode"]);
                        objCategory.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objCategory);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Category GetCategory | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Category> GetActiveCategory()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Category> objList = new Collection<Entity.Billing.Category>();
            Entity.Billing.Category objCategory = new Entity.Billing.Category();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CATEGORY);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        if (Convert.ToBoolean(drData["IsActive"]))
                        {
                            objCategory = new Entity.Billing.Category();

                            objCategory.CategoryID = Convert.ToInt32(drData["PK_CategoryID"]);
                            objCategory.CategoryName = Convert.ToString(drData["CategoryName"]);
                            objCategory.CategoryCode = Convert.ToString(drData["CategoryCode"]);
                            objCategory.IsActive = Convert.ToBoolean(drData["IsActive"]);

                            objList.Add(objCategory);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Category GetCategory | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.Category GetCategoryByID(int iCategoryID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Category objCategory = new Entity.Billing.Category();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CATEGORY);
                db.AddInParameter(cmd, "@PK_CategoryID", DbType.Int32, iCategoryID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCategory = new Entity.Billing.Category();
                        objCategory.CategoryID = Convert.ToInt32(drData["PK_CategoryID"]);
                        objCategory.CategoryName = Convert.ToString(drData["CategoryName"]);
                        objCategory.CategoryCode = Convert.ToString(drData["CategoryCode"]);
                        objCategory.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Category GetCategoryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objCategory;
        }
        public static int AddCategory(Entity.Billing.Category objCategory)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddCategory(oDb, objCategory, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tCategory", "PK_CategoryID", objCategory.CategoryID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objCategory.CreatedBy.UserID);
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
        private static int AddCategory(Database oDb, Entity.Billing.Category objCategory, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_CATEGORY);
                oDb.AddOutParameter(cmd, "@PK_CategoryID", DbType.Int32, objCategory.CategoryID);
                oDb.AddInParameter(cmd, "@CategoryName", DbType.String, objCategory.CategoryName);
                oDb.AddInParameter(cmd, "@CategoryCode", DbType.String, objCategory.CategoryCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objCategory.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objCategory.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_CategoryID"));
                    objCategory.CategoryID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Category AddCategory | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateCategory(Entity.Billing.Category objCategory)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateCategory(oDb, objCategory, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tCategory", "PK_CategoryID", objCategory.CategoryID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objCategory.ModifiedBy.UserID);
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
        private static bool UpdateCategory(Database oDb, Entity.Billing.Category objCategory, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_CATEGORY);
                oDb.AddInParameter(cmd, "@PK_CategoryID", DbType.Int32, objCategory.CategoryID);
                oDb.AddInParameter(cmd, "@CategoryName", DbType.String, objCategory.CategoryName);
                oDb.AddInParameter(cmd, "@CategoryCode", DbType.String, objCategory.CategoryCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objCategory.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objCategory.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Category UpdateCategory | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteCategory(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteCategory(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tCategory", "PK_CategoryID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteCategory(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_CATEGORY);
                oDb.AddInParameter(cmd, "@PK_CategoryID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Category DeleteCategory | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
