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
    public class Role
    {
        public static Collection<Entity.Role> GetRole()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Role> objList = new Collection<Entity.Role>();
            Entity.Role objRole = new Entity.Role();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ROLE);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRole = new Entity.Role();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objRole.RoleID = Convert.ToInt32(drData["PK_RoleID"]);
                        objRole.RoleName = Convert.ToString(drData["RoleName"]);
                        objRole.Description = Convert.ToString(drData["Description"]);
                        objRole.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objRole.CommonMargin = Convert.ToBoolean(drData["CommonMargin"]);
                        objRole.MarginView = Convert.ToBoolean(drData["MarginView"]);

                        objList.Add(objRole);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Role GetRole | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Role GetRoleByID(int iRoleID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Role objRole = new Entity.Role();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ROLE);
                db.AddInParameter(cmd, "@PK_RoleID", DbType.Int32, iRoleID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRole = new Entity.Role();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objRole.RoleID = Convert.ToInt32(drData["PK_RoleID"]);
                        objRole.RoleName = Convert.ToString(drData["RoleName"]);
                        objRole.Description = Convert.ToString(drData["Description"]);
                        objRole.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objRole.CommonMargin = Convert.ToBoolean(drData["CommonMargin"]);
                        objRole.MarginView = Convert.ToBoolean(drData["MarginView"]);
                        objRole.RoleConfig_Count = Convert.ToInt32(drData["RoleConfig_Count"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Role GetRoleByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objRole;
        }
        public static int AddRole(Entity.Role objRole)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddRole(oDb, objRole, oTrans);
                    oTrans.Commit();
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
        private static int AddRole(Database oDb, Entity.Role objRole, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_ROLE);
                oDb.AddOutParameter(cmd, "@PK_RoleID", DbType.Int32, objRole.RoleID);
                oDb.AddInParameter(cmd, "@RoleName", DbType.String, objRole.RoleName);
                oDb.AddInParameter(cmd, "@Description", DbType.String, objRole.Description);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objRole.IsActive);
                oDb.AddInParameter(cmd, "@CommonMargin", DbType.Boolean, objRole.CommonMargin);
                oDb.AddInParameter(cmd, "@MarginView", DbType.Boolean, objRole.MarginView);
                oDb.AddInParameter(cmd, "@CreatedBy", DbType.Int32, objRole.CreatedBy.UserID);
                iID = oDb.ExecuteNonQuery(cmd, oTrans);

                if (iID != 0) iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_RoleID"));
            }
            catch (Exception ex)
            {
                sException = "Role AddRole | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateRole(Entity.Role objRole)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateRole(oDb, objRole, oTrans);
                    oTrans.Commit();
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
        private static bool UpdateRole(Database oDb, Entity.Role objRole, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_ROLE);
                oDb.AddInParameter(cmd, "@PK_RoleID", DbType.Int32, objRole.RoleID);
                oDb.AddInParameter(cmd, "@RoleName", DbType.String, objRole.RoleName);
                oDb.AddInParameter(cmd, "@Description", DbType.String, objRole.Description);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objRole.IsActive);
                oDb.AddInParameter(cmd, "@CommonMargin", DbType.Boolean, objRole.CommonMargin);
                oDb.AddInParameter(cmd, "@MarginView", DbType.Boolean, objRole.MarginView);
                oDb.AddInParameter(cmd, "@ModifiedBy", DbType.Int32, objRole.ModifiedBy.UserID);
                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Role UpdateRole| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteRole(int ID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteRole(oDb, ID, oTrans);
                    oTrans.Commit();
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
        private static bool DeleteRole(Database oDb, int ID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_ROLE);
                oDb.AddInParameter(cmd, "@PK_RoleID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Role DeleteRole | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeletePermissionByRoleID(int ID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteRole(oDb, ID, oTrans);
                    oTrans.Commit();
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
        private static bool DeletePermissionByRoleID(Database oDb, int ID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_PERMISSIONBYROLEID);
                oDb.AddInParameter(cmd, "@PK_RoleID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Role DeleteRole | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }

    public class RoleConfiguration
    {
        public static DataSet GetRoleConfigurationByRoleID(int iRoleID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ROLECONFIGURATION);
                db.AddInParameter(cmd, "@FK_RoleID", DbType.Int32, iRoleID);
                dsList = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "Role GetRoleConfigurationByRoleID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsList;
        }
        public static Collection<Entity.RoleConfiguration> GetRoleConfiguration(int iRoleID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.RoleConfiguration> objList = new Collection<Entity.RoleConfiguration>();
            Entity.RoleConfiguration objRoleConfiguration;
            Entity.Role objRole;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_ROLECONFIGURATION);
                db.AddInParameter(cmd, "@FK_RoleID", DbType.Int32, iRoleID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRoleConfiguration = new Entity.RoleConfiguration();
                        objRole = new Entity.Role();

                        objRole.RoleID = Convert.ToInt32(drData["FK_RoleID"]);
                        objRole.RoleName = Convert.ToString(drData["RoleName"]);

                        objRoleConfiguration.MenuID = Convert.ToInt32(drData["FK_MenuID"]);
                        objRoleConfiguration.MenuName = Convert.ToString(drData["MenuName"]);

                        objRoleConfiguration.ModuleID = Convert.ToInt32(drData["FK_ModuleID"]);
                        objRoleConfiguration.ModuleName = Convert.ToString(drData["ModuleName"]);

                        objRoleConfiguration.RoleConfigurationID = Convert.ToInt32(drData["PK_RoleConfigurationID"]);
                        objRoleConfiguration.IsAccess = Convert.ToBoolean(drData["IsAccess"]);
                        objRoleConfiguration.IsAdd = Convert.ToBoolean(drData["IsAdd"]);
                        objRoleConfiguration.IsDelete = Convert.ToBoolean(drData["IsDelete"]);
                        objRoleConfiguration.IsEdit = Convert.ToBoolean(drData["IsEdit"]);
                        objRoleConfiguration.IsView = Convert.ToBoolean(drData["IsView"]);

                        objList.Add(objRoleConfiguration);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Role GetRoleConfiguration | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }        
        public static void SaveRoleConfiguration(Collection<Entity.RoleConfiguration> objList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.RoleConfiguration objRoleConfiguration in objList)
            {
                if (objRoleConfiguration.StatusFlag == "I")
                    iID = AddRoleConfiguration(objRoleConfiguration);
                else if (objRoleConfiguration.StatusFlag == "U")
                    bResult = UpdateRoleConfiguration(objRoleConfiguration);
                else if (objRoleConfiguration.StatusFlag == "D")
                    bResult = DeleteRoleConfiguration(objRoleConfiguration.RoleConfigurationID);
            }
        }
        private static int AddRoleConfiguration(Entity.RoleConfiguration objRoleConfiguration)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = null;

                cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_ROLECONFIGURATION);
                db.AddInParameter(cmd, "@FK_RoleID", DbType.Int32, objRoleConfiguration.RoleID);
                db.AddInParameter(cmd, "@FK_ModuleID", DbType.Int32, objRoleConfiguration.ModuleID);
                db.AddInParameter(cmd, "@IsAccess", DbType.Boolean, objRoleConfiguration.IsAccess);
                db.AddInParameter(cmd, "@IsView", DbType.Boolean, objRoleConfiguration.IsView);
                db.AddInParameter(cmd, "@IsAdd", DbType.Boolean, objRoleConfiguration.IsAdd);
                db.AddInParameter(cmd, "@IsEdit", DbType.Boolean, objRoleConfiguration.IsEdit);
                db.AddInParameter(cmd, "@IsDelete", DbType.Boolean, objRoleConfiguration.IsDelete);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = 1;
            }
            catch (Exception ex)
            {
                sException = "RoleConfiguration AddRoleConfiguration | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        private static bool UpdateRoleConfiguration(Entity.RoleConfiguration objRoleConfiguration)
        {
            string sException = string.Empty;
            int iID = 0; bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = null;

                cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_ROLECONFIGURATION);
                db.AddInParameter(cmd, "@PK_RoleConfigurationID", DbType.Int32, objRoleConfiguration.RoleConfigurationID);
                db.AddInParameter(cmd, "@FK_RoleID", DbType.Int32, objRoleConfiguration.RoleID);
                db.AddInParameter(cmd, "@FK_ModuleID", DbType.Int32, objRoleConfiguration.ModuleID);
                db.AddInParameter(cmd, "@IsAccess", DbType.Boolean, objRoleConfiguration.IsAccess);
                db.AddInParameter(cmd, "@IsView", DbType.Boolean, objRoleConfiguration.IsView);
                db.AddInParameter(cmd, "@IsAdd", DbType.Boolean, objRoleConfiguration.IsAdd);
                db.AddInParameter(cmd, "@IsEdit", DbType.Boolean, objRoleConfiguration.IsEdit);
                db.AddInParameter(cmd, "@IsDelete", DbType.Boolean, objRoleConfiguration.IsDelete);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "RoleConfiguration UpdateRoleConfiguration | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        private static bool DeleteRoleConfiguration(int iRoleConfigurationID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_ROLECONFIGURATION);
                db.AddInParameter(cmd, "@PK_RoleConfigurationID", DbType.Int32, iRoleConfigurationID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "RoleConfiguration DeleteRoleConfiguration | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
