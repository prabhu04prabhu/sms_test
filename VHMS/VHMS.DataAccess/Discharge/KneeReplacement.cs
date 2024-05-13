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
    public class KneeReplacement
    {
        public static Collection<Entity.Discharge.KneeReplacement> GetKneeReplacement(int DischargeEntryID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Discharge.KneeReplacement> objList = new Collection<Entity.Discharge.KneeReplacement>();
            Entity.Discharge.KneeReplacement objKneeReplacement = new Entity.Discharge.KneeReplacement();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_KNEEREPLACEMENT);
                db.AddInParameter(cmd, "@FK_DischargeEntryID", DbType.Int32, DischargeEntryID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objKneeReplacement = new Entity.Discharge.KneeReplacement();
                        objKneeReplacement.KneeReplacementID = Convert.ToInt32(drData["PK_KneeReplacementID"]);
                        objKneeReplacement.DischargeEntryID = Convert.ToInt32(drData["FK_DischargeEntryID"]);
                        objKneeReplacement.KneeSurgeryTypeID = Convert.ToInt32(drData["KneeSurgeryTypeID"]);
                        objKneeReplacement.KneeSurgeryName = Convert.ToString(drData["KneeSurgeryName"]);
                        objKneeReplacement.KneeSurgeryDate = Convert.ToDateTime(drData["KneeSurgeryDate"]);
                        objKneeReplacement.sKneeSurgeryDate = objKneeReplacement.KneeSurgeryDate.ToString("dd/MM/yyyy");
                        //Modified on 20-07-2017
                        objKneeReplacement.LKneeImplant = Convert.ToString(drData["LKneeImplant"]);
                        objKneeReplacement.LFemur = Convert.ToString(drData["LFemur"]);
                        objKneeReplacement.LTibia = Convert.ToString(drData["LTibia"]);
                        objKneeReplacement.LPoly = Convert.ToString(drData["LPoly"]);
                        objKneeReplacement.LStem = Convert.ToString(drData["LStem"]);
                        //Added on 20-07-2017
                        objKneeReplacement.RKneeImplant = Convert.ToString(drData["RKneeImplant"]);
                        objKneeReplacement.RFemur = Convert.ToString(drData["RFemur"]);
                        objKneeReplacement.RTibia = Convert.ToString(drData["RTibia"]);
                        objKneeReplacement.RPoly = Convert.ToString(drData["RPoly"]);
                        objKneeReplacement.RStem = Convert.ToString(drData["RStem"]);

                        //Added on 15-09-2017
                        //LEFT Title
                        objKneeReplacement.LFemurTitle = Convert.ToString(drData["LFemurTitle"]);
                        objKneeReplacement.LTibiaTitle = Convert.ToString(drData["LTibiaTitle"]);
                        objKneeReplacement.LPolyTitle = Convert.ToString(drData["LPolyTitle"]);
                        objKneeReplacement.LStemTitle = Convert.ToString(drData["LStemTitle"]);
                        //RIGHT Title
                        objKneeReplacement.RFemurTitle = Convert.ToString(drData["RFemurTitle"]);
                        objKneeReplacement.RTibiaTitle = Convert.ToString(drData["RTibiaTitle"]);
                        objKneeReplacement.RPolyTitle = Convert.ToString(drData["RPolyTitle"]);
                        objKneeReplacement.RStemTitle = Convert.ToString(drData["RStemTitle"]);

                        objList.Add(objKneeReplacement);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.KneeReplacement GetKneeReplacement | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static bool AddKneeReplacement(Entity.Discharge.KneeReplacement objKneeReplacement)
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
                    ID = AddKneeReplacement(oDb, objKneeReplacement, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tKneeReplacement", "PK_KneeReplacementID", objKneeReplacement.KneeReplacementID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objKneeReplacement.CreatedBy.UserID);
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
        private static int AddKneeReplacement(Database oDb, Entity.Discharge.KneeReplacement objKneeReplacement, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_KNEEREPLACEMENT);
                oDb.AddOutParameter(cmd, "@PK_KneeReplacementID", DbType.Int32, objKneeReplacement.KneeReplacementID);
                oDb.AddInParameter(cmd, "@KneeSurgeryTypeID", DbType.Int32, objKneeReplacement.KneeSurgeryTypeID);
                oDb.AddInParameter(cmd, "@FK_DischargeEntryID", DbType.Int32, objKneeReplacement.DischargeEntryID);
                oDb.AddInParameter(cmd, "@KneeSurgeryName", DbType.String, objKneeReplacement.KneeSurgeryName);
                oDb.AddInParameter(cmd, "@KneeSurgeryDate", DbType.String, objKneeReplacement.sKneeSurgeryDate);
                //Modified on 20-07-2017
                oDb.AddInParameter(cmd, "@LKneeImplant", DbType.String, objKneeReplacement.LKneeImplant);
                oDb.AddInParameter(cmd, "@LFemur", DbType.String, objKneeReplacement.LFemur);
                oDb.AddInParameter(cmd, "@LTibia", DbType.String, objKneeReplacement.LTibia);
                oDb.AddInParameter(cmd, "@LPoly", DbType.String, objKneeReplacement.LPoly);
                oDb.AddInParameter(cmd, "@LStem", DbType.String, objKneeReplacement.LStem);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objKneeReplacement.CreatedBy.UserID);
                //Added on 20-07-2017
                oDb.AddInParameter(cmd, "@RKneeImplant", DbType.String, objKneeReplacement.RKneeImplant);
                oDb.AddInParameter(cmd, "@RFemur", DbType.String, objKneeReplacement.RFemur);
                oDb.AddInParameter(cmd, "@RTibia", DbType.String, objKneeReplacement.RTibia);
                oDb.AddInParameter(cmd, "@RPoly", DbType.String, objKneeReplacement.RPoly);
                oDb.AddInParameter(cmd, "@RStem", DbType.String, objKneeReplacement.RStem);

                //Added on 15-09-2017
                //LEFT Title
                oDb.AddInParameter(cmd, "@LFemurTitle", DbType.String, objKneeReplacement.LFemurTitle);
                oDb.AddInParameter(cmd, "@LTibiaTitle", DbType.String, objKneeReplacement.LTibiaTitle);
                oDb.AddInParameter(cmd, "@LPolyTitle", DbType.String, objKneeReplacement.LPolyTitle);
                oDb.AddInParameter(cmd, "@LStemTitle", DbType.String, objKneeReplacement.LStemTitle);
                //RIGHT Title
                oDb.AddInParameter(cmd, "@RFemurTitle", DbType.String, objKneeReplacement.RFemurTitle);
                oDb.AddInParameter(cmd, "@RTibiaTitle", DbType.String, objKneeReplacement.RTibiaTitle);
                oDb.AddInParameter(cmd, "@RPolyTitle", DbType.String, objKneeReplacement.RPolyTitle);
                oDb.AddInParameter(cmd, "@RStemTitle", DbType.String, objKneeReplacement.RStemTitle);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_KneeReplacementID"));
                    objKneeReplacement.KneeReplacementID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.KneeReplacement AddKneeReplacement | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateKneeReplacement(Entity.Discharge.KneeReplacement objKneeReplacement)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateKneeReplacement(oDb, objKneeReplacement, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tKneeReplacement", "PK_KneeReplacementID", objKneeReplacement.KneeReplacementID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objKneeReplacement.ModifiedBy.UserID);
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
        private static bool UpdateKneeReplacement(Database oDb, Entity.Discharge.KneeReplacement objKneeReplacement, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_KNEEREPLACEMENT);
                oDb.AddInParameter(cmd, "@PK_KneeReplacementID", DbType.Int32, objKneeReplacement.KneeReplacementID);
                oDb.AddInParameter(cmd, "@KneeSurgeryTypeID", DbType.Int32, objKneeReplacement.KneeSurgeryTypeID);
                oDb.AddInParameter(cmd, "@FK_DischargeEntryID", DbType.Int32, objKneeReplacement.DischargeEntryID);
                oDb.AddInParameter(cmd, "@KneeSurgeryName", DbType.String, objKneeReplacement.KneeSurgeryName);
                oDb.AddInParameter(cmd, "@KneeSurgeryDate", DbType.String, objKneeReplacement.sKneeSurgeryDate);
                //Modified on 20-07-2017
                oDb.AddInParameter(cmd, "@LKneeImplant", DbType.String, objKneeReplacement.LKneeImplant);
                oDb.AddInParameter(cmd, "@LFemur", DbType.String, objKneeReplacement.LFemur);
                oDb.AddInParameter(cmd, "@LTibia", DbType.String, objKneeReplacement.LTibia);
                oDb.AddInParameter(cmd, "@LPoly", DbType.String, objKneeReplacement.LPoly);
                oDb.AddInParameter(cmd, "@LStem", DbType.String, objKneeReplacement.LStem);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objKneeReplacement.ModifiedBy.UserID);
                //Added on 20-07-2017
                oDb.AddInParameter(cmd, "@RKneeImplant", DbType.String, objKneeReplacement.RKneeImplant);
                oDb.AddInParameter(cmd, "@RFemur", DbType.String, objKneeReplacement.RFemur);
                oDb.AddInParameter(cmd, "@RTibia", DbType.String, objKneeReplacement.RTibia);
                oDb.AddInParameter(cmd, "@RPoly", DbType.String, objKneeReplacement.RPoly);
                oDb.AddInParameter(cmd, "@RStem", DbType.String, objKneeReplacement.RStem);

                //Added on 15-09-2017
                //LEFT Title
                oDb.AddInParameter(cmd, "@LFemurTitle", DbType.String, objKneeReplacement.LFemurTitle);
                oDb.AddInParameter(cmd, "@LTibiaTitle", DbType.String, objKneeReplacement.LTibiaTitle);
                oDb.AddInParameter(cmd, "@LPolyTitle", DbType.String, objKneeReplacement.LPolyTitle);
                oDb.AddInParameter(cmd, "@LStemTitle", DbType.String, objKneeReplacement.LStemTitle);
                //RIGHT Title
                oDb.AddInParameter(cmd, "@RFemurTitle", DbType.String, objKneeReplacement.RFemurTitle);
                oDb.AddInParameter(cmd, "@RTibiaTitle", DbType.String, objKneeReplacement.RTibiaTitle);
                oDb.AddInParameter(cmd, "@RPolyTitle", DbType.String, objKneeReplacement.RPolyTitle);
                oDb.AddInParameter(cmd, "@RStemTitle", DbType.String, objKneeReplacement.RStemTitle);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.KneeReplacement UpdateKneeReplacement | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteKneeReplacement(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteKneeReplacement(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tKneeReplacement", "PK_KneeReplacementID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteKneeReplacement(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_KNEEREPLACEMENT);
                oDb.AddInParameter(cmd, "@PK_KneeReplacementID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.KneeReplacement DeleteKneeReplacement | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
