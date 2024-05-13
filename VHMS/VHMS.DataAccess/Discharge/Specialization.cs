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
    public class Specialization
    {
        public static Collection<Entity.Discharge.Specialization> GetSpecialization()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Discharge.Specialization> objList = new Collection<Entity.Discharge.Specialization>();
            Entity.Discharge.Specialization objSpecialization = new Entity.Discharge.Specialization();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SPECIALIZATION);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSpecialization = new Entity.Discharge.Specialization();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objSpecialization.SpecializationID = Convert.ToInt32(drData["PK_SpecializationID"]);
                        objSpecialization.SpecializationName = Convert.ToString(drData["SpecializationName"]);
                        objSpecialization.SpecializationCode = Convert.ToString(drData["SpecializationCode"]);                        
                        objSpecialization.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objSpecialization);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Specialization GetSpecialization | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Discharge.Specialization GetSpecializationByID(int iSpecializationID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Discharge.Specialization objSpecialization = new Entity.Discharge.Specialization();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SPECIALIZATION);
                db.AddInParameter(cmd, "@PK_SpecializationID", DbType.Int32, iSpecializationID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSpecialization = new Entity.Discharge.Specialization();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objSpecialization.SpecializationID = Convert.ToInt32(drData["PK_SpecializationID"]);
                        objSpecialization.SpecializationName = Convert.ToString(drData["SpecializationName"]);
                        objSpecialization.SpecializationCode = Convert.ToString(drData["SpecializationCode"]);                        
                        objSpecialization.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Specialization GetSpecializationByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSpecialization;
        }
        public static int AddSpecialization(Entity.Discharge.Specialization objSpecialization)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddSpecialization(oDb, objSpecialization, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tSpecialization", "PK_SpecializationID", objSpecialization.SpecializationID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objSpecialization.CreatedBy.UserID);
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
        private static int AddSpecialization(Database oDb, Entity.Discharge.Specialization objSpecialization, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_SPECIALIZATION);
                oDb.AddOutParameter(cmd, "@PK_SpecializationID", DbType.Int32, objSpecialization.SpecializationID);
                oDb.AddInParameter(cmd, "@SpecializationName", DbType.String, objSpecialization.SpecializationName);
                oDb.AddInParameter(cmd, "@SpecializationCode", DbType.String, objSpecialization.SpecializationCode);                
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objSpecialization.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objSpecialization.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_SpecializationID"));
                    objSpecialization.SpecializationID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Specialization AddSpecialization | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateSpecialization(Entity.Discharge.Specialization objSpecialization)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateSpecialization(oDb, objSpecialization, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tSpecialization", "PK_SpecializationID", objSpecialization.SpecializationID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objSpecialization.ModifiedBy.UserID);
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
        private static bool UpdateSpecialization(Database oDb, Entity.Discharge.Specialization objSpecialization, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_SPECIALIZATION);
                oDb.AddInParameter(cmd, "@PK_SpecializationID", DbType.Int32, objSpecialization.SpecializationID);
                oDb.AddInParameter(cmd, "@SpecializationName", DbType.String, objSpecialization.SpecializationName);
                oDb.AddInParameter(cmd, "@SpecializationCode", DbType.String, objSpecialization.SpecializationCode);                
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objSpecialization.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objSpecialization.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Specialization UpdateSpecialization | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteSpecialization(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteSpecialization(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tSpecialization", "PK_SpecializationID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteSpecialization(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_SPECIALIZATION);
                oDb.AddInParameter(cmd, "@PK_SpecializationID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Specialization DeleteSpecialization | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
