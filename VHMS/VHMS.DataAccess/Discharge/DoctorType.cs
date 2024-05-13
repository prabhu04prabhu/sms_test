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
    public class DoctorType
    {
        public static Collection<Entity.Discharge.DoctorType> GetDoctorType()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Discharge.DoctorType> objList = new Collection<Entity.Discharge.DoctorType>();
            Entity.Discharge.DoctorType objDoctorType = new Entity.Discharge.DoctorType();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DOCTORTYPE);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDoctorType = new Entity.Discharge.DoctorType();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objDoctorType.DoctorTypeID = Convert.ToInt32(drData["PK_DoctorTypeID"]);
                        objDoctorType.DoctorTypeName = Convert.ToString(drData["DoctorTypeName"]);
                        objDoctorType.DoctorTypeCode = Convert.ToString(drData["DoctorTypeCode"]);
                        objDoctorType.Description = Convert.ToString(drData["Description"]);
                        objDoctorType.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objDoctorType);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.DoctorType GetDoctorType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Discharge.DoctorType GetDoctorTypeByID(int iDoctorTypeID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Discharge.DoctorType objDoctorType = new Entity.Discharge.DoctorType();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DOCTORTYPE);
                db.AddInParameter(cmd, "@PK_DoctorTypeID", DbType.Int32, iDoctorTypeID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDoctorType = new Entity.Discharge.DoctorType();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objDoctorType.DoctorTypeID = Convert.ToInt32(drData["PK_DoctorTypeID"]);
                        objDoctorType.DoctorTypeName = Convert.ToString(drData["DoctorTypeName"]);
                        objDoctorType.DoctorTypeCode = Convert.ToString(drData["DoctorTypeCode"]);
                        objDoctorType.Description = Convert.ToString(drData["Description"]);
                        objDoctorType.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.DoctorType GetDoctorTypeByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objDoctorType;
        }
        public static int AddDoctorType(Entity.Discharge.DoctorType objDoctorType)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddDoctorType(oDb, objDoctorType, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tDoctorType", "PK_DoctorTypeID", objDoctorType.DoctorTypeID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objDoctorType.CreatedBy.UserID);
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
        private static int AddDoctorType(Database oDb, Entity.Discharge.DoctorType objDoctorType, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_DOCTORTYPE);
                oDb.AddOutParameter(cmd, "@PK_DoctorTypeID", DbType.Int32, objDoctorType.DoctorTypeID);
                oDb.AddInParameter(cmd, "@DoctorTypeName", DbType.String, objDoctorType.DoctorTypeName);
                oDb.AddInParameter(cmd, "@DoctorTypeCode", DbType.String, objDoctorType.DoctorTypeCode);
                oDb.AddInParameter(cmd, "@Description", DbType.String, objDoctorType.Description);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objDoctorType.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objDoctorType.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_DoctorTypeID"));
                    objDoctorType.DoctorTypeID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.DoctorType AddDoctorType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateDoctorType(Entity.Discharge.DoctorType objDoctorType)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateDoctorType(oDb, objDoctorType, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tDoctorType", "PK_DoctorTypeID", objDoctorType.DoctorTypeID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objDoctorType.ModifiedBy.UserID);
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
        private static bool UpdateDoctorType(Database oDb, Entity.Discharge.DoctorType objDoctorType, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_DOCTORTYPE);
                oDb.AddInParameter(cmd, "@PK_DoctorTypeID", DbType.Int32, objDoctorType.DoctorTypeID);
                oDb.AddInParameter(cmd, "@DoctorTypeName", DbType.String, objDoctorType.DoctorTypeName);
                oDb.AddInParameter(cmd, "@DoctorTypeCode", DbType.String, objDoctorType.DoctorTypeCode);
                oDb.AddInParameter(cmd, "@Description", DbType.String, objDoctorType.Description);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objDoctorType.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objDoctorType.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.DoctorType UpdateDoctorType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteDoctorType(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteDoctorType(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tDoctorType", "PK_DoctorTypeID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteDoctorType(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_DOCTORTYPE);
                oDb.AddInParameter(cmd, "@PK_DoctorTypeID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.DoctorType DeleteDoctorType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
