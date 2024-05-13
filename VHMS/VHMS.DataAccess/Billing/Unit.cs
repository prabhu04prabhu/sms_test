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
    public class Unit
    {
        public static Collection<Entity.Billing.Unit> GetUnit()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Unit> objList = new Collection<Entity.Billing.Unit>();
            Entity.Billing.Unit objUnit = new Entity.Billing.Unit();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_UNIT);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objUnit = new Entity.Billing.Unit();

                        objUnit.UnitID = Convert.ToInt32(drData["PK_UnitID"]);
                        objUnit.UnitName = Convert.ToString(drData["UnitName"]);
                        objUnit.UnitCode = Convert.ToString(drData["UnitCode"]);
                        objUnit.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objUnit);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Unit GetUnit | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.Unit GetUnitByID(int iUnitID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Unit objUnit = new Entity.Billing.Unit();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_UNIT);
                db.AddInParameter(cmd, "@PK_UnitID", DbType.Int32, iUnitID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objUnit = new Entity.Billing.Unit();
                        objUnit.UnitID = Convert.ToInt32(drData["PK_UnitID"]);
                        objUnit.UnitName = Convert.ToString(drData["UnitName"]);
                        objUnit.UnitCode = Convert.ToString(drData["UnitCode"]);
                        objUnit.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Unit GetUnitByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objUnit;
        }
        public static int AddUnit(Entity.Billing.Unit objUnit)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddUnit(oDb, objUnit, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tUnit", "PK_UnitID", objUnit.UnitID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objUnit.CreatedBy.UserID);
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
        private static int AddUnit(Database oDb, Entity.Billing.Unit objUnit, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_UNIT);
                oDb.AddOutParameter(cmd, "@PK_UnitID", DbType.Int32, objUnit.UnitID);
                oDb.AddInParameter(cmd, "@UnitName", DbType.String, objUnit.UnitName);
                oDb.AddInParameter(cmd, "@UnitCode", DbType.String, objUnit.UnitCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objUnit.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objUnit.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_UnitID"));
                    objUnit.UnitID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Unit AddUnit | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateUnit(Entity.Billing.Unit objUnit)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateUnit(oDb, objUnit, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tUnit", "PK_UnitID", objUnit.UnitID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objUnit.ModifiedBy.UserID);
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
        private static bool UpdateUnit(Database oDb, Entity.Billing.Unit objUnit, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_UNIT);
                oDb.AddInParameter(cmd, "@PK_UnitID", DbType.Int32, objUnit.UnitID);
                oDb.AddInParameter(cmd, "@UnitName", DbType.String, objUnit.UnitName);
                oDb.AddInParameter(cmd, "@UnitCode", DbType.String, objUnit.UnitCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objUnit.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objUnit.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Unit UpdateUnit | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteUnit(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteUnit(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tUnit", "PK_UnitID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteUnit(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_UNIT);
                oDb.AddInParameter(cmd, "@PK_UnitID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Unit DeleteUnit | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
