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
    public class BranchMove
    {
        public static Collection<Entity.Billing.BranchMove> GetBranchMove(int ipatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.BranchMove> objList = new Collection<Entity.Billing.BranchMove>();
            Entity.Billing.BranchMove objBranchMove;
            Entity.Branch objFromBranch;
            Entity.Branch objToBranch;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_BRANCHMOVE);
                db.AddInParameter(cmd, "@PK_BranchMoveID", DbType.Int32, ipatientID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objBranchMove = new Entity.Billing.BranchMove();
                        objFromBranch = new Entity.Branch();
                        objToBranch = new Entity.Branch();

                        objBranchMove.BranchMoveID = Convert.ToInt32(drData["PK_BranchMoveID"]);
                        objBranchMove.MoveNo = Convert.ToString(drData["MoveNo"]);
                        objBranchMove.MoveDate = Convert.ToDateTime(drData["MoveDate"]);
                        objBranchMove.sMoveDate = objBranchMove.MoveDate.ToString("dd/MM/yyyy");
                        objBranchMove.TotalQuantity = Convert.ToInt32(drData["TotalQuantity"]);
                        objBranchMove.TotalWeight = Convert.ToDecimal(drData["TotalWeight"]);
                        objFromBranch.BranchID = Convert.ToInt32(drData["FK_FromBranchID"]);
                        objFromBranch.BranchName = Convert.ToString(drData["FromBranchName"]);
                        objFromBranch.MobileNo = Convert.ToString(drData["FromBranchMobileNo"]);
                        objFromBranch.Address = Convert.ToString(drData["FromBranchAddress"]);
                        objBranchMove.FromBranch = objFromBranch;

                        objToBranch.BranchID = Convert.ToInt32(drData["FK_ToBranchID"]);
                        objToBranch.BranchName = Convert.ToString(drData["ToBranchName"]);
                        objToBranch.MobileNo = Convert.ToString(drData["ToBranchMobileNo"]);
                        objToBranch.Address = Convert.ToString(drData["ToBranchAddress"]);
                        objBranchMove.ToBranch = objToBranch;

                        objBranchMove.Status = Convert.ToString(drData["Status"]);
                        
                        objList.Add(objBranchMove);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "BranchMove GetBranchMove | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.BranchMove> GetTopBranchMove(int ipatientID = 0, int iBranchID=0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.BranchMove> objList = new Collection<Entity.Billing.BranchMove>();
            Entity.Billing.BranchMove objBranchMove;
            Entity.Branch objFromBranch;
            Entity.Branch objToBranch;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPBRANCHMOVE);
                db.AddInParameter(cmd, "@PK_BranchMoveID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, iBranchID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objBranchMove = new Entity.Billing.BranchMove();
                        objFromBranch = new Entity.Branch();
                        objToBranch = new Entity.Branch();

                        objBranchMove.BranchMoveID = Convert.ToInt32(drData["PK_BranchMoveID"]);
                        objBranchMove.MoveNo = Convert.ToString(drData["MoveNo"]);
                        objBranchMove.MoveDate = Convert.ToDateTime(drData["MoveDate"]);
                        objBranchMove.sMoveDate = objBranchMove.MoveDate.ToString("dd/MM/yyyy");
                        objBranchMove.TotalQuantity = Convert.ToInt32(drData["TotalQuantity"]);
                        objBranchMove.TotalWeight = Convert.ToDecimal(drData["TotalWeight"]);
                        objFromBranch.BranchID = Convert.ToInt32(drData["FK_FromBranchID"]);
                        objFromBranch.BranchName = Convert.ToString(drData["FromBranchName"]);
                        objFromBranch.MobileNo = Convert.ToString(drData["FromBranchMobileNo"]);
                        objFromBranch.Address = Convert.ToString(drData["FromBranchAddress"]);
                        objBranchMove.FromBranch = objFromBranch;

                        objToBranch.BranchID = Convert.ToInt32(drData["FK_ToBranchID"]);
                        objToBranch.BranchName = Convert.ToString(drData["ToBranchName"]);
                        objToBranch.MobileNo = Convert.ToString(drData["ToBranchMobileNo"]);
                        objToBranch.Address = Convert.ToString(drData["ToBranchAddress"]);
                        objBranchMove.ToBranch = objToBranch;

                        objBranchMove.Status = Convert.ToString(drData["Status"]);

                        objList.Add(objBranchMove);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "BranchMove GetBranchMove | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.BranchMove> GetBranchMoveID(int ipatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.BranchMove> objList = new Collection<Entity.Billing.BranchMove>();
            Entity.Billing.BranchMove objBranchMove;
            Entity.Branch objFromBranch;
            Entity.Branch objToBranch;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_BRANCHMOVE);
                db.AddInParameter(cmd, "@PK_BranchMoveID", DbType.Int32, ipatientID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objBranchMove = new Entity.Billing.BranchMove();

                        objFromBranch = new Entity.Branch();
                        objToBranch = new Entity.Branch();

                        objBranchMove.BranchMoveID = Convert.ToInt32(drData["PK_BranchMoveID"]);
                        objBranchMove.MoveNo = Convert.ToString(drData["MoveNo"]);
                        objBranchMove.MoveDate = Convert.ToDateTime(drData["MoveDate"]);
                        objBranchMove.sMoveDate = objBranchMove.MoveDate.ToString("dd/MM/yyyy");
                        objBranchMove.TotalQuantity = Convert.ToInt32(drData["TotalQuantity"]);
                        objBranchMove.TotalWeight = Convert.ToDecimal(drData["TotalWeight"]);
                        objFromBranch.BranchID = Convert.ToInt32(drData["FK_FromBranchID"]);
                        objFromBranch.BranchName = Convert.ToString(drData["FromBranchName"]);
                        objFromBranch.MobileNo = Convert.ToString(drData["FromBranchMobileNo"]);
                        objFromBranch.Address = Convert.ToString(drData["FromBranchAddress"]);
                        objBranchMove.FromBranch = objFromBranch;

                        objToBranch.BranchID = Convert.ToInt32(drData["FK_ToBranchID"]);
                        objToBranch.BranchName = Convert.ToString(drData["ToBranchName"]);
                        objToBranch.MobileNo = Convert.ToString(drData["ToBranchMobileNo"]);
                        objToBranch.Address = Convert.ToString(drData["ToBranchAddress"]);
                        objBranchMove.ToBranch = objToBranch;

                        objBranchMove.Status = Convert.ToString(drData["Status"]);
                        objList.Add(objBranchMove);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "BranchMove GetBranchMove | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.BranchMove> SearchBranchMove(string ID, int iBranchID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.BranchMove> objList = new Collection<Entity.Billing.BranchMove>();
            Entity.Billing.BranchMove objBranchMove;
            Entity.Branch objFromBranch;
            Entity.Branch objToBranch;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_BRANCHMOVE);
                db.AddInParameter(cmd, "@key", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, iBranchID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objBranchMove = new Entity.Billing.BranchMove();
                        objFromBranch = new Entity.Branch();
                        objToBranch = new Entity.Branch();

                        objBranchMove.BranchMoveID = Convert.ToInt32(drData["PK_BranchMoveID"]);
                        objBranchMove.MoveNo = Convert.ToString(drData["MoveNo"]);
                        objBranchMove.MoveDate = Convert.ToDateTime(drData["MoveDate"]);
                        objBranchMove.sMoveDate = objBranchMove.MoveDate.ToString("dd/MM/yyyy");
                        objBranchMove.TotalQuantity = Convert.ToInt32(drData["TotalQuantity"]);
                        objBranchMove.TotalWeight = Convert.ToDecimal(drData["TotalWeight"]);
                        objFromBranch.BranchID = Convert.ToInt32(drData["FK_FromBranchID"]);
                        objFromBranch.BranchName = Convert.ToString(drData["FromBranchName"]);
                        objFromBranch.MobileNo = Convert.ToString(drData["FromBranchMobileNo"]);
                        objFromBranch.Address = Convert.ToString(drData["FromBranchAddress"]);
                        objBranchMove.FromBranch = objFromBranch;

                        objToBranch.BranchID = Convert.ToInt32(drData["FK_ToBranchID"]);
                        objToBranch.BranchName = Convert.ToString(drData["ToBranchName"]);
                        objToBranch.MobileNo = Convert.ToString(drData["ToBranchMobileNo"]);
                        objToBranch.Address = Convert.ToString(drData["ToBranchAddress"]);
                        objBranchMove.ToBranch = objToBranch;

                        objBranchMove.Status = Convert.ToString(drData["Status"]);

                        objList.Add(objBranchMove);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "BranchMove GetBranchMove | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        //public static Collection<Entity.Billing.BranchMove> GetBranchMoveReport(Entity.Billing.BranchMoveFilter oJobCardFilter)
        //{
        //    string sException = string.Empty;
        //    Database db;
        //    DataSet dsList = null;
        //    Collection<Entity.Billing.BranchMove> objList = new Collection<Entity.Billing.BranchMove>();
        //    Entity.Billing.BranchMove objBranchMove;
        //    Entity.patient objpatient; Entity.Discharge.Doctor objdoctor;

        //    try
        //    {
        //        db = VHMS.Entity.DBConnection.dbCon;
        //        DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_BRANCHMOVEREPORT);
        //        db.AddInParameter(cmd, "@FK_PatientID", DbType.Int32, oJobCardFilter.PatientID);
        //        db.AddInParameter(cmd, "@DateFrom", DbType.String, oJobCardFilter.DateFrom);
        //        db.AddInParameter(cmd, "@DateTo", DbType.String, oJobCardFilter.DateTo);
        //        db.AddInParameter(cmd, "@FK_DoctorID", DbType.Int32, oJobCardFilter.DoctorID);
        //        db.AddInParameter(cmd, "@FK_DescriptionID", DbType.Int32, oJobCardFilter.DescriptionID);
        //        db.AddInParameter(cmd, "@FK_UserID", DbType.Int32, oJobCardFilter.UserID);
        //        dsList = db.ExecuteDataSet(cmd);

        //        if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow drData in dsList.Tables[0].Rows)
        //            {
        //                objBranchMove = new Entity.Billing.BranchMove();
        //                objpatient = new Entity.patient();
        //                objdoctor = new Entity.Discharge.Doctor();

        //                objBranchMove.BranchMoveID = Convert.ToInt32(drData["PK_BranchMoveID"]);
        //                objBranchMove.BillNo = Convert.ToString(drData["BillNo"]);
        //              //  objBranchMove.BillDate = Convert.ToDateTime(drData["BillDate"]);
        //                objBranchMove.sBillDate = (Convert.ToDateTime(drData["BillDate"])).ToString("dd/MM/yyyy");

        //               // objpatient.patientID = Convert.ToInt32(drData["FK_patientID"]);
        //              //  objpatient.HName = Convert.ToString(drData["HName"]);
        //                objpatient.WName = Convert.ToString(drData["WName"]);
        //                objpatient.WMobileNo = Convert.ToString(drData["WMobileNo"]);
        //               // objpatient.OPDNo = Convert.ToString(drData["OPDNo"]);
        //                objBranchMove.Patient = objpatient;
        //               // objdoctor.DoctorID = Convert.ToInt32(drData["FK_DoctorID"]);
        //                objdoctor.DoctorName = Convert.ToString(drData["DoctorName"]);
        //                objBranchMove.Doctor = objdoctor;
        //               // objBranchMove.PaymentMode = Convert.ToString(drData["PaymentMode"]);
        //                objBranchMove.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
        //               // objBranchMove.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
        //              //  objBranchMove.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
        //                //objBranchMove.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
        //                //objBranchMove.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
        //                objBranchMove.Subtotal = Convert.ToDecimal(drData["Subtotal"]);
        //                objBranchMove.DescriptionName = Convert.ToString(drData["DescriptionName"]);
        //                objBranchMove.CUserName = Convert.ToString(drData["CUserName"]);
        //                objBranchMove.IsCancelled = Convert.ToBoolean(drData["IsCancelled"]);
        //                objBranchMove.CancelReason = Convert.ToString(drData["CancelReason"]);
        //                objList.Add(objBranchMove);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "BranchMove GetBranchMove | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return objList;
        //}
        public static Entity.Billing.BranchMove GetBranchMoveByID(int iBranchMoveID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.BranchMove objBranchMove = new Entity.Billing.BranchMove();
            Entity.Branch objFromBranch;
            Entity.Branch objToBranch;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_BRANCHMOVE);
                db.AddInParameter(cmd, "@PK_BranchMoveID", DbType.Int32, iBranchMoveID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objBranchMove = new Entity.Billing.BranchMove();
                        objFromBranch = new Entity.Branch();
                        objToBranch = new Entity.Branch();

                        objBranchMove.BranchMoveID = Convert.ToInt32(drData["PK_BranchMoveID"]);
                        objBranchMove.MoveNo = Convert.ToString(drData["MoveNo"]);
                        objBranchMove.MoveDate = Convert.ToDateTime(drData["MoveDate"]);
                        objBranchMove.sMoveDate = objBranchMove.MoveDate.ToString("dd/MM/yyyy");
                        objBranchMove.TotalQuantity = Convert.ToInt32(drData["TotalQuantity"]);
                        objBranchMove.TotalWeight = Convert.ToDecimal(drData["TotalWeight"]);
                        objFromBranch.BranchID = Convert.ToInt32(drData["FK_FromBranchID"]);
                        objFromBranch.BranchName = Convert.ToString(drData["FromBranchName"]);
                        objFromBranch.MobileNo = Convert.ToString(drData["FromBranchMobileNo"]);
                        objFromBranch.Address = Convert.ToString(drData["FromBranchAddress"]);
                        objBranchMove.FromBranch = objFromBranch;

                        objToBranch.BranchID = Convert.ToInt32(drData["FK_ToBranchID"]);
                        objToBranch.BranchName = Convert.ToString(drData["ToBranchName"]);
                        objToBranch.MobileNo = Convert.ToString(drData["ToBranchMobileNo"]);
                        objToBranch.Address = Convert.ToString(drData["ToBranchAddress"]);
                        objBranchMove.ToBranch = objToBranch;

                        objBranchMove.Status = Convert.ToString(drData["Status"]);
                        objBranchMove.BranchMoveTrans = BranchMoveTrans.GetBranchMoveTransByBranchMoveID(objBranchMove.BranchMoveID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "BranchMove GetBranchMoveByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objBranchMove;
        }

        public static Entity.Billing.BranchMove GetBranchMoveByMove(string MoveNo)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.BranchMove objBranchMove = new Entity.Billing.BranchMove();
            Entity.Branch objFromBranch;
            Entity.Branch objToBranch;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_BRANCHMOVEINVOICE);
                db.AddInParameter(cmd, "@MoveNo", DbType.String, MoveNo);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objBranchMove = new Entity.Billing.BranchMove();
                        objFromBranch = new Entity.Branch();
                        objToBranch = new Entity.Branch();

                        objBranchMove.BranchMoveID = Convert.ToInt32(drData["PK_BranchMoveID"]);
                        objBranchMove.MoveNo = Convert.ToString(drData["MoveNo"]);
                        objBranchMove.MoveDate = Convert.ToDateTime(drData["MoveDate"]);
                        objBranchMove.sMoveDate = objBranchMove.MoveDate.ToString("dd/MM/yyyy");
                        objBranchMove.TotalQuantity = Convert.ToInt32(drData["TotalQuantity"]);
                       // objBranchMove.TotalWeight = Convert.ToDecimal(drData["TotalWeight"]);
                        objFromBranch.BranchID = Convert.ToInt32(drData["FK_FromBranchID"]);
                        objFromBranch.BranchName = Convert.ToString(drData["FromBranchName"]);
                        objFromBranch.MobileNo = Convert.ToString(drData["FromBranchMobileNo"]);
                        objFromBranch.Address = Convert.ToString(drData["FromBranchAddress"]);
                        objBranchMove.FromBranch = objFromBranch;

                        objToBranch.BranchID = Convert.ToInt32(drData["FK_ToBranchID"]);
                        objToBranch.BranchName = Convert.ToString(drData["ToBranchName"]);
                        objToBranch.MobileNo = Convert.ToString(drData["ToBranchMobileNo"]);
                        objToBranch.Address = Convert.ToString(drData["ToBranchAddress"]);
                        objBranchMove.ToBranch = objToBranch;

                        objBranchMove.Status = Convert.ToString(drData["Status"]);
                        objBranchMove.BranchMoveTrans = BranchMoveTrans.GetBranchMoveTransByBranchMoveID(objBranchMove.BranchMoveID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "BranchMove GetBranchMoveByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objBranchMove;
        }
        public static int AddBranchMove(Entity.Billing.BranchMove objBranchMove)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_BRANCHMOVE);
                db.AddOutParameter(cmd, "@PK_BranchMoveID", DbType.Int32, objBranchMove.BranchMoveID);
                db.AddInParameter(cmd, "@MoveNo", DbType.String, objBranchMove.MoveNo);
                db.AddInParameter(cmd, "@MoveDate", DbType.String, objBranchMove.sMoveDate);
                db.AddInParameter(cmd, "@FK_FromBranchID", DbType.Int32, objBranchMove.FromBranch.BranchID);
                db.AddInParameter(cmd, "@FK_ToBranchID", DbType.Int32, objBranchMove.ToBranch.BranchID);
                db.AddInParameter(cmd, "@TotalQuantity", DbType.Int32, objBranchMove.TotalQuantity);
                db.AddInParameter(cmd, "@TotalWeight", DbType.Decimal, objBranchMove.TotalWeight);
                db.AddInParameter(cmd, "@Status", DbType.String, "Waiting for Approval");               
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objBranchMove.CreatedBy.UserID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_BranchMoveID"));

                foreach (Entity.Billing.BranchMoveTrans ObjBranchMoveTrans in objBranchMove.BranchMoveTrans)
                    ObjBranchMoveTrans.BranchMoveID = iID;

                BranchMoveTrans.SaveBranchMoveTransaction(objBranchMove.BranchMoveTrans);
            }
            catch (Exception ex)
            {
                sException = "BranchMove AddBranchMove | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateBranchMove(Entity.Billing.BranchMove objBranchMove)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_BRANCHMOVE);
                db.AddInParameter(cmd, "@PK_BranchMoveID", DbType.Int32, objBranchMove.BranchMoveID);
                db.AddInParameter(cmd, "@MoveDate", DbType.String, objBranchMove.sMoveDate);
                db.AddInParameter(cmd, "@FK_FromBranchID", DbType.Int32, objBranchMove.FromBranch.BranchID);
                db.AddInParameter(cmd, "@FK_ToBranchID", DbType.Int32, objBranchMove.ToBranch.BranchID);
                db.AddInParameter(cmd, "@Status", DbType.String, objBranchMove.Status);
                db.AddInParameter(cmd, "@TotalQuantity", DbType.Int32, objBranchMove.TotalQuantity);
                db.AddInParameter(cmd, "@TotalWeight", DbType.Decimal, objBranchMove.TotalWeight);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objBranchMove.ModifiedBy.UserID);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                BranchMoveTrans.SaveBranchMoveTransaction(objBranchMove.BranchMoveTrans);
            }
            catch (Exception ex)
            {
                sException = "BranchMove UpdateBranchMove| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteBranchMove(int iBranchMoveId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_BRANCHMOVE);
                db.AddInParameter(cmd, "@PK_BranchMoveID", DbType.Int32, iBranchMoveId);
               // db.AddInParameter(cmd, "@Reason", DbType.String, Reason);
              //  db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, iUserId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "BranchMove DeleteBranchMove | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

        public static bool UpdateMoveStatus(int ID, string Status, int iUserID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_BRANCHMOVESTATUS);
                db.AddInParameter(cmd, "@PK_BranchMoveID", DbType.Int32, ID);
                db.AddInParameter(cmd, "@Status", DbType.String, Status);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.String, iUserID);
                // db.AddInParameter(cmd, "@Reason", DbType.String, Reason);
                //  db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, iUserId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "BranchMove DeleteBranchMove | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static DataSet GetBranchMoveSummary()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_BRANCHMOVESUMMARY);
                dsList = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "BranchMove GetBranchMoveSummary | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsList;
        }
    }

    public class BranchMoveTrans
    {
        public static Collection<Entity.Billing.BranchMoveTrans> GetBranchMoveTransByBranchMoveID(int iBranchMoveID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.BranchMoveTrans> objList = new Collection<Entity.Billing.BranchMoveTrans>();
            Entity.Billing.BranchMoveTrans objBranchMoveTrans = new Entity.Billing.BranchMoveTrans();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_BRANCHMOVETRANS);
                db.AddInParameter(cmd, "@FK_BranchMoveID", DbType.Int32, iBranchMoveID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objBranchMoveTrans = new Entity.Billing.BranchMoveTrans();

                        objBranchMoveTrans.BranchMoveTransID = Convert.ToInt32(drData["PK_BranchMoveTransID"]);
                        objBranchMoveTrans.BranchMoveID = Convert.ToInt32(drData["FK_BranchMoveID"]);
                        objBranchMoveTrans.StockID = Convert.ToInt32(drData["Fk_StockID"]);
                        objBranchMoveTrans.Quantity = Convert.ToInt32(drData["Quantity"]);
                        objBranchMoveTrans.CategoryName = Convert.ToString(drData["CategoryName"]);
                        objBranchMoveTrans.Barcode = Convert.ToString(drData["Barcode"]);
                        objBranchMoveTrans.ProductName = Convert.ToString(drData["ProductName"]);
                        objBranchMoveTrans.WastagePercent = Convert.ToDecimal(drData["WastagePercent"]);
                        objBranchMoveTrans.Wastage = Convert.ToDecimal(drData["Wastage"]);
                        objBranchMoveTrans.TotalWeight = Convert.ToDecimal(drData["TotalWeight"]);
                        objBranchMoveTrans.NetWeight = Convert.ToDecimal(drData["NetWeight"]);
                        objBranchMoveTrans.Ratti = Convert.ToDecimal(drData["Ratti"]);
                        objBranchMoveTrans.PureWeight = Convert.ToDecimal(drData["PureWeight"]);
                        objBranchMoveTrans.Lacquer = Convert.ToDecimal(drData["Lacquer"]);
                        objBranchMoveTrans.Design = Convert.ToString(drData["Design"]);
                        objBranchMoveTrans.Karat = Convert.ToString(drData["Karat"]);
                        objBranchMoveTrans.SellingPrice = Convert.ToDecimal(drData["SellingPrice"]);
                        objBranchMoveTrans.StoneName = Convert.ToString(drData["StoneName"]);
                        objBranchMoveTrans.StoneQuantity = Convert.ToInt32(drData["StoneQuantity"]);
                        objBranchMoveTrans.StonePrice = Convert.ToDecimal(drData["StonePrice"]);
                        objBranchMoveTrans.StoneWeight = Convert.ToDecimal(drData["StoneWeight"]);

                        objList.Add(objBranchMoveTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "BranchMoveTrans GetBranchMoveTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveBranchMoveTransaction(Collection<Entity.Billing.BranchMoveTrans> ObjBranchMoveTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.BranchMoveTrans ObjBranchMoveTransaction in ObjBranchMoveTransList)
            {
                if (ObjBranchMoveTransaction.StatusFlag == "I")
                    iID = AddBranchMoveTrans(ObjBranchMoveTransaction);
                else if (ObjBranchMoveTransaction.StatusFlag == "U")
                    bResult = UpdateBranchMoveTrans(ObjBranchMoveTransaction);
                else if (ObjBranchMoveTransaction.StatusFlag == "D")
                    bResult = DeleteBranchMoveTrans(ObjBranchMoveTransaction.BranchMoveTransID);
            }
        }
        public static int AddBranchMoveTrans(Entity.Billing.BranchMoveTrans objBranchMoveTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_BRANCHMOVETRANS);
                db.AddOutParameter(cmd, "@PK_BranchMoveTransID", DbType.Int32, objBranchMoveTrans.BranchMoveTransID);
                db.AddInParameter(cmd, "@FK_BranchMoveID", DbType.Int32, objBranchMoveTrans.BranchMoveID);
                db.AddInParameter(cmd, "@FK_StockID", DbType.Int32, objBranchMoveTrans.Stock.StockID);
                db.AddInParameter(cmd, "@Quantity", DbType.Int32, objBranchMoveTrans.Quantity);
                db.AddInParameter(cmd, "@Barcode", DbType.String, objBranchMoveTrans.Barcode);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_BranchMoveTransID"));
            }
            catch (Exception ex)
            {
                sException = "BranchMoveTrans AddBranchMoveTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateBranchMoveTrans(Entity.Billing.BranchMoveTrans objBranchMoveTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_BRANCHMOVETRANS);
                db.AddInParameter(cmd, "@PK_BranchMoveTransID", DbType.Int32, objBranchMoveTrans.BranchMoveTransID);
                db.AddInParameter(cmd, "@FK_BranchMoveID", DbType.Int32, objBranchMoveTrans.BranchMoveID);
                db.AddInParameter(cmd, "@FK_StockID", DbType.Int32, objBranchMoveTrans.Stock.StockID);
                db.AddInParameter(cmd, "@Quantity", DbType.Int32, objBranchMoveTrans.Quantity);
                db.AddInParameter(cmd, "@Barcode", DbType.String, objBranchMoveTrans.Barcode);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "BranchMoveTrans UpdateBranchMoveTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteBranchMoveTrans(int iBranchMoveTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_BRANCHMOVETRANS);
                db.AddInParameter(cmd, "@PK_BranchMoveTransID", DbType.Int32, iBranchMoveTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "BranchMoveTrans DeleteBranchMoveTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        
    }
}
