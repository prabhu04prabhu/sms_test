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
    public class ShareScreen
    {
        public static Collection<Entity.ShareScreen> GetShareScreen()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.ShareScreen> objList = new Collection<Entity.ShareScreen>();
            Entity.ShareScreen objShareScreen = new Entity.ShareScreen();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SHARESCREEN);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objShareScreen = new Entity.ShareScreen();

                        objShareScreen.TemplateID = Convert.ToInt32(drData["PK_TemplateID"]);
                        objShareScreen.TemplateName = Convert.ToString(drData["TemplateName"]);
                        objShareScreen.TemplateContent = Convert.ToString(drData["TemplateContent"]);
                        objShareScreen.TemplateMessage = Convert.ToString(drData["TemplateMessage"]);
                        objShareScreen.MobileNo1 = Convert.ToString(drData["MobileNo1"]);
                        objShareScreen.IsActive = Convert.ToBoolean(drData["IsActive"]);
                
                        objList.Add(objShareScreen);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.ShareScreen GetShareScreen | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.ShareScreen> GetActiveShareScreen()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.ShareScreen> objList = new Collection<Entity.ShareScreen>();
            Entity.ShareScreen objShareScreen = new Entity.ShareScreen();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SHARESCREEN);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        if (Convert.ToBoolean(drData["IsActive"]))
                        {
                            objShareScreen = new Entity.ShareScreen();

                            objShareScreen.TemplateID = Convert.ToInt32(drData["PK_TemplateID"]);
                            objShareScreen.TemplateName = Convert.ToString(drData["TemplateName"]);
                            objShareScreen.TemplateContent = Convert.ToString(drData["TemplateContent"]);
                            objShareScreen.MobileNo1 = Convert.ToString(drData["MobileNo1"]);
                            objShareScreen.TemplateMessage = Convert.ToString(drData["TemplateMessage"]);
                            objShareScreen.IsActive = Convert.ToBoolean(drData["IsActive"]);

                            objList.Add(objShareScreen);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.ShareScreen GetShareScreen | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.ShareScreen GetShareScreenByID(int iTemplateID)
        {
            string sException = string.Empty;
            Database db;
            Entity.ShareScreen objShareScreen = new Entity.ShareScreen();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SHARESCREEN);
                db.AddInParameter(cmd, "@PK_TemplateID", DbType.Int32, iTemplateID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objShareScreen = new Entity.ShareScreen();
                        objShareScreen.TemplateID = Convert.ToInt32(drData["PK_TemplateID"]);
                        objShareScreen.TemplateName = Convert.ToString(drData["TemplateName"]);
                        objShareScreen.TemplateContent = Convert.ToString(drData["TemplateContent"]);
                        objShareScreen.MobileNo1 = Convert.ToString(drData["MobileNo1"]);
                        objShareScreen.TemplateMessage = Convert.ToString(drData["TemplateMessage"]);
                        objShareScreen.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.ShareScreen GetShareScreenByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objShareScreen;
        }
        public static int AddShareScreen(Entity.ShareScreen objShareScreen)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddShareScreen(oDb, objShareScreen, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tShareScreen", "PK_TemplateID", objShareScreen.TemplateID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objShareScreen.CreatedBy.UserID);
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
        private static int AddShareScreen(Database oDb, Entity.ShareScreen objShareScreen, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_SHARESCREEN);
                oDb.AddOutParameter(cmd, "@PK_TemplateID", DbType.Int32, objShareScreen.TemplateID);
                oDb.AddInParameter(cmd, "@TemplateName", DbType.String, objShareScreen.TemplateName);
                oDb.AddInParameter(cmd, "@TemplateContent", DbType.String, objShareScreen.TemplateContent);
                oDb.AddInParameter(cmd, "@MobileNo1", DbType.String, objShareScreen.MobileNo1);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objShareScreen.IsActive);      
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objShareScreen.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_TemplateID"));
                    objShareScreen.TemplateID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.ShareScreen AddShareScreen | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateShareScreen(Entity.ShareScreen objShareScreen)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateShareScreen(oDb, objShareScreen, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tShareScreen", "PK_TemplateID", objShareScreen.TemplateID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objShareScreen.ModifiedBy.UserID);
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
        private static bool UpdateShareScreen(Database oDb, Entity.ShareScreen objShareScreen, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_SHARESCREEN);
                oDb.AddInParameter(cmd, "@PK_TemplateID", DbType.Int32, objShareScreen.TemplateID);
                oDb.AddInParameter(cmd, "@TemplateName", DbType.String, objShareScreen.TemplateName);
                oDb.AddInParameter(cmd, "@TemplateContent", DbType.String, objShareScreen.TemplateContent);
                oDb.AddInParameter(cmd, "@MobileNo1", DbType.String, objShareScreen.MobileNo1);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objShareScreen.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objShareScreen.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.ShareScreen UpdateShareScreen | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteShareScreen(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteShareScreen(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tShareScreen", "PK_TemplateID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteShareScreen(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_SHARESCREEN);
                oDb.AddInParameter(cmd, "@PK_TemplateID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.ShareScreen DeleteShareScreen | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
