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
    public class ProcessingOutward
    {
        public static Collection<Entity.Billing.ProcessingOutward> GetProcessingOutward(int ipatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.ProcessingOutward> objList = new Collection<Entity.Billing.ProcessingOutward>();
            Entity.Billing.ProcessingOutward objProcessingOutward;
            Entity.Billing.Vendor objVendor; Entity.Work objWork;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PROCESSINGOUTWARD);
                db.AddInParameter(cmd, "@PK_ProcessingOutwardID", DbType.Int32, ipatientID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objProcessingOutward = new Entity.Billing.ProcessingOutward();
                        objVendor= new Entity.Billing.Vendor();
                        objWork = new Entity.Work();

                        objProcessingOutward.ProcessingOutwardID = Convert.ToInt32(drData["PK_ProcessingOutwardID"]);
                        objProcessingOutward.ProcessingOutwardNo = Convert.ToString(drData["ProcessingOutwardNo"]);
                        objProcessingOutward.ProcessingOutwardDate = Convert.ToDateTime(drData["ProcessingOutwardDate"]);
                        objProcessingOutward.TotalQuantity = Convert.ToInt32(drData["TotalQuantity"]);
                        objProcessingOutward.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objVendor.VendorID = Convert.ToInt32(drData["FK_VendorID"]);
                        objVendor.VendorName = Convert.ToString(drData["VendorName"]);
                        objProcessingOutward.Vendor = objVendor;

                        objWork.WorkID = Convert.ToInt32(drData["FK_WorkID"]);
                        objWork.WorkName = Convert.ToString(drData["WorkName"]);
                        objWork.Amount = Convert.ToDecimal(drData["Amount"]);
                        objProcessingOutward.Work = objWork;


                        objProcessingOutward.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objProcessingOutward.sProcessingOutwardDate = objProcessingOutward.ProcessingOutwardDate.ToString("dd/MM/yyyy") + " " + objProcessingOutward.CreatedOn.ToString("h:mm");

                        objList.Add(objProcessingOutward);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "ProcessingOutward GetProcessingOutward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
               
        public static Collection<Entity.Billing.ProcessingOutward> SearchProcessingOutward(string ID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.ProcessingOutward> objList = new Collection<Entity.Billing.ProcessingOutward>();
            Entity.Billing.ProcessingOutward objProcessingOutward;
            Entity.Billing.Vendor objVendor;
            Entity.Work objWork;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PROCESSINGOUTWARDDYNAMIC);
                db.AddInParameter(cmd, "@PK_ProcessingOutwardID", DbType.String, ID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objProcessingOutward = new Entity.Billing.ProcessingOutward();
                        objVendor = new Entity.Billing.Vendor();
                        objWork = new Entity.Work();

                        objProcessingOutward.ProcessingOutwardID = Convert.ToInt32(drData["PK_ProcessingOutwardID"]);
                        objProcessingOutward.ProcessingOutwardNo = Convert.ToString(drData["ProcessingOutwardNo"]);
                        objProcessingOutward.ProcessingOutwardDate = Convert.ToDateTime(drData["ProcessingOutwardDate"]);
                        objProcessingOutward.TotalQuantity = Convert.ToInt32(drData["TotalQuantity"]);
                        objProcessingOutward.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objVendor.VendorID = Convert.ToInt32(drData["FK_VendorID"]);
                        objVendor.VendorName = Convert.ToString(drData["VendorName"]);
                        objProcessingOutward.Vendor = objVendor;
                        objWork.WorkID = Convert.ToInt32(drData["FK_WorkID"]);
                        objWork.WorkName = Convert.ToString(drData["WorkName"]);
                        objWork.Amount = Convert.ToDecimal(drData["Amount"]);
                        objProcessingOutward.Work = objWork;
                        objProcessingOutward.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objProcessingOutward.sProcessingOutwardDate = objProcessingOutward.ProcessingOutwardDate.ToString("dd/MM/yyyy") + " " + objProcessingOutward.CreatedOn.ToString("h:mm");


                        objList.Add(objProcessingOutward);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "ProcessingOutward GetProcessingOutward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
                
        public static Entity.Billing.ProcessingOutward GetProcessingOutwardByID(int iProcessingOutwardID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.ProcessingOutward objProcessingOutward = new Entity.Billing.ProcessingOutward();
            Entity.Billing.Vendor objVendor;
            Entity.Work objWork;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PROCESSINGOUTWARD);
                db.AddInParameter(cmd, "@PK_ProcessingOutwardID", DbType.Int32, iProcessingOutwardID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objProcessingOutward = new Entity.Billing.ProcessingOutward();
                        objVendor = new Entity.Billing.Vendor();
                        objWork = new Entity.Work();

                        objProcessingOutward.ProcessingOutwardID = Convert.ToInt32(drData["PK_ProcessingOutwardID"]);
                        objProcessingOutward.ProcessingOutwardNo = Convert.ToString(drData["ProcessingOutwardNo"]);
                        objProcessingOutward.ProcessingOutwardDate = Convert.ToDateTime(drData["ProcessingOutwardDate"]);
                        objProcessingOutward.TotalQuantity = Convert.ToInt32(drData["TotalQuantity"]);
                        objProcessingOutward.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objVendor.VendorID = Convert.ToInt32(drData["FK_VendorID"]);
                        objVendor.VendorName = Convert.ToString(drData["VendorName"]);
                        objProcessingOutward.Vendor = objVendor;
                        objWork.WorkID = Convert.ToInt32(drData["FK_WorkID"]);
                        objWork.WorkName = Convert.ToString(drData["WorkName"]);
                        objWork.Amount = Convert.ToDecimal(drData["Amount"]);
                        objProcessingOutward.Work = objWork;
                        objProcessingOutward.sProcessingOutwardDate = objProcessingOutward.ProcessingOutwardDate.ToString("dd/MM/yyyy");


                        objProcessingOutward.ProcessingOutwardTrans = ProcessingOutwardTrans.GetProcessingOutwardTransByProcessingOutwardID(objProcessingOutward.ProcessingOutwardID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "ProcessingOutward GetProcessingOutwardByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objProcessingOutward;
        }
        public static int AddProcessingOutward(Entity.Billing.ProcessingOutward objProcessingOutward)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_PROCESSINGOUTWARD);
                db.AddOutParameter(cmd, "@PK_ProcessingOutwardID", DbType.Int32, objProcessingOutward.ProcessingOutwardID);
                db.AddInParameter(cmd, "@ProcessingOutwardNo", DbType.String, objProcessingOutward.ProcessingOutwardNo);
                db.AddInParameter(cmd, "@ProcessingOutwardDate", DbType.String, objProcessingOutward.sProcessingOutwardDate);
                db.AddInParameter(cmd, "@FK_VendorID", DbType.Int32, objProcessingOutward.Vendor.VendorID);
                db.AddInParameter(cmd, "@FK_WorkID", DbType.Int32, objProcessingOutward.Work.WorkID);
                db.AddInParameter(cmd, "@TotalQuantity", DbType.Int32, objProcessingOutward.TotalQuantity);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objProcessingOutward.TotalAmount);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objProcessingOutward.CreatedBy.UserID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_ProcessingOutwardID"));

                foreach (Entity.Billing.ProcessingOutwardTrans ObjProcessingOutwardTrans in objProcessingOutward.ProcessingOutwardTrans)
                    ObjProcessingOutwardTrans.ProcessingOutwardID = iID;

                ProcessingOutwardTrans.SaveProcessingOutwardTransaction(objProcessingOutward.ProcessingOutwardTrans);
            }
            catch (Exception ex)
            {
                sException = "ProcessingOutward AddProcessingOutward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateProcessingOutward(Entity.Billing.ProcessingOutward objProcessingOutward)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_PROCESSINGOUTWARD);
                db.AddInParameter(cmd, "@PK_ProcessingOutwardID", DbType.Int32, objProcessingOutward.ProcessingOutwardID);
                db.AddInParameter(cmd, "@ProcessingOutwardNo", DbType.String, objProcessingOutward.ProcessingOutwardNo);
                db.AddInParameter(cmd, "@ProcessingOutwardDate", DbType.String, objProcessingOutward.sProcessingOutwardDate);
                db.AddInParameter(cmd, "@FK_VendorID", DbType.Int32, objProcessingOutward.Vendor.VendorID);
                db.AddInParameter(cmd, "@FK_WorkID", DbType.Int32, objProcessingOutward.Work.WorkID);
                db.AddInParameter(cmd, "@TotalQuantity", DbType.Int32, objProcessingOutward.TotalQuantity);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objProcessingOutward.TotalAmount);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objProcessingOutward.ModifiedBy.UserID);


                foreach (Entity.Billing.ProcessingOutwardTrans ObjProcessingOutwardTrans in objProcessingOutward.ProcessingOutwardTrans)
                    ObjProcessingOutwardTrans.ProcessingOutwardID = objProcessingOutward.ProcessingOutwardID;

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                ProcessingOutwardTrans.SaveProcessingOutwardTransaction(objProcessingOutward.ProcessingOutwardTrans);
            }
            catch (Exception ex)
            {
                sException = "ProcessingOutward UpdateProcessingOutward| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteProcessingOutward(int iProcessingOutwardId, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_PROCESSINGOUTWARD);
                db.AddInParameter(cmd, "@PK_ProcessingOutwardID", DbType.Int32, iProcessingOutwardId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "ProcessingOutward DeleteProcessingOutward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        //public static DataSet GetProcessingOutwardSummary()
        //{
        //    string sException = string.Empty;
        //    Database db;
        //    DataSet dsList = null;

        //    try
        //    {
        //        db = Entity.DBConnection.dbCon;
        //        DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ProcessingOutwardSUMMARY);
        //        dsList = db.ExecuteDataSet(cmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "ProcessingOutward GetProcessingOutwardSummary | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return dsList;
        //}
    }

    public class ProcessingOutwardTrans
    {
        public static Collection<Entity.Billing.ProcessingOutwardTrans> GetProcessingOutwardTransByProcessingOutwardID(int iProcessingOutwardID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.ProcessingOutwardTrans> objList = new Collection<Entity.Billing.ProcessingOutwardTrans>();
            Entity.Billing.ProcessingOutwardTrans objProcessingOutwardTrans = new Entity.Billing.ProcessingOutwardTrans();
            Entity.Master.Product objProduct;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PROCESSINGOUTWARDTRANS);
                db.AddInParameter(cmd, "@FK_ProcessingOutwardID", DbType.Int32, iProcessingOutwardID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objProcessingOutwardTrans = new Entity.Billing.ProcessingOutwardTrans();
                        objProduct = new Entity.Master.Product();

                        objProcessingOutwardTrans.ProcessingOutwardTransID = Convert.ToInt32(drData["PK_ProcessingOutwardTransID"]);
                        objProcessingOutwardTrans.ProcessingOutwardID = Convert.ToInt32(drData["FK_ProcessingOutwardID"]);
                        objProcessingOutwardTrans.Quantity = Convert.ToInt32(drData["Quantity"]);
                        objProcessingOutwardTrans.Rate = Convert.ToDecimal(drData["Rate"]);
                        objProcessingOutwardTrans.SubTotal = Convert.ToDecimal(drData["SubTotal"]);
                        objProcessingOutwardTrans.ProcessingInwardTransID = Convert.ToInt32(drData["FK_ProcessingInwardTransID"]);
                        objProcessingOutwardTrans.ProcessingInwardNo = Convert.ToString(drData["ProcessingInwardNo"]);
                        objProcessingOutwardTrans.ProcessingInwardDate = Convert.ToDateTime(drData["ProcessingInwardDate"]);
                        objProcessingOutwardTrans.sProcessingInwardDate = objProcessingOutwardTrans.ProcessingInwardDate.ToString("dd/MM/yyyy");
                        
                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objProcessingOutwardTrans.Product = objProduct;

                        objList.Add(objProcessingOutwardTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "ProcessingOutwardTrans GetProcessingOutwardTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveProcessingOutwardTransaction(Collection<Entity.Billing.ProcessingOutwardTrans> ObjProcessingOutwardTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.ProcessingOutwardTrans ObjProcessingOutwardTransaction in ObjProcessingOutwardTransList)
            {
                if (ObjProcessingOutwardTransaction.StatusFlag == "I")
                    iID = AddProcessingOutwardTrans(ObjProcessingOutwardTransaction);
                else if (ObjProcessingOutwardTransaction.StatusFlag == "U")
                    bResult = UpdateProcessingOutwardTrans(ObjProcessingOutwardTransaction);
                else if (ObjProcessingOutwardTransaction.StatusFlag == "D")
                    bResult = DeleteProcessingOutwardTrans(ObjProcessingOutwardTransaction.ProcessingOutwardTransID);
            }
        }
        public static int AddProcessingOutwardTrans(Entity.Billing.ProcessingOutwardTrans objProcessingOutwardTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_PROCESSINGOUTWARDTRANS);
                db.AddOutParameter(cmd, "@PK_ProcessingOutwardTransID", DbType.Int32, objProcessingOutwardTrans.ProcessingOutwardTransID);
                db.AddInParameter(cmd, "@FK_ProcessingOutwardID", DbType.Int32, objProcessingOutwardTrans.ProcessingOutwardID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objProcessingOutwardTrans.Product.ProductID);
                db.AddInParameter(cmd, "@Quantity", DbType.Int32, objProcessingOutwardTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objProcessingOutwardTrans.Rate);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objProcessingOutwardTrans.SubTotal);
                db.AddInParameter(cmd, "@FK_ProcessingInwardTransID", DbType.Int32, objProcessingOutwardTrans.ProcessingInwardTransID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_ProcessingOutwardTransID"));
            }
            catch (Exception ex)
            {
                sException = "ProcessingOutwardTrans AddProcessingOutwardTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateProcessingOutwardTrans(Entity.Billing.ProcessingOutwardTrans objProcessingOutwardTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_PROCESSINGOUTWARDTRANS);
                db.AddInParameter(cmd, "@PK_ProcessingOutwardTransID", DbType.Int32, objProcessingOutwardTrans.ProcessingOutwardTransID);
                db.AddInParameter(cmd, "@FK_ProcessingOutwardID", DbType.Int32, objProcessingOutwardTrans.ProcessingOutwardID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objProcessingOutwardTrans.Product.ProductID);
                db.AddInParameter(cmd, "@Quantity", DbType.Int32, objProcessingOutwardTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objProcessingOutwardTrans.Rate);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objProcessingOutwardTrans.SubTotal);
                db.AddInParameter(cmd, "@FK_ProcessingInwardTransID", DbType.Int32, objProcessingOutwardTrans.ProcessingInwardTransID);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "ProcessingOutwardTrans UpdateProcessingOutwardTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteProcessingOutwardTrans(int iProcessingOutwardTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_PROCESSINGOUTWARDTRANS);
                db.AddInParameter(cmd, "@PK_ProcessingOutwardTransID", DbType.Int32, iProcessingOutwardTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "ProcessingOutwardTrans DeleteProcessingOutwardTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

    }
}
