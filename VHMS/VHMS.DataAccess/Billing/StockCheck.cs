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
    public class StockCheck
    {
        public static Collection<Entity.Billing.StockCheck> GetStockCheck(int ipatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.StockCheck> objList = new Collection<Entity.Billing.StockCheck>();
            Entity.Billing.StockCheck objStockCheck;
            Entity.Branch objBranch; 
            Entity.User objEmployee; 

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_STOCKCHECK);
                db.AddInParameter(cmd, "@PK_StockCheckID", DbType.Int32, ipatientID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objStockCheck = new Entity.Billing.StockCheck();
                        objBranch = new Entity.Branch();
                        objEmployee = new Entity.User();

                        objStockCheck.StockCheckID = Convert.ToInt32(drData["PK_StockCheckID"]);
                        objStockCheck.CheckDate = Convert.ToDateTime(drData["CheckDate"]);
                        objStockCheck.sCheckDate = objStockCheck.CheckDate.ToString("dd/MM/yyyy");
                        objStockCheck.Status = Convert.ToString(drData["Status"]);
                        objStockCheck.StockCheckNo = Convert.ToInt32(drData["StockCheckNo"]);
                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objStockCheck.Employee = objEmployee;
                        objList.Add(objStockCheck);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "StockCheck GetStockCheck | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.StockCheck> GetTopStockCheck(int ipatientID = 0, int iBranchID=0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.StockCheck> objList = new Collection<Entity.Billing.StockCheck>();
            Entity.Billing.StockCheck objStockCheck;
            Entity.Branch objBranch;
            Entity.User objEmployee;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPSTOCKCHECK);
                db.AddInParameter(cmd, "@PK_StockCheckID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, iBranchID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objStockCheck = new Entity.Billing.StockCheck();
                        objBranch = new Entity.Branch();
                        objEmployee = new Entity.User();

                        objStockCheck.StockCheckID = Convert.ToInt32(drData["PK_StockCheckID"]);
                        objStockCheck.CheckDate = Convert.ToDateTime(drData["CheckDate"]);
                        objStockCheck.sCheckDate = objStockCheck.CheckDate.ToString("dd/MM/yyyy");

                        objStockCheck.Status = Convert.ToString(drData["Status"]);
                        objStockCheck.StockCheckNo = Convert.ToInt32(drData["StockCheckNo"]);

                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objStockCheck.Employee = objEmployee;
                        objList.Add(objStockCheck);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "StockCheck GetStockCheck | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.StockCheck> SearchStockCheck(string ID, int iBranchID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.StockCheck> objList = new Collection<Entity.Billing.StockCheck>();
            Entity.Billing.StockCheck objStockCheck;
            Entity.Branch objBranch;
            Entity.User objEmployee;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_STOCKCHECK);
                db.AddInParameter(cmd, "@key", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, iBranchID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objStockCheck = new Entity.Billing.StockCheck();
                        objBranch = new Entity.Branch();
                        objEmployee = new Entity.User();

                        objStockCheck.StockCheckID = Convert.ToInt32(drData["PK_StockCheckID"]);
                        objStockCheck.CheckDate = Convert.ToDateTime(drData["CheckDate"]);
                        objStockCheck.sCheckDate = objStockCheck.CheckDate.ToString("dd/MM/yyyy");

                        objStockCheck.Status = Convert.ToString(drData["Status"]);
                        objStockCheck.StockCheckNo = Convert.ToInt32(drData["StockCheckNo"]);
                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objStockCheck.Employee = objEmployee;
                        objList.Add(objStockCheck);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "StockCheck GetStockCheck | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.StockCheck> GetStockCheckID(int ipatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.StockCheck> objList = new Collection<Entity.Billing.StockCheck>();
            Entity.Billing.StockCheck objStockCheck;
            Entity.Branch objBranch;
            Entity.User objEmployee; 

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_STOCKCHECK);
                db.AddInParameter(cmd, "@PK_StockCheckID", DbType.Int32, ipatientID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objStockCheck = new Entity.Billing.StockCheck();

                        objBranch = new Entity.Branch();
                        objEmployee = new Entity.User();


                        objStockCheck.StockCheckID = Convert.ToInt32(drData["PK_StockCheckID"]);
                        objStockCheck.CheckDate = Convert.ToDateTime(drData["CheckDate"]);
                        objStockCheck.sCheckDate = objStockCheck.CheckDate.ToString("dd/MM/yyyy");

                        objStockCheck.Status = Convert.ToString(drData["Status"]);
                        objStockCheck.StockCheckNo = Convert.ToInt32(drData["StockCheckNo"]);

                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objStockCheck.Employee = objEmployee;
                        objList.Add(objStockCheck);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "StockCheck GetStockCheck | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        
        public static Entity.Billing.StockCheck GetStockCheckByID(int iStockCheckID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.StockCheck objStockCheck = new Entity.Billing.StockCheck();
             Entity.Branch objBranch; 
            Entity.User objEmployee; 

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_STOCKCHECK);
                db.AddInParameter(cmd, "@PK_StockCheckID", DbType.Int32, iStockCheckID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objStockCheck = new Entity.Billing.StockCheck();
                        objBranch = new Entity.Branch();
                        objEmployee = new Entity.User();



                        objStockCheck.StockCheckID = Convert.ToInt32(drData["PK_StockCheckID"]);
                        objStockCheck.CheckDate = Convert.ToDateTime(drData["CheckDate"]);
                        objStockCheck.sCheckDate = objStockCheck.CheckDate.ToString("dd/MM/yyyy");

                        objStockCheck.Status = Convert.ToString(drData["Status"]);
                        objStockCheck.StockCheckNo = Convert.ToInt32(drData["StockCheckNo"]);

                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objStockCheck.Employee = objEmployee;
                        objStockCheck.StockCheckTrans = StockCheckTrans.GetStockCheckTransByStockCheckID(objStockCheck.StockCheckID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "StockCheck GetStockCheckByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objStockCheck;
        }

        public static Entity.Billing.StockCheck GetStockCheckByInvoice(string InvoiceNo)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.StockCheck objStockCheck = new Entity.Billing.StockCheck();
            Entity.Branch objBranch;
            Entity.User objEmployee; 

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_STOCKCHECKINVOICE);
                db.AddInParameter(cmd, "@InvoiceNo", DbType.String, InvoiceNo);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objStockCheck = new Entity.Billing.StockCheck();
                        objBranch = new Entity.Branch();
                        objEmployee = new Entity.User();



                        objStockCheck.StockCheckID = Convert.ToInt32(drData["PK_StockCheckID"]);
                        objStockCheck.CheckDate = Convert.ToDateTime(drData["CheckDate"]);
                        objStockCheck.sCheckDate = objStockCheck.CheckDate.ToString("dd/MM/yyyy");

                        objStockCheck.Status = Convert.ToString(drData["Status"]);
                        objStockCheck.StockCheckNo = Convert.ToInt32(drData["StockCheckNo"]);

                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objStockCheck.Employee = objEmployee;
                        objStockCheck.StockCheckTrans = StockCheckTrans.GetStockCheckTransByStockCheckID(objStockCheck.StockCheckID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "StockCheck GetStockCheckByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objStockCheck;
        }
        public static int AddStockCheck(Entity.Billing.StockCheck objStockCheck)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_STOCKCHECK);
                db.AddOutParameter(cmd, "@PK_StockCheckID", DbType.Int32, objStockCheck.StockCheckID);
                db.AddInParameter(cmd, "@CheckDate", DbType.String, objStockCheck.sCheckDate);
                db.AddInParameter(cmd, "@Status", DbType.String, objStockCheck.Status);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, objStockCheck.Employee.UserID);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objStockCheck.CreatedBy.UserID);
                db.AddInParameter(cmd, "@StockCheckNo", DbType.Int32, objStockCheck.StockCheckNo);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_StockCheckID"));

                foreach (Entity.Billing.StockCheckTrans ObjStockCheckTrans in objStockCheck.StockCheckTrans)
                    ObjStockCheckTrans.StockCheckID = iID;

                StockCheckTrans.SaveStockCheckTransaction(objStockCheck.StockCheckTrans);
            }
            catch (Exception ex)
            {
                sException = "StockCheck AddStockCheck | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateStockCheck(Entity.Billing.StockCheck objStockCheck)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_STOCKCHECK);
                db.AddInParameter(cmd, "@PK_StockCheckID", DbType.Int32, objStockCheck.StockCheckID);
                db.AddInParameter(cmd, "@CheckDate", DbType.String, objStockCheck.sCheckDate);
                db.AddInParameter(cmd, "@Status", DbType.String, objStockCheck.Status);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, objStockCheck.Employee.UserID);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objStockCheck.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@StockCheckNo", DbType.Int32, objStockCheck.StockCheckNo);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                foreach (Entity.Billing.StockCheckTrans ObjStockCheckTrans in objStockCheck.StockCheckTrans)
                    ObjStockCheckTrans.StockCheckID = objStockCheck.StockCheckID;

                StockCheckTrans.SaveStockCheckTransaction(objStockCheck.StockCheckTrans);
            }
            catch (Exception ex)
            {
                sException = "StockCheck UpdateStockCheck| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteStockCheck(int iStockCheckId, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_STOCKCHECK);
                db.AddInParameter(cmd, "@PK_StockCheckID", DbType.Int32, iStockCheckId);
             
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "StockCheck DeleteStockCheck | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static DataSet GetStockCheckSummary()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_STOCKCHECKSUMMARY);
                dsList = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "StockCheck GetStockCheckSummary | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsList;
        }
    }

    public class StockCheckTrans
    {
        public static Collection<Entity.Billing.StockCheckTrans> GetStockCheckTransByStockCheckID(int iStockCheckID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.StockCheckTrans> objList = new Collection<Entity.Billing.StockCheckTrans>();
            Entity.Billing.StockCheckTrans objStockCheckTrans = new Entity.Billing.StockCheckTrans();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_STOCKCHECKTRANS);
                db.AddInParameter(cmd, "@FK_StockCheckID", DbType.Int32, iStockCheckID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objStockCheckTrans = new Entity.Billing.StockCheckTrans();

                        objStockCheckTrans.StockCheckTransID = Convert.ToInt32(drData["PK_StockCheckTransID"]);
                        objStockCheckTrans.StockCheckID = Convert.ToInt32(drData["FK_StockCheckID"]);
                        objStockCheckTrans.StockID = Convert.ToInt32(drData["Fk_StockID"]);
                        objStockCheckTrans.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objStockCheckTrans.Barcode = Convert.ToString(drData["Barcode"]);

                        objStockCheckTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objStockCheckTrans.ProductName = Convert.ToString(drData["ProductName"]);

                        objList.Add(objStockCheckTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "StockCheckTrans GetStockCheckTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveStockCheckTransaction(Collection<Entity.Billing.StockCheckTrans> ObjStockCheckTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.StockCheckTrans ObjStockCheckTransaction in ObjStockCheckTransList)
            {
                if (ObjStockCheckTransaction.StatusFlag == "I")
                    iID = AddStockCheckTrans(ObjStockCheckTransaction);
                else if (ObjStockCheckTransaction.StatusFlag == "U")
                    bResult = UpdateStockCheckTrans(ObjStockCheckTransaction);
                else if (ObjStockCheckTransaction.StatusFlag == "D")
                    bResult = DeleteStockCheckTrans(ObjStockCheckTransaction.StockCheckTransID);
            }
        }
        public static int AddStockCheckTrans(Entity.Billing.StockCheckTrans objStockCheckTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_STOCKCHECKTRANS);
                db.AddOutParameter(cmd, "@PK_StockCheckTransID", DbType.Int32, objStockCheckTrans.StockCheckTransID);
                db.AddInParameter(cmd, "@FK_StockCheckID", DbType.Int32, objStockCheckTrans.StockCheckID);
                db.AddInParameter(cmd, "@FK_StockID", DbType.Int32, objStockCheckTrans.StockID);
                db.AddInParameter(cmd, "@Barcode", DbType.String, objStockCheckTrans.Barcode);
                db.AddInParameter(cmd, "@SMSCode", DbType.String, objStockCheckTrans.SMSCode);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objStockCheckTrans.Quantity);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_StockCheckTransID"));
            }
            catch (Exception ex)
            {
                sException = "StockCheckTrans AddStockCheckTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateStockCheckTrans(Entity.Billing.StockCheckTrans objStockCheckTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_STOCKCHECKTRANS);
                db.AddInParameter(cmd, "@PK_StockCheckTransID", DbType.Int32, objStockCheckTrans.StockCheckTransID);
                db.AddInParameter(cmd, "@FK_StockCheckID", DbType.Int32, objStockCheckTrans.StockCheckID);
                db.AddInParameter(cmd, "@FK_StockID", DbType.Int32, objStockCheckTrans.StockID);
                db.AddInParameter(cmd, "@Barcode", DbType.String, objStockCheckTrans.Barcode);
                db.AddInParameter(cmd, "@SMSCode", DbType.String, objStockCheckTrans.SMSCode);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objStockCheckTrans.Quantity);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "StockCheckTrans UpdateStockCheckTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteStockCheckTrans(int iStockCheckTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_STOCKCHECKTRANS);
                db.AddInParameter(cmd, "@PK_StockCheckTransID", DbType.Int32, iStockCheckTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "StockCheckTrans DeleteStockCheckTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        
    }

}
