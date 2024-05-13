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
    public class Prescription
    {
        public static Collection<Entity.Discharge.Prescription> GetPrescription(int DischargeEntryID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Discharge.Prescription> objList = new Collection<Entity.Discharge.Prescription>();
            Entity.Discharge.Prescription objPrescription = new Entity.Discharge.Prescription();
            Entity.Discharge.Drug oDrug;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PRESCRIPTION);
                db.AddInParameter(cmd, "@FK_DischargeEntryID", DbType.Int32, DischargeEntryID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPrescription = new Entity.Discharge.Prescription();
                        oDrug = new Entity.Discharge.Drug();

                        objPrescription.PrescriptionID = Convert.ToInt32(drData["PK_PrescriptionID"]);
                        objPrescription.DischargeEntryID = Convert.ToInt32(drData["FK_DischargeEntryID"]);

                        oDrug.DrugID = Convert.ToInt32(drData["FK_DrugID"]);
                        oDrug.DrugName = Convert.ToString(drData["DrugName"]);
                        //oDrug.Dosage = Convert.ToString(drData["Dosage"]);
                        //oDrug.Duration = Convert.ToString(drData["Duration"]);
                        //oDrug.FrequencyID = Convert.ToInt16(drData["FrequencyID"]);
                        oDrug.Ingredient = Convert.ToString(drData["Ingredient"]);
                        objPrescription.Drug = oDrug;

                        objPrescription.Duration = Convert.ToString(drData["Duration"]);
                        objPrescription.InstructionType = Convert.ToInt16(drData["InstructionType"]);
                        objPrescription.Ingredient = Convert.ToString(drData["Ingredient"]);

                        //Added on 01-09-2017
                        objPrescription.Dosage = Convert.ToString(drData["Dosage"]);
                        //objPrescription.FrequencyID = Convert.ToInt16(drData["FrequencyID"]);
                        objPrescription.Frequency = Convert.ToString(drData["Frequency"]);

                        //Added on 04-09-2017
                        objPrescription.OtherFrequency = Convert.ToString(drData["OtherFrequency"]);
                        objPrescription.Instruction = Convert.ToString(drData["Instruction"]);
                        objList.Add(objPrescription);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Prescription GetPrescription | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static bool AddPrescription(Entity.Discharge.Prescription objPrescription)
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
                    ID = AddPrescription(oDb, objPrescription, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tPrescription", "PK_PrescriptionID", objPrescription.PrescriptionID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objPrescription.CreatedBy.UserID);
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
        private static int AddPrescription(Database oDb, Entity.Discharge.Prescription objPrescription, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_PRESCRIPTION);
                oDb.AddOutParameter(cmd, "@PK_PrescriptionID", DbType.Int32, objPrescription.PrescriptionID);
                oDb.AddInParameter(cmd, "@FK_DischargeEntryID", DbType.Int32, objPrescription.DischargeEntryID);
                oDb.AddInParameter(cmd, "@FK_DrugID", DbType.Int32, objPrescription.Drug.DrugID);
                oDb.AddInParameter(cmd, "@InstructionType", DbType.Byte, objPrescription.InstructionType);
                oDb.AddInParameter(cmd, "@Ingredient", DbType.String, objPrescription.Ingredient);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objPrescription.CreatedBy.UserID);

                //Added on 01-09-2017
                oDb.AddInParameter(cmd, "@Dosage", DbType.String, objPrescription.Dosage);
                //oDb.AddInParameter(cmd, "@FrequencyID", DbType.Int16, objPrescription.FrequencyID);
                oDb.AddInParameter(cmd, "@Duration", DbType.String, objPrescription.Duration);

                //Added on 04-09-2017
                oDb.AddInParameter(cmd, "@OtherFrequency", DbType.String, objPrescription.OtherFrequency);
                //Added on 14-09-2017
                oDb.AddInParameter(cmd, "@DrugName", DbType.String, objPrescription.Drug.DrugName);
                //Added on 24-10-2017
                oDb.AddInParameter(cmd, "@Frequency", DbType.String, objPrescription.Frequency);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_PrescriptionID"));
                    objPrescription.PrescriptionID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Prescription AddPrescription | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdatePrescription(Entity.Discharge.Prescription objPrescription)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdatePrescription(oDb, objPrescription, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tPrescription", "PK_PrescriptionID", objPrescription.PrescriptionID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objPrescription.ModifiedBy.UserID);
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
        private static bool UpdatePrescription(Database oDb, Entity.Discharge.Prescription objPrescription, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_PRESCRIPTION);
                oDb.AddInParameter(cmd, "@PK_PrescriptionID", DbType.Int32, objPrescription.PrescriptionID);
                oDb.AddInParameter(cmd, "@FK_DischargeEntryID", DbType.Int32, objPrescription.DischargeEntryID);
                oDb.AddInParameter(cmd, "@FK_DrugID", DbType.Int32, objPrescription.Drug.DrugID);
                oDb.AddInParameter(cmd, "@InstructionType", DbType.Int32, objPrescription.InstructionType);
                oDb.AddInParameter(cmd, "@Ingredient", DbType.String, objPrescription.Ingredient);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objPrescription.ModifiedBy.UserID);

                //Added on 01-09-2017
                oDb.AddInParameter(cmd, "@Dosage", DbType.String, objPrescription.Dosage);
                //oDb.AddInParameter(cmd, "@FrequencyID", DbType.Int16, objPrescription.FrequencyID);
                oDb.AddInParameter(cmd, "@Duration", DbType.String, objPrescription.Duration);

                //Added on 04-09-2017
                oDb.AddInParameter(cmd, "@OtherFrequency", DbType.String, objPrescription.OtherFrequency);
                //Added on 14-09-2017
                oDb.AddInParameter(cmd, "@DrugName", DbType.String, objPrescription.Drug.DrugName);
                //Added on 24-10-2017
                oDb.AddInParameter(cmd, "@Frequency", DbType.String, objPrescription.Frequency);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Prescription UpdatePrescription | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeletePrescription(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeletePrescription(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tPrescription", "PK_PrescriptionID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeletePrescription(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_PRESCRIPTION);
                oDb.AddInParameter(cmd, "@PK_PrescriptionID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Prescription DeletePrescription | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
