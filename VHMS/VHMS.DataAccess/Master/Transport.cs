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
    public class Transport
    {
        public static Collection<Entity.Transport> GetTransport()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Transport> objList = new Collection<Entity.Transport>();
            Entity.Transport objTransport = new Entity.Transport();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TRANSPORT);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objTransport = new Entity.Transport();

                        objTransport.TransportID = Convert.ToInt32(drData["PK_TransportID"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.GSTNo = Convert.ToString(drData["GSTNo"]);
                        objTransport.Address = Convert.ToString(drData["Address"]);
                        objTransport.MobileNo1 = Convert.ToString(drData["MobileNo1"]);
                        objTransport.MobileNo2 = Convert.ToString(drData["MobileNo2"]);
                        objTransport.Area = Convert.ToString(drData["Area"]);
                        objTransport.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objTransport.VehicleNo = Convert.ToString(drData["Vehicle_No"]);

                        objList.Add(objTransport);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Transport GetTransport | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Transport> GetActiveTransport()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Transport> objList = new Collection<Entity.Transport>();
            Entity.Transport objTransport = new Entity.Transport();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TRANSPORT);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        if (Convert.ToBoolean(drData["IsActive"]))
                        {
                            objTransport = new Entity.Transport();

                            objTransport.TransportID = Convert.ToInt32(drData["PK_TransportID"]);
                            objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                            objTransport.GSTNo = Convert.ToString(drData["GSTNo"]);
                            objTransport.Address = Convert.ToString(drData["Address"]);
                            objTransport.MobileNo1 = Convert.ToString(drData["MobileNo1"]);
                            objTransport.MobileNo2 = Convert.ToString(drData["MobileNo2"]);
                            objTransport.Area = Convert.ToString(drData["Area"]);
                            objTransport.IsActive = Convert.ToBoolean(drData["IsActive"]);
                            objTransport.VehicleNo = Convert.ToString(drData["Vehicle_No"]);

                            objList.Add(objTransport);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Transport GetTransport | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Transport GetTransportByID(int iTransportID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Transport objTransport = new Entity.Transport();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TRANSPORT);
                db.AddInParameter(cmd, "@PK_TransportID", DbType.Int32, iTransportID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objTransport = new Entity.Transport();
                        objTransport.TransportID = Convert.ToInt32(drData["PK_TransportID"]);
                        objTransport.TransportName = Convert.ToString(drData["TransportName"]);
                        objTransport.GSTNo = Convert.ToString(drData["GSTNo"]);
                        objTransport.MobileNo1 = Convert.ToString(drData["MobileNo1"]);
                        objTransport.MobileNo2 = Convert.ToString(drData["MobileNo2"]);
                        objTransport.Area = Convert.ToString(drData["Area"]);
                        objTransport.Address = Convert.ToString(drData["Address"]);
                        objTransport.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objTransport.VehicleNo = Convert.ToString(drData["Vehicle_No"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Transport GetTransportByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objTransport;
        }
        public static int AddTransport(Entity.Transport objTransport)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddTransport(oDb, objTransport, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tTransport", "PK_TransportID", objTransport.TransportID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objTransport.CreatedBy.UserID);
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
        private static int AddTransport(Database oDb, Entity.Transport objTransport, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_TRANSPORT);
                oDb.AddOutParameter(cmd, "@PK_TransportID", DbType.Int32, objTransport.TransportID);
                oDb.AddInParameter(cmd, "@TransportName", DbType.String, objTransport.TransportName);
                oDb.AddInParameter(cmd, "@GSTNo", DbType.String, objTransport.GSTNo);
                oDb.AddInParameter(cmd, "@MobileNo1", DbType.String, objTransport.MobileNo1);
                oDb.AddInParameter(cmd, "@MobileNo2", DbType.String, objTransport.MobileNo2);
                oDb.AddInParameter(cmd, "@Area", DbType.String, objTransport.Area);
                oDb.AddInParameter(cmd, "@Address", DbType.String, objTransport.Address);
                oDb.AddInParameter(cmd, "@Vehicle_No", DbType.String, objTransport.VehicleNo);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objTransport.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objTransport.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_TransportID"));
                    objTransport.TransportID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Transport AddTransport | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateTransport(Entity.Transport objTransport)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateTransport(oDb, objTransport, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tTransport", "PK_TransportID", objTransport.TransportID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objTransport.ModifiedBy.UserID);
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
        private static bool UpdateTransport(Database oDb, Entity.Transport objTransport, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_TRANSPORT);
                oDb.AddInParameter(cmd, "@PK_TransportID", DbType.Int32, objTransport.TransportID);
                oDb.AddInParameter(cmd, "@TransportName", DbType.String, objTransport.TransportName);
                oDb.AddInParameter(cmd, "@GSTNo", DbType.String, objTransport.GSTNo);
                oDb.AddInParameter(cmd, "@Area", DbType.String, objTransport.Area);
                oDb.AddInParameter(cmd, "@MobileNo1", DbType.String, objTransport.MobileNo1);
                oDb.AddInParameter(cmd, "@MobileNo2", DbType.String, objTransport.MobileNo2);
                oDb.AddInParameter(cmd, "@Address", DbType.String, objTransport.Address);
                oDb.AddInParameter(cmd, "@Vehicle_No", DbType.String, objTransport.VehicleNo);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objTransport.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objTransport.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Transport UpdateTransport | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteTransport(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteTransport(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tTransport", "PK_TransportID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteTransport(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_TRANSPORT);
                oDb.AddInParameter(cmd, "@PK_TransportID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Transport DeleteTransport | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
