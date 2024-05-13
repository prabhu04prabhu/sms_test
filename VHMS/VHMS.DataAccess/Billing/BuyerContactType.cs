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
    public class BuyerContactType
    {
        public static Collection<Entity.Billing.BuyerContactType> GetBuyerContactType()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.BuyerContactType> objList = new Collection<Entity.Billing.BuyerContactType>();
            Entity.Billing.BuyerContactType objBuyerContactType = new Entity.Billing.BuyerContactType();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_BUYERCONTACTTYPE);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objBuyerContactType = new Entity.Billing.BuyerContactType();

                        objBuyerContactType.BuyerContactTypeID = Convert.ToInt32(drData["PK_BuyerContactTypeID"]);
                        objBuyerContactType.BuyerContactTypeName = Convert.ToString(drData["BuyerContactTypeName"]);
                        objBuyerContactType.BuyerContactTypeCode = Convert.ToString(drData["BuyerContactTypeCode"]);
                        objBuyerContactType.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objBuyerContactType);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.BuyerContactType GetBuyerContactType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.BuyerContactType> GetActiveBuyerContactType()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.BuyerContactType> objList = new Collection<Entity.Billing.BuyerContactType>();
            Entity.Billing.BuyerContactType objBuyerContactType = new Entity.Billing.BuyerContactType();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_BUYERCONTACTTYPE);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        if (Convert.ToBoolean(drData["IsActive"]))
                        {
                            objBuyerContactType = new Entity.Billing.BuyerContactType();

                            objBuyerContactType.BuyerContactTypeID = Convert.ToInt32(drData["PK_BuyerContactTypeID"]);
                            objBuyerContactType.BuyerContactTypeName = Convert.ToString(drData["BuyerContactTypeName"]);
                            objBuyerContactType.BuyerContactTypeCode = Convert.ToString(drData["BuyerContactTypeCode"]);
                            objBuyerContactType.IsActive = Convert.ToBoolean(drData["IsActive"]);

                            objList.Add(objBuyerContactType);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.BuyerContactType GetBuyerContactType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.BuyerContactType GetBuyerContactTypeByID(int iBuyerContactTypeID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.BuyerContactType objBuyerContactType = new Entity.Billing.BuyerContactType();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_BUYERCONTACTTYPE);
                db.AddInParameter(cmd, "@PK_BuyerContactTypeID", DbType.Int32, iBuyerContactTypeID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objBuyerContactType = new Entity.Billing.BuyerContactType();
                        objBuyerContactType.BuyerContactTypeID = Convert.ToInt32(drData["PK_BuyerContactTypeID"]);
                        objBuyerContactType.BuyerContactTypeName = Convert.ToString(drData["BuyerContactTypeName"]);
                        objBuyerContactType.BuyerContactTypeCode = Convert.ToString(drData["BuyerContactTypeCode"]);
                        objBuyerContactType.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.BuyerContactType GetBuyerContactTypeByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objBuyerContactType;
        }
        public static int AddBuyerContactType(Entity.Billing.BuyerContactType objBuyerContactType)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddBuyerContactType(oDb, objBuyerContactType, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tBuyerContactType", "PK_BuyerContactTypeID", objBuyerContactType.BuyerContactTypeID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objBuyerContactType.CreatedBy.UserID);
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
        private static int AddBuyerContactType(Database oDb, Entity.Billing.BuyerContactType objBuyerContactType, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_BUYERCONTACTTYPE);
                oDb.AddOutParameter(cmd, "@PK_BuyerContactTypeID", DbType.Int32, objBuyerContactType.BuyerContactTypeID);
                oDb.AddInParameter(cmd, "@BuyerContactTypeName", DbType.String, objBuyerContactType.BuyerContactTypeName);
                oDb.AddInParameter(cmd, "@BuyerContactTypeCode", DbType.String, objBuyerContactType.BuyerContactTypeCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objBuyerContactType.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objBuyerContactType.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_BuyerContactTypeID"));
                    objBuyerContactType.BuyerContactTypeID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.BuyerContactType AddBuyerContactType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateBuyerContactType(Entity.Billing.BuyerContactType objBuyerContactType)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateBuyerContactType(oDb, objBuyerContactType, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tBuyerContactType", "PK_BuyerContactTypeID", objBuyerContactType.BuyerContactTypeID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objBuyerContactType.ModifiedBy.UserID);
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
        private static bool UpdateBuyerContactType(Database oDb, Entity.Billing.BuyerContactType objBuyerContactType, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_BUYERCONTACTTYPE);
                oDb.AddInParameter(cmd, "@PK_BuyerContactTypeID", DbType.Int32, objBuyerContactType.BuyerContactTypeID);
                oDb.AddInParameter(cmd, "@BuyerContactTypeName", DbType.String, objBuyerContactType.BuyerContactTypeName);
                oDb.AddInParameter(cmd, "@BuyerContactTypeCode", DbType.String, objBuyerContactType.BuyerContactTypeCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objBuyerContactType.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objBuyerContactType.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.BuyerContactType UpdateBuyerContactType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteBuyerContactType(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteBuyerContactType(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tBuyerContactType", "PK_BuyerContactTypeID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteBuyerContactType(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_BUYERCONTACTTYPE);
                oDb.AddInParameter(cmd, "@PK_BuyerContactTypeID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Billing.BuyerContactType DeleteBuyerContactType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
