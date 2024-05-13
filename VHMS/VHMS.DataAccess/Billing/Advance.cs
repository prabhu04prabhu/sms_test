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
    public class Advance
    {
        public static Collection<Entity.Advance> GetAdvance(int IsRetail=0,int FK_FinancialYearID=0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Advance> objList = new Collection<Entity.Advance>();
            Entity.Advance objAdvance = new Entity.Advance();
            Entity.Employee objEmployee;
            Entity.Ledger objBank;
            Entity.Ledger objLedger;
            Entity.Billing.Vendor objVendor;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ADVANCE);
                db.AddInParameter(cmd, "@PK_AdvanceID", DbType.Int32, 0);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, IsRetail);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objAdvance = new Entity.Advance();
                        objEmployee = new Entity.Employee();
                        objBank = new Entity.Ledger();
                        objLedger = new Entity.Ledger();
                        objVendor = new Entity.Billing.Vendor();

                        objAdvance.AdvanceID = Convert.ToInt32(drData["PK_AdvanceID"]);
                        objAdvance.DateofGiven = Convert.ToDateTime(drData["DateofGiven"]);
                        objAdvance.sDateofGiven = objAdvance.DateofGiven.ToString("dd/MM/yyyy");

                        objEmployee.EmployeeID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objAdvance.Employee = objEmployee;

                        objVendor.VendorID = Convert.ToInt32(drData["FK_VendorID"]);
                        objVendor.VendorName = Convert.ToString(drData["VendorName"]);
                        objAdvance.Vendor = objVendor;


                        objLedger.LedgerID = Convert.ToInt32(drData["FK_LedgerTypeID"]);
                        objLedger.LedgerName = Convert.ToString(drData["LedgerName"]);
                        objAdvance.Ledger = objLedger;
                       
                        objAdvance.Type = Convert.ToString(drData["Type"]);
                        objAdvance.Amount = Convert.ToDecimal(drData["Amount"]);
                        objAdvance.AdvanceType = Convert.ToString(drData["AdvanceType"]);
                        objAdvance.Advances = Convert.ToString(drData["Advances"]);
                        objAdvance.PartyName = Convert.ToString(drData["PartyName"]);

                        objList.Add(objAdvance);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Advance GetAdvance | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Advance GetAdvanceByID(int ID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Advance objAdvance = new Entity.Advance();
            Entity.Employee objEmployee;
            Entity.Ledger objBank;
            Entity.Ledger objLedger;
            Entity.Billing.Vendor objVendor;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ADVANCE);
                db.AddInParameter(cmd, "@PK_AdvanceID", DbType.Int32, ID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objAdvance = new Entity.Advance();
                        objEmployee = new Entity.Employee();
                        objBank = new Entity.Ledger();
                        objLedger = new Entity.Ledger();
                        objVendor = new Entity.Billing.Vendor();

                        objAdvance.AdvanceID = Convert.ToInt32(drData["PK_AdvanceID"]);
                        objAdvance.DateofGiven = Convert.ToDateTime(drData["DateofGiven"]);
                        objAdvance.sDateofGiven = objAdvance.DateofGiven.ToString("dd/MM/yyyy");

                        objEmployee.EmployeeID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objAdvance.Employee = objEmployee;

                        objVendor.VendorID = Convert.ToInt32(drData["FK_VendorID"]);
                        objVendor.VendorName = Convert.ToString(drData["VendorName"]);
                        objAdvance.Vendor = objVendor;


                        objLedger.LedgerID = Convert.ToInt32(drData["FK_LedgerTypeID"]);
                        objLedger.LedgerName = Convert.ToString(drData["LedgerName"]);
                        objAdvance.Ledger = objLedger;


                        objAdvance.Type = Convert.ToString(drData["Type"]);
                        objAdvance.Amount = Convert.ToDecimal(drData["Amount"]);
                        objAdvance.AdvanceType = Convert.ToString(drData["AdvanceType"]);
                        objAdvance.Comments = Convert.ToString(drData["Comments"]);
                        objAdvance.Advances = Convert.ToString(drData["Advances"]);
                        objAdvance.PartyName = Convert.ToString(drData["PartyName"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Advance GetAdvanceByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objAdvance;
        }

        public static Entity.Advance GetEmployeeAdvanceAmount(string FromDate = "",string ToDate = "",int EmployeeID=0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Advance objAdvance = new Entity.Advance();
            Entity.Employee objEmployee;
            Entity.Ledger objBank;
             Entity.Ledger objLedger;
            Entity.Billing.Vendor objVendor;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ADVANCEBYID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, FromDate);
                db.AddInParameter(cmd, "@DateTo", DbType.String, ToDate);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, EmployeeID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objAdvance = new Entity.Advance();
                        objEmployee = new Entity.Employee();
                        objBank = new Entity.Ledger();
                        objLedger = new Entity.Ledger();
                        objVendor = new Entity.Billing.Vendor();

                        objAdvance.AdvanceID = Convert.ToInt32(drData["PK_AdvanceID"]);
                        objAdvance.DateofGiven = Convert.ToDateTime(drData["DateofGiven"]);
                        objAdvance.sDateofGiven = objAdvance.DateofGiven.ToString("dd/MM/yyyy");

                        objEmployee.EmployeeID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objAdvance.Employee = objEmployee;

                        objVendor.VendorID = Convert.ToInt32(drData["FK_VendorID"]);
                        objVendor.VendorName = Convert.ToString(drData["VendorName"]);
                        objAdvance.Vendor = objVendor;


                        objLedger.LedgerID = Convert.ToInt32(drData["FK_LedgerTypeID"]);
                        objLedger.LedgerName = Convert.ToString(drData["LedgerName"]);
                        objAdvance.Ledger = objLedger;


                        objAdvance.Type = Convert.ToString(drData["Type"]);

                        objAdvance.Amount = Convert.ToDecimal(drData["Amount"]);
                        objAdvance.AdvanceType = Convert.ToString(drData["AdvanceType"]);
                        objAdvance.Advances = Convert.ToString(drData["Advances"]);
                        objAdvance.PartyName = Convert.ToString(drData["PartyName"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Advance GetAdvanceByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objAdvance;
        }

        public static Entity.Advance GetAdvanceAmount(string FromDate = "", string ToDate = "", int EmployeeID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Advance objAdvance = new Entity.Advance();
            Entity.Employee objEmployee;
            Entity.Ledger objBank;
            Entity.Ledger objLedger;
            Entity.Billing.Vendor objVendor;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ADVANCEAMOUNT);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, FromDate);
                db.AddInParameter(cmd, "@DateTo", DbType.String, ToDate);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, EmployeeID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objAdvance = new Entity.Advance();
                        objEmployee = new Entity.Employee();
                        objBank = new Entity.Ledger();
                        objLedger = new Entity.Ledger();
                        objVendor = new Entity.Billing.Vendor();

                        objAdvance.AdvanceID = Convert.ToInt32(drData["PK_AdvanceID"]);
                        objEmployee.EmployeeID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objAdvance.Employee = objEmployee;
                        objAdvance.Amount = Convert.ToDecimal(drData["Amount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Advance GetAdvanceByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objAdvance;
        }

        public static Entity.Advance GetAdvanceInSalary(int ID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Advance objAdvance = new Entity.Advance();
            Entity.Employee objEmployee;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ADVANCEINSALARY);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, ID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objAdvance = new Entity.Advance();
                        objEmployee = new Entity.Employee();
                        objAdvance.AdvanceID = Convert.ToInt32(drData["PK_AdvanceID"]);
                        objEmployee.EmployeeID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objAdvance.Employee = objEmployee;
                        objAdvance.Amount = Convert.ToDecimal(drData["Amount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Advance GetAdvanceByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objAdvance;
        }

        public static Entity.Advance GetAdvanceOutSalary(int ID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Advance objAdvance = new Entity.Advance();
            Entity.Employee objEmployee;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ADVANCEOUTSALARY);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, ID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objAdvance = new Entity.Advance();
                        objEmployee = new Entity.Employee();
                        objAdvance.AdvanceID = Convert.ToInt32(drData["PK_AdvanceID"]);
                        objEmployee.EmployeeID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objAdvance.Employee = objEmployee;
                        objAdvance.Amount = Convert.ToDecimal(drData["Amount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Advance GetAdvanceByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objAdvance;
        }
        public static int AddAdvance(Entity.Advance objAdvance)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_ADVANCE);
                db.AddOutParameter(cmd, "@PK_AdvanceID", DbType.Int32, objAdvance.AdvanceID);
                db.AddInParameter(cmd, "@DateofGiven", DbType.String, objAdvance.sDateofGiven);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, objAdvance.Employee.EmployeeID);
                db.AddInParameter(cmd, "@FK_VendorID", DbType.Int32, objAdvance.Vendor.VendorID);
                db.AddInParameter(cmd, "@FK_LedgerTypeID", DbType.Int32, objAdvance.Ledger.LedgerID);
                db.AddInParameter(cmd, "@Type", DbType.String, objAdvance.Type);
                db.AddInParameter(cmd, "@Amount", DbType.Decimal, objAdvance.Amount);
                db.AddInParameter(cmd, "@AdvanceType", DbType.String, objAdvance.AdvanceType);
                db.AddInParameter(cmd, "@Advances", DbType.String, objAdvance.Advances);
                db.AddInParameter(cmd, "@Comments", DbType.String, objAdvance.Comments);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objAdvance.CreatedBy.UserID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objAdvance.FinancialYear.FinancialYearID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_AdvanceID"));
            }
            catch (Exception ex)
            {
                sException = "Advance AddAdvance | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateAdvance(Entity.Advance objAdvance)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_ADVANCE);
                db.AddInParameter(cmd, "@PK_AdvanceID", DbType.Int32, objAdvance.AdvanceID);
                db.AddInParameter(cmd, "@DateofGiven", DbType.String, objAdvance.sDateofGiven);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, objAdvance.Employee.EmployeeID);
                db.AddInParameter(cmd, "@FK_VendorID", DbType.Int32, objAdvance.Vendor.VendorID);
                db.AddInParameter(cmd, "@FK_LedgerTypeID", DbType.Int32, objAdvance.Ledger.LedgerID);
                db.AddInParameter(cmd, "@Type", DbType.String, objAdvance.Type);
                db.AddInParameter(cmd, "@Amount", DbType.Decimal, objAdvance.Amount);
                db.AddInParameter(cmd, "@AdvanceType", DbType.String, objAdvance.AdvanceType);
                db.AddInParameter(cmd, "@Advances", DbType.String, objAdvance.Advances);
                db.AddInParameter(cmd, "@Comments", DbType.String, objAdvance.Comments);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objAdvance.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objAdvance.FinancialYear.FinancialYearID);

                iID = db.ExecuteNonQuery(cmd);
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
        public static bool DeleteAdvance(int iAdvanceId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_ADVANCE);
                db.AddInParameter(cmd, "@PK_AdvanceID", DbType.Int32, iAdvanceId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Advance DeleteAdvance | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
