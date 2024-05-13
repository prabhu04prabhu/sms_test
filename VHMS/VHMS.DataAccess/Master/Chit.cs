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
    public class Chit
    {
        public static Collection<Entity.Chit> GetChit()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Chit> objList = new Collection<Entity.Chit>();
            Entity.Chit objChit = new Entity.Chit();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CHIT);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objChit = new Entity.Chit();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objChit.ChitID = Convert.ToInt32(drData["PK_ChitID"]);
                        objChit.ChitName = Convert.ToString(drData["ChitName"]);
                        objChit.ChitCode = Convert.ToString(drData["ChitCode"]);
                        objChit.Duration = Convert.ToInt32(drData["Duration"]);
                        //objChit.GrandTotal = Convert.ToDecimal(drData["GrandTotal"]);
                        //objChit.BonusAmount = Convert.ToDecimal(drData["BonusAmount"]);
                        //objChit.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        //objChit.InstallmentAmount = Convert.ToDecimal(drData["InstallmentAmount"]);
                        objChit.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objChit);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Chit GetChit | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Chit GetChitByID(int iChitID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Chit objChit = new Entity.Chit();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CHIT);
                db.AddInParameter(cmd, "@PK_ChitID", DbType.Int32, iChitID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objChit = new Entity.Chit();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objChit.ChitID = Convert.ToInt32(drData["PK_ChitID"]);
                        objChit.ChitName = Convert.ToString(drData["ChitName"]);
                        objChit.ChitCode = Convert.ToString(drData["ChitCode"]);
                        objChit.Duration = Convert.ToInt32(drData["Duration"]);
                        //objChit.BonusAmount = Convert.ToDecimal(drData["BonusAmount"]);
                        //objChit.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        //objChit.GrandTotal = Convert.ToDecimal(drData["GrandTotal"]);
                        //objChit.InstallmentAmount = Convert.ToDecimal(drData["InstallmentAmount"]);
                        objChit.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Chit GetChitByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objChit;
        }

       

        public static int AddChit(Entity.Chit objChit)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddChit(oDb, objChit, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tChit", "PK_ChitID", objChit.ChitID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objChit.CreatedBy.UserID);
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
        private static int AddChit(Database oDb, Entity.Chit objChit, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_CHIT);
                oDb.AddOutParameter(cmd, "@PK_ChitID", DbType.Int32, objChit.ChitID);
                oDb.AddInParameter(cmd, "@ChitName", DbType.String, objChit.ChitName);
                oDb.AddInParameter(cmd, "@ChitCode", DbType.String, objChit.ChitCode);
                oDb.AddInParameter(cmd, "@Duration", DbType.Int32, objChit.Duration);
                oDb.AddInParameter(cmd, "@GrandTotal", DbType.Decimal, 0);
                oDb.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, 0);
                oDb.AddInParameter(cmd, "@BonusAmount", DbType.Decimal, 0);
                oDb.AddInParameter(cmd, "@InstallmentAmount", DbType.Decimal, 0);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objChit.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objChit.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_ChitID"));
                    objChit.ChitID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Chit AddChit | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateChit(Entity.Chit objChit)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateChit(oDb, objChit, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tChit", "PK_ChitID", objChit.ChitID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objChit.ModifiedBy.UserID);
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
        private static bool UpdateChit(Database oDb, Entity.Chit objChit, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_CHIT);
                oDb.AddInParameter(cmd, "@PK_ChitID", DbType.Int32, objChit.ChitID);
                oDb.AddInParameter(cmd, "@ChitName", DbType.String, objChit.ChitName);
                oDb.AddInParameter(cmd, "@ChitCode", DbType.String, objChit.ChitCode);
                oDb.AddInParameter(cmd, "@Duration", DbType.Int32, objChit.Duration);
                oDb.AddInParameter(cmd, "@GrandTotal", DbType.Decimal, 0);
                oDb.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, 0);
                oDb.AddInParameter(cmd, "@BonusAmount", DbType.Decimal, 0);
                oDb.AddInParameter(cmd, "@InstallmentAmount", DbType.Decimal, 0);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objChit.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objChit.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Chit UpdateChit | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteChit(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteChit(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tChit", "PK_ChitID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteChit(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_CHIT);
                oDb.AddInParameter(cmd, "@PK_ChitID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Chit DeleteChit | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
