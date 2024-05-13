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
    public class Supplier
    {
        public static Collection<Entity.Billing.Supplier> GetSupplier()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Supplier> objList = new Collection<Entity.Billing.Supplier>();
            Entity.Billing.Supplier objSupplier = new Entity.Billing.Supplier();
            Entity.State objState = new Entity.State();
            Entity.Transport objTransport = new Entity.Transport();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SUPPLIER);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSupplier = new Entity.Billing.Supplier();

                        objSupplier.SupplierID = Convert.ToInt32(drData["PK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objSupplier.SupplierCode = Convert.ToString(drData["SupplierCode"]);
                        objSupplier.SupplierAddress = Convert.ToString(drData["SupplierAddress"]);

                        objState = new Entity.State();
                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objState.StateName = Convert.ToString(drData["StateName"]);
                        objSupplier.State = objState;

                        objTransport = new Entity.Transport();
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objSupplier.Transport = objTransport;

                        objSupplier.City = Convert.ToString(drData["City"]);
                        objSupplier.Days = Convert.ToInt32(drData["Days"]);
                        objSupplier.Taluk = Convert.ToString(drData["Taluk"]);
                        objSupplier.Pincode = Convert.ToString(drData["Pincode"]);
                        objSupplier.Area = Convert.ToString(drData["Area"]);
                        objSupplier.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSupplier.PhoneNo2 = Convert.ToString(drData["PhoneNo2"]);
                        objSupplier.LandLine = Convert.ToString(drData["Landline"]);
                        objSupplier.Fax = Convert.ToString(drData["Fax"]);
                        objSupplier.Email = Convert.ToString(drData["Email"]);
                        objSupplier.WebSite = Convert.ToString(drData["Website"]);
                        objSupplier.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSupplier.LinkClick = Convert.ToBoolean(drData["LinkClick"]);
                        objSupplier.AccountNo = Convert.ToString(drData["AccountNo"]);
                        objSupplier.BankName = Convert.ToString(drData["BankName"]);
                        objSupplier.BranchName = Convert.ToString(drData["BranchName"]);
                        objSupplier.AccountHolderName = Convert.ToString(drData["AccountHolderName"]);
                        objSupplier.IFSCCode = Convert.ToString(drData["IFSCCode"]);
                        objSupplier.ImagePath = Convert.ToString(drData["ImagePath"]);

                        objList.Add(objSupplier);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Supplier GetSupplier | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Supplier> GetAllSupplier(int iSupplierID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Supplier> objList = new Collection<Entity.Billing.Supplier>();
            Entity.Billing.Supplier objSupplier = new Entity.Billing.Supplier();
            Entity.State objState = new Entity.State();
            Entity.Transport objTransport = new Entity.Transport();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SUPPLIERALL);
                db.AddInParameter(cmd, "@PK_SupplierID", DbType.Int32, iSupplierID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSupplier = new Entity.Billing.Supplier();

                        objSupplier.SupplierID = Convert.ToInt32(drData["PK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objSupplier.SupplierCode = Convert.ToString(drData["SupplierCode"]);
                        objSupplier.SupplierAddress = Convert.ToString(drData["SupplierAddress"]);

                        objState = new Entity.State();
                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objState.StateName = Convert.ToString(drData["StateName"]);
                        objSupplier.State = objState;

                        objTransport = new Entity.Transport();
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objSupplier.Transport = objTransport;

                        objSupplier.City = Convert.ToString(drData["City"]);
                        objSupplier.Days = Convert.ToInt32(drData["Days"]);
                        objSupplier.Taluk = Convert.ToString(drData["Taluk"]);
                        objSupplier.Pincode = Convert.ToString(drData["Pincode"]);
                        objSupplier.Area = Convert.ToString(drData["Area"]);
                        objSupplier.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSupplier.PhoneNo2 = Convert.ToString(drData["PhoneNo2"]);
                        objSupplier.LandLine = Convert.ToString(drData["Landline"]);
                        objSupplier.Fax = Convert.ToString(drData["Fax"]);
                        objSupplier.Email = Convert.ToString(drData["Email"]);
                        objSupplier.WebSite = Convert.ToString(drData["Website"]);
                        objSupplier.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSupplier.LinkClick = Convert.ToBoolean(drData["LinkClick"]);
                        objSupplier.AccountNo = Convert.ToString(drData["AccountNo"]);
                        objSupplier.BankName = Convert.ToString(drData["BankName"]);
                        objSupplier.BranchName = Convert.ToString(drData["BranchName"]);
                        objSupplier.AccountHolderName = Convert.ToString(drData["AccountHolderName"]);
                        objSupplier.IFSCCode = Convert.ToString(drData["IFSCCode"]);
                        objSupplier.ImagePath = Convert.ToString(drData["ImagePath"]);

                        objList.Add(objSupplier);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Supplier GetSupplier | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Supplier> GetActiveSupplier()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Supplier> objList = new Collection<Entity.Billing.Supplier>();
            Entity.Billing.Supplier objSupplier = new Entity.Billing.Supplier();
            Entity.State objState = new Entity.State();
            Entity.Transport objTransport = new Entity.Transport();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SUPPLIER);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        if (Convert.ToBoolean(drData["IsActive"]))
                        {
                            objSupplier = new Entity.Billing.Supplier();

                            objSupplier.SupplierID = Convert.ToInt32(drData["PK_SupplierID"]);
                            objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                            objSupplier.SupplierCode = Convert.ToString(drData["SupplierCode"]);
                            objSupplier.SupplierAddress = Convert.ToString(drData["SupplierAddress"]);

                            objState = new Entity.State();
                            objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                            objState.StateName = Convert.ToString(drData["StateName"]);
                            objSupplier.State = objState;

                            objTransport = new Entity.Transport();
                            objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                            objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                            objSupplier.Transport = objTransport;

                            objSupplier.City = Convert.ToString(drData["City"]);
                            objSupplier.Days = Convert.ToInt32(drData["Days"]);
                            objSupplier.Taluk = Convert.ToString(drData["Taluk"]);
                            objSupplier.Pincode = Convert.ToString(drData["Pincode"]);
                            objSupplier.Area = Convert.ToString(drData["Area"]);
                            objSupplier.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                            objSupplier.PhoneNo2 = Convert.ToString(drData["PhoneNo2"]);
                            objSupplier.LandLine = Convert.ToString(drData["Landline"]);
                            objSupplier.Fax = Convert.ToString(drData["Fax"]);
                            objSupplier.Email = Convert.ToString(drData["Email"]);
                            objSupplier.WebSite = Convert.ToString(drData["Website"]);
                            objSupplier.IsActive = Convert.ToBoolean(drData["IsActive"]);
                            objSupplier.LinkClick = Convert.ToBoolean(drData["LinkClick"]);
                            objSupplier.AccountNo = Convert.ToString(drData["AccountNo"]);
                            objSupplier.BankName = Convert.ToString(drData["BankName"]);
                            objSupplier.BranchName = Convert.ToString(drData["BranchName"]);
                            objSupplier.AccountHolderName = Convert.ToString(drData["AccountHolderName"]);
                            objSupplier.IFSCCode = Convert.ToString(drData["IFSCCode"]);
                            objSupplier.ImagePath = Convert.ToString(drData["ImagePath"]);

                            objList.Add(objSupplier);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Supplier GetSupplier | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.Supplier GetSupplierByID(int iSupplierID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Supplier objSupplier = new Entity.Billing.Supplier();
            Entity.State objState = new Entity.State();
            Entity.Transport objTransport = new Entity.Transport();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SUPPLIER);
                db.AddInParameter(cmd, "@PK_SupplierID", DbType.Int32, iSupplierID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSupplier = new Entity.Billing.Supplier();

                        objSupplier.SupplierID = Convert.ToInt32(drData["PK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objSupplier.SupplierCode = Convert.ToString(drData["SupplierCode"]);
                        objSupplier.SupplierAddress = Convert.ToString(drData["SupplierAddress"]);

                        objState = new Entity.State();
                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objState.StateName = Convert.ToString(drData["StateName"]);
                        objState.StateCode = Convert.ToString(drData["StateCode"]);
                        objSupplier.State = objState;

                        objTransport = new Entity.Transport();
                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objSupplier.Transport = objTransport;

                        objSupplier.City = Convert.ToString(drData["City"]);
                        objSupplier.Days = Convert.ToInt32(drData["Days"]);
                        objSupplier.Taluk = Convert.ToString(drData["Taluk"]);
                        objSupplier.Pincode = Convert.ToString(drData["Pincode"]);
                        objSupplier.Area = Convert.ToString(drData["Area"]);
                        objSupplier.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objSupplier.PhoneNo2 = Convert.ToString(drData["PhoneNo2"]);
                        objSupplier.LandLine = Convert.ToString(drData["Landline"]);
                        objSupplier.Fax = Convert.ToString(drData["Fax"]);
                        objSupplier.Email = Convert.ToString(drData["Email"]);
                        objSupplier.WebSite = Convert.ToString(drData["Website"]);
                        objSupplier.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objSupplier.LinkClick = Convert.ToBoolean(drData["LinkClick"]);
                        objSupplier.AccountNo = Convert.ToString(drData["AccountNo"]);
                        objSupplier.BankName = Convert.ToString(drData["BankName"]);
                        objSupplier.BranchName = Convert.ToString(drData["BranchName"]);
                        objSupplier.AccountHolderName = Convert.ToString(drData["AccountHolderName"]);
                        objSupplier.IFSCCode = Convert.ToString(drData["IFSCCode"]);
                        objSupplier.ImagePath = Convert.ToString(drData["ImagePath"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Supplier GetSupplierByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSupplier;
        }
        public static int AddSupplier(Entity.Billing.Supplier objSupplier)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddSupplier(oDb, objSupplier, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tSupplier", "PK_SupplierID", objSupplier.SupplierID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objSupplier.CreatedBy.UserID);
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
        private static int AddSupplier(Database oDb, Entity.Billing.Supplier objSupplier, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_SUPPLIER);
                oDb.AddOutParameter(cmd, "@PK_SupplierID", DbType.Int32, objSupplier.SupplierID);
                oDb.AddInParameter(cmd, "@SupplierName", DbType.String, objSupplier.SupplierName);
                oDb.AddInParameter(cmd, "@SupplierCode", DbType.String, objSupplier.SupplierCode);
                oDb.AddInParameter(cmd, "@SupplierAddress", DbType.String, objSupplier.SupplierAddress);
                oDb.AddInParameter(cmd, "@FK_StateID", DbType.Int32, objSupplier.State.StateID);
                oDb.AddInParameter(cmd, "@FK_TransportID", DbType.Int32, objSupplier.Transport.TransportID);
                oDb.AddInParameter(cmd, "@City", DbType.String, objSupplier.City);
                oDb.AddInParameter(cmd, "@Days", DbType.Int32, objSupplier.Days);
                oDb.AddInParameter(cmd, "@Taluk", DbType.String, objSupplier.Taluk);
                oDb.AddInParameter(cmd, "@Pincode", DbType.String, objSupplier.Pincode);
                oDb.AddInParameter(cmd, "@PhoneNo1", DbType.String, objSupplier.PhoneNo1);
                oDb.AddInParameter(cmd, "@PhoneNo2", DbType.String, objSupplier.PhoneNo2);
                oDb.AddInParameter(cmd, "@Landline", DbType.String, objSupplier.LandLine);
                oDb.AddInParameter(cmd, "@Fax", DbType.String, objSupplier.Fax);
                oDb.AddInParameter(cmd, "@Email", DbType.String, objSupplier.Email);
                oDb.AddInParameter(cmd, "@Website", DbType.String, objSupplier.WebSite);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objSupplier.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objSupplier.CreatedBy.UserID);
                oDb.AddInParameter(cmd, "@Area", DbType.String, objSupplier.Area);
                oDb.AddInParameter(cmd, "@LinkClick", DbType.Boolean, objSupplier.LinkClick);
                oDb.AddInParameter(cmd, "@IsRateUpdated", DbType.Int32, objSupplier.IsRateUpdated);
                oDb.AddInParameter(cmd, "@AccountNo ", DbType.String, objSupplier.AccountNo);
                oDb.AddInParameter(cmd, "@BankName", DbType.String, objSupplier.BankName);
                oDb.AddInParameter(cmd, "@BranchName", DbType.String, objSupplier.BranchName);
                oDb.AddInParameter(cmd, "@AccountHolderName", DbType.String, objSupplier.AccountHolderName);
                oDb.AddInParameter(cmd, "@IFSCCode", DbType.String, objSupplier.IFSCCode);
                oDb.AddInParameter(cmd, "@ImagePath", DbType.String, objSupplier.ImagePath);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_SupplierID"));
                    objSupplier.SupplierID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "Supplier AddSupplier | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateSupplier(Entity.Billing.Supplier objSupplier)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateSupplier(oDb, objSupplier, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tSupplier", "PK_SupplierID", objSupplier.SupplierID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objSupplier.ModifiedBy.UserID);
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
        private static bool UpdateSupplier(Database oDb, Entity.Billing.Supplier objSupplier, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_SUPPLIER);
                oDb.AddInParameter(cmd, "@PK_SupplierID", DbType.Int32, objSupplier.SupplierID);
                oDb.AddInParameter(cmd, "@SupplierName", DbType.String, objSupplier.SupplierName);
                oDb.AddInParameter(cmd, "@SupplierCode", DbType.String, objSupplier.SupplierCode);
                oDb.AddInParameter(cmd, "@SupplierAddress", DbType.String, objSupplier.SupplierAddress);
                oDb.AddInParameter(cmd, "@FK_StateID", DbType.Int32, objSupplier.State.StateID);
                oDb.AddInParameter(cmd, "@FK_TransportID", DbType.Int32, objSupplier.Transport.TransportID);
                oDb.AddInParameter(cmd, "@City", DbType.String, objSupplier.City);
                oDb.AddInParameter(cmd, "@Days", DbType.Int32, objSupplier.Days);
                oDb.AddInParameter(cmd, "@Taluk", DbType.String, objSupplier.Taluk);
                oDb.AddInParameter(cmd, "@Pincode", DbType.String, objSupplier.Pincode);
                oDb.AddInParameter(cmd, "@PhoneNo1", DbType.String, objSupplier.PhoneNo1);
                oDb.AddInParameter(cmd, "@PhoneNo2", DbType.String, objSupplier.PhoneNo2);
                oDb.AddInParameter(cmd, "@Landline", DbType.String, objSupplier.LandLine);
                oDb.AddInParameter(cmd, "@Fax", DbType.String, objSupplier.Fax);
                oDb.AddInParameter(cmd, "@Email", DbType.String, objSupplier.Email);
                oDb.AddInParameter(cmd, "@Website", DbType.String, objSupplier.WebSite);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objSupplier.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objSupplier.ModifiedBy.UserID);
                oDb.AddInParameter(cmd, "@Area", DbType.String, objSupplier.Area);
                oDb.AddInParameter(cmd, "@LinkClick", DbType.Boolean, objSupplier.LinkClick);
                oDb.AddInParameter(cmd, "@IsRateUpdated", DbType.Int32, objSupplier.IsRateUpdated);
                oDb.AddInParameter(cmd, "@AccountNo ", DbType.String, objSupplier.AccountNo);
                oDb.AddInParameter(cmd, "@BankName", DbType.String, objSupplier.BankName);
                oDb.AddInParameter(cmd, "@BranchName", DbType.String, objSupplier.BranchName);
                oDb.AddInParameter(cmd, "@AccountHolderName", DbType.String, objSupplier.AccountHolderName);
                oDb.AddInParameter(cmd, "@IFSCCode", DbType.String, objSupplier.IFSCCode);
                oDb.AddInParameter(cmd, "@ImagePath", DbType.String, objSupplier.ImagePath);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Supplier UpdateSupplier| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteSupplier(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteSupplier(oDb, ID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tSupplier", "PK_SupplierID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteSupplier(Database oDb, int ID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_SUPPLIER);
                oDb.AddInParameter(cmd, "@PK_SupplierID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Supplier DeleteSupplier | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
