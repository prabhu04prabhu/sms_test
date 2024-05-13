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
    public class Shift
    {
        public static Collection<Entity.Shift> GetShift(int CountryID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Shift> objList = new Collection<Entity.Shift>();
            Entity.Shift objShift = new Entity.Shift();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SHIFT);
                db.AddInParameter(cmd, "@PK_ShiftID", DbType.Int32, CountryID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objShift = new Entity.Shift();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objShift.ShiftID = Convert.ToInt32(drData["PK_ShiftID"]);
                        objShift.ShiftName = Convert.ToString(drData["ShiftName"]);
                        objShift.InTime = Convert.ToString(drData["InTime"]);
                        objShift.OutTime = Convert.ToString(drData["OutTime"]);
                        objShift.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objShift);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Shift GetShift | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Shift GetShiftByID(int iShiftID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Shift objShift = new Entity.Shift();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SHIFT);
                db.AddInParameter(cmd, "@PK_ShiftID", DbType.Int32, iShiftID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objShift = new Entity.Shift();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objShift.ShiftID = Convert.ToInt32(drData["PK_ShiftID"]);
                        objShift.ShiftName = Convert.ToString(drData["ShiftName"]);
                        objShift.InTime = Convert.ToString(drData["InTime"]);
                        objShift.OutTime = Convert.ToString(drData["OutTime"]);
                        objShift.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Shift GetShiftByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objShift;
        }
        public static int AddShift(Entity.Shift objShift)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddShift(oDb, objShift, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tShift", "PK_ShiftID", objShift.ShiftID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objShift.CreatedBy.UserID);
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
        private static int AddShift(Database oDb, Entity.Shift objShift, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_SHIFT);
                oDb.AddOutParameter(cmd, "@PK_ShiftID", DbType.Int32, objShift.ShiftID);
                oDb.AddInParameter(cmd, "@ShiftName", DbType.String, objShift.ShiftName);
                oDb.AddInParameter(cmd, "@InTime", DbType.String, objShift.InTime);
                oDb.AddInParameter(cmd, "@OutTime", DbType.String, objShift.OutTime);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objShift.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objShift.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_ShiftID"));
                    objShift.ShiftID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Shift AddShift | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateShift(Entity.Shift objShift)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateShift(oDb, objShift, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tShift", "PK_ShiftID", objShift.ShiftID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objShift.ModifiedBy.UserID);
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
        private static bool UpdateShift(Database oDb, Entity.Shift objShift, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_SHIFT);
                oDb.AddInParameter(cmd, "@PK_ShiftID", DbType.Int32, objShift.ShiftID);
                oDb.AddInParameter(cmd, "@ShiftName", DbType.String, objShift.ShiftName);
                oDb.AddInParameter(cmd, "@InTime", DbType.String, objShift.InTime);
                oDb.AddInParameter(cmd, "@OutTime", DbType.String, objShift.OutTime);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objShift.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objShift.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Shift UpdateShift | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteShift(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteShift(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tShift", "PK_ShiftID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteShift(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_SHIFT);
                oDb.AddInParameter(cmd, "@PK_ShiftID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Shift DeleteShift | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
