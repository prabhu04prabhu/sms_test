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
    public class EstimateSalesEntry
    {
        public static Collection<Entity.Billing.EstimateSalesEntry> GetEstimateSalesEntry(int ipatientID = 0, int IsRetail = 0, int FK_FinancialYearID = 0021
             )
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.EstimateSalesEntry> objList = new Collection<Entity.Billing.EstimateSalesEntry>();
            Entity.Billing.EstimateSalesEntry objEstimateSalesEntry;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank;
           Entity.Billing.SalesOrder objEstimateSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ESTIMATESALESENTRY);
                db.AddInParameter(cmd, "@PK_EstimateSalesEntryID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objEstimateSalesEntry = new Entity.Billing.EstimateSalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objEstimateSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();

                        objEstimateSalesEntry.EstimateSalesEntryID = Convert.ToInt32(drData["PK_EstimateSalesEntryID"]);
                        objEstimateSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objEstimateSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objCustomer.GSTNo = Convert.ToString(drData["GSTNo"]);
                        objEstimateSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objEstimateSalesEntry.Tax = objTax;
                        objEstimateSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objEstimateSalesEntry.sLRDate = objEstimateSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objEstimateSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objEstimateSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objEstimateSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objEstimateSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objEstimateSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objEstimateSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objEstimateSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objEstimateSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objEstimateSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objEstimateSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objEstimateSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objEstimateSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objEstimateSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objEstimateSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objEstimateSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objEstimateSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objEstimateSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objEstimateSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objEstimateSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objEstimateSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objEstimateSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objEstimateSalesEntry.Notes = Convert.ToString(drData["Notes"]);
                        objEstimateSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objEstimateSalesEntry.sDeliveryDate = objEstimateSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objEstimateSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);
                        objEstimateSalesEntry.SalesConverted = Convert.ToBoolean(drData["SalesConverted"]);
                        
                        objEstimateSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objEstimateSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objEstimateSalesEntry.Bank = objBank;

                        objEstimateSalesOrder.SalesOrderID = 0;
                        objEstimateSalesEntry.SalesOrder = objEstimateSalesOrder;

                        objEstimateSalesEntry.EstimateSalesPoints = Convert.ToDecimal(drData["EstimateSalesPoints"]);
                        objEstimateSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objEstimateSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objEstimateSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objEstimateSalesEntry.Transport = objTransport;

                        objEstimateSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objEstimateSalesEntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objEstimateSalesEntry.sInvoiceDate = objEstimateSalesEntry.InvoiceDate.ToString("dd/MM/yyyy") + " " + objEstimateSalesEntry.CreatedOn.ToString("h:mm");
                        objEstimateSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);

                        objList.Add(objEstimateSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "EstimateSalesEntry GetEstimateSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.EstimateSalesEntry GetLastInvoiceNo(int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.EstimateSalesEntry objUser = new Entity.Billing.EstimateSalesEntry();
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_LASTINVOICENO);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objUser.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objUser.Status = Convert.ToString(drData["PreInvoiceNo"]);

                    }
                }
            }
            catch (Exception ex)
            {
                sException = "User GetUserByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objUser;
        }


        public static Collection<Entity.Billing.EstimateSalesEntry> GetPendingRetailEstimateSales(int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.EstimateSalesEntry> objList = new Collection<Entity.Billing.EstimateSalesEntry>();
            Entity.Billing.EstimateSalesEntry objEstimateSalesEntry;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objEstimateSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PENDINGRETAILESTIMATESALES);

                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objEstimateSalesEntry = new Entity.Billing.EstimateSalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objEstimateSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]) + " | " + Convert.ToString(drData["MobileNo"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objEstimateSalesEntry.Customer = objCustomer;

                        objList.Add(objEstimateSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "EstimateSalesEntry GetEstimateSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.EstimateSalesEntry> GetTopEstimateSalesEntry(int ipatientID = 0, int IsRetail = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.EstimateSalesEntry> objList = new Collection<Entity.Billing.EstimateSalesEntry>();
            Entity.Billing.EstimateSalesEntry objEstimateSalesEntry;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objEstimateSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPESTIMATESALESENTRY);
                db.AddInParameter(cmd, "@PK_EstimateSalesEntryID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objEstimateSalesEntry = new Entity.Billing.EstimateSalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objEstimateSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();

                        objEstimateSalesEntry.EstimateSalesEntryID = Convert.ToInt32(drData["PK_EstimateSalesEntryID"]);
                        objEstimateSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objEstimateSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Area = Convert.ToString(drData["Area"]);
                        objCustomer.MinDueDays = Convert.ToInt32(drData["MinDueDays"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objCustomer.GSTNo = Convert.ToString(drData["GSTNo"]);
                        objEstimateSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objEstimateSalesEntry.Tax = objTax;
                        objEstimateSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objEstimateSalesEntry.DueDays = Convert.ToInt32(drData["DueDays"]);
                        objEstimateSalesEntry.sLRDate = objEstimateSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objEstimateSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objEstimateSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objEstimateSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objEstimateSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objEstimateSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objEstimateSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objEstimateSalesEntry.MarginPercent = Convert.ToDecimal(drData["MarginPercent"]);
                        objEstimateSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objEstimateSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objEstimateSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objEstimateSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objEstimateSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objEstimateSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objEstimateSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objEstimateSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objEstimateSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objEstimateSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objEstimateSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objEstimateSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objEstimateSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objEstimateSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objEstimateSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objEstimateSalesEntry.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objEstimateSalesEntry.SalesConverted = Convert.ToBoolean(drData["SalesConverted"]);
                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objEstimateSalesEntry.Bank = objBank;

                        objEstimateSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objEstimateSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objEstimateSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objEstimateSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objEstimateSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objEstimateSalesOrder.SalesOrderID = 0;
                        objEstimateSalesEntry.SalesOrder = objEstimateSalesOrder;

                        objEstimateSalesEntry.EstimateSalesPoints = Convert.ToDecimal(drData["EstimateSalesPoints"]);
                        objEstimateSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objEstimateSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objEstimateSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objEstimateSalesEntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objEstimateSalesEntry.sInvoiceDate = objEstimateSalesEntry.InvoiceDate.ToString("dd/MM/yyyy") + " " + objEstimateSalesEntry.CreatedOn.ToString("h:mm");
                        objEstimateSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objEstimateSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objEstimateSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objEstimateSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objEstimateSalesEntry.Transport = objTransport;

                        objList.Add(objEstimateSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "EstimateSalesEntry GetEstimateSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.EstimateSalesEntry> GetEstimateSalesEntryBookingBill(int ipatientID = 0, int IsRetail = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.EstimateSalesEntry> objList = new Collection<Entity.Billing.EstimateSalesEntry>();
            Entity.Billing.EstimateSalesEntry objEstimateSalesEntry;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objEstimateSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ESTIMATESALESENTRYBOOKINGBILL);
                db.AddInParameter(cmd, "@PK_EstimateSalesEntryID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objEstimateSalesEntry = new Entity.Billing.EstimateSalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objEstimateSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();

                        objEstimateSalesEntry.EstimateSalesEntryID = Convert.ToInt32(drData["PK_EstimateSalesEntryID"]);
                        objEstimateSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objEstimateSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Days = Convert.ToInt32(drData["Days"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objCustomer.GSTNo = Convert.ToString(drData["GSTNo"]);
                        objEstimateSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objEstimateSalesEntry.Tax = objTax;
                        objEstimateSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objEstimateSalesEntry.DueDays = Convert.ToInt32(drData["DueDays"]);
                        objEstimateSalesEntry.sLRDate = objEstimateSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objEstimateSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objEstimateSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objEstimateSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objEstimateSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objEstimateSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objEstimateSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objEstimateSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objEstimateSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objEstimateSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objEstimateSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objEstimateSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objEstimateSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objEstimateSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objEstimateSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objEstimateSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objEstimateSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objEstimateSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objEstimateSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objEstimateSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objEstimateSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objEstimateSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objEstimateSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objEstimateSalesEntry.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objEstimateSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objEstimateSalesEntry.sDeliveryDate = objEstimateSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objEstimateSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objEstimateSalesEntry.Bank = objBank;

                        objEstimateSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objEstimateSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objEstimateSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objEstimateSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objEstimateSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objEstimateSalesOrder.SalesOrderID = 0;
                        objEstimateSalesEntry.SalesOrder = objEstimateSalesOrder;

                        objEstimateSalesEntry.EstimateSalesPoints = Convert.ToDecimal(drData["EstimateSalesPoints"]);
                        objEstimateSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objEstimateSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objEstimateSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objEstimateSalesEntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objEstimateSalesEntry.sInvoiceDate = objEstimateSalesEntry.InvoiceDate.ToString("dd/MM/yyyy") + " " + objEstimateSalesEntry.CreatedOn.ToString("h:mm");
                        objEstimateSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objEstimateSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objEstimateSalesEntry.Transport = objTransport;

                        objList.Add(objEstimateSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "EstimateSalesEntry GetEstimateSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.EstimateSalesEntry> SearchEstimateSalesEntryBokingBill(string ID, int IsRetail = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.EstimateSalesEntry> objList = new Collection<Entity.Billing.EstimateSalesEntry>();
            Entity.Billing.EstimateSalesEntry objEstimateSalesEntry; Entity.Ledger objBank; Entity.Billing.SalesOrder objEstimateSalesOrder;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ESTIMATESALESENTRYBILLBOOKINGDYNAMIC);
                db.AddInParameter(cmd, "@PK_EstimateSalesEntryID", DbType.String, ID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objEstimateSalesEntry = new Entity.Billing.EstimateSalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objEstimateSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();

                        objEstimateSalesEntry.EstimateSalesEntryID = Convert.ToInt32(drData["PK_EstimateSalesEntryID"]);
                        objEstimateSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objEstimateSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Days = Convert.ToInt32(drData["Days"]);
                        objCustomer.GSTNo = Convert.ToString(drData["GSTNo"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objEstimateSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objEstimateSalesEntry.Tax = objTax;
                        objEstimateSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objEstimateSalesEntry.DueDays = Convert.ToInt32(drData["DueDays"]);
                        objEstimateSalesEntry.sLRDate = objEstimateSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objEstimateSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objEstimateSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objEstimateSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objEstimateSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objEstimateSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objEstimateSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objEstimateSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objEstimateSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objEstimateSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objEstimateSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objEstimateSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objEstimateSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objEstimateSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objEstimateSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objEstimateSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objEstimateSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objEstimateSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objEstimateSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objEstimateSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objEstimateSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objEstimateSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objEstimateSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objEstimateSalesEntry.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objEstimateSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objEstimateSalesEntry.sDeliveryDate = objEstimateSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                       

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objEstimateSalesEntry.Bank = objBank;


                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objEstimateSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objEstimateSalesEntry.Transport = objTransport;
                        objEstimateSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objEstimateSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objEstimateSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objEstimateSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objEstimateSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objEstimateSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objEstimateSalesEntry.sDeliveryDate = objEstimateSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objEstimateSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objEstimateSalesOrder.SalesOrderID = 0;
                        objEstimateSalesEntry.SalesOrder = objEstimateSalesOrder;

                        objEstimateSalesEntry.EstimateSalesPoints = Convert.ToDecimal(drData["EstimateSalesPoints"]);
                        objEstimateSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objEstimateSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objEstimateSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objEstimateSalesEntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objEstimateSalesEntry.sInvoiceDate = objEstimateSalesEntry.InvoiceDate.ToString("dd/MM/yyyy") + " " + objEstimateSalesEntry.CreatedOn.ToString("h:mm");
                        objEstimateSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);

                        objList.Add(objEstimateSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "EstimateSalesEntry GetEstimateSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.EstimateSalesEntry> GetPendingEstimateSalesEntry(int ipatientID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.EstimateSalesEntry> objList = new Collection<Entity.Billing.EstimateSalesEntry>();
            Entity.Billing.EstimateSalesEntry objEstimateSalesEntry;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objEstimateSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PENDINGESTIMATESALESENTRY);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objEstimateSalesEntry = new Entity.Billing.EstimateSalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objEstimateSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();

                        objEstimateSalesEntry.EstimateSalesEntryID = Convert.ToInt32(drData["PK_EstimateSalesEntryID"]);
                        objEstimateSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objEstimateSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objCustomer.GSTNo = Convert.ToString(drData["GSTNo"]);
                        objEstimateSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objEstimateSalesEntry.Tax = objTax;
                        objEstimateSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objEstimateSalesEntry.sLRDate = objEstimateSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objEstimateSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objEstimateSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objEstimateSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objEstimateSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objEstimateSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objEstimateSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objEstimateSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objEstimateSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objEstimateSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objEstimateSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objEstimateSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objEstimateSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objEstimateSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objEstimateSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objEstimateSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objEstimateSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objEstimateSalesEntry.ReturnAmount = Convert.ToDecimal(drData["ReturnAmount"]);
                        objEstimateSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objEstimateSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objEstimateSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objEstimateSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objEstimateSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objEstimateSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objEstimateSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objEstimateSalesEntry.sDeliveryDate = objEstimateSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objEstimateSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objEstimateSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objEstimateSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objEstimateSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objEstimateSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objEstimateSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objEstimateSalesEntry.Bank = objBank;

                        objEstimateSalesOrder.SalesOrderID =0;
                        objEstimateSalesEntry.SalesOrder = objEstimateSalesOrder;

                        objEstimateSalesEntry.EstimateSalesPoints = Convert.ToDecimal(drData["EstimateSalesPoints"]);
                        objEstimateSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objEstimateSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objEstimateSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objEstimateSalesEntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objEstimateSalesEntry.sInvoiceDate = objEstimateSalesEntry.InvoiceDate.ToString("dd/MM/yyyy") + " " + objEstimateSalesEntry.CreatedOn.ToString("h:mm");
                        objEstimateSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);

                        objList.Add(objEstimateSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "EstimateSalesEntry GetEstimateSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.EstimateSalesEntry> GetPendingRetailBills(int ipatientID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.EstimateSalesEntry> objList = new Collection<Entity.Billing.EstimateSalesEntry>();
            Entity.Billing.EstimateSalesEntry objEstimateSalesEntry;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objEstimateSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PENDINGRETAILBILLS);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objEstimateSalesEntry = new Entity.Billing.EstimateSalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objEstimateSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();

                        objEstimateSalesEntry.EstimateSalesEntryID = Convert.ToInt32(drData["PK_EstimateSalesEntryID"]);
                        objEstimateSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objEstimateSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objCustomer.GSTNo = Convert.ToString(drData["GSTNo"]);
                        objEstimateSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objEstimateSalesEntry.Tax = objTax;
                        objEstimateSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objEstimateSalesEntry.sLRDate = objEstimateSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objEstimateSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objEstimateSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objEstimateSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objEstimateSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objEstimateSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objEstimateSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objEstimateSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objEstimateSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objEstimateSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objEstimateSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objEstimateSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objEstimateSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objEstimateSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objEstimateSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objEstimateSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objEstimateSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objEstimateSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objEstimateSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objEstimateSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objEstimateSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objEstimateSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objEstimateSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objEstimateSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objEstimateSalesEntry.sDeliveryDate = objEstimateSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objEstimateSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        //objEstimateSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        //objEstimateSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objEstimateSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objEstimateSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objEstimateSalesEntry.Bank = objBank;

                        objEstimateSalesOrder.SalesOrderID = 0;
                        objEstimateSalesEntry.SalesOrder = objEstimateSalesOrder;

                        objEstimateSalesEntry.EstimateSalesPoints = Convert.ToDecimal(drData["EstimateSalesPoints"]);
                        objEstimateSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objEstimateSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objEstimateSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objEstimateSalesEntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objEstimateSalesEntry.sInvoiceDate = objEstimateSalesEntry.InvoiceDate.ToString("dd/MM/yyyy") + " " + objEstimateSalesEntry.CreatedOn.ToString("h:mm");
                        objEstimateSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);

                        objList.Add(objEstimateSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "EstimateSalesEntry GetEstimateSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.EstimateSalesEntry> SearchEstimateSalesEntry(string ID, int IsRetail = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.EstimateSalesEntry> objList = new Collection<Entity.Billing.EstimateSalesEntry>();
            Entity.Billing.EstimateSalesEntry objEstimateSalesEntry; Entity.Ledger objBank; Entity.Billing.SalesOrder objEstimateSalesOrder;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ESTIMATESALESENTRYDYNAMIC);
                db.AddInParameter(cmd, "@PK_EstimateSalesEntryID", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objEstimateSalesEntry = new Entity.Billing.EstimateSalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objEstimateSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();

                        objEstimateSalesEntry.EstimateSalesEntryID = Convert.ToInt32(drData["PK_EstimateSalesEntryID"]);
                        objEstimateSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objEstimateSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MinDueDays = Convert.ToInt32(drData["MinDueDays"]);
                        objCustomer.Area = Convert.ToString(drData["Area"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objCustomer.GSTNo = Convert.ToString(drData["GSTNo"]);
                        objEstimateSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objEstimateSalesEntry.Tax = objTax;
                        objEstimateSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objEstimateSalesEntry.DueDays = Convert.ToInt32(drData["DueDays"]);
                        objEstimateSalesEntry.sLRDate = objEstimateSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objEstimateSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objEstimateSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objEstimateSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objEstimateSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objEstimateSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objEstimateSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objEstimateSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objEstimateSalesEntry.MarginPercent = Convert.ToDecimal(drData["MarginPercent"]);
                        objEstimateSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objEstimateSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objEstimateSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objEstimateSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objEstimateSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objEstimateSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objEstimateSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objEstimateSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objEstimateSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objEstimateSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objEstimateSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objEstimateSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objEstimateSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objEstimateSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objEstimateSalesEntry.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objEstimateSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objEstimateSalesEntry.sDeliveryDate = objEstimateSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objEstimateSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);
                        objEstimateSalesEntry.SalesConverted = Convert.ToBoolean(drData["SalesConverted"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objEstimateSalesEntry.Bank = objBank;


                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objEstimateSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objEstimateSalesEntry.Transport = objTransport;
                        objEstimateSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objEstimateSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objEstimateSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objEstimateSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objEstimateSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objEstimateSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objEstimateSalesEntry.sDeliveryDate = objEstimateSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");

                        objEstimateSalesOrder.SalesOrderID = 0;
                        objEstimateSalesEntry.SalesOrder = objEstimateSalesOrder;

                        objEstimateSalesEntry.EstimateSalesPoints = Convert.ToDecimal(drData["EstimateSalesPoints"]);
                        objEstimateSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objEstimateSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objEstimateSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objEstimateSalesEntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objEstimateSalesEntry.sInvoiceDate = objEstimateSalesEntry.InvoiceDate.ToString("dd/MM/yyyy") + " " + objEstimateSalesEntry.CreatedOn.ToString("h:mm");
                        objEstimateSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);

                        objList.Add(objEstimateSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "EstimateSalesEntry GetEstimateSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }



        public static Entity.Billing.EstimateSalesEntry GetEstimateSalesEntryByInvoice(string InvoiceNo, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.EstimateSalesEntry objEstimateSalesEntry = new Entity.Billing.EstimateSalesEntry();
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objEstimateSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ESTIMATESALESENTRYINVOICE);
                db.AddInParameter(cmd, "@InvoiceNo", DbType.String, InvoiceNo);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {

                        objEstimateSalesEntry = new Entity.Billing.EstimateSalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objEstimateSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();

                        objEstimateSalesEntry.EstimateSalesEntryID = Convert.ToInt32(drData["PK_EstimateSalesEntryID"]);
                        objEstimateSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objEstimateSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objEstimateSalesEntry.sInvoiceDate = objEstimateSalesEntry.InvoiceDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objCustomer.GSTNo = Convert.ToString(drData["GSTNo"]);
                        objEstimateSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objEstimateSalesEntry.Tax = objTax;
                        objEstimateSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objEstimateSalesEntry.sLRDate = objEstimateSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objEstimateSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objEstimateSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objEstimateSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objEstimateSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objEstimateSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objEstimateSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objEstimateSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objEstimateSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objEstimateSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objEstimateSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objEstimateSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objEstimateSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objEstimateSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objEstimateSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objEstimateSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objEstimateSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objEstimateSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objEstimateSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objEstimateSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objEstimateSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objEstimateSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objEstimateSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objEstimateSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objEstimateSalesEntry.sDeliveryDate = objEstimateSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objEstimateSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objEstimateSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objEstimateSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objEstimateSalesEntry.Bank = objBank;
                        objEstimateSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objEstimateSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objEstimateSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objEstimateSalesOrder.SalesOrderID = 0;
                        objEstimateSalesEntry.SalesOrder = objEstimateSalesOrder;

                        objEstimateSalesEntry.EstimateSalesPoints = Convert.ToDecimal(drData["EstimateSalesPoints"]);
                        objEstimateSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objEstimateSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objEstimateSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objEstimateSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objEstimateSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objEstimateSalesEntry.Transport = objTransport;

                        objEstimateSalesEntry.EstimateSalesEntryTrans = EstimateSalesEntryTrans.GetEstimateSalesEntryTransByEstimateSalesEntryID(objEstimateSalesEntry.EstimateSalesEntryID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "EstimateEstimateSales GetEstimateSalesByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objEstimateSalesEntry;
        }

        public static Entity.Billing.EstimateSalesEntry GetEstimateSalesEntryByID(int iEstimateSalesEntryID, int IsRetail, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.EstimateSalesEntry objEstimateSalesEntry = new Entity.Billing.EstimateSalesEntry();
            Entity.Customer objCustomer; Entity.Billing.Tax objTax; Entity.Gift objGift; Entity.State objState;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objEstimateSalesOrder; Entity.CustomerTypes objCustomerTypes;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ESTIMATESALESENTRY);
                db.AddInParameter(cmd, "@PK_EstimateSalesEntryID", DbType.Int32, iEstimateSalesEntryID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objEstimateSalesEntry = new Entity.Billing.EstimateSalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objEstimateSalesOrder = new Entity.Billing.SalesOrder();
                        objGift = new Entity.Gift();
                        objCustomerTypes = new Entity.CustomerTypes();
                        objState = new Entity.State();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();

                        objEstimateSalesEntry.EstimateSalesEntryID = Convert.ToInt32(drData["PK_EstimateSalesEntryID"]);
                        objEstimateSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objEstimateSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objEstimateSalesEntry.sInvoiceDate = objEstimateSalesEntry.InvoiceDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        string Date = objEstimateSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.sLRDate = objEstimateSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objEstimateSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objEstimateSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objEstimateSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objCustomer.GSTNo = Convert.ToString(drData["GSTNo"]);
                        objEstimateSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objEstimateSalesEntry.Tax = objTax;

                        objCustomerTypes.CustomertypeID = Convert.ToInt32(drData["FK_CustomertypeID"]);
                        objEstimateSalesEntry.CustomerType = objCustomerTypes;

                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objEstimateSalesEntry.State = objState;

                        objGift.GiftID = Convert.ToInt32(drData["FK_GiftID"]);
                        objEstimateSalesEntry.Gift = objGift;
                        objEstimateSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objEstimateSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objEstimateSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objEstimateSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objEstimateSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objEstimateSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objEstimateSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objEstimateSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objEstimateSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objEstimateSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objEstimateSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objEstimateSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objEstimateSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objEstimateSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objEstimateSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objEstimateSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objEstimateSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objEstimateSalesEntry.Status = Convert.ToString(drData["Status"]);

                        objEstimateSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objEstimateSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objEstimateSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objEstimateSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objEstimateSalesEntry.Notes = Convert.ToString(drData["Notes"]);
                        objEstimateSalesEntry.CardCharges = Convert.ToDecimal(drData["CardCharges"]);
                        objEstimateSalesEntry.CardNo = Convert.ToString(drData["CardNo"]);
                        objEstimateSalesEntry.TenderAmount = Convert.ToDecimal(drData["TenderAmount"]);
                        objEstimateSalesEntry.BalanceGiven = Convert.ToDecimal(drData["BalanceGiven"]);
                        objEstimateSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objEstimateSalesEntry.sDeliveryDate = objEstimateSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objEstimateSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);
                        objEstimateSalesEntry.SalesConverted = Convert.ToBoolean(drData["SalesConverted"]);

                        objEstimateSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objEstimateSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objEstimateSalesEntry.Bank = objBank;

                        objEstimateSalesOrder.SalesOrderID = 0;
                        objEstimateSalesEntry.SalesOrder = objEstimateSalesOrder;

                        objEstimateSalesEntry.EstimateSalesPoints = Convert.ToDecimal(drData["EstimateSalesPoints"]);
                        objEstimateSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objEstimateSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objEstimateSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objEstimateSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objEstimateSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objEstimateSalesEntry.Transport = objTransport;

                        objEstimateSalesEntry.EstimateSalesEntryTrans = EstimateSalesEntryTrans.GetEstimateSalesEntryTransByEstimateSalesEntryID(objEstimateSalesEntry.EstimateSalesEntryID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "EstimateSalesEntry GetEstimateSalesEntryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objEstimateSalesEntry;
        }

        public static Entity.Billing.EstimateSalesEntry GetEstimateSalesEntryByIDAddress(int iEstimateSalesEntryID, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.EstimateSalesEntry objEstimateSalesEntry = new Entity.Billing.EstimateSalesEntry();
            Entity.Customer objCustomer; Entity.Billing.Tax objTax; Entity.Gift objGift; Entity.State objState;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objEstimateSalesOrder; Entity.CustomerTypes objCustomerTypes;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ESTIMATESALESENTRYBYADDRESS);
                db.AddInParameter(cmd, "@PK_EstimateSalesEntryID", DbType.Int32, iEstimateSalesEntryID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objEstimateSalesEntry = new Entity.Billing.EstimateSalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objEstimateSalesOrder = new Entity.Billing.SalesOrder();
                        objGift = new Entity.Gift();
                        objCustomerTypes = new Entity.CustomerTypes();
                        objState = new Entity.State();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();

                        objEstimateSalesEntry.EstimateSalesEntryID = Convert.ToInt32(drData["PK_EstimateSalesEntryID"]);
                        objEstimateSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objEstimateSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objEstimateSalesEntry.sInvoiceDate = objEstimateSalesEntry.InvoiceDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        string Date = objEstimateSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.sLRDate = objEstimateSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objEstimateSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objEstimateSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objEstimateSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objCustomer.GSTNo = Convert.ToString(drData["GSTNo"]);
                        objEstimateSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objEstimateSalesEntry.Tax = objTax;

                        objCustomerTypes.CustomertypeID = Convert.ToInt32(drData["FK_CustomertypeID"]);
                        objEstimateSalesEntry.CustomerType = objCustomerTypes;

                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objEstimateSalesEntry.State = objState;

                        objGift.GiftID = Convert.ToInt32(drData["FK_GiftID"]);
                        objEstimateSalesEntry.Gift = objGift;
                        objEstimateSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objEstimateSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objEstimateSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objEstimateSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objEstimateSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objEstimateSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objEstimateSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objEstimateSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objEstimateSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objEstimateSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objEstimateSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objEstimateSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objEstimateSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objEstimateSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objEstimateSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objEstimateSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objEstimateSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objEstimateSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objEstimateSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objEstimateSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objEstimateSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objEstimateSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objEstimateSalesEntry.Notes = Convert.ToString(drData["Notes"]);
                        objEstimateSalesEntry.CardCharges = Convert.ToDecimal(drData["CardCharges"]);
                        objEstimateSalesEntry.CardNo = Convert.ToString(drData["CardNo"]);
                        objEstimateSalesEntry.TenderAmount = Convert.ToDecimal(drData["TenderAmount"]);
                        objEstimateSalesEntry.BalanceGiven = Convert.ToDecimal(drData["BalanceGiven"]);
                        objEstimateSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objEstimateSalesEntry.sDeliveryDate = objEstimateSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objEstimateSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objEstimateSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objEstimateSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objEstimateSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objEstimateSalesEntry.Bank = objBank;

                        objEstimateSalesOrder.SalesOrderID = 0;
                        objEstimateSalesEntry.SalesOrder = objEstimateSalesOrder;

                        objEstimateSalesEntry.EstimateSalesPoints = Convert.ToDecimal(drData["EstimateSalesPoints"]);
                        objEstimateSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objEstimateSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objEstimateSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objEstimateSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objEstimateSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objEstimateSalesEntry.Transport = objTransport;

                        objEstimateSalesEntry.EstimateSalesEntryTrans = EstimateSalesEntryTrans.GetEstimateSalesEntryTransByEstimateSalesEntryID(objEstimateSalesEntry.EstimateSalesEntryID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "EstimateSalesEntry GetEstimateSalesEntryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objEstimateSalesEntry;
        }
        public static int AddEstimateSalesEntry(Entity.Billing.EstimateSalesEntry objEstimateSalesEntry)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_ESTIMATESALESENTRY);
                db.AddOutParameter(cmd, "@PK_EstimateSalesEntryID", DbType.Int32, objEstimateSalesEntry.EstimateSalesEntryID);
                db.AddInParameter(cmd, "@InvoiceNo", DbType.String, objEstimateSalesEntry.InvoiceNo);
                db.AddInParameter(cmd, "@InvoiceDate", DbType.String, objEstimateSalesEntry.sInvoiceDate);

                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objEstimateSalesEntry.Customer.CustomerID);
                db.AddInParameter(cmd, "@CustomerName", DbType.String, objEstimateSalesEntry.Customer.CustomerName);
                db.AddInParameter(cmd, "@MobileNo", DbType.String, objEstimateSalesEntry.Customer.MobileNo);
                db.AddInParameter(cmd, "@Area", DbType.String, objEstimateSalesEntry.Customer.Area);
                db.AddInParameter(cmd, "@Address", DbType.String, objEstimateSalesEntry.Customer.Address);
                db.AddInParameter(cmd, "@GSTNo", DbType.String, objEstimateSalesEntry.Customer.GSTNo);

                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objEstimateSalesEntry.Tax.TaxID);

                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objEstimateSalesEntry.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objEstimateSalesEntry.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objEstimateSalesEntry.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objEstimateSalesEntry.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objEstimateSalesEntry.TaxAmount);

                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objEstimateSalesEntry.TotalAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objEstimateSalesEntry.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objEstimateSalesEntry.DiscountAmount);
                db.AddInParameter(cmd, "@AdditionalDiscount", DbType.Decimal, objEstimateSalesEntry.AdditionalDiscount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objEstimateSalesEntry.Roundoff);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objEstimateSalesEntry.NetAmount);
                db.AddInParameter(cmd, "@PaidAmount", DbType.Decimal, objEstimateSalesEntry.PaidAmount);
                db.AddInParameter(cmd, "@BalanceAmount", DbType.Decimal, objEstimateSalesEntry.BalanceAmount);
                db.AddInParameter(cmd, "@FK_EstimateSalesOrderID", DbType.Int32, 0);
                db.AddInParameter(cmd, "@CancelReason", DbType.String, objEstimateSalesEntry.CancelReason);
                db.AddInParameter(cmd, "@IsActive", DbType.Boolean, objEstimateSalesEntry.IsActive);
                db.AddInParameter(cmd, "@PaymentMode", DbType.String, objEstimateSalesEntry.PaymentMode);
                db.AddInParameter(cmd, "@Narration", DbType.String, objEstimateSalesEntry.Narration);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objEstimateSalesEntry.Bank.LedgerID);
                db.AddInParameter(cmd, "@Status", DbType.String, objEstimateSalesEntry.Status);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, objEstimateSalesEntry.IsRetailBill);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objEstimateSalesEntry.CreatedBy.UserID);
                db.AddInParameter(cmd, "@TransportName", DbType.String, objEstimateSalesEntry.TransportName);
                db.AddInParameter(cmd, "@LRNo", DbType.String, objEstimateSalesEntry.LRNo);
                db.AddInParameter(cmd, "@EWayNo", DbType.String, objEstimateSalesEntry.EWayNo);
                db.AddInParameter(cmd, "@VehicleNo", DbType.String, objEstimateSalesEntry.VehicleNo);
                db.AddInParameter(cmd, "@CardNo", DbType.String, objEstimateSalesEntry.CardNo);
                db.AddInParameter(cmd, "@BalanceGiven", DbType.Decimal, objEstimateSalesEntry.BalanceGiven);
                db.AddInParameter(cmd, "@TenderAmount", DbType.Decimal, objEstimateSalesEntry.TenderAmount);
                db.AddInParameter(cmd, "@CardCharges", DbType.Decimal, objEstimateSalesEntry.CardCharges);
                db.AddInParameter(cmd, "@EstimateSalesPoints", DbType.Decimal, objEstimateSalesEntry.EstimateSalesPoints);
                db.AddInParameter(cmd, "@UsedPoints", DbType.Decimal, objEstimateSalesEntry.UsedPoints);
                db.AddInParameter(cmd, "@ExchangeAmount", DbType.Decimal, objEstimateSalesEntry.ExchangeAmount);
                db.AddInParameter(cmd, "@LRDate", DbType.String, objEstimateSalesEntry.sLRDate);
                db.AddInParameter(cmd, "@FK_GiftID", DbType.Int32, objEstimateSalesEntry.Gift.GiftID);
                db.AddInParameter(cmd, "@CustomerType", DbType.String, objEstimateSalesEntry.CustomerType.CustomerTypeName);
                db.AddInParameter(cmd, "@FK_CustomerTypeID", DbType.Int32, objEstimateSalesEntry.CustomerType.CustomertypeID);
                db.AddInParameter(cmd, "@FK_StateID", DbType.Int32, objEstimateSalesEntry.State.StateID);
                db.AddInParameter(cmd, "@TCSPercent", DbType.Decimal, objEstimateSalesEntry.TCSPercent);
                db.AddInParameter(cmd, "@TCSAmount", DbType.Decimal, objEstimateSalesEntry.TCSAmount);
                db.AddInParameter(cmd, "@FK_ShippingAddressID", DbType.Int32, objEstimateSalesEntry.ShippingAddress.ShippingAddressID);
                db.AddInParameter(cmd, "@FK_TransportID", DbType.Int32, objEstimateSalesEntry.Transport.TransportID);
                db.AddInParameter(cmd, "@NoofBags", DbType.String, objEstimateSalesEntry.NoofBags);
                db.AddInParameter(cmd, "@Notes", DbType.String, objEstimateSalesEntry.Notes);
                db.AddInParameter(cmd, "@ImagePath1", DbType.String, objEstimateSalesEntry.ImagePath1);
                db.AddInParameter(cmd, "@ImagePath2", DbType.String, objEstimateSalesEntry.ImagePath2);
                db.AddInParameter(cmd, "@ImagePath3", DbType.String, objEstimateSalesEntry.ImagePath3);
                db.AddInParameter(cmd, "@BillStatus", DbType.String, objEstimateSalesEntry.BillStatus);
                db.AddInParameter(cmd, "@DeliveryDate", DbType.String, objEstimateSalesEntry.sDeliveryDate);
                db.AddInParameter(cmd, "@TransportCharges", DbType.Decimal, objEstimateSalesEntry.TransportCharges);
                db.AddInParameter(cmd, "@OtherCharges", DbType.Decimal, objEstimateSalesEntry.OtherCharges);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objEstimateSalesEntry.FinancialYear.FinancialYearID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_EstimateSalesEntryID"));

                foreach (Entity.Billing.EstimateSalesEntryTrans ObjEstimateSalesEntryTrans in objEstimateSalesEntry.EstimateSalesEntryTrans)
                    ObjEstimateSalesEntryTrans.EstimateSalesEntryID = iID;

                EstimateSalesEntryTrans.SaveEstimateSalesEntryTransaction(objEstimateSalesEntry.EstimateSalesEntryTrans);
            }
            catch (Exception ex)
            {
                sException = "EstimateSalesEntry AddEstimateSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateEstimateSalesEntry(Entity.Billing.EstimateSalesEntry objEstimateSalesEntry)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_ESTIMATESALESENTRY);
                db.AddInParameter(cmd, "@PK_EstimateSalesEntryID", DbType.Int32, objEstimateSalesEntry.EstimateSalesEntryID);
                db.AddInParameter(cmd, "@InvoiceNo", DbType.String, objEstimateSalesEntry.InvoiceNo);
                db.AddInParameter(cmd, "@InvoiceDate", DbType.String, objEstimateSalesEntry.sInvoiceDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objEstimateSalesEntry.Customer.CustomerID);
                db.AddInParameter(cmd, "@CustomerName", DbType.String, objEstimateSalesEntry.Customer.CustomerName);
                db.AddInParameter(cmd, "@MobileNo", DbType.String, objEstimateSalesEntry.Customer.MobileNo);
                db.AddInParameter(cmd, "@Address", DbType.String, objEstimateSalesEntry.Customer.Address);
                db.AddInParameter(cmd, "@Area", DbType.String, objEstimateSalesEntry.Customer.Area);
                db.AddInParameter(cmd, "@GSTNo", DbType.String, objEstimateSalesEntry.Customer.GSTNo);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objEstimateSalesEntry.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objEstimateSalesEntry.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objEstimateSalesEntry.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objEstimateSalesEntry.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objEstimateSalesEntry.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objEstimateSalesEntry.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objEstimateSalesEntry.TotalAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objEstimateSalesEntry.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objEstimateSalesEntry.DiscountAmount);
                db.AddInParameter(cmd, "@AdditionalDiscount", DbType.Decimal, objEstimateSalesEntry.AdditionalDiscount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objEstimateSalesEntry.Roundoff);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objEstimateSalesEntry.NetAmount);
                db.AddInParameter(cmd, "@PaidAmount", DbType.Decimal, objEstimateSalesEntry.PaidAmount);
                db.AddInParameter(cmd, "@BalanceAmount", DbType.Decimal, objEstimateSalesEntry.BalanceAmount);
                db.AddInParameter(cmd, "@FK_EstimateSalesOrderID", DbType.Int32, 0);
                db.AddInParameter(cmd, "@CancelReason", DbType.String, objEstimateSalesEntry.CancelReason);
                db.AddInParameter(cmd, "@IsActive", DbType.Boolean, objEstimateSalesEntry.IsActive);
                db.AddInParameter(cmd, "@PaymentMode", DbType.String, objEstimateSalesEntry.PaymentMode);
                db.AddInParameter(cmd, "@Narration", DbType.String, objEstimateSalesEntry.Narration);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objEstimateSalesEntry.Bank.LedgerID);
                db.AddInParameter(cmd, "@Status", DbType.String, objEstimateSalesEntry.Status);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objEstimateSalesEntry.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@TransportName", DbType.String, objEstimateSalesEntry.TransportName);
                db.AddInParameter(cmd, "@LRNo", DbType.String, objEstimateSalesEntry.LRNo);
                db.AddInParameter(cmd, "@EWayNo", DbType.String, objEstimateSalesEntry.EWayNo);
                db.AddInParameter(cmd, "@VehicleNo", DbType.String, objEstimateSalesEntry.VehicleNo);
                db.AddInParameter(cmd, "@CardNo", DbType.String, objEstimateSalesEntry.CardNo);
                db.AddInParameter(cmd, "@BalanceGiven", DbType.Decimal, objEstimateSalesEntry.BalanceGiven);
                db.AddInParameter(cmd, "@TenderAmount", DbType.Decimal, objEstimateSalesEntry.TenderAmount);
                db.AddInParameter(cmd, "@CardCharges", DbType.Decimal, objEstimateSalesEntry.CardCharges);
                db.AddInParameter(cmd, "@EstimateSalesPoints", DbType.Decimal, objEstimateSalesEntry.EstimateSalesPoints);
                db.AddInParameter(cmd, "@UsedPoints", DbType.Decimal, objEstimateSalesEntry.UsedPoints);
                db.AddInParameter(cmd, "@ExchangeAmount", DbType.Decimal, objEstimateSalesEntry.ExchangeAmount);
                db.AddInParameter(cmd, "@LRDate", DbType.String, objEstimateSalesEntry.sLRDate);
                db.AddInParameter(cmd, "@FK_GiftID", DbType.Int32, objEstimateSalesEntry.Gift.GiftID);
                db.AddInParameter(cmd, "@CustomerType", DbType.String, objEstimateSalesEntry.CustomerType.CustomerTypeName);
                db.AddInParameter(cmd, "@FK_CustomerTypeID", DbType.Int32, objEstimateSalesEntry.CustomerType.CustomertypeID);
                db.AddInParameter(cmd, "@FK_StateID", DbType.Int32, objEstimateSalesEntry.State.StateID);
                db.AddInParameter(cmd, "@TCSPercent", DbType.Decimal, objEstimateSalesEntry.TCSPercent);
                db.AddInParameter(cmd, "@TCSAmount", DbType.Decimal, objEstimateSalesEntry.TCSAmount);
                db.AddInParameter(cmd, "@FK_ShippingAddressID", DbType.Int32, objEstimateSalesEntry.ShippingAddress.ShippingAddressID);
                db.AddInParameter(cmd, "@FK_TransportID", DbType.Int32, objEstimateSalesEntry.Transport.TransportID);
                db.AddInParameter(cmd, "@NoofBags", DbType.String, objEstimateSalesEntry.NoofBags);
                db.AddInParameter(cmd, "@Notes", DbType.String, objEstimateSalesEntry.Notes);
                db.AddInParameter(cmd, "@ImagePath1", DbType.String, objEstimateSalesEntry.ImagePath1);
                db.AddInParameter(cmd, "@ImagePath2", DbType.String, objEstimateSalesEntry.ImagePath2);
                db.AddInParameter(cmd, "@ImagePath3", DbType.String, objEstimateSalesEntry.ImagePath3);
                db.AddInParameter(cmd, "@BillStatus", DbType.String, objEstimateSalesEntry.BillStatus);
                db.AddInParameter(cmd, "@DeliveryDate", DbType.String, objEstimateSalesEntry.sDeliveryDate);
                db.AddInParameter(cmd, "@TransportCharges", DbType.Decimal, objEstimateSalesEntry.TransportCharges);
                db.AddInParameter(cmd, "@OtherCharges", DbType.Decimal, objEstimateSalesEntry.OtherCharges);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objEstimateSalesEntry.FinancialYear.FinancialYearID);

                foreach (Entity.Billing.EstimateSalesEntryTrans ObjEstimateSalesEntryTrans in objEstimateSalesEntry.EstimateSalesEntryTrans)
                    ObjEstimateSalesEntryTrans.EstimateSalesEntryID = objEstimateSalesEntry.EstimateSalesEntryID;

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                EstimateSalesEntryTrans.SaveEstimateSalesEntryTransaction(objEstimateSalesEntry.EstimateSalesEntryTrans);
            }
            catch (Exception ex)
            {
                sException = "EstimateSalesEntry UpdateEstimateSalesEntry| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteEstimateSalesEntry(int iEstimateSalesEntryId, string Reason, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_ESTIMATESALESENTRY);
                db.AddInParameter(cmd, "@PK_EstimateSalesEntryID", DbType.Int32, iEstimateSalesEntryId);
                db.AddInParameter(cmd, "@Reason", DbType.String, Reason);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, iUserId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "EstimateSalesEntry DeleteEstimateSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

        public static bool UpdateEstimateSalesStatus(int iEstimateSalesEntryId, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_ESTIMATESALESENTRYSTATUS);
                db.AddInParameter(cmd, "@PK_EstimateSalesEntryID", DbType.Int32, iEstimateSalesEntryId);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, iUserId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "EstimateSalesEntry DeleteEstimateSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

        public static Collection<Entity.Billing.EstimateSalesEntryTransDetails> GetProductDetails(int iID = 0, int iValue = 0, int iSupplierID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.EstimateSalesEntryTransDetails> objList = new Collection<Entity.Billing.EstimateSalesEntryTransDetails>();
            Entity.Billing.EstimateSalesEntryTransDetails objEstimateSalesEntryTrans = new Entity.Billing.EstimateSalesEntryTransDetails();
            Entity.Master.Product objProduct;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ESTIMATESALESENTRYTRANSDETAILS);
                db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
                db.AddInParameter(cmd, "@Type", DbType.Int32, iValue);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objEstimateSalesEntryTrans = new Entity.Billing.EstimateSalesEntryTransDetails();
                        objProduct = new Entity.Master.Product();

                        objEstimateSalesEntryTrans.EstimateSalesEntryTransID = Convert.ToInt32(drData["PK_EstimateSalesEntryTransID"]);
                        objEstimateSalesEntryTrans.EstimateSalesEntryID = Convert.ToInt32(drData["FK_EstimateSalesEntryID"]);
                        objEstimateSalesEntryTrans.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objEstimateSalesEntryTrans.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objEstimateSalesEntryTrans.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objEstimateSalesEntryTrans.sInvoiceDate = objEstimateSalesEntryTrans.InvoiceDate.ToString("dd/MM/yyyy");

                        objEstimateSalesEntryTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objEstimateSalesEntryTrans.Rate = Convert.ToDecimal(drData["Rate"]);
                        objEstimateSalesEntryTrans.SubTotal = Convert.ToDecimal(drData["SubTotal"]);
                        objEstimateSalesEntryTrans.DiscountPercentage = Convert.ToDecimal(drData["DiscountPercentage"]);
                        objEstimateSalesEntryTrans.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objEstimateSalesEntryTrans.Product = objProduct;
                        objEstimateSalesEntryTrans.Barcode = Convert.ToString(drData["Barcode"]);
                        objList.Add(objEstimateSalesEntryTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "EstimateSalesEntryTrans GetEstimateSalesEntryTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.EstimateSalesEntryTransDetails> GetNewProductDetails(int iID = 0,string code="", int iValue = 0, int iSupplierID = 0, int iEstimateSalesENtryID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.EstimateSalesEntryTransDetails> objList = new Collection<Entity.Billing.EstimateSalesEntryTransDetails>();
            Entity.Billing.EstimateSalesEntryTransDetails objEstimateSalesEntryTrans = new Entity.Billing.EstimateSalesEntryTransDetails();
            Entity.Master.Product objProduct;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_NEWESTIMATESALESENTRYTRANSDETAILS);
                db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
                db.AddInParameter(cmd, "@Type", DbType.Int32, iValue);
                db.AddInParameter(cmd, "@SMSCode", DbType.String, code);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@FK_EstimateSalesEntryID", DbType.Int32, iEstimateSalesENtryID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objEstimateSalesEntryTrans = new Entity.Billing.EstimateSalesEntryTransDetails();
                        objProduct = new Entity.Master.Product();

                        objEstimateSalesEntryTrans.EstimateSalesEntryTransID = Convert.ToInt32(drData["PK_EstimateSalesEntryTransID"]);
                        objEstimateSalesEntryTrans.EstimateSalesEntryID = Convert.ToInt32(drData["FK_EstimateSalesEntryID"]);
                        objEstimateSalesEntryTrans.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objEstimateSalesEntryTrans.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objEstimateSalesEntryTrans.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objEstimateSalesEntryTrans.sInvoiceDate = objEstimateSalesEntryTrans.InvoiceDate.ToString("dd/MM/yyyy");

                        objEstimateSalesEntryTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objEstimateSalesEntryTrans.Rate = Convert.ToDecimal(drData["Rate"]);
                        objEstimateSalesEntryTrans.SubTotal = Convert.ToDecimal(drData["SubTotal"]);
                        objEstimateSalesEntryTrans.DiscountPercentage = Convert.ToDecimal(drData["DiscountPercentage"]);
                        objEstimateSalesEntryTrans.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objEstimateSalesEntryTrans.Product = objProduct;
                        objEstimateSalesEntryTrans.Barcode = Convert.ToString(drData["Barcode"]);
                        objList.Add(objEstimateSalesEntryTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "EstimateSalesEntryTrans GetEstimateSalesEntryTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

      

    }

    public class EstimateSalesEntryTrans
    {
        public static Collection<Entity.Billing.EstimateSalesEntryTrans> GetEstimateSalesEntryTransByEstimateSalesEntryID(int iEstimateSalesEntryID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.EstimateSalesEntryTrans> objList = new Collection<Entity.Billing.EstimateSalesEntryTrans>();
            Entity.Billing.EstimateSalesEntryTrans objEstimateSalesEntryTrans = new Entity.Billing.EstimateSalesEntryTrans();
            Entity.Master.Product objProduct; Entity.Billing.Tax objTax;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ESTIMATESALESENTRYTRANS);
                db.AddInParameter(cmd, "@FK_EstimateSalesEntryID", DbType.Int32, iEstimateSalesEntryID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objEstimateSalesEntryTrans = new Entity.Billing.EstimateSalesEntryTrans();
                        objProduct = new Entity.Master.Product();
                        objTax = new Entity.Billing.Tax();

                        objEstimateSalesEntryTrans.EstimateSalesEntryTransID = Convert.ToInt32(drData["PK_EstimateSalesEntryTransID"]);
                        objEstimateSalesEntryTrans.EstimateSalesEntryID = Convert.ToInt32(drData["FK_EstimateSalesEntryID"]);

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxPercentage = Convert.ToDecimal(drData["TaxPercent"]);
                        objTax.SGSTPercent = Convert.ToDecimal(drData["SGSTPercent"]);
                        objTax.IGSTPercent = Convert.ToDecimal(drData["IGSTPercent"]);
                        objTax.CGSTPercent = Convert.ToDecimal(drData["CGSTPercent"]);
                        objEstimateSalesEntryTrans.Tax = objTax;

                        objEstimateSalesEntryTrans.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objEstimateSalesEntryTrans.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objEstimateSalesEntryTrans.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objEstimateSalesEntryTrans.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objEstimateSalesEntryTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objEstimateSalesEntryTrans.Rate = Convert.ToInt32(drData["Rate"]);
                        objEstimateSalesEntryTrans.SubTotal = Convert.ToDecimal(drData["SubTotal"]);
                        objEstimateSalesEntryTrans.DiscountPercentage = Convert.ToDecimal(drData["DiscountPercentage"]);
                        objEstimateSalesEntryTrans.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);

                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objEstimateSalesEntryTrans.Product = objProduct;
                        objEstimateSalesEntryTrans.Barcode = Convert.ToString(drData["Barcode"]);
                        objEstimateSalesEntryTrans.NewProductFlag = Convert.ToBoolean(drData["NewProductFlag"]);
                        objList.Add(objEstimateSalesEntryTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "EstimateSalesEntryTrans GetEstimateSalesEntryTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveEstimateSalesEntryTransaction(Collection<Entity.Billing.EstimateSalesEntryTrans> ObjEstimateSalesEntryTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.EstimateSalesEntryTrans ObjEstimateSalesEntryTransaction in ObjEstimateSalesEntryTransList)
            {
                if (ObjEstimateSalesEntryTransaction.StatusFlag == "I")
                    iID = AddEstimateSalesEntryTrans(ObjEstimateSalesEntryTransaction);
                else if (ObjEstimateSalesEntryTransaction.StatusFlag == "U")
                    bResult = UpdateEstimateSalesEntryTrans(ObjEstimateSalesEntryTransaction);
                else if (ObjEstimateSalesEntryTransaction.StatusFlag == "D")
                    bResult = DeleteEstimateSalesEntryTrans(ObjEstimateSalesEntryTransaction.EstimateSalesEntryTransID);
            }
        }
        public static int AddEstimateSalesEntryTrans(Entity.Billing.EstimateSalesEntryTrans objEstimateSalesEntryTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_ESTIMATESALESENTRYTRANS);
                db.AddOutParameter(cmd, "@PK_EstimateSalesEntryTransID", DbType.Int32, objEstimateSalesEntryTrans.EstimateSalesEntryTransID);
                db.AddInParameter(cmd, "@FK_EstimateSalesEntryID", DbType.Int32, objEstimateSalesEntryTrans.EstimateSalesEntryID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objEstimateSalesEntryTrans.Product.ProductID);
                db.AddInParameter(cmd, "@SMSCode", DbType.String, objEstimateSalesEntryTrans.Product.SMSCode);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objEstimateSalesEntryTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objEstimateSalesEntryTrans.Rate);
                db.AddInParameter(cmd, "@DiscountPercentage", DbType.Decimal, objEstimateSalesEntryTrans.DiscountPercentage);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objEstimateSalesEntryTrans.DiscountAmount);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objEstimateSalesEntryTrans.SubTotal);
                db.AddInParameter(cmd, "@Barcode", DbType.String, objEstimateSalesEntryTrans.Barcode);
                db.AddInParameter(cmd, "@ProductName", DbType.String, objEstimateSalesEntryTrans.Product.ProductName);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.String, objEstimateSalesEntryTrans.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objEstimateSalesEntryTrans.Tax.TaxPercentage);
                db.AddInParameter(cmd, "@IGSTPercent", DbType.Decimal, objEstimateSalesEntryTrans.Tax.IGSTPercent);
                db.AddInParameter(cmd, "@SGSTPercent", DbType.Decimal, objEstimateSalesEntryTrans.Tax.SGSTPercent);
                db.AddInParameter(cmd, "@CGSTPercent", DbType.Decimal, objEstimateSalesEntryTrans.Tax.CGSTPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objEstimateSalesEntryTrans.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objEstimateSalesEntryTrans.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objEstimateSalesEntryTrans.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objEstimateSalesEntryTrans.TaxAmount);
                db.AddInParameter(cmd, "@NewProductFlag", DbType.Boolean, objEstimateSalesEntryTrans.NewProductFlag);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_EstimateSalesEntryTransID"));
            }
            catch (Exception ex)
            {
                sException = "EstimateSalesEntryTrans AddEstimateSalesEntryTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateEstimateSalesEntryTrans(Entity.Billing.EstimateSalesEntryTrans objEstimateSalesEntryTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_ESTIMATESALESENTRYTRANS);
                db.AddInParameter(cmd, "@PK_EstimateSalesEntryTransID", DbType.Int32, objEstimateSalesEntryTrans.EstimateSalesEntryTransID);
                db.AddInParameter(cmd, "@FK_EstimateSalesEntryID", DbType.Int32, objEstimateSalesEntryTrans.EstimateSalesEntryID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objEstimateSalesEntryTrans.Product.ProductID);
                db.AddInParameter(cmd, "@SMSCode", DbType.String, objEstimateSalesEntryTrans.Product.SMSCode);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objEstimateSalesEntryTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objEstimateSalesEntryTrans.Rate);
                db.AddInParameter(cmd, "@DiscountPercentage", DbType.Decimal, objEstimateSalesEntryTrans.DiscountPercentage);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objEstimateSalesEntryTrans.DiscountAmount);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objEstimateSalesEntryTrans.SubTotal);
                db.AddInParameter(cmd, "@Barcode", DbType.String, objEstimateSalesEntryTrans.Barcode);
                db.AddInParameter(cmd, "@ProductName", DbType.String, objEstimateSalesEntryTrans.Product.ProductName);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.String, objEstimateSalesEntryTrans.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objEstimateSalesEntryTrans.Tax.TaxPercentage);
                db.AddInParameter(cmd, "@IGSTPercent", DbType.Decimal, objEstimateSalesEntryTrans.Tax.IGSTPercent);
                db.AddInParameter(cmd, "@SGSTPercent", DbType.Decimal, objEstimateSalesEntryTrans.Tax.SGSTPercent);
                db.AddInParameter(cmd, "@CGSTPercent", DbType.Decimal, objEstimateSalesEntryTrans.Tax.CGSTPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objEstimateSalesEntryTrans.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objEstimateSalesEntryTrans.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objEstimateSalesEntryTrans.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objEstimateSalesEntryTrans.TaxAmount);
                db.AddInParameter(cmd, "@NewProductFlag", DbType.Boolean, objEstimateSalesEntryTrans.NewProductFlag);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "EstimateSalesEntryTrans UpdateEstimateSalesEntryTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteEstimateSalesEntryTrans(int iEstimateSalesEntryTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_ESTIMATESALESENTRYTRANS);
                db.AddInParameter(cmd, "@PK_EstimateSalesEntryTransID", DbType.Int32, iEstimateSalesEntryTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "EstimateSalesEntryTrans DeleteEstimateSalesEntryTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

    }
}
