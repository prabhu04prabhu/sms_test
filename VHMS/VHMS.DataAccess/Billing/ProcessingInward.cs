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
    public class ProcessingInward
    {
        public static Collection<Entity.Billing.ProcessingInward> GetProcessingInward(int ipatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.ProcessingInward> objList = new Collection<Entity.Billing.ProcessingInward>();
            Entity.Billing.ProcessingInward objProcessingInward;
            Entity.Billing.Vendor objVendor; Entity.Work objWork;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PROCESSINGINWARD);
                db.AddInParameter(cmd, "@PK_ProcessingInwardID", DbType.Int32, ipatientID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objProcessingInward = new Entity.Billing.ProcessingInward();
                        objVendor= new Entity.Billing.Vendor();
                        objWork = new Entity.Work();

                        objProcessingInward.ProcessingInwardID = Convert.ToInt32(drData["PK_ProcessingInwardID"]);
                        objProcessingInward.ProcessingInwardNo = Convert.ToString(drData["ProcessingInwardNo"]);
                        objProcessingInward.ProcessingInwardDate = Convert.ToDateTime(drData["ProcessingInwardDate"]);
                        objProcessingInward.TotalQuantity = Convert.ToInt32(drData["TotalQuantity"]);
                        objVendor.VendorID = Convert.ToInt32(drData["FK_VendorID"]);
                        objVendor.VendorName = Convert.ToString(drData["VendorName"]);
                        objProcessingInward.Vendor = objVendor;

                        objWork.WorkID = Convert.ToInt32(drData["FK_WorkID"]);
                        objWork.WorkName = Convert.ToString(drData["WorkName"]);
                        objWork.Amount = Convert.ToDecimal(drData["Amount"]);
                        objProcessingInward.Work = objWork;


                        objProcessingInward.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objProcessingInward.sProcessingInwardDate = objProcessingInward.ProcessingInwardDate.ToString("dd/MM/yyyy") + " " + objProcessingInward.CreatedOn.ToString("h:mm");

                        objList.Add(objProcessingInward);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "ProcessingInward GetProcessingInward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.PendingProcessingInward> GetPendingProcessingInward(int iWorkID = 0, int iVendorID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.PendingProcessingInward> objList = new Collection<Entity.Billing.PendingProcessingInward>();
            Entity.Billing.PendingProcessingInward objProcessingInward;
            Entity.Master.Product objProduct; 

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PENDINGPROCESSINGINWARD);
                db.AddInParameter(cmd, "@FK_WorkID", DbType.Int32, iWorkID);
                db.AddInParameter(cmd, "@FK_VendorID", DbType.Int32, iVendorID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objProcessingInward = new Entity.Billing.PendingProcessingInward();
                        objProduct = new Entity.Master.Product();

                        objProcessingInward.ProcessingInwardID = Convert.ToInt32(drData["FK_ProcessingInwardID"]);
                        objProcessingInward.ProcessingInwardNo = Convert.ToString(drData["ProcessingInwardNo"]);
                        objProcessingInward.ProcessingInwardDate = Convert.ToDateTime(drData["ProcessingInwardDate"]);
                        objProcessingInward.Quantity = 0;
                        objProcessingInward.Rate = Convert.ToDecimal(drData["Rate"]);
                        objProcessingInward.BalanceQuantity = Convert.ToInt32(drData["BalanceQuantity"]);
                        objProcessingInward.ProcessingInwardTransID = Convert.ToInt32(drData["PK_ProcessingInwardTransID"]);
                        objProcessingInward.SubTotal= 0;
                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objProcessingInward.Product = objProduct;
                        objProcessingInward.sProcessingInwardDate = objProcessingInward.ProcessingInwardDate.ToString("dd/MM/yyyy");

                        objList.Add(objProcessingInward);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "ProcessingInward GetProcessingInward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.ProcessingInward> SearchProcessingInward(string ID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.ProcessingInward> objList = new Collection<Entity.Billing.ProcessingInward>();
            Entity.Billing.ProcessingInward objProcessingInward;
            Entity.Billing.Vendor objVendor;
            Entity.Work objWork;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PROCESSINGINWARDDYNAMIC);
                db.AddInParameter(cmd, "@PK_ProcessingInwardID", DbType.String, ID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objProcessingInward = new Entity.Billing.ProcessingInward();
                        objVendor = new Entity.Billing.Vendor();
                        objWork = new Entity.Work();

                        objProcessingInward.ProcessingInwardID = Convert.ToInt32(drData["PK_ProcessingInwardID"]);
                        objProcessingInward.ProcessingInwardNo = Convert.ToString(drData["ProcessingInwardNo"]);
                        objProcessingInward.ProcessingInwardDate = Convert.ToDateTime(drData["ProcessingInwardDate"]);
                        objProcessingInward.TotalQuantity = Convert.ToInt32(drData["TotalQuantity"]);
                        objVendor.VendorID = Convert.ToInt32(drData["FK_VendorID"]);
                        objVendor.VendorName = Convert.ToString(drData["VendorName"]);
                        objProcessingInward.Vendor = objVendor;
                        objWork.WorkID = Convert.ToInt32(drData["FK_WorkID"]);
                        objWork.WorkName = Convert.ToString(drData["WorkName"]);
                        objWork.Amount = Convert.ToDecimal(drData["Amount"]);
                        objProcessingInward.Work = objWork;
                        objProcessingInward.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objProcessingInward.sProcessingInwardDate = objProcessingInward.ProcessingInwardDate.ToString("dd/MM/yyyy") + " " + objProcessingInward.CreatedOn.ToString("h:mm");


                        objList.Add(objProcessingInward);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "ProcessingInward GetProcessingInward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
                
        public static Entity.Billing.ProcessingInward GetProcessingInwardByID(int iProcessingInwardID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.ProcessingInward objProcessingInward = new Entity.Billing.ProcessingInward();
            Entity.Billing.Vendor objVendor;
            Entity.Work objWork;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PROCESSINGINWARD);
                db.AddInParameter(cmd, "@PK_ProcessingInwardID", DbType.Int32, iProcessingInwardID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objProcessingInward = new Entity.Billing.ProcessingInward();
                        objVendor = new Entity.Billing.Vendor();
                        objWork = new Entity.Work();

                        objProcessingInward.ProcessingInwardID = Convert.ToInt32(drData["PK_ProcessingInwardID"]);
                        objProcessingInward.ProcessingInwardNo = Convert.ToString(drData["ProcessingInwardNo"]);
                        objProcessingInward.ProcessingInwardDate = Convert.ToDateTime(drData["ProcessingInwardDate"]);
                        objProcessingInward.TotalQuantity = Convert.ToInt32(drData["TotalQuantity"]);
                        objVendor.VendorID = Convert.ToInt32(drData["FK_VendorID"]);
                        objVendor.VendorName = Convert.ToString(drData["VendorName"]);
                        objProcessingInward.Vendor = objVendor;
                        objWork.WorkID = Convert.ToInt32(drData["FK_WorkID"]);
                        objWork.WorkName = Convert.ToString(drData["WorkName"]);
                        objWork.Amount = Convert.ToDecimal(drData["Amount"]);
                        objProcessingInward.Work = objWork;
                        objProcessingInward.sProcessingInwardDate = objProcessingInward.ProcessingInwardDate.ToString("dd/MM/yyyy");


                        objProcessingInward.ProcessingInwardTrans = ProcessingInwardTrans.GetProcessingInwardTransByProcessingInwardID(objProcessingInward.ProcessingInwardID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "ProcessingInward GetProcessingInwardByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objProcessingInward;
        }
        public static int AddProcessingInward(Entity.Billing.ProcessingInward objProcessingInward)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_PROCESSINGINWARD);
                db.AddOutParameter(cmd, "@PK_ProcessingInwardID", DbType.Int32, objProcessingInward.ProcessingInwardID);
                db.AddInParameter(cmd, "@ProcessingInwardNo", DbType.String, objProcessingInward.ProcessingInwardNo);
                db.AddInParameter(cmd, "@ProcessingInwardDate", DbType.String, objProcessingInward.sProcessingInwardDate);
                db.AddInParameter(cmd, "@FK_VendorID", DbType.Int32, objProcessingInward.Vendor.VendorID);
                db.AddInParameter(cmd, "@FK_WorkID", DbType.Int32, objProcessingInward.Work.WorkID);
                db.AddInParameter(cmd, "@TotalQuantity", DbType.Int32, objProcessingInward.TotalQuantity);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objProcessingInward.CreatedBy.UserID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_ProcessingInwardID"));

                foreach (Entity.Billing.ProcessingInwardTrans ObjProcessingInwardTrans in objProcessingInward.ProcessingInwardTrans)
                    ObjProcessingInwardTrans.ProcessingInwardID = iID;

                ProcessingInwardTrans.SaveProcessingInwardTransaction(objProcessingInward.ProcessingInwardTrans);
            }
            catch (Exception ex)
            {
                sException = "ProcessingInward AddProcessingInward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateProcessingInward(Entity.Billing.ProcessingInward objProcessingInward)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_PROCESSINGINWARD);
                db.AddInParameter(cmd, "@PK_ProcessingInwardID", DbType.Int32, objProcessingInward.ProcessingInwardID);
                db.AddInParameter(cmd, "@ProcessingInwardNo", DbType.String, objProcessingInward.ProcessingInwardNo);
                db.AddInParameter(cmd, "@ProcessingInwardDate", DbType.String, objProcessingInward.sProcessingInwardDate);
                db.AddInParameter(cmd, "@FK_VendorID", DbType.Int32, objProcessingInward.Vendor.VendorID);
                db.AddInParameter(cmd, "@FK_WorkID", DbType.Int32, objProcessingInward.Work.WorkID);
                db.AddInParameter(cmd, "@TotalQuantity", DbType.Int32, objProcessingInward.TotalQuantity);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objProcessingInward.ModifiedBy.UserID);


                foreach (Entity.Billing.ProcessingInwardTrans ObjProcessingInwardTrans in objProcessingInward.ProcessingInwardTrans)
                    ObjProcessingInwardTrans.ProcessingInwardID = objProcessingInward.ProcessingInwardID;

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                ProcessingInwardTrans.SaveProcessingInwardTransaction(objProcessingInward.ProcessingInwardTrans);
            }
            catch (Exception ex)
            {
                sException = "ProcessingInward UpdateProcessingInward| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteProcessingInward(int iProcessingInwardId, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_PROCESSINGINWARD);
                db.AddInParameter(cmd, "@PK_ProcessingInwardID", DbType.Int32, iProcessingInwardId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "ProcessingInward DeleteProcessingInward | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        //public static DataSet GetProcessingInwardSummary()
        //{
        //    string sException = string.Empty;
        //    Database db;
        //    DataSet dsList = null;

        //    try
        //    {
        //        db = Entity.DBConnection.dbCon;
        //        DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ProcessingInwardSUMMARY);
        //        dsList = db.ExecuteDataSet(cmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "ProcessingInward GetProcessingInwardSummary | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return dsList;
        //}
    }

    public class ProcessingInwardTrans
    {
        public static Collection<Entity.Billing.ProcessingInwardTrans> GetProcessingInwardTransByProcessingInwardID(int iProcessingInwardID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.ProcessingInwardTrans> objList = new Collection<Entity.Billing.ProcessingInwardTrans>();
            Entity.Billing.ProcessingInwardTrans objProcessingInwardTrans = new Entity.Billing.ProcessingInwardTrans();
            Entity.Master.Product objProduct;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PROCESSINGINWARDTRANS);
                db.AddInParameter(cmd, "@FK_ProcessingInwardID", DbType.Int32, iProcessingInwardID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objProcessingInwardTrans = new Entity.Billing.ProcessingInwardTrans();
                        objProduct = new Entity.Master.Product();

                        objProcessingInwardTrans.ProcessingInwardTransID = Convert.ToInt32(drData["PK_ProcessingInwardTransID"]);
                        objProcessingInwardTrans.ProcessingInwardID = Convert.ToInt32(drData["FK_ProcessingInwardID"]);

                        objProcessingInwardTrans.Quantity = Convert.ToInt32(drData["Quantity"]);

                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objProcessingInwardTrans.Product = objProduct;

                        objList.Add(objProcessingInwardTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "ProcessingInwardTrans GetProcessingInwardTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveProcessingInwardTransaction(Collection<Entity.Billing.ProcessingInwardTrans> ObjProcessingInwardTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.ProcessingInwardTrans ObjProcessingInwardTransaction in ObjProcessingInwardTransList)
            {
                if (ObjProcessingInwardTransaction.StatusFlag == "I")
                    iID = AddProcessingInwardTrans(ObjProcessingInwardTransaction);
                else if (ObjProcessingInwardTransaction.StatusFlag == "U")
                    bResult = UpdateProcessingInwardTrans(ObjProcessingInwardTransaction);
                else if (ObjProcessingInwardTransaction.StatusFlag == "D")
                    bResult = DeleteProcessingInwardTrans(ObjProcessingInwardTransaction.ProcessingInwardTransID);
            }
        }
        public static int AddProcessingInwardTrans(Entity.Billing.ProcessingInwardTrans objProcessingInwardTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_PROCESSINGINWARDTRANS);
                db.AddOutParameter(cmd, "@PK_ProcessingInwardTransID", DbType.Int32, objProcessingInwardTrans.ProcessingInwardTransID);
                db.AddInParameter(cmd, "@FK_ProcessingInwardID", DbType.Int32, objProcessingInwardTrans.ProcessingInwardID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objProcessingInwardTrans.Product.ProductID);
                db.AddInParameter(cmd, "@Quantity", DbType.Int32, objProcessingInwardTrans.Quantity);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_ProcessingInwardTransID"));
            }
            catch (Exception ex)
            {
                sException = "ProcessingInwardTrans AddProcessingInwardTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateProcessingInwardTrans(Entity.Billing.ProcessingInwardTrans objProcessingInwardTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_PROCESSINGINWARDTRANS);
                db.AddInParameter(cmd, "@PK_ProcessingInwardTransID", DbType.Int32, objProcessingInwardTrans.ProcessingInwardTransID);
                db.AddInParameter(cmd, "@FK_ProcessingInwardID", DbType.Int32, objProcessingInwardTrans.ProcessingInwardID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objProcessingInwardTrans.Product.ProductID);
                db.AddInParameter(cmd, "@Quantity", DbType.Int32, objProcessingInwardTrans.Quantity);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "ProcessingInwardTrans UpdateProcessingInwardTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteProcessingInwardTrans(int iProcessingInwardTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_PROCESSINGINWARDTRANS);
                db.AddInParameter(cmd, "@PK_ProcessingInwardTransID", DbType.Int32, iProcessingInwardTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "ProcessingInwardTrans DeleteProcessingInwardTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

    }
}
