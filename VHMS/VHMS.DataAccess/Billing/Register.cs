using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections.ObjectModel;
using VHMS;
using System.Web;

namespace VHMS.DataAccess.Billing
{
    public class Register
    {
        public static Collection<Entity.Billing.Register> GetRegister( int iCustomerID=0, int iRegionID = 0, int iZoneID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Register> objList = new Collection<Entity.Billing.Register>();
            Entity.Billing.Register objRegister = new Entity.Billing.Register();
            Entity.Customer objCustomer; Entity.Branch objBranch;Entity.User objEmployee;
            Entity.Chit objChit;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_REGISTER);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iCustomerID);
                db.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, iRegionID);
                db.AddInParameter(cmd, "@FK_ZoneID", DbType.Int32, iZoneID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRegister = new Entity.Billing.Register();
                        objChit = new Entity.Chit();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objEmployee = new Entity.User();

                        objRegister.RegisterID = Convert.ToInt32(drData["PK_RegisterID"]);
                        objRegister.AccountNo = Convert.ToString(drData["AccountNo"]);
                        objRegister.RegisterDate = Convert.ToDateTime(drData["RegisterDate"]);
                        objRegister.sRegisterDate = objRegister.RegisterDate.ToString("dd/MM/yyyy");
                        objRegister.IDProof = Convert.ToString(drData["IDProof"]);
                        objRegister.IDNo = Convert.ToString(drData["IDNo"]);
                        objChit.ChitID = Convert.ToInt32(drData["FK_ChitID"]);
                        objChit.ChitName = Convert.ToString(drData["ChitName"]);
                        objChit.ChitCode = Convert.ToString(drData["ChitCode"]);
                        objChit.Duration = Convert.ToInt32(drData["Duration"]);
                        objRegister.Chit = objChit;

                        objRegister.InstallmentAmount = Convert.ToDecimal(drData["InstallmentAmount"]);
                        objRegister.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objRegister.BonusAmount = Convert.ToDecimal(drData["BonusAmount"]);
                        objRegister.ReceiptNo = Convert.ToString(drData["ReceiptNo"]);

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objRegister.Customer = objCustomer;
                        objRegister.DOB = Convert.ToString(drData["DOB"]);
                        objRegister.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objRegister.Address = Convert.ToString(drData["Address"]);
                        objRegister.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objRegister.Branch = objBranch;

                        objRegister.ProofImage = Convert.ToString(drData["ProofImage"]);
                        objRegister.ProofBackImage = Convert.ToString(drData["ProofBackImage"]);
                        objRegister.RegisterImage1 = Convert.ToString(drData["RegisterImage1"]);
                        objRegister.RegisterImage2 = Convert.ToString(drData["RegisterImage2"]);

                        objEmployee.UserID= Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objRegister.Employee = objEmployee;

                        objRegister.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objRegister);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Register GetRegister | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        
        public static Collection<Entity.Billing.Register> SearchRegister(string ID, int iBranchID, int iRegionID = 0, int iZoneID = 0)
        {
            
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Register> objList = new Collection<Entity.Billing.Register>();
            Collection<Entity.Branch> objBranch1 = new Collection<Entity.Branch>();
            Entity.Billing.Register objRegister = new Entity.Billing.Register();
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.User objEmployee;
            Entity.Chit objChit;
            try
            {
               
                objEmployee = new Entity.User();
               //int UserID = objEmployee.UserID;
                objBranch = new Entity.Branch();
               // int EmployeeID = Convert.ToInt32(HttpContext.Current.Session["EmployeeID"].ToString());
                string emp = objEmployee.EmployeeName;
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_REGISTER);
                db.AddInParameter(cmd, "@key", DbType.String, ID);
                //db.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, iRegionID);
                //db.AddInParameter(cmd, "@FK_ZoneID", DbType.Int32, iZoneID);
                // if (EmployeeID == 1)

                // {
                //  iBranchID = objBranch.BranchID;
                // db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, iBranchID);
                // }

                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRegister = new Entity.Billing.Register();
                        objChit = new Entity.Chit();
                        objCustomer = new Entity.Customer();
                       
                        objEmployee = new Entity.User();

                        objRegister.RegisterID = Convert.ToInt32(drData["PK_RegisterID"]);
                        objRegister.AccountNo = Convert.ToString(drData["AccountNo"]);
                        objRegister.RegisterDate = Convert.ToDateTime(drData["RegisterDate"]);
                        objRegister.sRegisterDate = objRegister.RegisterDate.ToString("dd/MM/yyyy");
                        objRegister.IDProof = Convert.ToString(drData["IDProof"]);
                        objRegister.IDNo = Convert.ToString(drData["IDNo"]);
                        objChit.ChitID = Convert.ToInt32(drData["FK_ChitID"]);
                        objChit.ChitName = Convert.ToString(drData["ChitName"]);
                        objChit.ChitCode = Convert.ToString(drData["ChitCode"]);
                        objChit.Duration = Convert.ToInt32(drData["Duration"]);
                        objRegister.Chit = objChit;

                        objRegister.InstallmentAmount = Convert.ToDecimal(drData["InstallmentAmount"]);
                        objRegister.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objRegister.BonusAmount = Convert.ToDecimal(drData["BonusAmount"]);
                        objRegister.ReceiptNo = Convert.ToString(drData["ReceiptNo"]);

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objRegister.Customer = objCustomer;
                        objRegister.DOB = Convert.ToString(drData["DOB"]);
                        objRegister.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objRegister.Address = Convert.ToString(drData["Address"]);
                        objRegister.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objRegister.Branch = objBranch;

                        objRegister.ProofImage = Convert.ToString(drData["ProofImage"]);
                        objRegister.ProofBackImage = Convert.ToString(drData["ProofBackImage"]);
                        objRegister.RegisterImage1 = Convert.ToString(drData["RegisterImage1"]);
                        objRegister.RegisterImage2 = Convert.ToString(drData["RegisterImage2"]);

                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objRegister.Employee = objEmployee;

                        objRegister.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objRegister);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Register GetRegister | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Register> GetRegisterNotification()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Register> objList = new Collection<Entity.Billing.Register>();
            Entity.Billing.Register objRegister = new Entity.Billing.Register();
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.User objEmployee;
            Entity.Chit objChit;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_REGISTERNOTIFICATION);
              
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRegister = new Entity.Billing.Register();
                        objChit = new Entity.Chit();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objEmployee = new Entity.User();

                        objRegister.RegisterID = Convert.ToInt32(drData["PK_RegisterID"]);
                        objRegister.AccountNo = Convert.ToString(drData["AccountNo"]);
                        objRegister.RegisterDate = Convert.ToDateTime(drData["RegisterDate"]);
                        objRegister.sRegisterDate = objRegister.RegisterDate.ToString("dd/MM/yyyy");
                        objRegister.IDProof = Convert.ToString(drData["IDProof"]);
                        objRegister.IDNo = Convert.ToString(drData["IDNo"]);
                        objChit.ChitID = Convert.ToInt32(drData["FK_ChitID"]);
                        objChit.ChitName = Convert.ToString(drData["ChitName"]);
                        objChit.ChitCode = Convert.ToString(drData["ChitCode"]);
                        objChit.Duration = Convert.ToInt32(drData["Duration"]);
                        objRegister.Chit = objChit;

                        objRegister.InstallmentAmount = Convert.ToDecimal(drData["InstallmentAmount"]);
                        objRegister.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objRegister.BonusAmount = Convert.ToDecimal(drData["BonusAmount"]);
                        objRegister.ReceiptNo = Convert.ToString(drData["ReceiptNo"]);
                        objRegister.sDuedate = Convert.ToDateTime(drData["Duedate"]);
                        objRegister.Duedate = objRegister.sDuedate.ToString("dd/MM/yyyy");
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objRegister.Customer = objCustomer;
                        objRegister.DOB = Convert.ToString(drData["DOB"]);
                        objRegister.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objRegister.Address = Convert.ToString(drData["Address"]);
                        objRegister.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objRegister.Branch = objBranch;

                        objRegister.ProofImage = Convert.ToString(drData["ProofImage"]);
                        objRegister.ProofBackImage = Convert.ToString(drData["ProofBackImage"]);
                        objRegister.RegisterImage1 = Convert.ToString(drData["RegisterImage1"]);
                        objRegister.RegisterImage2 = Convert.ToString(drData["RegisterImage2"]);

                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objRegister.Employee = objEmployee;

                        objRegister.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objRegister);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Register GetRegister | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Register> SearchChitClosed(string ID, int iBranchID,String iStatus)
        {

            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Register> objList = new Collection<Entity.Billing.Register>();
            Entity.Billing.Register objRegister = new Entity.Billing.Register();
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.User objEmployee;
            Entity.Chit objChit;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_CHITCLOSED);
                db.AddInParameter(cmd, "@key", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, iBranchID);
                db.AddInParameter(cmd, "@Status", DbType.String, iStatus);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRegister = new Entity.Billing.Register();
                        objChit = new Entity.Chit();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objEmployee = new Entity.User();

                        objRegister.RegisterID = Convert.ToInt32(drData["PK_RegisterID"]);
                        objRegister.AccountNo = Convert.ToString(drData["AccountNo"]);
                        objRegister.RegisterDate = Convert.ToDateTime(drData["RegisterDate"]);
                        objRegister.sRegisterDate = objRegister.RegisterDate.ToString("dd/MM/yyyy");
                        objRegister.IDProof = Convert.ToString(drData["IDProof"]);
                        objRegister.IDNo = Convert.ToString(drData["IDNo"]);
                        objChit.ChitID = Convert.ToInt32(drData["FK_ChitID"]);
                        objChit.ChitName = Convert.ToString(drData["ChitName"]);
                        objChit.ChitCode = Convert.ToString(drData["ChitCode"]);
                        objChit.Duration = Convert.ToInt32(drData["Duration"]);
                        objRegister.Chit = objChit;

                        objRegister.InstallmentAmount = Convert.ToDecimal(drData["InstallmentAmount"]);
                        objRegister.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objRegister.BonusAmount = Convert.ToDecimal(drData["BonusAmount"]);

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objRegister.Customer = objCustomer;
                        objRegister.DOB = Convert.ToString(drData["DOB"]);
                        objRegister.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objRegister.Address = Convert.ToString(drData["Address"]);
                        objRegister.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objRegister.Branch = objBranch;

                        objRegister.ProofImage = Convert.ToString(drData["ProofImage"]);
                        objRegister.ProofBackImage = Convert.ToString(drData["ProofBackImage"]);
                        objRegister.RegisterImage1 = Convert.ToString(drData["RegisterImage1"]);
                        objRegister.RegisterImage2 = Convert.ToString(drData["RegisterImage2"]);

                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objRegister.Employee = objEmployee;

                        objRegister.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objRegister);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Register GetRegister | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Register> GetRegisterByStatus(int iCustomerID = 0, string Status = null)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Register> objList = new Collection<Entity.Billing.Register>();
            Entity.Billing.Register objRegister = new Entity.Billing.Register();
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.User objEmployee;
            Entity.Chit objChit;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CLOSEDREGISTER);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iCustomerID);
                db.AddInParameter(cmd, "@Status", DbType.String, Status);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRegister = new Entity.Billing.Register();
                        objChit = new Entity.Chit();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objEmployee = new Entity.User();

                        objRegister.RegisterID = Convert.ToInt32(drData["PK_RegisterID"]);
                        objRegister.AccountNo = Convert.ToString(drData["AccountNo"]);
                        objRegister.RegisterDate = Convert.ToDateTime(drData["RegisterDate"]);
                        objRegister.sRegisterDate = objRegister.RegisterDate.ToString("dd/MM/yyyy");
                        objRegister.IDProof = Convert.ToString(drData["IDProof"]);
                        objRegister.IDNo = Convert.ToString(drData["IDNo"]);
                        objChit.ChitID = Convert.ToInt32(drData["FK_ChitID"]);
                        objChit.ChitName = Convert.ToString(drData["ChitName"]);
                        objChit.ChitCode = Convert.ToString(drData["ChitCode"]);
                        objChit.Duration = Convert.ToInt32(drData["Duration"]);
                        objRegister.Chit = objChit;
                        objRegister.InstallmentAmount = Convert.ToDecimal(drData["InstallmentAmount"]);
                        objRegister.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objRegister.BonusAmount = Convert.ToDecimal(drData["BonusAmount"]);

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objRegister.Customer = objCustomer;

                        objRegister.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objRegister.Address = Convert.ToString(drData["Address"]);
                        objRegister.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objRegister.Branch = objBranch;

                       // objRegister.ProofImage = Convert.ToString(drData["ProofImage"]);
                        //objRegister.RegisterImage1 = Convert.ToString(drData["RegisterImage1"]);
                        //objRegister.RegisterImage2 = Convert.ToString(drData["RegisterImage2"]);

                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objRegister.Employee = objEmployee;
                        objRegister.Status = Convert.ToString(drData["Status"]);
                        objRegister.ChitAmount = Convert.ToDecimal(drData["ChitAmount"]);
                        objRegister.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objRegister);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Register GetRegister | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Register> GetClosedRegisterByStatus(int iCustomerID = 0, string Status = null)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Register> objList = new Collection<Entity.Billing.Register>();
            Entity.Billing.Register objRegister = new Entity.Billing.Register();
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.User objEmployee;
            Entity.Chit objChit;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CLOSEDREGISTERS);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iCustomerID);
                db.AddInParameter(cmd, "@Status", DbType.String, Status);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRegister = new Entity.Billing.Register();
                        objChit = new Entity.Chit();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objEmployee = new Entity.User();

                        objRegister.RegisterID = Convert.ToInt32(drData["PK_RegisterID"]);
                        objRegister.AccountNo = Convert.ToString(drData["AccountNo"]);
                        //objRegister.RegisterDate = Convert.ToDateTime(drData["RegisterDate"]);
                       // objRegister.sRegisterDate = objRegister.RegisterDate.ToString("dd/MM/yyyy");
                        //objRegister.IDProof = Convert.ToString(drData["IDProof"]);
                        //objRegister.IDNo = Convert.ToString(drData["IDNo"]);
                       // objChit.ChitID = Convert.ToInt32(drData["FK_ChitID"]);
                       // objChit.ChitName = Convert.ToString(drData["ChitName"]);
                       // objChit.ChitCode = Convert.ToString(drData["ChitCode"]);
                       // objChit.Duration = Convert.ToInt32(drData["Duration"]);
                       // objRegister.Chit = objChit;
                       // objRegister.InstallmentAmount = Convert.ToDecimal(drData["InstallmentAmount"]);
                       // objRegister.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                       // objRegister.BonusAmount = Convert.ToDecimal(drData["BonusAmount"]);

                       // objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                       // objRegister.Customer = objCustomer;

                        //objRegister.CustomerName = Convert.ToString(drData["CustomerName"]);
                        //objRegister.Address = Convert.ToString(drData["Address"]);
                        //objRegister.MobileNo = Convert.ToString(drData["MobileNo"]);
                        //objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        //objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        //objRegister.Branch = objBranch;

                        // objRegister.ProofImage = Convert.ToString(drData["ProofImage"]);
                        //objRegister.RegisterImage1 = Convert.ToString(drData["RegisterImage1"]);
                        //objRegister.RegisterImage2 = Convert.ToString(drData["RegisterImage2"]);

                      //  objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                     //   objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                     //   objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                     //   objRegister.Employee = objEmployee;
                        objRegister.Status = Convert.ToString(drData["Status"]);
                       // objRegister.ChitAmount = Convert.ToDecimal(drData["ChitAmount"]);
                        objRegister.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objRegister);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Register GetRegister | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.Register GetRegisterByID(int iRegisterID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Register objRegister = new Entity.Billing.Register();
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.User objEmployee;
            Entity.Chit objChit;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_REGISTER);
                db.AddInParameter(cmd, "@PK_RegisterID", DbType.Int32, iRegisterID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRegister = new Entity.Billing.Register();
                        objChit = new Entity.Chit();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objEmployee = new Entity.User();

                        objRegister.RegisterID = Convert.ToInt32(drData["PK_RegisterID"]);
                        objRegister.AccountNo = Convert.ToString(drData["AccountNo"]);
                        objRegister.RegisterDate = Convert.ToDateTime(drData["RegisterDate"]);
                        objRegister.sRegisterDate = objRegister.RegisterDate.ToString("dd/MM/yyyy");
                        objRegister.IDProof = Convert.ToString(drData["IDProof"]);
                        objRegister.IDNo = Convert.ToString(drData["IDNo"]);
                        objChit.ChitID = Convert.ToInt32(drData["FK_ChitID"]);
                        objChit.ChitName = Convert.ToString(drData["ChitName"]);
                        objChit.ChitCode = Convert.ToString(drData["ChitCode"]);
                        objChit.Duration = Convert.ToInt32(drData["Duration"]);
                        objRegister.Chit = objChit;
                        objRegister.DOB = Convert.ToString(drData["DOB"]);
                        objRegister.InstallmentAmount = Convert.ToDecimal(drData["InstallmentAmount"]);
                        objRegister.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objRegister.BonusAmount = Convert.ToDecimal(drData["BonusAmount"]);

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objRegister.Customer = objCustomer;
                        objRegister.ReceiptNo = Convert.ToString(drData["ReceiptNo"]);
                        objRegister.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objRegister.Address = Convert.ToString(drData["Address"]);
                        objRegister.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objRegister.Branch = objBranch;

                        objRegister.ProofImage = Convert.ToString(drData["ProofImage"]);
                        objRegister.ProofBackImage = Convert.ToString(drData["ProofBackImage"]);
                        objRegister.RegisterImage1 = Convert.ToString(drData["RegisterImage1"]);
                        objRegister.RegisterImage2 = Convert.ToString(drData["RegisterImage2"]);

                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objRegister.Employee = objEmployee;

                        objRegister.Status = Convert.ToString(drData["Status"]);
                        objRegister.ChitAmount = Convert.ToDecimal(drData["ChitAmount"]);


                        objRegister.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Register GetRegisterByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objRegister;
        }

        public static Entity.Billing.Register GetRegisterByNo(string iRegisterID, string IsActive, int iRegionID = 0, int iZoneID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Register objRegister = new Entity.Billing.Register();
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.User objEmployee;
            Entity.Chit objChit;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_REGISTERBYNO);
                db.AddInParameter(cmd, "@PK_RegisterID", DbType.String, iRegisterID);
                db.AddInParameter(cmd, "@IsActive", DbType.String, IsActive);
                db.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, iRegionID);
                db.AddInParameter(cmd, "@FK_ZoneID", DbType.Int32, iZoneID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRegister = new Entity.Billing.Register();
                        objChit = new Entity.Chit();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objEmployee = new Entity.User();
                        objRegister.DOB = Convert.ToString(drData["DOB"]);
                        objRegister.RegisterID = Convert.ToInt32(drData["PK_RegisterID"]);
                        objRegister.AccountNo = Convert.ToString(drData["AccountNo"]);
                        objRegister.RegisterDate = Convert.ToDateTime(drData["RegisterDate"]);
                        objRegister.sRegisterDate = objRegister.RegisterDate.ToString("dd/MM/yyyy");
                        objRegister.IDProof = Convert.ToString(drData["IDProof"]);
                        objRegister.IDNo = Convert.ToString(drData["IDNo"]);
                        objChit.ChitID = Convert.ToInt32(drData["FK_ChitID"]);
                        objChit.ChitName = Convert.ToString(drData["ChitName"]);
                        objChit.ChitCode = Convert.ToString(drData["ChitCode"]);
                        objChit.Duration = Convert.ToInt32(drData["Duration"]);
                        objRegister.Chit = objChit;
                        objRegister.RenewalCount= Convert.ToInt32(drData["RenewalCount"]);
                        objRegister.InstallmentAmount = Convert.ToDecimal(drData["InstallmentAmount"]);
                        objRegister.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objRegister.BonusAmount = Convert.ToDecimal(drData["BonusAmount"]);

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objRegister.Customer = objCustomer;

                        objRegister.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objRegister.Address = Convert.ToString(drData["Address"]);
                        objRegister.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objRegister.Branch = objBranch;

                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objRegister.Employee = objEmployee;

                        objRegister.ProofImage = Convert.ToString(drData["ProofImage"]);
                        objRegister.ProofBackImage = Convert.ToString(drData["ProofBackImage"]);
                        objRegister.RegisterImage1 = Convert.ToString(drData["RegisterImage1"]);
                        objRegister.RegisterImage2 = Convert.ToString(drData["RegisterImage2"]);

                        objRegister.Status = Convert.ToString(drData["Status"]);
                        objRegister.ChitAmount = Convert.ToDecimal(drData["ChitAmount"]);

                        objRegister.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Register GetRegisterByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objRegister;
        }
        public static int AddRegister(Entity.Billing.Register objRegister)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddRegister(oDb, objRegister, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tRegister", "PK_RegisterID", objRegister.RegisterID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objRegister.CreatedBy.UserID);
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
        private static int AddRegister(Database oDb, Entity.Billing.Register objRegister, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_REGISTER);
                oDb.AddOutParameter(cmd, "@PK_RegisterID", DbType.Int32, objRegister.RegisterID);
                oDb.AddInParameter(cmd, "@RegisterDate", DbType.String, objRegister.sRegisterDate);
                oDb.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objRegister.Customer.CustomerID);
                oDb.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, objRegister.Branch.BranchID);
                oDb.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, objRegister.Employee.UserID);
                oDb.AddInParameter(cmd, "@FK_ChitID", DbType.Int32, objRegister.Chit.ChitID);
                oDb.AddInParameter(cmd, "@RegisterImage1", DbType.String, objRegister.RegisterImage1);
                oDb.AddInParameter(cmd, "@RegisterImage2", DbType.String, objRegister.RegisterImage2);
                oDb.AddInParameter(cmd, "@ProofBackImage", DbType.String, objRegister.ProofBackImage);
                oDb.AddInParameter(cmd, "@ProofImage", DbType.String, objRegister.ProofImage);
                oDb.AddInParameter(cmd, "@CustomerName", DbType.String, objRegister.CustomerName);
                oDb.AddInParameter(cmd, "@Address", DbType.String, objRegister.Address);
                oDb.AddInParameter(cmd, "@MobileNo", DbType.String, objRegister.MobileNo);
                oDb.AddInParameter(cmd, "@DOB", DbType.String, objRegister.DOB);
                oDb.AddInParameter(cmd, "@InstallmentAmount", DbType.Decimal, objRegister.InstallmentAmount);
                oDb.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objRegister.TotalAmount);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, true);
                oDb.AddInParameter(cmd, "@IDProof", DbType.String, objRegister.IDProof);
                oDb.AddInParameter(cmd, "@IDNo", DbType.String, objRegister.IDNo);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objRegister.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_RegisterID"));
                    objRegister.RegisterID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Register AddRegister | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateRegister(Entity.Billing.Register objRegister)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateRegister(oDb, objRegister, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tRegister", "PK_RegisterID", objRegister.RegisterID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objRegister.ModifiedBy.UserID);
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
        private static bool UpdateRegister(Database oDb, Entity.Billing.Register objRegister, DbTransaction oTrans)
        {
            string sException = string.Empty; Entity.Branch objEmployee;
            int iID = 0;
            bool bResult = false;
            try
            {
                objEmployee = new Entity.Branch();
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_REGISTER);
                oDb.AddInParameter(cmd, "@PK_RegisterID", DbType.Int32, objRegister.RegisterID);
                oDb.AddInParameter(cmd, "@RegisterDate", DbType.String, objRegister.sRegisterDate);
                oDb.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objRegister.Customer.CustomerID);
                oDb.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, objRegister.Employee.UserID);
                //if (objRegister.Employee.UserID == 1008)
                //    objRegister.BranchID = 1;
                //else
                //    objRegister.BranchID = objRegister.Branch.BranchID;
              /// oDb.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, objRegister.Branch.BranchID);
                oDb.AddInParameter(cmd, "@FK_ChitID", DbType.Int32, objRegister.Chit.ChitID);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objRegister.IsActive);
                oDb.AddInParameter(cmd, "@IDProof", DbType.String, objRegister.IDProof);
                oDb.AddInParameter(cmd, "@RegisterImage1", DbType.String, objRegister.RegisterImage1);
                oDb.AddInParameter(cmd, "@RegisterImage2", DbType.String, objRegister.RegisterImage2);
                oDb.AddInParameter(cmd, "@ProofBackImage", DbType.String, objRegister.ProofBackImage);
                oDb.AddInParameter(cmd, "@ProofImage", DbType.String, objRegister.ProofImage);
                oDb.AddInParameter(cmd, "@CustomerName", DbType.String, objRegister.CustomerName);
                oDb.AddInParameter(cmd, "@Address", DbType.String, objRegister.Address);
                oDb.AddInParameter(cmd, "@MobileNo", DbType.String, objRegister.MobileNo);
                oDb.AddInParameter(cmd, "@DOB", DbType.String, objRegister.DOB);
                oDb.AddInParameter(cmd, "@InstallmentAmount", DbType.Decimal, objRegister.InstallmentAmount);
                oDb.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objRegister.TotalAmount);
                oDb.AddInParameter(cmd, "@IDNo", DbType.String, objRegister.IDNo);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objRegister.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Register UpdateRegister | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteRegister(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteRegister(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tRegister", "PK_RegisterID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteRegister(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_REGISTER);
                oDb.AddInParameter(cmd, "@PK_RegisterID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Register DeleteRegister | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

        public static Collection<Entity.Billing.Register> GetCancelledRegister(int iCustomerID = 0, int iRegionID = 0, int iZoneID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Register> objList = new Collection<Entity.Billing.Register>();
            Entity.Billing.Register objRegister = new Entity.Billing.Register();
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.User objEmployee;
            Entity.Chit objChit; Entity.User objCancelledBy;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CANCELLEDREGISTER);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iCustomerID);
                db.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, iRegionID);
                db.AddInParameter(cmd, "@FK_ZoneID", DbType.Int32, iZoneID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRegister = new Entity.Billing.Register();
                        objChit = new Entity.Chit();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objEmployee = new Entity.User();
                        objCancelledBy = new Entity.User();

                        objRegister.RegisterID = Convert.ToInt32(drData["PK_RegisterID"]);
                        objRegister.AccountNo = Convert.ToString(drData["AccountNo"]);
                        objRegister.RegisterDate = Convert.ToDateTime(drData["RegisterDate"]);
                        objRegister.sRegisterDate = objRegister.RegisterDate.ToString("dd/MM/yyyy");
                        objRegister.IDProof = Convert.ToString(drData["IDProof"]);
                        objRegister.IDNo = Convert.ToString(drData["IDNo"]);
                        objChit.ChitID = Convert.ToInt32(drData["FK_ChitID"]);
                        objChit.ChitName = Convert.ToString(drData["ChitName"]);
                        objChit.ChitCode = Convert.ToString(drData["ChitCode"]);
                        objChit.Duration = Convert.ToInt32(drData["Duration"]);
                        objRegister.Chit = objChit;

                        objRegister.InstallmentAmount = Convert.ToDecimal(drData["InstallmentAmount"]);
                        objRegister.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objRegister.BonusAmount = Convert.ToDecimal(drData["BonusAmount"]);

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objRegister.Customer = objCustomer;

                        objRegister.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objRegister.Address = Convert.ToString(drData["Address"]);
                        objRegister.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objRegister.Branch = objBranch;


                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objRegister.Employee = objEmployee;

                        objCancelledBy.UserID = Convert.ToInt32(drData["CancelledBy"]);
                        objCancelledBy.EmployeeName = Convert.ToString(drData["CancelledEmployeeName"]);
                        objCancelledBy.EmployeeCode = Convert.ToString(drData["CancelledEmployeeCode"]);
                        objRegister.CancelledBy = objCancelledBy;

                        objRegister.CancelledDate = Convert.ToDateTime(drData["CancelledDate"]);
                        objRegister.sCancelledDate = objRegister.CancelledDate.ToString("dd/MM/yyyy");
                        objRegister.ReasonforCancel = Convert.ToString(drData["ReasonforCancel"]);
                        objRegister.ChitAmount = Convert.ToDecimal(drData["ChitAmount"]);
                        objRegister.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objRegister);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Register GetRegister | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static bool UpdateCancelledRegister(Entity.Billing.Register objRegister)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateCancelledRegister(oDb, objRegister, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tRegister", "PK_RegisterID", objRegister.RegisterID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objRegister.CancelledBy.UserID);
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
        private static bool UpdateCancelledRegister(Database oDb, Entity.Billing.Register objRegister, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_CANCELLEDREGISTER);
                oDb.AddInParameter(cmd, "@PK_RegisterID", DbType.Int32, objRegister.RegisterID);
                oDb.AddInParameter(cmd, "@ReasonforCancel", DbType.String, objRegister.ReasonforCancel);
                oDb.AddInParameter(cmd, "@CancelledBy", DbType.Int32, objRegister.CancelledBy.UserID);
                oDb.AddInParameter(cmd, "@BonusAmount", DbType.Decimal, objRegister.BonusAmount);
                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Register UpdateRegister | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
