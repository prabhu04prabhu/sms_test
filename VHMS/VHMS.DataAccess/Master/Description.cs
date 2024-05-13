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
    public class Description
    {
        public static Collection<Entity.Description> GetDescription()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Description> objList = new Collection<Entity.Description>();
            Entity.Description objDescription = new Entity.Description();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            Entity.DescriptionCategory objDescriptionCategory;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DESCRIPTION);
                dsList = db.ExecuteDataSet(cmd);
               
                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDescription = new Entity.Description();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();
                        objDescriptionCategory = new Entity.DescriptionCategory();

                        objDescriptionCategory.DescriptionCategoryID= Convert.ToInt32(drData["Fk_DescriptionCategoryID"]);
                        objDescriptionCategory.DescriptionCategoryName = Convert.ToString(drData["DescriptionCategoryName"]);
                        objDescription.DescriptionCategory = objDescriptionCategory;

                        objDescription.DescriptionID = Convert.ToInt32(drData["PK_DescriptionID"]);
                        objDescription.DescriptionName = Convert.ToString(drData["DescriptionName"]);
                        objDescription.Amount = Convert.ToDecimal(drData["Amount"]);
                        objDescription.DescriptionID = Convert.ToInt32(drData["PK_DescriptionID"]);
                        objDescription.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objDescription);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Description GetDescription | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Description GetDescriptionByID(int iDescriptionID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Description objDescription = new Entity.Description();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            Entity.DescriptionCategory objDescriptionCategory;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DESCRIPTION);
                db.AddInParameter(cmd, "@PK_DescriptionID", DbType.Int32, iDescriptionID);
                DataSet dsList = db.ExecuteDataSet(cmd);
               
                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDescription = new Entity.Description();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();
                        objDescriptionCategory = new Entity.DescriptionCategory();

                        objDescriptionCategory.DescriptionCategoryID = Convert.ToInt32(drData["Fk_DescriptionCategoryID"]);
                        objDescriptionCategory.DescriptionCategoryName = Convert.ToString(drData["DescriptionCategoryName"]);
                        objDescription.DescriptionCategory = objDescriptionCategory;

                        objDescription.DescriptionID = Convert.ToInt32(drData["PK_DescriptionID"]);
                        objDescription.DescriptionName = Convert.ToString(drData["DescriptionName"]);
                        objDescription.Amount = Convert.ToDecimal(drData["Amount"]);
                        objDescription.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Description GetDescriptionByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objDescription;
        }
        public static int AddDescription(Entity.Description objDescription)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddDescription(oDb, objDescription, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tDescription", "PK_DescriptionID", objDescription.DescriptionID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objDescription.CreatedBy.UserID);
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
        private static int AddDescription(Database oDb, Entity.Description objDescription, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_DESCRIPTION);
                oDb.AddOutParameter(cmd, "@PK_DescriptionID", DbType.Int32, objDescription.DescriptionID);
                oDb.AddInParameter(cmd, "@DescriptionName", DbType.String, objDescription.DescriptionName);
                oDb.AddInParameter(cmd, "@Amount", DbType.Decimal, objDescription.Amount);
                oDb.AddInParameter(cmd, "@FK_DescriptionCategoryID", DbType.Int32, objDescription.DescriptionCategory.DescriptionCategoryID);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objDescription.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objDescription.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_DescriptionID"));
                    objDescription.DescriptionID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Description AddDescription | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateDescription(Entity.Description objDescription)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateDescription(oDb, objDescription, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tDescription", "PK_DescriptionID", objDescription.DescriptionID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objDescription.ModifiedBy.UserID);
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
        private static bool UpdateDescription(Database oDb, Entity.Description objDescription, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_DESCRIPTION);
                oDb.AddInParameter(cmd, "@PK_DescriptionID", DbType.Int32, objDescription.DescriptionID);
                oDb.AddInParameter(cmd, "@DescriptionName", DbType.String, objDescription.DescriptionName);
                oDb.AddInParameter(cmd, "@Amount", DbType.Decimal, objDescription.Amount);
                oDb.AddInParameter(cmd, "@FK_DescriptionCategoryID", DbType.Int32, objDescription.DescriptionCategory.DescriptionCategoryID);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objDescription.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objDescription.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Description UpdateDescription | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteDescription(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteDescription(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tDescription", "PK_DescriptionID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteDescription(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_DESCRIPTION);
                oDb.AddInParameter(cmd, "@PK_DescriptionID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Description DeleteDescription | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
