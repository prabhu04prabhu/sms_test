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
    public class Salary
    {
        public static Collection<Entity.Salary> GetSalary(int CountryID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Salary> objList = new Collection<Entity.Salary>();
            Entity.Salary objSalary = new Entity.Salary();
            Entity.User objCreatedUser; Entity.Employee objEmployee;
            Entity.User objModifiedUser; Entity.Ledger objLedger;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALARY);
                db.AddInParameter(cmd, "@PK_SalaryID", DbType.Int32, 0);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, CountryID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalary = new Entity.Salary();
                        objEmployee = new Entity.Employee();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objEmployee.EmployeeID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objSalary.Employee = objEmployee;

                        objSalary.SalaryID = Convert.ToInt32(drData["PK_SalaryID"]);
                        objSalary.MonthYear = Convert.ToString(drData["MonthYear"]);
                        objSalary.SalaryDate = Convert.ToDateTime(drData["SalaryDate"]);
                        objSalary.sSalaryDate = objSalary.SalaryDate.ToString("dd/MM/yyyy");
                        objSalary.BasicPay = Convert.ToDecimal(drData["BasicPay"]);
                        objSalary.Deduction = Convert.ToDecimal(drData["Deduction"]);
                        objSalary.Incentives = Convert.ToDecimal(drData["Incentives"]);
                        objSalary.NetSalary = Convert.ToDecimal(drData["NetSalary"]);
                        objSalary.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSalary.ReferenceNo = Convert.ToString(drData["ReferenceNo"]);
                        objSalary.AdvanceDeduction = Convert.ToDecimal(drData["AdvanceDeduction"]);
                        objSalary.AttendanceDeduction = Convert.ToDecimal(drData["AttendanceDeduction"]);
                        objSalary.OvertimeIncentive = Convert.ToDecimal(drData["OvertimeIncentive"]);
                        objSalary.InAdvanceDeduction = Convert.ToDecimal(drData["InAdvanceDeduction"]);

                        objSalary.Active = Convert.ToBoolean(drData["Active"]);


                        objList.Add(objSalary);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Salary GetSalary | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Salary GetSalaryByID(int iSalaryID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Salary objSalary = new Entity.Salary();
            Entity.User objCreatedUser; Entity.Employee objEmployee;
            Entity.User objModifiedUser; 

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALARY);
                db.AddInParameter(cmd, "@PK_SalaryID", DbType.Int32, iSalaryID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalary = new Entity.Salary();
                        objEmployee = new Entity.Employee();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objEmployee.EmployeeID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objSalary.Employee = objEmployee;

                        objSalary.SalaryID = Convert.ToInt32(drData["PK_SalaryID"]);
                        objSalary.MonthYear = Convert.ToString(drData["MonthYear"]);
                        objSalary.Comments = Convert.ToString(drData["Comments"]);
                        objSalary.SalaryDate = Convert.ToDateTime(drData["SalaryDate"]);
                        objSalary.sSalaryDate = objSalary.SalaryDate.ToString("dd/MM/yyyy");
                        objSalary.BasicPay = Convert.ToDecimal(drData["BasicPay"]);
                        objSalary.Deduction = Convert.ToDecimal(drData["Deduction"]);
                        objSalary.Incentives = Convert.ToDecimal(drData["Incentives"]);
                        objSalary.NetSalary = Convert.ToDecimal(drData["NetSalary"]);
                        objSalary.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSalary.ReferenceNo = Convert.ToString(drData["ReferenceNo"]);
                        objSalary.AdvanceDeduction = Convert.ToDecimal(drData["AdvanceDeduction"]);
                        objSalary.AttendanceDeduction = Convert.ToDecimal(drData["AttendanceDeduction"]);
                        objSalary.InAdvanceDeduction = Convert.ToDecimal(drData["InAdvanceDeduction"]);
                        objSalary.OvertimeIncentive = Convert.ToDecimal(drData["OvertimeIncentive"]);
                        objSalary.Active = Convert.ToBoolean(drData["Active"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Salary GetSalaryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSalary;
        }

        public static Entity.Salary GetEmployeeSalaryCount(string MonthYear,int iEmployeeID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Salary objSalary = new Entity.Salary();
            Entity.User objCreatedUser; Entity.Employee objEmployee;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALARYBYMONTH);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, iEmployeeID);
                db.AddInParameter(cmd, "@MonthYear", DbType.String, MonthYear);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalary = new Entity.Salary();
                        objEmployee = new Entity.Employee();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objEmployee.EmployeeID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objSalary.Employee = objEmployee;

                        objSalary.SalaryID = Convert.ToInt32(drData["PK_SalaryID"]);
                        objSalary.MonthYear = Convert.ToString(drData["MonthYear"]);
                        objSalary.SalaryDate = Convert.ToDateTime(drData["SalaryDate"]);
                        objSalary.sSalaryDate = objSalary.SalaryDate.ToString("dd/MM/yyyy");
                        objSalary.BasicPay = Convert.ToDecimal(drData["BasicPay"]);
                        objSalary.Deduction = Convert.ToDecimal(drData["Deduction"]);
                        objSalary.Incentives = Convert.ToDecimal(drData["Incentives"]);
                        objSalary.NetSalary = Convert.ToDecimal(drData["NetSalary"]);
                        objSalary.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objSalary.ReferenceNo = Convert.ToString(drData["ReferenceNo"]);
                        objSalary.AdvanceDeduction = Convert.ToDecimal(drData["AdvanceDeduction"]);
                        objSalary.AttendanceDeduction = Convert.ToDecimal(drData["AttendanceDeduction"]);
                        objSalary.InAdvanceDeduction = Convert.ToDecimal(drData["InAdvanceDeduction"]);
                        objSalary.OvertimeIncentive = Convert.ToDecimal(drData["OvertimeIncentive"]);
                        objSalary.Active = Convert.ToBoolean(drData["Active"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Salary GetSalaryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSalary;
        }
        public static int AddSalary(Entity.Salary objAdvance)
        {
            string sException = string.Empty;
            int iID = 0;
            Database oDb;
            try
            {
                oDb = Entity.DBConnection.dbCon;
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_SALARY);
                oDb.AddOutParameter(cmd, "@PK_SalaryID", DbType.Int32, objAdvance.SalaryID);
                oDb.AddInParameter(cmd, "@MonthYear", DbType.String, objAdvance.MonthYear);
                oDb.AddInParameter(cmd, "@SalaryDate", DbType.String, objAdvance.sSalaryDate);
                oDb.AddInParameter(cmd, "@FK_EmployeeID", DbType.String, objAdvance.Employee.EmployeeID);
                oDb.AddInParameter(cmd, "@BasicPay", DbType.Decimal, objAdvance.BasicPay);
                oDb.AddInParameter(cmd, "@Deduction", DbType.Decimal, objAdvance.Deduction);
                oDb.AddInParameter(cmd, "@Incentives", DbType.Decimal, objAdvance.Incentives);
                oDb.AddInParameter(cmd, "@NetSalary", DbType.Decimal, objAdvance.NetSalary);
                oDb.AddInParameter(cmd, "@PaymentMode", DbType.String, objAdvance.PaymentMode);
                oDb.AddInParameter(cmd, "@ReferenceNo", DbType.String, objAdvance.ReferenceNo);
                oDb.AddInParameter(cmd, "@Comments", DbType.String, objAdvance.Comments);
                oDb.AddInParameter(cmd, "@AdvanceDeduction", DbType.Decimal, objAdvance.AdvanceDeduction);
                oDb.AddInParameter(cmd, "@AttendanceDeduction", DbType.Decimal, objAdvance.AttendanceDeduction);
                oDb.AddInParameter(cmd, "@InAdvanceDeduction", DbType.Decimal, objAdvance.InAdvanceDeduction);
                oDb.AddInParameter(cmd, "@OvertimeIncentive", DbType.Decimal, objAdvance.OvertimeIncentive);
                oDb.AddInParameter(cmd, "@Active", DbType.Boolean, objAdvance.Active);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objAdvance.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_SalaryID"));
            }
            catch (Exception ex)
            {
                sException = "Salary AddSalary | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateSalary(Entity.Salary objSalary)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database oDb;
            try
            {
                oDb = Entity.DBConnection.dbCon;
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_SALARY);
                oDb.AddInParameter(cmd, "@PK_SalaryID", DbType.Int32, objSalary.SalaryID);
                oDb.AddInParameter(cmd, "@MonthYear", DbType.String, objSalary.MonthYear);
                oDb.AddInParameter(cmd, "@SalaryDate", DbType.String, objSalary.sSalaryDate);
                oDb.AddInParameter(cmd, "@FK_EmployeeID", DbType.String, objSalary.Employee.EmployeeID);
                oDb.AddInParameter(cmd, "@BasicPay", DbType.Decimal, objSalary.BasicPay);
                oDb.AddInParameter(cmd, "@Deduction", DbType.Decimal, objSalary.Deduction);
                oDb.AddInParameter(cmd, "@Incentives", DbType.Decimal, objSalary.Incentives);
                oDb.AddInParameter(cmd, "@NetSalary", DbType.Decimal, objSalary.NetSalary);
                oDb.AddInParameter(cmd, "@PaymentMode", DbType.String, objSalary.PaymentMode);
                oDb.AddInParameter(cmd, "@ReferenceNo", DbType.String, objSalary.ReferenceNo);
                oDb.AddInParameter(cmd, "@AdvanceDeduction", DbType.Decimal, objSalary.AdvanceDeduction);
                oDb.AddInParameter(cmd, "@AttendanceDeduction", DbType.Decimal, objSalary.AttendanceDeduction);
                oDb.AddInParameter(cmd, "@InAdvanceDeduction", DbType.Decimal, objSalary.InAdvanceDeduction); ;
                oDb.AddInParameter(cmd, "@OvertimeIncentive", DbType.Decimal, objSalary.OvertimeIncentive);
                oDb.AddInParameter(cmd, "@Active", DbType.Boolean, objSalary.Active);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objSalary.ModifiedBy.UserID);
                oDb.AddInParameter(cmd, "@Comments", DbType.String, objSalary.Comments);
                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Advance UpdateAdvance| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteSalary(int ID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database oDb;
            try
            {
                oDb = Entity.DBConnection.dbCon;
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_SALARY);
                oDb.AddInParameter(cmd, "@PK_SalaryID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Salary DeleteSalary | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
     
    }
}
