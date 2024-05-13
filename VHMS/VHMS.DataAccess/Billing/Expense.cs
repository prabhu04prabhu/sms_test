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
    public class Expense
    {
        public static Collection<Entity.Billing.Expense> GetExpense(int ipatientID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Expense> objList = new Collection<Entity.Billing.Expense>();
            Entity.Billing.Expense objExpense;
            Entity.Ledger objParty; Entity.Ledger objBank; Entity.Billing.Tax objTax;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPEXPENSE);
                db.AddInParameter(cmd, "@PK_ExpenseID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objExpense = new Entity.Billing.Expense();
                        objParty = new Entity.Ledger();
                        objBank = new Entity.Ledger();
                        objTax = new Entity.Billing.Tax();

                        objExpense.ExpenseID = Convert.ToInt32(drData["PK_ExpenseID"]);
                        objExpense.ExpenseNo = Convert.ToString(drData["ExpenseNo"]);
                        objExpense.ExpenseDate = Convert.ToDateTime(drData["ExpenseDate"]);
                        objParty.LedgerID = Convert.ToInt32(drData["FK_PartyID"]);
                        objParty.LedgerName = Convert.ToString(drData["PartyName"]);
                        objExpense.Party = objParty;
                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objExpense.Bank = objBank;
                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objExpense.Tax = objTax;
                        objExpense.ReceiptModeID = Convert.ToInt16(drData["FK_ReceiptModeID"]);
                        objExpense.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objExpense.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objExpense.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objExpense.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objExpense.AdditionalDiscountAmount = Convert.ToDecimal(drData["AdditionalDiscountAmount"]);
                        objExpense.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objExpense.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objExpense.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objExpense.sBillDate = objExpense.BillDate.ToString("dd/MM/yyyy");
                        objExpense.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objExpense.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objExpense.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objExpense.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objExpense.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        if (drData["IssueDate"] != System.DBNull.Value)
                        {
                            objExpense.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                            objExpense.sIssueDate = objExpense.IssueDate.ToString("dd/MM/yyyy");
                        }
                        objExpense.Narration = Convert.ToString(drData["Narration"]);
                        objExpense.Status = Convert.ToString(drData["Status"]);
                        objExpense.GSTIN = Convert.ToString(drData["GSTIN"]);
                        objExpense.BillNo = Convert.ToString(drData["BillNo"]);
                        objExpense.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objExpense.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objExpense.sExpenseDate = objExpense.ExpenseDate.ToString("dd/MM/yyyy") + " " + objExpense.CreatedOn.ToString("h:mm");

                        objList.Add(objExpense);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Expense GetExpense | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Expense> GetAdjustTDSExpense(int ipatientID = 0,int iTDSID=0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Expense> objList = new Collection<Entity.Billing.Expense>();
            Entity.Billing.Expense objExpense;
            Entity.Ledger objParty; Entity.Ledger objBank; Entity.Billing.Tax objTax;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ADJUSTTDSEXPENSEENTRY);
                db.AddInParameter(cmd, "@FK_PartyID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@PK_ExpenseID", DbType.Int32, iTDSID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objExpense = new Entity.Billing.Expense();
                        objParty = new Entity.Ledger();
                        objBank = new Entity.Ledger();
                        objTax = new Entity.Billing.Tax();

                        objExpense.ExpenseID = Convert.ToInt32(drData["PK_ExpenseID"]);
                        objExpense.ExpenseNo = Convert.ToString(drData["ExpenseNo"]);
                        objExpense.ExpenseDate = Convert.ToDateTime(drData["ExpenseDate"]);
                        objParty.LedgerID = Convert.ToInt32(drData["FK_PartyID"]);
                        objParty.LedgerName = Convert.ToString(drData["PartyName"]);
                        objExpense.Party = objParty;
                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objExpense.Bank = objBank;
                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objExpense.Tax = objTax;
                        objExpense.ReceiptModeID = Convert.ToInt16(drData["FK_ReceiptModeID"]);
                        objExpense.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objExpense.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objExpense.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objExpense.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objExpense.AdditionalDiscountAmount = Convert.ToDecimal(drData["AdditionalDiscountAmount"]);
                        objExpense.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objExpense.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objExpense.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objExpense.sBillDate = objExpense.BillDate.ToString("dd/MM/yyyy");
                        objExpense.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objExpense.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objExpense.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objExpense.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objExpense.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        if (drData["IssueDate"] != System.DBNull.Value)
                        {
                            objExpense.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                            objExpense.sIssueDate = objExpense.IssueDate.ToString("dd/MM/yyyy");
                        }
                        objExpense.Narration = Convert.ToString(drData["Narration"]);
                        objExpense.Status = Convert.ToString(drData["Status"]);
                        objExpense.GSTIN = Convert.ToString(drData["GSTIN"]);
                        objExpense.BillNo = Convert.ToString(drData["BillNo"]);
                        objExpense.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objExpense.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objExpense.sExpenseDate = objExpense.ExpenseDate.ToString("dd/MM/yyyy") + " " + objExpense.CreatedOn.ToString("h:mm");

                        objList.Add(objExpense);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Expense GetExpense | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.Expense> GetTDSExpense(int ipatientID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Expense> objList = new Collection<Entity.Billing.Expense>();
            Entity.Billing.Expense objExpense;
            Entity.Ledger objParty; Entity.Ledger objBank; Entity.Billing.Tax objTax;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TDSEXPENSEENTRY);
                db.AddInParameter(cmd, "@FK_PartyID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objExpense = new Entity.Billing.Expense();
                        objParty = new Entity.Ledger();
                        objBank = new Entity.Ledger();
                        objTax = new Entity.Billing.Tax();

                        objExpense.ExpenseID = Convert.ToInt32(drData["PK_ExpenseID"]);
                        objExpense.ExpenseNo = Convert.ToString(drData["ExpenseNo"]);
                        objExpense.ExpenseDate = Convert.ToDateTime(drData["ExpenseDate"]);
                        objParty.LedgerID = Convert.ToInt32(drData["FK_PartyID"]);
                        objParty.LedgerName = Convert.ToString(drData["PartyName"]);
                        objExpense.Party = objParty;
                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objExpense.Bank = objBank;
                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objExpense.Tax = objTax;
                        objExpense.ReceiptModeID = Convert.ToInt16(drData["FK_ReceiptModeID"]);
                        objExpense.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objExpense.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objExpense.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objExpense.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objExpense.AdditionalDiscountAmount = Convert.ToDecimal(drData["AdditionalDiscountAmount"]);
                        objExpense.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objExpense.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objExpense.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objExpense.sBillDate = objExpense.BillDate.ToString("dd/MM/yyyy");
                        objExpense.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objExpense.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objExpense.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objExpense.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objExpense.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        if (drData["IssueDate"] != System.DBNull.Value)
                        {
                            objExpense.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                            objExpense.sIssueDate = objExpense.IssueDate.ToString("dd/MM/yyyy");
                        }
                        objExpense.Narration = Convert.ToString(drData["Narration"]);
                        objExpense.Status = Convert.ToString(drData["Status"]);
                        objExpense.GSTIN = Convert.ToString(drData["GSTIN"]);
                        objExpense.BillNo = Convert.ToString(drData["BillNo"]);
                        objExpense.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objExpense.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objExpense.sExpenseDate = objExpense.ExpenseDate.ToString("dd/MM/yyyy") + " " + objExpense.CreatedOn.ToString("h:mm");

                        objList.Add(objExpense);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Expense GetExpense | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.Expense> GetPendingExpense(int ipatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Expense> objList = new Collection<Entity.Billing.Expense>();
            Entity.Billing.Expense objExpense;
            Entity.Ledger objParty; Entity.Ledger objBank; Entity.Billing.Tax objTax;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PENDINGEXPENSE);
                db.AddInParameter(cmd, "@FK_PartyID", DbType.Int32, ipatientID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objExpense = new Entity.Billing.Expense();
                        objParty = new Entity.Ledger();
                        objBank = new Entity.Ledger();
                        objTax = new Entity.Billing.Tax();

                        objExpense.ExpenseID = Convert.ToInt32(drData["PK_ExpenseID"]);
                        objExpense.ExpenseNo = Convert.ToString(drData["ExpenseNo"]);
                        objExpense.ExpenseDate = Convert.ToDateTime(drData["ExpenseDate"]);
                        objParty.LedgerID = Convert.ToInt32(drData["FK_PartyID"]);
                        objParty.LedgerName = Convert.ToString(drData["PartyName"]);
                        objExpense.Party = objParty;
                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objExpense.Bank = objBank;
                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objExpense.Tax = objTax;
                        objExpense.ReceiptModeID = Convert.ToInt16(drData["FK_ReceiptModeID"]);
                        objExpense.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objExpense.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objExpense.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objExpense.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objExpense.AdditionalDiscountAmount = Convert.ToDecimal(drData["AdditionalDiscountAmount"]);
                        objExpense.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objExpense.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objExpense.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objExpense.TDSAmount = Convert.ToDecimal(drData["TDSAmount"]);
                        objExpense.OnAmount = Convert.ToDecimal(drData["OnAmount"]);
                        objExpense.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objExpense.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objExpense.sBillDate = objExpense.BillDate.ToString("dd/MM/yyyy");
                        objExpense.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objExpense.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objExpense.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        if (drData["IssueDate"] != System.DBNull.Value)
                        {
                            objExpense.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                            objExpense.sIssueDate = objExpense.IssueDate.ToString("dd/MM/yyyy");
                        }
                        objExpense.Narration = Convert.ToString(drData["Narration"]);
                        objExpense.Status = Convert.ToString(drData["Status"]);
                        objExpense.GSTIN = Convert.ToString(drData["GSTIN"]);
                        objExpense.BillNo = Convert.ToString(drData["BillNo"]);
                        objExpense.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objExpense.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objExpense.sExpenseDate = objExpense.ExpenseDate.ToString("dd/MM/yyyy") + " " + objExpense.CreatedOn.ToString("h:mm");

                        objList.Add(objExpense);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Expense GetExpense | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Expense> SearchExpense(string ID, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Expense> objList = new Collection<Entity.Billing.Expense>();
            Entity.Billing.Expense objExpense;
            Entity.Ledger objParty; Entity.Ledger objBank; Entity.Billing.Tax objTax;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_EXPENSEDYNAMIC);
                db.AddInParameter(cmd, "@PK_ExpenseID", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objExpense = new Entity.Billing.Expense();
                        objParty = new Entity.Ledger();
                        objBank = new Entity.Ledger();
                        objTax = new Entity.Billing.Tax();

                        objExpense.ExpenseID = Convert.ToInt32(drData["PK_ExpenseID"]);
                        objExpense.ExpenseNo = Convert.ToString(drData["ExpenseNo"]);
                        objExpense.ExpenseDate = Convert.ToDateTime(drData["ExpenseDate"]);
                        objParty.LedgerID = Convert.ToInt32(drData["FK_PartyID"]);
                        objParty.LedgerName = Convert.ToString(drData["PartyName"]);
                        objExpense.AdditionalDiscountAmount = Convert.ToDecimal(drData["AdditionalDiscountAmount"]);
                        objExpense.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objExpense.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objExpense.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objExpense.sBillDate = objExpense.BillDate.ToString("dd/MM/yyyy");
                        objExpense.Party = objParty;
                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objExpense.Bank = objBank;
                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objExpense.Tax = objTax;
                        objExpense.ReceiptModeID = Convert.ToInt16(drData["FK_ReceiptModeID"]);
                        objExpense.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objExpense.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objExpense.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objExpense.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objExpense.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objExpense.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objExpense.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objExpense.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objExpense.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        if (drData["IssueDate"] != System.DBNull.Value)
                        {
                            objExpense.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                            objExpense.sIssueDate = objExpense.IssueDate.ToString("dd/MM/yyyy");
                        }
                        objExpense.Narration = Convert.ToString(drData["Narration"]);
                        objExpense.Status = Convert.ToString(drData["Status"]);
                        objExpense.GSTIN = Convert.ToString(drData["GSTIN"]);
                        objExpense.BillNo = Convert.ToString(drData["BillNo"]);
                        objExpense.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objExpense.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objExpense.sExpenseDate = objExpense.ExpenseDate.ToString("dd/MM/yyyy") + " " + objExpense.CreatedOn.ToString("h:mm");


                        objList.Add(objExpense);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Expense GetExpense | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Entity.Billing.Expense GetExpenseByID(int iExpenseID, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Expense objExpense = new Entity.Billing.Expense();
            Entity.Ledger objParty; Entity.Ledger objBank; Entity.Billing.Tax objTax;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_EXPENSE);
                db.AddInParameter(cmd, "@PK_ExpenseID", DbType.Int32, iExpenseID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objExpense = new Entity.Billing.Expense();
                        objParty = new Entity.Ledger();
                        objBank = new Entity.Ledger();
                        objTax = new Entity.Billing.Tax();

                        objExpense.ExpenseID = Convert.ToInt32(drData["PK_ExpenseID"]);
                        objExpense.ExpenseNo = Convert.ToString(drData["ExpenseNo"]);
                        objExpense.Description = Convert.ToString(drData["Description"]);
                        objExpense.ExpenseDate = Convert.ToDateTime(drData["ExpenseDate"]);

                        objParty.LedgerID = Convert.ToInt32(drData["FK_PartyID"]);
                        objParty.LedgerName = Convert.ToString(drData["PartyName"]);
                        objExpense.Party = objParty;
                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objExpense.Bank = objBank;
                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objExpense.Tax = objTax;
                        objExpense.ReceiptModeID = Convert.ToInt16(drData["FK_ReceiptModeID"]);
                        objExpense.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objExpense.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objExpense.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objExpense.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objExpense.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objExpense.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objExpense.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objExpense.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objExpense.AdditionalDiscountAmount = Convert.ToDecimal(drData["AdditionalDiscountAmount"]);
                        objExpense.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objExpense.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objExpense.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objExpense.sBillDate = objExpense.BillDate.ToString("dd/MM/yyyy");
                        objExpense.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objExpense.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        if (drData["IssueDate"] != System.DBNull.Value)
                        {
                            objExpense.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                            objExpense.sIssueDate = objExpense.IssueDate.ToString("dd/MM/yyyy");
                        }
                        objExpense.sExpenseDate = objExpense.ExpenseDate.ToString("dd/MM/yyyy");

                        objExpense.Narration = Convert.ToString(drData["Narration"]);
                        objExpense.Status = Convert.ToString(drData["Status"]);
                        objExpense.GSTIN = Convert.ToString(drData["GSTIN"]);
                        objExpense.BillNo = Convert.ToString(drData["BillNo"]);
                        objExpense.ChequeNo = Convert.ToString(drData["ChequeNo"]);

                        objExpense.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objExpense.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objExpense.ImagePath3 = Convert.ToString(drData["ImagePath3"]);

                        objExpense.ExpenseTrans = ExpenseTrans.GetExpenseTransByExpenseID(objExpense.ExpenseID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Expense GetExpenseByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objExpense;
        }

        public static Entity.Billing.Expense GetTDSExpenseByID(int iExpenseID, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Expense objExpense = new Entity.Billing.Expense();
            Entity.Ledger objParty; Entity.Ledger objBank; Entity.Billing.Tax objTax;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TDSEXPENSEBYID);
                db.AddInParameter(cmd, "@PK_ExpenseID", DbType.Int32, iExpenseID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objExpense = new Entity.Billing.Expense();
                        objParty = new Entity.Ledger();
                        objBank = new Entity.Ledger();
                        objTax = new Entity.Billing.Tax();

                        objExpense.ExpenseID = Convert.ToInt32(drData["PK_ExpenseID"]);
                        objExpense.ExpenseNo = Convert.ToString(drData["ExpenseNo"]);
                        objExpense.Description = Convert.ToString(drData["Description"]);
                        objExpense.ExpenseDate = Convert.ToDateTime(drData["ExpenseDate"]);

                        objParty.LedgerID = Convert.ToInt32(drData["FK_PartyID"]);
                        objParty.LedgerName = Convert.ToString(drData["PartyName"]);
                        objExpense.Party = objParty;
                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objExpense.Bank = objBank;
                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objExpense.Tax = objTax;
                        objExpense.ReceiptModeID = Convert.ToInt16(drData["FK_ReceiptModeID"]);
                        objExpense.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objExpense.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objExpense.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objExpense.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objExpense.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objExpense.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objExpense.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objExpense.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objExpense.AdditionalDiscountAmount = Convert.ToDecimal(drData["AdditionalDiscountAmount"]);
                        objExpense.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objExpense.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objExpense.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objExpense.sBillDate = objExpense.BillDate.ToString("dd/MM/yyyy");
                        objExpense.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objExpense.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        if (drData["IssueDate"] != System.DBNull.Value)
                        {
                            objExpense.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                            objExpense.sIssueDate = objExpense.IssueDate.ToString("dd/MM/yyyy");
                        }
                        objExpense.sExpenseDate = objExpense.ExpenseDate.ToString("dd/MM/yyyy");

                        objExpense.Narration = Convert.ToString(drData["Narration"]);
                        objExpense.Status = Convert.ToString(drData["Status"]);
                        objExpense.GSTIN = Convert.ToString(drData["GSTIN"]);
                        objExpense.BillNo = Convert.ToString(drData["BillNo"]);
                        objExpense.ChequeNo = Convert.ToString(drData["ChequeNo"]);

                        objExpense.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objExpense.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objExpense.ImagePath3 = Convert.ToString(drData["ImagePath3"]);

                        objExpense.ExpenseTrans = ExpenseTrans.GetExpenseTransByExpenseID(objExpense.ExpenseID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Expense GetExpenseByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objExpense;
        }
        public static int AddExpense(Entity.Billing.Expense objExpense)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_EXPENSE);
                db.AddOutParameter(cmd, "@PK_ExpenseID", DbType.Int32, objExpense.ExpenseID);
                db.AddInParameter(cmd, "@ExpenseNo", DbType.String, objExpense.ExpenseNo);
                db.AddInParameter(cmd, "@Description", DbType.String, objExpense.Description);
                db.AddInParameter(cmd, "@ExpenseDate", DbType.String, objExpense.sExpenseDate);
                db.AddInParameter(cmd, "@BillDate", DbType.String, objExpense.sBillDate);
                db.AddInParameter(cmd, "@FK_PartyID", DbType.Int32, objExpense.Party.LedgerID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objExpense.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objExpense.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objExpense.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objExpense.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objExpense.IGSTAmount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objExpense.Roundoff);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objExpense.TaxAmount);
                db.AddInParameter(cmd, "@AdditionalDiscountAmount", DbType.Decimal, objExpense.AdditionalDiscountAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objExpense.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objExpense.DiscountAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objExpense.TotalAmount);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objExpense.NetAmount);
                db.AddInParameter(cmd, "@BalanceAmount", DbType.Decimal, objExpense.BalanceAmount);
                db.AddInParameter(cmd, "@PaidAmount", DbType.Decimal, objExpense.PaidAmount);
                db.AddInParameter(cmd, "@Narration", DbType.String, objExpense.Narration);
                db.AddInParameter(cmd, "@GSTIN", DbType.String, objExpense.GSTIN);
                db.AddInParameter(cmd, "@BillNo", DbType.String, objExpense.BillNo);
                db.AddInParameter(cmd, "@ChequeNo", DbType.String, objExpense.ChequeNo);
                db.AddInParameter(cmd, "@Status", DbType.String, objExpense.Status);
                db.AddInParameter(cmd, "@IssueDate", DbType.String, objExpense.sIssueDate);
                db.AddInParameter(cmd, "@FK_ReceiptModeID", DbType.Int16, objExpense.ReceiptModeID);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objExpense.Bank.LedgerID);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objExpense.CreatedBy.UserID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objExpense.FinancialYear.FinancialYearID);

                db.AddInParameter(cmd, "@ImagePath1", DbType.String, objExpense.ImagePath1);
                db.AddInParameter(cmd, "@ImagePath2", DbType.String, objExpense.ImagePath2);
                db.AddInParameter(cmd, "@ImagePath3", DbType.String, objExpense.ImagePath3);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_ExpenseID"));

                foreach (Entity.Billing.ExpenseTrans ObjExpenseTrans in objExpense.ExpenseTrans)
                    ObjExpenseTrans.ExpenseID = iID;

                ExpenseTrans.SaveExpenseTransaction(objExpense.ExpenseTrans);
            }
            catch (Exception ex)
            {
                sException = "Expense AddExpense | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateExpense(Entity.Billing.Expense objExpense)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_EXPENSE);
                db.AddInParameter(cmd, "@PK_ExpenseID", DbType.Int32, objExpense.ExpenseID);
                db.AddInParameter(cmd, "@ExpenseNo", DbType.String, objExpense.ExpenseNo);
                db.AddInParameter(cmd, "@Description", DbType.String, objExpense.Description);
                db.AddInParameter(cmd, "@ExpenseDate", DbType.String, objExpense.sExpenseDate);
                db.AddInParameter(cmd, "@BillDate", DbType.String, objExpense.sBillDate);
                db.AddInParameter(cmd, "@FK_PartyID", DbType.Int32, objExpense.Party.LedgerID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objExpense.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objExpense.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objExpense.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objExpense.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objExpense.IGSTAmount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objExpense.Roundoff);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objExpense.TaxAmount);
                db.AddInParameter(cmd, "@AdditionalDiscountAmount", DbType.Decimal, objExpense.AdditionalDiscountAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objExpense.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objExpense.DiscountAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objExpense.TotalAmount);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objExpense.NetAmount);
                db.AddInParameter(cmd, "@BalanceAmount", DbType.Decimal, objExpense.BalanceAmount);
                db.AddInParameter(cmd, "@PaidAmount", DbType.Decimal, objExpense.PaidAmount);
                db.AddInParameter(cmd, "@Narration", DbType.String, objExpense.Narration);
                db.AddInParameter(cmd, "@GSTIN", DbType.String, objExpense.GSTIN);
                db.AddInParameter(cmd, "@BillNo", DbType.String, objExpense.BillNo);
                db.AddInParameter(cmd, "@ChequeNo", DbType.String, objExpense.ChequeNo);
                db.AddInParameter(cmd, "@Status", DbType.String, objExpense.Status);
                db.AddInParameter(cmd, "@IssueDate", DbType.String, objExpense.sIssueDate);
                db.AddInParameter(cmd, "@FK_ReceiptModeID", DbType.Int16, objExpense.ReceiptModeID);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objExpense.Bank.LedgerID);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objExpense.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objExpense.FinancialYear.FinancialYearID);

                db.AddInParameter(cmd, "@ImagePath1", DbType.String, objExpense.ImagePath1);
                db.AddInParameter(cmd, "@ImagePath2", DbType.String, objExpense.ImagePath2);
                db.AddInParameter(cmd, "@ImagePath3", DbType.String, objExpense.ImagePath3);


                foreach (Entity.Billing.ExpenseTrans ObjExpenseTrans in objExpense.ExpenseTrans)
                    ObjExpenseTrans.ExpenseID = objExpense.ExpenseID;

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                ExpenseTrans.SaveExpenseTransaction(objExpense.ExpenseTrans);
            }
            catch (Exception ex)
            {
                sException = "Expense UpdateExpense| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteExpense(int iExpenseId, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_EXPENSE);
                db.AddInParameter(cmd, "@PK_ExpenseID", DbType.Int32, iExpenseId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Expense DeleteExpense | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        //public static DataSet GetExpenseSummary()
        //{
        //    string sException = string.Empty;
        //    Database db;
        //    DataSet dsList = null;

        //    try
        //    {
        //        db = Entity.DBConnection.dbCon;
        //        DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ExpenseSUMMARY);
        //        dsList = db.ExecuteDataSet(cmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "Expense GetExpenseSummary | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return dsList;
        //}
    }

    public class ExpenseTrans
    {
        public static Collection<Entity.Billing.ExpenseTrans> GetExpenseTransByExpenseID(int iExpenseID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.ExpenseTrans> objList = new Collection<Entity.Billing.ExpenseTrans>();
            Entity.Billing.ExpenseTrans objExpenseTrans = new Entity.Billing.ExpenseTrans();
            Entity.Ledger objProduct; Entity.Billing.Tax objTax;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_EXPENSETRANS);
                db.AddInParameter(cmd, "@FK_ExpenseID", DbType.Int32, iExpenseID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objExpenseTrans = new Entity.Billing.ExpenseTrans();
                        objProduct = new Entity.Ledger();
                        objTax = new Entity.Billing.Tax();
                        objExpenseTrans.ExpenseTransID = Convert.ToInt32(drData["PK_ExpenseTransID"]);
                        objExpenseTrans.ExpenseID = Convert.ToInt32(drData["FK_ExpenseID"]);
                        objExpenseTrans.Amount = Convert.ToDecimal(drData["Amount"]);

                        objProduct.LedgerID = Convert.ToInt32(drData["FK_LedgerID"]);
                        objProduct.LedgerName = Convert.ToString(drData["LedgerName"]);
                        objExpenseTrans.Ledger = objProduct;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxPercentage = Convert.ToDecimal(drData["TaxPercent"]);
                        objTax.SGSTPercent = Convert.ToDecimal(drData["SGSTPercent"]);
                        objTax.IGSTPercent = Convert.ToDecimal(drData["IGSTPercent"]);
                        objTax.CGSTPercent = Convert.ToDecimal(drData["CGSTPercent"]);
                        objExpenseTrans.Tax = objTax;

                        objExpenseTrans.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objExpenseTrans.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objExpenseTrans.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objExpenseTrans.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objExpenseTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objExpenseTrans.Rate = Convert.ToDecimal(drData["Rate"]);
                        objExpenseTrans.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objExpenseTrans.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);

                        objList.Add(objExpenseTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "ExpenseTrans GetExpenseTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveExpenseTransaction(Collection<Entity.Billing.ExpenseTrans> ObjExpenseTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.ExpenseTrans ObjExpenseTransaction in ObjExpenseTransList)
            {
                if (ObjExpenseTransaction.StatusFlag == "I")
                    iID = AddExpenseTrans(ObjExpenseTransaction);
                else if (ObjExpenseTransaction.StatusFlag == "U")
                    bResult = UpdateExpenseTrans(ObjExpenseTransaction);
                else if (ObjExpenseTransaction.StatusFlag == "D")
                    bResult = DeleteExpenseTrans(ObjExpenseTransaction.ExpenseTransID);
            }
        }
        public static int AddExpenseTrans(Entity.Billing.ExpenseTrans objExpenseTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_EXPENSETRANS);
                db.AddOutParameter(cmd, "@PK_ExpenseTransID", DbType.Int32, objExpenseTrans.ExpenseTransID);
                db.AddInParameter(cmd, "@FK_ExpenseID", DbType.Int32, objExpenseTrans.ExpenseID);
                db.AddInParameter(cmd, "@FK_LedgerID", DbType.String, objExpenseTrans.Ledger.LedgerID);
                db.AddInParameter(cmd, "@Amount", DbType.Decimal, objExpenseTrans.Amount);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.String, objExpenseTrans.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objExpenseTrans.Tax.TaxPercentage);
                db.AddInParameter(cmd, "@IGSTPercent", DbType.Decimal, objExpenseTrans.Tax.IGSTPercent);
                db.AddInParameter(cmd, "@SGSTPercent", DbType.Decimal, objExpenseTrans.Tax.SGSTPercent);
                db.AddInParameter(cmd, "@CGSTPercent", DbType.Decimal, objExpenseTrans.Tax.CGSTPercent);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objExpenseTrans.TaxAmount);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objExpenseTrans.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objExpenseTrans.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objExpenseTrans.IGSTAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objExpenseTrans.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objExpenseTrans.DiscountAmount);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objExpenseTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objExpenseTrans.Rate);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_ExpenseTransID"));
            }
            catch (Exception ex)
            {
                sException = "ExpenseTrans AddExpenseTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateExpenseTrans(Entity.Billing.ExpenseTrans objExpenseTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_EXPENSETRANS);
                db.AddInParameter(cmd, "@PK_ExpenseTransID", DbType.Int32, objExpenseTrans.ExpenseTransID);
                db.AddInParameter(cmd, "@FK_ExpenseID", DbType.Int32, objExpenseTrans.ExpenseID);
                db.AddInParameter(cmd, "@FK_LedgerID", DbType.String, objExpenseTrans.Ledger.LedgerID);
                db.AddInParameter(cmd, "@Amount", DbType.Decimal, objExpenseTrans.Amount);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.String, objExpenseTrans.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objExpenseTrans.Tax.TaxPercentage);
                db.AddInParameter(cmd, "@IGSTPercent", DbType.Decimal, objExpenseTrans.Tax.IGSTPercent);
                db.AddInParameter(cmd, "@SGSTPercent", DbType.Decimal, objExpenseTrans.Tax.SGSTPercent);
                db.AddInParameter(cmd, "@CGSTPercent", DbType.Decimal, objExpenseTrans.Tax.CGSTPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objExpenseTrans.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objExpenseTrans.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objExpenseTrans.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objExpenseTrans.TaxAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objExpenseTrans.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objExpenseTrans.DiscountAmount);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objExpenseTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objExpenseTrans.Rate);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "ExpenseTrans UpdateExpenseTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteExpenseTrans(int iExpenseTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_EXPENSETRANS);
                db.AddInParameter(cmd, "@PK_ExpenseTransID", DbType.Int32, iExpenseTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "ExpenseTrans DeleteExpenseTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

    }
}
