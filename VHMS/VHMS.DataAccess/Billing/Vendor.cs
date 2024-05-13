using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections.ObjectModel;
using VHMS;
using VHMS.Entity.Billing;

namespace VHMS.DataAccess.Billing
{
    public class Vendor
    {
        public static Collection<Entity.Billing.Vendor> GetVendor()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Vendor> objList = new Collection<Entity.Billing.Vendor>();
            Entity.Billing.Vendor objVendor = new Entity.Billing.Vendor();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_VENDOR);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objVendor = new Entity.Billing.Vendor();

                        objVendor.VendorID = Convert.ToInt32(drData["PK_VendorID"]);
                        objVendor.VendorName = Convert.ToString(drData["VendorName"]);
                        objVendor.VendorCode = Convert.ToString(drData["VendorCode"]);
                        objVendor.VendorAddress = Convert.ToString(drData["VendorAddress"]);
                        objVendor.Comments = Convert.ToString(drData["Comments"]);
                        objVendor.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objVendor.PhoneNo2 = Convert.ToString(drData["PhoneNo2"]);
                        objVendor.PANNo = Convert.ToString(drData["PANNo"]);
                        objVendor.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objVendor);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Vendor GetVendor | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Vendor> GetActiveVendor()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Vendor> objList = new Collection<Entity.Billing.Vendor>();
            Entity.Billing.Vendor objVendor = new Entity.Billing.Vendor();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_VENDOR);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        if (Convert.ToBoolean(drData["IsActive"]))
                        {
                            objVendor = new Entity.Billing.Vendor();

                            objVendor.VendorID = Convert.ToInt32(drData["PK_VendorID"]);
                            objVendor.VendorName = Convert.ToString(drData["VendorName"]);
                            objVendor.VendorCode = Convert.ToString(drData["VendorCode"]);
                            objVendor.VendorAddress = Convert.ToString(drData["VendorAddress"]);

                            objVendor.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                            objVendor.PhoneNo2 = Convert.ToString(drData["PhoneNo2"]);
                            objVendor.PANNo = Convert.ToString(drData["PANNo"]);
                            objVendor.IsActive = Convert.ToBoolean(drData["IsActive"]);

                            objList.Add(objVendor);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Vendor GetVendor | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.Vendor GetVendorByID(int iVendorID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Vendor objVendor = new Entity.Billing.Vendor();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_VENDOR);
                db.AddInParameter(cmd, "@PK_VendorID", DbType.Int32, iVendorID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objVendor = new Entity.Billing.Vendor();

                        objVendor.VendorID = Convert.ToInt32(drData["PK_VendorID"]);
                        objVendor.VendorName = Convert.ToString(drData["VendorName"]);
                        objVendor.VendorCode = Convert.ToString(drData["VendorCode"]);
                        objVendor.VendorAddress = Convert.ToString(drData["VendorAddress"]);
                        objVendor.Comments = Convert.ToString(drData["Comments"]);
                        objVendor.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objVendor.PhoneNo2 = Convert.ToString(drData["PhoneNo2"]);
                        objVendor.PANNo = Convert.ToString(drData["PANNo"]);
                        objVendor.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objVendor.VendorWorkTrans = VendorWorkTrans.GetVendorWorkID(objVendor.VendorID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Vendor GetVendorByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objVendor;
        }
        public static int AddVendor(Entity.Billing.Vendor objVendor)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddVendor(oDb, objVendor, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tVendor", "PK_VendorID", objVendor.VendorID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objVendor.CreatedBy.UserID);
                }
                catch (Exception ex)
                {
                    oTrans.Rollback();
                    throw ex;
                }
                finally
                {
                    oDbCon.Close();
                }
            }
            return ID;
        }
        private static int AddVendor(Database oDb, Entity.Billing.Vendor objVendor, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_VENDOR);
                oDb.AddOutParameter(cmd, "@PK_VendorID", DbType.Int32, objVendor.VendorID);
                oDb.AddInParameter(cmd, "@VendorName", DbType.String, objVendor.VendorName);
                oDb.AddInParameter(cmd, "@VendorCode", DbType.String, objVendor.VendorCode);
                oDb.AddInParameter(cmd, "@VendorAddress", DbType.String, objVendor.VendorAddress);
                oDb.AddInParameter(cmd, "@PhoneNo1", DbType.String, objVendor.PhoneNo1);
                oDb.AddInParameter(cmd, "@PhoneNo2", DbType.String, objVendor.PhoneNo2);
                oDb.AddInParameter(cmd, "@PANNo", DbType.String, objVendor.PANNo);
                oDb.AddInParameter(cmd, "@Comments", DbType.String, objVendor.Comments);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objVendor.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objVendor.CreatedBy.UserID);
                iID = oDb.ExecuteNonQuery(cmd, oTrans);

                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_VendorID"));
                    objVendor.VendorID = iID;
                }
                foreach (Entity.Billing.VendorWorkTrans ObjVendorTrans in objVendor.VendorWorkTrans)
                    ObjVendorTrans.VendorID = iID;

                VendorWorkTrans.SaveVendorTransaction(objVendor.VendorWorkTrans); 
            }
            catch (Exception ex)
            {
                sException = "Vendor AddVendor | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateVendor(Entity.Billing.Vendor objVendor)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateVendor(oDb, objVendor, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tVendor", "PK_VendorID", objVendor.VendorID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objVendor.ModifiedBy.UserID);
                }
                catch (Exception ex)
                {
                    oTrans.Rollback();
                    throw ex;
                }
                finally
                {
                    oDbCon.Close();
                }
            }
            return IsUpdated;
        }
        private static bool UpdateVendor(Database oDb, Entity.Billing.Vendor objVendor, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_VENDOR);
                oDb.AddInParameter(cmd, "@PK_VendorID", DbType.Int32, objVendor.VendorID);
                oDb.AddInParameter(cmd, "@VendorName", DbType.String, objVendor.VendorName);
                oDb.AddInParameter(cmd, "@VendorCode", DbType.String, objVendor.VendorCode);
                oDb.AddInParameter(cmd, "@VendorAddress", DbType.String, objVendor.VendorAddress);
                oDb.AddInParameter(cmd, "@PhoneNo1", DbType.String, objVendor.PhoneNo1);
                oDb.AddInParameter(cmd, "@PhoneNo2", DbType.String, objVendor.PhoneNo2);
                oDb.AddInParameter(cmd, "@PANNo", DbType.String, objVendor.PANNo);
                oDb.AddInParameter(cmd, "@Comments", DbType.String, objVendor.Comments);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objVendor.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objVendor.ModifiedBy.UserID);

                foreach (Entity.Billing.VendorWorkTrans ObjVendorTrans in objVendor.VendorWorkTrans)
                    ObjVendorTrans.VendorID = objVendor.VendorID;

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0) bResult = true;

                VendorWorkTrans.SaveVendorTransaction(objVendor.VendorWorkTrans);
            }
            catch (Exception ex)
            {
                sException = "Vendor UpdateVendor| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteVendor(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteVendor(oDb, ID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tVendor", "PK_VendorID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
                }
                catch (Exception ex)
                {
                    oTrans.Rollback();
                    throw ex;
                }
                finally
                {
                    oDbCon.Close();
                }
            }
            return IsDeleted;
        }
        private static bool DeleteVendor(Database oDb, int ID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_VENDOR);
                oDb.AddInParameter(cmd, "@PK_VendorID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Vendor DeleteVendor | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

        public class VendorWorkTrans
        {
            public static Collection<Entity.Billing.VendorWorkTrans> GetVendorWorkID(int iVendorEntryID = 0)
            {
                string sException = string.Empty;
                Database db;
                DataSet dsList = null;
                Collection<Entity.Billing.VendorWorkTrans> objList = new Collection<Entity.Billing.VendorWorkTrans>();
                Entity.Billing.VendorWorkTrans objVendorTrans = new Entity.Billing.VendorWorkTrans();
                try
                {
                    db = Entity.DBConnection.dbCon;
                    DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_VENDORWORKTRANS);
                    db.AddInParameter(cmd, "@FK_VendorID", DbType.Int32, iVendorEntryID);
                    dsList = db.ExecuteDataSet(cmd);

                    if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drData in dsList.Tables[0].Rows)
                        {
                            objVendorTrans = new Entity.Billing.VendorWorkTrans();

                            objVendorTrans.VendorWorkTransID = Convert.ToInt32(drData["PK_VendorWorkTransID"]);
                            objVendorTrans.VendorID = Convert.ToInt32(drData["FK_VendorID"]);
                            objVendorTrans.WorkID = Convert.ToInt32(drData["FK_WorkID"]);
                            objVendorTrans.Amount = Convert.ToDecimal(drData["Amount"]);
                            objVendorTrans.WorkName = Convert.ToString(drData["WorkName"]);

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
            public static void SaveVendorTransaction(Collection<Entity.Billing.VendorWorkTrans> ObjVendorTransList)
            {
                int iID = 0;
                bool bResult = false;

                foreach (Entity.Billing.VendorWorkTrans ObjVendorTransaction in ObjVendorTransList)
                {
                    if (ObjVendorTransaction.StatusFlag == "I")
                        iID = AddVendorTrans(ObjVendorTransaction);
                    else if (ObjVendorTransaction.StatusFlag == "U")
                        bResult = UpdateVendorTrans(ObjVendorTransaction);
                    else if (ObjVendorTransaction.StatusFlag == "D")
                        bResult = DeleteVendorTrans(ObjVendorTransaction.VendorWorkTransID);
                }
            }
            public static int AddVendorTrans(Entity.Billing.VendorWorkTrans objVendorTrans)
            {
                string sException = string.Empty;
                int iID = 0;
                Database db;
                try
                {
                    db = Entity.DBConnection.dbCon;
                    DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_VENDORWORKTRANS);
                    db.AddOutParameter(cmd, "@PK_VendorWorkTransID", DbType.Int32, objVendorTrans.VendorWorkTransID);
                    db.AddInParameter(cmd, "@FK_VendorID", DbType.Int32, objVendorTrans.VendorID);
                    db.AddInParameter(cmd, "@FK_WorkID", DbType.Int32, objVendorTrans.WorkID);
                    db.AddInParameter(cmd, "@Amount", DbType.Decimal, objVendorTrans.Amount);

                    iID = db.ExecuteNonQuery(cmd);
                    if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_VendorWorkTransID"));
                }
                catch (Exception ex)
                {
                    sException = "VendorTrans AddVendorTrans | " + ex.ToString();
                    Log.Write(sException);
                    throw;
                }
                return iID;
            }
            public static bool UpdateVendorTrans(Entity.Billing.VendorWorkTrans objVendorTrans)
            {
                string sException = string.Empty;
                int iID = 0;
                bool bResult = false;
                Database db;
                try
                {
                    db = Entity.DBConnection.dbCon;
                    DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_VENDORWORKTRANS);
                    db.AddInParameter(cmd, "@PK_VendorWorkTransID", DbType.Int32, objVendorTrans.VendorWorkTransID);
                    db.AddInParameter(cmd, "@FK_VendorID", DbType.Int32, objVendorTrans.VendorID);
                    db.AddInParameter(cmd, "@FK_WorkID", DbType.Int32, objVendorTrans.WorkID);
                    db.AddInParameter(cmd, "@Amount", DbType.Decimal, objVendorTrans.Amount);
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
                    DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_VENDORWORKTRANS);
                    db.AddInParameter(cmd, "@PK_VendorWorkTransID", DbType.Int32, iVendorTransID);
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
    }
}
