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
    public class Tax
    {
        public static Collection<Entity.Billing.Tax> GetTax()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Tax> objList = new Collection<Entity.Billing.Tax>();
            Entity.Billing.Tax objTax = new Entity.Billing.Tax();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TAX);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objTax = new Entity.Billing.Tax();

                        objTax.TaxID = Convert.ToInt32(drData["PK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objTax.TaxPercentage = Convert.ToDecimal(drData["TaxPercentage"]);
                        objTax.CGSTPercent = Convert.ToDecimal(drData["CGSTPercent"]);
                        objTax.SGSTPercent = Convert.ToDecimal(drData["SGSTPercent"]);
                        objTax.IGSTPercent = Convert.ToDecimal(drData["IGSTPercent"]);
                        objTax.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objTax);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Tax GetTax | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.Tax GetTaxByID(int iTaxID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Tax objTax = new Entity.Billing.Tax();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TAX);
                db.AddInParameter(cmd, "@PK_TaxID", DbType.Int32, iTaxID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objTax = new Entity.Billing.Tax();
                        objTax.TaxID = Convert.ToInt32(drData["PK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objTax.TaxPercentage = Convert.ToDecimal(drData["TaxPercentage"]);
                        objTax.CGSTPercent = Convert.ToDecimal(drData["CGSTPercent"]);
                        objTax.SGSTPercent = Convert.ToDecimal(drData["SGSTPercent"]);
                        objTax.IGSTPercent = Convert.ToDecimal(drData["IGSTPercent"]);
                        objTax.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Tax GetTaxByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objTax;
        }
        public static int AddTax(Entity.Billing.Tax objTax)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddTax(oDb, objTax, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tTax", "PK_TaxID", objTax.TaxID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objTax.CreatedBy.UserID);
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
        private static int AddTax(Database oDb, Entity.Billing.Tax objTax, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_TAX);
                oDb.AddOutParameter(cmd, "@PK_TaxID", DbType.Int32, objTax.TaxID);
                oDb.AddInParameter(cmd, "@TaxName", DbType.String, objTax.TaxName);
                oDb.AddInParameter(cmd, "@TaxPercentage", DbType.Decimal, objTax.TaxPercentage);
                oDb.AddInParameter(cmd, "@CGSTPercent", DbType.Decimal, objTax.CGSTPercent);
                oDb.AddInParameter(cmd, "@SGSTPercent", DbType.Decimal, objTax.SGSTPercent);
                oDb.AddInParameter(cmd, "@IGSTPercent", DbType.Decimal, objTax.IGSTPercent);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objTax.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objTax.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_TaxID"));
                    objTax.TaxID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Tax AddTax | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateTax(Entity.Billing.Tax objTax)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateTax(oDb, objTax, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tTax", "PK_TaxID", objTax.TaxID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objTax.ModifiedBy.UserID);
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
        private static bool UpdateTax(Database oDb, Entity.Billing.Tax objTax, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_TAX);
                oDb.AddInParameter(cmd, "@PK_TaxID", DbType.Int32, objTax.TaxID);
                oDb.AddInParameter(cmd, "@TaxName", DbType.String, objTax.TaxName);
                oDb.AddInParameter(cmd, "@TaxPercentage", DbType.Decimal, objTax.TaxPercentage);
                oDb.AddInParameter(cmd, "@CGSTPercent", DbType.Decimal, objTax.CGSTPercent);
                oDb.AddInParameter(cmd, "@SGSTPercent", DbType.Decimal, objTax.SGSTPercent);
                oDb.AddInParameter(cmd, "@IGSTPercent", DbType.Decimal, objTax.IGSTPercent);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objTax.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objTax.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Tax UpdateTax | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteTax(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteTax(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tTax", "PK_TaxID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteTax(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_TAX);
                oDb.AddInParameter(cmd, "@PK_TaxID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.Tax DeleteTax | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
