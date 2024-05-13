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
    public class PurchaseDiscount
    {
        public static Collection<Entity.Billing.PurchaseDiscount> GetPurchaseDiscount(int ipatientID = 0, int iSupplierID = 0, int iFinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.PurchaseDiscount> objList = new Collection<Entity.Billing.PurchaseDiscount>();
            Entity.Billing.PurchaseDiscount objPurchaseDiscount;
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.Purchase objPurchase;
            Entity.Billing.Purchase objAdjPurchase;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASEDISCOUNT);
                db.AddInParameter(cmd, "@PK_PurchaseDiscountID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, iFinancialYearID);

                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchaseDiscount = new Entity.Billing.PurchaseDiscount();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchase = new Entity.Billing.Purchase();
                        objAdjPurchase = new Entity.Billing.Purchase();

                        objPurchaseDiscount.PurchaseDiscountID = Convert.ToInt32(drData["PK_PurchaseDiscountID"]);
                        objPurchaseDiscount.DiscountNo = Convert.ToString(drData["DiscountNo"]);
                        objPurchaseDiscount.DiscountDate = Convert.ToDateTime(drData["DiscountDate"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPurchaseDiscount.Supplier = objSupplier;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchaseDiscount.Tax = objTax;

                        objPurchase.PurchaseID = Convert.ToInt32(drData["FK_PurchaseID"]);
                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchaseDiscount.Purchase = objPurchase;

                        objAdjPurchase.PurchaseID = Convert.ToInt32(drData["FK_AdjPurchaseID"]);
                        objPurchaseDiscount.AdjPurchase = objAdjPurchase;

                        objPurchaseDiscount.Notes = Convert.ToString(drData["Notes"]);
                        objPurchaseDiscount.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchaseDiscount.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchaseDiscount.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchaseDiscount.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchaseDiscount.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objPurchaseDiscount.ImagePath = Convert.ToString(drData["ImagePath"]);
                        objPurchaseDiscount.sDiscountDate = objPurchaseDiscount.DiscountDate.ToString("dd/MM/yyyy") + " " + objPurchaseDiscount.CreatedOn.ToString("h:mm");
                       
                        objList.Add(objPurchaseDiscount);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "PurchaseDiscount GetPurchaseDiscount | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.PurchaseDiscount> GetTopPurchaseDiscount(int ipatientID = 0, int iSupplierID = 0, int iFinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.PurchaseDiscount> objList = new Collection<Entity.Billing.PurchaseDiscount>();
            Entity.Billing.PurchaseDiscount objPurchaseDiscount;
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.Purchase objPurchase;
            Entity.Billing.Purchase objAdjPurchase;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPPURCHASEDISCOUNT);
                db.AddInParameter(cmd, "@PK_PurchaseDiscountID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, iFinancialYearID);

                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchaseDiscount = new Entity.Billing.PurchaseDiscount();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchase = new Entity.Billing.Purchase();
                        objAdjPurchase = new Entity.Billing.Purchase();

                        objPurchaseDiscount.PurchaseDiscountID = Convert.ToInt32(drData["PK_PurchaseDiscountID"]);
                        objPurchaseDiscount.DiscountNo = Convert.ToString(drData["DiscountNo"]);
                        objPurchaseDiscount.DiscountDate = Convert.ToDateTime(drData["DiscountDate"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPurchaseDiscount.Supplier = objSupplier;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchaseDiscount.Tax = objTax;

                        objPurchase.PurchaseID = Convert.ToInt32(drData["FK_PurchaseID"]);
                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchaseDiscount.Purchase = objPurchase;

                        objAdjPurchase.PurchaseID = Convert.ToInt32(drData["FK_AdjPurchaseID"]);
                        objPurchaseDiscount.AdjPurchase = objAdjPurchase;

                        objPurchaseDiscount.Notes = Convert.ToString(drData["Notes"]);
                        objPurchaseDiscount.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchaseDiscount.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchaseDiscount.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchaseDiscount.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchaseDiscount.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objPurchaseDiscount.ImagePath = Convert.ToString(drData["ImagePath"]);
                        objPurchaseDiscount.sDiscountDate = objPurchaseDiscount.DiscountDate.ToString("dd/MM/yyyy") + " " + objPurchaseDiscount.CreatedOn.ToString("h:mm");

                        objList.Add(objPurchaseDiscount);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "PurchaseDiscount GetPurchaseDiscount | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.PurchaseDiscount> GetSearchPurchaseDiscount(string ipatientID, int iSupplierID = 0, int iFinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.PurchaseDiscount> objList = new Collection<Entity.Billing.PurchaseDiscount>();
            Entity.Billing.PurchaseDiscount objPurchaseDiscount;
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.Purchase objPurchase;
            Entity.Billing.Purchase objAdjPurchase;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_PURCHASEDISCOUNT);
                db.AddInParameter(cmd, "@PK_PurchaseDiscountID", DbType.String, ipatientID);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, iFinancialYearID);

                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchaseDiscount = new Entity.Billing.PurchaseDiscount();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchase = new Entity.Billing.Purchase();
                        objAdjPurchase = new Entity.Billing.Purchase();

                        objPurchaseDiscount.PurchaseDiscountID = Convert.ToInt32(drData["PK_PurchaseDiscountID"]);
                        objPurchaseDiscount.DiscountNo = Convert.ToString(drData["DiscountNo"]);
                        objPurchaseDiscount.DiscountDate = Convert.ToDateTime(drData["DiscountDate"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPurchaseDiscount.Supplier = objSupplier;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchaseDiscount.Tax = objTax;

                        objPurchase.PurchaseID = Convert.ToInt32(drData["FK_PurchaseID"]);
                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchaseDiscount.Purchase = objPurchase;

                        objAdjPurchase.PurchaseID = Convert.ToInt32(drData["FK_AdjPurchaseID"]);
                        objPurchaseDiscount.AdjPurchase = objAdjPurchase;

                        objPurchaseDiscount.Notes = Convert.ToString(drData["Notes"]);
                        objPurchaseDiscount.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchaseDiscount.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchaseDiscount.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchaseDiscount.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchaseDiscount.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objPurchaseDiscount.ImagePath = Convert.ToString(drData["ImagePath"]);
                        objPurchaseDiscount.sDiscountDate = objPurchaseDiscount.DiscountDate.ToString("dd/MM/yyyy") + " " + objPurchaseDiscount.CreatedOn.ToString("h:mm");

                        objList.Add(objPurchaseDiscount);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "PurchaseDiscount GetPurchaseDiscount | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        //public static Collection<Entity.Billing.PurchaseDiscount> SearchPurchaseReturn(string ID, int BillType, int iFinancialYearID = 0)
        //{
        //    string sException = string.Empty;
        //    Database db;
        //    DataSet dsList = null;
        //    Collection<Entity.Billing.PurchaseReturn> objList = new Collection<Entity.Billing.PurchaseReturn>();
        //    Entity.Billing.PurchaseReturn objPurchaseReturn;
        //    Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.Purchase objPurchase;

        //    try
        //    {
        //        db = VHMS.Entity.DBConnection.dbCon;
        //        DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASERETURNDYNAMIC);
        //        db.AddInParameter(cmd, "@PK_PurchaseReturnID", DbType.String, ID);
        //        db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, iFinancialYearID);
        //        db.AddInParameter(cmd, "@BillType", DbType.Boolean, BillType);
        //        dsList = db.ExecuteDataSet(cmd);

        //        if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow drData in dsList.Tables[0].Rows)
        //            {
        //                objPurchaseReturn = new Entity.Billing.PurchaseReturn();
        //                objSupplier = new Entity.Billing.Supplier();
        //                objTax = new Entity.Billing.Tax();
        //                objPurchase = new Entity.Billing.Purchase();

        //                objPurchaseReturn.PurchaseReturnID = Convert.ToInt32(drData["PK_PurchaseReturnID"]);
        //                objPurchaseReturn.ReturnNo = Convert.ToString(drData["ReturnNo"]);
        //                objPurchaseReturn.ReturnDate = Convert.ToDateTime(drData["ReturnDate"]);
        //                objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
        //                objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
        //                objPurchaseReturn.Supplier = objSupplier;

        //                objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
        //                objPurchaseReturn.Tax = objTax;

        //                objPurchase.PurchaseID = Convert.ToInt32(drData["FK_PurchaseID"]);
        //                objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
        //                objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
        //                objPurchaseReturn.Purchase = objPurchase;

        //                objPurchaseReturn.Notes = Convert.ToString(drData["Notes"]);
        //                objPurchaseReturn.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
        //                objPurchaseReturn.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
        //                objPurchaseReturn.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
        //                objPurchaseReturn.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
        //                objPurchaseReturn.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
        //                objPurchaseReturn.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
        //                objPurchaseReturn.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
        //                objPurchaseReturn.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
        //                objPurchaseReturn.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
        //                objPurchaseReturn.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
        //                objPurchaseReturn.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
        //                objPurchaseReturn.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
        //                objPurchaseReturn.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
        //                objPurchaseReturn.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
        //                objPurchaseReturn.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
        //                objPurchaseReturn.IsDiscount = Convert.ToBoolean(drData["IsDiscount"]);
        //                objPurchaseReturn.sReturnDate = objPurchaseReturn.ReturnDate.ToString("dd/MM/yyyy") + " " + objPurchaseReturn.CreatedOn.ToString("h:mm");
        //                objPurchaseReturn.CEmployeeName = Convert.ToString(drData["CEmployeeName"]);
        //                objPurchaseReturn.MEmployeeName = Convert.ToString(drData["MEmployeeName"]);

        //                objList.Add(objPurchaseReturn);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "PurchaseReturn GetPurchaseReturn | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return objList;
        //}

        public static Entity.Billing.PurchaseDiscount GetPurchaseDiscountByID(int iPurchaseDiscountID, int BillType, int iFinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.PurchaseDiscount objPurchaseDiscount = new Entity.Billing.PurchaseDiscount();
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.Purchase objPurchase;
            Entity.Billing.Purchase objAdjPurchase;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASEDISCOUNT);
                db.AddInParameter(cmd, "@PK_PurchaseDiscountID", DbType.Int32, iPurchaseDiscountID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, iFinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchaseDiscount = new Entity.Billing.PurchaseDiscount();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchase = new Entity.Billing.Purchase();
                        objAdjPurchase = new Entity.Billing.Purchase();

                        objPurchaseDiscount.PurchaseDiscountID = Convert.ToInt32(drData["PK_PurchaseDiscountID"]);
                        objPurchaseDiscount.DiscountNo = Convert.ToString(drData["DiscountNo"]);
                        objPurchaseDiscount.DiscountDate = Convert.ToDateTime(drData["DiscountDate"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPurchaseDiscount.Supplier = objSupplier;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchaseDiscount.Tax = objTax;

                        objPurchase.PurchaseID = Convert.ToInt32(drData["FK_PurchaseID"]);
                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchaseDiscount.Purchase = objPurchase;

                        objAdjPurchase.PurchaseID = Convert.ToInt32(drData["FK_AdjPurchaseID"]);
                        objPurchaseDiscount.AdjPurchase = objAdjPurchase;

                        objPurchaseDiscount.Notes = Convert.ToString(drData["Notes"]);
                        objPurchaseDiscount.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchaseDiscount.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchaseDiscount.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchaseDiscount.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchaseDiscount.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objPurchaseDiscount.ImagePath = Convert.ToString(drData["ImagePath"]);
                        objPurchaseDiscount.sDiscountDate = objPurchaseDiscount.DiscountDate.ToString("dd/MM/yyyy") + " " + objPurchaseDiscount.CreatedOn.ToString("h:mm");


                    }
                }
            }
            catch (Exception ex)
            {
                sException = "PurchaseReturn GetPurchaseDiscountByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objPurchaseDiscount;
        }
        public static int AddPurchaseDiscount(Entity.Billing.PurchaseDiscount objpurchaseDiscount)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_PURCHASEDISCOUNT);
                db.AddOutParameter(cmd, "@PK_PurchaseDiscountID", DbType.Int32, objpurchaseDiscount.PurchaseDiscountID);
                db.AddInParameter(cmd, "@DiscountNo", DbType.String, objpurchaseDiscount.DiscountNo);
                db.AddInParameter(cmd, "@DiscountDate", DbType.String, objpurchaseDiscount.sDiscountDate);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, objpurchaseDiscount.Supplier.SupplierID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objpurchaseDiscount.Tax.TaxID);
                db.AddInParameter(cmd, "@FK_PurchaseID", DbType.Int32, objpurchaseDiscount.Purchase.PurchaseID);
                db.AddInParameter(cmd, "@FK_AdjPurchaseID", DbType.Int32, objpurchaseDiscount.AdjPurchase.PurchaseID);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objpurchaseDiscount.TaxAmount);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objpurchaseDiscount.DiscountAmount);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objpurchaseDiscount.NetAmount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objpurchaseDiscount.Roundoff);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objpurchaseDiscount.CreatedBy.UserID);
                db.AddInParameter(cmd, "@Notes", DbType.String, objpurchaseDiscount.Notes);
                db.AddInParameter(cmd, "@ImagePath", DbType.String, objpurchaseDiscount.ImagePath);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objpurchaseDiscount.FinancialYear.FinancialYearID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_PurchaseDiscountID"));
                
            }
            catch (Exception ex)
            {
                sException = "PurchaseDiscount AddPurchaseDiscount | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdatePurchaseDiscount(Entity.Billing.PurchaseDiscount objPurchaseDiscount)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_PURCHASEDISCOUNT);
                db.AddInParameter(cmd, "@PK_PurchaseDiscountID", DbType.Int32, objPurchaseDiscount.PurchaseDiscountID);
                db.AddInParameter(cmd, "@DiscountNo", DbType.String, objPurchaseDiscount.DiscountNo);
                db.AddInParameter(cmd, "@DiscountDate", DbType.String, objPurchaseDiscount.sDiscountDate);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, objPurchaseDiscount.Supplier.SupplierID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objPurchaseDiscount.Tax.TaxID);
                db.AddInParameter(cmd, "@FK_PurchaseID", DbType.Int32, objPurchaseDiscount.Purchase.PurchaseID);
                db.AddInParameter(cmd, "@FK_AdjPurchaseID", DbType.Int32, objPurchaseDiscount.AdjPurchase.PurchaseID);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objPurchaseDiscount.TaxAmount);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objPurchaseDiscount.DiscountAmount);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objPurchaseDiscount.NetAmount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objPurchaseDiscount.Roundoff);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objPurchaseDiscount.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@Notes", DbType.String, objPurchaseDiscount.Notes);
                db.AddInParameter(cmd, "@ImagePath", DbType.String, objPurchaseDiscount.ImagePath);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objPurchaseDiscount.FinancialYear.FinancialYearID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }

            catch (Exception ex)
            {
                sException = "PurchaseDiscount UpdatePurchaseDiscount| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeletePurchaseDiscount(int iPurchaseReturnId, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_PURCHASEDISCOUNT);
                db.AddInParameter(cmd, "@PK_PurchaseDiscountID", DbType.Int32, iPurchaseReturnId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "PurchaseReturn DeletePurchaseReturn | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
  
    }


}
