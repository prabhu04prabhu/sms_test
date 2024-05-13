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
    public class Drug
    {
        public static Collection<Entity.Discharge.Drug> GetDrug()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Discharge.Drug> objList = new Collection<Entity.Discharge.Drug>();
            Entity.Discharge.Drug objDrug = new Entity.Discharge.Drug();
            Entity.Discharge.Dosage oDosage;
            Entity.Discharge.Duration oDuration;
            Entity.Discharge.Frequency oFrequency;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DRUG);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDrug = new Entity.Discharge.Drug();
                        oDosage = new Entity.Discharge.Dosage();
                        oDuration = new Entity.Discharge.Duration();
                        oFrequency = new Entity.Discharge.Frequency();

                        objDrug.DrugID = Convert.ToInt32(drData["PK_DrugID"]);
                        objDrug.DrugName = Convert.ToString(drData["DrugName"]);
                        objDrug.DrugCode = Convert.ToString(drData["DrugCode"]);                        
                        objDrug.Instruction = Convert.ToString(drData["Instruction"]);
                        objDrug.Ingredient = Convert.ToString(drData["Ingredient"]);
                        objDrug.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        //Added on 28-07-2017
                        objDrug.InstructionID = Convert.ToInt16(drData["InstructionID"]);

                        //Added on 01-09-2017
                        oDosage.DosageID = Convert.ToInt32(drData["FK_DosageID"]);
                        oDosage.DosageName = Convert.ToString(drData["DosageName"]);
                        objDrug.Dosage = oDosage;

                        oDuration.DurationID = Convert.ToInt32(drData["FK_DurationID"]);
                        oDuration.DurationName = Convert.ToString(drData["DurationName"]);
                        objDrug.Duration = oDuration;

                        //Added on 24-10-2017
                        oFrequency.FrequencyID = Convert.ToInt32(drData["FK_FrequencyID"]);
                        oFrequency.FrequencyName = Convert.ToString(drData["FrequencyName"]);
                        objDrug.Frequency = oFrequency;

                        objList.Add(objDrug);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Drug GetDrug | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Discharge.Drug GetDrugByID(int iDrugID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Discharge.Drug objDrug = new Entity.Discharge.Drug();
            Entity.Discharge.Dosage oDosage;
            Entity.Discharge.Duration oDuration;
            Entity.Discharge.Frequency oFrequency;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DRUG);
                db.AddInParameter(cmd, "@PK_DrugID", DbType.Int32, iDrugID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDrug = new Entity.Discharge.Drug();
                        oDosage = new Entity.Discharge.Dosage();
                        oDuration = new Entity.Discharge.Duration();

                        objDrug.DrugID = Convert.ToInt32(drData["PK_DrugID"]);
                        objDrug.DrugName = Convert.ToString(drData["DrugName"]);
                        objDrug.DrugCode = Convert.ToString(drData["DrugCode"]);                        
                        objDrug.Instruction = Convert.ToString(drData["Instruction"]);
                        objDrug.Ingredient = Convert.ToString(drData["Ingredient"]);
                        objDrug.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        //Added on 28-07-2017
                        objDrug.InstructionID = Convert.ToInt16(drData["InstructionID"]);
                        //Added on 01-09-2017
                        oDosage.DosageID = Convert.ToInt32(drData["FK_DosageID"]);
                        oDosage.DosageName = Convert.ToString(drData["DosageName"]);
                        objDrug.Dosage = oDosage;

                        oDuration.DurationID = Convert.ToInt32(drData["FK_DurationID"]);
                        oDuration.DurationName = Convert.ToString(drData["DurationName"]);
                        objDrug.Duration = oDuration;

                        //Added on 24-10-2017
                        oFrequency = new Entity.Discharge.Frequency();
                        oFrequency.FrequencyID = Convert.ToInt32(drData["FK_FrequencyID"]);
                        oFrequency.FrequencyName = Convert.ToString(drData["FrequencyName"]);
                        objDrug.Frequency = oFrequency;
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Drug GetDrugByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objDrug;
        }
        public static int AddDrug(Entity.Discharge.Drug objDrug)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddDrug(oDb, objDrug, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tDrug", "PK_DrugID", objDrug.DrugID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objDrug.CreatedBy.UserID);
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
        private static int AddDrug(Database oDb, Entity.Discharge.Drug objDrug, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_DRUG);
                oDb.AddOutParameter(cmd, "@PK_DrugID", DbType.Int32, objDrug.DrugID);
                oDb.AddInParameter(cmd, "@DrugName", DbType.String, objDrug.DrugName);
                oDb.AddInParameter(cmd, "@DrugCode", DbType.String, objDrug.DrugCode);                
                //Modified ColumnName on 28-07-2017
                oDb.AddInParameter(cmd, "@InstructionID", DbType.Int16, objDrug.InstructionID);
                oDb.AddInParameter(cmd, "@Ingredient", DbType.String, objDrug.Ingredient);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objDrug.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objDrug.CreatedBy.UserID);

                //Added on 01-09-2017 Removed Dosage and Duration
                oDb.AddInParameter(cmd, "@FK_DosageID", DbType.Int32, objDrug.Dosage.DosageID);
                oDb.AddInParameter(cmd, "@FK_DurationID", DbType.Int32, objDrug.Duration.DurationID);
                //Added on 24-10-2017 Removed FrequencyID
                oDb.AddInParameter(cmd, "@FK_FrequencyID", DbType.Int32, objDrug.Frequency.FrequencyID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_DrugID"));
                    objDrug.DrugID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Drug AddDrug | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateDrug(Entity.Discharge.Drug objDrug)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateDrug(oDb, objDrug, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tDrug", "PK_DrugID", objDrug.DrugID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objDrug.ModifiedBy.UserID);
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
        private static bool UpdateDrug(Database oDb, Entity.Discharge.Drug objDrug, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_DRUG);
                oDb.AddInParameter(cmd, "@PK_DrugID", DbType.Int32, objDrug.DrugID);
                oDb.AddInParameter(cmd, "@DrugName", DbType.String, objDrug.DrugName);
                oDb.AddInParameter(cmd, "@DrugCode", DbType.String, objDrug.DrugCode);                
                //Modified ColumnName on 28-07-2017
                oDb.AddInParameter(cmd, "@InstructionID", DbType.Int16, objDrug.InstructionID);
                oDb.AddInParameter(cmd, "@Ingredient", DbType.String, objDrug.Ingredient);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objDrug.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objDrug.ModifiedBy.UserID);

                //Added on 01-09-2017 Removed Dosage and Duration
                oDb.AddInParameter(cmd, "@FK_DosageID", DbType.Int32, objDrug.Dosage.DosageID);
                oDb.AddInParameter(cmd, "@FK_DurationID", DbType.Int32, objDrug.Duration.DurationID);
                //Added on 24-10-2017 Removed FrequencyID
                oDb.AddInParameter(cmd, "@FK_FrequencyID", DbType.Int32, objDrug.Frequency.FrequencyID);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Drug UpdateDrug | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteDrug(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteDrug(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tDrug", "PK_DrugID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteDrug(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_DRUG);
                oDb.AddInParameter(cmd, "@PK_DrugID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Drug DeleteDrug | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
