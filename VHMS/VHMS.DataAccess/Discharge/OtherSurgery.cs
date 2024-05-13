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
    public class OtherSurgery
    {
        public static Collection<Entity.Discharge.OtherSurgery> GetOtherSurgery(int DischargeEntryID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Discharge.OtherSurgery> objList = new Collection<Entity.Discharge.OtherSurgery>();
            Entity.Discharge.OtherSurgery objOtherSurgery = new Entity.Discharge.OtherSurgery();
            Entity.Discharge.OtherProcedure oOtherProcedure;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_OTHERSURGERY);
                db.AddInParameter(cmd, "@FK_DischargeEntryID", DbType.Int32, DischargeEntryID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objOtherSurgery = new Entity.Discharge.OtherSurgery();

                        objOtherSurgery.OtherSurgeryID = Convert.ToInt32(drData["PK_OtherSurgeryID"]);
                        objOtherSurgery.DischargeEntryID = Convert.ToInt32(drData["FK_DischargeEntryID"]);
                        objOtherSurgery.OtherSurgeryName = Convert.ToString(drData["SurgeryName"]);
                        objOtherSurgery.OtherSurgeryDate = Convert.ToDateTime(drData["SurgeryDate"]);
                        objOtherSurgery.sOtherSurgeryDate = objOtherSurgery.OtherSurgeryDate.ToString("dd/MM/yyyy");

                        oOtherProcedure = new Entity.Discharge.OtherProcedure();
                        oOtherProcedure.OtherProcedureID = Convert.ToInt32(drData["FK_OtherProcedureID"]);
                        oOtherProcedure.OtherProcedureName = Convert.ToString(drData["OtherProcedureName"]);
                        oOtherProcedure.OtherProcedureDescription = Convert.ToString(drData["DetailProcedure"]);
                        objOtherSurgery.OtherProcedure = oOtherProcedure;

                        objList.Add(objOtherSurgery);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.OtherSurgery GetOtherSurgery | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static bool AddOtherSurgery(Entity.Discharge.OtherSurgery objOtherSurgery)
        {
            bool IsInserted = true;
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddOtherSurgery(oDb, objOtherSurgery, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tOtherSurgery", "PK_OtherSurgeryID", objOtherSurgery.OtherSurgeryID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objOtherSurgery.CreatedBy.UserID);
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
            return IsInserted;
        }
        private static int AddOtherSurgery(Database oDb, Entity.Discharge.OtherSurgery objOtherSurgery, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_OTHERSURGERY);
                oDb.AddOutParameter(cmd, "@PK_OtherSurgeryID", DbType.Int32, objOtherSurgery.OtherSurgeryID);
                oDb.AddInParameter(cmd, "@FK_DischargeEntryID", DbType.Int32, objOtherSurgery.DischargeEntryID);
                oDb.AddInParameter(cmd, "@SurgeryName", DbType.String, objOtherSurgery.OtherSurgeryName);
                oDb.AddInParameter(cmd, "@SurgeryDate", DbType.String, objOtherSurgery.sOtherSurgeryDate);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objOtherSurgery.CreatedBy.UserID);
                //Added on 05-09-2017
                oDb.AddInParameter(cmd, "@FK_OtherProcedureID", DbType.Int32, objOtherSurgery.OtherProcedure.OtherProcedureID);
                oDb.AddInParameter(cmd, "@DetailProcedure", DbType.String, objOtherSurgery.OtherProcedure.OtherProcedureDescription);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_OtherSurgeryID"));
                    objOtherSurgery.OtherSurgeryID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.OtherSurgery AddOtherSurgery | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateOtherSurgery(Entity.Discharge.OtherSurgery objOtherSurgery)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateOtherSurgery(oDb, objOtherSurgery, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tOtherSurgery", "PK_OtherSurgeryID", objOtherSurgery.OtherSurgeryID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objOtherSurgery.ModifiedBy.UserID);
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
        private static bool UpdateOtherSurgery(Database oDb, Entity.Discharge.OtherSurgery objOtherSurgery, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_OTHERSURGERY);
                oDb.AddInParameter(cmd, "@PK_OtherSurgeryID", DbType.Int32, objOtherSurgery.OtherSurgeryID);
                oDb.AddInParameter(cmd, "@FK_DischargeEntryID", DbType.Int32, objOtherSurgery.DischargeEntryID);
                oDb.AddInParameter(cmd, "@SurgeryName", DbType.String, objOtherSurgery.OtherSurgeryName);
                oDb.AddInParameter(cmd, "@SurgeryDate", DbType.String, objOtherSurgery.sOtherSurgeryDate);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objOtherSurgery.ModifiedBy.UserID);
                //Added on 05-09-2017
                oDb.AddInParameter(cmd, "@FK_OtherProcedureID", DbType.Int32, objOtherSurgery.OtherProcedure.OtherProcedureID);
                oDb.AddInParameter(cmd, "@DetailProcedure", DbType.String, objOtherSurgery.OtherProcedure.OtherProcedureDescription);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.OtherSurgery UpdateOtherSurgery | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteOtherSurgery(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteOtherSurgery(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tOtherSurgery", "PK_OtherSurgeryID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteOtherSurgery(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_OTHERSURGERY);
                oDb.AddInParameter(cmd, "@PK_OtherSurgeryID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.OtherSurgery DeleteOtherSurgery | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
