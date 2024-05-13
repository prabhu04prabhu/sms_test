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
    public class Journal
    {
        public static Collection<Entity.Billing.Journal> GetJournal(int ipatientID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Journal> objList = new Collection<Entity.Billing.Journal>();
            Entity.Billing.Journal objJournal; Entity.Ledger objBank;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_JOURNAL);
                db.AddInParameter(cmd, "@PK_JournalID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objJournal = new Entity.Billing.Journal();
                        objBank = new Entity.Ledger();

                        objJournal.JournalID = Convert.ToInt32(drData["PK_JournalID"]);
                        objJournal.JournalNo = Convert.ToString(drData["JournalNo"]);
                        objJournal.JournalDate = Convert.ToDateTime(drData["JournalDate"]);
                        objJournal.Narration = Convert.ToString(drData["Narration"]);
                        objJournal.ReceiptModeID = Convert.ToInt16(drData["FK_ReceiptModeID"]);
                        objJournal.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objJournal.sJournalDate = objJournal.JournalDate.ToString("dd/MM/yyyy") + " " + objJournal.CreatedOn.ToString("h:mm");
                        objJournal.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objJournal.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objJournal.sBillDate = objJournal.BillDate.ToString("dd/MM/yyyy");
                        objJournal.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objJournal.Bank = objBank;
                        objJournal.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        if (drData["IssueDate"] != System.DBNull.Value)
                        {
                            objJournal.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                            objJournal.sIssueDate = objJournal.IssueDate.ToString("dd/MM/yyyy");
                        }

                        objJournal.Status = Convert.ToString(drData["Status"]);
                        objJournal.BillNo = Convert.ToString(drData["BillNo"]);
                        objList.Add(objJournal);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Journal GetJournal | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
               
        public static Collection<Entity.Billing.Journal> SearchJournal(string ID, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Journal> objList = new Collection<Entity.Billing.Journal>();
            Entity.Billing.Journal objJournal; Entity.Ledger objBank;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_JOURNALDYNAMIC);
                db.AddInParameter(cmd, "@PK_JournalID", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objJournal = new Entity.Billing.Journal();
                        objBank = new Entity.Ledger();

                        objJournal.JournalID = Convert.ToInt32(drData["PK_JournalID"]);
                        objJournal.JournalNo = Convert.ToString(drData["JournalNo"]);
                        objJournal.JournalDate = Convert.ToDateTime(drData["JournalDate"]);
                        objJournal.Narration = Convert.ToString(drData["Narration"]);
                        objJournal.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objJournal.ReceiptModeID = Convert.ToInt16(drData["FK_ReceiptModeID"]);
                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objJournal.Bank = objBank;
                        objJournal.sJournalDate = objJournal.JournalDate.ToString("dd/MM/yyyy") + " " + objJournal.CreatedOn.ToString("h:mm");
                        objJournal.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objJournal.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objJournal.sBillDate = objJournal.BillDate.ToString("dd/MM/yyyy");
                        objJournal.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objJournal.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        if (drData["IssueDate"] != System.DBNull.Value)
                        {
                            objJournal.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                            objJournal.sIssueDate = objJournal.IssueDate.ToString("dd/MM/yyyy");
                        }

                        objJournal.Status = Convert.ToString(drData["Status"]);
                        objJournal.BillNo = Convert.ToString(drData["BillNo"]);

                        objList.Add(objJournal);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Journal GetJournal | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
                
        public static Entity.Billing.Journal GetJournalByID(int iJournalID, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Journal objJournal = new Entity.Billing.Journal();
            Entity.Ledger objBank;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_JOURNAL);
                db.AddInParameter(cmd, "@PK_JournalID", DbType.Int32, iJournalID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objJournal = new Entity.Billing.Journal();
                        objBank = new Entity.Ledger();

                        objJournal.JournalID = Convert.ToInt32(drData["PK_JournalID"]);
                        objJournal.JournalNo = Convert.ToString(drData["JournalNo"]);
                        objJournal.JournalDate = Convert.ToDateTime(drData["JournalDate"]);
                        objJournal.Narration = Convert.ToString(drData["Narration"]);
                        objJournal.ReceiptModeID = Convert.ToInt16(drData["FK_ReceiptModeID"]);

                        objJournal.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objJournal.Bank = objBank;
                        objJournal.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objJournal.sBillDate = objJournal.BillDate.ToString("dd/MM/yyyy");
                        objJournal.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objJournal.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        if (drData["IssueDate"] != System.DBNull.Value)
                        {
                            objJournal.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                            objJournal.sIssueDate = objJournal.IssueDate.ToString("dd/MM/yyyy");
                        }

                        objJournal.Status = Convert.ToString(drData["Status"]);
                        objJournal.BillNo = Convert.ToString(drData["BillNo"]);

                        objJournal.sJournalDate = objJournal.JournalDate.ToString("dd/MM/yyyy");


                        objJournal.JournalTrans = JournalTrans.GetJournalTransByJournalID(objJournal.JournalID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Journal GetJournalByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objJournal;
        }
        public static int AddJournal(Entity.Billing.Journal objJournal)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_JOURNAL);
                db.AddOutParameter(cmd, "@PK_JournalID", DbType.Int32, objJournal.JournalID);
                db.AddInParameter(cmd, "@JournalNo", DbType.String, objJournal.JournalNo);
                db.AddInParameter(cmd, "@JournalDate", DbType.String, objJournal.sJournalDate);
                db.AddInParameter(cmd, "@Narration", DbType.String, objJournal.Narration);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objJournal.NetAmount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objJournal.Roundoff);
                db.AddInParameter(cmd, "@BillDate", DbType.String, objJournal.sBillDate);
                db.AddInParameter(cmd, "@BillNo", DbType.String, objJournal.BillNo);
                db.AddInParameter(cmd, "@ChequeNo", DbType.String, objJournal.ChequeNo);
                db.AddInParameter(cmd, "@Status", DbType.String, objJournal.Status);
                db.AddInParameter(cmd, "@IssueDate", DbType.String, objJournal.sIssueDate);
                db.AddInParameter(cmd, "@FK_ReceiptModeID", DbType.Int16, objJournal.ReceiptModeID);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objJournal.Bank.LedgerID);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objJournal.CreatedBy.UserID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objJournal.FinancialYear.FinancialYearID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_JournalID"));

                foreach (Entity.Billing.JournalTrans ObjJournalTrans in objJournal.JournalTrans)
                    ObjJournalTrans.JournalID = iID;

                JournalTrans.SaveJournalTransaction(objJournal.JournalTrans);
            }
            catch (Exception ex)
            {
                sException = "Journal AddJournal | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateJournal(Entity.Billing.Journal objJournal)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_JOURNAL);
                db.AddInParameter(cmd, "@PK_JournalID", DbType.Int32, objJournal.JournalID);
                db.AddInParameter(cmd, "@JournalNo", DbType.String, objJournal.JournalNo);
                db.AddInParameter(cmd, "@JournalDate", DbType.String, objJournal.sJournalDate);
                db.AddInParameter(cmd, "@Narration", DbType.String, objJournal.Narration);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objJournal.NetAmount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objJournal.Roundoff);
                db.AddInParameter(cmd, "@BillDate", DbType.String, objJournal.sBillDate);
                db.AddInParameter(cmd, "@BillNo", DbType.String, objJournal.BillNo);
                db.AddInParameter(cmd, "@ChequeNo", DbType.String, objJournal.ChequeNo);
                db.AddInParameter(cmd, "@Status", DbType.String, objJournal.Status);
                db.AddInParameter(cmd, "@IssueDate", DbType.String, objJournal.sIssueDate);
                db.AddInParameter(cmd, "@FK_ReceiptModeID", DbType.Int16, objJournal.ReceiptModeID);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objJournal.Bank.LedgerID);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objJournal.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objJournal.FinancialYear.FinancialYearID);


                foreach (Entity.Billing.JournalTrans ObjJournalTrans in objJournal.JournalTrans)
                    ObjJournalTrans.JournalID = objJournal.JournalID;

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                JournalTrans.SaveJournalTransaction(objJournal.JournalTrans);
            }
            catch (Exception ex)
            {
                sException = "Journal UpdateJournal| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteJournal(int iJournalId, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_JOURNAL);
                db.AddInParameter(cmd, "@PK_JournalID", DbType.Int32, iJournalId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Journal DeleteJournal | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        //public static DataSet GetJournalSummary()
        //{
        //    string sException = string.Empty;
        //    Database db;
        //    DataSet dsList = null;

        //    try
        //    {
        //        db = Entity.DBConnection.dbCon;
        //        DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_JournalSUMMARY);
        //        dsList = db.ExecuteDataSet(cmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "Journal GetJournalSummary | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return dsList;
        //}
    }

    public class JournalTrans
    {
        public static Collection<Entity.Billing.JournalTrans> GetJournalTransByJournalID(int iJournalID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.JournalTrans> objList = new Collection<Entity.Billing.JournalTrans>();
            Entity.Billing.JournalTrans objJournalTrans = new Entity.Billing.JournalTrans();
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_JOURNALTRANS);
                db.AddInParameter(cmd, "@FK_JournalID", DbType.Int32, iJournalID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objJournalTrans = new Entity.Billing.JournalTrans();

                        objJournalTrans.JournalTransID = Convert.ToInt32(drData["PK_JournalTransID"]);
                        objJournalTrans.JournalID = Convert.ToInt32(drData["FK_JournalID"]);

                        objJournalTrans.LedgerID = Convert.ToInt32(drData["FK_LedgerID"]);
                        objJournalTrans.LedgerName = Convert.ToString(drData["LedgerName"]);
                        objJournalTrans.PartyType = Convert.ToString(drData["PartyType"]);
                        objJournalTrans.Notes = Convert.ToString(drData["Notes"]);
                        objJournalTrans.CreditOrDebit = Convert.ToString(drData["CreditOrDebit"]);
                        objJournalTrans.CreditAmount = Convert.ToDecimal(drData["CreditAmount"]);
                        objJournalTrans.DebitAmount = Convert.ToDecimal(drData["DebitAmount"]);
                      

                        objList.Add(objJournalTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "JournalTrans GetJournalTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveJournalTransaction(Collection<Entity.Billing.JournalTrans> ObjJournalTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.JournalTrans ObjJournalTransaction in ObjJournalTransList)
            {
                if (ObjJournalTransaction.StatusFlag == "I")
                    iID = AddJournalTrans(ObjJournalTransaction);
                else if (ObjJournalTransaction.StatusFlag == "U")
                    bResult = UpdateJournalTrans(ObjJournalTransaction);
                else if (ObjJournalTransaction.StatusFlag == "D")
                    bResult = DeleteJournalTrans(ObjJournalTransaction.JournalTransID);
            }
        }
        public static int AddJournalTrans(Entity.Billing.JournalTrans objJournalTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_JOURNALTRANS);
                db.AddOutParameter(cmd, "@PK_JournalTransID", DbType.Int32, objJournalTrans.JournalTransID);
                db.AddInParameter(cmd, "@FK_JournalID", DbType.Int32, objJournalTrans.JournalID);
                db.AddInParameter(cmd, "@FK_LedgerID", DbType.Int32, objJournalTrans.LedgerID);
                db.AddInParameter(cmd, "@LedgerName", DbType.String, objJournalTrans.LedgerName);
                db.AddInParameter(cmd, "@Notes", DbType.String, objJournalTrans.Notes);
                db.AddInParameter(cmd, "@PartyType", DbType.String, objJournalTrans.PartyType);
                db.AddInParameter(cmd, "@CreditOrDebit", DbType.String, objJournalTrans.CreditOrDebit);
                db.AddInParameter(cmd, "@CreditAmount", DbType.Decimal, objJournalTrans.CreditAmount);
                db.AddInParameter(cmd, "@DebitAmount", DbType.Decimal, objJournalTrans.DebitAmount);
            

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_JournalTransID"));
            }
            catch (Exception ex)
            {
                sException = "JournalTrans AddJournalTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateJournalTrans(Entity.Billing.JournalTrans objJournalTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_JOURNALTRANS);
                db.AddInParameter(cmd, "@PK_JournalTransID", DbType.Int32, objJournalTrans.JournalTransID);
                db.AddInParameter(cmd, "@FK_JournalID", DbType.Int32, objJournalTrans.JournalID);
                db.AddInParameter(cmd, "@FK_LedgerID", DbType.Int32, objJournalTrans.LedgerID);
                db.AddInParameter(cmd, "@LedgerName", DbType.String, objJournalTrans.LedgerName);
                db.AddInParameter(cmd, "@Notes", DbType.String, objJournalTrans.Notes);
                db.AddInParameter(cmd, "@PartyType", DbType.String, objJournalTrans.PartyType);
                db.AddInParameter(cmd, "@CreditOrDebit", DbType.String, objJournalTrans.CreditOrDebit);
                db.AddInParameter(cmd, "@CreditAmount", DbType.Decimal, objJournalTrans.CreditAmount);
                db.AddInParameter(cmd, "@DebitAmount", DbType.Decimal, objJournalTrans.DebitAmount);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "JournalTrans UpdateJournalTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteJournalTrans(int iJournalTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_JOURNALTRANS);
                db.AddInParameter(cmd, "@PK_JournalTransID", DbType.Int32, iJournalTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "JournalTrans DeleteJournalTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

    }
}
