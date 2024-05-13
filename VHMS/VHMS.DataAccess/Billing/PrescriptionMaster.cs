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
    public class PrescriptionMaster
    {
        public static Collection<Entity.Billing.PrescriptionMaster> GetPrescription(int ipatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.PrescriptionMaster> objList = new Collection<Entity.Billing.PrescriptionMaster>();
            Entity.Billing.PrescriptionMaster objPrescription;
            Entity.patient objpatient;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PRESCRIPTIONMASTER);
                db.AddInParameter(cmd, "@PK_PrescriptionID", DbType.Int32, ipatientID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPrescription = new Entity.Billing.PrescriptionMaster();
                        objpatient = new Entity.patient();

                        objPrescription.PrescriptionID = Convert.ToInt32(drData["PK_PrescriptionID"]);
                        objPrescription.PrescriptionNo = Convert.ToString(drData["PrescriptionNo"]);
                        objPrescription.PrescriptionDate = Convert.ToDateTime(drData["PrescriptionDate"]);
                        objPrescription.sPrescriptionDate = objPrescription.PrescriptionDate.ToString("dd/MM/yyyy");

                        objpatient.patientID = Convert.ToInt32(drData["FK_patientID"]);
                        objpatient.HName = Convert.ToString(drData["HName"]);
                        objpatient.WName = Convert.ToString(drData["WName"]);
                        objPrescription.Patient = objpatient;

                        objList.Add(objPrescription);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Prescription GetPrescription | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.PrescriptionMaster GetPrescriptionByID(int iPrescriptionID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.PrescriptionMaster objPrescription = new Entity.Billing.PrescriptionMaster();
            Entity.patient objpatient;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PRESCRIPTIONMASTER);
                db.AddInParameter(cmd, "@PK_PrescriptionID", DbType.Int32, iPrescriptionID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPrescription = new Entity.Billing.PrescriptionMaster();
                        objpatient = new Entity.patient();

                        objPrescription.PrescriptionID = Convert.ToInt32(drData["PK_PrescriptionID"]);
                        objPrescription.PrescriptionNo = Convert.ToString(drData["PrescriptionNo"]);
                        objPrescription.PrescriptionDate = Convert.ToDateTime(drData["PrescriptionDate"]);
                        objPrescription.sPrescriptionDate = objPrescription.PrescriptionDate.ToString("dd/MM/yyyy");

                        objpatient.patientID = Convert.ToInt32(drData["FK_patientID"]);
                        //objpatient.patientName = Convert.ToString(drData["patientName"]);
                        //objpatient.patientCode = Convert.ToString(drData["patientCode"]);
                        objPrescription.Patient = objpatient;

                        objPrescription.PrescriptionTrans = PrescriptionTrans.GetPrescriptionTransByPrescriptionID(objPrescription.PrescriptionID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Prescription GetPrescriptionByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objPrescription;
        }
        public static int AddPrescription(Entity.Billing.PrescriptionMaster objPrescription)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_PRESCRIPTIONMASTER);
                db.AddOutParameter(cmd, "@PK_PrescriptionID", DbType.Int32, objPrescription.PrescriptionID);
                db.AddInParameter(cmd, "@PrescriptionNo", DbType.String, objPrescription.PrescriptionNo);
                db.AddInParameter(cmd, "@PrescriptionDate", DbType.String, objPrescription.sPrescriptionDate);
                db.AddInParameter(cmd, "@FK_patientID", DbType.Int32, objPrescription.Patient.patientID);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objPrescription.CreatedBy.UserID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_PrescriptionID"));

                foreach (Entity.Billing.PrescriptionTrans ObjPrescriptionTrans in objPrescription.PrescriptionTrans)
                    ObjPrescriptionTrans.PrescriptionID = iID;

                PrescriptionTrans.SavePrescriptionTransaction(objPrescription.PrescriptionTrans);
            }
            catch (Exception ex)
            {
                sException = "Prescription AddPrescription | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdatePrescription(Entity.Billing.PrescriptionMaster objPrescription)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_PRESCRIPTIONMASTER);
                db.AddInParameter(cmd, "@PK_PrescriptionID", DbType.Int32, objPrescription.PrescriptionID);
                db.AddInParameter(cmd, "@PrescriptionNo", DbType.String, objPrescription.PrescriptionNo);
                db.AddInParameter(cmd, "@PrescriptionDate", DbType.String, objPrescription.sPrescriptionDate);
                db.AddInParameter(cmd, "@FK_patientID", DbType.Int32, objPrescription.Patient.patientID);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objPrescription.ModifiedBy.UserID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                PrescriptionTrans.SavePrescriptionTransaction(objPrescription.PrescriptionTrans);
            }
            catch (Exception ex)
            {
                sException = "Prescription UpdatePrescription| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeletePrescription(int iPrescriptionId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_PRESCRIPTIONMASTER);
                db.AddInParameter(cmd, "@PK_PrescriptionID", DbType.Int32, iPrescriptionId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Prescription DeletePrescription | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static DataSet GetPrescriptionSummary()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PRESCRIPTIONSUMMARY);
                dsList = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "Prescription GetPrescriptionSummary | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsList;
        }
    }

    public class PrescriptionTrans
    {
        public static Collection<Entity.Billing.PrescriptionTrans> GetPrescriptionTransByPrescriptionID(int iPrescriptionID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.PrescriptionTrans> objList = new Collection<Entity.Billing.PrescriptionTrans>();
            Entity.Billing.PrescriptionTrans objPrescriptionTrans = new Entity.Billing.PrescriptionTrans();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PRESCRIPTIONTRANS);
                db.AddInParameter(cmd, "@FK_PrescriptionID", DbType.Int32, iPrescriptionID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPrescriptionTrans = new Entity.Billing.PrescriptionTrans();

                        objPrescriptionTrans.PrescriptionTransID = Convert.ToInt32(drData["PK_PrescriptionTransID"]);
                        objPrescriptionTrans.PrescriptionID = Convert.ToInt32(drData["FK_PrescriptionID"]);

                        objPrescriptionTrans.Ingredient = Convert.ToString(drData["Ingredient"]);
                        objPrescriptionTrans.Instruction = Convert.ToString(drData["Instruction"]);
                        objPrescriptionTrans.Duration = Convert.ToString(drData["Duration"]);
                        objPrescriptionTrans.Frequency = Convert.ToString(drData["Frequency"]);
                        objPrescriptionTrans.Dosage = Convert.ToString(drData["Dosage"]);
                        objPrescriptionTrans.DrugName = Convert.ToString(drData["DrugName"]);
                        objPrescriptionTrans.DrugID= Convert.ToInt32(drData["FK_DrugID"]);
                        objPrescriptionTrans.InstructionType = Convert.ToInt16(drData["InstructionType"]);

                        //objPrescriptionTrans.Drug.DrugName = Convert.ToString(drData["DrugName"]);
                        //objPrescriptionTrans.Drug.DrugID = Convert.ToInt32(drData["FK_DrugID"]);

                        //objPrescriptionTrans.FK = Convert.ToString(drData["DrugName"]);
                        //objPrescriptionTrans.ReceivedQty = Convert.ToInt32(drData["ReceivedQty"]);

                        objList.Add(objPrescriptionTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "PrescriptionTrans GetPrescriptionTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SavePrescriptionTransaction(Collection<Entity.Billing.PrescriptionTrans> ObjPrescriptionTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.PrescriptionTrans ObjPrescriptionTransaction in ObjPrescriptionTransList)
            {
                if (ObjPrescriptionTransaction.StatusFlag == "I")
                    iID = AddPrescriptionTrans(ObjPrescriptionTransaction);
                else if (ObjPrescriptionTransaction.StatusFlag == "U")
                    bResult = UpdatePrescriptionTrans(ObjPrescriptionTransaction);
                else if (ObjPrescriptionTransaction.StatusFlag == "D")
                    bResult = DeletePrescriptionTrans(ObjPrescriptionTransaction.PrescriptionTransID);
            }
        }
        public static int AddPrescriptionTrans(Entity.Billing.PrescriptionTrans objPrescriptionTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_PRESCRIPTIONTRANS);
                db.AddOutParameter(cmd, "@PK_PrescriptionTransID", DbType.Int32, objPrescriptionTrans.PrescriptionTransID);
                db.AddInParameter(cmd, "@FK_PrescriptionID", DbType.Int32, objPrescriptionTrans.PrescriptionID);
                db.AddInParameter(cmd, "@FK_DrugID", DbType.String, objPrescriptionTrans.Drug.DrugID);
                db.AddInParameter(cmd, "@Dosage", DbType.String, objPrescriptionTrans.Dosage);
                db.AddInParameter(cmd, "@Frequency", DbType.String, objPrescriptionTrans.Frequency);
                db.AddInParameter(cmd, "@Duration", DbType.String, objPrescriptionTrans.Duration);
                db.AddInParameter(cmd, "@Instruction", DbType.String, objPrescriptionTrans.Instruction);
                db.AddInParameter(cmd, "@Ingredient", DbType.String, objPrescriptionTrans.Ingredient);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_PrescriptionTransID"));
            }
            catch (Exception ex)
            {
                sException = "PrescriptionTrans AddPrescriptionTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdatePrescriptionTrans(Entity.Billing.PrescriptionTrans objPrescriptionTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_PRESCRIPTIONTRANS);
                db.AddInParameter(cmd, "@PK_PrescriptionTransID", DbType.Int32, objPrescriptionTrans.PrescriptionTransID);
                db.AddInParameter(cmd, "@FK_PrescriptionID", DbType.Int32, objPrescriptionTrans.PrescriptionID);
                db.AddInParameter(cmd, "@FK_DrugID", DbType.String, objPrescriptionTrans.Drug.DrugID);
                db.AddInParameter(cmd, "@Dosage", DbType.String, objPrescriptionTrans.Dosage);
                db.AddInParameter(cmd, "@Frequency", DbType.String, objPrescriptionTrans.Frequency);
                db.AddInParameter(cmd, "@Duration", DbType.String, objPrescriptionTrans.Duration);
                db.AddInParameter(cmd, "@Instruction", DbType.String, objPrescriptionTrans.Instruction);
                db.AddInParameter(cmd, "@Ingredient", DbType.String, objPrescriptionTrans.Ingredient);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "PrescriptionTrans UpdatePrescriptionTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeletePrescriptionTrans(int iPrescriptionTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_PRESCRIPTIONTRANS);
                db.AddInParameter(cmd, "@PK_PrescriptionTransID", DbType.Int32, iPrescriptionTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "PrescriptionTrans DeletePrescriptionTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        //public static DataSet GetPendingInwards(int ipatientID = 0, int iMagazineID = 0)
        //{
        //    string sException = string.Empty;
        //    Database db;
        //    DataSet dsList = null;

        //    try
        //    {
        //        db = Entity.DBConnection.dbCon;
        //        DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PENDINGSTOCK);
        //        db.AddInParameter(cmd, "@FK_patientID", DbType.Int32, ipatientID);
        //        db.AddInParameter(cmd, "@FK_MagazineID", DbType.Int32, iMagazineID);
        //        dsList = db.ExecuteDataSet(cmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "PrescriptionTrans GetPendingInwards | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return dsList;
        //}
        
    }
}
