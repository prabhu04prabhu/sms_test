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
    public class PunchLog
    {
        public static Collection<Entity.PunchLog> GetPunchLog(int CountryID = 0, string FromDate = "", string ToDate = "",int iEmployeeID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.PunchLog> objList = new Collection<Entity.PunchLog>();
            Entity.PunchLog objPunchLog = new Entity.PunchLog();
            Entity.User objCreatedUser; Entity.Employee objEmployee;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PUNCHLOG);
                db.AddInParameter(cmd, "@PK_PunchLogID", DbType.Int32, CountryID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, FromDate);
                db.AddInParameter(cmd, "@DateTo", DbType.String, ToDate);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, iEmployeeID);

                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objEmployee = new Entity.Employee();
                        objPunchLog = new Entity.PunchLog();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objEmployee.EmployeeID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objPunchLog.Employee = objEmployee;
                        objPunchLog.PunchLogID = Convert.ToInt32(drData["PK_PunchLogID"]);
                        objPunchLog.LogDate = Convert.ToDateTime(drData["PunchDate"]);
                        objPunchLog.sLogDate = objPunchLog.LogDate.ToString("dd/MM/yyyy");
                        objPunchLog.LogTime = Convert.ToString(drData["LogTime"]);
                        objList.Add(objPunchLog);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.PunchLog GetPunchLog | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.PunchLog GetPunchLogByID(int iPunchLogID)
        {
            string sException = string.Empty;
            Database db;
            Entity.PunchLog objPunchLog = new Entity.PunchLog();
            Entity.User objCreatedUser; Entity.Employee objEmployee;
            Entity.User objModifiedUser; Entity.Shift objShift;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PUNCHLOGBYID);
                db.AddInParameter(cmd, "@PK_PunchLogID", DbType.Int32, iPunchLogID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objEmployee = new Entity.Employee();

                        objPunchLog = new Entity.PunchLog();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objEmployee.EmployeeID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objPunchLog.Employee = objEmployee;
                        objPunchLog.PunchLogID = Convert.ToInt32(drData["PK_PunchLogID"]);
                        objPunchLog.LogDate = Convert.ToDateTime(drData["PunchDate"]);
                        objPunchLog.sLogDate = objPunchLog.LogDate.ToString("dd/MM/yyyy");
                        objPunchLog.LogTime = Convert.ToString(drData["LogTime"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.PunchLog GetPunchLogByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objPunchLog;
        }
        public static Collection<Entity.PunchLog> GetPunchLogByDynamic(string PunchDate = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.PunchLog> objList = new Collection<Entity.PunchLog>();
            Entity.PunchLog objPunchLog = new Entity.PunchLog();
            Entity.User objCreatedUser; Entity.Employee objEmployee;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_TB_PUNCHLOGFINAL_GETDYNAMIC);
                db.AddInParameter(cmd, "@PunchDate", DbType.String, PunchDate);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objEmployee = new Entity.Employee();
                        objPunchLog = new Entity.PunchLog();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objEmployee.EmployeeID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objPunchLog.Employee = objEmployee;
                        objPunchLog.PunchLogID = Convert.ToInt32(drData["PK_PunchLogID"]);
                        objPunchLog.LogDate = Convert.ToDateTime(drData["PunchDate"]);
                        objPunchLog.sLogDate = objPunchLog.LogDate.ToString("dd/MM/yyyy");
                        objPunchLog.LogTime = Convert.ToString(drData["LogTime"]);
                        objList.Add(objPunchLog);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.PunchLog GetPunchLog | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static int AddPunchLog(Entity.PunchLog objPurchase)
        {
            string sException = string.Empty;
            int iID = 0;
            Database oDb;
            try
            {
                oDb = Entity.DBConnection.dbCon;
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_PUNCHLOG);
                oDb.AddOutParameter(cmd, "@PK_PunchLogID", DbType.Int32, objPurchase.PunchLogID);
                oDb.AddInParameter(cmd, "@FK_EmployeeID", DbType.String, objPurchase.Employee.EmployeeID);
                oDb.AddInParameter(cmd, "@LogDate", DbType.DateTime, objPurchase.sLogDate);
                oDb.AddInParameter(cmd, "@LogTime", DbType.String, objPurchase.LogTime);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objPurchase.CreatedBy.UserID);
                iID = oDb.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                sException = "PunchLog AddPunchLog | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
    }
}
