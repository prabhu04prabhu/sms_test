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
    public class Purchase
    {
        #region SelectPurchase
        public static Collection<Entity.Billing.Purchase> GetPurchase(int ipatientID = 0, int BillType = 1,int FK_FinancialYearID=0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Purchase> objList = new Collection<Entity.Billing.Purchase>();
            Entity.Billing.Purchase objPurchase;
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.PurchaseOrder objPurchaseOrder;
            Entity.Transport objTransport;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASE);
                db.AddInParameter(cmd, "@PK_PurchaseID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, BillType);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase = new Entity.Billing.Purchase();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();
                        objTransport = new Entity.Transport();
                        objPurchase.PurchaseID = Convert.ToInt32(drData["PK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);

                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchase.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPurchase.Supplier = objSupplier;

                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objPurchase.Transport= objTransport;


                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchase.Tax = objTax;

                        objPurchaseOrder.PurchaseOrderID = Convert.ToInt32(drData["FK_PurchaseOrderID"]);
                        objPurchaseOrder.PurchaseOrderNo = Convert.ToString(drData["PurchaseOrderNo"]);
                        objPurchase.PurchaseOrder = objPurchaseOrder;
                        objPurchase.BillType = Convert.ToBoolean(drData["BillType"]);
                        objPurchase.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objPurchase.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchase.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchase.PaymentDiscount = Convert.ToDecimal(drData["PaymentDiscount"]);
                        objPurchase.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchase.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchase.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchase.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objPurchase.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchase.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objPurchase.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objPurchase.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);
                        objPurchase.CourierCharges = Convert.ToDecimal(drData["CourierCharges"]);
                        objPurchase.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchase.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchase.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objPurchase.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objPurchase.PaymentDiscountPercent = Convert.ToDecimal(drData["PaymentDiscountPercent"]);
                        objPurchase.Dis_amt_Type = Convert.ToString(drData["Dis_amt_Type"]);
                        objPurchase.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objPurchase.sPurchaseDate = objPurchase.PurchaseDate.ToString("dd/MM/yyyy") + " " + objPurchase.CreatedOn.ToString("h:mm");
                        objPurchase.sBillDate = objPurchase.BillDate.ToString("dd/MM/yyyy");
                        objPurchase.VerifiedBy = Convert.ToInt32(drData["VerifiedBy"]);
                        objPurchase.ConfirmedBy = Convert.ToInt32(drData["ConfirmedBy"]);
                        objPurchase.VerifiedName = Convert.ToString(drData["VerifiedName"]);
                        objPurchase.ConfirmedName = Convert.ToString(drData["ConfirmedName"]);
                        objPurchase.Comments = Convert.ToString(drData["Comments"]);
                        objPurchase.Status = Convert.ToString(drData["Status"]);
                        objPurchase.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objPurchase.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objPurchase.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objPurchase.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objPurchase.IsYarn = Convert.ToBoolean(drData["IsYarn"]);

                        objList.Add(objPurchase);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchase | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Purchase> GetTopPurchase(int ipatientID = 0, int BillType = 1, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Purchase> objList = new Collection<Entity.Billing.Purchase>();
            Entity.Billing.Purchase objPurchase;
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.PurchaseOrder objPurchaseOrder;
            Entity.Transport objTransport;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPPURCHASE);
                db.AddInParameter(cmd, "@PK_PurchaseID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, BillType);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase = new Entity.Billing.Purchase();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();
                        objTransport = new Entity.Transport();

                        objPurchase.PurchaseID = Convert.ToInt32(drData["PK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);

                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchase.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objSupplier.Days = Convert.ToInt32(drData["Days"]);
                        objPurchase.Supplier = objSupplier;

                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objPurchase.Transport = objTransport;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchase.Tax = objTax;

                        objPurchaseOrder.PurchaseOrderID = Convert.ToInt32(drData["FK_PurchaseOrderID"]);
                        objPurchaseOrder.PurchaseOrderNo = Convert.ToString(drData["PurchaseOrderNo"]);
                        objPurchase.PurchaseOrder = objPurchaseOrder;
                        objPurchase.DueDays = Convert.ToInt32(drData["DueDays"]);
                        objPurchase.BillType = Convert.ToBoolean(drData["BillType"]);
                        objPurchase.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objPurchase.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchase.PaymentDiscount = Convert.ToDecimal(drData["PaymentDiscount"]);
                        objPurchase.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchase.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchase.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchase.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);
                        objPurchase.CourierCharges = Convert.ToDecimal(drData["CourierCharges"]);
                        objPurchase.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchase.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objPurchase.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchase.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objPurchase.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objPurchase.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchase.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchase.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objPurchase.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objPurchase.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objPurchase.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objPurchase.sPurchaseDate = objPurchase.PurchaseDate.ToString("dd/MM/yyyy") + " " + objPurchase.CreatedOn.ToString("h:mm");
                        objPurchase.sBillDate = objPurchase.BillDate.ToString("dd/MM/yyyy");
                        objPurchase.VerifiedBy = Convert.ToInt32(drData["VerifiedBy"]);
                        objPurchase.ConfirmedBy = Convert.ToInt32(drData["ConfirmedBy"]);
                        objPurchase.VerifiedName = Convert.ToString(drData["VerifiedName"]);
                        objPurchase.ConfirmedName = Convert.ToString(drData["ConfirmedName"]);
                        objPurchase.Comments = Convert.ToString(drData["Comments"]);
                        objPurchase.Status = Convert.ToString(drData["Status"]);
                        objPurchase.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objPurchase.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objPurchase.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objPurchase.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objPurchase.IsYarn = Convert.ToBoolean(drData["IsYarn"]);

                        objList.Add(objPurchase);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchase | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Purchase> GetAdjustTDSPurchase(int ipatientID = 0, int iSupplierID =0,int iTDSID=0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Purchase> objList = new Collection<Entity.Billing.Purchase>();
            Entity.Billing.Purchase objPurchase;
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.PurchaseOrder objPurchaseOrder;
            Entity.Transport objTransport;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ADJUSTTDSPURCHASEENTRY);
                db.AddInParameter(cmd, "@PK_PurchaseID", DbType.Int32, iTDSID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase = new Entity.Billing.Purchase();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();
                        objTransport = new Entity.Transport();

                        objPurchase.PurchaseID = Convert.ToInt32(drData["PK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);

                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchase.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objSupplier.Days = Convert.ToInt32(drData["Days"]);
                        objPurchase.Supplier = objSupplier;

                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objPurchase.Transport = objTransport;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchase.Tax = objTax;

                        objPurchaseOrder.PurchaseOrderID = Convert.ToInt32(drData["FK_PurchaseOrderID"]);
                        objPurchaseOrder.PurchaseOrderNo = Convert.ToString(drData["PurchaseOrderNo"]);
                        objPurchase.PurchaseOrder = objPurchaseOrder;
                        objPurchase.DueDays = Convert.ToInt32(drData["DueDays"]);
                        objPurchase.BillType = Convert.ToBoolean(drData["BillType"]);
                        objPurchase.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objPurchase.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchase.PaymentDiscount = Convert.ToDecimal(drData["PaymentDiscount"]);
                        objPurchase.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchase.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchase.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchase.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);
                        objPurchase.CourierCharges = Convert.ToDecimal(drData["CourierCharges"]);
                        objPurchase.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchase.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objPurchase.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchase.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objPurchase.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objPurchase.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchase.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchase.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objPurchase.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objPurchase.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objPurchase.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objPurchase.sPurchaseDate = objPurchase.PurchaseDate.ToString("dd/MM/yyyy") + " " + objPurchase.CreatedOn.ToString("h:mm");
                        objPurchase.sBillDate = objPurchase.BillDate.ToString("dd/MM/yyyy");
                        objPurchase.VerifiedBy = Convert.ToInt32(drData["VerifiedBy"]);
                        objPurchase.ConfirmedBy = Convert.ToInt32(drData["ConfirmedBy"]);
                        objPurchase.VerifiedName = Convert.ToString(drData["VerifiedName"]);
                        objPurchase.ConfirmedName = Convert.ToString(drData["ConfirmedName"]);
                        objPurchase.Comments = Convert.ToString(drData["Comments"]);
                        objPurchase.Status = Convert.ToString(drData["Status"]);
                        objPurchase.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objPurchase.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objPurchase.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objPurchase.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objPurchase.IsYarn = Convert.ToBoolean(drData["IsYarn"]);

                        objList.Add(objPurchase);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchase | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Purchase> GetTDSPurchase(int ipatientID = 0, int iSupplierID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Purchase> objList = new Collection<Entity.Billing.Purchase>();
            Entity.Billing.Purchase objPurchase;
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.PurchaseOrder objPurchaseOrder;
            Entity.Transport objTransport;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TDSPURCHASEENTRY);
                db.AddInParameter(cmd, "@PK_PurchaseID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iSupplierID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase = new Entity.Billing.Purchase();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();
                        objTransport = new Entity.Transport();

                        objPurchase.PurchaseID = Convert.ToInt32(drData["PK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);

                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchase.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objSupplier.Days = Convert.ToInt32(drData["Days"]);
                        objPurchase.Supplier = objSupplier;

                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objPurchase.Transport = objTransport;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchase.Tax = objTax;

                        objPurchaseOrder.PurchaseOrderID = Convert.ToInt32(drData["FK_PurchaseOrderID"]);
                        objPurchaseOrder.PurchaseOrderNo = Convert.ToString(drData["PurchaseOrderNo"]);
                        objPurchase.PurchaseOrder = objPurchaseOrder;
                        objPurchase.DueDays = Convert.ToInt32(drData["DueDays"]);
                        objPurchase.BillType = Convert.ToBoolean(drData["BillType"]);
                        objPurchase.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objPurchase.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchase.PaymentDiscount = Convert.ToDecimal(drData["PaymentDiscount"]);
                        objPurchase.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchase.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchase.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchase.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);
                        objPurchase.CourierCharges = Convert.ToDecimal(drData["CourierCharges"]);
                        objPurchase.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchase.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objPurchase.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchase.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objPurchase.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objPurchase.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchase.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchase.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objPurchase.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objPurchase.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objPurchase.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objPurchase.sPurchaseDate = objPurchase.PurchaseDate.ToString("dd/MM/yyyy") + " " + objPurchase.CreatedOn.ToString("h:mm");
                        objPurchase.sBillDate = objPurchase.BillDate.ToString("dd/MM/yyyy");
                        objPurchase.VerifiedBy = Convert.ToInt32(drData["VerifiedBy"]);
                        objPurchase.ConfirmedBy = Convert.ToInt32(drData["ConfirmedBy"]);
                        objPurchase.VerifiedName = Convert.ToString(drData["VerifiedName"]);
                        objPurchase.ConfirmedName = Convert.ToString(drData["ConfirmedName"]);
                        objPurchase.Comments = Convert.ToString(drData["Comments"]);
                        objPurchase.Status = Convert.ToString(drData["Status"]);
                        objPurchase.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objPurchase.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objPurchase.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objPurchase.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objPurchase.IsYarn = Convert.ToBoolean(drData["IsYarn"]);

                        objList.Add(objPurchase);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchase | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Entity.Billing.Purchase GetPurchaseInvoice(string InvoiceNo)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Entity.Billing.Purchase objPurchase = new Entity.Billing.Purchase();
          //  Entity.Billing.Purchase objPurchase;
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.PurchaseOrder objPurchaseOrder;
            Entity.Transport objTransport;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASEINVOICE);
                db.AddInParameter(cmd, "@PurchaseNo", DbType.String, InvoiceNo);
                //db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase = new Entity.Billing.Purchase();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();
                        objTransport = new Entity.Transport();

                        objPurchase.PurchaseID = Convert.ToInt32(drData["PK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);

                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchase.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objSupplier.Days = Convert.ToInt32(drData["Days"]);
                        objPurchase.Supplier = objSupplier;

                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objPurchase.Transport = objTransport;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchase.Tax = objTax;

                        objPurchaseOrder.PurchaseOrderID = Convert.ToInt32(drData["FK_PurchaseOrderID"]);
                        objPurchaseOrder.PurchaseOrderNo = Convert.ToString(drData["PurchaseOrderNo"]);
                        objPurchase.PurchaseOrder = objPurchaseOrder;
                      //  objPurchase.DueDays = Convert.ToInt32(drData["DueDays"]);
                        objPurchase.BillType = Convert.ToBoolean(drData["BillType"]);
                        objPurchase.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objPurchase.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchase.PaymentDiscount = Convert.ToDecimal(drData["PaymentDiscount"]);
                        objPurchase.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchase.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchase.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchase.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);
                        objPurchase.CourierCharges = Convert.ToDecimal(drData["CourierCharges"]);
                        objPurchase.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchase.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objPurchase.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchase.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objPurchase.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objPurchase.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchase.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchase.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objPurchase.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objPurchase.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objPurchase.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objPurchase.sPurchaseDate = objPurchase.PurchaseDate.ToString("dd/MM/yyyy") + " " + objPurchase.CreatedOn.ToString("h:mm");
                        objPurchase.sBillDate = objPurchase.BillDate.ToString("dd/MM/yyyy");
                        objPurchase.VerifiedBy = Convert.ToInt32(drData["VerifiedBy"]);
                        objPurchase.ConfirmedBy = Convert.ToInt32(drData["ConfirmedBy"]);
                        objPurchase.VerifiedName = Convert.ToString(drData["VerifiedName"]);
                        objPurchase.ConfirmedName = Convert.ToString(drData["ConfirmedName"]);
                        objPurchase.Comments = Convert.ToString(drData["Comments"]);
                        objPurchase.Status = Convert.ToString(drData["Status"]);
                        objPurchase.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objPurchase.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objPurchase.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objPurchase.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objPurchase.IsYarn = Convert.ToBoolean(drData["IsYarn"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchase | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objPurchase;
        }
        public static Collection<Entity.Billing.Purchase> GetTopPurchaseSupplierWise(int iSupplierID = 0, int BillType = 1, int DC = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Purchase> objList = new Collection<Entity.Billing.Purchase>();
            Entity.Billing.Purchase objPurchase;
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.PurchaseOrder objPurchaseOrder;
            Entity.Transport objTransport;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPPURCHASESUPPLIERWISE);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, BillType);
                db.AddInParameter(cmd, "@IsDc", DbType.Boolean, DC);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase = new Entity.Billing.Purchase();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();
                        objTransport = new Entity.Transport();
                        objPurchase.PurchaseID = Convert.ToInt32(drData["PK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);

                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchase.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objSupplier.Days = Convert.ToInt32(drData["Days"]);
                        objPurchase.Supplier = objSupplier;

                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objPurchase.Transport = objTransport;
                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchase.Tax = objTax;

                        objPurchaseOrder.PurchaseOrderID = Convert.ToInt32(drData["FK_PurchaseOrderID"]);
                        objPurchaseOrder.PurchaseOrderNo = Convert.ToString(drData["PurchaseOrderNo"]);
                        objPurchase.PurchaseOrder = objPurchaseOrder;
                        objPurchase.DueDays = Convert.ToInt32(drData["DueDays"]);
                        objPurchase.BillType = Convert.ToBoolean(drData["BillType"]);
                        objPurchase.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objPurchase.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchase.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchase.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchase.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchase.PaymentDiscount = Convert.ToDecimal(drData["PaymentDiscount"]);
                        objPurchase.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchase.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objPurchase.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchase.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objPurchase.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objPurchase.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchase.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchase.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);
                        objPurchase.CourierCharges = Convert.ToDecimal(drData["CourierCharges"]);
                        objPurchase.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objPurchase.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objPurchase.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objPurchase.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objPurchase.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objPurchase.ModifiedOn = Convert.ToDateTime(drData["ModifiedOn"]);
                        objPurchase.sPurchaseDate = objPurchase.PurchaseDate.ToString("dd/MM/yyyy") + " " + objPurchase.ModifiedOn.ToString("h:mm");
                        objPurchase.sBillDate = objPurchase.BillDate.ToString("dd/MM/yyyy");
                        objPurchase.VerifiedBy = Convert.ToInt32(drData["VerifiedBy"]);
                        objPurchase.ConfirmedBy = Convert.ToInt32(drData["ConfirmedBy"]);
                        objPurchase.VerifiedName = Convert.ToString(drData["VerifiedName"]);
                        objPurchase.ConfirmedName = Convert.ToString(drData["ConfirmedName"]);
                        objPurchase.Comments = Convert.ToString(drData["Comments"]);
                        objPurchase.Status = Convert.ToString(drData["Status"]);
                        objPurchase.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objPurchase.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objPurchase.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objPurchase.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objPurchase.IsYarn = Convert.ToBoolean(drData["IsYarn"]);

                        objPurchase.CEmployeeName = Convert.ToString(drData["CEmployeeName"]);
                        objPurchase.MEmployeeName = Convert.ToString(drData["MEmployeeName"]);    

                        objList.Add(objPurchase);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchase | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Purchase> GetTopPurchasePending(int ipatientID = 0, int BillType = 1, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Purchase> objList = new Collection<Entity.Billing.Purchase>();
            Entity.Billing.Purchase objPurchase;
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.PurchaseOrder objPurchaseOrder;
            Entity.Transport objTransport;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPPURCHASEPENDING);
                db.AddInParameter(cmd, "@PK_PurchaseID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, BillType);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase = new Entity.Billing.Purchase();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();

                        objTransport = new Entity.Transport();
                        objPurchase.PurchaseID = Convert.ToInt32(drData["PK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);

                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchase.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPurchase.Supplier = objSupplier;

                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objPurchase.Transport = objTransport;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchase.Tax = objTax;

                        objPurchaseOrder.PurchaseOrderID = Convert.ToInt32(drData["FK_PurchaseOrderID"]);
                        objPurchaseOrder.PurchaseOrderNo = Convert.ToString(drData["PurchaseOrderNo"]);
                        objPurchase.PurchaseOrder = objPurchaseOrder;
                        objPurchase.BillType = Convert.ToBoolean(drData["BillType"]);
                        objPurchase.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objPurchase.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchase.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchase.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchase.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);
                        objPurchase.CourierCharges = Convert.ToDecimal(drData["CourierCharges"]);
                        objPurchase.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchase.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchase.PaymentDiscount = Convert.ToDecimal(drData["PaymentDiscount"]);
                        objPurchase.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objPurchase.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchase.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objPurchase.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objPurchase.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchase.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchase.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objPurchase.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objPurchase.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objPurchase.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objPurchase.sPurchaseDate = objPurchase.PurchaseDate.ToString("dd/MM/yyyy") + " " + objPurchase.CreatedOn.ToString("h:mm");
                        objPurchase.sBillDate = objPurchase.BillDate.ToString("dd/MM/yyyy");
                        objPurchase.VerifiedBy = Convert.ToInt32(drData["VerifiedBy"]);
                        objPurchase.ConfirmedBy = Convert.ToInt32(drData["ConfirmedBy"]);
                        objPurchase.VerifiedName = Convert.ToString(drData["VerifiedName"]);
                        objPurchase.ConfirmedName = Convert.ToString(drData["ConfirmedName"]);
                        objPurchase.Comments = Convert.ToString(drData["Comments"]);
                        objPurchase.Status = Convert.ToString(drData["Status"]);
                        objPurchase.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objPurchase.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objPurchase.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objPurchase.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objPurchase.IsYarn = Convert.ToBoolean(drData["IsYarn"]);
                        objPurchase.CEmployeeName = Convert.ToString(drData["CEmployeeName"]);
                        objPurchase.MEmployeeName = Convert.ToString(drData["MEmployeeName"]);

                        objList.Add(objPurchase);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchase | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.Purchase> GetPendingPurchase(int ipatientID = 0, int BillType = 1, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Purchase> objList = new Collection<Entity.Billing.Purchase>();
            Entity.Billing.Purchase objPurchase;
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.PurchaseOrder objPurchaseOrder;
            Entity.Transport objTransport;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PENDINGPURCHASE);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, BillType);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase = new Entity.Billing.Purchase();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();
                        objTransport = new Entity.Transport();

                        objPurchase.PurchaseID = Convert.ToInt32(drData["PK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);
                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchase.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPurchase.Supplier = objSupplier;
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objPurchase.Transport = objTransport;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchase.Tax = objTax;

                        objPurchaseOrder.PurchaseOrderID = Convert.ToInt32(drData["FK_PurchaseOrderID"]);
                        objPurchaseOrder.PurchaseOrderNo = Convert.ToString(drData["PurchaseOrderNo"]);
                        objPurchase.PurchaseOrder = objPurchaseOrder;
                        objPurchase.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objPurchase.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchase.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchase.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchase.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchase.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchase.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objPurchase.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchase.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objPurchase.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objPurchase.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchase.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchase.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objPurchase.PaymentDiscountPercent = Convert.ToDecimal(drData["PaymentDiscountPercent"]);                
                        objPurchase.PaymentDiscount = Convert.ToDecimal(drData["PaymentDiscount"]);
                        objPurchase.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);
                        objPurchase.CourierCharges = Convert.ToDecimal(drData["CourierCharges"]);
                        objPurchase.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objPurchase.ReturnAmount = Convert.ToDecimal(drData["ReturnAmount"]);
                        objPurchase.PurchaseDiscount = Convert.ToDecimal(drData["PurchaseDiscount"]);
                        objPurchase.TDSAmount = Convert.ToDecimal(drData["TDSAmount"]);
                        objPurchase.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objPurchase.sPurchaseDate = objPurchase.PurchaseDate.ToString("dd/MM/yyyy") + " " + objPurchase.CreatedOn.ToString("h:mm");
                        objPurchase.sBillDate = objPurchase.BillDate.ToString("dd/MM/yyyy");
                        objPurchase.VerifiedBy = Convert.ToInt32(drData["VerifiedBy"]);
                        objPurchase.ConfirmedBy = Convert.ToInt32(drData["ConfirmedBy"]);
                        objPurchase.VerifiedName = Convert.ToString(drData["VerifiedName"]);
                        objPurchase.ConfirmedName = Convert.ToString(drData["ConfirmedName"]);
                        objPurchase.Comments = Convert.ToString(drData["Comments"]);
                        objPurchase.Status = Convert.ToString(drData["Status"]);
                        objPurchase.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objPurchase.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objPurchase.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objPurchase.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objPurchase.IsYarn = Convert.ToBoolean(drData["IsYarn"]);

                        objList.Add(objPurchase);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchase | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.Purchase> GetBillNo(int iSupplierID = 0, int BillType = 1, int PurchaseReturnID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Purchase> objList = new Collection<Entity.Billing.Purchase>();
            Entity.Billing.Purchase objPurchase;
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.PurchaseOrder objPurchaseOrder;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASE_GETBILLNO);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, BillType);
                db.AddInParameter(cmd, "@FK_PurchaseReturnID", DbType.Int32, PurchaseReturnID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase = new Entity.Billing.Purchase();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();

                        objPurchase.PurchaseID = Convert.ToInt32(drData["PK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);
                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchase.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objPurchase.sPurchaseDate = objPurchase.PurchaseDate.ToString("dd/MM/yyyy") + " " + objPurchase.CreatedOn.ToString("h:mm");
                        objPurchase.sBillDate = objPurchase.BillDate.ToString("dd/MM/yyyy");

                        objList.Add(objPurchase);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchase | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.Purchase> GetPurchaseDiscountBillNo(int iSupplierID = 0, int BillType = 1, int PurchaseReturnID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Purchase> objList = new Collection<Entity.Billing.Purchase>();
            Entity.Billing.Purchase objPurchase;
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.PurchaseOrder objPurchaseOrder;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASE_GETBILLNO_DISCOUNT);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, BillType);
                db.AddInParameter(cmd, "@FK_PurchaseReturnID", DbType.Int32, PurchaseReturnID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase = new Entity.Billing.Purchase();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();

                        objPurchase.PurchaseID = Convert.ToInt32(drData["PK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);
                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchase.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objPurchase.sPurchaseDate = objPurchase.PurchaseDate.ToString("dd/MM/yyyy") + " " + objPurchase.CreatedOn.ToString("h:mm");
                        objPurchase.sBillDate = objPurchase.BillDate.ToString("dd/MM/yyyy");

                        objList.Add(objPurchase);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchase | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.Purchase> GetPendingPurchaseDiscountBillNo(int iSupplierID = 0, int BillType = 1, int PurchaseReturnID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Purchase> objList = new Collection<Entity.Billing.Purchase>();
            Entity.Billing.Purchase objPurchase;
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.PurchaseOrder objPurchaseOrder;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASE_PENDINGGETBILLNO_DISCOUNT);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, BillType);
                db.AddInParameter(cmd, "@FK_PurchaseReturnID", DbType.Int32, PurchaseReturnID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase = new Entity.Billing.Purchase();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();

                        objPurchase.PurchaseID = Convert.ToInt32(drData["PK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);
                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchase.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objPurchase.sPurchaseDate = objPurchase.PurchaseDate.ToString("dd/MM/yyyy") + " " + objPurchase.CreatedOn.ToString("h:mm");
                        objPurchase.sBillDate = objPurchase.BillDate.ToString("dd/MM/yyyy");

                        objList.Add(objPurchase);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchase | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.Purchase> SearchPurchase(string ID, int BillType = 1, int DC = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Purchase> objList = new Collection<Entity.Billing.Purchase>();
            Entity.Billing.Purchase objPurchase;
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.PurchaseOrder objPurchaseOrder;
            Entity.Transport objTransport;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASEDYNAMIC);
                db.AddInParameter(cmd, "@PK_PurchaseID", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, BillType);
                db.AddInParameter(cmd, "@IsDC", DbType.Boolean, DC);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase = new Entity.Billing.Purchase();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();
                        objTransport = new Entity.Transport();

                        objPurchase.PurchaseID = Convert.ToInt32(drData["PK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);
                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchase.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPurchase.Supplier = objSupplier;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchase.Tax = objTax;

                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objPurchase.Transport = objTransport;


                        objPurchaseOrder.PurchaseOrderID = Convert.ToInt32(drData["FK_PurchaseOrderID"]);
                        objPurchaseOrder.PurchaseOrderNo = Convert.ToString(drData["PurchaseOrderNo"]);
                        objPurchase.PurchaseOrder = objPurchaseOrder;

                        objPurchase.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objPurchase.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchase.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchase.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchase.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchase.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchase.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);
                        objPurchase.CourierCharges = Convert.ToDecimal(drData["CourierCharges"]);
                        objPurchase.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objPurchase.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchase.PaymentDiscount = Convert.ToDecimal(drData["PaymentDiscount"]);
                        objPurchase.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objPurchase.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objPurchase.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchase.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchase.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objPurchase.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objPurchase.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objPurchase.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        if (objPurchase.ModifiedBy== null)
                        {
                            objPurchase.sPurchaseDate = objPurchase.PurchaseDate.ToString("dd/MM/yyyy") + " " + objPurchase.CreatedOn.ToString("h:mm");
                        }
                        else
                        {
                            objPurchase.sPurchaseDate = objPurchase.PurchaseDate.ToString("dd/MM/yyyy") + " " + objPurchase.ModifiedOn.ToString("h:mm");
                        }
                        objPurchase.sBillDate = objPurchase.BillDate.ToString("dd/MM/yyyy");
                        objPurchase.VerifiedBy = Convert.ToInt32(drData["VerifiedBy"]);
                        objPurchase.ConfirmedBy = Convert.ToInt32(drData["ConfirmedBy"]);
                        objPurchase.VerifiedName = Convert.ToString(drData["VerifiedName"]);
                        objPurchase.ConfirmedName = Convert.ToString(drData["ConfirmedName"]);
                        objPurchase.Comments = Convert.ToString(drData["Comments"]);
                        objPurchase.Status = Convert.ToString(drData["Status"]);
                        objPurchase.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objPurchase.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objPurchase.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objPurchase.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objPurchase.IsYarn = Convert.ToBoolean(drData["IsYarn"]);
                        objPurchase.CEmployeeName = Convert.ToString(drData["CEmployeeName"]);
                        objPurchase.MEmployeeName = Convert.ToString(drData["MEmployeeName"]);

                        objList.Add(objPurchase);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchase | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.Purchase> SearchPurchasePending(string ID, int BillType = 1, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Purchase> objList = new Collection<Entity.Billing.Purchase>();
            Entity.Billing.Purchase objPurchase;
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.PurchaseOrder objPurchaseOrder;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_PURCHASEPENDING);
                db.AddInParameter(cmd, "@PK_PurchaseID", DbType.String, ID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, BillType);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase = new Entity.Billing.Purchase();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();

                        objPurchase.PurchaseID = Convert.ToInt32(drData["PK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);
                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchase.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPurchase.Supplier = objSupplier;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchase.Tax = objTax;

                        objPurchaseOrder.PurchaseOrderID = Convert.ToInt32(drData["FK_PurchaseOrderID"]);
                        objPurchaseOrder.PurchaseOrderNo = Convert.ToString(drData["PurchaseOrderNo"]);
                        objPurchase.PurchaseOrder = objPurchaseOrder;

                        objPurchase.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objPurchase.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchase.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchase.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchase.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchase.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchase.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);
                        objPurchase.CourierCharges = Convert.ToDecimal(drData["CourierCharges"]);
                        objPurchase.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objPurchase.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchase.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objPurchase.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objPurchase.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchase.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchase.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objPurchase.PaymentDiscount = Convert.ToDecimal(drData["PaymentDiscount"]);
                        objPurchase.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objPurchase.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objPurchase.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objPurchase.sPurchaseDate = objPurchase.PurchaseDate.ToString("dd/MM/yyyy") + " " + objPurchase.CreatedOn.ToString("h:mm");
                        objPurchase.sBillDate = objPurchase.BillDate.ToString("dd/MM/yyyy");
                        objPurchase.VerifiedBy = Convert.ToInt32(drData["VerifiedBy"]);
                        objPurchase.ConfirmedBy = Convert.ToInt32(drData["ConfirmedBy"]);
                        objPurchase.VerifiedName = Convert.ToString(drData["VerifiedName"]);
                        objPurchase.ConfirmedName = Convert.ToString(drData["ConfirmedName"]);
                        objPurchase.Comments = Convert.ToString(drData["Comments"]);
                        objPurchase.Status = Convert.ToString(drData["Status"]);
                        objPurchase.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objPurchase.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objPurchase.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objPurchase.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objPurchase.IsYarn = Convert.ToBoolean(drData["IsYarn"]);
                        objPurchase.CEmployeeName = Convert.ToString(drData["CEmployeeName"]);
                        objPurchase.MEmployeeName = Convert.ToString(drData["MEmployeeName"]);

                        objList.Add(objPurchase);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchase | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.Purchase GetPurchaseByID(int iPurchaseID, int BillType = 1, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Purchase objPurchase = new Entity.Billing.Purchase();
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.PurchaseOrder objPurchaseOrder;
            Entity.Transport objTransport;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASE);
                db.AddInParameter(cmd, "@PK_PurchaseID", DbType.Int32, iPurchaseID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, BillType);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase = new Entity.Billing.Purchase();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();
                        objTransport = new Entity.Transport();

                        objPurchase.PurchaseID = Convert.ToInt32(drData["PK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);
                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchase.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objPurchase.sPurchaseDate = objPurchase.PurchaseDate.ToString("dd/MM/yyyy");
                        objPurchase.sBillDate = objPurchase.BillDate.ToString("dd/MM/yyyy");

                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPurchase.Supplier = objSupplier;

                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objPurchase.Transport = objTransport;


                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchase.Tax = objTax;

                        objPurchaseOrder.PurchaseOrderID = Convert.ToInt32(drData["FK_PurchaseOrderID"]);
                        objPurchaseOrder.PurchaseOrderNo = Convert.ToString(drData["PurchaseOrderNo"]);
                        objPurchase.PurchaseOrder = objPurchaseOrder;

                        objPurchase.IsDC = Convert.ToBoolean(drData["IsDC"]);
                        objPurchase.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objPurchase.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchase.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchase.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchase.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchase.PaymentDiscount = Convert.ToDecimal(drData["PaymentDiscount"]);
                        objPurchase.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchase.PaymentDiscountPercent = Convert.ToDecimal(drData["PaymentDiscountPercent"]);
                        objPurchase.Dis_amt_Type = Convert.ToString(drData["Dis_amt_Type"]);
                        objPurchase.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objPurchase.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchase.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objPurchase.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objPurchase.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchase.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchase.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objPurchase.VerifiedBy = Convert.ToInt32(drData["VerifiedBy"]);
                        objPurchase.ConfirmedBy = Convert.ToInt32(drData["ConfirmedBy"]);
                        objPurchase.VerifiedName = Convert.ToString(drData["VerifiedName"]);
                        objPurchase.ConfirmedName = Convert.ToString(drData["ConfirmedName"]);
                        objPurchase.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);
                        objPurchase.CourierCharges = Convert.ToDecimal(drData["CourierCharges"]);
                        objPurchase.Comments = Convert.ToString(drData["Comments"]);
                        objPurchase.Status = Convert.ToString(drData["Status"]);
                        objPurchase.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objPurchase.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objPurchase.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objPurchase.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objPurchase.IsYarn = Convert.ToBoolean(drData["IsYarn"]);
                        objPurchase.PurchaseDiscount = Convert.ToDecimal(drData["PurchaseDiscount"]);
                        objPurchase.PurchaseTrans = PurchaseTrans.GetPurchaseTransByPurchaseID(objPurchase.PurchaseID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchaseByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objPurchase;
        }

        public static Entity.Billing.Purchase GetTDSPurchaseByID(int iPurchaseID, int BillType = 1)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Purchase objPurchase = new Entity.Billing.Purchase();
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.PurchaseOrder objPurchaseOrder;
            Entity.Transport objTransport;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TDSPURCHASEBYID);
                db.AddInParameter(cmd, "@PK_PurchaseID", DbType.Int32, iPurchaseID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, BillType);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase = new Entity.Billing.Purchase();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();
                        objTransport = new Entity.Transport();

                        objPurchase.PurchaseID = Convert.ToInt32(drData["PK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);
                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchase.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objPurchase.sPurchaseDate = objPurchase.PurchaseDate.ToString("dd/MM/yyyy");
                        objPurchase.sBillDate = objPurchase.BillDate.ToString("dd/MM/yyyy");

                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPurchase.Supplier = objSupplier;

                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objPurchase.Transport = objTransport;


                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchase.Tax = objTax;

                        objPurchaseOrder.PurchaseOrderID = Convert.ToInt32(drData["FK_PurchaseOrderID"]);
                        objPurchaseOrder.PurchaseOrderNo = Convert.ToString(drData["PurchaseOrderNo"]);
                        objPurchase.PurchaseOrder = objPurchaseOrder;

                        objPurchase.IsDC = Convert.ToBoolean(drData["IsDC"]);
                        objPurchase.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objPurchase.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchase.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchase.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchase.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchase.PaymentDiscount = Convert.ToDecimal(drData["PaymentDiscount"]);
                        objPurchase.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchase.PaymentDiscountPercent = Convert.ToDecimal(drData["PaymentDiscountPercent"]);
                        objPurchase.Dis_amt_Type = Convert.ToString(drData["Dis_amt_Type"]);
                        objPurchase.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objPurchase.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchase.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objPurchase.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objPurchase.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchase.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchase.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objPurchase.VerifiedBy = Convert.ToInt32(drData["VerifiedBy"]);
                        objPurchase.ConfirmedBy = Convert.ToInt32(drData["ConfirmedBy"]);
                        objPurchase.VerifiedName = Convert.ToString(drData["VerifiedName"]);
                        objPurchase.ConfirmedName = Convert.ToString(drData["ConfirmedName"]);
                        objPurchase.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);
                        objPurchase.CourierCharges = Convert.ToDecimal(drData["CourierCharges"]);
                        objPurchase.Comments = Convert.ToString(drData["Comments"]);
                        objPurchase.Status = Convert.ToString(drData["Status"]);
                        objPurchase.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objPurchase.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objPurchase.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objPurchase.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objPurchase.IsYarn = Convert.ToBoolean(drData["IsYarn"]);
                        objPurchase.PurchaseDiscount = Convert.ToDecimal(drData["PurchaseDiscount"]);
                        objPurchase.PurchaseTrans = PurchaseTrans.GetPurchaseTransByPurchaseID(objPurchase.PurchaseID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchaseByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objPurchase;
        }
        #endregion SelectPurchase

        #region SelectDC
        public static Collection<Entity.Billing.Purchase> GetDCPurchase(int ipatientID = 0, int BillType = 1, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Purchase> objList = new Collection<Entity.Billing.Purchase>();
            Entity.Billing.Purchase objPurchase;
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.PurchaseOrder objPurchaseOrder;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DCPURCHASE);
                db.AddInParameter(cmd, "@PK_PurchaseID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, BillType);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase = new Entity.Billing.Purchase();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();

                        objPurchase.PurchaseID = Convert.ToInt32(drData["PK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);

                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchase.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPurchase.Supplier = objSupplier;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchase.Tax = objTax;

                        objPurchaseOrder.PurchaseOrderID = Convert.ToInt32(drData["FK_PurchaseOrderID"]);
                        objPurchaseOrder.PurchaseOrderNo = Convert.ToString(drData["PurchaseOrderNo"]);
                        objPurchase.PurchaseOrder = objPurchaseOrder;
                        objPurchase.BillType = Convert.ToBoolean(drData["BillType"]);
                        objPurchase.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objPurchase.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchase.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchase.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchase.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchase.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchase.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objPurchase.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchase.PaymentDiscount = Convert.ToDecimal(drData["PaymentDiscount"]);
                        objPurchase.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objPurchase.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objPurchase.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchase.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchase.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);
                        objPurchase.CourierCharges = Convert.ToDecimal(drData["CourierCharges"]);
                        objPurchase.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objPurchase.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objPurchase.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objPurchase.sPurchaseDate = objPurchase.PurchaseDate.ToString("dd/MM/yyyy") + " " + objPurchase.CreatedOn.ToString("h:mm");
                        objPurchase.sBillDate = objPurchase.BillDate.ToString("dd/MM/yyyy");
                        objPurchase.VerifiedBy = Convert.ToInt32(drData["VerifiedBy"]);
                        objPurchase.ConfirmedBy = Convert.ToInt32(drData["ConfirmedBy"]);
                        objPurchase.VerifiedName = Convert.ToString(drData["VerifiedName"]);
                        objPurchase.ConfirmedName = Convert.ToString(drData["ConfirmedName"]);
                        objPurchase.Comments = Convert.ToString(drData["Comments"]);
                        objPurchase.Status = Convert.ToString(drData["Status"]);
                        objPurchase.DocumentPath = Convert.ToString(drData["DocumentPath"]);

                        objList.Add(objPurchase);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchase | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Purchase> GetTopDCPurchase(int ipatientID = 0, int BillType = 1, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Purchase> objList = new Collection<Entity.Billing.Purchase>();
            Entity.Billing.Purchase objPurchase;
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.PurchaseOrder objPurchaseOrder;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPDCPURCHASE);
                db.AddInParameter(cmd, "@PK_PurchaseID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, BillType);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase = new Entity.Billing.Purchase();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();

                        objPurchase.PurchaseID = Convert.ToInt32(drData["PK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);

                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchase.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPurchase.Supplier = objSupplier;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchase.Tax = objTax;

                        objPurchaseOrder.PurchaseOrderID = Convert.ToInt32(drData["FK_PurchaseOrderID"]);
                        objPurchaseOrder.PurchaseOrderNo = Convert.ToString(drData["PurchaseOrderNo"]);
                        objPurchase.PurchaseOrder = objPurchaseOrder;
                        objPurchase.BillType = Convert.ToBoolean(drData["BillType"]);
                        objPurchase.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objPurchase.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchase.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchase.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchase.PaymentDiscount = Convert.ToDecimal(drData["PaymentDiscount"]);
                        objPurchase.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchase.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchase.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objPurchase.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchase.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objPurchase.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objPurchase.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchase.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchase.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objPurchase.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objPurchase.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objPurchase.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objPurchase.sPurchaseDate = objPurchase.PurchaseDate.ToString("dd/MM/yyyy") + " " + objPurchase.CreatedOn.ToString("h:mm");
                        objPurchase.sBillDate = objPurchase.BillDate.ToString("dd/MM/yyyy");
                        objPurchase.VerifiedBy = Convert.ToInt32(drData["VerifiedBy"]);
                        objPurchase.ConfirmedBy = Convert.ToInt32(drData["ConfirmedBy"]);
                        objPurchase.VerifiedName = Convert.ToString(drData["VerifiedName"]);
                        objPurchase.ConfirmedName = Convert.ToString(drData["ConfirmedName"]);
                        objPurchase.Comments = Convert.ToString(drData["Comments"]);
                        objPurchase.Status = Convert.ToString(drData["Status"]);
                        objPurchase.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objList.Add(objPurchase);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchase | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.Purchase> GetPendingDCPurchase(int ipatientID = 0, int BillType = 1, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Purchase> objList = new Collection<Entity.Billing.Purchase>();
            Entity.Billing.Purchase objPurchase;
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.PurchaseOrder objPurchaseOrder;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PENDINGDCPURCHASE);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, BillType);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase = new Entity.Billing.Purchase();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();

                        objPurchase.PurchaseID = Convert.ToInt32(drData["PK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);
                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchase.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPurchase.Supplier = objSupplier;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchase.Tax = objTax;

                        objPurchaseOrder.PurchaseOrderID = Convert.ToInt32(drData["FK_PurchaseOrderID"]);
                        objPurchaseOrder.PurchaseOrderNo = Convert.ToString(drData["PurchaseOrderNo"]);
                        objPurchase.PurchaseOrder = objPurchaseOrder;
                        objPurchase.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objPurchase.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchase.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchase.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchase.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchase.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchase.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objPurchase.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchase.PaymentDiscount = Convert.ToDecimal(drData["PaymentDiscount"]);
                        objPurchase.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objPurchase.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objPurchase.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchase.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);
                        objPurchase.CourierCharges = Convert.ToDecimal(drData["CourierCharges"]);
                        objPurchase.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchase.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objPurchase.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objPurchase.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objPurchase.sPurchaseDate = objPurchase.PurchaseDate.ToString("dd/MM/yyyy") + " " + objPurchase.CreatedOn.ToString("h:mm");
                        objPurchase.sBillDate = objPurchase.BillDate.ToString("dd/MM/yyyy");
                        objPurchase.VerifiedBy = Convert.ToInt32(drData["VerifiedBy"]);
                        objPurchase.ConfirmedBy = Convert.ToInt32(drData["ConfirmedBy"]);
                        objPurchase.VerifiedName = Convert.ToString(drData["VerifiedName"]);
                        objPurchase.ConfirmedName = Convert.ToString(drData["ConfirmedName"]);
                        objPurchase.Comments = Convert.ToString(drData["Comments"]);
                        objPurchase.Status = Convert.ToString(drData["Status"]);
                        objPurchase.DocumentPath = Convert.ToString(drData["DocumentPath"]);

                        objList.Add(objPurchase);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchase | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.Purchase> SearchDCPurchase(string ID, int BillType = 1, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Purchase> objList = new Collection<Entity.Billing.Purchase>();
            Entity.Billing.Purchase objPurchase;
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.PurchaseOrder objPurchaseOrder;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DCPURCHASEDYNAMIC);
                db.AddInParameter(cmd, "@PK_PurchaseID", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, BillType);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase = new Entity.Billing.Purchase();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();

                        objPurchase.PurchaseID = Convert.ToInt32(drData["PK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);
                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchase.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPurchase.Supplier = objSupplier;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchase.Tax = objTax;

                        objPurchaseOrder.PurchaseOrderID = Convert.ToInt32(drData["FK_PurchaseOrderID"]);
                        objPurchaseOrder.PurchaseOrderNo = Convert.ToString(drData["PurchaseOrderNo"]);
                        objPurchase.PurchaseOrder = objPurchaseOrder;

                        objPurchase.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objPurchase.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchase.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchase.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchase.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchase.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchase.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objPurchase.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchase.PaymentDiscount = Convert.ToDecimal(drData["PaymentDiscount"]);
                        objPurchase.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objPurchase.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objPurchase.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchase.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchase.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objPurchase.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objPurchase.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objPurchase.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objPurchase.sPurchaseDate = objPurchase.PurchaseDate.ToString("dd/MM/yyyy") + " " + objPurchase.CreatedOn.ToString("h:mm");
                        objPurchase.sBillDate = objPurchase.BillDate.ToString("dd/MM/yyyy");
                        objPurchase.VerifiedBy = Convert.ToInt32(drData["VerifiedBy"]);
                        objPurchase.ConfirmedBy = Convert.ToInt32(drData["ConfirmedBy"]);
                        objPurchase.VerifiedName = Convert.ToString(drData["VerifiedName"]);
                        objPurchase.ConfirmedName = Convert.ToString(drData["ConfirmedName"]);
                        objPurchase.Comments = Convert.ToString(drData["Comments"]);
                        objPurchase.Status = Convert.ToString(drData["Status"]);
                        objPurchase.DocumentPath = Convert.ToString(drData["DocumentPath"]);

                        objList.Add(objPurchase);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchase | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.Purchase GetDCPurchaseByID(int iPurchaseID, int BillType = 1, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Purchase objPurchase = new Entity.Billing.Purchase();
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.PurchaseOrder objPurchaseOrder;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DCPURCHASE);
                db.AddInParameter(cmd, "@PK_PurchaseID", DbType.Int32, iPurchaseID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, BillType);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase = new Entity.Billing.Purchase();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();

                        objPurchase.PurchaseID = Convert.ToInt32(drData["PK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);
                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchase.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objPurchase.sPurchaseDate = objPurchase.PurchaseDate.ToString("dd/MM/yyyy");
                        objPurchase.sBillDate = objPurchase.BillDate.ToString("dd/MM/yyyy");

                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPurchase.Supplier = objSupplier;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchase.Tax = objTax;

                        objPurchaseOrder.PurchaseOrderID = Convert.ToInt32(drData["FK_PurchaseOrderID"]);
                        objPurchaseOrder.PurchaseOrderNo = Convert.ToString(drData["PurchaseOrderNo"]);
                        objPurchase.PurchaseOrder = objPurchaseOrder;

                        objPurchase.IsDCConverted = Convert.ToBoolean(drData["IsDCConverted"]);
                        objPurchase.IsDC = Convert.ToBoolean(drData["IsDC"]);
                        objPurchase.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objPurchase.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchase.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchase.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchase.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchase.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchase.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objPurchase.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchase.PaymentDiscount = Convert.ToDecimal(drData["PaymentDiscount"]);
                        objPurchase.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);
                        objPurchase.CourierCharges = Convert.ToDecimal(drData["CourierCharges"]);
                        objPurchase.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objPurchase.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objPurchase.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchase.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchase.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objPurchase.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objPurchase.VerifiedBy = Convert.ToInt32(drData["VerifiedBy"]);
                        objPurchase.ConfirmedBy = Convert.ToInt32(drData["ConfirmedBy"]);
                        objPurchase.VerifiedName = Convert.ToString(drData["VerifiedName"]);
                        objPurchase.ConfirmedName = Convert.ToString(drData["ConfirmedName"]);
                        objPurchase.Comments = Convert.ToString(drData["Comments"]);
                        objPurchase.Status = Convert.ToString(drData["Status"]);
                        objPurchase.PurchaseTrans = PurchaseTrans.GetPurchaseTransByPurchaseID(objPurchase.PurchaseID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchaseByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objPurchase;
        }
        #endregion SelectDC

        public static int AddPurchase(Entity.Billing.Purchase objPurchase)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_PURCHASE);
                db.AddOutParameter(cmd, "@PK_PurchaseID", DbType.Int32, objPurchase.PurchaseID);
                db.AddInParameter(cmd, "@PurchaseNo", DbType.String, objPurchase.PurchaseNo);
                db.AddInParameter(cmd, "@PurchaseDate", DbType.String, objPurchase.sPurchaseDate);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, objPurchase.Supplier.SupplierID);
                db.AddInParameter(cmd, "@FK_TransportID", DbType.Int32, objPurchase.Transport.TransportID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objPurchase.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objPurchase.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objPurchase.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objPurchase.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objPurchase.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objPurchase.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objPurchase.TotalAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objPurchase.DiscountPercent);
                db.AddInParameter(cmd, "@OtherCharges", DbType.Decimal, objPurchase.OtherCharges);
                db.AddInParameter(cmd, "@CourierCharges", DbType.Decimal, objPurchase.CourierCharges);
                db.AddInParameter(cmd, "@PaymentDiscount", DbType.Decimal, objPurchase.PaymentDiscount);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objPurchase.DiscountAmount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objPurchase.Roundoff);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objPurchase.NetAmount);
                db.AddInParameter(cmd, "@PaidAmount", DbType.Decimal, objPurchase.PaidAmount);
                db.AddInParameter(cmd, "@BalanceAmount", DbType.Decimal, objPurchase.BalanceAmount);
                db.AddInParameter(cmd, "@FK_PurchaseOrderID", DbType.Int32, objPurchase.PurchaseOrder.PurchaseOrderID);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objPurchase.CreatedBy.UserID);
                db.AddInParameter(cmd, "@Dis_amt_Type", DbType.String, objPurchase.Dis_amt_Type);
                db.AddInParameter(cmd, "@PaymentDiscountPercent", DbType.Decimal, objPurchase.PaymentDiscountPercent);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, objPurchase.BillType);
                db.AddInParameter(cmd, "@BillNo", DbType.String, objPurchase.BillNo);
                db.AddInParameter(cmd, "@BillDate", DbType.String, objPurchase.sBillDate);
                db.AddInParameter(cmd, "@TCSPercent", DbType.Decimal, objPurchase.TCSPercent);
                db.AddInParameter(cmd, "@TCSAmount", DbType.Decimal, objPurchase.TCSAmount);
                db.AddInParameter(cmd, "@DocumentPath", DbType.String, objPurchase.DocumentPath);
                db.AddInParameter(cmd, "@VerifiedBy", DbType.Int32, objPurchase.VerifiedBy);
                db.AddInParameter(cmd, "@ConfirmedBy", DbType.Int32, objPurchase.ConfirmedBy);
                db.AddInParameter(cmd, "@Comments", DbType.String, objPurchase.Comments);
                db.AddInParameter(cmd, "@IsDC", DbType.Boolean, objPurchase.IsDC);
                db.AddInParameter(cmd, "@IsDCConverted", DbType.Boolean, objPurchase.IsDCConverted);
                db.AddInParameter(cmd, "@DCID", DbType.Int32, objPurchase.DCID);
                db.AddInParameter(cmd, "@ImagePath1", DbType.String, objPurchase.ImagePath1);
                db.AddInParameter(cmd, "@ImagePath2", DbType.String, objPurchase.ImagePath2);
                db.AddInParameter(cmd, "@ImagePath3", DbType.String, objPurchase.ImagePath3);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objPurchase.FinancialYear.FinancialYearID);
                db.AddInParameter(cmd, "@IsYarn", DbType.Boolean, objPurchase.IsYarn);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_PurchaseID"));

                foreach (Entity.Billing.PurchaseTrans ObjPurchaseTrans in objPurchase.PurchaseTrans)
                    ObjPurchaseTrans.PurchaseID = iID;

                PurchaseTrans.SavePurchaseTransaction(objPurchase.PurchaseTrans);
            }
            catch (Exception ex)
            {
                sException = "Purchase AddPurchase | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdatePurchase(Entity.Billing.Purchase objPurchase)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
             db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_PURCHASE);
                db.AddInParameter(cmd, "@PK_PurchaseID", DbType.Int32, objPurchase.PurchaseID);
                db.AddInParameter(cmd, "@PurchaseNo", DbType.String, objPurchase.PurchaseNo);
                db.AddInParameter(cmd, "@PurchaseDate", DbType.String, objPurchase.sPurchaseDate);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, objPurchase.Supplier.SupplierID);
                db.AddInParameter(cmd, "@FK_TransportID", DbType.Int32, objPurchase.Transport.TransportID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objPurchase.Tax.TaxID);
                db.AddInParameter(cmd, "@FK_PurchaseOrderID", DbType.Int32, objPurchase.PurchaseOrder.PurchaseOrderID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objPurchase.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objPurchase.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objPurchase.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objPurchase.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objPurchase.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objPurchase.TotalAmount);
                db.AddInParameter(cmd, "@PaymentDiscount", DbType.Decimal, objPurchase.PaymentDiscount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objPurchase.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objPurchase.DiscountAmount);
                db.AddInParameter(cmd, "@OtherCharges", DbType.Decimal, objPurchase.OtherCharges);
                db.AddInParameter(cmd, "@CourierCharges", DbType.Decimal, objPurchase.CourierCharges);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objPurchase.Roundoff);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objPurchase.NetAmount);
                db.AddInParameter(cmd, "@PaidAmount", DbType.Decimal, objPurchase.PaidAmount);
                db.AddInParameter(cmd, "@BalanceAmount", DbType.Decimal, objPurchase.BalanceAmount);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objPurchase.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, objPurchase.BillType);
                db.AddInParameter(cmd, "@BillNo", DbType.String, objPurchase.BillNo);
                db.AddInParameter(cmd, "@BillDate", DbType.String, objPurchase.sBillDate);
                db.AddInParameter(cmd, "@TCSPercent", DbType.Decimal, objPurchase.TCSPercent);
                db.AddInParameter(cmd, "@Dis_amt_Type", DbType.String, objPurchase.Dis_amt_Type);
                db.AddInParameter(cmd, "@PaymentDiscountPercent", DbType.Decimal, objPurchase.PaymentDiscountPercent);
                db.AddInParameter(cmd, "@TCSAmount", DbType.Decimal, objPurchase.TCSAmount);
                db.AddInParameter(cmd, "@DocumentPath", DbType.String, objPurchase.DocumentPath);
                db.AddInParameter(cmd, "@VerifiedBy", DbType.Int32, objPurchase.VerifiedBy);
                db.AddInParameter(cmd, "@IsDC", DbType.Boolean, objPurchase.IsDC);
                db.AddInParameter(cmd, "@ConfirmedBy", DbType.Int32, objPurchase.ConfirmedBy);
                db.AddInParameter(cmd, "@Comments", DbType.String, objPurchase.Comments);
                db.AddInParameter(cmd, "@ImagePath1", DbType.String, objPurchase.ImagePath1);
                db.AddInParameter(cmd, "@ImagePath2", DbType.String, objPurchase.ImagePath2);
                db.AddInParameter(cmd, "@ImagePath3", DbType.String, objPurchase.ImagePath3);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objPurchase.FinancialYear.FinancialYearID);
                db.AddInParameter(cmd, "@IsYarn", DbType.Boolean, objPurchase.IsYarn);

                foreach (Entity.Billing.PurchaseTrans ObjPurchaseTrans in objPurchase.PurchaseTrans)
                    ObjPurchaseTrans.PurchaseID = objPurchase.PurchaseID;

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                PurchaseTrans.SavePurchaseTransaction(objPurchase.PurchaseTrans);
            }
            catch (Exception ex)
            {
                sException = "Purchase UpdatePurchase| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool UpdatePurchasePending(Entity.Billing.Purchase objPurchase)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_PURCHASEPENIDNG);
                db.AddInParameter(cmd, "@PK_PurchaseID", DbType.Int32, objPurchase.PurchaseID);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objPurchase.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@Comments", DbType.String, objPurchase.Comments);

                foreach (Entity.Billing.PurchaseTrans ObjPurchaseTrans in objPurchase.PurchaseTrans)
                    ObjPurchaseTrans.PurchaseID = objPurchase.PurchaseID;

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                PurchaseTrans.SavePurchaseTransaction(objPurchase.PurchaseTrans);
            }
            catch (Exception ex)
            {
                sException = "Purchase UpdatePurchase| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeletePurchase(int iPurchaseId, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_PURCHASE);
                db.AddInParameter(cmd, "@PK_PurchaseID", DbType.Int32, iPurchaseId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Purchase DeletePurchase | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

        public static bool CheckdeleteStockByProductID(int iProductID, decimal iQty = 0, int iPurchaseTransID = 0)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_CHECK_PRODUCTBYQTY);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, iProductID);
                db.AddInParameter(cmd, "@Qty", DbType.Decimal, iQty);
                db.AddInParameter(cmd, "@PK_PurchaseTransID", DbType.Int32, iPurchaseTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Purchase DeletePurchase | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

        public static Entity.Price GetProductRate(int iPurchaseID, string type, int iSupplierID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Price objPurchase = new Entity.Price();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PRICE);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, iPurchaseID);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@type", DbType.String, type);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase.Rate = Convert.ToDecimal(drData["Rate"]);
                        objPurchase.Type = Convert.ToString(drData["Type"]);
                        objPurchase.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objPurchase.ProductCode = Convert.ToString(drData["PartyCode"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchaseByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objPurchase;
        }
        public static Entity.Price GetWholeSaleProductRate(int iPurchaseID, string type, int iSupplierID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Price objPurchase = new Entity.Price();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_WHOLESALEPRICE);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, iPurchaseID);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@type", DbType.String, type);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase.Rate = Convert.ToDecimal(drData["Rate"]);
                        objPurchase.Type = Convert.ToString(drData["Type"]);
                        objPurchase.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objPurchase.ProductCode = Convert.ToString(drData["PartyCode"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchaseByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objPurchase;
        }
        public static Entity.Price GetRetailProductRate(int iPurchaseID, string type, int iSupplierID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Price objPurchase = new Entity.Price();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_RETAILPRICE);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, iPurchaseID);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@type", DbType.String, type);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase.Rate = Convert.ToDecimal(drData["Rate"]);
                        objPurchase.Type = Convert.ToString(drData["Type"]);
                        objPurchase.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objPurchase.ProductCode = Convert.ToString(drData["PartyCode"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchaseByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objPurchase;
        }
        public static Entity.Price GetCurrentStock(int ProductID = 0, int CategoryID = 0, int SubCategoryID = 0, int SupplierID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Price objPurchase = new Entity.Price();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_STOCKSUMMARY);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, ProductID);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, CategoryID);
                db.AddInParameter(cmd, "@FK_SubCategoryID", DbType.Int32, SubCategoryID);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, SupplierID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase.Rate = Convert.ToDecimal(drData["Quantity"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchaseByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objPurchase;
        }
        public static Entity.Price GetRateByProduct(int iPurchaseID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Price objPurchase = new Entity.Price();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PRICEBYPRODUCT);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, iPurchaseID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchase.Rate = Convert.ToDecimal(drData["WholeSalePrice"]);
                        objPurchase.RetailRate = Convert.ToDecimal(drData["RetailPrice"]);
                        objPurchase.WholeSalePriceA = Convert.ToDecimal(drData["WholeSalePriceA"]);
                        objPurchase.WholeSalePriceB = Convert.ToDecimal(drData["WholeSalePriceB"]);
                        objPurchase.WholeSalePriceC = Convert.ToDecimal(drData["WholeSalePriceC"]);
                        objPurchase.RetailPriceA = Convert.ToDecimal(drData["RetailPriceA"]);
                        objPurchase.RetailPriceB = Convert.ToDecimal(drData["RetailPriceB"]);
                        objPurchase.RetailPriceC = Convert.ToDecimal(drData["RetailPriceC"]);
                        objPurchase.PurchasePrice = Convert.ToDecimal(drData["PurchasePrice"]);
                        objPurchase.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objPurchase.ProductCode = Convert.ToString(drData["ProductCode"]);

                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Purchase GetPurchaseByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objPurchase;
        }
        public static Collection<Entity.Billing.PurchaseTransDetails> GetPurchaseNewProductDetails(int iID = 0, string code = "", int iValue = 0, int iSupplierID = 0, int iSalesENtryID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.PurchaseTransDetails> objList = new Collection<Entity.Billing.PurchaseTransDetails>();
            Entity.Billing.PurchaseTransDetails objSalesEntryTrans = new Entity.Billing.PurchaseTransDetails();
            Entity.Master.Product objProduct;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_NEWPURCHASEENTRYTRANSDETAILS);
                db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
                db.AddInParameter(cmd, "@Type", DbType.Int32, iValue);
                db.AddInParameter(cmd, "@SMSCode", DbType.String, code);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@FK_SalesEntryID", DbType.Int32, iSalesENtryID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntryTrans = new Entity.Billing.PurchaseTransDetails();
                        objProduct = new Entity.Master.Product();

                        objSalesEntryTrans.PurchaseTransID = Convert.ToInt32(drData["PK_PurchaseTransID"]);
                        objSalesEntryTrans.PurchaseID = Convert.ToInt32(drData["FK_PurchaseID"]);
                        objSalesEntryTrans.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objSalesEntryTrans.BillNo = Convert.ToString(drData["BillNo"]);
                        objSalesEntryTrans.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSalesEntryTrans.sBillDate = objSalesEntryTrans.BillDate.ToString("dd/MM/yyyy");
                        objSalesEntryTrans.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objSalesEntryTrans.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);
                        objSalesEntryTrans.sPurchaseDate = objSalesEntryTrans.PurchaseDate.ToString("dd/MM/yyyy");

                        objSalesEntryTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objSalesEntryTrans.Rate = Convert.ToDecimal(drData["Rate"]);
                        objSalesEntryTrans.SubTotal = Convert.ToDecimal(drData["SubTotal"]);
                        objSalesEntryTrans.DiscountPercentage = Convert.ToDecimal(drData["DiscountPercentage"]);
                        objSalesEntryTrans.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objSalesEntryTrans.Product = objProduct;
                        objSalesEntryTrans.Barcode = Convert.ToString(drData["Barcode"]);
                        objList.Add(objSalesEntryTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntryTrans GetSalesEntryTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.PurchaseTransDetails> GetProductDetailsPurchase(int iID = 0, int iValue = 0, int iSupplierID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.PurchaseTransDetails> objList = new Collection<Entity.Billing.PurchaseTransDetails>();
            Entity.Billing.PurchaseTransDetails objSalesEntryTrans = new Entity.Billing.PurchaseTransDetails();
            Entity.Master.Product objProduct;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASETRANSDETAILS);
                db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
                db.AddInParameter(cmd, "@FK_PurchaseID", DbType.Int32, iValue);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntryTrans = new Entity.Billing.PurchaseTransDetails();
                        objProduct = new Entity.Master.Product();

                        objSalesEntryTrans.PurchaseTransID = Convert.ToInt32(drData["PK_PurchaseTransID"]);
                        objSalesEntryTrans.PurchaseID = Convert.ToInt32(drData["FK_PurchaseID"]);
                        objSalesEntryTrans.SupplierName = Convert.ToString(drData["SupplierName"]);

                        objSalesEntryTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objSalesEntryTrans.Rate = Convert.ToInt32(drData["Rate"]);
                        objSalesEntryTrans.SubTotal = Convert.ToDecimal(drData["SubTotal"]);
                        objSalesEntryTrans.DiscountPercentage = Convert.ToDecimal(drData["DiscountPercentage"]);
                        objSalesEntryTrans.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objSalesEntryTrans.Product = objProduct;
                        objSalesEntryTrans.Barcode = Convert.ToString(drData["Barcode"]);
                        objList.Add(objSalesEntryTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntryTrans GetSalesEntryTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
    }

    public class PurchaseTrans
    {
        public static Collection<Entity.Billing.PurchaseTrans> GetPurchaseTransByPurchaseID(int iPurchaseID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.PurchaseTrans> objList = new Collection<Entity.Billing.PurchaseTrans>();
            Entity.Billing.PurchaseTrans objPurchaseTrans = new Entity.Billing.PurchaseTrans();
            Entity.Master.Product objProduct;
            Entity.Billing.Tax objTax;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASETRANS);
                db.AddInParameter(cmd, "@FK_PurchaseID", DbType.Int32, iPurchaseID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchaseTrans = new Entity.Billing.PurchaseTrans();
                        objProduct = new Entity.Master.Product();
                        objTax = new Entity.Billing.Tax();

                        objPurchaseTrans.PurchaseTransID = Convert.ToInt32(drData["PK_PurchaseTransID"]);
                        objPurchaseTrans.PurchaseID = Convert.ToInt32(drData["FK_PurchaseID"]);

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxPercentage = Convert.ToDecimal(drData["TaxPercent"]);
                        objTax.SGSTPercent = Convert.ToDecimal(drData["SGSTPercent"]);
                        objTax.IGSTPercent = Convert.ToDecimal(drData["IGSTPercent"]);
                        objTax.CGSTPercent = Convert.ToDecimal(drData["CGSTPercent"]);
                        objPurchaseTrans.Tax = objTax;

                        objPurchaseTrans.RateupdateFlag = Convert.ToBoolean(drData["RateupdateFlag"]);
                        objPurchaseTrans.NewProductFlag = Convert.ToBoolean(drData["NewProductFlag"]);
                        objPurchaseTrans.RateDecreaseFlag = Convert.ToBoolean(drData["RateDecreaseFlag"]);
                        objPurchaseTrans.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchaseTrans.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchaseTrans.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchaseTrans.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchaseTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objPurchaseTrans.Rate = Convert.ToDecimal(drData["Rate"]);
                        objPurchaseTrans.SubTotal = Convert.ToDecimal(drData["SubTotal"]);
                        objPurchaseTrans.Barcode = Convert.ToString(drData["Barcode"]);
                        objPurchaseTrans.DiscountPercentage = Convert.ToDecimal(drData["DiscountPercentage"]);
                        objPurchaseTrans.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objPurchaseTrans.Product = objProduct;

                        objList.Add(objPurchaseTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "PurchaseTrans GetPurchaseTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SavePurchaseTransaction(Collection<Entity.Billing.PurchaseTrans> ObjPurchaseTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.PurchaseTrans ObjPurchaseTransaction in ObjPurchaseTransList)
            {
                if (ObjPurchaseTransaction.StatusFlag == "I")
                    iID = AddPurchaseTrans(ObjPurchaseTransaction);
                else if (ObjPurchaseTransaction.StatusFlag == "U")
                    bResult = UpdatePurchaseTrans(ObjPurchaseTransaction);
                else if (ObjPurchaseTransaction.StatusFlag == "D")
                    bResult = DeletePurchaseTrans(ObjPurchaseTransaction.PurchaseTransID);
            }
        }
        public static int AddPurchaseTrans(Entity.Billing.PurchaseTrans objPurchaseTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_PURCHASETRANS);
                db.AddOutParameter(cmd, "@PK_PurchaseTransID", DbType.Int32, objPurchaseTrans.PurchaseTransID);
                db.AddInParameter(cmd, "@FK_PurchaseID", DbType.Int32, objPurchaseTrans.PurchaseID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objPurchaseTrans.Product.ProductID);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objPurchaseTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objPurchaseTrans.Rate);
                db.AddInParameter(cmd, "@DiscountPercentage", DbType.Decimal, objPurchaseTrans.DiscountPercentage);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objPurchaseTrans.DiscountAmount);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objPurchaseTrans.SubTotal);
                db.AddInParameter(cmd, "@Barcode", DbType.String, objPurchaseTrans.Barcode);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.String, objPurchaseTrans.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objPurchaseTrans.Tax.TaxPercentage);
                db.AddInParameter(cmd, "@IGSTPercent", DbType.Decimal, objPurchaseTrans.Tax.IGSTPercent);
                db.AddInParameter(cmd, "@SGSTPercent", DbType.Decimal, objPurchaseTrans.Tax.SGSTPercent);
                db.AddInParameter(cmd, "@CGSTPercent", DbType.Decimal, objPurchaseTrans.Tax.CGSTPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objPurchaseTrans.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objPurchaseTrans.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objPurchaseTrans.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objPurchaseTrans.TaxAmount);
                db.AddInParameter(cmd, "@RateupdateFlag", DbType.Boolean, objPurchaseTrans.RateupdateFlag);
                db.AddInParameter(cmd, "@NewProductFlag", DbType.Boolean, objPurchaseTrans.NewProductFlag);
                db.AddInParameter(cmd, "@RateDecreaseFlag", DbType.Boolean, objPurchaseTrans.RateDecreaseFlag);


                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_PurchaseTransID"));
            }
            catch (Exception ex)
            {
                sException = "PurchaseTrans AddPurchaseTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdatePurchaseTrans(Entity.Billing.PurchaseTrans objPurchaseTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_PURCHASETRANS);
                db.AddInParameter(cmd, "@PK_PurchaseTransID", DbType.Int32, objPurchaseTrans.PurchaseTransID);
                db.AddInParameter(cmd, "@FK_PurchaseID", DbType.Int32, objPurchaseTrans.PurchaseID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objPurchaseTrans.Product.ProductID);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objPurchaseTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objPurchaseTrans.Rate);
                db.AddInParameter(cmd, "@DiscountPercentage", DbType.Decimal, objPurchaseTrans.DiscountPercentage);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objPurchaseTrans.DiscountAmount);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objPurchaseTrans.SubTotal);
                db.AddInParameter(cmd, "@Barcode", DbType.String, objPurchaseTrans.Barcode);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.String, objPurchaseTrans.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objPurchaseTrans.Tax.TaxPercentage);
                db.AddInParameter(cmd, "@IGSTPercent", DbType.Decimal, objPurchaseTrans.Tax.IGSTPercent);
                db.AddInParameter(cmd, "@SGSTPercent", DbType.Decimal, objPurchaseTrans.Tax.SGSTPercent);
                db.AddInParameter(cmd, "@CGSTPercent", DbType.Decimal, objPurchaseTrans.Tax.CGSTPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objPurchaseTrans.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objPurchaseTrans.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objPurchaseTrans.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objPurchaseTrans.TaxAmount);
                db.AddInParameter(cmd, "@RateupdateFlag", DbType.Boolean, objPurchaseTrans.RateupdateFlag);
                db.AddInParameter(cmd, "@NewProductFlag", DbType.Boolean, objPurchaseTrans.NewProductFlag);
                db.AddInParameter(cmd, "@RateDecreaseFlag", DbType.Boolean, objPurchaseTrans.RateDecreaseFlag);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "PurchaseTrans UpdatePurchaseTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

        public static bool DeletePurchaseTrans(int iPurchaseTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_PURCHASETRANS);
                db.AddInParameter(cmd, "@PK_PurchaseTransID", DbType.Int32, iPurchaseTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "PurchaseTrans DeletePurchaseTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

    }


   
}
