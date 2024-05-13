using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections.ObjectModel;
using VHMS;

namespace VHMS.DataAccess
{
    public class Gift
    {
        public static Collection<Entity.Gift> GetGift()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Gift> objList = new Collection<Entity.Gift>();
            Entity.Gift objGift = new Entity.Gift();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_GIFT);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objGift = new Entity.Gift();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objGift.GiftID = Convert.ToInt32(drData["PK_GiftID"]);
                        objGift.GiftName = Convert.ToString(drData["GiftName"]);
                        objGift.GiftCode = Convert.ToString(drData["GiftCode"]);
                        objGift.FromAmount = Convert.ToDecimal(drData["FromAmount"]);
                       objGift.ToAmount = Convert.ToDecimal(drData["ToAmount"]);
                       
                        objGift.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objGift);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Gift GetGift | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Gift GetGiftByID(int iGiftID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Gift objGift = new Entity.Gift();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_GIFT);
                db.AddInParameter(cmd, "@PK_GiftID", DbType.Int32, iGiftID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objGift = new Entity.Gift();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objGift.GiftID = Convert.ToInt32(drData["PK_GiftID"]);
                        objGift.GiftName = Convert.ToString(drData["GiftName"]);
                        objGift.GiftCode = Convert.ToString(drData["GiftCode"]);
                        objGift.FromAmount = Convert.ToDecimal(drData["FromAmount"]);
                        objGift.ToAmount = Convert.ToDecimal(drData["ToAmount"]);
                        objGift.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Gift GetGiftByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objGift;
        }

        public static Collection<Entity.Gift> GetGiftAmount(decimal Amount)
        {
            string sException = string.Empty;
            Database db;
            Collection<Entity.Gift> objList = new Collection<Entity.Gift>();
            Entity.Gift objGift = new Entity.Gift();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_GIFTAMOUNT);
                db.AddInParameter(cmd, "@Amount", DbType.Decimal, Amount);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objGift = new Entity.Gift();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objGift.GiftID = Convert.ToInt32(drData["PK_GiftID"]);
                        objGift.GiftName = Convert.ToString(drData["GiftName"]);
                        objGift.GiftCode = Convert.ToString(drData["GiftCode"]);
                        objGift.FromAmount = Convert.ToDecimal(drData["FromAmount"]);
                        objGift.ToAmount = Convert.ToDecimal(drData["ToAmount"]);

                        objGift.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objGift);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Gift GetGift | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static int AddGift(Entity.Gift objGift)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddGift(oDb, objGift, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tGift", "PK_GiftID", objGift.GiftID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objGift.CreatedBy.UserID);
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
        private static int AddGift(Database oDb, Entity.Gift objGift, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_GIFT);
                oDb.AddOutParameter(cmd, "@PK_GiftID", DbType.Int32, objGift.GiftID);
                oDb.AddInParameter(cmd, "@GiftName", DbType.String, objGift.GiftName);
                oDb.AddInParameter(cmd, "@GiftCode", DbType.String, objGift.GiftCode);
                oDb.AddInParameter(cmd, "@FromAmount", DbType.Decimal, objGift.FromAmount);
                oDb.AddInParameter(cmd, "@ToAmount", DbType.Decimal, objGift.ToAmount);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objGift.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objGift.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_GiftID"));
                    objGift.GiftID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Gift AddGift | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateGift(Entity.Gift objGift)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateGift(oDb, objGift, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tGift", "PK_GiftID", objGift.GiftID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objGift.ModifiedBy.UserID);
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
        private static bool UpdateGift(Database oDb, Entity.Gift objGift, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_GIFT);
                oDb.AddInParameter(cmd, "@PK_GiftID", DbType.Int32, objGift.GiftID);
                oDb.AddInParameter(cmd, "@GiftName", DbType.String, objGift.GiftName);
                oDb.AddInParameter(cmd, "@GiftCode", DbType.String, objGift.GiftCode);
                oDb.AddInParameter(cmd, "@FromAmount", DbType.Decimal, objGift.FromAmount);
                oDb.AddInParameter(cmd, "@ToAmount", DbType.Decimal, objGift.ToAmount);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objGift.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objGift.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Gift UpdateGift | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteGift(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteGift(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tGift", "PK_GiftID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteGift(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_GIFT);
                oDb.AddInParameter(cmd, "@PK_GiftID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Gift DeleteGift | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
