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
    public class Settings
    {
        public static Entity.Settings GetSettings()
        {
            string sException = string.Empty;
            Database db;
            Entity.Settings objSettings = new Entity.Settings();
            Entity.Ledger objPaymentBanks;
            Entity.Ledger objReceiptBank;
            Entity.Ledger objRetailReceiptBank;
            Entity.Ledger objExpenseBank;
            Entity.Ledger objOtherExpenseBank;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SETTING_NOTIFICATION);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSettings = new Entity.Settings();
                        objPaymentBanks = new Entity.Ledger();
                        objReceiptBank = new Entity.Ledger();
                        objRetailReceiptBank = new Entity.Ledger();
                        objExpenseBank = new Entity.Ledger();
                        objOtherExpenseBank = new Entity.Ledger();

                        objSettings.MaxDiscountPercent = Convert.ToDecimal(drData["MaxDiscountPercent"]);
                        objSettings.SendSMS = Convert.ToBoolean(drData["SendSMS"]);
                        objSettings.SenderName = Convert.ToString(drData["SenderName"]);
                        objSettings.APILink = Convert.ToString(drData["APILink"]);
                        objSettings.SMSUsername = Convert.ToString(drData["SMSUsername"]);
                        objSettings.SMSPassword = Convert.ToString(drData["SMSPassword"]);
                        objSettings.MaxSalesDiscount = Convert.ToDecimal(drData["MaxSalesDiscount"]);
                        objSettings.SalesTaxAmount = Convert.ToDecimal(drData["SalesTaxAmount"]);
                        objSettings.WholeSaleMinMargin = Convert.ToDecimal(drData["WholeSale_MinMargin"]);
                        objSettings.RetailMinMargin = Convert.ToDecimal(drData["Retail_MinMargin"]);
                        objSettings.EnableSSL = Convert.ToBoolean(drData["EnableSSL"]);
                        objSettings.DefaultCrendentials = Convert.ToBoolean(drData["DefaultCrendentials"]);
                        objSettings.HostName = Convert.ToString(drData["HostName"]);
                        objSettings.UserMailPassword = Convert.ToString(drData["UserMailPassword"]);
                        objSettings.UserMailID = Convert.ToString(drData["UserMailID"]);
                        objSettings.Port = Convert.ToString(drData["Port"]);
                        objSettings.CompanyName = Convert.ToString(drData["CompanyName"]);
                        objSettings.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objSettings.Address = Convert.ToString(drData["Address"]);
                        objSettings.ContactEmail = Convert.ToString(drData["Email"]);

                        objPaymentBanks.LedgerID = Convert.ToInt32(drData["FK_PaymentBankID"]);
                        objSettings.PaymentBank = objPaymentBanks;

                        objReceiptBank.LedgerID = Convert.ToInt32(drData["FK_ReceiptBankID"]);
                        objSettings.ReceiptBank = objReceiptBank;

                        objExpenseBank.LedgerID = Convert.ToInt32(drData["FK_ExpenseBankID"]);
                        objSettings.ExpenseBank = objExpenseBank;

                        objOtherExpenseBank.LedgerID = Convert.ToInt32(drData["FK_OtherBankID"]);
                        objSettings.OtherExpenseBank = objOtherExpenseBank;

                        objRetailReceiptBank.LedgerID = Convert.ToInt32(drData["FK_RetailReceiptBankID"]);
                        objSettings.RetailReceiptBank = objRetailReceiptBank;

                        objSettings.PaymentModeID = Convert.ToInt32(drData["FK_PaymentModeID"]);
                        objSettings.ReceiptModeID = Convert.ToInt32(drData["FK_ReceiptModeID"]);
                        objSettings.ExpensePaymentModeID = Convert.ToInt32(drData["FK_ExpensePaymentModeID"]);
                        objSettings.OtherReceiptModeID = Convert.ToInt32(drData["FK_OtherReceiptModeID"]);
                        objSettings.RetailReceiptModeID = Convert.ToInt32(drData["FK_RetailReceiptModeID"]);

                        objSettings.Retails_Sales_MobileNo = Convert.ToString(drData["Retails_Sales_MobileNo"]);
                        objSettings.Retails_Sales_Email = Convert.ToString(drData["Retails_Sales_Email"]);
                        objSettings.Retails_Sales_GSTNO = Convert.ToString(drData["Retails_Sales_GSTNO"]);
                        objSettings.Retails_Sales_Address = Convert.ToString(drData["Retails_Sales_Address"]);

                        //objSettings.ContactNo = Convert.ToString(drData["ContactNo"]);
                        //objSettings.Email = Convert.ToString(drData["Email"]);
                        //objSettings.CSTNo = Convert.ToString(drData["CSTNo"]);
                        //objSettings.BankName = Convert.ToString(drData["BankName"]);
                        //objSettings.BranchName = Convert.ToString(drData["BranchName"]);
                        //objSettings.AccountNo = Convert.ToString(drData["AccountNo"]);
                        //objSettings.IFSCCode = Convert.ToString(drData["IFSCCode"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Settings GetSettingsByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSettings;
        }
        public static bool UpdateSettings(Entity.Settings objSettings)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateSettings(oDb, objSettings, oTrans);
                    oTrans.Commit();
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
        private static bool UpdateSettings(Database oDb, Entity.Settings objSettings, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_SETTINGS_NOTIFICATION);
                db.AddInParameter(cmd, "@MaxDiscountPercent", DbType.Decimal, objSettings.MaxDiscountPercent);
                db.AddInParameter(cmd, "@MaxSalesDiscount", DbType.Decimal, objSettings.MaxSalesDiscountPercent);
                db.AddInParameter(cmd, "@SendSMS", DbType.Boolean, objSettings.SendSMS);
                db.AddInParameter(cmd, "@SMSPassword", DbType.String, objSettings.SMSPassword);
                db.AddInParameter(cmd, "@SMSUsername", DbType.String, objSettings.SMSUsername);
                db.AddInParameter(cmd, "@SenderName", DbType.String, objSettings.SenderName);
                db.AddInParameter(cmd, "@APILink", DbType.String, objSettings.APILink);
                db.AddInParameter(cmd, "@SalesTaxAmount", DbType.Decimal, objSettings.SalesTaxAmount);
                db.AddInParameter(cmd, "@WholeSale_MinMargin", DbType.Decimal, objSettings.WholeSaleMinMargin);
                db.AddInParameter(cmd, "@Retail_MinMargin", DbType.Decimal, objSettings.RetailMinMargin);
                db.AddInParameter(cmd, "@EnableSSL", DbType.Boolean, objSettings.EnableSSL);
                db.AddInParameter(cmd, "@DefaultCrendentials", DbType.Boolean, objSettings.DefaultCrendentials);
                db.AddInParameter(cmd, "@HostName", DbType.String, objSettings.HostName);
                db.AddInParameter(cmd, "@Port", DbType.String, objSettings.Port);
                db.AddInParameter(cmd, "@UserMailID", DbType.String, objSettings.UserMailID);
                db.AddInParameter(cmd, "@UserMailPassword", DbType.String, objSettings.UserMailPassword);
                db.AddInParameter(cmd, "@MobileNo", DbType.String, objSettings.MobileNo);
                db.AddInParameter(cmd, "@Address", DbType.String, objSettings.Address);
                db.AddInParameter(cmd, "@Email", DbType.String, objSettings.ContactEmail);
                db.AddInParameter(cmd, "@FK_PaymentModeID", DbType.Int32, objSettings.PaymentModeID);
                db.AddInParameter(cmd, "@FK_PaymentBankID", DbType.Int32, objSettings.PaymentBank.LedgerID);
                db.AddInParameter(cmd, "@FK_ReceiptModeID", DbType.Int32, objSettings.ReceiptModeID);
                db.AddInParameter(cmd, "@FK_ReceiptBankID", DbType.Int32, objSettings.ReceiptBank.LedgerID);
                db.AddInParameter(cmd, "@FK_ExpensePaymentModeID", DbType.Int32, objSettings.ExpensePaymentModeID);
                db.AddInParameter(cmd, "@FK_ExpenseBankID", DbType.Int32, objSettings.ExpenseBank.LedgerID);
                db.AddInParameter(cmd, "@FK_OtherReceiptModeID", DbType.Int32, objSettings.OtherReceiptModeID);
                db.AddInParameter(cmd, "@FK_OtherBankID", DbType.Int32, objSettings.OtherExpenseBank.LedgerID);
                db.AddInParameter(cmd, "@FK_RetailReceiptModeID", DbType.Int32, objSettings.RetailReceiptModeID);
                db.AddInParameter(cmd, "@FK_RetailReceiptBankID", DbType.Int32, objSettings.RetailReceiptBank.LedgerID);

                db.AddInParameter(cmd, "@Retails_Sales_MobileNo", DbType.String, objSettings.Retails_Sales_MobileNo);
                db.AddInParameter(cmd, "@Retails_Sales_Email", DbType.String, objSettings.Retails_Sales_Email);
                db.AddInParameter(cmd, "@Retails_Sales_GSTNO", DbType.String, objSettings.Retails_Sales_GSTNO);
                db.AddInParameter(cmd, "@Retails_Sales_Address", DbType.String, objSettings.Retails_Sales_Address);

                //db.AddInParameter(cmd, "@ContactNo", DbType.String, objSettings.ContactNo);
                //db.AddInParameter(cmd, "@Email", DbType.String, objSettings.Email);
                //db.AddInParameter(cmd, "@CSTNo", DbType.String, objSettings.CSTNo);
                //db.AddInParameter(cmd, "@BankName", DbType.String, objSettings.BankName);
                //db.AddInParameter(cmd, "@BranchName", DbType.String, objSettings.BranchName);
                //db.AddInParameter(cmd, "@AccountNo", DbType.String, objSettings.AccountNo);
                //db.AddInParameter(cmd, "@IFSCCode", DbType.String, objSettings.IFSCCode);




                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Company UpdateCompany| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

    }
}
