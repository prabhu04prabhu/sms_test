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
    public class TDSPayment
    {
        public static Collection<Entity.Billing.TDSPayment> GetTDSPayment(int ipatientID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.TDSPayment> objList = new Collection<Entity.Billing.TDSPayment>();
            Entity.Billing.TDSPayment objTDSPayment;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TDSPAYMENT);
                db.AddInParameter(cmd, "@PK_TDSPaymentID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objTDSPayment = new Entity.Billing.TDSPayment();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();

                        objTDSPayment.TDSPaymentID = Convert.ToInt32(drData["PK_TDSPaymentID"]);
                        objTDSPayment.TDSPaymentNo = Convert.ToString(drData["TDSPaymentNo"]);
                        objTDSPayment.TDSPaymentDate = Convert.ToDateTime(drData["TDSPaymentDate"]);
                        objTDSPayment.sTDSPaymentDate = objTDSPayment.TDSPaymentDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objTDSPayment.Customer = objCustomer;

                        objTDSPayment.Narration = Convert.ToString(drData["Narration"]);
                        objTDSPayment.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objTDSPayment.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objTDSPayment.Roundoff = Convert.ToDecimal(drData["RoundOff"]);

                        objTDSPayment.SlipNo = Convert.ToString(drData["SlipNo"]);
                        objTDSPayment.SlipDate = Convert.ToDateTime(drData["SlipDate"]);
                        objTDSPayment.sSlipDate = objTDSPayment.SlipDate.ToString("dd/MM/yyyy");

                        objList.Add(objTDSPayment);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "TDSPayment GetTDSPayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.TDSPayment> GetTopTDSPayment(int ipatientID = 0, int iBranchID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.TDSPayment> objList = new Collection<Entity.Billing.TDSPayment>();
            Entity.Billing.TDSPayment objTDSPayment;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;

            try
            {

                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TDSPAYMENT);
                db.AddInParameter(cmd, "@PK_TDSPaymentID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objTDSPayment = new Entity.Billing.TDSPayment();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();

                        objTDSPayment.TDSPaymentID = Convert.ToInt32(drData["PK_TDSPaymentID"]);
                        objTDSPayment.TDSPaymentNo = Convert.ToString(drData["TDSPaymentNo"]);
                        objTDSPayment.TDSPaymentDate = Convert.ToDateTime(drData["TDSPaymentDate"]);
                        objTDSPayment.sTDSPaymentDate = objTDSPayment.TDSPaymentDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objTDSPayment.Customer = objCustomer;

                        objTDSPayment.Narration = Convert.ToString(drData["Narration"]);
                        objTDSPayment.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objTDSPayment.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objTDSPayment.Roundoff = Convert.ToDecimal(drData["RoundOff"]);

                        objTDSPayment.SlipNo = Convert.ToString(drData["SlipNo"]);
                        objTDSPayment.SlipDate = Convert.ToDateTime(drData["SlipDate"]);
                        objTDSPayment.sSlipDate = objTDSPayment.SlipDate.ToString("dd/MM/yyyy");

                        objList.Add(objTDSPayment);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "TDSPayment GetTDSPayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.TDSPayment> GetTDSPaymentID(int ipatientID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.TDSPayment> objList = new Collection<Entity.Billing.TDSPayment>();
            Entity.Billing.TDSPayment objTDSPayment;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TDSPAYMENT);
                db.AddInParameter(cmd, "@PK_TDSPaymentID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objTDSPayment = new Entity.Billing.TDSPayment();

                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();

                        objTDSPayment.TDSPaymentID = Convert.ToInt32(drData["PK_TDSPaymentID"]);
                        objTDSPayment.TDSPaymentNo = Convert.ToString(drData["TDSPaymentNo"]);
                        objTDSPayment.TDSPaymentDate = Convert.ToDateTime(drData["TDSPaymentDate"]);
                        objTDSPayment.sTDSPaymentDate = objTDSPayment.TDSPaymentDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objTDSPayment.Customer = objCustomer;

                        objTDSPayment.Narration = Convert.ToString(drData["Narration"]);
                        objTDSPayment.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objTDSPayment.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objTDSPayment.Roundoff = Convert.ToDecimal(drData["RoundOff"]);

                        objTDSPayment.SlipNo = Convert.ToString(drData["SlipNo"]);
                        objTDSPayment.SlipDate = Convert.ToDateTime(drData["SlipDate"]);
                        objTDSPayment.sSlipDate = objTDSPayment.SlipDate.ToString("dd/MM/yyyy");

                        objList.Add(objTDSPayment);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "TDSPayment GetTDSPayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.TDSPayment> SearchTDSPayment(string ID, int iBranchID, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.TDSPayment> objList = new Collection<Entity.Billing.TDSPayment>();
            Entity.Billing.TDSPayment objTDSPayment;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_TDSPAYMENT);
                db.AddInParameter(cmd, "@key", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objTDSPayment = new Entity.Billing.TDSPayment();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();

                        objTDSPayment.TDSPaymentID = Convert.ToInt32(drData["PK_TDSPaymentID"]);
                        objTDSPayment.TDSPaymentNo = Convert.ToString(drData["TDSPaymentNo"]);
                        objTDSPayment.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objTDSPayment.TDSPaymentDate = Convert.ToDateTime(drData["TDSPaymentDate"]);
                        objTDSPayment.sTDSPaymentDate = objTDSPayment.TDSPaymentDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objTDSPayment.Customer = objCustomer;

                        objTDSPayment.Narration = Convert.ToString(drData["Narration"]);
                        objTDSPayment.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objTDSPayment.Roundoff = Convert.ToDecimal(drData["RoundOff"]);

                        objTDSPayment.SlipNo = Convert.ToString(drData["SlipNo"]);
                        objTDSPayment.SlipDate = Convert.ToDateTime(drData["SlipDate"]);
                        objTDSPayment.sSlipDate = objTDSPayment.SlipDate.ToString("dd/MM/yyyy");

                        objList.Add(objTDSPayment);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "TDSPayment GetTDSPayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Entity.Billing.TDSPayment GetTDSPaymentByID(int iTDSPaymentID, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.TDSPayment objTDSPayment = new Entity.Billing.TDSPayment();
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TDSPAYMENT);
                db.AddInParameter(cmd, "@PK_TDSPaymentID", DbType.Int32, iTDSPaymentID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objTDSPayment = new Entity.Billing.TDSPayment();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();

                        objTDSPayment.TDSPaymentID = Convert.ToInt32(drData["PK_TDSPaymentID"]);
                        objTDSPayment.TDSPaymentNo = Convert.ToString(drData["TDSPaymentNo"]);
                        objTDSPayment.TDSPaymentDate = Convert.ToDateTime(drData["TDSPaymentDate"]);
                        objTDSPayment.sTDSPaymentDate = objTDSPayment.TDSPaymentDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objTDSPayment.Customer = objCustomer;

                        objTDSPayment.Narration = Convert.ToString(drData["Narration"]);
                        objTDSPayment.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objTDSPayment.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objTDSPayment.Roundoff = Convert.ToDecimal(drData["RoundOff"]);
                        objTDSPayment.ManualPayment = Convert.ToInt32(drData["ManualPayment"]);
                        
                        objTDSPayment.SlipNo = Convert.ToString(drData["SlipNo"]);
                        objTDSPayment.SlipDate = Convert.ToDateTime(drData["SlipDate"]);
                        objTDSPayment.sSlipDate = objTDSPayment.SlipDate.ToString("dd/MM/yyyy");

                        objTDSPayment.TDSPaymentTrans = TDSPaymentTrans.GetTDSPaymentTransByTDSPaymentID(objTDSPayment.TDSPaymentID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "TDSPayment GetTDSPaymentByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objTDSPayment;
        }
        public static int AddTDSPayment(Entity.Billing.TDSPayment objTDSPayment)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_TDSPAYMENT);
                db.AddOutParameter(cmd, "@PK_TDSPaymentID", DbType.Int32, objTDSPayment.TDSPaymentID);
                db.AddInParameter(cmd, "@TDSPaymentNo", DbType.String, objTDSPayment.TDSPaymentNo);
                db.AddInParameter(cmd, "@TDSPaymentDate", DbType.String, objTDSPayment.sTDSPaymentDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objTDSPayment.Customer.CustomerID);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objTDSPayment.TotalAmount);
                db.AddInParameter(cmd, "@ManualPayment", DbType.Int32, objTDSPayment.ManualPayment);
                db.AddInParameter(cmd, "@Narration", DbType.String, objTDSPayment.Narration);
                db.AddInParameter(cmd, "@DocumentPath", DbType.String, objTDSPayment.DocumentPath);
                db.AddInParameter(cmd, "@RoundOff", DbType.Decimal, objTDSPayment.Roundoff);
                db.AddInParameter(cmd, "@SlipNo", DbType.String, objTDSPayment.SlipNo);
                db.AddInParameter(cmd, "@SlipDate", DbType.String, objTDSPayment.sSlipDate);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objTDSPayment.CreatedBy.UserID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objTDSPayment.FinancialYear.FinancialYearID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_TDSPaymentID"));

                foreach (Entity.Billing.TDSPaymentTrans ObjTDSPaymentTrans in objTDSPayment.TDSPaymentTrans)
                    ObjTDSPaymentTrans.TDSPaymentID = iID;

                TDSPaymentTrans.SaveTDSPaymentTransaction(objTDSPayment.TDSPaymentTrans);
            }
            catch (Exception ex)
            {
                sException = "TDSPayment AddTDSPayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateTDSPayment(Entity.Billing.TDSPayment objTDSPayment)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_TDSPAYMENT);
                db.AddInParameter(cmd, "@PK_TDSPaymentID", DbType.Int32, objTDSPayment.TDSPaymentID);
                db.AddInParameter(cmd, "@TDSPaymentNo", DbType.String, objTDSPayment.TDSPaymentNo);
                db.AddInParameter(cmd, "@TDSPaymentDate", DbType.String, objTDSPayment.sTDSPaymentDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objTDSPayment.Customer.CustomerID);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objTDSPayment.TotalAmount);
                db.AddInParameter(cmd, "@ManualPayment", DbType.Int32, objTDSPayment.ManualPayment);
                db.AddInParameter(cmd, "@Narration", DbType.String, objTDSPayment.Narration);
                db.AddInParameter(cmd, "@DocumentPath", DbType.String, objTDSPayment.DocumentPath);
                db.AddInParameter(cmd, "@RoundOff", DbType.Decimal, objTDSPayment.Roundoff);
                db.AddInParameter(cmd, "@SlipNo", DbType.String, objTDSPayment.SlipNo);
                db.AddInParameter(cmd, "@SlipDate", DbType.String, objTDSPayment.sSlipDate);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objTDSPayment.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objTDSPayment.FinancialYear.FinancialYearID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                foreach (Entity.Billing.TDSPaymentTrans ObjTDSPaymentTrans in objTDSPayment.TDSPaymentTrans)
                    ObjTDSPaymentTrans.TDSPaymentID = objTDSPayment.TDSPaymentID;

                TDSPaymentTrans.SaveTDSPaymentTransaction(objTDSPayment.TDSPaymentTrans);
            }
            catch (Exception ex)
            {
                sException = "TDSPayment UpdateTDSPayment| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteTDSPayment(int iTDSPaymentId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_TDSPAYMENT);
                db.AddInParameter(cmd, "@PK_TDSPaymentID", DbType.Int32, iTDSPaymentId);
                // db.AddInParameter(cmd, "@Reason", DbType.String, Reason);
                //  db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, iUserId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "TDSPayment DeleteTDSPayment | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static DataSet GetTDSPaymentSummary()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESRETURNSUMMARY);
                dsList = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "TDSPayment GetTDSPaymentSummary | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsList;
        }
    }

    public class TDSPaymentTrans
    {
        public static Collection<Entity.Billing.TDSPaymentTrans> GetTDSPaymentTransByTDSPaymentID(int iTDSPaymentID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.TDSPaymentTrans> objList = new Collection<Entity.Billing.TDSPaymentTrans>();
            Entity.Billing.TDSPaymentTrans objTDSPaymentTrans = new Entity.Billing.TDSPaymentTrans();
            Entity.Billing.SalesEntry ObjProduct; Entity.Billing.Tax objTax; Entity.Billing.SalesEntry ObjoldSalesEntry;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TDSPAYMENTTRANS);
                db.AddInParameter(cmd, "@FK_TDSPaymentID", DbType.Int32, iTDSPaymentID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objTDSPaymentTrans = new Entity.Billing.TDSPaymentTrans();
                        ObjProduct = new Entity.Billing.SalesEntry();
                        objTax = new Entity.Billing.Tax();
                        ObjoldSalesEntry = new Entity.Billing.SalesEntry();


                        objTDSPaymentTrans.TDSPaymentTransID = Convert.ToInt32(drData["PK_TDSPaymentTransID"]);
                        objTDSPaymentTrans.TDSPaymentID = Convert.ToInt32(drData["FK_TDSPaymentID"]);

                        ObjProduct.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        ObjProduct.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        ObjProduct.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        ObjProduct.sInvoiceDate = ObjProduct.InvoiceDate.ToString("dd/MM/yyyy");
                        ObjProduct.NetAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objTDSPaymentTrans.SalesEntry = ObjProduct;

                        ObjoldSalesEntry.SalesEntryID = Convert.ToInt32(drData["OldFK_SalesEntryID"]);
                        if(drData["oldInvoiceNo"] != DBNull.Value)
                            ObjoldSalesEntry.InvoiceNo = Convert.ToString(drData["oldInvoiceNo"]);
                        else
                            ObjoldSalesEntry.InvoiceNo = "-";
                        if (drData["oldInvoiceDate"] != DBNull.Value)
                        {
                            ObjoldSalesEntry.InvoiceDate = Convert.ToDateTime(drData["oldInvoiceDate"]);
                            ObjoldSalesEntry.sInvoiceDate = ObjProduct.InvoiceDate.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            ObjoldSalesEntry.InvoiceDate = DateTime.Now;
                            ObjoldSalesEntry.sInvoiceDate = "";
                        }
                        if (drData["oldTotalAmount"] != DBNull.Value)
                            ObjoldSalesEntry.NetAmount = Convert.ToDecimal(drData["oldTotalAmount"]);
                        else
                            ObjoldSalesEntry.NetAmount = 0;
                        objTDSPaymentTrans.oldSalesEntry = ObjoldSalesEntry;

                        objTDSPaymentTrans.TDSAmount = Convert.ToDecimal(drData["TDSAmount"]);
                        objTDSPaymentTrans.TDSPercent = Convert.ToDecimal(drData["TDSPercent"]);
                        objTDSPaymentTrans.Roundoff = Convert.ToDecimal(drData["Roundoff"]);

                        objList.Add(objTDSPaymentTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "TDSPaymentTrans GetTDSPaymentTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveTDSPaymentTransaction(Collection<Entity.Billing.TDSPaymentTrans> ObjTDSPaymentTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.TDSPaymentTrans ObjTDSPaymentTransaction in ObjTDSPaymentTransList)
            {
                if (ObjTDSPaymentTransaction.StatusFlag == "I")
                    iID = AddTDSPaymentTrans(ObjTDSPaymentTransaction);
                else if (ObjTDSPaymentTransaction.StatusFlag == "U")
                    bResult = UpdateTDSPaymentTrans(ObjTDSPaymentTransaction);
                else if (ObjTDSPaymentTransaction.StatusFlag == "D")
                    bResult = DeleteTDSPaymentTrans(ObjTDSPaymentTransaction.TDSPaymentTransID);
            }
        }
        public static int AddTDSPaymentTrans(Entity.Billing.TDSPaymentTrans objTDSPaymentTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_TDSPAYMENTTRANS);
                db.AddOutParameter(cmd, "@PK_TDSPaymentTransID", DbType.Int32, objTDSPaymentTrans.TDSPaymentTransID);
                db.AddInParameter(cmd, "@FK_TDSPaymentID", DbType.Int32, objTDSPaymentTrans.TDSPaymentID);
                db.AddInParameter(cmd, "@FK_SalesEntryID", DbType.Int32, objTDSPaymentTrans.SalesEntry.SalesEntryID);
                db.AddInParameter(cmd, "@OldFK_SalesEntryID", DbType.Int32, objTDSPaymentTrans.oldSalesEntry.SalesEntryID);
                db.AddInParameter(cmd, "@TDSPercent", DbType.Decimal, objTDSPaymentTrans.TDSPercent);
                db.AddInParameter(cmd, "@TDSAmount", DbType.Decimal, objTDSPaymentTrans.TDSAmount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objTDSPaymentTrans.Roundoff);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_TDSPaymentTransID"));
            }
            catch (Exception ex)
            {
                sException = "TDSPaymentTrans AddTDSPaymentTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateTDSPaymentTrans(Entity.Billing.TDSPaymentTrans objTDSPaymentTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_TDSPAYMENTTRANS);
                db.AddInParameter(cmd, "@PK_TDSPaymentTransID", DbType.Int32, objTDSPaymentTrans.TDSPaymentTransID);
                db.AddInParameter(cmd, "@FK_TDSPaymentID", DbType.Int32, objTDSPaymentTrans.TDSPaymentID);
                db.AddInParameter(cmd, "@FK_SalesEntryID", DbType.Int32, objTDSPaymentTrans.SalesEntry.SalesEntryID);
                db.AddInParameter(cmd, "@OldFK_SalesEntryID", DbType.Int32, objTDSPaymentTrans.oldSalesEntry.SalesEntryID);
                db.AddInParameter(cmd, "@TDSPercent", DbType.Decimal, objTDSPaymentTrans.TDSPercent);
                db.AddInParameter(cmd, "@TDSAmount", DbType.Decimal, objTDSPaymentTrans.TDSAmount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objTDSPaymentTrans.Roundoff);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "TDSPaymentTrans UpdateTDSPaymentTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteTDSPaymentTrans(int iTDSPaymentTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_TDSPAYMENTTRANS);
                db.AddInParameter(cmd, "@PK_TDSPaymentTransID", DbType.Int32, iTDSPaymentTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "TDSPaymentTrans DeleteTDSPaymentTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

    }
}
