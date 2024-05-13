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
    public class Exchange
    {
        public static Collection<Entity.Billing.Exchange> GetExchange(int ipatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Exchange> objList = new Collection<Entity.Billing.Exchange>();
            Entity.Billing.Exchange objExchange;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;
            Entity.User objEmployee;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_EXCHANGE);
                db.AddInParameter(cmd, "@PK_ExchangeID", DbType.Int32, ipatientID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objExchange = new Entity.Billing.Exchange();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();
                        objEmployee = new Entity.User();
                        objExchange.ExchangeID = Convert.ToInt32(drData["PK_ExchangeID"]);
                        objExchange.ExchangeNo = Convert.ToString(drData["ExchangeNo"]);
                        objExchange.ExchangeDate = Convert.ToDateTime(drData["ExchangeDate"]);
                        objExchange.sExchangeDate = objExchange.ExchangeDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objExchange.Customer = objCustomer;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objExchange.Branch = objBranch;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objExchange.Tax = objTax;
                        objExchange.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objExchange.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objExchange.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objExchange.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objExchange.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objExchange.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objExchange.SalesID = Convert.ToInt32(drData["FK_SalesID"]);
                        objList.Add(objExchange);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Exchange GetExchange | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Exchange> GetTopExchange(int ipatientID = 0, int iBranchID=0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Exchange> objList = new Collection<Entity.Billing.Exchange>();
            Entity.Billing.Exchange objExchange;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;
            Entity.User objEmployee;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPEXCHANGE);
                db.AddInParameter(cmd, "@PK_ExchangeID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, iBranchID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objExchange = new Entity.Billing.Exchange();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();
                        objEmployee = new Entity.User();
                        objExchange.ExchangeID = Convert.ToInt32(drData["PK_ExchangeID"]);
                        objExchange.ExchangeNo = Convert.ToString(drData["ExchangeNo"]);
                        objExchange.ExchangeDate = Convert.ToDateTime(drData["ExchangeDate"]);
                        objExchange.sExchangeDate = objExchange.ExchangeDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objExchange.Customer = objCustomer;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objExchange.Branch = objBranch;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objExchange.Tax = objTax;
                        objExchange.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objExchange.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objExchange.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objExchange.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objExchange.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objExchange.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objExchange.SalesID = Convert.ToInt32(drData["FK_SalesID"]);
                        objList.Add(objExchange);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Exchange GetExchange | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Exchange> GetExchangeID(int ipatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Exchange> objList = new Collection<Entity.Billing.Exchange>();
            Entity.Billing.Exchange objExchange;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;
            Entity.User objEmployee; Entity.Chit objChit;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_EXCHANGE);
                db.AddInParameter(cmd, "@PK_ExchangeID", DbType.Int32, ipatientID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objExchange = new Entity.Billing.Exchange();

                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();
                        objEmployee = new Entity.User();
                        objChit = new Entity.Chit();

                        objExchange.ExchangeID = Convert.ToInt32(drData["PK_ExchangeID"]);
                        objExchange.ExchangeNo = Convert.ToString(drData["ExchangeNo"]);
                        objExchange.ExchangeDate = Convert.ToDateTime(drData["ExchangeDate"]);
                        objExchange.sExchangeDate = objExchange.ExchangeDate.ToString("dd/MM/yyyy");
                        objExchange.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objExchange.Customer = objCustomer;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objExchange.Branch = objBranch;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objExchange.Tax = objTax;
                        objExchange.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objExchange.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objExchange.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objExchange.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objExchange.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objExchange.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objExchange.SalesID = Convert.ToInt32(drData["FK_SalesID"]);

                        objList.Add(objExchange);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Exchange GetExchange | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Exchange> SearchExchange(string ID, int iBranchID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Exchange> objList = new Collection<Entity.Billing.Exchange>();
            Entity.Billing.Exchange objExchange;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;
            Entity.User objEmployee;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_EXCHANGE);
                db.AddInParameter(cmd, "@key", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, iBranchID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objExchange = new Entity.Billing.Exchange();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();
                        objEmployee = new Entity.User();
                        objExchange.ExchangeID = Convert.ToInt32(drData["PK_ExchangeID"]);
                        objExchange.ExchangeNo = Convert.ToString(drData["ExchangeNo"]);
                        objExchange.ExchangeDate = Convert.ToDateTime(drData["ExchangeDate"]);
                        objExchange.sExchangeDate = objExchange.ExchangeDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objExchange.Customer = objCustomer;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objExchange.Branch = objBranch;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objExchange.Tax = objTax;
                        objExchange.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objExchange.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objExchange.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objExchange.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objExchange.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objExchange.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objExchange.SalesID = Convert.ToInt32(drData["FK_SalesID"]);
                        objList.Add(objExchange);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Exchange GetExchange | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        //public static Collection<Entity.Billing.Exchange> GetExchangeReport(Entity.Billing.ExchangeFilter oJobCardFilter)
        //{
        //    string sException = string.Empty;
        //    Database db;
        //    DataSet dsList = null;
        //    Collection<Entity.Billing.Exchange> objList = new Collection<Entity.Billing.Exchange>();
        //    Entity.Billing.Exchange objExchange;
        //    Entity.patient objpatient; Entity.Discharge.Doctor objdoctor;

        //    try
        //    {
        //        db = VHMS.Entity.DBConnection.dbCon;
        //        DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_EXCHANGEREPORT);
        //        db.AddInParameter(cmd, "@FK_PatientID", DbType.Int32, oJobCardFilter.PatientID);
        //        db.AddInParameter(cmd, "@DateFrom", DbType.String, oJobCardFilter.DateFrom);
        //        db.AddInParameter(cmd, "@DateTo", DbType.String, oJobCardFilter.DateTo);
        //        db.AddInParameter(cmd, "@FK_DoctorID", DbType.Int32, oJobCardFilter.DoctorID);
        //        db.AddInParameter(cmd, "@FK_DescriptionID", DbType.Int32, oJobCardFilter.DescriptionID);
        //        db.AddInParameter(cmd, "@FK_UserID", DbType.Int32, oJobCardFilter.UserID);
        //        dsList = db.ExecuteDataSet(cmd);

        //        if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow drData in dsList.Tables[0].Rows)
        //            {
        //                objExchange = new Entity.Billing.Exchange();
        //                objpatient = new Entity.patient();
        //                objdoctor = new Entity.Discharge.Doctor();

        //                objExchange.ExchangeID = Convert.ToInt32(drData["PK_ExchangeID"]);
        //                objExchange.BillNo = Convert.ToString(drData["BillNo"]);
        //              //  objExchange.BillDate = Convert.ToDateTime(drData["BillDate"]);
        //                objExchange.sBillDate = (Convert.ToDateTime(drData["BillDate"])).ToString("dd/MM/yyyy");

        //               // objpatient.patientID = Convert.ToInt32(drData["FK_patientID"]);
        //              //  objpatient.HName = Convert.ToString(drData["HName"]);
        //                objpatient.WName = Convert.ToString(drData["WName"]);
        //                objpatient.WMobileNo = Convert.ToString(drData["WMobileNo"]);
        //               // objpatient.OPDNo = Convert.ToString(drData["OPDNo"]);
        //                objExchange.Patient = objpatient;
        //               // objdoctor.DoctorID = Convert.ToInt32(drData["FK_DoctorID"]);
        //                objdoctor.DoctorName = Convert.ToString(drData["DoctorName"]);
        //                objExchange.Doctor = objdoctor;
        //               // objExchange.PaymentMode = Convert.ToString(drData["PaymentMode"]);
        //                objExchange.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
        //               // objExchange.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
        //              //  objExchange.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
        //                //objExchange.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
        //                //objExchange.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
        //                objExchange.Subtotal = Convert.ToDecimal(drData["Subtotal"]);
        //                objExchange.DescriptionName = Convert.ToString(drData["DescriptionName"]);
        //                objExchange.CUserName = Convert.ToString(drData["CUserName"]);
        //                objExchange.IsCancelled = Convert.ToBoolean(drData["IsCancelled"]);
        //                objExchange.CancelReason = Convert.ToString(drData["CancelReason"]);
        //                objList.Add(objExchange);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "Exchange GetExchange | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return objList;
        //}
        public static Entity.Billing.Exchange GetExchangeByID(int iExchangeID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Exchange objExchange = new Entity.Billing.Exchange();
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;
            Entity.User objEmployee; Entity.Chit objChit;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_EXCHANGE);
                db.AddInParameter(cmd, "@PK_ExchangeID", DbType.Int32, iExchangeID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objExchange = new Entity.Billing.Exchange();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();
                        objEmployee = new Entity.User();
                        objChit = new Entity.Chit();

                        objExchange.ExchangeID = Convert.ToInt32(drData["PK_ExchangeID"]);
                        objExchange.ExchangeNo = Convert.ToString(drData["ExchangeNo"]);
                        objExchange.ExchangeDate = Convert.ToDateTime(drData["ExchangeDate"]);
                        objExchange.sExchangeDate = objExchange.ExchangeDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objExchange.Customer = objCustomer;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objExchange.Branch = objBranch;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objExchange.Tax = objTax;
                        objExchange.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objExchange.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objExchange.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objExchange.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objExchange.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objExchange.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objExchange.SalesID = Convert.ToInt32(drData["FK_SalesID"]);

                        objExchange.ExchangeTrans = ExchangeTrans.GetExchangeTransByExchangeID(objExchange.ExchangeID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Exchange GetExchangeByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objExchange;
        }
        public static int AddExchange(Entity.Billing.Exchange objExchange)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_EXCHANGE);
                db.AddOutParameter(cmd, "@PK_ExchangeID", DbType.Int32, objExchange.ExchangeID);
                //db.AddInParameter(cmd, "@ExchangeNo", DbType.String, objExchange.ExchangeNo);
                db.AddInParameter(cmd, "@ExchangeDate", DbType.String, objExchange.sExchangeDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objExchange.Customer.CustomerID);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, objExchange.Branch.BranchID);
               
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objExchange.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objExchange.Tax.TaxPercentage);
                db.AddInParameter(cmd, "@FK_SalesID", DbType.Int32, objExchange.SalesID);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objExchange.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objExchange.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objExchange.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objExchange.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objExchange.TotalAmount);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objExchange.NetAmount);               
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objExchange.CreatedBy.UserID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_ExchangeID"));

                foreach (Entity.Billing.ExchangeTrans ObjExchangeTrans in objExchange.ExchangeTrans)
                {
                    ObjExchangeTrans.ExchangeID = iID;
                    ObjExchangeTrans.SalesID = objExchange.SalesID;
                }
                ExchangeTrans.SaveExchangeTransaction(objExchange.ExchangeTrans);
            }
            catch (Exception ex)
            {
                sException = "Exchange AddExchange | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateExchange(Entity.Billing.Exchange objExchange)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_EXCHANGE);
                db.AddInParameter(cmd, "@PK_ExchangeID", DbType.Int32, objExchange.ExchangeID);
                db.AddInParameter(cmd, "@ExchangeDate", DbType.String, objExchange.sExchangeDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objExchange.Customer.CustomerID);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, objExchange.Branch.BranchID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objExchange.Tax.TaxID);
                db.AddInParameter(cmd, "@FK_SalesID", DbType.Int32, objExchange.SalesID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objExchange.Tax.TaxPercentage);               
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objExchange.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objExchange.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objExchange.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objExchange.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objExchange.TotalAmount);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objExchange.NetAmount);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objExchange.ModifiedBy.UserID);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                foreach (Entity.Billing.ExchangeTrans ObjExchangeTrans in objExchange.ExchangeTrans)
                {
                    ObjExchangeTrans.ExchangeID = objExchange.ExchangeID;
                    ObjExchangeTrans.SalesID = objExchange.SalesID;
                }
                ExchangeTrans.SaveExchangeTransaction(objExchange.ExchangeTrans);
            }
            catch (Exception ex)
            {
                sException = "Exchange UpdateExchange| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteExchange(int iExchangeId, string Reason, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_EXCHANGE);
                db.AddInParameter(cmd, "@PK_ExchangeID", DbType.Int32, iExchangeId);
               // db.AddInParameter(cmd, "@Reason", DbType.String, Reason);
              //  db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, iUserId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Exchange DeleteExchange | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }

    public class ExchangeTrans
    {
        public static Collection<Entity.Billing.ExchangeTrans> GetExchangeTransByExchangeID(int iExchangeID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.ExchangeTrans> objList = new Collection<Entity.Billing.ExchangeTrans>();
            Entity.Billing.ExchangeTrans objExchangeTrans = new Entity.Billing.ExchangeTrans();
            Entity.Billing.Category objCategory;
            Entity.Master.Product objProduct;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_EXCHANGETRANS);
                db.AddInParameter(cmd, "@FK_ExchangeID", DbType.Int32, iExchangeID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objExchangeTrans = new Entity.Billing.ExchangeTrans();
                        objProduct = new Entity.Master.Product();
                        objCategory = new Entity.Billing.Category();

                        objExchangeTrans.ExchangeTransID = Convert.ToInt32(drData["PK_ExchangeTransID"]);
                        objExchangeTrans.ExchangeID = Convert.ToInt32(drData["FK_ExchangeID"]);

                        objCategory.CategoryID = Convert.ToInt32(drData["Fk_CategoryID"]);
                        objCategory.CategoryName = Convert.ToString(drData["CategoryName"]);
                        objExchangeTrans.Category = objCategory;

                        objProduct.ProductID = Convert.ToInt32(drData["Fk_ProductID"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objExchangeTrans.Product = objProduct;
                        objExchangeTrans.SalesID = Convert.ToInt32(drData["FK_SalesID"]);
                        objExchangeTrans.NetWeight = Convert.ToDecimal(drData["NetWeight"]);
                        objExchangeTrans.MeltingWeight = Convert.ToDecimal(drData["MeltingWeight"]);
                        objExchangeTrans.StoneWeight = Convert.ToDecimal(drData["StoneWeight"]);
                        objExchangeTrans.GrossWeight = Convert.ToDecimal(drData["GrossWeight"]);
                        objExchangeTrans.CurrentRate = Convert.ToDecimal(drData["CurrentRate"]);
                        objExchangeTrans.Amount = Convert.ToDecimal(drData["Amount"]);
                        objExchangeTrans.Karat = Convert.ToString(drData["Karat"]);

                        objList.Add(objExchangeTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "ExchangeTrans GetExchangeTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveExchangeTransaction(Collection<Entity.Billing.ExchangeTrans> ObjExchangeTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.ExchangeTrans ObjExchangeTransaction in ObjExchangeTransList)
            {
                if (ObjExchangeTransaction.StatusFlag == "I")
                    iID = AddExchangeTrans(ObjExchangeTransaction);
                else if (ObjExchangeTransaction.StatusFlag == "U")
                    bResult = UpdateExchangeTrans(ObjExchangeTransaction);
                else if (ObjExchangeTransaction.StatusFlag == "D")
                    bResult = DeleteExchangeTrans(ObjExchangeTransaction.ExchangeTransID);
            }
        }
        public static int AddExchangeTrans(Entity.Billing.ExchangeTrans objExchangeTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_EXCHANGETRANS);
                db.AddOutParameter(cmd, "@PK_ExchangeTransID", DbType.Int32, objExchangeTrans.ExchangeTransID);
                db.AddInParameter(cmd, "@FK_ExchangeID", DbType.Int32, objExchangeTrans.ExchangeID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, objExchangeTrans.Product.ProductID);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, objExchangeTrans.Category.CategoryID);
                db.AddInParameter(cmd, "@NetWeight", DbType.Decimal, objExchangeTrans.NetWeight);
                db.AddInParameter(cmd, "@StoneWeight", DbType.Decimal, objExchangeTrans.StoneWeight);
                db.AddInParameter(cmd, "@MeltingWeight", DbType.Decimal, objExchangeTrans.MeltingWeight);
                db.AddInParameter(cmd, "@GrossWeight", DbType.Decimal, objExchangeTrans.GrossWeight);
                db.AddInParameter(cmd, "@CurrentRate", DbType.Decimal, objExchangeTrans.CurrentRate);
                db.AddInParameter(cmd, "@Amount", DbType.Decimal, objExchangeTrans.Amount);
                db.AddInParameter(cmd, "@Karat", DbType.String, objExchangeTrans.Karat);
                db.AddInParameter(cmd, "@FK_SalesID", DbType.Int32, objExchangeTrans.SalesID);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_ExchangeTransID"));
            }
            catch (Exception ex)
            {
                sException = "ExchangeTrans AddExchangeTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateExchangeTrans(Entity.Billing.ExchangeTrans objExchangeTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_EXCHANGETRANS);
                db.AddInParameter(cmd, "@PK_ExchangeTransID", DbType.Int32, objExchangeTrans.ExchangeTransID);
                db.AddInParameter(cmd, "@FK_ExchangeID", DbType.Int32, objExchangeTrans.ExchangeID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, objExchangeTrans.Product.ProductID);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, objExchangeTrans.Category.CategoryID);
                db.AddInParameter(cmd, "@NetWeight", DbType.Decimal, objExchangeTrans.NetWeight);
                db.AddInParameter(cmd, "@StoneWeight", DbType.Decimal, objExchangeTrans.StoneWeight);
                db.AddInParameter(cmd, "@MeltingWeight", DbType.Decimal, objExchangeTrans.MeltingWeight);
                db.AddInParameter(cmd, "@GrossWeight", DbType.Decimal, objExchangeTrans.GrossWeight);
                db.AddInParameter(cmd, "@CurrentRate", DbType.Decimal, objExchangeTrans.CurrentRate);
                db.AddInParameter(cmd, "@Amount", DbType.Decimal, objExchangeTrans.Amount);
                db.AddInParameter(cmd, "@Karat", DbType.String, objExchangeTrans.Karat);
                db.AddInParameter(cmd, "@FK_SalesID", DbType.Int32, objExchangeTrans.SalesID);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "ExchangeTrans UpdateExchangeTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteExchangeTrans(int iExchangeTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_EXCHANGETRANS);
                db.AddInParameter(cmd, "@PK_ExchangeTransID", DbType.Int32, iExchangeTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "ExchangeTrans DeleteExchangeTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        
    }
}
