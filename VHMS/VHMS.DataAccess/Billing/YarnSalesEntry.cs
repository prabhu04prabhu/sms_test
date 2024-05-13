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
    public class YarnSalesEntry
    {
        public static Collection<Entity.Billing.YarnSalesEntry> GetYarnSalesEntry(int ipatientID = 0, int IsRetail = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.YarnSalesEntry> objList = new Collection<Entity.Billing.YarnSalesEntry>();
            Entity.Billing.YarnSalesEntry objYarnSalesEntry;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_YARNSALESENTRY);
                db.AddInParameter(cmd, "@PK_YarnSalesEntryID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objYarnSalesEntry = new Entity.Billing.YarnSalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objYarnSalesEntry.YarnSalesEntryID = Convert.ToInt32(drData["PK_YarnSalesEntryID"]);
                        objYarnSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objYarnSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objYarnSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objYarnSalesEntry.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objYarnSalesEntry.Agent = objAgent;

                        objYarnSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objYarnSalesEntry.sLRDate = objYarnSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objYarnSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objYarnSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objYarnSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objYarnSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objYarnSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objYarnSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objYarnSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objYarnSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objYarnSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objYarnSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objYarnSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objYarnSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objYarnSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objYarnSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objYarnSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objYarnSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objYarnSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objYarnSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objYarnSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objYarnSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objYarnSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objYarnSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objYarnSalesEntry.Notes = Convert.ToString(drData["Notes"]);
                        objYarnSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objYarnSalesEntry.sDeliveryDate = objYarnSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objYarnSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objYarnSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objYarnSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objYarnSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objYarnSalesEntry.SalesOrder = objSalesOrder;

                        objYarnSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objYarnSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objYarnSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objYarnSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objYarnSalesEntry.Transport = objTransport;

                        objYarnSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objYarnSalesEntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objYarnSalesEntry.sInvoiceDate = objYarnSalesEntry.InvoiceDate.ToString("dd/MM/yyyy") + " " + objYarnSalesEntry.CreatedOn.ToString("h:mm");
                        objYarnSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);

                        objList.Add(objYarnSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "YarnSalesEntry GetYarnSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.YarnSalesEntry> GetTopYarnSalesEntry(int ipatientID = 0, int IsRetail = 0, int iCustomerID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.YarnSalesEntry> objList = new Collection<Entity.Billing.YarnSalesEntry>();
            Entity.Billing.YarnSalesEntry objYarnSalesEntry;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPYARNSALESENTRY);
                db.AddInParameter(cmd, "@PK_YarnSalesEntryID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iCustomerID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objYarnSalesEntry = new Entity.Billing.YarnSalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objYarnSalesEntry.YarnSalesEntryID = Convert.ToInt32(drData["PK_YarnSalesEntryID"]);
                        objYarnSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objYarnSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Area = Convert.ToString(drData["area"]);
                        objCustomer.MinDueDays = Convert.ToInt32(drData["MinDueDays"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objYarnSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objYarnSalesEntry.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objYarnSalesEntry.Agent = objAgent;

                        objYarnSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objYarnSalesEntry.DueDays = Convert.ToInt32(drData["DueDays"]);
                        objYarnSalesEntry.sLRDate = objYarnSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objYarnSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objYarnSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objYarnSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objYarnSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objYarnSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objYarnSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objYarnSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objYarnSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objYarnSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objYarnSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objYarnSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objYarnSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objYarnSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objYarnSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objYarnSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objYarnSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objYarnSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objYarnSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objYarnSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objYarnSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objYarnSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objYarnSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objYarnSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objYarnSalesEntry.TotalQty = Convert.ToDecimal(drData["TotalQty"]);

                        objYarnSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objYarnSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objYarnSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objYarnSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objYarnSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objYarnSalesEntry.SalesOrder = objSalesOrder;

                        objYarnSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objYarnSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objYarnSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objYarnSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objYarnSalesEntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objYarnSalesEntry.sCreatedOn = objYarnSalesEntry.CreatedOn.ToString("dd/MM/yyyy h:mm");
                        objYarnSalesEntry.sInvoiceDate = objYarnSalesEntry.InvoiceDate.ToString("dd/MM/yyyy");
                        objYarnSalesEntry.sWholeSalesInvoiceDate = objYarnSalesEntry.InvoiceDate.ToString("dd/MM/yyyy") + " " + objYarnSalesEntry.CreatedOn.ToString("h:mm");
                        objYarnSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objYarnSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objYarnSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objYarnSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objYarnSalesEntry.Transport = objTransport;

                        objList.Add(objYarnSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "YarnSalesEntry GetYarnSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.YarnSalesEntry> SearchYarnSalesEntry(string ID, int IsRetail = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.YarnSalesEntry> objList = new Collection<Entity.Billing.YarnSalesEntry>();
            Entity.Billing.YarnSalesEntry objYarnSalesEntry; Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_YARNSALESENTRYDYNAMIC);
                db.AddInParameter(cmd, "@PK_YarnSalesEntryID", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objYarnSalesEntry = new Entity.Billing.YarnSalesEntry();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objYarnSalesEntry.YarnSalesEntryID = Convert.ToInt32(drData["PK_YarnSalesEntryID"]);
                        objYarnSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objYarnSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MinDueDays = Convert.ToInt32(drData["MinDueDays"]);
                        objCustomer.Area = Convert.ToString(drData["Area"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objYarnSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objYarnSalesEntry.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objYarnSalesEntry.Agent = objAgent;

                        objYarnSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objYarnSalesEntry.DueDays = Convert.ToInt32(drData["DueDays"]);
                        objYarnSalesEntry.sLRDate = objYarnSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objYarnSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objYarnSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objYarnSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objYarnSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objYarnSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objYarnSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objYarnSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objYarnSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objYarnSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objYarnSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objYarnSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objYarnSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objYarnSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objYarnSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objYarnSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objYarnSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objYarnSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objYarnSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objYarnSalesEntry.Status = Convert.ToString(drData["Status"]);
                        objYarnSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objYarnSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objYarnSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objYarnSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objYarnSalesEntry.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objYarnSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objYarnSalesEntry.sDeliveryDate = objYarnSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objYarnSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objYarnSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                     


                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objYarnSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objYarnSalesEntry.Transport = objTransport;
                        objYarnSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objYarnSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objYarnSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objYarnSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objYarnSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);
                        objYarnSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objYarnSalesEntry.sDeliveryDate = objYarnSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objYarnSalesEntry.SalesOrder = objSalesOrder;

                        objYarnSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objYarnSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objYarnSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objYarnSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objYarnSalesEntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objYarnSalesEntry.sCreatedOn = objYarnSalesEntry.CreatedOn.ToString("dd/MM/yyyy h:mm");
                        objYarnSalesEntry.sInvoiceDate = objYarnSalesEntry.InvoiceDate.ToString("dd/MM/yyyy");
                        objYarnSalesEntry.sWholeSalesInvoiceDate = objYarnSalesEntry.InvoiceDate.ToString("dd/MM/yyyy") + " " + objYarnSalesEntry.CreatedOn.ToString("h:mm");

                        objYarnSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);

                        objList.Add(objYarnSalesEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "YarnSalesEntry GetYarnSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
      public static Entity.Billing.YarnSalesEntry GetYarnSalesEntryByID(int iYarnSalesEntryID, int IsRetail, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.YarnSalesEntry objYarnSalesEntry = new Entity.Billing.YarnSalesEntry();
            Entity.Customer objCustomer; Entity.Billing.Tax objTax; Entity.Gift objGift; Entity.State objState;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder; Entity.CustomerTypes objCustomerTypes;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_YARNSALESENTRY);
                db.AddInParameter(cmd, "@PK_YarnSalesEntryID", DbType.Int32, iYarnSalesEntryID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objYarnSalesEntry = new Entity.Billing.YarnSalesEntry();
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

                        objYarnSalesEntry.YarnSalesEntryID = Convert.ToInt32(drData["PK_YarnSalesEntryID"]);
                        objYarnSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objYarnSalesEntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objYarnSalesEntry.sInvoiceDate = objYarnSalesEntry.InvoiceDate.ToString("dd/MM/yyyy");
                        objYarnSalesEntry.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        string Date = objYarnSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objYarnSalesEntry.sLRDate = objYarnSalesEntry.LRDate.ToString("dd/MM/yyyy");
                        objYarnSalesEntry.TransportName = Convert.ToString(drData["TransportName"]);
                        objYarnSalesEntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objYarnSalesEntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objYarnSalesEntry.LRNo = Convert.ToString(drData["LRNo"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objYarnSalesEntry.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objYarnSalesEntry.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objYarnSalesEntry.Agent = objAgent;

                        objCustomerTypes.CustomertypeID = Convert.ToInt32(drData["FK_CustomertypeID"]);
                        objYarnSalesEntry.CustomerType = objCustomerTypes;

                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objYarnSalesEntry.State = objState;

                        objGift.GiftID = Convert.ToInt32(drData["FK_GiftID"]);
                        objYarnSalesEntry.Gift = objGift;
                        objYarnSalesEntry.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objYarnSalesEntry.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objYarnSalesEntry.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objYarnSalesEntry.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objYarnSalesEntry.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objYarnSalesEntry.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objYarnSalesEntry.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objYarnSalesEntry.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objYarnSalesEntry.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objYarnSalesEntry.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objYarnSalesEntry.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objYarnSalesEntry.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objYarnSalesEntry.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objYarnSalesEntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objYarnSalesEntry.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objYarnSalesEntry.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objYarnSalesEntry.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objYarnSalesEntry.Status = Convert.ToString(drData["Status"]);

                        objYarnSalesEntry.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objYarnSalesEntry.Narration = Convert.ToString(drData["Narration"]);
                        objYarnSalesEntry.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objYarnSalesEntry.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objYarnSalesEntry.Notes = Convert.ToString(drData["Notes"]);
                        objYarnSalesEntry.CardCharges = Convert.ToDecimal(drData["CardCharges"]);
                        objYarnSalesEntry.CardNo = Convert.ToString(drData["CardNo"]);
                        objYarnSalesEntry.TenderAmount = Convert.ToDecimal(drData["TenderAmount"]);
                        objYarnSalesEntry.BalanceGiven = Convert.ToDecimal(drData["BalanceGiven"]);
                        objYarnSalesEntry.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objYarnSalesEntry.sDeliveryDate = objYarnSalesEntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objYarnSalesEntry.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objYarnSalesEntry.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objYarnSalesEntry.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objYarnSalesEntry.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objYarnSalesEntry.SalesOrder = objSalesOrder;

                        objYarnSalesEntry.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objYarnSalesEntry.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objYarnSalesEntry.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objYarnSalesEntry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objYarnSalesEntry.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objYarnSalesEntry.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objYarnSalesEntry.Transport = objTransport;

                        objYarnSalesEntry.YarnSalesEntryTrans = YarnSalesEntryTrans.GetYarnSalesEntryTransByYarnSalesEntryID(objYarnSalesEntry.YarnSalesEntryID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "YarnSalesEntry GetYarnSalesEntryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objYarnSalesEntry;
        }
        public static int AddYarnSalesEntry(Entity.Billing.YarnSalesEntry objYarnSalesEntry)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_YARNSALESENTRY);
                db.AddOutParameter(cmd, "@PK_YarnSalesEntryID", DbType.Int32, objYarnSalesEntry.YarnSalesEntryID);
                db.AddInParameter(cmd, "@InvoiceNo", DbType.String, objYarnSalesEntry.InvoiceNo);
                db.AddInParameter(cmd, "@InvoiceDate", DbType.String, objYarnSalesEntry.sInvoiceDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objYarnSalesEntry.Customer.CustomerID);
                db.AddInParameter(cmd, "@CustomerName", DbType.String, objYarnSalesEntry.Customer.CustomerName);
                db.AddInParameter(cmd, "@MobileNo", DbType.String, objYarnSalesEntry.Customer.MobileNo);
                db.AddInParameter(cmd, "@Area", DbType.String, objYarnSalesEntry.Customer.Area);
                db.AddInParameter(cmd, "@Address", DbType.String, objYarnSalesEntry.Customer.Address);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objYarnSalesEntry.Tax.TaxID);
                db.AddInParameter(cmd, "@FK_AgentID", DbType.Int32, objYarnSalesEntry.Agent.AgentID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objYarnSalesEntry.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objYarnSalesEntry.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objYarnSalesEntry.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objYarnSalesEntry.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objYarnSalesEntry.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objYarnSalesEntry.TotalAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objYarnSalesEntry.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objYarnSalesEntry.DiscountAmount);
                db.AddInParameter(cmd, "@AdditionalDiscount", DbType.Decimal, objYarnSalesEntry.AdditionalDiscount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objYarnSalesEntry.Roundoff);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objYarnSalesEntry.NetAmount);
                db.AddInParameter(cmd, "@PaidAmount", DbType.Decimal, objYarnSalesEntry.PaidAmount);
                db.AddInParameter(cmd, "@BalanceAmount", DbType.Decimal, objYarnSalesEntry.BalanceAmount);
                db.AddInParameter(cmd, "@FK_SalesOrderID", DbType.Int32, objYarnSalesEntry.SalesOrder.SalesOrderID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objYarnSalesEntry.FinancialYear.FinancialYearID);
                db.AddInParameter(cmd, "@CancelReason", DbType.String, objYarnSalesEntry.CancelReason);
                db.AddInParameter(cmd, "@IsActive", DbType.Boolean, objYarnSalesEntry.IsActive);
                db.AddInParameter(cmd, "@PaymentMode", DbType.String, objYarnSalesEntry.PaymentMode);
                db.AddInParameter(cmd, "@Narration", DbType.String, objYarnSalesEntry.Narration);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objYarnSalesEntry.Bank.LedgerID);
                db.AddInParameter(cmd, "@Status", DbType.String, objYarnSalesEntry.Status);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, objYarnSalesEntry.IsRetailBill);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objYarnSalesEntry.CreatedBy.UserID);
                db.AddInParameter(cmd, "@TransportName", DbType.String, objYarnSalesEntry.TransportName);
                db.AddInParameter(cmd, "@LRNo", DbType.String, objYarnSalesEntry.LRNo);
                db.AddInParameter(cmd, "@EWayNo", DbType.String, objYarnSalesEntry.EWayNo);
                db.AddInParameter(cmd, "@VehicleNo", DbType.String, objYarnSalesEntry.VehicleNo);
                db.AddInParameter(cmd, "@CardNo", DbType.String, objYarnSalesEntry.CardNo);
                db.AddInParameter(cmd, "@BalanceGiven", DbType.Decimal, objYarnSalesEntry.BalanceGiven);
                db.AddInParameter(cmd, "@TenderAmount", DbType.Decimal, objYarnSalesEntry.TenderAmount);
                db.AddInParameter(cmd, "@CardCharges", DbType.Decimal, objYarnSalesEntry.CardCharges);
                db.AddInParameter(cmd, "@SalesPoints", DbType.Decimal, objYarnSalesEntry.SalesPoints);
                db.AddInParameter(cmd, "@UsedPoints", DbType.Decimal, objYarnSalesEntry.UsedPoints);
                db.AddInParameter(cmd, "@ExchangeAmount", DbType.Decimal, objYarnSalesEntry.ExchangeAmount);
                db.AddInParameter(cmd, "@LRDate", DbType.String, objYarnSalesEntry.sLRDate);
                db.AddInParameter(cmd, "@FK_GiftID", DbType.Int32, objYarnSalesEntry.Gift.GiftID);
                db.AddInParameter(cmd, "@CustomerType", DbType.String, objYarnSalesEntry.CustomerType.CustomerTypeName);
                db.AddInParameter(cmd, "@FK_CustomerTypeID", DbType.Int32, objYarnSalesEntry.CustomerType.CustomertypeID);
                db.AddInParameter(cmd, "@FK_StateID", DbType.Int32, objYarnSalesEntry.State.StateID);
                db.AddInParameter(cmd, "@TCSPercent", DbType.Decimal, objYarnSalesEntry.TCSPercent);
                db.AddInParameter(cmd, "@TCSAmount", DbType.Decimal, objYarnSalesEntry.TCSAmount);
                db.AddInParameter(cmd, "@FK_ShippingAddressID", DbType.Int32, objYarnSalesEntry.ShippingAddress.ShippingAddressID);
                db.AddInParameter(cmd, "@FK_TransportID", DbType.Int32, objYarnSalesEntry.Transport.TransportID);
                db.AddInParameter(cmd, "@NoofBags", DbType.String, objYarnSalesEntry.NoofBags);
                db.AddInParameter(cmd, "@Notes", DbType.String, objYarnSalesEntry.Notes);
                db.AddInParameter(cmd, "@ImagePath1", DbType.String, objYarnSalesEntry.ImagePath1);
                db.AddInParameter(cmd, "@ImagePath2", DbType.String, objYarnSalesEntry.ImagePath2);
                db.AddInParameter(cmd, "@ImagePath3", DbType.String, objYarnSalesEntry.ImagePath3);
                db.AddInParameter(cmd, "@BillStatus", DbType.String, objYarnSalesEntry.BillStatus);
                db.AddInParameter(cmd, "@DeliveryDate", DbType.String, objYarnSalesEntry.sDeliveryDate);
                db.AddInParameter(cmd, "@TransportCharges", DbType.Decimal, objYarnSalesEntry.TransportCharges);
                db.AddInParameter(cmd, "@OtherCharges", DbType.Decimal, objYarnSalesEntry.OtherCharges);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_YarnSalesEntryID"));

                foreach (Entity.Billing.YarnSalesEntryTrans ObjYarnSalesEntryTrans in objYarnSalesEntry.YarnSalesEntryTrans)
                    ObjYarnSalesEntryTrans.YarnSalesEntryID = iID;

                YarnSalesEntryTrans.SaveYarnSalesEntryTransaction(objYarnSalesEntry.YarnSalesEntryTrans);
            }
            catch (Exception ex)
            {
                sException = "YarnSalesEntry AddYarnSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateYarnSalesEntry(Entity.Billing.YarnSalesEntry objYarnSalesEntry)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_YARNSALESENTRY);
                db.AddInParameter(cmd, "@PK_YarnSalesEntryID", DbType.Int32, objYarnSalesEntry.YarnSalesEntryID);
                db.AddInParameter(cmd, "@InvoiceNo", DbType.String, objYarnSalesEntry.InvoiceNo);
                db.AddInParameter(cmd, "@InvoiceDate", DbType.String, objYarnSalesEntry.sInvoiceDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objYarnSalesEntry.Customer.CustomerID);
                db.AddInParameter(cmd, "@CustomerName", DbType.String, objYarnSalesEntry.Customer.CustomerName);
                db.AddInParameter(cmd, "@MobileNo", DbType.String, objYarnSalesEntry.Customer.MobileNo);
                db.AddInParameter(cmd, "@Address", DbType.String, objYarnSalesEntry.Customer.Address);
                db.AddInParameter(cmd, "@Area", DbType.String, objYarnSalesEntry.Customer.Area);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objYarnSalesEntry.Tax.TaxID);
                db.AddInParameter(cmd, "@FK_AgentID", DbType.Int32, objYarnSalesEntry.Agent.AgentID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objYarnSalesEntry.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objYarnSalesEntry.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objYarnSalesEntry.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objYarnSalesEntry.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objYarnSalesEntry.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objYarnSalesEntry.TotalAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objYarnSalesEntry.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objYarnSalesEntry.DiscountAmount);
                db.AddInParameter(cmd, "@AdditionalDiscount", DbType.Decimal, objYarnSalesEntry.AdditionalDiscount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objYarnSalesEntry.Roundoff);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objYarnSalesEntry.NetAmount);
                db.AddInParameter(cmd, "@PaidAmount", DbType.Decimal, objYarnSalesEntry.PaidAmount);
                db.AddInParameter(cmd, "@BalanceAmount", DbType.Decimal, objYarnSalesEntry.BalanceAmount);
                db.AddInParameter(cmd, "@FK_SalesOrderID", DbType.Int32, objYarnSalesEntry.SalesOrder.SalesOrderID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objYarnSalesEntry.FinancialYear.FinancialYearID);
                db.AddInParameter(cmd, "@CancelReason", DbType.String, objYarnSalesEntry.CancelReason);
                db.AddInParameter(cmd, "@IsActive", DbType.Boolean, objYarnSalesEntry.IsActive);
                db.AddInParameter(cmd, "@PaymentMode", DbType.String, objYarnSalesEntry.PaymentMode);
                db.AddInParameter(cmd, "@Narration", DbType.String, objYarnSalesEntry.Narration);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objYarnSalesEntry.Bank.LedgerID);
                db.AddInParameter(cmd, "@Status", DbType.String, objYarnSalesEntry.Status);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objYarnSalesEntry.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@TransportName", DbType.String, objYarnSalesEntry.TransportName);
                db.AddInParameter(cmd, "@LRNo", DbType.String, objYarnSalesEntry.LRNo);
                db.AddInParameter(cmd, "@EWayNo", DbType.String, objYarnSalesEntry.EWayNo);
                db.AddInParameter(cmd, "@VehicleNo", DbType.String, objYarnSalesEntry.VehicleNo);
                db.AddInParameter(cmd, "@CardNo", DbType.String, objYarnSalesEntry.CardNo);
                db.AddInParameter(cmd, "@BalanceGiven", DbType.Decimal, objYarnSalesEntry.BalanceGiven);
                db.AddInParameter(cmd, "@TenderAmount", DbType.Decimal, objYarnSalesEntry.TenderAmount);
                db.AddInParameter(cmd, "@CardCharges", DbType.Decimal, objYarnSalesEntry.CardCharges);
                db.AddInParameter(cmd, "@SalesPoints", DbType.Decimal, objYarnSalesEntry.SalesPoints);
                db.AddInParameter(cmd, "@UsedPoints", DbType.Decimal, objYarnSalesEntry.UsedPoints);
                db.AddInParameter(cmd, "@ExchangeAmount", DbType.Decimal, objYarnSalesEntry.ExchangeAmount);
                db.AddInParameter(cmd, "@LRDate", DbType.String, objYarnSalesEntry.sLRDate);
                db.AddInParameter(cmd, "@FK_GiftID", DbType.Int32, objYarnSalesEntry.Gift.GiftID);
                db.AddInParameter(cmd, "@CustomerType", DbType.String, objYarnSalesEntry.CustomerType.CustomerTypeName);
                db.AddInParameter(cmd, "@FK_CustomerTypeID", DbType.Int32, objYarnSalesEntry.CustomerType.CustomertypeID);
                db.AddInParameter(cmd, "@FK_StateID", DbType.Int32, objYarnSalesEntry.State.StateID);
                db.AddInParameter(cmd, "@TCSPercent", DbType.Decimal, objYarnSalesEntry.TCSPercent);
                db.AddInParameter(cmd, "@TCSAmount", DbType.Decimal, objYarnSalesEntry.TCSAmount);
                db.AddInParameter(cmd, "@FK_ShippingAddressID", DbType.Int32, objYarnSalesEntry.ShippingAddress.ShippingAddressID);
                db.AddInParameter(cmd, "@FK_TransportID", DbType.Int32, objYarnSalesEntry.Transport.TransportID);
                db.AddInParameter(cmd, "@NoofBags", DbType.String, objYarnSalesEntry.NoofBags);
                db.AddInParameter(cmd, "@Notes", DbType.String, objYarnSalesEntry.Notes);
                db.AddInParameter(cmd, "@ImagePath1", DbType.String, objYarnSalesEntry.ImagePath1);
                db.AddInParameter(cmd, "@ImagePath2", DbType.String, objYarnSalesEntry.ImagePath2);
                db.AddInParameter(cmd, "@ImagePath3", DbType.String, objYarnSalesEntry.ImagePath3);
                db.AddInParameter(cmd, "@BillStatus", DbType.String, objYarnSalesEntry.BillStatus);
                db.AddInParameter(cmd, "@DeliveryDate", DbType.String, objYarnSalesEntry.sDeliveryDate);
                db.AddInParameter(cmd, "@TransportCharges", DbType.Decimal, objYarnSalesEntry.TransportCharges);
                db.AddInParameter(cmd, "@OtherCharges", DbType.Decimal, objYarnSalesEntry.OtherCharges);

                foreach (Entity.Billing.YarnSalesEntryTrans ObjYarnSalesEntryTrans in objYarnSalesEntry.YarnSalesEntryTrans)
                    ObjYarnSalesEntryTrans.YarnSalesEntryID = objYarnSalesEntry.YarnSalesEntryID;

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                YarnSalesEntryTrans.SaveYarnSalesEntryTransaction(objYarnSalesEntry.YarnSalesEntryTrans);
            }
            catch (Exception ex)
            {
                sException = "YarnSalesEntry UpdateYarnSalesEntry| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteYarnSalesEntry(int iYarnSalesEntryId, string Reason, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_YARNSALESENTRY);
                db.AddInParameter(cmd, "@PK_YarnSalesEntryID", DbType.Int32, iYarnSalesEntryId);
                db.AddInParameter(cmd, "@Reason", DbType.String, Reason);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, iUserId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "YarnSalesEntry DeleteYarnSalesEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        
    }


    public class YarnSalesEntryTrans
    {
        public static Collection<Entity.Billing.YarnSalesEntryTrans> GetYarnSalesEntryTransByYarnSalesEntryID(int iYarnSalesEntryID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.YarnSalesEntryTrans> objList = new Collection<Entity.Billing.YarnSalesEntryTrans>();
            Entity.Billing.YarnSalesEntryTrans objYarnSalesEntryTrans = new Entity.Billing.YarnSalesEntryTrans();
            Entity.Master.Product objProduct; Entity.Billing.Tax objTax;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_YARNSALESENTRYTRANS);
                db.AddInParameter(cmd, "@FK_YarnSalesEntryID", DbType.Int32, iYarnSalesEntryID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objYarnSalesEntryTrans = new Entity.Billing.YarnSalesEntryTrans();
                        objProduct = new Entity.Master.Product();
                        objTax = new Entity.Billing.Tax();

                        objYarnSalesEntryTrans.YarnSalesEntryTransID = Convert.ToInt32(drData["PK_YarnSalesEntryTransID"]);
                        objYarnSalesEntryTrans.YarnSalesEntryID = Convert.ToInt32(drData["FK_YarnSalesEntryID"]);

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxPercentage = Convert.ToDecimal(drData["TaxPercent"]);
                        objTax.SGSTPercent = Convert.ToDecimal(drData["SGSTPercent"]);
                        objTax.IGSTPercent = Convert.ToDecimal(drData["IGSTPercent"]);
                        objTax.CGSTPercent = Convert.ToDecimal(drData["CGSTPercent"]);
                        objYarnSalesEntryTrans.Tax = objTax;

                        objYarnSalesEntryTrans.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objYarnSalesEntryTrans.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objYarnSalesEntryTrans.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objYarnSalesEntryTrans.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objYarnSalesEntryTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objYarnSalesEntryTrans.Rate = Convert.ToInt32(drData["Rate"]);
                        objYarnSalesEntryTrans.SubTotal = Convert.ToDecimal(drData["SubTotal"]);
                        objYarnSalesEntryTrans.DiscountPercentage = Convert.ToDecimal(drData["DiscountPercentage"]);
                        objYarnSalesEntryTrans.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);

                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objYarnSalesEntryTrans.Product = objProduct;
                        objYarnSalesEntryTrans.Barcode = Convert.ToString(drData["Barcode"]);
                        objYarnSalesEntryTrans.NewProductFlag = Convert.ToBoolean(drData["NewProductFlag"]);
                        objList.Add(objYarnSalesEntryTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "YarnSalesEntryTrans GetYarnSalesEntryTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveYarnSalesEntryTransaction(Collection<Entity.Billing.YarnSalesEntryTrans> ObjYarnSalesEntryTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.YarnSalesEntryTrans ObjYarnSalesEntryTransaction in ObjYarnSalesEntryTransList)
            {
                if (ObjYarnSalesEntryTransaction.StatusFlag == "I")
                    iID = AddYarnSalesEntryTrans(ObjYarnSalesEntryTransaction);
                else if (ObjYarnSalesEntryTransaction.StatusFlag == "U")
                    bResult = UpdateYarnSalesEntryTrans(ObjYarnSalesEntryTransaction);
                else if (ObjYarnSalesEntryTransaction.StatusFlag == "D")
                    bResult = DeleteYarnSalesEntryTrans(ObjYarnSalesEntryTransaction.YarnSalesEntryTransID);
            }
        }
        public static int AddYarnSalesEntryTrans(Entity.Billing.YarnSalesEntryTrans objYarnSalesEntryTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_YARNSALESENTRYTRANS);
                db.AddOutParameter(cmd, "@PK_YarnSalesEntryTransID", DbType.Int32, objYarnSalesEntryTrans.YarnSalesEntryTransID);
                db.AddInParameter(cmd, "@FK_YarnSalesEntryID", DbType.Int32, objYarnSalesEntryTrans.YarnSalesEntryID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objYarnSalesEntryTrans.Product.ProductID);
                db.AddInParameter(cmd, "@SMSCode", DbType.String, objYarnSalesEntryTrans.Product.SMSCode);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objYarnSalesEntryTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objYarnSalesEntryTrans.Rate);
                db.AddInParameter(cmd, "@DiscountPercentage", DbType.Decimal, objYarnSalesEntryTrans.DiscountPercentage);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objYarnSalesEntryTrans.DiscountAmount);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objYarnSalesEntryTrans.SubTotal);
                db.AddInParameter(cmd, "@Barcode", DbType.String, objYarnSalesEntryTrans.Barcode);
                db.AddInParameter(cmd, "@ProductName", DbType.String, objYarnSalesEntryTrans.Product.ProductName);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.String, objYarnSalesEntryTrans.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objYarnSalesEntryTrans.Tax.TaxPercentage);
                db.AddInParameter(cmd, "@IGSTPercent", DbType.Decimal, objYarnSalesEntryTrans.Tax.IGSTPercent);
                db.AddInParameter(cmd, "@SGSTPercent", DbType.Decimal, objYarnSalesEntryTrans.Tax.SGSTPercent);
                db.AddInParameter(cmd, "@CGSTPercent", DbType.Decimal, objYarnSalesEntryTrans.Tax.CGSTPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objYarnSalesEntryTrans.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objYarnSalesEntryTrans.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objYarnSalesEntryTrans.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objYarnSalesEntryTrans.TaxAmount);
                db.AddInParameter(cmd, "@NewProductFlag", DbType.Boolean, objYarnSalesEntryTrans.NewProductFlag);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_YarnSalesEntryTransID"));
            }
            catch (Exception ex)
            {
                sException = "YarnSalesEntryTrans AddYarnSalesEntryTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateYarnSalesEntryTrans(Entity.Billing.YarnSalesEntryTrans objYarnSalesEntryTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_YARNSALESENTRYTRANS);
                db.AddInParameter(cmd, "@PK_YarnSalesEntryTransID", DbType.Int32, objYarnSalesEntryTrans.YarnSalesEntryTransID);
                db.AddInParameter(cmd, "@FK_YarnSalesEntryID", DbType.Int32, objYarnSalesEntryTrans.YarnSalesEntryID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objYarnSalesEntryTrans.Product.ProductID);
                db.AddInParameter(cmd, "@SMSCode", DbType.String, objYarnSalesEntryTrans.Product.SMSCode);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objYarnSalesEntryTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objYarnSalesEntryTrans.Rate);
                db.AddInParameter(cmd, "@DiscountPercentage", DbType.Decimal, objYarnSalesEntryTrans.DiscountPercentage);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objYarnSalesEntryTrans.DiscountAmount);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objYarnSalesEntryTrans.SubTotal);
                db.AddInParameter(cmd, "@Barcode", DbType.String, objYarnSalesEntryTrans.Barcode);
                db.AddInParameter(cmd, "@ProductName", DbType.String, objYarnSalesEntryTrans.Product.ProductName);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.String, objYarnSalesEntryTrans.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objYarnSalesEntryTrans.Tax.TaxPercentage);
                db.AddInParameter(cmd, "@IGSTPercent", DbType.Decimal, objYarnSalesEntryTrans.Tax.IGSTPercent);
                db.AddInParameter(cmd, "@SGSTPercent", DbType.Decimal, objYarnSalesEntryTrans.Tax.SGSTPercent);
                db.AddInParameter(cmd, "@CGSTPercent", DbType.Decimal, objYarnSalesEntryTrans.Tax.CGSTPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objYarnSalesEntryTrans.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objYarnSalesEntryTrans.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objYarnSalesEntryTrans.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objYarnSalesEntryTrans.TaxAmount);
                db.AddInParameter(cmd, "@NewProductFlag", DbType.Boolean, objYarnSalesEntryTrans.NewProductFlag);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "YarnSalesEntryTrans UpdateYarnSalesEntryTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteYarnSalesEntryTrans(int iYarnSalesEntryTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_YARNSALESENTRYTRANS);
                db.AddInParameter(cmd, "@PK_YarnSalesEntryTransID", DbType.Int32, iYarnSalesEntryTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "YarnSalesEntryTrans DeleteYarnSalesEntryTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

    }

   
}
