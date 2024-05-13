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
    public class Receipt
    {
        public static Collection<Entity.Receipt> GetReceipt(int IsRetail=0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Receipt> objList = new Collection<Entity.Receipt>();
            Entity.Receipt objReceipt = new Entity.Receipt();
            Entity.Customer objCustomer;
            Entity.Ledger objBank;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_RECEIPT);
                db.AddInParameter(cmd, "@IsRetail", DbType.Boolean, Convert.ToBoolean(IsRetail));
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objReceipt = new Entity.Receipt();
                        objCustomer = new Entity.Customer();
                        objBank = new Entity.Ledger();

                        objReceipt.ReceiptID = Convert.ToInt32(drData["PK_ReceiptID"]);
                        objReceipt.VoucherNo = Convert.ToString(drData["VoucherNo"]);
                        objReceipt.VoucherDate = Convert.ToDateTime(drData["VoucherDate"]);
                        objReceipt.sVoucherDate = objReceipt.VoucherDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objReceipt.Customer = objCustomer;

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["sBankName"]);
                        objReceipt.Bank = objBank;
                        objReceipt.Status = Convert.ToString(drData["Status"]);
                        objReceipt.InvoiceNos = Convert.ToString(drData["BillNos"]);
                        objReceipt.ReceiptModeID = Convert.ToInt16(drData["FK_ReceiptModeID"]);
                        objReceipt.Amount = Convert.ToDecimal(drData["Amount"]);
                        objReceipt.TDSPayment = Convert.ToDecimal(drData["TDSPayment"]);
                        objReceipt.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objReceipt.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                        objReceipt.sIssueDate = objReceipt.IssueDate.ToString("dd/MM/yyyy");
                        objReceipt.CollectionDate = Convert.ToDateTime(drData["CollectionDate"]);
                        objReceipt.sCollectionDate = objReceipt.CollectionDate.ToString("dd/MM/yyyy");
                        objReceipt.IssuedBy = Convert.ToString(drData["IssuedBy"]);
                        objReceipt.Description = Convert.ToString(drData["Description"]);
                        objReceipt.BankName = Convert.ToString(drData["BankName"]);
                        objReceipt.Charges = Convert.ToDecimal(drData["Charges"]);
                        objReceipt.DiscountAmount = Convert.ToDecimal(drData["Discount_Amount"]);
                        objReceipt.DocumentPath = Convert.ToString(drData["DocumentPath"]);

                        objList.Add(objReceipt);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Receipt GetReceipt | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        
        public static Collection<Entity.Receipt> GetLastReceiptDetails(int ID, int IsRetail = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Receipt> objList = new Collection<Entity.Receipt>();
            Entity.Receipt objReceipt = new Entity.Receipt();
            Entity.Customer objCustomer;
            Entity.Ledger objBank;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_RECENTRECEIPT);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, ID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objReceipt = new Entity.Receipt();
                        objCustomer = new Entity.Customer();
                        objBank = new Entity.Ledger();

                        objReceipt.ReceiptID = Convert.ToInt32(drData["PK_ReceiptID"]);
                        objReceipt.VoucherNo = Convert.ToString(drData["VoucherNo"]);
                        objReceipt.VoucherDate = Convert.ToDateTime(drData["VoucherDate"]);
                        objReceipt.sVoucherDate = objReceipt.VoucherDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objReceipt.Customer = objCustomer;

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["sBankName"]);
                        objReceipt.Bank = objBank;
                        objReceipt.Status = Convert.ToString(drData["Status"]);
                        objReceipt.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objReceipt.ReceiptModeID = Convert.ToInt16(drData["FK_ReceiptModeID"]);
                        objReceipt.Amount = Convert.ToDecimal(drData["Amount"]);
                        objReceipt.TDSPayment = Convert.ToDecimal(drData["TDSPayment"]);
                        objReceipt.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objReceipt.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                        objReceipt.sIssueDate = objReceipt.IssueDate.ToString("dd/MM/yyyy");
                        objReceipt.CollectionDate = Convert.ToDateTime(drData["CollectionDate"]);
                        objReceipt.sCollectionDate = objReceipt.CollectionDate.ToString("dd/MM/yyyy");
                        objReceipt.IssuedBy = Convert.ToString(drData["IssuedBy"]);
                        objReceipt.Description = Convert.ToString(drData["Description"]);
                        objReceipt.BankName = Convert.ToString(drData["BankName"]);
                        objReceipt.Charges = Convert.ToDecimal(drData["Charges"]);
                        objReceipt.OnAccount = Convert.ToDecimal(drData["OnAccount"]);
                        objReceipt.DiscountAmount = Convert.ToDecimal(drData["Discount_Amount"]);
                        objReceipt.DocumentPath = Convert.ToString(drData["DocumentPath"]);

                        objList.Add(objReceipt);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Receipt GetReceipt | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Receipt> GetReceiptByStatus(string iStatus, int IsRetail = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Receipt> objList = new Collection<Entity.Receipt>();
            Entity.Receipt objReceipt = new Entity.Receipt();
            Entity.Customer objCustomer;
            Entity.Ledger objBank;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_RECEIPTBYSTATUS);
                db.AddInParameter(cmd, "@Status", DbType.String, iStatus);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objReceipt = new Entity.Receipt();
                        objCustomer = new Entity.Customer();
                        objBank = new Entity.Ledger();

                        objReceipt.ReceiptID = Convert.ToInt32(drData["PK_ReceiptID"]);
                        objReceipt.VoucherNo = Convert.ToString(drData["VoucherNo"]);
                        objReceipt.VoucherDate = Convert.ToDateTime(drData["VoucherDate"]);
                        objReceipt.sVoucherDate = objReceipt.VoucherDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objReceipt.Customer = objCustomer;

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["sBankName"]);
                        objReceipt.Bank = objBank;
                        objReceipt.Status = Convert.ToString(drData["Status"]);
                        objReceipt.ReceiptModeID = Convert.ToInt16(drData["FK_ReceiptModeID"]);
                        objReceipt.Amount = Convert.ToDecimal(drData["Amount"]);
                        objReceipt.TDSPayment = Convert.ToDecimal(drData["TDSPayment"]);
                        objReceipt.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objReceipt.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                        objReceipt.sIssueDate = objReceipt.IssueDate.ToString("dd/MM/yyyy");
                        objReceipt.CollectionDate = Convert.ToDateTime(drData["CollectionDate"]);
                        objReceipt.sCollectionDate = objReceipt.CollectionDate.ToString("dd/MM/yyyy");
                        objReceipt.IssuedBy = Convert.ToString(drData["IssuedBy"]);
                        objReceipt.Description = Convert.ToString(drData["Description"]);
                        objReceipt.BankName = Convert.ToString(drData["BankName"]);
                        objReceipt.Charges = Convert.ToDecimal(drData["Charges"]);
                        objReceipt.DiscountAmount = Convert.ToDecimal(drData["Discount_Amount"]);
                        objReceipt.DocumentPath = Convert.ToString(drData["DocumentPath"]);

                        objList.Add(objReceipt);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Receipt GetReceipt | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Receipt GetReceiptByID(int iReceiptID, int IsRetail=0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Receipt objReceipt = new Entity.Receipt();
            Entity.Customer objCustomer;
            Entity.Ledger objBank;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_RECEIPT);
                db.AddInParameter(cmd, "@PK_ReceiptID", DbType.Int32, iReceiptID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetail", DbType.Boolean, IsRetail);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objReceipt = new Entity.Receipt();
                        objCustomer = new Entity.Customer();
                        objBank = new Entity.Ledger();

                        objReceipt.ReceiptID = Convert.ToInt32(drData["PK_ReceiptID"]);
                        objReceipt.VoucherNo = Convert.ToString(drData["VoucherNo"]);
                        objReceipt.VoucherDate = Convert.ToDateTime(drData["VoucherDate"]);
                        objReceipt.sVoucherDate = objReceipt.VoucherDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objReceipt.Customer = objCustomer;
                        
                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["sBankName"]);
                        objReceipt.Bank = objBank;
                        objReceipt.Status = Convert.ToString(drData["Status"]);
                        objReceipt.ReceiptModeID = Convert.ToInt16(drData["FK_ReceiptModeID"]);
                        objReceipt.Amount = Convert.ToDecimal(drData["Amount"]);
                        objReceipt.TDSPayment = Convert.ToDecimal(drData["TDSPayment"]);
                        objReceipt.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objReceipt.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                        objReceipt.sIssueDate = objReceipt.IssueDate.ToString("dd/MM/yyyy");
                        objReceipt.CollectionDate = Convert.ToDateTime(drData["CollectionDate"]);
                        objReceipt.sCollectionDate = objReceipt.CollectionDate.ToString("dd/MM/yyyy");
                        objReceipt.IssuedBy = Convert.ToString(drData["IssuedBy"]);
                        objReceipt.Description = Convert.ToString(drData["Description"]);
                        objReceipt.BankName = Convert.ToString(drData["BankName"]);
                        objReceipt.Charges = Convert.ToDecimal(drData["Charges"]); 
                        objReceipt.OnAccount = Convert.ToDecimal(drData["OnAccount"]);
                        objReceipt.DiscountAmount = Convert.ToDecimal(drData["Discount_Amount"]);
                        objReceipt.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Receipt GetReceiptByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objReceipt;
        }

        public static Entity.Receipt GetOnAccountAmount(int iReceiptID, string Type)
        {
            string sException = string.Empty;
            Database db;
            Entity.Receipt objReceipt = new Entity.Receipt();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ONACCOUNTAMOUNT);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iReceiptID); 
                db.AddInParameter(cmd, "@PartyType", DbType.String, Type);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objReceipt.OnAccount = Convert.ToDecimal(drData["Amount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Receipt GetReceiptByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objReceipt;
        }
        public static int AddReceipt(Entity.Receipt objReceipt)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_RECEIPT);
                db.AddOutParameter(cmd, "@PK_ReceiptID", DbType.Int32, objReceipt.ReceiptID);
                db.AddInParameter(cmd, "@VoucherNo", DbType.String, objReceipt.VoucherNo);
                db.AddInParameter(cmd, "@VoucherDate", DbType.String, objReceipt.sVoucherDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objReceipt.Customer.CustomerID);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objReceipt.Bank.LedgerID);
                db.AddInParameter(cmd, "@BankName", DbType.String, objReceipt.BankName);
                db.AddInParameter(cmd, "@FK_ReceiptModeID", DbType.Int16, objReceipt.ReceiptModeID);
                db.AddInParameter(cmd, "@Amount", DbType.Decimal, objReceipt.Amount);
                db.AddInParameter(cmd, "@OnAccount", DbType.Decimal, objReceipt.OnAccount);
                db.AddInParameter(cmd, "@TDSPayment", DbType.Decimal, objReceipt.TDSPayment);
                db.AddInParameter(cmd, "@ChequeNo", DbType.String, objReceipt.ChequeNo);
                db.AddInParameter(cmd, "@Status", DbType.String, objReceipt.Status);
                db.AddInParameter(cmd, "@IssueDate", DbType.String, objReceipt.sIssueDate);
                db.AddInParameter(cmd, "@CollectionDate", DbType.String, objReceipt.sCollectionDate);
                db.AddInParameter(cmd, "@IssuedBy", DbType.String, objReceipt.IssuedBy);
                db.AddInParameter(cmd, "@SalesEntryIDs", DbType.String, objReceipt.SalesEntryIDs);
                db.AddInParameter(cmd, "@Description", DbType.String, objReceipt.Description);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objReceipt.CreatedBy.UserID);
                db.AddInParameter(cmd, "@IsRetail", DbType.Int32, objReceipt.IsRetail);
                db.AddInParameter(cmd, "@Charges", DbType.Decimal, objReceipt.Charges);
                db.AddInParameter(cmd, "@Discount_Amount", DbType.Decimal, objReceipt.DiscountAmount);
                db.AddInParameter(cmd, "@DocumentPath", DbType.String, objReceipt.DocumentPath);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objReceipt.FinancialYear.FinancialYearID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_ReceiptID"));
            }
            catch (Exception ex)
            {
                sException = "Receipt AddReceipt | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateReceipt(Entity.Receipt objReceipt)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_RECEIPT);
                db.AddInParameter(cmd, "@PK_ReceiptID", DbType.Int32, objReceipt.ReceiptID);
                db.AddInParameter(cmd, "@VoucherNo", DbType.String, objReceipt.VoucherNo);
                db.AddInParameter(cmd, "@VoucherDate", DbType.String, objReceipt.sVoucherDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objReceipt.Customer.CustomerID);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objReceipt.Bank.LedgerID);
                db.AddInParameter(cmd, "@BankName", DbType.String, objReceipt.BankName);
                db.AddInParameter(cmd, "@FK_ReceiptModeID", DbType.Int16, objReceipt.ReceiptModeID);
                db.AddInParameter(cmd, "@Amount", DbType.Decimal, objReceipt.Amount);
                db.AddInParameter(cmd, "@OnAccount", DbType.Decimal, objReceipt.OnAccount);
                db.AddInParameter(cmd, "@TDSPayment", DbType.Decimal, objReceipt.TDSPayment);
                db.AddInParameter(cmd, "@ChequeNo", DbType.String, objReceipt.ChequeNo);
                db.AddInParameter(cmd, "@Status", DbType.String, objReceipt.Status);
                db.AddInParameter(cmd, "@IssueDate", DbType.String, objReceipt.sIssueDate);
                db.AddInParameter(cmd, "@CollectionDate", DbType.String, objReceipt.sCollectionDate);
                db.AddInParameter(cmd, "@IssuedBy", DbType.String, objReceipt.IssuedBy);
                db.AddInParameter(cmd, "@Description", DbType.String, objReceipt.Description);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objReceipt.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@IsRetail", DbType.Int32, objReceipt.IsRetail);
                db.AddInParameter(cmd, "@Charges", DbType.Decimal, objReceipt.Charges);
                db.AddInParameter(cmd, "@Discount_Amount", DbType.Decimal, objReceipt.DiscountAmount);
                db.AddInParameter(cmd, "@DocumentPath", DbType.String, objReceipt.DocumentPath);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objReceipt.FinancialYear.FinancialYearID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Receipt UpdateReceipt| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteReceipt(int iReceiptId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_RECEIPT);
                db.AddInParameter(cmd, "@PK_ReceiptID", DbType.Int32, iReceiptId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Receipt DeleteReceipt | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
