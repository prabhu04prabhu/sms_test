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
    public class RetailOutward
    {
        public static Collection<Entity.Billing.RetailOutward> GetRetailOutward(int ipatientID = 0, int IsRetail = 0, int IsYarnBill = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.RetailOutward> objList = new Collection<Entity.Billing.RetailOutward>();
            Entity.Billing.RetailOutward objRetailOutward;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_RETAILOUTWARD);
                db.AddInParameter(cmd, "@PK_RetailOutwardID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, IsYarnBill);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRetailOutward = new Entity.Billing.RetailOutward();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objRetailOutward.RetailOutwardID = Convert.ToInt32(drData["PK_RetailOutwardID"]);
                        objRetailOutward.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objRetailOutward.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objRetailOutward.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objRetailOutward.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objRetailOutward.Agent = objAgent;

                        objRetailOutward.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objRetailOutward.Address = Convert.ToString(drData["Address"]);
                        objRetailOutward.Area = Convert.ToString(drData["Area"]);
                        objRetailOutward.CustomerName = Convert.ToString(drData["CustomerName"]);

                        objRetailOutward.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objRetailOutward.sLRDate = objRetailOutward.LRDate.ToString("dd/MM/yyyy");
                        objRetailOutward.TransportName = Convert.ToString(drData["TransportName"]);
                        objRetailOutward.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objRetailOutward.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objRetailOutward.LRNo = Convert.ToString(drData["LRNo"]);
                        objRetailOutward.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objRetailOutward.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objRetailOutward.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objRetailOutward.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objRetailOutward.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objRetailOutward.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objRetailOutward.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objRetailOutward.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objRetailOutward.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objRetailOutward.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objRetailOutward.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objRetailOutward.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objRetailOutward.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objRetailOutward.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objRetailOutward.Status = Convert.ToString(drData["Status"]);
                        objRetailOutward.Narration = Convert.ToString(drData["Narration"]);
                        objRetailOutward.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objRetailOutward.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objRetailOutward.ACKno = Convert.ToString(drData["ACKno"]);
                        objRetailOutward.IRNno = Convert.ToString(drData["IRNno"]);
                        objRetailOutward.Notes = Convert.ToString(drData["Notes"]);
                        objRetailOutward.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objRetailOutward.sDeliveryDate = objRetailOutward.DeliveryDate.ToString("dd/MM/yyyy");
                        objRetailOutward.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objRetailOutward.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objRetailOutward.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objRetailOutward.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objRetailOutward.Bank = objBank;

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objRetailOutward.SalesOrder = objSalesOrder;

                        objRetailOutward.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objRetailOutward.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objRetailOutward.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objRetailOutward.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objRetailOutward.Transport = objTransport;

                        objRetailOutward.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objRetailOutward.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objRetailOutward.sInvoiceDate = objRetailOutward.InvoiceDate.ToString("dd/MM/yyyy") + " " + objRetailOutward.CreatedOn.ToString("h:mm");
                        objRetailOutward.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objRetailOutward.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);

                        objList.Add(objRetailOutward);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "RetailOutward GetRetailOutward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }



        public static Collection<Entity.Billing.RetailOutward> GetTopRetailOutward(int ipatientID = 0, int IsRetail = 0, int IsYarnBill = 0, int iCustomerID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.RetailOutward> objList = new Collection<Entity.Billing.RetailOutward>();
            Entity.Billing.RetailOutward objRetailOutward;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPRETAILOUTWARD);
                db.AddInParameter(cmd, "@PK_RetailOutwardID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, IsYarnBill);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iCustomerID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRetailOutward = new Entity.Billing.RetailOutward();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objRetailOutward.RetailOutwardID = Convert.ToInt32(drData["PK_RetailOutwardID"]);
                        objRetailOutward.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objRetailOutward.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Area = Convert.ToString(drData["area"]);
                        objCustomer.MinDueDays = Convert.ToInt32(drData["MinDueDays"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objRetailOutward.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objRetailOutward.Tax = objTax;

                        objRetailOutward.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objRetailOutward.Address = Convert.ToString(drData["Address"]);
                        objRetailOutward.Area = Convert.ToString(drData["Area"]);
                        objRetailOutward.CustomerName = Convert.ToString(drData["CustomerName"]);

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objRetailOutward.Agent = objAgent;

                        objRetailOutward.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objRetailOutward.DueDays = Convert.ToInt32(drData["DueDays"]);
                        objRetailOutward.sLRDate = objRetailOutward.LRDate.ToString("dd/MM/yyyy");
                        objRetailOutward.TransportName = Convert.ToString(drData["TransportName"]);
                        objRetailOutward.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objRetailOutward.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objRetailOutward.LRNo = Convert.ToString(drData["LRNo"]);
                        objRetailOutward.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objRetailOutward.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objRetailOutward.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objRetailOutward.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objRetailOutward.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objRetailOutward.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objRetailOutward.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objRetailOutward.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objRetailOutward.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objRetailOutward.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objRetailOutward.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objRetailOutward.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        //objRetailOutward.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objRetailOutward.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objRetailOutward.Status = Convert.ToString(drData["Status"]);
                        objRetailOutward.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objRetailOutward.Narration = Convert.ToString(drData["Narration"]);
                        objRetailOutward.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objRetailOutward.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objRetailOutward.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                       // objRetailOutward.RetailsPaymentMode = Convert.ToString(drData["RetailsPaymentMode"]);
                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objRetailOutward.Bank = objBank;

                        objRetailOutward.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objRetailOutward.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objRetailOutward.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objRetailOutward.Comments = Convert.ToString(drData["Comments"]);
                        objRetailOutward.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objRetailOutward.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                      //  objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                       // objRetailOutward.SalesOrder = objSalesOrder;

                        objRetailOutward.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objRetailOutward.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objRetailOutward.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objRetailOutward.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objRetailOutward.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objRetailOutward.sCreatedOn = objRetailOutward.CreatedOn.ToString("dd/MM/yyyy h:mm");
                        objRetailOutward.sInvoiceDate = objRetailOutward.InvoiceDate.ToString("dd/MM/yyyy");
                        objRetailOutward.sWholeSalesInvoiceDate = objRetailOutward.InvoiceDate.ToString("dd/MM/yyyy") + " " + objRetailOutward.CreatedOn.ToString("h:mm");
                        objRetailOutward.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objRetailOutward.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);
                        objRetailOutward.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objRetailOutward.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objRetailOutward.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objRetailOutward.Transport = objTransport;
                        objRetailOutward.ACKno = Convert.ToString(drData["ACKno"]);
                        objRetailOutward.IRNno = Convert.ToString(drData["IRNno"]);
                        objList.Add(objRetailOutward);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "RetailOutward GetRetailOutward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.RetailOutward> SearchRetailOutward(string ID, int IsRetail = 0, int IsYarnBill = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.RetailOutward> objList = new Collection<Entity.Billing.RetailOutward>();
            Entity.Billing.RetailOutward objRetailOutward; Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_RETAILOUTWARDRDYNAMIC);
                db.AddInParameter(cmd, "@PK_RetailOutwardID", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, IsYarnBill);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRetailOutward = new Entity.Billing.RetailOutward();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objRetailOutward.RetailOutwardID = Convert.ToInt32(drData["PK_RetailOutwardID"]);
                        objRetailOutward.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objRetailOutward.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Area = Convert.ToString(drData["area"]);
                        objCustomer.MinDueDays = Convert.ToInt32(drData["MinDueDays"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objRetailOutward.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objRetailOutward.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objRetailOutward.Agent = objAgent;


                        objRetailOutward.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objRetailOutward.Address = Convert.ToString(drData["Address"]);
                        objRetailOutward.Area = Convert.ToString(drData["Area"]);
                        objRetailOutward.CustomerName = Convert.ToString(drData["CustomerName"]);

                        objRetailOutward.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objRetailOutward.DueDays = Convert.ToInt32(drData["DueDays"]);
                        objRetailOutward.sLRDate = objRetailOutward.LRDate.ToString("dd/MM/yyyy");
                        objRetailOutward.TransportName = Convert.ToString(drData["TransportName"]);
                        objRetailOutward.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objRetailOutward.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objRetailOutward.LRNo = Convert.ToString(drData["LRNo"]);
                        objRetailOutward.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objRetailOutward.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objRetailOutward.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objRetailOutward.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objRetailOutward.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objRetailOutward.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objRetailOutward.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objRetailOutward.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objRetailOutward.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objRetailOutward.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objRetailOutward.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objRetailOutward.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        //objRetailOutward.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objRetailOutward.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objRetailOutward.Status = Convert.ToString(drData["Status"]);
                        objRetailOutward.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objRetailOutward.Narration = Convert.ToString(drData["Narration"]);
                        objRetailOutward.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objRetailOutward.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objRetailOutward.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        // objRetailOutward.RetailsPaymentMode = Convert.ToString(drData["RetailsPaymentMode"]);
                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objRetailOutward.Bank = objBank;

                        objRetailOutward.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objRetailOutward.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objRetailOutward.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objRetailOutward.Comments = Convert.ToString(drData["Comments"]);
                        objRetailOutward.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objRetailOutward.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        //  objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        // objRetailOutward.SalesOrder = objSalesOrder;

                        objRetailOutward.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objRetailOutward.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objRetailOutward.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objRetailOutward.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objRetailOutward.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objRetailOutward.sCreatedOn = objRetailOutward.CreatedOn.ToString("dd/MM/yyyy h:mm");
                        objRetailOutward.sInvoiceDate = objRetailOutward.InvoiceDate.ToString("dd/MM/yyyy");
                        objRetailOutward.sWholeSalesInvoiceDate = objRetailOutward.InvoiceDate.ToString("dd/MM/yyyy") + " " + objRetailOutward.CreatedOn.ToString("h:mm");
                        objRetailOutward.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objRetailOutward.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);
                        objRetailOutward.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objRetailOutward.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objRetailOutward.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objRetailOutward.Transport = objTransport;
                        objRetailOutward.ACKno = Convert.ToString(drData["ACKno"]);
                        objRetailOutward.IRNno = Convert.ToString(drData["IRNno"]);

                        objList.Add(objRetailOutward);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "RetailOutward GetRetailOutward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Entity.Billing.RetailOutward GetRetailOutwardByID(int iRetailOutwardID, int IsRetail, int IsYarnBill = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.RetailOutward objRetailOutward = new Entity.Billing.RetailOutward();
            Entity.Customer objCustomer; Entity.Billing.Tax objTax; Entity.Gift objGift; Entity.State objState;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder; Entity.CustomerTypes objCustomerTypes;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_RETAILOUTWARD);
                db.AddInParameter(cmd, "@PK_RetailOutwardID", DbType.Int32, iRetailOutwardID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, IsYarnBill);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRetailOutward = new Entity.Billing.RetailOutward();
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

                        objRetailOutward.RetailOutwardID = Convert.ToInt32(drData["PK_RetailOutwardID"]);
                        objRetailOutward.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objRetailOutward.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objRetailOutward.sInvoiceDate = objRetailOutward.InvoiceDate.ToString("dd/MM/yyyy");
                        objRetailOutward.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        string Date = objRetailOutward.LRDate.ToString("dd/MM/yyyy");
                        objRetailOutward.sLRDate = objRetailOutward.LRDate.ToString("dd/MM/yyyy");
                        objRetailOutward.TransportName = Convert.ToString(drData["TransportName"]);
                        objRetailOutward.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objRetailOutward.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objRetailOutward.LRNo = Convert.ToString(drData["LRNo"]);

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objRetailOutward.Customer = objCustomer;

                        objRetailOutward.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objRetailOutward.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objRetailOutward.Address = Convert.ToString(drData["Address"]);
                        objRetailOutward.Area = Convert.ToString(drData["Area"]);

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objRetailOutward.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objRetailOutward.Agent = objAgent;

                        objCustomerTypes.CustomertypeID = Convert.ToInt32(drData["FK_CustomertypeID"]);
                        objRetailOutward.CustomerType = objCustomerTypes;

                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objRetailOutward.State = objState;

                        objGift.GiftID = Convert.ToInt32(drData["FK_GiftID"]);
                        objRetailOutward.Gift = objGift;
                        objRetailOutward.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objRetailOutward.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objRetailOutward.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objRetailOutward.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objRetailOutward.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objRetailOutward.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objRetailOutward.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objRetailOutward.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objRetailOutward.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objRetailOutward.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objRetailOutward.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objRetailOutward.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objRetailOutward.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objRetailOutward.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objRetailOutward.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objRetailOutward.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objRetailOutward.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objRetailOutward.Comments = Convert.ToString(drData["Comments"]);
                        objRetailOutward.Status = Convert.ToString(drData["Status"]);

                        objRetailOutward.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objRetailOutward.Narration = Convert.ToString(drData["Narration"]);
                        objRetailOutward.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objRetailOutward.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objRetailOutward.Notes = Convert.ToString(drData["Notes"]);
                        objRetailOutward.CardCharges = Convert.ToDecimal(drData["CardCharges"]);
                        objRetailOutward.CardNo = Convert.ToString(drData["CardNo"]);
                        objRetailOutward.TenderAmount = Convert.ToDecimal(drData["TenderAmount"]);
                        objRetailOutward.BalanceGiven = Convert.ToDecimal(drData["BalanceGiven"]);
                        objRetailOutward.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objRetailOutward.sDeliveryDate = objRetailOutward.DeliveryDate.ToString("dd/MM/yyyy");
                        objRetailOutward.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objRetailOutward.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);


                        objRetailOutward.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objRetailOutward.Address = Convert.ToString(drData["Address"]);
                        objRetailOutward.Area = Convert.ToString(drData["Area"]);
                        objRetailOutward.CustomerName = Convert.ToString(drData["CustomerName"]);

                        objRetailOutward.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objRetailOutward.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objRetailOutward.Bank = objBank;

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objRetailOutward.SalesOrder = objSalesOrder;

                        objRetailOutward.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objRetailOutward.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objRetailOutward.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objRetailOutward.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objRetailOutward.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objRetailOutward.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objRetailOutward.ShippingAddress = objShippingAddress;

                        objRetailOutward.ACKno = Convert.ToString(drData["ACKno"]);
                        objRetailOutward.IRNno = Convert.ToString(drData["IRNno"]);

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objRetailOutward.Transport = objTransport;
                        objRetailOutward.RetailOutwardTrans = RetailOutwardTrans.GetRetailOutwardTransByRetailOutwardID(objRetailOutward.RetailOutwardID);

                    }
                }
            }
            catch (Exception ex)
            {
                sException = "RetailOutward GetRetailOutwardByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objRetailOutward;
        }
        public static Entity.Billing.RetailOutward GetLastRetailOutwardByID(int iRetailOutwardID, int IsRetail, int IsYarnBill = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.RetailOutward objRetailOutward = new Entity.Billing.RetailOutward();
            Entity.Customer objCustomer; Entity.Billing.Tax objTax; Entity.Gift objGift; Entity.State objState;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder; Entity.CustomerTypes objCustomerTypes;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_RETAILOUTWARDLASTBILL);
                db.AddInParameter(cmd, "@PK_RetailOutwardID", DbType.Int32, iRetailOutwardID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, IsYarnBill);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRetailOutward = new Entity.Billing.RetailOutward();
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

                        objRetailOutward.RetailOutwardID = Convert.ToInt32(drData["PK_RetailOutwardID"]);
                        objRetailOutward.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objRetailOutward.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objRetailOutward.sInvoiceDate = objRetailOutward.InvoiceDate.ToString("dd/MM/yyyy");
                        objRetailOutward.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        string Date = objRetailOutward.LRDate.ToString("dd/MM/yyyy");
                        objRetailOutward.sLRDate = objRetailOutward.LRDate.ToString("dd/MM/yyyy");
                        objRetailOutward.TransportName = Convert.ToString(drData["TransportName"]);
                        objRetailOutward.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objRetailOutward.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objRetailOutward.LRNo = Convert.ToString(drData["LRNo"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objRetailOutward.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objRetailOutward.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objRetailOutward.Agent = objAgent;

                        objCustomerTypes.CustomertypeID = Convert.ToInt32(drData["FK_CustomertypeID"]);
                        objRetailOutward.CustomerType = objCustomerTypes;

                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objRetailOutward.State = objState;

                        objGift.GiftID = Convert.ToInt32(drData["FK_GiftID"]);
                        objRetailOutward.Gift = objGift;
                        objRetailOutward.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objRetailOutward.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objRetailOutward.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objRetailOutward.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objRetailOutward.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objRetailOutward.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objRetailOutward.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objRetailOutward.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objRetailOutward.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objRetailOutward.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objRetailOutward.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objRetailOutward.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objRetailOutward.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objRetailOutward.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objRetailOutward.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objRetailOutward.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objRetailOutward.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objRetailOutward.Status = Convert.ToString(drData["Status"]);

                        objRetailOutward.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objRetailOutward.Address = Convert.ToString(drData["Address"]);
                        objRetailOutward.Area = Convert.ToString(drData["Area"]);
                        objRetailOutward.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objRetailOutward.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objRetailOutward.Narration = Convert.ToString(drData["Narration"]);
                        objRetailOutward.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objRetailOutward.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objRetailOutward.ACKno = Convert.ToString(drData["ACKno"]);
                        objRetailOutward.IRNno = Convert.ToString(drData["IRNno"]);
                        objRetailOutward.Notes = Convert.ToString(drData["Notes"]);
                        objRetailOutward.CardCharges = Convert.ToDecimal(drData["CardCharges"]);
                        objRetailOutward.CardNo = Convert.ToString(drData["CardNo"]);
                        objRetailOutward.TenderAmount = Convert.ToDecimal(drData["TenderAmount"]);
                        objRetailOutward.BalanceGiven = Convert.ToDecimal(drData["BalanceGiven"]);
                        objRetailOutward.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objRetailOutward.sDeliveryDate = objRetailOutward.DeliveryDate.ToString("dd/MM/yyyy");
                        objRetailOutward.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objRetailOutward.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objRetailOutward.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objRetailOutward.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objRetailOutward.Bank = objBank;

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objRetailOutward.SalesOrder = objSalesOrder;

                        objRetailOutward.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objRetailOutward.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objRetailOutward.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objRetailOutward.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objRetailOutward.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objRetailOutward.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objRetailOutward.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objRetailOutward.Transport = objTransport;
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "RetailOutward GetRetailOutwardByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objRetailOutward;
        }
        public static int AddRetailOutward(Entity.Billing.RetailOutward objRetailOutward)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_RETAILOUTWARD);
                db.AddOutParameter(cmd, "@PK_RetailOutwardID", DbType.Int32, objRetailOutward.RetailOutwardID);
                db.AddInParameter(cmd, "@InvoiceNo", DbType.String, objRetailOutward.InvoiceNo);
                db.AddInParameter(cmd, "@InvoiceDate", DbType.String, objRetailOutward.sInvoiceDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objRetailOutward.Customer.CustomerID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objRetailOutward.Tax.TaxID);
                db.AddInParameter(cmd, "@FK_AgentID", DbType.Int32, objRetailOutward.Agent.AgentID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objRetailOutward.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objRetailOutward.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objRetailOutward.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objRetailOutward.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objRetailOutward.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objRetailOutward.TotalAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objRetailOutward.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objRetailOutward.DiscountAmount);
                db.AddInParameter(cmd, "@AdditionalDiscount", DbType.Decimal, objRetailOutward.AdditionalDiscount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objRetailOutward.Roundoff);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objRetailOutward.NetAmount);
                db.AddInParameter(cmd, "@PaidAmount", DbType.Decimal, objRetailOutward.PaidAmount);
                db.AddInParameter(cmd, "@BalanceAmount", DbType.Decimal, objRetailOutward.BalanceAmount);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objRetailOutward.FinancialYear.FinancialYearID);
                db.AddInParameter(cmd, "@CancelReason", DbType.String, objRetailOutward.CancelReason);
                db.AddInParameter(cmd, "@IsActive", DbType.Boolean, objRetailOutward.IsActive);
                db.AddInParameter(cmd, "@PaymentMode", DbType.String, objRetailOutward.PaymentMode);
                db.AddInParameter(cmd, "@Narration", DbType.String, objRetailOutward.Narration);
                db.AddInParameter(cmd, "@Comments", DbType.String, objRetailOutward.Comments);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objRetailOutward.Bank.LedgerID);
                db.AddInParameter(cmd, "@Status", DbType.String, objRetailOutward.Status);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, objRetailOutward.IsRetailBill);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, objRetailOutward.IsYarnBill);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objRetailOutward.CreatedBy.UserID);
                db.AddInParameter(cmd, "@TransportName", DbType.String, objRetailOutward.TransportName);
                db.AddInParameter(cmd, "@LRNo", DbType.String, objRetailOutward.LRNo);
                db.AddInParameter(cmd, "@EWayNo", DbType.String, objRetailOutward.EWayNo);
                db.AddInParameter(cmd, "@VehicleNo", DbType.String, objRetailOutward.VehicleNo);
                db.AddInParameter(cmd, "@CardNo", DbType.String, objRetailOutward.CardNo);
                db.AddInParameter(cmd, "@BalanceGiven", DbType.Decimal, objRetailOutward.BalanceGiven);
                db.AddInParameter(cmd, "@TenderAmount", DbType.Decimal, objRetailOutward.TenderAmount);
                db.AddInParameter(cmd, "@CardCharges", DbType.Decimal, objRetailOutward.CardCharges);
                db.AddInParameter(cmd, "@SalesPoints", DbType.Decimal, objRetailOutward.SalesPoints);
                db.AddInParameter(cmd, "@UsedPoints", DbType.Decimal, objRetailOutward.UsedPoints);
                db.AddInParameter(cmd, "@ExchangeAmount", DbType.Decimal, objRetailOutward.ExchangeAmount);
                db.AddInParameter(cmd, "@LRDate", DbType.String, objRetailOutward.sLRDate);
                db.AddInParameter(cmd, "@FK_GiftID", DbType.Int32, objRetailOutward.Gift.GiftID);
                db.AddInParameter(cmd, "@CustomerType", DbType.String, objRetailOutward.CustomerType.CustomerTypeName);
                db.AddInParameter(cmd, "@FK_CustomerTypeID", DbType.Int32, objRetailOutward.CustomerType.CustomertypeID);
                db.AddInParameter(cmd, "@FK_StateID", DbType.Int32, objRetailOutward.State.StateID);
                db.AddInParameter(cmd, "@TCSPercent", DbType.Decimal, objRetailOutward.TCSPercent);
                db.AddInParameter(cmd, "@TCSAmount", DbType.Decimal, objRetailOutward.TCSAmount);
                db.AddInParameter(cmd, "@FK_ShippingAddressID", DbType.Int32, objRetailOutward.ShippingAddress.ShippingAddressID);
                db.AddInParameter(cmd, "@FK_TransportID", DbType.Int32, objRetailOutward.Transport.TransportID);
                db.AddInParameter(cmd, "@NoofBags", DbType.String, objRetailOutward.NoofBags);
                db.AddInParameter(cmd, "@Notes", DbType.String, objRetailOutward.Notes);
                db.AddInParameter(cmd, "@ImagePath1", DbType.String, objRetailOutward.ImagePath1);
                db.AddInParameter(cmd, "@ImagePath2", DbType.String, objRetailOutward.ImagePath2);
                db.AddInParameter(cmd, "@ImagePath3", DbType.String, objRetailOutward.ImagePath3);
                db.AddInParameter(cmd, "@BillStatus", DbType.String, objRetailOutward.BillStatus);
                db.AddInParameter(cmd, "@DeliveryDate", DbType.String, objRetailOutward.sDeliveryDate);
                db.AddInParameter(cmd, "@TransportCharges", DbType.Decimal, objRetailOutward.TransportCharges);
                db.AddInParameter(cmd, "@OtherCharges", DbType.Decimal, objRetailOutward.OtherCharges);
                db.AddInParameter(cmd, "@ACKno", DbType.String, objRetailOutward.ACKno);
                db.AddInParameter(cmd, "@IRNno", DbType.String, objRetailOutward.IRNno);
                db.AddInParameter(cmd, "@MobileNo", DbType.String, objRetailOutward.MobileNo);
                db.AddInParameter(cmd, "@CustomerName", DbType.String, objRetailOutward.CustomerName);
                db.AddInParameter(cmd, "@Address", DbType.String, objRetailOutward.Address);
                db.AddInParameter(cmd, "@Area", DbType.String, objRetailOutward.Area);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_RetailOutwardID"));

                foreach (Entity.Billing.RetailOutwardTrans ObjRetailOutwardTrans in objRetailOutward.RetailOutwardTrans)
                    ObjRetailOutwardTrans.RetailOutwardID = iID;
                   RetailOutwardTrans.SaveRetailOutwardTransaction(objRetailOutward.RetailOutwardTrans);
            }
            catch (Exception ex)
            {
                sException = "RetailOutward AddRetailOutward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateRetailOutward(Entity.Billing.RetailOutward objRetailOutward)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_RETAILOUTWARD);
                db.AddInParameter(cmd, "@PK_RetailOutwardID", DbType.Int32, objRetailOutward.RetailOutwardID);
                db.AddInParameter(cmd, "@InvoiceNo", DbType.String, objRetailOutward.InvoiceNo);
                db.AddInParameter(cmd, "@InvoiceDate", DbType.String, objRetailOutward.sInvoiceDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objRetailOutward.Customer.CustomerID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objRetailOutward.Tax.TaxID);
                db.AddInParameter(cmd, "@FK_AgentID", DbType.Int32, objRetailOutward.Agent.AgentID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objRetailOutward.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objRetailOutward.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objRetailOutward.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objRetailOutward.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objRetailOutward.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objRetailOutward.TotalAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objRetailOutward.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objRetailOutward.DiscountAmount);
                db.AddInParameter(cmd, "@AdditionalDiscount", DbType.Decimal, objRetailOutward.AdditionalDiscount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objRetailOutward.Roundoff);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objRetailOutward.NetAmount);
                db.AddInParameter(cmd, "@PaidAmount", DbType.Decimal, objRetailOutward.PaidAmount);
                db.AddInParameter(cmd, "@BalanceAmount", DbType.Decimal, objRetailOutward.BalanceAmount);
               // db.AddInParameter(cmd, "@FK_SalesOrderID", DbType.Int32, objRetailOutward.SalesOrder.SalesOrderID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objRetailOutward.FinancialYear.FinancialYearID);
                db.AddInParameter(cmd, "@CancelReason", DbType.String, objRetailOutward.CancelReason);
                db.AddInParameter(cmd, "@IsActive", DbType.Boolean, objRetailOutward.IsActive);
                db.AddInParameter(cmd, "@PaymentMode", DbType.String, objRetailOutward.PaymentMode);
                db.AddInParameter(cmd, "@Narration", DbType.String, objRetailOutward.Narration);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objRetailOutward.Bank.LedgerID);
                db.AddInParameter(cmd, "@Status", DbType.String, objRetailOutward.Status);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objRetailOutward.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@TransportName", DbType.String, objRetailOutward.TransportName);
                db.AddInParameter(cmd, "@LRNo", DbType.String, objRetailOutward.LRNo);
                db.AddInParameter(cmd, "@EWayNo", DbType.String, objRetailOutward.EWayNo);
                db.AddInParameter(cmd, "@VehicleNo", DbType.String, objRetailOutward.VehicleNo);
                db.AddInParameter(cmd, "@CardNo", DbType.String, objRetailOutward.CardNo);
                db.AddInParameter(cmd, "@BalanceGiven", DbType.Decimal, objRetailOutward.BalanceGiven);
                db.AddInParameter(cmd, "@TenderAmount", DbType.Decimal, objRetailOutward.TenderAmount);
                db.AddInParameter(cmd, "@CardCharges", DbType.Decimal, objRetailOutward.CardCharges);
                db.AddInParameter(cmd, "@SalesPoints", DbType.Decimal, objRetailOutward.SalesPoints);
                db.AddInParameter(cmd, "@UsedPoints", DbType.Decimal, objRetailOutward.UsedPoints);
                db.AddInParameter(cmd, "@ExchangeAmount", DbType.Decimal, objRetailOutward.ExchangeAmount);
                db.AddInParameter(cmd, "@LRDate", DbType.String, objRetailOutward.sLRDate);
                db.AddInParameter(cmd, "@FK_GiftID", DbType.Int32, objRetailOutward.Gift.GiftID);
                db.AddInParameter(cmd, "@CustomerType", DbType.String, objRetailOutward.CustomerType.CustomerTypeName);
                db.AddInParameter(cmd, "@FK_CustomerTypeID", DbType.Int32, objRetailOutward.CustomerType.CustomertypeID);
                db.AddInParameter(cmd, "@FK_StateID", DbType.Int32, objRetailOutward.State.StateID);
                db.AddInParameter(cmd, "@TCSPercent", DbType.Decimal, objRetailOutward.TCSPercent);
                db.AddInParameter(cmd, "@TCSAmount", DbType.Decimal, objRetailOutward.TCSAmount);
                db.AddInParameter(cmd, "@FK_ShippingAddressID", DbType.Int32, objRetailOutward.ShippingAddress.ShippingAddressID);
                db.AddInParameter(cmd, "@FK_TransportID", DbType.Int32, objRetailOutward.Transport.TransportID);
                db.AddInParameter(cmd, "@NoofBags", DbType.String, objRetailOutward.NoofBags);
                db.AddInParameter(cmd, "@Notes", DbType.String, objRetailOutward.Notes);
                db.AddInParameter(cmd, "@ImagePath1", DbType.String, objRetailOutward.ImagePath1);
                db.AddInParameter(cmd, "@ImagePath2", DbType.String, objRetailOutward.ImagePath2);
                db.AddInParameter(cmd, "@ImagePath3", DbType.String, objRetailOutward.ImagePath3);
                db.AddInParameter(cmd, "@BillStatus", DbType.String, objRetailOutward.BillStatus);
                db.AddInParameter(cmd, "@Comments", DbType.String, objRetailOutward.Comments);
                db.AddInParameter(cmd, "@DeliveryDate", DbType.String, objRetailOutward.sDeliveryDate);
                db.AddInParameter(cmd, "@TransportCharges", DbType.Decimal, objRetailOutward.TransportCharges);
                db.AddInParameter(cmd, "@OtherCharges", DbType.Decimal, objRetailOutward.OtherCharges);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, objRetailOutward.IsYarnBill);
                db.AddInParameter(cmd, "@ACKno", DbType.String, objRetailOutward.ACKno);
                db.AddInParameter(cmd, "@IRNno", DbType.String, objRetailOutward.IRNno);
                db.AddInParameter(cmd, "@MobileNo", DbType.String, objRetailOutward.MobileNo);
                db.AddInParameter(cmd, "@CustomerName", DbType.String, objRetailOutward.CustomerName);
                db.AddInParameter(cmd, "@Address", DbType.String, objRetailOutward.Address);
                db.AddInParameter(cmd, "@Area", DbType.String, objRetailOutward.Area);

                foreach (Entity.Billing.RetailOutwardTrans ObjRetailOutwardTrans in objRetailOutward.RetailOutwardTrans)
                    ObjRetailOutwardTrans.RetailOutwardID = objRetailOutward.RetailOutwardID;

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
                  RetailOutwardTrans.SaveRetailOutwardTransaction(objRetailOutward.RetailOutwardTrans);

            }
            catch (Exception ex)
            {
                sException = "RetailOutward UpdateRetailOutward| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteRetailOutward(int iRetailOutwardId, string Reason, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_RETAILOUTWARD);
                db.AddInParameter(cmd, "@PK_RetailOutwardID", DbType.Int32, iRetailOutwardId);
                db.AddInParameter(cmd, "@Reason", DbType.String, Reason);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, iUserId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "RetailOutward DeleteRetailOutward | " + ex.ToString();
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
                sException = "RetailOutward DeleteRetailOutward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return stockcnt;
        }

        public static Collection<Entity.Billing.RetailOutwardTransDetails> GetProductDetailsBySMSCode(int iID = 0, int iValue = 0, int iSupplierID = 0, string iSMSCode = null)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.RetailOutwardTransDetails> objList = new Collection<Entity.Billing.RetailOutwardTransDetails>();
            Entity.Billing.RetailOutwardTransDetails objRetailOutwardTrans = new Entity.Billing.RetailOutwardTransDetails();
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
                        objRetailOutwardTrans = new Entity.Billing.RetailOutwardTransDetails();
                        objProduct = new Entity.Master.Product();

                        objRetailOutwardTrans.RetailOutwardTransID = Convert.ToInt32(drData["PK_RetailOutwardTransID"]);
                        objRetailOutwardTrans.RetailOutwardID = Convert.ToInt32(drData["FK_RetailOutwardID"]);
                        objRetailOutwardTrans.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objRetailOutwardTrans.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objRetailOutwardTrans.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objRetailOutwardTrans.sInvoiceDate = objRetailOutwardTrans.InvoiceDate.ToString("dd/MM/yyyy");

                        objRetailOutwardTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objRetailOutwardTrans.Rate = Convert.ToDecimal(drData["Rate"]);
                        objRetailOutwardTrans.SubTotal = Convert.ToDecimal(drData["SubTotal"]);
                        objRetailOutwardTrans.DiscountPercentage = Convert.ToDecimal(drData["DiscountPercentage"]);
                        objRetailOutwardTrans.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objRetailOutwardTrans.Product = objProduct;
                        objRetailOutwardTrans.Barcode = Convert.ToString(drData["Barcode"]);
                        objList.Add(objRetailOutwardTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "RetailOutwardTrans GetRetailOutwardTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }


        public class RetailOutwardTrans
        {
            public static Collection<Entity.Billing.RetailOutwardTrans> GetRetailOutwardTransByRetailOutwardID(int iRetailOutwardID = 0)
            {
                string sException = string.Empty;
                Database db;
                DataSet dsList = null;
                Collection<Entity.Billing.RetailOutwardTrans> objList = new Collection<Entity.Billing.RetailOutwardTrans>();
                Entity.Billing.RetailOutwardTrans objRetailOutwardTrans = new Entity.Billing.RetailOutwardTrans();
                Entity.Master.Product objProduct; Entity.Billing.Tax objTax;
                try
                {
                    db = Entity.DBConnection.dbCon;
                    DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_RETAILOUTWARDTRANS);
                    db.AddInParameter(cmd, "@FK_RetailOutwardID", DbType.Int32, iRetailOutwardID);
                    dsList = db.ExecuteDataSet(cmd);

                    if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drData in dsList.Tables[0].Rows)
                        {
                            objRetailOutwardTrans = new Entity.Billing.RetailOutwardTrans();
                            objProduct = new Entity.Master.Product();
                            objTax = new Entity.Billing.Tax();

                            objRetailOutwardTrans.RetailOutwardTransID = Convert.ToInt32(drData["PK_RetailOutwardTransID"]);
                            objRetailOutwardTrans.RetailOutwardID = Convert.ToInt32(drData["FK_RetailOutwardID"]);

                            objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                            objTax.TaxPercentage = Convert.ToDecimal(drData["TaxPercent"]);
                            objTax.SGSTPercent = Convert.ToDecimal(drData["SGSTPercent"]);
                            objTax.IGSTPercent = Convert.ToDecimal(drData["IGSTPercent"]);
                            objTax.CGSTPercent = Convert.ToDecimal(drData["CGSTPercent"]);
                            objRetailOutwardTrans.Tax = objTax;

                            objRetailOutwardTrans.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                            objRetailOutwardTrans.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                            objRetailOutwardTrans.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                            objRetailOutwardTrans.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                            objRetailOutwardTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);
                            objRetailOutwardTrans.Rate = Convert.ToInt32(drData["Rate"]);
                            objRetailOutwardTrans.SubTotal = Convert.ToDecimal(drData["SubTotal"]);
                            objRetailOutwardTrans.DiscountPercentage = Convert.ToDecimal(drData["DiscountPercentage"]);
                            objRetailOutwardTrans.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);

                            objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                            objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                            objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                            objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                            objRetailOutwardTrans.Product = objProduct;
                            objRetailOutwardTrans.Barcode = Convert.ToString(drData["Barcode"]);
                            objRetailOutwardTrans.NewProductFlag = Convert.ToBoolean(drData["NewProductFlag"]);
                            objList.Add(objRetailOutwardTrans);
                        }
                    }
                }
                catch (Exception ex)
                {
                    sException = "RetailOutwardTrans GetRetailOutwardTrans | " + ex.ToString();
                    Log.Write(sException);
                    throw;
                }
                return objList;
            }
            public static void SaveRetailOutwardTransaction(Collection<Entity.Billing.RetailOutwardTrans> ObjRetailOutwardTransList)
            {
                int iID = 0;
                bool bResult = false;

                foreach (Entity.Billing.RetailOutwardTrans ObjRetailOutwardTransaction in ObjRetailOutwardTransList)
                {
                    if (ObjRetailOutwardTransaction.StatusFlag == "I")
                        iID = AddRetailOutwardTrans(ObjRetailOutwardTransaction);
                    else if (ObjRetailOutwardTransaction.StatusFlag == "U")
                        bResult = UpdateRetailOutwardTrans(ObjRetailOutwardTransaction);
                    else if (ObjRetailOutwardTransaction.StatusFlag == "D")
                        bResult = DeleteRetailOutwardTrans(ObjRetailOutwardTransaction.RetailOutwardTransID);
                }
            }
            public static int AddRetailOutwardTrans(Entity.Billing.RetailOutwardTrans objRetailOutwardTrans)
            {
                string sException = string.Empty;
                int iID = 0;
                Database db;
                try
                {
                    db = Entity.DBConnection.dbCon;
                    DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_RETAILOUTWARDTRANS);
                    db.AddOutParameter(cmd, "@PK_RetailOutwardTransID", DbType.Int32, objRetailOutwardTrans.RetailOutwardTransID);
                    db.AddInParameter(cmd, "@FK_RetailOutwardID", DbType.Int32, objRetailOutwardTrans.RetailOutwardID);
                    db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objRetailOutwardTrans.Product.ProductID);
                    db.AddInParameter(cmd, "@SMSCode", DbType.String, objRetailOutwardTrans.Product.SMSCode);
                    db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objRetailOutwardTrans.Quantity);
                    db.AddInParameter(cmd, "@Rate", DbType.Decimal, objRetailOutwardTrans.Rate);
                    db.AddInParameter(cmd, "@DiscountPercentage", DbType.Decimal, objRetailOutwardTrans.DiscountPercentage);
                    db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objRetailOutwardTrans.DiscountAmount);
                    db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objRetailOutwardTrans.SubTotal);
                    db.AddInParameter(cmd, "@Barcode", DbType.String, objRetailOutwardTrans.Barcode);
                    db.AddInParameter(cmd, "@ProductName", DbType.String, objRetailOutwardTrans.Product.ProductName);
                    db.AddInParameter(cmd, "@FK_TaxID", DbType.String, objRetailOutwardTrans.Tax.TaxID);
                    db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objRetailOutwardTrans.Tax.TaxPercentage);
                    db.AddInParameter(cmd, "@IGSTPercent", DbType.Decimal, objRetailOutwardTrans.Tax.IGSTPercent);
                    db.AddInParameter(cmd, "@SGSTPercent", DbType.Decimal, objRetailOutwardTrans.Tax.SGSTPercent);
                    db.AddInParameter(cmd, "@CGSTPercent", DbType.Decimal, objRetailOutwardTrans.Tax.CGSTPercent);
                    db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objRetailOutwardTrans.CGSTAmount);
                    db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objRetailOutwardTrans.SGSTAmount);
                    db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objRetailOutwardTrans.IGSTAmount);
                    db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objRetailOutwardTrans.TaxAmount);
                    db.AddInParameter(cmd, "@NewProductFlag", DbType.Boolean, objRetailOutwardTrans.NewProductFlag);

                    iID = db.ExecuteNonQuery(cmd);
                    if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_RetailOutwardTransID"));
                }
                catch (Exception ex)
                {
                    sException = "RetailOutwardTrans AddRetailOutwardTrans | " + ex.ToString();
                    Log.Write(sException);
                    throw;
                }
                return iID;
            }
            public static bool UpdateRetailOutwardTrans(Entity.Billing.RetailOutwardTrans objRetailOutwardTrans)
            {
                string sException = string.Empty;
                int iID = 0;
                bool bResult = false;
                Database db;
                try
                {
                    db = Entity.DBConnection.dbCon;
                    DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_RETAILOUTWARDTRANS);
                    db.AddInParameter(cmd, "@PK_RetailOutwardTransID", DbType.Int32, objRetailOutwardTrans.RetailOutwardTransID);
                    db.AddInParameter(cmd, "@FK_RetailOutwardID", DbType.Int32, objRetailOutwardTrans.RetailOutwardID);
                    db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objRetailOutwardTrans.Product.ProductID);
                    db.AddInParameter(cmd, "@SMSCode", DbType.String, objRetailOutwardTrans.Product.SMSCode);
                    db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objRetailOutwardTrans.Quantity);
                    db.AddInParameter(cmd, "@Rate", DbType.Decimal, objRetailOutwardTrans.Rate);
                    db.AddInParameter(cmd, "@DiscountPercentage", DbType.Decimal, objRetailOutwardTrans.DiscountPercentage);
                    db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objRetailOutwardTrans.DiscountAmount);
                    db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objRetailOutwardTrans.SubTotal);
                    db.AddInParameter(cmd, "@Barcode", DbType.String, objRetailOutwardTrans.Barcode);
                    db.AddInParameter(cmd, "@ProductName", DbType.String, objRetailOutwardTrans.Product.ProductName);
                    db.AddInParameter(cmd, "@FK_TaxID", DbType.String, objRetailOutwardTrans.Tax.TaxID);
                    db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objRetailOutwardTrans.Tax.TaxPercentage);
                    db.AddInParameter(cmd, "@IGSTPercent", DbType.Decimal, objRetailOutwardTrans.Tax.IGSTPercent);
                    db.AddInParameter(cmd, "@SGSTPercent", DbType.Decimal, objRetailOutwardTrans.Tax.SGSTPercent);
                    db.AddInParameter(cmd, "@CGSTPercent", DbType.Decimal, objRetailOutwardTrans.Tax.CGSTPercent);
                    db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objRetailOutwardTrans.CGSTAmount);
                    db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objRetailOutwardTrans.SGSTAmount);
                    db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objRetailOutwardTrans.IGSTAmount);
                    db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objRetailOutwardTrans.TaxAmount);
                    db.AddInParameter(cmd, "@NewProductFlag", DbType.Boolean, objRetailOutwardTrans.NewProductFlag);
                    iID = db.ExecuteNonQuery(cmd);
                    if (iID != 0) bResult = true;
                }
                catch (Exception ex)
                {
                    sException = "RetailOutwardTrans UpdateRetailOutwardTrans| " + ex.ToString();
                    Log.Write(sException);
                    throw;
                }
                return bResult;
            }
            public static bool DeleteRetailOutwardTrans(int iRetailOutwardTransID)
            {
                string sException = string.Empty;
                int iRemoveId = 0;
                bool bResult = false;
                Database db;
                try
                {
                    db = Entity.DBConnection.dbCon;
                    DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_RETAILOUTWARDTRANS);
                    db.AddInParameter(cmd, "@PK_RetailOutwardTransID", DbType.Int32, iRetailOutwardTransID);
                    iRemoveId = db.ExecuteNonQuery(cmd);
                    if (iRemoveId != 0)
                        bResult = true;
                }
                catch (Exception ex)
                {
                    sException = "RetailOutwardTrans DeleteRetailOutwardTrans | " + ex.ToString();
                    Log.Write(sException);
                    throw;
                }
                return bResult;
            }
        }
    }
}

    

      

