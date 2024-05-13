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
    public class ExpensePayment
    {
        public static Collection<Entity.Billing.ExpensePayment> GetExpensePayment(int Type,int FK_FinancialYearID=0, int iPartyID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.ExpensePayment> objList = new Collection<Entity.Billing.ExpensePayment>();
            Entity.Billing.ExpensePayment objExpensePayment = new Entity.Billing.ExpensePayment();
            Entity.Ledger objSupplier;
            Entity.Ledger objBank;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_EXPENSEPAYMENT);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, Type);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iPartyID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objExpensePayment = new Entity.Billing.ExpensePayment();
                        objSupplier = new Entity.Ledger();
                        objBank = new Entity.Ledger();

                        objExpensePayment.ExpensePaymentID = Convert.ToInt32(drData["PK_ExpensePaymentID"]);
                        objExpensePayment.VoucherNo = Convert.ToString(drData["VoucherNo"]);
                        objExpensePayment.VoucherDate = Convert.ToDateTime(drData["VoucherDate"]);
                        objExpensePayment.sVoucherDate = objExpensePayment.VoucherDate.ToString("dd/MM/yyyy");

                        objSupplier.LedgerID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.LedgerName = Convert.ToString(drData["SupplierName"]);
                        objExpensePayment.Supplier = objSupplier;

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objExpensePayment.Bank = objBank;

                        objExpensePayment.PaymentModeID = Convert.ToInt16(drData["FK_PaymentModeID"]);
                        objExpensePayment.Amount = Convert.ToDecimal(drData["Amount"]);
                        objExpensePayment.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objExpensePayment.BillNos = Convert.ToString(drData["BillNos"]);
                        objExpensePayment.OtherDiscount = Convert.ToDecimal(drData["OtherDiscount"]);
                        objExpensePayment.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                        objExpensePayment.sIssueDate = objExpensePayment.IssueDate.ToString("dd/MM/yyyy");
                        objExpensePayment.CollectionDate = Convert.ToDateTime(drData["CollectionDate"]);
                        objExpensePayment.sCollectionDate = objExpensePayment.CollectionDate.ToString("dd/MM/yyyy");
                        objExpensePayment.IssuedBy = Convert.ToString(drData["IssuedBy"]);
                        objExpensePayment.Description = Convert.ToString(drData["Description"]);
                        objExpensePayment.DocumentPath = Convert.ToString(drData["DocumentPath"]);

                        objList.Add(objExpensePayment);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "ExpensePayment GetExpensePayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.ExpensePayment> GetLastExpensePaymentDetails(int ID, int Type, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.ExpensePayment> objList = new Collection<Entity.Billing.ExpensePayment>();
            Entity.Billing.ExpensePayment objExpensePayment = new Entity.Billing.ExpensePayment();
            Entity.Ledger objSupplier;
            Entity.Ledger objBank;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_RECENTEXPENSEPAYMENT);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, ID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, Type);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objExpensePayment = new Entity.Billing.ExpensePayment();
                        objSupplier = new Entity.Ledger();
                        objBank = new Entity.Ledger();

                        objExpensePayment.ExpensePaymentID = Convert.ToInt32(drData["PK_ExpensePaymentID"]);
                        objExpensePayment.VoucherNo = Convert.ToString(drData["VoucherNo"]);
                        objExpensePayment.VoucherDate = Convert.ToDateTime(drData["VoucherDate"]);
                        objExpensePayment.sVoucherDate = objExpensePayment.VoucherDate.ToString("dd/MM/yyyy");

                        objSupplier.LedgerID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.LedgerName = Convert.ToString(drData["SupplierName"]);
                        objExpensePayment.Supplier = objSupplier;

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objExpensePayment.Bank = objBank;

                        objExpensePayment.BillNo = Convert.ToString(drData["BillNo"]);
                        objExpensePayment.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objExpensePayment.sBillDate = objExpensePayment.BillDate.ToString("dd/MM/yyyy");

                        objExpensePayment.PaymentModeID = Convert.ToInt16(drData["FK_PaymentModeID"]);
                        objExpensePayment.Amount = Convert.ToDecimal(drData["Amount"]);
                        objExpensePayment.Charges = Convert.ToDecimal(drData["Charges"]);
                        objExpensePayment.DiscountAmount = Convert.ToDecimal(drData["Discount_Amount"]);
                        objExpensePayment.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objExpensePayment.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                        objExpensePayment.sIssueDate = objExpensePayment.IssueDate.ToString("dd/MM/yyyy");
                        objExpensePayment.CollectionDate = Convert.ToDateTime(drData["CollectionDate"]);
                        objExpensePayment.sCollectionDate = objExpensePayment.CollectionDate.ToString("dd/MM/yyyy");
                        objExpensePayment.IssuedBy = Convert.ToString(drData["IssuedBy"]);
                        objExpensePayment.Description = Convert.ToString(drData["Description"]);
                        objExpensePayment.DocumentPath = Convert.ToString(drData["DocumentPath"]);

                        objList.Add(objExpensePayment);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "ExpensePayment GetExpensePayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.ExpensePayment GetExpensePaymentByID(int iExpensePaymentID, int Type, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.ExpensePayment objExpensePayment = new Entity.Billing.ExpensePayment();
            Entity.Ledger objSupplier;
            Entity.Ledger objBank;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_EXPENSEPAYMENT);
                db.AddInParameter(cmd, "@PK_ExpensePaymentID", DbType.Int32, iExpensePaymentID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, Type);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objExpensePayment = new Entity.Billing.ExpensePayment();
                        objSupplier = new Entity.Ledger();
                        objBank = new Entity.Ledger();

                        objExpensePayment.ExpensePaymentID = Convert.ToInt32(drData["PK_ExpensePaymentID"]);
                        objExpensePayment.VoucherNo = Convert.ToString(drData["VoucherNo"]);
                        objExpensePayment.VoucherDate = Convert.ToDateTime(drData["VoucherDate"]);
                        objExpensePayment.sVoucherDate = objExpensePayment.VoucherDate.ToString("dd/MM/yyyy");

                        objSupplier.LedgerID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.LedgerName = Convert.ToString(drData["SupplierName"]);
                        objExpensePayment.Supplier = objSupplier;

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objExpensePayment.Bank = objBank;

                        objExpensePayment.PaymentModeID = Convert.ToInt16(drData["FK_PaymentModeID"]);
                        objExpensePayment.Amount = Convert.ToDecimal(drData["Amount"]);
                        objExpensePayment.Charges = Convert.ToDecimal(drData["Charges"]);
                        objExpensePayment.IsAdvance = Convert.ToBoolean(drData["IsAdvance"]);
                        objExpensePayment.OnAccount = Convert.ToDecimal(drData["OnAccount"]);
                        objExpensePayment.OtherDiscount = Convert.ToDecimal(drData["OtherDiscount"]);
                        objExpensePayment.DiscountAmount = Convert.ToDecimal(drData["Discount_Amount"]);
                        objExpensePayment.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objExpensePayment.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                        objExpensePayment.sIssueDate = objExpensePayment.IssueDate.ToString("dd/MM/yyyy");
                        objExpensePayment.CollectionDate = Convert.ToDateTime(drData["CollectionDate"]);
                        objExpensePayment.sCollectionDate = objExpensePayment.CollectionDate.ToString("dd/MM/yyyy");
                        objExpensePayment.IssuedBy = Convert.ToString(drData["IssuedBy"]);
                        objExpensePayment.Description = Convert.ToString(drData["Description"]);
                        objExpensePayment.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "ExpensePayment GetExpensePaymentByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objExpensePayment;
        }
        public static int AddExpensePayment(Entity.Billing.ExpensePayment objExpensePayment)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_EXPENSEPAYMENT);
                db.AddOutParameter(cmd, "@PK_ExpensePaymentID", DbType.Int32, objExpensePayment.ExpensePaymentID);
                db.AddInParameter(cmd, "@VoucherNo", DbType.String, objExpensePayment.VoucherNo);
                db.AddInParameter(cmd, "@VoucherDate", DbType.String, objExpensePayment.sVoucherDate);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, objExpensePayment.Supplier.LedgerID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, objExpensePayment.BillType);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objExpensePayment.Bank.LedgerID);
                db.AddInParameter(cmd, "@FK_PaymentModeID", DbType.Int16, objExpensePayment.PaymentModeID);
                db.AddInParameter(cmd, "@Amount", DbType.Decimal, objExpensePayment.Amount);
                db.AddInParameter(cmd, "@ChequeNo", DbType.String, objExpensePayment.ChequeNo);
                db.AddInParameter(cmd, "@OnAccount", DbType.Decimal, objExpensePayment.OnAccount);
                db.AddInParameter(cmd, "@IssueDate", DbType.String, objExpensePayment.sIssueDate);
                db.AddInParameter(cmd, "@CollectionDate", DbType.String, objExpensePayment.sCollectionDate);
                db.AddInParameter(cmd, "@IssuedBy", DbType.String, objExpensePayment.IssuedBy);
                db.AddInParameter(cmd, "@Description", DbType.String, objExpensePayment.Description);
                db.AddInParameter(cmd, "@PurchaseIDs", DbType.String, objExpensePayment.PurchaseIDs);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objExpensePayment.CreatedBy.UserID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objExpensePayment.FinancialYear.FinancialYearID);
                db.AddInParameter(cmd, "@Charges", DbType.Decimal, objExpensePayment.Charges);
                db.AddInParameter(cmd, "@OtherDiscount", DbType.Decimal, objExpensePayment.OtherDiscount);
                db.AddInParameter(cmd, "@Discount_Amount", DbType.Decimal, objExpensePayment.DiscountAmount);
                db.AddInParameter(cmd, "@IsAdvance", DbType.Boolean, objExpensePayment.IsAdvance);
                db.AddInParameter(cmd, "@DocumentPath", DbType.String, objExpensePayment.DocumentPath);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_ExpensePaymentID"));
            }
            catch (Exception ex)
            {
                sException = "ExpensePayment AddExpensePayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateExpensePayment(Entity.Billing.ExpensePayment objExpensePayment)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_EXPENSEPAYMENT);
                db.AddInParameter(cmd, "@PK_ExpensePaymentID", DbType.Int32, objExpensePayment.ExpensePaymentID);
                db.AddInParameter(cmd, "@VoucherNo", DbType.String, objExpensePayment.VoucherNo);
                db.AddInParameter(cmd, "@VoucherDate", DbType.String, objExpensePayment.sVoucherDate);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, objExpensePayment.Supplier.LedgerID);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objExpensePayment.Bank.LedgerID);
                db.AddInParameter(cmd, "@FK_PaymentModeID", DbType.Int16, objExpensePayment.PaymentModeID);
                db.AddInParameter(cmd, "@Amount", DbType.Decimal, objExpensePayment.Amount);
                db.AddInParameter(cmd, "@OnAccount", DbType.Decimal, objExpensePayment.OnAccount);
                db.AddInParameter(cmd, "@ChequeNo", DbType.String, objExpensePayment.ChequeNo);
                db.AddInParameter(cmd, "@IssueDate", DbType.String, objExpensePayment.sIssueDate);
                db.AddInParameter(cmd, "@CollectionDate", DbType.String, objExpensePayment.sCollectionDate);
                db.AddInParameter(cmd, "@IssuedBy", DbType.String, objExpensePayment.IssuedBy);
                db.AddInParameter(cmd, "@Description", DbType.String, objExpensePayment.Description);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objExpensePayment.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@Charges", DbType.Decimal, objExpensePayment.Charges);
                db.AddInParameter(cmd, "@OtherDiscount", DbType.Decimal, objExpensePayment.OtherDiscount);
                db.AddInParameter(cmd, "@Discount_Amount", DbType.Decimal, objExpensePayment.DiscountAmount);
                db.AddInParameter(cmd, "@DocumentPath", DbType.String, objExpensePayment.DocumentPath);
                db.AddInParameter(cmd, "@IsAdvance", DbType.Boolean, objExpensePayment.IsAdvance);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objExpensePayment.FinancialYear.FinancialYearID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "ExpensePayment UpdateExpensePayment| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteExpensePayment(int iExpensePaymentId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_EXPENSEPAYMENT);
                db.AddInParameter(cmd, "@PK_ExpensePaymentID", DbType.Int32, iExpensePaymentId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "ExpensePayment DeleteExpensePayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static Entity.Receipt GetExpenseBalanceAmount(int iReceiptID, string Type)
        {
            string sException = string.Empty;
            Database db;
            Entity.Receipt objReceipt = new Entity.Receipt();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_EXPENSEPAYMENTAMOUNT);
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

    }
}
