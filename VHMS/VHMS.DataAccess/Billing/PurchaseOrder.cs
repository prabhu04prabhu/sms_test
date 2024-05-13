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
    public class PurchaseOrder
    {
        public static Collection<Entity.Billing.PurchaseOrder> GetPurchaseOrder(int ipatientID = 0, int iFinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.PurchaseOrder> objList = new Collection<Entity.Billing.PurchaseOrder>();
            Entity.Billing.PurchaseOrder objPurchaseOrder;
            Entity.Billing.Supplier objSupplier;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASEORDER);
                db.AddInParameter(cmd, "@PK_PurchaseOrderID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, iFinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();
                        objSupplier= new Entity.Billing.Supplier();

                        objPurchaseOrder.PurchaseOrderID = Convert.ToInt32(drData["PK_PurchaseOrderID"]);
                        objPurchaseOrder.PurchaseOrderNo = Convert.ToString(drData["PurchaseOrderNo"]);
                        objPurchaseOrder.PurchaseOrderDate = Convert.ToDateTime(drData["PurchaseOrderDate"]);
                        objPurchaseOrder.DeliveryDate = Convert.ToDateTime(drData["Delivery_Date"]);
                        objPurchaseOrder.sDeliveryDate = objPurchaseOrder.DeliveryDate.ToString("dd/MM/yyyy") + " " + objPurchaseOrder.CreatedOn.ToString("h:mm");
                        objPurchaseOrder.Status = Convert.ToString(drData["Status"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPurchaseOrder.Comments = Convert.ToString(drData["Comments"]);
                        objPurchaseOrder.Supplier = objSupplier;
                        objPurchaseOrder.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objPurchaseOrder.sPurchaseOrderDate = objPurchaseOrder.PurchaseOrderDate.ToString("dd/MM/yyyy") + " " + objPurchaseOrder.CreatedOn.ToString("h:mm");

                        objList.Add(objPurchaseOrder);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "PurchaseOrder GetPurchaseOrder | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
               
        public static Collection<Entity.Billing.PurchaseOrder> SearchPurchaseOrder(string ID, int iFinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.PurchaseOrder> objList = new Collection<Entity.Billing.PurchaseOrder>();
            Entity.Billing.PurchaseOrder objPurchaseOrder;
            Entity.patient objpatient;  Entity.Billing.Supplier objSupplier;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASEORDERDYNAMIC);
                db.AddInParameter(cmd, "@PK_PurchaseOrderID", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, iFinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();
                        objpatient = new Entity.patient();
                        objSupplier = new Entity.Billing.Supplier();

                        objPurchaseOrder.PurchaseOrderID = Convert.ToInt32(drData["PK_PurchaseOrderID"]);
                        objPurchaseOrder.PurchaseOrderNo = Convert.ToString(drData["PurchaseOrderNo"]);
                        objPurchaseOrder.PurchaseOrderDate = Convert.ToDateTime(drData["PurchaseOrderDate"]);
                        objPurchaseOrder.DeliveryDate = Convert.ToDateTime(drData["Delivery_Date"]);
                        objPurchaseOrder.sDeliveryDate = objPurchaseOrder.DeliveryDate.ToString("dd/MM/yyyy") + " " + objPurchaseOrder.CreatedOn.ToString("h:mm");
                        objPurchaseOrder.Status = Convert.ToString(drData["Status"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPurchaseOrder.Supplier = objSupplier;
                        objPurchaseOrder.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objPurchaseOrder.sPurchaseOrderDate = objPurchaseOrder.PurchaseOrderDate.ToString("dd/MM/yyyy") + " " + objPurchaseOrder.CreatedOn.ToString("h:mm");


                        objList.Add(objPurchaseOrder);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "PurchaseOrder GetPurchaseOrder | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
                
        public static Entity.Billing.PurchaseOrder GetPurchaseOrderByID(int iPurchaseOrderID, int iFinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.PurchaseOrder objPurchaseOrder = new Entity.Billing.PurchaseOrder();
             Entity.Billing.Supplier objSupplier;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASEORDER);
                db.AddInParameter(cmd, "@PK_PurchaseOrderID", DbType.Int32, iPurchaseOrderID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, iFinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchaseOrder = new Entity.Billing.PurchaseOrder();
                        objSupplier = new Entity.Billing.Supplier();

                        objPurchaseOrder.PurchaseOrderID = Convert.ToInt32(drData["PK_PurchaseOrderID"]);
                        objPurchaseOrder.PurchaseOrderNo = Convert.ToString(drData["PurchaseOrderNo"]);
                        objPurchaseOrder.PurchaseOrderDate = Convert.ToDateTime(drData["PurchaseOrderDate"]);
                        objPurchaseOrder.DeliveryDate = Convert.ToDateTime(drData["Delivery_Date"]);
                        objPurchaseOrder.sDeliveryDate = objPurchaseOrder.DeliveryDate.ToString("dd/MM/yyyy") ;
                        objPurchaseOrder.Status = Convert.ToString(drData["Status"]);
                        objPurchaseOrder.Comments = Convert.ToString(drData["Comments"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPurchaseOrder.Supplier = objSupplier;
                        objPurchaseOrder.sPurchaseOrderDate = objPurchaseOrder.PurchaseOrderDate.ToString("dd/MM/yyyy");


                        objPurchaseOrder.PurchaseOrderTrans = PurchaseOrderTrans.GetPurchaseOrderTransByPurchaseOrderID(objPurchaseOrder.PurchaseOrderID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "PurchaseOrder GetPurchaseOrderByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objPurchaseOrder;
        }
        public static int AddPurchaseOrder(Entity.Billing.PurchaseOrder objPurchaseOrder)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_PURCHASEORDER);
                db.AddOutParameter(cmd, "@PK_PurchaseOrderID", DbType.Int32, objPurchaseOrder.PurchaseOrderID);
                db.AddInParameter(cmd, "@PurchaseOrderNo", DbType.String, objPurchaseOrder.PurchaseOrderNo);
                db.AddInParameter(cmd, "@PurchaseOrderDate", DbType.String, objPurchaseOrder.sPurchaseOrderDate);
                db.AddInParameter(cmd, "@Delivery_Date", DbType.String, objPurchaseOrder.sDeliveryDate);
                db.AddInParameter(cmd, "@Comments", DbType.String, objPurchaseOrder.Comments);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, objPurchaseOrder.Supplier.SupplierID);
                db.AddInParameter(cmd, "@Status", DbType.Decimal, objPurchaseOrder.Status);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objPurchaseOrder.CreatedBy.UserID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objPurchaseOrder.FinancialYear.FinancialYearID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_PurchaseOrderID"));

                foreach (Entity.Billing.PurchaseOrderTrans ObjPurchaseOrderTrans in objPurchaseOrder.PurchaseOrderTrans)
                    ObjPurchaseOrderTrans.PurchaseOrderID = iID;

                PurchaseOrderTrans.SavePurchaseOrderTransaction(objPurchaseOrder.PurchaseOrderTrans);
            }
            catch (Exception ex)
            {
                sException = "PurchaseOrder AddPurchaseOrder | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdatePurchaseOrder(Entity.Billing.PurchaseOrder objPurchaseOrder)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_PURCHASEORDER);
                db.AddInParameter(cmd, "@PK_PurchaseOrderID", DbType.Int32, objPurchaseOrder.PurchaseOrderID);
                db.AddInParameter(cmd, "@PurchaseOrderNo", DbType.String, objPurchaseOrder.PurchaseOrderNo);
                db.AddInParameter(cmd, "@PurchaseOrderDate", DbType.String, objPurchaseOrder.sPurchaseOrderDate);
                db.AddInParameter(cmd, "@Delivery_Date", DbType.String, objPurchaseOrder.sDeliveryDate);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, objPurchaseOrder.Supplier.SupplierID);
                db.AddInParameter(cmd, "@Comments", DbType.String, objPurchaseOrder.Comments);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objPurchaseOrder.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objPurchaseOrder.FinancialYear.FinancialYearID);


                foreach (Entity.Billing.PurchaseOrderTrans ObjPurchaseOrderTrans in objPurchaseOrder.PurchaseOrderTrans)
                    ObjPurchaseOrderTrans.PurchaseOrderID = objPurchaseOrder.PurchaseOrderID;

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                PurchaseOrderTrans.SavePurchaseOrderTransaction(objPurchaseOrder.PurchaseOrderTrans);
            }
            catch (Exception ex)
            {
                sException = "PurchaseOrder UpdatePurchaseOrder| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeletePurchaseOrder(int iPurchaseOrderId, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_PURCHASEORDER);
                db.AddInParameter(cmd, "@PK_PurchaseOrderID", DbType.Int32, iPurchaseOrderId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "PurchaseOrder DeletePurchaseOrder | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        //public static DataSet GetPurchaseOrderSummary()
        //{
        //    string sException = string.Empty;
        //    Database db;
        //    DataSet dsList = null;

        //    try
        //    {
        //        db = Entity.DBConnection.dbCon;
        //        DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PurchaseOrderSUMMARY);
        //        dsList = db.ExecuteDataSet(cmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "PurchaseOrder GetPurchaseOrderSummary | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return dsList;
        //}
    }

    public class PurchaseOrderTrans
    {
        public static Collection<Entity.Billing.PurchaseOrderTrans> GetPurchaseOrderTransByPurchaseOrderID(int iPurchaseOrderID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.PurchaseOrderTrans> objList = new Collection<Entity.Billing.PurchaseOrderTrans>();
            Entity.Billing.PurchaseOrderTrans objPurchaseOrderTrans = new Entity.Billing.PurchaseOrderTrans();
            Entity.Master.Product objProduct;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASEORDERTRANS);
                db.AddInParameter(cmd, "@FK_PurchaseOrderID", DbType.Int32, iPurchaseOrderID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchaseOrderTrans = new Entity.Billing.PurchaseOrderTrans();
                        objProduct = new Entity.Master.Product();

                        objPurchaseOrderTrans.PurchaseOrderTransID = Convert.ToInt32(drData["PK_PurchaseOrderTransID"]);
                        objPurchaseOrderTrans.PurchaseOrderID = Convert.ToInt32(drData["FK_PurchaseOrderID"]);

                        objPurchaseOrderTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);

                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objPurchaseOrderTrans.Product = objProduct;

                        objList.Add(objPurchaseOrderTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "PurchaseOrderTrans GetPurchaseOrderTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SavePurchaseOrderTransaction(Collection<Entity.Billing.PurchaseOrderTrans> ObjPurchaseOrderTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.PurchaseOrderTrans ObjPurchaseOrderTransaction in ObjPurchaseOrderTransList)
            {
                if (ObjPurchaseOrderTransaction.StatusFlag == "I")
                    iID = AddPurchaseOrderTrans(ObjPurchaseOrderTransaction);
                else if (ObjPurchaseOrderTransaction.StatusFlag == "U")
                    bResult = UpdatePurchaseOrderTrans(ObjPurchaseOrderTransaction);
                else if (ObjPurchaseOrderTransaction.StatusFlag == "D")
                    bResult = DeletePurchaseOrderTrans(ObjPurchaseOrderTransaction.PurchaseOrderTransID);
            }
        }
        public static int AddPurchaseOrderTrans(Entity.Billing.PurchaseOrderTrans objPurchaseOrderTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_PURCHASEORDERTRANS);
                db.AddOutParameter(cmd, "@PK_PurchaseOrderTransID", DbType.Int32, objPurchaseOrderTrans.PurchaseOrderTransID);
                db.AddInParameter(cmd, "@FK_PurchaseOrderID", DbType.Int32, objPurchaseOrderTrans.PurchaseOrderID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objPurchaseOrderTrans.Product.ProductID);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objPurchaseOrderTrans.Quantity);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_PurchaseOrderTransID"));
            }
            catch (Exception ex)
            {
                sException = "PurchaseOrderTrans AddPurchaseOrderTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdatePurchaseOrderTrans(Entity.Billing.PurchaseOrderTrans objPurchaseOrderTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_PURCHASEORDERTRANS);
                db.AddInParameter(cmd, "@PK_PurchaseOrderTransID", DbType.Int32, objPurchaseOrderTrans.PurchaseOrderTransID);
                db.AddInParameter(cmd, "@FK_PurchaseOrderID", DbType.Int32, objPurchaseOrderTrans.PurchaseOrderID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objPurchaseOrderTrans.Product.ProductID);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objPurchaseOrderTrans.Quantity);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "PurchaseOrderTrans UpdatePurchaseOrderTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeletePurchaseOrderTrans(int iPurchaseOrderTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_PURCHASEORDERTRANS);
                db.AddInParameter(cmd, "@PK_PurchaseOrderTransID", DbType.Int32, iPurchaseOrderTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "PurchaseOrderTrans DeletePurchaseOrderTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

    }
}
