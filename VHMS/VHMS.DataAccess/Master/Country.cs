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
    public class Country
    {
        public static Collection<Entity.Country> GetCountry()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Country> objList = new Collection<Entity.Country>();
            Entity.Country objCountry = new Entity.Country();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_COUNTRY);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCountry = new Entity.Country();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objCountry.CountryID = Convert.ToInt32(drData["PK_CountryID"]);
                        objCountry.CountryName = Convert.ToString(drData["CountryName"]);
                        objCountry.CountryCode = Convert.ToString(drData["CountryCode"]);
                        objCountry.Currency = Convert.ToString(drData["Currency"]);
                        objCountry.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objCountry);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Country GetCountry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Country GetCountryByID(int iCountryID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Country objCountry = new Entity.Country();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_COUNTRY);
                db.AddInParameter(cmd, "@PK_CountryID", DbType.Int32, iCountryID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCountry = new Entity.Country();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objCountry.CountryID = Convert.ToInt32(drData["PK_CountryID"]);
                        objCountry.CountryName = Convert.ToString(drData["CountryName"]);
                        objCountry.CountryCode = Convert.ToString(drData["CountryCode"]);
                        objCountry.Currency = Convert.ToString(drData["Currency"]);
                        objCountry.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Country GetCountryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objCountry;
        }
        public static int AddCountry(Entity.Country objCountry)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddCountry(oDb, objCountry, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tCountry", "PK_CountryID", objCountry.CountryID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objCountry.CreatedBy.UserID);
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
        private static int AddCountry(Database oDb, Entity.Country objCountry, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_COUNTRY);
                oDb.AddOutParameter(cmd, "@PK_CountryID", DbType.Int32, objCountry.CountryID);
                oDb.AddInParameter(cmd, "@CountryName", DbType.String, objCountry.CountryName);
                oDb.AddInParameter(cmd, "@CountryCode", DbType.String, objCountry.CountryCode);
                oDb.AddInParameter(cmd, "@Currency", DbType.String, objCountry.Currency);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objCountry.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objCountry.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_CountryID"));
                    objCountry.CountryID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Country AddCountry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateCountry(Entity.Country objCountry)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateCountry(oDb, objCountry, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tCountry", "PK_CountryID", objCountry.CountryID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objCountry.ModifiedBy.UserID);
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
        private static bool UpdateCountry(Database oDb, Entity.Country objCountry, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_COUNTRY);
                oDb.AddInParameter(cmd, "@PK_CountryID", DbType.Int32, objCountry.CountryID);
                oDb.AddInParameter(cmd, "@CountryName", DbType.String, objCountry.CountryName);
                oDb.AddInParameter(cmd, "@CountryCode", DbType.String, objCountry.CountryCode);
                oDb.AddInParameter(cmd, "@Currency", DbType.String, objCountry.Currency);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objCountry.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objCountry.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Country UpdateCountry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteCountry(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteCountry(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tCountry", "PK_CountryID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteCountry(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_COUNTRY);
                oDb.AddInParameter(cmd, "@PK_CountryID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Country DeleteCountry | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
