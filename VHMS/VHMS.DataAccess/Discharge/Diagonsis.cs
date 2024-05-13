using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections.ObjectModel;
using VHMS;

namespace VHMS.DataAccess.Discharge
{
    public class Diagonsis
    {
        public static Collection<Entity.Discharge.Diagonsis> GetDiagonsis()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Discharge.Diagonsis> objList = new Collection<Entity.Discharge.Diagonsis>();
            Entity.Discharge.Diagonsis objDiagonsis = new Entity.Discharge.Diagonsis();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DIAGONSIS);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDiagonsis = new Entity.Discharge.Diagonsis();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objDiagonsis.DiagonsisID = Convert.ToInt32(drData["PK_DiagonsisID"]);
                        objDiagonsis.DiagonsisName = Convert.ToString(drData["DiagonsisName"]);
                        objDiagonsis.DiagonsisCode = Convert.ToString(drData["DiagonsisCode"]);
                        objDiagonsis.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objDiagonsis);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Diagonsis GetDiagonsis | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Discharge.Diagonsis GetDiagonsisByID(int iDiagonsisID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Discharge.Diagonsis objDiagonsis = new Entity.Discharge.Diagonsis();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DIAGONSIS);
                db.AddInParameter(cmd, "@PK_DiagonsisID", DbType.Int32, iDiagonsisID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDiagonsis = new Entity.Discharge.Diagonsis();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objDiagonsis.DiagonsisID = Convert.ToInt32(drData["PK_DiagonsisID"]);
                        objDiagonsis.DiagonsisName = Convert.ToString(drData["DiagonsisName"]);
                        objDiagonsis.DiagonsisCode = Convert.ToString(drData["DiagonsisCode"]);
                        objDiagonsis.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Diagonsis GetDiagonsisByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objDiagonsis;
        }
        public static int AddDiagonsis(Entity.Discharge.Diagonsis objDiagonsis)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddDiagonsis(oDb, objDiagonsis, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tDiagonsis", "PK_DiagonsisID", objDiagonsis.DiagonsisID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objDiagonsis.CreatedBy.UserID);
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
        private static int AddDiagonsis(Database oDb, Entity.Discharge.Diagonsis objDiagonsis, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_DIAGONSIS);
                oDb.AddOutParameter(cmd, "@PK_DiagonsisID", DbType.Int32, objDiagonsis.DiagonsisID);
                oDb.AddInParameter(cmd, "@DiagonsisName", DbType.String, objDiagonsis.DiagonsisName);
                oDb.AddInParameter(cmd, "@DiagonsisCode", DbType.String, objDiagonsis.DiagonsisCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objDiagonsis.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objDiagonsis.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_DiagonsisID"));
                    objDiagonsis.DiagonsisID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Diagonsis AddDiagonsis | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateDiagonsis(Entity.Discharge.Diagonsis objDiagonsis)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateDiagonsis(oDb, objDiagonsis, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tDiagonsis", "PK_DiagonsisID", objDiagonsis.DiagonsisID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objDiagonsis.ModifiedBy.UserID);
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
        private static bool UpdateDiagonsis(Database oDb, Entity.Discharge.Diagonsis objDiagonsis, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_DIAGONSIS);
                oDb.AddInParameter(cmd, "@PK_DiagonsisID", DbType.Int32, objDiagonsis.DiagonsisID);
                oDb.AddInParameter(cmd, "@DiagonsisName", DbType.String, objDiagonsis.DiagonsisName);
                oDb.AddInParameter(cmd, "@DiagonsisCode", DbType.String, objDiagonsis.DiagonsisCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objDiagonsis.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objDiagonsis.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Diagonsis UpdateDiagonsis | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteDiagonsis(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteDiagonsis(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tDiagonsis", "PK_DiagonsisID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteDiagonsis(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_DIAGONSIS);
                oDb.AddInParameter(cmd, "@PK_DiagonsisID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Diagonsis DeleteDiagonsis | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
