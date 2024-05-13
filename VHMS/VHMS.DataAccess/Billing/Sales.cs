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
    public class Sales
    {
        public static Collection<Entity.Billing.Sales> GetSales(int ipatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Sales> objList = new Collection<Entity.Billing.Sales>();
            Entity.Billing.Sales objSales;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;
            Entity.User objEmployee; 

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALES);
                db.AddInParameter(cmd, "@PK_SalesID", DbType.Int32, ipatientID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSales = new Entity.Billing.Sales();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();
                        objEmployee = new Entity.User();
                        

                        objSales.SalesID = Convert.ToInt32(drData["PK_SalesID"]);
                        objSales.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSales.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSales.sInvoiceDate = objSales.InvoiceDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objSales.Customer = objCustomer;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objSales.Branch = objBranch;


                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objSales.Tax = objTax;
                        objSales.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSales.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSales.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSales.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSales.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSales.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSales.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSales.InvoiceAmount = Convert.ToDecimal(drData["InvoiceAmount"]);
                        objSales.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSales.Status = Convert.ToString(drData["Status"]);
                        objSales.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSales.CardNo = Convert.ToString(drData["CardNo"]);
                        objSales.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSales.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSales.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSales.CardAmount = Convert.ToDecimal(drData["CardAmount"]);
                        objSales.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSales.ReturnAmount = Convert.ToDecimal(drData["ReturnAmount"]);
                        objSales.Exchange = Convert.ToDecimal(drData["Exchange"]);
                        //objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);//

                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objSales.Employee = objEmployee;
                        objList.Add(objSales);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Sales GetSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Entity.Billing.Sales GetLastBillNo(int ipatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Entity.Billing.Sales objSales=new Entity.Billing.Sales();
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;
            Entity.User objEmployee;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_LASTBILLNO);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, ipatientID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();
                        objEmployee = new Entity.User();

                        objSales.SalesID = Convert.ToInt32(drData["PK_SalesID"]);
                        objSales.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSales.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSales.sInvoiceDate = objSales.InvoiceDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objSales.Customer = objCustomer;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objSales.Branch = objBranch;


                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objSales.Tax = objTax;
                        objSales.AuditBillNo = Convert.ToString(drData["AuditBillNo"]);
                        objSales.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSales.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSales.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSales.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSales.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSales.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSales.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSales.InvoiceAmount = Convert.ToDecimal(drData["InvoiceAmount"]);
                        objSales.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSales.Status = Convert.ToString(drData["Status"]);
                        objSales.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSales.CardNo = Convert.ToString(drData["CardNo"]);
                        objSales.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSales.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSales.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSales.CardAmount = Convert.ToDecimal(drData["CardAmount"]);
                        objSales.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSales.ReturnAmount = Convert.ToDecimal(drData["ReturnAmount"]);
                        objSales.Exchange = Convert.ToDecimal(drData["Exchange"]);
                        //objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);//

                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objSales.Employee = objEmployee;
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Sales GetSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSales;
        }

        public static Entity.Billing.Sales GetAuditBillNo(string LastBillNo,int ipatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Entity.Billing.Sales objSales = new Entity.Billing.Sales();
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;
            Entity.User objEmployee;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_AUDITBILLNO);
                db.AddInParameter(cmd, "@PK_SalesID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@LastBillNo", DbType.String, LastBillNo);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();
                        objEmployee = new Entity.User();

                        objSales.SalesID = Convert.ToInt32(drData["PK_SalesID"]);
                        objSales.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSales.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSales.sInvoiceDate = objSales.InvoiceDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objSales.Customer = objCustomer;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objSales.Branch = objBranch;


                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objSales.Tax = objTax;
                        objSales.AuditBillNo = Convert.ToString(drData["AuditBillNo"]);
                        objSales.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSales.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSales.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSales.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSales.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSales.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSales.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSales.InvoiceAmount = Convert.ToDecimal(drData["InvoiceAmount"]);
                        objSales.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSales.Status = Convert.ToString(drData["Status"]);
                        objSales.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSales.CardNo = Convert.ToString(drData["CardNo"]);
                        objSales.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSales.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSales.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSales.CardAmount = Convert.ToDecimal(drData["CardAmount"]);
                        objSales.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSales.ReturnAmount = Convert.ToDecimal(drData["ReturnAmount"]);
                        objSales.Exchange = Convert.ToDecimal(drData["Exchange"]);
                        //objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);//

                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objSales.Employee = objEmployee;
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Sales GetSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSales;
        }

        public static Collection<Entity.Billing.Sales> GetSalesID(int ipatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Sales> objList = new Collection<Entity.Billing.Sales>();
            Entity.Billing.Sales objSales;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;
            Entity.User objEmployee; 

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALES);
                db.AddInParameter(cmd, "@PK_SalesID", DbType.Int32, ipatientID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSales = new Entity.Billing.Sales();

                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();
                        objEmployee = new Entity.User();
                        

                        objSales.SalesID = Convert.ToInt32(drData["PK_SalesID"]);
                        objSales.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSales.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSales.sInvoiceDate = objSales.InvoiceDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objSales.Customer = objCustomer;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objSales.Branch = objBranch;

                      

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objSales.Tax = objTax;
                        objSales.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSales.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSales.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSales.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSales.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSales.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSales.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSales.InvoiceAmount = Convert.ToDecimal(drData["InvoiceAmount"]);
                        objSales.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSales.Status = Convert.ToString(drData["Status"]);
                        objSales.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSales.CardNo = Convert.ToString(drData["CardNo"]);
                        objSales.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSales.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSales.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSales.CardAmount = Convert.ToDecimal(drData["CardAmount"]);
                        objSales.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSales.ReturnAmount = Convert.ToDecimal(drData["ReturnAmount"]);
                        objSales.Exchange = Convert.ToDecimal(drData["Exchange"]);
                        objEmployee.UserID1 = Convert.ToInt32(drData["FK_SalesManID"]);

                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objSales.Employee = objEmployee;
                        objList.Add(objSales);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Sales GetSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Sales> SearchSales(string ID, int iBranchID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Sales> objList = new Collection<Entity.Billing.Sales>();
            Entity.Billing.Sales objSales;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;
            Entity.User objEmployee;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_SALES);
                db.AddInParameter(cmd, "@key", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, iBranchID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSales = new Entity.Billing.Sales();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();
                        objEmployee = new Entity.User();


                        objSales.SalesID = Convert.ToInt32(drData["PK_SalesID"]);
                        objSales.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSales.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSales.sInvoiceDate = objSales.InvoiceDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objSales.Customer = objCustomer;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objSales.Branch = objBranch;


                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objSales.Tax = objTax;
                        objSales.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSales.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSales.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSales.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSales.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSales.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSales.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSales.InvoiceAmount = Convert.ToDecimal(drData["InvoiceAmount"]);
                        objSales.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSales.Status = Convert.ToString(drData["Status"]);
                        objSales.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSales.CardNo = Convert.ToString(drData["CardNo"]);
                        objSales.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSales.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSales.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSales.CardAmount = Convert.ToDecimal(drData["CardAmount"]);
                        objSales.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSales.ReturnAmount = Convert.ToDecimal(drData["ReturnAmount"]);
                        objSales.Exchange = Convert.ToDecimal(drData["Exchange"]);

                        // objSales.SalesManCode = Convert.ToString(drData["SalesManCode"]);

                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objSales.Employee = objEmployee;

                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objSales.SalesMan = objEmployee;
                        objList.Add(objSales);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Sales GetSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Sales> GetTopSales(int ipatientID = 0, int iBranchID=0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Sales> objList = new Collection<Entity.Billing.Sales>();
            Entity.Billing.Sales objSales;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;
            Entity.User objEmployee;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPSALES);
                db.AddInParameter(cmd, "@PK_SalesID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, iBranchID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSales = new Entity.Billing.Sales();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();
                        objEmployee = new Entity.User();


                        objSales.SalesID = Convert.ToInt32(drData["PK_SalesID"]);
                        objSales.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSales.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSales.sInvoiceDate = objSales.InvoiceDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objSales.Customer = objCustomer;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objSales.Branch = objBranch;


                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objSales.Tax = objTax;
                        objSales.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSales.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSales.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSales.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSales.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSales.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSales.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSales.InvoiceAmount = Convert.ToDecimal(drData["InvoiceAmount"]);
                        objSales.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSales.Status = Convert.ToString(drData["Status"]);
                        objSales.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSales.CardNo = Convert.ToString(drData["CardNo"]);
                        objSales.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSales.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSales.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSales.CardAmount = Convert.ToDecimal(drData["CardAmount"]);
                        objSales.Roundoff = Convert.ToDecimal(drData["Roundoff"]);

                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objSales.Employee = objEmployee;
                        objList.Add(objSales);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Sales GetSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        //public static Collection<Entity.Billing.Sales> GetSalesReport(Entity.Billing.SalesFilter oJobCardFilter)
        //{
        //    string sException = string.Empty;
        //    Database db;
        //    DataSet dsList = null;
        //    Collection<Entity.Billing.Sales> objList = new Collection<Entity.Billing.Sales>();
        //    Entity.Billing.Sales objSales;
        //    Entity.patient objpatient; Entity.Discharge.Doctor objdoctor;

        //    try
        //    {
        //        db = VHMS.Entity.DBConnection.dbCon;
        //        DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESREPORT);
        //        db.AddInParameter(cmd, "@FK_PatientID", DbType.Int32, oJobCardFilter.PatientID);
        //        db.AddInParameter(cmd, "@DateFrom", DbType.String, oJobCardFilter.DateFrom);
        //        db.AddInParameter(cmd, "@DateTo", DbType.String, oJobCardFilter.DateTo);
        //        db.AddInParameter(cmd, "@FK_DoctorID", DbType.Int32, oJobCardFilter.DoctorID);
        //        db.AddInParameter(cmd, "@FK_DescriptionID", DbType.Int32, oJobCardFilter.DescriptionID);
        //        db.AddInParameter(cmd, "@FK_UserID", DbType.Int32, oJobCardFilter.UserID);
        //        dsList = db.ExecuteDataSet(cmd);

        //        if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow drData in dsList.Tables[0].Rows)
        //            {
        //                objSales = new Entity.Billing.Sales();
        //                objpatient = new Entity.patient();
        //                objdoctor = new Entity.Discharge.Doctor();

        //                objSales.SalesID = Convert.ToInt32(drData["PK_SalesID"]);
        //                objSales.BillNo = Convert.ToString(drData["BillNo"]);
        //              //  objSales.BillDate = Convert.ToDateTime(drData["BillDate"]);
        //                objSales.sBillDate = (Convert.ToDateTime(drData["BillDate"])).ToString("dd/MM/yyyy");

        //               // objpatient.patientID = Convert.ToInt32(drData["FK_patientID"]);
        //              //  objpatient.HName = Convert.ToString(drData["HName"]);
        //                objpatient.WName = Convert.ToString(drData["WName"]);
        //                objpatient.WMobileNo = Convert.ToString(drData["WMobileNo"]);
        //               // objpatient.OPDNo = Convert.ToString(drData["OPDNo"]);
        //                objSales.Patient = objpatient;
        //               // objdoctor.DoctorID = Convert.ToInt32(drData["FK_DoctorID"]);
        //                objdoctor.DoctorName = Convert.ToString(drData["DoctorName"]);
        //                objSales.Doctor = objdoctor;
        //               // objSales.PaymentMode = Convert.ToString(drData["PaymentMode"]);
        //                objSales.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
        //               // objSales.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
        //              //  objSales.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
        //                //objSales.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
        //                //objSales.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
        //                objSales.Subtotal = Convert.ToDecimal(drData["Subtotal"]);
        //                objSales.DescriptionName = Convert.ToString(drData["DescriptionName"]);
        //                objSales.CUserName = Convert.ToString(drData["CUserName"]);
        //                objSales.IsCancelled = Convert.ToBoolean(drData["IsCancelled"]);
        //                objSales.CancelReason = Convert.ToString(drData["CancelReason"]);
        //                objList.Add(objSales);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "Sales GetSales | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return objList;
        //}
        public static Entity.Billing.Sales GetSalesByID(int iSalesID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Sales objSales = new Entity.Billing.Sales();
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;
            Entity.User objEmployee; 

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALES);
                db.AddInParameter(cmd, "@PK_SalesID", DbType.Int32, iSalesID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSales = new Entity.Billing.Sales();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();
                        objEmployee = new Entity.User();
                       

                        objSales.SalesID = Convert.ToInt32(drData["PK_SalesID"]);
                        objSales.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSales.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSales.sInvoiceDate = objSales.InvoiceDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSales.Customer = objCustomer;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objSales.Branch = objBranch;

                       
                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objSales.Tax = objTax;
                        objSales.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSales.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSales.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSales.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSales.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSales.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSales.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSales.InvoiceAmount = Convert.ToDecimal(drData["InvoiceAmount"]);
                        objSales.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSales.Status = Convert.ToString(drData["Status"]);
                        objSales.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSales.CardNo = Convert.ToString(drData["CardNo"]);
                        objSales.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSales.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSales.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSales.CardAmount = Convert.ToDecimal(drData["CardAmount"]);
                        objSales.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSales.ReturnAmount = Convert.ToDecimal(drData["ReturnAmount"]);
                        objEmployee.UserID1 = Convert.ToInt32(drData["FK_SalesManID"]);
                        objSales.Exchange = Convert.ToDecimal(drData["Exchange"]);

                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objSales.Employee = objEmployee;
                        objSales.SalesTrans = SalesTrans.GetSalesTransBySalesID(objSales.SalesID);
                        objSales.SalesChitTrans = SalesChitTrans.GetSalesChitTransBySalesID(objSales.SalesID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Sales GetSalesByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSales;
        }

        public static Entity.Billing.Sales GetSalesByInvoice(string InvoiceNo)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Sales objSales = new Entity.Billing.Sales();
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;
            Entity.User objEmployee; 

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESINVOICE);
                db.AddInParameter(cmd, "@InvoiceNo", DbType.String, InvoiceNo);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSales = new Entity.Billing.Sales();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();
                        objEmployee = new Entity.User();
                       

                        objSales.SalesID = Convert.ToInt32(drData["PK_SalesID"]);
                        objSales.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSales.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objSales.sInvoiceDate = objSales.InvoiceDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSales.Customer = objCustomer;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objSales.Branch = objBranch;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objSales.Tax = objTax;
                        objSales.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSales.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSales.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSales.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSales.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSales.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSales.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSales.InvoiceAmount = Convert.ToDecimal(drData["InvoiceAmount"]);
                        objSales.ExchangeAmount = Convert.ToDecimal(drData["ExchangeAmount"]);
                        objSales.Status = Convert.ToString(drData["Status"]);
                        objSales.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSales.CardNo = Convert.ToString(drData["CardNo"]);
                        objSales.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objSales.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objSales.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSales.CardAmount = Convert.ToDecimal(drData["CardAmount"]);
                        objSales.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSales.ReturnAmount = Convert.ToDecimal(drData["ReturnAmount"]);
                       // objSales.Exchange = Convert.ToDecimal(drData["Exchange"]);
                        // objSales.SalesManCode = Convert.ToString(drData["SalesManCode"]);

                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objSales.Employee = objEmployee;
                        objSales.SalesTrans = SalesTrans.GetSalesTransBySalesID(objSales.SalesID);
                        objSales.SalesChitTrans = SalesChitTrans.GetSalesChitTransBySalesID(objSales.SalesID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Sales GetSalesByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSales;
        }
        public static int AddSales(Entity.Billing.Sales objSales)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_SALES);
                db.AddOutParameter(cmd, "@PK_SalesID", DbType.Int32, objSales.SalesID);
                db.AddInParameter(cmd, "@InvoiceNo", DbType.String, objSales.InvoiceNo);
                db.AddInParameter(cmd, "@InvoiceDate", DbType.String, objSales.sInvoiceDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objSales.Customer.CustomerID);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, objSales.Branch.BranchID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objSales.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objSales.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objSales.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objSales.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objSales.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objSales.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objSales.TotalAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objSales.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objSales.DiscountAmount);
                db.AddInParameter(cmd, "@InvoiceAmount", DbType.Decimal, objSales.InvoiceAmount);
                db.AddInParameter(cmd, "@ExchangeAmount", DbType.Decimal, 0);
                db.AddInParameter(cmd, "@Status", DbType.String, objSales.Status);
                db.AddInParameter(cmd, "@PaymentMode", DbType.String, objSales.PaymentMode);
                db.AddInParameter(cmd, "@CardNo", DbType.String, objSales.CardNo);
                db.AddInParameter(cmd, "@PaidAmount", DbType.Decimal, objSales.PaidAmount);
                db.AddInParameter(cmd, "@BalanceAmount", DbType.Decimal, objSales.BalanceAmount);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, objSales.Employee.UserID);
                db.AddInParameter(cmd, "@Fk_SalesManID", DbType.Int32, objSales.Employee.UserID1);
                db.AddInParameter(cmd, "@IsActive", DbType.Boolean, true);
                db.AddInParameter(cmd, "@FK_RegisterID", DbType.Int32, 0);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objSales.CreatedBy.UserID);
                db.AddInParameter(cmd, "@CardAmount", DbType.Decimal, objSales.CardAmount);
                db.AddInParameter(cmd, "@ReturnAmount", DbType.Decimal, objSales.ReturnAmount);
                db.AddInParameter(cmd, "@Exchange", DbType.Decimal, objSales.Exchange);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objSales.Roundoff);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_SalesID"));

                foreach (Entity.Billing.SalesTrans ObjSalesTrans in objSales.SalesTrans)
                    ObjSalesTrans.SalesID = iID;

                foreach (Entity.Billing.SalesChitTrans ObjSalesChitTrans in objSales.SalesChitTrans)
                    ObjSalesChitTrans.SalesID = iID;

                //foreach (Entity.Billing.ExchangeTrans ObjExchangeTrans in objSales.ExchangeTrans)
                //    ObjExchangeTrans.SalesID = iID;

                SalesTrans.SaveSalesTransaction(objSales.SalesTrans);
                SalesChitTrans.SaveSalesChitTransaction(objSales.SalesChitTrans);
               // ExchangeTrans.SaveExchangeTransaction(objSales.ExchangeTrans);
            }
            catch (Exception ex)
            {
                sException = "Sales AddSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateSales(Entity.Billing.Sales objSales)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_SALES);
                db.AddInParameter(cmd, "@PK_SalesID", DbType.Int32, objSales.SalesID);
               // db.AddInParameter(cmd, "@BillNo", DbType.String, objSales.InvoiceNo);
                db.AddInParameter(cmd, "@InvoiceDate", DbType.String, objSales.sInvoiceDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objSales.Customer.CustomerID);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, objSales.Branch.BranchID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objSales.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objSales.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objSales.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objSales.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objSales.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objSales.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objSales.TotalAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objSales.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objSales.DiscountAmount);
                db.AddInParameter(cmd, "@InvoiceAmount", DbType.Decimal, objSales.InvoiceAmount);
                db.AddInParameter(cmd, "@ExchangeAmount", DbType.Decimal, 0);
                db.AddInParameter(cmd, "@Status", DbType.String, objSales.Status);
                db.AddInParameter(cmd, "@PaymentMode", DbType.String, objSales.PaymentMode);
                db.AddInParameter(cmd, "@CardNo", DbType.String, objSales.CardNo);
                db.AddInParameter(cmd, "@PaidAmount", DbType.Decimal, objSales.PaidAmount);
                db.AddInParameter(cmd, "@BalanceAmount", DbType.Decimal, objSales.BalanceAmount);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objSales.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, objSales.Employee.UserID);
                db.AddInParameter(cmd, "@Fk_SalesManID", DbType.Int32, objSales.Employee.UserID1);
                db.AddInParameter(cmd, "@FK_RegisterID", DbType.Int32, 0);
                db.AddInParameter(cmd, "@IsActive", DbType.Boolean, true);
                db.AddInParameter(cmd, "@CardAmount", DbType.Decimal, objSales.CardAmount);
                db.AddInParameter(cmd, "@ReturnAmount", DbType.Decimal, objSales.ReturnAmount);
                db.AddInParameter(cmd, "@Exchange", DbType.Decimal, objSales.Exchange);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objSales.Roundoff);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                foreach (Entity.Billing.SalesTrans ObjSalesTrans in objSales.SalesTrans)
                    ObjSalesTrans.SalesID = objSales.SalesID;

                foreach (Entity.Billing.SalesChitTrans ObjSalesChitTrans in objSales.SalesChitTrans)
                    ObjSalesChitTrans.SalesID = objSales.SalesID;

                //foreach (Entity.Billing.ExchangeTrans ObjExchangeTrans in objSales.ExchangeTrans)
                //    ObjExchangeTrans.SalesID = objSales.SalesID;

                SalesTrans.SaveSalesTransaction(objSales.SalesTrans);
                SalesChitTrans.SaveSalesChitTransaction(objSales.SalesChitTrans);
               // ExchangeTrans.SaveExchangeTransaction(objSales.ExchangeTrans);
            }
            catch (Exception ex)
            {
                sException = "Sales UpdateSales| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteSales(int iSalesId, string Reason, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_SALES);
                db.AddInParameter(cmd, "@PK_SalesID", DbType.Int32, iSalesId);
               // db.AddInParameter(cmd, "@Reason", DbType.String, Reason);
              //  db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, iUserId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Sales DeleteSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static DataSet GetSalesSummary()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESSUMMARY);
                dsList = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "Sales GetSalesSummary | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsList;
        }
    }

    public class SalesTrans
    {
        public static Collection<Entity.Billing.SalesTrans> GetSalesTransBySalesID(int iSalesID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesTrans> objList = new Collection<Entity.Billing.SalesTrans>();
            Entity.Billing.SalesTrans objSalesTrans = new Entity.Billing.SalesTrans();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESTRANS);
                db.AddInParameter(cmd, "@FK_SalesID", DbType.Int32, iSalesID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesTrans = new Entity.Billing.SalesTrans();

                        objSalesTrans.SalesTransID = Convert.ToInt32(drData["PK_SalesTransID"]);
                        objSalesTrans.SalesID = Convert.ToInt32(drData["FK_SalesID"]);
                        objSalesTrans.StockID = Convert.ToInt32(drData["Fk_StockID"]);
                        objSalesTrans.Quantity = Convert.ToInt32(drData["Quantity"]);
                        objSalesTrans.Rate = Convert.ToDecimal(drData["Rate"]);
                        objSalesTrans.Subtotal = Convert.ToDecimal(drData["Subtotal"]);
                        objSalesTrans.CategoryName = Convert.ToString(drData["CategoryName"]);
                        objSalesTrans.Barcode = Convert.ToString(drData["Barcode"]);
                        objSalesTrans.ProductName = Convert.ToString(drData["ProductName"]);
                        objSalesTrans.WastagePercent = Convert.ToDecimal(drData["WastagePercent"]);
                        objSalesTrans.Wastage = Convert.ToDecimal(drData["Wastage"]);
                        objSalesTrans.TotalWeight = Convert.ToDecimal(drData["TotalWeight"]);
                        objSalesTrans.NetWeight = Convert.ToDecimal(drData["NetWeight"]);
                        objSalesTrans.Ratti = Convert.ToDecimal(drData["Ratti"]);
                        objSalesTrans.PureWeight = Convert.ToDecimal(drData["PureWeight"]);
                        objSalesTrans.Lacquer = Convert.ToDecimal(drData["Lacquer"]);
                        objSalesTrans.Design = Convert.ToString(drData["Design"]);
                        objSalesTrans.Karat = Convert.ToString(drData["Karat"]);
                        objSalesTrans.SellingPrice = Convert.ToDecimal(drData["SellingPrice"]);
                        objSalesTrans.StoneName = Convert.ToString(drData["StoneName"]);
                        objSalesTrans.StoneQuantity = Convert.ToInt32(drData["StoneQuantity"]);
                        objSalesTrans.StonePrice = Convert.ToDecimal(drData["StonePrice"]);
                        objSalesTrans.StoneWeight = Convert.ToDecimal(drData["StoneWeight"]);

                        objList.Add(objSalesTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesTrans GetSalesTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveSalesTransaction(Collection<Entity.Billing.SalesTrans> ObjSalesTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.SalesTrans ObjSalesTransaction in ObjSalesTransList)
            {
                if (ObjSalesTransaction.StatusFlag == "I")
                    iID = AddSalesTrans(ObjSalesTransaction);
                else if (ObjSalesTransaction.StatusFlag == "U")
                    bResult = UpdateSalesTrans(ObjSalesTransaction);
                else if (ObjSalesTransaction.StatusFlag == "D")
                    bResult = DeleteSalesTrans(ObjSalesTransaction.SalesTransID);
            }
        }
        public static int AddSalesTrans(Entity.Billing.SalesTrans objSalesTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_SALESTRANS);
                db.AddOutParameter(cmd, "@PK_SalesTransID", DbType.Int32, objSalesTrans.SalesTransID);
                db.AddInParameter(cmd, "@FK_SalesID", DbType.Int32, objSalesTrans.SalesID);
                db.AddInParameter(cmd, "@FK_StockID", DbType.Int32, objSalesTrans.Stock.StockID);
                db.AddInParameter(cmd, "@Quantity", DbType.Int32, objSalesTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objSalesTrans.Rate);
                db.AddInParameter(cmd, "@Subtotal", DbType.Decimal, objSalesTrans.Subtotal);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_SalesTransID"));
            }
            catch (Exception ex)
            {
                sException = "SalesTrans AddSalesTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateSalesTrans(Entity.Billing.SalesTrans objSalesTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_SALESTRANS);
                db.AddInParameter(cmd, "@PK_SalesTransID", DbType.Int32, objSalesTrans.SalesTransID);
                db.AddInParameter(cmd, "@FK_SalesID", DbType.Int32, objSalesTrans.SalesID);
                db.AddInParameter(cmd, "@FK_StockID", DbType.Int32, objSalesTrans.Stock.StockID);
                db.AddInParameter(cmd, "@Quantity", DbType.Int32, objSalesTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objSalesTrans.Rate);
                db.AddInParameter(cmd, "@Subtotal", DbType.Decimal, objSalesTrans.Subtotal);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "SalesTrans UpdateSalesTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteSalesTrans(int iSalesTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_SALESTRANS);
                db.AddInParameter(cmd, "@PK_SalesTransID", DbType.Int32, iSalesTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "SalesTrans DeleteSalesTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        
    }

    public class SalesChitTrans
    {
        public static Collection<Entity.Billing.SalesChitTrans> GetSalesChitTransBySalesID(int iSalesID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesChitTrans> objList = new Collection<Entity.Billing.SalesChitTrans>();
            Entity.Billing.SalesChitTrans objSalesChitTrans = new Entity.Billing.SalesChitTrans();
            Entity.Billing.Register objRegister; Entity.Chit objChit;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESCHITTRANS);
                db.AddInParameter(cmd, "@FK_SalesID", DbType.Int32, iSalesID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesChitTrans = new Entity.Billing.SalesChitTrans();
                        objRegister = new Entity.Billing.Register();
                        objChit = new Entity.Chit();
                        objSalesChitTrans.SalesChitTransID = Convert.ToInt32(drData["PK_SalesChitTransID"]);
                        objSalesChitTrans.SalesID = Convert.ToInt32(drData["FK_SalesID"]);
                        objRegister.RegisterID = Convert.ToInt32(drData["FK_RegisterID"]);
                        objRegister.AccountNo = Convert.ToString(drData["AccountNo"]);
                        objChit.ChitName = Convert.ToString(drData["ChitName"]);
                        objRegister.Chit = objChit;
                        objSalesChitTrans.Register = objRegister;
                        objSalesChitTrans.ChitAmount = Convert.ToDecimal(drData["ChitAmount"]);
                        objList.Add(objSalesChitTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesChitTrans GetSalesChitTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveSalesChitTransaction(Collection<Entity.Billing.SalesChitTrans> ObjSalesChitTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.SalesChitTrans ObjSalesChitTransaction in ObjSalesChitTransList)
            {
                if (ObjSalesChitTransaction.StatusFlag == "I")
                    iID = AddSalesChitTrans(ObjSalesChitTransaction);
                else if (ObjSalesChitTransaction.StatusFlag == "U")
                    bResult = UpdateSalesChitTrans(ObjSalesChitTransaction);
                else if (ObjSalesChitTransaction.StatusFlag == "D")
                    bResult = DeleteSalesChitTrans(ObjSalesChitTransaction.SalesChitTransID);
            }
        }
        public static int AddSalesChitTrans(Entity.Billing.SalesChitTrans objSalesChitTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_SALESCHITTRANS);
                db.AddOutParameter(cmd, "@PK_SalesChitTransID", DbType.Int32, objSalesChitTrans.SalesChitTransID);
                db.AddInParameter(cmd, "@FK_SalesID", DbType.Int32, objSalesChitTrans.SalesID);
                db.AddInParameter(cmd, "@FK_RegisterID", DbType.Int32, objSalesChitTrans.Register.RegisterID);
                db.AddInParameter(cmd, "@ChitAmount", DbType.Decimal, objSalesChitTrans.ChitAmount);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_SalesChitTransID"));
            }
            catch (Exception ex)
            {
                sException = "SalesChitTrans AddSalesChitTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateSalesChitTrans(Entity.Billing.SalesChitTrans objSalesChitTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_SALESCHITTRANS);
                db.AddInParameter(cmd, "@PK_SalesChitTransID", DbType.Int32, objSalesChitTrans.SalesChitTransID);
                db.AddInParameter(cmd, "@FK_SalesID", DbType.Int32, objSalesChitTrans.SalesID);
                db.AddInParameter(cmd, "@FK_RegisterID", DbType.Int32, objSalesChitTrans.Register.RegisterID);
                db.AddInParameter(cmd, "@ChitAmount", DbType.Decimal, objSalesChitTrans.ChitAmount);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "SalesChitTrans UpdateSalesChitTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteSalesChitTrans(int iSalesChitTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_SALESCHITTRANS);
                db.AddInParameter(cmd, "@PK_SalesChitTransID", DbType.Int32, iSalesChitTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "SalesChitTrans DeleteSalesChitTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

    }
}
