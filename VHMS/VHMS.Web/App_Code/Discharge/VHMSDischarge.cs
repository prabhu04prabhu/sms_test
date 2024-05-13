using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;
using System.Data;
using System.Web;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Globalization;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using VHMS.Entity;

public partial class VHMSService : IVHMSService
{
    #region "Department"
    public string GetDepartment()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Discharge.Department> ObjList = new Collection<VHMS.Entity.Discharge.Department>();
                ObjList = VHMS.DataAccess.Discharge.Department.GetDepartment();
                objResponse.Status = "Success";
                objResponse.Value = ObjList.Count > 0 ? jsObject.Serialize(ObjList) : "NoRecord";
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Department GetDepartment |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetDepartmentByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Discharge.Department objDepartment = new VHMS.Entity.Discharge.Department();
                objDepartment = VHMS.DataAccess.Discharge.Department.GetDepartmentByID(ID);
                if (objDepartment.DepartmentID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objDepartment);
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "NoRecord";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Department GetDepartmentByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddDepartment(VHMS.Entity.Discharge.Department Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iDepartmentId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iDepartmentId = VHMS.DataAccess.Discharge.Department.AddDepartment(Objdata);
                if (iDepartmentId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iDepartmentId.ToString();
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Department AddDepartment |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Department_A_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "Error";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string UpdateDepartment(VHMS.Entity.Discharge.Department Objdata)
    {
        string sDepartmentId = string.Empty;
        string sException = string.Empty;
        bool bDepartment = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserID;
                Objdata.ModifiedBy = objUser;
                bDepartment = VHMS.DataAccess.Discharge.Department.UpdateDepartment(Objdata);
                if (bDepartment == true)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "1";
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "0";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Department UpdateDepartment |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Department_U_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string DeleteDepartment(int ID)
    {
        string sDepartmentId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bDepartment = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bDepartment = VHMS.DataAccess.Discharge.Department.DeleteDepartment(ID, UserID);
                if (bDepartment == true)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "1";
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "0";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Department DeleteDepartment |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Department_R_01" || ex.Message.ToString() == "Department_D_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    #endregion

    #region "Specialization"
    public string GetSpecialization()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Discharge.Specialization> ObjList = new Collection<VHMS.Entity.Discharge.Specialization>();
                ObjList = VHMS.DataAccess.Discharge.Specialization.GetSpecialization();
                objResponse.Status = "Success";
                objResponse.Value = ObjList.Count > 0 ? jsObject.Serialize(ObjList) : "NoRecord";
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Specialization GetSpecialization |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSpecializationByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Discharge.Specialization objSpecialization = new VHMS.Entity.Discharge.Specialization();
                objSpecialization = VHMS.DataAccess.Discharge.Specialization.GetSpecializationByID(ID);
                if (objSpecialization.SpecializationID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objSpecialization);
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "NoRecord";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Specialization GetSpecializationByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddSpecialization(VHMS.Entity.Discharge.Specialization Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iSpecializationId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iSpecializationId = VHMS.DataAccess.Discharge.Specialization.AddSpecialization(Objdata);
                if (iSpecializationId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iSpecializationId.ToString();
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Specialization AddSpecialization |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Specialization_A_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "Error";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string UpdateSpecialization(VHMS.Entity.Discharge.Specialization Objdata)
    {
        string sSpecializationId = string.Empty;
        string sException = string.Empty;
        bool bSpecialization = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserID;
                Objdata.ModifiedBy = objUser;
                bSpecialization = VHMS.DataAccess.Discharge.Specialization.UpdateSpecialization(Objdata);
                if (bSpecialization == true)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "1";
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "0";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Specialization UpdateSpecialization |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Specialization_U_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string DeleteSpecialization(int ID)
    {
        string sSpecializationId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bSpecialization = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bSpecialization = VHMS.DataAccess.Discharge.Specialization.DeleteSpecialization(ID, UserID);
                if (bSpecialization == true)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "1";
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "0";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Specialization DeleteSpecialization |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Specialization_R_01" || ex.Message.ToString() == "Specialization_D_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    #endregion

    #region "Drug"
    public string GetDrug()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();        
        try
        {
            if (ValidateSession())
            {
                jsObject.MaxJsonLength = int.MaxValue;
                Collection<VHMS.Entity.Discharge.Drug> ObjList = new Collection<VHMS.Entity.Discharge.Drug>();
                ObjList = VHMS.DataAccess.Discharge.Drug.GetDrug();
                objResponse.Status = "Success";
                objResponse.Value = ObjList.Count > 0 ? jsObject.Serialize(ObjList) : "NoRecord";
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Drug GetDrug |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetDrugByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Discharge.Drug objDrug = new VHMS.Entity.Discharge.Drug();
                objDrug = VHMS.DataAccess.Discharge.Drug.GetDrugByID(ID);
                if (objDrug.DrugID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objDrug);
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "NoRecord";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Drug GetDrugByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddDrug(VHMS.Entity.Discharge.Drug Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iDrugId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iDrugId = VHMS.DataAccess.Discharge.Drug.AddDrug(Objdata);
                if (iDrugId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iDrugId.ToString();
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Drug AddDrug |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Drug_A_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "Error";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string UpdateDrug(VHMS.Entity.Discharge.Drug Objdata)
    {
        string sDrugId = string.Empty;
        string sException = string.Empty;
        bool bDrug = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserID;
                Objdata.ModifiedBy = objUser;
                bDrug = VHMS.DataAccess.Discharge.Drug.UpdateDrug(Objdata);
                if (bDrug == true)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "1";
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "0";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Drug UpdateDrug |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Drug_U_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string DeleteDrug(int ID)
    {
        string sDrugId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bDrug = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bDrug = VHMS.DataAccess.Discharge.Drug.DeleteDrug(ID, UserID);
                if (bDrug == true)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "1";
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "0";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Drug DeleteDrug |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Drug_R_01" || ex.Message.ToString() == "Drug_D_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    #endregion

    #region "Diagonsis"
    public string GetDiagonsis()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                jsObject.MaxJsonLength = int.MaxValue;
                Collection<VHMS.Entity.Discharge.Diagonsis> ObjList = new Collection<VHMS.Entity.Discharge.Diagonsis>();
                ObjList = VHMS.DataAccess.Discharge.Diagonsis.GetDiagonsis();
                objResponse.Status = "Success";
                objResponse.Value = ObjList.Count > 0 ? jsObject.Serialize(ObjList) : "NoRecord";
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Diagonsis GetDiagonsis |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetDiagonsisByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Discharge.Diagonsis objDiagonsis = new VHMS.Entity.Discharge.Diagonsis();
                objDiagonsis = VHMS.DataAccess.Discharge.Diagonsis.GetDiagonsisByID(ID);
                if (objDiagonsis.DiagonsisID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objDiagonsis);
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "NoRecord";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Diagonsis GetDiagonsisByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddDiagonsis(VHMS.Entity.Discharge.Diagonsis Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iDiagonsisId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iDiagonsisId = VHMS.DataAccess.Discharge.Diagonsis.AddDiagonsis(Objdata);
                if (iDiagonsisId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iDiagonsisId.ToString();
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Diagonsis AddDiagonsis |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Diagonsis_A_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "Error";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string UpdateDiagonsis(VHMS.Entity.Discharge.Diagonsis Objdata)
    {
        string sDiagonsisId = string.Empty;
        string sException = string.Empty;
        bool bDiagonsis = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserID;
                Objdata.ModifiedBy = objUser;
                bDiagonsis = VHMS.DataAccess.Discharge.Diagonsis.UpdateDiagonsis(Objdata);
                if (bDiagonsis == true)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "1";
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "0";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Diagonsis UpdateDiagonsis |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Diagonsis_U_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string DeleteDiagonsis(int ID)
    {
        string sDiagonsisId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bDiagonsis = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bDiagonsis = VHMS.DataAccess.Discharge.Diagonsis.DeleteDiagonsis(ID, UserID);
                if (bDiagonsis == true)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "1";
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "0";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Diagonsis DeleteDiagonsis |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Diagonsis_R_01" || ex.Message.ToString() == "Diagonsis_D_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    #endregion

    #region "Doctor"
    public string GetDoctor()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                jsObject.MaxJsonLength = int.MaxValue;
                Collection<VHMS.Entity.Discharge.Doctor> ObjList = new Collection<VHMS.Entity.Discharge.Doctor>();
                ObjList = VHMS.DataAccess.Discharge.Doctor.GetDoctor(0);
                objResponse.Status = "Success";
                objResponse.Value = ObjList.Count > 0 ? jsObject.Serialize(ObjList) : "NoRecord";
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Doctor GetDoctor |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetDoctorByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Discharge.Doctor objDoctor = new VHMS.Entity.Discharge.Doctor();
                objDoctor = VHMS.DataAccess.Discharge.Doctor.GetDoctorByID(ID);
                if (objDoctor.DoctorID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objDoctor);
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "NoRecord";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Doctor GetDoctorByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddDoctor(VHMS.Entity.Discharge.Doctor Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iDoctorId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iDoctorId = VHMS.DataAccess.Discharge.Doctor.AddDoctor(Objdata);
                if (iDoctorId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iDoctorId.ToString();
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Doctor AddDoctor |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Doctor_A_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "Error";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string UpdateDoctor(VHMS.Entity.Discharge.Doctor Objdata)
    {
        string sDoctorId = string.Empty;
        string sException = string.Empty;
        bool bDoctor = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserID;
                Objdata.ModifiedBy = objUser;
                bDoctor = VHMS.DataAccess.Discharge.Doctor.UpdateDoctor(Objdata);
                if (bDoctor == true)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "1";
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "0";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Doctor UpdateDoctor |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Doctor_U_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string DeleteDoctor(int ID)
    {
        string sDoctorId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bDoctor = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bDoctor = VHMS.DataAccess.Discharge.Doctor.DeleteDoctor(ID, UserID);
                if (bDoctor == true)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "1";
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "0";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Doctor DeleteDoctor |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Doctor_R_01" || ex.Message.ToString() == "Doctor_D_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    #endregion

    #region "DoctorType"
    public string GetDoctorType()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Discharge.DoctorType> ObjList = new Collection<VHMS.Entity.Discharge.DoctorType>();
                ObjList = VHMS.DataAccess.Discharge.DoctorType.GetDoctorType();
                objResponse.Status = "Success";
                objResponse.Value = ObjList.Count > 0 ? jsObject.Serialize(ObjList) : "NoRecord";
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.DoctorType GetDoctorType |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetDoctorTypeByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Discharge.DoctorType objDoctorType = new VHMS.Entity.Discharge.DoctorType();
                objDoctorType = VHMS.DataAccess.Discharge.DoctorType.GetDoctorTypeByID(ID);
                if (objDoctorType.DoctorTypeID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objDoctorType);
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "NoRecord";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.DoctorType GetDoctorTypeByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddDoctorType(VHMS.Entity.Discharge.DoctorType Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iDoctorTypeId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iDoctorTypeId = VHMS.DataAccess.Discharge.DoctorType.AddDoctorType(Objdata);
                if (iDoctorTypeId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iDoctorTypeId.ToString();
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.DoctorType AddDoctorType |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "DoctorType_A_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "Error";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string UpdateDoctorType(VHMS.Entity.Discharge.DoctorType Objdata)
    {
        string sDoctorTypeId = string.Empty;
        string sException = string.Empty;
        bool bDoctorType = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserID;
                Objdata.ModifiedBy = objUser;
                bDoctorType = VHMS.DataAccess.Discharge.DoctorType.UpdateDoctorType(Objdata);
                if (bDoctorType == true)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "1";
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "0";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.DoctorType UpdateDoctorType |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "DoctorType_U_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string DeleteDoctorType(int ID)
    {
        string sDoctorTypeId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bDoctorType = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bDoctorType = VHMS.DataAccess.Discharge.DoctorType.DeleteDoctorType(ID, UserID);
                if (bDoctorType == true)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "1";
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "0";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.DoctorType DeleteDoctorType |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "DoctorType_R_01" || ex.Message.ToString() == "DoctorType_D_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    #endregion

    #region "OtherProcedure"
    public string GetOtherProcedure()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                jsObject.MaxJsonLength = int.MaxValue;
                Collection<VHMS.Entity.Discharge.OtherProcedure> ObjList = new Collection<VHMS.Entity.Discharge.OtherProcedure>();
                ObjList = VHMS.DataAccess.Discharge.OtherProcedure.GetOtherProcedure();
                objResponse.Status = "Success";
                objResponse.Value = ObjList.Count > 0 ? jsObject.Serialize(ObjList) : "NoRecord";
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.OtherProcedure GetOtherProcedure |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetOtherProcedureByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Discharge.OtherProcedure objOtherProcedure = new VHMS.Entity.Discharge.OtherProcedure();
                objOtherProcedure = VHMS.DataAccess.Discharge.OtherProcedure.GetOtherProcedureByID(ID);
                if (objOtherProcedure.OtherProcedureID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objOtherProcedure);
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "NoRecord";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.OtherProcedure GetOtherProcedureByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddOtherProcedure(VHMS.Entity.Discharge.OtherProcedure Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iOtherProcedureId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iOtherProcedureId = VHMS.DataAccess.Discharge.OtherProcedure.AddOtherProcedure(Objdata);
                if (iOtherProcedureId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iOtherProcedureId.ToString();
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.OtherProcedure AddOtherProcedure |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "OtherProcedure_A_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "Error";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string UpdateOtherProcedure(VHMS.Entity.Discharge.OtherProcedure Objdata)
    {
        string sOtherProcedureId = string.Empty;
        string sException = string.Empty;
        bool bOtherProcedure = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserID;
                Objdata.ModifiedBy = objUser;
                bOtherProcedure = VHMS.DataAccess.Discharge.OtherProcedure.UpdateOtherProcedure(Objdata);
                if (bOtherProcedure == true)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "1";
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "0";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.OtherProcedure UpdateOtherProcedure |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "OtherProcedure_U_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string DeleteOtherProcedure(int ID)
    {
        string sOtherProcedureId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bOtherProcedure = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bOtherProcedure = VHMS.DataAccess.Discharge.OtherProcedure.DeleteOtherProcedure(ID, UserID);
                if (bOtherProcedure == true)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "1";
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "0";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.OtherProcedure DeleteOtherProcedure |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "OtherProcedure_R_01" || ex.Message.ToString() == "OtherProcedure_D_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    #endregion

    //Added on 01-09-2017
    #region "Dosage"
    public string GetDosage()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                jsObject.MaxJsonLength = int.MaxValue;
                Collection<VHMS.Entity.Discharge.Dosage> ObjList = new Collection<VHMS.Entity.Discharge.Dosage>();
                ObjList = VHMS.DataAccess.Discharge.Dosage.GetDosage();
                objResponse.Status = "Success";
                objResponse.Value = ObjList.Count > 0 ? jsObject.Serialize(ObjList) : "NoRecord";
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Dosage GetDosage |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetDosageByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Discharge.Dosage objDosage = new VHMS.Entity.Discharge.Dosage();
                objDosage = VHMS.DataAccess.Discharge.Dosage.GetDosageByID(ID);
                if (objDosage.DosageID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objDosage);
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "NoRecord";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Dosage GetDosageByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddDosage(VHMS.Entity.Discharge.Dosage Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iDosageId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iDosageId = VHMS.DataAccess.Discharge.Dosage.AddDosage(Objdata);
                if (iDosageId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iDosageId.ToString();
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Dosage AddDosage |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Dosage_A_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "Error";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string UpdateDosage(VHMS.Entity.Discharge.Dosage Objdata)
    {
        string sDosageId = string.Empty;
        string sException = string.Empty;
        bool bDosage = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserID;
                Objdata.ModifiedBy = objUser;
                bDosage = VHMS.DataAccess.Discharge.Dosage.UpdateDosage(Objdata);
                if (bDosage == true)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "1";
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "0";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Dosage UpdateDosage |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Dosage_U_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string DeleteDosage(int ID)
    {
        string sDosageId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bDosage = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bDosage = VHMS.DataAccess.Discharge.Dosage.DeleteDosage(ID, UserID);
                if (bDosage == true)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "1";
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "0";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Dosage DeleteDosage |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Dosage_R_01" || ex.Message.ToString() == "Dosage_D_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string GetSearchDosage(string key)
    {
        string exception = string.Empty;
        VHMS.Entity.SearchResult ObjSearchResult;
        List<VHMS.Entity.SearchResult> ObjSearchResultList = new List<VHMS.Entity.SearchResult>();

        JavaScriptSerializer jsSerializeobj = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        DataSet dsResult = null;
        try
        {
            jsSerializeobj.MaxJsonLength = Int32.MaxValue;
            if (ValidateSession())
            {
                dsResult = VHMS.DataAccess.Discharge.Dosage.GetSearchDosage(key);
                if ((dsResult != null) && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drTag in dsResult.Tables[0].Rows)
                    {
                        ObjSearchResult = new VHMS.Entity.SearchResult();
                        ObjSearchResult.ID = drTag[0].ToString();
                        ObjSearchResult.FirstName = drTag[1].ToString();
                        ObjSearchResultList.Add(ObjSearchResult);
                    }
                }
            }
            else
                return "0";
        }
        catch (Exception Ex)
        {
            exception = "Dosage GetSearchDosage| " + Ex.ToString();
            Log.Write(exception);
        }
        return jsSerializeobj.Serialize(ObjSearchResultList);
    }
    #endregion

    #region "Duration"
    public string GetDuration()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                jsObject.MaxJsonLength = int.MaxValue;
                Collection<VHMS.Entity.Discharge.Duration> ObjList = new Collection<VHMS.Entity.Discharge.Duration>();
                ObjList = VHMS.DataAccess.Discharge.Duration.GetDuration();
                objResponse.Status = "Success";
                objResponse.Value = ObjList.Count > 0 ? jsObject.Serialize(ObjList) : "NoRecord";
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Duration GetDuration |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetDurationByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Discharge.Duration objDuration = new VHMS.Entity.Discharge.Duration();
                objDuration = VHMS.DataAccess.Discharge.Duration.GetDurationByID(ID);
                if (objDuration.DurationID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objDuration);
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "NoRecord";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Duration GetDurationByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddDuration(VHMS.Entity.Discharge.Duration Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iDurationId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iDurationId = VHMS.DataAccess.Discharge.Duration.AddDuration(Objdata);
                if (iDurationId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iDurationId.ToString();
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Duration AddDuration |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Duration_A_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "Error";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string UpdateDuration(VHMS.Entity.Discharge.Duration Objdata)
    {
        string sDurationId = string.Empty;
        string sException = string.Empty;
        bool bDuration = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserID;
                Objdata.ModifiedBy = objUser;
                bDuration = VHMS.DataAccess.Discharge.Duration.UpdateDuration(Objdata);
                if (bDuration == true)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "1";
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "0";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Duration UpdateDuration |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Duration_U_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string DeleteDuration(int ID)
    {
        string sDurationId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bDuration = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bDuration = VHMS.DataAccess.Discharge.Duration.DeleteDuration(ID, UserID);
                if (bDuration == true)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "1";
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "0";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Duration DeleteDuration |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Duration_R_01" || ex.Message.ToString() == "Duration_D_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string GetSearchDuration(string key)
    {
        string exception = string.Empty;
        VHMS.Entity.SearchResult ObjSearchResult;
        List<VHMS.Entity.SearchResult> ObjSearchResultList = new List<VHMS.Entity.SearchResult>();

        JavaScriptSerializer jsSerializeobj = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        DataSet dsResult = null;
        try
        {
            jsSerializeobj.MaxJsonLength = Int32.MaxValue;
            if (ValidateSession())
            {
                dsResult = VHMS.DataAccess.Discharge.Duration.GetSearchDuration(key);
                if ((dsResult != null) && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drTag in dsResult.Tables[0].Rows)
                    {
                        ObjSearchResult = new VHMS.Entity.SearchResult();
                        ObjSearchResult.ID = drTag[0].ToString();
                        ObjSearchResult.FirstName = drTag[1].ToString();
                        ObjSearchResultList.Add(ObjSearchResult);
                    }
                }
            }
            else
                return "0";
        }
        catch (Exception Ex)
        {
            exception = "Duration GetSearchDuration| " + Ex.ToString();
            Log.Write(exception);
        }
        return jsSerializeobj.Serialize(ObjSearchResultList);
    }
    #endregion

    //Added on 24-10-2017
    #region "Frequency"
    public string GetFrequency()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                jsObject.MaxJsonLength = int.MaxValue;
                Collection<VHMS.Entity.Discharge.Frequency> ObjList = new Collection<VHMS.Entity.Discharge.Frequency>();
                ObjList = VHMS.DataAccess.Discharge.Frequency.GetFrequency();
                objResponse.Status = "Success";
                objResponse.Value = ObjList.Count > 0 ? jsObject.Serialize(ObjList) : "NoRecord";
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Frequency GetFrequency |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetFrequencyByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Discharge.Frequency objFrequency = new VHMS.Entity.Discharge.Frequency();
                objFrequency = VHMS.DataAccess.Discharge.Frequency.GetFrequencyByID(ID);
                if (objFrequency.FrequencyID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objFrequency);
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "NoRecord";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Frequency GetFrequencyByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddFrequency(VHMS.Entity.Discharge.Frequency Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iFrequencyId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iFrequencyId = VHMS.DataAccess.Discharge.Frequency.AddFrequency(Objdata);
                if (iFrequencyId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iFrequencyId.ToString();
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Frequency AddFrequency |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Frequency_A_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "Error";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string UpdateFrequency(VHMS.Entity.Discharge.Frequency Objdata)
    {
        string sFrequencyId = string.Empty;
        string sException = string.Empty;
        bool bFrequency = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserID;
                Objdata.ModifiedBy = objUser;
                bFrequency = VHMS.DataAccess.Discharge.Frequency.UpdateFrequency(Objdata);
                if (bFrequency == true)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "1";
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "0";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Frequency UpdateFrequency |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Frequency_U_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string DeleteFrequency(int ID)
    {
        string sFrequencyId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bFrequency = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bFrequency = VHMS.DataAccess.Discharge.Frequency.DeleteFrequency(ID, UserID);
                if (bFrequency == true)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "1";
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = "0";
                }
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VHMSService.Frequency DeleteFrequency |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Frequency_R_01" || ex.Message.ToString() == "Frequency_D_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        return jsonObject.Serialize(objResponse);
    }
    public string GetSearchFrequency(string key)
    {
        string exception = string.Empty;
        VHMS.Entity.SearchResult ObjSearchResult;
        List<VHMS.Entity.SearchResult> ObjSearchResultList = new List<VHMS.Entity.SearchResult>();

        JavaScriptSerializer jsSerializeobj = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        DataSet dsResult = null;
        try
        {
            jsSerializeobj.MaxJsonLength = Int32.MaxValue;
            if (ValidateSession())
            {
                dsResult = VHMS.DataAccess.Discharge.Frequency.GetSearchFrequency(key);
                if ((dsResult != null) && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drTag in dsResult.Tables[0].Rows)
                    {
                        ObjSearchResult = new VHMS.Entity.SearchResult();
                        ObjSearchResult.ID = drTag[0].ToString();
                        ObjSearchResult.FirstName = drTag[1].ToString();
                        ObjSearchResultList.Add(ObjSearchResult);
                    }
                }
            }
            else
                return "0";
        }
        catch (Exception Ex)
        {
            exception = "Frequency GetSearchFrequency| " + Ex.ToString();
            Log.Write(exception);
        }
        return jsSerializeobj.Serialize(ObjSearchResultList);
    }
    #endregion
}