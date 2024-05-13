using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections.ObjectModel;
using VHMS;

namespace VHMS.DataAccess
{
    public class Framework
    {
        public static int InsertAuditLog(string sTableName, string sPrimaryKeyField, string sPrimaryKeyValue, char DBAction, int UserID)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbLogCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_AUDITALL);
                db.AddOutParameter(cmd, "@PK_AuditID", DbType.Int32, 0);
                db.AddInParameter(cmd, "@DatabaseName", DbType.String, Entity.DBConnection.DatabaseName);
                db.AddInParameter(cmd, "@TableName", DbType.String, sTableName);
                db.AddInParameter(cmd, "@PrimaryKeyField", DbType.String, sPrimaryKeyField);
                db.AddInParameter(cmd, "@PrimaryKeyValue", DbType.String, sPrimaryKeyValue);
                db.AddInParameter(cmd, "@Action", DbType.String, DBAction);
                db.AddInParameter(cmd, "@UserID", DbType.Int32, UserID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_AuditID"));
            }
            catch (Exception ex)
            {
                sException = "Framework InsertAuditLog | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static DataSet GetDashboard(int UserID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DASHBOARD);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.String, UserID);
                dsData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "Framework GetDashboard | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsData;
        }
        public static DataSet GetRecentAdmission(int UserID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_RECENTADMISSION);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.String, UserID);
                dsData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "Framework GetRecentAdmission | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsData;
        }
    }
}
