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
    public class LetterPad
    {
        public static Collection<Entity.LetterPad> GetLetterPad()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.LetterPad> objList = new Collection<Entity.LetterPad>();
            Entity.LetterPad objLetterPad = new Entity.LetterPad();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_LETTERPAD);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objLetterPad = new Entity.LetterPad();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objLetterPad.LetterPadID = Convert.ToInt32(drData["PK_LetterPadID"]);
                        objLetterPad.Commands = Convert.ToString(drData["Commands"]);

                        objList.Add(objLetterPad);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.LetterPad GetLetterPad | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.LetterPad> GetTopLetterPad()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.LetterPad> objList = new Collection<Entity.LetterPad>();
            Entity.LetterPad objLetterPad = new Entity.LetterPad();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPLETTERPAD);
               
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objLetterPad = new Entity.LetterPad();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objLetterPad.LetterPadID = Convert.ToInt32(drData["PK_LetterPadID"]);
                        objLetterPad.Commands = Convert.ToString(drData["Commands"]);

                        objList.Add(objLetterPad);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.LetterPad GetLetterPad | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.LetterPad GetLetterPadByID(int iLetterPadID)
        {
            string sException = string.Empty;
            Database db;
            Entity.LetterPad objLetterPad = new Entity.LetterPad();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_LETTERPAD);
                db.AddInParameter(cmd, "@PK_LetterPadID", DbType.Int32, iLetterPadID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objLetterPad = new Entity.LetterPad();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objLetterPad.LetterPadID = Convert.ToInt32(drData["PK_LetterPadID"]);
                        objLetterPad.Commands = Convert.ToString(drData["Commands"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.LetterPad GetLetterPadByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objLetterPad;
        }
        public static int AddLetterPad(Entity.LetterPad objLetterPad)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddLetterPad(oDb, objLetterPad, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tLetterPad", "PK_LetterPadID", objLetterPad.LetterPadID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objLetterPad.CreatedBy.UserID);
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
        private static int AddLetterPad(Database oDb, Entity.LetterPad objLetterPad, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_LETTERPAD);
                oDb.AddOutParameter(cmd, "@PK_LetterPadID", DbType.Int32, objLetterPad.LetterPadID);
                oDb.AddInParameter(cmd, "@Commands", DbType.String, objLetterPad.Commands);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objLetterPad.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_LetterPadID"));
                    objLetterPad.LetterPadID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.LetterPad AddLetterPad | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateLetterPad(Entity.LetterPad objLetterPad)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateLetterPad(oDb, objLetterPad, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tLetterPad", "PK_LetterPadID", objLetterPad.LetterPadID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objLetterPad.ModifiedBy.UserID);
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
        private static bool UpdateLetterPad(Database oDb, Entity.LetterPad objLetterPad, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_LETTERPAD);
                oDb.AddInParameter(cmd, "@PK_LetterPadID", DbType.Int32, objLetterPad.LetterPadID);
                oDb.AddInParameter(cmd, "@Commands", DbType.String, objLetterPad.Commands);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objLetterPad.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.LetterPad UpdateLetterPad | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteLetterPad(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteLetterPad(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tLetterPad", "PK_LetterPadID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteLetterPad(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_LETTERPAD);
                oDb.AddInParameter(cmd, "@PK_LetterPadID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.LetterPad DeleteLetterPad | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
