using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections.ObjectModel;

namespace VHMS.DataAccess
{
    public class patient
    {
        public static Collection<Entity.patient> Getpatient()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.patient> objList = new Collection<Entity.patient>();
            Entity.patient objpatient = new Entity.patient();
            Entity.District objDistrict;
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PATIENT);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objpatient = new Entity.patient();
                        objDistrict = new Entity.District();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objpatient.patientID = Convert.ToInt32(drData["PK_patientID"]);
                        objpatient.HName = Convert.ToString(drData["HName"]);
                        objpatient.WName = Convert.ToString(drData["WName"]);
                        objpatient.HBloodGroup = Convert.ToString(drData["HBloodGroup"]);
                        objpatient.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objpatient.WBloodGroup = Convert.ToString(drData["WBloodGroup"]);
                        objpatient.HReferredBy = Convert.ToString(drData["HReferredBy"]);
                        objpatient.WReferredBy = Convert.ToString(drData["WReferredBy"]);
                        objpatient.HEmail = Convert.ToString(drData["HEmail"]);
                        objpatient.WEmail = Convert.ToString(drData["WEmail"]);
                        objpatient.HAddress = Convert.ToString(drData["HAddress"]);
                        objpatient.WAddress = Convert.ToString(drData["WAddress"]);
                        objpatient.HMobileNo = Convert.ToString(drData["HMobileNo"]);
                        objpatient.WMobileNo = Convert.ToString(drData["WMobileNo"]);
                        objpatient.HPincode = Convert.ToString(drData["HPincode"]);
                        objpatient.WPincode = Convert.ToString(drData["WPincode"]);
                        objpatient.HCity = Convert.ToString(drData["HCity"]);
                        objpatient.WCity = Convert.ToString(drData["WCity"]);
                        objpatient.HCountry = Convert.ToString(drData["HCountry"]);
                        objpatient.WCountry = Convert.ToString(drData["WCountry"]);
                        objpatient.HCountryID = Convert.ToInt32(drData["HCountryID"]);
                        objpatient.WCountryID = Convert.ToInt32(drData["WCountryID"]);
                        objpatient.HAge = Convert.ToInt32(drData["HAge"]);
                        objpatient.WAge = Convert.ToInt32(drData["WAge"]);
                        objpatient.HDOB = Convert.ToDateTime(drData["HDOB"]);
                        objpatient.sHDOB = objpatient.HDOB.ToString("dd/MM/yyyy");
                        objpatient.WDOB = Convert.ToDateTime(drData["WDOB"]);
                        objpatient.sWDOB = objpatient.WDOB.ToString("dd/MM/yyyy");
                        objpatient.OPDNo = Convert.ToString(drData["OPDNo"]);
                        objpatient.HImage = Convert.ToString(drData["HImage"]);
                        objpatient.WImage = Convert.ToString(drData["WImage"]);
                        objpatient.Category = Convert.ToString(drData["Category"]);
                        objpatient.HIDProof = Convert.ToString(drData["HIDProof"]);
                        objpatient.WIDProof = Convert.ToString(drData["WIDProof"]);
                        objpatient.HReferredDetails = Convert.ToString(drData["HReferredDetails"]);
                        objpatient.WReferredDetails = Convert.ToString(drData["WReferredDetails"]);
                        objpatient.LastOPDNo = Convert.ToString(drData["LastOPDNo"]);
                        objCreatedUser.UserID = Convert.ToInt32(drData["FK_CreatedBy"]);
                        objCreatedUser.UserName = Convert.ToString(drData["CUserName"]);
                        objpatient.CreatedBy = objCreatedUser;
                        objpatient.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objpatient.HProfession = Convert.ToString(drData["HProfession"]);
                        objpatient.WProfession = Convert.ToString(drData["WProfession"]);

                        objModifiedUser.UserID = Convert.ToInt32(drData["FK_CreatedBy"]);
                        objModifiedUser.UserName = Convert.ToString(drData["MUserName"]);
                        objpatient.ModifiedBy = objModifiedUser;
                        objpatient.ModifiedOn = Convert.ToDateTime(drData["ModifiedOn"]);
                        objpatient.RefDoctorMobileNo = Convert.ToString(drData["RefDoctorMobileNo"]);
                        objList.Add(objpatient);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "patient Getpatient | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }


        public static Collection<Entity.patient> GetpatientName()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.patient> objList = new Collection<Entity.patient>();
            Entity.patient objpatient = new Entity.patient();
            Entity.District objDistrict;
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PATIENT);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objpatient = new Entity.patient();
                        objDistrict = new Entity.District();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objpatient.patientID = Convert.ToInt32(drData["PK_patientID"]);
                        objpatient.WName = Convert.ToString(drData["WName"]);
                        objpatient.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objpatient.WMobileNo = Convert.ToString(drData["WMobileNo"]);
                        objList.Add(objpatient);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "patient Getpatient | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.patient> GetpatientID()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.patient> objList = new Collection<Entity.patient>();
            Entity.patient objpatient = new Entity.patient();
            Entity.District objDistrict;
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PATIENTID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objpatient = new Entity.patient();
                        objDistrict = new Entity.District();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objpatient.patientID = Convert.ToInt32(drData["PK_patientID"]);
                        objpatient.HName = Convert.ToString(drData["HName"]);
                        objpatient.WName = Convert.ToString(drData["WName"]);
                        objpatient.HBloodGroup = Convert.ToString(drData["HBloodGroup"]);
                        objpatient.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objpatient.WBloodGroup = Convert.ToString(drData["WBloodGroup"]);
                        objpatient.HReferredBy = Convert.ToString(drData["HReferredBy"]);
                        objpatient.WReferredBy = Convert.ToString(drData["WReferredBy"]);
                        objpatient.HEmail = Convert.ToString(drData["HEmail"]);
                        objpatient.WEmail = Convert.ToString(drData["WEmail"]);
                        objpatient.HAddress = Convert.ToString(drData["HAddress"]);
                        objpatient.WAddress = Convert.ToString(drData["WAddress"]);
                        objpatient.HMobileNo = Convert.ToString(drData["HMobileNo"]);
                        objpatient.WMobileNo = Convert.ToString(drData["WMobileNo"]);
                        objpatient.HPincode = Convert.ToString(drData["HPincode"]);
                        objpatient.WPincode = Convert.ToString(drData["WPincode"]);
                        objpatient.HCity = Convert.ToString(drData["HCity"]);
                        objpatient.WCity = Convert.ToString(drData["WCity"]);
                        objpatient.HCountry = Convert.ToString(drData["HCountry"]);
                        objpatient.WCountry = Convert.ToString(drData["WCountry"]);
                        objpatient.HCountryID = Convert.ToInt32(drData["HCountryID"]);
                        objpatient.WCountryID = Convert.ToInt32(drData["WCountryID"]);
                        objpatient.HAge = Convert.ToInt32(drData["HAge"]);
                        objpatient.WAge = Convert.ToInt32(drData["WAge"]);
                        objpatient.HDOB = Convert.ToDateTime(drData["HDOB"]);
                        objpatient.sHDOB = objpatient.HDOB.ToString("dd/MM/yyyy");
                        objpatient.WDOB = Convert.ToDateTime(drData["WDOB"]);
                        objpatient.sWDOB = objpatient.WDOB.ToString("dd/MM/yyyy");
                        objpatient.OPDNo = Convert.ToString(drData["OPDNo"]);
                        objpatient.HImage = Convert.ToString(drData["HImage"]);
                        objpatient.WImage = Convert.ToString(drData["WImage"]);
                        objpatient.Category = Convert.ToString(drData["Category"]);
                        objpatient.HIDProof = Convert.ToString(drData["HIDProof"]);
                        objpatient.WIDProof = Convert.ToString(drData["WIDProof"]);
                        objpatient.HReferredDetails = Convert.ToString(drData["HReferredDetails"]);
                        objpatient.WReferredDetails = Convert.ToString(drData["WReferredDetails"]);
                        objpatient.LastOPDNo = Convert.ToString(drData["LastOPDNo"]);
                        objCreatedUser.UserID = Convert.ToInt32(drData["FK_CreatedBy"]);
                        objCreatedUser.UserName = Convert.ToString(drData["CUserName"]);
                        objpatient.CreatedBy = objCreatedUser;
                        objpatient.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objpatient.HProfession = Convert.ToString(drData["HProfession"]);
                        objpatient.WProfession = Convert.ToString(drData["WProfession"]);

                        objModifiedUser.UserID = Convert.ToInt32(drData["FK_CreatedBy"]);
                        objModifiedUser.UserName = Convert.ToString(drData["MUserName"]);
                        objpatient.ModifiedBy = objModifiedUser;
                        objpatient.ModifiedOn = Convert.ToDateTime(drData["ModifiedOn"]);
                        objpatient.RefDoctorMobileNo = Convert.ToString(drData["RefDoctorMobileNo"]);
                        objList.Add(objpatient);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "patient Getpatient | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.patient> GetSearchpatient(string ipatientID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.patient> objList = new Collection<Entity.patient>();
            Entity.patient objpatient = new Entity.patient();
            Entity.District objDistrict;
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PATIENTDYNAMIC);
                db.AddInParameter(cmd, "@OPDNo", DbType.String, ipatientID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objpatient = new Entity.patient();
                        objDistrict = new Entity.District();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objpatient.patientID = Convert.ToInt32(drData["PK_patientID"]);
                        objpatient.HName = Convert.ToString(drData["HName"]);
                        objpatient.WName = Convert.ToString(drData["WName"]);
                        objpatient.HBloodGroup = Convert.ToString(drData["HBloodGroup"]);
                        objpatient.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objpatient.WBloodGroup = Convert.ToString(drData["WBloodGroup"]);
                        objpatient.HReferredBy = Convert.ToString(drData["HReferredBy"]);
                        objpatient.WReferredBy = Convert.ToString(drData["WReferredBy"]);
                        objpatient.HEmail = Convert.ToString(drData["HEmail"]);
                        objpatient.WEmail = Convert.ToString(drData["WEmail"]);
                        objpatient.HAddress = Convert.ToString(drData["HAddress"]);
                        objpatient.WAddress = Convert.ToString(drData["WAddress"]);
                        objpatient.HMobileNo = Convert.ToString(drData["HMobileNo"]);
                        objpatient.WMobileNo = Convert.ToString(drData["WMobileNo"]);
                        objpatient.HPincode = Convert.ToString(drData["HPincode"]);
                        objpatient.WPincode = Convert.ToString(drData["WPincode"]);
                        objpatient.HCity = Convert.ToString(drData["HCity"]);
                        objpatient.WCity = Convert.ToString(drData["WCity"]);
                        objpatient.HCountry = Convert.ToString(drData["HCountry"]);
                        objpatient.WCountry = Convert.ToString(drData["WCountry"]);
                        objpatient.HCountryID = Convert.ToInt32(drData["HCountryID"]);
                        objpatient.WCountryID = Convert.ToInt32(drData["WCountryID"]);
                        objpatient.HAge = Convert.ToInt32(drData["HAge"]);
                        objpatient.WAge = Convert.ToInt32(drData["WAge"]);
                        objpatient.HDOB = Convert.ToDateTime(drData["HDOB"]);
                        objpatient.sHDOB = objpatient.HDOB.ToString("dd/MM/yyyy");
                        objpatient.WDOB = Convert.ToDateTime(drData["WDOB"]);
                        objpatient.sWDOB = objpatient.WDOB.ToString("dd/MM/yyyy");
                        objpatient.OPDNo = Convert.ToString(drData["OPDNo"]);
                        objpatient.HImage = Convert.ToString(drData["HImage"]);
                        objpatient.WImage = Convert.ToString(drData["WImage"]);
                        objpatient.Category = Convert.ToString(drData["Category"]);
                        objpatient.HIDProof = Convert.ToString(drData["HIDProof"]);
                        objpatient.WIDProof = Convert.ToString(drData["WIDProof"]);
                        objpatient.HReferredDetails = Convert.ToString(drData["HReferredDetails"]);
                        objpatient.WReferredDetails = Convert.ToString(drData["WReferredDetails"]);
                        objpatient.LastOPDNo = Convert.ToString(drData["LastOPDNo"]);
                        objCreatedUser.UserID = Convert.ToInt32(drData["FK_CreatedBy"]);
                        objCreatedUser.UserName = Convert.ToString(drData["CUserName"]);
                        objpatient.CreatedBy = objCreatedUser;
                        objpatient.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objpatient.HProfession = Convert.ToString(drData["HProfession"]);
                        objpatient.WProfession = Convert.ToString(drData["WProfession"]);

                        objModifiedUser.UserID = Convert.ToInt32(drData["FK_CreatedBy"]);
                        objModifiedUser.UserName = Convert.ToString(drData["MUserName"]);
                        objpatient.ModifiedBy = objModifiedUser;
                        objpatient.ModifiedOn = Convert.ToDateTime(drData["ModifiedOn"]);
                        objpatient.RefDoctorMobileNo = Convert.ToString(drData["RefDoctorMobileNo"]);
                        objList.Add(objpatient);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "patient Getpatient | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }


        public static Collection<Entity.patient> GetpatientDetails(Entity.PatientFilter oJobCardFilter)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.patient> objList = new Collection<Entity.patient>();
            Entity.patient objpatient = new Entity.patient();
            Entity.District objDistrict;
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PATIENTDETAILS);
                db.AddInParameter(cmd, "@FK_PatientID", DbType.Int32, oJobCardFilter.PatientID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, oJobCardFilter.DateFrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, oJobCardFilter.DateTo);
                db.AddInParameter(cmd, "@Category", DbType.String, oJobCardFilter.Category);
                db.AddInParameter(cmd, "@ReferredBy", DbType.String, oJobCardFilter.ReferredBy);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objpatient = new Entity.patient();
                        objDistrict = new Entity.District();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objpatient.patientID = Convert.ToInt32(drData["PK_patientID"]);
                        objpatient.HName = Convert.ToString(drData["HName"]);
                        objpatient.WName = Convert.ToString(drData["WName"]);
                      //  objpatient.HBloodGroup = Convert.ToString(drData["HBloodGroup"]);
                        objpatient.IsActive = Convert.ToBoolean(drData["IsActive"]);
                       // objpatient.WBloodGroup = Convert.ToString(drData["WBloodGroup"]);
                        objpatient.HReferredBy = Convert.ToString(drData["HReferredBy"]);
                        objpatient.WReferredBy = Convert.ToString(drData["WReferredBy"]);
                       // objpatient.HEmail = Convert.ToString(drData["HEmail"]);
                        objpatient.WEmail = Convert.ToString(drData["WEmail"]);
                        //objpatient.HAge = Convert.ToInt32(drData["HAge"]);
                        //objpatient.WAge = Convert.ToInt32(drData["WAge"]);
                        //objpatient.HAddress = Convert.ToString(drData["HAddress"]);
                        objpatient.WAddress = Convert.ToString(drData["WAddress"]);
                        //objpatient.HMobileNo = Convert.ToString(drData["HMobileNo"]);
                        objpatient.WMobileNo = Convert.ToString(drData["WMobileNo"]);
                        //objpatient.HPincode = Convert.ToString(drData["HPincode"]);
                        //objpatient.WPincode = Convert.ToString(drData["WPincode"]);
                        //objpatient.HCity = Convert.ToString(drData["HCity"]);
                        //objpatient.WCity = Convert.ToString(drData["WCity"]);
                        //objpatient.HCountry = Convert.ToString(drData["HCountry"]);
                        //objpatient.WCountry = Convert.ToString(drData["WCountry"]);
                        //objpatient.HDOB = Convert.ToDateTime(drData["HDOB"]);
                        //objpatient.sHDOB = objpatient.HDOB.ToString("dd/MM/yyyy");
                        //objpatient.WDOB = Convert.ToDateTime(drData["WDOB"]);
                        //objpatient.sWDOB = objpatient.WDOB.ToString("dd/MM/yyyy");
                        objpatient.OPDNo = Convert.ToString(drData["OPDNo"]);
                        //objpatient.HImage = Convert.ToString(drData["HImage"]);
                        //objpatient.WImage = Convert.ToString(drData["WImage"]);
                        objpatient.Category = Convert.ToString(drData["Category"]);
                        //objpatient.HIDProof = Convert.ToString(drData["HIDProof"]);
                        //objpatient.WIDProof = Convert.ToString(drData["WIDProof"]);
                        objpatient.HReferredDetails = Convert.ToString(drData["HReferredDetails"]);
                        objpatient.WReferredDetails = Convert.ToString(drData["WReferredDetails"]);
                        //objpatient.HCountryID = Convert.ToInt32(drData["HCountryID"]);
                        //objpatient.WCountryID = Convert.ToInt32(drData["WCountryID"]);
                        //objpatient.LastOPDNo = Convert.ToString(drData["LastOPDNo"]);
                        //objCreatedUser.UserID = Convert.ToInt32(drData["FK_CreatedBy"]);
                       // objCreatedUser.UserName = Convert.ToString(drData["CUserName"]);
                      //  objpatient.CreatedBy = objCreatedUser;
                      //  objpatient.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                       // objpatient.HProfession = Convert.ToString(drData["HProfession"]);
                       // objpatient.WProfession = Convert.ToString(drData["WProfession"]);

                      //  objModifiedUser.UserID = Convert.ToInt32(drData["FK_CreatedBy"]);
                        //objModifiedUser.UserName = Convert.ToString(drData["MUserName"]);
                        //objpatient.ModifiedBy = objModifiedUser;
                        //objpatient.ModifiedOn = Convert.ToDateTime(drData["ModifiedOn"]);
                        objpatient.RefDoctorMobileNo = Convert.ToString(drData["RefDoctorMobileNo"]);
                        objList.Add(objpatient);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "patient Getpatient | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.patient GetpatientByID(int ipatientID)
        {
            string sException = string.Empty;
            Database db;
            Entity.patient objpatient = new Entity.patient();
            Entity.District objDistrict;
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PATIENT);
                db.AddInParameter(cmd, "@PK_patientID", DbType.Int32, ipatientID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objpatient = new Entity.patient();
                        objDistrict = new Entity.District();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objpatient.patientID = Convert.ToInt32(drData["PK_patientID"]);
                        objpatient.HName = Convert.ToString(drData["HName"]);
                        objpatient.WName = Convert.ToString(drData["WName"]);
                        objpatient.HBloodGroup = Convert.ToString(drData["HBloodGroup"]);
                        objpatient.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objpatient.WBloodGroup = Convert.ToString(drData["WBloodGroup"]);
                        objpatient.HReferredBy = Convert.ToString(drData["HReferredBy"]);
                        objpatient.WReferredBy = Convert.ToString(drData["WReferredBy"]);
                        objpatient.HEmail = Convert.ToString(drData["HEmail"]);
                        objpatient.WEmail = Convert.ToString(drData["WEmail"]);
                        objpatient.HAge = Convert.ToInt32(drData["HAge"]);
                        objpatient.WAge = Convert.ToInt32(drData["WAge"]);
                        objpatient.HAddress = Convert.ToString(drData["HAddress"]);
                        objpatient.WAddress = Convert.ToString(drData["WAddress"]);
                        objpatient.HMobileNo = Convert.ToString(drData["HMobileNo"]);
                        objpatient.WMobileNo = Convert.ToString(drData["WMobileNo"]);
                        objpatient.HCountryID = Convert.ToInt32(drData["HCountryID"]);
                        objpatient.WCountryID = Convert.ToInt32(drData["WCountryID"]);
                        objpatient.HPincode = Convert.ToString(drData["HPincode"]);
                        objpatient.WPincode = Convert.ToString(drData["WPincode"]);
                        objpatient.HCity = Convert.ToString(drData["HCity"]);
                        objpatient.WCity = Convert.ToString(drData["WCity"]);
                        objpatient.HCountry = Convert.ToString(drData["HCountry"]);
                        objpatient.WCountry = Convert.ToString(drData["WCountry"]);
                        objpatient.HNationality = Convert.ToString(drData["HNationality"]);
                        objpatient.WNationality = Convert.ToString(drData["WNationality"]);
                        objpatient.HDOB = Convert.ToDateTime(drData["HDOB"]);
                        objpatient.sHDOB = objpatient.HDOB.ToString("dd/MM/yyyy");
                        objpatient.WDOB = Convert.ToDateTime(drData["WDOB"]);
                        objpatient.sWDOB = objpatient.WDOB.ToString("dd/MM/yyyy");
                        objpatient.OPDNo = Convert.ToString(drData["OPDNo"]);
                        objpatient.HImage = Convert.ToString(drData["HImage"]);
                        objpatient.WImage = Convert.ToString(drData["WImage"]);
                        objpatient.Category = Convert.ToString(drData["Category"]);
                        objpatient.HIDProof = Convert.ToString(drData["HIDProof"]);
                        objpatient.WIDProof = Convert.ToString(drData["WIDProof"]);
                        objpatient.HReferredDetails = Convert.ToString(drData["HReferredDetails"]);
                        objpatient.WReferredDetails = Convert.ToString(drData["WReferredDetails"]);
                        objpatient.HProfession = Convert.ToString(drData["HProfession"]);
                        objpatient.WProfession = Convert.ToString(drData["WProfession"]);

                        objCreatedUser.UserID = Convert.ToInt32(drData["FK_CreatedBy"]);
                        objCreatedUser.UserName = Convert.ToString(drData["CUserName"]);
                        objpatient.CreatedBy = objCreatedUser;
                        objpatient.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);

                        objModifiedUser.UserID = Convert.ToInt32(drData["FK_CreatedBy"]);
                        objModifiedUser.UserName = Convert.ToString(drData["MUserName"]);
                        objpatient.ModifiedBy = objModifiedUser;
                        objpatient.ModifiedOn = Convert.ToDateTime(drData["ModifiedOn"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "patient GetpatientByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objpatient;
        }
        public static Entity.patient GetPatientByOPDNo(string ipatientID)
        {
            string sException = string.Empty;
            Database db;
            Entity.patient objpatient = new Entity.patient();
            Entity.District objDistrict;
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PATIENTBYOPDNO);
                db.AddInParameter(cmd, "@OPDNo", DbType.String, ipatientID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objpatient = new Entity.patient();
                        objDistrict = new Entity.District();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objpatient.patientID = Convert.ToInt32(drData["PK_patientID"]);
                        objpatient.HName = Convert.ToString(drData["HName"]);
                        objpatient.WName = Convert.ToString(drData["WName"]);
                        objpatient.HBloodGroup = Convert.ToString(drData["HBloodGroup"]);
                        objpatient.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objpatient.WBloodGroup = Convert.ToString(drData["WBloodGroup"]);
                        objpatient.HReferredBy = Convert.ToString(drData["HReferredBy"]);
                        objpatient.WReferredBy = Convert.ToString(drData["WReferredBy"]);
                        objpatient.HEmail = Convert.ToString(drData["HEmail"]);
                        objpatient.WEmail = Convert.ToString(drData["WEmail"]);
                        objpatient.HAge = Convert.ToInt32(drData["HAge"]);
                        objpatient.WAge = Convert.ToInt32(drData["WAge"]);
                        objpatient.HAddress = Convert.ToString(drData["HAddress"]);
                        objpatient.WAddress = Convert.ToString(drData["WAddress"]);
                        objpatient.HMobileNo = Convert.ToString(drData["HMobileNo"]);
                        objpatient.WMobileNo = Convert.ToString(drData["WMobileNo"]);
                        objpatient.HPincode = Convert.ToString(drData["HPincode"]);
                        objpatient.WPincode = Convert.ToString(drData["WPincode"]);
                        objpatient.HCity = Convert.ToString(drData["HCity"]);
                        objpatient.WCity = Convert.ToString(drData["WCity"]);
                        objpatient.HCountry = Convert.ToString(drData["HCountry"]);
                        objpatient.WCountry = Convert.ToString(drData["WCountry"]);
                        objpatient.HCountryID = Convert.ToInt32(drData["HCountryID"]);
                        objpatient.WCountryID = Convert.ToInt32(drData["WCountryID"]);
                        objpatient.HNationality = Convert.ToString(drData["HNationality"]);
                        objpatient.WNationality = Convert.ToString(drData["WNationality"]);
                        objpatient.HDOB = Convert.ToDateTime(drData["HDOB"]);
                        objpatient.sHDOB = objpatient.HDOB.ToString("dd/MM/yyyy");
                        objpatient.WDOB = Convert.ToDateTime(drData["WDOB"]);
                        objpatient.sWDOB = objpatient.WDOB.ToString("dd/MM/yyyy");
                        objpatient.OPDNo = Convert.ToString(drData["OPDNo"]);
                        objpatient.HImage = Convert.ToString(drData["HImage"]);
                        objpatient.WImage = Convert.ToString(drData["WImage"]);
                        objpatient.Category = Convert.ToString(drData["Category"]);
                        objpatient.HIDProof = Convert.ToString(drData["HIDProof"]);
                        objpatient.WIDProof = Convert.ToString(drData["WIDProof"]);
                        objpatient.HReferredDetails = Convert.ToString(drData["HReferredDetails"]);
                        objpatient.WReferredDetails = Convert.ToString(drData["WReferredDetails"]);
                        objpatient.HProfession = Convert.ToString(drData["HProfession"]);
                        objpatient.WProfession = Convert.ToString(drData["WProfession"]);
                     

                        objCreatedUser.UserID = Convert.ToInt32(drData["FK_CreatedBy"]);
                        objCreatedUser.UserName = Convert.ToString(drData["CUserName"]);
                        objpatient.CreatedBy = objCreatedUser;
                        objpatient.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);

                        objModifiedUser.UserID = Convert.ToInt32(drData["FK_CreatedBy"]);
                        objModifiedUser.UserName = Convert.ToString(drData["MUserName"]);
                        objpatient.ModifiedBy = objModifiedUser;
                        objpatient.ModifiedOn = Convert.ToDateTime(drData["ModifiedOn"]);
                        objpatient.RefDoctorMobileNo = Convert.ToString(drData["RefDoctorMobileNo"]);

                        
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "patient GetpatientByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objpatient;
        }
        public static int Addpatient(Entity.patient objpatient)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_PATIENT);
                db.AddOutParameter(cmd, "@PK_patientID", DbType.Int32, objpatient.patientID);
                db.AddInParameter(cmd, "@HName", DbType.String, objpatient.HName);
                db.AddInParameter(cmd, "@WName", DbType.String, objpatient.WName);
                db.AddInParameter(cmd, "@HDOB", DbType.String, objpatient.sHDOB);
                db.AddInParameter(cmd, "@WDOB", DbType.String, objpatient.sWDOB);
                db.AddInParameter(cmd, "@HBloodGroup", DbType.String, objpatient.HBloodGroup);
                db.AddInParameter(cmd, "@WBloodGroup", DbType.String, objpatient.WBloodGroup);
                db.AddInParameter(cmd, "@HReferredBy", DbType.String, objpatient.HReferredBy);
                db.AddInParameter(cmd, "@WReferredBy", DbType.String, objpatient.WReferredBy);
                db.AddInParameter(cmd, "@HEmail", DbType.String, objpatient.HEmail);
                db.AddInParameter(cmd, "@WEmail", DbType.String, objpatient.WEmail);
                db.AddInParameter(cmd, "@HAddress", DbType.String, objpatient.HAddress);
                db.AddInParameter(cmd, "@WAddress", DbType.String, objpatient.WAddress);
                db.AddInParameter(cmd, "@HMobileNo", DbType.String, objpatient.HMobileNo);
                db.AddInParameter(cmd, "@WMobileNo", DbType.String, objpatient.WMobileNo);
                db.AddInParameter(cmd, "@HPincode", DbType.String, objpatient.HPincode);
                db.AddInParameter(cmd, "@WPincode", DbType.String, objpatient.WPincode);
                db.AddInParameter(cmd, "@HNationality", DbType.String, objpatient.HNationality);
                db.AddInParameter(cmd, "@WNationality", DbType.String, objpatient.WNationality);
                db.AddInParameter(cmd, "@WReferredDetails", DbType.String, objpatient.WReferredDetails);
                db.AddInParameter(cmd, "@HReferredDetails", DbType.String, objpatient.HReferredDetails);
                db.AddInParameter(cmd, "@WIDProof", DbType.String, objpatient.WIDProof);
                db.AddInParameter(cmd, "@HIDProof", DbType.String, objpatient.HIDProof);
                db.AddInParameter(cmd, "@Category", DbType.String, objpatient.Category);
                db.AddInParameter(cmd, "@HImage", DbType.String, objpatient.HImage);
                db.AddInParameter(cmd, "@WImage", DbType.String, objpatient.WImage);
                db.AddInParameter(cmd, "@HCity", DbType.String, objpatient.HCity);
                db.AddInParameter(cmd, "@WCity", DbType.String, objpatient.WCity);
                db.AddInParameter(cmd, "@HAge", DbType.Int32, objpatient.HAge);
                db.AddInParameter(cmd, "@WAge", DbType.Int32, objpatient.WAge);
                db.AddInParameter(cmd, "@HCountryID", DbType.Int32, objpatient.HCountryID);
                db.AddInParameter(cmd, "@WCountryID", DbType.Int32, objpatient.WCountryID);
                db.AddInParameter(cmd, "@IsActive", DbType.Boolean, objpatient.IsActive);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objpatient.CreatedBy.UserID);
                db.AddInParameter(cmd, "@RefDoctorMobileNo", DbType.String, objpatient.RefDoctorMobileNo);
                db.AddInParameter(cmd, "@HProfession", DbType.String, objpatient.HProfession);
                db.AddInParameter(cmd, "@WProfession", DbType.String, objpatient.WProfession);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_patientID"));
            }
            catch (Exception ex)
            {
                sException = "patient Addpatient | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool Updatepatient(Entity.patient objpatient)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_PATIENT);
                db.AddInParameter(cmd, "@PK_patientID", DbType.Int32, objpatient.patientID);
                db.AddInParameter(cmd, "@HName", DbType.String, objpatient.HName);
                db.AddInParameter(cmd, "@WName", DbType.String, objpatient.WName);
                db.AddInParameter(cmd, "@HDOB", DbType.String, objpatient.sHDOB);
                db.AddInParameter(cmd, "@WDOB", DbType.String, objpatient.sWDOB);
                db.AddInParameter(cmd, "@HBloodGroup", DbType.String, objpatient.HBloodGroup);
                db.AddInParameter(cmd, "@WBloodGroup", DbType.String, objpatient.WBloodGroup);
                db.AddInParameter(cmd, "@HReferredBy", DbType.String, objpatient.HReferredBy);
                db.AddInParameter(cmd, "@WReferredBy", DbType.String, objpatient.WReferredBy);
                db.AddInParameter(cmd, "@HEmail", DbType.String, objpatient.HEmail);
                db.AddInParameter(cmd, "@WEmail", DbType.String, objpatient.WEmail);
                db.AddInParameter(cmd, "@HAddress", DbType.String, objpatient.HAddress);
                db.AddInParameter(cmd, "@WAddress", DbType.String, objpatient.WAddress);
                db.AddInParameter(cmd, "@HMobileNo", DbType.String, objpatient.HMobileNo);
                db.AddInParameter(cmd, "@WMobileNo", DbType.String, objpatient.WMobileNo);
                db.AddInParameter(cmd, "@HPincode", DbType.String, objpatient.HPincode);
                db.AddInParameter(cmd, "@WPincode", DbType.String, objpatient.WPincode);
                db.AddInParameter(cmd, "@HAge", DbType.Int32, objpatient.HAge);
                db.AddInParameter(cmd, "@WAge", DbType.Int32, objpatient.WAge);
                db.AddInParameter(cmd, "@HNationality", DbType.String, objpatient.HNationality);
                db.AddInParameter(cmd, "@WNationality", DbType.String, objpatient.WNationality);
                db.AddInParameter(cmd, "@WReferredDetails", DbType.String, objpatient.WReferredDetails);
                db.AddInParameter(cmd, "@HReferredDetails", DbType.String, objpatient.HReferredDetails);
                db.AddInParameter(cmd, "@WIDProof", DbType.String, objpatient.WIDProof);
                db.AddInParameter(cmd, "@HIDProof", DbType.String, objpatient.HIDProof);
                db.AddInParameter(cmd, "@Category", DbType.String, objpatient.Category);
                db.AddInParameter(cmd, "@HImage", DbType.String, objpatient.HImage);
                db.AddInParameter(cmd, "@WImage", DbType.String, objpatient.WImage);
                db.AddInParameter(cmd, "@HCity", DbType.String, objpatient.HCity);
                db.AddInParameter(cmd, "@WCity", DbType.String, objpatient.WCity);
                db.AddInParameter(cmd, "@HCountryID", DbType.Int32, objpatient.HCountryID);
                db.AddInParameter(cmd, "@WCountryID", DbType.Int32, objpatient.WCountryID);
                db.AddInParameter(cmd, "@IsActive", DbType.Boolean, objpatient.IsActive);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objpatient.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@RefDoctorMobileNo", DbType.String, objpatient.RefDoctorMobileNo);
                db.AddInParameter(cmd, "@HProfession", DbType.String, objpatient.HProfession);
                db.AddInParameter(cmd, "@WProfession", DbType.String, objpatient.WProfession);
               
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "patient Updatepatient| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool Deletepatient(int ipatientId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_PATIENT);
                db.AddInParameter(cmd, "@PK_patientID", DbType.Int32, ipatientId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "patient Deletepatient | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static DataSet Searchpatient(string sKey)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsTags = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_PATIENT);
                db.AddInParameter(cmd, "@key", DbType.String, sKey);
                dsTags = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "patient Searchpatient | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsTags;
        }
        public static DataSet GetpatientList(int ipatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PATIENT);
                db.AddInParameter(cmd, "@PK_patientID", DbType.Int32, ipatientID);
                dsList = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "patient Getpatient | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsList;
        }
    }
}
