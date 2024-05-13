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
    public class BankEntry
    {
        public static Collection<Entity.Billing.BankEntry> GetBankEntry()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.BankEntry> objList = new Collection<Entity.Billing.BankEntry>();
            Entity.Billing.BankEntry objBankEntry = new Entity.Billing.BankEntry();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_BANKENTRY);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objBankEntry = new Entity.Billing.BankEntry();

                        objBankEntry.BankEntryID = Convert.ToInt32(drData["PK_BankEntryID"]);
                        objBankEntry.EntryDate = Convert.ToDateTime(drData["EntryDate"]);
                        objBankEntry.sEntryDate = objBankEntry.EntryDate.ToString("dd/MM/yyyy");

                        objBankEntry.CreditID = Convert.ToInt32(drData["FK_CreditID"]);
                        objBankEntry.DebitID = Convert.ToInt32(drData["FK_DebitID"]);
                        objBankEntry.CreditName = Convert.ToString(drData["CreditName"]);
                        objBankEntry.DebitName = Convert.ToString(drData["DebitName"]);
                        objBankEntry.Narration = Convert.ToString(drData["Narration"]);
                        objBankEntry.Amount = Convert.ToDecimal(drData["Amount"]);

                        objList.Add(objBankEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.BankEntry GetBankEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.BankEntry GetBankEntryByID(int iBankEntryID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.BankEntry objBankEntry = new Entity.Billing.BankEntry();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_BANKENTRY);
                db.AddInParameter(cmd, "@PK_BankEntryID", DbType.Int32, iBankEntryID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objBankEntry = new Entity.Billing.BankEntry();
                        objBankEntry.BankEntryID = Convert.ToInt32(drData["PK_BankEntryID"]);
                        objBankEntry.EntryDate = Convert.ToDateTime(drData["EntryDate"]);
                        objBankEntry.sEntryDate = objBankEntry.EntryDate.ToString("dd/MM/yyyy");

                        objBankEntry.CreditID = Convert.ToInt32(drData["FK_CreditID"]);
                        objBankEntry.DebitID = Convert.ToInt32(drData["FK_DebitID"]);
                        objBankEntry.CreditName = Convert.ToString(drData["CreditName"]);
                        objBankEntry.DebitName = Convert.ToString(drData["DebitName"]);
                        objBankEntry.Narration = Convert.ToString(drData["Narration"]);
                        objBankEntry.Amount = Convert.ToDecimal(drData["Amount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.BankEntry GetBankEntryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objBankEntry;
        }
        public static int AddBankEntry(Entity.Billing.BankEntry objBankEntry)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddBankEntry(oDb, objBankEntry, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tBankEntry", "PK_BankEntryID", objBankEntry.BankEntryID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objBankEntry.CreatedBy.UserID);
                }
                catch (Exception ex)
                {
                    oTrans.Rollback();
                    throw ex;
                }
                finally
                {
                    oDbCon.Close();
                }
            }
            return ID;
        }
        private static int AddBankEntry(Database oDb, Entity.Billing.BankEntry objBankEntry, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_BANKENTRY);
                oDb.AddOutParameter(cmd, "@PK_BankEntryID", DbType.Int32, objBankEntry.BankEntryID);
                oDb.AddInParameter(cmd, "@EntryDate", DbType.String, objBankEntry.sEntryDate);
                oDb.AddInParameter(cmd, "@FK_CreditID", DbType.Int32, objBankEntry.CreditID);
                oDb.AddInParameter(cmd, "@FK_DebitID", DbType.Int32, objBankEntry.DebitID);
                oDb.AddInParameter(cmd, "@Narration", DbType.String, objBankEntry.Narration);
                oDb.AddInParameter(cmd, "@Amount", DbType.Decimal, objBankEntry.Amount);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objBankEntry.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_BankEntryID"));
                    objBankEntry.BankEntryID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.BankEntry AddBankEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateBankEntry(Entity.Billing.BankEntry objBankEntry)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateBankEntry(oDb, objBankEntry, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tBankEntry", "PK_BankEntryID", objBankEntry.BankEntryID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objBankEntry.ModifiedBy.UserID);
                }
                catch (Exception ex)
                {
                    oTrans.Rollback();
                    throw ex;
                }
                finally
                {
                    oDbCon.Close();
                }
            }
            return IsUpdated;
        }
        private static bool UpdateBankEntry(Database oDb, Entity.Billing.BankEntry objBankEntry, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_BANKENTRY);
                oDb.AddInParameter(cmd, "@PK_BankEntryID", DbType.Int32, objBankEntry.BankEntryID);
                oDb.AddInParameter(cmd, "@EntryDate", DbType.String, objBankEntry.sEntryDate);
                oDb.AddInParameter(cmd, "@FK_CreditID", DbType.Int32, objBankEntry.CreditID);
                oDb.AddInParameter(cmd, "@FK_DebitID", DbType.Int32, objBankEntry.DebitID);
                oDb.AddInParameter(cmd, "@Narration", DbType.String, objBankEntry.Narration);
                oDb.AddInParameter(cmd, "@Amount", DbType.Decimal, objBankEntry.Amount); 
                
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objBankEntry.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.BankEntry UpdateBankEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteBankEntry(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteBankEntry(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tBankEntry", "PK_BankEntryID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
                }
                catch (Exception ex)
                {
                    oTrans.Rollback();
                    throw ex;
                }
                finally
                {
                    oDbCon.Close();
                }
            }
            return IsDeleted;
        }
        private static bool DeleteBankEntry(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_BANKENTRY);
                oDb.AddInParameter(cmd, "@PK_BankEntryID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.BankEntry DeleteBankEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
