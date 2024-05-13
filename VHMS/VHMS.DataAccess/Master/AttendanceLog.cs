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
    public class AttendanceLog
    {
        public static Collection<Entity.AttendanceLog> GetAttendanceLog(int CountryID = 0, string FromDate = "", string ToDate = "",int iEmployeeID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.AttendanceLog> objList = new Collection<Entity.AttendanceLog>();
            Entity.AttendanceLog objAttendanceLog = new Entity.AttendanceLog();
            Entity.User objCreatedUser; Entity.Employee objEmployee;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ATTENDANCELOG);
                db.AddInParameter(cmd, "@PK_AttendanceLogID", DbType.Int32, CountryID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, FromDate);
                db.AddInParameter(cmd, "@DateTo", DbType.String, ToDate);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, iEmployeeID);

                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objEmployee = new Entity.Employee();
                        objAttendanceLog = new Entity.AttendanceLog();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objEmployee.EmployeeID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objAttendanceLog.Employee = objEmployee;

                        objAttendanceLog.AttendanceLogID = Convert.ToInt32(drData["PK_AttendanceLogID"]);
                        objAttendanceLog.PunchDate = Convert.ToDateTime(drData["PunchDate"]);
                        objAttendanceLog.sPunchDate = objAttendanceLog.PunchDate.ToString("dd/MM/yyyy");
                        objAttendanceLog.PunchInTime = Convert.ToString(drData["PunchInTime"]);
                        objAttendanceLog.PunchOutTime = Convert.ToString(drData["PunchOutTime"]);
                        objAttendanceLog.Duration = Convert.ToString(drData["Duration"]);
                        objAttendanceLog.DeductionAmt = Convert.ToDecimal(drData["DeductionAmt"]);
                        objAttendanceLog.LateMinutes = Convert.ToInt32(drData["LateMinutes"]);
                        objAttendanceLog.SpecialStatus = Convert.ToString(drData["SpecialStatus"]);
                        objAttendanceLog.Status = Convert.ToString(drData["Status"]);
                        objAttendanceLog.Active = Convert.ToBoolean(drData["Active"]);
                        objAttendanceLog.OvertimeCount = Convert.ToInt32(drData["OvertimeCount"]);

                        objList.Add(objAttendanceLog);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.AttendanceLog GetAttendanceLog | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.AttendanceLog> GetAttendanceLogByDynamic(string PunchDate = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.AttendanceLog> objList = new Collection<Entity.AttendanceLog>();
            Entity.AttendanceLog objAttendanceLog = new Entity.AttendanceLog();
            Entity.User objCreatedUser; Entity.Employee objEmployee;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_TB_ATTENDANCELOGFINAL_GETDYNAMIC);
                db.AddInParameter(cmd, "@PunchDate", DbType.String, PunchDate);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objEmployee = new Entity.Employee();
                        objAttendanceLog = new Entity.AttendanceLog();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objEmployee.EmployeeID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objAttendanceLog.Employee = objEmployee;

                        objAttendanceLog.AttendanceLogID = Convert.ToInt32(drData["PK_AttendanceLogID"]);
                        objAttendanceLog.PunchDate = Convert.ToDateTime(drData["PunchDate"]);
                        objAttendanceLog.sPunchDate = objAttendanceLog.PunchDate.ToString("dd/MM/yyyy");
                        objAttendanceLog.PunchInTime = Convert.ToString(drData["PunchInTime"]);
                        objAttendanceLog.PunchOutTime = Convert.ToString(drData["PunchOutTime"]);
                        objAttendanceLog.Duration = Convert.ToString(drData["Duration"]);
                        objAttendanceLog.DeductionAmt = Convert.ToDecimal(drData["DeductionAmt"]);
                        objAttendanceLog.LateMinutes = Convert.ToInt32(drData["LateMinutes"]);
                        objAttendanceLog.SpecialStatus = Convert.ToString(drData["SpecialStatus"]);
                        objAttendanceLog.Status = Convert.ToString(drData["Status"]);
                        objAttendanceLog.Active = Convert.ToBoolean(drData["Active"]);
                        objAttendanceLog.OvertimeCount = Convert.ToInt32(drData["OvertimeCount"]);

                        objList.Add(objAttendanceLog);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.AttendanceLog GetAttendanceLog | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.AttendanceLog> GetEmployeeAttendanceLogByID(int EmployeeID = 0, string FromDate = "", string ToDate = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.AttendanceLog> objList = new Collection<Entity.AttendanceLog>();
            Entity.AttendanceLog objAttendanceLog = new Entity.AttendanceLog();
            Entity.User objCreatedUser; Entity.Employee objEmployee;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_EMPLOYEEATTENDANCEBYID);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, EmployeeID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, FromDate);
                db.AddInParameter(cmd, "@DateTo", DbType.String, ToDate);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objEmployee = new Entity.Employee();
                        objAttendanceLog = new Entity.AttendanceLog();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objEmployee.EmployeeID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objAttendanceLog.Employee = objEmployee;
                        
                        objAttendanceLog.AttendanceLogID = Convert.ToInt32(drData["PK_AttendanceLogID"]);
                        objAttendanceLog.PunchDate = Convert.ToDateTime(drData["PunchDate"]);
                        objAttendanceLog.sPunchDate = objAttendanceLog.PunchDate.ToString("dd/MM/yyyy");
                        objAttendanceLog.PunchInTime = Convert.ToString(drData["PunchInTime"]);
                        objAttendanceLog.PunchOutTime = Convert.ToString(drData["PunchOutTime"]);
                        objAttendanceLog.Duration = Convert.ToString(drData["Duration"]);
                        objAttendanceLog.DeductionAmt = Convert.ToDecimal(drData["DeductionAmt"]);
                        objAttendanceLog.LateMinutes = Convert.ToInt32(drData["LateMinutes"]);
                        objAttendanceLog.SpecialStatus = Convert.ToString(drData["SpecialStatus"]);
                        objAttendanceLog.Status = Convert.ToString(drData["Status"]);
                        objAttendanceLog.Active = Convert.ToBoolean(drData["Active"]);
                        objAttendanceLog.OvertimeCount = Convert.ToInt32(drData["OvertimeCount"]);
          
                        objList.Add(objAttendanceLog);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.AttendanceLog GetAttendanceLog | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        //public static Entity.AttendanceLog GetEmployeeAttendanceLogByID(string FromDate = "", string ToDate = "", int EmployeeID = 0)

        //{
        //    string sException = string.Empty;
        //    Database db;
        //    Entity.AttendanceLog objAttendanceLog = new Entity.AttendanceLog();
        //    Entity.User objCreatedUser; Entity.Employee objEmployee;
        //    Entity.User objModifiedUser; Entity.Shift objShift;

        //    try
        //    {
        //        db = Entity.DBConnection.dbCon;
        //        DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_EMPLOYEEATTENDANCEBYID);
        //        db.AddInParameter(cmd, "@DateFrom", DbType.String, FromDate);
        //        db.AddInParameter(cmd, "@DateTo", DbType.String, ToDate);
        //        db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, EmployeeID);
        //        DataSet dsList = db.ExecuteDataSet(cmd);

        //        if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow drData in dsList.Tables[0].Rows)
        //            {
        //                objEmployee = new Entity.Employee();

        //                objAttendanceLog = new Entity.AttendanceLog();
        //                objCreatedUser = new Entity.User();
        //                objModifiedUser = new Entity.User();

        //                objEmployee.EmployeeID = Convert.ToInt32(drData["FK_EmployeeID"]);
        //                objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
        //                objAttendanceLog.Employee = objEmployee;
                        
        //                objAttendanceLog.DeductionAmt = Convert.ToDecimal(drData["DeductionAmt"]);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "VHMS.DataAccess.AttendanceLog GetAttendanceLogByID | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return objAttendanceLog;
        //}
        public static Entity.AttendanceLog GetAttendanceLogByID(int iAttendanceLogID)
        {
            string sException = string.Empty;
            Database db;
            Entity.AttendanceLog objAttendanceLog = new Entity.AttendanceLog();
            Entity.User objCreatedUser; Entity.Employee objEmployee;
            Entity.User objModifiedUser; Entity.Shift objShift;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ATTENDANCELOGBYID);
                db.AddInParameter(cmd, "@PK_AttendanceLogID", DbType.Int32, iAttendanceLogID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objEmployee = new Entity.Employee();

                        objAttendanceLog = new Entity.AttendanceLog();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objEmployee.EmployeeID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objAttendanceLog.Employee = objEmployee;


                        objAttendanceLog.AttendanceLogID = Convert.ToInt32(drData["PK_AttendanceLogID"]);
                        objAttendanceLog.PunchDate = Convert.ToDateTime(drData["PunchDate"]);
                        objAttendanceLog.sPunchDate = objAttendanceLog.PunchDate.ToString("dd/MM/yyyy");
                        objAttendanceLog.PunchInTime = Convert.ToString(drData["PunchInTime"]);
                        objAttendanceLog.PunchOutTime = Convert.ToString(drData["PunchOutTime"]);
                        objAttendanceLog.Duration = Convert.ToString(drData["Duration"]);
                        objAttendanceLog.DeductionAmt = Convert.ToDecimal(drData["DeductionAmt"]);
                        objAttendanceLog.LateMinutes = Convert.ToInt32(drData["LateMinutes"]);
                        objAttendanceLog.SpecialStatus = Convert.ToString(drData["SpecialStatus"]);
                        objAttendanceLog.Status = Convert.ToString(drData["Status"]);
                        objAttendanceLog.Active = Convert.ToBoolean(drData["Active"]);
                        objAttendanceLog.OvertimeCount = Convert.ToInt32(drData["OvertimeCount"]);
                        objAttendanceLog.Edit = Convert.ToInt32(drData["Edit"]);

                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.AttendanceLog GetAttendanceLogByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objAttendanceLog;
        }


        //public static int AddAttendance(Entity.AttendanceLog objAttendanceLog)
        //{
        //    int ID = 0;
        //    Database oDb = Entity.DBConnection.dbCon;
        //    using (DbConnection oDbCon = oDb.CreateConnection())
        //    {
        //        oDbCon.Open();
        //        DbTransaction oTrans = oDbCon.BeginTransaction();
        //        try
        //        {
        //            ID = AddAttendanceLog(oDb, objAttendanceLog);
        //            oTrans.Commit();
        //            if (ID > 0)
        //                Framework.InsertAuditLog("tAttendanceLog", "PK_AttendanceLogID", objAttendanceLog.AttendanceLogID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objAttendanceLog.CreatedBy.UserID);
        //        }
        //        catch (Exception ex)
        //        {
        //            oTrans.Rollback();
        //            throw ex;
        //        }
        //        finally
        //        {
        //            oDbCon.Close();
        //        }
        //    }
        //    return ID;
        //}
   
        public static int AddAttendanceLog(Entity.AttendanceLog objPurchase)
        {
            string sException = string.Empty;
            int iID = 0;
            Database oDb;
            try
            {
                oDb = Entity.DBConnection.dbCon;
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_ATTENDANCELOG);
                oDb.AddOutParameter(cmd, "@PK_AttendanceLogID", DbType.Int32, objPurchase.AttendanceLogID);
                oDb.AddInParameter(cmd, "@FK_EmployeeID", DbType.String, objPurchase.Employee.EmployeeID);
                oDb.AddInParameter(cmd, "@PunchDate", DbType.String, objPurchase.sPunchDate);
                oDb.AddInParameter(cmd, "@PunchInTime", DbType.String, objPurchase.PunchInTime);
                oDb.AddInParameter(cmd, "@PunchOutTime", DbType.String, objPurchase.PunchOutTime);
                oDb.AddInParameter(cmd, "@Duration", DbType.String, objPurchase.Duration);
                oDb.AddInParameter(cmd, "@DeductionAmt", DbType.Decimal, objPurchase.DeductionAmt);
                oDb.AddInParameter(cmd, "@LateMinutes", DbType.Int32, objPurchase.LateMinutes);
                oDb.AddInParameter(cmd, "@SpecialStatus", DbType.String, objPurchase.SpecialStatus);
                oDb.AddInParameter(cmd, "@Status", DbType.String, objPurchase.Status);
                oDb.AddInParameter(cmd, "@Active", DbType.Boolean, objPurchase.Active);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objPurchase.CreatedBy.UserID);
                oDb.AddInParameter(cmd, "@OvertimeCount", DbType.Int32, objPurchase.OvertimeCount);
                oDb.AddInParameter(cmd, "@Edit", DbType.Int32, objPurchase.Edit);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_AttendanceLogID"));

            }
            catch (Exception ex)
            {
                sException = "AttendanceLog AddAttendanceLog | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }

        public static bool UpdateAttendanceLog(Entity.AttendanceLog objAttendanceLog)
        {
            string sException = string.Empty;
            int iID = 0; bool bResult = false;
            Database oDb;
            try
            {
                oDb = Entity.DBConnection.dbCon;
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_ATTENDANCELOG);
                oDb.AddInParameter(cmd, "@PK_AttendanceLogID", DbType.Int32, objAttendanceLog.AttendanceLogID);
                oDb.AddInParameter(cmd, "@FK_EmployeeID", DbType.String, objAttendanceLog.Employee.EmployeeID);
                oDb.AddInParameter(cmd, "@PunchDate", DbType.String, objAttendanceLog.sPunchDate);
                oDb.AddInParameter(cmd, "@PunchInTime", DbType.String, objAttendanceLog.PunchInTime);
                oDb.AddInParameter(cmd, "@PunchOutTime", DbType.String, objAttendanceLog.PunchOutTime);
                oDb.AddInParameter(cmd, "@Duration", DbType.String, objAttendanceLog.Duration);
                oDb.AddInParameter(cmd, "@DeductionAmt", DbType.Decimal, objAttendanceLog.DeductionAmt);
                oDb.AddInParameter(cmd, "@LateMinutes", DbType.Int32, objAttendanceLog.LateMinutes);
                oDb.AddInParameter(cmd, "@SpecialStatus", DbType.String, objAttendanceLog.SpecialStatus);
                oDb.AddInParameter(cmd, "@Status", DbType.String, objAttendanceLog.Status);
                oDb.AddInParameter(cmd, "@Active", DbType.Boolean, objAttendanceLog.Active);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objAttendanceLog.ModifiedBy.UserID);
                oDb.AddInParameter(cmd, "@OvertimeCount", DbType.Int32, objAttendanceLog.OvertimeCount);
                oDb.AddInParameter(cmd, "@Edit", DbType.Int32, objAttendanceLog.Edit);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.AttendanceLog UpdateAttendanceLog | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        //private static bool UpdateAttendanceLog(Database oDb, Entity.AttendanceLog objAttendanceLog, DbTransaction oTrans)
        //{

        //}
        public static bool DeleteAttendanceLog(int ID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_ATTENDANCELOG);
                db.AddInParameter(cmd, "@PK_AttendanceLogID", DbType.Int32, ID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "AttendanceLog DeleteAttendanceLog | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        //public static bool DeleteAttendanceLog(int ID, int UserID)
        //{
        //    bool IsDeleted = false;
        //    Database oDb = Entity.DBConnection.dbCon;
        //    using (DbConnection oDbCon = oDb.CreateConnection())
        //    {
        //        oDbCon.Open();
        //        DbTransaction oTrans = oDbCon.BeginTransaction();
        //        try
        //        {
        //            IsDeleted = DeleteAttendanceLog(oDb, ID, UserID, oTrans);
        //            oTrans.Commit();

        //            if (IsDeleted) Framework.InsertAuditLog("tAttendanceLog", "PK_AttendanceLogID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
        //        }
        //        catch (Exception ex)
        //        {
        //            oTrans.Rollback();
        //            throw ex;
        //        }
        //        finally
        //        {
        //            oDbCon.Close();
        //        }
        //    }
        //    return IsDeleted;
        //}
        //private static bool DeleteAttendanceLog(Database oDb, int ID, int UserID, DbTransaction oTrans)
        //{
        //    string sException = string.Empty;
        //    int iRemoveId = 0;
        //    bool bResult = false;
        //    try
        //    {
        //        DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_ATTENDANCELOG);
        //        oDb.AddInParameter(cmd, "@PK_AttendanceLogID", DbType.Int32, ID);
        //        iRemoveId = oDb.ExecuteNonQuery(cmd);
        //        if (iRemoveId != 0) bResult = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "VHMS.DataAccess.AttendanceLog DeleteAttendanceLog | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return bResult;
        //}
    }
}
