using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.ServiceModel.Activation;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using VHMS.Entity;
using System.Collections;
using System.Net;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections.ObjectModel;
using VHMS.DataAccess;
using System.IO;


[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    #region demo
    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    #endregion

    #region Login
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void getEmployeeDetails(string UserName, string Password, string token)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();

        List<string> InvoiceDetails = new List<string>();
        ReportDataSet dsEmployee = null;
        VHMSReports oEmployeeMasSystem = null;
        try
        {
            oEmployeeMasSystem = new VHMSReports();

            dsEmployee = new ReportDataSet();

            ReportDataSet.tUserRow drV_EmployeeMasterRow = null;
            DataSet dsData = new DataSet();
            dsData = VHMS.DataAccess.VHMSReports.GetLoginDetails(UserName, Password);

            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tUser";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsEmployee.tUser.ImportRow(drow);
            }

            List<EmployeeDetail> servDetail = new List<EmployeeDetail>();

            if (dsEmployee.tUser.Rows.Count > 0)
            {
                drV_EmployeeMasterRow = (ReportDataSet.tUserRow)dsEmployee.tUser.Rows[0];

                EmployeeDetail e = new EmployeeDetail();
                e.EmployeeID = drV_EmployeeMasterRow.PK_UserID.ToString();
                e.EmployeeName = drV_EmployeeMasterRow.EmployeeName;
                e.RoleID = drV_EmployeeMasterRow.FK_RoleID.ToString();
                e.RoleName = drV_EmployeeMasterRow.RoleName;
                e.BranchID = drV_EmployeeMasterRow.FK_BranchID.ToString();
                e.ZoneID = drV_EmployeeMasterRow.FK_ZoneID.ToString();
                e.RegionID = drV_EmployeeMasterRow.FK_RegionID.ToString();
                e.ReportAccess = drV_EmployeeMasterRow.ReportAccess.ToString();
                // e.MobileNo = drV_EmployeeMasterRow.MobileNo;
                // e.Status = drV_EmployeeMasterRow.Status;

                servDetail.Add(e);
            }

            Employee invDetails = new Employee();
            invDetails.Details = servDetail;
            if (dsEmployee.tUser.Rows.Count > 0)
                invDetails.status = "true";
            else
                invDetails.status = "false";

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(serializer.Serialize(invDetails));

        }
        catch
        {
            throw;
        }
        finally
        {
            if (dsEmployee != null)
                dsEmployee = null;
        }
    }

    public class Employee
    {
        public List<EmployeeDetail> Details;
        public string status;
    }

    public class EmployeeDetail
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string RoleID { get; set; }
        public string ZoneID { get; set; }
        public string RegionID { get; set; }
        public string RoleName { get; set; }
        public string BranchID { get; set; }
        public string ReportAccess { get; set; }
    }
    #endregion

    #region Scheme
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void getSchemeDetails(string SchemeID)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();

        List<string> InvoiceDetails = new List<string>();
        try
        {
            Database db;
            DataSet dsList = null;
            Collection<VHMS.Entity.Chit> objList = new Collection<VHMS.Entity.Chit>();
            VHMS.Entity.Chit objChit = new VHMS.Entity.Chit();

            db = VHMS.Entity.DBConnection.dbCon;
            DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CHIT);
            db.AddInParameter(cmd, "@PK_ChitID", DbType.Int32, Convert.ToInt32(SchemeID));
            dsList = db.ExecuteDataSet(cmd);

            List<VHMS.Entity.Chit> servDetail = new List<VHMS.Entity.Chit>();

            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drData in dsList.Tables[0].Rows)
                {
                    objChit = new VHMS.Entity.Chit();

                    objChit.ChitID = Convert.ToInt32(drData["PK_ChitID"]);
                    objChit.ChitName = Convert.ToString(drData["ChitName"]);
                    objChit.ChitCode = Convert.ToString(drData["ChitCode"]);
                    objChit.Duration = Convert.ToInt32(drData["Duration"]);
                    objChit.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    if (objChit.IsActive)
                        servDetail.Add(objChit);
                }
                Chit oChit = new Chit();
                oChit.Details = servDetail;
                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                    oChit.status = "true";
                else
                    oChit.status = "false";
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(serializer.Serialize(oChit));
            }

        }
        catch
        {
            throw;
        }
    }

    public class Chit
    {
        public List<VHMS.Entity.Chit> Details;
        public string status;
    }
    #endregion

    #region EmployeeDetails
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void getEmployeeList()
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();

        List<string> InvoiceDetails = new List<string>();
        try
        {
            Database db;
            DataSet dsList = null;
            Collection<VHMS.Entity.User> objList = new Collection<VHMS.Entity.User>();
            EmployeesData objUser = new EmployeesData();

            db = VHMS.Entity.DBConnection.dbCon;
            DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_USER);
            dsList = db.ExecuteDataSet(cmd);

            List<EmployeesData> servDetail = new List<EmployeesData>();

            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drData in dsList.Tables[0].Rows)
                {
                    objUser = new EmployeesData();

                    objUser.UserID = Convert.ToString(drData["PK_UserID"]);
                    objUser.UserName = Convert.ToString(drData["UserName"]);
                    objUser.Email = Convert.ToString(drData["Email"]);
                    objUser.RoleID = Convert.ToString(drData["FK_RoleID"]);
                    objUser.RoleName = Convert.ToString(drData["RoleName"]);
                    objUser.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    objUser.Password = CommonMethods.Security.Decrypt(drData["Password"].ToString(), true);
                    objUser.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                    objUser.MobileNo = Convert.ToString(drData["MobileNo"]);
                    objUser.IDProof = Convert.ToString(drData["IDProof"]);
                    objUser.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                    objUser.BasicPay = Convert.ToString(drData["BasicPay"]);
                    objUser.DOB = Convert.ToDateTime(drData["DOB"]);
                    objUser.sDOB = objUser.DOB.ToString("dd/MM/yyyy");
                    objUser.DOJ = Convert.ToDateTime(drData["DOJ"]);
                    objUser.sDOJ = objUser.DOJ.ToString("dd/MM/yyyy");
                    objUser.Address = Convert.ToString(drData["Address"]);
                    objUser.ConfirmPassword = CommonMethods.Security.Decrypt(drData["ConfirmPassword"].ToString(), true);

                    objUser.BranchID = Convert.ToString(drData["FK_BranchID"]);
                    objUser.BranchName = Convert.ToString(drData["BranchName"]);

                    objUser.DesignationID = Convert.ToString(drData["FK_DesignationID"]);
                    objUser.DesignationName = Convert.ToString(drData["DesignationName"]);

                    if (objUser.IsActive)
                        servDetail.Add(objUser);
                }
                Employees oChit = new Employees();
                oChit.Details = servDetail;
                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                    oChit.status = "true";
                else
                    oChit.status = "false";
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(serializer.Serialize(oChit));
            }

        }
        catch
        {
            throw;
        }
    }

    public class Employees
    {
        public List<EmployeesData> Details;
        public string status;
    }

    public class EmployeesData
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public Boolean IsActive { get; set; }
        public string Password { get; set; }
        public string DesignationName { get; set; }
        public string DesignationID { get; set; }
        public string BranchName { get; set; }
        public string BranchID { get; set; }
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
        public string sDOJ { get; set; }
        public string sDOB { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOJ { get; set; }
        public string BasicPay { get; set; }
        public string EmployeeCode { get; set; }
        public string IDProof { get; set; }
        public string EmployeeName { get; set; }
        public string MobileNo { get; set; }
    }
    #endregion

    #region Register

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void getRegisterList(string EmployeeID, string BranchID, string RegisterID, string RegionID, string ZoneID)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();

        List<string> InvoiceDetails = new List<string>();
        try
        {
            Database db;
            DataSet dsList = null;
            Collection<VHMS.Entity.Billing.Register> objList = new Collection<VHMS.Entity.Billing.Register>();
            RegisterData objRegister = new RegisterData();

            db = VHMS.Entity.DBConnection.dbCon;
            DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_REGISTERSERVICE);
            db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, Convert.ToInt32(EmployeeID));
            db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, Convert.ToInt32(BranchID));
            db.AddInParameter(cmd, "@FK_RegisterID", DbType.Int32, Convert.ToInt32(RegisterID));
            db.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, Convert.ToInt32(RegionID));
            db.AddInParameter(cmd, "@FK_ZoneID", DbType.Int32, Convert.ToInt32(ZoneID));
            dsList = db.ExecuteDataSet(cmd);

            List<RegisterData> servDetail = new List<RegisterData>();

            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drData in dsList.Tables[0].Rows)
                {
                    objRegister = new RegisterData();

                    objRegister.RegisterID = Convert.ToString(drData["PK_RegisterID"]);
                    objRegister.AccountNo = Convert.ToString(drData["AccountNo"]);
                    objRegister.RegisterDate = Convert.ToDateTime(drData["RegisterDate"]);
                    objRegister.sRegisterDate = objRegister.RegisterDate.ToString("dd/MM/yyyy");
                    objRegister.IDProof = Convert.ToString(drData["IDProof"]);
                    objRegister.IDNo = Convert.ToString(drData["IDNo"]);
                    objRegister.ChitID = Convert.ToString(drData["FK_ChitID"]);
                    objRegister.ChitName = Convert.ToString(drData["ChitName"]);
                    objRegister.ChitCode = Convert.ToString(drData["ChitCode"]);
                    objRegister.Duration = Convert.ToString(drData["Duration"]);
                    objRegister.InstallmentAmount = Convert.ToString(drData["InstallmentAmount"]);
                    objRegister.TotalAmount = Convert.ToString(drData["TotalAmount"]);
                    objRegister.BonusAmount = Convert.ToString(drData["BonusAmount"]);
                    objRegister.CustomerID = Convert.ToString(drData["FK_CustomerID"]);
                    objRegister.DOB = Convert.ToString(drData["DOB"]);
                    objRegister.CustomerName = Convert.ToString(drData["CustomerName"]);
                    objRegister.Address = Convert.ToString(drData["Address"]);
                    objRegister.MobileNo = Convert.ToString(drData["MobileNo"]);
                    objRegister.BranchID = Convert.ToString(drData["FK_BranchID"]);
                    objRegister.BranchName = Convert.ToString(drData["BranchName"]);
                    objRegister.ProofImage = Convert.ToString(drData["ProofImage"]);
                    objRegister.ProofBackImage = Convert.ToString(drData["ProofBackImage"]);
                    objRegister.RegisterImage1 = Convert.ToString(drData["RegisterImage1"]);
                    objRegister.RegisterImage2 = Convert.ToString(drData["RegisterImage2"]);
                    objRegister.ReceiptNo = Convert.ToString(drData["ReceiptNo"]);
                    objRegister.UserID = Convert.ToString(drData["FK_EmployeeID"]);
                    objRegister.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                    objRegister.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);

                    objRegister.IsActive = Convert.ToBoolean(drData["IsActive"]);

                    servDetail.Add(objRegister);
                }

            }
            Register oChit = new Register();
            oChit.Details = servDetail;
            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                oChit.status = "true";
            else
                oChit.status = "false";
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(serializer.Serialize(oChit));

        }
        catch
        {
            throw;
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void getRegisterByNo(string AccountNo)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();

        List<string> InvoiceDetails = new List<string>();
        try
        {
            Database db;
            DataSet dsList = null;
            Collection<VHMS.Entity.Billing.Register> objList = new Collection<VHMS.Entity.Billing.Register>();
            RegisterData objRegister = new RegisterData();

            db = VHMS.Entity.DBConnection.dbCon;
            DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_REGISTERBYNO);
            db.AddInParameter(cmd, "@PK_RegisterID", DbType.String, AccountNo);
            db.AddInParameter(cmd, "@IsActive", DbType.String, "Open");
            //db.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, Convert.ToInt32(RegionID));
            //db.AddInParameter(cmd, "@FK_ZoneID", DbType.Int32, Convert.ToInt32(ZoneID));
            dsList = db.ExecuteDataSet(cmd);

            List<RegisterData> servDetail = new List<RegisterData>();

            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drData in dsList.Tables[0].Rows)
                {
                    objRegister = new RegisterData();

                    objRegister.RegisterID = Convert.ToString(drData["PK_RegisterID"]);
                    objRegister.AccountNo = Convert.ToString(drData["AccountNo"]);
                    objRegister.RegisterDate = Convert.ToDateTime(drData["RegisterDate"]);
                    objRegister.sRegisterDate = objRegister.RegisterDate.ToString("dd/MM/yyyy");
                    objRegister.IDProof = Convert.ToString(drData["IDProof"]);
                    objRegister.IDNo = Convert.ToString(drData["IDNo"]);
                    objRegister.ChitID = Convert.ToString(drData["FK_ChitID"]);
                    objRegister.ChitName = Convert.ToString(drData["ChitName"]);
                    objRegister.ChitCode = Convert.ToString(drData["ChitCode"]);
                    objRegister.Duration = Convert.ToString(drData["Duration"]);
                    objRegister.InstallmentAmount = Convert.ToString(drData["InstallmentAmount"]);
                    objRegister.TotalAmount = Convert.ToString(drData["TotalAmount"]);
                    objRegister.BonusAmount = Convert.ToString(drData["BonusAmount"]);
                    objRegister.CustomerID = Convert.ToString(drData["FK_CustomerID"]);
                    objRegister.DOB = Convert.ToString(drData["DOB"]);
                    objRegister.CustomerName = Convert.ToString(drData["CustomerName"]);
                    objRegister.Address = Convert.ToString(drData["Address"]);
                    objRegister.MobileNo = Convert.ToString(drData["MobileNo"]);
                    objRegister.BranchID = Convert.ToString(drData["FK_BranchID"]);
                    objRegister.BranchName = Convert.ToString(drData["BranchName"]);
                    objRegister.ProofImage = Convert.ToString(drData["ProofImage"]);
                    objRegister.ProofBackImage = Convert.ToString(drData["ProofBackImage"]);
                    objRegister.RegisterImage1 = Convert.ToString(drData["RegisterImage1"]);
                    objRegister.RegisterImage2 = Convert.ToString(drData["RegisterImage2"]);
                    objRegister.RenewalCount = Convert.ToString(drData["RenewalCount"]);
                    objRegister.UserID = Convert.ToString(drData["FK_EmployeeID"]);
                    objRegister.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                    objRegister.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);

                    objRegister.IsActive = Convert.ToBoolean(drData["IsActive"]);

                    servDetail.Add(objRegister);
                }
            }
            Register oChit = new Register();
            oChit.Details = servDetail;
            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                oChit.status = "true";
            else
                oChit.status = "false";
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(serializer.Serialize(oChit));

        }
        catch
        {
            throw;
        }
    }


    public class Register
    {
        public List<RegisterData> Details;
        public string status;
    }
    public class RegisterData
    {
        public string RegisterID { get; set; }
        public DateTime RegisterDate { get; set; }
        public string sRegisterDate { get; set; }
        public string AccountNo { get; set; }
        public string IDProof { get; set; }
        public string IDNo { get; set; }
        public string ReceiptNo { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string DOB { get; set; }
        public string ProofImage { get; set; }
        public string ProofBackImage { get; set; }
        public string RegisterImage1 { get; set; }
        public string RegisterImage2 { get; set; }
        public string BonusAmount { get; set; }
        public string TotalAmount { get; set; }
        public string InstallmentAmount { get; set; }
        public string ChitID { get; set; }
        public string ChitName { get; set; }
        public string ChitCode { get; set; }
        public string Duration { get; set; }
        public string CustomerID { get; set; }
        public string BranchID { get; set; }
        public string BranchName { get; set; }
        public string UserID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public Boolean IsActive { get; set; }
        public string ChitAmount { get; set; }
        public string RenewalCount { get; set; }
        public string Status { get; set; }
        public DateTime CancelledDate { get; set; }
        public string sCancelledDate { get; set; }
        public string ReasonforCancel { get; set; }
    }
    #endregion

    #region Renewal

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void getRenewalList(string EmployeeID, string BranchID, string RenewalID, string RegionID, string ZoneID)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();

        List<string> InvoiceDetails = new List<string>();
        try
        {
            Database db;
            DataSet dsList = null;
            Collection<VHMS.Entity.Billing.Renewal> objList = new Collection<VHMS.Entity.Billing.Renewal>();
            RenewalData objRenewal = new RenewalData();

            db = VHMS.Entity.DBConnection.dbCon;
            DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_RENEWALSERVICE);
            db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, Convert.ToInt32(EmployeeID));
            db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, Convert.ToInt32(BranchID));
            db.AddInParameter(cmd, "@FK_RenewalID", DbType.Int32, Convert.ToInt32(RenewalID));
            db.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, Convert.ToInt32(RegionID));
            db.AddInParameter(cmd, "@FK_ZoneID", DbType.Int32, Convert.ToInt32(ZoneID));

            dsList = db.ExecuteDataSet(cmd);

            List<RenewalData> servDetail = new List<RenewalData>();

            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drData in dsList.Tables[0].Rows)
                {
                    objRenewal = new RenewalData();

                    objRenewal.RenewalID = Convert.ToString(drData["PK_RenewalID"]);
                    objRenewal.RenewalNo = Convert.ToString(drData["RenewalNo"]);
                    objRenewal.RenewalDate = Convert.ToDateTime(drData["RenewalDate"]);
                    objRenewal.sRenewalDate = objRenewal.RenewalDate.ToString("dd/MM/yyyy");

                    objRenewal.RegisterID = Convert.ToString(drData["PK_RegisterID"]);
                    objRenewal.AccountNo = Convert.ToString(drData["AccountNo"]);
                    objRenewal.RegisterDate = Convert.ToDateTime(drData["RegisterDate"]);
                    objRenewal.sRegisterDate = objRenewal.RegisterDate.ToString("dd/MM/yyyy");
                    objRenewal.IDProof = Convert.ToString(drData["IDProof"]);
                    objRenewal.IDNo = Convert.ToString(drData["IDNo"]);

                    objRenewal.ChitID = Convert.ToString(drData["FK_ChitID"]);
                    objRenewal.ChitName = Convert.ToString(drData["ChitName"]);
                    objRenewal.ChitCode = Convert.ToString(drData["ChitCode"]);
                    objRenewal.TotalAmount = Convert.ToString(drData["TotalAmount"]);
                    objRenewal.BonusAmount = Convert.ToString(drData["BonusAmount"]);
                    objRenewal.Duration = Convert.ToString(drData["Duration"]);
                    objRenewal.InstallmentAmount = Convert.ToString(drData["InstallmentAmount"]);
                    objRenewal.GrandTotal = Convert.ToString(drData["GrandTotal"]);

                    objRenewal.CustomerID = Convert.ToString(drData["FK_CustomerID"]);
                    objRenewal.CustomerName = Convert.ToString(drData["CustomerName"]);
                    objRenewal.Address = Convert.ToString(drData["Address"]);
                    objRenewal.MobileNo = Convert.ToString(drData["MobileNo"]);

                    objRenewal.BranchID = Convert.ToString(drData["FK_BranchID"]);
                    objRenewal.BranchName = Convert.ToString(drData["BranchName"]);

                    objRenewal.UserID = Convert.ToString(drData["FK_EmployeeID"]);
                    objRenewal.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                    objRenewal.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);

                    objRenewal.AmountPaid = Convert.ToString(drData["AmountPaid"]);
                    objRenewal.Status = Convert.ToString(drData["Status"]);

                    objRenewal.RUserID = Convert.ToString(drData["REmployeeID"]);
                    objRenewal.REmployeeName = Convert.ToString(drData["REmployeeName"]);
                    objRenewal.REmployeeCode = Convert.ToString(drData["REmployeeCode"]);
                    objRenewal.RenewalCount = Convert.ToString(drData["RenewalCount"]);
                    servDetail.Add(objRenewal);
                }
            }
            Renewal oChit = new Renewal();
            oChit.Details = servDetail;
            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                oChit.status = "true";
            else
                oChit.status = "false";
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(serializer.Serialize(oChit));

        }
        catch
        {
            throw;
        }
    }

    public class Renewal
    {
        public List<RenewalData> Details;
        public string status;
    }

    public class RenewalData
    {
        public string RenewalID { get; set; }
        public string RenewalNo { get; set; }
        public DateTime RenewalDate { get; set; }
        public string sRenewalDate { get; set; }
        public string RegisterID { get; set; }
        public DateTime RegisterDate { get; set; }
        public string sRegisterDate { get; set; }
        public string AccountNo { get; set; }
        public string IDProof { get; set; }
        public string IDNo { get; set; }
        public string ReceiptNo { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string DOB { get; set; }
        public string ProofImage { get; set; }
        public string RegisterImage1 { get; set; }
        public string RegisterImage2 { get; set; }
        public string BonusAmount { get; set; }
        public string TotalAmount { get; set; }
        public string InstallmentAmount { get; set; }
        public string GrandTotal { get; set; }
        public string AmountPaid { get; set; }
        public string ChitID { get; set; }
        public string ChitName { get; set; }
        public string ChitCode { get; set; }
        public string Duration { get; set; }
        public string CustomerID { get; set; }
        public string BranchID { get; set; }
        public string BranchName { get; set; }
        public string RUserID { get; set; }
        public string REmployeeName { get; set; }
        public string REmployeeCode { get; set; }
        public string UserID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public Boolean IsActive { get; set; }
        public string ChitAmount { get; set; }
        public string RenewalCount { get; set; }
        public string Status { get; set; }
        public DateTime CancelledDate { get; set; }
        public string sCancelledDate { get; set; }
        public string ReasonforCancel { get; set; }
    }
    #endregion

    #region Branch

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void getBranchList(string RegionID, string ZoneID)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();

        List<string> InvoiceDetails = new List<string>();
        try
        {
            Database db;
            DataSet dsList = null;
            Collection<VHMS.Entity.Billing.Renewal> objList = new Collection<VHMS.Entity.Billing.Renewal>();
            VHMS.Entity.Branch objBranch = new VHMS.Entity.Branch();

            db = VHMS.Entity.DBConnection.dbCon;
            DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_BRANCH);
            db.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, Convert.ToInt32(RegionID));
            db.AddInParameter(cmd, "@FK_ZoneID", DbType.Int32, Convert.ToInt32(ZoneID));
            dsList = db.ExecuteDataSet(cmd);

            List<VHMS.Entity.Branch> servDetail = new List<VHMS.Entity.Branch>();

            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drData in dsList.Tables[0].Rows)
                {
                    objBranch = new VHMS.Entity.Branch();

                    objBranch.BranchID = Convert.ToInt32(drData["PK_BranchID"]);
                    objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                    objBranch.BranchCode = Convert.ToString(drData["BranchCode"]);
                    objBranch.Address = Convert.ToString(drData["Address"]);
                    objBranch.ContactPerson = Convert.ToString(drData["ContactPerson"]);
                    objBranch.MobileNo = Convert.ToString(drData["MobileNo"]);
                    objBranch.Landline = Convert.ToString(drData["Landline"]);
                    objBranch.Email = Convert.ToString(drData["Email"]);
                    objBranch.Website = Convert.ToString(drData["Website"]);
                    objBranch.HeadOfficeFlag = Convert.ToBoolean(drData["HeadOfficeFlag"]);
                    objBranch.IsActive = Convert.ToBoolean(drData["IsActive"]);

                    servDetail.Add(objBranch);
                }
                Branch oChit = new Branch();
                oChit.Details = servDetail;
                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                    oChit.status = "true";
                else
                    oChit.status = "false";
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(serializer.Serialize(oChit));
            }

        }
        catch
        {
            throw;
        }
    }

    public class Branch
    {
        public List<VHMS.Entity.Branch> Details;
        public string status;
    }
    #endregion

    #region Customer

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void getCustomerList(string MobileNo)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();

        List<string> InvoiceDetails = new List<string>();
        try
        {
            Database db;
            DataSet dsList = null;
            Collection<VHMS.Entity.Customer> objList = new Collection<VHMS.Entity.Customer>();
            CustomerData objCustomer = new CustomerData();

            db = VHMS.Entity.DBConnection.dbCon;
            DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CUSTOMERBYCODE);
            db.AddInParameter(cmd, "@PK_CustomerID", DbType.String, MobileNo);

            dsList = db.ExecuteDataSet(cmd);

            List<CustomerData> servDetail = new List<CustomerData>();

            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drData in dsList.Tables[0].Rows)
                {
                    objCustomer = new CustomerData();

                    objCustomer.CustomerID = Convert.ToString(drData["PK_CustomerID"]);
                    objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                    objCustomer.CustomerCode = Convert.ToString(drData["CustomerCode"]);
                    objCustomer.Address = Convert.ToString(drData["Address"]);
                    objCustomer.CustomerType = Convert.ToString(drData["CustomerType"]);
                    objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                    objCustomer.AlternateNo = Convert.ToString(drData["AlternateNo"]);
                    objCustomer.Email = Convert.ToString(drData["Email"]);
                    objCustomer.DOB = Convert.ToString(drData["DOB"]);
                    objCustomer.GSTNo = Convert.ToString(drData["GSTNo"]);
                    objCustomer.IsActive = Convert.ToBoolean(drData["IsActive"]);

                    if (objCustomer.IsActive)
                        servDetail.Add(objCustomer);
                }
                Customer oChit = new Customer();
                oChit.Details = servDetail;
                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                    oChit.status = "true";
                else
                    oChit.status = "false";
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(serializer.Serialize(oChit));
            }

        }
        catch
        {
            throw;
        }
    }

    public class Customer
    {
        public List<CustomerData> Details;
        public string status;
    }

    public class CustomerData
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string AlternateNo { get; set; }
        public string Email { get; set; }
        public string GSTNo { get; set; }
        public string DOB { get; set; }
        public string CustomerType { get; set; }
        public Boolean IsActive { get; set; }

    }
    #endregion

    #region Add & Update Renewal

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void AddRenewalDetails(string RegisterID, string EmployeeID, string BranchID, string AmountPaid)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();
        int iID = 0;
        List<string> InvoiceDetails = new List<string>();
        try
        {
            Database db;
            db = VHMS.Entity.DBConnection.dbCon;

            using (DbConnection oDbCon = db.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();

                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_RENEWALSERVICE);
                db.AddOutParameter(cmd, "@PK_RenewalID", DbType.Int32, 10);
                db.AddInParameter(cmd, "@RenewalDate", DbType.String, DateTime.Now.ToString("dd-MM-yyyy"));
                db.AddInParameter(cmd, "@AmountPaid", DbType.Decimal, Convert.ToDecimal(AmountPaid));
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, Convert.ToInt32(BranchID));
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, Convert.ToInt32(EmployeeID));
                db.AddInParameter(cmd, "@Status", DbType.String, "Closed");
                db.AddInParameter(cmd, "@FK_RegisterID", DbType.Int32, Convert.ToInt32(RegisterID));
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, Convert.ToInt32(EmployeeID));
                iID = Convert.ToInt32(db.ExecuteScalar(cmd, oTrans));
                oTrans.Commit();
            }
            List<AddRenewalData> servDetail = new List<AddRenewalData>();


            if (iID > 0)
            {
                AddRenewalData e = new AddRenewalData();
                SendSMS(iID, "Renewal");
                e.Service = "Renewal Added Successfully..";
                servDetail.Add(e);
            }

            AddRenewal invDetails = new AddRenewal();
            invDetails.Details = servDetail;
            if (iID > 0)
                invDetails.status = "true";
            else
                invDetails.status = "false";


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(serializer.Serialize(invDetails));
        }
        catch
        {
            throw;
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void UpdateRenewalData(string RenewalID, string RegisterID, string EmployeeID, string BranchID, string AmountPaid)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();
        int iID = 0;
        List<string> InvoiceDetails = new List<string>();
        try
        {
            Database db;
            db = VHMS.Entity.DBConnection.dbCon;

            using (DbConnection oDbCon = db.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();

                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_RENEWAL);
                db.AddInParameter(cmd, "@PK_RenewalID", DbType.Int32, Convert.ToInt32(RenewalID));
                db.AddInParameter(cmd, "@RenewalDate", DbType.String, DateTime.Now.ToString("dd-MM-yyyy"));
                db.AddInParameter(cmd, "@AmountPaid", DbType.Decimal, Convert.ToDecimal(AmountPaid));
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, Convert.ToInt32(BranchID));
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, Convert.ToInt32(EmployeeID));
                db.AddInParameter(cmd, "@Status", DbType.String, "Closed");
                db.AddInParameter(cmd, "@FK_RegisterID", DbType.Int32, Convert.ToInt32(RegisterID));
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, Convert.ToInt32(EmployeeID));
                iID = db.ExecuteNonQuery(cmd, oTrans);
                oTrans.Commit();
            }
            List<AddRenewalData> servDetail = new List<AddRenewalData>();


            if (iID > 0)
            {
                AddRenewalData e = new AddRenewalData();
                e.Service = "Renewal Updated Successfully..";
                servDetail.Add(e);
            }

            AddRenewal invDetails = new AddRenewal();
            invDetails.Details = servDetail;
            if (iID > 0)
                invDetails.status = "true";
            else
                invDetails.status = "false";


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(serializer.Serialize(invDetails));
        }
        catch
        {
            throw;
        }
    }

    public class AddRenewal
    {
        public List<AddRenewalData> Details;
        public string status;
    }

    public class AddRenewalData
    {
        public string Service { get; set; }
    }
    #endregion

    #region Add & Update Register

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void AddRegisterDetails(string RegisterID, string EmployeeID, string BranchID, string IDProof, string InstallmentAmount, string TotalAmount, string DOB, string Address, string CustomerName, string MobileNo, string CustomerID, string ChitID, string ProofImage, string ProofBackImage, string RegisterImage1, string RegisterImage2)
    {
        int iID = 0;
        List<string> InvoiceDetails = new List<string>();
        try
        {
            Database db;
            db = VHMS.Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = db.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_REGISTERSERVICE);
                db.AddOutParameter(cmd, "@PK_RegisterID", DbType.Int32, 10);
                db.AddInParameter(cmd, "@RegisterDate", DbType.String, DateTime.Now.ToString("dd-MM-yyyy"));
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, Convert.ToInt32(CustomerID));
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, Convert.ToInt32(BranchID));
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, Convert.ToInt32(EmployeeID));
                db.AddInParameter(cmd, "@FK_ChitID", DbType.Int32, Convert.ToInt32(ChitID));
                db.AddInParameter(cmd, "@RegisterImage1", DbType.String, SaveImage(RegisterImage1));
                db.AddInParameter(cmd, "@RegisterImage2", DbType.String, SaveImage(RegisterImage2));
                db.AddInParameter(cmd, "@ProofImage", DbType.String, SaveImage(ProofImage));
                db.AddInParameter(cmd, "@ProofBackImage", DbType.String, SaveImage(ProofBackImage));
                //db.AddInParameter(cmd, "@RegisterImage1", DbType.String, "test");
                //db.AddInParameter(cmd, "@RegisterImage2", DbType.String, "test");
                //db.AddInParameter(cmd, "@ProofImage", DbType.String, "test");
                db.AddInParameter(cmd, "@CustomerName", DbType.String, CustomerName);
                db.AddInParameter(cmd, "@Address", DbType.String, Address);
                db.AddInParameter(cmd, "@MobileNo", DbType.String, MobileNo);
                db.AddInParameter(cmd, "@DOB", DbType.String, DOB);
                db.AddInParameter(cmd, "@InstallmentAmount", DbType.Decimal, Convert.ToDecimal(InstallmentAmount));
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, Convert.ToDecimal(TotalAmount));
                db.AddInParameter(cmd, "@IsActive", DbType.Boolean, true);
                db.AddInParameter(cmd, "@IDProof", DbType.String, IDProof);
                db.AddInParameter(cmd, "@IDNo", DbType.String, "");
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, Convert.ToInt32(EmployeeID));
                iID = Convert.ToInt32(db.ExecuteScalar(cmd, oTrans));
                oTrans.Commit();
            }
            List<AddRegisterData> servDetail = new List<AddRegisterData>();

            if (iID > 0)
            {
                SendSMS(iID, "Register");
                AddRegisterData e = new AddRegisterData();
                e.Service = "Register Added Successfully..";
                servDetail.Add(e);
            }

            AddRegister invDetails = new AddRegister();
            invDetails.Details = servDetail;
            if (iID > 0)
                invDetails.status = "true";
            else
                invDetails.status = "false";

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(serializer.Serialize(invDetails));
        }
        catch
        {
            throw;
        }
    }

    //[WebMethod]
    //[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    //public void imgtest( string ProofImage)
    //{
    //    JavaScriptSerializer ser = new JavaScriptSerializer();
    //    List<string> InvoiceDetails = new List<string>();
    //    try
    //    {
    //        SaveImage(ProofImage);
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    public string SaveImage(string ImgStr)
    {
        byte[] imageBytes = Convert.FromBase64String(ImgStr);
        string imageName = Guid.NewGuid().ToString("N");
        string imagePath = string.Format("~/images/IDPhotos/{0}.png", imageName);
        File.WriteAllBytes(Server.MapPath(imagePath), imageBytes);

        return "images/IDPhotos/" + imageName + ".png";
    }
    public string UpdateImage(string ImgStr, string filename)
    {
        byte[] imageBytes = Convert.FromBase64String(ImgStr);
        // string imageName = Guid.NewGuid().ToString("N");
        string imagePath = "~/images/IDPhotos/" + filename;
        File.WriteAllBytes(Server.MapPath(imagePath), imageBytes);

        return "images/IDPhotos/" + filename;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void UpdateRegisterData(string RegisterID, string EmployeeID, string BranchID, string IDProof, string InstallmentAmount, string TotalAmount, string DOB, string Address, string CustomerName, string MobileNo, string CustomerID, string ChitID, string ProofImage, string ProofBackImage, string RegisterImage1, string RegisterImage2)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();
        int iID = 0;
        DataSet dsList = null;
        string CustomerImage = "", RegisterFrontImage = "", RegisterBackImage = "";
        List<string> InvoiceDetails = new List<string>();
        try
        {
            Database db;
            db = VHMS.Entity.DBConnection.dbCon;
            DbCommand cmd1 = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_REGISTERSERVICE);
            db.AddInParameter(cmd1, "@FK_EmployeeID", DbType.Int32, Convert.ToInt32(0));
            db.AddInParameter(cmd1, "@FK_BranchID", DbType.Int32, Convert.ToInt32(0));
            db.AddInParameter(cmd1, "@FK_RegisterID", DbType.Int32, Convert.ToInt32(RegisterID));
            dsList = db.ExecuteDataSet(cmd1);

            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drData in dsList.Tables[0].Rows)
                {
                    CustomerImage = Convert.ToString(drData["ProofImage"]);
                    RegisterFrontImage = Convert.ToString(drData["RegisterImage1"]);
                    RegisterBackImage = Convert.ToString(drData["RegisterImage2"]);
                }
            }

            using (DbConnection oDbCon = db.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();

                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_REGISTER);
                db.AddInParameter(cmd, "@PK_RegisterID", DbType.Int32, Convert.ToInt32(RegisterID));
                db.AddInParameter(cmd, "@RegisterDate", DbType.String, DateTime.Now.ToString("dd-MM-yyyy"));
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, Convert.ToInt32(CustomerID));
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, Convert.ToInt32(BranchID));
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, Convert.ToInt32(EmployeeID));
                db.AddInParameter(cmd, "@FK_ChitID", DbType.Int32, Convert.ToInt32(ChitID));

                if (RegisterFrontImage != null && RegisterFrontImage.Length > 5)
                    db.AddInParameter(cmd, "@RegisterImage1", DbType.String, UpdateImage(RegisterImage1, RegisterFrontImage.Replace("images/IDPhotos/", "")));
                else
                    db.AddInParameter(cmd, "@RegisterImage1", DbType.String, SaveImage(RegisterImage1));

                if (RegisterBackImage != null && RegisterBackImage.Length > 5)
                    db.AddInParameter(cmd, "@RegisterImage2", DbType.String, UpdateImage(RegisterImage2, RegisterBackImage.Replace("images/IDPhotos/", "")));
                else
                    db.AddInParameter(cmd, "@RegisterImage2", DbType.String, SaveImage(RegisterImage2));

                if (CustomerImage != null && CustomerImage.Length > 5)
                    db.AddInParameter(cmd, "@ProofImage", DbType.String, UpdateImage(ProofImage, CustomerImage.Replace("images/IDPhotos/", "")));
                else
                    db.AddInParameter(cmd, "@ProofImage", DbType.String, SaveImage(ProofImage));

                if (CustomerImage != null && CustomerImage.Length > 5)
                    db.AddInParameter(cmd, "@ProofBackImage", DbType.String, UpdateImage(ProofBackImage, CustomerImage.Replace("images/IDPhotos/", "")));
                else
                    db.AddInParameter(cmd, "@ProofBackImage", DbType.String, SaveImage(ProofBackImage));

                db.AddInParameter(cmd, "@CustomerName", DbType.String, CustomerName);
                db.AddInParameter(cmd, "@Address", DbType.String, Address);
                db.AddInParameter(cmd, "@MobileNo", DbType.String, MobileNo);
                db.AddInParameter(cmd, "@DOB", DbType.String, DOB);
                db.AddInParameter(cmd, "@InstallmentAmount", DbType.Decimal, Convert.ToDecimal(InstallmentAmount));
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, Convert.ToDecimal(TotalAmount));
                db.AddInParameter(cmd, "@IsActive", DbType.Boolean, true);
                db.AddInParameter(cmd, "@IDProof", DbType.String, IDProof);
                db.AddInParameter(cmd, "@IDNo", DbType.String, "");
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, Convert.ToInt32(EmployeeID));
                iID = db.ExecuteNonQuery(cmd, oTrans);
                oTrans.Commit();
            }
            List<AddRegisterData> servDetail = new List<AddRegisterData>();


            if (iID > 0)
            {
                AddRegisterData e = new AddRegisterData();
                e.Service = "Register Updated Successfully..";
                servDetail.Add(e);
            }

            AddRegister invDetails = new AddRegister();
            invDetails.Details = servDetail;
            if (iID > 0)
                invDetails.status = "true";
            else
                invDetails.status = "false";


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(serializer.Serialize(invDetails));
        }
        catch
        {
            throw;
        }
    }

    public class AddRegister
    {
        public List<AddRegisterData> Details;
        public string status;
    }

    public class AddRegisterData
    {
        public string Service { get; set; }
    }
    #endregion

    #region Ledger
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void GetLedgerReport(string RegisterNo, string MobileNo)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();
        List<string> InvoiceDetails = new List<string>();

        ReportDataSet dsReportData = new ReportDataSet();
        DataSet dsData = new DataSet();
        dsData = VHMS.DataAccess.VHMSReports.PrintSchemeLedgerReport(MobileNo, RegisterNo);
        string TableHTML = "";
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tRenewal";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tRenewal.ImportRow(drow);

                dsData.Tables[1].TableName = "tRegister";
                foreach (DataRow drow in dsData.Tables[1].Rows)
                    dsReportData.tRegister.ImportRow(drow);

                dsData.Tables[2].TableName = "tCompany";
                foreach (DataRow drow in dsData.Tables[2].Rows)
                    dsReportData.tCompany.ImportRow(drow);
            }
            if (dsReportData.tRegister.Rows.Count > 0)
            {
                string CompanyName = ""; 
                foreach (ReportDataSet.tRegisterRow drtJobCardRow in dsReportData.tRegister)
                {
                    if (dsReportData.tCompany.Rows.Count > 0)
                    {
                        TableHTML += "<html><body><table cellpadding='0' cellspacing='0'>";
                        foreach (ReportDataSet.tCompanyRow drtBranchRow in dsReportData.tCompany)
                        {
                            CompanyName = drtBranchRow.CompanyName.ToString();
                            TableHTML += "<tr valign='middle'><th rowspan='3' align='right' style='padding-left:25px;width:20%'><img src='http://117.200.70.70/svs/images/smslogo.jpg' width='100%' alt=''/></th><th style='font-size:19px;text-align:center;'>" + drtBranchRow.CompanyName + "</th></tr>";
                            TableHTML += "<tr><th style='text-align:center;'>" + drtBranchRow.CompanyAddress + "</th></tr>";
                            if (drtBranchRow.PhoneNo2.Length > 2)
                                TableHTML += "<tr><th style='text-align:center;'> Phone :" + drtBranchRow.PhoneNo1 + "/" + drtBranchRow.PhoneNo2 + "</th></tr>";
                            else
                                TableHTML += "<tr><th style='text-align:center;'> Phone :" + drtBranchRow.PhoneNo1 + "</th></tr>";
                        }

                        TableHTML += "</table>";

                    }
                    TableHTML += "<table cellpadding='0' cellspacing='0' ><thead><tr  colspan='12'><th  style='text-align:center;'>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbspCustomer Ledger</th></tr></thead></table>";
                    TableHTML += "<table cellpadding='0' cellspacing='0'><thead>";
                    TableHTML += "<tr><td><b>Mr./Ms." + drtJobCardRow.CustomerName + "</b></td><td><b>Account No.: </b>" + drtJobCardRow.AccountNo + "</td></tr>";
                    TableHTML += "<tr><td> " + drtJobCardRow.Address + "</td><td><b>Installment Amount:</b> Rs." + drtJobCardRow.InstallmentAmount + "</td></tr>";
                    TableHTML += "<tr><td><b>Mobile No.</b>: " + drtJobCardRow.MobileNo + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</td><td><b>Joining Dt.: </b>" + drtJobCardRow.RegisterDate.ToString("dd-MM-yyyy") + "</td></tr>";
                    TableHTML += "<tr><td><b>Salesman Code: </b>" + drtJobCardRow.EmployeeCode + "</td><td><b>Maturity Dt.: </b>" + drtJobCardRow.RegisterDate.AddMonths(drtJobCardRow.Duration).ToString("dd-MM-yyyy") + "</td></tr>";
                    TableHTML += "<tr><td><b>Scheme: </b>" + drtJobCardRow.ChitName + "</td><td><b>Branch: </b>" + drtJobCardRow.BranchName + "</td></tr>";
                    TableHTML += "<br/>";
                    TableHTML += "</thead></table><br/>";
                    TableHTML += "<table cellpadding='0' cellspacing='0' class='border_details' ><thead>";
                    TableHTML += "<tr><th style='width:5%;text-align:left'><b>&nbsp S.No</b></th><th style='width:10%;text-align:left'><b>Receipt Dt.</b></th></th><th   style='width:10%;text-align:right'><b>Receipt No.</b></th><th   style='width:10%;text-align:right'><b>Amount</b></th><th   style='width:10%;text-align:right'><b>Total Amount</b></th></tr></thead>";
                    decimal TAmount = 0;
                    if (dsReportData.tRenewal.Rows.Count > 0)
                    {
                        int sno = 1;

                        foreach (ReportDataSet.tRenewalRow drtJobCardComplaintsRow in dsReportData.tRenewal)
                        {
                            if (drtJobCardComplaintsRow.FK_RegisterID == drtJobCardRow.PK_RegisterID)
                            {
                                TAmount += drtJobCardComplaintsRow.AmountPaid;
                                TableHTML += "<tr><td>&nbsp &nbsp" + sno + "</td><td>" + drtJobCardComplaintsRow.RenewalDate.ToString("dd-MM-yyyy") + "</td><td  style='text-align:right'> " + drtJobCardComplaintsRow.RenewalNo + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.AmountPaid + "</td><td  style='text-align:right'>" + TAmount + "</td></tr>";
                                sno++;
                            }
                        }
                    }

                    TableHTML += "<br/><tr style='text-align:right'><td colspan='3'></td><td style='text-align:right'><b>Total Amount  </b></td><td style='text-align:right'><b> " + TAmount + "</b></td></tr>";
                    TableHTML += "</table>";
                    if (drtJobCardRow.Status != "Open")
                    {
                        TableHTML += "<table cellpadding='0' cellspacing='0'><thead>";
                        TableHTML += "<tr><td><b style='font-size: 18px;font-weight: bold;color: red;'>A/C Closed&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</b></td><td><b style='font-size: 18px;font-weight: bold;color: red;'>Closed Date:" + drtJobCardRow.CancelledDate.ToString("dd-MM-yyyy") + "</b></td></tr>";
                        TableHTML += "</thead></table>";
                    }
                    TableHTML += "<br/>";
                    TableHTML += "<br/>";
                    TableHTML += "<br/>";
                    TableHTML += "<br/></body></html>";
                }


            }

            List<AddRegisterData> servDetail = new List<AddRegisterData>();

            if (dsReportData.tRegister.Rows.Count > 0)
            {
                AddRegisterData e = new AddRegisterData();
                e.Service = TableHTML;
                servDetail.Add(e);
            }

            AddRegister invDetails = new AddRegister();
            invDetails.Details = servDetail;
            if (dsReportData.tRegister.Rows.Count > 0)
                invDetails.status = "true";
            else
                invDetails.status = "false";


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(serializer.Serialize(invDetails));
        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region Report

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void getRegisterReport(string EmployeeID, string BranchID, string RegisterID, string DateFrom, string DateTo)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();

        List<string> InvoiceDetails = new List<string>();
        try
        {
            Database db;
            DataSet dsList = null;
            Collection<VHMS.Entity.Billing.Register> objList = new Collection<VHMS.Entity.Billing.Register>();
            RegisterData objRegister = new RegisterData();

            db = VHMS.Entity.DBConnection.dbCon;
            DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_REGISTER);
            db.AddInParameter(cmd, "@FK_UserID", DbType.Int32, Convert.ToInt32(EmployeeID));
            db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, Convert.ToInt32(BranchID));
            db.AddInParameter(cmd, "@DateFrom", DbType.String, DateTime.Now.ToString("dd-MM-yyy"));
            db.AddInParameter(cmd, "@DateTo", DbType.String, DateTime.Now.ToString("dd-MM-yyy"));
            db.AddInParameter(cmd, "@PK_RegisterID", DbType.Int32, Convert.ToInt32(RegisterID));
            dsList = db.ExecuteDataSet(cmd);

            List<RegisterData> servDetail = new List<RegisterData>();

            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drData in dsList.Tables[0].Rows)
                {
                    objRegister = new RegisterData();

                    objRegister.RegisterID = Convert.ToString(drData["PK_RegisterID"]);
                    objRegister.AccountNo = Convert.ToString(drData["AccountNo"]);
                    objRegister.RegisterDate = Convert.ToDateTime(drData["RegisterDate"]);
                    objRegister.sRegisterDate = objRegister.RegisterDate.ToString("dd/MM/yyyy");
                    objRegister.IDProof = Convert.ToString(drData["IDProof"]);
                    objRegister.IDNo = Convert.ToString(drData["IDNo"]);
                    objRegister.ChitID = Convert.ToString(drData["FK_ChitID"]);
                    objRegister.ChitName = Convert.ToString(drData["ChitName"]);
                    objRegister.ChitCode = Convert.ToString(drData["ChitCode"]);
                    objRegister.Duration = Convert.ToString(drData["Duration"]);
                    objRegister.InstallmentAmount = Convert.ToString(drData["InstallmentAmount"]);
                    objRegister.TotalAmount = Convert.ToString(drData["TotalAmount"]);
                    objRegister.BonusAmount = Convert.ToString(drData["BonusAmount"]);
                    objRegister.CustomerID = Convert.ToString(drData["FK_CustomerID"]);
                    objRegister.DOB = Convert.ToString(drData["DOB"]);
                    objRegister.CustomerName = Convert.ToString(drData["CustomerName"]);
                    objRegister.Address = Convert.ToString(drData["Address"]);
                    objRegister.MobileNo = Convert.ToString(drData["MobileNo"]);
                    objRegister.BranchID = Convert.ToString(drData["FK_BranchID"]);
                    objRegister.BranchName = Convert.ToString(drData["BranchName"]);
                    objRegister.ProofImage = Convert.ToString(drData["ProofImage"]);
                    objRegister.ProofBackImage = Convert.ToString(drData["ProofBackImage"]);
                    objRegister.RegisterImage1 = Convert.ToString(drData["RegisterImage1"]);
                    objRegister.RegisterImage2 = Convert.ToString(drData["RegisterImage2"]);

                    objRegister.UserID = Convert.ToString(drData["FK_EmployeeID"]);
                    objRegister.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                    objRegister.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);

                    objRegister.IsActive = Convert.ToBoolean(drData["IsActive"]);

                    servDetail.Add(objRegister);
                }
            }
            Register oChit = new Register();
            oChit.Details = servDetail;
            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                oChit.status = "true";
            else
                oChit.status = "false";
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(serializer.Serialize(oChit));

        }
        catch
        {
            throw;
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void getRenewalReport(string EmployeeID = "0", string BranchID = "0", string RenewalID = "0", string DateFrom = "", string DateTo = "")
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();

        List<string> InvoiceDetails = new List<string>();
        try
        {
            Database db;
            DataSet dsList = null;
            Collection<VHMS.Entity.Billing.Renewal> objList = new Collection<VHMS.Entity.Billing.Renewal>();
            RenewalData objRenewal = new RenewalData();

            db = VHMS.Entity.DBConnection.dbCon;
            DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_RENEWAL);
            db.AddInParameter(cmd, "@FK_UserID", DbType.Int32, Convert.ToInt32(EmployeeID));
            db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, Convert.ToInt32(BranchID));
            db.AddInParameter(cmd, "@DateFrom", DbType.String, DateTime.Now.ToString("dd-MM-yyy"));
            db.AddInParameter(cmd, "@DateTo", DbType.String, DateTime.Now.ToString("dd-MM-yyy"));
            dsList = db.ExecuteDataSet(cmd);

            List<RenewalData> servDetail = new List<RenewalData>();

            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drData in dsList.Tables[0].Rows)
                {
                    objRenewal = new RenewalData();

                    objRenewal.RenewalID = Convert.ToString(drData["PK_RenewalID"]);
                    objRenewal.RenewalNo = Convert.ToString(drData["RenewalNo"]);
                    objRenewal.RenewalDate = Convert.ToDateTime(drData["RenewalDate"]);
                    objRenewal.sRenewalDate = objRenewal.RenewalDate.ToString("dd/MM/yyyy");

                    objRenewal.RegisterID = Convert.ToString(drData["PK_RegisterID"]);
                    objRenewal.AccountNo = Convert.ToString(drData["AccountNo"]);
                    objRenewal.RegisterDate = Convert.ToDateTime(drData["RegisterDate"]);
                    objRenewal.sRegisterDate = objRenewal.RegisterDate.ToString("dd/MM/yyyy");
                    objRenewal.IDProof = Convert.ToString(drData["IDProof"]);
                    objRenewal.IDNo = Convert.ToString(drData["IDNo"]);

                    objRenewal.ChitID = Convert.ToString(drData["FK_ChitID"]);
                    objRenewal.ChitName = Convert.ToString(drData["ChitName"]);
                    objRenewal.ChitCode = Convert.ToString(drData["ChitCode"]);
                    objRenewal.TotalAmount = Convert.ToString(drData["TotalAmount"]);
                    objRenewal.BonusAmount = Convert.ToString(drData["BonusAmount"]);
                    objRenewal.Duration = Convert.ToString(drData["Duration"]);
                    objRenewal.InstallmentAmount = Convert.ToString(drData["InstallmentAmount"]);
                    objRenewal.GrandTotal = Convert.ToString(drData["GrandTotal"]);

                    objRenewal.CustomerID = Convert.ToString(drData["FK_CustomerID"]);
                    objRenewal.CustomerName = Convert.ToString(drData["CustomerName"]);
                    objRenewal.Address = Convert.ToString(drData["Address"]);
                    objRenewal.MobileNo = Convert.ToString(drData["MobileNo"]);

                    objRenewal.BranchID = Convert.ToString(drData["FK_BranchID"]);
                    objRenewal.BranchName = Convert.ToString(drData["BranchName"]);

                    objRenewal.UserID = Convert.ToString(drData["FK_EmployeeID"]);
                    objRenewal.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                    objRenewal.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);

                    objRenewal.AmountPaid = Convert.ToString(drData["AmountPaid"]);
                    objRenewal.Status = Convert.ToString(drData["Status"]);

                    objRenewal.RUserID = Convert.ToString(drData["REmployeeID"]);
                    objRenewal.REmployeeName = Convert.ToString(drData["REmployeeName"]);
                    objRenewal.REmployeeCode = Convert.ToString(drData["REmployeeCode"]);
                    servDetail.Add(objRenewal);
                }
            }
            Renewal oChit = new Renewal();
            oChit.Details = servDetail;
            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                oChit.status = "true";
            else
                oChit.status = "false";
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(serializer.Serialize(oChit));

        }
        catch
        {
            throw;
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void getUnpaidRegisterList(string EmployeeID, string BranchID)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();

        List<string> InvoiceDetails = new List<string>();
        try
        {
            Database db;
            DataSet dsList = null;
            Collection<VHMS.Entity.Billing.Register> objList = new Collection<VHMS.Entity.Billing.Register>();
            Register1Data objRegister = new Register1Data();

            db = VHMS.Entity.DBConnection.dbCon;
            DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_UNPAIDREGISTERREPORT);
            db.AddInParameter(cmd, "@FK_UserID", DbType.Int32, Convert.ToInt32(EmployeeID));
            db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, Convert.ToInt32(BranchID));
            dsList = db.ExecuteDataSet(cmd);

            List<Register1Data> servDetail = new List<Register1Data>();

            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drData in dsList.Tables[0].Rows)
                {
                    objRegister = new Register1Data();

                    objRegister.RegisterID = Convert.ToString(drData["PK_RegisterID"]);
                    objRegister.AccountNo = Convert.ToString(drData["AccountNo"]);
                    objRegister.CustomerName = Convert.ToString(drData["CustomerName"]);
                    objRegister.Address = Convert.ToString(drData["Address"]);
                    objRegister.MobileNo = Convert.ToString(drData["MobileNo"]);
                    objRegister.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                    objRegister.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                    objRegister.RegisterDate = Convert.ToDateTime(drData["RegisterDate"]);
                    objRegister.sRegisterDate = objRegister.RegisterDate.ToString("dd/MM/yyyy");
                    objRegister.ChitName = Convert.ToString(drData["ChitName"]);
                    objRegister.ChitCode = Convert.ToString(drData["ChitCode"]);
                    objRegister.Duration = Convert.ToString(drData["Duration"]);
                    objRegister.InstallmentAmount = Convert.ToString(drData["InstallmentAmount"]);
                    objRegister.Balance = Convert.ToDecimal(drData["Balance"]);
                    objRegister.NoOfDues = Convert.ToInt32(drData["NoOfDues"]);
                    objRegister.CustomerID = Convert.ToString(drData["FK_CustomerID"]);
                    objRegister.BranchID = Convert.ToString(drData["FK_BranchID"]);
                    objRegister.BranchName = Convert.ToString(drData["BranchName"]);
                    objRegister.UserID = Convert.ToString(drData["FK_EmployeeID"]);
                    servDetail.Add(objRegister);
                }
                Register1 oChit = new Register1();
                oChit.Details = servDetail;
                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                    oChit.status = "true";
                else
                    oChit.status = "false";
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(serializer.Serialize(oChit));
            }

        }
        catch
        {
            throw;
        }
    }


    public class Register1
    {
        public List<Register1Data> Details;
        public string status;
    }
    public class Register1Data
    {
        public string RegisterID { get; set; }
        public DateTime RegisterDate { get; set; }
        public string sRegisterDate { get; set; }
        public decimal Balance { get; set; }
        public int NoOfDues { get; set; }
        public string AccountNo { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string InstallmentAmount { get; set; }
        public string ChitID { get; set; }
        public string ChitName { get; set; }
        public string ChitCode { get; set; }
        public string Duration { get; set; }
        public string CustomerID { get; set; }
        public string BranchID { get; set; }
        public string BranchName { get; set; }
        public string UserID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
    }
    #endregion

    #region VisitCustomer

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void getVisitCustomerList(string EmployeeID, string BranchID, string CustomerID, string RegionID, string ZoneID)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();

        List<string> InvoiceDetails = new List<string>();
        try
        {
            Database db;
            DataSet dsList = null;
            VisitCustomerData objRenewal = new VisitCustomerData();

            db = VHMS.Entity.DBConnection.dbCon;
            DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_VISITCUSTOMER);
            db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, Convert.ToInt32(EmployeeID));
            db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, Convert.ToInt32(BranchID));
            db.AddInParameter(cmd, "@PK_CustomerID", DbType.Int32, Convert.ToInt32(CustomerID));
            db.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, Convert.ToInt32(RegionID));
            db.AddInParameter(cmd, "@FK_ZoneID", DbType.Int32, Convert.ToInt32(ZoneID));
            dsList = db.ExecuteDataSet(cmd);

            List<VisitCustomerData> servDetail = new List<VisitCustomerData>();

            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drData in dsList.Tables[0].Rows)
                {
                    objRenewal = new VisitCustomerData();


                    objRenewal.CustomerID = Convert.ToString(drData["PK_CustomerID"]);
                    objRenewal.CustomerName = Convert.ToString(drData["CustomerName"]);
                    objRenewal.Address = Convert.ToString(drData["Address"]);
                    objRenewal.MobileNo = Convert.ToString(drData["MobileNo"]);
                    objRenewal.DOB = Convert.ToString(drData["DOB"]);
                    objRenewal.AnniversaryDate = Convert.ToString(drData["AnniversaryDate"]);
                    objRenewal.EmployeeID = Convert.ToString(drData["FK_CreatedBy"]);
                    objRenewal.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                    objRenewal.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                    servDetail.Add(objRenewal);
                }
            }
            VisitCustomer oChit = new VisitCustomer();
            oChit.Details = servDetail;
            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                oChit.status = "true";
            else
                oChit.status = "false";
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(serializer.Serialize(oChit));

        }
        catch
        {
            throw;
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void AddVisitCustomerDetails(string CustomerID, string EmployeeID, string DOB, string Address, string CustomerName, string MobileNo, string AnniversaryDate)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();
        int iID = 0;
        List<string> InvoiceDetails = new List<string>();
        try
        {
            Database db;
            db = VHMS.Entity.DBConnection.dbCon;

            using (DbConnection oDbCon = db.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();

                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_CUSTOMER);

                db.AddInParameter(cmd, "@PK_CustomerID", DbType.Int32, 10);
                db.AddInParameter(cmd, "@CustomerName", DbType.String, CustomerName);
                db.AddInParameter(cmd, "@Customercode", DbType.String, "");
                db.AddInParameter(cmd, "@CustomerType", DbType.String, "Visit Customer");
                db.AddInParameter(cmd, "@Address", DbType.String, Address);
                db.AddInParameter(cmd, "@MobileNo", DbType.String, MobileNo);
                db.AddInParameter(cmd, "@DOB", DbType.String, DOB);
                db.AddInParameter(cmd, "@AlternateNo", DbType.String, "");
                db.AddInParameter(cmd, "@GSTNo", DbType.String, "");
                db.AddInParameter(cmd, "@Email", DbType.String, "");
                db.AddInParameter(cmd, "@AnniversaryDate", DbType.String, AnniversaryDate);
                db.AddInParameter(cmd, "@IsActive", DbType.Boolean, true);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, Convert.ToInt32(EmployeeID));
                iID = db.ExecuteNonQuery(cmd, oTrans);
                oTrans.Commit();
            }
            List<AddRegisterData> servDetail = new List<AddRegisterData>();

            if (iID > 0)
            {
                AddRegisterData e = new AddRegisterData();
                e.Service = "Customer Added Successfully..";
                servDetail.Add(e);
            }

            AddRegister invDetails = new AddRegister();
            invDetails.Details = servDetail;
            if (iID > 0)
                invDetails.status = "true";
            else
                invDetails.status = "false";

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(serializer.Serialize(invDetails));
        }
        catch
        {
            throw;
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void UpdateVisitCustomerDetails(string CustomerID, string EmployeeID, string DOB, string Address, string CustomerName, string MobileNo, string AnniversaryDate)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();
        int iID = 0;
        List<string> InvoiceDetails = new List<string>();
        try
        {
            Database db;
            db = VHMS.Entity.DBConnection.dbCon;

            using (DbConnection oDbCon = db.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();

                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_CUSTOMER);

                db.AddInParameter(cmd, "@PK_CustomerID", DbType.Int32, CustomerID);
                db.AddInParameter(cmd, "@CustomerName", DbType.String, CustomerName);
                db.AddInParameter(cmd, "@Customercode", DbType.String, "");
                db.AddInParameter(cmd, "@Address", DbType.String, Address);
                db.AddInParameter(cmd, "@MobileNo", DbType.String, MobileNo);
                db.AddInParameter(cmd, "@DOB", DbType.String, DOB);
                db.AddInParameter(cmd, "@AlternateNo", DbType.String, "");
                db.AddInParameter(cmd, "@GSTNo", DbType.String, "");
                db.AddInParameter(cmd, "@Email", DbType.String, "");
                db.AddInParameter(cmd, "@AnniversaryDate", DbType.String, AnniversaryDate);
                db.AddInParameter(cmd, "@IsActive", DbType.Boolean, true);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, Convert.ToInt32(EmployeeID));
                iID = db.ExecuteNonQuery(cmd, oTrans);
                oTrans.Commit();
            }
            List<AddRegisterData> servDetail = new List<AddRegisterData>();

            if (iID > 0)
            {
                AddRegisterData e = new AddRegisterData();
                e.Service = "Customer Updated Successfully..";
                servDetail.Add(e);
            }

            AddRegister invDetails = new AddRegister();
            invDetails.Details = servDetail;
            if (iID > 0)
                invDetails.status = "true";
            else
                invDetails.status = "false";

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(serializer.Serialize(invDetails));
        }
        catch
        {
            throw;
        }
    }

    public class VisitCustomer
    {
        public List<VisitCustomerData> Details;
        public string status;
    }

    public class VisitCustomerData
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string AnniversaryDate { get; set; }
        public string Email { get; set; }
        public string GSTNo { get; set; }
        public string DOB { get; set; }
        public string CustomerType { get; set; }
        public Boolean IsActive { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }

    }
    #endregion

    #region Settings

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void getSettings()
    {
        try
        {
            Database db;
            DataSet dsList = null;
            SettingsData objRenewal = new SettingsData();

            db = VHMS.Entity.DBConnection.dbCon;
            DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SETTINGS);

            dsList = db.ExecuteDataSet(cmd);

            List<SettingsData> servDetail = new List<SettingsData>();

            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drData in dsList.Tables[0].Rows)
                {
                    objRenewal = new SettingsData();
                    objRenewal.SendSMS = Convert.ToString(drData["SendSMS"]);
                    objRenewal.SenderName = Convert.ToString(drData["SenderName"]);
                    objRenewal.APILink = Convert.ToString(drData["APILink"]);
                    objRenewal.SMSUsername = Convert.ToString(drData["SMSUsername"]);
                    objRenewal.SMSPassword = Convert.ToString(drData["SMSPassword"]);

                    servDetail.Add(objRenewal);
                }
            }
            Settings oChit = new Settings();
            oChit.Details = servDetail;
            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                oChit.status = "true";
            else
                oChit.status = "false";
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(serializer.Serialize(oChit));

        }
        catch
        {
            throw;
        }
    }

    public class Settings
    {
        public List<SettingsData> Details;
        public string status;
    }

    public class SettingsData
    {
        public string SendSMS { get; set; }
        public string SenderName { get; set; }
        public string APILink { get; set; }
        public string SMSUsername { get; set; }
        public string SMSPassword { get; set; }
    }

    private void SendSMS(int id, string type)
    {
        try
        {
            string strMessage = "";
            bool SendSMS = false;
            string SenderName = "";
            string APILink = "";
            string SMSUsername = "";
            string SMSPassword = "";

            Database db;
            DataSet dsList = null;
            SettingsData objRenewal = new SettingsData();

            db = VHMS.Entity.DBConnection.dbCon;
            DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SETTINGS);

            dsList = db.ExecuteDataSet(cmd);
            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drData in dsList.Tables[0].Rows)
                {
                    objRenewal = new SettingsData();
                    SendSMS = Convert.ToBoolean(drData["SendSMS"]);
                    SenderName = Convert.ToString(drData["SenderName"]);
                    APILink = Convert.ToString(drData["APILink"]);
                    SMSUsername = Convert.ToString(drData["SMSUsername"]);
                    SMSPassword = Convert.ToString(drData["SMSPassword"]);
                }
                if (SendSMS)
                {
                    if (type == "Register")
                    {
                        VHMS.Entity.Billing.Register obj = new VHMS.Entity.Billing.Register();
                        obj = VHMS.DataAccess.Billing.Register.GetRegisterByID(id);
                        if (obj != null)
                            strMessage = "Dear " + obj.CustomerName + ", You are registered in " + obj.Chit.ChitName + " Scheme. Your Account No.: " + obj.AccountNo + ", Receipt No.: " + obj.ReceiptNo + " Paid Amount : Rs." + obj.InstallmentAmount + "- SVS Jewellers.";
                        if (APILink.Contains("#mobile#"))
                            APILink = APILink.Replace("#mobile#", obj.MobileNo);
                    }
                    else
                    {
                        VHMS.Entity.Billing.Renewal obj = new VHMS.Entity.Billing.Renewal();
                        obj = VHMS.DataAccess.Billing.Renewal.GetRenewalByID(id);
                        if (obj != null)
                            strMessage = "Dear " + obj.Register.Customer.CustomerName + ", Your Receipt No : " + obj.RenewalNo + " Scheme. Your Register No.: " + obj.Register.AccountNo + ", Installation Amount : Rs." + obj.AmountPaid + ". Total Amount Paid : Rs." + obj.Register.ChitAmount + "- SVS Jewellers.";
                        if (APILink.Contains("#mobile#"))
                            APILink = APILink.Replace("#mobile#", obj.Register.Customer.MobileNo);
                    }
                    if (APILink.Contains("#message#"))
                        APILink = APILink.Replace("#message#", strMessage);
                    if (APILink.Contains("#uname#"))
                        APILink = APILink.Replace("#uname#", SMSUsername);
                    if (APILink.Contains("#pwd#"))
                        APILink = APILink.Replace("#pwd#", SMSPassword);

                    if (APILink.Contains("#sendername#"))
                        APILink = APILink.Replace("#sendername#", SenderName);

                    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(APILink);
                    HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                    System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                    string responseString = respStreamReader.ReadToEnd();
                    respStreamReader.Close();
                    myResp.Close();
                }
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    #endregion

    #region Branch

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void getProductbyCode(string Code)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();

        List<string> InvoiceDetails = new List<string>();
        try
        {
            Database db;
            DataSet dsList = null;
            ProductData objBranch = new ProductData();

            db = VHMS.Entity.DBConnection.dbCon;
            DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PRODUCTBYCODE_APP);
            db.AddInParameter(cmd, "@Code", DbType.String, Convert.ToString(Code));
            dsList = db.ExecuteDataSet(cmd);

            List<ProductData> servDetail = new List<ProductData>();

            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drData in dsList.Tables[0].Rows)
                {
                    objBranch = new ProductData();

                    objBranch.ProductID = Convert.ToString(drData["PK_ProductID"]);
                    objBranch.ProductName = Convert.ToString(drData["ProductName"]);
                    objBranch.ProductCode = Convert.ToString(drData["ProductCode"]);
                    objBranch.SMSCode = Convert.ToString(drData["SMSCode"]);
                    objBranch.WholeSalePrice = Convert.ToString(drData["WholeSalePrice"]);
                    objBranch.RetailPrice = Convert.ToString(drData["RetailPrice"]);
                    objBranch.PurchasePrice = Convert.ToString(drData["PurchasePrice"]);

                    servDetail.Add(objBranch);
                }

                Product oChit = new Product();
                oChit.Details = servDetail;
                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                    oChit.status = "true";
                else
                    oChit.status = "false";
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(serializer.Serialize(oChit));
            }
            else
            {
                Product oChit = new Product();
                oChit.Details = servDetail;
                oChit.status = "false";
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(serializer.Serialize(oChit));
            }

        }
        catch
        {
            throw;
        }
    }

    public class ProductData
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string SMSCode { get; set; }
        public string WholeSalePrice { get; set; }
        public string RetailPrice { get; set; }
        public string PurchasePrice { get; set; }

    }

    public class Product
    {
        public List<ProductData> Details;
        public string status;
    }
    #endregion
}
