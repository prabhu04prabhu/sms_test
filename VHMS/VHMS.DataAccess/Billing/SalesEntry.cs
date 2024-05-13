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
    public class SalesEntry
    {
        public static Collection<Entity.Billing.SalesEntry> GetSalesEntry(int ipatientID = 0, int IsRetail = 0, int IsYarnBill = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesEntry> objList = new Collection<Entity.Billing.SalesEntry>();
            Entity.Billing.SalesEntry objSalesEntry;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESENTRY);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, IsYarnBill);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["PK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesEntry.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSalesEntry.Agent = objAgent;

                        objSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objSalesEntry.sLRDate = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objSalesEntry.ACKno = Convert.ToString(drData["ACKno"]);
                        objSalesEntry.IRNno = Convert.ToString(drData["IRNno"]);
                        objSalesEntry.Notes = Convert.ToString(drData["Notes"]);
                        objSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objSalesEntry.sDeliveryDate = objSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objSalesEntry.Bank = objBank;

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objSalesEntry.SalesOrder = objSalesOrder;

                        objSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objSalesEntry.Transport = objTransport;

                        objSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSalesEntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objSalesEntry.sInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy") + " " + objSalesEntry.CreatedOn.ToString("h:mm");
                        objSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesEntry.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);
                        objSalesEntry.Order_No = Convert.ToString(drData["Order_No"]);

                        objList.Add(objSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry GetSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.SalesEntry> GetTDSSalesEntry(int ipatientID = 0, int TDSSalesID = 0, int IsRetail = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesEntry> objList = new Collection<Entity.Billing.SalesEntry>();
            Entity.Billing.SalesEntry objSalesEntry;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TDSSALESENTRY);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.Boolean, TDSSalesID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["PK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesEntry.Tax = objTax;
                        objSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objSalesEntry.sLRDate = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objSalesEntry.Notes = Convert.ToString(drData["Notes"]);
                        objSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objSalesEntry.sDeliveryDate = objSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSalesEntry.Agent = objAgent;

                        objSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objSalesEntry.Bank = objBank;

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objSalesEntry.SalesOrder = objSalesOrder;

                        objSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objSalesEntry.Transport = objTransport;

                        objSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSalesEntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objSalesEntry.sInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy");
                        objSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesEntry.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);

                        objList.Add(objSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry GetSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.SalesEntry> GetAdjustTDSSalesEntry(int ipatientID = 0, int TDSSalesID = 0, int IsRetail = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesEntry> objList = new Collection<Entity.Billing.SalesEntry>();
            Entity.Billing.SalesEntry objSalesEntry;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ADJUSTTDSSALESENTRY);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.Boolean, TDSSalesID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["PK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesEntry.Tax = objTax;
                        objSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objSalesEntry.sLRDate = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objSalesEntry.Notes = Convert.ToString(drData["Notes"]);
                        objSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objSalesEntry.sDeliveryDate = objSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSalesEntry.Agent = objAgent;

                        objSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objSalesEntry.Bank = objBank;

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objSalesEntry.SalesOrder = objSalesOrder;

                        objSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objSalesEntry.Transport = objTransport;

                        objSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSalesEntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objSalesEntry.sInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy");
                        objSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesEntry.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);

                        objList.Add(objSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry GetSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.SalesEntry GetLastInvoiceNo(int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.SalesEntry objUser = new Entity.Billing.SalesEntry();
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
        public static Collection<Entity.Billing.SalesEntry> GetPendingRetailSales(int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesEntry> objList = new Collection<Entity.Billing.SalesEntry>();
            Entity.Billing.SalesEntry objSalesEntry;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PENDINGRETAILSALES);

                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]) + " | " + Convert.ToString(drData["MobileNo"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objSalesEntry.Customer = objCustomer;

                        objList.Add(objSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry GetSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.SalesEntry> GetTopSalesEntry(int ipatientID = 0, int IsRetail = 0, int IsYarnBill = 0, int iCustomerID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesEntry> objList = new Collection<Entity.Billing.SalesEntry>();
            Entity.Billing.SalesEntry objSalesEntry;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPSALESENTRY);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, IsYarnBill);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iCustomerID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["PK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Area = Convert.ToString(drData["area"]);
                        objCustomer.MinDueDays = Convert.ToInt32(drData["MinDueDays"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesEntry.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSalesEntry.Agent = objAgent;

                        objSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objSalesEntry.DueDays = Convert.ToInt32(drData["DueDays"]);
                        objSalesEntry.sLRDate = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objSalesEntry.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objSalesEntry.RetailsPaymentMode = Convert.ToString(drData["RetailsPaymentMode"]);
                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objSalesEntry.Bank = objBank;

                        objSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objSalesEntry.Comments = Convert.ToString(drData["Comments"]);
                        objSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objSalesEntry.SalesOrder = objSalesOrder;

                        objSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSalesEntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objSalesEntry.sCreatedOn = objSalesEntry.CreatedOn.ToString("dd/MM/yyyy h:mm");
                        objSalesEntry.sInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy");
                        objSalesEntry.sWholeSalesInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy") + " " + objSalesEntry.CreatedOn.ToString("h:mm");
                        objSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesEntry.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);
                        objSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objSalesEntry.Transport = objTransport;
                        objSalesEntry.ACKno = Convert.ToString(drData["ACKno"]);
                        objSalesEntry.IRNno = Convert.ToString(drData["IRNno"]);
                        objSalesEntry.Order_No = Convert.ToString(drData["Order_No"]);
                        objList.Add(objSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry GetSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.SalesEntry> GetTopRetailsSalesEntry(int ipatientID = 0, int IsRetail = 0, int IsYarnBill = 0, int iCustomerID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesEntry> objList = new Collection<Entity.Billing.SalesEntry>();
            Entity.Billing.SalesEntry objSalesEntry;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPSALESENTRYBYRETAILS);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, IsYarnBill);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iCustomerID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["PK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Area = Convert.ToString(drData["area"]);
                        objCustomer.MinDueDays = Convert.ToInt32(drData["MinDueDays"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesEntry.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSalesEntry.Agent = objAgent;

                        objSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objSalesEntry.DueDays = Convert.ToInt32(drData["DueDays"]);
                        objSalesEntry.sLRDate = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objSalesEntry.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objSalesEntry.RetailsPaymentMode = Convert.ToString(drData["RetailsPaymentMode"]);
                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objSalesEntry.Bank = objBank;

                        objSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objSalesEntry.Comments = Convert.ToString(drData["Comments"]);
                        objSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objSalesEntry.SalesOrder = objSalesOrder;

                        objSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSalesEntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objSalesEntry.sCreatedOn = objSalesEntry.CreatedOn.ToString("dd/MM/yyyy h:mm");
                        objSalesEntry.sInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy");
                        objSalesEntry.sWholeSalesInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy") + " " + objSalesEntry.CreatedOn.ToString("h:mm");
                        objSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesEntry.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);
                        objSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objSalesEntry.Transport = objTransport;
                        objSalesEntry.ACKno = Convert.ToString(drData["ACKno"]);
                        objSalesEntry.IRNno = Convert.ToString(drData["IRNno"]);
                        objSalesEntry.Order_No = Convert.ToString(drData["Order_No"]);
                        objList.Add(objSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry GetSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.SalesEntry> GetTopSalesEntryDeleteList(int ipatientID = 0, int IsRetail = 0, int iCustomerID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesEntry> objList = new Collection<Entity.Billing.SalesEntry>();
            Entity.Billing.SalesEntry objSalesEntry;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPSALESENTRYDELETE);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iCustomerID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["PK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Area = Convert.ToString(drData["area"]);
                        objCustomer.MinDueDays = Convert.ToInt32(drData["MinDueDays"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesEntry.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSalesEntry.Agent = objAgent;

                        objSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objSalesEntry.DueDays = Convert.ToInt32(drData["DueDays"]);
                        objSalesEntry.sLRDate = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objSalesEntry.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objSalesEntry.RetailsPaymentMode = Convert.ToString(drData["RetailsPaymentMode"]);
                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objSalesEntry.Bank = objBank;

                        objSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objSalesEntry.SalesOrder = objSalesOrder;

                        objSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSalesEntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objSalesEntry.sInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy") + " " + objSalesEntry.CreatedOn.ToString("h:mm");
                        objSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesEntry.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);
                        objSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objSalesEntry.Transport = objTransport;

                        objList.Add(objSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry GetSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.SalesEntry> GetSalesEntryBookingBill(int ipatientID = 0, int IsRetail = 0, int IsYarnBill = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesEntry> objList = new Collection<Entity.Billing.SalesEntry>();
            Entity.Billing.SalesEntry objSalesEntry;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESENTRYBOOKINGBILL);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, IsYarnBill);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["PK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Days = Convert.ToInt32(drData["Days"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesEntry.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSalesEntry.Agent = objAgent;

                        objSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objSalesEntry.DueDays = Convert.ToInt32(drData["DueDays"]);
                        objSalesEntry.sLRDate = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objSalesEntry.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objSalesEntry.sDeliveryDate = objSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objSalesEntry.Bank = objBank;

                        objSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objSalesEntry.SalesOrder = objSalesOrder;

                        objSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSalesEntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objSalesEntry.sInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy") + " " + objSalesEntry.CreatedOn.ToString("h:mm");
                        objSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesEntry.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objSalesEntry.Transport = objTransport;

                        objList.Add(objSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry GetSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.SalesEntry> SearchSalesEntryBokingBill(string ID, int IsRetail = 0, int IsYarnBill = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesEntry> objList = new Collection<Entity.Billing.SalesEntry>();
            Entity.Billing.SalesEntry objSalesEntry; Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESENTRYBILLBOOKINGDYNAMIC);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.String, ID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, IsYarnBill);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["PK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Days = Convert.ToInt32(drData["Days"]);

                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesEntry.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSalesEntry.Agent = objAgent;

                        objSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objSalesEntry.DueDays = Convert.ToInt32(drData["DueDays"]);
                        objSalesEntry.sLRDate = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objSalesEntry.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objSalesEntry.sDeliveryDate = objSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");


                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objSalesEntry.Bank = objBank;


                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objSalesEntry.Transport = objTransport;
                        objSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objSalesEntry.sDeliveryDate = objSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objSalesEntry.SalesOrder = objSalesOrder;

                        objSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSalesEntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objSalesEntry.sInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy") + " " + objSalesEntry.CreatedOn.ToString("h:mm");
                        objSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesEntry.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);

                        objList.Add(objSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry GetSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.SalesEntry> GetPendingSalesEntry(int ipatientID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesEntry> objList = new Collection<Entity.Billing.SalesEntry>();
            Entity.Billing.SalesEntry objSalesEntry;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PENDINGSALESENTRY);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["PK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesEntry.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSalesEntry.Agent = objAgent;

                        objSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objSalesEntry.sLRDate = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSalesEntry.ReturnAmount = Convert.ToDecimal(drData["ReturnAmount"]);
                        objSalesEntry.TDSPayment = Convert.ToDecimal(drData["TDSPayment"]);
                        objSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objSalesEntry.sDeliveryDate = objSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);
                        objSalesEntry.SalesDiscountAmount = Convert.ToDecimal(drData["SalesDiscountAmount"]);

                        objSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objSalesEntry.Bank = objBank;

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objSalesEntry.SalesOrder = objSalesOrder;

                        objSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSalesEntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objSalesEntry.sInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy") + " " + objSalesEntry.CreatedOn.ToString("h:mm");
                        objSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesEntry.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);

                        objList.Add(objSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry GetSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.RetailPaymentMode> GetAmountClearEntry(int ipatientID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.RetailPaymentMode> objList = new Collection<Entity.Billing.RetailPaymentMode>();
            Entity.Billing.SalesEntry objSalesEntry;
            Entity.Billing.RetailPaymentMode objRetailPaymentMode;
            Entity.Customer objCustomer;
            Entity.Ledger objBank;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USB_SELECT_RETAILAMOUNTCLEAR);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objRetailPaymentMode = new Entity.Billing.RetailPaymentMode();
                        objCustomer = new Entity.Customer();
                        objBank = new Entity.Ledger();

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSalesEntry.sInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objSalesEntry.Customer = objCustomer;

                        objRetailPaymentMode.Status = Convert.ToString(drData["Status"]);
                        objRetailPaymentMode.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objRetailPaymentMode.Amount = Convert.ToDecimal(drData["Amount"]);
                        objRetailPaymentMode.ChequeNo = Convert.ToString(drData["ChequeNo"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objSalesEntry.Bank = objBank;

                        objRetailPaymentMode.SalesEntry = objSalesEntry;

                        objList.Add(objRetailPaymentMode);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry GetSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.SalesEntry> GetPendingRetailBills(int ipatientID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesEntry> objList = new Collection<Entity.Billing.SalesEntry>();
            Entity.Billing.SalesEntry objSalesEntry;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
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
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["PK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesEntry.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSalesEntry.Agent = objAgent;

                        objSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objSalesEntry.sLRDate = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSalesEntry.ReturnAmount = Convert.ToDecimal(drData["ReturnAmount"]);
                        objSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objSalesEntry.sDeliveryDate = objSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        //objSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        //objSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objSalesEntry.Bank = objBank;

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objSalesEntry.SalesOrder = objSalesOrder;

                        objSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);
                        objSalesEntry.SalesExchangeAmt = Convert.ToDecimal(drData["SalesExchangeAmt"]);

                        objSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSalesEntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objSalesEntry.sInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy") + " " + objSalesEntry.CreatedOn.ToString("h:mm");
                        objSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesEntry.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);

                        objList.Add(objSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry GetSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.SalesEntry> SearchSalesEntry(string ID, int IsRetail = 0, int IsYarnBill = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesEntry> objList = new Collection<Entity.Billing.SalesEntry>();
            Entity.Billing.SalesEntry objSalesEntry; Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESENTRYDYNAMIC);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, IsYarnBill);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["PK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MinDueDays = Convert.ToInt32(drData["MinDueDays"]);
                        objCustomer.Area = Convert.ToString(drData["Area"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesEntry.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSalesEntry.Agent = objAgent;

                        objSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objSalesEntry.DueDays = Convert.ToInt32(drData["DueDays"]);
                        objSalesEntry.sLRDate = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objSalesEntry.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objSalesEntry.sDeliveryDate = objSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objSalesEntry.Bank = objBank;


                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objSalesEntry.ShippingAddress = objShippingAddress;
                        objSalesEntry.RetailsPaymentMode = Convert.ToString(drData["RetailsPaymentMode"]);

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objSalesEntry.Transport = objTransport;
                        objSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objSalesEntry.sDeliveryDate = objSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objSalesEntry.SalesOrder = objSalesOrder;

                        objSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSalesEntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objSalesEntry.sCreatedOn = objSalesEntry.CreatedOn.ToString("dd/MM/yyyy h:mm");
                        objSalesEntry.sInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy");
                        objSalesEntry.sWholeSalesInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy") + " " + objSalesEntry.CreatedOn.ToString("h:mm");

                        objSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesEntry.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);
                        objSalesEntry.Order_No = Convert.ToString(drData["Order_No"]);

                        objList.Add(objSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry GetSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.SalesEntry> SearchSalesEntryDeleteList(string ID, int IsRetail = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesEntry> objList = new Collection<Entity.Billing.SalesEntry>();
            Entity.Billing.SalesEntry objSalesEntry; Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESENTRYDELETELISTDYNAMIC);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["PK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MinDueDays = Convert.ToInt32(drData["MinDueDays"]);
                        objCustomer.Area = Convert.ToString(drData["Area"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesEntry.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSalesEntry.Agent = objAgent;

                        objSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objSalesEntry.DueDays = Convert.ToInt32(drData["DueDays"]);
                        objSalesEntry.sLRDate = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objSalesEntry.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objSalesEntry.sDeliveryDate = objSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objSalesEntry.Bank = objBank;


                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objSalesEntry.ShippingAddress = objShippingAddress;
                        objSalesEntry.RetailsPaymentMode = Convert.ToString(drData["RetailsPaymentMode"]);

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objSalesEntry.Transport = objTransport;
                        objSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objSalesEntry.sDeliveryDate = objSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objSalesEntry.SalesOrder = objSalesOrder;

                        objSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSalesEntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objSalesEntry.sInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy") + " " + objSalesEntry.CreatedOn.ToString("h:mm");
                        objSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesEntry.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);

                        objList.Add(objSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry GetSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.SalesEntry GetSalesEntryByInvoice(string InvoiceNo, int FK_FinancialYearID = 0,int IsRetail=0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.SalesEntry objSalesEntry = new Entity.Billing.SalesEntry();
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESENTRYINVOICE);
                db.AddInParameter(cmd, "@InvoiceNo", DbType.String, InvoiceNo);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Int32, IsRetail);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {

                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["PK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSalesEntry.sInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesEntry.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSalesEntry.Agent = objAgent;


                        objSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objSalesEntry.sLRDate = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objSalesEntry.sDeliveryDate = objSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objSalesEntry.Bank = objBank;
                        objSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objSalesEntry.SalesOrder = objSalesOrder;

                        objSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesEntry.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objSalesEntry.Transport = objTransport;

                        objSalesEntry.SalesEntryTrans = SalesEntryTrans.GetSalesEntryTransBySalesEntryID(objSalesEntry.SalesEntryID);
                        objSalesEntry.SalesExchangeTrans = SalesExchangeTrans.GetSalesExchangeTransBySalesEntryID(objSalesEntry.SalesEntryID);
                        objSalesEntry.RetailPaymentMode = RetailPaymentMode.GetRetailPaymentModeByID(objSalesEntry.SalesEntryID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Sales GetSalesByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSalesEntry;
        }
        public static Entity.Billing.SalesEntry GetSalesEntryByInvoiceReturn(string InvoiceNo, int SalesReturnID, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.SalesEntry objSalesEntry = new Entity.Billing.SalesEntry();
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESENTRYINVOICERETURN);
                db.AddInParameter(cmd, "@InvoiceNo", DbType.String, InvoiceNo);
                db.AddInParameter(cmd, "@FK_SalesReturnID", DbType.String, SalesReturnID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {

                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["PK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSalesEntry.sInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesEntry.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSalesEntry.Agent = objAgent;

                        objSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objSalesEntry.sLRDate = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objSalesEntry.sDeliveryDate = objSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objSalesEntry.Bank = objBank;
                        objSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objSalesEntry.SalesOrder = objSalesOrder;

                        objSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesEntry.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objSalesEntry.Transport = objTransport;

                        objSalesEntry.SalesEntryTrans = SalesEntryTrans.GetSalesEntryTransBySalesEntryID(objSalesEntry.SalesEntryID);
                        objSalesEntry.SalesExchangeTrans = SalesExchangeTrans.GetSalesExchangeTransBySalesEntryID(objSalesEntry.SalesEntryID);
                        objSalesEntry.RetailPaymentMode = RetailPaymentMode.GetRetailPaymentModeByID(objSalesEntry.SalesEntryID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Sales GetSalesByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSalesEntry;
        }

        public static Collection<Entity.Billing.SalesEntry> GetPendingSalesDiscountBillNo(int iCustomerID = 0, int IsRetailBill = 1, int SalesReturnID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesEntry> objList = new Collection<Entity.Billing.SalesEntry>();
            Entity.Billing.SalesEntry objSalesEntry;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax; Entity.Billing.SalesOrder objSalesOrder;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PENDINGSALESENTRY);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iCustomerID);

                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objSalesOrder = new Entity.Billing.SalesOrder();

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["PK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSalesEntry.sInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy") + " " + objSalesEntry.CreatedOn.ToString("h:mm");
                        // objSalesEntry.sBillDate = objPurchase.BillDate.ToString("dd/MM/yyyy");

                        objList.Add(objSalesEntry);
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
        public static Collection<Entity.Billing.SalesEntry> GetSalesDiscountBillNo(int iCustomerID = 0, int SalesReturnID = 0, int IsRetailBill = 1, int FK_FinancialYearID = 0, int IsActive = 1)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesEntry> objList = new Collection<Entity.Billing.SalesEntry>();
            Entity.Billing.SalesEntry objSalesEntry;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax; Entity.Billing.SalesOrder objSalesEntryOrder;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALES_GETBILLNO_DISCOUNT);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetailBill);
                db.AddInParameter(cmd, "@IsActive", DbType.Boolean, IsActive);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iCustomerID);
                db.AddInParameter(cmd, "@FK_SalesReturnID", DbType.Int32, SalesReturnID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objSalesEntryOrder = new Entity.Billing.SalesOrder();

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["PK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSalesEntry.sInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy") + " " + objSalesEntry.CreatedOn.ToString("h:mm");


                        objList.Add(objSalesEntry);
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

        public static Entity.Billing.SalesEntry GetSalesEntryByID(int iSalesEntryID, int IsRetail, int IsYarnBill = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.SalesEntry objSalesEntry = new Entity.Billing.SalesEntry();
            Entity.Customer objCustomer; Entity.Billing.Tax objTax; Entity.Gift objGift; Entity.State objState;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder; Entity.CustomerTypes objCustomerTypes;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESENTRY);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.Int32, iSalesEntryID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, IsYarnBill);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objGift = new Entity.Gift();
                        objCustomerTypes = new Entity.CustomerTypes();
                        objState = new Entity.State();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["PK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSalesEntry.sInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy");
                        objSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        string Date = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.sLRDate = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesEntry.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSalesEntry.Agent = objAgent;

                        objCustomerTypes.CustomertypeID = Convert.ToInt32(drData["FK_CustomertypeID"]);
                        objSalesEntry.CustomerType = objCustomerTypes;

                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objSalesEntry.State = objState;

                        objGift.GiftID = Convert.ToInt32(drData["FK_GiftID"]);
                        objSalesEntry.Gift = objGift;
                        objSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSalesEntry.Comments = Convert.ToString(drData["Comments"]);
                        objSalesEntry.Status = Convert.ToString(drData["Status"]);

                        objSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objSalesEntry.Notes = Convert.ToString(drData["Notes"]);
                        objSalesEntry.CardCharges = Convert.ToDecimal(drData["CardCharges"]);
                        objSalesEntry.CardNo = Convert.ToString(drData["CardNo"]);
                        objSalesEntry.TenderAmount = Convert.ToDecimal(drData["TenderAmount"]);
                        objSalesEntry.BalanceGiven = Convert.ToDecimal(drData["BalanceGiven"]);
                        objSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objSalesEntry.sDeliveryDate = objSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objSalesEntry.Bank = objBank;

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objSalesEntry.SalesOrder = objSalesOrder;

                        objSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesEntry.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);
                        objSalesEntry.Order_No = Convert.ToString(drData["Order_No"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objSalesEntry.ShippingAddress = objShippingAddress;

                        objSalesEntry.ACKno = Convert.ToString(drData["ACKno"]);
                        objSalesEntry.IRNno = Convert.ToString(drData["IRNno"]);

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objSalesEntry.Transport = objTransport;

                        objSalesEntry.SalesEntryTrans = SalesEntryTrans.GetSalesEntryTransBySalesEntryID(objSalesEntry.SalesEntryID);
                        objSalesEntry.SalesExchangeTrans = SalesExchangeTrans.GetSalesExchangeTransBySalesEntryID(objSalesEntry.SalesEntryID);
                        objSalesEntry.RetailPaymentMode = RetailPaymentMode.GetRetailPaymentModeByID(objSalesEntry.SalesEntryID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry GetSalesEntryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSalesEntry;
        }

        public static Entity.Billing.SalesEntry GetTDSSalesEntryByID(int iSalesEntryID, int IsRetail, int IsYarnBill = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.SalesEntry objSalesEntry = new Entity.Billing.SalesEntry();
            Entity.Customer objCustomer; Entity.Billing.Tax objTax; Entity.Gift objGift; Entity.State objState;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder; Entity.CustomerTypes objCustomerTypes;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESENTRY);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.Int32, iSalesEntryID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, IsYarnBill);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objGift = new Entity.Gift();
                        objCustomerTypes = new Entity.CustomerTypes();
                        objState = new Entity.State();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["PK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSalesEntry.sInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy");
                        objSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        string Date = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.sLRDate = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesEntry.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSalesEntry.Agent = objAgent;

                        objCustomerTypes.CustomertypeID = Convert.ToInt32(drData["FK_CustomertypeID"]);
                        objSalesEntry.CustomerType = objCustomerTypes;

                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objSalesEntry.State = objState;

                        objGift.GiftID = Convert.ToInt32(drData["FK_GiftID"]);
                        objSalesEntry.Gift = objGift;
                        objSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSalesEntry.Comments = Convert.ToString(drData["Comments"]);
                        objSalesEntry.Status = Convert.ToString(drData["Status"]);

                        objSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objSalesEntry.Notes = Convert.ToString(drData["Notes"]);
                        objSalesEntry.CardCharges = Convert.ToDecimal(drData["CardCharges"]);
                        objSalesEntry.CardNo = Convert.ToString(drData["CardNo"]);
                        objSalesEntry.TenderAmount = Convert.ToDecimal(drData["TenderAmount"]);
                        objSalesEntry.BalanceGiven = Convert.ToDecimal(drData["BalanceGiven"]);
                        objSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objSalesEntry.sDeliveryDate = objSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objSalesEntry.Bank = objBank;

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objSalesEntry.SalesOrder = objSalesOrder;

                        objSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesEntry.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objSalesEntry.ShippingAddress = objShippingAddress;

                        objSalesEntry.ACKno = Convert.ToString(drData["ACKno"]);
                        objSalesEntry.IRNno = Convert.ToString(drData["IRNno"]);

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objSalesEntry.Transport = objTransport;

                        objSalesEntry.SalesEntryTrans = SalesEntryTrans.GetSalesEntryTransBySalesEntryID(objSalesEntry.SalesEntryID);
                        objSalesEntry.SalesExchangeTrans = SalesExchangeTrans.GetSalesExchangeTransBySalesEntryID(objSalesEntry.SalesEntryID);
                        objSalesEntry.RetailPaymentMode = RetailPaymentMode.GetRetailPaymentModeByID(objSalesEntry.SalesEntryID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry GetSalesEntryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSalesEntry;
        }

        public static Entity.Billing.SalesJSON GetSalesEntryJsonFormat(int iSalesEntryID)
        {
            string sException = string.Empty;
            Database db;

            Entity.Billing.SalesJSON objSalesJSON = new Entity.Billing.SalesJSON(); 
            Entity.Billing.TransDetails objTransDetails;
            Entity.Billing.DocumentDetails objDocumentDetails; Entity.Billing.SellerDetails objSellerDetails;
            Entity.Billing.BuyerDetails objBuyerDetails; Entity.Billing.ShippingDetails objShippingDetails;
            Entity.Billing.ValueDetails objValueDetails;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESENTRYJSON);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.Int32, iSalesEntryID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesJSON = new Entity.Billing.SalesJSON();
                        objTransDetails = new Entity.Billing.TransDetails();
                        objDocumentDetails = new Entity.Billing.DocumentDetails();
                        objSellerDetails= new Entity.Billing.SellerDetails();
                        objBuyerDetails = new Entity.Billing.BuyerDetails();
                        objShippingDetails = new Entity.Billing.ShippingDetails();
                        objValueDetails = new Entity.Billing.ValueDetails();

                        objSalesJSON.Version = "1.1";

                        //Trans Details
                        objTransDetails.TaxSch = "GST";
                        objTransDetails.SupTyp = "B2B";
                        objTransDetails.IgstOnIntra = "N";
                        objTransDetails.RegRev = "N";
                        objTransDetails.EcmGstin = null;
                        objSalesJSON.TranDtls = objTransDetails;

                        //Document Details
                        objDocumentDetails.Typ = "INV";
                        objDocumentDetails.No = Convert.ToString(drData["InvoiceNo"]);
                        objDocumentDetails.Dt = Convert.ToDateTime(drData["InvoiceDate"]).ToString("dd/MM/yyyy");
                        objSalesJSON.DocDtls = objDocumentDetails;

                        //Seller Details
                        foreach (DataRow drCompany in dsList.Tables[2].Rows)
                        {
                            objSellerDetails.Gstin = Convert.ToString(drCompany["CSTNo"]);
                            objSellerDetails.LglNm = Convert.ToString(drCompany["CompanyName"]);
                            objSellerDetails.TrdNm = Convert.ToString(drCompany["CompanyName"]);
                            objSellerDetails.Addr1 = Convert.ToString(drCompany["CompanyAddress"]);
                            objSellerDetails.Addr2 = null;
                            objSellerDetails.Loc = "OMALUR";
                            objSellerDetails.Pin = 636455;
                            objSellerDetails.Stcd = "33";
                            string iContact = Convert.ToString(drCompany["ContactNo"]);
                            objSellerDetails.Ph = iContact.Substring(iContact.Length - 10); 
                            objSellerDetails.Em = Convert.ToString(drCompany["Email"]);
                            objSalesJSON.SellerDtls = objSellerDetails;
                        }                                       

                        // Buyer Details
                        objBuyerDetails.Gstin = Convert.ToString(drData["GSTNo"]);
                        objBuyerDetails.LglNm = Convert.ToString(drData["CustomerName"]);
                        objBuyerDetails.TrdNm = Convert.ToString(drData["CustomerName"]);
                        objBuyerDetails.Pos = Convert.ToString(drData["StateCode"]);
                        objBuyerDetails.Addr1 = Convert.ToString(drData["Address"]);
                        objBuyerDetails.Addr2 = null;
                        objBuyerDetails.Loc = Convert.ToString(drData["City"]);
                        objBuyerDetails.Pin = Convert.ToInt32(drData["Pincode"]);
                        objBuyerDetails.Stcd = Convert.ToString(drData["StateCode"]);
                        string iBuyerMobile = Convert.ToString(drData["MobileNo"]);
                        objBuyerDetails.Ph = iBuyerMobile.Length > 0 ? iBuyerMobile.Substring(iBuyerMobile.Length - 10) : null;
                        //objBuyerDetails.Ph = Convert.ToString(drData["MobileNo"]);
                        objBuyerDetails.Em = Convert.ToString(drData["Email"]).Length >0? Convert.ToString(drData["Email"]) : null;
                        objSalesJSON.BuyerDtls = objBuyerDetails;
                       
                        // Shipping Details
                        objShippingDetails.Gstin = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingDetails.LglNm = Convert.ToString(drData["CustomerName"]);
                        objShippingDetails.TrdNm = Convert.ToString(drData["CustomerName"]);
                        objShippingDetails.Addr1 = Convert.ToString(drData["City"]);
                        objShippingDetails.Addr2 = null;
                        objShippingDetails.Loc = Convert.ToString(drData["ShippingBranch"]);
                        objShippingDetails.Pin = Convert.ToInt32(drData["Pincode"]); 
                        objShippingDetails.Stcd = Convert.ToString(drData["StateCode"]);
                        objSalesJSON.ShipDtls = objShippingDetails;

                        // Value Details
                        objValueDetails.AssVal = Convert.ToDecimal(drData["TotalAmount"]);
                        objValueDetails.IgstVal = Convert.ToDecimal(drData["IGSTAmount"]);
                        objValueDetails.CgstVal = Convert.ToDecimal(drData["CGSTAmount"]);
                        objValueDetails.SgstVal = Convert.ToDecimal(drData["SGSTAmount"]);
                        objValueDetails.CesVal = 0;
                        objValueDetails.StCesVal = 0;
                        objValueDetails.Discount = 0;
                        objValueDetails.OthChrg = Convert.ToDecimal(drData["OtherCharges"]);
                        objValueDetails.RndOffAmt = Convert.ToDecimal(drData["Roundoff"]);
                        objValueDetails.TotInvVal = Convert.ToDecimal(drData["NetAmount"]);
                        objValueDetails.TotInvValFc = 0;
                        objSalesJSON.ValDtls = objValueDetails;

                        objSalesJSON.PayDtls = null;
                        objSalesJSON.RefDtls = null;

                        Collection<Entity.Billing.AdditionalDetails> objAddCollection = new Collection<Entity.Billing.AdditionalDetails>();
                        Entity.Billing.AdditionalDetails objAdditional = new Entity.Billing.AdditionalDetails();
                        objAdditional.Url = null;
                        objAdditional.Docs = null;
                        objAdditional.Info = null;
                        objAddCollection.Add(objAdditional);
                        objSalesJSON.AddlDocDtls = objAddCollection;


                        Collection<Entity.Billing.ItemDetails> objItemList = new Collection<Entity.Billing.ItemDetails>();
                        Entity.Billing.ItemDetails objItemDetails;
                        int cnt_val = 1;
                        foreach (DataRow drTrans in dsList.Tables[1].Rows)
                        {
                            objItemDetails= new Entity.Billing.ItemDetails();
                            objItemDetails.SlNo = cnt_val.ToString();
                            objItemDetails.PrdDesc = Convert.ToString(drTrans["ProductName"]);
                            objItemDetails.IsServc = "N";
                            objItemDetails.HsnCd = Convert.ToString(drTrans["HSNCode"]).Length > 0 ? Convert.ToString(drTrans["HSNCode"]) : null; 
                            objItemDetails.Qty = Convert.ToDecimal(drTrans["Quantity"]);
                            objItemDetails.Unit = Convert.ToString(drTrans["UnitName"]);
                            objItemDetails.UnitPrice = Convert.ToDecimal(drTrans["Rate"]);
                            objItemDetails.TotAmt = objItemDetails.Qty * objItemDetails.UnitPrice;
                            objItemDetails.Discount = Convert.ToDecimal(drTrans["DiscountAmount"]);
                            objItemDetails.PreTaxVal = Convert.ToDecimal(drTrans["SubTotal"]);
                            objItemDetails.AssAmt = Convert.ToDecimal(drTrans["SubTotal"]);
                            objItemDetails.GstRt = Convert.ToDecimal(drTrans["TaxPercent"]);
                            objItemDetails.IgstAmt = Convert.ToDecimal(drTrans["IGSTAmount"]);
                            objItemDetails.CgstAmt = Convert.ToDecimal(drTrans["CGSTAmount"]);
                            objItemDetails.SgstAmt = Convert.ToDecimal(drTrans["SGSTAmount"]);
                            objItemDetails.CesRt = 0;
                            objItemDetails.CesAmt = 0;
                            objItemDetails.CesNonAdvlAmt = 0;
                            objItemDetails.StateCesRt = 0;
                            objItemDetails.StateCesAmt = 0;
                            objItemDetails.StateCesNonAdvlAmt = 0;
                            objItemDetails.OthChrg = 0;
                            objItemDetails.TotItemVal = objItemDetails.AssAmt + objItemDetails.IgstAmt + objItemDetails.CgstAmt + objItemDetails.SgstAmt;
                            cnt_val++;

                            objItemList.Add(objItemDetails);
                        }
                        objSalesJSON.ItemList = objItemList;
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry GetSalesEntryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSalesJSON;
        }

        public static Entity.Billing.SalesJSON GetSalesEntryJsonHSNFormat(int iSalesEntryID)
        {
            string sException = string.Empty;
            Database db;

            Entity.Billing.SalesJSON objSalesJSON = new Entity.Billing.SalesJSON();
            Entity.Billing.TransDetails objTransDetails;
            Entity.Billing.DocumentDetails objDocumentDetails; Entity.Billing.SellerDetails objSellerDetails;
            Entity.Billing.BuyerDetails objBuyerDetails; Entity.Billing.ShippingDetails objShippingDetails;
            Entity.Billing.ValueDetails objValueDetails;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESENTRYJSONHSN);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.Int32, iSalesEntryID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesJSON = new Entity.Billing.SalesJSON();
                        objTransDetails = new Entity.Billing.TransDetails();
                        objDocumentDetails = new Entity.Billing.DocumentDetails();
                        objSellerDetails = new Entity.Billing.SellerDetails();
                        objBuyerDetails = new Entity.Billing.BuyerDetails();
                        objShippingDetails = new Entity.Billing.ShippingDetails();
                        objValueDetails = new Entity.Billing.ValueDetails();

                        objSalesJSON.Version = "1.1";

                        //Trans Details
                        objTransDetails.TaxSch = "GST";
                        objTransDetails.SupTyp = "B2B";
                        objTransDetails.IgstOnIntra = "N";
                        objTransDetails.RegRev = "N";
                        objTransDetails.EcmGstin = null;
                        objSalesJSON.TranDtls = objTransDetails;

                        //Document Details
                        objDocumentDetails.Typ = "INV";
                        objDocumentDetails.No = Convert.ToString(drData["InvoiceNo"]);
                        objDocumentDetails.Dt = Convert.ToDateTime(drData["InvoiceDate"]).ToString("dd/MM/yyyy");
                        objSalesJSON.DocDtls = objDocumentDetails;

                        //Seller Details
                        foreach (DataRow drCompany in dsList.Tables[2].Rows)
                        {
                            objSellerDetails.Gstin = Convert.ToString(drCompany["CSTNo"]);
                            objSellerDetails.LglNm = Convert.ToString(drCompany["CompanyName"]);
                            objSellerDetails.TrdNm = Convert.ToString(drCompany["CompanyName"]);
                            objSellerDetails.Addr1 = Convert.ToString(drCompany["CompanyAddress"]);
                            objSellerDetails.Addr2 = null;
                            objSellerDetails.Loc = "OMALUR";
                            objSellerDetails.Pin = 636455;
                            objSellerDetails.Stcd = "33";
                            string iContact = Convert.ToString(drCompany["ContactNo"]);
                            objSellerDetails.Ph = iContact.Substring(iContact.Length - 10);
                            objSellerDetails.Em = Convert.ToString(drCompany["Email"]);
                            objSalesJSON.SellerDtls = objSellerDetails;
                        }

                        // Buyer Details
                        objBuyerDetails.Gstin = Convert.ToString(drData["GSTNo"]);
                        objBuyerDetails.LglNm = Convert.ToString(drData["CustomerName"]);
                        objBuyerDetails.TrdNm = Convert.ToString(drData["CustomerName"]);
                        objBuyerDetails.Pos = Convert.ToString(drData["StateCode"]);

                        string message = Convert.ToString(drData["Address"]);
                        string firstline = message;
                        string secondline = null;
                        if (message.Length > 100)
                        {
                            for (int i = 100; i > 0;)
                            {
                                if (message[i] == ' ')
                                {
                                    firstline = message.Substring(0, i);
                                    secondline = message.Substring(i + 1);
                                    break;
                                }
                                else
                                {
                                    i--;
                                }
                            }
                        }
                        objBuyerDetails.Addr1 = firstline;                        ;
                        objBuyerDetails.Addr2 = secondline;
                        objBuyerDetails.Loc = Convert.ToString(drData["City"]);
                        objBuyerDetails.Pin = Convert.ToInt32(drData["Pincode"]);
                        objBuyerDetails.Stcd = Convert.ToString(drData["StateCode"]);
                        string iBuyerMobile = Convert.ToString(drData["MobileNo"]);
                        objBuyerDetails.Ph = iBuyerMobile.Length > 0 ? iBuyerMobile.Substring(iBuyerMobile.Length - 10) : null;
                        //objBuyerDetails.Ph = Convert.ToString(drData["MobileNo"]);
                        objBuyerDetails.Em = Convert.ToString(drData["Email"]).Length > 0 ? Convert.ToString(drData["Email"]) : null;
                        objSalesJSON.BuyerDtls = objBuyerDetails;

                        // Shipping Details
                        objShippingDetails.Gstin = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingDetails.LglNm = Convert.ToString(drData["CustomerName"]);
                        objShippingDetails.TrdNm = Convert.ToString(drData["CustomerName"]);

                        string s_message = Convert.ToString(drData["ShippingAddress"]);
                        string s_firstline = s_message;
                        string s_secondline = null;
                        if (s_message.Length > 100)
                        {
                            for (int i = 100; i > 0;)
                            {
                                if (s_message[i] == ' ')
                                {
                                    s_firstline = s_message.Substring(0, i);
                                    s_secondline = s_message.Substring(i + 1);
                                    break;
                                }
                                else
                                {
                                    i--;
                                }
                            }
                        }
                        objShippingDetails.Addr1 = s_firstline;
                        objShippingDetails.Addr2 = s_secondline;
                        objShippingDetails.Loc = Convert.ToString(drData["ShippingBranch"]);
                        objShippingDetails.Pin = Convert.ToInt32(drData["Pincode"]);
                        objShippingDetails.Stcd = Convert.ToString(drData["StateCode"]);
                        objSalesJSON.ShipDtls = objShippingDetails;

                        // Value Details
                        objValueDetails.AssVal = Convert.ToDecimal(drData["TotalAmount"]);
                        objValueDetails.IgstVal = Convert.ToDecimal(drData["IGSTAmount"]);
                        objValueDetails.CgstVal = Convert.ToDecimal(drData["CGSTAmount"]);
                        objValueDetails.SgstVal = Convert.ToDecimal(drData["SGSTAmount"]);
                        objValueDetails.CesVal = 0;
                        objValueDetails.StCesVal = 0;
                        objValueDetails.Discount = 0;
                        objValueDetails.OthChrg = Convert.ToDecimal(drData["OtherCharges"]);
                        objValueDetails.RndOffAmt = Convert.ToDecimal(drData["Roundoff"]);
                        objValueDetails.TotInvVal = Convert.ToDecimal(drData["NetAmount"]);
                        objValueDetails.TotInvValFc = 0;
                        objSalesJSON.ValDtls = objValueDetails;

                        objSalesJSON.PayDtls = null;
                        objSalesJSON.RefDtls = null;

                        Collection<Entity.Billing.AdditionalDetails> objAddCollection = new Collection<Entity.Billing.AdditionalDetails>();
                        Entity.Billing.AdditionalDetails objAdditional = new Entity.Billing.AdditionalDetails();
                        objAdditional.Url = null;
                        objAdditional.Docs = null;
                        objAdditional.Info = null;
                        objAddCollection.Add(objAdditional);
                        objSalesJSON.AddlDocDtls = objAddCollection;


                        Collection<Entity.Billing.ItemDetails> objItemList = new Collection<Entity.Billing.ItemDetails>();
                        Entity.Billing.ItemDetails objItemDetails;
                        int cnt_val = 1;
                        foreach (DataRow drTrans in dsList.Tables[1].Rows)
                        {
                            objItemDetails = new Entity.Billing.ItemDetails();
                            objItemDetails.SlNo = cnt_val.ToString();
                            objItemDetails.PrdDesc = Convert.ToString(drTrans["ProductName"]);
                            objItemDetails.IsServc = "N";
                            objItemDetails.PreTaxVal = Convert.ToDecimal(drTrans["SubTotal"]);
                            objItemDetails.AssAmt = Convert.ToDecimal(drTrans["SubTotal"]);
                            objItemDetails.HsnCd = Convert.ToString(drTrans["HSNCode"]).Length > 0 ? Convert.ToString(drTrans["HSNCode"]) : null;
                            objItemDetails.Qty = Convert.ToDecimal(drTrans["Quantity"]);
                            objItemDetails.Unit = Convert.ToString(drTrans["UnitName"]);
                            objItemDetails.Discount = Convert.ToDecimal(drTrans["DiscountAmount"]);
                            objItemDetails.TotAmt = Convert.ToDecimal((objItemDetails.AssAmt + objItemDetails.Discount).ToString("0.00"));
                            objItemDetails.UnitPrice = Convert.ToDecimal((objItemDetails.TotAmt/objItemDetails.Qty).ToString("0.00"));
                          
                           
                            objItemDetails.GstRt = Convert.ToDecimal(drTrans["TaxPercent"]);
                            objItemDetails.IgstAmt = Convert.ToDecimal(drTrans["IGSTAmount"]);
                            objItemDetails.CgstAmt = Convert.ToDecimal(drTrans["CGSTAmount"]);
                            objItemDetails.SgstAmt = Convert.ToDecimal(drTrans["SGSTAmount"]);
                            objItemDetails.CesRt = 0;
                            objItemDetails.CesAmt = 0;
                            objItemDetails.CesNonAdvlAmt = 0;
                            objItemDetails.StateCesRt = 0;
                            objItemDetails.StateCesAmt = 0;
                            objItemDetails.StateCesNonAdvlAmt = 0;
                            objItemDetails.OthChrg = 0;
                            objItemDetails.TotItemVal = objItemDetails.AssAmt + objItemDetails.IgstAmt + objItemDetails.CgstAmt + objItemDetails.SgstAmt;
                            cnt_val++;

                            objItemList.Add(objItemDetails);
                        }
                        objSalesJSON.ItemList = objItemList;
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry GetSalesEntryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSalesJSON;
        }

        public static Entity.Billing.SalesEntry GetSalesEntryReturnByID(int iSalesEntryID, int IsRetail,  int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.SalesEntry objSalesEntry = new Entity.Billing.SalesEntry();
            Entity.Customer objCustomer; Entity.Billing.Tax objTax; Entity.Gift objGift; Entity.State objState;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder; Entity.CustomerTypes objCustomerTypes;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESENTRYBYRETURNID);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.Int32, iSalesEntryID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objGift = new Entity.Gift();
                        objCustomerTypes = new Entity.CustomerTypes();
                        objState = new Entity.State();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["PK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSalesEntry.sInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy");
                        objSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        string Date = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.sLRDate = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesEntry.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSalesEntry.Agent = objAgent;

                        objCustomerTypes.CustomertypeID = Convert.ToInt32(drData["FK_CustomertypeID"]);
                        objSalesEntry.CustomerType = objCustomerTypes;

                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objSalesEntry.State = objState;

                        objGift.GiftID = Convert.ToInt32(drData["FK_GiftID"]);
                        objSalesEntry.Gift = objGift;
                        objSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSalesEntry.Comments = Convert.ToString(drData["Comments"]);
                        objSalesEntry.Status = Convert.ToString(drData["Status"]);

                        objSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objSalesEntry.Notes = Convert.ToString(drData["Notes"]);
                        objSalesEntry.CardCharges = Convert.ToDecimal(drData["CardCharges"]);
                        objSalesEntry.CardNo = Convert.ToString(drData["CardNo"]);
                        objSalesEntry.TenderAmount = Convert.ToDecimal(drData["TenderAmount"]);
                        objSalesEntry.BalanceGiven = Convert.ToDecimal(drData["BalanceGiven"]);
                        objSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objSalesEntry.sDeliveryDate = objSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objSalesEntry.Bank = objBank;

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objSalesEntry.SalesOrder = objSalesOrder;

                        objSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesEntry.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objSalesEntry.Transport = objTransport;

                        objSalesEntry.SalesEntryTrans = SalesEntryTrans.GetSalesEntryTransBySalesEntryID(objSalesEntry.SalesEntryID);
                        objSalesEntry.SalesExchangeTrans = SalesExchangeTrans.GetSalesExchangeTransBySalesEntryID(objSalesEntry.SalesEntryID);
                        objSalesEntry.RetailPaymentMode = RetailPaymentMode.GetRetailPaymentModeByID(objSalesEntry.SalesEntryID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry GetSalesEntryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSalesEntry;
        }
        public static Entity.Billing.SalesEntry GetLastSalesEntryByID(int iSalesEntryID, int IsRetail, int IsYarnBill = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.SalesEntry objSalesEntry = new Entity.Billing.SalesEntry();
            Entity.Customer objCustomer; Entity.Billing.Tax objTax; Entity.Gift objGift; Entity.State objState;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder; Entity.CustomerTypes objCustomerTypes;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESENTRYLASTBILL);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.Int32, iSalesEntryID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, IsYarnBill);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objGift = new Entity.Gift();
                        objCustomerTypes = new Entity.CustomerTypes();
                        objState = new Entity.State();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["PK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSalesEntry.sInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy");
                        objSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        string Date = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.sLRDate = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesEntry.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSalesEntry.Agent = objAgent;

                        objCustomerTypes.CustomertypeID = Convert.ToInt32(drData["FK_CustomertypeID"]);
                        objSalesEntry.CustomerType = objCustomerTypes;

                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objSalesEntry.State = objState;

                        objGift.GiftID = Convert.ToInt32(drData["FK_GiftID"]);
                        objSalesEntry.Gift = objGift;
                        objSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSalesEntry.Status = Convert.ToString(drData["Status"]);

                        objSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objSalesEntry.ACKno = Convert.ToString(drData["ACKno"]);
                        objSalesEntry.IRNno = Convert.ToString(drData["IRNno"]);
                        objSalesEntry.Notes = Convert.ToString(drData["Notes"]);
                        objSalesEntry.CardCharges = Convert.ToDecimal(drData["CardCharges"]);
                        objSalesEntry.CardNo = Convert.ToString(drData["CardNo"]);
                        objSalesEntry.TenderAmount = Convert.ToDecimal(drData["TenderAmount"]);
                        objSalesEntry.BalanceGiven = Convert.ToDecimal(drData["BalanceGiven"]);
                        objSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objSalesEntry.sDeliveryDate = objSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objSalesEntry.Bank = objBank;

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objSalesEntry.SalesOrder = objSalesOrder;

                        objSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesEntry.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objSalesEntry.Transport = objTransport;
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry GetSalesEntryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSalesEntry;
        }
        public static Entity.Billing.SalesEntry GetSalesEntryByIDAddress(int iSalesEntryID, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.SalesEntry objSalesEntry = new Entity.Billing.SalesEntry();
            Entity.Customer objCustomer; Entity.Billing.Tax objTax; Entity.Gift objGift; Entity.State objState;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder; Entity.CustomerTypes objCustomerTypes;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESENTRYBYADDRESS);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.Int32, iSalesEntryID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntry = new Entity.Billing.SalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objGift = new Entity.Gift();
                        objCustomerTypes = new Entity.CustomerTypes();
                        objState = new Entity.State();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["PK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSalesEntry.sInvoiceDate = objSalesEntry.InvoiceDate.ToString("dd/MM/yyyy");
                        objSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        string Date = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.sLRDate = objSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesEntry.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSalesEntry.Agent = objAgent;

                        objCustomerTypes.CustomertypeID = Convert.ToInt32(drData["FK_CustomertypeID"]);
                        objSalesEntry.CustomerType = objCustomerTypes;

                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objSalesEntry.State = objState;

                        objGift.GiftID = Convert.ToInt32(drData["FK_GiftID"]);
                        objSalesEntry.Gift = objGift;
                        objSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objSalesEntry.ACKno = Convert.ToString(drData["ACKno"]);
                        objSalesEntry.IRNno = Convert.ToString(drData["IRNno"]);
                        objSalesEntry.Notes = Convert.ToString(drData["Notes"]);
                        objSalesEntry.CardCharges = Convert.ToDecimal(drData["CardCharges"]);
                        objSalesEntry.CardNo = Convert.ToString(drData["CardNo"]);
                        objSalesEntry.TenderAmount = Convert.ToDecimal(drData["TenderAmount"]);
                        objSalesEntry.BalanceGiven = Convert.ToDecimal(drData["BalanceGiven"]);
                        objSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objSalesEntry.sDeliveryDate = objSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objSalesEntry.Bank = objBank;

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objSalesEntry.SalesOrder = objSalesOrder;

                        objSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesEntry.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objSalesEntry.Transport = objTransport;

                        objSalesEntry.SalesEntryTrans = SalesEntryTrans.GetSalesEntryTransBySalesEntryID(objSalesEntry.SalesEntryID);
                        objSalesEntry.SalesExchangeTrans = SalesExchangeTrans.GetSalesExchangeTransBySalesEntryID(objSalesEntry.SalesEntryID);
                        objSalesEntry.RetailPaymentMode = RetailPaymentMode.GetRetailPaymentModeByID(objSalesEntry.SalesEntryID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry GetSalesEntryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSalesEntry;
        }
        public static int AddSalesEntry(Entity.Billing.SalesEntry objSalesEntry)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_SALESENTRY);
                db.AddOutParameter(cmd, "@PK_SalesEntryID", DbType.Int32, objSalesEntry.SalesEntryID);
                db.AddInParameter(cmd, "@InvoiceNo", DbType.String, objSalesEntry.InvoiceNo);
                db.AddInParameter(cmd, "@InvoiceDate", DbType.String, objSalesEntry.sInvoiceDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objSalesEntry.Customer.CustomerID);
                db.AddInParameter(cmd, "@CustomerName", DbType.String, objSalesEntry.Customer.CustomerName);
                db.AddInParameter(cmd, "@MobileNo", DbType.String, objSalesEntry.Customer.MobileNo);
                db.AddInParameter(cmd, "@Area", DbType.String, objSalesEntry.Customer.Area);
                db.AddInParameter(cmd, "@Address", DbType.String, objSalesEntry.Customer.Address);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objSalesEntry.Tax.TaxID);
                db.AddInParameter(cmd, "@FK_AgentID", DbType.Int32, objSalesEntry.Agent.AgentID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objSalesEntry.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objSalesEntry.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objSalesEntry.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objSalesEntry.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objSalesEntry.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objSalesEntry.TotalAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objSalesEntry.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objSalesEntry.DiscountAmount);
                db.AddInParameter(cmd, "@AdditionalDiscount", DbType.Decimal, objSalesEntry.AdditionalDiscount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objSalesEntry.Roundoff);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objSalesEntry.NetAmount);
                db.AddInParameter(cmd, "@PaidAmount", DbType.Decimal, objSalesEntry.PaidAmount);
                db.AddInParameter(cmd, "@BalanceAmount", DbType.Decimal, objSalesEntry.BalanceAmount);
                db.AddInParameter(cmd, "@FK_SalesOrderID", DbType.Int32, objSalesEntry.SalesOrder.SalesOrderID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objSalesEntry.FinancialYear.FinancialYearID);
                db.AddInParameter(cmd, "@CancelReason", DbType.String, objSalesEntry.CancelReason);
                db.AddInParameter(cmd, "@IsActive", DbType.Boolean, objSalesEntry.IsActive);
                db.AddInParameter(cmd, "@PaymentMode", DbType.String, objSalesEntry.PaymentMode);
                db.AddInParameter(cmd, "@Narration", DbType.String, objSalesEntry.Narration);
                db.AddInParameter(cmd, "@Comments", DbType.String, objSalesEntry.Comments);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objSalesEntry.Bank.LedgerID);
                db.AddInParameter(cmd, "@Status", DbType.String, objSalesEntry.Status);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, objSalesEntry.IsRetailBill);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, objSalesEntry.IsYarnBill);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objSalesEntry.CreatedBy.UserID);
                db.AddInParameter(cmd, "@TransportName", DbType.String, objSalesEntry.TransportName);
                db.AddInParameter(cmd, "@LRNo", DbType.String, objSalesEntry.LRNo);
                db.AddInParameter(cmd, "@EWayNo", DbType.String, objSalesEntry.EWayNo);
                db.AddInParameter(cmd, "@VehicleNo", DbType.String, objSalesEntry.VehicleNo);
                db.AddInParameter(cmd, "@CardNo", DbType.String, objSalesEntry.CardNo);
                db.AddInParameter(cmd, "@BalanceGiven", DbType.Decimal, objSalesEntry.BalanceGiven);
                db.AddInParameter(cmd, "@TenderAmount", DbType.Decimal, objSalesEntry.TenderAmount);
                db.AddInParameter(cmd, "@CardCharges", DbType.Decimal, objSalesEntry.CardCharges);
                db.AddInParameter(cmd, "@SalesPoints", DbType.Decimal, objSalesEntry.SalesPoints);
                db.AddInParameter(cmd, "@UsedPoints", DbType.Decimal, objSalesEntry.UsedPoints);
                db.AddInParameter(cmd, "@ExchangeAmount", DbType.Decimal, objSalesEntry.ExchangeAmount);
                db.AddInParameter(cmd, "@LRDate", DbType.String, objSalesEntry.sLRDate);
                db.AddInParameter(cmd, "@FK_GiftID", DbType.Int32, objSalesEntry.Gift.GiftID);
                db.AddInParameter(cmd, "@CustomerType", DbType.String, objSalesEntry.CustomerType.CustomerTypeName);
                db.AddInParameter(cmd, "@FK_CustomerTypeID", DbType.Int32, objSalesEntry.CustomerType.CustomertypeID);
                db.AddInParameter(cmd, "@FK_StateID", DbType.Int32, objSalesEntry.State.StateID);
                db.AddInParameter(cmd, "@TCSPercent", DbType.Decimal, objSalesEntry.TCSPercent);
                db.AddInParameter(cmd, "@TCSAmount", DbType.Decimal, objSalesEntry.TCSAmount);
                db.AddInParameter(cmd, "@FK_ShippingAddressID", DbType.Int32, objSalesEntry.ShippingAddress.ShippingAddressID);
                db.AddInParameter(cmd, "@FK_TransportID", DbType.Int32, objSalesEntry.Transport.TransportID);
                db.AddInParameter(cmd, "@NoofBags", DbType.String, objSalesEntry.NoofBags);
                db.AddInParameter(cmd, "@Notes", DbType.String, objSalesEntry.Notes);
                db.AddInParameter(cmd, "@ImagePath1", DbType.String, objSalesEntry.ImagePath1);
                db.AddInParameter(cmd, "@ImagePath2", DbType.String, objSalesEntry.ImagePath2);
                db.AddInParameter(cmd, "@ImagePath3", DbType.String, objSalesEntry.ImagePath3);
                db.AddInParameter(cmd, "@BillStatus", DbType.String, objSalesEntry.BillStatus);
                db.AddInParameter(cmd, "@DeliveryDate", DbType.String, objSalesEntry.sDeliveryDate);
                db.AddInParameter(cmd, "@TransportCharges", DbType.Decimal, objSalesEntry.TransportCharges);
                db.AddInParameter(cmd, "@OtherCharges", DbType.Decimal, objSalesEntry.OtherCharges);
                db.AddInParameter(cmd, "@ACKno", DbType.String, objSalesEntry.ACKno);
                db.AddInParameter(cmd, "@IRNno", DbType.String, objSalesEntry.IRNno);
                db.AddInParameter(cmd, "@Order_No", DbType.String, objSalesEntry.Order_No);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_SalesEntryID"));

                foreach (Entity.Billing.SalesEntryTrans ObjSalesEntryTrans in objSalesEntry.SalesEntryTrans)
                    ObjSalesEntryTrans.SalesEntryID = iID;

                foreach (Entity.Billing.SalesExchangeTrans ObjSalesEntryTrans in objSalesEntry.SalesExchangeTrans)
                    ObjSalesEntryTrans.SalesEntryID = iID;

                foreach (Entity.Billing.RetailPaymentMode ObjSalesEntryTrans in objSalesEntry.RetailPaymentMode)
                    ObjSalesEntryTrans.SalesEntryID = iID;

                SalesEntryTrans.SaveSalesEntryTransaction(objSalesEntry.SalesEntryTrans);
                SalesExchangeTrans.SaveSalesExchangeTransaction(objSalesEntry.SalesExchangeTrans);
                RetailPaymentMode.SaveRetailPaymentModeaction(objSalesEntry.RetailPaymentMode);
            }
            catch (Exception ex)
            {
                sException = "SalesEntry AddSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateSalesEntry(Entity.Billing.SalesEntry objSalesEntry)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_SALESENTRY);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.Int32, objSalesEntry.SalesEntryID);
                db.AddInParameter(cmd, "@InvoiceNo", DbType.String, objSalesEntry.InvoiceNo);
                db.AddInParameter(cmd, "@InvoiceDate", DbType.String, objSalesEntry.sInvoiceDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objSalesEntry.Customer.CustomerID);
                db.AddInParameter(cmd, "@CustomerName", DbType.String, objSalesEntry.Customer.CustomerName);
                db.AddInParameter(cmd, "@MobileNo", DbType.String, objSalesEntry.Customer.MobileNo);
                db.AddInParameter(cmd, "@Address", DbType.String, objSalesEntry.Customer.Address);
                db.AddInParameter(cmd, "@Area", DbType.String, objSalesEntry.Customer.Area);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objSalesEntry.Tax.TaxID);
                db.AddInParameter(cmd, "@FK_AgentID", DbType.Int32, objSalesEntry.Agent.AgentID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objSalesEntry.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objSalesEntry.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objSalesEntry.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objSalesEntry.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objSalesEntry.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objSalesEntry.TotalAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objSalesEntry.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objSalesEntry.DiscountAmount);
                db.AddInParameter(cmd, "@AdditionalDiscount", DbType.Decimal, objSalesEntry.AdditionalDiscount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objSalesEntry.Roundoff);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objSalesEntry.NetAmount);
                db.AddInParameter(cmd, "@PaidAmount", DbType.Decimal, objSalesEntry.PaidAmount);
                db.AddInParameter(cmd, "@BalanceAmount", DbType.Decimal, objSalesEntry.BalanceAmount);
                db.AddInParameter(cmd, "@FK_SalesOrderID", DbType.Int32, objSalesEntry.SalesOrder.SalesOrderID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objSalesEntry.FinancialYear.FinancialYearID);
                db.AddInParameter(cmd, "@CancelReason", DbType.String, objSalesEntry.CancelReason);
                db.AddInParameter(cmd, "@IsActive", DbType.Boolean, objSalesEntry.IsActive);
                db.AddInParameter(cmd, "@PaymentMode", DbType.String, objSalesEntry.PaymentMode);
                db.AddInParameter(cmd, "@Narration", DbType.String, objSalesEntry.Narration);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objSalesEntry.Bank.LedgerID);
                db.AddInParameter(cmd, "@Status", DbType.String, objSalesEntry.Status);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objSalesEntry.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@TransportName", DbType.String, objSalesEntry.TransportName);
                db.AddInParameter(cmd, "@LRNo", DbType.String, objSalesEntry.LRNo);
                db.AddInParameter(cmd, "@EWayNo", DbType.String, objSalesEntry.EWayNo);
                db.AddInParameter(cmd, "@VehicleNo", DbType.String, objSalesEntry.VehicleNo);
                db.AddInParameter(cmd, "@CardNo", DbType.String, objSalesEntry.CardNo);
                db.AddInParameter(cmd, "@BalanceGiven", DbType.Decimal, objSalesEntry.BalanceGiven);
                db.AddInParameter(cmd, "@TenderAmount", DbType.Decimal, objSalesEntry.TenderAmount);
                db.AddInParameter(cmd, "@CardCharges", DbType.Decimal, objSalesEntry.CardCharges);
                db.AddInParameter(cmd, "@SalesPoints", DbType.Decimal, objSalesEntry.SalesPoints);
                db.AddInParameter(cmd, "@UsedPoints", DbType.Decimal, objSalesEntry.UsedPoints);
                db.AddInParameter(cmd, "@ExchangeAmount", DbType.Decimal, objSalesEntry.ExchangeAmount);
                db.AddInParameter(cmd, "@LRDate", DbType.String, objSalesEntry.sLRDate);
                db.AddInParameter(cmd, "@FK_GiftID", DbType.Int32, objSalesEntry.Gift.GiftID);
                db.AddInParameter(cmd, "@CustomerType", DbType.String, objSalesEntry.CustomerType.CustomerTypeName);
                db.AddInParameter(cmd, "@FK_CustomerTypeID", DbType.Int32, objSalesEntry.CustomerType.CustomertypeID);
                db.AddInParameter(cmd, "@FK_StateID", DbType.Int32, objSalesEntry.State.StateID);
                db.AddInParameter(cmd, "@TCSPercent", DbType.Decimal, objSalesEntry.TCSPercent);
                db.AddInParameter(cmd, "@TCSAmount", DbType.Decimal, objSalesEntry.TCSAmount);
                db.AddInParameter(cmd, "@FK_ShippingAddressID", DbType.Int32, objSalesEntry.ShippingAddress.ShippingAddressID);
                db.AddInParameter(cmd, "@FK_TransportID", DbType.Int32, objSalesEntry.Transport.TransportID);
                db.AddInParameter(cmd, "@NoofBags", DbType.String, objSalesEntry.NoofBags);
                db.AddInParameter(cmd, "@Notes", DbType.String, objSalesEntry.Notes);
                db.AddInParameter(cmd, "@ImagePath1", DbType.String, objSalesEntry.ImagePath1);
                db.AddInParameter(cmd, "@ImagePath2", DbType.String, objSalesEntry.ImagePath2);
                db.AddInParameter(cmd, "@ImagePath3", DbType.String, objSalesEntry.ImagePath3);
                db.AddInParameter(cmd, "@BillStatus", DbType.String, objSalesEntry.BillStatus);
                db.AddInParameter(cmd, "@Comments", DbType.String, objSalesEntry.Comments);
                db.AddInParameter(cmd, "@DeliveryDate", DbType.String, objSalesEntry.sDeliveryDate);
                db.AddInParameter(cmd, "@TransportCharges", DbType.Decimal, objSalesEntry.TransportCharges);
                db.AddInParameter(cmd, "@OtherCharges", DbType.Decimal, objSalesEntry.OtherCharges);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, objSalesEntry.IsYarnBill);
                db.AddInParameter(cmd, "@ACKno", DbType.String, objSalesEntry.ACKno);
                db.AddInParameter(cmd, "@IRNno", DbType.String, objSalesEntry.IRNno);
                db.AddInParameter(cmd, "@Order_No", DbType.String, objSalesEntry.Order_No);
                //  db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, objSalesEntry.IsRetailBill);

                foreach (Entity.Billing.SalesEntryTrans ObjSalesEntryTrans in objSalesEntry.SalesEntryTrans)
                    ObjSalesEntryTrans.SalesEntryID = objSalesEntry.SalesEntryID;

                foreach (Entity.Billing.SalesExchangeTrans ObjSalesEntryTrans in objSalesEntry.SalesExchangeTrans)
                    ObjSalesEntryTrans.SalesEntryID = objSalesEntry.SalesEntryID;

                foreach (Entity.Billing.RetailPaymentMode ObjSalesEntryTrans in objSalesEntry.RetailPaymentMode)
                    ObjSalesEntryTrans.SalesEntryID = objSalesEntry.SalesEntryID;

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                SalesEntryTrans.SaveSalesEntryTransaction(objSalesEntry.SalesEntryTrans);
                SalesExchangeTrans.SaveSalesExchangeTransaction(objSalesEntry.SalesExchangeTrans);
                RetailPaymentMode.SaveRetailPaymentModeaction(objSalesEntry.RetailPaymentMode);
            }
            catch (Exception ex)
            {
                sException = "SalesEntry UpdateSalesEntry| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteSalesEntry(int iSalesEntryId, string Reason, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_SALESENTRY);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.Int32, iSalesEntryId);
                db.AddInParameter(cmd, "@Reason", DbType.String, Reason);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, iUserId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "SalesEntry DeleteSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

        public static decimal GetTransStockByID(int productID, int TransID, string Tablename)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            decimal stockcnt = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TRANSSTOCK);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, productID);
                db.AddInParameter(cmd, "@FK_TransID", DbType.Int32, TransID);
                db.AddInParameter(cmd, "@Tablename", DbType.String, Tablename);
                DataSet dsList = db.ExecuteDataSet(cmd);
                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        stockcnt = Convert.ToDecimal(drData["Quantity"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry DeleteSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return stockcnt;
        }
        public static Collection<Entity.Billing.SalesEntryTransDetails> GetProductDetails(int iID = 0, int iValue = 0, int iSupplierID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesEntryTransDetails> objList = new Collection<Entity.Billing.SalesEntryTransDetails>();
            Entity.Billing.SalesEntryTransDetails objSalesEntryTrans = new Entity.Billing.SalesEntryTransDetails();
            Entity.Master.Product objProduct;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESENTRYTRANSDETAILS);
                db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
                db.AddInParameter(cmd, "@Type", DbType.Int32, iValue);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntryTrans = new Entity.Billing.SalesEntryTransDetails();
                        objProduct = new Entity.Master.Product();

                        objSalesEntryTrans.SalesEntryTransID = Convert.ToInt32(drData["PK_SalesEntryTransID"]);
                        objSalesEntryTrans.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objSalesEntryTrans.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objSalesEntryTrans.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntryTrans.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSalesEntryTrans.sInvoiceDate = objSalesEntryTrans.InvoiceDate.ToString("dd/MM/yyyy");

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

        public static Collection<Entity.Billing.SalesEntryTransDetails> GetProductDetailsBySMSCode(int iID = 0, int iValue = 0, int iSupplierID = 0,string iSMSCode=null)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesEntryTransDetails> objList = new Collection<Entity.Billing.SalesEntryTransDetails>();
            Entity.Billing.SalesEntryTransDetails objSalesEntryTrans = new Entity.Billing.SalesEntryTransDetails();
            Entity.Master.Product objProduct;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESENTRYTRANSDETAILSBYSMSCODE);
                db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
                db.AddInParameter(cmd, "@Type", DbType.Int32, iValue);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@ProductCode", DbType.String, iSMSCode);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntryTrans = new Entity.Billing.SalesEntryTransDetails();
                        objProduct = new Entity.Master.Product();

                        objSalesEntryTrans.SalesEntryTransID = Convert.ToInt32(drData["PK_SalesEntryTransID"]);
                        objSalesEntryTrans.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objSalesEntryTrans.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objSalesEntryTrans.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntryTrans.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSalesEntryTrans.sInvoiceDate = objSalesEntryTrans.InvoiceDate.ToString("dd/MM/yyyy");

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
        public static Collection<Entity.Billing.SalesEntryTransDetails> GetProductDetailsEstimate(int iID = 0, int iValue = 0, int iSupplierID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesEntryTransDetails> objList = new Collection<Entity.Billing.SalesEntryTransDetails>();
            Entity.Billing.SalesEntryTransDetails objSalesEntryTrans = new Entity.Billing.SalesEntryTransDetails();
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
                        objSalesEntryTrans = new Entity.Billing.SalesEntryTransDetails();
                        objProduct = new Entity.Master.Product();

                        objSalesEntryTrans.SalesEntryTransID = Convert.ToInt32(drData["PK_SalesEntryTransID"]);
                        objSalesEntryTrans.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objSalesEntryTrans.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objSalesEntryTrans.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntryTrans.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSalesEntryTrans.sInvoiceDate = objSalesEntryTrans.InvoiceDate.ToString("dd/MM/yyyy");

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

        public static Collection<Entity.Billing.SalesEntryTransDetails> GetProductDetailsEstimateBySMSCoode(int iID = 0, int iValue = 0, int iSupplierID = 0, string iSMSCode =null)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesEntryTransDetails> objList = new Collection<Entity.Billing.SalesEntryTransDetails>();
            Entity.Billing.SalesEntryTransDetails objSalesEntryTrans = new Entity.Billing.SalesEntryTransDetails();
            Entity.Master.Product objProduct;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ESTIMATESALESENTRYTRANSDETAILSBYSMSCODE);
                db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
                db.AddInParameter(cmd, "@Type", DbType.Int32, iValue);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@ProductCode", DbType.String, iSMSCode);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntryTrans = new Entity.Billing.SalesEntryTransDetails();
                        objProduct = new Entity.Master.Product();

                        objSalesEntryTrans.SalesEntryTransID = Convert.ToInt32(drData["PK_SalesEntryTransID"]);
                        objSalesEntryTrans.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objSalesEntryTrans.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objSalesEntryTrans.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntryTrans.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSalesEntryTrans.sInvoiceDate = objSalesEntryTrans.InvoiceDate.ToString("dd/MM/yyyy");

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
        public static Collection<Entity.Billing.SalesEntryTransDetails> GetNewProductDetails(int iID = 0, string code = "", int iValue = 0, int iSupplierID = 0, int iSalesENtryID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesEntryTransDetails> objList = new Collection<Entity.Billing.SalesEntryTransDetails>();
            Entity.Billing.SalesEntryTransDetails objSalesEntryTrans = new Entity.Billing.SalesEntryTransDetails();
            Entity.Master.Product objProduct;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_NEWSALESENTRYTRANSDETAILS);
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
                        objSalesEntryTrans = new Entity.Billing.SalesEntryTransDetails();
                        objProduct = new Entity.Master.Product();

                        objSalesEntryTrans.SalesEntryTransID = Convert.ToInt32(drData["PK_SalesEntryTransID"]);
                        objSalesEntryTrans.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objSalesEntryTrans.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objSalesEntryTrans.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesEntryTrans.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSalesEntryTrans.sInvoiceDate = objSalesEntryTrans.InvoiceDate.ToString("dd/MM/yyyy");

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
            }
    public class SalesEntryTrans
    {
        public static Collection<Entity.Billing.SalesEntryTrans> GetSalesEntryTransBySalesEntryID(int iSalesEntryID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesEntryTrans> objList = new Collection<Entity.Billing.SalesEntryTrans>();
            Entity.Billing.SalesEntryTrans objSalesEntryTrans = new Entity.Billing.SalesEntryTrans();
            Entity.Master.Product objProduct; Entity.Billing.Tax objTax;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESENTRYTRANS);
                db.AddInParameter(cmd, "@FK_SalesEntryID", DbType.Int32, iSalesEntryID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesEntryTrans = new Entity.Billing.SalesEntryTrans();
                        objProduct = new Entity.Master.Product();
                        objTax = new Entity.Billing.Tax();

                        objSalesEntryTrans.SalesEntryTransID = Convert.ToInt32(drData["PK_SalesEntryTransID"]);
                        objSalesEntryTrans.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxPercentage = Convert.ToDecimal(drData["TaxPercent"]);
                        objTax.SGSTPercent = Convert.ToDecimal(drData["SGSTPercent"]);
                        objTax.IGSTPercent = Convert.ToDecimal(drData["IGSTPercent"]);
                        objTax.CGSTPercent = Convert.ToDecimal(drData["CGSTPercent"]);
                        objSalesEntryTrans.Tax = objTax;

                        objSalesEntryTrans.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesEntryTrans.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesEntryTrans.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesEntryTrans.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
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
                        objSalesEntryTrans.NewProductFlag = Convert.ToBoolean(drData["NewProductFlag"]);
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
        public static void SaveSalesEntryTransaction(Collection<Entity.Billing.SalesEntryTrans> ObjSalesEntryTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.SalesEntryTrans ObjSalesEntryTransaction in ObjSalesEntryTransList)
            {
                if (ObjSalesEntryTransaction.StatusFlag == "I")
                    iID = AddSalesEntryTrans(ObjSalesEntryTransaction);
                else if (ObjSalesEntryTransaction.StatusFlag == "U")
                    bResult = UpdateSalesEntryTrans(ObjSalesEntryTransaction);
                else if (ObjSalesEntryTransaction.StatusFlag == "D")
                    bResult = DeleteSalesEntryTrans(ObjSalesEntryTransaction.SalesEntryTransID);
            }
        }
        public static int AddSalesEntryTrans(Entity.Billing.SalesEntryTrans objSalesEntryTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_SALESENTRYTRANS);
                db.AddOutParameter(cmd, "@PK_SalesEntryTransID", DbType.Int32, objSalesEntryTrans.SalesEntryTransID);
                db.AddInParameter(cmd, "@FK_SalesEntryID", DbType.Int32, objSalesEntryTrans.SalesEntryID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objSalesEntryTrans.Product.ProductID);
                db.AddInParameter(cmd, "@SMSCode", DbType.String, objSalesEntryTrans.Product.SMSCode);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objSalesEntryTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objSalesEntryTrans.Rate);
                db.AddInParameter(cmd, "@DiscountPercentage", DbType.Decimal, objSalesEntryTrans.DiscountPercentage);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objSalesEntryTrans.DiscountAmount);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objSalesEntryTrans.SubTotal);
                db.AddInParameter(cmd, "@Barcode", DbType.String, objSalesEntryTrans.Barcode);
                db.AddInParameter(cmd, "@ProductName", DbType.String, objSalesEntryTrans.Product.ProductName);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.String, objSalesEntryTrans.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objSalesEntryTrans.Tax.TaxPercentage);
                db.AddInParameter(cmd, "@IGSTPercent", DbType.Decimal, objSalesEntryTrans.Tax.IGSTPercent);
                db.AddInParameter(cmd, "@SGSTPercent", DbType.Decimal, objSalesEntryTrans.Tax.SGSTPercent);
                db.AddInParameter(cmd, "@CGSTPercent", DbType.Decimal, objSalesEntryTrans.Tax.CGSTPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objSalesEntryTrans.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objSalesEntryTrans.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objSalesEntryTrans.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objSalesEntryTrans.TaxAmount);
                db.AddInParameter(cmd, "@NewProductFlag", DbType.Boolean, objSalesEntryTrans.NewProductFlag);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_SalesEntryTransID"));
            }
            catch (Exception ex)
            {
                sException = "SalesEntryTrans AddSalesEntryTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateSalesEntryTrans(Entity.Billing.SalesEntryTrans objSalesEntryTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_SALESENTRYTRANS);
                db.AddInParameter(cmd, "@PK_SalesEntryTransID", DbType.Int32, objSalesEntryTrans.SalesEntryTransID);
                db.AddInParameter(cmd, "@FK_SalesEntryID", DbType.Int32, objSalesEntryTrans.SalesEntryID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objSalesEntryTrans.Product.ProductID);
                db.AddInParameter(cmd, "@SMSCode", DbType.String, objSalesEntryTrans.Product.SMSCode);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objSalesEntryTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objSalesEntryTrans.Rate);
                db.AddInParameter(cmd, "@DiscountPercentage", DbType.Decimal, objSalesEntryTrans.DiscountPercentage);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objSalesEntryTrans.DiscountAmount);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objSalesEntryTrans.SubTotal);
                db.AddInParameter(cmd, "@Barcode", DbType.String, objSalesEntryTrans.Barcode);
                db.AddInParameter(cmd, "@ProductName", DbType.String, objSalesEntryTrans.Product.ProductName);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.String, objSalesEntryTrans.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objSalesEntryTrans.Tax.TaxPercentage);
                db.AddInParameter(cmd, "@IGSTPercent", DbType.Decimal, objSalesEntryTrans.Tax.IGSTPercent);
                db.AddInParameter(cmd, "@SGSTPercent", DbType.Decimal, objSalesEntryTrans.Tax.SGSTPercent);
                db.AddInParameter(cmd, "@CGSTPercent", DbType.Decimal, objSalesEntryTrans.Tax.CGSTPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objSalesEntryTrans.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objSalesEntryTrans.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objSalesEntryTrans.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objSalesEntryTrans.TaxAmount);
                db.AddInParameter(cmd, "@NewProductFlag", DbType.Boolean, objSalesEntryTrans.NewProductFlag);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "SalesEntryTrans UpdateSalesEntryTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteSalesEntryTrans(int iSalesEntryTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_SALESENTRYTRANS);
                db.AddInParameter(cmd, "@PK_SalesEntryTransID", DbType.Int32, iSalesEntryTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "SalesEntryTrans DeleteSalesEntryTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
    public class SalesExchangeTrans
    {
        public static Collection<Entity.Billing.SalesExchangeTrans> GetSalesExchangeTransBySalesEntryID(int iSalesEntryID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesExchangeTrans> objList = new Collection<Entity.Billing.SalesExchangeTrans>();
            Entity.Billing.SalesExchangeTrans objSalesExchangeTrans = new Entity.Billing.SalesExchangeTrans();
            Entity.Master.Product objProduct; Entity.Billing.Tax objTax;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESEXCHANGETRANS);
                db.AddInParameter(cmd, "@FK_SalesEntryID", DbType.Int32, iSalesEntryID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesExchangeTrans = new Entity.Billing.SalesExchangeTrans();
                        objProduct = new Entity.Master.Product();
                        objTax = new Entity.Billing.Tax();

                        objSalesExchangeTrans.ExchangeTransID = Convert.ToInt32(drData["PK_ExchangeTransID"]);
                        objSalesExchangeTrans.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objSalesExchangeTrans.ExchangeSalesEntryID = Convert.ToInt32(drData["FK_ExchangeSalesEntryID"]);
                        objSalesExchangeTrans.SalesInvoiceNo = Convert.ToString(drData["InvoiceNo"]);

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxPercentage = Convert.ToDecimal(drData["TaxPercent"]);
                        objTax.SGSTPercent = Convert.ToDecimal(drData["SGSTPercent"]);
                        objTax.IGSTPercent = Convert.ToDecimal(drData["IGSTPercent"]);
                        objTax.CGSTPercent = Convert.ToDecimal(drData["CGSTPercent"]);
                        objSalesExchangeTrans.Tax = objTax;

                        objSalesExchangeTrans.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesExchangeTrans.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesExchangeTrans.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesExchangeTrans.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesExchangeTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objSalesExchangeTrans.Rate = Convert.ToInt32(drData["Rate"]);
                        objSalesExchangeTrans.SubTotal = Convert.ToDecimal(drData["SubTotal"]);
                        objSalesExchangeTrans.DiscountPercentage = Convert.ToInt32(drData["DiscountPercentage"]);
                        objSalesExchangeTrans.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objSalesExchangeTrans.Product = objProduct;
                        objSalesExchangeTrans.Barcode = Convert.ToString(drData["Barcode"]);
                        objList.Add(objSalesExchangeTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesExchangeTrans GetSalesExchangeTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveSalesExchangeTransaction(Collection<Entity.Billing.SalesExchangeTrans> ObjSalesExchangeTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.SalesExchangeTrans ObjSalesExchangeTransaction in ObjSalesExchangeTransList)
            {
                if (ObjSalesExchangeTransaction.StatusFlag == "I")
                    iID = AddSalesExchangeTrans(ObjSalesExchangeTransaction);
                else if (ObjSalesExchangeTransaction.StatusFlag == "U")
                    bResult = UpdateSalesExchangeTrans(ObjSalesExchangeTransaction);
                else if (ObjSalesExchangeTransaction.StatusFlag == "D")
                    bResult = DeleteSalesExchangeTrans(ObjSalesExchangeTransaction.ExchangeTransID);
            }
        }
        public static int AddSalesExchangeTrans(Entity.Billing.SalesExchangeTrans objSalesExchangeTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_SALESEXCHANGETRANS);
                db.AddOutParameter(cmd, "@PK_ExchangeTransID", DbType.Int32, objSalesExchangeTrans.ExchangeTransID);
                db.AddInParameter(cmd, "@FK_SalesEntryID", DbType.Int32, objSalesExchangeTrans.SalesEntryID);
                db.AddInParameter(cmd, "@FK_ExchangeSalesEntryID", DbType.Int32, objSalesExchangeTrans.ExchangeSalesEntryID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objSalesExchangeTrans.Product.ProductID);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objSalesExchangeTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objSalesExchangeTrans.Rate);
                db.AddInParameter(cmd, "@DiscountPercentage", DbType.Decimal, objSalesExchangeTrans.DiscountPercentage);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objSalesExchangeTrans.DiscountAmount);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objSalesExchangeTrans.SubTotal);
                db.AddInParameter(cmd, "@Barcode", DbType.String, objSalesExchangeTrans.Barcode);
                db.AddInParameter(cmd, "@ProductName", DbType.String, objSalesExchangeTrans.Product.ProductName);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.String, objSalesExchangeTrans.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objSalesExchangeTrans.Tax.TaxPercentage);
                db.AddInParameter(cmd, "@IGSTPercent", DbType.Decimal, objSalesExchangeTrans.Tax.IGSTPercent);
                db.AddInParameter(cmd, "@SGSTPercent", DbType.Decimal, objSalesExchangeTrans.Tax.SGSTPercent);
                db.AddInParameter(cmd, "@CGSTPercent", DbType.Decimal, objSalesExchangeTrans.Tax.CGSTPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objSalesExchangeTrans.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objSalesExchangeTrans.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objSalesExchangeTrans.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objSalesExchangeTrans.TaxAmount);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_ExchangeTransID"));
            }
            catch (Exception ex)
            {
                sException = "SalesExchangeTrans AddSalesExchangeTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateSalesExchangeTrans(Entity.Billing.SalesExchangeTrans objSalesExchangeTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_SALESEXCHANGETRANS);
                db.AddInParameter(cmd, "@PK_ExchangeTransID", DbType.Int32, objSalesExchangeTrans.ExchangeTransID);
                db.AddInParameter(cmd, "@FK_SalesEntryID", DbType.Int32, objSalesExchangeTrans.SalesEntryID);
                db.AddInParameter(cmd, "@FK_ExchangeSalesEntryID", DbType.Int32, objSalesExchangeTrans.ExchangeSalesEntryID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objSalesExchangeTrans.Product.ProductID);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objSalesExchangeTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objSalesExchangeTrans.Rate);
                db.AddInParameter(cmd, "@DiscountPercentage", DbType.Decimal, objSalesExchangeTrans.DiscountPercentage);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objSalesExchangeTrans.DiscountAmount);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objSalesExchangeTrans.SubTotal);
                db.AddInParameter(cmd, "@Barcode", DbType.String, objSalesExchangeTrans.Barcode);
                db.AddInParameter(cmd, "@ProductName", DbType.String, objSalesExchangeTrans.Product.ProductName);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.String, objSalesExchangeTrans.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objSalesExchangeTrans.Tax.TaxPercentage);
                db.AddInParameter(cmd, "@IGSTPercent", DbType.Decimal, objSalesExchangeTrans.Tax.IGSTPercent);
                db.AddInParameter(cmd, "@SGSTPercent", DbType.Decimal, objSalesExchangeTrans.Tax.SGSTPercent);
                db.AddInParameter(cmd, "@CGSTPercent", DbType.Decimal, objSalesExchangeTrans.Tax.CGSTPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objSalesExchangeTrans.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objSalesExchangeTrans.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objSalesExchangeTrans.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objSalesExchangeTrans.TaxAmount);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "SalesExchangeTrans UpdateSalesExchangeTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteSalesExchangeTrans(int iExchangeTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_SALESEXCHANGETRANS);
                db.AddInParameter(cmd, "@PK_ExchangeTransID", DbType.Int32, iExchangeTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "SalesExchangeTrans DeleteSalesExchangeTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }

    public class RetailPaymentMode
    {
        public static Collection<Entity.Billing.RetailPaymentMode> GetRetailPaymentModeByID(int iSalesEntryID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.RetailPaymentMode> objList = new Collection<Entity.Billing.RetailPaymentMode>();
            Entity.Billing.RetailPaymentMode objRetailPaymentMode = new Entity.Billing.RetailPaymentMode();
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_RETAILPAYMENTMODE);
                db.AddInParameter(cmd, "@FK_SalesEntryID", DbType.Int32, iSalesEntryID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRetailPaymentMode = new Entity.Billing.RetailPaymentMode();

                        objRetailPaymentMode.RetailPaymentModeID = Convert.ToInt32(drData["PK_RetailPaymentModeID"]);
                        objRetailPaymentMode.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objRetailPaymentMode.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                        objRetailPaymentMode.sIssueDate = objRetailPaymentMode.IssueDate.ToString("dd/MM/yyyy");
                        objRetailPaymentMode.CollectionDate = Convert.ToDateTime(drData["CollectionDate"]);
                        objRetailPaymentMode.sCollectionDate = objRetailPaymentMode.CollectionDate.ToString("dd/MM/yyyy");
                        objRetailPaymentMode.Charges = Convert.ToDecimal(drData["Charges"]);
                        objRetailPaymentMode.Amount = Convert.ToDecimal(drData["Amount"]);
                        objRetailPaymentMode.BankName = Convert.ToString(drData["BankName"]);
                        objRetailPaymentMode.Status = Convert.ToString(drData["Status"]);
                        objRetailPaymentMode.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objRetailPaymentMode.IssuedBy = Convert.ToString(drData["IssuedBy"]);
                        objRetailPaymentMode.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objRetailPaymentMode.BankID = Convert.ToInt32(drData["FK_BankID"]);

                        objList.Add(objRetailPaymentMode);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "RetailPaymentMode GetRetailPaymentMode | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveRetailPaymentModeaction(Collection<Entity.Billing.RetailPaymentMode> ObjRetailPaymentModeList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.RetailPaymentMode ObjRetailPaymentModeaction in ObjRetailPaymentModeList)
            {
                if (ObjRetailPaymentModeaction.StatusFlag == "I")
                    iID = AddRetailPaymentMode(ObjRetailPaymentModeaction);
                else if (ObjRetailPaymentModeaction.StatusFlag == "U")
                    bResult = UpdateRetailPaymentMode(ObjRetailPaymentModeaction);
                else if (ObjRetailPaymentModeaction.StatusFlag == "D")
                    bResult = DeleteRetailPaymentMode(ObjRetailPaymentModeaction.RetailPaymentModeID);
            }
        }
        public static int AddRetailPaymentMode(Entity.Billing.RetailPaymentMode objRetailPaymentMode)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_RETAILPAYMENTMODE);
                db.AddOutParameter(cmd, "@PK_RetailPaymentModeID", DbType.Int32, objRetailPaymentMode.RetailPaymentModeID);
                db.AddInParameter(cmd, "@FK_SalesEntryID", DbType.Int32, objRetailPaymentMode.SalesEntryID);
                db.AddInParameter(cmd, "@PaymentMode", DbType.String, objRetailPaymentMode.PaymentMode);
                db.AddInParameter(cmd, "@Amount", DbType.Decimal, objRetailPaymentMode.Amount);
                db.AddInParameter(cmd, "@ChequeNo", DbType.String, objRetailPaymentMode.ChequeNo);
                db.AddInParameter(cmd, "@IssueDate", DbType.String, objRetailPaymentMode.sIssueDate);
                db.AddInParameter(cmd, "@CollectionDate", DbType.String, objRetailPaymentMode.sCollectionDate);
                db.AddInParameter(cmd, "@IssuedBy", DbType.String, objRetailPaymentMode.IssuedBy);
                db.AddInParameter(cmd, "@Charges", DbType.Decimal, objRetailPaymentMode.Charges);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objRetailPaymentMode.BankID);
                db.AddInParameter(cmd, "@Status", DbType.String, objRetailPaymentMode.Status);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_RetailPaymentModeID"));
            }
            catch (Exception ex)
            {
                sException = "RetailPaymentMode AddRetailPaymentMode | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateRetailPaymentMode(Entity.Billing.RetailPaymentMode objRetailPaymentMode)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_RETAILPAYMENTMODE);
                db.AddInParameter(cmd, "@PK_RetailPaymentModeID", DbType.Int32, objRetailPaymentMode.RetailPaymentModeID);
                db.AddInParameter(cmd, "@FK_SalesEntryID", DbType.Int32, objRetailPaymentMode.SalesEntryID);
                db.AddInParameter(cmd, "@PaymentMode", DbType.String, objRetailPaymentMode.PaymentMode);
                db.AddInParameter(cmd, "@Amount", DbType.Decimal, objRetailPaymentMode.Amount);
                db.AddInParameter(cmd, "@ChequeNo", DbType.String, objRetailPaymentMode.ChequeNo);
                db.AddInParameter(cmd, "@IssueDate", DbType.String, objRetailPaymentMode.sIssueDate);
                db.AddInParameter(cmd, "@CollectionDate", DbType.String, objRetailPaymentMode.sCollectionDate);
                db.AddInParameter(cmd, "@IssuedBy", DbType.String, objRetailPaymentMode.IssuedBy);
                db.AddInParameter(cmd, "@Charges", DbType.Decimal, objRetailPaymentMode.Charges);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objRetailPaymentMode.BankID);
                db.AddInParameter(cmd, "@Status", DbType.String, objRetailPaymentMode.Status);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "RetailPaymentMode UpdateRetailPaymentMode| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteRetailPaymentMode(int iRetailPaymentModeID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_RETAILPAYMENTMODE);
                db.AddInParameter(cmd, "@PK_RetailPaymentModeID", DbType.Int32, iRetailPaymentModeID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "RetailPaymentMode DeleteRetailPaymentMode | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
