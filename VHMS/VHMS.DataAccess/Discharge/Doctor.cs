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
    public class Doctor
    {
        public static Collection<Entity.Discharge.Doctor> GetDoctor(int DoctorTypeID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Discharge.Doctor> objList = new Collection<Entity.Discharge.Doctor>();
            Entity.Discharge.Doctor objDoctor = new Entity.Discharge.Doctor();
            Entity.Discharge.Specialization objSpecialization;
            Entity.Discharge.DoctorType objDoctorType;
            Entity.Discharge.Department objDepartment;
            Entity.State objState;
            Entity.Country objCountry;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DOCTOR);
                db.AddInParameter(cmd, "@FK_DoctorTypeID", DbType.Int32, DoctorTypeID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDoctor = new Entity.Discharge.Doctor();
                        objSpecialization = new Entity.Discharge.Specialization();
                        objDoctorType = new Entity.Discharge.DoctorType();
                        objDepartment = new Entity.Discharge.Department();
                        objState = new Entity.State();
                        objCountry = new Entity.Country();

                        objSpecialization.SpecializationID = Convert.ToInt32(drData["FK_SpecializationID"]);
                        objSpecialization.SpecializationName = Convert.ToString(drData["SpecializationName"]);
                        objDoctor.Specialization = objSpecialization;

                        objDoctorType.DoctorTypeID = Convert.ToInt32(drData["FK_DoctorTypeID"]);
                        objDoctorType.DoctorTypeName = Convert.ToString(drData["DoctorTypeName"]);
                        objDoctor.DoctorType = objDoctorType;

                        objDepartment.DepartmentID = Convert.ToInt32(drData["FK_DepartmentID"]);
                        objDepartment.DepartmentName = Convert.ToString(drData["DepartmentName"]);
                        objDoctor.Department = objDepartment;

                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objState.StateName = Convert.ToString(drData["StateName"]);
                        objCountry.CountryID = Convert.ToInt32(drData["FK_CountryID"]);
                        objState.Country = objCountry;
                        objDoctor.State = objState;

                        objDoctor.DoctorID = Convert.ToInt32(drData["PK_DoctorID"]);
                        objDoctor.DoctorName = Convert.ToString(drData["DoctorName"]);
                        objDoctor.IsRMODoctor = Convert.ToBoolean(drData["IsRMODoctor"]);
                        objDoctor.IsExternalDoctor = Convert.ToBoolean(drData["IsExternalDoctor"]);
                        objDoctor.Qualification = Convert.ToString(drData["Qualification"]);
                        objDoctor.Address = Convert.ToString(drData["Address"]);
                        objDoctor.City = Convert.ToString(drData["City"]);
                        objDoctor.Pincode = Convert.ToString(drData["Pincode"]);
                        objDoctor.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objDoctor.PhoneNo2 = Convert.ToString(drData["PhoneNo2"]);
                        objDoctor.PhoneNo3 = Convert.ToString(drData["PhoneNo3"]);
                        objDoctor.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objDoctor.FaxNo = Convert.ToString(drData["FaxNo"]);
                        objDoctor.Email = Convert.ToString(drData["Email"]);
                        objDoctor.DoctorNo = Convert.ToString(drData["DoctorNo"]);
                        objDoctor.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        //Added on 31-07-2017
                        objDoctor.DisplayOrder = Convert.ToInt16(drData["DisplayOrder"]);
                        objList.Add(objDoctor);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Doctor GetDoctor | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Discharge.Doctor GetDoctorByID(int iDoctorID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Discharge.Doctor objDoctor = new Entity.Discharge.Doctor();
            Entity.Discharge.Specialization objSpecialization;
            Entity.Discharge.DoctorType objDoctorType;
            Entity.Discharge.Department objDepartment;
            Entity.State objState;
            Entity.Country objCountry;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DOCTOR);
                db.AddInParameter(cmd, "@PK_DoctorID", DbType.Int32, iDoctorID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDoctor = new Entity.Discharge.Doctor();
                        objSpecialization = new Entity.Discharge.Specialization();
                        objDoctorType = new Entity.Discharge.DoctorType();
                        objDepartment = new Entity.Discharge.Department();
                        objState = new Entity.State();
                        objCountry = new Entity.Country();

                        objSpecialization.SpecializationID = Convert.ToInt32(drData["FK_SpecializationID"]);
                        objSpecialization.SpecializationName = Convert.ToString(drData["SpecializationName"]);
                        objDoctor.Specialization = objSpecialization;

                        objDoctorType.DoctorTypeID = Convert.ToInt32(drData["FK_DoctorTypeID"]);
                        objDoctorType.DoctorTypeName = Convert.ToString(drData["DoctorTypeName"]);
                        objDoctor.DoctorType = objDoctorType;

                        objDepartment.DepartmentID = Convert.ToInt32(drData["FK_DepartmentID"]);
                        objDepartment.DepartmentName = Convert.ToString(drData["DepartmentName"]);
                        objDoctor.Department = objDepartment;

                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objState.StateName = Convert.ToString(drData["StateName"]);
                        objCountry.CountryID = Convert.ToInt32(drData["FK_CountryID"]);
                        objState.Country = objCountry;
                        objDoctor.State = objState;

                        objDoctor.DoctorID = Convert.ToInt32(drData["PK_DoctorID"]);
                        objDoctor.DoctorName = Convert.ToString(drData["DoctorName"]);
                        objDoctor.IsRMODoctor = Convert.ToBoolean(drData["IsRMODoctor"]);
                        objDoctor.IsExternalDoctor = Convert.ToBoolean(drData["IsExternalDoctor"]);
                        objDoctor.Qualification = Convert.ToString(drData["Qualification"]);
                        objDoctor.Address = Convert.ToString(drData["Address"]);
                        objDoctor.City = Convert.ToString(drData["City"]);
                        objDoctor.Pincode = Convert.ToString(drData["Pincode"]);
                        objDoctor.PhoneNo1 = Convert.ToString(drData["PhoneNo1"]);
                        objDoctor.PhoneNo2 = Convert.ToString(drData["PhoneNo2"]);
                        objDoctor.PhoneNo3 = Convert.ToString(drData["PhoneNo3"]);
                        objDoctor.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objDoctor.FaxNo = Convert.ToString(drData["FaxNo"]);
                        objDoctor.Email = Convert.ToString(drData["Email"]);
                        objDoctor.DoctorNo = Convert.ToString(drData["DoctorNo"]);
                        objDoctor.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        //Added on 31-07-2017
                        objDoctor.DisplayOrder = Convert.ToInt16(drData["DisplayOrder"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Doctor GetDoctorByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objDoctor;
        }
        public static int AddDoctor(Entity.Discharge.Doctor objDoctor)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddDoctor(oDb, objDoctor, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tDoctor", "PK_DoctorID", objDoctor.DoctorID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objDoctor.CreatedBy.UserID);
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
        private static int AddDoctor(Database oDb, Entity.Discharge.Doctor objDoctor, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_DOCTOR);
                oDb.AddOutParameter(cmd, "@PK_DoctorID", DbType.Int32, objDoctor.DoctorID);
                oDb.AddInParameter(cmd, "@DoctorName", DbType.String, objDoctor.DoctorName);
                oDb.AddInParameter(cmd, "@FK_DoctorTypeID", DbType.Int32, objDoctor.DoctorType.DoctorTypeID);
                oDb.AddInParameter(cmd, "@FK_SpecializationID", DbType.Int32, objDoctor.Specialization.SpecializationID);
                oDb.AddInParameter(cmd, "@IsRMODoctor", DbType.Boolean, objDoctor.IsRMODoctor);
                oDb.AddInParameter(cmd, "@IsExternalDoctor", DbType.Boolean, objDoctor.IsExternalDoctor);
                oDb.AddInParameter(cmd, "@Qualification", DbType.String, objDoctor.Qualification);
                oDb.AddInParameter(cmd, "@Address", DbType.String, objDoctor.Address);
                oDb.AddInParameter(cmd, "@FK_StateID", DbType.Int32, objDoctor.State.StateID);
                oDb.AddInParameter(cmd, "@City", DbType.String, objDoctor.City);
                oDb.AddInParameter(cmd, "@Pincode", DbType.String, objDoctor.Pincode);
                oDb.AddInParameter(cmd, "@PhoneNo1", DbType.String, objDoctor.PhoneNo1);
                oDb.AddInParameter(cmd, "@PhoneNo2", DbType.String, objDoctor.PhoneNo2);
                oDb.AddInParameter(cmd, "@PhoneNo3", DbType.String, objDoctor.PhoneNo3);
                oDb.AddInParameter(cmd, "@MobileNo", DbType.String, objDoctor.MobileNo);
                oDb.AddInParameter(cmd, "@FaxNo", DbType.String, objDoctor.FaxNo);
                oDb.AddInParameter(cmd, "@Email", DbType.String, objDoctor.Email);
                oDb.AddInParameter(cmd, "@FK_DepartmentID", DbType.Int32, objDoctor.Department.DepartmentID);
                oDb.AddInParameter(cmd, "@DoctorNo", DbType.String, objDoctor.DoctorNo);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objDoctor.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objDoctor.CreatedBy.UserID);
                //Added on 31-07-2017
                oDb.AddInParameter(cmd, "@DisplayOrder", DbType.Int16, objDoctor.DisplayOrder);
                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_DoctorID"));
                    objDoctor.DoctorID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Doctor AddDoctor | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateDoctor(Entity.Discharge.Doctor objDoctor)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateDoctor(oDb, objDoctor, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tDoctor", "PK_DoctorID", objDoctor.DoctorID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objDoctor.ModifiedBy.UserID);
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
        private static bool UpdateDoctor(Database oDb, Entity.Discharge.Doctor objDoctor, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_DOCTOR);
                oDb.AddInParameter(cmd, "@PK_DoctorID", DbType.Int32, objDoctor.DoctorID);
                oDb.AddInParameter(cmd, "@DoctorName", DbType.String, objDoctor.DoctorName);
                oDb.AddInParameter(cmd, "@FK_SpecializationID", DbType.Int32, objDoctor.Specialization.SpecializationID);
                oDb.AddInParameter(cmd, "@FK_DoctorTypeID", DbType.Int32, objDoctor.DoctorType.DoctorTypeID);
                oDb.AddInParameter(cmd, "@IsRMODoctor", DbType.Boolean, objDoctor.IsRMODoctor);
                oDb.AddInParameter(cmd, "@IsExternalDoctor", DbType.Boolean, objDoctor.IsExternalDoctor);
                oDb.AddInParameter(cmd, "@Qualification", DbType.String, objDoctor.Qualification);
                oDb.AddInParameter(cmd, "@Address", DbType.String, objDoctor.Address);
                oDb.AddInParameter(cmd, "@FK_StateID", DbType.Int32, objDoctor.State.StateID);
                oDb.AddInParameter(cmd, "@City", DbType.String, objDoctor.City);
                oDb.AddInParameter(cmd, "@Pincode", DbType.String, objDoctor.Pincode);
                oDb.AddInParameter(cmd, "@PhoneNo1", DbType.String, objDoctor.PhoneNo1);
                oDb.AddInParameter(cmd, "@PhoneNo2", DbType.String, objDoctor.PhoneNo2);
                oDb.AddInParameter(cmd, "@PhoneNo3", DbType.String, objDoctor.PhoneNo3);
                oDb.AddInParameter(cmd, "@MobileNo", DbType.String, objDoctor.MobileNo);
                oDb.AddInParameter(cmd, "@FaxNo", DbType.String, objDoctor.FaxNo);
                oDb.AddInParameter(cmd, "@Email", DbType.String, objDoctor.Email);
                oDb.AddInParameter(cmd, "@FK_DepartmentID", DbType.Int32, objDoctor.Department.DepartmentID);
                oDb.AddInParameter(cmd, "@DoctorNo", DbType.String, objDoctor.DoctorNo);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objDoctor.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objDoctor.ModifiedBy.UserID);
                //Added on 31-07-2017
                oDb.AddInParameter(cmd, "@DisplayOrder", DbType.Int16, objDoctor.DisplayOrder);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Doctor UpdateDoctor | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteDoctor(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteDoctor(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tDoctor", "PK_DoctorID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
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
        private static bool DeleteDoctor(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_DOCTOR);
                oDb.AddInParameter(cmd, "@PK_DoctorID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Doctor DeleteDoctor | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
