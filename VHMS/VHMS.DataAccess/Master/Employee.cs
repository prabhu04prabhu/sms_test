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
    public class Employee
    {
        public static Collection<Entity.Employee> GetEmployee(int CountryID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Employee> objList = new Collection<Entity.Employee>();
            Entity.Employee objEmployee = new Entity.Employee();
            Entity.User objCreatedUser; Entity.Shift objshift;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_EMPLOYEE);
                db.AddInParameter(cmd, "@PK_EmployeeID", DbType.Int32, CountryID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objEmployee = new Entity.Employee();
                        objshift = new Entity.Shift();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objshift.ShiftID = Convert.ToInt32(drData["FK_ShiftID"]);
                        objshift.ShiftName = Convert.ToString(drData["ShiftName"]);
                        objshift.InTime = Convert.ToString(drData["InTime"]);
                        objshift.OutTime = Convert.ToString(drData["OutTime"]);
                        objEmployee.Shift = objshift;
                        objEmployee.ESLID = Convert.ToInt32(drData["ESLID"]);
                        objEmployee.EmployeeID = Convert.ToInt32(drData["PK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objEmployee.Dob = Convert.ToDateTime(drData["Dob"]);
                        objEmployee.sDob = objEmployee.Dob.ToString("dd/MM/yyyy");
                        objEmployee.Address = Convert.ToString(drData["Address"]);
                        objEmployee.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objEmployee.PhoneNo2 = Convert.ToString(drData["PhoneNo2"]);
                        objEmployee.Dateofjoin = Convert.ToDateTime(drData["Dateofjoin"]);
                        objEmployee.sDateofjoin = objEmployee.Dateofjoin.ToString("dd/MM/yyyy");
                        objEmployee.IDProof = Convert.ToString(drData["IDProof"]);
                        objEmployee.BloodGroup = Convert.ToString(drData["BloodGroup"]);
                        objEmployee.ProofImage1 = Convert.ToString(drData["ProofImage1"]);
                        objEmployee.ProofImage2 = Convert.ToString(drData["ProofImage2"]);
                        objEmployee.Gender = Convert.ToString(drData["Gender"]);
                        objEmployee.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objEmployee.Conveyance = Convert.ToDecimal(drData["Conveyance"]);
                        objEmployee.BasicPay = Convert.ToDecimal(drData["BasicPay"]);
                        objEmployee.SpecialAllowance = Convert.ToDecimal(drData["SpecialAllowance"]);
                        objEmployee.MedicalAllowance = Convert.ToDecimal(drData["MedicalAllowance"]);
                        objEmployee.PF = Convert.ToDecimal(drData["PF"]);
                        objEmployee.HRA = Convert.ToDecimal(drData["HRA"]);
                        objEmployee.ESI = Convert.ToDecimal(drData["ESI"]);
                        objEmployee.OvertimeAllowance = Convert.ToDecimal(drData["OvertimeAllowance"]);
                        objEmployee.FoodAllowance = Convert.ToDecimal(drData["FoodAllowance"]);
                        objEmployee.NetSalary = Convert.ToDecimal(drData["NetSalary"]);
                        objEmployee.PaidLeaves = Convert.ToInt32(drData["PaidLeaves"]);
                        objEmployee.AdvanceDeduction = Convert.ToDecimal(drData["AdvanceDeduction"]);

                        objEmployee.EmployeeInTime = Convert.ToString(drData["EmployeeInTime"]);
                        objEmployee.EmployeeOutTime = Convert.ToString(drData["EmployeeOutTime"]);

                        objList.Add(objEmployee);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Employee GetEmployee | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Employee GetEmployeeByID(int iEmployeeID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Employee objEmployee = new Entity.Employee();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser; Entity.Shift objshift;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_EMPLOYEE);
                db.AddInParameter(cmd, "@PK_EmployeeID", DbType.Int32, iEmployeeID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objEmployee = new Entity.Employee();
                        objshift = new Entity.Shift();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objshift.ShiftID = Convert.ToInt32(drData["FK_ShiftID"]);
                        objshift.ShiftName = Convert.ToString(drData["ShiftName"]);
                        objshift.InTime = Convert.ToString(drData["InTime"]);
                        objshift.OutTime = Convert.ToString(drData["OutTime"]);
                        objEmployee.Shift = objshift;

                        objEmployee.EmployeeID = Convert.ToInt32(drData["PK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objEmployee.Dob = Convert.ToDateTime(drData["Dob"]);
                        objEmployee.sDob = objEmployee.Dob.ToString("dd/MM/yyyy");
                        objEmployee.Address = Convert.ToString(drData["Address"]);
                        objEmployee.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objEmployee.PhoneNo2 = Convert.ToString(drData["PhoneNo2"]);
                        objEmployee.Dateofjoin = Convert.ToDateTime(drData["Dateofjoin"]);
                        objEmployee.sDateofjoin = objEmployee.Dateofjoin.ToString("dd/MM/yyyy");
                        objEmployee.IDProof = Convert.ToString(drData["IDProof"]);
                        objEmployee.BloodGroup = Convert.ToString(drData["BloodGroup"]);
                        objEmployee.ProofImage1 = Convert.ToString(drData["ProofImage1"]);
                        objEmployee.ProofImage2 = Convert.ToString(drData["ProofImage2"]);
                        objEmployee.Gender = Convert.ToString(drData["Gender"]);
                        objEmployee.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objEmployee.Conveyance = Convert.ToDecimal(drData["Conveyance"]);
                        objEmployee.BasicPay = Convert.ToDecimal(drData["BasicPay"]);
                        objEmployee.SpecialAllowance = Convert.ToDecimal(drData["SpecialAllowance"]);
                        objEmployee.MedicalAllowance = Convert.ToDecimal(drData["MedicalAllowance"]);
                        objEmployee.PF = Convert.ToDecimal(drData["PF"]);
                        objEmployee.HRA = Convert.ToDecimal(drData["HRA"]);
                        objEmployee.ESI = Convert.ToDecimal(drData["ESI"]);
                        objEmployee.OvertimeAllowance = Convert.ToDecimal(drData["OvertimeAllowance"]);
                        objEmployee.FoodAllowance = Convert.ToDecimal(drData["FoodAllowance"]);
                        objEmployee.NetSalary = Convert.ToDecimal(drData["NetSalary"]);
                        objEmployee.PaidLeaves = Convert.ToInt32(drData["PaidLeaves"]);
                        objEmployee.ESLID = Convert.ToInt32(drData["ESLID"]);
                        objEmployee.AdvanceDeduction = Convert.ToDecimal(drData["AdvanceDeduction"]);
                        objEmployee.EmployeeInTime = Convert.ToString(drData["EmployeeInTime"]);
                        objEmployee.EmployeeOutTime = Convert.ToString(drData["EmployeeOutTime"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Employee GetEmployeeByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objEmployee;
        }
        public static int AddEmployee(Entity.Employee objEmployee)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddEmployee(oDb, objEmployee, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tEmployee", "PK_EmployeeID", objEmployee.EmployeeID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objEmployee.CreatedBy.UserID);
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
        private static int AddEmployee(Database oDb, Entity.Employee objEmployee, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_EMPLOYEE);
                oDb.AddOutParameter(cmd, "@PK_EmployeeID", DbType.Int32, objEmployee.EmployeeID);
                oDb.AddInParameter(cmd, "@EmployeeName", DbType.String, objEmployee.EmployeeName);
                oDb.AddInParameter(cmd, "@EmployeeCode", DbType.String, objEmployee.EmployeeCode);
                oDb.AddInParameter(cmd, "@Dob", DbType.String, objEmployee.sDob);
                oDb.AddInParameter(cmd, "@Address", DbType.String, objEmployee.Address);
                oDb.AddInParameter(cmd, "@ESLID", DbType.Int32, objEmployee.ESLID);
                oDb.AddInParameter(cmd, "@PhoneNo1", DbType.String, objEmployee.PhoneNo1);
                oDb.AddInParameter(cmd, "@PhoneNo2", DbType.String, objEmployee.PhoneNo2);
                oDb.AddInParameter(cmd, "@Dateofjoin", DbType.String, objEmployee.sDateofjoin);
                oDb.AddInParameter(cmd, "@IDProof", DbType.String, objEmployee.IDProof);
                oDb.AddInParameter(cmd, "@BloodGroup", DbType.String, objEmployee.BloodGroup);
                oDb.AddInParameter(cmd, "@Gender", DbType.String, objEmployee.Gender);
                oDb.AddInParameter(cmd, "@ProofImage1", DbType.String, objEmployee.ProofImage1);
                oDb.AddInParameter(cmd, "@ProofImage2", DbType.String, objEmployee.ProofImage2);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objEmployee.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objEmployee.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_EmployeeID"));
                    objEmployee.EmployeeID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Employee AddEmployee | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateEmployee(Entity.Employee objEmployee)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateEmployee(oDb, objEmployee, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tEmployee", "PK_EmployeeID", objEmployee.EmployeeID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objEmployee.ModifiedBy.UserID);
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
        public static bool UpdateEmployeeAmount(Entity.Employee objEmployee)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateEmployeeAmount(oDb, objEmployee, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tEmployee", "PK_EmployeeID", objEmployee.EmployeeID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objEmployee.ModifiedBy.UserID);
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
        private static bool UpdateEmployee(Database oDb, Entity.Employee objEmployee, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_EMPLOYEE);
                oDb.AddInParameter(cmd, "@PK_EmployeeID", DbType.Int32, objEmployee.EmployeeID);
                oDb.AddInParameter(cmd, "@EmployeeName", DbType.String, objEmployee.EmployeeName);
                oDb.AddInParameter(cmd, "@EmployeeCode", DbType.String, objEmployee.EmployeeCode);
                oDb.AddInParameter(cmd, "@Dob", DbType.String, objEmployee.sDob);
                oDb.AddInParameter(cmd, "@Address", DbType.String, objEmployee.Address);
                oDb.AddInParameter(cmd, "@ESLID", DbType.Int32, objEmployee.ESLID);
                oDb.AddInParameter(cmd, "@PhoneNo1", DbType.String, objEmployee.PhoneNo1);
                oDb.AddInParameter(cmd, "@PhoneNo2", DbType.String, objEmployee.PhoneNo2);
                oDb.AddInParameter(cmd, "@Dateofjoin", DbType.String, objEmployee.sDateofjoin);
                oDb.AddInParameter(cmd, "@IDProof", DbType.String, objEmployee.IDProof);
                oDb.AddInParameter(cmd, "@BloodGroup", DbType.String, objEmployee.BloodGroup);
                oDb.AddInParameter(cmd, "@Gender", DbType.String, objEmployee.Gender);
                oDb.AddInParameter(cmd, "@ProofImage1", DbType.String, objEmployee.ProofImage1);
                oDb.AddInParameter(cmd, "@ProofImage2", DbType.String, objEmployee.ProofImage2);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objEmployee.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objEmployee.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Employee UpdateEmployee | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

        private static bool UpdateEmployeeAmount(Database oDb, Entity.Employee objEmployee, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_EMPLOYEE_AMOUNT);
                oDb.AddInParameter(cmd, "@PK_EmployeeID", DbType.Int32, objEmployee.EmployeeID);
                oDb.AddInParameter(cmd, "@Conveyance", DbType.Decimal, objEmployee.Conveyance);
                oDb.AddInParameter(cmd, "@BasicPay", DbType.Decimal, objEmployee.BasicPay);
                oDb.AddInParameter(cmd, "@SpecialAllowance", DbType.Decimal, objEmployee.SpecialAllowance);
                oDb.AddInParameter(cmd, "@MedicalAllowance", DbType.Decimal, objEmployee.MedicalAllowance);
                oDb.AddInParameter(cmd, "@PF", DbType.Decimal, objEmployee.PF);
                oDb.AddInParameter(cmd, "@HRA", DbType.Decimal, objEmployee.HRA);
                oDb.AddInParameter(cmd, "@ESI", DbType.Decimal, objEmployee.ESI);
                oDb.AddInParameter(cmd, "@OvertimeAllowance", DbType.Decimal, objEmployee.OvertimeAllowance);
                oDb.AddInParameter(cmd, "@FoodAllowance", DbType.Decimal, objEmployee.FoodAllowance);
                oDb.AddInParameter(cmd, "@NetSalary", DbType.Decimal, objEmployee.NetSalary);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objEmployee.ModifiedBy.UserID);
                oDb.AddInParameter(cmd, "@AdvanceDeduction", DbType.Decimal, objEmployee.AdvanceDeduction);
                oDb.AddInParameter(cmd, "@FK_ShiftID", DbType.Int32, objEmployee.Shift.ShiftID);
                oDb.AddInParameter(cmd, "@EmployeeInTime", DbType.String, objEmployee.EmployeeInTime);
                oDb.AddInParameter(cmd, "@EmployeeOutTime", DbType.String, objEmployee.EmployeeOutTime);
                oDb.AddInParameter(cmd, "@PaidLeaves", DbType.Int32, objEmployee.PaidLeaves);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Employee UpdateEmployee | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteEmployee(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteEmployee(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tEmployee", "PK_EmployeeID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteEmployee(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_EMPLOYEE);
                oDb.AddInParameter(cmd, "@PK_EmployeeID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Employee DeleteEmployee | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
