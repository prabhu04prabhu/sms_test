using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections.ObjectModel;
using VHMS;

namespace VHMS.DataAccess
{
    public class User
    {
        public static DataSet GetMenuList()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                dsList = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM tMenuSection Where IsActive=1  ORDER BY MenuName");
            }
            catch (Exception ex)
            {
                sException = "User GetMenuSection() | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsList;
        }
        public static DataSet GetModuleList(int MenuID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            string sSql = string.Empty;
            try
            {
                db = Entity.DBConnection.dbCon;
                sSql = "SELECT * FROM tModule WHERE FK_MenuID=" + MenuID + " ORDER BY ModuleName";

                dsList = db.ExecuteDataSet(CommandType.Text, sSql);
            }
            catch (Exception ex)
            {
                sException = "User GetModuleName | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsList;
        }
        public static DataSet GetMenuandModule(int MenuID = 0)
        {
            Database db;
            DataSet dsList = null;
            string sException = string.Empty;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_MODULE);
                db.AddInParameter(cmd, "@FK_MenuID", DbType.Int32, MenuID);
                dsList = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "User GetMenuandModule | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsList;
        }
        public static Entity.User GetUserLogin(string sUserName, string sPassword, string sIPAddress, string clientIp)
        {
            string sException = string.Empty;
            Database db;
            Entity.User ObjUser = new Entity.User();
            Entity.Branch ObjBranch; Entity.Settings ObjSettings;
            Entity.Zone objZone; Entity.Region objRegion;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_USERLOGIN);
                db.AddInParameter(cmd, "@username", DbType.String, sUserName);
                db.AddInParameter(cmd, "@password", DbType.String, CommonMethods.Security.Encrypt(sPassword, true));
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drrow in dsList.Tables[0].Rows)
                    {
                        ObjUser = new Entity.User();
                        ObjBranch = new Entity.Branch();
                        ObjSettings = new Entity.Settings();
                        objZone = new Entity.Zone();
                        objRegion = new Entity.Region();

                        ObjUser.UserID = Convert.ToInt32(drrow["PK_UserID"]);
                        ObjUser.UserName = drrow["UserName"].ToString();
                        ObjUser.Password = CommonMethods.Security.Decrypt(drrow["Password"].ToString(), true);
                        ObjUser.RoleID = Convert.ToInt32(drrow["FK_RoleID"]);
                        ObjUser.RoleName = drrow["RoleName"].ToString();
                        ObjUser.EmployeeName = Convert.ToString(drrow["EmployeeName"]).ToString();
                        ObjUser.EmployeeCode = Convert.ToString(drrow["EmployeeCode"]).ToString();
                        ObjBranch.BranchID = Convert.ToInt32(drrow["FK_BranchID"]);
                        ObjUser.Branch = ObjBranch;

                        ObjUser.FinancialYearID = Convert.ToInt32(drrow["FinancialYearID"]);
                        ObjUser.Title = Convert.ToString(drrow["Title"]);

                        ObjSettings.SendSMS = Convert.ToBoolean(drrow["SendSMS"]);
                        ObjSettings.SMSUsername = Convert.ToString(drrow["SMSUsername"]);
                        ObjSettings.SMSPassword = Convert.ToString(drrow["SMSPassword"]);
                        ObjSettings.SenderName = Convert.ToString(drrow["SenderName"]);
                        ObjSettings.APILink = Convert.ToString(drrow["APILink"]);
                        ObjUser.Settings = ObjSettings;

                        objRegion.RegionID = Convert.ToInt32(drrow["FK_RegionID"]);
                        ObjUser.Region = objRegion;

                        objZone.ZoneID = Convert.ToInt32(drrow["FK_ZoneID"]);
                        ObjUser.Zone = objZone;

                        //ObjUser.HomePageID = Convert.ToInt16(drrow["FK_HomePageID"]);
                        //ObjUser.PageName = drrow["PageName"].ToString();
                        ///ObjUser.FileName = drrow["FileName"].ToString();
                    }
                    int i = AddLog(sIPAddress, ObjUser.UserID, clientIp);
                }
            }
            catch (Exception ex)
            {
                sException = "User GetUserLogin | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return ObjUser;
        }
        public static Collection<Entity.User> GetUser(int RegionID, int ZoneID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.User> objList = new Collection<Entity.User>();
            Entity.User objUser = new Entity.User();
            Entity.Branch objBranch; Entity.Discharge.Designation objDesignation;
            Entity.Zone objZone; Entity.Region objRegion;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_USER);
                db.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, RegionID);
                db.AddInParameter(cmd, "@FK_ZoneID", DbType.Int32, ZoneID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objUser = new Entity.User();
                        objBranch = new Entity.Branch();
                        objDesignation = new Entity.Discharge.Designation();
                        objZone = new Entity.Zone();
                        objRegion = new Entity.Region();

                        objUser.UserID = Convert.ToInt32(drData["PK_UserID"]);
                        objUser.UserName = Convert.ToString(drData["UserName"]);
                        objUser.Email = Convert.ToString(drData["Email"]);
                        objUser.RoleID = Convert.ToInt32(drData["FK_RoleID"]);
                        objUser.RoleName = Convert.ToString(drData["RoleName"]);
                        objUser.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objUser.Password = CommonMethods.Security.Decrypt(drData["Password"].ToString(), true);
                        objUser.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objUser.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objUser.IDProof = Convert.ToString(drData["IDProof"]);
                        objUser.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objUser.BasicPay = Convert.ToDecimal(drData["BasicPay"]);
                        objUser.DOB = Convert.ToDateTime(drData["DOB"]);
                        objUser.sDOB = objUser.DOB.ToString("dd/MM/yyyy");
                        objUser.DOJ = Convert.ToDateTime(drData["DOJ"]);
                        objUser.sDOJ = objUser.DOJ.ToString("dd/MM/yyyy");
                        objUser.Address = Convert.ToString(drData["Address"]);
                        objUser.ConfirmPassword = CommonMethods.Security.Decrypt(drData["ConfirmPassword"].ToString(), true);
                        // objUser.SalesManCode = Convert.ToString(drData["SalesManCode"]);

                        objRegion.RegionID = Convert.ToInt32(drData["FK_RegionID"]);
                        objRegion.RegionName = Convert.ToString(drData["RegionName"]);
                        objRegion.RegionCode = Convert.ToString(drData["RegionCode"]);
                        objUser.Region = objRegion;

                        objZone.ZoneID = Convert.ToInt32(drData["FK_ZoneID"]);
                        objZone.ZoneName = Convert.ToString(drData["ZoneName"]);
                        objZone.ZoneCode = Convert.ToString(drData["ZoneCode"]);
                        objUser.Zone = objZone;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objUser.Branch = objBranch;

                        objDesignation.DesignationID = Convert.ToInt32(drData["FK_DesignationID"]);
                        objDesignation.DesignationName = Convert.ToString(drData["DesignationName"]);
                        objUser.Designation = objDesignation;

                        objList.Add(objUser);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "User GetUser | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.User> GetUserStatus(int ID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.User> objList = new Collection<Entity.User>();
            Entity.User objUser = new Entity.User();
            Entity.Branch objBranch; Entity.Discharge.Designation objDesignation;
            Entity.Zone objZone; Entity.Region objRegion;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_USERSTATUS);
                db.AddInParameter(cmd, "@IsActive", DbType.Int32, ID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objUser = new Entity.User();
                        objBranch = new Entity.Branch();
                        objDesignation = new Entity.Discharge.Designation();
                        objZone = new Entity.Zone();
                        objRegion = new Entity.Region();

                        objUser.UserID = Convert.ToInt32(drData["PK_UserID"]);
                        objUser.UserName = Convert.ToString(drData["UserName"]);
                        objUser.Email = Convert.ToString(drData["Email"]);
                        objUser.RoleID = Convert.ToInt32(drData["FK_RoleID"]);
                        objUser.RoleName = Convert.ToString(drData["RoleName"]);
                        objUser.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objUser.Password = CommonMethods.Security.Decrypt(drData["Password"].ToString(), true);
                        objUser.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objUser.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objUser.IDProof = Convert.ToString(drData["IDProof"]);
                        objUser.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objUser.BasicPay = Convert.ToDecimal(drData["BasicPay"]);
                        objUser.DOB = Convert.ToDateTime(drData["DOB"]);
                        objUser.sDOB = objUser.DOB.ToString("dd/MM/yyyy");
                        objUser.DOJ = Convert.ToDateTime(drData["DOJ"]);
                        objUser.sDOJ = objUser.DOJ.ToString("dd/MM/yyyy");
                        objUser.Address = Convert.ToString(drData["Address"]);
                        objUser.ConfirmPassword = CommonMethods.Security.Decrypt(drData["ConfirmPassword"].ToString(), true);
                        // objUser.SalesManCode = Convert.ToString(drData["SalesManCode"]);

                        objRegion.RegionID = Convert.ToInt32(drData["FK_RegionID"]);
                        objRegion.RegionName = Convert.ToString(drData["RegionName"]);
                        objRegion.RegionCode = Convert.ToString(drData["RegionCode"]);
                        objUser.Region = objRegion;

                        objZone.ZoneID = Convert.ToInt32(drData["FK_ZoneID"]);
                        objZone.ZoneName = Convert.ToString(drData["ZoneName"]);
                        objZone.ZoneCode = Convert.ToString(drData["ZoneCode"]);
                        objUser.Zone = objZone;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objUser.Branch = objBranch;

                        objDesignation.DesignationID = Convert.ToInt32(drData["FK_DesignationID"]);
                        objDesignation.DesignationName = Convert.ToString(drData["DesignationName"]);
                        objUser.Designation = objDesignation;

                        objList.Add(objUser);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "User GetUser | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.User> GetBranchUser(int iBranchID=0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.User> objList = new Collection<Entity.User>();
            Entity.User objUser = new Entity.User();
            Entity.Branch objBranch; Entity.Discharge.Designation objDesignation;
            Entity.Zone objZone; Entity.Region objRegion;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_USER);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, iBranchID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objUser = new Entity.User();
                        objBranch = new Entity.Branch();
                        objDesignation = new Entity.Discharge.Designation();
                        objZone = new Entity.Zone();
                        objRegion = new Entity.Region();

                        objUser.UserID = Convert.ToInt32(drData["PK_UserID"]);
                        objUser.UserName = Convert.ToString(drData["UserName"]);
                        objUser.Email = Convert.ToString(drData["Email"]);
                        objUser.RoleID = Convert.ToInt32(drData["FK_RoleID"]);
                        objUser.RoleName = Convert.ToString(drData["RoleName"]);
                        objUser.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objUser.Password = CommonMethods.Security.Decrypt(drData["Password"].ToString(), true);
                        objUser.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objUser.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objUser.IDProof = Convert.ToString(drData["IDProof"]);
                        objUser.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objUser.BasicPay = Convert.ToDecimal(drData["BasicPay"]);
                        objUser.DOB = Convert.ToDateTime(drData["DOB"]);
                        objUser.sDOB = objUser.DOB.ToString("dd/MM/yyyy");
                        objUser.DOJ = Convert.ToDateTime(drData["DOJ"]);
                        objUser.sDOJ = objUser.DOJ.ToString("dd/MM/yyyy");
                        objUser.Address = Convert.ToString(drData["Address"]);
                        objUser.ConfirmPassword = CommonMethods.Security.Decrypt(drData["ConfirmPassword"].ToString(), true);
                        //objUser.SalesManCode = Convert.ToString(drData["SalesManCode"]);

                        objRegion.RegionID = Convert.ToInt32(drData["FK_RegionID"]);
                        objRegion.RegionName = Convert.ToString(drData["RegionName"]);
                        objRegion.RegionCode = Convert.ToString(drData["RegionCode"]);
                        objUser.Region = objRegion;

                        objZone.ZoneID = Convert.ToInt32(drData["FK_ZoneID"]);
                        objZone.ZoneName = Convert.ToString(drData["ZoneName"]);
                        objZone.ZoneCode = Convert.ToString(drData["ZoneCode"]);
                        objUser.Zone = objZone;


                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objUser.Branch = objBranch;

                        objDesignation.DesignationID = Convert.ToInt32(drData["FK_DesignationID"]);
                        objDesignation.DesignationName = Convert.ToString(drData["DesignationName"]);
                        objUser.Designation = objDesignation;

                        objList.Add(objUser);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "User GetUser | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.User GetUserByID(int iUserID)
        {
            string sException = string.Empty;
            Database db;
            Entity.User objUser = new Entity.User();
            Entity.Branch objBranch; Entity.Discharge.Designation objDesignation;
            Entity.Zone objZone; Entity.Region objRegion;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_USER);
                db.AddInParameter(cmd, "@PK_UserID", DbType.Int32, iUserID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objUser = new Entity.User();
                        objBranch = new Entity.Branch();
                        objDesignation = new Entity.Discharge.Designation();
                        objZone = new Entity.Zone();
                        objRegion = new Entity.Region();

                        objUser.UserID = Convert.ToInt32(drData["PK_UserID"]);
                        objUser.UserName = Convert.ToString(drData["UserName"]);
                        objUser.Email = Convert.ToString(drData["Email"]);
                        objUser.RoleID = Convert.ToInt32(drData["FK_RoleID"]);
                        objUser.RoleName = Convert.ToString(drData["RoleName"]);
                        objUser.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objUser.Password = CommonMethods.Security.Decrypt(drData["Password"].ToString(), true);
                        objUser.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objUser.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objUser.IDProof = Convert.ToString(drData["IDProof"]);
                        objUser.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objUser.BasicPay = Convert.ToDecimal(drData["BasicPay"]);
                        objUser.DOB = Convert.ToDateTime(drData["DOB"]);
                        objUser.sDOB = objUser.DOB.ToString("dd/MM/yyyy");
                        objUser.DOJ = Convert.ToDateTime(drData["DOJ"]);
                        objUser.sDOJ = objUser.DOJ.ToString("dd/MM/yyyy");
                        objUser.Address = Convert.ToString(drData["Address"]);
                        objUser.ConfirmPassword = CommonMethods.Security.Decrypt(drData["ConfirmPassword"].ToString(), true);
                        // objUser.SalesManCode = Convert.ToString(drData["SalesManCode"]);

                        objRegion.RegionID = Convert.ToInt32(drData["FK_RegionID"]);
                        objRegion.RegionName = Convert.ToString(drData["RegionName"]);
                        objRegion.RegionCode = Convert.ToString(drData["RegionCode"]);
                        objUser.Region = objRegion;

                        objZone.ZoneID = Convert.ToInt32(drData["FK_ZoneID"]);
                        objZone.ZoneName = Convert.ToString(drData["ZoneName"]);
                        objZone.ZoneCode = Convert.ToString(drData["ZoneCode"]);
                        objUser.Zone = objZone;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objUser.Branch = objBranch;

                        objDesignation.DesignationID = Convert.ToInt32(drData["FK_DesignationID"]);
                        objDesignation.DesignationName = Convert.ToString(drData["DesignationName"]);
                        objUser.Designation = objDesignation;
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "User GetUserByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objUser;
        }
        public static int AddUser(Entity.User objUser)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddUser(oDb, objUser, oTrans);
                    oTrans.Commit();
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
        private static int AddUser(Database oDb, Entity.User objUser, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_USER);
                oDb.AddOutParameter(cmd, "@PK_UserID", DbType.Int32, objUser.UserID);
                oDb.AddInParameter(cmd, "@UserName", DbType.String, objUser.UserName);
                oDb.AddInParameter(cmd, "@Password", DbType.String, CommonMethods.Security.Encrypt(objUser.Password, true));
                oDb.AddInParameter(cmd, "@Email", DbType.String, objUser.Email);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objUser.IsActive);
                oDb.AddInParameter(cmd, "@FK_RoleID", DbType.Int32, objUser.RoleID);
                oDb.AddInParameter(cmd, "@EmployeeName", DbType.String, objUser.EmployeeName);
                oDb.AddInParameter(cmd, "@EmployeeCode", DbType.String, objUser.EmployeeCode);
               // oDb.AddInParameter(cmd, "@SalesManCode", DbType.String, objUser.SalesManCode);
                oDb.AddInParameter(cmd, "@BasicPay", DbType.Decimal, objUser.BasicPay);
                oDb.AddInParameter(cmd, "@Address", DbType.String, objUser.Address);
                oDb.AddInParameter(cmd, "@ConfirmPassword", DbType.String, CommonMethods.Security.Encrypt(objUser.ConfirmPassword, true));
                oDb.AddInParameter(cmd, "@DOB", DbType.String, objUser.sDOB);
                oDb.AddInParameter(cmd, "@DOJ", DbType.String, objUser.sDOJ);
                oDb.AddInParameter(cmd, "@FK_DesignationID", DbType.Int32, objUser.Designation.DesignationID);
                oDb.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, objUser.Branch.BranchID);
                oDb.AddInParameter(cmd, "@MobileNo", DbType.String, objUser.MobileNo);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objUser.CreatedBy.UserID);
                oDb.AddInParameter(cmd, "@IDProof", DbType.String, objUser.IDProof);
                oDb.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, objUser.Region.RegionID);
                oDb.AddInParameter(cmd, "@FK_ZoneID", DbType.Int32, objUser.Zone.ZoneID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0) iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_UserID"));
            }
            catch (Exception ex)
            {
                sException = "User AddUser | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateUser(Entity.User objUser)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateUser(oDb, objUser, oTrans);
                    oTrans.Commit();
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
        private static bool UpdateUser(Database oDb, Entity.User objUser, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_USER);
                oDb.AddInParameter(cmd, "@PK_UserID", DbType.Int32, objUser.UserID);
                oDb.AddInParameter(cmd, "@UserName", DbType.String, objUser.UserName);
                oDb.AddInParameter(cmd, "@Password", DbType.String, CommonMethods.Security.Encrypt(objUser.Password, true));
                oDb.AddInParameter(cmd, "@Email", DbType.String, objUser.Email);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objUser.IsActive);
                oDb.AddInParameter(cmd, "@FK_RoleID", DbType.Int32, objUser.RoleID);
                oDb.AddInParameter(cmd, "@EmployeeName", DbType.String, objUser.EmployeeName);
                oDb.AddInParameter(cmd, "@EmployeeCode", DbType.String, objUser.EmployeeCode);
              //  oDb.AddInParameter(cmd, "@SalesManCode", DbType.String, objUser.SalesManCode);
                oDb.AddInParameter(cmd, "@BasicPay", DbType.Decimal, objUser.BasicPay);
                oDb.AddInParameter(cmd, "@Address", DbType.String, objUser.Address);
                oDb.AddInParameter(cmd, "@ConfirmPassword", DbType.String, CommonMethods.Security.Encrypt(objUser.ConfirmPassword, true));
                oDb.AddInParameter(cmd, "@DOB", DbType.String, objUser.sDOB);
                oDb.AddInParameter(cmd, "@DOJ", DbType.String, objUser.sDOJ);
                oDb.AddInParameter(cmd, "@FK_DesignationID", DbType.Int32, objUser.Designation.DesignationID);
                oDb.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, objUser.Branch.BranchID);
                oDb.AddInParameter(cmd, "@MobileNo", DbType.String, objUser.MobileNo);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objUser.ModifiedBy.UserID);
                oDb.AddInParameter(cmd, "@IDProof", DbType.String, objUser.IDProof);
                oDb.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, objUser.Region.RegionID);
                oDb.AddInParameter(cmd, "@FK_ZoneID", DbType.Int32, objUser.Zone.ZoneID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "User UpdateUser| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteUser(int ID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteUser(oDb, ID, oTrans);
                    oTrans.Commit();
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
        private static bool DeleteUser(Database oDb, int ID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_USER);
                oDb.AddInParameter(cmd, "@PK_UserID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "User DeleteUser | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool ChangePassword(Entity.User objUser)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_CHANGEPASSWORD);
                db.AddInParameter(cmd, "@PK_UserID", DbType.Int32, objUser.UserID);
                db.AddInParameter(cmd, "@OldPassword", DbType.String, CommonMethods.Security.Encrypt(objUser.OldPassword, true));
                db.AddInParameter(cmd, "@NewPassword", DbType.String, CommonMethods.Security.Encrypt(objUser.Password, true));
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "User ChangePassword| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static int AddLog(string sIPAddresss, int iUserID, string clientIp)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbLogCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_LOG);
                //db.AddInParameter(cmd, "@LogInDateTime", DbType.String, DateTime.Now.AddHours(5).AddMinutes(30).AddSeconds(DateTime.Now.Second).ToString("dd/MM/yyyy HH:mm:ss"));
                db.AddInParameter(cmd, "@IPAddress", DbType.String, sIPAddresss);
                db.AddInParameter(cmd, "@UserID", DbType.Int32, iUserID);
                db.AddInParameter(cmd, "@LocalIPAddress", DbType.String, clientIp);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = 1;
            }
            catch (Exception ex)
            {
                sException = "User AddLog | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static void AddPageVisitLog(string sIPAddresss, int iUserID, string sURL)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbLogCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_VISITLOG);
                //db.AddInParameter(cmd, "@LogInDateTime", DbType.String, DateTime.Now.AddHours(5).AddMinutes(30).AddSeconds(DateTime.Now.Second).ToString("dd/MM/yyyy HH:mm:ss"));
                db.AddInParameter(cmd, "@IPAddress", DbType.String, sIPAddresss);
                db.AddInParameter(cmd, "@FK_UserID", DbType.Int32, iUserID);
                db.AddInParameter(cmd, "@URL", DbType.String, sURL);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = 1;
            }
            catch (Exception ex)
            {
                sException = "User AddPageVisitLog | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            //return iID;
        }
        public static DataSet GetHomePageList()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                dsList = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM tHomePage ORDER BY PageName");
            }
            catch (Exception ex)
            {
                sException = "User GetHomePageList() | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsList;
        }
        public static Collection<Entity.VisitLog> GetVisitLog()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.VisitLog> objList = new Collection<Entity.VisitLog>();
            Entity.VisitLog objVisitLog;

            try
            {
                db = Entity.DBConnection.dbLogCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_VISITLOG);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objVisitLog = new Entity.VisitLog();

                        objVisitLog.PageLogID = Convert.ToInt32(drData["PK_PageLogID"]);
                        objVisitLog.VisitLogDateTime = Convert.ToDateTime(drData["VisitLogDateTime"]);
                        objVisitLog.sVisitLogDateTime = objVisitLog.VisitLogDateTime.ToString("dd/MM/yyyy HH:mm:ss");
                        objVisitLog.IPAddress = Convert.ToString(drData["IPAddress"]);
                        objVisitLog.UserName = Convert.ToString(drData["UserName"]);
                        objVisitLog.URL = Convert.ToString(drData["URL"]);

                        objList.Add(objVisitLog);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "User GetVisitLog | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        //Added on 25-10-2017
        public static bool ResetPassword(int UserID, string sPassword)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_RESET_PASSWORD);
                db.AddInParameter(cmd, "@PK_UserID", DbType.Int32, UserID);
                db.AddInParameter(cmd, "@UserPassword", DbType.String, CommonMethods.Security.Encrypt(sPassword, true));                
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "User ResetPassword| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
