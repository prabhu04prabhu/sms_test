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
    public class 
        Outward
    {
        public static Collection<Entity.Billing.Outward> GetOutward(int ipatientID = 0, int IsRetail = 0, int IsYarnBill = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Outward> objList = new Collection<Entity.Billing.Outward>();
            Entity.Billing.Outward objOutward;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_OUTWARD);
                db.AddInParameter(cmd, "@PK_OutwardID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, IsYarnBill);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objOutward = new Entity.Billing.Outward();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objOutward.OutwardID = Convert.ToInt32(drData["PK_OutwardID"]);
                        objOutward.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objOutward.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objOutward.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objOutward.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objOutward.Agent = objAgent;

                        objOutward.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objOutward.Address = Convert.ToString(drData["Address"]);
                        objOutward.Area = Convert.ToString(drData["Area"]);
                        objOutward.CustomerName = Convert.ToString(drData["CustomerName"]);

                        objOutward.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objOutward.sLRDate = objOutward.LRDate.ToString("dd/MM/yyyy");
                        objOutward.TransportName = Convert.ToString(drData["TransportName"]);
                        objOutward.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objOutward.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objOutward.LRNo = Convert.ToString(drData["LRNo"]);
                        objOutward.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objOutward.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objOutward.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objOutward.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objOutward.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objOutward.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objOutward.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objOutward.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objOutward.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objOutward.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objOutward.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objOutward.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objOutward.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objOutward.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objOutward.Status = Convert.ToString(drData["Status"]);
                        objOutward.Narration = Convert.ToString(drData["Narration"]);
                        objOutward.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objOutward.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objOutward.ACKno = Convert.ToString(drData["ACKno"]);
                        objOutward.IRNno = Convert.ToString(drData["IRNno"]);
                        objOutward.Notes = Convert.ToString(drData["Notes"]);
                        objOutward.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objOutward.sDeliveryDate = objOutward.DeliveryDate.ToString("dd/MM/yyyy");
                        objOutward.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objOutward.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objOutward.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objOutward.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objOutward.Bank = objBank;

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objOutward.SalesOrder = objSalesOrder;

                        objOutward.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objOutward.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objOutward.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objOutward.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objOutward.Transport = objTransport;

                        objOutward.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objOutward.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objOutward.sInvoiceDate = objOutward.InvoiceDate.ToString("dd/MM/yyyy") + " " + objOutward.CreatedOn.ToString("h:mm");
                        objOutward.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objOutward.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);

                        objList.Add(objOutward);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Outward GetOutward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }



        public static Collection<Entity.Billing.Outward> GetTopOutward(int ipatientID = 0, int IsRetail = 0, int IsYarnBill = 0, int iCustomerID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Outward> objList = new Collection<Entity.Billing.Outward>();
            Entity.Billing.Outward objOutward;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPOUTWARD);
                db.AddInParameter(cmd, "@PK_OutwardID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, IsYarnBill);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iCustomerID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objOutward = new Entity.Billing.Outward();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objOutward.OutwardID = Convert.ToInt32(drData["PK_OutwardID"]);
                        objOutward.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objOutward.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Area = Convert.ToString(drData["area"]);
                        objCustomer.MinDueDays = Convert.ToInt32(drData["MinDueDays"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objOutward.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objOutward.Tax = objTax;

                        objOutward.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objOutward.Address = Convert.ToString(drData["Address"]);
                        objOutward.Area = Convert.ToString(drData["Area"]);
                        objOutward.CustomerName = Convert.ToString(drData["CustomerName"]);

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objOutward.Agent = objAgent;

                        objOutward.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objOutward.DueDays = Convert.ToInt32(drData["DueDays"]);
                        objOutward.sLRDate = objOutward.LRDate.ToString("dd/MM/yyyy");
                        objOutward.TransportName = Convert.ToString(drData["TransportName"]);
                        objOutward.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objOutward.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objOutward.LRNo = Convert.ToString(drData["LRNo"]);
                        objOutward.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objOutward.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objOutward.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objOutward.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objOutward.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objOutward.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objOutward.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objOutward.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objOutward.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objOutward.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objOutward.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objOutward.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        //objOutward.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objOutward.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objOutward.Status = Convert.ToString(drData["Status"]);
                        objOutward.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objOutward.Narration = Convert.ToString(drData["Narration"]);
                        objOutward.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objOutward.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objOutward.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                       // objOutward.RetailsPaymentMode = Convert.ToString(drData["RetailsPaymentMode"]);
                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objOutward.Bank = objBank;

                        objOutward.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objOutward.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objOutward.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objOutward.Comments = Convert.ToString(drData["Comments"]);
                        objOutward.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objOutward.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                      //  objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                       // objOutward.SalesOrder = objSalesOrder;

                        objOutward.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objOutward.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objOutward.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objOutward.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objOutward.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objOutward.sCreatedOn = objOutward.CreatedOn.ToString("dd/MM/yyyy h:mm");
                        objOutward.sInvoiceDate = objOutward.InvoiceDate.ToString("dd/MM/yyyy");
                        objOutward.sWholeSalesInvoiceDate = objOutward.InvoiceDate.ToString("dd/MM/yyyy") + " " + objOutward.CreatedOn.ToString("h:mm");
                        objOutward.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objOutward.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);
                        objOutward.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objOutward.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objOutward.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objOutward.Transport = objTransport;
                        objOutward.ACKno = Convert.ToString(drData["ACKno"]);
                        objOutward.IRNno = Convert.ToString(drData["IRNno"]);
                        objList.Add(objOutward);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Outward GetOutward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Outward> SearchOutward(string ID, int IsRetail = 0, int IsYarnBill = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Outward> objList = new Collection<Entity.Billing.Outward>();
            Entity.Billing.Outward objOutward; Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_OUTWARDRDYNAMIC);
                db.AddInParameter(cmd, "@PK_OutwardID", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, IsYarnBill);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objOutward = new Entity.Billing.Outward();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();
                        objBank = new Entity.Ledger();
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();
                        objAgent = new Entity.Billing.Agent();

                        objOutward.OutwardID = Convert.ToInt32(drData["PK_OutwardID"]);
                        objOutward.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objOutward.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Area = Convert.ToString(drData["area"]);
                        objCustomer.MinDueDays = Convert.ToInt32(drData["MinDueDays"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objOutward.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objOutward.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objOutward.Agent = objAgent;


                        objOutward.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objOutward.Address = Convert.ToString(drData["Address"]);
                        objOutward.Area = Convert.ToString(drData["Area"]);
                        objOutward.CustomerName = Convert.ToString(drData["CustomerName"]);

                        objOutward.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        objOutward.DueDays = Convert.ToInt32(drData["DueDays"]);
                        objOutward.sLRDate = objOutward.LRDate.ToString("dd/MM/yyyy");
                        objOutward.TransportName = Convert.ToString(drData["TransportName"]);
                        objOutward.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objOutward.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objOutward.LRNo = Convert.ToString(drData["LRNo"]);
                        objOutward.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objOutward.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objOutward.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objOutward.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objOutward.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objOutward.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objOutward.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objOutward.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objOutward.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objOutward.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objOutward.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objOutward.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        //objOutward.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objOutward.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objOutward.Status = Convert.ToString(drData["Status"]);
                        objOutward.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objOutward.Narration = Convert.ToString(drData["Narration"]);
                        objOutward.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objOutward.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objOutward.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        // objOutward.RetailsPaymentMode = Convert.ToString(drData["RetailsPaymentMode"]);
                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objOutward.Bank = objBank;

                        objOutward.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objOutward.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objOutward.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objOutward.Comments = Convert.ToString(drData["Comments"]);
                        objOutward.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objOutward.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        //  objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        // objOutward.SalesOrder = objSalesOrder;

                        objOutward.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objOutward.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objOutward.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objOutward.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objOutward.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objOutward.sCreatedOn = objOutward.CreatedOn.ToString("dd/MM/yyyy h:mm");
                        objOutward.sInvoiceDate = objOutward.InvoiceDate.ToString("dd/MM/yyyy");
                        objOutward.sWholeSalesInvoiceDate = objOutward.InvoiceDate.ToString("dd/MM/yyyy") + " " + objOutward.CreatedOn.ToString("h:mm");
                        objOutward.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objOutward.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);
                        objOutward.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objOutward.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objOutward.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objOutward.Transport = objTransport;
                        objOutward.ACKno = Convert.ToString(drData["ACKno"]);
                        objOutward.IRNno = Convert.ToString(drData["IRNno"]);

                        objList.Add(objOutward);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Outward GetOutward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Entity.Billing.Outward GetOutwardByID(int iOutwardID, int IsRetail, int IsYarnBill = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Outward objOutward = new Entity.Billing.Outward();
            Entity.Customer objCustomer; Entity.Billing.Tax objTax; Entity.Gift objGift; Entity.State objState;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder; Entity.CustomerTypes objCustomerTypes;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_OUTWARD);
                db.AddInParameter(cmd, "@PK_OutwardID", DbType.Int32, iOutwardID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, IsYarnBill);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objOutward = new Entity.Billing.Outward();
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

                        objOutward.OutwardID = Convert.ToInt32(drData["PK_OutwardID"]);
                        objOutward.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objOutward.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objOutward.sInvoiceDate = objOutward.InvoiceDate.ToString("dd/MM/yyyy");
                        objOutward.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        string Date = objOutward.LRDate.ToString("dd/MM/yyyy");
                        objOutward.sLRDate = objOutward.LRDate.ToString("dd/MM/yyyy");
                        objOutward.TransportName = Convert.ToString(drData["TransportName"]);
                        objOutward.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objOutward.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objOutward.LRNo = Convert.ToString(drData["LRNo"]);

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objOutward.Customer = objCustomer;

                        objOutward.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objOutward.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objOutward.Address = Convert.ToString(drData["Address"]);
                        objOutward.Area = Convert.ToString(drData["Area"]);

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objOutward.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objOutward.Agent = objAgent;

                        objCustomerTypes.CustomertypeID = Convert.ToInt32(drData["FK_CustomertypeID"]);
                        objOutward.CustomerType = objCustomerTypes;

                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objOutward.State = objState;

                        objGift.GiftID = Convert.ToInt32(drData["FK_GiftID"]);
                        objOutward.Gift = objGift;
                        objOutward.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objOutward.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objOutward.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objOutward.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objOutward.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objOutward.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objOutward.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objOutward.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objOutward.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objOutward.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objOutward.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objOutward.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objOutward.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objOutward.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objOutward.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objOutward.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objOutward.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objOutward.Comments = Convert.ToString(drData["Comments"]);
                        objOutward.Status = Convert.ToString(drData["Status"]);

                        objOutward.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objOutward.Narration = Convert.ToString(drData["Narration"]);
                        objOutward.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objOutward.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objOutward.Notes = Convert.ToString(drData["Notes"]);
                        objOutward.CardCharges = Convert.ToDecimal(drData["CardCharges"]);
                        objOutward.CardNo = Convert.ToString(drData["CardNo"]);
                        objOutward.TenderAmount = Convert.ToDecimal(drData["TenderAmount"]);
                        objOutward.BalanceGiven = Convert.ToDecimal(drData["BalanceGiven"]);
                        objOutward.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objOutward.sDeliveryDate = objOutward.DeliveryDate.ToString("dd/MM/yyyy");
                        objOutward.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objOutward.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);


                        objOutward.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objOutward.Address = Convert.ToString(drData["Address"]);
                        objOutward.Area = Convert.ToString(drData["Area"]);
                        objOutward.CustomerName = Convert.ToString(drData["CustomerName"]);

                        objOutward.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objOutward.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objOutward.Bank = objBank;

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objOutward.SalesOrder = objSalesOrder;

                        objOutward.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objOutward.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objOutward.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objOutward.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objOutward.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objOutward.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objOutward.ShippingAddress = objShippingAddress;

                        objOutward.ACKno = Convert.ToString(drData["ACKno"]);
                        objOutward.IRNno = Convert.ToString(drData["IRNno"]);

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objOutward.Transport = objTransport;
                        objOutward.OutwardTrans = OutwardTrans.GetOutwardTransByOutwardID(objOutward.OutwardID);

                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Outward GetOutwardByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objOutward;
        }
        public static Entity.Billing.Outward GetLastOutwardByID(int iOutwardID, int IsRetail, int IsYarnBill = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Outward objOutward = new Entity.Billing.Outward();
            Entity.Customer objCustomer; Entity.Billing.Tax objTax; Entity.Gift objGift; Entity.State objState;
            Entity.Ledger objBank; Entity.Billing.SalesOrder objSalesOrder; Entity.CustomerTypes objCustomerTypes;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;
            Entity.Billing.Agent objAgent;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_OUTWARDLASTBILL);
                db.AddInParameter(cmd, "@PK_OutwardID", DbType.Int32, iOutwardID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, IsRetail);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, IsYarnBill);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objOutward = new Entity.Billing.Outward();
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

                        objOutward.OutwardID = Convert.ToInt32(drData["PK_OutwardID"]);
                        objOutward.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objOutward.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objOutward.sInvoiceDate = objOutward.InvoiceDate.ToString("dd/MM/yyyy");
                        objOutward.LRDate = Convert.ToDateTime(drData["LRDate"]);
                        string Date = objOutward.LRDate.ToString("dd/MM/yyyy");
                        objOutward.sLRDate = objOutward.LRDate.ToString("dd/MM/yyyy");
                        objOutward.TransportName = Convert.ToString(drData["TransportName"]);
                        objOutward.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objOutward.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objOutward.LRNo = Convert.ToString(drData["LRNo"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objOutward.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objOutward.Tax = objTax;

                        objAgent.AgentID = Convert.ToInt32(drData["FK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objOutward.Agent = objAgent;

                        objCustomerTypes.CustomertypeID = Convert.ToInt32(drData["FK_CustomertypeID"]);
                        objOutward.CustomerType = objCustomerTypes;

                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objOutward.State = objState;

                        objGift.GiftID = Convert.ToInt32(drData["FK_GiftID"]);
                        objOutward.Gift = objGift;
                        objOutward.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objOutward.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objOutward.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objOutward.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objOutward.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objOutward.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objOutward.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objOutward.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objOutward.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objOutward.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objOutward.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objOutward.AdditionalDiscount = Convert.ToDecimal(drData["AdditionalDiscount"]);
                        objOutward.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objOutward.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objOutward.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objOutward.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objOutward.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objOutward.Status = Convert.ToString(drData["Status"]);

                        objOutward.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objOutward.Address = Convert.ToString(drData["Address"]);
                        objOutward.Area = Convert.ToString(drData["Area"]);
                        objOutward.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objOutward.BillStatus = Convert.ToString(drData["BillStatus"]);
                        objOutward.Narration = Convert.ToString(drData["Narration"]);
                        objOutward.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objOutward.NoofBags = Convert.ToString(drData["NoofBags"]);
                        objOutward.ACKno = Convert.ToString(drData["ACKno"]);
                        objOutward.IRNno = Convert.ToString(drData["IRNno"]);
                        objOutward.Notes = Convert.ToString(drData["Notes"]);
                        objOutward.CardCharges = Convert.ToDecimal(drData["CardCharges"]);
                        objOutward.CardNo = Convert.ToString(drData["CardNo"]);
                        objOutward.TenderAmount = Convert.ToDecimal(drData["TenderAmount"]);
                        objOutward.BalanceGiven = Convert.ToDecimal(drData["BalanceGiven"]);
                        objOutward.DeliveryDate = Convert.ToDateTime(drData["DeliveryDate"]);
                        objOutward.sDeliveryDate = objOutward.DeliveryDate.ToString("dd/MM/yyyy");
                        objOutward.TransportCharges = Convert.ToDecimal(drData["TransportCharges"]);
                        objOutward.OtherCharges = Convert.ToDecimal(drData["OtherCharges"]);

                        objOutward.TCSPercent = Convert.ToDecimal(drData["TCSPercent"]);
                        objOutward.TCSAmount = Convert.ToDecimal(drData["TCSAmount"]);

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objOutward.Bank = objBank;

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);
                        objOutward.SalesOrder = objSalesOrder;

                        objOutward.SalesPoints = Convert.ToDecimal(drData["SalesPoints"]);
                        objOutward.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objOutward.UsedPoints = Convert.ToDecimal(drData["UsedPoints"]);

                        objOutward.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objOutward.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objOutward.IsYarnBill = Convert.ToBoolean(drData["IsYarnBill"]);

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranch"]);
                        objOutward.ShippingAddress = objShippingAddress;

                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objOutward.Transport = objTransport;
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Outward GetOutwardByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objOutward;
        }
        public static int AddOutward(Entity.Billing.Outward objOutward)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_OUTWARD);
                db.AddOutParameter(cmd, "@PK_OutwardID", DbType.Int32, objOutward.OutwardID);
                db.AddInParameter(cmd, "@InvoiceNo", DbType.String, objOutward.InvoiceNo);
                db.AddInParameter(cmd, "@InvoiceDate", DbType.String, objOutward.sInvoiceDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objOutward.Customer.CustomerID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objOutward.Tax.TaxID);
                db.AddInParameter(cmd, "@FK_AgentID", DbType.Int32, objOutward.Agent.AgentID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objOutward.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objOutward.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objOutward.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objOutward.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objOutward.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objOutward.TotalAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objOutward.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objOutward.DiscountAmount);
                db.AddInParameter(cmd, "@AdditionalDiscount", DbType.Decimal, objOutward.AdditionalDiscount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objOutward.Roundoff);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objOutward.NetAmount);
                db.AddInParameter(cmd, "@PaidAmount", DbType.Decimal, objOutward.PaidAmount);
                db.AddInParameter(cmd, "@BalanceAmount", DbType.Decimal, objOutward.BalanceAmount);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objOutward.FinancialYear.FinancialYearID);
                db.AddInParameter(cmd, "@CancelReason", DbType.String, objOutward.CancelReason);
                db.AddInParameter(cmd, "@IsActive", DbType.Boolean, objOutward.IsActive);
                db.AddInParameter(cmd, "@PaymentMode", DbType.String, objOutward.PaymentMode);
                db.AddInParameter(cmd, "@Narration", DbType.String, objOutward.Narration);
                db.AddInParameter(cmd, "@Comments", DbType.String, objOutward.Comments);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objOutward.Bank.LedgerID);
                db.AddInParameter(cmd, "@Status", DbType.String, objOutward.Status);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, objOutward.IsRetailBill);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, objOutward.IsYarnBill);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objOutward.CreatedBy.UserID);
                db.AddInParameter(cmd, "@TransportName", DbType.String, objOutward.TransportName);
                db.AddInParameter(cmd, "@LRNo", DbType.String, objOutward.LRNo);
                db.AddInParameter(cmd, "@EWayNo", DbType.String, objOutward.EWayNo);
                db.AddInParameter(cmd, "@VehicleNo", DbType.String, objOutward.VehicleNo);
                db.AddInParameter(cmd, "@CardNo", DbType.String, objOutward.CardNo);
                db.AddInParameter(cmd, "@BalanceGiven", DbType.Decimal, objOutward.BalanceGiven);
                db.AddInParameter(cmd, "@TenderAmount", DbType.Decimal, objOutward.TenderAmount);
                db.AddInParameter(cmd, "@CardCharges", DbType.Decimal, objOutward.CardCharges);
                db.AddInParameter(cmd, "@SalesPoints", DbType.Decimal, objOutward.SalesPoints);
                db.AddInParameter(cmd, "@UsedPoints", DbType.Decimal, objOutward.UsedPoints);
                db.AddInParameter(cmd, "@ExchangeAmount", DbType.Decimal, objOutward.ExchangeAmount);
                db.AddInParameter(cmd, "@LRDate", DbType.String, objOutward.sLRDate);
                db.AddInParameter(cmd, "@FK_GiftID", DbType.Int32, objOutward.Gift.GiftID);
                db.AddInParameter(cmd, "@CustomerType", DbType.String, objOutward.CustomerType.CustomerTypeName);
                db.AddInParameter(cmd, "@FK_CustomerTypeID", DbType.Int32, objOutward.CustomerType.CustomertypeID);
                db.AddInParameter(cmd, "@FK_StateID", DbType.Int32, objOutward.State.StateID);
                db.AddInParameter(cmd, "@TCSPercent", DbType.Decimal, objOutward.TCSPercent);
                db.AddInParameter(cmd, "@TCSAmount", DbType.Decimal, objOutward.TCSAmount);
                db.AddInParameter(cmd, "@FK_ShippingAddressID", DbType.Int32, objOutward.ShippingAddress.ShippingAddressID);
                db.AddInParameter(cmd, "@FK_TransportID", DbType.Int32, objOutward.Transport.TransportID);
                db.AddInParameter(cmd, "@NoofBags", DbType.String, objOutward.NoofBags);
                db.AddInParameter(cmd, "@Notes", DbType.String, objOutward.Notes);
                db.AddInParameter(cmd, "@ImagePath1", DbType.String, objOutward.ImagePath1);
                db.AddInParameter(cmd, "@ImagePath2", DbType.String, objOutward.ImagePath2);
                db.AddInParameter(cmd, "@ImagePath3", DbType.String, objOutward.ImagePath3);
                db.AddInParameter(cmd, "@BillStatus", DbType.String, objOutward.BillStatus);
                db.AddInParameter(cmd, "@DeliveryDate", DbType.String, objOutward.sDeliveryDate);
                db.AddInParameter(cmd, "@TransportCharges", DbType.Decimal, objOutward.TransportCharges);
                db.AddInParameter(cmd, "@OtherCharges", DbType.Decimal, objOutward.OtherCharges);
                db.AddInParameter(cmd, "@ACKno", DbType.String, objOutward.ACKno);
                db.AddInParameter(cmd, "@IRNno", DbType.String, objOutward.IRNno);
                db.AddInParameter(cmd, "@MobileNo", DbType.String, objOutward.MobileNo);
                db.AddInParameter(cmd, "@CustomerName", DbType.String, objOutward.CustomerName);
                db.AddInParameter(cmd, "@Address", DbType.String, objOutward.Address);
                db.AddInParameter(cmd, "@Area", DbType.String, objOutward.Area);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_OutwardID"));

                foreach (Entity.Billing.OutwardTrans ObjOutwardTrans in objOutward.OutwardTrans)
                    ObjOutwardTrans.OutwardID = iID;
                   OutwardTrans.SaveOutwardTransaction(objOutward.OutwardTrans);
            }
            catch (Exception ex)
            {
                sException = "Outward AddOutward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateOutward(Entity.Billing.Outward objOutward)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_OUTWARD);
                db.AddInParameter(cmd, "@PK_OutwardID", DbType.Int32, objOutward.OutwardID);
                db.AddInParameter(cmd, "@InvoiceNo", DbType.String, objOutward.InvoiceNo);
                db.AddInParameter(cmd, "@InvoiceDate", DbType.String, objOutward.sInvoiceDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objOutward.Customer.CustomerID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objOutward.Tax.TaxID);
                db.AddInParameter(cmd, "@FK_AgentID", DbType.Int32, objOutward.Agent.AgentID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objOutward.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objOutward.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objOutward.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objOutward.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objOutward.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objOutward.TotalAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objOutward.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objOutward.DiscountAmount);
                db.AddInParameter(cmd, "@AdditionalDiscount", DbType.Decimal, objOutward.AdditionalDiscount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objOutward.Roundoff);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objOutward.NetAmount);
                db.AddInParameter(cmd, "@PaidAmount", DbType.Decimal, objOutward.PaidAmount);
                db.AddInParameter(cmd, "@BalanceAmount", DbType.Decimal, objOutward.BalanceAmount);
               // db.AddInParameter(cmd, "@FK_SalesOrderID", DbType.Int32, objOutward.SalesOrder.SalesOrderID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objOutward.FinancialYear.FinancialYearID);
                db.AddInParameter(cmd, "@CancelReason", DbType.String, objOutward.CancelReason);
                db.AddInParameter(cmd, "@IsActive", DbType.Boolean, objOutward.IsActive);
                db.AddInParameter(cmd, "@PaymentMode", DbType.String, objOutward.PaymentMode);
                db.AddInParameter(cmd, "@Narration", DbType.String, objOutward.Narration);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objOutward.Bank.LedgerID);
                db.AddInParameter(cmd, "@Status", DbType.String, objOutward.Status);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objOutward.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@TransportName", DbType.String, objOutward.TransportName);
                db.AddInParameter(cmd, "@LRNo", DbType.String, objOutward.LRNo);
                db.AddInParameter(cmd, "@EWayNo", DbType.String, objOutward.EWayNo);
                db.AddInParameter(cmd, "@VehicleNo", DbType.String, objOutward.VehicleNo);
                db.AddInParameter(cmd, "@CardNo", DbType.String, objOutward.CardNo);
                db.AddInParameter(cmd, "@BalanceGiven", DbType.Decimal, objOutward.BalanceGiven);
                db.AddInParameter(cmd, "@TenderAmount", DbType.Decimal, objOutward.TenderAmount);
                db.AddInParameter(cmd, "@CardCharges", DbType.Decimal, objOutward.CardCharges);
                db.AddInParameter(cmd, "@SalesPoints", DbType.Decimal, objOutward.SalesPoints);
                db.AddInParameter(cmd, "@UsedPoints", DbType.Decimal, objOutward.UsedPoints);
                db.AddInParameter(cmd, "@ExchangeAmount", DbType.Decimal, objOutward.ExchangeAmount);
                db.AddInParameter(cmd, "@LRDate", DbType.String, objOutward.sLRDate);
                db.AddInParameter(cmd, "@FK_GiftID", DbType.Int32, objOutward.Gift.GiftID);
                db.AddInParameter(cmd, "@CustomerType", DbType.String, objOutward.CustomerType.CustomerTypeName);
                db.AddInParameter(cmd, "@FK_CustomerTypeID", DbType.Int32, objOutward.CustomerType.CustomertypeID);
                db.AddInParameter(cmd, "@FK_StateID", DbType.Int32, objOutward.State.StateID);
                db.AddInParameter(cmd, "@TCSPercent", DbType.Decimal, objOutward.TCSPercent);
                db.AddInParameter(cmd, "@TCSAmount", DbType.Decimal, objOutward.TCSAmount);
                db.AddInParameter(cmd, "@FK_ShippingAddressID", DbType.Int32, objOutward.ShippingAddress.ShippingAddressID);
                db.AddInParameter(cmd, "@FK_TransportID", DbType.Int32, objOutward.Transport.TransportID);
                db.AddInParameter(cmd, "@NoofBags", DbType.String, objOutward.NoofBags);
                db.AddInParameter(cmd, "@Notes", DbType.String, objOutward.Notes);
                db.AddInParameter(cmd, "@ImagePath1", DbType.String, objOutward.ImagePath1);
                db.AddInParameter(cmd, "@ImagePath2", DbType.String, objOutward.ImagePath2);
                db.AddInParameter(cmd, "@ImagePath3", DbType.String, objOutward.ImagePath3);
                db.AddInParameter(cmd, "@BillStatus", DbType.String, objOutward.BillStatus);
                db.AddInParameter(cmd, "@Comments", DbType.String, objOutward.Comments);
                db.AddInParameter(cmd, "@DeliveryDate", DbType.String, objOutward.sDeliveryDate);
                db.AddInParameter(cmd, "@TransportCharges", DbType.Decimal, objOutward.TransportCharges);
                db.AddInParameter(cmd, "@OtherCharges", DbType.Decimal, objOutward.OtherCharges);
                db.AddInParameter(cmd, "@IsYarnBill", DbType.Boolean, objOutward.IsYarnBill);
                db.AddInParameter(cmd, "@ACKno", DbType.String, objOutward.ACKno);
                db.AddInParameter(cmd, "@IRNno", DbType.String, objOutward.IRNno);
                db.AddInParameter(cmd, "@MobileNo", DbType.String, objOutward.MobileNo);
                db.AddInParameter(cmd, "@CustomerName", DbType.String, objOutward.CustomerName);
                db.AddInParameter(cmd, "@Address", DbType.String, objOutward.Address);
                db.AddInParameter(cmd, "@Area", DbType.String, objOutward.Area);

                foreach (Entity.Billing.OutwardTrans ObjOutwardTrans in objOutward.OutwardTrans)
                    ObjOutwardTrans.OutwardID = objOutward.OutwardID;

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
                  OutwardTrans.SaveOutwardTransaction(objOutward.OutwardTrans);

            }
            catch (Exception ex)
            {
                sException = "Outward UpdateOutward| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteOutward(int iOutwardId, string Reason, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_OUTWARD);
                db.AddInParameter(cmd, "@PK_OutwardID", DbType.Int32, iOutwardId);
                db.AddInParameter(cmd, "@Reason", DbType.String, Reason);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, iUserId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Outward DeleteOutward | " + ex.ToString();
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
                sException = "Outward DeleteOutward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return stockcnt;
        }

        public static Collection<Entity.Billing.OutwardTransDetails> GetProductDetailsBySMSCode(int iID = 0, int iValue = 0, int iSupplierID = 0, string iSMSCode = null)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.OutwardTransDetails> objList = new Collection<Entity.Billing.OutwardTransDetails>();
            Entity.Billing.OutwardTransDetails objOutwardTrans = new Entity.Billing.OutwardTransDetails();
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
                        objOutwardTrans = new Entity.Billing.OutwardTransDetails();
                        objProduct = new Entity.Master.Product();

                        objOutwardTrans.OutwardTransID = Convert.ToInt32(drData["PK_OutwardTransID"]);
                        objOutwardTrans.OutwardID = Convert.ToInt32(drData["FK_OutwardID"]);
                        objOutwardTrans.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objOutwardTrans.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objOutwardTrans.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objOutwardTrans.sInvoiceDate = objOutwardTrans.InvoiceDate.ToString("dd/MM/yyyy");

                        objOutwardTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objOutwardTrans.Rate = Convert.ToDecimal(drData["Rate"]);
                        objOutwardTrans.SubTotal = Convert.ToDecimal(drData["SubTotal"]);
                        objOutwardTrans.DiscountPercentage = Convert.ToDecimal(drData["DiscountPercentage"]);
                        objOutwardTrans.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objOutwardTrans.Product = objProduct;
                        objOutwardTrans.Barcode = Convert.ToString(drData["Barcode"]);
                        objList.Add(objOutwardTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "OutwardTrans GetOutwardTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }


        public class OutwardTrans
        {
            public static Collection<Entity.Billing.OutwardTrans> GetOutwardTransByOutwardID(int iOutwardID = 0)
            {
                string sException = string.Empty;
                Database db;
                DataSet dsList = null;
                Collection<Entity.Billing.OutwardTrans> objList = new Collection<Entity.Billing.OutwardTrans>();
                Entity.Billing.OutwardTrans objOutwardTrans = new Entity.Billing.OutwardTrans();
                Entity.Master.Product objProduct; Entity.Billing.Tax objTax;
                try
                {
                    db = Entity.DBConnection.dbCon;
                    DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_OUTWARDTRANS);
                    db.AddInParameter(cmd, "@FK_OutwardID", DbType.Int32, iOutwardID);
                    dsList = db.ExecuteDataSet(cmd);

                    if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drData in dsList.Tables[0].Rows)
                        {
                            objOutwardTrans = new Entity.Billing.OutwardTrans();
                            objProduct = new Entity.Master.Product();
                            objTax = new Entity.Billing.Tax();

                            objOutwardTrans.OutwardTransID = Convert.ToInt32(drData["PK_OutwardTransID"]);
                            objOutwardTrans.OutwardID = Convert.ToInt32(drData["FK_OutwardID"]);

                            objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                            objTax.TaxPercentage = Convert.ToDecimal(drData["TaxPercent"]);
                            objTax.SGSTPercent = Convert.ToDecimal(drData["SGSTPercent"]);
                            objTax.IGSTPercent = Convert.ToDecimal(drData["IGSTPercent"]);
                            objTax.CGSTPercent = Convert.ToDecimal(drData["CGSTPercent"]);
                            objOutwardTrans.Tax = objTax;

                            objOutwardTrans.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                            objOutwardTrans.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                            objOutwardTrans.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                            objOutwardTrans.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                            objOutwardTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);
                            objOutwardTrans.Rate = Convert.ToInt32(drData["Rate"]);
                            objOutwardTrans.SubTotal = Convert.ToDecimal(drData["SubTotal"]);
                            objOutwardTrans.DiscountPercentage = Convert.ToDecimal(drData["DiscountPercentage"]);
                            objOutwardTrans.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);

                            objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                            objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                            objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                            objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                            objOutwardTrans.Product = objProduct;
                            objOutwardTrans.Barcode = Convert.ToString(drData["Barcode"]);
                            objOutwardTrans.NewProductFlag = Convert.ToBoolean(drData["NewProductFlag"]);
                            objList.Add(objOutwardTrans);
                        }
                    }
                }
                catch (Exception ex)
                {
                    sException = "OutwardTrans GetOutwardTrans | " + ex.ToString();
                    Log.Write(sException);
                    throw;
                }
                return objList;
            }
            public static void SaveOutwardTransaction(Collection<Entity.Billing.OutwardTrans> ObjOutwardTransList)
            {
                int iID = 0;
                bool bResult = false;

                foreach (Entity.Billing.OutwardTrans ObjOutwardTransaction in ObjOutwardTransList)
                {
                    if (ObjOutwardTransaction.StatusFlag == "I")
                        iID = AddOutwardTrans(ObjOutwardTransaction);
                    else if (ObjOutwardTransaction.StatusFlag == "U")
                        bResult = UpdateOutwardTrans(ObjOutwardTransaction);
                    else if (ObjOutwardTransaction.StatusFlag == "D")
                        bResult = DeleteOutwardTrans(ObjOutwardTransaction.OutwardTransID);
                }
            }
            public static int AddOutwardTrans(Entity.Billing.OutwardTrans objOutwardTrans)
            {
                string sException = string.Empty;
                int iID = 0;
                Database db;
                try
                {
                    db = Entity.DBConnection.dbCon;
                    DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_OUTWARDTRANS);
                    db.AddOutParameter(cmd, "@PK_OutwardTransID", DbType.Int32, objOutwardTrans.OutwardTransID);
                    db.AddInParameter(cmd, "@FK_OutwardID", DbType.Int32, objOutwardTrans.OutwardID);
                    db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objOutwardTrans.Product.ProductID);
                    db.AddInParameter(cmd, "@SMSCode", DbType.String, objOutwardTrans.Product.SMSCode);
                    db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objOutwardTrans.Quantity);
                    db.AddInParameter(cmd, "@Rate", DbType.Decimal, objOutwardTrans.Rate);
                    db.AddInParameter(cmd, "@DiscountPercentage", DbType.Decimal, objOutwardTrans.DiscountPercentage);
                    db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objOutwardTrans.DiscountAmount);
                    db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objOutwardTrans.SubTotal);
                    db.AddInParameter(cmd, "@Barcode", DbType.String, objOutwardTrans.Barcode);
                    db.AddInParameter(cmd, "@ProductName", DbType.String, objOutwardTrans.Product.ProductName);
                    db.AddInParameter(cmd, "@FK_TaxID", DbType.String, objOutwardTrans.Tax.TaxID);
                    db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objOutwardTrans.Tax.TaxPercentage);
                    db.AddInParameter(cmd, "@IGSTPercent", DbType.Decimal, objOutwardTrans.Tax.IGSTPercent);
                    db.AddInParameter(cmd, "@SGSTPercent", DbType.Decimal, objOutwardTrans.Tax.SGSTPercent);
                    db.AddInParameter(cmd, "@CGSTPercent", DbType.Decimal, objOutwardTrans.Tax.CGSTPercent);
                    db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objOutwardTrans.CGSTAmount);
                    db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objOutwardTrans.SGSTAmount);
                    db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objOutwardTrans.IGSTAmount);
                    db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objOutwardTrans.TaxAmount);
                    db.AddInParameter(cmd, "@NewProductFlag", DbType.Boolean, objOutwardTrans.NewProductFlag);

                    iID = db.ExecuteNonQuery(cmd);
                    if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_OutwardTransID"));
                }
                catch (Exception ex)
                {
                    sException = "OutwardTrans AddOutwardTrans | " + ex.ToString();
                    Log.Write(sException);
                    throw;
                }
                return iID;
            }
            public static bool UpdateOutwardTrans(Entity.Billing.OutwardTrans objOutwardTrans)
            {
                string sException = string.Empty;
                int iID = 0;
                bool bResult = false;
                Database db;
                try
                {
                    db = Entity.DBConnection.dbCon;
                    DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_OUTWARDTRANS);
                    db.AddInParameter(cmd, "@PK_OutwardTransID", DbType.Int32, objOutwardTrans.OutwardTransID);
                    db.AddInParameter(cmd, "@FK_OutwardID", DbType.Int32, objOutwardTrans.OutwardID);
                    db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objOutwardTrans.Product.ProductID);
                    db.AddInParameter(cmd, "@SMSCode", DbType.String, objOutwardTrans.Product.SMSCode);
                    db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objOutwardTrans.Quantity);
                    db.AddInParameter(cmd, "@Rate", DbType.Decimal, objOutwardTrans.Rate);
                    db.AddInParameter(cmd, "@DiscountPercentage", DbType.Decimal, objOutwardTrans.DiscountPercentage);
                    db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objOutwardTrans.DiscountAmount);
                    db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objOutwardTrans.SubTotal);
                    db.AddInParameter(cmd, "@Barcode", DbType.String, objOutwardTrans.Barcode);
                    db.AddInParameter(cmd, "@ProductName", DbType.String, objOutwardTrans.Product.ProductName);
                    db.AddInParameter(cmd, "@FK_TaxID", DbType.String, objOutwardTrans.Tax.TaxID);
                    db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objOutwardTrans.Tax.TaxPercentage);
                    db.AddInParameter(cmd, "@IGSTPercent", DbType.Decimal, objOutwardTrans.Tax.IGSTPercent);
                    db.AddInParameter(cmd, "@SGSTPercent", DbType.Decimal, objOutwardTrans.Tax.SGSTPercent);
                    db.AddInParameter(cmd, "@CGSTPercent", DbType.Decimal, objOutwardTrans.Tax.CGSTPercent);
                    db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objOutwardTrans.CGSTAmount);
                    db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objOutwardTrans.SGSTAmount);
                    db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objOutwardTrans.IGSTAmount);
                    db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objOutwardTrans.TaxAmount);
                    db.AddInParameter(cmd, "@NewProductFlag", DbType.Boolean, objOutwardTrans.NewProductFlag);
                    iID = db.ExecuteNonQuery(cmd);
                    if (iID != 0) bResult = true;
                }
                catch (Exception ex)
                {
                    sException = "OutwardTrans UpdateOutwardTrans| " + ex.ToString();
                    Log.Write(sException);
                    throw;
                }
                return bResult;
            }
            public static bool DeleteOutwardTrans(int iOutwardTransID)
            {
                string sException = string.Empty;
                int iRemoveId = 0;
                bool bResult = false;
                Database db;
                try
                {
                    db = Entity.DBConnection.dbCon;
                    DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_OUTWARDTRANS);
                    db.AddInParameter(cmd, "@PK_OutwardTransID", DbType.Int32, iOutwardTransID);
                    iRemoveId = db.ExecuteNonQuery(cmd);
                    if (iRemoveId != 0)
                        bResult = true;
                }
                catch (Exception ex)
                {
                    sException = "OutwardTrans DeleteOutwardTrans | " + ex.ToString();
                    Log.Write(sException);
                    throw;
                }
                return bResult;
            }
        }
    }
}

    

      

