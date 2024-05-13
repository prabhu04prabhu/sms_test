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
    public class Section
    {
        public static Collection<Entity.Section> GetSection()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Section> objList = new Collection<Entity.Section>();
            Entity.Section objSection = new Entity.Section();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SECTION);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSection = new Entity.Section();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objSection.SectionID = Convert.ToInt32(drData["PK_SectionID"]);
                        objSection.SectionName = Convert.ToString(drData["SectionName"]);
                        objSection.SectionCode = Convert.ToString(drData["SectionCode"]);
                        objSection.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objSection);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Section GetSection | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Section GetSectionByID(int iSectionID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Section objSection = new Entity.Section();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SECTION);
                db.AddInParameter(cmd, "@PK_SectionID", DbType.Int32, iSectionID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSection = new Entity.Section();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objSection.SectionID = Convert.ToInt32(drData["PK_SectionID"]);
                        objSection.SectionName = Convert.ToString(drData["SectionName"]);
                        objSection.SectionCode = Convert.ToString(drData["SectionCode"]);
                        objSection.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Section GetSectionByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSection;
        }
        public static int AddSection(Entity.Section objSection)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddSection(oDb, objSection, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tSection", "PK_SectionID", objSection.SectionID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objSection.CreatedBy.UserID);
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
        private static int AddSection(Database oDb, Entity.Section objSection, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_SECTION);
                oDb.AddOutParameter(cmd, "@PK_SectionID", DbType.Int32, objSection.SectionID);
                oDb.AddInParameter(cmd, "@SectionName", DbType.String, objSection.SectionName);
                oDb.AddInParameter(cmd, "@SectionCode", DbType.String, objSection.SectionCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objSection.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objSection.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_SectionID"));
                    objSection.SectionID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Section AddSection | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateSection(Entity.Section objSection)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateSection(oDb, objSection, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tSection", "PK_SectionID", objSection.SectionID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objSection.ModifiedBy.UserID);
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
        private static bool UpdateSection(Database oDb, Entity.Section objSection, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_SECTION);
                oDb.AddInParameter(cmd, "@PK_SectionID", DbType.Int32, objSection.SectionID);
                oDb.AddInParameter(cmd, "@SectionName", DbType.String, objSection.SectionName);
                oDb.AddInParameter(cmd, "@SectionCode", DbType.String, objSection.SectionCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objSection.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objSection.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Section UpdateSection | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteSection(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteSection(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tSection", "PK_SectionID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteSection(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_SECTION);
                oDb.AddInParameter(cmd, "@PK_SectionID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Section DeleteSection | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
