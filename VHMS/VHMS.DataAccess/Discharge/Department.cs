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
    public class Department
    {
        public static Collection<Entity.Discharge.Department> GetDepartment()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Discharge.Department> objList = new Collection<Entity.Discharge.Department>();
            Entity.Discharge.Department objDepartment = new Entity.Discharge.Department();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DEPARTMENT);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDepartment = new Entity.Discharge.Department();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objDepartment.DepartmentID = Convert.ToInt32(drData["PK_DepartmentID"]);
                        objDepartment.DepartmentName = Convert.ToString(drData["DepartmentName"]);
                        objDepartment.DepartmentCode = Convert.ToString(drData["DepartmentCode"]);
                        objDepartment.ShortName = Convert.ToString(drData["ShortName"]);
                        objDepartment.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objDepartment);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Department GetDepartment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Discharge.Department GetDepartmentByID(int iDepartmentID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Discharge.Department objDepartment = new Entity.Discharge.Department();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DEPARTMENT);
                db.AddInParameter(cmd, "@PK_DepartmentID", DbType.Int32, iDepartmentID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDepartment = new Entity.Discharge.Department();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objDepartment.DepartmentID = Convert.ToInt32(drData["PK_DepartmentID"]);
                        objDepartment.DepartmentName = Convert.ToString(drData["DepartmentName"]);
                        objDepartment.DepartmentCode = Convert.ToString(drData["DepartmentCode"]);
                        objDepartment.ShortName = Convert.ToString(drData["ShortName"]);
                        objDepartment.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Department GetDepartmentByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objDepartment;
        }
        public static int AddDepartment(Entity.Discharge.Department objDepartment)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddDepartment(oDb, objDepartment, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tDepartment", "PK_DepartmentID", objDepartment.DepartmentID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objDepartment.CreatedBy.UserID);
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
        private static int AddDepartment(Database oDb, Entity.Discharge.Department objDepartment, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_DEPARTMENT);
                oDb.AddOutParameter(cmd, "@PK_DepartmentID", DbType.Int32, objDepartment.DepartmentID);
                oDb.AddInParameter(cmd, "@DepartmentName", DbType.String, objDepartment.DepartmentName);
                oDb.AddInParameter(cmd, "@DepartmentCode", DbType.String, objDepartment.DepartmentCode);
                oDb.AddInParameter(cmd, "@ShortName", DbType.String, objDepartment.ShortName);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objDepartment.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objDepartment.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_DepartmentID"));
                    objDepartment.DepartmentID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Department AddDepartment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateDepartment(Entity.Discharge.Department objDepartment)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateDepartment(oDb, objDepartment, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tDepartment", "PK_DepartmentID", objDepartment.DepartmentID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objDepartment.ModifiedBy.UserID);
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
        private static bool UpdateDepartment(Database oDb, Entity.Discharge.Department objDepartment, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_DEPARTMENT);
                oDb.AddInParameter(cmd, "@PK_DepartmentID", DbType.Int32, objDepartment.DepartmentID);
                oDb.AddInParameter(cmd, "@DepartmentName", DbType.String, objDepartment.DepartmentName);
                oDb.AddInParameter(cmd, "@DepartmentCode", DbType.String, objDepartment.DepartmentCode);
                oDb.AddInParameter(cmd, "@ShortName", DbType.String, objDepartment.ShortName);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objDepartment.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objDepartment.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Department UpdateDepartment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteDepartment(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteDepartment(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tDepartment", "PK_DepartmentID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteDepartment(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_DEPARTMENT);
                oDb.AddInParameter(cmd, "@PK_DepartmentID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Department DeleteDepartment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
