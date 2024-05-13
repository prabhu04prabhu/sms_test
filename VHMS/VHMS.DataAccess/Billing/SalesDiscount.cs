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
    public class SalesDiscount
    {
        public static Collection<Entity.Billing.SalesDiscount> GetSalesDiscount(int ipatientID = 0, int iSupplierID = 0, int iFinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesDiscount> objList = new Collection<Entity.Billing.SalesDiscount>();
            Entity.Billing.SalesDiscount objSalesDiscount;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax; Entity.Billing.SalesEntry objSalesEntry;
            Entity.Billing.SalesEntry objAdjSales;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESDISCOUNT);
                db.AddInParameter(cmd, "@PK_SalesDiscountID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, iFinancialYearID);

                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesDiscount = new Entity.Billing.SalesDiscount();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objAdjSales = new Entity.Billing.SalesEntry();

                        objSalesDiscount.SalesDiscountID = Convert.ToInt32(drData["PK_SalesDiscountID"]);
                        objSalesDiscount.DiscountNo = Convert.ToString(drData["DiscountNo"]);
                        objSalesDiscount.DiscountDate = Convert.ToDateTime(drData["DiscountDate"]);
                        objSalesDiscount.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSalesDiscount.SalesDate = Convert.ToDateTime(drData["SalesDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName= Convert.ToString(drData["CustomerName"]);
                        objSalesDiscount.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesDiscount.Tax = objTax;

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesDiscount.SalesEntry = objSalesEntry;

                        objAdjSales.SalesEntryID = Convert.ToInt32(drData["FK_AdjSalesID"]);
                        objSalesDiscount.AdjSales = objAdjSales;

                        objSalesDiscount.Comments = Convert.ToString(drData["Comments"]);
                        objSalesDiscount.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesDiscount.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesDiscount.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesDiscount.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesDiscount.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objSalesDiscount.ImagePath = Convert.ToString(drData["ImagePath"]);
                        objSalesDiscount.sDiscountDate = objSalesDiscount.DiscountDate.ToString("dd/MM/yyyy") + " " + objSalesDiscount.CreatedOn.ToString("h:mm");
                        objSalesDiscount.sBillDate = objSalesDiscount.BillDate.ToString("dd/MM/yyyy") + " " + objSalesDiscount.CreatedOn.ToString("h:mm");
                        objSalesDiscount.Notes = Convert.ToString(drData["Notes"]);
                        objList.Add(objSalesDiscount);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesDiscount GetSalesDiscount | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.SalesDiscount> GetTopSalesDiscount(int ipatientID = 0, int iSupplierID = 0, int iFinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesDiscount> objList = new Collection<Entity.Billing.SalesDiscount>();
            Entity.Billing.SalesDiscount objSalesDiscount;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax; Entity.Billing.SalesEntry objSalesEntry;
            Entity.Billing.SalesEntry objAdjSales;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPSALESDISCOUNT);
                db.AddInParameter(cmd, "@PK_SalesDiscountID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, iFinancialYearID);

                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesDiscount = new Entity.Billing.SalesDiscount();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objAdjSales = new Entity.Billing.SalesEntry();

                        objSalesDiscount.SalesDiscountID = Convert.ToInt32(drData["PK_SalesDiscountID"]);
                        objSalesDiscount.DiscountNo = Convert.ToString(drData["DiscountNo"]);
                        objSalesDiscount.DiscountDate = Convert.ToDateTime(drData["DiscountDate"]);
                        objSalesDiscount.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSalesDiscount.SalesDate = Convert.ToDateTime(drData["SalesDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objSalesDiscount.Customer = objCustomer;
                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesDiscount.Tax = objTax;
                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesDiscount.SalesEntry = objSalesEntry;
                        objAdjSales.SalesEntryID = Convert.ToInt32(drData["FK_AdjSalesID"]);
                        objSalesDiscount.AdjSales = objAdjSales;
                        objSalesDiscount.Comments = Convert.ToString(drData["Comments"]);
                        objSalesDiscount.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesDiscount.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesDiscount.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesDiscount.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesDiscount.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objSalesDiscount.ImagePath = Convert.ToString(drData["ImagePath"]);
                        objSalesDiscount.sDiscountDate = objSalesDiscount.DiscountDate.ToString("dd/MM/yyyy") + " " + objSalesDiscount.CreatedOn.ToString("h:mm");
                        objSalesDiscount.sBillDate = objSalesDiscount.BillDate.ToString("dd/MM/yyyy") + " " + objSalesDiscount.CreatedOn.ToString("h:mm");
                        objSalesDiscount.sSalesDate = objSalesDiscount.SalesDate.ToString("dd/MM/yyyy");
                        objSalesDiscount.Notes = Convert.ToString(drData["Notes"]);
                        objList.Add(objSalesDiscount);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesDiscount GetSalesDiscount | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

       

        public static Collection<Entity.Billing.SalesDiscount> GetSearchSalesDiscount(string ipatientID, int iSupplierID = 0, int iFinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesDiscount> objList = new Collection<Entity.Billing.SalesDiscount>();
            Entity.Billing.SalesDiscount objSalesDiscount;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax; Entity.Billing.SalesEntry objSalesEntry;
            Entity.Billing.SalesEntry objAdjSales;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_SALESDISCOUNT);
                db.AddInParameter(cmd, "@PK_SalesDiscountID", DbType.String, ipatientID);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, iFinancialYearID);

                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesDiscount = new Entity.Billing.SalesDiscount();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objAdjSales = new Entity.Billing.SalesEntry();

                        objSalesDiscount.SalesDiscountID = Convert.ToInt32(drData["PK_SalesDiscountID"]);
                        objSalesDiscount.DiscountNo = Convert.ToString(drData["DiscountNo"]);
                        objSalesDiscount.DiscountDate = Convert.ToDateTime(drData["DiscountDate"]);
                        objSalesDiscount.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSalesDiscount.SalesDate = Convert.ToDateTime(drData["SalesDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objSalesDiscount.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesDiscount.Tax = objTax;

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesDiscount.SalesEntry = objSalesEntry;

                        objAdjSales.SalesEntryID = Convert.ToInt32(drData["FK_AdjSalesID"]);
                        objSalesDiscount.AdjSales = objAdjSales;

                        objSalesDiscount.Comments = Convert.ToString(drData["Comments"]);
                        objSalesDiscount.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesDiscount.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesDiscount.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesDiscount.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesDiscount.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objSalesDiscount.ImagePath = Convert.ToString(drData["ImagePath"]);
                        objSalesDiscount.sDiscountDate = objSalesDiscount.DiscountDate.ToString("dd/MM/yyyy") + " " + objSalesDiscount.CreatedOn.ToString("h:mm");
                        objSalesDiscount.sBillDate = objSalesDiscount.BillDate.ToString("dd/MM/yyyy") + " " + objSalesDiscount.CreatedOn.ToString("h:mm");
                        objSalesDiscount.Notes = Convert.ToString(drData["Notes"]);
                        objList.Add(objSalesDiscount);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesDiscount GetSalesDiscount | " + ex.ToString();
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

        public static Entity.Billing.SalesDiscount GetSalesDiscountByID(int iSalesDiscountID, int BillType, int iFinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
     
            Entity.Billing.SalesDiscount objSalesDiscount = new Entity.Billing.SalesDiscount();
            Entity.Customer objCustomer; Entity.Billing.Tax objTax; Entity.Billing.SalesEntry objSalesEntry;
            Entity.Billing.SalesEntry objAdjSales;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESDISCOUNT);
                db.AddInParameter(cmd, "@PK_SalesDiscountID", DbType.Int32, iSalesDiscountID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, iFinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesDiscount = new Entity.Billing.SalesDiscount();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objAdjSales = new Entity.Billing.SalesEntry();

                        objSalesDiscount.SalesDiscountID = Convert.ToInt32(drData["PK_SalesDiscountID"]);
                        objSalesDiscount.DiscountNo = Convert.ToString(drData["DiscountNo"]);
                        objSalesDiscount.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSalesDiscount.SalesDate = Convert.ToDateTime(drData["SalesDate"]);
                        objSalesDiscount.DiscountDate = Convert.ToDateTime(drData["DiscountDate"]);
                        objSalesDiscount.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objSalesDiscount.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesDiscount.Tax = objTax;

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesDiscount.SalesEntry = objSalesEntry;

                        objAdjSales.SalesEntryID = Convert.ToInt32(drData["FK_AdjSalesID"]);
                        objSalesDiscount.AdjSales = objAdjSales;

                        objSalesDiscount.Comments = Convert.ToString(drData["Comments"]);
                        objSalesDiscount.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesDiscount.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesDiscount.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesDiscount.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesDiscount.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objSalesDiscount.ImagePath = Convert.ToString(drData["ImagePath"]);
                        objSalesDiscount.sDiscountDate = objSalesDiscount.DiscountDate.ToString("dd/MM/yyyy") + " " + objSalesDiscount.CreatedOn.ToString("h:mm");
                        objSalesDiscount.sBillDate = objSalesDiscount.BillDate.ToString("dd/MM/yyyy") + " " + objSalesDiscount.CreatedOn.ToString("h:mm");
                        objSalesDiscount.sSalesDate = objSalesDiscount.SalesDate.ToString("dd/MM/yyyy");
                        objSalesDiscount.Notes = Convert.ToString(drData["Notes"]);


                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesDiscount GetSalesDiscountByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSalesDiscount;
        }
        public static int AddSalesDiscount(Entity.Billing.SalesDiscount objSalesDiscount)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_SALESDISCOUNT);
                db.AddOutParameter(cmd, "@PK_SalesDiscountID", DbType.Int32, objSalesDiscount.SalesDiscountID);
                db.AddInParameter(cmd, "@DiscountNo", DbType.String, objSalesDiscount.DiscountNo);
                db.AddInParameter(cmd, "@DiscountDate", DbType.String, objSalesDiscount.sDiscountDate);
                db.AddInParameter(cmd, "@BillDate", DbType.String, objSalesDiscount.sBillDate);
                db.AddInParameter(cmd, "@SalesDate", DbType.String, objSalesDiscount.sSalesDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objSalesDiscount.Customer.CustomerID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objSalesDiscount.Tax.TaxID);
                db.AddInParameter(cmd, "@FK_SalesEntryID", DbType.Int32, objSalesDiscount.SalesEntry.SalesEntryID);
                db.AddInParameter(cmd, "@FK_AdjSalesID", DbType.Int32, objSalesDiscount.AdjSales.SalesEntryID);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objSalesDiscount.TaxAmount);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objSalesDiscount.DiscountAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objSalesDiscount.TotalAmount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objSalesDiscount.Roundoff);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objSalesDiscount.CreatedBy.UserID);
                db.AddInParameter(cmd, "@Comments", DbType.String, objSalesDiscount.Comments);
                db.AddInParameter(cmd, "@ImagePath", DbType.String, objSalesDiscount.ImagePath);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objSalesDiscount.FinancialYear.FinancialYearID);
                db.AddInParameter(cmd, "@Notes", DbType.String, objSalesDiscount.Notes);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_SalesDiscountID"));
                
            }
            catch (Exception ex)
            {
                sException = "SalesDiscount AddSalesDiscount | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateSalesDiscount(Entity.Billing.SalesDiscount objSalesDiscount)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_SALESDISCOUNT);
                db.AddInParameter(cmd, "@PK_SalesDiscountID", DbType.Int32, objSalesDiscount.SalesDiscountID);
                db.AddInParameter(cmd, "@DiscountNo", DbType.String, objSalesDiscount.DiscountNo);
                db.AddInParameter(cmd, "@DiscountDate", DbType.String, objSalesDiscount.sDiscountDate);
                db.AddInParameter(cmd, "@BillDate", DbType.String, objSalesDiscount.sBillDate);
                db.AddInParameter(cmd, "@SalesDate", DbType.String, objSalesDiscount.sSalesDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objSalesDiscount.Customer.CustomerID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objSalesDiscount.Tax.TaxID);
                db.AddInParameter(cmd, "@FK_SalesEntryID", DbType.Int32, objSalesDiscount.SalesEntry.SalesEntryID);
                db.AddInParameter(cmd, "@FK_AdjSalesID", DbType.Int32, objSalesDiscount.AdjSales.SalesEntryID);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objSalesDiscount.TaxAmount);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objSalesDiscount.DiscountAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objSalesDiscount.TotalAmount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objSalesDiscount.Roundoff);
                db.AddInParameter(cmd, "@Comments", DbType.String, objSalesDiscount.Comments);
                db.AddInParameter(cmd, "@ImagePath", DbType.String, objSalesDiscount.ImagePath);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objSalesDiscount.FinancialYear.FinancialYearID);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objSalesDiscount.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@Notes", DbType.String, objSalesDiscount.Notes);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }

            catch (Exception ex)
            {
                sException = "SalesDiscount UpdateSalesDiscount| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteSalesDiscount(int iSalesDiscountId, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_SALESDISCOUNT);
                db.AddInParameter(cmd, "@PK_SalesDiscountID", DbType.Int32, iSalesDiscountId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "SalesDiscount DeleteSalesDiscount | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
  
    }


}
