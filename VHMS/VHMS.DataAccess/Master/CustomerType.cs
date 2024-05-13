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
    public class CustomerType
    {
        public static Collection<Entity.CustomerTypes> GetCustomerType()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.CustomerTypes> objList = new Collection<Entity.CustomerTypes>();
            Entity.CustomerTypes objCustomerType = new Entity.CustomerTypes();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CUSTOMERTYPE);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCustomerType = new Entity.CustomerTypes();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objCustomerType.CustomertypeID = Convert.ToInt32(drData["PK_CustomerTypeID"]);
                        objCustomerType.TypeName = Convert.ToString(drData["TypeName"]);
                        objCustomerType.CustomerTypeName = Convert.ToString(drData["CustomerType_Name"]);
                        objCustomerType.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objCustomerType);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.CustomerType GetCustomerType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.CustomerTypes GetCustomerTypeByID(int iCustomerTypeID)
        {
            string sException = string.Empty;
            Database db;
            Entity.CustomerTypes objCustomerType = new Entity.CustomerTypes();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CUSTOMERTYPE);
                db.AddInParameter(cmd, "@PK_CustomertypeID", DbType.Int32, iCustomerTypeID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCustomerType = new Entity.CustomerTypes();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objCustomerType.CustomertypeID = Convert.ToInt32(drData["PK_CustomerTypeID"]);
                        objCustomerType.TypeName = Convert.ToString(drData["TypeName"]);
                        objCustomerType.CustomerTypeName = Convert.ToString(drData["CustomerType_Name"]);
                        objCustomerType.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.CustomerType GetCustomerTypeByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objCustomerType;
        }

        public static Collection<Entity.CustomerTypes> GetCustomertypeName(string TypeNames)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.CustomerTypes> objList = new Collection<Entity.CustomerTypes>();
            Entity.CustomerTypes objCustomerType = new Entity.CustomerTypes();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_CUSTOMERTYPE);
                db.AddInParameter(cmd, "@key", DbType.String, TypeNames);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCustomerType = new Entity.CustomerTypes();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objCustomerType.CustomertypeID = Convert.ToInt32(drData["PK_CustomerTypeID"]);
                        objCustomerType.TypeName = Convert.ToString(drData["TypeName"]);
                        objCustomerType.CustomerTypeName = Convert.ToString(drData["CustomerType_Name"]);
                        objCustomerType.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objCustomerType);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.CustomerType GetCustomerType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        //public static Entity.CustomerType GetCustomertypeName(string TypeNames)
        //{
        //    string sException = string.Empty;
        //    Database db;
        //    Entity.CustomerType objCustomerType = new Entity.CustomerType();
        //    Entity.User objCreatedUser;
        //    Entity.User objModifiedUser;

        //    try
        //    {
        //        db = Entity.DBConnection.dbCon;
        //        DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_CUSTOMERTYPE);
        //        db.AddInParameter(cmd, "@key", DbType.String, TypeNames);
        //        DataSet dsList = db.ExecuteDataSet(cmd);

        //        if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow drData in dsList.Tables[0].Rows)
        //            {
        //                objCustomerType = new Entity.CustomerType();
        //                objCreatedUser = new Entity.User();
        //                objModifiedUser = new Entity.User();

        //                objCustomerType.CustomertypeID = Convert.ToInt32(drData["PK_CustomerTypeID"]);
        //                objCustomerType.TypeName = Convert.ToString(drData["TypeName"]);
        //                objCustomerType.CustomerTypeName = Convert.ToString(drData["CustomerType_Name"]);
        //                objCustomerType.IsActive = Convert.ToBoolean(drData["IsActive"]);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "VHMS.DataAccess.CustomerType GetCustomerTypeByID | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return objCustomerType;
        //}
        public static int AddCustomerType(Entity.CustomerTypes objCustomerType)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddCustomerType(oDb, objCustomerType, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tCustomerType", "PK_CustomerTypeID", objCustomerType.CustomertypeID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objCustomerType.CreatedBy.UserID);
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
        private static int AddCustomerType(Database oDb, Entity.CustomerTypes objCustomerType, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_CUSTOMERTYPE);
                oDb.AddOutParameter(cmd, "@PK_CustomertypeID", DbType.Int32, objCustomerType.CustomertypeID);
                oDb.AddInParameter(cmd, "@TypeName", DbType.String, objCustomerType.TypeName);
                oDb.AddInParameter(cmd, "@CustomerType_Name ", DbType.String, objCustomerType.CustomerTypeName);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objCustomerType.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objCustomerType.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_CustomertypeID"));
                    objCustomerType.CustomertypeID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.CustomerType AddCustomerType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateCustomerType(Entity.CustomerTypes objCustomerType)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateCustomerType(oDb, objCustomerType, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tCustomerType", "PK_CustomerTypeID", objCustomerType.CustomertypeID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objCustomerType.ModifiedBy.UserID);
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
        private static bool UpdateCustomerType(Database oDb, Entity.CustomerTypes objCustomerType, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_CUSTOMERTYPE);
                oDb.AddInParameter(cmd, "@PK_CustomertypeID", DbType.Int32, objCustomerType.CustomertypeID);
                oDb.AddInParameter(cmd, "@TypeName", DbType.String, objCustomerType.TypeName);
                oDb.AddInParameter(cmd, "@CustomerType_Name", DbType.String, objCustomerType.CustomerTypeName);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objCustomerType.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objCustomerType.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.CustomerType UpdateCustomerType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteCustomerType(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteCustomerType(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tCustomerType", "PK_CustomerTypeID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteCustomerType(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_CUSTOMERTYPE);
                oDb.AddInParameter(cmd, "@PK_CustomertypeID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.CustomerType DeleteCustomerType | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
