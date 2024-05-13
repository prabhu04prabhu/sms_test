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
    public class Designation
    {
        public static Collection<Entity.Discharge.Designation> GetDesignation()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Discharge.Designation> objList = new Collection<Entity.Discharge.Designation>();
            Entity.Discharge.Designation objDesignation = new Entity.Discharge.Designation();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DESIGNATION);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDesignation = new Entity.Discharge.Designation();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objDesignation.DesignationID = Convert.ToInt32(drData["PK_DesignationID"]);
                        objDesignation.DesignationName = Convert.ToString(drData["DesignationName"]);
                        objDesignation.DesignationCode = Convert.ToString(drData["DesignationCode"]);
                        objDesignation.ShortName = Convert.ToString(drData["ShortName"]);
                        objDesignation.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objDesignation);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Designation GetDesignation | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Discharge.Designation GetDesignationByID(int iDesignationID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Discharge.Designation objDesignation = new Entity.Discharge.Designation();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DESIGNATION);
                db.AddInParameter(cmd, "@PK_DesignationID", DbType.Int32, iDesignationID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDesignation = new Entity.Discharge.Designation();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objDesignation.DesignationID = Convert.ToInt32(drData["PK_DesignationID"]);
                        objDesignation.DesignationName = Convert.ToString(drData["DesignationName"]);
                        objDesignation.DesignationCode = Convert.ToString(drData["DesignationCode"]);
                        objDesignation.ShortName = Convert.ToString(drData["ShortName"]);
                        objDesignation.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Designation GetDesignationByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objDesignation;
        }
        public static int AddDesignation(Entity.Discharge.Designation objDesignation)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddDesignation(oDb, objDesignation, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tDesignation", "PK_DesignationID", objDesignation.DesignationID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objDesignation.CreatedBy.UserID);
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
        private static int AddDesignation(Database oDb, Entity.Discharge.Designation objDesignation, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_DESIGNATION);
                oDb.AddOutParameter(cmd, "@PK_DesignationID", DbType.Int32, objDesignation.DesignationID);
                oDb.AddInParameter(cmd, "@DesignationName", DbType.String, objDesignation.DesignationName);
                oDb.AddInParameter(cmd, "@DesignationCode", DbType.String, objDesignation.DesignationCode);
                oDb.AddInParameter(cmd, "@ShortName", DbType.String, objDesignation.ShortName);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objDesignation.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objDesignation.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_DesignationID"));
                    objDesignation.DesignationID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Designation AddDesignation | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateDesignation(Entity.Discharge.Designation objDesignation)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateDesignation(oDb, objDesignation, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tDesignation", "PK_DesignationID", objDesignation.DesignationID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objDesignation.ModifiedBy.UserID);
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
        private static bool UpdateDesignation(Database oDb, Entity.Discharge.Designation objDesignation, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_DESIGNATION);
                oDb.AddInParameter(cmd, "@PK_DesignationID", DbType.Int32, objDesignation.DesignationID);
                oDb.AddInParameter(cmd, "@DesignationName", DbType.String, objDesignation.DesignationName);
                oDb.AddInParameter(cmd, "@DesignationCode", DbType.String, objDesignation.DesignationCode);
                oDb.AddInParameter(cmd, "@ShortName", DbType.String, objDesignation.ShortName);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objDesignation.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objDesignation.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Designation UpdateDesignation | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteDesignation(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteDesignation(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tDesignation", "PK_DesignationID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteDesignation(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_DESIGNATION);
                oDb.AddInParameter(cmd, "@PK_DesignationID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Designation DeleteDesignation | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
