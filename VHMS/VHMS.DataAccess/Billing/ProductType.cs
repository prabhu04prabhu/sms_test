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
    public class ProductType
    {
        public static Collection<Entity.Billing.ProductType> GetProductType()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.ProductType> objList = new Collection<Entity.Billing.ProductType>();
            Entity.Billing.ProductType objProductType = new Entity.Billing.ProductType();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PRODUCTTYPE);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objProductType = new Entity.Billing.ProductType();

                        objProductType.ProductTypeID = Convert.ToInt32(drData["PK_ProductTypeID"]);
                        objProductType.ProductTypeName = Convert.ToString(drData["ProductTypeName"]);
                        objProductType.ProductTypeCode = Convert.ToString(drData["ProductTypeCode"]);
                        objProductType.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objProductType);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.ProductType GetProductType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.ProductType> GetActiveProductType()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.ProductType> objList = new Collection<Entity.Billing.ProductType>();
            Entity.Billing.ProductType objProductType = new Entity.Billing.ProductType();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PRODUCTTYPE);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        if (Convert.ToBoolean(drData["IsActive"]))
                        {
                            objProductType = new Entity.Billing.ProductType();

                            objProductType.ProductTypeID = Convert.ToInt32(drData["PK_ProductTypeID"]);
                            objProductType.ProductTypeName = Convert.ToString(drData["ProductTypeName"]);
                            objProductType.ProductTypeCode = Convert.ToString(drData["ProductTypeCode"]);
                            objProductType.IsActive = Convert.ToBoolean(drData["IsActive"]);

                            objList.Add(objProductType);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.ProductType GetProductType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.ProductType GetProductTypeByID(int iProductTypeID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.ProductType objProductType = new Entity.Billing.ProductType();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PRODUCTTYPE);
                db.AddInParameter(cmd, "@PK_ProductTypeID", DbType.Int32, iProductTypeID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objProductType = new Entity.Billing.ProductType();
                        objProductType.ProductTypeID = Convert.ToInt32(drData["PK_ProductTypeID"]);
                        objProductType.ProductTypeName = Convert.ToString(drData["ProductTypeName"]);
                        objProductType.ProductTypeCode = Convert.ToString(drData["ProductTypeCode"]);
                        objProductType.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.ProductType GetProductTypeByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objProductType;
        }
        public static int AddProductType(Entity.Billing.ProductType objProductType)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddProductType(oDb, objProductType, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tProductTypeMaster", "PK_ProductTypeID", objProductType.ProductTypeID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objProductType.CreatedBy.UserID);
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
        private static int AddProductType(Database oDb, Entity.Billing.ProductType objProductType, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_PRODUCTTYPE);
                oDb.AddOutParameter(cmd, "@PK_ProductTypeID", DbType.Int32, objProductType.ProductTypeID);
                oDb.AddInParameter(cmd, "@ProductTypeName", DbType.String, objProductType.ProductTypeName);
                oDb.AddInParameter(cmd, "@ProductTypeCode", DbType.String, objProductType.ProductTypeCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objProductType.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objProductType.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_ProductTypeID"));
                    objProductType.ProductTypeID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.ProductType AddProductType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateProductType(Entity.Billing.ProductType objProductType)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateProductType(oDb, objProductType, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tProductTypeMaster", "PK_ProductTypeID", objProductType.ProductTypeID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objProductType.ModifiedBy.UserID);
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
        private static bool UpdateProductType(Database oDb, Entity.Billing.ProductType objProductType, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_PRODUCTTYPE);
                oDb.AddInParameter(cmd, "@PK_ProductTypeID", DbType.Int32, objProductType.ProductTypeID);
                oDb.AddInParameter(cmd, "@ProductTypeName", DbType.String, objProductType.ProductTypeName);
                oDb.AddInParameter(cmd, "@ProductTypeCode", DbType.String, objProductType.ProductTypeCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objProductType.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objProductType.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.ProductType UpdateProductType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteProductType(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteProductType(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tProductTypeMaster", "PK_ProductTypeID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteProductType(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_PRODUCTTYPE);
                oDb.AddInParameter(cmd, "@PK_ProductTypeID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.ProductType DeleteProductType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
