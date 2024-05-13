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
    public class INLREntry
    {
        public static Collection<Entity.Billing.INLREntry> GetINLREntry(int ipatientID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.INLREntry> objList = new Collection<Entity.Billing.INLREntry>();
            Entity.Billing.INLREntry objINLREntry;
            Entity.Billing.Supplier objSupplier;
            Entity.Transport objTransport;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_INLRENTRY);
                db.AddInParameter(cmd, "@PK_INLREntryID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objINLREntry = new Entity.Billing.INLREntry();
                        objSupplier = new Entity.Billing.Supplier();
                      
                        objTransport = new Entity.Transport();

                        objINLREntry.INLREntryID = Convert.ToInt32(drData["PK_INLREntryID"]);
                        objINLREntry.LREntryNo = Convert.ToString(drData["LREntryNo"]);
                        objINLREntry.LREntryDate = Convert.ToDateTime(drData["LREntryDate"]);
                        objINLREntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objINLREntry.VehicleType = Convert.ToString(drData["VehicleType"]);
                        objINLREntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objINLREntry.Description = Convert.ToString(drData["Description"]);
                        objINLREntry.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objINLREntry.DocumentPath1 = Convert.ToString(drData["DocumentPath1"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objINLREntry.Supplier = objSupplier;

                        objTransport.Address = Convert.ToString(drData["TransportAddress"]);
                        objTransport.Area = Convert.ToString(drData["TransportArea"]);
                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objINLREntry.Transport = objTransport;

                        objINLREntry.PurchaseID = Convert.ToInt32(drData["FK_PurchaseID"]);
                        objINLREntry.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objINLREntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objINLREntry.Status = Convert.ToString(drData["Status"]);
                        objINLREntry.Payment = Convert.ToString(drData["Payment"]);
                        objINLREntry.TransportBranch = Convert.ToString(drData["TransportBranch"]);
                        objINLREntry.FrieghtCharges = Convert.ToDecimal(drData["FrieghtCharges"]);
                        objINLREntry.AWBNo = Convert.ToString(drData["AWBNo"]);
                        objINLREntry.NoofBags = Convert.ToString(drData["No_of_Bundles"]);

                        objINLREntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objINLREntry.sLREntryDate = objINLREntry.LREntryDate.ToString("dd/MM/yyyy") + " " + objINLREntry.CreatedOn.ToString("h:mm");
                        objINLREntry.DeliveryDate = Convert.ToDateTime(drData["Delivery_Date"]);
                        objINLREntry.sDeliveryDate = objINLREntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objINLREntry.FollowedPerson = Convert.ToString(drData["Followed_Person"]);


                        objList.Add(objINLREntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "INLREntry GetINLREntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
               
        public static Collection<Entity.Billing.INLREntry> SearchINLREntry(string ID, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.INLREntry> objList = new Collection<Entity.Billing.INLREntry>();
            Entity.Billing.INLREntry objINLREntry;
            Entity.Billing.Supplier objSupplier;
            Entity.Transport objTransport; 

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_INLRENTRYDYNAMIC);
                db.AddInParameter(cmd, "@PK_INLREntryID", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objINLREntry = new Entity.Billing.INLREntry();
                        objSupplier = new Entity.Billing.Supplier();
                        objTransport = new Entity.Transport();

                        objINLREntry.INLREntryID = Convert.ToInt32(drData["PK_INLREntryID"]);
                        objINLREntry.LREntryNo = Convert.ToString(drData["LREntryNo"]);
                        objINLREntry.LREntryDate = Convert.ToDateTime(drData["LREntryDate"]);
                        objINLREntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objINLREntry.VehicleType = Convert.ToString(drData["VehicleType"]);
                        objINLREntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objINLREntry.Description = Convert.ToString(drData["Description"]);
                        objINLREntry.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objINLREntry.DocumentPath1 = Convert.ToString(drData["DocumentPath1"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objINLREntry.Supplier = objSupplier;

                        objTransport.Address = Convert.ToString(drData["TransportAddress"]);
                        objTransport.Area = Convert.ToString(drData["TransportArea"]);
                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objINLREntry.Transport = objTransport;

                        objINLREntry.PurchaseID = Convert.ToInt32(drData["FK_PurchaseID"]);
                        objINLREntry.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objINLREntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objINLREntry.Status = Convert.ToString(drData["Status"]);
                        objINLREntry.Payment = Convert.ToString(drData["Payment"]);
                        objINLREntry.TransportBranch = Convert.ToString(drData["TransportBranch"]);
                        objINLREntry.FrieghtCharges = Convert.ToDecimal(drData["FrieghtCharges"]);
                        objINLREntry.AWBNo = Convert.ToString(drData["AWBNo"]);
                        objINLREntry.NoofBags = Convert.ToString(drData["No_of_Bundles"]);

                        objINLREntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objINLREntry.sLREntryDate = objINLREntry.LREntryDate.ToString("dd/MM/yyyy") + " " + objINLREntry.CreatedOn.ToString("h:mm");
                        objINLREntry.DeliveryDate = Convert.ToDateTime(drData["Delivery_Date"]);
                        objINLREntry.sDeliveryDate = objINLREntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objINLREntry.FollowedPerson = Convert.ToString(drData["Followed_Person"]);

                        objList.Add(objINLREntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "INLREntry GetINLREntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
                
        public static Entity.Billing.INLREntry GetINLREntryByID(int iINLREntryID, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.INLREntry objINLREntry = new Entity.Billing.INLREntry();
            Entity.Billing.Supplier objSupplier;
            Entity.Transport objTransport;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_INLRENTRY);
                db.AddInParameter(cmd, "@PK_INLREntryID", DbType.Int32, iINLREntryID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objINLREntry = new Entity.Billing.INLREntry();
                        objSupplier = new Entity.Billing.Supplier();
                        objTransport = new Entity.Transport();

                        objINLREntry.INLREntryID = Convert.ToInt32(drData["PK_INLREntryID"]);
                        objINLREntry.LREntryNo = Convert.ToString(drData["LREntryNo"]);
                        objINLREntry.LREntryDate = Convert.ToDateTime(drData["LREntryDate"]);
                        objINLREntry.sLREntryDate = objINLREntry.LREntryDate.ToString("dd/MM/yyyy");
                        objINLREntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objINLREntry.VehicleType = Convert.ToString(drData["VehicleType"]);
                        objINLREntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objINLREntry.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objINLREntry.DocumentPath1 = Convert.ToString(drData["DocumentPath1"]);
                        objINLREntry.Description = Convert.ToString(drData["Description"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objSupplier.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSupplier.Email = Convert.ToString(drData["Email"]);
                        objINLREntry.Supplier  = objSupplier;
                        objTransport.Address = Convert.ToString(drData["TransportAddress"]);
                        objTransport.Area = Convert.ToString(drData["TransportArea"]);
                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objINLREntry.Transport = objTransport;
                        objINLREntry.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);
                        objINLREntry.sPurchaseDate = objINLREntry.PurchaseDate.ToString("dd/MM/yyyy");
                        objINLREntry.PurchaseID = Convert.ToInt32(drData["FK_PurchaseID"]);
                        objINLREntry.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objINLREntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objINLREntry.Status = Convert.ToString(drData["Status"]);
                        objINLREntry.Payment = Convert.ToString(drData["Payment"]);
                        objINLREntry.TransportBranch = Convert.ToString(drData["TransportBranch"]);
                        objINLREntry.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objINLREntry.FrieghtCharges = Convert.ToDecimal(drData["FrieghtCharges"]);
                        objINLREntry.AWBNo = Convert.ToString(drData["AWBNo"]);
                        objINLREntry.NoofBags = Convert.ToString(drData["No_of_Bundles"]);
                        objINLREntry.TemplateMessage = Convert.ToString(drData["TemplateMessage"]);
                        objINLREntry.DeliveryDate = Convert.ToDateTime(drData["Delivery_Date"]);
                        objINLREntry.sDeliveryDate = objINLREntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objINLREntry.FollowedPerson = Convert.ToString(drData["Followed_Person"]);

                        objINLREntry.INLREntryTrans = INLREntryTrans.GetINLREntryTransByINLREntryID(objINLREntry.INLREntryID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "INLREntry GetINLREntryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objINLREntry;
        }

        public static Entity.Billing.INLREntry GetINLREntrySalesID(int iID, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.INLREntry objINLREntry = new Entity.Billing.INLREntry();
            Entity.Billing.Supplier objSupplier;
            Entity.Transport objTransport;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_INGETLRENTRYSALESID);
                db.AddInParameter(cmd, "@FK_PurchaseID", DbType.Int32, iID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objINLREntry = new Entity.Billing.INLREntry();
                        objSupplier = new Entity.Billing.Supplier();
                        objTransport = new Entity.Transport();

                        objINLREntry.INLREntryID = Convert.ToInt32(drData["PK_INLREntryID"]);
                        objINLREntry.LREntryNo = Convert.ToString(drData["LREntryNo"]);
                        objINLREntry.LREntryDate = Convert.ToDateTime(drData["LREntryDate"]);
                        objINLREntry.sLREntryDate = objINLREntry.LREntryDate.ToString("dd/MM/yyyy");
                        objINLREntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objINLREntry.VehicleType = Convert.ToString(drData["VehicleType"]);
                        objINLREntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objINLREntry.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objINLREntry.DocumentPath1 = Convert.ToString(drData["DocumentPath1"]);
                        objINLREntry.Description = Convert.ToString(drData["Description"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objSupplier.PhoneNo1 = Convert.ToString(drData["WhatsAppNo"]);
                        objSupplier.Email = Convert.ToString(drData["Email"]);
                        objINLREntry.Supplier = objSupplier;

                      
                        objTransport.Address = Convert.ToString(drData["TransportAddress"]);
                        objTransport.Area = Convert.ToString(drData["TransportArea"]);
                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objINLREntry.Transport = objTransport;

                        objINLREntry.PurchaseDate = Convert.ToDateTime(drData["PurchaseDate"]);
                        objINLREntry.sPurchaseDate = objINLREntry.PurchaseDate.ToString("dd/MM/yyyy");

                        objINLREntry.PurchaseID = Convert.ToInt32(drData["FK_PurchaseID"]);
                        objINLREntry.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objINLREntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objINLREntry.Status = Convert.ToString(drData["Status"]);
                        objINLREntry.Payment = Convert.ToString(drData["Payment"]);
                        objINLREntry.TransportBranch = Convert.ToString(drData["TransportBranch"]);
                        objINLREntry.FrieghtCharges = Convert.ToDecimal(drData["FrieghtCharges"]);
                        objINLREntry.AWBNo = Convert.ToString(drData["AWBNo"]);
                        objINLREntry.NoofBags = Convert.ToString(drData["No_of_Bundles"]);
                        objINLREntry.DeliveryDate = Convert.ToDateTime(drData["Delivery_Date"]);
                        objINLREntry.sDeliveryDate = objINLREntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objINLREntry.FollowedPerson = Convert.ToString(drData["Followed_Person"]);

                        objINLREntry.INLREntryTrans = INLREntryTrans.GetINLREntryTransByINLREntryID(objINLREntry.INLREntryID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "INLREntry GetINLREntryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objINLREntry;
        }
        public static int AddINLREntry(Entity.Billing.INLREntry objINLREntry)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_INLRENTRY);
                db.AddOutParameter(cmd, "@PK_INLREntryID", DbType.Int32, objINLREntry.INLREntryID);
                db.AddInParameter(cmd, "@LREntryNo", DbType.String, objINLREntry.LREntryNo);
                db.AddInParameter(cmd, "@LREntryDate", DbType.String, objINLREntry.sLREntryDate);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, objINLREntry.Supplier.SupplierID);
                db.AddInParameter(cmd, "@VehicleNo", DbType.String, objINLREntry.VehicleNo);
                db.AddInParameter(cmd, "@VehicleType", DbType.String, objINLREntry.VehicleType);
                db.AddInParameter(cmd, "@DocumentPath", DbType.String, objINLREntry.DocumentPath);
                db.AddInParameter(cmd, "@DocumentPath1", DbType.String, objINLREntry.DocumentPath1);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objINLREntry.NetAmount);
                db.AddInParameter(cmd, "@Description", DbType.String, objINLREntry.Description);
                db.AddInParameter(cmd, "@FrieghtCharges", DbType.Decimal, objINLREntry.FrieghtCharges);
                db.AddInParameter(cmd, "@TransportBranch", DbType.String, objINLREntry.TransportBranch);
                db.AddInParameter(cmd, "@FK_TransportID", DbType.Int32, objINLREntry.Transport.TransportID);
                db.AddInParameter(cmd, "@Status", DbType.String, objINLREntry.Status);
                db.AddInParameter(cmd, "@Payment", DbType.String, objINLREntry.Payment);
                db.AddInParameter(cmd, "@FK_PurchaseID", DbType.Int32, objINLREntry.PurchaseID);
                db.AddInParameter(cmd, "@EWayNo", DbType.String, objINLREntry.EWayNo);
                db.AddInParameter(cmd, "@AWBNo", DbType.String, objINLREntry.AWBNo);
                db.AddInParameter(cmd, "@No_of_Bundles", DbType.String, objINLREntry.NoofBags);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objINLREntry.CreatedBy.UserID);
                db.AddInParameter(cmd, "@Delivery_Date", DbType.String, objINLREntry.sDeliveryDate);
                db.AddInParameter(cmd, "@Followed_Person", DbType.String, objINLREntry.FollowedPerson);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objINLREntry.FinancialYear.FinancialYearID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_INLREntryID"));

                foreach (Entity.Billing.INLREntryTrans ObjINLREntryTrans in objINLREntry.INLREntryTrans)
                    ObjINLREntryTrans.INLREntryID = iID;

                INLREntryTrans.SaveINLREntryTransaction(objINLREntry.INLREntryTrans);
            }
            catch (Exception ex)
            {
                sException = "INLREntry AddINLREntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateINLREntry(Entity.Billing.INLREntry objINLREntry)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_INLRENTRY);
                db.AddInParameter(cmd, "@PK_INLREntryID", DbType.Int32, objINLREntry.INLREntryID);
                db.AddInParameter(cmd, "@LREntryNo", DbType.String, objINLREntry.LREntryNo);
                db.AddInParameter(cmd, "@LREntryDate", DbType.String, objINLREntry.sLREntryDate);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, objINLREntry.Supplier.SupplierID);
                db.AddInParameter(cmd, "@VehicleNo", DbType.String, objINLREntry.VehicleNo);
                db.AddInParameter(cmd, "@VehicleType", DbType.String, objINLREntry.VehicleType);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objINLREntry.NetAmount);
                db.AddInParameter(cmd, "@DocumentPath", DbType.String, objINLREntry.DocumentPath);
                db.AddInParameter(cmd, "@DocumentPath1", DbType.String, objINLREntry.DocumentPath1);
                db.AddInParameter(cmd, "@Description", DbType.String, objINLREntry.Description);
                db.AddInParameter(cmd, "@FrieghtCharges", DbType.Decimal, objINLREntry.FrieghtCharges);
                db.AddInParameter(cmd, "@TransportBranch", DbType.String, objINLREntry.TransportBranch);
                db.AddInParameter(cmd, "@FK_TransportID", DbType.Int32, objINLREntry.Transport.TransportID);
                db.AddInParameter(cmd, "@Status", DbType.String, objINLREntry.Status);
                db.AddInParameter(cmd, "@Payment", DbType.String, objINLREntry.Payment);
                db.AddInParameter(cmd, "@FK_PurchaseID", DbType.Int32, objINLREntry.PurchaseID);
                db.AddInParameter(cmd, "@AWBNo", DbType.String, objINLREntry.AWBNo);
                db.AddInParameter(cmd, "@No_of_Bundles", DbType.String, objINLREntry.NoofBags);
                db.AddInParameter(cmd, "@EWayNo", DbType.String, objINLREntry.EWayNo);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objINLREntry.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@Delivery_Date", DbType.String, objINLREntry.sDeliveryDate);
                db.AddInParameter(cmd, "@Followed_Person", DbType.String, objINLREntry.FollowedPerson);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objINLREntry.FinancialYear.FinancialYearID);

                foreach (Entity.Billing.INLREntryTrans ObjINLREntryTrans in objINLREntry.INLREntryTrans)
                    ObjINLREntryTrans.INLREntryID = objINLREntry.INLREntryID;

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                INLREntryTrans.SaveINLREntryTransaction(objINLREntry.INLREntryTrans);
            }
            catch (Exception ex)
            {
                sException = "INLREntry UpdateINLREntry| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool UpdateINLREntryStatus(Entity.Billing.INLREntry objINLREntry)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_INLRENTRYSTATUS);
                db.AddInParameter(cmd, "@Status", DbType.String, objINLREntry.Status);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objINLREntry.ModifiedBy.UserID);

                foreach (Entity.Billing.INLREntryTrans ObjINLREntryTrans in objINLREntry.INLREntryTrans)
                    ObjINLREntryTrans.INLREntryID = objINLREntry.INLREntryID;

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                INLREntryTrans.SaveINLREntryTransaction(objINLREntry.INLREntryTrans);
            }
            catch (Exception ex)
            {
                sException = "INLREntry UpdateINLREntry| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteINLREntry(int iINLREntryId, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_INLRENTRY);
                db.AddInParameter(cmd, "@PK_INLREntryID", DbType.Int32, iINLREntryId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "INLREntry DeleteINLREntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        
    }

    public class INLREntryTrans
    {
        public static Collection<Entity.Billing.INLREntryTrans> GetINLREntryTransByINLREntryID(int iINLREntryID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.INLREntryTrans> objList = new Collection<Entity.Billing.INLREntryTrans>();
            Entity.Billing.INLREntryTrans objINLREntryTrans = new Entity.Billing.INLREntryTrans();
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_INLRENTRYTRANS);
                db.AddInParameter(cmd, "@FK_INLREntryID", DbType.Int32, iINLREntryID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objINLREntryTrans = new Entity.Billing.INLREntryTrans();

                        objINLREntryTrans.INLREntryTransID = Convert.ToInt32(drData["PK_INLREntryTransID"]);
                        objINLREntryTrans.INLREntryID = Convert.ToInt32(drData["FK_INLREntryID"]);

                        objINLREntryTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);
                      
                        objINLREntryTrans.Rate = Convert.ToDecimal(drData["Rate"]);
                        objINLREntryTrans.SubTotal = Convert.ToDecimal(drData["SubTotal"]);
                        objINLREntryTrans.ProductDescription = Convert.ToString(drData["ProductDescription"]);
                        objINLREntryTrans.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);

                        objList.Add(objINLREntryTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "INLREntryTrans GetINLREntryTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveINLREntryTransaction(Collection<Entity.Billing.INLREntryTrans> ObjINLREntryTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.INLREntryTrans ObjINLREntryTransaction in ObjINLREntryTransList)
            {
                if (ObjINLREntryTransaction.StatusFlag == "I")
                    iID = AddINLREntryTrans(ObjINLREntryTransaction);
                else if (ObjINLREntryTransaction.StatusFlag == "U")
                    bResult = UpdateINLREntryTrans(ObjINLREntryTransaction);
                else if (ObjINLREntryTransaction.StatusFlag == "D")
                    bResult = DeleteINLREntryTrans(ObjINLREntryTransaction.INLREntryTransID);
            }
        }
        public static int AddINLREntryTrans(Entity.Billing.INLREntryTrans objINLREntryTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_INLRENTRYTRANS);
                db.AddOutParameter(cmd, "@PK_INLREntryTransID", DbType.Int32, objINLREntryTrans.INLREntryTransID);
                db.AddInParameter(cmd, "@FK_INLREntryID", DbType.Int32, objINLREntryTrans.INLREntryID);
                db.AddInParameter(cmd, "@ProductDescription", DbType.String, objINLREntryTrans.ProductDescription);
                db.AddInParameter(cmd, "@InvoiceNo", DbType.String, objINLREntryTrans.InvoiceNo);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objINLREntryTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objINLREntryTrans.Rate);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objINLREntryTrans.SubTotal);
               

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_INLREntryTransID"));
            }
            catch (Exception ex)
            {
                sException = "INLREntryTrans AddINLREntryTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateINLREntryTrans(Entity.Billing.INLREntryTrans objINLREntryTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_INLRENTRYTRANS);
                db.AddInParameter(cmd, "@PK_INLREntryTransID", DbType.Int32, objINLREntryTrans.INLREntryTransID);
                db.AddInParameter(cmd, "@FK_INLREntryID", DbType.Int32, objINLREntryTrans.INLREntryID);
                db.AddInParameter(cmd, "@ProductDescription", DbType.String, objINLREntryTrans.ProductDescription);
                db.AddInParameter(cmd, "@InvoiceNo", DbType.String, objINLREntryTrans.InvoiceNo);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objINLREntryTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objINLREntryTrans.Rate);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objINLREntryTrans.SubTotal);
              
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "INLREntryTrans UpdateINLREntryTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteINLREntryTrans(int iINLREntryTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_INLRENTRYTRANS);
                db.AddInParameter(cmd, "@PK_INLREntryTransID", DbType.Int32, iINLREntryTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "INLREntryTrans DeleteINLREntryTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

    }
}
