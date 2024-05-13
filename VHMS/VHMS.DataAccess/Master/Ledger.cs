using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections.ObjectModel;
using VHMS;

namespace VHMS.DataAccess
{
    public class Ledger
    {
        public static Collection<Entity.Ledger> GetLedger()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Ledger> objList = new Collection<Entity.Ledger>();
            Entity.Ledger objLedger = new Entity.Ledger();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            Entity.LedgerType objLedgerType;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_LEDGER);
                dsList = db.ExecuteDataSet(cmd);
               
                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objLedger = new Entity.Ledger();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();
                        objLedgerType = new Entity.LedgerType();

                        objLedger.LedgerID = Convert.ToInt32(drData["PK_LedgerID"]);
                        objLedger.LedgerName = Convert.ToString(drData["LedgerName"]);
                        objLedger.OpeningBalanceType = Convert.ToString(drData["OpeningBalanceType"]);
                        objLedger.OpeningBalance = Convert.ToDecimal(drData["OpeningBalance"]);

                        objLedgerType.LedgerTypeID = Convert.ToInt32(drData["FK_LedgerTypeID"]);
                        objLedgerType.LedgerTypeName = Convert.ToString(drData["LedgerTypeName"]);
                        objLedger.LedgerType = objLedgerType;

                        objLedger.IsDefaultRecord = Convert.ToBoolean(drData["IsDefaultRecord"]);
                        objLedger.AddDetail = Convert.ToBoolean(drData["AddDetail"]);
                        objLedger.PhoneNo = Convert.ToString(drData["PhoneNo"]);
                        objLedger.GSTIN = Convert.ToString(drData["GSTIN"]);
                        objLedger.Address = Convert.ToString(drData["Address"]);
                        objLedger.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objLedger);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Ledger GetLedger | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Ledger> GetPartyLedger()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Ledger> objList = new Collection<Entity.Ledger>();
            Entity.Ledger objLedger = new Entity.Ledger();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            Entity.LedgerType objLedgerType;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PARTYLEDGER);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objLedger = new Entity.Ledger();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();
                        objLedgerType = new Entity.LedgerType();

                        objLedger.LedgerID = Convert.ToInt32(drData["PK_LedgerID"]);
                        objLedger.LedgerName = Convert.ToString(drData["LedgerName"]);
                        objLedger.OpeningBalanceType = Convert.ToString(drData["OpeningBalanceType"]);
                        objLedger.OpeningBalance = Convert.ToDecimal(drData["OpeningBalance"]);

                        objLedgerType.LedgerTypeID = Convert.ToInt32(drData["FK_LedgerTypeID"]);
                        objLedgerType.LedgerTypeName = Convert.ToString(drData["LedgerTypeName"]);
                        objLedger.LedgerType = objLedgerType;

                        objLedger.IsDefaultRecord = Convert.ToBoolean(drData["IsDefaultRecord"]);
                        objLedger.AddDetail = Convert.ToBoolean(drData["AddDetail"]);
                        objLedger.PhoneNo = Convert.ToString(drData["PhoneNo"]);
                        objLedger.GSTIN = Convert.ToString(drData["GSTIN"]);
                        objLedger.Address = Convert.ToString(drData["Address"]);
                        objLedger.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objLedger);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Ledger GetLedger | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Ledger> GetExpenseLedger()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Ledger> objList = new Collection<Entity.Ledger>();
            Entity.Ledger objLedger = new Entity.Ledger();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            Entity.LedgerType objLedgerType;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_EXPENSELEDGER);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objLedger = new Entity.Ledger();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();
                        objLedgerType = new Entity.LedgerType();

                        objLedger.LedgerID = Convert.ToInt32(drData["PK_LedgerID"]);
                        objLedger.LedgerName = Convert.ToString(drData["LedgerName"]);
                        objLedger.OpeningBalanceType = Convert.ToString(drData["OpeningBalanceType"]);
                        objLedger.OpeningBalance = Convert.ToDecimal(drData["OpeningBalance"]);

                        objLedgerType.LedgerTypeID = Convert.ToInt32(drData["FK_LedgerTypeID"]);
                        objLedgerType.LedgerTypeName = Convert.ToString(drData["LedgerTypeName"]);
                        objLedger.LedgerType = objLedgerType;

                        objLedger.IsDefaultRecord = Convert.ToBoolean(drData["IsDefaultRecord"]);
                        objLedger.AddDetail = Convert.ToBoolean(drData["AddDetail"]);
                        objLedger.PhoneNo = Convert.ToString(drData["PhoneNo"]);
                        objLedger.GSTIN = Convert.ToString(drData["GSTIN"]);
                        objLedger.Address = Convert.ToString(drData["Address"]);
                        objLedger.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objLedger);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Ledger GetLedger | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Ledger> GetLedgerBank()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Ledger> objList = new Collection<Entity.Ledger>();
            Entity.Ledger objLedger = new Entity.Ledger();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            Entity.LedgerType objLedgerType;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_LEDGERBANK);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        if (Convert.ToBoolean(drData["IsActive"]))
                        {
                            objLedger = new Entity.Ledger();
                            objCreatedUser = new Entity.User();
                            objModifiedUser = new Entity.User();
                            objLedgerType = new Entity.LedgerType();

                            objLedger.LedgerID = Convert.ToInt32(drData["PK_LedgerID"]);
                            objLedger.LedgerName = Convert.ToString(drData["LedgerName"]);
                            objLedger.OpeningBalanceType = Convert.ToString(drData["OpeningBalanceType"]);
                            objLedger.OpeningBalance = Convert.ToDecimal(drData["OpeningBalance"]);

                            objLedgerType.LedgerTypeID = Convert.ToInt32(drData["FK_LedgerTypeID"]);
                            objLedgerType.LedgerTypeName = Convert.ToString(drData["LedgerTypeName"]);
                            objLedger.LedgerType = objLedgerType;
                            
                            objLedger.IsDefaultRecord = Convert.ToBoolean(drData["IsDefaultRecord"]);
                            objLedger.AddDetail = Convert.ToBoolean(drData["AddDetail"]);
                            objLedger.PhoneNo = Convert.ToString(drData["PhoneNo"]);
                            objLedger.GSTIN = Convert.ToString(drData["GSTIN"]);
                            objLedger.Address = Convert.ToString(drData["Address"]);
                            objLedger.IsActive = Convert.ToBoolean(drData["IsActive"]);

                            objList.Add(objLedger);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Ledger GetLedger | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Ledger> GetLedgerExpense()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Ledger> objList = new Collection<Entity.Ledger>();
            Entity.Ledger objLedger = new Entity.Ledger();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            Entity.LedgerType objLedgerType;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_LEDGEREXPENSE);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        if (Convert.ToBoolean(drData["IsActive"]))
                        {
                            objLedger = new Entity.Ledger();
                            objCreatedUser = new Entity.User();
                            objModifiedUser = new Entity.User();
                            objLedgerType = new Entity.LedgerType();

                            objLedger.LedgerID = Convert.ToInt32(drData["PK_LedgerID"]);
                            objLedger.LedgerName = Convert.ToString(drData["LedgerName"]);
                            objLedger.OpeningBalanceType = Convert.ToString(drData["OpeningBalanceType"]);
                            objLedger.OpeningBalance = Convert.ToDecimal(drData["OpeningBalance"]);

                            objLedgerType.LedgerTypeID = Convert.ToInt32(drData["FK_LedgerTypeID"]);
                            objLedgerType.LedgerTypeName = Convert.ToString(drData["LedgerTypeName"]);
                            objLedger.LedgerType = objLedgerType;

                            objLedger.IsDefaultRecord = Convert.ToBoolean(drData["IsDefaultRecord"]);
                            objLedger.AddDetail = Convert.ToBoolean(drData["AddDetail"]);
                            objLedger.PhoneNo = Convert.ToString(drData["PhoneNo"]);
                            objLedger.GSTIN = Convert.ToString(drData["GSTIN"]);
                            objLedger.Address = Convert.ToString(drData["Address"]);
                            objLedger.IsActive = Convert.ToBoolean(drData["IsActive"]);

                            objList.Add(objLedger);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Ledger GetLedger | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }


        public static Entity.Ledger GetLedgerByID(int iLedgerID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Ledger objLedger = new Entity.Ledger();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser; Entity.State objState;
            Entity.LedgerType objLedgerType;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_LEDGER);
                db.AddInParameter(cmd, "@PK_LedgerID", DbType.Int32, iLedgerID);
                DataSet dsList = db.ExecuteDataSet(cmd);
               
                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objLedger = new Entity.Ledger();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();
                        objLedgerType = new Entity.LedgerType();
                        objState = new Entity.State();

                        objLedger.LedgerID = Convert.ToInt32(drData["PK_LedgerID"]);
                        objLedger.LedgerName = Convert.ToString(drData["LedgerName"]);
                        objLedger.OpeningBalanceType = Convert.ToString(drData["OpeningBalanceType"]);
                        objLedger.OpeningBalance = Convert.ToDecimal(drData["OpeningBalance"]);

                        objLedgerType.LedgerTypeID = Convert.ToInt32(drData["FK_LedgerTypeID"]);
                        objLedgerType.LedgerTypeName = Convert.ToString(drData["LedgerTypeName"]);
                        objLedger.LedgerType = objLedgerType;
                        objLedger.PhoneNo = Convert.ToString(drData["PhoneNo"]);
                        objState.StateCode = Convert.ToString(drData["StateCode"]);
                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objLedger.State = objState;
                        objLedger.City = Convert.ToString(drData["City"]);

                        objLedger.IsDefaultRecord = Convert.ToBoolean(drData["IsDefaultRecord"]);
                        objLedger.AddDetail = Convert.ToBoolean(drData["AddDetail"]);
                        objLedger.PhoneNo = Convert.ToString(drData["PhoneNo"]);
                        objLedger.GSTIN = Convert.ToString(drData["GSTIN"]);
                        objLedger.Address = Convert.ToString(drData["Address"]);
                        objLedger.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Ledger GetLedgerByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objLedger;
        }

        public static Collection<Entity.Ledger> GetLedgerByCategoryID(int iLedgerID)
        {
            string sException = string.Empty;
            Database db;
            Collection<Entity.Ledger> objList = new Collection<Entity.Ledger>();
            Entity.Ledger objLedger = new Entity.Ledger();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            Entity.LedgerType objLedgerType;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_LEDGER);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, iLedgerID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objLedger = new Entity.Ledger();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();
                        objLedgerType = new Entity.LedgerType();

                        objLedger.LedgerID = Convert.ToInt32(drData["PK_LedgerID"]);
                        objLedger.LedgerName = Convert.ToString(drData["LedgerName"]);
                        objLedger.OpeningBalanceType = Convert.ToString(drData["OpeningBalanceType"]);
                        objLedger.OpeningBalance = Convert.ToDecimal(drData["OpeningBalance"]);

                        objLedgerType.LedgerTypeID = Convert.ToInt32(drData["FK_LedgerTypeID"]);
                        objLedgerType.LedgerTypeName = Convert.ToString(drData["LedgerTypeName"]);
                        objLedger.LedgerType = objLedgerType;

                        objLedger.IsDefaultRecord = Convert.ToBoolean(drData["IsDefaultRecord"]);
                        objLedger.AddDetail = Convert.ToBoolean(drData["AddDetail"]);
                        objLedger.PhoneNo = Convert.ToString(drData["PhoneNo"]);
                        objLedger.GSTIN = Convert.ToString(drData["GSTIN"]);
                        objLedger.Address = Convert.ToString(drData["Address"]);
                        objLedger.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objLedger);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Ledger GetLedgerByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static int AddLedger(Entity.Ledger objLedger)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddLedger(oDb, objLedger, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tLedger", "PK_LedgerID", objLedger.LedgerID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objLedger.CreatedBy.UserID);
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
        private static int AddLedger(Database oDb, Entity.Ledger objLedger, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_LEDGER);
                oDb.AddOutParameter(cmd, "@PK_LedgerID", DbType.Int32, objLedger.LedgerID);
                oDb.AddInParameter(cmd, "@LedgerName", DbType.String, objLedger.LedgerName);
                oDb.AddInParameter(cmd, "@OpeningBalance", DbType.Decimal, objLedger.OpeningBalance);
                oDb.AddInParameter(cmd, "@OpeningBalanceType", DbType.String, objLedger.OpeningBalanceType);
                oDb.AddInParameter(cmd, "@FK_LedgerTypeID", DbType.Int32, objLedger.LedgerType.LedgerTypeID);
                oDb.AddInParameter(cmd, "@IsDefaultRecord", DbType.Boolean, objLedger.IsDefaultRecord);
                oDb.AddInParameter(cmd, "@AddDetail", DbType.Boolean, objLedger.AddDetail);
                oDb.AddInParameter(cmd, "@PhoneNo", DbType.String, objLedger.PhoneNo);
                oDb.AddInParameter(cmd, "@City", DbType.String, objLedger.City);
                oDb.AddInParameter(cmd, "@FK_StateID", DbType.Int32, objLedger.State.StateID);
                oDb.AddInParameter(cmd, "@GSTIN", DbType.String, objLedger.GSTIN);
                oDb.AddInParameter(cmd, "@Address", DbType.String, objLedger.Address);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objLedger.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objLedger.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_LedgerID"));
                    objLedger.LedgerID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Ledger AddLedger | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateLedger(Entity.Ledger objLedger)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateLedger(oDb, objLedger, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tLedger", "PK_LedgerID", objLedger.LedgerID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objLedger.ModifiedBy.UserID);
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
        private static bool UpdateLedger(Database oDb, Entity.Ledger objLedger, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_LEDGER);
                oDb.AddInParameter(cmd, "@PK_LedgerID", DbType.Int32, objLedger.LedgerID);
                oDb.AddInParameter(cmd, "@LedgerName", DbType.String, objLedger.LedgerName);
                oDb.AddInParameter(cmd, "@OpeningBalance", DbType.Decimal, objLedger.OpeningBalance);
                oDb.AddInParameter(cmd, "@OpeningBalanceType", DbType.String, objLedger.OpeningBalanceType);
                oDb.AddInParameter(cmd, "@FK_LedgerTypeID", DbType.Int32, objLedger.LedgerType.LedgerTypeID);
                oDb.AddInParameter(cmd, "@IsDefaultRecord", DbType.Boolean, objLedger.IsDefaultRecord);
                oDb.AddInParameter(cmd, "@AddDetail", DbType.Boolean, objLedger.AddDetail);
                oDb.AddInParameter(cmd, "@PhoneNo", DbType.String, objLedger.PhoneNo);
                oDb.AddInParameter(cmd, "@City", DbType.String, objLedger.City);
                oDb.AddInParameter(cmd, "@FK_StateID", DbType.Int32, objLedger.State.StateID);
                oDb.AddInParameter(cmd, "@GSTIN", DbType.String, objLedger.GSTIN);
                oDb.AddInParameter(cmd, "@Address", DbType.String, objLedger.Address);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objLedger.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objLedger.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Ledger UpdateLedger | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteLedger(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteLedger(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tLedger", "PK_LedgerID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteLedger(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_LEDGER);
                oDb.AddInParameter(cmd, "@PK_LedgerID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Ledger DeleteLedger | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
