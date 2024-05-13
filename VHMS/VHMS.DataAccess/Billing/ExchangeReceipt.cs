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
    public class ExchangeReceipt
    {
        public static Collection<Entity.ExchangeReceipt> GetExchangeReceipt(int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.ExchangeReceipt> objList = new Collection<Entity.ExchangeReceipt>();
            Entity.ExchangeReceipt objExchangeReceipt = new Entity.ExchangeReceipt();
            Entity.Customer objCustomer;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_EXCHANGERECEIPT);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objExchangeReceipt = new Entity.ExchangeReceipt();
                        objCustomer = new Entity.Customer();

                        objExchangeReceipt.ExchangeReceiptID = Convert.ToInt32(drData["PK_ExchangeReceiptID"]);
                        objExchangeReceipt.VoucherNo = Convert.ToString(drData["VoucherNo"]);
                        objExchangeReceipt.VoucherDate = Convert.ToDateTime(drData["VoucherDate"]);
                        objExchangeReceipt.sVoucherDate = objExchangeReceipt.VoucherDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objExchangeReceipt.Customer = objCustomer;
                                               
                        objExchangeReceipt.Narration = Convert.ToString(drData["Narration"]);
                        objExchangeReceipt.Rate = Convert.ToDecimal(drData["Rate"]);
                        objExchangeReceipt.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objExchangeReceipt.Amount = Convert.ToDecimal(drData["Amount"]);
                        objExchangeReceipt.ProductName = Convert.ToString(drData["ProductName"]);

                        objList.Add(objExchangeReceipt);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "ExchangeReceipt GetExchangeReceipt | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.ExchangeReceipt> GetExchangeReceiptByStatus(string iStatus, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.ExchangeReceipt> objList = new Collection<Entity.ExchangeReceipt>();
            Entity.ExchangeReceipt objExchangeReceipt = new Entity.ExchangeReceipt();
            Entity.Customer objCustomer;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_EXCHANGERECEIPTBYSTATUS);
                db.AddInParameter(cmd, "@Status", DbType.String, iStatus);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objExchangeReceipt = new Entity.ExchangeReceipt();
                        objCustomer = new Entity.Customer();

                        objExchangeReceipt.ExchangeReceiptID = Convert.ToInt32(drData["PK_ExchangeReceiptID"]);
                        objExchangeReceipt.VoucherNo = Convert.ToString(drData["VoucherNo"]);
                        objExchangeReceipt.VoucherDate = Convert.ToDateTime(drData["VoucherDate"]);
                        objExchangeReceipt.sVoucherDate = objExchangeReceipt.VoucherDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objExchangeReceipt.Customer = objCustomer;

                        objExchangeReceipt.Narration = Convert.ToString(drData["Narration"]);
                        objExchangeReceipt.Rate = Convert.ToDecimal(drData["Rate"]);
                        objExchangeReceipt.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objExchangeReceipt.Amount = Convert.ToDecimal(drData["Amount"]);
                        objExchangeReceipt.ProductName = Convert.ToString(drData["ProductName"]);

                        objList.Add(objExchangeReceipt);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "ExchangeReceipt GetExchangeReceipt | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.ExchangeReceipt GetExchangeReceiptByID(int iExchangeReceiptID, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.ExchangeReceipt objExchangeReceipt = new Entity.ExchangeReceipt();
            Entity.Customer objCustomer;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_EXCHANGERECEIPT);
                db.AddInParameter(cmd, "@PK_ExchangeReceiptID", DbType.Int32, iExchangeReceiptID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objExchangeReceipt = new Entity.ExchangeReceipt();
                        objCustomer = new Entity.Customer();

                        objExchangeReceipt.ExchangeReceiptID = Convert.ToInt32(drData["PK_ExchangeReceiptID"]);
                        objExchangeReceipt.VoucherNo = Convert.ToString(drData["VoucherNo"]);
                        objExchangeReceipt.VoucherDate = Convert.ToDateTime(drData["VoucherDate"]);
                        objExchangeReceipt.sVoucherDate = objExchangeReceipt.VoucherDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objExchangeReceipt.Customer = objCustomer;

                        objExchangeReceipt.Narration = Convert.ToString(drData["Narration"]);
                        objExchangeReceipt.Rate = Convert.ToDecimal(drData["Rate"]);
                        objExchangeReceipt.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objExchangeReceipt.Amount = Convert.ToDecimal(drData["Amount"]);
                        objExchangeReceipt.ProductName = Convert.ToString(drData["ProductName"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "ExchangeReceipt GetExchangeReceiptByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objExchangeReceipt;
        }
        public static int AddExchangeReceipt(Entity.ExchangeReceipt objExchangeReceipt)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_EXCHANGERECEIPT);
                db.AddOutParameter(cmd, "@PK_ExchangeReceiptID", DbType.Int32, objExchangeReceipt.ExchangeReceiptID);
                db.AddInParameter(cmd, "@VoucherNo", DbType.String, objExchangeReceipt.VoucherNo);
                db.AddInParameter(cmd, "@VoucherDate", DbType.String, objExchangeReceipt.sVoucherDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objExchangeReceipt.Customer.CustomerID);
                db.AddInParameter(cmd, "@ProductName", DbType.String, objExchangeReceipt.ProductName);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objExchangeReceipt.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objExchangeReceipt.Rate);
                db.AddInParameter(cmd, "@Amount", DbType.Decimal, objExchangeReceipt.Amount);
                db.AddInParameter(cmd, "@Narration", DbType.String, objExchangeReceipt.Narration);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objExchangeReceipt.CreatedBy.UserID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objExchangeReceipt.FinancialYear.FinancialYearID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_ExchangeReceiptID"));
            }
            catch (Exception ex)
            {
                sException = "ExchangeReceipt AddExchangeReceipt | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateExchangeReceipt(Entity.ExchangeReceipt objExchangeReceipt)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_EXCHANGERECEIPT);
                db.AddInParameter(cmd, "@PK_ExchangeReceiptID", DbType.Int32, objExchangeReceipt.ExchangeReceiptID);
                db.AddInParameter(cmd, "@VoucherNo", DbType.String, objExchangeReceipt.VoucherNo);
                db.AddInParameter(cmd, "@VoucherDate", DbType.String, objExchangeReceipt.sVoucherDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objExchangeReceipt.Customer.CustomerID);
                db.AddInParameter(cmd, "@ProductName", DbType.String, objExchangeReceipt.ProductName);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objExchangeReceipt.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objExchangeReceipt.Rate);
                db.AddInParameter(cmd, "@Amount", DbType.Decimal, objExchangeReceipt.Amount);
                db.AddInParameter(cmd, "@Narration", DbType.String, objExchangeReceipt.Narration);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objExchangeReceipt.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objExchangeReceipt.FinancialYear.FinancialYearID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "ExchangeReceipt UpdateExchangeReceipt| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteExchangeReceipt(int iExchangeReceiptId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_EXCHANGERECEIPT);
                db.AddInParameter(cmd, "@PK_ExchangeReceiptID", DbType.Int32, iExchangeReceiptId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "ExchangeReceipt DeleteExchangeReceipt | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
