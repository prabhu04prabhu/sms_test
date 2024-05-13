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
    public class Payment
    {
        public static Collection<Entity.Billing.Payment> GetPayment(int Type,int FK_FinancialYearID=0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Payment> objList = new Collection<Entity.Billing.Payment>();
            Entity.Billing.Payment objPayment = new Entity.Billing.Payment();
            Entity.Billing.Supplier objSupplier;
            Entity.Ledger objBank;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PAYMENT);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, Type);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPayment = new Entity.Billing.Payment();
                        objSupplier = new Entity.Billing.Supplier();
                        objBank = new Entity.Ledger();

                        objPayment.PaymentID = Convert.ToInt32(drData["PK_PaymentID"]);
                        objPayment.VoucherNo = Convert.ToString(drData["VoucherNo"]);
                        objPayment.VoucherDate = Convert.ToDateTime(drData["VoucherDate"]);
                        objPayment.sVoucherDate = objPayment.VoucherDate.ToString("dd/MM/yyyy");

                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPayment.Supplier = objSupplier;

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objPayment.Bank = objBank;

                        objPayment.PaymentModeID = Convert.ToInt16(drData["FK_PaymentModeID"]);
                        objPayment.Amount = Convert.ToDecimal(drData["Amount"]);
                        objPayment.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objPayment.BillNos = Convert.ToString(drData["BillNos"]);
                        objPayment.OtherDiscount = Convert.ToDecimal(drData["OtherDiscount"]);
                        objPayment.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                        objPayment.sIssueDate = objPayment.IssueDate.ToString("dd/MM/yyyy");
                        objPayment.CollectionDate = Convert.ToDateTime(drData["CollectionDate"]);
                        objPayment.sCollectionDate = objPayment.CollectionDate.ToString("dd/MM/yyyy");
                        objPayment.IssuedBy = Convert.ToString(drData["IssuedBy"]);
                        objPayment.Description = Convert.ToString(drData["Description"]);
                        objPayment.DocumentPath = Convert.ToString(drData["DocumentPath"]);

                        objList.Add(objPayment);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Payment GetPayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Payment> GetLastPaymentDetails(int ID, int Type, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Payment> objList = new Collection<Entity.Billing.Payment>();
            Entity.Billing.Payment objPayment = new Entity.Billing.Payment();
            Entity.Billing.Supplier objSupplier;
            Entity.Ledger objBank;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_RECENTPAYMENT);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, ID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, Type);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPayment = new Entity.Billing.Payment();
                        objSupplier = new Entity.Billing.Supplier();
                        objBank = new Entity.Ledger();

                        objPayment.PaymentID = Convert.ToInt32(drData["PK_PaymentID"]);
                        objPayment.VoucherNo = Convert.ToString(drData["VoucherNo"]);
                        objPayment.VoucherDate = Convert.ToDateTime(drData["VoucherDate"]);
                        objPayment.sVoucherDate = objPayment.VoucherDate.ToString("dd/MM/yyyy");

                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPayment.Supplier = objSupplier;

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objPayment.Bank = objBank;

                        objPayment.BillNo = Convert.ToString(drData["BillNo"]);
                        objPayment.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objPayment.sBillDate = objPayment.BillDate.ToString("dd/MM/yyyy");

                        objPayment.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPayment.PaymentModeID = Convert.ToInt16(drData["FK_PaymentModeID"]);
                        objPayment.Amount = Convert.ToDecimal(drData["Amount"]);
                        objPayment.Charges = Convert.ToDecimal(drData["Charges"]);
                        objPayment.DiscountAmount = Convert.ToDecimal(drData["Discount_Amount"]);
                        objPayment.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objPayment.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                        objPayment.sIssueDate = objPayment.IssueDate.ToString("dd/MM/yyyy");
                        objPayment.CollectionDate = Convert.ToDateTime(drData["CollectionDate"]);
                        objPayment.sCollectionDate = objPayment.CollectionDate.ToString("dd/MM/yyyy");
                        objPayment.IssuedBy = Convert.ToString(drData["IssuedBy"]);
                        objPayment.Description = Convert.ToString(drData["Description"]);
                        objPayment.DocumentPath = Convert.ToString(drData["DocumentPath"]);

                        objList.Add(objPayment);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Payment GetPayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.Payment GetPaymentByID(int iPaymentID, int Type, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Payment objPayment = new Entity.Billing.Payment();
            Entity.Billing.Supplier objSupplier;
            Entity.Ledger objBank;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PAYMENT);
                db.AddInParameter(cmd, "@PK_PaymentID", DbType.Int32, iPaymentID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, Type);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPayment = new Entity.Billing.Payment();
                        objSupplier = new Entity.Billing.Supplier();
                        objBank = new Entity.Ledger();

                        objPayment.PaymentID = Convert.ToInt32(drData["PK_PaymentID"]);
                        objPayment.VoucherNo = Convert.ToString(drData["VoucherNo"]);
                        objPayment.VoucherDate = Convert.ToDateTime(drData["VoucherDate"]);
                        objPayment.sVoucherDate = objPayment.VoucherDate.ToString("dd/MM/yyyy");

                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPayment.Supplier = objSupplier;

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objPayment.Bank = objBank;

                        objPayment.PaymentModeID = Convert.ToInt16(drData["FK_PaymentModeID"]);
                        objPayment.Amount = Convert.ToDecimal(drData["Amount"]);
                        objPayment.Charges = Convert.ToDecimal(drData["Charges"]);
                        objPayment.OnAccount = Convert.ToDecimal(drData["OnAccount"]);
                        objPayment.OtherDiscount = Convert.ToDecimal(drData["OtherDiscount"]);
                        objPayment.DiscountAmount = Convert.ToDecimal(drData["Discount_Amount"]);
                        objPayment.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objPayment.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                        objPayment.sIssueDate = objPayment.IssueDate.ToString("dd/MM/yyyy");
                        objPayment.CollectionDate = Convert.ToDateTime(drData["CollectionDate"]);
                        objPayment.sCollectionDate = objPayment.CollectionDate.ToString("dd/MM/yyyy");
                        objPayment.IssuedBy = Convert.ToString(drData["IssuedBy"]);
                        objPayment.Description = Convert.ToString(drData["Description"]);
                        objPayment.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Payment GetPaymentByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objPayment;
        }
        public static int AddPayment(Entity.Billing.Payment objPayment)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_PAYMENT);
                db.AddOutParameter(cmd, "@PK_PaymentID", DbType.Int32, objPayment.PaymentID);
                db.AddInParameter(cmd, "@VoucherNo", DbType.String, objPayment.VoucherNo);
                db.AddInParameter(cmd, "@VoucherDate", DbType.String, objPayment.sVoucherDate);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, objPayment.Supplier.SupplierID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, objPayment.BillType);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objPayment.Bank.LedgerID);
                db.AddInParameter(cmd, "@FK_PaymentModeID", DbType.Int16, objPayment.PaymentModeID);
                db.AddInParameter(cmd, "@Amount", DbType.Decimal, objPayment.Amount);
                db.AddInParameter(cmd, "@ChequeNo", DbType.String, objPayment.ChequeNo);
                db.AddInParameter(cmd, "@OnAccount", DbType.Decimal, objPayment.OnAccount);
                db.AddInParameter(cmd, "@IssueDate", DbType.String, objPayment.sIssueDate);
                db.AddInParameter(cmd, "@CollectionDate", DbType.String, objPayment.sCollectionDate);
                db.AddInParameter(cmd, "@IssuedBy", DbType.String, objPayment.IssuedBy);
                db.AddInParameter(cmd, "@Description", DbType.String, objPayment.Description);
                db.AddInParameter(cmd, "@PurchaseIDs", DbType.String, objPayment.PurchaseIDs);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objPayment.CreatedBy.UserID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objPayment.FinancialYear.FinancialYearID);
                db.AddInParameter(cmd, "@Charges", DbType.Decimal, objPayment.Charges);
                db.AddInParameter(cmd, "@OtherDiscount", DbType.Decimal, objPayment.OtherDiscount);
                db.AddInParameter(cmd, "@Discount_Amount", DbType.Decimal, objPayment.DiscountAmount);
                db.AddInParameter(cmd, "@DocumentPath", DbType.String, objPayment.DocumentPath);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_PaymentID"));
            }
            catch (Exception ex)
            {
                sException = "Payment AddPayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdatePayment(Entity.Billing.Payment objPayment)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_PAYMENT);
                db.AddInParameter(cmd, "@PK_PaymentID", DbType.Int32, objPayment.PaymentID);
                db.AddInParameter(cmd, "@VoucherNo", DbType.String, objPayment.VoucherNo);
                db.AddInParameter(cmd, "@VoucherDate", DbType.String, objPayment.sVoucherDate);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, objPayment.Supplier.SupplierID);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objPayment.Bank.LedgerID);
                db.AddInParameter(cmd, "@FK_PaymentModeID", DbType.Int16, objPayment.PaymentModeID);
                db.AddInParameter(cmd, "@Amount", DbType.Decimal, objPayment.Amount);
                db.AddInParameter(cmd, "@OnAccount", DbType.Decimal, objPayment.OnAccount);
                db.AddInParameter(cmd, "@ChequeNo", DbType.String, objPayment.ChequeNo);
                db.AddInParameter(cmd, "@IssueDate", DbType.String, objPayment.sIssueDate);
                db.AddInParameter(cmd, "@CollectionDate", DbType.String, objPayment.sCollectionDate);
                db.AddInParameter(cmd, "@IssuedBy", DbType.String, objPayment.IssuedBy);
                db.AddInParameter(cmd, "@Description", DbType.String, objPayment.Description);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objPayment.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@Charges", DbType.Decimal, objPayment.Charges);
                db.AddInParameter(cmd, "@OtherDiscount", DbType.Decimal, objPayment.OtherDiscount);
                db.AddInParameter(cmd, "@Discount_Amount", DbType.Decimal, objPayment.DiscountAmount);
                db.AddInParameter(cmd, "@DocumentPath", DbType.String, objPayment.DocumentPath);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objPayment.FinancialYear.FinancialYearID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Payment UpdatePayment| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeletePayment(int iPaymentId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_PAYMENT);
                db.AddInParameter(cmd, "@PK_PaymentID", DbType.Int32, iPaymentId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Payment DeletePayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
