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
    public class PurchaseTDSPayment
    {
        public static Collection<Entity.Billing.PurchaseTDSPayment> GetPurchaseTDSPayment(int ipatientID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.PurchaseTDSPayment> objList = new Collection<Entity.Billing.PurchaseTDSPayment>();
            Entity.Billing.PurchaseTDSPayment objPurchaseTDSPayment;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASETDSPAYMENT);
                db.AddInParameter(cmd, "@PK_TDSPaymentID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchaseTDSPayment = new Entity.Billing.PurchaseTDSPayment();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();

                        objPurchaseTDSPayment.TDSPaymentID = Convert.ToInt32(drData["PK_TDSPaymentID"]);
                        objPurchaseTDSPayment.TDSPaymentNo = Convert.ToString(drData["TDSPaymentNo"]);
                        objPurchaseTDSPayment.TDSPaymentDate = Convert.ToDateTime(drData["TDSPaymentDate"]);
                        objPurchaseTDSPayment.sTDSPaymentDate = objPurchaseTDSPayment.TDSPaymentDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objPurchaseTDSPayment.Customer = objCustomer;

                        objPurchaseTDSPayment.Narration = Convert.ToString(drData["Narration"]);
                        objPurchaseTDSPayment.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objPurchaseTDSPayment.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchaseTDSPayment.Roundoff = Convert.ToDecimal(drData["RoundOff"]);
                        objPurchaseTDSPayment.PaymentType = Convert.ToInt32(drData["PaymentType"]);
                        objPurchaseTDSPayment.BillType = Convert.ToString(drData["BillType"]);

                        objPurchaseTDSPayment.SlipNo = Convert.ToString(drData["SlipNo"]);
                        objPurchaseTDSPayment.SlipDate = Convert.ToDateTime(drData["SlipDate"]);
                        objPurchaseTDSPayment.sSlipDate = objPurchaseTDSPayment.SlipDate.ToString("dd/MM/yyyy");

                        objList.Add(objPurchaseTDSPayment);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "PurchaseTDSPayment GetPurchaseTDSPayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.PurchaseTDSPayment> GetTopPurchaseTDSPayment(int ipatientID = 0, int iBranchID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.PurchaseTDSPayment> objList = new Collection<Entity.Billing.PurchaseTDSPayment>();
            Entity.Billing.PurchaseTDSPayment objPurchaseTDSPayment;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;

            try
            {

                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASETDSPAYMENT);
                db.AddInParameter(cmd, "@PK_TDSPaymentID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchaseTDSPayment = new Entity.Billing.PurchaseTDSPayment();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();

                        objPurchaseTDSPayment.TDSPaymentID = Convert.ToInt32(drData["PK_TDSPaymentID"]);
                        objPurchaseTDSPayment.TDSPaymentNo = Convert.ToString(drData["TDSPaymentNo"]);
                        objPurchaseTDSPayment.TDSPaymentDate = Convert.ToDateTime(drData["TDSPaymentDate"]);
                        objPurchaseTDSPayment.sTDSPaymentDate = objPurchaseTDSPayment.TDSPaymentDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objPurchaseTDSPayment.Customer = objCustomer;

                        objPurchaseTDSPayment.Narration = Convert.ToString(drData["Narration"]);
                        objPurchaseTDSPayment.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objPurchaseTDSPayment.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchaseTDSPayment.Roundoff = Convert.ToDecimal(drData["RoundOff"]);
                        objPurchaseTDSPayment.PaymentType = Convert.ToInt32(drData["PaymentType"]);
                        objPurchaseTDSPayment.BillType = Convert.ToString(drData["BillType"]);

                        objPurchaseTDSPayment.SlipNo = Convert.ToString(drData["SlipNo"]);
                        objPurchaseTDSPayment.SlipDate = Convert.ToDateTime(drData["SlipDate"]);
                        objPurchaseTDSPayment.sSlipDate = objPurchaseTDSPayment.SlipDate.ToString("dd/MM/yyyy");

                        objList.Add(objPurchaseTDSPayment);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "PurchaseTDSPayment GetPurchaseTDSPayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.PurchaseTDSPayment> GetPurchaseTDSPaymentID(int ipatientID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.PurchaseTDSPayment> objList = new Collection<Entity.Billing.PurchaseTDSPayment>();
            Entity.Billing.PurchaseTDSPayment objPurchaseTDSPayment;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASETDSPAYMENT);
                db.AddInParameter(cmd, "@PK_TDSPaymentID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchaseTDSPayment = new Entity.Billing.PurchaseTDSPayment();

                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();

                        objPurchaseTDSPayment.TDSPaymentID = Convert.ToInt32(drData["PK_TDSPaymentID"]);
                        objPurchaseTDSPayment.TDSPaymentNo = Convert.ToString(drData["TDSPaymentNo"]);
                        objPurchaseTDSPayment.TDSPaymentDate = Convert.ToDateTime(drData["TDSPaymentDate"]);
                        objPurchaseTDSPayment.sTDSPaymentDate = objPurchaseTDSPayment.TDSPaymentDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objPurchaseTDSPayment.Customer = objCustomer;

                        objPurchaseTDSPayment.Narration = Convert.ToString(drData["Narration"]);
                        objPurchaseTDSPayment.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objPurchaseTDSPayment.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchaseTDSPayment.Roundoff = Convert.ToDecimal(drData["RoundOff"]);
                        objPurchaseTDSPayment.PaymentType = Convert.ToInt32(drData["PaymentType"]);
                        objPurchaseTDSPayment.BillType = Convert.ToString(drData["BillType"]);

                        objPurchaseTDSPayment.SlipNo = Convert.ToString(drData["SlipNo"]);
                        objPurchaseTDSPayment.SlipDate = Convert.ToDateTime(drData["SlipDate"]);
                        objPurchaseTDSPayment.sSlipDate = objPurchaseTDSPayment.SlipDate.ToString("dd/MM/yyyy");

                        objList.Add(objPurchaseTDSPayment);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "PurchaseTDSPayment GetPurchaseTDSPayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.PurchaseTDSPayment> SearchPurchaseTDSPayment(string ID, int iBranchID, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.PurchaseTDSPayment> objList = new Collection<Entity.Billing.PurchaseTDSPayment>();
            Entity.Billing.PurchaseTDSPayment objPurchaseTDSPayment;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_PURCHASETDSPAYMENT);
                db.AddInParameter(cmd, "@key", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchaseTDSPayment = new Entity.Billing.PurchaseTDSPayment();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();

                        objPurchaseTDSPayment.TDSPaymentID = Convert.ToInt32(drData["PK_TDSPaymentID"]);
                        objPurchaseTDSPayment.TDSPaymentNo = Convert.ToString(drData["TDSPaymentNo"]);
                        objPurchaseTDSPayment.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objPurchaseTDSPayment.TDSPaymentDate = Convert.ToDateTime(drData["TDSPaymentDate"]);
                        objPurchaseTDSPayment.sTDSPaymentDate = objPurchaseTDSPayment.TDSPaymentDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objPurchaseTDSPayment.Customer = objCustomer;

                        objPurchaseTDSPayment.Narration = Convert.ToString(drData["Narration"]);
                        objPurchaseTDSPayment.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchaseTDSPayment.Roundoff = Convert.ToDecimal(drData["RoundOff"]);
                        objPurchaseTDSPayment.PaymentType = Convert.ToInt32(drData["PaymentType"]);
                        objPurchaseTDSPayment.BillType = Convert.ToString(drData["BillType"]);

                        objPurchaseTDSPayment.SlipNo = Convert.ToString(drData["SlipNo"]);
                        objPurchaseTDSPayment.SlipDate = Convert.ToDateTime(drData["SlipDate"]);
                        objPurchaseTDSPayment.sSlipDate = objPurchaseTDSPayment.SlipDate.ToString("dd/MM/yyyy");

                        objList.Add(objPurchaseTDSPayment);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "PurchaseTDSPayment GetPurchaseTDSPayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Entity.Billing.PurchaseTDSPayment GetPurchaseTDSPaymentByID(int iTDSPaymentID, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.PurchaseTDSPayment objPurchaseTDSPayment = new Entity.Billing.PurchaseTDSPayment();
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASETDSPAYMENT);
                db.AddInParameter(cmd, "@PK_TDSPaymentID", DbType.Int32, iTDSPaymentID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchaseTDSPayment = new Entity.Billing.PurchaseTDSPayment();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();

                        objPurchaseTDSPayment.TDSPaymentID = Convert.ToInt32(drData["PK_TDSPaymentID"]);
                        objPurchaseTDSPayment.TDSPaymentNo = Convert.ToString(drData["TDSPaymentNo"]);
                        objPurchaseTDSPayment.TDSPaymentDate = Convert.ToDateTime(drData["TDSPaymentDate"]);
                        objPurchaseTDSPayment.sTDSPaymentDate = objPurchaseTDSPayment.TDSPaymentDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objPurchaseTDSPayment.Customer = objCustomer;

                        objPurchaseTDSPayment.Narration = Convert.ToString(drData["Narration"]);
                        objPurchaseTDSPayment.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchaseTDSPayment.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objPurchaseTDSPayment.Roundoff = Convert.ToDecimal(drData["RoundOff"]);
                        objPurchaseTDSPayment.PaymentType = Convert.ToInt32(drData["PaymentType"]); 
                        objPurchaseTDSPayment.BillType = Convert.ToString(drData["BillType"]);

                        objPurchaseTDSPayment.SlipNo = Convert.ToString(drData["SlipNo"]);
                        objPurchaseTDSPayment.SlipDate = Convert.ToDateTime(drData["SlipDate"]);
                        objPurchaseTDSPayment.sSlipDate = objPurchaseTDSPayment.SlipDate.ToString("dd/MM/yyyy");

                        objPurchaseTDSPayment.PurchaseTDSPaymentTrans = PurchaseTDSPaymentTrans.GetPurchaseTDSPaymentTransByTDSPaymentID(objPurchaseTDSPayment.TDSPaymentID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "PurchaseTDSPayment GetPurchaseTDSPaymentByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objPurchaseTDSPayment;
        }
        public static int AddPurchaseTDSPayment(Entity.Billing.PurchaseTDSPayment objPurchaseTDSPayment)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_PURCHASETDSPAYMENT);
                db.AddOutParameter(cmd, "@PK_TDSPaymentID", DbType.Int32, objPurchaseTDSPayment.TDSPaymentID);
                db.AddInParameter(cmd, "@TDSPaymentNo", DbType.String, objPurchaseTDSPayment.TDSPaymentNo);
                db.AddInParameter(cmd, "@TDSPaymentDate", DbType.String, objPurchaseTDSPayment.sTDSPaymentDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objPurchaseTDSPayment.Customer.CustomerID);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objPurchaseTDSPayment.TotalAmount);
                db.AddInParameter(cmd, "@PaymentType", DbType.Int32, objPurchaseTDSPayment.PaymentType);
                db.AddInParameter(cmd, "@BillType", DbType.String, objPurchaseTDSPayment.BillType);
                db.AddInParameter(cmd, "@Narration", DbType.String, objPurchaseTDSPayment.Narration);
                db.AddInParameter(cmd, "@DocumentPath", DbType.String, objPurchaseTDSPayment.DocumentPath);
                db.AddInParameter(cmd, "@RoundOff", DbType.Decimal, objPurchaseTDSPayment.Roundoff);
                db.AddInParameter(cmd, "@SlipNo", DbType.String, objPurchaseTDSPayment.SlipNo);
                db.AddInParameter(cmd, "@SlipDate", DbType.String, objPurchaseTDSPayment.sSlipDate);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objPurchaseTDSPayment.CreatedBy.UserID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objPurchaseTDSPayment.FinancialYear.FinancialYearID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_TDSPaymentID"));

                foreach (Entity.Billing.PurchaseTDSPaymentTrans ObjPurchaseTDSPaymentTrans in objPurchaseTDSPayment.PurchaseTDSPaymentTrans)
                    ObjPurchaseTDSPaymentTrans.TDSPaymentID = iID;

                PurchaseTDSPaymentTrans.SavePurchaseTDSPaymentTransaction(objPurchaseTDSPayment.PurchaseTDSPaymentTrans);
            }
            catch (Exception ex)
            {
                sException = "PurchaseTDSPayment AddPurchaseTDSPayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdatePurchaseTDSPayment(Entity.Billing.PurchaseTDSPayment objPurchaseTDSPayment)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_PURCHASETDSPAYMENT);
                db.AddInParameter(cmd, "@PK_TDSPaymentID", DbType.Int32, objPurchaseTDSPayment.TDSPaymentID);
                db.AddInParameter(cmd, "@TDSPaymentNo", DbType.String, objPurchaseTDSPayment.TDSPaymentNo);
                db.AddInParameter(cmd, "@TDSPaymentDate", DbType.String, objPurchaseTDSPayment.sTDSPaymentDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objPurchaseTDSPayment.Customer.CustomerID);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objPurchaseTDSPayment.TotalAmount);
                db.AddInParameter(cmd, "@PaymentType", DbType.Int32, objPurchaseTDSPayment.PaymentType);
                db.AddInParameter(cmd, "@BillType", DbType.String, objPurchaseTDSPayment.BillType);
                db.AddInParameter(cmd, "@Narration", DbType.String, objPurchaseTDSPayment.Narration);
                db.AddInParameter(cmd, "@DocumentPath", DbType.String, objPurchaseTDSPayment.DocumentPath);
                db.AddInParameter(cmd, "@RoundOff", DbType.Decimal, objPurchaseTDSPayment.Roundoff);
                db.AddInParameter(cmd, "@SlipNo", DbType.String, objPurchaseTDSPayment.SlipNo);
                db.AddInParameter(cmd, "@SlipDate", DbType.String, objPurchaseTDSPayment.sSlipDate);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objPurchaseTDSPayment.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objPurchaseTDSPayment.FinancialYear.FinancialYearID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                foreach (Entity.Billing.PurchaseTDSPaymentTrans ObjPurchaseTDSPaymentTrans in objPurchaseTDSPayment.PurchaseTDSPaymentTrans)
                    ObjPurchaseTDSPaymentTrans.TDSPaymentID = objPurchaseTDSPayment.TDSPaymentID;

                PurchaseTDSPaymentTrans.SavePurchaseTDSPaymentTransaction(objPurchaseTDSPayment.PurchaseTDSPaymentTrans);
            }
            catch (Exception ex)
            {
                sException = "PurchaseTDSPayment UpdatePurchaseTDSPayment| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeletePurchaseTDSPayment(int iTDSPaymentID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_PURCHASETDSPAYMENT);
                db.AddInParameter(cmd, "@PK_TDSPaymentID", DbType.Int32, iTDSPaymentID);
                // db.AddInParameter(cmd, "@Reason", DbType.String, Reason);
                //  db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, iUserId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "PurchaseTDSPayment DeletePurchaseTDSPayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static DataSet GetPurchaseTDSPaymentSummary()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;

            try
            {
                //db = Entity.DBConnection.dbCon;
                //DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PurchaseRETURNSUMMARY);
                //dsList = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "PurchaseTDSPayment GetPurchaseTDSPaymentSummary | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsList;
        }
    }

    public class PurchaseTDSPaymentTrans
    {
        public static Collection<Entity.Billing.PurchaseTDSPaymentTrans> GetPurchaseTDSPaymentTransByTDSPaymentID(int iTDSPaymentID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.PurchaseTDSPaymentTrans> objList = new Collection<Entity.Billing.PurchaseTDSPaymentTrans>();
            Entity.Billing.PurchaseTDSPaymentTrans objPurchaseTDSPaymentTrans = new Entity.Billing.PurchaseTDSPaymentTrans();
            Entity.Billing.Purchase ObjProduct; Entity.Billing.Tax objTax; Entity.Billing.Purchase ObjoldPurchase;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASETDSPAYMENTTRANS);
                db.AddInParameter(cmd, "@FK_TDSPaymentID", DbType.Int32, iTDSPaymentID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchaseTDSPaymentTrans = new Entity.Billing.PurchaseTDSPaymentTrans();
                        ObjProduct = new Entity.Billing.Purchase();
                        objTax = new Entity.Billing.Tax();
                        ObjoldPurchase = new Entity.Billing.Purchase();


                        objPurchaseTDSPaymentTrans.TDSPaymentTransID = Convert.ToInt32(drData["PK_TDSPaymentTransID"]);
                        objPurchaseTDSPaymentTrans.TDSPaymentID = Convert.ToInt32(drData["FK_TDSPaymentID"]);

                        ObjProduct.PurchaseID = Convert.ToInt32(drData["FK_PurchaseID"]);
                        ObjProduct.BillNo = Convert.ToString(drData["BillNo"]);
                        ObjProduct.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        ObjProduct.sBillDate = ObjProduct.BillDate.ToString("dd/MM/yyyy");
                        ObjProduct.NetAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchaseTDSPaymentTrans.PurchaseEntry = ObjProduct;

                        ObjoldPurchase.PurchaseID = Convert.ToInt32(drData["OldFK_PurchaseID"]);
                        if (drData["oldBillNo"] != DBNull.Value)
                            ObjoldPurchase.BillNo = Convert.ToString(drData["oldBillNo"]);
                        else
                            ObjoldPurchase.BillNo = "-";
                        if (drData["oldBillDate"] != DBNull.Value)
                        {
                            ObjoldPurchase.BillDate = Convert.ToDateTime(drData["oldBillDate"]);
                            ObjoldPurchase.sBillDate = ObjProduct.BillDate.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            ObjoldPurchase.BillDate = DateTime.Now;
                            ObjoldPurchase.sBillDate = "";
                        }
                        if (drData["oldTotalAmount"] != DBNull.Value)
                            ObjoldPurchase.NetAmount = Convert.ToDecimal(drData["oldTotalAmount"]);
                        else
                            ObjoldPurchase.NetAmount = 0;
                        objPurchaseTDSPaymentTrans.oldPurchaseEntry = ObjoldPurchase;

                        objPurchaseTDSPaymentTrans.TDSAmount = Convert.ToDecimal(drData["TDSAmount"]);
                        objPurchaseTDSPaymentTrans.TDSPercent = Convert.ToDecimal(drData["TDSPercent"]);
                        objPurchaseTDSPaymentTrans.Roundoff = Convert.ToDecimal(drData["Roundoff"]);

                        objList.Add(objPurchaseTDSPaymentTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "PurchaseTDSPaymentTrans GetPurchaseTDSPaymentTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SavePurchaseTDSPaymentTransaction(Collection<Entity.Billing.PurchaseTDSPaymentTrans> ObjPurchaseTDSPaymentTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.PurchaseTDSPaymentTrans ObjPurchaseTDSPaymentTransaction in ObjPurchaseTDSPaymentTransList)
            {
                if (ObjPurchaseTDSPaymentTransaction.StatusFlag == "I")
                    iID = AddPurchaseTDSPaymentTrans(ObjPurchaseTDSPaymentTransaction);
                else if (ObjPurchaseTDSPaymentTransaction.StatusFlag == "U")
                    bResult = UpdatePurchaseTDSPaymentTrans(ObjPurchaseTDSPaymentTransaction);
                else if (ObjPurchaseTDSPaymentTransaction.StatusFlag == "D")
                    bResult = DeletePurchaseTDSPaymentTrans(ObjPurchaseTDSPaymentTransaction.TDSPaymentTransID);
            }
        }
        public static int AddPurchaseTDSPaymentTrans(Entity.Billing.PurchaseTDSPaymentTrans objPurchaseTDSPaymentTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_PURCHASETDSPAYMENTTRANS);
                db.AddOutParameter(cmd, "@PK_TDSPaymentTransID", DbType.Int32, objPurchaseTDSPaymentTrans.TDSPaymentTransID);
                db.AddInParameter(cmd, "@FK_TDSPaymentID", DbType.Int32, objPurchaseTDSPaymentTrans.TDSPaymentID);
                db.AddInParameter(cmd, "@FK_PurchaseID", DbType.Int32, objPurchaseTDSPaymentTrans.PurchaseEntry.PurchaseID);
                db.AddInParameter(cmd, "@OldFK_PurchaseID", DbType.Int32, objPurchaseTDSPaymentTrans.oldPurchaseEntry.PurchaseID);
                db.AddInParameter(cmd, "@TDSPercent", DbType.Decimal, objPurchaseTDSPaymentTrans.TDSPercent);
                db.AddInParameter(cmd, "@TDSAmount", DbType.Decimal, objPurchaseTDSPaymentTrans.TDSAmount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objPurchaseTDSPaymentTrans.Roundoff);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_TDSPaymentTransID"));
            }
            catch (Exception ex)
            {
                sException = "PurchaseTDSPaymentTrans AddPurchaseTDSPaymentTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdatePurchaseTDSPaymentTrans(Entity.Billing.PurchaseTDSPaymentTrans objPurchaseTDSPaymentTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_PURCHASETDSPAYMENTTRANS);
                db.AddInParameter(cmd, "@PK_TDSPaymentTransID", DbType.Int32, objPurchaseTDSPaymentTrans.TDSPaymentTransID);
                db.AddInParameter(cmd, "@FK_TDSPaymentID", DbType.Int32, objPurchaseTDSPaymentTrans.TDSPaymentID);
                db.AddInParameter(cmd, "@FK_PurchaseID", DbType.Int32, objPurchaseTDSPaymentTrans.PurchaseEntry.PurchaseID);
                db.AddInParameter(cmd, "@OldFK_PurchaseID", DbType.Int32, objPurchaseTDSPaymentTrans.oldPurchaseEntry.PurchaseID);
                db.AddInParameter(cmd, "@TDSPercent", DbType.Decimal, objPurchaseTDSPaymentTrans.TDSPercent);
                db.AddInParameter(cmd, "@TDSAmount", DbType.Decimal, objPurchaseTDSPaymentTrans.TDSAmount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objPurchaseTDSPaymentTrans.Roundoff);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "PurchaseTDSPaymentTrans UpdatePurchaseTDSPaymentTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeletePurchaseTDSPaymentTrans(int iTDSPaymentTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_PURCHASETDSPAYMENTTRANS);
                db.AddInParameter(cmd, "@PK_TDSPaymentTransID", DbType.Int32, iTDSPaymentTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "PurchaseTDSPaymentTrans DeletePurchaseTDSPaymentTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

    }
}
