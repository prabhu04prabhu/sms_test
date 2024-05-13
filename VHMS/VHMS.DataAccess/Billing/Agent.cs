using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections.ObjectModel;
using VHMS;

namespace VHMS.DataAccess.Billing
{
    public class Agent
    {
        public static Collection<Entity.Billing.Agent> GetAgent()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Agent> objList = new Collection<Entity.Billing.Agent>();
            Entity.Billing.Agent objAgent = new Entity.Billing.Agent();
            Entity.State objState = new Entity.State();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_AGENTMASTER);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objAgent = new Entity.Billing.Agent();

                        objAgent.AgentID = Convert.ToInt32(drData["PK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.AgentAddress = Convert.ToString(drData["AgentAddress"]);

                        objState = new Entity.State();
                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objState.StateName = Convert.ToString(drData["StateName"]);
                        objAgent.State = objState;

                        objAgent.City = Convert.ToString(drData["City"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objAgent.PhoneNo2 = Convert.ToString(drData["PhoneNo2"]);
                        objAgent.WhatsAppNo = Convert.ToString(drData["WhatsAppNo"]);
                        objAgent.EmailID = Convert.ToString(drData["EmailID"]);
                        objAgent.City = Convert.ToString(drData["City"]);
                        objAgent.BankName = Convert.ToString(drData["BankName"]);
                        objAgent.BranchName = Convert.ToString(drData["BranchName"]);
                        objAgent.IFSCCode = Convert.ToString(drData["IFSCCode"]);
                        objAgent.AccountNo = Convert.ToString(drData["AccountNo"]);
                        objAgent.AccountHolderName = Convert.ToString(drData["AccountHolderName"]);
                        objAgent.CommissionType = Convert.ToString(drData["CommissionType"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.CommissionAmount = Convert.ToString(drData["CommissionAmount"]);
                        objAgent.AadharNo = Convert.ToString(drData["AadharNo"]);
                        objAgent.PanNo = Convert.ToString(drData["PanNo"]);
                        objAgent.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objAgent);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Agent GetAgentMaster | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Agent> GetActiveAgent()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Agent> objList = new Collection<Entity.Billing.Agent>();
            Entity.Billing.Agent objAgent = new Entity.Billing.Agent();
            Entity.State objState = new Entity.State();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_AGENTMASTER);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        if (Convert.ToBoolean(drData["IsActive"]))
                        {
                            objAgent = new Entity.Billing.Agent();

                            objAgent.AgentID = Convert.ToInt32(drData["PK_AgentID"]);
                            objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                            objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                            objAgent.AgentAddress = Convert.ToString(drData["AgentAddress"]);

                            objState = new Entity.State();
                            objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                            objState.StateName = Convert.ToString(drData["StateName"]);
                            objAgent.State = objState;

                            objAgent.City = Convert.ToString(drData["City"]);
                            objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                            objAgent.PhoneNo2 = Convert.ToString(drData["PhoneNo2"]);
                            objAgent.WhatsAppNo = Convert.ToString(drData["WhatsAppNo"]);
                            objAgent.EmailID = Convert.ToString(drData["EmailID"]);
                            objAgent.City = Convert.ToString(drData["City"]);
                            objAgent.BankName = Convert.ToString(drData["BankName"]);
                            objAgent.BranchName = Convert.ToString(drData["BranchName"]);
                            objAgent.IFSCCode = Convert.ToString(drData["IFSCCode"]);
                            objAgent.AccountNo = Convert.ToString(drData["AccountNo"]);
                            objAgent.AccountHolderName = Convert.ToString(drData["AccountHolderName"]);
                            objAgent.CommissionType = Convert.ToString(drData["CommissionType"]);
                            objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                            objAgent.CommissionAmount = Convert.ToString(drData["CommissionAmount"]);
                            objAgent.AadharNo = Convert.ToString(drData["AadharNo"]);
                            objAgent.PanNo = Convert.ToString(drData["PanNo"]);
                            objAgent.IsActive = Convert.ToBoolean(drData["IsActive"]);

                            objList.Add(objAgent);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Agent GetAgentMaster | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.Agent GetAgentByID(int iAgentID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Agent objAgent = new Entity.Billing.Agent();
            Entity.State objState = new Entity.State();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_AGENTMASTER);
                db.AddInParameter(cmd, "@PK_AgentID", DbType.Int32, iAgentID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objAgent = new Entity.Billing.Agent();

                        objAgent.AgentID = Convert.ToInt32(drData["PK_AgentID"]);
                        objAgent.AgentName = Convert.ToString(drData["AgentName"]);
                        objAgent.AgentCode = Convert.ToString(drData["AgentCode"]);
                        objAgent.AgentAddress = Convert.ToString(drData["AgentAddress"]);

                        objState = new Entity.State();
                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objState.StateName = Convert.ToString(drData["StateName"]);
                        objAgent.State = objState;

                        objAgent.City = Convert.ToString(drData["City"]);
                        objAgent.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objAgent.PhoneNo2 = Convert.ToString(drData["PhoneNo2"]);
                        objAgent.WhatsAppNo = Convert.ToString(drData["WhatsAppNo"]);
                        objAgent.EmailID = Convert.ToString(drData["EmailID"]);
                        objAgent.City = Convert.ToString(drData["City"]);
                        objAgent.BankName = Convert.ToString(drData["BankName"]);
                        objAgent.BranchName = Convert.ToString(drData["BranchName"]);
                        objAgent.IFSCCode = Convert.ToString(drData["IFSCCode"]);
                        objAgent.AccountNo = Convert.ToString(drData["AccountNo"]);
                        objAgent.AccountHolderName = Convert.ToString(drData["AccountHolderName"]);
                        objAgent.CommissionType = Convert.ToString(drData["CommissionType"]);
                        objAgent.CommissionPercentage = Convert.ToString(drData["CommissionPercentage"]);
                        objAgent.CommissionAmount = Convert.ToString(drData["CommissionAmount"]);
                        objAgent.AadharNo = Convert.ToString(drData["AadharNo"]);
                        objAgent.PanNo = Convert.ToString(drData["PanNo"]);
                        objAgent.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Agent GetAgentMasterByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objAgent;
        }
        public static int AddAgent(Entity.Billing.Agent objAgent)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddAgent(oDb, objAgent, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tAgentMaster", "PK_AgentID", objAgent.AgentID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objAgent.CreatedBy.UserID);
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
        private static int AddAgent(Database oDb, Entity.Billing.Agent objAgent, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_AGENTMASTER);
                oDb.AddOutParameter(cmd, "@PK_AgentID", DbType.Int32, objAgent.AgentID);
                oDb.AddInParameter(cmd, "@AgentName", DbType.String, objAgent.AgentName);
                oDb.AddInParameter(cmd, "@AgentCode", DbType.String, objAgent.AgentCode);
                oDb.AddInParameter(cmd, "@AgentAddress", DbType.String, objAgent.AgentAddress);
                oDb.AddInParameter(cmd, "@PhoneNo1", DbType.String, objAgent.PhoneNo1);
                oDb.AddInParameter(cmd, "@PhoneNo2", DbType.String, objAgent.PhoneNo2);
                oDb.AddInParameter(cmd, "@WhatsAppNo", DbType.String, objAgent.WhatsAppNo);
                oDb.AddInParameter(cmd, "@EmailID", DbType.String, objAgent.EmailID);
                oDb.AddInParameter(cmd, "@City", DbType.String, objAgent.City);
                oDb.AddInParameter(cmd, "@BankName", DbType.String, objAgent.BankName);
                oDb.AddInParameter(cmd, "@BranchName", DbType.String, objAgent.BranchName);
                oDb.AddInParameter(cmd, "@FK_StateID", DbType.Int32, objAgent.State.StateID);
                oDb.AddInParameter(cmd, "@IFSCCode", DbType.String, objAgent.IFSCCode);
                oDb.AddInParameter(cmd, "@AccountNo", DbType.String, objAgent.AccountNo);
                oDb.AddInParameter(cmd, "@AccountHolderName", DbType.String, objAgent.AccountHolderName);
                oDb.AddInParameter(cmd, "@CommissionType", DbType.String, objAgent.CommissionType);
                oDb.AddInParameter(cmd, "@CommissionPercentage", DbType.Decimal, objAgent.CommissionPercentage);
                oDb.AddInParameter(cmd, "@CommissionAmount", DbType.Decimal, objAgent.CommissionAmount);
                oDb.AddInParameter(cmd, "@AadharNo", DbType.String, objAgent.AadharNo);
                oDb.AddInParameter(cmd, "@PanNo", DbType.String, objAgent.PanNo);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objAgent.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objAgent.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_AgentID"));
                    objAgent.AgentID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "Agent AddAgent | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateAgent(Entity.Billing.Agent objAgent)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateAgent(oDb, objAgent, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tAgentMaster", "PK_AgentID", objAgent.AgentID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objAgent.ModifiedBy.UserID);
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
        private static bool UpdateAgent(Database oDb, Entity.Billing.Agent objAgent, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_AGENTMASTER);
                oDb.AddInParameter(cmd, "@PK_AgentID", DbType.Int32, objAgent.AgentID);
                oDb.AddInParameter(cmd, "@AgentName", DbType.String, objAgent.AgentName);
                oDb.AddInParameter(cmd, "@AgentCode", DbType.String, objAgent.AgentCode);
                oDb.AddInParameter(cmd, "@AgentAddress", DbType.String, objAgent.AgentAddress);
                oDb.AddInParameter(cmd, "@PhoneNo1", DbType.String, objAgent.PhoneNo1);
                oDb.AddInParameter(cmd, "@PhoneNo2", DbType.String, objAgent.PhoneNo2);
                oDb.AddInParameter(cmd, "@WhatsAppNo", DbType.String, objAgent.WhatsAppNo);
                oDb.AddInParameter(cmd, "@EmailID", DbType.String, objAgent.EmailID);
                oDb.AddInParameter(cmd, "@City", DbType.String, objAgent.City);
                oDb.AddInParameter(cmd, "@BankName", DbType.String, objAgent.BankName);
                oDb.AddInParameter(cmd, "@BranchName", DbType.String, objAgent.BranchName);
                oDb.AddInParameter(cmd, "@FK_StateID", DbType.Int32, objAgent.State.StateID);
                oDb.AddInParameter(cmd, "@IFSCCode", DbType.String, objAgent.IFSCCode);
                oDb.AddInParameter(cmd, "@AccountNo", DbType.String, objAgent.AccountNo);
                oDb.AddInParameter(cmd, "@AccountHolderName", DbType.String, objAgent.AccountHolderName);
                oDb.AddInParameter(cmd, "@CommissionType", DbType.String, objAgent.CommissionType);
                oDb.AddInParameter(cmd, "@CommissionPercentage", DbType.Decimal, objAgent.CommissionPercentage);
                oDb.AddInParameter(cmd, "@CommissionAmount", DbType.Decimal, objAgent.CommissionAmount);
                oDb.AddInParameter(cmd, "@AadharNo", DbType.String, objAgent.AadharNo);
                oDb.AddInParameter(cmd, "@PanNo", DbType.String, objAgent.PanNo);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objAgent.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objAgent.ModifiedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Agent UpdateAgent| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteAgent(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteAgent(oDb, ID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tAgentMaster", "PK_AgentID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteAgent(Database oDb, int ID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_AGENTMASTER);
                oDb.AddInParameter(cmd, "@PK_AgentID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Agent DeleteAgent | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
