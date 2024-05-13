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
    public class Inward
    {
        public static Collection<Entity.Billing.Inward> GetInward(int ipatientID = 0, int iFinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Inward> objList = new Collection<Entity.Billing.Inward>();
            Entity.Billing.Inward objInward;


            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_INWARD);
                db.AddInParameter(cmd, "@PK_InwardID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, iFinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objInward = new Entity.Billing.Inward();
                        objInward.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objInward.InwardID = Convert.ToInt32(drData["PK_InwardID"]);
                        objInward.InwardNo = Convert.ToString(drData["InwardNo"]);
                        objInward.InwardDate = Convert.ToDateTime(drData["InwardDate"]);
                        objInward.TotalQuantity = Convert.ToInt32(drData["TotalQuantity"]);
                        objInward.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objInward.Comments = Convert.ToString(drData["Comments"]);
                        objInward.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objInward.sInwardDate = objInward.InwardDate.ToString("dd/MM/yyyy") + " " + objInward.CreatedOn.ToString("h:mm");
                        objInward.CEmployeeName = Convert.ToString(drData["CEmployeeName"]);
                        objInward.MEmployeeName = Convert.ToString(drData["MEmployeeName"]);

                        objList.Add(objInward);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Inward GetInward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
               
        public static Collection<Entity.Billing.Inward> SearchInward(string ID, int iFinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Inward> objList = new Collection<Entity.Billing.Inward>();
            Entity.Billing.Inward objInward;
            Entity.patient objpatient; Entity.Discharge.Doctor objdoctor;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_INWARDDYNAMIC);
                db.AddInParameter(cmd, "@PK_InwardID", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, iFinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objInward = new Entity.Billing.Inward();
                        objpatient = new Entity.patient();
                        objdoctor = new Entity.Discharge.Doctor();
                        objInward.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objInward.InwardID = Convert.ToInt32(drData["PK_InwardID"]);
                        objInward.InwardNo = Convert.ToString(drData["InwardNo"]);
                        objInward.InwardDate = Convert.ToDateTime(drData["InwardDate"]);
                        objInward.TotalQuantity = Convert.ToInt32(drData["TotalQuantity"]);
                        objInward.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objInward.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objInward.sInwardDate = objInward.InwardDate.ToString("dd/MM/yyyy") + " " + objInward.CreatedOn.ToString("h:mm");
                        objInward.CEmployeeName = Convert.ToString(drData["CEmployeeName"]);
                        objInward.MEmployeeName = Convert.ToString(drData["MEmployeeName"]);

                        objList.Add(objInward);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Inward GetInward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
                
        public static Entity.Billing.Inward GetInwardByID(int iInwardID,int iFinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Inward objInward = new Entity.Billing.Inward();
            

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_INWARD);
                db.AddInParameter(cmd, "@PK_InwardID", DbType.Int32, iInwardID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, iFinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objInward = new Entity.Billing.Inward();
                        objInward.InwardID = Convert.ToInt32(drData["PK_InwardID"]);
                        objInward.InwardNo = Convert.ToString(drData["InwardNo"]);
                        objInward.InwardDate = Convert.ToDateTime(drData["InwardDate"]);
                        objInward.TotalQuantity = Convert.ToInt32(drData["TotalQuantity"]);
                        objInward.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objInward.Comments = Convert.ToString(drData["Comments"]);
                        objInward.sInwardDate = objInward.InwardDate.ToString("dd/MM/yyyy");


                        objInward.InwardTrans = InwardTrans.GetInwardTransByInwardID(objInward.InwardID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Inward GetInwardByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objInward;
        }
        public static int AddInward(Entity.Billing.Inward objInward)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_INWARD);
                db.AddOutParameter(cmd, "@PK_InwardID", DbType.Int32, objInward.InwardID);
                db.AddInParameter(cmd, "@InwardNo", DbType.String, objInward.InwardNo);
                db.AddInParameter(cmd, "@InwardDate", DbType.String, objInward.sInwardDate);
                db.AddInParameter(cmd, "@Comments", DbType.String, objInward.Comments);
                db.AddInParameter(cmd, "@TotalQuantity", DbType.Int32, objInward.TotalQuantity);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objInward.NetAmount);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objInward.FinancialYear.FinancialYearID);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objInward.CreatedBy.UserID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_InwardID"));

                foreach (Entity.Billing.InwardTrans ObjInwardTrans in objInward.InwardTrans)
                    ObjInwardTrans.InwardID = iID;

                InwardTrans.SaveInwardTransaction(objInward.InwardTrans);
            }
            catch (Exception ex)
            {
                sException = "Inward AddInward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateInward(Entity.Billing.Inward objInward)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_INWARD);
                db.AddInParameter(cmd, "@PK_InwardID", DbType.Int32, objInward.InwardID);
                db.AddInParameter(cmd, "@InwardNo", DbType.String, objInward.InwardNo);
                db.AddInParameter(cmd, "@InwardDate", DbType.String, objInward.sInwardDate);
                db.AddInParameter(cmd, "@TotalQuantity", DbType.Int32, objInward.TotalQuantity);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objInward.NetAmount);
                db.AddInParameter(cmd, "@Comments", DbType.String, objInward.Comments);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objInward.FinancialYear.FinancialYearID);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objInward.ModifiedBy.UserID);


                foreach (Entity.Billing.InwardTrans ObjInwardTrans in objInward.InwardTrans)
                    ObjInwardTrans.InwardID = objInward.InwardID;

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                InwardTrans.SaveInwardTransaction(objInward.InwardTrans);
            }
            catch (Exception ex)
            {
                sException = "Inward UpdateInward| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteInward(int iInwardId, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_INWARD);
                db.AddInParameter(cmd, "@PK_InwardID", DbType.Int32, iInwardId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Inward DeleteInward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        //public static DataSet GetInwardSummary()
        //{
        //    string sException = string.Empty;
        //    Database db;
        //    DataSet dsList = null;

        //    try
        //    {
        //        db = Entity.DBConnection.dbCon;
        //        DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_InwardSUMMARY);
        //        dsList = db.ExecuteDataSet(cmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "Inward GetInwardSummary | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return dsList;
        //}
    }

    public class InwardTrans
    {
        public static Collection<Entity.Billing.InwardTrans> GetInwardTransByInwardID(int iInwardID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.InwardTrans> objList = new Collection<Entity.Billing.InwardTrans>();
            Entity.Billing.InwardTrans objInwardTrans = new Entity.Billing.InwardTrans();
            Entity.Master.Product objProduct;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_INWARDTRANS);
                db.AddInParameter(cmd, "@FK_InwardID", DbType.Int32, iInwardID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objInwardTrans = new Entity.Billing.InwardTrans();
                        objProduct = new Entity.Master.Product();

                        objInwardTrans.InwardTransID = Convert.ToInt32(drData["PK_InwardTransID"]);
                        objInwardTrans.InwardID = Convert.ToInt32(drData["FK_InwardID"]);

                        objInwardTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objInwardTrans.Rate = Convert.ToInt32(drData["Rate"]);
                        objInwardTrans.SubTotal = Convert.ToDecimal(drData["SubTotal"]);
                        objInwardTrans.Barcode = Convert.ToString(drData["Barcode"]);

                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objInwardTrans.Product = objProduct;

                        objList.Add(objInwardTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "InwardTrans GetInwardTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveInwardTransaction(Collection<Entity.Billing.InwardTrans> ObjInwardTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.InwardTrans ObjInwardTransaction in ObjInwardTransList)
            {
                if (ObjInwardTransaction.StatusFlag == "I")
                    iID = AddInwardTrans(ObjInwardTransaction);
                else if (ObjInwardTransaction.StatusFlag == "U")
                    bResult = UpdateInwardTrans(ObjInwardTransaction);
                else if (ObjInwardTransaction.StatusFlag == "D")
                    bResult = DeleteInwardTrans(ObjInwardTransaction.InwardTransID);
            }
        }
        public static int AddInwardTrans(Entity.Billing.InwardTrans objInwardTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_INWARDTRANS);
                db.AddOutParameter(cmd, "@PK_InwardTransID", DbType.Int32, objInwardTrans.InwardTransID);
                db.AddInParameter(cmd, "@FK_InwardID", DbType.Int32, objInwardTrans.InwardID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objInwardTrans.Product.ProductID);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objInwardTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objInwardTrans.Rate);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objInwardTrans.SubTotal);
                db.AddInParameter(cmd, "@Barcode", DbType.String, objInwardTrans.Barcode);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_InwardTransID"));
            }
            catch (Exception ex)
            {
                sException = "InwardTrans AddInwardTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateInwardTrans(Entity.Billing.InwardTrans objInwardTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_INWARDTRANS);
                db.AddInParameter(cmd, "@PK_InwardTransID", DbType.Int32, objInwardTrans.InwardTransID);
                db.AddInParameter(cmd, "@FK_InwardID", DbType.Int32, objInwardTrans.InwardID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objInwardTrans.Product.ProductID);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objInwardTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objInwardTrans.Rate);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objInwardTrans.SubTotal);
                db.AddInParameter(cmd, "@Barcode", DbType.String, objInwardTrans.Barcode);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "InwardTrans UpdateInwardTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteInwardTrans(int iInwardTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_INWARDTRANS);
                db.AddInParameter(cmd, "@PK_InwardTransID", DbType.Int32, iInwardTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "InwardTrans DeleteInwardTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

    }
}
