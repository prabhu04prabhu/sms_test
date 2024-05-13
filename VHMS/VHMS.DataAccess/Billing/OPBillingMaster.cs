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
    public class OPBillingMaster
    {
        public static Collection<Entity.Billing.OPBillingMaster> GetOPBilling(int ipatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.OPBillingMaster> objList = new Collection<Entity.Billing.OPBillingMaster>();
            Entity.Billing.OPBillingMaster objOPBilling;
            Entity.patient objpatient; Entity.Discharge.Doctor objdoctor;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_OPBILLINGMASTER);
                db.AddInParameter(cmd, "@PK_OPID", DbType.Int32, ipatientID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objOPBilling = new Entity.Billing.OPBillingMaster();
                        objpatient = new Entity.patient();
                        objdoctor = new Entity.Discharge.Doctor();

                        objOPBilling.OPID = Convert.ToInt32(drData["PK_OPID"]);
                        objOPBilling.BillNo = Convert.ToString(drData["BillNo"]);
                        objOPBilling.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objOPBilling.sBillDate = objOPBilling.BillDate.ToString("dd/MM/yyyy");

                        objpatient.patientID = Convert.ToInt32(drData["FK_patientID"]);
                        objpatient.HName = Convert.ToString(drData["HName"]);
                        objpatient.WName = Convert.ToString(drData["WName"]);
                        objpatient.OPDNo = Convert.ToString(drData["OPDNo"]);
                        objOPBilling.Patient = objpatient;
                        objdoctor.DoctorID = Convert.ToInt32(drData["FK_DoctorID"]);
                        objOPBilling.Doctor = objdoctor;
                        objOPBilling.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objOPBilling.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objOPBilling.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objOPBilling.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objOPBilling.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objOPBilling.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objOPBilling.IsCancelled = Convert.ToBoolean(drData["IsCancelled"]);
                        objOPBilling.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objList.Add(objOPBilling);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "OPBilling GetOPBilling | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.OPBillingMaster> GetOPBillingID(int ipatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.OPBillingMaster> objList = new Collection<Entity.Billing.OPBillingMaster>();
            Entity.Billing.OPBillingMaster objOPBilling;
            Entity.patient objpatient; Entity.Discharge.Doctor objdoctor;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_OPBILLINGID);
                db.AddInParameter(cmd, "@PK_OPID", DbType.Int32, ipatientID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objOPBilling = new Entity.Billing.OPBillingMaster();
                        objpatient = new Entity.patient();
                        objdoctor = new Entity.Discharge.Doctor();

                        objOPBilling.OPID = Convert.ToInt32(drData["PK_OPID"]);
                        objOPBilling.BillNo = Convert.ToString(drData["BillNo"]);
                        objOPBilling.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objOPBilling.sBillDate = objOPBilling.BillDate.ToString("dd/MM/yyyy");

                        objpatient.patientID = Convert.ToInt32(drData["FK_patientID"]);
                        objpatient.HName = Convert.ToString(drData["HName"]);
                        objpatient.WName = Convert.ToString(drData["WName"]);
                        objpatient.OPDNo = Convert.ToString(drData["OPDNo"]);
                        objOPBilling.Patient = objpatient;
                        objdoctor.DoctorID = Convert.ToInt32(drData["FK_DoctorID"]);
                        objOPBilling.Doctor = objdoctor;
                        objOPBilling.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objOPBilling.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objOPBilling.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objOPBilling.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objOPBilling.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objOPBilling.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objOPBilling.IsCancelled = Convert.ToBoolean(drData["IsCancelled"]);
                        objOPBilling.CancelReason = Convert.ToString(drData["CancelReason"]);

                        objList.Add(objOPBilling);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "OPBilling GetOPBilling | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.OPBillingMaster> SearchOPBilling(string ID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.OPBillingMaster> objList = new Collection<Entity.Billing.OPBillingMaster>();
            Entity.Billing.OPBillingMaster objOPBilling;
            Entity.patient objpatient; Entity.Discharge.Doctor objdoctor;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_OPBILLINGDYNAMIC);
                db.AddInParameter(cmd, "@PK_OPID", DbType.String, ID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objOPBilling = new Entity.Billing.OPBillingMaster();
                        objpatient = new Entity.patient();
                        objdoctor = new Entity.Discharge.Doctor();

                        objOPBilling.OPID = Convert.ToInt32(drData["PK_OPID"]);
                        objOPBilling.BillNo = Convert.ToString(drData["BillNo"]);
                        objOPBilling.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objOPBilling.sBillDate = objOPBilling.BillDate.ToString("dd/MM/yyyy");

                        objpatient.patientID = Convert.ToInt32(drData["FK_patientID"]);
                        objpatient.HName = Convert.ToString(drData["HName"]);
                        objpatient.WName = Convert.ToString(drData["WName"]);
                        objpatient.OPDNo = Convert.ToString(drData["OPDNo"]);
                        objOPBilling.Patient = objpatient;
                        objdoctor.DoctorID = Convert.ToInt32(drData["FK_DoctorID"]);
                        objOPBilling.Doctor = objdoctor;
                        objOPBilling.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objOPBilling.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objOPBilling.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objOPBilling.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objOPBilling.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objOPBilling.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objOPBilling.IsCancelled = Convert.ToBoolean(drData["IsCancelled"]);
                        objOPBilling.CancelReason = Convert.ToString(drData["CancelReason"]);

                        objList.Add(objOPBilling);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "OPBilling GetOPBilling | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.OPBillingMaster> GetOPBillingReport(Entity.Billing.OPBillingFilter oJobCardFilter)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.OPBillingMaster> objList = new Collection<Entity.Billing.OPBillingMaster>();
            Entity.Billing.OPBillingMaster objOPBilling;
            Entity.patient objpatient; Entity.Discharge.Doctor objdoctor;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_OPBILLINGREPORT);
                db.AddInParameter(cmd, "@FK_PatientID", DbType.Int32, oJobCardFilter.PatientID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, oJobCardFilter.DateFrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, oJobCardFilter.DateTo);
                db.AddInParameter(cmd, "@FK_DoctorID", DbType.Int32, oJobCardFilter.DoctorID);
                db.AddInParameter(cmd, "@FK_DescriptionID", DbType.Int32, oJobCardFilter.DescriptionID);
                db.AddInParameter(cmd, "@FK_UserID", DbType.Int32, oJobCardFilter.UserID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objOPBilling = new Entity.Billing.OPBillingMaster();
                        objpatient = new Entity.patient();
                        objdoctor = new Entity.Discharge.Doctor();

                        objOPBilling.OPID = Convert.ToInt32(drData["PK_OPID"]);
                        objOPBilling.BillNo = Convert.ToString(drData["BillNo"]);
                      //  objOPBilling.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objOPBilling.sBillDate = (Convert.ToDateTime(drData["BillDate"])).ToString("dd/MM/yyyy");

                       // objpatient.patientID = Convert.ToInt32(drData["FK_patientID"]);
                      //  objpatient.HName = Convert.ToString(drData["HName"]);
                        objpatient.WName = Convert.ToString(drData["WName"]);
                        objpatient.WMobileNo = Convert.ToString(drData["WMobileNo"]);
                       // objpatient.OPDNo = Convert.ToString(drData["OPDNo"]);
                        objOPBilling.Patient = objpatient;
                       // objdoctor.DoctorID = Convert.ToInt32(drData["FK_DoctorID"]);
                        objdoctor.DoctorName = Convert.ToString(drData["DoctorName"]);
                        objOPBilling.Doctor = objdoctor;
                       // objOPBilling.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objOPBilling.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                       // objOPBilling.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                      //  objOPBilling.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        //objOPBilling.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        //objOPBilling.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objOPBilling.Subtotal = Convert.ToDecimal(drData["Subtotal"]);
                        objOPBilling.DescriptionName = Convert.ToString(drData["DescriptionName"]);
                        objOPBilling.CUserName = Convert.ToString(drData["CUserName"]);
                        objOPBilling.IsCancelled = Convert.ToBoolean(drData["IsCancelled"]);
                        objOPBilling.CancelReason = Convert.ToString(drData["CancelReason"]);
                        objList.Add(objOPBilling);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "OPBilling GetOPBilling | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.OPBillingMaster GetOPBillingByID(int iOPID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.OPBillingMaster objOPBilling = new Entity.Billing.OPBillingMaster();
            Entity.patient objpatient; Entity.Discharge.Doctor objdoctor;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_OPBILLINGMASTER);
                db.AddInParameter(cmd, "@PK_OPID", DbType.Int32, iOPID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objOPBilling = new Entity.Billing.OPBillingMaster();
                        objpatient = new Entity.patient();
                        objdoctor = new Entity.Discharge.Doctor();

                        objOPBilling.OPID = Convert.ToInt32(drData["PK_OPID"]);
                        objOPBilling.BillNo = Convert.ToString(drData["BillNo"]);
                        objOPBilling.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objOPBilling.sBillDate = objOPBilling.BillDate.ToString("dd/MM/yyyy");

                        objpatient.patientID = Convert.ToInt32(drData["FK_patientID"]);
                        objpatient.OPDNo = Convert.ToString(drData["OPDNo"]);
                        objpatient.HName = Convert.ToString(drData["HName"]);
                        objpatient.WName = Convert.ToString(drData["WName"]);
                        objOPBilling.Patient = objpatient;
                        objdoctor.DoctorID = Convert.ToInt32(drData["FK_DoctorID"]);
                        objOPBilling.Doctor = objdoctor;
                        objOPBilling.PaymentMode = Convert.ToString(drData["PaymentMode"]);
                        objOPBilling.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objOPBilling.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objOPBilling.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objOPBilling.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
                        objOPBilling.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objOPBilling.IsCancelled = Convert.ToBoolean(drData["IsCancelled"]);
                        objOPBilling.CancelReason = Convert.ToString(drData["CancelReason"]);

                        objOPBilling.OPBillingTrans = OPBillingTrans.GetOPBillingTransByOPID(objOPBilling.OPID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "OPBilling GetOPBillingByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objOPBilling;
        }
        public static int AddOPBilling(Entity.Billing.OPBillingMaster objOPBilling)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_OPBILLINGMASTER);
                db.AddOutParameter(cmd, "@PK_OPID", DbType.Int32, objOPBilling.OPID);
                db.AddInParameter(cmd, "@BillNo", DbType.String, objOPBilling.BillNo);
                db.AddInParameter(cmd, "@BillDate", DbType.String, objOPBilling.sBillDate);
                db.AddInParameter(cmd, "@FK_patientID", DbType.Int32, objOPBilling.Patient.patientID);
                db.AddInParameter(cmd, "@FK_DoctorID", DbType.Int32, objOPBilling.Doctor.DoctorID);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objOPBilling.DiscountAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objOPBilling.TotalAmount);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objOPBilling.NetAmount);
                db.AddInParameter(cmd, "@PaidAmount", DbType.Decimal, objOPBilling.PaidAmount);
                db.AddInParameter(cmd, "@BalanceAmount", DbType.Decimal, objOPBilling.BalanceAmount);
                db.AddInParameter(cmd, "@PaymentMode", DbType.String, objOPBilling.PaymentMode);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objOPBilling.CreatedBy.UserID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_OPID"));

                foreach (Entity.Billing.OPBillingTrans ObjOPBillingTrans in objOPBilling.OPBillingTrans)
                    ObjOPBillingTrans.OPID = iID;

                OPBillingTrans.SaveOPBillingTransaction(objOPBilling.OPBillingTrans);
            }
            catch (Exception ex)
            {
                sException = "OPBilling AddOPBilling | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateOPBilling(Entity.Billing.OPBillingMaster objOPBilling)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_OPBILLINGMASTER);
                db.AddInParameter(cmd, "@PK_OPID", DbType.Int32, objOPBilling.OPID);
                db.AddInParameter(cmd, "@BillNo", DbType.String, objOPBilling.BillNo);
                db.AddInParameter(cmd, "@BillDate", DbType.String, objOPBilling.sBillDate);
                db.AddInParameter(cmd, "@FK_patientID", DbType.Int32, objOPBilling.Patient.patientID);
                db.AddInParameter(cmd, "@FK_DoctorID", DbType.Int32, objOPBilling.Doctor.DoctorID);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objOPBilling.DiscountAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objOPBilling.TotalAmount);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objOPBilling.NetAmount);
                db.AddInParameter(cmd, "@PaidAmount", DbType.Decimal, objOPBilling.PaidAmount);
                db.AddInParameter(cmd, "@BalanceAmount", DbType.Decimal, objOPBilling.BalanceAmount);
                db.AddInParameter(cmd, "@PaymentMode", DbType.String, objOPBilling.PaymentMode);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objOPBilling.ModifiedBy.UserID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                OPBillingTrans.SaveOPBillingTransaction(objOPBilling.OPBillingTrans);
            }
            catch (Exception ex)
            {
                sException = "OPBilling UpdateOPBilling| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteOPBilling(int iOPBillingId, string Reason, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_OPBILLINGMASTER);
                db.AddInParameter(cmd, "@PK_OPID", DbType.Int32, iOPBillingId);
                db.AddInParameter(cmd, "@Reason", DbType.String, Reason);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, iUserId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "OPBilling DeleteOPBilling | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static DataSet GetOPBillingSummary()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_OPBILLINGSUMMARY);
                dsList = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "OPBilling GetOPBillingSummary | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsList;
        }
    }

    public class OPBillingTrans
    {
        public static Collection<Entity.Billing.OPBillingTrans> GetOPBillingTransByOPID(int iOPID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.OPBillingTrans> objList = new Collection<Entity.Billing.OPBillingTrans>();
            Entity.Billing.OPBillingTrans objOPBillingTrans = new Entity.Billing.OPBillingTrans();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_OPBILLINGTRANS);
                db.AddInParameter(cmd, "@FK_OPID", DbType.Int32, iOPID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objOPBillingTrans = new Entity.Billing.OPBillingTrans();

                        objOPBillingTrans.OPTransID = Convert.ToInt32(drData["PK_OPTransID"]);
                        objOPBillingTrans.OPID = Convert.ToInt32(drData["FK_OPID"]);

                        objOPBillingTrans.Charges = Convert.ToDecimal(drData["Charges"]);
                        objOPBillingTrans.Subtotal = Convert.ToDecimal(drData["Subtotal"]);
                        objOPBillingTrans.DescriptionName = Convert.ToString(drData["DescriptionName"]);
                        objOPBillingTrans.DescriptionID= Convert.ToInt32(drData["FK_DescriptionID"]);

                        objList.Add(objOPBillingTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "OPBillingTrans GetOPBillingTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveOPBillingTransaction(Collection<Entity.Billing.OPBillingTrans> ObjOPBillingTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.OPBillingTrans ObjOPBillingTransaction in ObjOPBillingTransList)
            {
                if (ObjOPBillingTransaction.StatusFlag == "I")
                    iID = AddOPBillingTrans(ObjOPBillingTransaction);
                else if (ObjOPBillingTransaction.StatusFlag == "U")
                    bResult = UpdateOPBillingTrans(ObjOPBillingTransaction);
                else if (ObjOPBillingTransaction.StatusFlag == "D")
                    bResult = DeleteOPBillingTrans(ObjOPBillingTransaction.OPTransID);
            }
        }
        public static int AddOPBillingTrans(Entity.Billing.OPBillingTrans objOPBillingTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_OPBILLINGTRANS);
                db.AddOutParameter(cmd, "@PK_OPTransID", DbType.Int32, objOPBillingTrans.OPTransID);
                db.AddInParameter(cmd, "@FK_OPID", DbType.Int32, objOPBillingTrans.OPID);
                db.AddInParameter(cmd, "@FK_DescriptionID", DbType.String, objOPBillingTrans.Description.DescriptionID);
                db.AddInParameter(cmd, "@Charges", DbType.Decimal, objOPBillingTrans.Charges);
                db.AddInParameter(cmd, "@Subtotal", DbType.Decimal, objOPBillingTrans.Subtotal);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_OPTransID"));
            }
            catch (Exception ex)
            {
                sException = "OPBillingTrans AddOPBillingTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateOPBillingTrans(Entity.Billing.OPBillingTrans objOPBillingTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_OPBILLINGTRANS);
                db.AddInParameter(cmd, "@PK_OPTransID", DbType.Int32, objOPBillingTrans.OPTransID);
                db.AddInParameter(cmd, "@FK_OPID", DbType.Int32, objOPBillingTrans.OPID);
                db.AddInParameter(cmd, "@FK_DescriptionID", DbType.String, objOPBillingTrans.Description.DescriptionID);
                db.AddInParameter(cmd, "@Charges", DbType.Decimal, objOPBillingTrans.Charges);
                db.AddInParameter(cmd, "@Subtotal", DbType.Decimal, objOPBillingTrans.Subtotal);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "OPBillingTrans UpdateOPBillingTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteOPBillingTrans(int iOPTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_OPBILLINGTRANS);
                db.AddInParameter(cmd, "@PK_OPTransID", DbType.Int32, iOPTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "OPBillingTrans DeleteOPBillingTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        
    }
}
