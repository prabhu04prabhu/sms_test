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
    public class StockAdjuest
    {
        public static Collection<Entity.StockAdjuest> GetStockAdjust()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.StockAdjuest> objList = new Collection<Entity.StockAdjuest>();
            Entity.StockAdjuest objStockAdjuest = new Entity.StockAdjuest();
            Entity.Master.Product objProduct;
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_STOCKADJUEST);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objStockAdjuest = new Entity.StockAdjuest();
                        objProduct = new Entity.Master.Product();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objStockAdjuest.StockAdjustID = Convert.ToInt32(drData["PK_StockAdjustID"]);
                        objStockAdjuest.Barcode = Convert.ToString(drData["Barcode"]);

                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMScode"]);
                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        objStockAdjuest.Product = objProduct;

                        objStockAdjuest.Available_Qty = Convert.ToDecimal(drData["Available_Qty"]);
                        objStockAdjuest.Updated_Qty = Convert.ToDecimal(drData["Updated_Qty"]);
                        objStockAdjuest.Purchase_Price = Convert.ToDecimal(drData["Purchase_Price"]);
                        objStockAdjuest.RetailSalesMargin = Convert.ToDecimal(drData["RetailSalesMargin"]);
                        objStockAdjuest.WholeSalesMargin = Convert.ToDecimal(drData["WholeSalesMargin"]);
                        objStockAdjuest.RetailSalesPrice = Convert.ToDecimal(drData["RetailSalesPrice"]);
                        objStockAdjuest.WholeSalesPrice = Convert.ToDecimal(drData["WholeSalesPrice"]);
                        objStockAdjuest.CreatedOn= Convert.ToDateTime(drData["CreatedOn"]);
                        objStockAdjuest.sCreatedOn = objStockAdjuest.CreatedOn.ToString("dd/MM/yyyy h:mm");
                        objList.Add(objStockAdjuest);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.StockAdjuest GetStockAdjust | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.StockAdjuest> GetAllStockAdjust(int iProductID, int iCategoryID, int iSubCategoryID, int iSupplierID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.StockAdjuest> objList = new Collection<Entity.StockAdjuest>();
            Entity.StockAdjuest objStockAdjuest = new Entity.StockAdjuest();
            Entity.Master.Product objProduct;
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_GETALLSTOCKADJUEST);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, iProductID);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, iCategoryID);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@FK_SubCategoryID", DbType.Int32, iSubCategoryID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objStockAdjuest = new Entity.StockAdjuest();
                        objProduct = new Entity.Master.Product();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objStockAdjuest.StockAdjustID = Convert.ToInt32(drData["PK_StockAdjustID"]);
                        objStockAdjuest.Barcode = Convert.ToString(drData["Barcode"]);

                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMScode"]);
                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        objStockAdjuest.Product = objProduct;

                        objStockAdjuest.Available_Qty = Convert.ToDecimal(drData["Available_Qty"]);
                        objStockAdjuest.Updated_Qty = Convert.ToDecimal(drData["Updated_Qty"]);
                        objStockAdjuest.Purchase_Price = Convert.ToDecimal(drData["Purchase_Price"]);
                        objStockAdjuest.RetailSalesMargin = Convert.ToDecimal(drData["RetailSalesMargin"]);
                        objStockAdjuest.WholeSalesMargin = Convert.ToDecimal(drData["WholeSalesMargin"]);
                        objStockAdjuest.RetailSalesPrice = Convert.ToDecimal(drData["RetailSalesPrice"]);
                        objStockAdjuest.WholeSalesPrice = Convert.ToDecimal(drData["WholeSalesPrice"]);
                        objStockAdjuest.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objStockAdjuest.sCreatedOn = objStockAdjuest.CreatedOn.ToString("dd/MM/yyyy h:mm");

                        objList.Add(objStockAdjuest);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.StockAdjuest GetStockAdjust | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static int AddStockAdjuest(Entity.StockAdjuest objStockAdjuest)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddStockAdjuest(oDb, objStockAdjuest, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tStockAdjust", "PK_StockAdjustID", objStockAdjuest.StockAdjustID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objStockAdjuest.CreatedBy.UserID);
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
        private static int AddStockAdjuest(Database oDb, Entity.StockAdjuest objStockAdjuest, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_STOCKADJUEST);
                oDb.AddOutParameter(cmd, "@PK_StockAdjustID", DbType.Int32, objStockAdjuest.StockAdjustID);
                oDb.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, objStockAdjuest.Product.ProductID);
                oDb.AddInParameter(cmd, "@Barcode", DbType.String, objStockAdjuest.Barcode);
                oDb.AddInParameter(cmd, "@SMScode", DbType.String, objStockAdjuest.Product.SMSCode);
                oDb.AddInParameter(cmd, "@AdjustType", DbType.String, objStockAdjuest.AdjustType);
                oDb.AddInParameter(cmd, "@Available_Qty", DbType.Decimal, objStockAdjuest.Available_Qty);
                oDb.AddInParameter(cmd, "@Updated_Qty", DbType.Decimal, objStockAdjuest.Updated_Qty);
                oDb.AddInParameter(cmd, "@Purchase_Price", DbType.Decimal, objStockAdjuest.Purchase_Price);
                oDb.AddInParameter(cmd, "@RetailSalesMargin", DbType.Decimal, objStockAdjuest.RetailSalesMargin);
                oDb.AddInParameter(cmd, "@WholeSalesMargin", DbType.Decimal, objStockAdjuest.WholeSalesMargin);
                oDb.AddInParameter(cmd, "@RetailSalesPrice", DbType.Decimal, objStockAdjuest.RetailSalesPrice);
                oDb.AddInParameter(cmd, "@WholeSalesPrice", DbType.String, objStockAdjuest.WholeSalesPrice);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objStockAdjuest.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_StockAdjustID"));
                    objStockAdjuest.StockAdjustID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.StockAdjuest AddStockAdjuest | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }

        //public static int UpdateSplitWeight(int stockID, decimal iWeight, int iUserID)
        //{
        //    int ID = 0;
        //    Database oDb = Entity.DBConnection.dbCon;
        //    using (DbConnection oDbCon = oDb.CreateConnection())
        //    {
        //        oDbCon.Open();
        //        DbTransaction oTrans = oDbCon.BeginTransaction();
        //        try
        //        {
        //            ID = UpdateSplitWeight(oDb, stockID, iWeight, iUserID, oTrans);
        //            oTrans.Commit();
        //            if (ID > 0)
        //                Framework.InsertAuditLog("tStockAdjust", "PK_StockAdjustID", stockID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, iUserID);
        //        }
        //        catch (Exception ex)
        //        {
        //            oTrans.Rollback();
        //            throw ex;
        //        }
        //        finally
        //        {
        //            oDbCon.Close();
        //        }
        //    }
        //    return ID;
        //}
        //private static int UpdateSplitWeight(Database oDb, int stockID, decimal iWeight, int iUserID, DbTransaction oTrans)
        //{
        //    string sException = string.Empty;
        //    int iID = 0;
        //    try
        //    {
        //        DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_STOCKSPLIT);
        //        oDb.AddOutParameter(cmd, "@PK_StockAdjustID", DbType.Int32, 0);
        //        oDb.AddInParameter(cmd, "@PK_StockAdjustID1", DbType.Int32, stockID);
        //        oDb.AddInParameter(cmd, "@NetWeight", DbType.Decimal, iWeight);
        //        oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, iUserID);

        //        iID = oDb.ExecuteNonQuery(cmd, oTrans);
        //        if (iID != 0)
        //        {
        //            iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_StockAdjustID"));
        //            //objStockAdjuest.StockAdjuestID = iID;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "VHMS.DataAccess.StockAdjuest AddStockAdjuest | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return iID;
        //}
        //public static bool UpdateStockAdjuest(Entity.StockAdjuest objStockAdjuest)
        //{
        //    bool IsUpdated = true;
        //    Database oDb = Entity.DBConnection.dbCon;
        //    using (DbConnection oDbCon = oDb.CreateConnection())
        //    {
        //        oDbCon.Open();
        //        DbTransaction oTrans = oDbCon.BeginTransaction();
        //        try
        //        {
        //            IsUpdated = UpdateStockAdjuest(oDb, objStockAdjuest, oTrans);
        //            oTrans.Commit();
        //            if (IsUpdated) Framework.InsertAuditLog("tStockAdjust", "PK_StockAdjustID", objStockAdjuest.StockAdjuestID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objStockAdjuest.ModifiedBy.UserID);
        //        }
        //        catch (Exception ex)
        //        {
        //            oTrans.Rollback();
        //            throw ex;
        //        }
        //        finally
        //        {
        //            oDbCon.Close();
        //        }
        //    }
        //    return IsUpdated;
        //}
        //private static bool UpdateStockAdjuest(Database oDb, Entity.StockAdjuest objStockAdjuest, DbTransaction oTrans)
        //{
        //    string sException = string.Empty;
        //    int iID = 0;
        //    bool bResult = false;
        //    try
        //    {
        //        DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_STOCK);
        //        oDb.AddInParameter(cmd, "@PK_StockAdjustID", DbType.Int32, objStockAdjuest.StockAdjuestID);
        //        oDb.AddInParameter(cmd, "@StockAdjuestDate", DbType.String, objStockAdjuest.sStockAdjuestDate);
        //        oDb.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, objStockAdjuest.Product.ProductID);
        //        oDb.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, objStockAdjuest.Category.CategoryID);
        //        oDb.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, objStockAdjuest.Branch.BranchID);
        //        //oDb.AddInParameter(cmd, "@Barcode", DbType.String, objStockAdjuest.Barcode);
        //        oDb.AddInParameter(cmd, "@NetWeight", DbType.Decimal, objStockAdjuest.NetWeight);
        //        oDb.AddInParameter(cmd, "@Wastage", DbType.Decimal, objStockAdjuest.Wastage);
        //        oDb.AddInParameter(cmd, "@WastagePercent", DbType.Decimal, objStockAdjuest.WastagePercent);
        //        oDb.AddInParameter(cmd, "@TotalWeight", DbType.Decimal, objStockAdjuest.TotalWeight);
        //        oDb.AddInParameter(cmd, "@Ratti", DbType.Decimal, objStockAdjuest.Ratti);
        //        oDb.AddInParameter(cmd, "@PureWeight", DbType.Decimal, objStockAdjuest.PureWeight);
        //        oDb.AddInParameter(cmd, "@Lacquer", DbType.Decimal, objStockAdjuest.Lacquer);
        //        oDb.AddInParameter(cmd, "@Making", DbType.String, objStockAdjuest.Making);
        //        oDb.AddInParameter(cmd, "@Design", DbType.String, objStockAdjuest.Design);
        //        oDb.AddInParameter(cmd, "@Karat", DbType.String, objStockAdjuest.Karat);
        //        oDb.AddInParameter(cmd, "@Quantity", DbType.Int32, objStockAdjuest.Quantity);
        //        oDb.AddInParameter(cmd, "@Pieces", DbType.Int32, objStockAdjuest.Pieces);
        //        oDb.AddInParameter(cmd, "@Size", DbType.String, objStockAdjuest.Size);
        //        oDb.AddInParameter(cmd, "@Description", DbType.String, objStockAdjuest.Description);
        //        oDb.AddInParameter(cmd, "@PurchasePrice", DbType.Decimal, objStockAdjuest.PurchasePrice);
        //        oDb.AddInParameter(cmd, "@SellingPrice", DbType.Decimal, objStockAdjuest.SellingPrice);
        //        oDb.AddInParameter(cmd, "@StoneName", DbType.String, objStockAdjuest.StoneName);
        //        oDb.AddInParameter(cmd, "@StoneQuantity", DbType.Int32, objStockAdjuest.StoneQuantity);
        //        oDb.AddInParameter(cmd, "@StoneWeight", DbType.Decimal, objStockAdjuest.StoneWeight);
        //        oDb.AddInParameter(cmd, "@StoneRate", DbType.Decimal, objStockAdjuest.StoneRate);
        //        oDb.AddInParameter(cmd, "@StonePrice", DbType.Decimal, objStockAdjuest.StonePrice);
        //        oDb.AddInParameter(cmd, "@StoneColor", DbType.String, objStockAdjuest.StoneColor);
        //        oDb.AddInParameter(cmd, "@StoneCut", DbType.String, objStockAdjuest.StoneCut);
        //        oDb.AddInParameter(cmd, "@StoneClarity", DbType.String, objStockAdjuest.StoneClarity);
        //        oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objStockAdjuest.ModifiedBy.UserID);

        //        iID = oDb.ExecuteNonQuery(cmd);
        //        if (iID != 0) bResult = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "VHMS.DataAccess.StockAdjuest UpdateStockAdjuest | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return bResult;
        //}
        //public static bool DeleteStockAdjuest(int ID, int UserID)
        //{
        //    bool IsDeleted = false;
        //    Database oDb = Entity.DBConnection.dbCon;
        //    using (DbConnection oDbCon = oDb.CreateConnection())
        //    {
        //        oDbCon.Open();
        //        DbTransaction oTrans = oDbCon.BeginTransaction();
        //        try
        //        {
        //            IsDeleted = DeleteStockAdjuest(oDb, ID, UserID, oTrans);
        //            oTrans.Commit();

        //            if (IsDeleted) Framework.InsertAuditLog("tStockAdjust", "PK_StockAdjustID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
        //        }
        //        catch (Exception ex)
        //        {
        //            oTrans.Rollback();
        //            throw ex;
        //        }
        //        finally
        //        {
        //            oDbCon.Close();
        //        }
        //    }
        //    return IsDeleted;
        //}
        //private static bool DeleteStockAdjuest(Database oDb, int ID, int UserID, DbTransaction oTrans)
        //{
        //    string sException = string.Empty;
        //    int iRemoveId = 0;
        //    bool bResult = false;
        //    try
        //    {
        //        DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_STOCK);
        //        oDb.AddInParameter(cmd, "@PK_StockAdjustID", DbType.Int32, ID);
        //        iRemoveId = oDb.ExecuteNonQuery(cmd);
        //        if (iRemoveId != 0) bResult = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "VHMS.DataAccess.StockAdjuest DeleteStockAdjuest | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return bResult;
        //}
    }
}
