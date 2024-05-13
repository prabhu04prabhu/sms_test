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
    public class Work
    {
        public static Collection<Entity.Work> GetWork()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Work> objList = new Collection<Entity.Work>();
            Entity.Work objWork = new Entity.Work();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_WORK);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objWork = new Entity.Work();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objWork.WorkID = Convert.ToInt32(drData["PK_WorkID"]);
                        objWork.WorkName = Convert.ToString(drData["WorkName"]);
                        objWork.Amount = Convert.ToDecimal(drData["Amount"]);
                        objWork.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objWork);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Work GetWork | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Work GetWorkByID(int iWorkID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Work objWork = new Entity.Work();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_WORK);
                db.AddInParameter(cmd, "@PK_WorkID", DbType.Int32, iWorkID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objWork = new Entity.Work();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objWork.WorkID = Convert.ToInt32(drData["PK_WorkID"]);
                        objWork.WorkName = Convert.ToString(drData["WorkName"]);
                        objWork.Amount = Convert.ToDecimal(drData["Amount"]);
                        objWork.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Work GetWorkByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objWork;
        }

        public static Entity.Work GetWorkRate(int iWorkID,int iVendorID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Work objWork = new Entity.Work();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_WORKRATE);
                db.AddInParameter(cmd, "@PK_WorkID", DbType.Int32, iWorkID);
                db.AddInParameter(cmd, "@FK_VendorID", DbType.Int32, iVendorID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objWork = new Entity.Work();

                        objWork.WorkID = Convert.ToInt32(drData["PK_WorkID"]);
                        objWork.WorkName = Convert.ToString(drData["WorkName"]);
                        objWork.Amount = Convert.ToDecimal(drData["Amount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Work GetWorkByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objWork;
        }

        public static Entity.Work GetWorkRatebyVendor(int iWorkID, int iVendorID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Work objWork = new Entity.Work();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_VENDORWORKTRANSBYAMOUNT);
                db.AddInParameter(cmd, "@FK_WorkID", DbType.Int32, iWorkID);
                db.AddInParameter(cmd, "@FK_VendorID", DbType.Int32, iVendorID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objWork = new Entity.Work();

                        objWork.WorkID = Convert.ToInt32(drData["FK_WorkID"]);
                        objWork.WorkName = Convert.ToString(drData["WorkName"]);
                        objWork.Amount = Convert.ToDecimal(drData["Amount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Work GetWorkByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objWork;
        }
        public static int AddWork(Entity.Work objWork)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddWork(oDb, objWork, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tWork", "PK_WorkID", objWork.WorkID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objWork.CreatedBy.UserID);
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
        private static int AddWork(Database oDb, Entity.Work objWork, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_WORK);
                oDb.AddOutParameter(cmd, "@PK_WorkID", DbType.Int32, objWork.WorkID);
                oDb.AddInParameter(cmd, "@WorkName", DbType.String, objWork.WorkName);
                oDb.AddInParameter(cmd, "@Amount", DbType.Decimal, objWork.Amount);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objWork.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objWork.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_WorkID"));
                    objWork.WorkID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Work AddWork | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateWork(Entity.Work objWork)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateWork(oDb, objWork, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tWork", "PK_WorkID", objWork.WorkID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objWork.ModifiedBy.UserID);
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
        private static bool UpdateWork(Database oDb, Entity.Work objWork, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_WORK);
                oDb.AddInParameter(cmd, "@PK_WorkID", DbType.Int32, objWork.WorkID);
                oDb.AddInParameter(cmd, "@WorkName", DbType.String, objWork.WorkName);
                oDb.AddInParameter(cmd, "@Amount", DbType.Decimal, objWork.Amount);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objWork.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objWork.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Work UpdateWork | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteWork(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteWork(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tWork", "PK_WorkID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteWork(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_WORK);
                oDb.AddInParameter(cmd, "@PK_WorkID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Work DeleteWork | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
