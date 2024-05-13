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
    public class Anesthesia
    {
        public static Collection<Entity.Billing.Anesthesia> GetAnesthesia()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Anesthesia> objList = new Collection<Entity.Billing.Anesthesia>();
            Entity.Billing.Anesthesia objAnesthesia = new Entity.Billing.Anesthesia();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ANESTHESIA);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objAnesthesia = new Entity.Billing.Anesthesia();

                        objAnesthesia.AnesthesiaID = Convert.ToInt32(drData["PK_AnesthesiaID"]);
                        objAnesthesia.AnesthesiaName = Convert.ToString(drData["AnesthesiaName"]);
                        objAnesthesia.AnesthesiaCode = Convert.ToString(drData["AnesthesiaCode"]);
                        objAnesthesia.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objAnesthesia);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Anesthesia GetAnesthesia | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.Anesthesia GetAnesthesiaByID(int iAnesthesiaID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Anesthesia objAnesthesia = new Entity.Billing.Anesthesia();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ANESTHESIA);
                db.AddInParameter(cmd, "@PK_AnesthesiaID", DbType.Int32, iAnesthesiaID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objAnesthesia = new Entity.Billing.Anesthesia();
                        objAnesthesia.AnesthesiaID = Convert.ToInt32(drData["PK_AnesthesiaID"]);
                        objAnesthesia.AnesthesiaName = Convert.ToString(drData["AnesthesiaName"]);
                        objAnesthesia.AnesthesiaCode = Convert.ToString(drData["AnesthesiaCode"]);
                        objAnesthesia.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Anesthesia GetAnesthesiaByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objAnesthesia;
        }
        public static int AddAnesthesia(Entity.Billing.Anesthesia objAnesthesia)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddAnesthesia(oDb, objAnesthesia, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tAnesthesia", "PK_AnesthesiaID", objAnesthesia.AnesthesiaID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objAnesthesia.CreatedBy.UserID);
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
        private static int AddAnesthesia(Database oDb, Entity.Billing.Anesthesia objAnesthesia, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_ANESTHESIA);
                oDb.AddOutParameter(cmd, "@PK_AnesthesiaID", DbType.Int32, objAnesthesia.AnesthesiaID);
                oDb.AddInParameter(cmd, "@AnesthesiaName", DbType.String, objAnesthesia.AnesthesiaName);
                oDb.AddInParameter(cmd, "@AnesthesiaCode", DbType.String, objAnesthesia.AnesthesiaCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objAnesthesia.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objAnesthesia.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_AnesthesiaID"));
                    objAnesthesia.AnesthesiaID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Anesthesia AddAnesthesia | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateAnesthesia(Entity.Billing.Anesthesia objAnesthesia)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateAnesthesia(oDb, objAnesthesia, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tAnesthesia", "PK_AnesthesiaID", objAnesthesia.AnesthesiaID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objAnesthesia.ModifiedBy.UserID);
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
        private static bool UpdateAnesthesia(Database oDb, Entity.Billing.Anesthesia objAnesthesia, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_ANESTHESIA);
                oDb.AddInParameter(cmd, "@PK_AnesthesiaID", DbType.Int32, objAnesthesia.AnesthesiaID);
                oDb.AddInParameter(cmd, "@AnesthesiaName", DbType.String, objAnesthesia.AnesthesiaName);
                oDb.AddInParameter(cmd, "@AnesthesiaCode", DbType.String, objAnesthesia.AnesthesiaCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objAnesthesia.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objAnesthesia.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Anesthesia UpdateAnesthesia | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteAnesthesia(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteAnesthesia(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tAnesthesia", "PK_AnesthesiaID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteAnesthesia(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_ANESTHESIA);
                oDb.AddInParameter(cmd, "@PK_AnesthesiaID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Anesthesia DeleteAnesthesia | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
