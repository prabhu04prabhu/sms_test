using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections.ObjectModel;
using VHMS;

namespace VHMS.DataAccess.Discharge
{
    public class HipReplacement
    {
        public static Collection<Entity.Discharge.HipReplacement> GetHipReplacement(int DischargeEntryID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Discharge.HipReplacement> objList = new Collection<Entity.Discharge.HipReplacement>();
            Entity.Discharge.HipReplacement objHipReplacement = new Entity.Discharge.HipReplacement();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_HIPREPLACEMENT);
                db.AddInParameter(cmd, "@FK_DischargeEntryID", DbType.Int32, DischargeEntryID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objHipReplacement = new Entity.Discharge.HipReplacement();
                        objHipReplacement.HipReplacementID = Convert.ToInt32(drData["PK_HipReplacementID"]);
                        objHipReplacement.DischargeEntryID = Convert.ToInt32(drData["FK_DischargeEntryID"]);
                        objHipReplacement.HipSurgeryName = Convert.ToString(drData["HipSurgeryName"]);
                        objHipReplacement.HipSurgeryDate = Convert.ToDateTime(drData["HipSurgeryDate"]);
                        objHipReplacement.sHipSurgeryDate = objHipReplacement.HipSurgeryDate.ToString("dd/MM/yyyy");
                        //Modified on 20-07-2017
                        //LEFT-
                        objHipReplacement.LHipImplant = Convert.ToString(drData["LHipImplant"]);
                        objHipReplacement.LAcetabulumCup = Convert.ToString(drData["LAcetabulumCup"]);
                        objHipReplacement.LLiner = Convert.ToString(drData["LLiner"]);
                        objHipReplacement.LFemoralStem = Convert.ToString(drData["LFemoralStem"]);
                        objHipReplacement.LFemoralHead = Convert.ToString(drData["LFemoralHead"]);

                        //Added on 20-07-2017
                        //RIGHT
                        objHipReplacement.RHipImplant = Convert.ToString(drData["RHipImplant"]);
                        objHipReplacement.RAcetabulumCup = Convert.ToString(drData["RAcetabulumCup"]);
                        objHipReplacement.RLiner = Convert.ToString(drData["RLiner"]);
                        objHipReplacement.RFemoralStem = Convert.ToString(drData["RFemoralStem"]);
                        objHipReplacement.RFemoralHead = Convert.ToString(drData["RFemoralHead"]);

                        objHipReplacement.HipSurgeryTypeID = Convert.ToInt16(drData["HipSurgeryTypeID"]);

                        //Added on 15-09-2017
                        //LEFT Title
                        objHipReplacement.LAcetabulumCupTitle = Convert.ToString(drData["LAcetabulumCupTitle"]);
                        objHipReplacement.LLinerTitle = Convert.ToString(drData["LLinerTitle"]);
                        objHipReplacement.LFemoralStemTitle = Convert.ToString(drData["LFemoralStemTitle"]);
                        objHipReplacement.LFemoralHeadTitle = Convert.ToString(drData["LFemoralHeadTitle"]);

                        //RIGHT Title
                        objHipReplacement.RAcetabulumCupTitle = Convert.ToString(drData["RAcetabulumCupTitle"]);
                        objHipReplacement.RLinerTitle = Convert.ToString(drData["RLinerTitle"]);
                        objHipReplacement.RFemoralStemTitle = Convert.ToString(drData["RFemoralStemTitle"]);
                        objHipReplacement.RFemoralHeadTitle = Convert.ToString(drData["RFemoralHeadTitle"]);
                        objList.Add(objHipReplacement);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.HipReplacement GetHipReplacement | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static bool AddHipReplacement(Entity.Discharge.HipReplacement objHipReplacement)
        {
            bool IsInserted = true;
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddHipReplacement(oDb, objHipReplacement, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tHipReplacement", "PK_HipReplacementID", objHipReplacement.HipReplacementID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objHipReplacement.CreatedBy.UserID);
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
            return IsInserted;
        }
        private static int AddHipReplacement(Database oDb, Entity.Discharge.HipReplacement objHipReplacement, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_HIPREPLACEMENT);
                oDb.AddOutParameter(cmd, "@PK_HipReplacementID", DbType.Int32, objHipReplacement.HipReplacementID);
                oDb.AddInParameter(cmd, "@FK_DischargeEntryID", DbType.Int32, objHipReplacement.DischargeEntryID);
                oDb.AddInParameter(cmd, "@HipSurgeryName", DbType.String, objHipReplacement.HipSurgeryName);
                oDb.AddInParameter(cmd, "@HipSurgeryDate", DbType.String, objHipReplacement.sHipSurgeryDate);
                //Modified Column Name as Left on 20-07-2017
                oDb.AddInParameter(cmd, "@LHipImplant", DbType.String, objHipReplacement.LHipImplant);
                oDb.AddInParameter(cmd, "@LAcetabulumCup", DbType.String, objHipReplacement.LAcetabulumCup);
                oDb.AddInParameter(cmd, "@LLiner", DbType.String, objHipReplacement.LLiner);
                oDb.AddInParameter(cmd, "@LFemoralStem", DbType.String, objHipReplacement.LFemoralStem);
                oDb.AddInParameter(cmd, "@LFemoralHead", DbType.String, objHipReplacement.LFemoralHead);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objHipReplacement.CreatedBy.UserID);
                //Right - Added on 20-07-2017
                oDb.AddInParameter(cmd, "@RHipImplant", DbType.String, objHipReplacement.RHipImplant);
                oDb.AddInParameter(cmd, "@RAcetabulumCup", DbType.String, objHipReplacement.RAcetabulumCup);
                oDb.AddInParameter(cmd, "@RLiner", DbType.String, objHipReplacement.RLiner);
                oDb.AddInParameter(cmd, "@RFemoralStem", DbType.String, objHipReplacement.RFemoralStem);
                oDb.AddInParameter(cmd, "@RFemoralHead", DbType.String, objHipReplacement.RFemoralHead);

                oDb.AddInParameter(cmd, "@HipSurgeryTypeID", DbType.Int16, objHipReplacement.HipSurgeryTypeID);
                
                //Added on 15-09-2017
                //LEFT
                oDb.AddInParameter(cmd, "@LAcetabulumCupTitle", DbType.String, objHipReplacement.LAcetabulumCupTitle);
                oDb.AddInParameter(cmd, "@LLinerTitle", DbType.String, objHipReplacement.LLinerTitle);
                oDb.AddInParameter(cmd, "@LFemoralStemTitle", DbType.String, objHipReplacement.LFemoralStemTitle);
                oDb.AddInParameter(cmd, "@LFemoralHeadTitle", DbType.String, objHipReplacement.LFemoralHeadTitle);
                //RIGHT
                oDb.AddInParameter(cmd, "@RAcetabulumCupTitle", DbType.String, objHipReplacement.RAcetabulumCupTitle);
                oDb.AddInParameter(cmd, "@RLinerTitle", DbType.String, objHipReplacement.RLinerTitle);
                oDb.AddInParameter(cmd, "@RFemoralStemTitle", DbType.String, objHipReplacement.RFemoralStemTitle);
                oDb.AddInParameter(cmd, "@RFemoralHeadTitle", DbType.String, objHipReplacement.RFemoralHeadTitle);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_HipReplacementID"));
                    objHipReplacement.HipReplacementID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.HipReplacement AddHipReplacement | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateHipReplacement(Entity.Discharge.HipReplacement objHipReplacement)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateHipReplacement(oDb, objHipReplacement, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tHipReplacement", "PK_HipReplacementID", objHipReplacement.HipReplacementID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objHipReplacement.ModifiedBy.UserID);
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
        private static bool UpdateHipReplacement(Database oDb, Entity.Discharge.HipReplacement objHipReplacement, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_HIPREPLACEMENT);
                oDb.AddInParameter(cmd, "@PK_HipReplacementID", DbType.Int32, objHipReplacement.HipReplacementID);
                oDb.AddInParameter(cmd, "@FK_DischargeEntryID", DbType.Int32, objHipReplacement.DischargeEntryID);
                oDb.AddInParameter(cmd, "@HipSurgeryName", DbType.String, objHipReplacement.HipSurgeryName);
                oDb.AddInParameter(cmd, "@HipSurgeryDate", DbType.String, objHipReplacement.sHipSurgeryDate);
                //Modified column name as LEFT on 20-07-2017
                oDb.AddInParameter(cmd, "@LHipImplant", DbType.String, objHipReplacement.LHipImplant);
                oDb.AddInParameter(cmd, "@LAcetabulumCup", DbType.String, objHipReplacement.LAcetabulumCup);
                oDb.AddInParameter(cmd, "@LLiner", DbType.String, objHipReplacement.LLiner);
                oDb.AddInParameter(cmd, "@LFemoralStem", DbType.String, objHipReplacement.LFemoralStem);
                oDb.AddInParameter(cmd, "@LFemoralHead", DbType.String, objHipReplacement.LFemoralHead);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objHipReplacement.ModifiedBy.UserID);
                //Right - Added on 20-07-2017
                oDb.AddInParameter(cmd, "@RHipImplant", DbType.String, objHipReplacement.RHipImplant);
                oDb.AddInParameter(cmd, "@RAcetabulumCup", DbType.String, objHipReplacement.RAcetabulumCup);
                oDb.AddInParameter(cmd, "@RLiner", DbType.String, objHipReplacement.RLiner);
                oDb.AddInParameter(cmd, "@RFemoralStem", DbType.String, objHipReplacement.RFemoralStem);
                oDb.AddInParameter(cmd, "@RFemoralHead", DbType.String, objHipReplacement.RFemoralHead);

                oDb.AddInParameter(cmd, "@HipSurgeryTypeID", DbType.Int16, objHipReplacement.HipSurgeryTypeID);
                //Added on 15-09-2017
                //LEFT
                oDb.AddInParameter(cmd, "@LAcetabulumCupTitle", DbType.String, objHipReplacement.LAcetabulumCupTitle);
                oDb.AddInParameter(cmd, "@LLinerTitle", DbType.String, objHipReplacement.LLinerTitle);
                oDb.AddInParameter(cmd, "@LFemoralStemTitle", DbType.String, objHipReplacement.LFemoralStemTitle);
                oDb.AddInParameter(cmd, "@LFemoralHeadTitle", DbType.String, objHipReplacement.LFemoralHeadTitle);
                //RIGHT
                oDb.AddInParameter(cmd, "@RAcetabulumCupTitle", DbType.String, objHipReplacement.RAcetabulumCupTitle);
                oDb.AddInParameter(cmd, "@RLinerTitle", DbType.String, objHipReplacement.RLinerTitle);
                oDb.AddInParameter(cmd, "@RFemoralStemTitle", DbType.String, objHipReplacement.RFemoralStemTitle);
                oDb.AddInParameter(cmd, "@RFemoralHeadTitle", DbType.String, objHipReplacement.RFemoralHeadTitle);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.HipReplacement UpdateHipReplacement | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteHipReplacement(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteHipReplacement(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tHipReplacement", "PK_HipReplacementID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteHipReplacement(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_HIPREPLACEMENT);
                oDb.AddInParameter(cmd, "@PK_HipReplacementID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.HipReplacement DeleteHipReplacement | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
