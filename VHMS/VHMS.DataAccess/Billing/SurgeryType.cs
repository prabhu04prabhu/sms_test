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
    public class SurgeryType
    {
        public static Collection<Entity.Billing.SurgeryType> GetSurgeryType()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SurgeryType> objList = new Collection<Entity.Billing.SurgeryType>();
            Entity.Billing.SurgeryType objSurgeryType = new Entity.Billing.SurgeryType();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SURGERYTYPE);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSurgeryType = new Entity.Billing.SurgeryType();

                        objSurgeryType.SurgeryTypeID = Convert.ToInt32(drData["PK_SurgeryTypeID"]);
                        objSurgeryType.SurgeryTypeName = Convert.ToString(drData["SurgeryTypeName"]);
                        objSurgeryType.SurgeryTypeCode = Convert.ToString(drData["SurgeryTypeCode"]);
                        objSurgeryType.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objSurgeryType);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.SurgeryType GetSurgeryType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.SurgeryType GetSurgeryTypeByID(int iSurgeryTypeID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.SurgeryType objSurgeryType = new Entity.Billing.SurgeryType();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SURGERYTYPE);
                db.AddInParameter(cmd, "@PK_SurgeryTypeID", DbType.Int32, iSurgeryTypeID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSurgeryType = new Entity.Billing.SurgeryType();
                        objSurgeryType.SurgeryTypeID = Convert.ToInt32(drData["PK_SurgeryTypeID"]);
                        objSurgeryType.SurgeryTypeName = Convert.ToString(drData["SurgeryTypeName"]);
                        objSurgeryType.SurgeryTypeCode = Convert.ToString(drData["SurgeryTypeCode"]);
                        objSurgeryType.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.SurgeryType GetSurgeryTypeByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSurgeryType;
        }
        public static int AddSurgeryType(Entity.Billing.SurgeryType objSurgeryType)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddSurgeryType(oDb, objSurgeryType, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tSurgeryType", "PK_SurgeryTypeID", objSurgeryType.SurgeryTypeID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objSurgeryType.CreatedBy.UserID);
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
        private static int AddSurgeryType(Database oDb, Entity.Billing.SurgeryType objSurgeryType, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_SURGERYTYPE);
                oDb.AddOutParameter(cmd, "@PK_SurgeryTypeID", DbType.Int32, objSurgeryType.SurgeryTypeID);
                oDb.AddInParameter(cmd, "@SurgeryTypeName", DbType.String, objSurgeryType.SurgeryTypeName);
                oDb.AddInParameter(cmd, "@SurgeryTypeCode", DbType.String, objSurgeryType.SurgeryTypeCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objSurgeryType.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objSurgeryType.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_SurgeryTypeID"));
                    objSurgeryType.SurgeryTypeID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.SurgeryType AddSurgeryType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateSurgeryType(Entity.Billing.SurgeryType objSurgeryType)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateSurgeryType(oDb, objSurgeryType, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tSurgeryType", "PK_SurgeryTypeID", objSurgeryType.SurgeryTypeID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objSurgeryType.ModifiedBy.UserID);
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
        private static bool UpdateSurgeryType(Database oDb, Entity.Billing.SurgeryType objSurgeryType, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_SURGERYTYPE);
                oDb.AddInParameter(cmd, "@PK_SurgeryTypeID", DbType.Int32, objSurgeryType.SurgeryTypeID);
                oDb.AddInParameter(cmd, "@SurgeryTypeName", DbType.String, objSurgeryType.SurgeryTypeName);
                oDb.AddInParameter(cmd, "@SurgeryTypeCode", DbType.String, objSurgeryType.SurgeryTypeCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objSurgeryType.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objSurgeryType.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.SurgeryType UpdateSurgeryType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteSurgeryType(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteSurgeryType(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tSurgeryType", "PK_SurgeryTypeID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteSurgeryType(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_SURGERYTYPE);
                oDb.AddInParameter(cmd, "@PK_SurgeryTypeID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.SurgeryType DeleteSurgeryType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
