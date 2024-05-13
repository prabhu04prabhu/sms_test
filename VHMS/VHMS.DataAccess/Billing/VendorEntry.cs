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
using VHMS.Entity.Billing;

namespace VHMS.DataAccess.Billing
{
    public class VendorEntry
    {
        public static Collection<Entity.Billing.VendorEntry> GetVendorEntry(int ipatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.VendorEntry> objList = new Collection<Entity.Billing.VendorEntry>();
            Entity.Billing.VendorEntry objVendorEntry; Entity.Billing.Vendor objVendor;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_VENDORENTRY);
                db.AddInParameter(cmd, "@PK_VendorEntryID", DbType.Int32, ipatientID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objVendorEntry = new Entity.Billing.VendorEntry();
                        objVendor = new Entity.Billing.Vendor();

                        objVendorEntry.VendorEntryID = Convert.ToInt32(drData["PK_VendorEntryID"]);
                        objVendor.VendorName = Convert.ToString(drData["VendorName"]);
                        objVendor.VendorAddress = Convert.ToString(drData["VendorAddress"]);
                        objVendor.VendorCode = Convert.ToString(drData["VendorCode"]);
                        objVendor.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objVendor.VendorID = Convert.ToInt32(drData["FK_VendorID"]);
                        objVendorEntry.Vendor = objVendor;



                        objVendorEntry.Status = Convert.ToString(drData["Status"]);
                        objVendorEntry.ClosingDate = Convert.ToDateTime(drData["ClosingDate"]);
                        objVendorEntry.sClosingDate = objVendorEntry.ClosingDate.ToString("dd/MM/yyyy");

                        objVendorEntry.TotalInQty = Convert.ToInt32(drData["TotalInQty"]);
                        objVendorEntry.TotalOutQty = Convert.ToInt32(drData["TotalOutQty"]);
                        objVendorEntry.BalanceQty = Convert.ToInt32(drData["BalanceQty"]);
                        objVendorEntry.ClosingBalance = Convert.ToDecimal(drData["ClosingBalance"]);
                        objVendorEntry.MonthYear = Convert.ToString(drData["MonthYear"]);
                        objList.Add(objVendorEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VendorEntry GetVendorEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.VendorEntry> SearchVendorEntry(string ID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.VendorEntry> objList = new Collection<Entity.Billing.VendorEntry>();
            Entity.Billing.VendorEntry objVendorEntry; Entity.Billing.Vendor objVendor;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_VENDORENTRY);
                db.AddInParameter(cmd, "@Key", DbType.String, ID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objVendorEntry = new Entity.Billing.VendorEntry();
                        objVendor = new Entity.Billing.Vendor();

                        objVendorEntry.VendorEntryID = Convert.ToInt32(drData["PK_VendorEntryID"]);
                        objVendor.VendorName = Convert.ToString(drData["VendorName"]);
                        objVendor.VendorAddress = Convert.ToString(drData["VendorAddress"]);
                        objVendor.VendorCode = Convert.ToString(drData["VendorCode"]);
                        objVendor.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objVendor.VendorID = Convert.ToInt32(drData["FK_VendorID"]);
                        objVendorEntry.Vendor = objVendor;

                        objVendorEntry.Status = Convert.ToString(drData["Status"]);
                        objVendorEntry.ClosingDate = Convert.ToDateTime(drData["ClosingDate"]);
                        objVendorEntry.sClosingDate = objVendorEntry.ClosingDate.ToString("dd/MM/yyyy");

                        objVendorEntry.TotalInQty = Convert.ToInt32(drData["TotalInQty"]);
                        objVendorEntry.TotalOutQty = Convert.ToInt32(drData["TotalOutQty"]);
                        objVendorEntry.BalanceQty = Convert.ToInt32(drData["BalanceQty"]);
                        objVendorEntry.ClosingBalance = Convert.ToDecimal(drData["ClosingBalance"]);
                        objVendorEntry.MonthYear = Convert.ToString(drData["MonthYear"]);
                        objList.Add(objVendorEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VendorEntry GetVendorEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Entity.Billing.VendorEntry GetVendorEntryByStatus(int iVendorID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.VendorEntry objVendorEntry = new Entity.Billing.VendorEntry();
            Entity.Billing.Vendor objVendor;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_VENDORENTRYBYSTATUS);
                db.AddInParameter(cmd, "@FK_VendorID", DbType.Int32, iVendorID);
                db.AddInParameter(cmd, "@Status", DbType.String, "Open");
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objVendorEntry = new Entity.Billing.VendorEntry();
                        objVendor = new Entity.Billing.Vendor();

                        objVendorEntry.VendorEntryID = Convert.ToInt32(drData["PK_VendorEntryID"]);
                        objVendor.VendorName = Convert.ToString(drData["VendorName"]);
                        objVendor.VendorAddress = Convert.ToString(drData["VendorAddress"]);
                        objVendor.VendorCode = Convert.ToString(drData["VendorCode"]);
                        objVendor.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objVendor.VendorID = Convert.ToInt32(drData["FK_VendorID"]);
                        objVendorEntry.Vendor = objVendor;

                        objVendorEntry.Status = Convert.ToString(drData["Status"]);
                        objVendorEntry.ClosingDate = Convert.ToDateTime(drData["ClosingDate"]);
                        objVendorEntry.sClosingDate = objVendorEntry.ClosingDate.ToString("dd/MM/yyyy");

                        objVendorEntry.TotalInQty = Convert.ToInt32(drData["TotalInQty"]);
                        objVendorEntry.TotalOutQty = Convert.ToInt32(drData["TotalOutQty"]);
                        objVendorEntry.BalanceQty = Convert.ToInt32(drData["BalanceQty"]);
                        if (drData["OpeningQty"] != DBNull.Value)
                            objVendorEntry.OpeningQty = Convert.ToInt32(drData["OpeningQty"]);
                        objVendorEntry.ClosingBalance = Convert.ToDecimal(drData["ClosingBalance"]);
                        if (drData["OpeningBalance"] != DBNull.Value)
                            objVendorEntry.OpeningBalance = Convert.ToDecimal(drData["OpeningBalance"]);
                        objVendorEntry.MonthYear = Convert.ToString(drData["MonthYear"]);

                        objVendorEntry.VendorTrans = VendorTrans.GetVendorTransByVendorEntryID(objVendorEntry.VendorEntryID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VendorEntry GetVendorEntryByStatus | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objVendorEntry;
        }

        public static Entity.Billing.VendorStockCheck GetVendorStockCheck(int iVendorID, int iWorkID, int iVendorEntryID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.VendorStockCheck objVendorEntry = new Entity.Billing.VendorStockCheck();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_VENDORSTOCKCHECK);
                db.AddInParameter(cmd, "@FK_VendorID", DbType.Int32, iVendorID);
                db.AddInParameter(cmd, "@FK_WorkID", DbType.Int32, iWorkID);
                db.AddInParameter(cmd, "@PK_VendorEntryID", DbType.Int32, iVendorEntryID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objVendorEntry = new Entity.Billing.VendorStockCheck();

                        objVendorEntry.Quantity = Convert.ToInt32(drData["Quantity"]);
                        objVendorEntry.RePolishQuantity = Convert.ToInt32(drData["RePolishQuantity"]);

                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VendorEntry GetVendorEntryByStatus | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objVendorEntry;
        }

        public static Entity.Billing.VendorEntry GetVendorEntryByOpeningQty(int iVendorID,int iVendorEntryID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.VendorEntry objVendorEntry = new Entity.Billing.VendorEntry();
            Entity.Billing.Vendor objVendor;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_VENDORENTRYBYOPENINGQTY);
                db.AddInParameter(cmd, "@FK_VendorID", DbType.Int32, iVendorID);
                db.AddInParameter(cmd, "@PK_VendorEntryID", DbType.Int32, iVendorEntryID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objVendorEntry = new Entity.Billing.VendorEntry();
                        objVendor = new Entity.Billing.Vendor();

                        if (drData["OpeningQty"] != DBNull.Value)
                            objVendorEntry.OpeningQty = Convert.ToInt32(drData["OpeningQty"]);
                        if (drData["OpeningBalance"] != DBNull.Value)
                            objVendorEntry.OpeningBalance = Convert.ToDecimal(drData["OpeningBalance"]);


                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VendorEntry GetVendorEntryByStatus | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objVendorEntry;
        }

        public static Entity.Billing.VendorEntry GetVendorEntryByID(int iVendorEntryID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.VendorEntry objVendorEntry = new Entity.Billing.VendorEntry();
            Entity.Billing.Vendor objVendor;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_VENDORENTRY);
                db.AddInParameter(cmd, "@PK_VendorEntryID", DbType.Int32, iVendorEntryID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objVendorEntry = new Entity.Billing.VendorEntry();
                        objVendor = new Entity.Billing.Vendor();

                        objVendorEntry.VendorEntryID = Convert.ToInt32(drData["PK_VendorEntryID"]);
                        objVendor.VendorName = Convert.ToString(drData["VendorName"]);
                        objVendor.VendorAddress = Convert.ToString(drData["VendorAddress"]);
                        objVendor.VendorCode = Convert.ToString(drData["VendorCode"]);
                        objVendor.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objVendorEntry.Comments = Convert.ToString(drData["Comments"]);
                        objVendor.VendorID = Convert.ToInt32(drData["FK_VendorID"]);
                        objVendorEntry.Vendor = objVendor;
                        objVendorEntry.Status = Convert.ToString(drData["Status"]);
                        objVendorEntry.ClosingDate = Convert.ToDateTime(drData["ClosingDate"]);
                        objVendorEntry.sClosingDate = objVendorEntry.ClosingDate.ToString("dd/MM/yyyy");
                        objVendorEntry.TotalInQty = Convert.ToInt32(drData["TotalInQty"]);
                        objVendorEntry.TotalOutQty = Convert.ToInt32(drData["TotalOutQty"]);
                        objVendorEntry.BalanceQty = Convert.ToInt32(drData["BalanceQty"]);
                        objVendorEntry.ClosingBalance = Convert.ToDecimal(drData["ClosingBalance"]);
                        objVendorEntry.MonthYear = Convert.ToString(drData["MonthYear"]);


                        objVendorEntry.VendorTrans = VendorTrans.GetVendorTransByVendorEntryID(objVendorEntry.VendorEntryID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VendorEntry GetVendorEntryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objVendorEntry;
        }
        public static int AddVendorEntry(Entity.Billing.VendorEntry objVendorEntry)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                string ClDate = DateTime.Now.ToString("dd/MM/yyyy");
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_VENDORENTRY);
                db.AddOutParameter(cmd, "@PK_VendorEntryID", DbType.Int32, objVendorEntry.VendorEntryID);
                db.AddInParameter(cmd, "@Status", DbType.String, objVendorEntry.Status);
                db.AddInParameter(cmd, "@Comments", DbType.String, objVendorEntry.Comments);
                db.AddInParameter(cmd, "@FK_VendorID", DbType.Int32, objVendorEntry.Vendor.VendorID);
                db.AddInParameter(cmd, "@TotalInQty", DbType.Int32, objVendorEntry.TotalInQty);
                db.AddInParameter(cmd, "@TotalOutQty", DbType.Int32, objVendorEntry.TotalOutQty);
                db.AddInParameter(cmd, "@BalanceQty", DbType.Int32, objVendorEntry.BalanceQty);
                db.AddInParameter(cmd, "@ClosingBalance", DbType.Decimal, objVendorEntry.ClosingBalance);
                db.AddInParameter(cmd, "@ClosingDate", DbType.String, ClDate);
                db.AddInParameter(cmd, "@MonthYear", DbType.String, objVendorEntry.MonthYear);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_VendorEntryID"));

                foreach (Entity.Billing.VendorTrans ObjVendorTrans in objVendorEntry.VendorTrans)
                    ObjVendorTrans.VendorEntryID = iID;
                VendorTrans.SaveVendorTransaction(objVendorEntry.VendorTrans);
            }
            catch (Exception ex)
            {
                sException = "VendorEntry AddVendorEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateVendorEntry(Entity.Billing.VendorEntry objVendorEntry)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                string ClDate = DateTime.Now.ToString("dd/MM/yyyy");
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_VENDORENTRY);
                db.AddInParameter(cmd, "@PK_VendorEntryID", DbType.Int32, objVendorEntry.VendorEntryID);
                db.AddInParameter(cmd, "@Status", DbType.String, objVendorEntry.Status);
                db.AddInParameter(cmd, "@Comments", DbType.String, objVendorEntry.Comments);
                db.AddInParameter(cmd, "@FK_VendorID", DbType.Int32, objVendorEntry.Vendor.VendorID);
                db.AddInParameter(cmd, "@TotalInQty", DbType.Int32, objVendorEntry.TotalInQty);
                db.AddInParameter(cmd, "@TotalOutQty", DbType.Int32, objVendorEntry.TotalOutQty);
                db.AddInParameter(cmd, "@BalanceQty", DbType.Int32, objVendorEntry.BalanceQty);
                db.AddInParameter(cmd, "@ClosingBalance", DbType.Decimal, objVendorEntry.ClosingBalance);
                db.AddInParameter(cmd, "@ClosingDate", DbType.String, ClDate);
                db.AddInParameter(cmd, "@MonthYear", DbType.String, objVendorEntry.MonthYear);

                foreach (Entity.Billing.VendorTrans ObjVendorTrans in objVendorEntry.VendorTrans)
                    ObjVendorTrans.VendorEntryID = objVendorEntry.VendorEntryID;

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                VendorTrans.SaveVendorTransaction(objVendorEntry.VendorTrans);
            }
            catch (Exception ex)
            {
                sException = "VendorEntry UpdateVendorEntry| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteVendorEntry(int iVendorEntryId, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_VENDORENTRY);
                db.AddInParameter(cmd, "@PK_VendorEntryID", DbType.Int32, iVendorEntryId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VendorEntry DeleteVendorEntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        //public static DataSet GetVendorEntrySummary()
        //{
        //    string sException = string.Empty;
        //    Database db;
        //    DataSet dsList = null;

        //    try
        //    {
        //        db = Entity.DBConnection.dbCon;
        //        DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_VendorEntrySUMMARY);
        //        dsList = db.ExecuteDataSet(cmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "VendorEntry GetVendorEntrySummary | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return dsList;
        //}
    }

    public class VendorTrans
    {
        public static Collection<Entity.Billing.VendorTrans> GetVendorTransByVendorEntryID(int iVendorEntryID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.VendorTrans> objList = new Collection<Entity.Billing.VendorTrans>();
            Entity.Billing.VendorTrans objVendorTrans = new Entity.Billing.VendorTrans();
            Entity.Ledger objLedger;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_VENDORTRANS);
                db.AddInParameter(cmd, "@FK_VendorEntryID", DbType.Int32, iVendorEntryID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objVendorTrans = new Entity.Billing.VendorTrans();
                        objLedger = new Entity.Ledger();

                        objVendorTrans.VendorTransID = Convert.ToInt32(drData["PK_VendorTransID"]);
                        objVendorTrans.VendorEntryID = Convert.ToInt32(drData["FK_VendorEntryID"]);
                        objVendorTrans.EntryDate = Convert.ToDateTime(drData["EntryDate"]);
                        objVendorTrans.sEntryDate = objVendorTrans.EntryDate.ToString("dd/MM/yyyy");
                        objVendorTrans.EntryType = Convert.ToString(drData["EntryType"]);
                        objVendorTrans.InQty = Convert.ToInt32(drData["InQty"]);
                        objVendorTrans.Balance_Qty = Convert.ToInt32(drData["Balance_Qty"]);
                        objVendorTrans.OutQty = Convert.ToInt32(drData["OutQty"]);
                        objVendorTrans.RePolishQty = Convert.ToInt32(drData["RePolishQty"]);
                        objVendorTrans.RePolish = Convert.ToInt32(drData["RePolish"]);
                        objVendorTrans.ReturnQty = Convert.ToInt32(drData["ReturnQty"]);
                        objVendorTrans.Rate = Convert.ToDecimal(drData["Rate"]);
                        objVendorTrans.SubTotal = Convert.ToDecimal(drData["SubTotal"]);
                        objVendorTrans.DamageAmount = Convert.ToDecimal(drData["DamageAmount"]);
                        objVendorTrans.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objVendorTrans.WorkID = Convert.ToInt32(drData["FK_WorkID"]);
                        objVendorTrans.WorkName = Convert.ToString(drData["WorkName"]);

                        objVendorTrans.LedgerName = Convert.ToString(drData["BankName"]);
                        objVendorTrans.BankID = Convert.ToInt32(drData["FK_BankID"]);

                        objVendorTrans.CollectionDate = Convert.ToDateTime(drData["CollectionDate"]);
                        objVendorTrans.sCollectionDate = objVendorTrans.CollectionDate.ToString("dd/MM/yyyy");
                        objVendorTrans.PaymentModeID = Convert.ToInt32(drData["FK_PaymentModeID"]);
                        objVendorTrans.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objVendorTrans.ChequeNo = Convert.ToString(drData["ChequeNo"]);

                        objVendorTrans.TDSPercent = Convert.ToDecimal(drData["TDSPercent"]);
                        objVendorTrans.TDSAmount = Convert.ToDecimal(drData["TDSAmount"]);
                        objVendorTrans.RoundOff = Convert.ToDecimal(drData["RoundOff"]);
                        objVendorTrans.PayableAmount = Convert.ToDecimal(drData["PayableAmount"]);

                        objList.Add(objVendorTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VendorTrans GetVendorTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveVendorTransaction(Collection<Entity.Billing.VendorTrans> ObjVendorTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.VendorTrans ObjVendorTransaction in ObjVendorTransList)
            {
                if (ObjVendorTransaction.StatusFlag == "I")
                    iID = AddVendorTrans(ObjVendorTransaction);
                else if (ObjVendorTransaction.StatusFlag == "U")
                    bResult = UpdateVendorTrans(ObjVendorTransaction);
                else if (ObjVendorTransaction.StatusFlag == "D")
                    bResult = DeleteVendorTrans(ObjVendorTransaction.VendorTransID);
            }
        }
        public static int AddVendorTrans(Entity.Billing.VendorTrans objVendorTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_VENDORTRANS);
                db.AddOutParameter(cmd, "@PK_VendorTransID", DbType.Int32, objVendorTrans.VendorTransID);
                db.AddInParameter(cmd, "@FK_VendorEntryID", DbType.Int32, objVendorTrans.VendorEntryID);
                db.AddInParameter(cmd, "@EntryDate", DbType.String, objVendorTrans.sEntryDate);
                db.AddInParameter(cmd, "@EntryType", DbType.String, objVendorTrans.EntryType);
                db.AddInParameter(cmd, "@InQty", DbType.Int32, objVendorTrans.InQty);
                db.AddInParameter(cmd, "@Balance_Qty", DbType.Int32, objVendorTrans.Balance_Qty);
                db.AddInParameter(cmd, "@RePolishQty", DbType.Int32, objVendorTrans.RePolishQty);
                db.AddInParameter(cmd, "@RePolish", DbType.Int32, objVendorTrans.RePolish);
                db.AddInParameter(cmd, "@OutQty", DbType.Int32, objVendorTrans.OutQty);
                db.AddInParameter(cmd, "@ReturnQty", DbType.Int32, objVendorTrans.ReturnQty);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objVendorTrans.Rate);
                db.AddInParameter(cmd, "@DamageAmount", DbType.Decimal, objVendorTrans.DamageAmount);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objVendorTrans.SubTotal);
                db.AddInParameter(cmd, "@PaidAmount", DbType.Decimal, objVendorTrans.PaidAmount);
                db.AddInParameter(cmd, "@FK_WorkID", DbType.Int32, objVendorTrans.WorkID);
                db.AddInParameter(cmd, "@CollectionDate", DbType.String, objVendorTrans.sCollectionDate);
                db.AddInParameter(cmd, "@FK_PaymentModeID", DbType.Int32, objVendorTrans.PaymentModeID);
                db.AddInParameter(cmd, "@ChequeNo", DbType.String, objVendorTrans.ChequeNo);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objVendorTrans.BankID);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, HttpContext.Current.Session["UserID"].ToString());
                db.AddInParameter(cmd, "@TDSPercent", DbType.Decimal, objVendorTrans.TDSPercent);
                db.AddInParameter(cmd, "@TDSAmount", DbType.Decimal, objVendorTrans.TDSAmount);
                db.AddInParameter(cmd, "@RoundOff", DbType.Decimal, objVendorTrans.RoundOff);
                db.AddInParameter(cmd, "@PayableAmount", DbType.Decimal, objVendorTrans.PayableAmount);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_VendorTransID"));
            }
            catch (Exception ex)
            {
                sException = "VendorTrans AddVendorTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateVendorTrans(Entity.Billing.VendorTrans objVendorTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_VENDORTRANS);
                db.AddInParameter(cmd, "@PK_VendorTransID", DbType.Int32, objVendorTrans.VendorTransID);
                db.AddInParameter(cmd, "@FK_VendorEntryID", DbType.Int32, objVendorTrans.VendorEntryID);
                db.AddInParameter(cmd, "@EntryDate", DbType.String, objVendorTrans.sEntryDate);
                db.AddInParameter(cmd, "@EntryType", DbType.String, objVendorTrans.EntryType);
                db.AddInParameter(cmd, "@InQty", DbType.Int32, objVendorTrans.InQty);
                db.AddInParameter(cmd, "@Balance_Qty", DbType.Int32, objVendorTrans.Balance_Qty);
                db.AddInParameter(cmd, "@RePolishQty", DbType.Int32, objVendorTrans.RePolishQty);
                db.AddInParameter(cmd, "@RePolish", DbType.Int32, objVendorTrans.RePolish);
                db.AddInParameter(cmd, "@OutQty", DbType.Int32, objVendorTrans.OutQty);
                db.AddInParameter(cmd, "@ReturnQty", DbType.Int32, objVendorTrans.ReturnQty);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objVendorTrans.Rate);
                db.AddInParameter(cmd, "@DamageAmount", DbType.Decimal, objVendorTrans.DamageAmount);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objVendorTrans.SubTotal);
                db.AddInParameter(cmd, "@PaidAmount", DbType.Decimal, objVendorTrans.PaidAmount);
                db.AddInParameter(cmd, "@FK_WorkID", DbType.Int32, objVendorTrans.WorkID);
                db.AddInParameter(cmd, "@CollectionDate", DbType.String, objVendorTrans.sCollectionDate);
                db.AddInParameter(cmd, "@FK_PaymentModeID", DbType.Int32, objVendorTrans.PaymentModeID);
                db.AddInParameter(cmd, "@ChequeNo", DbType.String, objVendorTrans.ChequeNo);
                db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objVendorTrans.BankID);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, HttpContext.Current.Session["UserID"].ToString());
                db.AddInParameter(cmd, "@TDSPercent", DbType.Decimal, objVendorTrans.TDSPercent);
                db.AddInParameter(cmd, "@TDSAmount", DbType.Decimal, objVendorTrans.TDSAmount);
                db.AddInParameter(cmd, "@RoundOff", DbType.Decimal, objVendorTrans.RoundOff);
                db.AddInParameter(cmd, "@PayableAmount", DbType.Decimal, objVendorTrans.PayableAmount);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VendorTrans UpdateVendorTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteVendorTrans(int iVendorTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_VENDORTRANS);
                db.AddInParameter(cmd, "@PK_VendorTransID", DbType.Int32, iVendorTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VendorTrans DeleteVendorTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

    }
    //public static void SaveVendorPaymentMode(Collection<Entity.Billing.VendorPaymentMode> ObjVendorTransList)
    //{
    //    int iID = 0;
    //    bool bResult = false;

    //    foreach (Entity.Billing.VendorTrans ObjVendorTransaction in ObjVendorTransList)
    //    {
    //        if (ObjVendorTransaction.StatusFlag == "I")
    //            iID = AddVendorTrans(ObjVendorTransaction);
    //        else if (ObjVendorTransaction.StatusFlag == "U")
    //            bResult = UpdateVendorTrans(ObjVendorTransaction);
    //        else if (ObjVendorTransaction.StatusFlag == "D")
    //            bResult = DeleteVendorTrans(ObjVendorTransaction.VendorTransID);
    //    }
    //}
    //public static int AddVendorPaymentMode(Entity.VendorPaymentMode objVendorPaymentMode)
    //    {
    //        string sException = string.Empty;
    //        int iID = 0;
    //        Database db;
    //        try
    //        {
    //            db = Entity.DBConnection.dbCon;
    //            DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_VendorPaymentMode);
    //            db.AddOutParameter(cmd, "@PK_VendorPaymentModeID", DbType.Int32, objVendorPaymentMode.VendorPaymentModeID);
    //            db.AddInParameter(cmd, "@FK_VendorEntryID", DbType.Int32, objVendorPaymentMode.VendorEntryID);
    //            db.AddInParameter(cmd, "@PaymentMode", DbType.String, objVendorPaymentMode.PaymentMode);
    //            db.AddInParameter(cmd, "@Amount", DbType.Decimal, objVendorPaymentMode.Amount);
    //            db.AddInParameter(cmd, "@ChequeNo", DbType.String, objVendorPaymentMode.ChequeNo);
    //            db.AddInParameter(cmd, "@IssueDate", DbType.String, objVendorPaymentMode.sIssueDate);
    //            db.AddInParameter(cmd, "@CollectionDate", DbType.String, objVendorPaymentMode.sCollectionDate);
    //            db.AddInParameter(cmd, "@IssuedBy", DbType.String, objVendorPaymentMode.IssuedBy);
    //            db.AddInParameter(cmd, "@Charges", DbType.Decimal, objVendorPaymentMode.Charges);
    //            db.AddInParameter(cmd, "@FK_BankID", DbType.Int32, objVendorPaymentMode.BankID);
    //            db.AddInParameter(cmd, "@Status", DbType.String, objVendorPaymentMode.Status);

    //            iID = db.ExecuteNonQuery(cmd);
    //            if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_VendorPaymentModeID"));
    //        }
    //        catch (Exception ex)
    //        {
    //            sException = "VendorPaymentMode AddVendorPaymentMode | " + ex.ToString();
    //            Log.Write(sException);
    //            throw;
    //        }
    //        return iID;
    //    }
}
