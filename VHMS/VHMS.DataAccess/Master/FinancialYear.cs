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
    public class FinancialYear
    {
        public static Collection<Entity.FinancialYear> GetFinancialYear()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.FinancialYear> objList = new Collection<Entity.FinancialYear>();
            Entity.FinancialYear objFinancialYear = new Entity.FinancialYear();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_FINANCIALYEAR);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objFinancialYear = new Entity.FinancialYear();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();
                        objFinancialYear.FinancialYearID = Convert.ToInt32(drData["PK_FinancialYearID"]);
                        objFinancialYear.Title = Convert.ToString(drData["Title"]);
                        objFinancialYear.YearFrom = Convert.ToDateTime(drData["YearFrom"]);
                        objFinancialYear.sYearFrom = objFinancialYear.YearFrom.ToString("dd/MM/yyyy");
                        objFinancialYear.YearTo = Convert.ToDateTime(drData["YearTo"]);
                        objFinancialYear.sYearTo = objFinancialYear.YearTo.ToString("dd/MM/yyyy");
                        objFinancialYear.Status = Convert.ToString(drData["Status"]);
                        objFinancialYear.Active = Convert.ToBoolean(drData["Active"]);

                        objList.Add(objFinancialYear);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.FinancialYear GetFinancialYear | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.FinancialYear GetFinancialYearByID(int iFinancialYearID)
        {
            string sException = string.Empty;
            Database db;
            Entity.FinancialYear objFinancialYear = new Entity.FinancialYear();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_FINANCIALYEAR);
                db.AddInParameter(cmd, "@PK_FinancialYearID", DbType.Int32, iFinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objFinancialYear = new Entity.FinancialYear();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objFinancialYear.FinancialYearID = Convert.ToInt32(drData["PK_FinancialYearID"]);
                        objFinancialYear.Title = Convert.ToString(drData["Title"]);
                        objFinancialYear.YearFrom = Convert.ToDateTime(drData["YearFrom"]);
                        objFinancialYear.sYearFrom = objFinancialYear.YearFrom.ToString("dd/MM/yyyy");

                        objFinancialYear.YearTo = Convert.ToDateTime(drData["YearTo"]);
                        objFinancialYear.sYearTo = objFinancialYear.YearTo.ToString("dd/MM/yyyy");

                        objFinancialYear.Status = Convert.ToString(drData["Status"]);
                        objFinancialYear.Active = Convert.ToBoolean(drData["Active"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.FinancialYear GetFinancialYearByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objFinancialYear;
        }

    

        public static int AddFinancialYear(Entity.FinancialYear objFinancialYear)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddFinancialYear(oDb, objFinancialYear, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tFinancialYear", "PK_FinancialYearID", objFinancialYear.FinancialYearID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objFinancialYear.CreatedBy.UserID);
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
        private static int AddFinancialYear(Database oDb, Entity.FinancialYear objFinancialYear, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_FINANCIALYEAR);
                oDb.AddOutParameter(cmd, "@PK_FinancialYearID", DbType.Int32, objFinancialYear.FinancialYearID);
                oDb.AddInParameter(cmd, "@Title", DbType.String, objFinancialYear.Title);
                oDb.AddInParameter(cmd, "@YearFrom", DbType.String, objFinancialYear.sYearFrom);
                oDb.AddInParameter(cmd, "@YearTo", DbType.String, objFinancialYear.sYearTo);
                oDb.AddInParameter(cmd, "@Status", DbType.String, objFinancialYear.Status);
                oDb.AddInParameter(cmd, "@Active", DbType.Boolean, objFinancialYear.Active);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objFinancialYear.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_FinancialYearID"));
                    objFinancialYear.FinancialYearID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.FinancialYear AddFinancialYear | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateFinancialYear(Entity.FinancialYear objFinancialYear)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateFinancialYear(oDb, objFinancialYear, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tFinancialYear", "PK_FinancialYearID", objFinancialYear.FinancialYearID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objFinancialYear.ModifiedBy.UserID);
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
        private static bool UpdateFinancialYear(Database oDb, Entity.FinancialYear objFinancialYear, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_FINANCIALYEAR);
                oDb.AddInParameter(cmd, "@PK_FinancialYearID", DbType.Int32, objFinancialYear.FinancialYearID);
                oDb.AddInParameter(cmd, "@Title", DbType.String, objFinancialYear.Title);
                oDb.AddInParameter(cmd, "@YearFrom", DbType.String, objFinancialYear.sYearFrom);
                oDb.AddInParameter(cmd, "@YearTo", DbType.String, objFinancialYear.sYearTo);
                oDb.AddInParameter(cmd, "@Status", DbType.String, objFinancialYear.Status);
                oDb.AddInParameter(cmd, "@Active", DbType.Boolean, objFinancialYear.Active);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objFinancialYear.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.FinancialYear UpdateFinancialYear | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteFinancialYear(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteFinancialYear(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tFinancialYear", "PK_FinancialYearID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteFinancialYear(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_FINANCIALYEAR);
                oDb.AddInParameter(cmd, "@PK_FinancialYearID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.FinancialYear DeleteFinancialYear | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
