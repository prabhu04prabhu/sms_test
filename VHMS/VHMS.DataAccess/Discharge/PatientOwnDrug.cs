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
    public class PatientOwnDrug
    {
        public static Collection<Entity.Discharge.PatientOwnDrug> GetPatientOwnDrug(int DischargeEntryID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Discharge.PatientOwnDrug> objList = new Collection<Entity.Discharge.PatientOwnDrug>();
            Entity.Discharge.PatientOwnDrug objPatientOwnDrug = new Entity.Discharge.PatientOwnDrug();
            Entity.Discharge.Drug oDrug;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PATIENTOWNDRUG);
                db.AddInParameter(cmd, "@FK_DischargeEntryID", DbType.Int32, DischargeEntryID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPatientOwnDrug = new Entity.Discharge.PatientOwnDrug();
                        oDrug = new Entity.Discharge.Drug();

                        objPatientOwnDrug.PatientOwnDrugID = Convert.ToInt32(drData["PK_PatientOwnDrugID"]);
                        objPatientOwnDrug.DischargeEntryID = Convert.ToInt32(drData["FK_DischargeEntryID"]);

                        oDrug.DrugID = Convert.ToInt32(drData["FK_DrugID"]);
                        oDrug.DrugName = Convert.ToString(drData["DrugName"]);
                        //oDrug.Dosage = Convert.ToString(drData["Dosage"]);
                        //oDrug.Duration = Convert.ToString(drData["Duration"]);
                        //oDrug.FrequencyID = Convert.ToInt16(drData["FrequencyID"]);
                        oDrug.Ingredient = Convert.ToString(drData["Ingredient"]);
                        objPatientOwnDrug.Drug = oDrug;

                        objPatientOwnDrug.Duration = Convert.ToString(drData["Duration"]);
                        objPatientOwnDrug.InstructionType = Convert.ToInt16(drData["InstructionType"]);
                        objPatientOwnDrug.Ingredient = Convert.ToString(drData["Ingredient"]);

                        //Added on 01-09-2017
                        objPatientOwnDrug.Dosage = Convert.ToString(drData["Dosage"]);
                        //objPatientOwnDrug.FrequencyID = Convert.ToInt16(drData["FrequencyID"]);
                        objPatientOwnDrug.Frequency = Convert.ToString(drData["Frequency"]);

                        //Added on 04-09-2017
                        objPatientOwnDrug.OtherFrequency = Convert.ToString(drData["OtherFrequency"]);
                        objPatientOwnDrug.Instruction = Convert.ToString(drData["Instruction"]);
                        objList.Add(objPatientOwnDrug);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.PatientOwnDrug GetPatientOwnDrug | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static bool AddPatientOwnDrug(Entity.Discharge.PatientOwnDrug objPatientOwnDrug)
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
                    ID = AddPatientOwnDrug(oDb, objPatientOwnDrug, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tPatientOwnDrug", "PK_PatientOwnDrugID", objPatientOwnDrug.PatientOwnDrugID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objPatientOwnDrug.CreatedBy.UserID);
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
        private static int AddPatientOwnDrug(Database oDb, Entity.Discharge.PatientOwnDrug objPatientOwnDrug, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_PATIENTOWNDRUG);
                oDb.AddOutParameter(cmd, "@PK_PatientOwnDrugID", DbType.Int32, objPatientOwnDrug.PatientOwnDrugID);
                oDb.AddInParameter(cmd, "@FK_DischargeEntryID", DbType.Int32, objPatientOwnDrug.DischargeEntryID);
                oDb.AddInParameter(cmd, "@FK_DrugID", DbType.Int32, objPatientOwnDrug.Drug.DrugID);
                oDb.AddInParameter(cmd, "@InstructionType", DbType.Byte, objPatientOwnDrug.InstructionType);
                oDb.AddInParameter(cmd, "@Ingredient", DbType.String, objPatientOwnDrug.Ingredient);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objPatientOwnDrug.CreatedBy.UserID);

                //Added on 01-09-2017
                oDb.AddInParameter(cmd, "@Dosage", DbType.String, objPatientOwnDrug.Dosage);
                //oDb.AddInParameter(cmd, "@FrequencyID", DbType.Int16, objPatientOwnDrug.FrequencyID);
                oDb.AddInParameter(cmd, "@Duration", DbType.String, objPatientOwnDrug.Duration);

                //Added on 04-09-2017
                oDb.AddInParameter(cmd, "@OtherFrequency", DbType.String, objPatientOwnDrug.OtherFrequency);
                //Added on 14-09-2017
                oDb.AddInParameter(cmd, "@DrugName", DbType.String, objPatientOwnDrug.Drug.DrugName);
                //Added on 24-10-2017
                oDb.AddInParameter(cmd, "@Frequency", DbType.String, objPatientOwnDrug.Frequency);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_PatientOwnDrugID"));
                    objPatientOwnDrug.PatientOwnDrugID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.PatientOwnDrug AddPatientOwnDrug | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdatePatientOwnDrug(Entity.Discharge.PatientOwnDrug objPatientOwnDrug)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdatePatientOwnDrug(oDb, objPatientOwnDrug, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tPatientOwnDrug", "PK_PatientOwnDrugID", objPatientOwnDrug.PatientOwnDrugID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objPatientOwnDrug.ModifiedBy.UserID);
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
        private static bool UpdatePatientOwnDrug(Database oDb, Entity.Discharge.PatientOwnDrug objPatientOwnDrug, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_PATIENTOWNDRUG);
                oDb.AddInParameter(cmd, "@PK_PatientOwnDrugID", DbType.Int32, objPatientOwnDrug.PatientOwnDrugID);
                oDb.AddInParameter(cmd, "@FK_DischargeEntryID", DbType.Int32, objPatientOwnDrug.DischargeEntryID);
                oDb.AddInParameter(cmd, "@FK_DrugID", DbType.Int32, objPatientOwnDrug.Drug.DrugID);
                oDb.AddInParameter(cmd, "@InstructionType", DbType.Int32, objPatientOwnDrug.InstructionType);
                oDb.AddInParameter(cmd, "@Ingredient", DbType.String, objPatientOwnDrug.Ingredient);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objPatientOwnDrug.ModifiedBy.UserID);

                //Added on 01-09-2017
                oDb.AddInParameter(cmd, "@Dosage", DbType.String, objPatientOwnDrug.Dosage);
                //oDb.AddInParameter(cmd, "@FrequencyID", DbType.Int16, objPatientOwnDrug.FrequencyID);
                oDb.AddInParameter(cmd, "@Duration", DbType.String, objPatientOwnDrug.Duration);

                //Added on 04-09-2017
                oDb.AddInParameter(cmd, "@OtherFrequency", DbType.String, objPatientOwnDrug.OtherFrequency);
                //Added on 14-09-2017
                oDb.AddInParameter(cmd, "@DrugName", DbType.String, objPatientOwnDrug.Drug.DrugName);
                //Added on 24-10-2017
                oDb.AddInParameter(cmd, "@Frequency", DbType.String, objPatientOwnDrug.Frequency);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.PatientOwnDrug UpdatePatientOwnDrug | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeletePatientOwnDrug(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeletePatientOwnDrug(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tPatientOwnDrug", "PK_PatientOwnDrugID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeletePatientOwnDrug(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_PATIENTOWNDRUG);
                oDb.AddInParameter(cmd, "@PK_PatientOwnDrugID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.PatientOwnDrug DeletePatientOwnDrug | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
