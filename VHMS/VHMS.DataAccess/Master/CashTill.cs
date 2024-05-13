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
    public class CashTill
    {
        public static Collection<Entity.CashTill> GetCashTill()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.CashTill> objList = new Collection<Entity.CashTill>();
            Entity.CashTill objCashTill = new Entity.CashTill();
            Entity.User objCreatedUser; Entity.Branch objBranch;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CASHTILL);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCashTill = new Entity.CashTill();
                        objCreatedUser = new Entity.User();
                        objBranch = new Entity.Branch();

                        objCashTill.CashTillID = Convert.ToInt32(drData["PK_CashTillID"]);
                        objCashTill.TillDate = Convert.ToDateTime(drData["TillDate"]);
                        objCashTill.sTillDate = objCashTill.TillDate.ToString("dd/MM/yyyy");

                        objCashTill.OneRs = Convert.ToInt32(drData["OneRs"]);
                        objCashTill.TwoRs = Convert.ToInt32(drData["TwoRs"]);
                        objCashTill.FiveRs = Convert.ToInt32(drData["FiveRs"]);
                        objCashTill.TenRs = Convert.ToInt32(drData["TenRs"]);
                        objCashTill.TwentyRs = Convert.ToInt32(drData["TwentyRs"]);
                        objCashTill.FiftyRs = Convert.ToInt32(drData["FiftyRs"]);
                        objCashTill.HundredRs = Convert.ToInt32(drData["HundredRs"]);
                        objCashTill.TwoHundredRs = Convert.ToInt32(drData["TwoHundredRs"]);
                        objCashTill.FiveHundredRs = Convert.ToInt32(drData["FiveHundredRs"]);
                        objCashTill.ThousandRs = Convert.ToInt32(drData["ThousandRs"]);
                        objCashTill.TwoThousandRs = Convert.ToInt32(drData["TwoThousandRs"]);
                        objCashTill.CardAmount = Convert.ToDecimal(drData["CardAmount"]);
                        objCashTill.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objCashTill.Notes = Convert.ToString(drData["Notes"]);
                        objCashTill.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objCashTill.Branch = objBranch;

                        objList.Add(objCashTill);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.CashTill GetCashTill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.CashTill> GetTopCashTill( int iBranchID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.CashTill> objList = new Collection<Entity.CashTill>();
            Entity.CashTill objCashTill = new Entity.CashTill();
            Entity.User objCreatedUser; Entity.Branch objBranch;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPCASHTILL);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, iBranchID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCashTill = new Entity.CashTill();
                        objCreatedUser = new Entity.User();
                        objBranch = new Entity.Branch();

                        objCashTill.CashTillID = Convert.ToInt32(drData["PK_CashTillID"]);
                        objCashTill.TillDate = Convert.ToDateTime(drData["TillDate"]);
                        objCashTill.sTillDate = objCashTill.TillDate.ToString("dd/MM/yyyy");

                        objCashTill.OneRs = Convert.ToInt32(drData["OneRs"]);
                        objCashTill.TwoRs = Convert.ToInt32(drData["TwoRs"]);
                        objCashTill.FiveRs = Convert.ToInt32(drData["FiveRs"]);
                        objCashTill.TenRs = Convert.ToInt32(drData["TenRs"]);
                        objCashTill.TwentyRs = Convert.ToInt32(drData["TwentyRs"]);
                        objCashTill.FiftyRs = Convert.ToInt32(drData["FiftyRs"]);
                        objCashTill.HundredRs = Convert.ToInt32(drData["HundredRs"]);
                        objCashTill.TwoHundredRs = Convert.ToInt32(drData["TwoHundredRs"]);
                        objCashTill.FiveHundredRs = Convert.ToInt32(drData["FiveHundredRs"]);
                        objCashTill.ThousandRs = Convert.ToInt32(drData["ThousandRs"]);
                        objCashTill.TwoThousandRs = Convert.ToInt32(drData["TwoThousandRs"]);
                        objCashTill.CardAmount = Convert.ToDecimal(drData["CardAmount"]);
                        objCashTill.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objCashTill.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objCashTill.Branch = objBranch;
                        objCashTill.Notes = Convert.ToString(drData["Notes"]);

                        objList.Add(objCashTill);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.CashTill GetCashTill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.CashTill> SearchCashTill( string ID ,int iBranchID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.CashTill> objList = new Collection<Entity.CashTill>();
            Entity.CashTill objCashTill = new Entity.CashTill();
            Entity.User objCreatedUser; Entity.Branch objBranch;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_CASHTILL);
                db.AddInParameter(cmd, "@key", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, iBranchID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCashTill = new Entity.CashTill();
                        objCreatedUser = new Entity.User();
                        objBranch = new Entity.Branch();

                        objCashTill.CashTillID = Convert.ToInt32(drData["PK_CashTillID"]);
                        objCashTill.TillDate = Convert.ToDateTime(drData["TillDate"]);
                        objCashTill.sTillDate = objCashTill.TillDate.ToString("dd/MM/yyyy");

                        objCashTill.OneRs = Convert.ToInt32(drData["OneRs"]);
                        objCashTill.TwoRs = Convert.ToInt32(drData["TwoRs"]);
                        objCashTill.FiveRs = Convert.ToInt32(drData["FiveRs"]);
                        objCashTill.TenRs = Convert.ToInt32(drData["TenRs"]);
                        objCashTill.TwentyRs = Convert.ToInt32(drData["TwentyRs"]);
                        objCashTill.FiftyRs = Convert.ToInt32(drData["FiftyRs"]);
                        objCashTill.HundredRs = Convert.ToInt32(drData["HundredRs"]);
                        objCashTill.TwoHundredRs = Convert.ToInt32(drData["TwoHundredRs"]);
                        objCashTill.FiveHundredRs = Convert.ToInt32(drData["FiveHundredRs"]);
                        objCashTill.ThousandRs = Convert.ToInt32(drData["ThousandRs"]);
                        objCashTill.TwoThousandRs = Convert.ToInt32(drData["TwoThousandRs"]);
                        objCashTill.CardAmount = Convert.ToDecimal(drData["CardAmount"]);
                        objCashTill.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objCashTill.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objCashTill.Branch = objBranch;
                        objCashTill.Notes = Convert.ToString(drData["Notes"]);

                        objList.Add(objCashTill);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.CashTill GetCashTill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.CashTill GetCashTillByID(int ID)
        {
            string sException = string.Empty;
            Database db;
            Entity.CashTill objCashTill = new Entity.CashTill();
            Entity.User objCreatedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CASHTILL);
                db.AddInParameter(cmd, "@PK_CashTillID", DbType.Int32, ID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCashTill = new Entity.CashTill();
                        objCreatedUser = new Entity.User();

                        objCashTill.CashTillID = Convert.ToInt32(drData["PK_CashTillID"]);
                        objCashTill.TillDate = Convert.ToDateTime(drData["TillDate"]);
                        objCashTill.sTillDate = objCashTill.TillDate.ToString("dd/MM/yyyy");

                        objCashTill.OneRs = Convert.ToInt32(drData["OneRs"]);
                        objCashTill.TwoRs = Convert.ToInt32(drData["TwoRs"]);
                        objCashTill.FiveRs = Convert.ToInt32(drData["FiveRs"]);
                        objCashTill.TenRs = Convert.ToInt32(drData["TenRs"]);
                        objCashTill.TwentyRs = Convert.ToInt32(drData["TwentyRs"]);
                        objCashTill.FiftyRs = Convert.ToInt32(drData["FiftyRs"]);
                        objCashTill.HundredRs = Convert.ToInt32(drData["HundredRs"]);
                        objCashTill.TwoHundredRs = Convert.ToInt32(drData["TwoHundredRs"]);
                        objCashTill.FiveHundredRs = Convert.ToInt32(drData["FiveHundredRs"]);
                        objCashTill.ThousandRs = Convert.ToInt32(drData["ThousandRs"]);
                        objCashTill.TwoThousandRs = Convert.ToInt32(drData["TwoThousandRs"]);
                        objCashTill.CardAmount = Convert.ToDecimal(drData["CardAmount"]);
                        objCashTill.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objCashTill.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objCashTill.Notes = Convert.ToString(drData["Notes"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.CashTill GetCashTillByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objCashTill;
        }
        public static int AddCashTill(Entity.CashTill objCashTill)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddCashTill(oDb, objCashTill, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tCashTill", "PK_CashTillID", objCashTill.CashTillID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objCashTill.CreatedBy.UserID);
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
        private static int AddCashTill(Database oDb, Entity.CashTill objCashTill, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_CASHTILL);
                oDb.AddOutParameter(cmd, "@PK_CashTillID", DbType.Int32, objCashTill.CashTillID);
                oDb.AddInParameter(cmd, "@TillDate", DbType.String, objCashTill.sTillDate);
                oDb.AddInParameter(cmd, "@OneRs", DbType.Int32, objCashTill.OneRs);
                oDb.AddInParameter(cmd, "@TwoRs", DbType.Int32, objCashTill.TwoRs);
                oDb.AddInParameter(cmd, "@FiveRs", DbType.Int32, objCashTill.FiveRs);
                oDb.AddInParameter(cmd, "@TenRs", DbType.Int32, objCashTill.TenRs);
                oDb.AddInParameter(cmd, "@TwentyRs", DbType.Int32, objCashTill.TwentyRs);
                oDb.AddInParameter(cmd, "@FiftyRs", DbType.Int32, objCashTill.FiftyRs);
                oDb.AddInParameter(cmd, "@HundredRs", DbType.Int32, objCashTill.HundredRs);
                oDb.AddInParameter(cmd, "@TwoHundredRs", DbType.Int32, objCashTill.TwoHundredRs);
                oDb.AddInParameter(cmd, "@FiveHundredRs", DbType.Int32, objCashTill.FiveHundredRs);
                oDb.AddInParameter(cmd, "@ThousandRs", DbType.Int32, objCashTill.ThousandRs);
                oDb.AddInParameter(cmd, "@TwoThousandRs", DbType.Int32, objCashTill.TwoThousandRs);
                oDb.AddInParameter(cmd, "@CardAmount", DbType.Decimal, objCashTill.CardAmount);
                oDb.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objCashTill.TotalAmount);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objCashTill.IsActive);
                oDb.AddInParameter(cmd, "@Notes", DbType.String, objCashTill.Notes);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objCashTill.CreatedBy.UserID);
                oDb.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, objCashTill.Branch.BranchID);
                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_CashTillID"));
                    objCashTill.CashTillID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.CashTill AddCashTill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }

        public static bool UpdateCashTill(Entity.CashTill objCashTill)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateCashTill(oDb, objCashTill, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tCashTill", "PK_CashTillID", objCashTill.CashTillID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objCashTill.ModifiedBy.UserID);
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
        private static bool UpdateCashTill(Database oDb, Entity.CashTill objCashTill, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_CASHTILL);
                oDb.AddInParameter(cmd, "@PK_CashTillID", DbType.Int32, objCashTill.CashTillID);
                oDb.AddInParameter(cmd, "@TillDate", DbType.String, objCashTill.sTillDate);
                oDb.AddInParameter(cmd, "@OneRs", DbType.Int32, objCashTill.OneRs);
                oDb.AddInParameter(cmd, "@TwoRs", DbType.Int32, objCashTill.TwoRs);
                oDb.AddInParameter(cmd, "@FiveRs", DbType.Int32, objCashTill.FiveRs);
                oDb.AddInParameter(cmd, "@TenRs", DbType.Int32, objCashTill.TenRs);
                oDb.AddInParameter(cmd, "@TwentyRs", DbType.Int32, objCashTill.TwentyRs);
                oDb.AddInParameter(cmd, "@FiftyRs", DbType.Int32, objCashTill.FiftyRs);
                oDb.AddInParameter(cmd, "@HundredRs", DbType.Int32, objCashTill.HundredRs);
                oDb.AddInParameter(cmd, "@TwoHundredRs", DbType.Int32, objCashTill.TwoHundredRs);
                oDb.AddInParameter(cmd, "@FiveHundredRs", DbType.Int32, objCashTill.FiveHundredRs);
                oDb.AddInParameter(cmd, "@ThousandRs", DbType.Int32, objCashTill.ThousandRs);
                oDb.AddInParameter(cmd, "@TwoThousandRs", DbType.Int32, objCashTill.TwoThousandRs);
                oDb.AddInParameter(cmd, "@CardAmount", DbType.Decimal, objCashTill.CardAmount);
                oDb.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objCashTill.TotalAmount);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objCashTill.IsActive);
                oDb.AddInParameter(cmd, "@Notes", DbType.String, objCashTill.Notes);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objCashTill.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.CashTill UpdateCashTill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteCashTill(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteCashTill(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tCashTill", "PK_CashTillID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteCashTill(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_CASHTILL);
                oDb.AddInParameter(cmd, "@PK_CashTillID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.CashTill DeleteCashTill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

    }
}
