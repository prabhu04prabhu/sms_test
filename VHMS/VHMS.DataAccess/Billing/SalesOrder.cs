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
    public class SalesOrder
    {
        public static Collection<Entity.Billing.SalesOrder> GetSalesOrder(int ipatientID = 0,int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesOrder> objList = new Collection<Entity.Billing.SalesOrder>();
            Entity.Billing.SalesOrder objSalesOrder;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESORDER);
                db.AddInParameter(cmd, "@PK_SalesOrderID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["PK_SalesOrderID"]);
                        objSalesOrder.SalesOrderNo = Convert.ToString(drData["SalesOrderNo"]);
                        objSalesOrder.SalesOrderDate = Convert.ToDateTime(drData["SalesOrderDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objSalesOrder.Comments = Convert.ToString(drData["Comments"]);
                        objSalesOrder.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesOrder.Tax = objTax;

                        objSalesOrder.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objSalesOrder.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesOrder.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesOrder.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesOrder.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesOrder.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesOrder.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesOrder.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesOrder.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesOrder.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objSalesOrder.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objSalesOrder.sSalesOrderDate = objSalesOrder.SalesOrderDate.ToString("dd/MM/yyyy") + " " + objSalesOrder.CreatedOn.ToString("h:mm");

                        objList.Add(objSalesOrder);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesOrder GetSalesOrder | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
               
        public static Collection<Entity.Billing.SalesOrder> SearchSalesOrder(string ID, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesOrder> objList = new Collection<Entity.Billing.SalesOrder>();
            Entity.Billing.SalesOrder objSalesOrder;
            Entity.Customer objCustomer; Entity.Billing.Tax objTax; 

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESORDERDYNAMIC);
                db.AddInParameter(cmd, "@PK_SalesOrderID", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["PK_SalesOrderID"]);
                        objSalesOrder.SalesOrderNo = Convert.ToString(drData["SalesOrderNo"]);
                        objSalesOrder.SalesOrderDate = Convert.ToDateTime(drData["SalesOrderDate"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objSalesOrder.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesOrder.Tax = objTax;

                        objSalesOrder.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objSalesOrder.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesOrder.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesOrder.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesOrder.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesOrder.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesOrder.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesOrder.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesOrder.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesOrder.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objSalesOrder.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objSalesOrder.sSalesOrderDate = objSalesOrder.SalesOrderDate.ToString("dd/MM/yyyy") + " " + objSalesOrder.CreatedOn.ToString("h:mm");


                        objList.Add(objSalesOrder);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesOrder GetSalesOrder | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
                
        public static Entity.Billing.SalesOrder GetSalesOrderByID(int iSalesOrderID, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.SalesOrder objSalesOrder = new Entity.Billing.SalesOrder();
            Entity.Customer objCustomer; Entity.Billing.Tax objTax; 

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESORDER);
                db.AddInParameter(cmd, "@PK_SalesOrderID", DbType.Int32, iSalesOrderID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesOrder = new Entity.Billing.SalesOrder();
                        objCustomer = new Entity.Customer();
                        objTax = new Entity.Billing.Tax();

                        objSalesOrder.SalesOrderID = Convert.ToInt32(drData["PK_SalesOrderID"]);
                        objSalesOrder.SalesOrderNo = Convert.ToString(drData["SalesOrderNo"]);
                        objSalesOrder.SalesOrderDate = Convert.ToDateTime(drData["SalesOrderDate"]);
                        objSalesOrder.sSalesOrderDate = objSalesOrder.SalesOrderDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objSalesOrder.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objSalesOrder.Tax = objTax;

                        objSalesOrder.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objSalesOrder.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesOrder.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesOrder.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesOrder.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesOrder.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesOrder.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesOrder.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesOrder.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objSalesOrder.Comments = Convert.ToString(drData["Comments"]);
                        objSalesOrder.NetAmount = Convert.ToDecimal(drData["NetAmount"]);

                        objSalesOrder.SalesOrderTrans = SalesOrderTrans.GetSalesOrderTransBySalesOrderID(objSalesOrder.SalesOrderID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesOrder GetSalesOrderByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSalesOrder;
        }
        public static int AddSalesOrder(Entity.Billing.SalesOrder objSalesOrder)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_SALESORDER);
                db.AddOutParameter(cmd, "@PK_SalesOrderID", DbType.Int32, objSalesOrder.SalesOrderID);
                db.AddInParameter(cmd, "@SalesOrderNo", DbType.String, objSalesOrder.SalesOrderNo);
                db.AddInParameter(cmd, "@SalesOrderDate", DbType.String, objSalesOrder.sSalesOrderDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objSalesOrder.Customer.CustomerID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objSalesOrder.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objSalesOrder.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objSalesOrder.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objSalesOrder.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objSalesOrder.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objSalesOrder.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objSalesOrder.TotalAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objSalesOrder.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objSalesOrder.DiscountAmount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objSalesOrder.Roundoff);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objSalesOrder.NetAmount);
                db.AddInParameter(cmd, "@Comments", DbType.String, objSalesOrder.Comments);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objSalesOrder.CreatedBy.UserID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objSalesOrder.FinancialYear.FinancialYearID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_SalesOrderID"));

                foreach (Entity.Billing.SalesOrderTrans ObjSalesOrderTrans in objSalesOrder.SalesOrderTrans)
                    ObjSalesOrderTrans.SalesOrderID = iID;

                SalesOrderTrans.SaveSalesOrderTransaction(objSalesOrder.SalesOrderTrans);
            }
            catch (Exception ex)
            {
                sException = "SalesOrder AddSalesOrder | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateSalesOrder(Entity.Billing.SalesOrder objSalesOrder)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_SALESORDER);
                db.AddInParameter(cmd, "@PK_SalesOrderID", DbType.Int32, objSalesOrder.SalesOrderID);
                db.AddInParameter(cmd, "@SalesOrderNo", DbType.String, objSalesOrder.SalesOrderNo);
                db.AddInParameter(cmd, "@SalesOrderDate", DbType.String, objSalesOrder.sSalesOrderDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objSalesOrder.Customer.CustomerID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objSalesOrder.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objSalesOrder.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objSalesOrder.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objSalesOrder.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objSalesOrder.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objSalesOrder.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objSalesOrder.TotalAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objSalesOrder.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objSalesOrder.DiscountAmount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objSalesOrder.Roundoff);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objSalesOrder.NetAmount);
                db.AddInParameter(cmd, "@Comments", DbType.String, objSalesOrder.Comments);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objSalesOrder.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objSalesOrder.FinancialYear.FinancialYearID);


                foreach (Entity.Billing.SalesOrderTrans ObjSalesOrderTrans in objSalesOrder.SalesOrderTrans)
                    ObjSalesOrderTrans.SalesOrderID = objSalesOrder.SalesOrderID;

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                SalesOrderTrans.SaveSalesOrderTransaction(objSalesOrder.SalesOrderTrans);
            }
            catch (Exception ex)
            {
                sException = "SalesOrder UpdateSalesOrder| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteSalesOrder(int iSalesOrderId, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_SALESORDER);
                db.AddInParameter(cmd, "@PK_SalesOrderID", DbType.Int32, iSalesOrderId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "SalesOrder DeleteSalesOrder | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        //public static DataSet GetSalesOrderSummary()
        //{
        //    string sException = string.Empty;
        //    Database db;
        //    DataSet dsList = null;

        //    try
        //    {
        //        db = Entity.DBConnection.dbCon;
        //        DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SalesOrderSUMMARY);
        //        dsList = db.ExecuteDataSet(cmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "SalesOrder GetSalesOrderSummary | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return dsList;
        //}
    }

    public class SalesOrderTrans
    {
        public static Collection<Entity.Billing.SalesOrderTrans> GetSalesOrderTransBySalesOrderID(int iSalesOrderID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesOrderTrans> objList = new Collection<Entity.Billing.SalesOrderTrans>();
            Entity.Billing.SalesOrderTrans objSalesOrderTrans = new Entity.Billing.SalesOrderTrans();
            Entity.Master.Product objProduct;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESORDERTRANS);
                db.AddInParameter(cmd, "@FK_SalesOrderID", DbType.Int32, iSalesOrderID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesOrderTrans = new Entity.Billing.SalesOrderTrans();
                        objProduct = new Entity.Master.Product();

                        objSalesOrderTrans.SalesOrderTransID = Convert.ToInt32(drData["PK_SalesOrderTransID"]);
                        objSalesOrderTrans.SalesOrderID = Convert.ToInt32(drData["FK_SalesOrderID"]);

                        objSalesOrderTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objSalesOrderTrans.Rate = Convert.ToInt32(drData["Rate"]);
                        objSalesOrderTrans.SubTotal = Convert.ToDecimal(drData["SubTotal"]);

                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objSalesOrderTrans.Product = objProduct;

                        objList.Add(objSalesOrderTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesOrderTrans GetSalesOrderTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveSalesOrderTransaction(Collection<Entity.Billing.SalesOrderTrans> ObjSalesOrderTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.SalesOrderTrans ObjSalesOrderTransaction in ObjSalesOrderTransList)
            {
                if (ObjSalesOrderTransaction.StatusFlag == "I")
                    iID = AddSalesOrderTrans(ObjSalesOrderTransaction);
                else if (ObjSalesOrderTransaction.StatusFlag == "U")
                    bResult = UpdateSalesOrderTrans(ObjSalesOrderTransaction);
                else if (ObjSalesOrderTransaction.StatusFlag == "D")
                    bResult = DeleteSalesOrderTrans(ObjSalesOrderTransaction.SalesOrderTransID);
            }
        }
        public static int AddSalesOrderTrans(Entity.Billing.SalesOrderTrans objSalesOrderTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_SALESORDERTRANS);
                db.AddOutParameter(cmd, "@PK_SalesOrderTransID", DbType.Int32, objSalesOrderTrans.SalesOrderTransID);
                db.AddInParameter(cmd, "@FK_SalesOrderID", DbType.Int32, objSalesOrderTrans.SalesOrderID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objSalesOrderTrans.Product.ProductID);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objSalesOrderTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objSalesOrderTrans.Rate);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objSalesOrderTrans.SubTotal);
                db.AddInParameter(cmd, "@ProductName", DbType.String, objSalesOrderTrans.Product.ProductName);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_SalesOrderTransID"));
            }
            catch (Exception ex)
            {
                sException = "SalesOrderTrans AddSalesOrderTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateSalesOrderTrans(Entity.Billing.SalesOrderTrans objSalesOrderTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_SALESORDERTRANS);
                db.AddInParameter(cmd, "@PK_SalesOrderTransID", DbType.Int32, objSalesOrderTrans.SalesOrderTransID);
                db.AddInParameter(cmd, "@FK_SalesOrderID", DbType.Int32, objSalesOrderTrans.SalesOrderID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objSalesOrderTrans.Product.ProductID);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objSalesOrderTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objSalesOrderTrans.Rate);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objSalesOrderTrans.SubTotal);
                db.AddInParameter(cmd, "@ProductName", DbType.String, objSalesOrderTrans.Product.ProductName);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "SalesOrderTrans UpdateSalesOrderTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteSalesOrderTrans(int iSalesOrderTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_SALESORDERTRANS);
                db.AddInParameter(cmd, "@PK_SalesOrderTransID", DbType.Int32, iSalesOrderTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "SalesOrderTrans DeleteSalesOrderTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

    }
}
