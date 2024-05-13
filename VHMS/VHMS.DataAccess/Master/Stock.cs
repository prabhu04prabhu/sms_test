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
    public class Stock
    {
        public static Collection<Entity.Stock> GetStock()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Stock> objList = new Collection<Entity.Stock>();
            Entity.Stock objStock = new Entity.Stock();
            Entity.Master.Product objProduct;
            Entity.Billing.Category objCategory;
            Entity.Branch objBranch;
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_STOCK);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objStock = new Entity.Stock();
                        objCategory = new Entity.Billing.Category();
                        objProduct = new Entity.Master.Product();
                        objBranch = new Entity.Branch();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objStock.StockID = Convert.ToInt32(drData["PK_StockID"]);
                        //objStock.StockDate = Convert.ToDateTime(drData["StockDate"]);
                       // objStock.sStockDate = objStock.StockDate.ToString("dd/MM/yyyy");
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);

                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objStock.Product = objProduct;

                        //objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        //objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        //objStock.Branch = objBranch;

                        //objCategory.CategoryID = Convert.ToInt32(drData["FK_CategoryID"]);
                        //objCategory.CategoryName = Convert.ToString(drData["CategoryName"]);
                        //objStock.Category = objCategory;

                        //objStock.NetWeight = Convert.ToDecimal(drData["NetWeight"]);
                        //objStock.WastagePercent = Convert.ToDecimal(drData["WastagePercent"]);
                        //objStock.Wastage = Convert.ToDecimal(drData["Wastage"]);
                        //objStock.TotalWeight = Convert.ToDecimal(drData["TotalWeight"]);
                        //objStock.Ratti = Convert.ToDecimal(drData["Ratti"]);
                        //objStock.PureWeight = Convert.ToDecimal(drData["PureWeight"]);
                        //objStock.Lacquer = Convert.ToDecimal(drData["Lacquer"]);
                        //objStock.Making = Convert.ToString(drData["Making"]);
                        //objStock.Design = Convert.ToString(drData["Design"]);
                        //objStock.Karat = Convert.ToString(drData["Karat"]);
                        objStock.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        //objStock.Pieces = Convert.ToInt32(drData["Pieces"]);
                        //objStock.Size = Convert.ToString(drData["Size"]);
                        //objStock.Description = Convert.ToString(drData["Description"]);
                        //objStock.PurchasePrice = Convert.ToDecimal(drData["PurchasePrice"]);
                        //objStock.SellingPrice = Convert.ToDecimal(drData["SellingPrice"]);
                        //objStock.StoneName = Convert.ToString(drData["StoneName"]);
                        //objStock.StoneQuantity = Convert.ToInt32(drData["StoneQuantity"]);
                        //objStock.StoneWeight = Convert.ToDecimal(drData["StoneWeight"]);
                        //objStock.StoneRate = Convert.ToDecimal(drData["StoneRate"]);
                        //objStock.StonePrice = Convert.ToDecimal(drData["StonePrice"]);
                        //objStock.StoneColor = Convert.ToString(drData["StoneColor"]);
                        //objStock.StoneCut = Convert.ToString(drData["StoneCut"]);
                        //objStock.StoneClarity = Convert.ToString(drData["StoneClarity"]);
                        //objStock.Status = Convert.ToString(drData["Status"]);


                        objList.Add(objStock);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Stock GetStock | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Barcode> BarcodeYarnStock(int ID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Barcode> objList = new Collection<Entity.Barcode>();
            Entity.Barcode objStock = new Entity.Barcode();
            Entity.Master.Product objProduct;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_BARCODELISTBYYARN);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, ID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objStock = new Entity.Barcode();
                        objProduct = new Entity.Master.Product();

                        objStock.BarcodeID = Convert.ToInt32(drData["PK_BarcodeID"]);
                        //objStock.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);
                        //objStock.sPurchaseDate = objStock.PurchaseDate.ToString("dd/MM/yyyy");
                        objStock.Barcodes = Convert.ToString(drData["Barcode"]);

                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        objStock.Product = objProduct;

                        objStock.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objStock.PurchasePrice = Convert.ToDecimal(drData["PurchasePrice"]);
                        //objStock.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        //objStock.BillNo = Convert.ToString(drData["BillNo"]);
                        //objStock.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        //objStock.sBillDate = objStock.BillDate.ToString("dd/MM/yyyy");

                        objList.Add(objStock);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Stock GetStock | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Stock> SearchStock(string ID, int iBranchID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Stock> objList = new Collection<Entity.Stock>();
            Entity.Stock objStock = new Entity.Stock();
            Entity.Master.Product objProduct;
            Entity.Billing.Category objCategory;
            Entity.Branch objBranch;
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_STOCK);
                db.AddInParameter(cmd, "@key", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, iBranchID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objStock = new Entity.Stock();
                        objCategory = new Entity.Billing.Category();
                        objProduct = new Entity.Master.Product();
                        objBranch = new Entity.Branch();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objStock.StockID = Convert.ToInt32(drData["PK_StockID"]);
                       // objStock.StockDate = Convert.ToDateTime(drData["StockDate"]);
                        //objStock.sStockDate = objStock.StockDate.ToString("dd/MM/yyyy");
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);

                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objStock.Product = objProduct;

                        //objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        //objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        //objStock.Branch = objBranch;

                        //objCategory.CategoryID = Convert.ToInt32(drData["FK_CategoryID"]);
                        //objCategory.CategoryName = Convert.ToString(drData["CategoryName"]);
                        //objStock.Category = objCategory;

                        //objStock.NetWeight = Convert.ToDecimal(drData["NetWeight"]);
                        //objStock.WastagePercent = Convert.ToDecimal(drData["WastagePercent"]);
                        //objStock.Wastage = Convert.ToDecimal(drData["Wastage"]);
                        //objStock.TotalWeight = Convert.ToDecimal(drData["TotalWeight"]);
                        //objStock.Ratti = Convert.ToDecimal(drData["Ratti"]);
                        //objStock.PureWeight = Convert.ToDecimal(drData["PureWeight"]);
                        //objStock.Lacquer = Convert.ToDecimal(drData["Lacquer"]);
                        //objStock.Making = Convert.ToString(drData["Making"]);
                        //objStock.Design = Convert.ToString(drData["Design"]);
                        //objStock.Karat = Convert.ToString(drData["Karat"]);
                        objStock.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        //objStock.Pieces = Convert.ToInt32(drData["Pieces"]);
                        //objStock.Size = Convert.ToString(drData["Size"]);
                        //objStock.Description = Convert.ToString(drData["Description"]);
                        //objStock.PurchasePrice = Convert.ToDecimal(drData["PurchasePrice"]);
                        //objStock.SellingPrice = Convert.ToDecimal(drData["SellingPrice"]);
                        //objStock.StoneName = Convert.ToString(drData["StoneName"]);
                        //objStock.StoneQuantity = Convert.ToInt32(drData["StoneQuantity"]);
                        //objStock.StoneWeight = Convert.ToDecimal(drData["StoneWeight"]);
                        //objStock.StoneRate = Convert.ToDecimal(drData["StoneRate"]);
                        //objStock.StonePrice = Convert.ToDecimal(drData["StonePrice"]);
                        //objStock.StoneColor = Convert.ToString(drData["StoneColor"]);
                        //objStock.StoneCut = Convert.ToString(drData["StoneCut"]);
                        //objStock.StoneClarity = Convert.ToString(drData["StoneClarity"]);
                        //objStock.Status = Convert.ToString(drData["Status"]);

                        objList.Add(objStock);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Stock GetStock | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Stock> GetTopStock(int iBranchID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Stock> objList = new Collection<Entity.Stock>();
            Entity.Stock objStock = new Entity.Stock();
            Entity.Master.Product objProduct;
            Entity.Billing.Category objCategory;
            Entity.Branch objBranch;
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPSTOCK);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, iBranchID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objStock = new Entity.Stock();
                        objCategory = new Entity.Billing.Category();
                        objProduct = new Entity.Master.Product();
                        objBranch = new Entity.Branch();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objStock.StockID = Convert.ToInt32(drData["PK_StockID"]);
                       // objStock.StockDate = Convert.ToDateTime(drData["StockDate"]);
                       // objStock.sStockDate = objStock.StockDate.ToString("dd/MM/yyyy");
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);

                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objStock.Product = objProduct;

                        //objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        //objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        //objStock.Branch = objBranch;

                        //objCategory.CategoryID = Convert.ToInt32(drData["FK_CategoryID"]);
                        //objCategory.CategoryName = Convert.ToString(drData["CategoryName"]);
                        //objStock.Category = objCategory;

                        //objStock.NetWeight = Convert.ToDecimal(drData["NetWeight"]);
                        //objStock.WastagePercent = Convert.ToDecimal(drData["WastagePercent"]);
                        //objStock.Wastage = Convert.ToDecimal(drData["Wastage"]);
                        //objStock.TotalWeight = Convert.ToDecimal(drData["TotalWeight"]);
                        //objStock.Ratti = Convert.ToDecimal(drData["Ratti"]);
                        //objStock.PureWeight = Convert.ToDecimal(drData["PureWeight"]);
                        //objStock.Lacquer = Convert.ToDecimal(drData["Lacquer"]);
                        //objStock.Making = Convert.ToString(drData["Making"]);
                        //objStock.Design = Convert.ToString(drData["Design"]);
                        //objStock.Karat = Convert.ToString(drData["Karat"]);
                        objStock.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        //objStock.Pieces = Convert.ToInt32(drData["Pieces"]);
                        //objStock.Size = Convert.ToString(drData["Size"]);
                        //objStock.Description = Convert.ToString(drData["Description"]);
                        //objStock.PurchasePrice = Convert.ToDecimal(drData["PurchasePrice"]);
                        //objStock.SellingPrice = Convert.ToDecimal(drData["SellingPrice"]);
                        //objStock.StoneName = Convert.ToString(drData["StoneName"]);
                        //objStock.StoneQuantity = Convert.ToInt32(drData["StoneQuantity"]);
                        //objStock.StoneWeight = Convert.ToDecimal(drData["StoneWeight"]);
                        //objStock.StoneRate = Convert.ToDecimal(drData["StoneRate"]);
                        //objStock.StonePrice = Convert.ToDecimal(drData["StonePrice"]);
                        //objStock.StoneColor = Convert.ToString(drData["StoneColor"]);
                        //objStock.StoneCut = Convert.ToString(drData["StoneCut"]);
                        //objStock.StoneClarity = Convert.ToString(drData["StoneClarity"]);
                        //objStock.Status = Convert.ToString(drData["Status"]);


                        objList.Add(objStock);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Stock GetStock | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Entity.Barcode GetBarcodeStockByID(string iStockID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Barcode objStock = new Entity.Barcode();
            Entity.Master.Product objProduct;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_BARCODESTOCKBYID);
                db.AddInParameter(cmd, "@Barcode", DbType.String, iStockID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objStock = new Entity.Barcode();
                        objProduct = new Entity.Master.Product();

                        objStock.BarcodeID = Convert.ToInt32(drData["PK_BarcodeID"]);
                        objStock.Barcodes = Convert.ToString(drData["Barcode"]);

                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        objStock.Product = objProduct;

                        objStock.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objStock.PurchasePrice = Convert.ToDecimal(drData["PurchasePrice"]);

                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Stock GetStockByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objStock;
        }
        public static Entity.Stock GetStockByID(int iStockID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Stock objStock = new Entity.Stock();
            Entity.Master.Product objProduct;
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PRODUCTSTOCK);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, iStockID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objStock = new Entity.Stock();
                        objProduct = new Entity.Master.Product();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();
                        objStock.StockID = Convert.ToInt32(drData["PK_StockID"]);
                        objStock.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objStock.RetailMinPrice = Convert.ToDecimal(drData["RetailMinPrice"]);
                        objStock.WholesaleMinPrice = Convert.ToDecimal(drData["WholesaleMinPrice"]);

                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Stock GetStockByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objStock;
        }

        public static Entity.Stock GetStockBarcodeByID(int iStockID,string iBarcode, string itype)
        {
            string sException = string.Empty;
            Database db;
            Entity.Stock objStock = new Entity.Stock();
            Entity.Master.Product objProduct;
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PRODUCTSTOCKBYBARCODE);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, iStockID);
                db.AddInParameter(cmd, "@Barcode", DbType.String, iBarcode);
                db.AddInParameter(cmd, "@type", DbType.String, itype);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objStock = new Entity.Stock();
                        objProduct = new Entity.Master.Product();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();
                        objStock.StockID = Convert.ToInt32(drData["PK_StockID"]);
                        objStock.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objStock.RetailMinPrice = Convert.ToDecimal(drData["RetailMinPrice"]);
                        objStock.WholesaleMinPrice = Convert.ToDecimal(drData["WholesaleMinPrice"]);

                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Stock GetStockByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objStock;
        }

        public static Entity.Stock GetStockByBarcode(string iStockID, string Status = null, int iBranchID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Stock objStock = new Entity.Stock();
            Entity.Master.Product objProduct;
            Entity.Billing.Category objCategory;
            Entity.Branch objBranch;
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_STOCKBYBARCODE);
                db.AddInParameter(cmd, "@PK_StockID", DbType.String, iStockID);
                db.AddInParameter(cmd, "@Status", DbType.String, Status);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, iBranchID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objStock = new Entity.Stock();
                        objCategory = new Entity.Billing.Category();
                        objProduct = new Entity.Master.Product();
                        objBranch = new Entity.Branch();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objStock.StockID = Convert.ToInt32(drData["PK_StockID"]);
                        //objStock.StockDate = Convert.ToDateTime(drData["StockDate"]);
                       // objStock.sStockDate = objStock.StockDate.ToString("dd/MM/yyyy");
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);

                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objStock.Product = objProduct;

                        //objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        //objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        //objStock.Branch = objBranch;

                        //objCategory.CategoryID = Convert.ToInt32(drData["FK_CategoryID"]);
                        //objCategory.CategoryName = Convert.ToString(drData["CategoryName"]);
                        //objStock.Category = objCategory;

                        //objStock.NetWeight = Convert.ToDecimal(drData["NetWeight"]);
                        //objStock.WastagePercent = Convert.ToDecimal(drData["WastagePercent"]);
                        //objStock.Wastage = Convert.ToDecimal(drData["Wastage"]);
                        //objStock.TotalWeight = Convert.ToDecimal(drData["TotalWeight"]);
                        //objStock.Ratti = Convert.ToDecimal(drData["Ratti"]);
                        //objStock.PureWeight = Convert.ToDecimal(drData["PureWeight"]);
                        //objStock.Lacquer = Convert.ToDecimal(drData["Lacquer"]);
                        //objStock.Making = Convert.ToString(drData["Making"]);
                        //objStock.Design = Convert.ToString(drData["Design"]);
                        //objStock.Karat = Convert.ToString(drData["Karat"]);
                        objStock.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        //objStock.Pieces = Convert.ToInt32(drData["Pieces"]);
                        //objStock.Size = Convert.ToString(drData["Size"]);
                        //objStock.Description = Convert.ToString(drData["Description"]);
                        //objStock.PurchasePrice = Convert.ToDecimal(drData["PurchasePrice"]);
                        //objStock.SellingPrice = Convert.ToDecimal(drData["SellingPrice"]);
                        //objStock.StoneName = Convert.ToString(drData["StoneName"]);
                        //objStock.StoneQuantity = Convert.ToInt32(drData["StoneQuantity"]);
                        //objStock.StoneWeight = Convert.ToDecimal(drData["StoneWeight"]);
                        //objStock.StoneRate = Convert.ToDecimal(drData["StoneRate"]);
                        //objStock.StonePrice = Convert.ToDecimal(drData["StonePrice"]);
                        //objStock.StoneColor = Convert.ToString(drData["StoneColor"]);
                        //objStock.StoneCut = Convert.ToString(drData["StoneCut"]);
                        //objStock.StoneClarity = Convert.ToString(drData["StoneClarity"]);
                        //objStock.Status = Convert.ToString(drData["Status"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Stock GetStockByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objStock;
        }

        public static Collection<Entity.Stock> GetMissingBarcode(string StockIDs, int iBranchID = 0,int Quantity=0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Stock> objList = new Collection<Entity.Stock>();
            Entity.Stock objStock = new Entity.Stock();
            Entity.Master.Product objProduct;
            Entity.Billing.Category objCategory;
            Entity.Branch objBranch;
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_MISSINGSTOCK);
                db.AddInParameter(cmd, "@StockIDs", DbType.String, StockIDs);
                db.AddInParameter(cmd, "@Quantity", DbType.Int32, Quantity);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, 0);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objStock = new Entity.Stock();
                        objCategory = new Entity.Billing.Category();
                        objProduct = new Entity.Master.Product();
                        objBranch = new Entity.Branch();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objStock.StockID = Convert.ToInt32(drData["PK_StockID"]);
                        // objStock.StockDate = Convert.ToDateTime(drData["StockDate"]);
                        //  objStock.sStockDate = objStock.StockDate.ToString("dd/MM/yyyy");
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);

                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objStock.Product = objProduct;

                        //objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        //objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        //objStock.Branch = objBranch;

                        //objCategory.CategoryID = Convert.ToInt32(drData["FK_CategoryID"]);
                        //objCategory.CategoryName = Convert.ToString(drData["CategoryName"]);
                        //objStock.Category = objCategory;

                        //objStock.NetWeight = Convert.ToDecimal(drData["NetWeight"]);
                        //objStock.WastagePercent = Convert.ToDecimal(drData["WastagePercent"]);
                        //objStock.Wastage = Convert.ToDecimal(drData["Wastage"]);
                        //objStock.TotalWeight = Convert.ToDecimal(drData["TotalWeight"]);
                        //objStock.Ratti = Convert.ToDecimal(drData["Ratti"]);
                        //objStock.PureWeight = Convert.ToDecimal(drData["PureWeight"]);
                        //objStock.Lacquer = Convert.ToDecimal(drData["Lacquer"]);
                        //objStock.Making = Convert.ToString(drData["Making"]);
                        //objStock.Design = Convert.ToString(drData["Design"]);
                        //objStock.Karat = Convert.ToString(drData["Karat"]);
                        objStock.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        //objStock.Pieces = Convert.ToInt32(drData["Pieces"]);
                        //objStock.Size = Convert.ToString(drData["Size"]);
                        //objStock.Description = Convert.ToString(drData["Description"]);
                        //objStock.PurchasePrice = Convert.ToDecimal(drData["PurchasePrice"]);
                        //objStock.SellingPrice = Convert.ToDecimal(drData["SellingPrice"]);
                        //objStock.StoneName = Convert.ToString(drData["StoneName"]);
                        //objStock.StoneQuantity = Convert.ToInt32(drData["StoneQuantity"]);
                        //objStock.StoneWeight = Convert.ToDecimal(drData["StoneWeight"]);
                        //objStock.StoneRate = Convert.ToDecimal(drData["StoneRate"]);
                        //objStock.StonePrice = Convert.ToDecimal(drData["StonePrice"]);
                        //objStock.StoneColor = Convert.ToString(drData["StoneColor"]);
                        //objStock.StoneCut = Convert.ToString(drData["StoneCut"]);
                        //objStock.StoneClarity = Convert.ToString(drData["StoneClarity"]);
                        //objStock.Status = Convert.ToString(drData["Status"]);

                        objList.Add(objStock);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Stock GetStock | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Stock GetStockByBarcodeQuotation(string iStockID, string Status = null, int iBranchID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Stock objStock = new Entity.Stock();
            Entity.Master.Product objProduct;
            Entity.Billing.Category objCategory;
            Entity.Branch objBranch;
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_STOCKBYBARCODEQUOTATION);
                db.AddInParameter(cmd, "@PK_StockID", DbType.String, iStockID);
                db.AddInParameter(cmd, "@Status", DbType.String, Status);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, iBranchID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objStock = new Entity.Stock();
                        objCategory = new Entity.Billing.Category();
                        objProduct = new Entity.Master.Product();
                        objBranch = new Entity.Branch();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objStock.StockID = Convert.ToInt32(drData["PK_StockID"]);
                        // objStock.StockDate = Convert.ToDateTime(drData["StockDate"]);
                        //objStock.sStockDate = objStock.StockDate.ToString("dd/MM/yyyy");
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);

                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objStock.Product = objProduct;

                        //objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        //objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        //objStock.Branch = objBranch;

                        //objCategory.CategoryID = Convert.ToInt32(drData["FK_CategoryID"]);
                        //objCategory.CategoryName = Convert.ToString(drData["CategoryName"]);
                        //objStock.Category = objCategory;

                        //objStock.NetWeight = Convert.ToDecimal(drData["NetWeight"]);
                        //objStock.WastagePercent = Convert.ToDecimal(drData["WastagePercent"]);
                        //objStock.Wastage = Convert.ToDecimal(drData["Wastage"]);
                        //objStock.TotalWeight = Convert.ToDecimal(drData["TotalWeight"]);
                        //objStock.Ratti = Convert.ToDecimal(drData["Ratti"]);
                        //objStock.PureWeight = Convert.ToDecimal(drData["PureWeight"]);
                        //objStock.Lacquer = Convert.ToDecimal(drData["Lacquer"]);
                        //objStock.Making = Convert.ToString(drData["Making"]);
                        //objStock.Design = Convert.ToString(drData["Design"]);
                        //objStock.Karat = Convert.ToString(drData["Karat"]);
                        objStock.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        //objStock.Pieces = Convert.ToInt32(drData["Pieces"]);
                        //objStock.Size = Convert.ToString(drData["Size"]);
                        //objStock.Description = Convert.ToString(drData["Description"]);
                        //objStock.PurchasePrice = Convert.ToDecimal(drData["PurchasePrice"]);
                        //objStock.SellingPrice = Convert.ToDecimal(drData["SellingPrice"]);
                        //objStock.StoneName = Convert.ToString(drData["StoneName"]);
                        //objStock.StoneQuantity = Convert.ToInt32(drData["StoneQuantity"]);
                        //objStock.StoneWeight = Convert.ToDecimal(drData["StoneWeight"]);
                        //objStock.StoneRate = Convert.ToDecimal(drData["StoneRate"]);
                        //objStock.StonePrice = Convert.ToDecimal(drData["StonePrice"]);
                        //objStock.StoneColor = Convert.ToString(drData["StoneColor"]);
                        //objStock.StoneCut = Convert.ToString(drData["StoneCut"]);
                        //objStock.StoneClarity = Convert.ToString(drData["StoneClarity"]);
                        //objStock.Status = Convert.ToString(drData["Status"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Stock GetStockByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objStock;
        }
        public static int AddStock(Entity.Stock objStock)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddStock(oDb, objStock, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tStock", "PK_StockID", objStock.StockID.ToString(), (char)Entity.Common.DatabaseAction.INSERT,1008);
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
        private static int AddStock(Database oDb, Entity.Stock objStock, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_STOCK);
                oDb.AddOutParameter(cmd, "@PK_StockID", DbType.Int32, objStock.StockID);
                //oDb.AddInParameter(cmd, "@StockDate", DbType.String, objStock.sStockDate);
                oDb.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, objStock.Product.ProductID);
                //oDb.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, objStock.Category.CategoryID);
                //oDb.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, objStock.Branch.BranchID);
                oDb.AddInParameter(cmd, "@SMSCode", DbType.String, objStock.Product.SMSCode);
                //oDb.AddInParameter(cmd, "@NetWeight", DbType.Decimal, objStock.NetWeight);
                //oDb.AddInParameter(cmd, "@Wastage", DbType.Decimal, objStock.Wastage);
                //oDb.AddInParameter(cmd, "@WastagePercent", DbType.Decimal, objStock.WastagePercent);
                //oDb.AddInParameter(cmd, "@TotalWeight", DbType.Decimal, objStock.TotalWeight);
                //oDb.AddInParameter(cmd, "@Ratti", DbType.Decimal, objStock.Ratti);
                //oDb.AddInParameter(cmd, "@PureWeight", DbType.Decimal, objStock.PureWeight);
                //oDb.AddInParameter(cmd, "@Lacquer", DbType.Decimal, objStock.Lacquer);
                //oDb.AddInParameter(cmd, "@Making", DbType.String, objStock.Making);
                //oDb.AddInParameter(cmd, "@Design", DbType.String, objStock.Design);
                //oDb.AddInParameter(cmd, "@Karat", DbType.String, objStock.Karat);
                oDb.AddInParameter(cmd, "@Quantity", DbType.Decimal, objStock.Quantity);
                //oDb.AddInParameter(cmd, "@Pieces", DbType.Int32, objStock.Pieces);
                //oDb.AddInParameter(cmd, "@Size", DbType.String, objStock.Size);
                //oDb.AddInParameter(cmd, "@Description", DbType.String, objStock.Description);
                //oDb.AddInParameter(cmd, "@PurchasePrice", DbType.Decimal, objStock.PurchasePrice);
                //oDb.AddInParameter(cmd, "@SellingPrice", DbType.Decimal, objStock.SellingPrice);
                //oDb.AddInParameter(cmd, "@StoneName", DbType.String, objStock.StoneName);
                //oDb.AddInParameter(cmd, "@StoneQuantity", DbType.Int32, objStock.StoneQuantity);
                //oDb.AddInParameter(cmd, "@StoneWeight", DbType.Decimal, objStock.StoneWeight);
                //oDb.AddInParameter(cmd, "@StoneRate", DbType.Decimal, objStock.StoneRate);
                //oDb.AddInParameter(cmd, "@StonePrice", DbType.Decimal, objStock.StonePrice);
                //oDb.AddInParameter(cmd, "@StoneColor", DbType.String, objStock.StoneColor);
                //oDb.AddInParameter(cmd, "@StoneCut", DbType.String, objStock.StoneCut);
                //oDb.AddInParameter(cmd, "@StoneClarity", DbType.String, objStock.StoneClarity);
                //oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objStock.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_StockID"));
                    objStock.StockID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Stock AddStock | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }

        public static int UpdateSplitWeight(int stockID, decimal iWeight, int iUserID)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = UpdateSplitWeight(oDb, stockID, iWeight, iUserID, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tStock", "PK_StockID", stockID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, iUserID);
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
        private static int UpdateSplitWeight(Database oDb, int stockID, decimal iWeight, int iUserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_STOCKSPLIT);
                oDb.AddOutParameter(cmd, "@PK_StockID", DbType.Int32, 0);
                oDb.AddInParameter(cmd, "@PK_StockID1", DbType.Int32, stockID);
                oDb.AddInParameter(cmd, "@NetWeight", DbType.Decimal, iWeight);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, iUserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_StockID"));
                    //objStock.StockID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Stock AddStock | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateStock(Entity.Stock objStock)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateStock(oDb, objStock, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tStock", "PK_StockID", objStock.StockID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, 1008);
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
        private static bool UpdateStock(Database oDb, Entity.Stock objStock, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_STOCK);
                oDb.AddInParameter(cmd, "@PK_StockID", DbType.Int32, objStock.StockID);
                //oDb.AddInParameter(cmd, "@StockDate", DbType.String, objStock.sStockDate);
                oDb.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, objStock.Product.ProductID);
                //oDb.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, objStock.Category.CategoryID);
                //oDb.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, objStock.Branch.BranchID);
                oDb.AddInParameter(cmd, "@SMSCode", DbType.String, objStock.Product.SMSCode);
                //oDb.AddInParameter(cmd, "@NetWeight", DbType.Decimal, objStock.NetWeight);
                //oDb.AddInParameter(cmd, "@Wastage", DbType.Decimal, objStock.Wastage);
                //oDb.AddInParameter(cmd, "@WastagePercent", DbType.Decimal, objStock.WastagePercent);
                //oDb.AddInParameter(cmd, "@TotalWeight", DbType.Decimal, objStock.TotalWeight);
                //oDb.AddInParameter(cmd, "@Ratti", DbType.Decimal, objStock.Ratti);
                //oDb.AddInParameter(cmd, "@PureWeight", DbType.Decimal, objStock.PureWeight);
                //oDb.AddInParameter(cmd, "@Lacquer", DbType.Decimal, objStock.Lacquer);
                //oDb.AddInParameter(cmd, "@Making", DbType.String, objStock.Making);
                //oDb.AddInParameter(cmd, "@Design", DbType.String, objStock.Design);
                //oDb.AddInParameter(cmd, "@Karat", DbType.String, objStock.Karat);
                oDb.AddInParameter(cmd, "@Quantity", DbType.Decimal, objStock.Quantity);
                //oDb.AddInParameter(cmd, "@Pieces", DbType.Int32, objStock.Pieces);
                //oDb.AddInParameter(cmd, "@Size", DbType.String, objStock.Size);
                //oDb.AddInParameter(cmd, "@Description", DbType.String, objStock.Description);
                //oDb.AddInParameter(cmd, "@PurchasePrice", DbType.Decimal, objStock.PurchasePrice);
                //oDb.AddInParameter(cmd, "@SellingPrice", DbType.Decimal, objStock.SellingPrice);
                //oDb.AddInParameter(cmd, "@StoneName", DbType.String, objStock.StoneName);
                //oDb.AddInParameter(cmd, "@StoneQuantity", DbType.Int32, objStock.StoneQuantity);
                //oDb.AddInParameter(cmd, "@StoneWeight", DbType.Decimal, objStock.StoneWeight);
                //oDb.AddInParameter(cmd, "@StoneRate", DbType.Decimal, objStock.StoneRate);
                //oDb.AddInParameter(cmd, "@StonePrice", DbType.Decimal, objStock.StonePrice);
                //oDb.AddInParameter(cmd, "@StoneColor", DbType.String, objStock.StoneColor);
                //oDb.AddInParameter(cmd, "@StoneCut", DbType.String, objStock.StoneCut);
                //oDb.AddInParameter(cmd, "@StoneClarity", DbType.String, objStock.StoneClarity);
                //oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objStock.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Stock UpdateStock | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteStock(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteStock(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tStock", "PK_StockID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteStock(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_STOCK);
                oDb.AddInParameter(cmd, "@PK_StockID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Stock DeleteStock | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
