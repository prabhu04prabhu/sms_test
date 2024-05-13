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
    public class Pricing
    {
        public static Collection<Entity.Pricing> GetPricing(int ProductID, int CategoryID, int SubCategoryID, int SupplierID,int TypeID, string ProductCode)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Pricing> objList = new Collection<Entity.Pricing>();
            Entity.Pricing objPricing = new Entity.Pricing();
            Entity.Master.Product objProduct = new Entity.Master.Product();
            Entity.Billing.Category objCategory; Entity.Billing.Supplier objSupplier;
            Entity.Billing.Unit objUnit; Entity.SubCategory objSubCategory;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PRICING);
                db.AddInParameter(cmd, "@PK_ProductID", DbType.Int32, ProductID);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, CategoryID);
                db.AddInParameter(cmd, "@FK_SubCategoryID", DbType.Int32, SubCategoryID);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, SupplierID);
                db.AddInParameter(cmd, "@FK_ProductTypeID", DbType.Int32, TypeID);
                db.AddInParameter(cmd, "@Code", DbType.String, ProductCode);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCategory = new Entity.Billing.Category();
                        objSupplier = new Entity.Billing.Supplier();
                        objUnit = new Entity.Billing.Unit();
                        objSubCategory = new Entity.SubCategory();
                        objProduct = new Entity.Master.Product();
                        objPricing = new Entity.Pricing();

                        objProduct.ProductID = Convert.ToInt32(drData["PK_ProductID"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);

                        objCategory.CategoryID = Convert.ToInt32(drData["FK_CategoryID"]);
                        objCategory.CategoryName = Convert.ToString(drData["CategoryName"]);
                        objProduct.Category = objCategory;

                        objSubCategory.SubCategoryID = Convert.ToInt32(drData["FK_SubCategoryID"]);
                        objSubCategory.SubCategoryName = Convert.ToString(drData["SubCategoryName"]);
                        objProduct.SubCategory = objSubCategory;

                        objUnit.UnitID = Convert.ToInt32(drData["FK_UnitID"]);
                        objUnit.UnitName = Convert.ToString(drData["UnitName"]);
                        objProduct.Unit = objUnit;

                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objSupplier.SupplierCode = Convert.ToString(drData["SupplierCode"]);
                        objProduct.Supplier = objSupplier;

                        objProduct.ProductImage1 = Convert.ToString(drData["ProductImage1"]);
                        objProduct.ProductImage2 = Convert.ToString(drData["ProductImage2"]);
                        objProduct.ProductImage3 = Convert.ToString(drData["ProductImage3"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objProduct.PricingA = Convert.ToBoolean(drData["PricingA"]);
                        objProduct.PricingB = Convert.ToBoolean(drData["PricingB"]);
                        objProduct.PricingC = Convert.ToBoolean(drData["PricingC"]);

                        objPricing.PricingID = Convert.ToInt32(drData["PK_PricingID"]);
                        objPricing.PurchasePrice = Convert.ToDecimal(drData["PurchasePrice"]);
                        objPricing.MRP = Convert.ToDecimal(drData["MRP"]);
                        objPricing.WholeSalePrice = Convert.ToDecimal(drData["WholeSalePrice"]);

                        objPricing.RetailPrice = Convert.ToDecimal(drData["RetailPrice"]);
                        objPricing.RetailMargin = Convert.ToDecimal(drData["RetailMargin"]);
                        objPricing.WholeSaleMargin = Convert.ToDecimal(drData["WholeSaleMargin"]);
                        objPricing.WholeSalePriceA = Convert.ToDecimal(drData["WholeSalePriceA"]);
                        objPricing.RetailPriceA = Convert.ToDecimal(drData["RetailPriceA"]);
                        objPricing.WholeSalePriceB = Convert.ToDecimal(drData["WholeSalePriceB"]);
                        objPricing.RetailPriceB = Convert.ToDecimal(drData["RetailPriceB"]);
                        objPricing.WholeSalePriceC = Convert.ToDecimal(drData["WholeSalePriceC"]);
                        objPricing.RetailPriceC = Convert.ToDecimal(drData["RetailPriceC"]);
                        objPricing.MaxDiscountPercent = Convert.ToDecimal(drData["Maximum_DiscountPercentage"]);
                        objPricing.MinDiscountPercent = Convert.ToDecimal(drData["Minimum_DiscountPercentage"]);

                        objPricing.Product = objProduct;
                        objList.Add(objPricing);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Master.Product GetProduct | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Pricing GetPricingID(int iProductID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Pricing objPricing = new Entity.Pricing();
            Entity.Master.Product objProduct = new Entity.Master.Product();
            Entity.Billing.Category objCategory; Entity.Billing.Supplier objSupplier;
            Entity.Billing.Unit objUnit; Entity.SubCategory objSubCategory;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PRICING);
                db.AddInParameter(cmd, "@PK_ProductID", DbType.Int32, iProductID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCategory = new Entity.Billing.Category();
                        objProduct = new Entity.Master.Product();
                        objSupplier = new Entity.Billing.Supplier();
                        objUnit = new Entity.Billing.Unit();
                        objSubCategory = new Entity.SubCategory();
                        objPricing = new Entity.Pricing();

                        objProduct.ProductID = Convert.ToInt32(drData["PK_ProductID"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);

                        objCategory.CategoryID = Convert.ToInt32(drData["FK_CategoryID"]);
                        objCategory.CategoryName = Convert.ToString(drData["CategoryName"]);
                        objProduct.Category = objCategory;

                        objSubCategory.SubCategoryID = Convert.ToInt32(drData["FK_SubCategoryID"]);
                        objSubCategory.SubCategoryName = Convert.ToString(drData["SubCategoryName"]);
                        objProduct.SubCategory = objSubCategory;

                        objUnit.UnitID = Convert.ToInt32(drData["FK_UnitID"]);
                        objUnit.UnitName = Convert.ToString(drData["UnitName"]);
                        objProduct.Unit = objUnit;

                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objProduct.Supplier = objSupplier;

                        objProduct.ProductImage1 = Convert.ToString(drData["ProductImage1"]);
                        objProduct.ProductImage2 = Convert.ToString(drData["ProductImage2"]);
                        objProduct.ProductImage3 = Convert.ToString(drData["ProductImage3"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.PricingA = Convert.ToBoolean(drData["PricingA"]);
                        objProduct.PricingB = Convert.ToBoolean(drData["PricingB"]);
                        objProduct.PricingC = Convert.ToBoolean(drData["PricingC"]);

                        objProduct.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objPricing.PricingID = Convert.ToInt32(drData["PK_PricingID"]);
                        objPricing.PurchasePrice = Convert.ToDecimal(drData["PurchasePrice"]);
                        objPricing.MRP = Convert.ToDecimal(drData["MRP"]);
                        objPricing.WholeSalePrice = Convert.ToDecimal(drData["WholeSalePrice"]);
                        objPricing.RetailPrice = Convert.ToDecimal(drData["RetailPrice"]);
                        objPricing.RetailMargin = Convert.ToDecimal(drData["RetailMargin"]);
                        objPricing.WholeSaleMargin = Convert.ToDecimal(drData["WholeSaleMargin"]);
                        objPricing.WholeSalePriceA = Convert.ToDecimal(drData["WholeSalePriceA"]);
                        objPricing.RetailPriceA = Convert.ToDecimal(drData["RetailPriceA"]);
                        objPricing.WholeSalePriceB = Convert.ToDecimal(drData["WholeSalePriceB"]);
                        objPricing.RetailPriceB = Convert.ToDecimal(drData["RetailPriceB"]);
                        objPricing.WholeSalePriceC = Convert.ToDecimal(drData["WholeSalePriceC"]);
                        objPricing.RetailPriceC = Convert.ToDecimal(drData["RetailPriceC"]);
                        objPricing.MaxDiscountPercent = Convert.ToDecimal(drData["Maximum_DiscountPercentage"]);
                        objPricing.MinDiscountPercent = Convert.ToDecimal(drData["Minimum_DiscountPercentage"]);

                        objPricing.Product = objProduct;
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Master.Product GetProductByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objPricing;
        }

        public static Entity.Pricing GetPricingByProductName(int iProductID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Pricing objPricing = new Entity.Pricing();
            Entity.Master.Product objProduct = new Entity.Master.Product();
            Entity.Billing.Category objCategory; Entity.Billing.Supplier objSupplier;
            Entity.Billing.Unit objUnit; Entity.SubCategory objSubCategory;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.usp_Select_PricingByProductName);
                db.AddInParameter(cmd, "@PK_ProductID", DbType.Int32, iProductID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCategory = new Entity.Billing.Category();
                        objProduct = new Entity.Master.Product();
                        objSupplier = new Entity.Billing.Supplier();
                        objUnit = new Entity.Billing.Unit();
                        objSubCategory = new Entity.SubCategory();
                        objPricing = new Entity.Pricing();

                        objProduct.ProductID = Convert.ToInt32(drData["PK_ProductID"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);

                        objCategory.CategoryID = Convert.ToInt32(drData["FK_CategoryID"]);
                        objCategory.CategoryName = Convert.ToString(drData["CategoryName"]);
                        objProduct.Category = objCategory;

                        objSubCategory.SubCategoryID = Convert.ToInt32(drData["FK_SubCategoryID"]);
                        objSubCategory.SubCategoryName = Convert.ToString(drData["SubCategoryName"]);
                        objProduct.SubCategory = objSubCategory;

                        objUnit.UnitID = Convert.ToInt32(drData["FK_UnitID"]);
                        objUnit.UnitName = Convert.ToString(drData["UnitName"]);
                        objProduct.Unit = objUnit;

                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objProduct.Supplier = objSupplier;

                        objProduct.ProductImage1 = Convert.ToString(drData["ProductImage1"]);
                        objProduct.ProductImage2 = Convert.ToString(drData["ProductImage2"]);
                        objProduct.ProductImage3 = Convert.ToString(drData["ProductImage3"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.PricingA = Convert.ToBoolean(drData["PricingA"]);
                        objProduct.PricingB = Convert.ToBoolean(drData["PricingB"]);
                        objProduct.PricingC = Convert.ToBoolean(drData["PricingC"]);

                        objProduct.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objPricing.PricingID = Convert.ToInt32(drData["PK_PricingID"]);
                        objPricing.PurchasePrice = Convert.ToDecimal(drData["PurchasePrice"]);
                        objPricing.MRP = Convert.ToDecimal(drData["MRP"]);
                        objPricing.WholeSalePrice = Convert.ToDecimal(drData["WholeSalePrice"]);
                        objPricing.RetailPrice = Convert.ToDecimal(drData["RetailPrice"]);
                        objPricing.RetailMargin = Convert.ToDecimal(drData["RetailMargin"]);
                        objPricing.WholeSaleMargin = Convert.ToDecimal(drData["WholeSaleMargin"]);
                        objPricing.WholeSalePriceA = Convert.ToDecimal(drData["WholeSalePriceA"]);
                        objPricing.RetailPriceA = Convert.ToDecimal(drData["RetailPriceA"]);
                        objPricing.WholeSalePriceB = Convert.ToDecimal(drData["WholeSalePriceB"]);
                        objPricing.RetailPriceB = Convert.ToDecimal(drData["RetailPriceB"]);
                        objPricing.WholeSalePriceC = Convert.ToDecimal(drData["WholeSalePriceC"]);
                        objPricing.RetailPriceC = Convert.ToDecimal(drData["RetailPriceC"]);
                        objPricing.MaxDiscountPercent = Convert.ToDecimal(drData["Maximum_DiscountPercentage"]);
                        objPricing.MinDiscountPercent = Convert.ToDecimal(drData["Minimum_DiscountPercentage"]);

                        objPricing.Product = objProduct;
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Master.Product GetProductByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objPricing;
        }

        public static Entity.Pricing GetBarcodeByID(int iProductID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Pricing objPricing = new Entity.Pricing();
            Entity.Master.Product objProduct = new Entity.Master.Product();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_BARCODEBYID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, iProductID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objProduct = new Entity.Master.Product();
                        objPricing = new Entity.Pricing();

                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        
                        objPricing.Barcode = Convert.ToString(drData["Barcode"]);

                        objPricing.Product = objProduct;
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Master.Product GetProductByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objPricing;
        }

        public static Entity.Pricing GetBarcodeBystock(int ID, string iBarcode)
        {
            string sException = string.Empty;
            Database db;
            Entity.Pricing objPricing = new Entity.Pricing();
            Entity.Master.Product objProduct = new Entity.Master.Product();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_BARCODEBYSTOCK);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, ID);
                db.AddInParameter(cmd, "@Barcode", DbType.String, iBarcode);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objProduct = new Entity.Master.Product();
                        objPricing = new Entity.Pricing();

                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);

                        objPricing.Barcode = Convert.ToString(drData["Barcode"]);

                        objPricing.Product = objProduct;
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Master.Product GetProductByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objPricing;
        }


        public static Collection<Entity.Pricing> GetPricingByCategoryID(int iProductID, int iCategoryID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Pricing> objList = new Collection<Entity.Pricing>();
            Entity.Pricing objPricing = new Entity.Pricing();
            Entity.Master.Product objProduct = new Entity.Master.Product();
            Entity.Billing.Category objCategory; Entity.Billing.Supplier objSupplier;
            Entity.Billing.Unit objUnit; Entity.SubCategory objSubCategory;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PRICING);
                db.AddInParameter(cmd, "@PK_ProductID", DbType.Int32, iProductID);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, iCategoryID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCategory = new Entity.Billing.Category();
                        objProduct = new Entity.Master.Product();
                        objSupplier = new Entity.Billing.Supplier();
                        objUnit = new Entity.Billing.Unit();
                        objSubCategory = new Entity.SubCategory();
                        objPricing = new Entity.Pricing();

                        objProduct.ProductID = Convert.ToInt32(drData["PK_ProductID"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);

                        objCategory.CategoryID = Convert.ToInt32(drData["FK_CategoryID"]);
                        objCategory.CategoryName = Convert.ToString(drData["CategoryName"]);
                        objProduct.Category = objCategory;

                        objSubCategory.SubCategoryID = Convert.ToInt32(drData["FK_SubCategoryID"]);
                        objSubCategory.SubCategoryName = Convert.ToString(drData["SubCategoryName"]);
                        objProduct.SubCategory = objSubCategory;

                        objUnit.UnitID = Convert.ToInt32(drData["FK_UnitID"]);
                        objUnit.UnitName = Convert.ToString(drData["UnitName"]);
                        objProduct.Unit = objUnit;

                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objProduct.Supplier = objSupplier;

                        objProduct.ProductImage1 = Convert.ToString(drData["ProductImage1"]);
                        objProduct.ProductImage2 = Convert.ToString(drData["ProductImage2"]);
                        objProduct.ProductImage3 = Convert.ToString(drData["ProductImage3"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objProduct.PricingA = Convert.ToBoolean(drData["PricingA"]);
                        objProduct.PricingB = Convert.ToBoolean(drData["PricingB"]);
                        objProduct.PricingC = Convert.ToBoolean(drData["PricingC"]);

                        objPricing.PricingID = Convert.ToInt32(drData["PK_PricingID"]);
                        objPricing.PurchasePrice = Convert.ToDecimal(drData["PurchasePrice"]);
                        objPricing.MRP = Convert.ToDecimal(drData["MRP"]);
                        objPricing.WholeSalePrice = Convert.ToDecimal(drData["WholeSalePrice"]);
                        objPricing.RetailPrice = Convert.ToDecimal(drData["RetailPrice"]);
                        objPricing.RetailMargin = Convert.ToDecimal(drData["RetailMargin"]);
                        objPricing.WholeSaleMargin = Convert.ToDecimal(drData["WholeSaleMargin"]);
                        objPricing.WholeSalePriceA = Convert.ToDecimal(drData["WholeSalePriceA"]);
                        objPricing.RetailPriceA = Convert.ToDecimal(drData["RetailPriceA"]);
                        objPricing.WholeSalePriceB = Convert.ToDecimal(drData["WholeSalePriceB"]);
                        objPricing.RetailPriceB = Convert.ToDecimal(drData["RetailPriceB"]);
                        objPricing.WholeSalePriceC = Convert.ToDecimal(drData["WholeSalePriceC"]);
                        objPricing.RetailPriceC = Convert.ToDecimal(drData["RetailPriceC"]);
                        objPricing.MaxDiscountPercent = Convert.ToDecimal(drData["Maximum_DiscountPercentage"]);
                        objPricing.MinDiscountPercent = Convert.ToDecimal(drData["Minimum_DiscountPercentage"]);

                        objPricing.Product = objProduct;

                        objList.Add(objPricing);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Master.Product GetProduct | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static int AddPricing(Entity.Pricing objProduct)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddPricing(oDb, objProduct, oTrans);
                    oTrans.Commit();
                    //if (ID > 0)
                    //    Framework.InsertAuditLog("tPricing", "PK_PricingID", objProduct.PricingID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objProduct.PricingID);
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
        private static int AddPricing(Database oDb, Entity.Pricing objProduct, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_PRICING);
                oDb.AddOutParameter(cmd, "@PK_PricingID", DbType.Int32, objProduct.PricingID);
                oDb.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, objProduct.Product.ProductID);
                oDb.AddInParameter(cmd, "@PurchasePrice", DbType.Decimal, objProduct.PurchasePrice);
                oDb.AddInParameter(cmd, "@MRP", DbType.Decimal, objProduct.MRP);
                oDb.AddInParameter(cmd, "@WholeSalePrice", DbType.Decimal, objProduct.WholeSalePrice);
                oDb.AddInParameter(cmd, "@RetailPrice", DbType.Decimal, objProduct.RetailPrice);
                oDb.AddInParameter(cmd, "@WholeSalePriceA", DbType.Decimal, objProduct.WholeSalePriceA);
                oDb.AddInParameter(cmd, "@RetailPriceA", DbType.Decimal, objProduct.RetailPriceA);
                oDb.AddInParameter(cmd, "@WholeSalePriceB", DbType.Decimal, objProduct.WholeSalePriceB);
                oDb.AddInParameter(cmd, "@RetailPriceB", DbType.Decimal, objProduct.RetailPriceB);
                oDb.AddInParameter(cmd, "@WholeSalePriceC", DbType.Decimal, objProduct.WholeSalePriceC);
                oDb.AddInParameter(cmd, "@RetailPriceC", DbType.Decimal, objProduct.RetailPriceC);
                oDb.AddInParameter(cmd, "@WholeSaleMargin", DbType.Decimal, objProduct.WholeSaleMargin);
                oDb.AddInParameter(cmd, "@RetailMargin", DbType.Decimal, objProduct.RetailMargin);
                oDb.AddInParameter(cmd, "@FK_UpdatedBy", DbType.Int32, objProduct.UpdatedBy);
                oDb.AddInParameter(cmd, "@Minimum_DiscountPercentage", DbType.Decimal, objProduct.MinDiscountPercent);
                oDb.AddInParameter(cmd, "@Maximum_DiscountPercentage", DbType.Decimal, objProduct.MaxDiscountPercent);
                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_PricingID"));
                    objProduct.PricingID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Master.Product AddProduct | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdatePricing(Entity.Pricing objProduct)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdatePricing(oDb, objProduct, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tPricing", "PK_PricingID", objProduct.PricingID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objProduct.UpdatedBy);
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
        private static bool UpdatePricing(Database oDb, Entity.Pricing objProduct, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_PRICING);
                oDb.AddInParameter(cmd, "@PK_PricingID", DbType.Int32, objProduct.PricingID);
                oDb.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, objProduct.Product.ProductID);
                oDb.AddInParameter(cmd, "@PurchasePrice", DbType.Decimal, objProduct.PurchasePrice);
                oDb.AddInParameter(cmd, "@MRP", DbType.Decimal, objProduct.MRP);
                oDb.AddInParameter(cmd, "@WholeSalePrice", DbType.Decimal, objProduct.WholeSalePrice);
                oDb.AddInParameter(cmd, "@RetailPrice", DbType.Decimal, objProduct.RetailPrice);
                oDb.AddInParameter(cmd, "@WholeSalePriceA", DbType.Decimal, objProduct.WholeSalePriceA);
                oDb.AddInParameter(cmd, "@RetailPriceA", DbType.Decimal, objProduct.RetailPriceA);
                oDb.AddInParameter(cmd, "@WholeSalePriceB", DbType.Decimal, objProduct.WholeSalePriceB);
                oDb.AddInParameter(cmd, "@RetailPriceB", DbType.Decimal, objProduct.RetailPriceB);
                oDb.AddInParameter(cmd, "@WholeSalePriceC", DbType.Decimal, objProduct.WholeSalePriceC);
                oDb.AddInParameter(cmd, "@RetailPriceC", DbType.Decimal, objProduct.RetailPriceC);
                oDb.AddInParameter(cmd, "@WholeSaleMargin", DbType.Decimal, objProduct.WholeSaleMargin);
                oDb.AddInParameter(cmd, "@RetailMargin", DbType.Decimal, objProduct.RetailMargin);
                oDb.AddInParameter(cmd, "@FK_UpdatedBy", DbType.Int32, objProduct.UpdatedBy);
                oDb.AddInParameter(cmd, "@Minimum_DiscountPercentage", DbType.Decimal, objProduct.MinDiscountPercent);
                oDb.AddInParameter(cmd, "@Maximum_DiscountPercentage", DbType.Decimal, objProduct.MaxDiscountPercent);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Pricing UpdatePricing | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteProduct(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeletePricing(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tPricing", "PK_PricingID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeletePricing(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_PRICING);
                oDb.AddInParameter(cmd, "@PK_PricingID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Pricing DeletePricing | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
