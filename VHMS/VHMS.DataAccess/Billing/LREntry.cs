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
    public class LREntry
    {
        public static Collection<Entity.Billing.LREntry> GetLREntry(int ipatientID = 0, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.LREntry> objList = new Collection<Entity.Billing.LREntry>();
            Entity.Billing.LREntry objLREntry; Entity.Customer objCustomer;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_LRENTRY);
                db.AddInParameter(cmd, "@PK_LREntryID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objLREntry = new Entity.Billing.LREntry();
                        objCustomer= new Entity.Customer();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();

                        objLREntry.LREntryID = Convert.ToInt32(drData["PK_LREntryID"]);
                        objLREntry.LREntryNo = Convert.ToString(drData["LREntryNo"]);
                        objLREntry.LREntryDate = Convert.ToDateTime(drData["LREntryDate"]);
                        objLREntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objLREntry.VehicleType = Convert.ToString(drData["VehicleType"]);
                        objLREntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objLREntry.Description = Convert.ToString(drData["Description"]);
                        objLREntry.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objLREntry.DocumentPath1 = Convert.ToString(drData["DocumentPath1"]);

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objLREntry.Customer = objCustomer;

                        objShippingAddress.Address= Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranchName"]);
                        objLREntry.ShippingAddress = objShippingAddress;

                        objTransport.Address = Convert.ToString(drData["TransportAddress"]);
                        objTransport.Area = Convert.ToString(drData["TransportArea"]);
                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objLREntry.Transport = objTransport;

                        objLREntry.AWB_Date = Convert.ToDateTime(drData["AWB_Date"]);
                        objLREntry.sAWB_Date = objLREntry.AWB_Date.ToString("dd/MM/yyyy");

                        objLREntry.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objLREntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objLREntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objLREntry.Status = Convert.ToString(drData["Status"]);
                        objLREntry.Payment = Convert.ToString(drData["Payment"]);
                        objLREntry.TransportBranch = Convert.ToString(drData["TransportBranch"]);
                        objLREntry.FrieghtCharges = Convert.ToDecimal(drData["FrieghtCharges"]);
                        objLREntry.AWBNo = Convert.ToString(drData["AWBNo"]);
                        objLREntry.NoofBags = Convert.ToString(drData["No_of_Bundles"]);

                        objLREntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objLREntry.sLREntryDate = objLREntry.LREntryDate.ToString("dd/MM/yyyy") + " " + objLREntry.CreatedOn.ToString("h:mm");
                        objLREntry.DeliveryDate = Convert.ToDateTime(drData["Delivery_Date"]);
                        objLREntry.sDeliveryDate = objLREntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objLREntry.FollowedPerson = Convert.ToString(drData["Followed_Person"]);


                        objList.Add(objLREntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "LREntry GetLREntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
               
        public static Collection<Entity.Billing.LREntry> SearchLREntry(string ID, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.LREntry> objList = new Collection<Entity.Billing.LREntry>();
            Entity.Billing.LREntry objLREntry; Entity.Customer objCustomer;
            Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport; 

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_LRENTRYDYNAMIC);
                db.AddInParameter(cmd, "@PK_LREntryID", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objLREntry = new Entity.Billing.LREntry();
                        objCustomer = new Entity.Customer();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();

                        objLREntry.LREntryID = Convert.ToInt32(drData["PK_LREntryID"]);
                        objLREntry.LREntryNo = Convert.ToString(drData["LREntryNo"]);
                        objLREntry.LREntryDate = Convert.ToDateTime(drData["LREntryDate"]);
                        objLREntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objLREntry.VehicleType = Convert.ToString(drData["VehicleType"]);
                        objLREntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objLREntry.Description = Convert.ToString(drData["Description"]);
                        objLREntry.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objLREntry.DocumentPath1 = Convert.ToString(drData["DocumentPath1"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objLREntry.Customer = objCustomer;

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranchName"]);
                        objLREntry.ShippingAddress = objShippingAddress;

                        objTransport.Address = Convert.ToString(drData["TransportAddress"]);
                        objTransport.Area = Convert.ToString(drData["TransportArea"]);
                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objLREntry.Transport = objTransport;

                        objLREntry.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objLREntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objLREntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objLREntry.Status = Convert.ToString(drData["Status"]);
                        objLREntry.Payment = Convert.ToString(drData["Payment"]);
                        objLREntry.TransportBranch = Convert.ToString(drData["TransportBranch"]);
                        objLREntry.FrieghtCharges = Convert.ToDecimal(drData["FrieghtCharges"]);
                        objLREntry.AWBNo = Convert.ToString(drData["AWBNo"]);
                        objLREntry.NoofBags = Convert.ToString(drData["No_of_Bundles"]);

                        objLREntry.AWB_Date = Convert.ToDateTime(drData["AWB_Date"]);
                        objLREntry.sAWB_Date = objLREntry.AWB_Date.ToString("dd/MM/yyyy");

                        objLREntry.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objLREntry.sLREntryDate = objLREntry.LREntryDate.ToString("dd/MM/yyyy") + " " + objLREntry.CreatedOn.ToString("h:mm");
                        objLREntry.DeliveryDate = Convert.ToDateTime(drData["Delivery_Date"]);
                        objLREntry.sDeliveryDate = objLREntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objLREntry.FollowedPerson = Convert.ToString(drData["Followed_Person"]);

                        objList.Add(objLREntry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "LREntry GetLREntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
                
        public static Entity.Billing.LREntry GetLREntryByID(int iLREntryID, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.LREntry objLREntry = new Entity.Billing.LREntry();
            Entity.Customer objCustomer; Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_LRENTRY);
                db.AddInParameter(cmd, "@PK_LREntryID", DbType.Int32, iLREntryID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objLREntry = new Entity.Billing.LREntry();
                        objCustomer = new Entity.Customer();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();

                        objLREntry.LREntryID = Convert.ToInt32(drData["PK_LREntryID"]);
                        objLREntry.LREntryNo = Convert.ToString(drData["LREntryNo"]);
                        objLREntry.LREntryDate = Convert.ToDateTime(drData["LREntryDate"]);
                        objLREntry.sLREntryDate = objLREntry.LREntryDate.ToString("dd/MM/yyyy");
                        objLREntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objLREntry.VehicleType = Convert.ToString(drData["VehicleType"]);
                        objLREntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objLREntry.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objLREntry.DocumentPath1 = Convert.ToString(drData["DocumentPath1"]);
                        objLREntry.Description = Convert.ToString(drData["Description"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.WhatsAppNo = Convert.ToString(drData["WhatsAppNo"]);
                        objCustomer.Email = Convert.ToString(drData["Email"]);
                        objLREntry.Customer = objCustomer;

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranchName"]);
                        objLREntry.ShippingAddress = objShippingAddress;

                        objTransport.Address = Convert.ToString(drData["TransportAddress"]);
                        objTransport.Area = Convert.ToString(drData["TransportArea"]);
                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objLREntry.Transport = objTransport;

                        objLREntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objLREntry.sInvoiceDate = objLREntry.InvoiceDate.ToString("dd/MM/yyyy");

                        objLREntry.AWB_Date = Convert.ToDateTime(drData["AWB_Date"]);
                        objLREntry.sAWB_Date = objLREntry.AWB_Date.ToString("dd/MM/yyyy");

                        objLREntry.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objLREntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objLREntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objLREntry.Status = Convert.ToString(drData["Status"]);
                        objLREntry.Payment = Convert.ToString(drData["Payment"]);
                        objLREntry.TransportBranch = Convert.ToString(drData["TransportBranch"]);
                        objLREntry.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objLREntry.FrieghtCharges = Convert.ToDecimal(drData["FrieghtCharges"]);
                        objLREntry.AWBNo = Convert.ToString(drData["AWBNo"]);
                        objLREntry.NoofBags = Convert.ToString(drData["No_of_Bundles"]);
                        objLREntry.TemplateMessage = Convert.ToString(drData["TemplateMessage"]);
                        objLREntry.DeliveryDate = Convert.ToDateTime(drData["Delivery_Date"]);
                        objLREntry.sDeliveryDate = objLREntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objLREntry.FollowedPerson = Convert.ToString(drData["Followed_Person"]);

                        objLREntry.LREntryTrans = LREntryTrans.GetLREntryTransByLREntryID(objLREntry.LREntryID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "LREntry GetLREntryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objLREntry;
        }

        public static Entity.Billing.LREntry GetLREntrySalesID(int iID, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.LREntry objLREntry = new Entity.Billing.LREntry();
            Entity.Customer objCustomer; Entity.ShippingAddress objShippingAddress; Entity.Transport objTransport;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_GETLRENTRYSALESID);
                db.AddInParameter(cmd, "@FK_SalesEntryID", DbType.Int32, iID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objLREntry = new Entity.Billing.LREntry();
                        objCustomer = new Entity.Customer();
                        objShippingAddress = new Entity.ShippingAddress();
                        objTransport = new Entity.Transport();

                        objLREntry.LREntryID = Convert.ToInt32(drData["PK_LREntryID"]);
                        objLREntry.LREntryNo = Convert.ToString(drData["LREntryNo"]);
                        objLREntry.LREntryDate = Convert.ToDateTime(drData["LREntryDate"]);
                        objLREntry.sLREntryDate = objLREntry.LREntryDate.ToString("dd/MM/yyyy");
                        objLREntry.VehicleNo = Convert.ToString(drData["VehicleNo"]);
                        objLREntry.VehicleType = Convert.ToString(drData["VehicleType"]);
                        objLREntry.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objLREntry.DocumentPath = Convert.ToString(drData["DocumentPath"]);
                        objLREntry.DocumentPath1 = Convert.ToString(drData["DocumentPath1"]);
                        objLREntry.Description = Convert.ToString(drData["Description"]);
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.WhatsAppNo = Convert.ToString(drData["WhatsAppNo"]);
                        objCustomer.Email = Convert.ToString(drData["Email"]);
                        objLREntry.Customer = objCustomer;

                        objShippingAddress.Address = Convert.ToString(drData["ShippingAddress"]);
                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["FK_ShippingAddressID"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["ShippingBranchName"]);
                        objLREntry.ShippingAddress = objShippingAddress;

                        objTransport.Address = Convert.ToString(drData["TransportAddress"]);
                        objTransport.Area = Convert.ToString(drData["TransportArea"]);
                        objTransport.GSTNo = Convert.ToString(drData["TransportGSTIN"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objLREntry.Transport = objTransport;

                        objLREntry.InvoiceDate = Convert.ToDateTime(drData["InvoiceDate"]);
                        objLREntry.sInvoiceDate = objLREntry.InvoiceDate.ToString("dd/MM/yyyy");

                        objLREntry.AWB_Date = Convert.ToDateTime(drData["AWB_Date"]);
                        objLREntry.sAWB_Date = objLREntry.AWB_Date.ToString("dd/MM/yyyy");

                        objLREntry.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objLREntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objLREntry.EWayNo = Convert.ToString(drData["EWayNo"]);
                        objLREntry.Status = Convert.ToString(drData["Status"]);
                        objLREntry.Payment = Convert.ToString(drData["Payment"]);
                        objLREntry.TransportBranch = Convert.ToString(drData["TransportBranch"]);
                        objLREntry.FrieghtCharges = Convert.ToDecimal(drData["FrieghtCharges"]);
                        objLREntry.AWBNo = Convert.ToString(drData["AWBNo"]);
                        objLREntry.NoofBags = Convert.ToString(drData["No_of_Bundles"]);
                        objLREntry.DeliveryDate = Convert.ToDateTime(drData["Delivery_Date"]);
                        objLREntry.sDeliveryDate = objLREntry.DeliveryDate.ToString("dd/MM/yyyy");
                        objLREntry.FollowedPerson = Convert.ToString(drData["Followed_Person"]);

                        objLREntry.LREntryTrans = LREntryTrans.GetLREntryTransByLREntryID(objLREntry.LREntryID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "LREntry GetLREntryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objLREntry;
        }
        public static int AddLREntry(Entity.Billing.LREntry objLREntry)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_LRENTRY);
                db.AddOutParameter(cmd, "@PK_LREntryID", DbType.Int32, objLREntry.LREntryID);
                db.AddInParameter(cmd, "@LREntryNo", DbType.String, objLREntry.LREntryNo);
                db.AddInParameter(cmd, "@LREntryDate", DbType.String, objLREntry.sLREntryDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objLREntry.Customer.CustomerID);
                db.AddInParameter(cmd, "@VehicleNo", DbType.String, objLREntry.VehicleNo);
                db.AddInParameter(cmd, "@VehicleType", DbType.String, objLREntry.VehicleType);
                db.AddInParameter(cmd, "@DocumentPath", DbType.String, objLREntry.DocumentPath);
                db.AddInParameter(cmd, "@DocumentPath1", DbType.String, objLREntry.DocumentPath1);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objLREntry.NetAmount);
                db.AddInParameter(cmd, "@Description", DbType.String, objLREntry.Description);
                db.AddInParameter(cmd, "@FrieghtCharges", DbType.Decimal, objLREntry.FrieghtCharges);
                db.AddInParameter(cmd, "@TransportBranch", DbType.String, objLREntry.TransportBranch);
                db.AddInParameter(cmd, "@FK_ShippingAddressID", DbType.Int32, objLREntry.ShippingAddress.ShippingAddressID);
                db.AddInParameter(cmd, "@FK_TransportID", DbType.Int32, objLREntry.Transport.TransportID);
                db.AddInParameter(cmd, "@Status", DbType.String, objLREntry.Status);
                db.AddInParameter(cmd, "@Payment", DbType.String, objLREntry.Payment);
                db.AddInParameter(cmd, "@FK_SalesEntryID", DbType.Int32, objLREntry.SalesEntryID);
                db.AddInParameter(cmd, "@EWayNo", DbType.String, objLREntry.EWayNo);
                db.AddInParameter(cmd, "@AWBNo", DbType.String, objLREntry.AWBNo);
                db.AddInParameter(cmd, "@No_of_Bundles", DbType.String, objLREntry.NoofBags);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objLREntry.CreatedBy.UserID);
                db.AddInParameter(cmd, "@Delivery_Date", DbType.String, objLREntry.sDeliveryDate);
                db.AddInParameter(cmd, "@AWB_Date", DbType.String, objLREntry.sAWB_Date);
                db.AddInParameter(cmd, "@Followed_Person", DbType.String, objLREntry.FollowedPerson);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objLREntry.FinancialYear.FinancialYearID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_LREntryID"));

                foreach (Entity.Billing.LREntryTrans ObjLREntryTrans in objLREntry.LREntryTrans)
                    ObjLREntryTrans.LREntryID = iID;

                LREntryTrans.SaveLREntryTransaction(objLREntry.LREntryTrans);
            }
            catch (Exception ex)
            {
                sException = "LREntry AddLREntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateLREntry(Entity.Billing.LREntry objLREntry)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_LRENTRY);
                db.AddInParameter(cmd, "@PK_LREntryID", DbType.Int32, objLREntry.LREntryID);
                db.AddInParameter(cmd, "@LREntryNo", DbType.String, objLREntry.LREntryNo);
                db.AddInParameter(cmd, "@LREntryDate", DbType.String, objLREntry.sLREntryDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objLREntry.Customer.CustomerID);
                db.AddInParameter(cmd, "@VehicleNo", DbType.String, objLREntry.VehicleNo);
                db.AddInParameter(cmd, "@VehicleType", DbType.String, objLREntry.VehicleType);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objLREntry.NetAmount);
                db.AddInParameter(cmd, "@DocumentPath", DbType.String, objLREntry.DocumentPath);
                db.AddInParameter(cmd, "@DocumentPath1", DbType.String, objLREntry.DocumentPath1);
                db.AddInParameter(cmd, "@Description", DbType.String, objLREntry.Description);
                db.AddInParameter(cmd, "@FrieghtCharges", DbType.Decimal, objLREntry.FrieghtCharges);
                db.AddInParameter(cmd, "@TransportBranch", DbType.String, objLREntry.TransportBranch);
                db.AddInParameter(cmd, "@FK_ShippingAddressID", DbType.Int32, objLREntry.ShippingAddress.ShippingAddressID);
                db.AddInParameter(cmd, "@FK_TransportID", DbType.Int32, objLREntry.Transport.TransportID);
                db.AddInParameter(cmd, "@Status", DbType.String, objLREntry.Status);
                db.AddInParameter(cmd, "@Payment", DbType.String, objLREntry.Payment);
                db.AddInParameter(cmd, "@FK_SalesEntryID", DbType.Int32, objLREntry.SalesEntryID);
                db.AddInParameter(cmd, "@AWBNo", DbType.String, objLREntry.AWBNo);
                db.AddInParameter(cmd, "@No_of_Bundles", DbType.String, objLREntry.NoofBags);
                db.AddInParameter(cmd, "@EWayNo", DbType.String, objLREntry.EWayNo);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objLREntry.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@Delivery_Date", DbType.String, objLREntry.sDeliveryDate);
                db.AddInParameter(cmd, "@AWB_Date", DbType.String, objLREntry.sAWB_Date);
                db.AddInParameter(cmd, "@Followed_Person", DbType.String, objLREntry.FollowedPerson);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objLREntry.FinancialYear.FinancialYearID);

                foreach (Entity.Billing.LREntryTrans ObjLREntryTrans in objLREntry.LREntryTrans)
                    ObjLREntryTrans.LREntryID = objLREntry.LREntryID;

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                LREntryTrans.SaveLREntryTransaction(objLREntry.LREntryTrans);
            }
            catch (Exception ex)
            {
                sException = "LREntry UpdateLREntry| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool UpdateLREntryStatus(Entity.Billing.LREntry objLREntry)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_LRENTRYSTATUS);
                db.AddInParameter(cmd, "@Status", DbType.String, objLREntry.Status);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objLREntry.ModifiedBy.UserID);

                foreach (Entity.Billing.LREntryTrans ObjLREntryTrans in objLREntry.LREntryTrans)
                    ObjLREntryTrans.LREntryID = objLREntry.LREntryID;

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                LREntryTrans.SaveLREntryTransaction(objLREntry.LREntryTrans);
            }
            catch (Exception ex)
            {
                sException = "LREntry UpdateLREntry| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteLREntry(int iLREntryId, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_LRENTRY);
                db.AddInParameter(cmd, "@PK_LREntryID", DbType.Int32, iLREntryId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "LREntry DeleteLREntry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        
    }

    public class LREntryTrans
    {
        public static Collection<Entity.Billing.LREntryTrans> GetLREntryTransByLREntryID(int iLREntryID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.LREntryTrans> objList = new Collection<Entity.Billing.LREntryTrans>();
            Entity.Billing.LREntryTrans objLREntryTrans = new Entity.Billing.LREntryTrans();
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_LRENTRYTRANS);
                db.AddInParameter(cmd, "@FK_LREntryID", DbType.Int32, iLREntryID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objLREntryTrans = new Entity.Billing.LREntryTrans();

                        objLREntryTrans.LREntryTransID = Convert.ToInt32(drData["PK_LREntryTransID"]);
                        objLREntryTrans.LREntryID = Convert.ToInt32(drData["FK_LREntryID"]);

                        objLREntryTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);
                      
                        objLREntryTrans.Rate = Convert.ToDecimal(drData["Rate"]);
                        objLREntryTrans.SubTotal = Convert.ToDecimal(drData["SubTotal"]);
                        objLREntryTrans.ProductDescription = Convert.ToString(drData["ProductDescription"]);
                        objLREntryTrans.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);

                        objList.Add(objLREntryTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "LREntryTrans GetLREntryTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveLREntryTransaction(Collection<Entity.Billing.LREntryTrans> ObjLREntryTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.LREntryTrans ObjLREntryTransaction in ObjLREntryTransList)
            {
                if (ObjLREntryTransaction.StatusFlag == "I")
                    iID = AddLREntryTrans(ObjLREntryTransaction);
                else if (ObjLREntryTransaction.StatusFlag == "U")
                    bResult = UpdateLREntryTrans(ObjLREntryTransaction);
                else if (ObjLREntryTransaction.StatusFlag == "D")
                    bResult = DeleteLREntryTrans(ObjLREntryTransaction.LREntryTransID);
            }
        }
        public static int AddLREntryTrans(Entity.Billing.LREntryTrans objLREntryTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_LRENTRYTRANS);
                db.AddOutParameter(cmd, "@PK_LREntryTransID", DbType.Int32, objLREntryTrans.LREntryTransID);
                db.AddInParameter(cmd, "@FK_LREntryID", DbType.Int32, objLREntryTrans.LREntryID);
                db.AddInParameter(cmd, "@ProductDescription", DbType.String, objLREntryTrans.ProductDescription);
                db.AddInParameter(cmd, "@InvoiceNo", DbType.String, objLREntryTrans.InvoiceNo);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objLREntryTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objLREntryTrans.Rate);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objLREntryTrans.SubTotal);
               

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_LREntryTransID"));
            }
            catch (Exception ex)
            {
                sException = "LREntryTrans AddLREntryTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateLREntryTrans(Entity.Billing.LREntryTrans objLREntryTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_LRENTRYTRANS);
                db.AddInParameter(cmd, "@PK_LREntryTransID", DbType.Int32, objLREntryTrans.LREntryTransID);
                db.AddInParameter(cmd, "@FK_LREntryID", DbType.Int32, objLREntryTrans.LREntryID);
                db.AddInParameter(cmd, "@ProductDescription", DbType.String, objLREntryTrans.ProductDescription);
                db.AddInParameter(cmd, "@InvoiceNo", DbType.String, objLREntryTrans.InvoiceNo);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objLREntryTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objLREntryTrans.Rate);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objLREntryTrans.SubTotal);
              
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "LREntryTrans UpdateLREntryTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteLREntryTrans(int iLREntryTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_LRENTRYTRANS);
                db.AddInParameter(cmd, "@PK_LREntryTransID", DbType.Int32, iLREntryTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "LREntryTrans DeleteLREntryTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

    }
}
