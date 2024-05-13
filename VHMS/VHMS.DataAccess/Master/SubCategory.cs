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
    public class SubCategory
    {
        public static Collection<Entity.SubCategory> GetSubCategory()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.SubCategory> objList = new Collection<Entity.SubCategory>();
            Entity.SubCategory objSubCategory = new Entity.SubCategory();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            Entity.Billing.Category objCategory;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SUBCATEGORY);
                dsList = db.ExecuteDataSet(cmd);
               
                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSubCategory = new Entity.SubCategory();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();
                        objCategory = new Entity.Billing.Category();

                        objCategory.CategoryID= Convert.ToInt32(drData["FK_CategoryID"]);
                        objCategory.CategoryName = Convert.ToString(drData["CategoryName"]);
                        objSubCategory.Category = objCategory;

                        objSubCategory.SubCategoryID = Convert.ToInt32(drData["PK_SubCategoryID"]);
                        objSubCategory.SubCategoryName = Convert.ToString(drData["SubCategoryName"]);
                        objSubCategory.SubCategoryCode = Convert.ToString(drData["SubCategoryCode"]);
                        objSubCategory.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objSubCategory);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.SubCategory GetSubCategory | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static List<string> GetSubCategoryNameList(string prefix, int CategoryID)
        {
            List<string> Products = new List<string>();
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Master.Product> objList = new Collection<Entity.Master.Product>();
            Entity.Master.Product objProduct = new Entity.Master.Product();
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SUBCATEGORYLIST);
                db.AddInParameter(cmd, "@SubCategoryName", DbType.String, prefix);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, CategoryID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                        Products.Add(string.Format("{0,-200}", drData["SubCategoryName"]));
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Master.Product GetProduct | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return Products;
        }
        public static Entity.SubCategory GetSubCategoryByID(int iSubCategoryID)
        {
            string sException = string.Empty;
            Database db;
            Entity.SubCategory objSubCategory = new Entity.SubCategory();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            Entity.Billing.Category objCategory;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SUBCATEGORY);
                db.AddInParameter(cmd, "@PK_SubCategoryID", DbType.Int32, iSubCategoryID);
                DataSet dsList = db.ExecuteDataSet(cmd);
               
                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSubCategory = new Entity.SubCategory();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();
                        objCategory = new Entity.Billing.Category();

                        objCategory.CategoryID = Convert.ToInt32(drData["Fk_CategoryID"]);
                        objCategory.CategoryName = Convert.ToString(drData["CategoryName"]);
                        objSubCategory.Category = objCategory;

                        objSubCategory.SubCategoryID = Convert.ToInt32(drData["PK_SubCategoryID"]);
                        objSubCategory.SubCategoryName = Convert.ToString(drData["SubCategoryName"]);
                        objSubCategory.SubCategoryCode = Convert.ToString(drData["SubCategoryCode"]);
                        objSubCategory.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.SubCategory GetSubCategoryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSubCategory;
        }

        public static Collection<Entity.SubCategory> GetSubCategoryByCategoryID(int iSubCategoryID)
        {
            string sException = string.Empty;
            Database db;
            Collection<Entity.SubCategory> objList = new Collection<Entity.SubCategory>();
            Entity.SubCategory objSubCategory = new Entity.SubCategory();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            Entity.Billing.Category objCategory;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SUBCATEGORY);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, iSubCategoryID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSubCategory = new Entity.SubCategory();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();
                        objCategory = new Entity.Billing.Category();

                        objCategory.CategoryID = Convert.ToInt32(drData["Fk_CategoryID"]);
                        objCategory.CategoryName = Convert.ToString(drData["CategoryName"]);
                        objSubCategory.Category = objCategory;

                        objSubCategory.SubCategoryID = Convert.ToInt32(drData["PK_SubCategoryID"]);
                        objSubCategory.SubCategoryName = Convert.ToString(drData["SubCategoryName"]);
                        objSubCategory.SubCategoryCode = Convert.ToString(drData["SubCategoryCode"]);
                        objSubCategory.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objSubCategory);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.SubCategory GetSubCategoryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.SubCategory> GetActiveSubCategoryByCategoryID(int iSubCategoryID)
        {
            string sException = string.Empty;
            Database db;
            Collection<Entity.SubCategory> objList = new Collection<Entity.SubCategory>();
            Entity.SubCategory objSubCategory = new Entity.SubCategory();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            Entity.Billing.Category objCategory;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SUBCATEGORY);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, iSubCategoryID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        if (Convert.ToBoolean(drData["IsActive"]))
                        {
                            objSubCategory = new Entity.SubCategory();
                            objCreatedUser = new Entity.User();
                            objModifiedUser = new Entity.User();
                            objCategory = new Entity.Billing.Category();

                            objCategory.CategoryID = Convert.ToInt32(drData["Fk_CategoryID"]);
                            objCategory.CategoryName = Convert.ToString(drData["CategoryName"]);
                            objSubCategory.Category = objCategory;

                            objSubCategory.SubCategoryID = Convert.ToInt32(drData["PK_SubCategoryID"]);
                            objSubCategory.SubCategoryName = Convert.ToString(drData["SubCategoryName"]);
                            objSubCategory.SubCategoryCode = Convert.ToString(drData["SubCategoryCode"]);
                            objSubCategory.IsActive = Convert.ToBoolean(drData["IsActive"]);

                            objList.Add(objSubCategory);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.SubCategory GetSubCategoryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static int AddSubCategory(Entity.SubCategory objSubCategory)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddSubCategory(oDb, objSubCategory, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tSubCategory", "PK_SubCategoryID", objSubCategory.SubCategoryID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objSubCategory.CreatedBy.UserID);
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
        private static int AddSubCategory(Database oDb, Entity.SubCategory objSubCategory, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_SUBCATEGORY);
                oDb.AddOutParameter(cmd, "@PK_SubCategoryID", DbType.Int32, objSubCategory.SubCategoryID);
                oDb.AddInParameter(cmd, "@SubCategoryName", DbType.String, objSubCategory.SubCategoryName);
                oDb.AddInParameter(cmd, "@SubCategoryCode", DbType.String, objSubCategory.SubCategoryCode);
                oDb.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, objSubCategory.Category.CategoryID);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objSubCategory.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objSubCategory.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_SubCategoryID"));
                    objSubCategory.SubCategoryID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.SubCategory AddSubCategory | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateSubCategory(Entity.SubCategory objSubCategory)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateSubCategory(oDb, objSubCategory, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tSubCategory", "PK_SubCategoryID", objSubCategory.SubCategoryID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objSubCategory.ModifiedBy.UserID);
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
        private static bool UpdateSubCategory(Database oDb, Entity.SubCategory objSubCategory, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_SUBCATEGORY);
                oDb.AddInParameter(cmd, "@PK_SubCategoryID", DbType.Int32, objSubCategory.SubCategoryID);
                oDb.AddInParameter(cmd, "@SubCategoryName", DbType.String, objSubCategory.SubCategoryName);
                oDb.AddInParameter(cmd, "@SubCategoryCode", DbType.String, objSubCategory.SubCategoryCode);
                oDb.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, objSubCategory.Category.CategoryID);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objSubCategory.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objSubCategory.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.SubCategory UpdateSubCategory | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteSubCategory(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteSubCategory(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tSubCategory", "PK_SubCategoryID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteSubCategory(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_SUBCATEGORY);
                oDb.AddInParameter(cmd, "@PK_SubCategoryID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.SubCategory DeleteSubCategory | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
