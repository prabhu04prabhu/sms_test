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
    public class SurgicalProcedure
    {
        public static Collection<Entity.Billing.SurgicalProcedure> GetSurgicalProcedure()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SurgicalProcedure> objList = new Collection<Entity.Billing.SurgicalProcedure>();
            Entity.Billing.SurgicalProcedure objSurgicalProcedure = new Entity.Billing.SurgicalProcedure();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SURGICALPROCEDURE);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSurgicalProcedure = new Entity.Billing.SurgicalProcedure();

                        objSurgicalProcedure.SurgicalProcedureID = Convert.ToInt32(drData["PK_SurgicalProcedureID"]);
                        objSurgicalProcedure.SurgicalProcedureName = Convert.ToString(drData["SurgicalProcedureName"]);
                        objSurgicalProcedure.SurgicalProcedureCode = Convert.ToString(drData["SurgicalProcedureCode"]);
                        objSurgicalProcedure.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objSurgicalProcedure);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.SurgicalProcedure GetSurgicalProcedure | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.SurgicalProcedure GetSurgicalProcedureByID(int iSurgicalProcedureID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.SurgicalProcedure objSurgicalProcedure = new Entity.Billing.SurgicalProcedure();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SURGICALPROCEDURE);
                db.AddInParameter(cmd, "@PK_SurgicalProcedureID", DbType.Int32, iSurgicalProcedureID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSurgicalProcedure = new Entity.Billing.SurgicalProcedure();
                        objSurgicalProcedure.SurgicalProcedureID = Convert.ToInt32(drData["PK_SurgicalProcedureID"]);
                        objSurgicalProcedure.SurgicalProcedureName = Convert.ToString(drData["SurgicalProcedureName"]);
                        objSurgicalProcedure.SurgicalProcedureCode = Convert.ToString(drData["SurgicalProcedureCode"]);
                        objSurgicalProcedure.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.SurgicalProcedure GetSurgicalProcedureByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSurgicalProcedure;
        }
        public static int AddSurgicalProcedure(Entity.Billing.SurgicalProcedure objSurgicalProcedure)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddSurgicalProcedure(oDb, objSurgicalProcedure, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tSurgicalProcedure", "PK_SurgicalProcedureID", objSurgicalProcedure.SurgicalProcedureID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objSurgicalProcedure.CreatedBy.UserID);
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
        private static int AddSurgicalProcedure(Database oDb, Entity.Billing.SurgicalProcedure objSurgicalProcedure, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_SURGICALPROCEDURE);
                oDb.AddOutParameter(cmd, "@PK_SurgicalProcedureID", DbType.Int32, objSurgicalProcedure.SurgicalProcedureID);
                oDb.AddInParameter(cmd, "@SurgicalProcedureName", DbType.String, objSurgicalProcedure.SurgicalProcedureName);
                oDb.AddInParameter(cmd, "@SurgicalProcedureCode", DbType.String, objSurgicalProcedure.SurgicalProcedureCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objSurgicalProcedure.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objSurgicalProcedure.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_SurgicalProcedureID"));
                    objSurgicalProcedure.SurgicalProcedureID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.SurgicalProcedure AddSurgicalProcedure | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateSurgicalProcedure(Entity.Billing.SurgicalProcedure objSurgicalProcedure)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateSurgicalProcedure(oDb, objSurgicalProcedure, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tSurgicalProcedure", "PK_SurgicalProcedureID", objSurgicalProcedure.SurgicalProcedureID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objSurgicalProcedure.ModifiedBy.UserID);
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
        private static bool UpdateSurgicalProcedure(Database oDb, Entity.Billing.SurgicalProcedure objSurgicalProcedure, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_SURGICALPROCEDURE);
                oDb.AddInParameter(cmd, "@PK_SurgicalProcedureID", DbType.Int32, objSurgicalProcedure.SurgicalProcedureID);
                oDb.AddInParameter(cmd, "@SurgicalProcedureName", DbType.String, objSurgicalProcedure.SurgicalProcedureName);
                oDb.AddInParameter(cmd, "@SurgicalProcedureCode", DbType.String, objSurgicalProcedure.SurgicalProcedureCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objSurgicalProcedure.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objSurgicalProcedure.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.SurgicalProcedure UpdateSurgicalProcedure | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteSurgicalProcedure(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteSurgicalProcedure(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tSurgicalProcedure", "PK_SurgicalProcedureID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteSurgicalProcedure(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_SURGICALPROCEDURE);
                oDb.AddInParameter(cmd, "@PK_SurgicalProcedureID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.SurgicalProcedure DeleteSurgicalProcedure | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
