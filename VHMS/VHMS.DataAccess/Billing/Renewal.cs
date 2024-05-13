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
    public class Renewal
    {
        public static Collection<Entity.Billing.Renewal> GetRenewal(int iRegionID = 0, int iZoneID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Renewal> objList = new Collection<Entity.Billing.Renewal>();
            Entity.Billing.Renewal objRenewal = new Entity.Billing.Renewal();
            Entity.Customer objCustomer; Entity.Branch objBranch;Entity.User objEmployee;
            Entity.Chit objChit;
            Entity.Billing.Register objRegister; Entity.User objREmployee;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_RENEWAL);
                db.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, iRegionID);
                db.AddInParameter(cmd, "@FK_ZoneID", DbType.Int32, iZoneID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRenewal = new Entity.Billing.Renewal();
                        objChit = new Entity.Chit();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objEmployee = new Entity.User();
                        objRegister = new Entity.Billing.Register();
                        objREmployee = new Entity.User();

                        objRenewal.RenewalID = Convert.ToInt32(drData["PK_RenewalID"]);
                        objRenewal.RenewalNo = Convert.ToString(drData["RenewalNo"]);
                        objRenewal.RenewalDate = Convert.ToDateTime(drData["RenewalDate"]);
                        objRenewal.sRenewalDate = objRenewal.RenewalDate.ToString("dd/MM/yyyy");
                        objRenewal.isRegisterEntry = Convert.ToBoolean(drData["isRegisterEntry"]);

                        objRegister.RegisterID = Convert.ToInt32(drData["PK_RegisterID"]);
                        objRegister.AccountNo = Convert.ToString(drData["AccountNo"]);
                        objRegister.RegisterDate = Convert.ToDateTime(drData["RegisterDate"]);
                        objRegister.sRegisterDate = objRegister.RegisterDate.ToString("dd/MM/yyyy");
                        objRegister.IDProof = Convert.ToString(drData["IDProof"]);
                        objRegister.IDNo = Convert.ToString(drData["IDNo"]);

                        objChit.ChitID = Convert.ToInt32(drData["FK_ChitID"]);
                        objChit.ChitName = Convert.ToString(drData["ChitName"]);
                        objChit.ChitCode = Convert.ToString(drData["ChitCode"]);
                        objChit.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objChit.BonusAmount = Convert.ToDecimal(drData["BonusAmount"]);
                        objChit.Duration = Convert.ToInt32(drData["Duration"]);
                        objChit.InstallmentAmount = Convert.ToDecimal(drData["InstallmentAmount"]);
                        objChit.GrandTotal = Convert.ToDecimal(drData["GrandTotal"]);
                        objRegister.Chit = objChit;

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objRegister.Customer = objCustomer;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objRegister.Branch = objBranch;

                        objEmployee.UserID= Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objRegister.Employee = objEmployee;

                        objRenewal.Register = objRegister;
                        objRenewal.AmountPaid = Convert.ToDecimal(drData["AmountPaid"]);
                        objRenewal.Status = Convert.ToString(drData["Status"]);

                        objREmployee.UserID = Convert.ToInt32(drData["REmployeeID"]);
                        objREmployee.EmployeeName = Convert.ToString(drData["REmployeeName"]);
                        objREmployee.EmployeeCode = Convert.ToString(drData["REmployeeCode"]);
                        objRenewal.REmployee = objREmployee;

                        objList.Add(objRenewal);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Renewal GetRenewal | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Renewal> SearchRenewal(string ID, int iBranchID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Renewal> objList = new Collection<Entity.Billing.Renewal>();
            Entity.Billing.Renewal objRenewal = new Entity.Billing.Renewal();
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.User objEmployee;
            Entity.Chit objChit;
            Entity.Billing.Register objRegister; Entity.User objREmployee;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_RENEWAL);
                db.AddInParameter(cmd, "@key", DbType.String, ID);
               // db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, iBranchID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRenewal = new Entity.Billing.Renewal();
                        objChit = new Entity.Chit();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objEmployee = new Entity.User();
                        objRegister = new Entity.Billing.Register();
                        objREmployee = new Entity.User();

                        objRenewal.RenewalID = Convert.ToInt32(drData["PK_RenewalID"]);
                        objRenewal.RenewalNo = Convert.ToString(drData["RenewalNo"]);
                        objRenewal.RenewalDate = Convert.ToDateTime(drData["RenewalDate"]);
                        objRenewal.sRenewalDate = objRenewal.RenewalDate.ToString("dd/MM/yyyy");
                        objRenewal.isRegisterEntry = Convert.ToBoolean(drData["isRegisterEntry"]);

                        objRegister.RegisterID = Convert.ToInt32(drData["PK_RegisterID"]);
                        objRegister.AccountNo = Convert.ToString(drData["AccountNo"]);
                        objRegister.RegisterDate = Convert.ToDateTime(drData["RegisterDate"]);
                        objRegister.sRegisterDate = objRegister.RegisterDate.ToString("dd/MM/yyyy");
                        objRegister.IDProof = Convert.ToString(drData["IDProof"]);
                        objRegister.IDNo = Convert.ToString(drData["IDNo"]);

                        objChit.ChitID = Convert.ToInt32(drData["FK_ChitID"]);
                        objChit.ChitName = Convert.ToString(drData["ChitName"]);
                        objChit.ChitCode = Convert.ToString(drData["ChitCode"]);
                        objChit.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objChit.BonusAmount = Convert.ToDecimal(drData["BonusAmount"]);
                        objChit.Duration = Convert.ToInt32(drData["Duration"]);
                        objChit.InstallmentAmount = Convert.ToDecimal(drData["InstallmentAmount"]);
                        objChit.GrandTotal = Convert.ToDecimal(drData["GrandTotal"]);
                        objRegister.Chit = objChit;

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objRegister.Customer = objCustomer;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objRegister.Branch = objBranch;

                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objRegister.Employee = objEmployee;

                        objRenewal.Register = objRegister;
                        objRenewal.AmountPaid = Convert.ToDecimal(drData["AmountPaid"]);
                        objRenewal.Status = Convert.ToString(drData["Status"]);

                        objREmployee.UserID = Convert.ToInt32(drData["REmployeeID"]);
                        objREmployee.EmployeeName = Convert.ToString(drData["REmployeeName"]);
                        objREmployee.EmployeeCode = Convert.ToString(drData["REmployeeCode"]);
                        objRenewal.REmployee = objREmployee;

                        objList.Add(objRenewal);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Renewal GetRenewal | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Entity.Billing.Renewal GetRenewalByID(int iRenewalID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Renewal objRenewal = new Entity.Billing.Renewal();
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.User objEmployee;
            Entity.Chit objChit;
            Entity.Billing.Register objRegister; Entity.User objREmployee;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_RENEWAL);
                db.AddInParameter(cmd, "@PK_RenewalID", DbType.Int32, iRenewalID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRenewal = new Entity.Billing.Renewal();
                        objChit = new Entity.Chit();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objEmployee = new Entity.User();
                        objRegister = new Entity.Billing.Register();
                        objREmployee = new Entity.User();

                        objRenewal.RenewalID = Convert.ToInt32(drData["PK_RenewalID"]);
                        objRenewal.RenewalNo = Convert.ToString(drData["RenewalNo"]);
                        objRenewal.isRegisterEntry = Convert.ToBoolean(drData["isRegisterEntry"]);
                        objRenewal.RenewalDate = Convert.ToDateTime(drData["RenewalDate"]);
                        objRenewal.sRenewalDate = objRenewal.RenewalDate.ToString("dd/MM/yyyy");

                        objRegister.RegisterID = Convert.ToInt32(drData["PK_RegisterID"]);
                        objRegister.AccountNo = Convert.ToString(drData["AccountNo"]);
                        objRegister.RegisterDate = Convert.ToDateTime(drData["RegisterDate"]);
                        objRegister.sRegisterDate = objRegister.RegisterDate.ToString("dd/MM/yyyy");
                        objRegister.IDProof = Convert.ToString(drData["IDProof"]);
                        objRegister.IDNo = Convert.ToString(drData["IDNo"]);

                        objChit.ChitID = Convert.ToInt32(drData["FK_ChitID"]);
                        objChit.ChitName = Convert.ToString(drData["ChitName"]);
                        objChit.ChitCode = Convert.ToString(drData["ChitCode"]);
                        objChit.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objChit.BonusAmount = Convert.ToDecimal(drData["BonusAmount"]);
                        objChit.Duration = Convert.ToInt32(drData["Duration"]);
                        objChit.InstallmentAmount = Convert.ToDecimal(drData["InstallmentAmount"]);
                        objChit.GrandTotal = Convert.ToDecimal(drData["GrandTotal"]);
                        objRegister.Chit = objChit;

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objRegister.Customer = objCustomer;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objRegister.Branch = objBranch;

                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objRegister.Employee = objEmployee;
                        objRegister.ChitAmount = Convert.ToDecimal(drData["ChitAmount"]);
                        objRenewal.Register = objRegister;
                        objRenewal.AmountPaid = Convert.ToDecimal(drData["AmountPaid"]);
                        objRenewal.Status = Convert.ToString(drData["Status"]);

                        objREmployee.UserID = Convert.ToInt32(drData["REmployeeID"]);
                        objREmployee.EmployeeName = Convert.ToString(drData["REmployeeName"]);
                        objREmployee.EmployeeCode = Convert.ToString(drData["REmployeeCode"]);
                        objRenewal.REmployee = objREmployee;
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Renewal GetRenewalByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objRenewal;
        }
        public static int AddRenewal(Entity.Billing.Renewal objRenewal)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddRenewal(oDb, objRenewal, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tRenewal", "PK_RenewalID", objRenewal.RenewalID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objRenewal.CreatedBy.UserID);
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
        private static int AddRenewal(Database oDb, Entity.Billing.Renewal objRenewal, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_RENEWAL);
                oDb.AddOutParameter(cmd, "@PK_RenewalID", DbType.Int32, objRenewal.RenewalID);
                oDb.AddInParameter(cmd, "@RenewalDate", DbType.String, objRenewal.sRenewalDate);
                oDb.AddInParameter(cmd, "@AmountPaid", DbType.Decimal, objRenewal.AmountPaid);
                oDb.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, objRenewal.Branch.BranchID);
                oDb.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, objRenewal.Employee.UserID);
                oDb.AddInParameter(cmd, "@Status", DbType.String, objRenewal.Status);
                oDb.AddInParameter(cmd, "@FK_RegisterID", DbType.Int32, objRenewal.Register.RegisterID);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objRenewal.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_RenewalID"));
                    objRenewal.RenewalID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Renewal AddRenewal | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateRenewal(Entity.Billing.Renewal objRenewal)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateRenewal(oDb, objRenewal, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tRenewal", "PK_RenewalID", objRenewal.RenewalID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objRenewal.ModifiedBy.UserID);
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
        private static bool UpdateRenewal(Database oDb, Entity.Billing.Renewal objRenewal, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_RENEWAL);
                oDb.AddInParameter(cmd, "@PK_RenewalID", DbType.Int32, objRenewal.RenewalID);
                oDb.AddInParameter(cmd, "@RenewalDate", DbType.String, objRenewal.sRenewalDate);
                oDb.AddInParameter(cmd, "@AmountPaid", DbType.Decimal, objRenewal.AmountPaid);
                oDb.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, objRenewal.Branch.BranchID);
                oDb.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, objRenewal.Employee.UserID);
                oDb.AddInParameter(cmd, "@Status", DbType.String, objRenewal.Status);
                oDb.AddInParameter(cmd, "@FK_RegisterID", DbType.Int32, objRenewal.Register.RegisterID);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objRenewal.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Renewal UpdateRenewal | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteRenewal(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteRenewal(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tRenewal", "PK_RenewalID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteRenewal(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_RENEWAL);
                oDb.AddInParameter(cmd, "@PK_RenewalID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Renewal DeleteRenewal | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
