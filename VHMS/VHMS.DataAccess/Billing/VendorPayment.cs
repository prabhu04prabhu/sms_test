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
    public class VendorPayment
    {
        public static Collection<Entity.Billing.VendorPayment> GetVendorPayment()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.VendorPayment> objList = new Collection<Entity.Billing.VendorPayment>();
            Entity.Billing.VendorPayment objVendorPayment = new Entity.Billing.VendorPayment();
            Entity.Billing.Vendor objVendor;
            Entity.Ledger objBank;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_VENDORPAYMENT);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objVendorPayment = new Entity.Billing.VendorPayment();
                        objVendor = new Entity.Billing.Vendor();
                        objBank = new Entity.Ledger();

                        objVendorPayment.VendorPaymentID = Convert.ToInt32(drData["PK_VendorPaymentID"]);
                        objVendorPayment.VoucherNo = Convert.ToString(drData["VoucherNo"]);
                        objVendorPayment.VoucherDate = Convert.ToDateTime(drData["VoucherDate"]);
                        objVendorPayment.sVoucherDate = objVendorPayment.VoucherDate.ToString("dd/MM/yyyy");

                        objVendor.VendorID = Convert.ToInt32(drData["FK_VendorID"]);
                        objVendor.VendorName = Convert.ToString(drData["VendorName"]);
                        objVendorPayment.Vendor = objVendor;

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objVendorPayment.Bank = objBank;

                        objVendorPayment.PaymentModeID = Convert.ToInt16(drData["FK_PaymentModeID"]);
                        objVendorPayment.Amount = Convert.ToDecimal(drData["Amount"]);
                        objVendorPayment.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objVendorPayment.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                        objVendorPayment.sIssueDate = objVendorPayment.IssueDate.ToString("dd/MM/yyyy");
                        objVendorPayment.CollectionDate = Convert.ToDateTime(drData["CollectionDate"]);
                        objVendorPayment.sCollectionDate = objVendorPayment.CollectionDate.ToString("dd/MM/yyyy");
                        objVendorPayment.IssuedBy = Convert.ToString(drData["IssuedBy"]);
                        objVendorPayment.Description = Convert.ToString(drData["Description"]);

                        objList.Add(objVendorPayment);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VendorPayment GetVendorPayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.VendorPayment GetVendorPaymentByID(int iVendorPaymentID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.VendorPayment objVendorPayment = new Entity.Billing.VendorPayment();
            Entity.Billing.Vendor objVendor;
            Entity.Ledger objBank;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_VENDORPAYMENT);
                db.AddInParameter(cmd, "@PK_VendorPaymentID", DbType.Int32, iVendorPaymentID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objVendorPayment = new Entity.Billing.VendorPayment();
                        objVendor = new Entity.Billing.Vendor();
                        objBank = new Entity.Ledger();

                        objVendorPayment.VendorPaymentID = Convert.ToInt32(drData["PK_VendorPaymentID"]);
                        objVendorPayment.VoucherNo = Convert.ToString(drData["VoucherNo"]);
                        objVendorPayment.VoucherDate = Convert.ToDateTime(drData["VoucherDate"]);
                        objVendorPayment.sVoucherDate = objVendorPayment.VoucherDate.ToString("dd/MM/yyyy");

                        objVendor.VendorID = Convert.ToInt32(drData["FK_VendorID"]);
                        objVendor.VendorName = Convert.ToString(drData["VendorName"]);
                        objVendorPayment.Vendor = objVendor;

                        objBank.LedgerID = Convert.ToInt32(drData["FK_BankID"]);
                        objBank.LedgerName = Convert.ToString(drData["BankName"]);
                        objVendorPayment.Bank = objBank;

                        objVendorPayment.PaymentModeID = Convert.ToInt16(drData["FK_PaymentModeID"]);
                        objVendorPayment.Amount = Convert.ToDecimal(drData["Amount"]);
                        objVendorPayment.ChequeNo = Convert.ToString(drData["ChequeNo"]);
                        objVendorPayment.IssueDate = Convert.ToDateTime(drData["IssueDate"]);
                        objVendorPayment.sIssueDate = objVendorPayment.IssueDate.ToString("dd/MM/yyyy");
                        objVendorPayment.CollectionDate = Convert.ToDateTime(drData["CollectionDate"]);
                        objVendorPayment.sCollectionDate = objVendorPayment.CollectionDate.ToString("dd/MM/yyyy");
                        objVendorPayment.IssuedBy = Convert.ToString(drData["IssuedBy"]);
                        objVendorPayment.Description = Convert.ToString(drData["Description"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VendorPayment GetVendorPaymentByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objVendorPayment;
        }
        public static int AddVendorPayment(Entity.Billing.VendorPayment objVendorPayment)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_VENDORPAYMENT);
                db.AddOutParameter(cmd, "@PK_VendorPaymentID", DbType.Int32, objVendorPayment.VendorPaymentID);
                db.AddInParameter(cmd, "@VoucherNo", DbType.String, objVendorPayment.VoucherNo);
                db.AddInParameter(cmd, "@VoucherDate", DbType.String, objVendorPayment.sVoucherDate);
                db.AddInParameter(cmd, "@FK_VendorID", DbType.Int32, objVendorPayment.Vendor.VendorID);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objVendorPayment.Bank.LedgerID);
                db.AddInParameter(cmd, "@FK_PaymentModeID", DbType.Int16, objVendorPayment.PaymentModeID);
                db.AddInParameter(cmd, "@Amount", DbType.Decimal, objVendorPayment.Amount);
                db.AddInParameter(cmd, "@ChequeNo", DbType.String, objVendorPayment.ChequeNo);
                db.AddInParameter(cmd, "@IssueDate", DbType.String, objVendorPayment.sIssueDate);
                db.AddInParameter(cmd, "@CollectionDate", DbType.String, objVendorPayment.sCollectionDate);
                db.AddInParameter(cmd, "@IssuedBy", DbType.String, objVendorPayment.IssuedBy);
                db.AddInParameter(cmd, "@Description", DbType.String, objVendorPayment.Description);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objVendorPayment.CreatedBy.UserID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_VendorPaymentID"));
            }
            catch (Exception ex)
            {
                sException = "VendorPayment AddVendorPayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateVendorPayment(Entity.Billing.VendorPayment objVendorPayment)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_VENDORPAYMENT);
                db.AddInParameter(cmd, "@PK_VendorPaymentID", DbType.Int32, objVendorPayment.VendorPaymentID);
                db.AddInParameter(cmd, "@VoucherNo", DbType.String, objVendorPayment.VoucherNo);
                db.AddInParameter(cmd, "@VoucherDate", DbType.String, objVendorPayment.sVoucherDate);
                db.AddInParameter(cmd, "@FK_VendorID", DbType.Int32, objVendorPayment.Vendor.VendorID);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objVendorPayment.Bank.LedgerID);
                db.AddInParameter(cmd, "@FK_PaymentModeID", DbType.Int16, objVendorPayment.PaymentModeID);
                db.AddInParameter(cmd, "@Amount", DbType.Decimal, objVendorPayment.Amount);
                db.AddInParameter(cmd, "@ChequeNo", DbType.String, objVendorPayment.ChequeNo);
                db.AddInParameter(cmd, "@IssueDate", DbType.String, objVendorPayment.sIssueDate);
                db.AddInParameter(cmd, "@CollectionDate", DbType.String, objVendorPayment.sCollectionDate);
                db.AddInParameter(cmd, "@IssuedBy", DbType.String, objVendorPayment.IssuedBy);
                db.AddInParameter(cmd, "@Description", DbType.String, objVendorPayment.Description);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objVendorPayment.ModifiedBy.UserID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VendorPayment UpdateVendorPayment| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteVendorPayment(int iVendorPaymentId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_VENDORPAYMENT);
                db.AddInParameter(cmd, "@PK_VendorPaymentID", DbType.Int32, iVendorPaymentId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VendorPayment DeleteVendorPayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
