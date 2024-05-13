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

    #region "WareHouse"
    public string GetWareHouse()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.WareHouse> ObjList = new Collection<VHMS.Entity.Billing.WareHouse>();
                ObjList = VHMS.DataAccess.Billing.WareHouse.GetWareHouse();
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
            sException = "VHMSService.Billing.WareHouse GetWareHouse |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetWareHouseByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.WareHouse objWareHouse = new VHMS.Entity.Billing.WareHouse();
                objWareHouse = VHMS.DataAccess.Billing.WareHouse.GetWareHouseByID(ID);
                if (objWareHouse.WareHouseID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objWareHouse);
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
            sException = "VHMSService.Billing.WareHouse GetWareHouseByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddWareHouse(VHMS.Entity.Billing.WareHouse Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iWareHouseId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iWareHouseId = VHMS.DataAccess.Billing.WareHouse.AddWareHouse(Objdata);
                if (iWareHouseId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iWareHouseId.ToString();
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
            sException = "VHMSService.Billing.WareHouse AddWareHouse |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "WareHouse_A_01")
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
    public string UpdateWareHouse(VHMS.Entity.Billing.WareHouse Objdata)
    {
        string sWareHouseId = string.Empty;
        string sException = string.Empty;
        bool bWareHouse = false;
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
                bWareHouse = VHMS.DataAccess.Billing.WareHouse.UpdateWareHouse(Objdata);
                if (bWareHouse == true)
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
            sException = "VHMSService.Billing.WareHouse UpdateWareHouse |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "WareHouse_U_01")
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
    public string DeleteWareHouse(int ID)
    {
        string sWareHouseId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bWareHouse = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bWareHouse = VHMS.DataAccess.Billing.WareHouse.DeleteWareHouse(ID, UserID);
                if (bWareHouse == true)
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
            sException = "VHMSService.Billing.WareHouse DeleteWareHouse |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "WareHouse_R_01" || ex.Message.ToString() == "WareHouse_D_01")
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

    #region "Patient"
    public string GetPatient()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.patient> ObjList = new Collection<VHMS.Entity.patient>();
                ObjList = VHMS.DataAccess.patient.Getpatient();
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
            sException = "Patient GetPatient |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetpatientName()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.patient> ObjList = new Collection<VHMS.Entity.patient>();
                ObjList = VHMS.DataAccess.patient.GetpatientName();
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
            sException = "Patient GetPatient |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetPatientID()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.patient> ObjList = new Collection<VHMS.Entity.patient>();
                ObjList = VHMS.DataAccess.patient.GetpatientID();
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
            sException = "Patient GetPatient |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSearchpatient(string ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.patient> ObjList = new Collection<VHMS.Entity.patient>();
                ObjList = VHMS.DataAccess.patient.GetSearchpatient(ID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Patient GetPatient |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }

    //public string GetLastOPDNo()
    //{
    //    string sException = string.Empty;
    //    string sFileNames = string.Empty;
    //    JavaScriptSerializer jsObject = new JavaScriptSerializer();
    //    VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
    //    try
    //    {
    //        if (ValidateSession())
    //        {
    //            Collection<VHMS.Entity.patient> ObjList = new Collection<VHMS.Entity.patient>();
    //            ObjList = VHMS.DataAccess.patient.Getpatient();
    //            objResponse.Status = "Success";
    //            objResponse.Value = ObjList.Count > 0 ? jsObject.Serialize(ObjList) : "NoRecord";
    //        }
    //        else
    //        {
    //            objResponse.Status = "Error";
    //            objResponse.Value = "0";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        sException = "Patient GetPatient |" + ex.Message.ToString();
    //        Log.Write(sException);
    //        objResponse.Status = "Error";
    //        objResponse.Value = "Error";
    //    }
    //    return jsObject.Serialize(objResponse);
    //}
    public string GetPatientDetails(VHMS.Entity.PatientFilter oJobCardFilter)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            //   int BranchID = 0;
            if (ValidateSession())
            {
                Collection<VHMS.Entity.patient> ObjList = new Collection<VHMS.Entity.patient>();
                ObjList = VHMS.DataAccess.patient.GetpatientDetails(oJobCardFilter);
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
            sException = "JobCard GetJobCard |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetPatientByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.patient objPatient = new VHMS.Entity.patient();
                objPatient = VHMS.DataAccess.patient.GetpatientByID(ID);
                if (objPatient.patientID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objPatient);
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
            sException = "Patient GetPatientByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetPatientByOPDNo(string ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.patient objPatient = new VHMS.Entity.patient();
                objPatient = VHMS.DataAccess.patient.GetPatientByOPDNo(ID);
                if (objPatient.patientID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objPatient);
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
            sException = "Patient GetPatientByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddPatient(VHMS.Entity.patient Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iPatientId = 0;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                //Upload images

                //    // Get the uploaded image from the Files collection
                //    var httpPostedFile = HttpContext.Current.Request.Files[0];
                //    var count = HttpContext.Current.Request.Files.Count;

                //if (httpPostedFile != null)
                //    {
                //        // Validate the uploaded image(optional)
                //        // Get the complete file path
                //        var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/images/PatientPhotos"), httpPostedFile.FileName);
                //        // Save the uploaded file to "UploadedFiles" folder
                //        httpPostedFile.SaveAs(fileSavePath);
                //    }                

                iPatientId = VHMS.DataAccess.patient.Addpatient(Objdata);
                if (iPatientId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iPatientId.ToString();
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
            sException = "Patient AddPatient |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Patient_A_01")
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
    public string UpdatePatient(VHMS.Entity.patient Objdata)
    {
        string sPatientId = string.Empty;
        string sException = string.Empty;
        bool bPatient = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;
                bPatient = VHMS.DataAccess.patient.Updatepatient(Objdata);
                if (bPatient == true)
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
            sException = "Patient UpdatePatient |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Patient_U_01")
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
    public string DeletePatient(int ID)
    {
        string sPatientId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bPatient = false;
        try
        {
            if (ValidateSession())
            {
                bPatient = VHMS.DataAccess.patient.Deletepatient(ID);
                if (bPatient == true)
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
            sException = "Patient DeletePatient |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Patient_R_01" || ex.Message.ToString() == "Patient_D_01")
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
    public string SearchPatient(string key)
    {
        string exception = string.Empty;
        VHMS.Entity.SearchResult ObjSearchResult;
        List<VHMS.Entity.SearchResult> ObjSearchResultList = new List<VHMS.Entity.SearchResult>();
        JavaScriptSerializer jsSerializeobj = new JavaScriptSerializer();
        DataSet dsTags = null;
        try
        {
            jsSerializeobj.MaxJsonLength = Int32.MaxValue;
            if (ValidateSession())
            {
                dsTags = VHMS.DataAccess.patient.Searchpatient(key);
                if ((dsTags != null) && dsTags.Tables.Count > 0 && dsTags.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drTag in dsTags.Tables[0].Rows)
                    {
                        ObjSearchResult = new VHMS.Entity.SearchResult();
                        ObjSearchResult.ID = drTag[0].ToString();
                        ObjSearchResult.FirstName = drTag[1].ToString();
                        ObjSearchResultList.Add(ObjSearchResult);
                    }
                }
            }
            else
            {
                return "0";
            }
        }
        catch (Exception Ex)
        {
            exception = "Patient SearchPatient| " + Ex.ToString();
            Log.Write(exception);
        }
        return jsSerializeobj.Serialize(ObjSearchResultList);
    }

    //public string PostImage(Stream sm)
    //{
    //    System.Drawing.Bitmap imag = new System.Drawing.Bitmap(sm);
    //    byte[] imagedata = ImageToByte(imag);
    //    return "success";
    //}

    //public static byte[] ImageToByte(System.Drawing.Image img)
    //{

    //    ImageConverter converter = new ImageConverter();
    //    return (byte[])converter.ConvertTo(img, typeof(byte[]));
    //}
    #endregion

    #region "Unit"
    public string GetUnit()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.Unit> ObjList = new Collection<VHMS.Entity.Billing.Unit>();
                ObjList = VHMS.DataAccess.Billing.Unit.GetUnit();
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
            sException = "VHMSService.Billing.Unit GetUnit |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetUnitByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.Unit objUnit = new VHMS.Entity.Billing.Unit();
                objUnit = VHMS.DataAccess.Billing.Unit.GetUnitByID(ID);
                if (objUnit.UnitID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objUnit);
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
            sException = "VHMSService.Billing.Unit GetUnitByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddUnit(VHMS.Entity.Billing.Unit Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iUnitId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iUnitId = VHMS.DataAccess.Billing.Unit.AddUnit(Objdata);
                if (iUnitId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iUnitId.ToString();
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
            sException = "VHMSService.Billing.Unit AddUnit |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Unit_A_01")
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
    public string UpdateUnit(VHMS.Entity.Billing.Unit Objdata)
    {
        string sUnitId = string.Empty;
        string sException = string.Empty;
        bool bUnit = false;
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
                bUnit = VHMS.DataAccess.Billing.Unit.UpdateUnit(Objdata);
                if (bUnit == true)
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
            sException = "VHMSService.Billing.Unit UpdateUnit |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Unit_U_01")
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
    public string DeleteUnit(int ID)
    {
        string sUnitId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bUnit = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bUnit = VHMS.DataAccess.Billing.Unit.DeleteUnit(ID, UserID);
                if (bUnit == true)
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
            sException = "VHMSService.Billing.Unit DeleteUnit |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Unit_R_01" || ex.Message.ToString() == "Unit_D_01")
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

    #region "Category"
    public string GetCategory()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.Category> ObjList = new Collection<VHMS.Entity.Billing.Category>();
                ObjList = VHMS.DataAccess.Billing.Category.GetCategory();
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
            sException = "VHMSService.Billing.Category GetCategory |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetCategoryByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.Category objCategory = new VHMS.Entity.Billing.Category();
                objCategory = VHMS.DataAccess.Billing.Category.GetCategoryByID(ID);
                if (objCategory.CategoryID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objCategory);
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
            sException = "VHMSService.Billing.Category GetCategoryByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddCategory(VHMS.Entity.Billing.Category Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iCategoryId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iCategoryId = VHMS.DataAccess.Billing.Category.AddCategory(Objdata);
                if (iCategoryId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iCategoryId.ToString();
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
            sException = "VHMSService.Billing.Category AddCategory |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Category_A_01")
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
    public string UpdateCategory(VHMS.Entity.Billing.Category Objdata)
    {
        string sCategoryId = string.Empty;
        string sException = string.Empty;
        bool bCategory = false;
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
                bCategory = VHMS.DataAccess.Billing.Category.UpdateCategory(Objdata);
                if (bCategory == true)
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
            sException = "VHMSService.Billing.Category UpdateCategory |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Category_U_01")
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
    public string DeleteCategory(int ID)
    {
        string sCategoryId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bCategory = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bCategory = VHMS.DataAccess.Billing.Category.DeleteCategory(ID, UserID);
                if (bCategory == true)
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
            sException = "VHMSService.Billing.Category DeleteCategory |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Category_R_01" || ex.Message.ToString() == "Category_D_01")
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

    #region "ProductType"
    public string GetProductType()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.ProductType> ObjList = new Collection<VHMS.Entity.Billing.ProductType>();
                ObjList = VHMS.DataAccess.Billing.ProductType.GetProductType();
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "VHMSService.Billing.ProductType GetProductType |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetProductTypeByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.ProductType objCategory = new VHMS.Entity.Billing.ProductType();
                objCategory = VHMS.DataAccess.Billing.ProductType.GetProductTypeByID(ID);
                if (objCategory.ProductTypeID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objCategory);
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
            sException = "VHMSService.Billing.ProductType GetProductTypeByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddProductType(VHMS.Entity.Billing.ProductType Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iProductTypeId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iProductTypeId = VHMS.DataAccess.Billing.ProductType.AddProductType(Objdata);
                if (iProductTypeId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iProductTypeId.ToString();
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
            sException = "VHMSService.Billing.ProductType AddProductType |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "ProductType_A_01")
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
    public string UpdateProductType(VHMS.Entity.Billing.ProductType Objdata)
    {
        string sProductTypeId = string.Empty;
        string sException = string.Empty;
        bool bProductType = false;
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
                bProductType = VHMS.DataAccess.Billing.ProductType.UpdateProductType(Objdata);
                if (bProductType == true)
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
            sException = "VHMSService.Billing.ProductType UpdateProductType |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "ProductType_U_01")
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
    public string DeleteProductType(int ID)
    {
        string sCategoryId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bCategory = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bCategory = VHMS.DataAccess.Billing.ProductType.DeleteProductType(ID, UserID);
                if (bCategory == true)
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
            sException = "VHMSService.Billing.ProductType DeleteProductType |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "ProductType_R_01" || ex.Message.ToString() == "ProductType_D_01")
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

    #region "Tax"
    public string GetTax()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.Tax> ObjList = new Collection<VHMS.Entity.Billing.Tax>();
                ObjList = VHMS.DataAccess.Billing.Tax.GetTax();
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
            sException = "VHMSService.Billing.Tax GetTax |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTaxByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.Tax objTax = new VHMS.Entity.Billing.Tax();
                objTax = VHMS.DataAccess.Billing.Tax.GetTaxByID(ID);
                if (objTax.TaxID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objTax);
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
            sException = "VHMSService.Billing.Tax GetTaxByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddTax(VHMS.Entity.Billing.Tax Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iTaxId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iTaxId = VHMS.DataAccess.Billing.Tax.AddTax(Objdata);
                if (iTaxId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iTaxId.ToString();
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
            sException = "VHMSService.Billing.Tax AddTax |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Tax_A_01")
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
    public string UpdateTax(VHMS.Entity.Billing.Tax Objdata)
    {
        string sTaxId = string.Empty;
        string sException = string.Empty;
        bool bTax = false;
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
                bTax = VHMS.DataAccess.Billing.Tax.UpdateTax(Objdata);
                if (bTax == true)
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
            sException = "VHMSService.Billing.Tax UpdateTax |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Tax_U_01")
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
    public string DeleteTax(int ID)
    {
        string sTaxId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bTax = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bTax = VHMS.DataAccess.Billing.Tax.DeleteTax(ID, UserID);
                if (bTax == true)
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
            sException = "VHMSService.Billing.Tax DeleteTax |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Tax_R_01" || ex.Message.ToString() == "Tax_D_01")
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

    #region "Agent"
    public string GetAgent()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        Response objResponse = new Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.Agent> ObjList = new Collection<VHMS.Entity.Billing.Agent>();
                ObjList = VHMS.DataAccess.Billing.Agent.GetAgent();
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "VHMSService.Billing.Agent GetAgent |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetAgentByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        Response objResponse = new Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.Agent objAgent = new VHMS.Entity.Billing.Agent();
                objAgent = VHMS.DataAccess.Billing.Agent.GetAgentByID(ID);
                if (objAgent.AgentID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objAgent);
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
            sException = "VHMSService.Billing.Agent GetAgentByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddAgent(VHMS.Entity.Billing.Agent Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        Response objResponse = new Response();
        int iAgentId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                User objUser = new User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iAgentId = VHMS.DataAccess.Billing.Agent.AddAgent(Objdata);
                if (iAgentId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iAgentId.ToString();
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
            sException = "VHMSService.Billing.Agent AddAgent |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Agent_A_01")
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
    public string UpdateAgent(VHMS.Entity.Billing.Agent Objdata)
    {
        string sAgentId = string.Empty;
        string sException = string.Empty;
        bool bAgent = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        Response objResponse = new Response();
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                User objUser = new User();
                objUser.UserID = UserID;
                Objdata.ModifiedBy = objUser;
                bAgent = VHMS.DataAccess.Billing.Agent.UpdateAgent(Objdata);
                if (bAgent == true)
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
            sException = "VHMSService.Billing.Agent UpdateAgent |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Agent_U_01")
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

    //public string UpdateWeaverWage(VHMS.Entity.Billing.WeaverWage Objdata)
    //{
    //    string sAgentId = string.Empty;
    //    string sException = string.Empty;
    //    bool bAgent = false;
    //    JavaScriptSerializer jsonObject = new JavaScriptSerializer();
    //    Response objResponse = new Response();
    //    try
    //    {
    //        int UserID = 0;
    //        if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
    //        {
    //            bAgent = VHMS.DataAccess.Billing.WeaverWage.UpdateWeaverWage(Objdata);
    //            if (bAgent == true)
    //            {
    //                objResponse.Status = "Success";
    //                objResponse.Value = "1";
    //            }
    //            else
    //            {
    //                objResponse.Status = "Success";
    //                objResponse.Value = "0";
    //            }
    //        }
    //        else
    //        {
    //            objResponse.Status = "Error";
    //            objResponse.Value = "0";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        sException = "VHMSService.Billing.Agent UpdateAgent |" + ex.Message.ToString();
    //        Log.Write(sException);
    //        if (ex.Message.ToString() == "Agent_U_01")
    //        {
    //            objResponse.Status = "Error";
    //            objResponse.Value = ex.Message.ToString();
    //        }
    //        else
    //        {
    //            objResponse.Status = "Error";
    //            objResponse.Value = "0";
    //        }
    //    }
    //    return jsonObject.Serialize(objResponse);
    //}
    public string DeleteAgent(int ID)
    {
        string sAgentId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        Response objResponse = new Response();
        bool bAgent = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bAgent = VHMS.DataAccess.Billing.Agent.DeleteAgent(ID, UserID);
                if (bAgent == true)
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
            sException = "VHMSService.Billing.Agent DeleteAgent |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Agent_R_01" || ex.Message.ToString() == "Agent_D_01")
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

    #region "SurgeryType"
    public string GetSurgeryType()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.SurgeryType> ObjList = new Collection<VHMS.Entity.Billing.SurgeryType>();
                ObjList = VHMS.DataAccess.Billing.SurgeryType.GetSurgeryType();
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
            sException = "VHMSService.Billing.SurgeryType GetSurgeryType |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSurgeryTypeByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.SurgeryType objSurgeryType = new VHMS.Entity.Billing.SurgeryType();
                objSurgeryType = VHMS.DataAccess.Billing.SurgeryType.GetSurgeryTypeByID(ID);
                if (objSurgeryType.SurgeryTypeID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objSurgeryType);
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
            sException = "VHMSService.Billing.SurgeryType GetSurgeryTypeByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddSurgeryType(VHMS.Entity.Billing.SurgeryType Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iSurgeryTypeId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iSurgeryTypeId = VHMS.DataAccess.Billing.SurgeryType.AddSurgeryType(Objdata);
                if (iSurgeryTypeId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iSurgeryTypeId.ToString();
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
            sException = "VHMSService.Billing.SurgeryType AddSurgeryType |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "SurgeryType_A_01")
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
    public string UpdateSurgeryType(VHMS.Entity.Billing.SurgeryType Objdata)
    {
        string sSurgeryTypeId = string.Empty;
        string sException = string.Empty;
        bool bSurgeryType = false;
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
                bSurgeryType = VHMS.DataAccess.Billing.SurgeryType.UpdateSurgeryType(Objdata);
                if (bSurgeryType == true)
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
            sException = "VHMSService.Billing.SurgeryType UpdateSurgeryType |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "SurgeryType_U_01")
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
    public string DeleteSurgeryType(int ID)
    {
        string sSurgeryTypeId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bSurgeryType = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bSurgeryType = VHMS.DataAccess.Billing.SurgeryType.DeleteSurgeryType(ID, UserID);
                if (bSurgeryType == true)
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
            sException = "VHMSService.Billing.SurgeryType DeleteSurgeryType |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "SurgeryType_R_01" || ex.Message.ToString() == "SurgeryType_D_01")
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

    #region "SurgicalProcedure"
    public string GetSurgicalProcedure()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.SurgicalProcedure> ObjList = new Collection<VHMS.Entity.Billing.SurgicalProcedure>();
                ObjList = VHMS.DataAccess.Billing.SurgicalProcedure.GetSurgicalProcedure();
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
            sException = "VHMSService.Billing.SurgicalProcedure GetSurgicalProcedure |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSurgicalProcedureByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.SurgicalProcedure objSurgicalProcedure = new VHMS.Entity.Billing.SurgicalProcedure();
                objSurgicalProcedure = VHMS.DataAccess.Billing.SurgicalProcedure.GetSurgicalProcedureByID(ID);
                if (objSurgicalProcedure.SurgicalProcedureID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objSurgicalProcedure);
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
            sException = "VHMSService.Billing.SurgicalProcedure GetSurgicalProcedureByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddSurgicalProcedure(VHMS.Entity.Billing.SurgicalProcedure Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iSurgicalProcedureId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iSurgicalProcedureId = VHMS.DataAccess.Billing.SurgicalProcedure.AddSurgicalProcedure(Objdata);
                if (iSurgicalProcedureId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iSurgicalProcedureId.ToString();
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
            sException = "VHMSService.Billing.SurgicalProcedure AddSurgicalProcedure |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "SurgicalProcedure_A_01")
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
    public string UpdateSurgicalProcedure(VHMS.Entity.Billing.SurgicalProcedure Objdata)
    {
        string sSurgicalProcedureId = string.Empty;
        string sException = string.Empty;
        bool bSurgicalProcedure = false;
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
                bSurgicalProcedure = VHMS.DataAccess.Billing.SurgicalProcedure.UpdateSurgicalProcedure(Objdata);
                if (bSurgicalProcedure == true)
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
            sException = "VHMSService.Billing.SurgicalProcedure UpdateSurgicalProcedure |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "SurgicalProcedure_U_01")
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
    public string DeleteSurgicalProcedure(int ID)
    {
        string sSurgicalProcedureId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bSurgicalProcedure = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bSurgicalProcedure = VHMS.DataAccess.Billing.SurgicalProcedure.DeleteSurgicalProcedure(ID, UserID);
                if (bSurgicalProcedure == true)
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
            sException = "VHMSService.Billing.SurgicalProcedure DeleteSurgicalProcedure |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "SurgicalProcedure_R_01" || ex.Message.ToString() == "SurgicalProcedure_D_01")
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

    #region "Anesthesia"
    public string GetAnesthesia()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.Anesthesia> ObjList = new Collection<VHMS.Entity.Billing.Anesthesia>();
                ObjList = VHMS.DataAccess.Billing.Anesthesia.GetAnesthesia();
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
            sException = "VHMSService.Billing.Anesthesia GetAnesthesia |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetAnesthesiaByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.Anesthesia objAnesthesia = new VHMS.Entity.Billing.Anesthesia();
                objAnesthesia = VHMS.DataAccess.Billing.Anesthesia.GetAnesthesiaByID(ID);
                if (objAnesthesia.AnesthesiaID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objAnesthesia);
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
            sException = "VHMSService.Billing.Anesthesia GetAnesthesiaByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddAnesthesia(VHMS.Entity.Billing.Anesthesia Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iAnesthesiaId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iAnesthesiaId = VHMS.DataAccess.Billing.Anesthesia.AddAnesthesia(Objdata);
                if (iAnesthesiaId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iAnesthesiaId.ToString();
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
            sException = "VHMSService.Billing.Anesthesia AddAnesthesia |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Anesthesia_A_01")
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
    public string UpdateAnesthesia(VHMS.Entity.Billing.Anesthesia Objdata)
    {
        string sAnesthesiaId = string.Empty;
        string sException = string.Empty;
        bool bAnesthesia = false;
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
                bAnesthesia = VHMS.DataAccess.Billing.Anesthesia.UpdateAnesthesia(Objdata);
                if (bAnesthesia == true)
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
            sException = "VHMSService.Billing.Anesthesia UpdateAnesthesia |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Anesthesia_U_01")
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
    public string DeleteAnesthesia(int ID)
    {
        string sAnesthesiaId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bAnesthesia = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bAnesthesia = VHMS.DataAccess.Billing.Anesthesia.DeleteAnesthesia(ID, UserID);
                if (bAnesthesia == true)
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
            sException = "VHMSService.Billing.Anesthesia DeleteAnesthesia |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Anesthesia_R_01" || ex.Message.ToString() == "Anesthesia_D_01")
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

    #region "Supplier"
    public string GetSupplier()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.Supplier> ObjList = new Collection<VHMS.Entity.Billing.Supplier>();
                ObjList = VHMS.DataAccess.Billing.Supplier.GetSupplier();
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "VHMSService.Billing.Supplier GetSupplier |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetAllSupplier(int iSupplierID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.Supplier> ObjList = new Collection<VHMS.Entity.Billing.Supplier>();
                ObjList = VHMS.DataAccess.Billing.Supplier.GetAllSupplier(iSupplierID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "VHMSService.Billing.Supplier GetSupplier |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSupplierByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.Supplier objSupplier = new VHMS.Entity.Billing.Supplier();
                objSupplier = VHMS.DataAccess.Billing.Supplier.GetSupplierByID(ID);
                if (objSupplier.SupplierID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objSupplier);
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
            sException = "VHMSService.Billing.Supplier GetSupplierByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddSupplier(VHMS.Entity.Billing.Supplier Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iSupplierId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iSupplierId = VHMS.DataAccess.Billing.Supplier.AddSupplier(Objdata);
                if (iSupplierId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iSupplierId.ToString();
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
            sException = "VHMSService.Billing.Supplier AddSupplier |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Supplier_A_01")
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
    public string UpdateSupplier(VHMS.Entity.Billing.Supplier Objdata)
    {
        string sSupplierId = string.Empty;
        string sException = string.Empty;
        bool bSupplier = false;
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
                bSupplier = VHMS.DataAccess.Billing.Supplier.UpdateSupplier(Objdata);
                if (bSupplier == true)
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
            sException = "VHMSService.Billing.Supplier UpdateSupplier |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Supplier_U_01")
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
    public string DeleteSupplier(int ID)
    {
        string sSupplierId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bSupplier = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bSupplier = VHMS.DataAccess.Billing.Supplier.DeleteSupplier(ID, UserID);
                if (bSupplier == true)
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
            sException = "VHMSService.Billing.Supplier DeleteSupplier |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Supplier_R_01" || ex.Message.ToString() == "Supplier_D_01")
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

    #region "Vendor"
    public string GetVendor()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.Vendor> ObjList = new Collection<VHMS.Entity.Billing.Vendor>();
                ObjList = VHMS.DataAccess.Billing.Vendor.GetVendor();
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "VHMSService.Billing.Vendor GetVendor |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetVendorByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.Vendor objVendor = new VHMS.Entity.Billing.Vendor();
                objVendor = VHMS.DataAccess.Billing.Vendor.GetVendorByID(ID);
                if (objVendor.VendorID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objVendor);
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
            sException = "VHMSService.Billing.Vendor GetVendorByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddVendor(VHMS.Entity.Billing.Vendor Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iVendorId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iVendorId = VHMS.DataAccess.Billing.Vendor.AddVendor(Objdata);
                if (iVendorId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iVendorId.ToString();
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
            sException = "VHMSService.Billing.Vendor AddVendor |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Vendor_A_01")
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
    public string UpdateVendor(VHMS.Entity.Billing.Vendor Objdata)
    {
        string sVendorId = string.Empty;
        string sException = string.Empty;
        bool bVendor = false;
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
                bVendor = VHMS.DataAccess.Billing.Vendor.UpdateVendor(Objdata);
                if (bVendor == true)
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
            sException = "VHMSService.Billing.Vendor UpdateVendor |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Vendor_U_01")
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
    public string DeleteVendor(int ID)
    {
        string sVendorId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bVendor = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bVendor = VHMS.DataAccess.Billing.Vendor.DeleteVendor(ID, UserID);
                if (bVendor == true)
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
            sException = "VHMSService.Billing.Vendor DeleteVendor |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Vendor_R_01" || ex.Message.ToString() == "Vendor_D_01")
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
    public string GetVendorWorkID(int VendorID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.VendorWorkTrans> ObjList = new Collection<VHMS.Entity.Billing.VendorWorkTrans>();
                ObjList = VHMS.DataAccess.Billing.Vendor.VendorWorkTrans.GetVendorWorkID(VendorID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "VHMSService.Work GetWork |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    #endregion

    #region "Prescription"
    public string GetPrescription(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.PrescriptionMaster> ObjList = new Collection<VHMS.Entity.Billing.PrescriptionMaster>();
                ObjList = VHMS.DataAccess.Billing.PrescriptionMaster.GetPrescription(PublisherID);
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
            sException = "Prescription GetPrescription |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetPrescriptionByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.PrescriptionMaster objPrescription = new VHMS.Entity.Billing.PrescriptionMaster();
                objPrescription = VHMS.DataAccess.Billing.PrescriptionMaster.GetPrescriptionByID(ID);
                if (objPrescription.PrescriptionID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objPrescription);
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
            sException = "Prescription GetPrescriptionByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddPrescription(VHMS.Entity.Billing.PrescriptionMaster Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iPrescriptionId = 0;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;
                iPrescriptionId = VHMS.DataAccess.Billing.PrescriptionMaster.AddPrescription(Objdata);
                if (iPrescriptionId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iPrescriptionId.ToString();
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
            sException = "Prescription AddPrescription |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Prescription_A_01")
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
    public string UpdatePrescription(VHMS.Entity.Billing.PrescriptionMaster Objdata)
    {
        string sPrescriptionId = string.Empty;
        string sException = string.Empty;
        bool bPrescription = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;
                bPrescription = VHMS.DataAccess.Billing.PrescriptionMaster.UpdatePrescription(Objdata);
                if (bPrescription == true)
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
            sException = "Prescription UpdatePrescription |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Prescription_U_01")
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
    public string DeletePrescription(int ID)
    {
        string sPrescriptionId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bPrescription = false;
        try
        {
            if (ValidateSession())
            {
                bPrescription = VHMS.DataAccess.Billing.PrescriptionMaster.DeletePrescription(ID);
                if (bPrescription == true)
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
            sException = "Prescription DeletePrescription |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Prescription_R_01" || ex.Message.ToString() == "Prescription_D_01")
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
    public string GetPrescriptionSummary()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                DataSet dsResult = new DataSet();
                dsResult = VHMS.DataAccess.Billing.PrescriptionMaster.GetPrescriptionSummary();

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = GetDtJson(dsResult.Tables[0]);
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
            sException = "Prescription GetPrescriptionSummary |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }

    #endregion

    #region "OPBilling"
    public string GetOPBilling(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.OPBillingMaster> ObjList = new Collection<VHMS.Entity.Billing.OPBillingMaster>();
                ObjList = VHMS.DataAccess.Billing.OPBillingMaster.GetOPBilling(PublisherID);
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
            sException = "OPBilling GetOPBilling |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetOPBillingID(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.OPBillingMaster> ObjList = new Collection<VHMS.Entity.Billing.OPBillingMaster>();
                ObjList = VHMS.DataAccess.Billing.OPBillingMaster.GetOPBillingID(PublisherID);
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
            sException = "OPBilling GetOPBilling |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchOPBilling(string ID = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.OPBillingMaster> ObjList = new Collection<VHMS.Entity.Billing.OPBillingMaster>();
                ObjList = VHMS.DataAccess.Billing.OPBillingMaster.SearchOPBilling(ID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "OPBilling GetOPBilling |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetOPBillingByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.OPBillingMaster objOPBilling = new VHMS.Entity.Billing.OPBillingMaster();
                objOPBilling = VHMS.DataAccess.Billing.OPBillingMaster.GetOPBillingByID(ID);
                if (objOPBilling.OPID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objOPBilling);
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
            sException = "OPBilling GetOPBillingByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddOPBilling(VHMS.Entity.Billing.OPBillingMaster Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iOPBillingId = 0;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;
                iOPBillingId = VHMS.DataAccess.Billing.OPBillingMaster.AddOPBilling(Objdata);
                if (iOPBillingId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iOPBillingId.ToString();
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
            sException = "OPBilling AddOPBilling |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "OPBilling_A_01")
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
    public string UpdateOPBilling(VHMS.Entity.Billing.OPBillingMaster Objdata)
    {
        string sOPBillingId = string.Empty;
        string sException = string.Empty;
        bool bOPBilling = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;
                bOPBilling = VHMS.DataAccess.Billing.OPBillingMaster.UpdateOPBilling(Objdata);
                if (bOPBilling == true)
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
            sException = "OPBilling UpdateOPBilling |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "OPBilling_U_01")
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
    public string DeleteOPBilling(int ID, string Reason)
    {
        string sOPBillingId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bOPBilling = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bOPBilling = VHMS.DataAccess.Billing.OPBillingMaster.DeleteOPBilling(ID, Reason, iUserId);
                if (bOPBilling == true)
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
            sException = "OPBilling DeleteOPBilling |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "OPBilling_R_01" || ex.Message.ToString() == "OPBilling_D_01")
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
    public string GetOPBillingReport(VHMS.Entity.Billing.OPBillingFilter oJobCardFilter)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            // int BranchID = 0;
            if (ValidateSession())
            {
                //Collection<VHMS.Entity.patient> ObjList = new Collection<VHMS.Entity.patient>();
                //ObjList = VHMS.DataAccess.patient.GetpatientDetails(oJobCardFilter);
                Collection<VHMS.Entity.Billing.OPBillingMaster> ObjList = new Collection<VHMS.Entity.Billing.OPBillingMaster>();
                ObjList = VHMS.DataAccess.Billing.OPBillingMaster.GetOPBillingReport(oJobCardFilter);
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
            sException = "JobCard GetJobCard |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetOPBillingSummary()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                DataSet dsResult = new DataSet();
                dsResult = VHMS.DataAccess.Billing.OPBillingMaster.GetOPBillingSummary();

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = GetDtJson(dsResult.Tables[0]);
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
            sException = "OPBilling GetOPBillingSummary |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }

    #endregion

    #region "Sales"
    public string GetSales(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.Sales> ObjList = new Collection<VHMS.Entity.Billing.Sales>();
                ObjList = VHMS.DataAccess.Billing.Sales.GetSales(PublisherID);
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
            sException = "Sales GetSales |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTopSales(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iBranchID = 0;
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.Sales> ObjList = new Collection<VHMS.Entity.Billing.Sales>();
                ObjList = VHMS.DataAccess.Billing.Sales.GetTopSales(PublisherID, iBranchID);
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
            sException = "Sales GetSales |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSalesID(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.Sales> ObjList = new Collection<VHMS.Entity.Billing.Sales>();
                ObjList = VHMS.DataAccess.Billing.Sales.GetSalesID(PublisherID);
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
            sException = "Sales GetSales |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchSales(string ID = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iUserId = 0;
        int iBranchID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {
                if (iUserId <= 1)
                    iBranchID = 0;
                Collection<VHMS.Entity.Billing.Sales> ObjList = new Collection<VHMS.Entity.Billing.Sales>();
                ObjList = VHMS.DataAccess.Billing.Sales.SearchSales(ID, iBranchID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Sales GetSales |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSalesByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.Sales objSales = new VHMS.Entity.Billing.Sales();
                objSales = VHMS.DataAccess.Billing.Sales.GetSalesByID(ID);
                if (objSales.SalesID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objSales);
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
            sException = "Sales GetSalesByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSalesByInvoice(string InvoiceNo)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.Sales objSales = new VHMS.Entity.Billing.Sales();
                objSales = VHMS.DataAccess.Billing.Sales.GetSalesByInvoice(InvoiceNo);
                if (objSales.SalesID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objSales);
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
            sException = "Sales GetSalesByInvoice |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddSales(VHMS.Entity.Billing.Sales Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iSalesId = 0;
        int iUserId = 0;
        int iBranchID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {
                VHMS.Entity.Branch objBranch = new VHMS.Entity.Branch();
                objBranch.BranchID = iBranchID;
                Objdata.Branch = objBranch;

                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                iSalesId = VHMS.DataAccess.Billing.Sales.AddSales(Objdata);
                if (iSalesId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iSalesId.ToString();
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
            sException = "Sales AddSales |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Sales_A_01")
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
    public string UpdateSales(VHMS.Entity.Billing.Sales Objdata)
    {
        string sSalesId = string.Empty;
        string sException = string.Empty;
        bool bSales = false;
        int iBranchID = 0;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {
                VHMS.Entity.Branch objBranch = new VHMS.Entity.Branch();
                objBranch.BranchID = iBranchID;
                Objdata.Branch = objBranch;

                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;
                bSales = VHMS.DataAccess.Billing.Sales.UpdateSales(Objdata);
                if (bSales == true)
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
            sException = "Sales UpdateSales |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Sales_U_01")
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
    public string DeleteSales(int ID, string Reason)
    {
        string sSalesId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bSales = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bSales = VHMS.DataAccess.Billing.Sales.DeleteSales(ID, Reason, iUserId);
                if (bSales == true)
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
            sException = "Sales DeleteSales |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Sales_R_01" || ex.Message.ToString() == "Sales_D_01")
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
    public string GetSalesReport(VHMS.Entity.Billing.SalesFilter oJobCardFilter)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            //int BranchID = 0;
            if (ValidateSession())
            {
                //Collection<VHMS.Entity.patient> ObjList = new Collection<VHMS.Entity.patient>();
                //ObjList = VHMS.DataAccess.patient.GetpatientDetails(oJobCardFilter);
                Collection<VHMS.Entity.Billing.Sales> ObjList = new Collection<VHMS.Entity.Billing.Sales>();
                //ObjList = VHMS.DataAccess.Billing.Sales.GetSalesReport(oJobCardFilter);
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
            sException = "JobCard GetJobCard |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSalesSummary()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                DataSet dsResult = new DataSet();
                dsResult = VHMS.DataAccess.Billing.Sales.GetSalesSummary();

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = GetDtJson(dsResult.Tables[0]);
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
            sException = "Sales GetSalesSummary |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }

    #endregion

    #region "Register"
    public string GetRegister(int RegisterID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int RegionID = 0;
        int ZoneID = 0;
        try
        {
            if (ValidateSession())
            {

                int RoleID = Convert.ToInt32(HttpContext.Current.Session["RoleID"]);
                if (RoleID == 7)
                    RegionID = Convert.ToInt32(HttpContext.Current.Session["RegionID"]);
                else if (RoleID == 9)
                    ZoneID = Convert.ToInt32(HttpContext.Current.Session["ZoneID"]);
                Collection<VHMS.Entity.Billing.Register> ObjList = new Collection<VHMS.Entity.Billing.Register>();
                ObjList = VHMS.DataAccess.Billing.Register.GetRegister(RegisterID, RegionID, ZoneID);
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
            sException = "VHMSService.Billing.Register GetRegister |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetRegisterNotification()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int RegionID = 0;
        int ZoneID = 0;
        try
        {
            if (ValidateSession())
            {

                int RoleID = Convert.ToInt32(HttpContext.Current.Session["RoleID"]);
                if (RoleID == 7)
                    RegionID = Convert.ToInt32(HttpContext.Current.Session["RegionID"]);
                else if (RoleID == 9)
                    ZoneID = Convert.ToInt32(HttpContext.Current.Session["ZoneID"]);
                Collection<VHMS.Entity.Billing.Register> ObjList = new Collection<VHMS.Entity.Billing.Register>();
                ObjList = VHMS.DataAccess.Billing.Register.GetRegisterNotification();
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
            sException = "VHMSService.Billing.Register GetRegister |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetRegisterByStatus(int ID = 0, string Status = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.Register> ObjList = new Collection<VHMS.Entity.Billing.Register>();
                ObjList = VHMS.DataAccess.Billing.Register.GetRegisterByStatus(ID, Status);
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
            sException = "VHMSService.Billing.Register GetRegister |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetClosedRegisterByStatus(int ID = 0, string Status = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.Register> ObjList = new Collection<VHMS.Entity.Billing.Register>();
                ObjList = VHMS.DataAccess.Billing.Register.GetClosedRegisterByStatus(ID, Status);
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
            sException = "VHMSService.Billing.Register GetRegister |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetRegisterByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.Register objRegister = new VHMS.Entity.Billing.Register();
                objRegister = VHMS.DataAccess.Billing.Register.GetRegisterByID(ID);
                if (objRegister.RegisterID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objRegister);
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
            sException = "VHMSService.Billing.Register GetRegisterByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchRegister(string ID = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iUserId = 0;
        int iBranchID = 0;
        int RegionID = 0;
        int ZoneID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {
                if (iUserId <= 1)
                    iBranchID = 0;
                int RoleID = Convert.ToInt32(HttpContext.Current.Session["RoleID"]);
                if (RoleID == 7)
                    RegionID = Convert.ToInt32(HttpContext.Current.Session["RegionID"]);
                else if (RoleID == 9)
                    ZoneID = Convert.ToInt32(HttpContext.Current.Session["ZoneID"]);
                Collection<VHMS.Entity.Billing.Register> ObjList = new Collection<VHMS.Entity.Billing.Register>();
                ObjList = VHMS.DataAccess.Billing.Register.SearchRegister(ID, iBranchID, RegionID, ZoneID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "VHMSService.Billing.Register GetSearchRegister |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchChitClosed(string ID = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iUserId = 0;
        int iBranchID = 0;
        String iStatus = "";
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {
                if (iUserId <= 1)
                    iBranchID = 0;
                iStatus = "";
                Collection<VHMS.Entity.Billing.Register> ObjList = new Collection<VHMS.Entity.Billing.Register>();
                ObjList = VHMS.DataAccess.Billing.Register.SearchChitClosed(ID, iBranchID, iStatus);

                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "SearchChitClosed GetSearchRecord |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetRegisterByNo(string ID, string IsActive)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int RegionID = 0;
        int ZoneID = 0;
        try
        {
            if (ValidateSession())
            {

                int RoleID = Convert.ToInt32(HttpContext.Current.Session["RoleID"]);
                if (RoleID == 7)
                    RegionID = Convert.ToInt32(HttpContext.Current.Session["RegionID"]);
                else if (RoleID == 9)
                    ZoneID = Convert.ToInt32(HttpContext.Current.Session["ZoneID"]);
                VHMS.Entity.Billing.Register objRegister = new VHMS.Entity.Billing.Register();
                objRegister = VHMS.DataAccess.Billing.Register.GetRegisterByNo(ID, IsActive, RegionID, ZoneID);
                if (objRegister.RegisterID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objRegister);
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
            sException = "VHMSService.Billing.Register GetRegisterByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddRegister(VHMS.Entity.Billing.Register Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iRegisterId = 0;
        int iUserID = 0;
        int iBranchID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {

                VHMS.Entity.Branch objBranch = new VHMS.Entity.Branch();
                objBranch.BranchID = iBranchID;
                Objdata.Branch = objBranch;

                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iRegisterId = VHMS.DataAccess.Billing.Register.AddRegister(Objdata);
                if (iRegisterId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iRegisterId.ToString();
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
            sException = "VHMSService.Billing.Register AddRegister |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Register_A_01")
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
    public string UpdateRegister(VHMS.Entity.Billing.Register Objdata)
    {
        string sRegisterId = string.Empty;
        string sException = string.Empty;
        bool bRegister = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserIDs = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserIDs))
            {
                //  VHMS.Entity.Branch objBranch = new VHMS.Entity.Branch();
                //  VHMS.Entity.Branch objBranch1 = new VHMS.Entity.Branch();
                // objBranch.BranchID = BranchID;
                // Objdata.Branch = objBranch;

                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserIDs;
                Objdata.ModifiedBy = objUser;
                bRegister = VHMS.DataAccess.Billing.Register.UpdateRegister(Objdata);
                if (bRegister == true)
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
            sException = "VHMSService.Billing.Register UpdateRegister |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Register_U_01")
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
    public string UpdateCancelledRegister(VHMS.Entity.Billing.Register Objdata)
    {
        string sRegisterId = string.Empty;
        string sException = string.Empty;
        bool bRegister = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {


                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserID;
                Objdata.CancelledBy = objUser;
                bRegister = VHMS.DataAccess.Billing.Register.UpdateCancelledRegister(Objdata);
                if (bRegister == true)
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
            sException = "VHMSService.Billing.Register UpdateRegister |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Register_U_01")
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
    public string DeleteRegister(int ID)
    {
        string sRegisterId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bRegister = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bRegister = VHMS.DataAccess.Billing.Register.DeleteRegister(ID, UserID);
                if (bRegister == true)
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
            sException = "VHMSService.Billing.Register DeleteRegister |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Register_R_01" || ex.Message.ToString() == "Register_D_01")
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
    public string GetCancelledRegister(int RegisterID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int RegionID = 0;
        int ZoneID = 0;
        try
        {
            if (ValidateSession())
            {
                int RoleID = Convert.ToInt32(HttpContext.Current.Session["RoleID"]);
                if (RoleID == 7)
                    RegionID = Convert.ToInt32(HttpContext.Current.Session["RegionID"]);
                else if (RoleID == 9)
                    ZoneID = Convert.ToInt32(HttpContext.Current.Session["ZoneID"]);
                Collection<VHMS.Entity.Billing.Register> ObjList = new Collection<VHMS.Entity.Billing.Register>();
                ObjList = VHMS.DataAccess.Billing.Register.GetCancelledRegister(RegisterID, RegionID, ZoneID);
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
            sException = "VHMSService.Billing.Register GetRegister |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    #endregion

    #region "Renewal"
    public string GetRenewal()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int RegionID = 0;
        int ZoneID = 0;
        try
        {
            if (ValidateSession())
            {
                int RoleID = Convert.ToInt32(HttpContext.Current.Session["RoleID"]);
                if (RoleID == 7)
                    RegionID = Convert.ToInt32(HttpContext.Current.Session["RegionID"]);
                else if (RoleID == 9)
                    ZoneID = Convert.ToInt32(HttpContext.Current.Session["ZoneID"]);
                Collection<VHMS.Entity.Billing.Renewal> ObjList = new Collection<VHMS.Entity.Billing.Renewal>();
                ObjList = VHMS.DataAccess.Billing.Renewal.GetRenewal(RegionID, ZoneID);
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
            sException = "VHMSService.Billing.Renewal GetRenewal |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetRenewalByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.Renewal objRenewal = new VHMS.Entity.Billing.Renewal();
                objRenewal = VHMS.DataAccess.Billing.Renewal.GetRenewalByID(ID);
                if (objRenewal.RenewalID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objRenewal);
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
            sException = "VHMSService.Billing.Renewal GetRenewalByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchRenewal(string ID = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iUserId = 0;
        int iBranchID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {
                if (iUserId <= 1)
                    iBranchID = 0;
                Collection<VHMS.Entity.Billing.Renewal> ObjList = new Collection<VHMS.Entity.Billing.Renewal>();
                ObjList = VHMS.DataAccess.Billing.Renewal.SearchRenewal(ID, iBranchID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "VHMSService.Billing.Renewal GetSearchRenewal |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddRenewal(VHMS.Entity.Billing.Renewal Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iRenewalId = 0;
        int iUserID = 0;
        int iBranchID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {

                VHMS.Entity.Branch objBranch = new VHMS.Entity.Branch();
                objBranch.BranchID = iBranchID;
                Objdata.Branch = objBranch;

                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iRenewalId = VHMS.DataAccess.Billing.Renewal.AddRenewal(Objdata);
                if (iRenewalId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iRenewalId.ToString();
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
            sException = "VHMSService.Billing.Renewal AddRenewal |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Renewal_A_01")
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
    public string UpdateRenewal(VHMS.Entity.Billing.Renewal Objdata)
    {
        string sRenewalId = string.Empty;
        string sException = string.Empty;
        bool bRenewal = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iBranchID = 0;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {
                VHMS.Entity.Branch objBranch = new VHMS.Entity.Branch();
                objBranch.BranchID = iBranchID;
                Objdata.Branch = objBranch;

                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserID;
                Objdata.ModifiedBy = objUser;
                bRenewal = VHMS.DataAccess.Billing.Renewal.UpdateRenewal(Objdata);
                if (bRenewal == true)
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
            sException = "VHMSService.Billing.Renewal UpdateRenewal |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Renewal_U_01")
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
    public string DeleteRenewal(int ID)
    {
        string sRenewalId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bRenewal = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bRenewal = VHMS.DataAccess.Billing.Renewal.DeleteRenewal(ID, UserID);
                if (bRenewal == true)
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
            sException = "VHMSService.Billing.Renewal DeleteRenewal |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Renewal_R_01" || ex.Message.ToString() == "Renewal_D_01")
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

    #region "Quotation"
    public string GetQuotation(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.Quotation> ObjList = new Collection<VHMS.Entity.Billing.Quotation>();
                ObjList = VHMS.DataAccess.Billing.Quotation.GetQuotation(PublisherID);
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
            sException = "Quotation GetQuotation |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTopQuotation(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();

        int iUserId = 0;
        int iBranchID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {
                if (iUserId <= 1)
                    iBranchID = 0;
                Collection<VHMS.Entity.Billing.Quotation> ObjList = new Collection<VHMS.Entity.Billing.Quotation>();
                ObjList = VHMS.DataAccess.Billing.Quotation.GetTopQuotation(PublisherID, iBranchID);
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
            sException = "Quotation GetQuotation |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetQuotationID(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.Quotation> ObjList = new Collection<VHMS.Entity.Billing.Quotation>();
                ObjList = VHMS.DataAccess.Billing.Quotation.GetQuotationID(PublisherID);
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
            sException = "Quotation GetQuotation |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchQuotation(string ID = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iUserId = 0;
        int iBranchID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {
                if (iUserId <= 1)
                    iBranchID = 0;
                Collection<VHMS.Entity.Billing.Quotation> ObjList = new Collection<VHMS.Entity.Billing.Quotation>();
                ObjList = VHMS.DataAccess.Billing.Quotation.SearchQuotation(ID, iBranchID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Quotation GetQuotation |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetQuotationByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.Quotation objQuotation = new VHMS.Entity.Billing.Quotation();
                objQuotation = VHMS.DataAccess.Billing.Quotation.GetQuotationByID(ID);
                if (objQuotation.QuotationID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objQuotation);
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
            sException = "Quotation GetQuotationByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetQuotationByInvoice(string InvoiceNo)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.Quotation objQuotation = new VHMS.Entity.Billing.Quotation();
                // objQuotation = VHMS.DataAccess.Billing.Quotation.GetQuotationByInvoice(InvoiceNo);
                if (objQuotation.QuotationID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objQuotation);
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
            sException = "Quotation GetQuotationByInvoice |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddQuotation(VHMS.Entity.Billing.Quotation Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iQuotationId = 0;
        int iUserId = 0;
        int iBranchID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {
                VHMS.Entity.Branch objBranch = new VHMS.Entity.Branch();
                objBranch.BranchID = iBranchID;
                Objdata.Branch = objBranch;

                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                iQuotationId = VHMS.DataAccess.Billing.Quotation.AddQuotation(Objdata);
                if (iQuotationId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iQuotationId.ToString();
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
            sException = "Quotation AddQuotation |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Quotation_A_01")
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
    public string UpdateQuotation(VHMS.Entity.Billing.Quotation Objdata)
    {
        string sQuotationId = string.Empty;
        string sException = string.Empty;
        bool bQuotation = false;
        int iBranchID = 0;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {
                VHMS.Entity.Branch objBranch = new VHMS.Entity.Branch();
                objBranch.BranchID = iBranchID;
                Objdata.Branch = objBranch;

                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;
                bQuotation = VHMS.DataAccess.Billing.Quotation.UpdateQuotation(Objdata);
                if (bQuotation == true)
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
            sException = "Quotation UpdateQuotation |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Quotation_U_01")
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
    public string DeleteQuotation(int ID, string Reason)
    {
        string sQuotationId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bQuotation = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bQuotation = VHMS.DataAccess.Billing.Quotation.DeleteQuotation(ID, Reason, iUserId);
                if (bQuotation == true)
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
            sException = "Quotation DeleteQuotation |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Quotation_R_01" || ex.Message.ToString() == "Quotation_D_01")
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
    public string GetQuotationReport(VHMS.Entity.Billing.QuotationFilter oJobCardFilter)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            // int BranchID = 0;
            if (ValidateSession())
            {
                //Collection<VHMS.Entity.patient> ObjList = new Collection<VHMS.Entity.patient>();
                //ObjList = VHMS.DataAccess.patient.GetpatientDetails(oJobCardFilter);
                Collection<VHMS.Entity.Billing.Quotation> ObjList = new Collection<VHMS.Entity.Billing.Quotation>();
                //ObjList = VHMS.DataAccess.Billing.Quotation.GetQuotationReport(oJobCardFilter);
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
            sException = "JobCard GetJobCard |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetQuotationSummary()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                DataSet dsResult = new DataSet();
                dsResult = VHMS.DataAccess.Billing.Quotation.GetQuotationSummary();

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = GetDtJson(dsResult.Tables[0]);
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
            sException = "Quotation GetQuotationSummary |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }

    #endregion

    #region "Exchange"
    public string GetExchange(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.Exchange> ObjList = new Collection<VHMS.Entity.Billing.Exchange>();
                ObjList = VHMS.DataAccess.Billing.Exchange.GetExchange(PublisherID);
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
            sException = "Exchange GetExchange |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTopExchange(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iUserId = 0;
        int iBranchID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {
                if (iUserId <= 1)
                    iBranchID = 0;
                Collection<VHMS.Entity.Billing.Exchange> ObjList = new Collection<VHMS.Entity.Billing.Exchange>();
                ObjList = VHMS.DataAccess.Billing.Exchange.GetTopExchange(PublisherID, iBranchID);
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
            sException = "Exchange GetExchange |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetExchangeID(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.Exchange> ObjList = new Collection<VHMS.Entity.Billing.Exchange>();
                ObjList = VHMS.DataAccess.Billing.Exchange.GetExchangeID(PublisherID);
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
            sException = "Exchange GetExchange |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchExchange(string ID = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();

        int iUserId = 0;
        int iBranchID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {
                if (iUserId <= 1)
                    iBranchID = 0;
                Collection<VHMS.Entity.Billing.Exchange> ObjList = new Collection<VHMS.Entity.Billing.Exchange>();
                ObjList = VHMS.DataAccess.Billing.Exchange.SearchExchange(ID, iBranchID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Exchange GetExchange |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetExchangeByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.Exchange objExchange = new VHMS.Entity.Billing.Exchange();
                objExchange = VHMS.DataAccess.Billing.Exchange.GetExchangeByID(ID);
                if (objExchange.ExchangeID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objExchange);
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
            sException = "Exchange GetExchangeByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddExchange(VHMS.Entity.Billing.Exchange Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iExchangeId = 0;
        int iUserId = 0;
        int iBranchID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {
                VHMS.Entity.Branch objBranch = new VHMS.Entity.Branch();
                objBranch.BranchID = iBranchID;
                Objdata.Branch = objBranch;

                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                iExchangeId = VHMS.DataAccess.Billing.Exchange.AddExchange(Objdata);
                if (iExchangeId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iExchangeId.ToString();
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
            sException = "Exchange AddExchange |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Exchange_A_01")
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
    public string UpdateExchange(VHMS.Entity.Billing.Exchange Objdata)
    {
        string sExchangeId = string.Empty;
        string sException = string.Empty;
        bool bExchange = false;
        int iBranchID = 0;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {
                VHMS.Entity.Branch objBranch = new VHMS.Entity.Branch();
                objBranch.BranchID = iBranchID;
                Objdata.Branch = objBranch;

                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;
                bExchange = VHMS.DataAccess.Billing.Exchange.UpdateExchange(Objdata);
                if (bExchange == true)
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
            sException = "Exchange UpdateExchange |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Exchange_U_01")
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
    public string DeleteExchange(int ID, string Reason)
    {
        string sExchangeId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bExchange = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bExchange = VHMS.DataAccess.Billing.Exchange.DeleteExchange(ID, Reason, iUserId);
                if (bExchange == true)
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
            sException = "Exchange DeleteExchange |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Exchange_R_01" || ex.Message.ToString() == "Exchange_D_01")
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
    public string GetExchangeReport(VHMS.Entity.Billing.ExchangeFilter oJobCardFilter)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            // int BranchID = 0;
            if (ValidateSession())
            {
                //Collection<VHMS.Entity.patient> ObjList = new Collection<VHMS.Entity.patient>();
                //ObjList = VHMS.DataAccess.patient.GetpatientDetails(oJobCardFilter);
                Collection<VHMS.Entity.Billing.Exchange> ObjList = new Collection<VHMS.Entity.Billing.Exchange>();
                //ObjList = VHMS.DataAccess.Billing.Exchange.GetExchangeReport(oJobCardFilter);
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
            sException = "JobCard GetJobCard |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetExchangeSummary()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                DataSet dsResult = new DataSet();
                //    dsResult = VHMS.DataAccess.Billing.Exchange.GetExchangeSummary();

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = GetDtJson(dsResult.Tables[0]);
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
            sException = "Exchange GetExchangeSummary |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }

    #endregion

    #region "SalesReturn"
    public string GetSalesReturn(int PublisherID = 0, int IsRetail = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.SalesReturn> ObjList = new Collection<VHMS.Entity.Billing.SalesReturn>();
                ObjList = VHMS.DataAccess.Billing.SalesReturn.GetSalesReturn(PublisherID, FK_FinancialYearID, IsRetail);
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
            sException = "SalesReturn GetSalesReturn |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTopSalesReturn(int PublisherID = 0, int IsRetail = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iUserId = 0;
        int iBranchID = 0;
        try
        {
            int FK_FinancialYearID = 0;

            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                if (iUserId <= 1)
                    iBranchID = 0;
                Collection<VHMS.Entity.Billing.SalesReturn> ObjList = new Collection<VHMS.Entity.Billing.SalesReturn>();
                ObjList = VHMS.DataAccess.Billing.SalesReturn.GetTopSalesReturn(PublisherID, iBranchID, FK_FinancialYearID, IsRetail);
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
            sException = "SalesReturn GetSalesReturn |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSalesReturnID(int PublisherID = 0, int IsRetail = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.SalesReturn> ObjList = new Collection<VHMS.Entity.Billing.SalesReturn>();
                ObjList = VHMS.DataAccess.Billing.SalesReturn.GetSalesReturnID(PublisherID, FK_FinancialYearID, IsRetail);
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
            sException = "SalesReturn GetSalesReturn |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchSalesReturn(string ID = null, int IsRetail = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iUserId = 0;
        int iBranchID = 0;
        try
        {
            int FK_FinancialYearID = 0;

            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                if (iUserId <= 1)
                    iBranchID = 0;
                Collection<VHMS.Entity.Billing.SalesReturn> ObjList = new Collection<VHMS.Entity.Billing.SalesReturn>();
                ObjList = VHMS.DataAccess.Billing.SalesReturn.SearchSalesReturn(ID, iBranchID, FK_FinancialYearID, IsRetail);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "SalesReturn GetSalesReturn |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSalesReturnByID(int ID, int IsRetail = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.SalesReturn objSalesReturn = new VHMS.Entity.Billing.SalesReturn();
                objSalesReturn = VHMS.DataAccess.Billing.SalesReturn.GetSalesReturnByID(ID, FK_FinancialYearID, IsRetail);
                if (objSalesReturn.SalesReturnID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objSalesReturn);
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
            sException = "SalesReturn GetSalesReturnByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSalesReturnByInvoice(string InvoiceNo)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.SalesReturn objSalesReturn = new VHMS.Entity.Billing.SalesReturn();
                //  objSalesReturn = VHMS.DataAccess.Billing.SalesReturn.GetSalesReturnByInvoice(InvoiceNo);
                if (objSalesReturn.SalesReturnID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objSalesReturn);
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
            sException = "SalesReturn GetSalesReturnByInvoice |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddSalesReturn(VHMS.Entity.Billing.SalesReturn Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iSalesReturnId = 0;
        int iUserId = 0;
        int iBranchID = 0;
        int FK_FinancialYearID = 0;
        try
        {

            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                iSalesReturnId = VHMS.DataAccess.Billing.SalesReturn.AddSalesReturn(Objdata);
                if (iSalesReturnId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iSalesReturnId.ToString();
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
            sException = "SalesReturn AddSalesReturn |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "SalesReturn_A_01")
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
    public string UpdateSalesReturn(VHMS.Entity.Billing.SalesReturn Objdata)
    {
        string sSalesReturnId = string.Empty;
        string sException = string.Empty;
        bool bSalesReturn = false;
        int iBranchID = 0;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;


                bSalesReturn = VHMS.DataAccess.Billing.SalesReturn.UpdateSalesReturn(Objdata);
                if (bSalesReturn == true)
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
            sException = "SalesReturn UpdateSalesReturn |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "SalesReturn_U_01")
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
    public string DeleteSalesReturn(int ID)
    {
        string sSalesReturnId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bSalesReturn = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bSalesReturn = VHMS.DataAccess.Billing.SalesReturn.DeleteSalesReturn(ID);
                if (bSalesReturn == true)
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
            sException = "SalesReturn DeleteSalesReturn |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "SalesReturn_R_01" || ex.Message.ToString() == "SalesReturn_D_01")
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
    public string GetSalesReturnReport(VHMS.Entity.Billing.SalesReturnFilter oJobCardFilter)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            //int BranchID = 0;
            if (ValidateSession())
            {
                //Collection<VHMS.Entity.patient> ObjList = new Collection<VHMS.Entity.patient>();
                //ObjList = VHMS.DataAccess.patient.GetpatientDetails(oJobCardFilter);
                Collection<VHMS.Entity.Billing.SalesReturn> ObjList = new Collection<VHMS.Entity.Billing.SalesReturn>();
                //ObjList = VHMS.DataAccess.Billing.SalesReturn.GetSalesReturnReport(oJobCardFilter);
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
            sException = "JobCard GetJobCard |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSalesReturnSummary()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                DataSet dsResult = new DataSet();
                dsResult = VHMS.DataAccess.Billing.SalesReturn.GetSalesReturnSummary();

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = GetDtJson(dsResult.Tables[0]);
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
            sException = "SalesReturn GetSalesReturnSummary |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }

    public string GetSalesReturnJsonHSNFormat(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.SalesReturnJSON> objAddCollection = new Collection<VHMS.Entity.Billing.SalesReturnJSON>();
                VHMS.Entity.Billing.SalesReturnJSON objSalesEntry = new VHMS.Entity.Billing.SalesReturnJSON();
                objSalesEntry = VHMS.DataAccess.Billing.SalesReturn.GetSalesReturnJsonHSNFormat(ID);
                objAddCollection.Add(objSalesEntry);
                objResponse.Status = "Success";
                objResponse.Value = jsObject.Serialize(objAddCollection);

            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "SalesEntry GetSalesEntryJsonFormat |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }

    #endregion

    #region "TDSPayment"
    public string GetTDSPayment(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.TDSPayment> ObjList = new Collection<VHMS.Entity.Billing.TDSPayment>();
                ObjList = VHMS.DataAccess.Billing.TDSPayment.GetTDSPayment(PublisherID, FK_FinancialYearID);
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
            sException = "TDSPayment GetTDSPayment |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTopTDSPayment(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iUserId = 0;
        int iBranchID = 0;
        int FK_FinancialYearID = 0;
        try
        {

            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                if (iUserId <= 1)
                    iBranchID = 0;
                Collection<VHMS.Entity.Billing.TDSPayment> ObjList = new Collection<VHMS.Entity.Billing.TDSPayment>();
                ObjList = VHMS.DataAccess.Billing.TDSPayment.GetTopTDSPayment(PublisherID, iBranchID, FK_FinancialYearID);
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
            sException = "TDSPayment GetTDSPayment |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTDSPaymentID(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.TDSPayment> ObjList = new Collection<VHMS.Entity.Billing.TDSPayment>();
                ObjList = VHMS.DataAccess.Billing.TDSPayment.GetTDSPaymentID(PublisherID, FK_FinancialYearID);
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
            sException = "TDSPayment GetTDSPayment |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchTDSPayment(string ID = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iUserId = 0;
        int iBranchID = 0;
        int FK_FinancialYearID = 0;
        try
        {


            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                if (iUserId <= 1)
                    iBranchID = 0;
                Collection<VHMS.Entity.Billing.TDSPayment> ObjList = new Collection<VHMS.Entity.Billing.TDSPayment>();
                ObjList = VHMS.DataAccess.Billing.TDSPayment.SearchTDSPayment(ID, iBranchID, FK_FinancialYearID);
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
            sException = "TDSPayment GetTDSPayment |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTDSPaymentByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.TDSPayment objTDSPayment = new VHMS.Entity.Billing.TDSPayment();
                objTDSPayment = VHMS.DataAccess.Billing.TDSPayment.GetTDSPaymentByID(ID, FK_FinancialYearID);
                if (objTDSPayment.TDSPaymentID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objTDSPayment);
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
            sException = "TDSPayment GetTDSPaymentByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTDSPaymentByInvoice(string InvoiceNo)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.TDSPayment objTDSPayment = new VHMS.Entity.Billing.TDSPayment();
                //  objTDSPayment = VHMS.DataAccess.Billing.TDSPayment.GetTDSPaymentByInvoice(InvoiceNo);
                if (objTDSPayment.TDSPaymentID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objTDSPayment);
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
            sException = "TDSPayment GetTDSPaymentByInvoice |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddTDSPayment(VHMS.Entity.Billing.TDSPayment Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iTDSPaymentId = 0;
        int iUserId = 0;
        int iBranchID = 0;
        int FK_FinancialYearID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                iTDSPaymentId = VHMS.DataAccess.Billing.TDSPayment.AddTDSPayment(Objdata);
                if (iTDSPaymentId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iTDSPaymentId.ToString();
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
            sException = "TDSPayment AddTDSPayment |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "TDSPayment_A_01")
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
    public string UpdateTDSPayment(VHMS.Entity.Billing.TDSPayment Objdata)
    {
        string sTDSPaymentId = string.Empty;
        string sException = string.Empty;
        bool bTDSPayment = false;
        int iBranchID = 0;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                bTDSPayment = VHMS.DataAccess.Billing.TDSPayment.UpdateTDSPayment(Objdata);
                if (bTDSPayment == true)
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
            sException = "TDSPayment UpdateTDSPayment |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "TDSPayment_U_01")
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
    public string DeleteTDSPayment(int ID)
    {
        string sTDSPaymentId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bTDSPayment = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bTDSPayment = VHMS.DataAccess.Billing.TDSPayment.DeleteTDSPayment(ID);
                if (bTDSPayment == true)
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
            sException = "TDSPayment DeleteTDSPayment |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "TDSPayment_R_01" || ex.Message.ToString() == "TDSPayment_D_01")
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
    public string GetTDSPaymentSummary()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                DataSet dsResult = new DataSet();
                dsResult = VHMS.DataAccess.Billing.TDSPayment.GetTDSPaymentSummary();

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = GetDtJson(dsResult.Tables[0]);
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
            sException = "TDSPayment GetTDSPaymentSummary |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }

    #endregion

    #region "BranchMove"
    public string GetBranchMove(int BranchMoveID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.BranchMove> ObjList = new Collection<VHMS.Entity.Billing.BranchMove>();
                ObjList = VHMS.DataAccess.Billing.BranchMove.GetBranchMove(BranchMoveID);
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
            sException = "BranchMove GetBranchMove |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTopBranchMove(int BranchMoveID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iUserId = 0;
        int iBranchID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {
                if (iUserId <= 1)
                    iBranchID = 0;
                Collection<VHMS.Entity.Billing.BranchMove> ObjList = new Collection<VHMS.Entity.Billing.BranchMove>();
                ObjList = VHMS.DataAccess.Billing.BranchMove.GetTopBranchMove(BranchMoveID, iBranchID);
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
            sException = "BranchMove GetBranchMove |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetBranchMoveID(int BranchMoveID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.BranchMove> ObjList = new Collection<VHMS.Entity.Billing.BranchMove>();
                ObjList = VHMS.DataAccess.Billing.BranchMove.GetBranchMoveID(BranchMoveID);
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
            sException = "BranchMove GetBranchMove |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchBranchMove(string ID = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iUserId = 0;
        int iBranchID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {
                Collection<VHMS.Entity.Billing.BranchMove> ObjList = new Collection<VHMS.Entity.Billing.BranchMove>();
                ObjList = VHMS.DataAccess.Billing.BranchMove.SearchBranchMove(ID, iBranchID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "BranchMove GetBranchMove |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetBranchMoveByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.BranchMove objBranchMove = new VHMS.Entity.Billing.BranchMove();
                objBranchMove = VHMS.DataAccess.Billing.BranchMove.GetBranchMoveByID(ID);
                if (objBranchMove.BranchMoveID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objBranchMove);
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
            sException = "BranchMove GetBranchMoveByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetBranchMoveByInvoice(string InvoiceNo)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.BranchMove objBranchMove = new VHMS.Entity.Billing.BranchMove();
                //  objBranchMove = VHMS.DataAccess.Billing.BranchMove.GetBranchMoveByInvoice(InvoiceNo);
                if (objBranchMove.BranchMoveID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objBranchMove);
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
            sException = "BranchMove GetBranchMoveByInvoice |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddBranchMove(VHMS.Entity.Billing.BranchMove Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iBranchMoveId = 0;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                iBranchMoveId = VHMS.DataAccess.Billing.BranchMove.AddBranchMove(Objdata);
                if (iBranchMoveId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iBranchMoveId.ToString();
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
            sException = "BranchMove AddBranchMove |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "BranchMove_A_01")
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
    public string UpdateBranchMove(VHMS.Entity.Billing.BranchMove Objdata)
    {
        string sBranchMoveId = string.Empty;
        string sException = string.Empty;
        bool bBranchMove = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;
                bBranchMove = VHMS.DataAccess.Billing.BranchMove.UpdateBranchMove(Objdata);
                if (bBranchMove == true)
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
            sException = "BranchMove UpdateBranchMove |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "BranchMove_U_01")
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
    public string DeleteBranchMove(int ID)
    {
        string sBranchMoveId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bBranchMove = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bBranchMove = VHMS.DataAccess.Billing.BranchMove.DeleteBranchMove(ID);
                if (bBranchMove == true)
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
            sException = "BranchMove DeleteBranchMove |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "BranchMove_R_01" || ex.Message.ToString() == "BranchMove_D_01")
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
    public string UpdateMoveStatus(int ID, string Status)
    {
        string sBranchMoveId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bBranchMove = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bBranchMove = VHMS.DataAccess.Billing.BranchMove.UpdateMoveStatus(ID, Status, iUserId);
                if (bBranchMove == true)
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
            sException = "BranchMove UpdateMoveStatus |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "BranchMove_R_01" || ex.Message.ToString() == "BranchMove_D_01")
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
    public string GetBranchMoveReport(VHMS.Entity.Billing.BranchMoveFilter oJobCardFilter)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                //Collection<VHMS.Entity.patient> ObjList = new Collection<VHMS.Entity.patient>();
                //ObjList = VHMS.DataAccess.patient.GetpatientDetails(oJobCardFilter);
                Collection<VHMS.Entity.Billing.BranchMove> ObjList = new Collection<VHMS.Entity.Billing.BranchMove>();
                //ObjList = VHMS.DataAccess.Billing.BranchMove.GetBranchMoveReport(oJobCardFilter);
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
            sException = "JobCard GetJobCard |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetBranchMoveSummary()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                DataSet dsResult = new DataSet();
                dsResult = VHMS.DataAccess.Billing.BranchMove.GetBranchMoveSummary();

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = GetDtJson(dsResult.Tables[0]);
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
            sException = "BranchMove GetBranchMoveSummary |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }

    #endregion

    #region "StockCheck"
    public string GetStockCheck(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.StockCheck> ObjList = new Collection<VHMS.Entity.Billing.StockCheck>();
                ObjList = VHMS.DataAccess.Billing.StockCheck.GetStockCheck(PublisherID);
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
            sException = "StockCheck GetStockCheck |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetStockCheckID(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.StockCheck> ObjList = new Collection<VHMS.Entity.Billing.StockCheck>();
                ObjList = VHMS.DataAccess.Billing.StockCheck.GetStockCheckID(PublisherID);
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
            sException = "StockCheck GetStockCheck |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTopStockCheck(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iUserId = 0;
        int iBranchID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {
                if (iUserId <= 1)
                    iBranchID = 0;
                Collection<VHMS.Entity.Billing.StockCheck> ObjList = new Collection<VHMS.Entity.Billing.StockCheck>();
                ObjList = VHMS.DataAccess.Billing.StockCheck.GetTopStockCheck(PublisherID, iBranchID);
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
            sException = "StockCheck GetStockCheck |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchStockCheck(string ID = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iUserId = 0;
        int iBranchID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {
                if (iUserId <= 1)
                    iBranchID = 0;
                Collection<VHMS.Entity.Billing.StockCheck> ObjList = new Collection<VHMS.Entity.Billing.StockCheck>();
                ObjList = VHMS.DataAccess.Billing.StockCheck.SearchStockCheck(ID, iBranchID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "StockCheck GetStockCheck |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetStockCheckByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.StockCheck objStockCheck = new VHMS.Entity.Billing.StockCheck();
                objStockCheck = VHMS.DataAccess.Billing.StockCheck.GetStockCheckByID(ID);
                if (objStockCheck.StockCheckID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objStockCheck);
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
            sException = "StockCheck GetStockCheckByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetStockCheckByInvoice(string InvoiceNo)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.StockCheck objStockCheck = new VHMS.Entity.Billing.StockCheck();
                objStockCheck = VHMS.DataAccess.Billing.StockCheck.GetStockCheckByInvoice(InvoiceNo);
                if (objStockCheck.StockCheckID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objStockCheck);
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
            sException = "StockCheck GetStockCheckByInvoice |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddStockCheck(VHMS.Entity.Billing.StockCheck Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iStockCheckId = 0;
        int iUserId = 0;
        int iBranchID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {
                // VHMS.Entity.Branch objBranch = new VHMS.Entity.Branch();
                // objBranch.BranchID = iBranchID;
                // Objdata.Branch = objBranch;

                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                iStockCheckId = VHMS.DataAccess.Billing.StockCheck.AddStockCheck(Objdata);
                if (iStockCheckId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iStockCheckId.ToString();
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
            sException = "StockCheck AddStockCheck |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "StockCheck_A_01")
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
    public string UpdateStockCheck(VHMS.Entity.Billing.StockCheck Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iStockCheckId = 0;
        int iUserId = 0;
        int iBranchID = 0;
        bool bStockCheck = false;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID))
            {
                // VHMS.Entity.Branch objBranch = new VHMS.Entity.Branch();
                // objBranch.BranchID = iBranchID;
                // Objdata.Branch = objBranch;

                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.Employee.UserID = iUserId;
                Objdata.ModifiedBy = objUser;

                bStockCheck = VHMS.DataAccess.Billing.StockCheck.UpdateStockCheck(Objdata);
                if (bStockCheck == true)
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
            sException = "StockCheck UpdateStockCheck |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "StockCheck_U_01")
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
    public string DeleteStockCheck(int ID)
    {
        string sStockCheckId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bStockCheck = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bStockCheck = VHMS.DataAccess.Billing.StockCheck.DeleteStockCheck(ID, iUserId);
                if (bStockCheck == true)
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
            sException = "StockCheck DeleteStockCheck |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "StockCheck_R_01" || ex.Message.ToString() == "StockCheck_D_01")
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
    public string GetStockCheckSummary()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                DataSet dsResult = new DataSet();
                dsResult = VHMS.DataAccess.Billing.StockCheck.GetStockCheckSummary();

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = GetDtJson(dsResult.Tables[0]);
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
            sException = "StockCheck GetStockCheckSummary |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }

    #endregion

    #region "INWARD"
    public string GetInward(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Inward> ObjList = new Collection<VHMS.Entity.Billing.Inward>();
                ObjList = VHMS.DataAccess.Billing.Inward.GetInward(PublisherID, FK_FinancialYearID);
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
            sException = "Inward GetInward |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }

    //public string GetInwardID(int PublisherID = 0)
    //{
    //    string sException = string.Empty;
    //    string sFileNames = string.Empty;
    //    JavaScriptSerializer jsObject = new JavaScriptSerializer();
    //    VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
    //    try
    //    {
    //        if (ValidateSession())
    //        {
    //            Collection<VHMS.Entity.Billing.Inward> ObjList = new Collection<VHMS.Entity.Billing.Inward>();
    //            ObjList = VHMS.DataAccess.Billing.Inward.GetInwardID(PublisherID);
    //            objResponse.Status = "Success";
    //            objResponse.Value = ObjList.Count > 0 ? jsObject.Serialize(ObjList) : "NoRecord";
    //        }
    //        else
    //        {
    //            objResponse.Status = "Error";
    //            objResponse.Value = "0";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        sException = "Inward GetInward |" + ex.Message.ToString();
    //        Log.Write(sException);
    //        objResponse.Status = "Error";
    //        objResponse.Value = "Error";
    //    }
    //    return jsObject.Serialize(objResponse);
    //}
        public string SearchInward(string ID = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Inward> ObjList = new Collection<VHMS.Entity.Billing.Inward>();
                ObjList = VHMS.DataAccess.Billing.Inward.SearchInward(ID, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Inward GetInward |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetInwardByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.Inward objInward = new VHMS.Entity.Billing.Inward();
                objInward = VHMS.DataAccess.Billing.Inward.GetInwardByID(ID, FK_FinancialYearID);
                if (objInward.InwardID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objInward);
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
            sException = "Inward GetInwardByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddInward(VHMS.Entity.Billing.Inward Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iInwardId = 0;
        int iUserId = 0;
        try
        {
            int FK_FinancialYearID = 0;

            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                iInwardId = VHMS.DataAccess.Billing.Inward.AddInward(Objdata);
                if (iInwardId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iInwardId.ToString();
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
            sException = "Inward AddInward |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Inward_A_01")
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
    public string UpdateInward(VHMS.Entity.Billing.Inward Objdata)
    {
        string sInwardId = string.Empty;
        string sException = string.Empty;
        bool bInward = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            int FK_FinancialYearID = 0;

            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                bInward = VHMS.DataAccess.Billing.Inward.UpdateInward(Objdata);
                if (bInward == true)
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
            sException = "Inward UpdateInward |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Inward_U_01")
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
    public string DeleteInward(int ID)
    {
        string sInwardId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bInward = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bInward = VHMS.DataAccess.Billing.Inward.DeleteInward(ID, iUserId);
                if (bInward == true)
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
            sException = "Inward DeleteInward |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Inward_R_01" || ex.Message.ToString() == "Inward_D_01")
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

    #region "PurchaseOrder"
    public string GetPurchaseOrder(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.PurchaseOrder> ObjList = new Collection<VHMS.Entity.Billing.PurchaseOrder>();
                ObjList = VHMS.DataAccess.Billing.PurchaseOrder.GetPurchaseOrder(PublisherID, FK_FinancialYearID);
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
            sException = "PurchaseOrder GetPurchaseOrder |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }

    //public string GetPurchaseOrderID(int PublisherID = 0)
    //{
    //    string sException = string.Empty;
    //    string sFileNames = string.Empty;
    //    JavaScriptSerializer jsObject = new JavaScriptSerializer();
    //    VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
    //    try
    //    {
    //        if (ValidateSession())
    //        {
    //            Collection<VHMS.Entity.Billing.PurchaseOrder> ObjList = new Collection<VHMS.Entity.Billing.PurchaseOrder>();
    //            ObjList = VHMS.DataAccess.Billing.PurchaseOrder.GetPurchaseOrderID(PublisherID);
    //            objResponse.Status = "Success";
    //            objResponse.Value = ObjList.Count > 0 ? jsObject.Serialize(ObjList) : "NoRecord";
    //        }
    //        else
    //        {
    //            objResponse.Status = "Error";
    //            objResponse.Value = "0";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        sException = "PurchaseOrder GetPurchaseOrder |" + ex.Message.ToString();
    //        Log.Write(sException);
    //        objResponse.Status = "Error";
    //        objResponse.Value = "Error";
    //    }
    //    return jsObject.Serialize(objResponse);
    //}

    public string SearchPurchaseOrder(string ID = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.PurchaseOrder> ObjList = new Collection<VHMS.Entity.Billing.PurchaseOrder>();
                ObjList = VHMS.DataAccess.Billing.PurchaseOrder.SearchPurchaseOrder(ID, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "PurchaseOrder GetPurchaseOrder |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetPurchaseOrderByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.PurchaseOrder objPurchaseOrder = new VHMS.Entity.Billing.PurchaseOrder();
                objPurchaseOrder = VHMS.DataAccess.Billing.PurchaseOrder.GetPurchaseOrderByID(ID, FK_FinancialYearID);
                if (objPurchaseOrder.PurchaseOrderID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objPurchaseOrder);
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
            sException = "PurchaseOrder GetPurchaseOrderByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddPurchaseOrder(VHMS.Entity.Billing.PurchaseOrder Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iPurchaseOrderId = 0;
        int iUserId = 0;
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                iPurchaseOrderId = VHMS.DataAccess.Billing.PurchaseOrder.AddPurchaseOrder(Objdata);
                if (iPurchaseOrderId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iPurchaseOrderId.ToString();
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
            sException = "PurchaseOrder AddPurchaseOrder |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "PurchaseOrder_A_01")
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
    public string UpdatePurchaseOrder(VHMS.Entity.Billing.PurchaseOrder Objdata)
    {
        string sPurchaseOrderId = string.Empty;
        string sException = string.Empty;
        bool bPurchaseOrder = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;


                bPurchaseOrder = VHMS.DataAccess.Billing.PurchaseOrder.UpdatePurchaseOrder(Objdata);
                if (bPurchaseOrder == true)
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
            sException = "PurchaseOrder UpdatePurchaseOrder |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "PurchaseOrder_U_01")
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
    public string DeletePurchaseOrder(int ID)
    {
        string sPurchaseOrderId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bPurchaseOrder = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bPurchaseOrder = VHMS.DataAccess.Billing.PurchaseOrder.DeletePurchaseOrder(ID, iUserId);
                if (bPurchaseOrder == true)
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
            sException = "PurchaseOrder DeletePurchaseOrder |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "PurchaseOrder_R_01" || ex.Message.ToString() == "PurchaseOrder_D_01")
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

    #region "Purchase"
    public string GetPurchase(int PublisherID = 0, int BillType = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {

                Collection<VHMS.Entity.Billing.Purchase> ObjList = new Collection<VHMS.Entity.Billing.Purchase>();
                ObjList = VHMS.DataAccess.Billing.Purchase.GetPurchase(PublisherID, BillType, FK_FinancialYearID);
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
            sException = "Purchase GetPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTopPurchase(int PublisherID = 0, int BillType = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Purchase> ObjList = new Collection<VHMS.Entity.Billing.Purchase>();
                ObjList = VHMS.DataAccess.Billing.Purchase.GetTopPurchase(PublisherID, BillType, FK_FinancialYearID);
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
            sException = "Purchase GetPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetAdjustTDSPurchase(int ipatientID = 0, int iSupplierID = 0,int iTDSID=0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Purchase> ObjList = new Collection<VHMS.Entity.Billing.Purchase>();
                ObjList = VHMS.DataAccess.Billing.Purchase.GetAdjustTDSPurchase(ipatientID, iSupplierID, iTDSID, FK_FinancialYearID);
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
            sException = "Purchase GetPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }

    public string GetTDSPurchase(int ipatientID = 0, int iSupplierID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Purchase> ObjList = new Collection<VHMS.Entity.Billing.Purchase>();
                ObjList = VHMS.DataAccess.Billing.Purchase.GetTDSPurchase(ipatientID, iSupplierID, FK_FinancialYearID);
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
            sException = "Purchase GetPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTopPurchaseSupplierWise(int iSupplierID = 0, int BillType = 0, int DC = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Purchase> ObjList = new Collection<VHMS.Entity.Billing.Purchase>();
                ObjList = VHMS.DataAccess.Billing.Purchase.GetTopPurchaseSupplierWise(iSupplierID, BillType, DC, FK_FinancialYearID);
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
            sException = "Purchase GetPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTopPurchasePending(int PublisherID = 0, int BillType = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Purchase> ObjList = new Collection<VHMS.Entity.Billing.Purchase>();
                ObjList = VHMS.DataAccess.Billing.Purchase.GetTopPurchasePending(PublisherID, BillType, FK_FinancialYearID);
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
            sException = "Purchase GetPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetPendingPurchase(int PublisherID = 0, int BillType = 1)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Purchase> ObjList = new Collection<VHMS.Entity.Billing.Purchase>();
                ObjList = VHMS.DataAccess.Billing.Purchase.GetPendingPurchase(PublisherID, BillType, FK_FinancialYearID);
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
            sException = "Purchase GetPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetBillNo(int SupplierID = 0, int BillType = 1, int PurchaseReturnID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Purchase> ObjList = new Collection<VHMS.Entity.Billing.Purchase>();
                ObjList = VHMS.DataAccess.Billing.Purchase.GetBillNo(SupplierID, BillType, PurchaseReturnID, FK_FinancialYearID);
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
            sException = "Purchase GetPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetPurchaseDiscountBillNo(int SupplierID = 0, int BillType = 1, int PurchaseReturnID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Purchase> ObjList = new Collection<VHMS.Entity.Billing.Purchase>();
                ObjList = VHMS.DataAccess.Billing.Purchase.GetPurchaseDiscountBillNo(SupplierID, BillType, PurchaseReturnID, FK_FinancialYearID);
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
            sException = "Purchase GetPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetPendingPurchaseDiscountBillNo(int SupplierID = 0, int BillType = 1, int PurchaseReturnID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Purchase> ObjList = new Collection<VHMS.Entity.Billing.Purchase>();
                ObjList = VHMS.DataAccess.Billing.Purchase.GetPendingPurchaseDiscountBillNo(SupplierID, BillType, PurchaseReturnID, FK_FinancialYearID);
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
            sException = "Purchase GetPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchPurchase(string ID = null, int BillType = 1, int DC = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Purchase> ObjList = new Collection<VHMS.Entity.Billing.Purchase>();
                ObjList = VHMS.DataAccess.Billing.Purchase.SearchPurchase(ID, BillType, DC, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Purchase GetPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchPurchasePending(string ID = null, int BillType = 1)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Purchase> ObjList = new Collection<VHMS.Entity.Billing.Purchase>();
                ObjList = VHMS.DataAccess.Billing.Purchase.SearchPurchasePending(ID, BillType, FK_FinancialYearID);
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
            sException = "Purchase GetPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetPurchaseByID(int ID, int BillType = 1)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.Purchase objPurchase = new VHMS.Entity.Billing.Purchase();
                objPurchase = VHMS.DataAccess.Billing.Purchase.GetPurchaseByID(ID, BillType, FK_FinancialYearID);
                if (objPurchase.PurchaseID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objPurchase);
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
            sException = "Purchase GetPurchaseByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }

    public string GetTDSPurchaseByID(int iPurchaseID, int BillType = 1)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.Purchase objPurchase = new VHMS.Entity.Billing.Purchase();
                objPurchase = VHMS.DataAccess.Billing.Purchase.GetTDSPurchaseByID(iPurchaseID, BillType);
                if (objPurchase.PurchaseID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objPurchase);
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
            sException = "Purchase GetPurchaseByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetDCPurchase(int PublisherID = 0, int BillType = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Purchase> ObjList = new Collection<VHMS.Entity.Billing.Purchase>();
                ObjList = VHMS.DataAccess.Billing.Purchase.GetDCPurchase(PublisherID, BillType, FK_FinancialYearID);
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
            sException = "Purchase GetPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTopDCPurchase(int PublisherID = 0, int BillType = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Purchase> ObjList = new Collection<VHMS.Entity.Billing.Purchase>();
                ObjList = VHMS.DataAccess.Billing.Purchase.GetTopDCPurchase(PublisherID, BillType, FK_FinancialYearID);
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
            sException = "Purchase GetPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetPurchaseInvoice(string InvoiceNo)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {

                VHMS.Entity.Billing.Purchase objPurchase = new VHMS.Entity.Billing.Purchase();
                objPurchase = VHMS.DataAccess.Billing.Purchase.GetPurchaseInvoice(InvoiceNo);
                objResponse.Status = "Success";
                if (objPurchase.PurchaseID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objPurchase);
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
            sException = "Purchase GetPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }

    //public string GetPurchaseInvoice(string InvoiceNo)
    //{
    //    string sException = string.Empty;
    //    string sFileNames = string.Empty;
    //    JavaScriptSerializer jsObject = new JavaScriptSerializer();
    //    VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
    //    try
    //    {
    //        int FK_FinancialYearID = 0;
    //        if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
    //        {
    //            Collection<VHMS.Entity.Billing.Purchase> ObjList = new Collection<VHMS.Entity.Billing.Purchase>();
    //            //VHMS.Entity.Billing.Purchase ObjList = new VHMS.Entity.Billing.Purchase();
    //            ObjList = VHMS.DataAccess.Billing.Purchase.GetPurchaseInvoice(InvoiceNo);

    //            if (ObjList.PurchaseID > 0)
    //            {
    //                objResponse.Status = "Success";
    //                objResponse.Value = jsObject.Serialize(ObjList);
    //            }
    //            else
    //            {
    //                objResponse.Status = "Success";
    //                objResponse.Value = "NoRecord";
    //            }
    //        }
    //        else
    //        {
    //            objResponse.Status = "Error";
    //            objResponse.Value = "0";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        sException = "Sales GetSalesByInvoice |" + ex.Message.ToString();
    //        Log.Write(sException);
    //        objResponse.Status = "Error";
    //        objResponse.Value = "Error";
    //    }
    //    return jsObject.Serialize(objResponse);
    //}
    public string GetPendingDCPurchase(int PublisherID = 0, int BillType = 1)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Purchase> ObjList = new Collection<VHMS.Entity.Billing.Purchase>();
                ObjList = VHMS.DataAccess.Billing.Purchase.GetPendingDCPurchase(PublisherID, BillType, FK_FinancialYearID);
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
            sException = "Purchase GetPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchDCPurchase(string ID = null, int BillType = 1)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Purchase> ObjList = new Collection<VHMS.Entity.Billing.Purchase>();
                ObjList = VHMS.DataAccess.Billing.Purchase.SearchDCPurchase(ID, BillType, FK_FinancialYearID);
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
            sException = "Purchase GetPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetDCPurchaseByID(int ID, int BillType = 1)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.Purchase objPurchase = new VHMS.Entity.Billing.Purchase();
                objPurchase = VHMS.DataAccess.Billing.Purchase.GetDCPurchaseByID(ID, BillType, FK_FinancialYearID);
                if (objPurchase.PurchaseID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objPurchase);
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
            sException = "Purchase GetPurchaseByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddPurchase(VHMS.Entity.Billing.Purchase Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iPurchaseId = 0;
        int iUserId = 0;
        int FK_FinancialYearID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                iPurchaseId = VHMS.DataAccess.Billing.Purchase.AddPurchase(Objdata);
                if (iPurchaseId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iPurchaseId.ToString();
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
            sException = "Purchase AddPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Purchase_A_01")
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
    public string UpdatePurchase(VHMS.Entity.Billing.Purchase Objdata)
    {
        string sPurchaseId = string.Empty;
        string sException = string.Empty;
        bool bPurchase = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;
                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;
                bPurchase = VHMS.DataAccess.Billing.Purchase.UpdatePurchase(Objdata);
                if (bPurchase == true)
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
            sException = "Purchase UpdatePurchase |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Purchase_U_01")
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
    public string UpdatePurchasePending(VHMS.Entity.Billing.Purchase Objdata)
    {
        string sPurchaseId = string.Empty;
        string sException = string.Empty;
        bool bPurchase = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;
                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                bPurchase = VHMS.DataAccess.Billing.Purchase.UpdatePurchasePending(Objdata);
                if (bPurchase == true)
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
            sException = "Purchase UpdatePurchasePending |" + ex.Message.ToString();
            Log.Write(sException);
        }
        return jsonObject.Serialize(objResponse);
    }
    public string DeletePurchase(int ID)
    {
        string sPurchaseId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bPurchase = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bPurchase = VHMS.DataAccess.Billing.Purchase.DeletePurchase(ID, iUserId);
                if (bPurchase == true)
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
            sException = "Purchase DeletePurchase |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Purchase_R_01" || ex.Message.ToString() == "Purchase_D_01")
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
    public string CheckdeleteStockByProductID(int iProductID, decimal iQty = 0, int iPurchaseTransID = 0)
    {
        string sPurchaseId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bPurchase = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bPurchase = VHMS.DataAccess.Billing.Purchase.CheckdeleteStockByProductID(iProductID, iQty, iPurchaseTransID);

                objResponse.Status = "Success";
                objResponse.Value = "1";
            }
        }
        catch (Exception ex)
        {
            sException = "Purchase DeletePurchase |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Stock_R_01" || ex.Message.ToString() == "Stock_D_01")
            {
                objResponse.Status = "Error";
                objResponse.Value = ex.Message.ToString();
            }

        }
        return jsonObject.Serialize(objResponse);
    }
    #endregion

    #region "SalesOrder"
    public string GetSalesOrder(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.SalesOrder> ObjList = new Collection<VHMS.Entity.Billing.SalesOrder>();
                ObjList = VHMS.DataAccess.Billing.SalesOrder.GetSalesOrder(PublisherID, FK_FinancialYearID);
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
            sException = "SalesOrder GetSalesOrder |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }

    //public string GetSalesOrderID(int PublisherID = 0)
    //{
    //    string sException = string.Empty;
    //    string sFileNames = string.Empty;
    //    JavaScriptSerializer jsObject = new JavaScriptSerializer();
    //    VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
    //    try
    //    {
    //        if (ValidateSession())
    //        {
    //            Collection<VHMS.Entity.Billing.SalesOrder> ObjList = new Collection<VHMS.Entity.Billing.SalesOrder>();
    //            ObjList = VHMS.DataAccess.Billing.SalesOrder.GetSalesOrderID(PublisherID);
    //            objResponse.Status = "Success";
    //            objResponse.Value = ObjList.Count > 0 ? jsObject.Serialize(ObjList) : "NoRecord";
    //        }
    //        else
    //        {
    //            objResponse.Status = "Error";
    //            objResponse.Value = "0";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        sException = "SalesOrder GetSalesOrder |" + ex.Message.ToString();
    //        Log.Write(sException);
    //        objResponse.Status = "Error";
    //        objResponse.Value = "Error";
    //    }
    //    return jsObject.Serialize(objResponse);
    //}
    public string SearchSalesOrder(string ID = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.SalesOrder> ObjList = new Collection<VHMS.Entity.Billing.SalesOrder>();
                ObjList = VHMS.DataAccess.Billing.SalesOrder.SearchSalesOrder(ID, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "SalesOrder GetSalesOrder |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSalesOrderByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.SalesOrder objSalesOrder = new VHMS.Entity.Billing.SalesOrder();
                objSalesOrder = VHMS.DataAccess.Billing.SalesOrder.GetSalesOrderByID(ID, FK_FinancialYearID);
                if (objSalesOrder.SalesOrderID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objSalesOrder);
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
            sException = "SalesOrder GetSalesOrderByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddSalesOrder(VHMS.Entity.Billing.SalesOrder Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iSalesOrderId = 0;
        int iUserId = 0;
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                iSalesOrderId = VHMS.DataAccess.Billing.SalesOrder.AddSalesOrder(Objdata);
                if (iSalesOrderId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iSalesOrderId.ToString();
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
            sException = "SalesOrder AddSalesOrder |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "SalesOrder_A_01")
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
    public string UpdateSalesOrder(VHMS.Entity.Billing.SalesOrder Objdata)
    {
        string sSalesOrderId = string.Empty;
        string sException = string.Empty;
        bool bSalesOrder = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            int FK_FinancialYearID = 0;

            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                bSalesOrder = VHMS.DataAccess.Billing.SalesOrder.UpdateSalesOrder(Objdata);
                if (bSalesOrder == true)
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
            sException = "SalesOrder UpdateSalesOrder |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "SalesOrder_U_01")
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
    public string DeleteSalesOrder(int ID)
    {
        string sSalesOrderId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bSalesOrder = false;
        int iUserId = 0;
        try
        {

            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bSalesOrder = VHMS.DataAccess.Billing.SalesOrder.DeleteSalesOrder(ID, iUserId);
                if (bSalesOrder == true)
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
            sException = "SalesOrder DeleteSalesOrder |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "SalesOrder_R_01" || ex.Message.ToString() == "SalesOrder_D_01")
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

    #region "SalesEntry"
    public string GetSalesEntry(int PublisherID = 0, int IsRetail = 0, int IsYarnBill = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.SalesEntry> ObjList = new Collection<VHMS.Entity.Billing.SalesEntry>();
                ObjList = VHMS.DataAccess.Billing.SalesEntry.GetSalesEntry(PublisherID, IsRetail, IsYarnBill, FK_FinancialYearID);
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
            sException = "SalesEntry GetSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTDSSalesEntry(int PublisherID = 0, int TDSSalesID = 0, int IsRetail = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.SalesEntry> ObjList = new Collection<VHMS.Entity.Billing.SalesEntry>();
                ObjList = VHMS.DataAccess.Billing.SalesEntry.GetTDSSalesEntry(PublisherID, TDSSalesID, IsRetail, FK_FinancialYearID);
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
            sException = "SalesEntry GetSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetAdjustTDSSalesEntry(int PublisherID = 0, int TDSSalesID = 0, int IsRetail = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.SalesEntry> ObjList = new Collection<VHMS.Entity.Billing.SalesEntry>();
                ObjList = VHMS.DataAccess.Billing.SalesEntry.GetAdjustTDSSalesEntry(PublisherID, TDSSalesID, IsRetail, 0);
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
            sException = "SalesEntry GetSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetLastInvoiceNo()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iUserId = 0;
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.SalesEntry objUser = new VHMS.Entity.Billing.SalesEntry();
                objUser = VHMS.DataAccess.Billing.SalesEntry.GetLastInvoiceNo(FK_FinancialYearID);
                if (objUser.InvoiceNo.Length > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objUser);
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
            sException = "User GetUserByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetPendingRetailSales()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.SalesEntry> ObjList = new Collection<VHMS.Entity.Billing.SalesEntry>();
                ObjList = VHMS.DataAccess.Billing.SalesEntry.GetPendingRetailSales(FK_FinancialYearID);
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
            sException = "SalesEntry GetSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTopSalesEntry(int PublisherID = 0, int IsRetail = 0, int IsYarnBill = 0, int iCustomerID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.SalesEntry> ObjList = new Collection<VHMS.Entity.Billing.SalesEntry>();
                ObjList = VHMS.DataAccess.Billing.SalesEntry.GetTopSalesEntry(PublisherID, IsRetail, IsYarnBill, iCustomerID, FK_FinancialYearID);
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
            sException = "SalesEntry GetSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }

    public string GetTopRetailsSalesEntry(int PublisherID = 0, int IsRetail = 0, int IsYarnBill = 0, int iCustomerID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.SalesEntry> ObjList = new Collection<VHMS.Entity.Billing.SalesEntry>();
                ObjList = VHMS.DataAccess.Billing.SalesEntry.GetTopRetailsSalesEntry(PublisherID, IsRetail, IsYarnBill, iCustomerID, FK_FinancialYearID);
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
            sException = "SalesEntry GetSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTopSalesEntryDeleteList(int PublisherID = 0, int IsRetail = 0, int iCustomerID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.SalesEntry> ObjList = new Collection<VHMS.Entity.Billing.SalesEntry>();
                ObjList = VHMS.DataAccess.Billing.SalesEntry.GetTopSalesEntryDeleteList(PublisherID, IsRetail, iCustomerID, FK_FinancialYearID);
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
            sException = "SalesEntry GetSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSalesEntryBookingBill(int PublisherID = 0, int IsRetail = 0, int IsYarnBill = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.SalesEntry> ObjList = new Collection<VHMS.Entity.Billing.SalesEntry>();
                ObjList = VHMS.DataAccess.Billing.SalesEntry.GetSalesEntryBookingBill(PublisherID, IsRetail, IsYarnBill, FK_FinancialYearID);
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
            sException = "SalesEntry GetSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchSalesEntryBokingBill(string ID = null, int IsRetail = 0, int IsYarnBill = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.SalesEntry> ObjList = new Collection<VHMS.Entity.Billing.SalesEntry>();
                ObjList = VHMS.DataAccess.Billing.SalesEntry.SearchSalesEntryBokingBill(ID, IsRetail, IsYarnBill, FK_FinancialYearID);
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
            sException = "SalesEntry GetSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetPendingSalesEntry(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.SalesEntry> ObjList = new Collection<VHMS.Entity.Billing.SalesEntry>();
                ObjList = VHMS.DataAccess.Billing.SalesEntry.GetPendingSalesEntry(PublisherID, FK_FinancialYearID);
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
            sException = "SalesEntry GetSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetAmountClearEntry(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.RetailPaymentMode> ObjList = new Collection<VHMS.Entity.Billing.RetailPaymentMode>();
                ObjList = VHMS.DataAccess.Billing.SalesEntry.GetAmountClearEntry(PublisherID, FK_FinancialYearID);
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
            sException = "SalesEntry GetSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetPendingRetailBills(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.SalesEntry> ObjList = new Collection<VHMS.Entity.Billing.SalesEntry>();
                ObjList = VHMS.DataAccess.Billing.SalesEntry.GetPendingRetailBills(PublisherID, FK_FinancialYearID);
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
            sException = "SalesEntry GetSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchSalesEntry(string ID = null, int IsRetail = 0, int IsYarnBill = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.SalesEntry> ObjList = new Collection<VHMS.Entity.Billing.SalesEntry>();
                ObjList = VHMS.DataAccess.Billing.SalesEntry.SearchSalesEntry(ID, IsRetail, IsYarnBill, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "SalesEntry GetSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchSalesEntryDeleteList(string ID = null, int IsRetail = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.SalesEntry> ObjList = new Collection<VHMS.Entity.Billing.SalesEntry>();
                ObjList = VHMS.DataAccess.Billing.SalesEntry.SearchSalesEntryDeleteList(ID, IsRetail, FK_FinancialYearID);
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
            sException = "SalesEntry GetSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSalesEntryByInvoice(string InvoiceNo, int IsRetail)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.SalesEntry objSales = new VHMS.Entity.Billing.SalesEntry();
                objSales = VHMS.DataAccess.Billing.SalesEntry.GetSalesEntryByInvoice(InvoiceNo, FK_FinancialYearID, IsRetail);
                if (objSales.SalesEntryID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objSales);
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
            sException = "Sales GetSalesByInvoice |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSalesEntryByInvoiceReturn(string InvoiceNo, int SalesReturnID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.SalesEntry objSales = new VHMS.Entity.Billing.SalesEntry();
                objSales = VHMS.DataAccess.Billing.SalesEntry.GetSalesEntryByInvoiceReturn(InvoiceNo, SalesReturnID, FK_FinancialYearID);
                if (objSales.SalesEntryID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objSales);
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
            sException = "Sales GetSalesByInvoice |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSalesEntryByID(int ID, int IsRetail = 0, int IsYarnBill = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.SalesEntry objSalesEntry = new VHMS.Entity.Billing.SalesEntry();
                objSalesEntry = VHMS.DataAccess.Billing.SalesEntry.GetSalesEntryByID(ID, IsRetail, IsYarnBill, FK_FinancialYearID);
                if (objSalesEntry.SalesEntryID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objSalesEntry);
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
            sException = "SalesEntry GetSalesEntryByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTDSSalesEntryByID(int ID, int IsRetail = 0, int IsYarnBill = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.SalesEntry objSalesEntry = new VHMS.Entity.Billing.SalesEntry();
                objSalesEntry = VHMS.DataAccess.Billing.SalesEntry.GetTDSSalesEntryByID(ID, IsRetail, IsYarnBill, 0);
                if (objSalesEntry.SalesEntryID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objSalesEntry);
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
            sException = "SalesEntry GetSalesEntryByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSalesEntryJsonFormat(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.SalesJSON> objAddCollection = new Collection<VHMS.Entity.Billing.SalesJSON>();
                VHMS.Entity.Billing.SalesJSON objSalesEntry = new VHMS.Entity.Billing.SalesJSON();
                objSalesEntry = VHMS.DataAccess.Billing.SalesEntry.GetSalesEntryJsonFormat(ID);
                objAddCollection.Add(objSalesEntry);
                objResponse.Status = "Success";
                objResponse.Value = jsObject.Serialize(objAddCollection);

            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "SalesEntry GetSalesEntryJsonFormat |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSalesEntryJsonHSNFormat(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.SalesJSON> objAddCollection = new Collection<VHMS.Entity.Billing.SalesJSON>();
                VHMS.Entity.Billing.SalesJSON objSalesEntry = new VHMS.Entity.Billing.SalesJSON();
                objSalesEntry = VHMS.DataAccess.Billing.SalesEntry.GetSalesEntryJsonHSNFormat(ID);
                objAddCollection.Add(objSalesEntry);
                objResponse.Status = "Success";
                objResponse.Value = jsObject.Serialize(objAddCollection);

            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "SalesEntry GetSalesEntryJsonFormat |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSalesEntryReturnByID(int ID, int IsRetail = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.SalesEntry objSalesEntry = new VHMS.Entity.Billing.SalesEntry();
                objSalesEntry = VHMS.DataAccess.Billing.SalesEntry.GetSalesEntryReturnByID(ID, IsRetail, 0);
                if (objSalesEntry.SalesEntryID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objSalesEntry);
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
            sException = "SalesEntry GetSalesEntryByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetLastSalesEntryByID(int ID, int IsRetail = 0, int IsYarnBill = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.SalesEntry objSalesEntry = new VHMS.Entity.Billing.SalesEntry();
                objSalesEntry = VHMS.DataAccess.Billing.SalesEntry.GetLastSalesEntryByID(ID, IsRetail, IsYarnBill, FK_FinancialYearID);
                if (objSalesEntry.SalesEntryID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objSalesEntry);
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
            sException = "SalesEntry GetSalesEntryByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetLastOutwardByID(int ID, int IsRetail = 0, int IsYarnBill = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.Outward objOutward = new VHMS.Entity.Billing.Outward();
                objOutward = VHMS.DataAccess.Billing.Outward.GetLastOutwardByID(ID, IsRetail, IsYarnBill, FK_FinancialYearID);
                if (objOutward.OutwardID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objOutward);
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
            sException = "Outward GetOutwardByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddSalesEntry(VHMS.Entity.Billing.SalesEntry Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iSalesEntryId = 0;
        int iUserId = 0;
        int FK_FinancialYearID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                iSalesEntryId = VHMS.DataAccess.Billing.SalesEntry.AddSalesEntry(Objdata);
                if (iSalesEntryId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iSalesEntryId.ToString();
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
            sException = "SalesEntry AddSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "SalesEntry_A_01")
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
    public string UpdateSalesEntry(VHMS.Entity.Billing.SalesEntry Objdata)
    {
        string sSalesEntryId = string.Empty;
        string sException = string.Empty;
        bool bSalesEntry = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                bSalesEntry = VHMS.DataAccess.Billing.SalesEntry.UpdateSalesEntry(Objdata);
                if (bSalesEntry == true)
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
            sException = "SalesEntry UpdateSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "SalesEntry_U_01")
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
    public string DeleteSalesEntry(int ID, string Reason)
    {
        string sSalesEntryId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bSalesEntry = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bSalesEntry = VHMS.DataAccess.Billing.SalesEntry.DeleteSalesEntry(ID, Reason, iUserId);
                if (bSalesEntry == true)
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
            sException = "SalesEntry DeleteSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "SalesEntry_R_01" || ex.Message.ToString() == "SalesEntry_D_01")
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
    public string GetTransStockByID(int ID, int transaction_id, string type)
    {
        string sSalesEntryId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        decimal stockcnt = 0;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                stockcnt = VHMS.DataAccess.Billing.SalesEntry.GetTransStockByID(ID, transaction_id, type);
                if (stockcnt >= 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = stockcnt.ToString();
                }
                else
                {
                    objResponse.Status = "Success";
                    objResponse.Value = stockcnt.ToString();
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
            sException = "SalesEntry DeleteSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "SalesEntry_R_01" || ex.Message.ToString() == "SalesEntry_D_01")
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

    #region "YarnSalesEntry"
    public string GetYarnSalesEntry(int PublisherID = 0, int IsRetail = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.YarnSalesEntry> ObjList = new Collection<VHMS.Entity.Billing.YarnSalesEntry>();
                ObjList = VHMS.DataAccess.Billing.YarnSalesEntry.GetYarnSalesEntry(PublisherID, IsRetail, FK_FinancialYearID);
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
            sException = "YarnSalesEntry GetYarnSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTopYarnSalesEntry(int PublisherID = 0, int IsRetail = 0, int iCustomerID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.YarnSalesEntry> ObjList = new Collection<VHMS.Entity.Billing.YarnSalesEntry>();
                ObjList = VHMS.DataAccess.Billing.YarnSalesEntry.GetTopYarnSalesEntry(PublisherID, IsRetail, iCustomerID, FK_FinancialYearID);
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
            sException = "YarnSalesEntry GetYarnSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchYarnSalesEntry(string ID = null, int IsRetail = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.YarnSalesEntry> ObjList = new Collection<VHMS.Entity.Billing.YarnSalesEntry>();
                ObjList = VHMS.DataAccess.Billing.YarnSalesEntry.SearchYarnSalesEntry(ID, IsRetail, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "YarnSalesEntry GetYarnSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetYarnSalesEntryByID(int ID, int IsRetail = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.YarnSalesEntry objYarnSalesEntry = new VHMS.Entity.Billing.YarnSalesEntry();
                objYarnSalesEntry = VHMS.DataAccess.Billing.YarnSalesEntry.GetYarnSalesEntryByID(ID, IsRetail, FK_FinancialYearID);
                if (objYarnSalesEntry.YarnSalesEntryID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objYarnSalesEntry);
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
            sException = "YarnSalesEntry GetYarnSalesEntryByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddYarnSalesEntry(VHMS.Entity.Billing.YarnSalesEntry Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iYarnSalesEntryId = 0;
        int iUserId = 0;
        int FK_FinancialYearID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                iYarnSalesEntryId = VHMS.DataAccess.Billing.YarnSalesEntry.AddYarnSalesEntry(Objdata);
                if (iYarnSalesEntryId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iYarnSalesEntryId.ToString();
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
            sException = "YarnSalesEntry AddYarnSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "YarnSalesEntry_A_01")
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
    public string UpdateYarnSalesEntry(VHMS.Entity.Billing.YarnSalesEntry Objdata)
    {
        string sYarnSalesEntryId = string.Empty;
        string sException = string.Empty;
        bool bYarnSalesEntry = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                bYarnSalesEntry = VHMS.DataAccess.Billing.YarnSalesEntry.UpdateYarnSalesEntry(Objdata);
                if (bYarnSalesEntry == true)
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
            sException = "YarnSalesEntry UpdateYarnSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "YarnSalesEntry_U_01")
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
    public string DeleteYarnSalesEntry(int ID, string Reason)
    {
        string sYarnSalesEntryId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bYarnSalesEntry = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bYarnSalesEntry = VHMS.DataAccess.Billing.YarnSalesEntry.DeleteYarnSalesEntry(ID, Reason, iUserId);
                if (bYarnSalesEntry == true)
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
            sException = "YarnSalesEntry DeleteYarnSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "YarnSalesEntry_R_01" || ex.Message.ToString() == "YarnSalesEntry_D_01")
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

    #region "EstimateSalesEntry"
    public string GetEstimateSalesEntry(int PublisherID = 0, int IsRetail = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.EstimateSalesEntry> ObjList = new Collection<VHMS.Entity.Billing.EstimateSalesEntry>();
                ObjList = VHMS.DataAccess.Billing.EstimateSalesEntry.GetEstimateSalesEntry(PublisherID, IsRetail, FK_FinancialYearID);
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
            sException = "EstimateSalesEntry GetEstimateSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetPendingRetailEstimateSales()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.EstimateSalesEntry> ObjList = new Collection<VHMS.Entity.Billing.EstimateSalesEntry>();
                ObjList = VHMS.DataAccess.Billing.EstimateSalesEntry.GetPendingRetailEstimateSales(FK_FinancialYearID);
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
            sException = "EstimateSalesEntry GetEstimateSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTopEstimateSalesEntry(int PublisherID = 0, int IsRetail = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.EstimateSalesEntry> ObjList = new Collection<VHMS.Entity.Billing.EstimateSalesEntry>();
                ObjList = VHMS.DataAccess.Billing.EstimateSalesEntry.GetTopEstimateSalesEntry(PublisherID, IsRetail, FK_FinancialYearID);
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
            sException = "EstimateSalesEntry GetEstimateSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetEstimateSalesEntryBookingBill(int PublisherID = 0, int IsRetail = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.EstimateSalesEntry> ObjList = new Collection<VHMS.Entity.Billing.EstimateSalesEntry>();
                ObjList = VHMS.DataAccess.Billing.EstimateSalesEntry.GetEstimateSalesEntryBookingBill(PublisherID, IsRetail, FK_FinancialYearID);
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
            sException = "EstimateSalesEntry GetEstimateSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchEstimateSalesEntryBokingBill(string ID = null, int IsRetail = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.EstimateSalesEntry> ObjList = new Collection<VHMS.Entity.Billing.EstimateSalesEntry>();
                ObjList = VHMS.DataAccess.Billing.EstimateSalesEntry.SearchEstimateSalesEntryBokingBill(ID, IsRetail, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "EstimateSalesEntry GetEstimateSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetPendingEstimateSalesEntry(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.EstimateSalesEntry> ObjList = new Collection<VHMS.Entity.Billing.EstimateSalesEntry>();
                ObjList = VHMS.DataAccess.Billing.EstimateSalesEntry.GetPendingEstimateSalesEntry(PublisherID, FK_FinancialYearID);
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
            sException = "EstimateSalesEntry GetEstimateSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchEstimateSalesEntry(string ID = null, int IsRetail = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.EstimateSalesEntry> ObjList = new Collection<VHMS.Entity.Billing.EstimateSalesEntry>();
                ObjList = VHMS.DataAccess.Billing.EstimateSalesEntry.SearchEstimateSalesEntry(ID, IsRetail, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "EstimateSalesEntry GetEstimateSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetEstimateSalesEntryByInvoice(string InvoiceNo)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.EstimateSalesEntry objEstimateSales = new VHMS.Entity.Billing.EstimateSalesEntry();
                objEstimateSales = VHMS.DataAccess.Billing.EstimateSalesEntry.GetEstimateSalesEntryByInvoice(InvoiceNo, FK_FinancialYearID);
                if (objEstimateSales.EstimateSalesEntryID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objEstimateSales);
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
            sException = "EstimateSales GetEstimateSalesByInvoice |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetEstimateSalesEntryByID(int ID, int IsRetail = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.EstimateSalesEntry objEstimateSalesEntry = new VHMS.Entity.Billing.EstimateSalesEntry();
                objEstimateSalesEntry = VHMS.DataAccess.Billing.EstimateSalesEntry.GetEstimateSalesEntryByID(ID, IsRetail, FK_FinancialYearID);
                if (objEstimateSalesEntry.EstimateSalesEntryID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objEstimateSalesEntry);
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
            sException = "EstimateSalesEntry GetEstimateSalesEntryByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddEstimateSalesEntry(VHMS.Entity.Billing.EstimateSalesEntry Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iEstimateSalesEntryId = 0;
        int iUserId = 0;
        int FK_FinancialYearID = 0;

        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                iEstimateSalesEntryId = VHMS.DataAccess.Billing.EstimateSalesEntry.AddEstimateSalesEntry(Objdata);
                if (iEstimateSalesEntryId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iEstimateSalesEntryId.ToString();
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
            sException = "EstimateSalesEntry AddEstimateSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "EstimateSalesEntry_A_01")
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
    public string UpdateEstimateSalesEntry(VHMS.Entity.Billing.EstimateSalesEntry Objdata)
    {
        string sEstimateSalesEntryId = string.Empty;
        string sException = string.Empty;
        bool bEstimateSalesEntry = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                bEstimateSalesEntry = VHMS.DataAccess.Billing.EstimateSalesEntry.UpdateEstimateSalesEntry(Objdata);
                if (bEstimateSalesEntry == true)
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
            sException = "EstimateSalesEntry UpdateEstimateSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "EstimateSalesEntry_U_01")
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
    public string DeleteEstimateSalesEntry(int ID, string Reason)
    {
        string sEstimateSalesEntryId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bEstimateSalesEntry = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bEstimateSalesEntry = VHMS.DataAccess.Billing.EstimateSalesEntry.DeleteEstimateSalesEntry(ID, Reason, iUserId);
                if (bEstimateSalesEntry == true)
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
            sException = "EstimateSalesEntry DeleteEstimateSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "EstimateSalesEntry_R_01" || ex.Message.ToString() == "EstimateSalesEntry_D_01")
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
    public string UpdateEstimateSalesStatus(int ID)
    {
        string sEstimateSalesEntryId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bEstimateSalesEntry = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bEstimateSalesEntry = VHMS.DataAccess.Billing.EstimateSalesEntry.UpdateEstimateSalesStatus(ID, iUserId);
                if (bEstimateSalesEntry == true)
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
            sException = "EstimateSalesEntry DeleteEstimateSalesEntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "EstimateSalesEntry_R_01" || ex.Message.ToString() == "EstimateSalesEntry_D_01")
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

    #region "PurchaseReturn"
    public string GetPurchaseReturn(int PublisherID = 0, int iSupplierID = 0, int BillType = 1)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.PurchaseReturn> ObjList = new Collection<VHMS.Entity.Billing.PurchaseReturn>();
                ObjList = VHMS.DataAccess.Billing.PurchaseReturn.GetPurchaseReturn(PublisherID, iSupplierID, BillType, FK_FinancialYearID);
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
            sException = "PurchaseReturn GetPurchaseReturn |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }

    //public string GetPurchaseReturnID(int PublisherID = 0)
    //{
    //    string sException = string.Empty;
    //    string sFileNames = string.Empty;
    //    JavaScriptSerializer jsObject = new JavaScriptSerializer();
    //    VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
    //    try
    //    {
    //        if (ValidateSession())
    //        {
    //            Collection<VHMS.Entity.Billing.PurchaseReturn> ObjList = new Collection<VHMS.Entity.Billing.PurchaseReturn>();
    //            ObjList = VHMS.DataAccess.Billing.PurchaseReturn.GetPurchaseReturnID(PublisherID);
    //            objResponse.Status = "Success";
    //            objResponse.Value = ObjList.Count > 0 ? jsObject.Serialize(ObjList) : "NoRecord";
    //        }
    //        else
    //        {
    //            objResponse.Status = "Error";
    //            objResponse.Value = "0";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        sException = "PurchaseReturn GetPurchaseReturn |" + ex.Message.ToString();
    //        Log.Write(sException);
    //        objResponse.Status = "Error";
    //        objResponse.Value = "Error";
    //    }
    //    return jsObject.Serialize(objResponse);
    //}

    public string SearchPurchaseReturn(string ID = null, int BillType = 1)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.PurchaseReturn> ObjList = new Collection<VHMS.Entity.Billing.PurchaseReturn>();
                ObjList = VHMS.DataAccess.Billing.PurchaseReturn.SearchPurchaseReturn(ID, BillType, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "PurchaseReturn GetPurchaseReturn |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetPurchaseReturnByID(int ID, int Type)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.PurchaseReturn objPurchaseReturn = new VHMS.Entity.Billing.PurchaseReturn();
                objPurchaseReturn = VHMS.DataAccess.Billing.PurchaseReturn.GetPurchaseReturnByID(ID, Type, FK_FinancialYearID);
                if (objPurchaseReturn.PurchaseReturnID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objPurchaseReturn);
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
            sException = "PurchaseReturn GetPurchaseReturnByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetQuantity(int ID, int PurchaseID, int SupplierID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.PurchaseReturnTrans objPurchaseReturn = new VHMS.Entity.Billing.PurchaseReturnTrans();
                objPurchaseReturn = VHMS.DataAccess.Billing.PurchaseReturnTrans.GetQuantity(ID, PurchaseID, SupplierID);
                if (objPurchaseReturn.PurchaseReturnID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objPurchaseReturn);
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
            sException = "PurchaseReturn GetPurchaseReturnByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddPurchaseReturn(VHMS.Entity.Billing.PurchaseReturn Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iPurchaseReturnId = 0;
        int iUserId = 0;
        int FK_FinancialYearID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;
                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;
                iPurchaseReturnId = VHMS.DataAccess.Billing.PurchaseReturn.AddPurchaseReturn(Objdata);
                if (iPurchaseReturnId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iPurchaseReturnId.ToString();
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
            sException = "PurchaseReturn AddPurchaseReturn |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "PurchaseReturn_A_01")
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
    public string UpdatePurchaseReturn(VHMS.Entity.Billing.PurchaseReturn Objdata)
    {
        string sPurchaseReturnId = string.Empty;
        string sException = string.Empty;
        bool bPurchaseReturn = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0; int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;
                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;
                bPurchaseReturn = VHMS.DataAccess.Billing.PurchaseReturn.UpdatePurchaseReturn(Objdata);
                if (bPurchaseReturn == true)
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
            sException = "PurchaseReturn UpdatePurchaseReturn |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "PurchaseReturn_U_01")
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
    public string DeletePurchaseReturn(int ID)
    {
        string sPurchaseReturnId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bPurchaseReturn = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bPurchaseReturn = VHMS.DataAccess.Billing.PurchaseReturn.DeletePurchaseReturn(ID, iUserId);
                if (bPurchaseReturn == true)
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
            sException = "PurchaseReturn DeletePurchaseReturn |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "PurchaseReturn_R_01" || ex.Message.ToString() == "PurchaseReturn_D_01")
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

    public string GetPurchaseReturnJsonHSNFormat(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.PurchaseReturnJSON> objAddCollection = new Collection<VHMS.Entity.Billing.PurchaseReturnJSON>();
                VHMS.Entity.Billing.PurchaseReturnJSON objSalesEntry = new VHMS.Entity.Billing.PurchaseReturnJSON();
                objSalesEntry = VHMS.DataAccess.Billing.PurchaseReturn.GetPurchaseReturnJsonHSNFormat(ID);
                objAddCollection.Add(objSalesEntry);
                objResponse.Status = "Success";
                objResponse.Value = jsObject.Serialize(objAddCollection);

            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "SalesEntry GetSalesEntryJsonFormat |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    #endregion

    #region "ProcessingInward"
    public string GetPendingProcessingInward(int WorkID = 0, int VendorID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.PendingProcessingInward> ObjList = new Collection<VHMS.Entity.Billing.PendingProcessingInward>();
                ObjList = VHMS.DataAccess.Billing.ProcessingInward.GetPendingProcessingInward(WorkID, VendorID);
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
            sException = "ProcessingInward GetPendingProcessingInward |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetProcessingInward(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.ProcessingInward> ObjList = new Collection<VHMS.Entity.Billing.ProcessingInward>();
                ObjList = VHMS.DataAccess.Billing.ProcessingInward.GetProcessingInward(PublisherID);
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
            sException = "ProcessingInward GetProcessingInward |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchProcessingInward(string ID = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.ProcessingInward> ObjList = new Collection<VHMS.Entity.Billing.ProcessingInward>();
                ObjList = VHMS.DataAccess.Billing.ProcessingInward.SearchProcessingInward(ID);
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
            sException = "ProcessingInward GetProcessingInward |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetProcessingInwardByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.ProcessingInward objProcessingInward = new VHMS.Entity.Billing.ProcessingInward();
                objProcessingInward = VHMS.DataAccess.Billing.ProcessingInward.GetProcessingInwardByID(ID);
                if (objProcessingInward.ProcessingInwardID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objProcessingInward);
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
            sException = "ProcessingInward GetProcessingInwardByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddProcessingInward(VHMS.Entity.Billing.ProcessingInward Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iProcessingInwardId = 0;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;
                iProcessingInwardId = VHMS.DataAccess.Billing.ProcessingInward.AddProcessingInward(Objdata);
                if (iProcessingInwardId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iProcessingInwardId.ToString();
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
            sException = "ProcessingInward AddProcessingInward |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "ProcessingInward_A_01")
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
    public string UpdateProcessingInward(VHMS.Entity.Billing.ProcessingInward Objdata)
    {
        string sProcessingInwardId = string.Empty;
        string sException = string.Empty;
        bool bProcessingInward = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;
                bProcessingInward = VHMS.DataAccess.Billing.ProcessingInward.UpdateProcessingInward(Objdata);
                if (bProcessingInward == true)
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
            sException = "ProcessingInward UpdateProcessingInward |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "ProcessingInward_U_01")
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
    public string DeleteProcessingInward(int ID)
    {
        string sProcessingInwardId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bProcessingInward = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bProcessingInward = VHMS.DataAccess.Billing.ProcessingInward.DeleteProcessingInward(ID, iUserId);
                if (bProcessingInward == true)
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
            sException = "ProcessingInward DeleteProcessingInward |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "ProcessingInward_R_01" || ex.Message.ToString() == "ProcessingInward_D_01")
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

    #region "ProcessingOutward"
    public string GetProcessingOutward(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.ProcessingOutward> ObjList = new Collection<VHMS.Entity.Billing.ProcessingOutward>();
                ObjList = VHMS.DataAccess.Billing.ProcessingOutward.GetProcessingOutward(PublisherID);
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
            sException = "ProcessingOutward GetProcessingOutward |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchProcessingOutward(string ID = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.ProcessingOutward> ObjList = new Collection<VHMS.Entity.Billing.ProcessingOutward>();
                ObjList = VHMS.DataAccess.Billing.ProcessingOutward.SearchProcessingOutward(ID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "ProcessingOutward GetProcessingOutward |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetProcessingOutwardByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.ProcessingOutward objProcessingOutward = new VHMS.Entity.Billing.ProcessingOutward();
                objProcessingOutward = VHMS.DataAccess.Billing.ProcessingOutward.GetProcessingOutwardByID(ID);
                if (objProcessingOutward.ProcessingOutwardID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objProcessingOutward);
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
            sException = "ProcessingOutward GetProcessingOutwardByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddProcessingOutward(VHMS.Entity.Billing.ProcessingOutward Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iProcessingOutwardId = 0;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;
                iProcessingOutwardId = VHMS.DataAccess.Billing.ProcessingOutward.AddProcessingOutward(Objdata);
                if (iProcessingOutwardId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iProcessingOutwardId.ToString();
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
            sException = "ProcessingOutward AddProcessingOutward |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "ProcessingOutward_A_01")
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
    public string UpdateProcessingOutward(VHMS.Entity.Billing.ProcessingOutward Objdata)
    {
        string sProcessingOutwardId = string.Empty;
        string sException = string.Empty;
        bool bProcessingOutward = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;
                bProcessingOutward = VHMS.DataAccess.Billing.ProcessingOutward.UpdateProcessingOutward(Objdata);
                if (bProcessingOutward == true)
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
            sException = "ProcessingOutward UpdateProcessingOutward |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "ProcessingOutward_U_01")
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
    public string DeleteProcessingOutward(int ID)
    {
        string sProcessingOutwardId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bProcessingOutward = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bProcessingOutward = VHMS.DataAccess.Billing.ProcessingOutward.DeleteProcessingOutward(ID, iUserId);
                if (bProcessingOutward == true)
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
            sException = "ProcessingOutward DeleteProcessingOutward |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "ProcessingOutward_R_01" || ex.Message.ToString() == "ProcessingOutward_D_01")
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

    #region "Payment"
    public string GetPayment(int Type)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Payment> ObjList = new Collection<VHMS.Entity.Billing.Payment>();
                ObjList = VHMS.DataAccess.Billing.Payment.GetPayment(Type, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Payment GetPayment |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetLastPaymentDetails(int ID, int Type)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Payment> ObjList = new Collection<VHMS.Entity.Billing.Payment>();
                ObjList = VHMS.DataAccess.Billing.Payment.GetLastPaymentDetails(ID, Type, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Payment GetPayment |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetPaymentByID(int ID, int Type)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.Payment objPayment = new VHMS.Entity.Billing.Payment();
                objPayment = VHMS.DataAccess.Billing.Payment.GetPaymentByID(ID, Type, FK_FinancialYearID);
                if (objPayment.PaymentID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objPayment);
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
            sException = "Payment GetPaymentByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddPayment(VHMS.Entity.Billing.Payment Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iPaymentId = 0;
        int iUserId = 0;
        int FK_FinancialYearID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;
                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;
                iPaymentId = VHMS.DataAccess.Billing.Payment.AddPayment(Objdata);
                if (iPaymentId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iPaymentId.ToString();
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
            sException = "Payment AddPayment |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Payment_A_01")
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
    public string UpdatePayment(VHMS.Entity.Billing.Payment Objdata)
    {
        string sPaymentId = string.Empty;
        string sException = string.Empty;
        bool bPayment = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int iUserId = 0;
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.ModifiedBy = objUser;
                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;
                bPayment = VHMS.DataAccess.Billing.Payment.UpdatePayment(Objdata);
                if (bPayment == true)
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
            sException = "Payment UpdatePayment |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Payment_U_01")
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
    public string DeletePayment(int ID)
    {
        string sPaymentId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bPayment = false;
        try
        {
            if (ValidateSession())
            {
                bPayment = VHMS.DataAccess.Billing.Payment.DeletePayment(ID);
                if (bPayment == true)
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
            sException = "Payment DeletePayment |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Payment_R_01" || ex.Message.ToString() == "Payment_D_01")
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

    #region "Receipt"
    public string GetReceipt(int IsRetail)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Receipt> ObjList = new Collection<VHMS.Entity.Receipt>();
                ObjList = VHMS.DataAccess.Billing.Receipt.GetReceipt(IsRetail, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Receipt GetReceipt |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetLastReceiptDetails(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Receipt> ObjList = new Collection<VHMS.Entity.Receipt>();
                ObjList = VHMS.DataAccess.Billing.Receipt.GetLastReceiptDetails(ID, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Receipt GetReceipt |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetReceiptByStatus(string Status)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Receipt> ObjList = new Collection<VHMS.Entity.Receipt>();
                ObjList = VHMS.DataAccess.Billing.Receipt.GetReceiptByStatus(Status, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Receipt GetReceipt |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetReceiptByID(int ID, int IsRetail)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Receipt objReceipt = new VHMS.Entity.Receipt();
                objReceipt = VHMS.DataAccess.Billing.Receipt.GetReceiptByID(ID, IsRetail, FK_FinancialYearID);
                if (objReceipt.ReceiptID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objReceipt);
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
            sException = "Receipt GetReceiptByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetOnAccountAmount(int ID, string Type)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Receipt objReceipt = new VHMS.Entity.Receipt();
                objReceipt = VHMS.DataAccess.Billing.Receipt.GetOnAccountAmount(ID, Type);
                if (objReceipt.OnAccount >= 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objReceipt);
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
            sException = "Receipt GetReceiptByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddReceipt(VHMS.Entity.Receipt Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iReceiptId = 0;
        int iUserId = 0;
        int FK_FinancialYearID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                iReceiptId = VHMS.DataAccess.Billing.Receipt.AddReceipt(Objdata);
                if (iReceiptId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iReceiptId.ToString();
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
            sException = "Receipt AddReceipt |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Receipt_A_01")
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
    public string UpdateReceipt(VHMS.Entity.Receipt Objdata)
    {
        string sReceiptId = string.Empty;
        string sException = string.Empty;
        bool bReceipt = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            int FK_FinancialYearID = 0;

            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                bReceipt = VHMS.DataAccess.Billing.Receipt.UpdateReceipt(Objdata);
                if (bReceipt == true)
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
            sException = "Receipt UpdateReceipt |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Receipt_U_01")
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
    public string DeleteReceipt(int ID)
    {
        string sReceiptId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bReceipt = false;
        try
        {
            if (ValidateSession())
            {
                bReceipt = VHMS.DataAccess.Billing.Receipt.DeleteReceipt(ID);
                if (bReceipt == true)
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
            sException = "Receipt DeleteReceipt |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Receipt_R_01" || ex.Message.ToString() == "Receipt_D_01")
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

    #region "Advance"
    public string GetAdvance(int IsRetail = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Advance> ObjList = new Collection<VHMS.Entity.Advance>();
                ObjList = VHMS.DataAccess.Billing.Advance.GetAdvance(IsRetail, FK_FinancialYearID);
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
            sException = "Advance GetAdvance |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetAdvanceByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Advance objAdvance = new VHMS.Entity.Advance();
                objAdvance = VHMS.DataAccess.Billing.Advance.GetAdvanceByID(ID);
                if (objAdvance.AdvanceID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objAdvance);
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
            sException = "Advance GetAdvanceByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetAdvanceOutSalary(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Advance objAdvance = new VHMS.Entity.Advance();
                objAdvance = VHMS.DataAccess.Billing.Advance.GetAdvanceOutSalary(ID);
                if (objAdvance.AdvanceID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objAdvance);
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
            sException = "Advance GetAdvanceByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetAdvanceInSalary(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Advance objAdvance = new VHMS.Entity.Advance();
                objAdvance = VHMS.DataAccess.Billing.Advance.GetAdvanceInSalary(ID);
                if (objAdvance.AdvanceID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objAdvance);
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
            sException = "Advance GetAdvanceOutSalary |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetEmployeeAdvanceAmount(string FromDate = "", string ToDate = "", int EmployeeID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Advance objAdvance = new VHMS.Entity.Advance();
                objAdvance = VHMS.DataAccess.Billing.Advance.GetEmployeeAdvanceAmount(FromDate, ToDate, EmployeeID);
                if (objAdvance.AdvanceID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objAdvance);
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
            sException = "Advance GetAdvanceByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetAdvanceAmount(string FromDate = "", string ToDate = "", int EmployeeID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Advance objAdvance = new VHMS.Entity.Advance();
                objAdvance = VHMS.DataAccess.Billing.Advance.GetAdvanceAmount(FromDate, ToDate, EmployeeID);
                if (objAdvance.AdvanceID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objAdvance);
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
            sException = "Advance GetAdvanceByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddAdvance(VHMS.Entity.Advance Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iAdvanceId = 0;
        int iUserId = 0;
        int FK_FinancialYearID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                iAdvanceId = VHMS.DataAccess.Billing.Advance.AddAdvance(Objdata);
                if (iAdvanceId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iAdvanceId.ToString();
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
            sException = "Advance AddAdvance |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Advance_A_01")
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
    public string UpdateAdvance(VHMS.Entity.Advance Objdata)
    {
        string sAdvanceId = string.Empty;
        string sException = string.Empty;
        bool bAdvance = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                bAdvance = VHMS.DataAccess.Billing.Advance.UpdateAdvance(Objdata);
                if (bAdvance == true)
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
            sException = "Advance UpdateAdvance |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Advance_U_01")
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
    public string DeleteAdvance(int ID)
    {
        string sAdvanceId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bAdvance = false;
        try
        {
            if (ValidateSession())
            {
                bAdvance = VHMS.DataAccess.Billing.Advance.DeleteAdvance(ID);
                if (bAdvance == true)
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
            sException = "Advance DeleteAdvance |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Advance_R_01" || ex.Message.ToString() == "Advance_D_01")
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

    #region "AttendanceLog"
    public string GetAttendanceLog(int CountryID = 0, string FromDate = "", string ToDate = "", int iEmployeeID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.AttendanceLog> ObjList = new Collection<VHMS.Entity.AttendanceLog>();
                ObjList = VHMS.DataAccess.AttendanceLog.GetAttendanceLog(CountryID, FromDate, ToDate, iEmployeeID);
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
            sException = "AttendanceLog GetAttendanceLog |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetAttendanceLogByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.AttendanceLog objAttendanceLog = new VHMS.Entity.AttendanceLog();
                objAttendanceLog = VHMS.DataAccess.AttendanceLog.GetAttendanceLogByID(ID);
                if (objAttendanceLog.AttendanceLogID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objAttendanceLog);
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
            sException = "AttendanceLog GetAttendanceLogByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetEmployeeAttendanceLogByID(int EmployeeID = 0, string FromDate = "", string ToDate = "")
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.AttendanceLog> ObjList = new Collection<VHMS.Entity.AttendanceLog>();
                ObjList = VHMS.DataAccess.AttendanceLog.GetEmployeeAttendanceLogByID(EmployeeID, FromDate, ToDate);
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
            sException = "AttendanceLog GetAttendanceLog |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddAttendanceLog(VHMS.Entity.AttendanceLog Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iPurchaseId = 0;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;
                iPurchaseId = VHMS.DataAccess.AttendanceLog.AddAttendanceLog(Objdata);
                if (iPurchaseId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iPurchaseId.ToString();
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
            sException = "AttendanceLog AddAttendanceLog |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "AttendanceLogFinal_A_01")
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
    public string UpdateAttendanceLog(VHMS.Entity.AttendanceLog Objdata)
    {
        string sAttendanceLogId = string.Empty;
        string sException = string.Empty;
        bool bAttendanceLog = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;
                bAttendanceLog = VHMS.DataAccess.AttendanceLog.UpdateAttendanceLog(Objdata);
                if (bAttendanceLog == true)
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
            sException = "AttendanceLog UpdateAttendanceLog |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "AttendanceLogFinal_U_01")
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
    public string DeleteAttendanceLog(int ID)
    {
        string sStateId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bState = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bState = VHMS.DataAccess.AttendanceLog.DeleteAttendanceLog(ID);
                if (bState == true)
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
            sException = "VHMSService.AttendanceLogFinal DeleteAttendanceLogFinal |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "AttendanceLogFinal_R_01" || ex.Message.ToString() == "AttendanceLogFinal_D_01")
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

    #region "Salary"
    public string GetSalary(int CountryID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Salary> ObjList = new Collection<VHMS.Entity.Salary>();
                ObjList = VHMS.DataAccess.Salary.GetSalary(CountryID);
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
            sException = "Salary GetSalary |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSalaryByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Salary objSalary = new VHMS.Entity.Salary();
                objSalary = VHMS.DataAccess.Salary.GetSalaryByID(ID);
                if (objSalary.SalaryID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objSalary);
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
            sException = "Salary GetSalaryByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetEmployeeSalaryCount(string MonthYear, int iEmployeeID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Salary objSalary = new VHMS.Entity.Salary();
                objSalary = VHMS.DataAccess.Salary.GetEmployeeSalaryCount(MonthYear, iEmployeeID);
                if (objSalary.SalaryID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objSalary);
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
            sException = "Salary GetSalaryByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddSalary(VHMS.Entity.Salary Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iAdvanceId = 0;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;
                iAdvanceId = VHMS.DataAccess.Salary.AddSalary(Objdata);
                if (iAdvanceId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iAdvanceId.ToString();
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
            sException = "Salary AddSalary |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Salary_A_01")
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
    public string UpdateSalary(VHMS.Entity.Salary Objdata)
    {
        string sSalaryId = string.Empty;
        string sException = string.Empty;
        bool bSalary = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;
                bSalary = VHMS.DataAccess.Salary.UpdateSalary(Objdata);
                if (bSalary == true)
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
            sException = "Salary UpdateSalary |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Salary_U_01")
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
    public string DeleteSalary(int ID)
    {
        string sSalaryId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bSalary = false;
        try
        {
            if (ValidateSession())
            {
                bSalary = VHMS.DataAccess.Salary.DeleteSalary(ID);
                if (bSalary == true)
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
            sException = "Salary DeleteSalary |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Salary_R_01" || ex.Message.ToString() == "Salary_D_01")
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

    #region "VendorPayment"
    public string GetVendorPayment()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.VendorPayment> ObjList = new Collection<VHMS.Entity.Billing.VendorPayment>();
                ObjList = VHMS.DataAccess.Billing.VendorPayment.GetVendorPayment();
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "VendorPayment GetVendorPayment |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetVendorPaymentByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.VendorPayment objVendorPayment = new VHMS.Entity.Billing.VendorPayment();
                objVendorPayment = VHMS.DataAccess.Billing.VendorPayment.GetVendorPaymentByID(ID);
                if (objVendorPayment.VendorPaymentID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objVendorPayment);
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
            sException = "VendorPayment GetVendorPaymentByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddVendorPayment(VHMS.Entity.Billing.VendorPayment Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iVendorPaymentId = 0;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;
                iVendorPaymentId = VHMS.DataAccess.Billing.VendorPayment.AddVendorPayment(Objdata);
                if (iVendorPaymentId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iVendorPaymentId.ToString();
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
            sException = "VendorPayment AddVendorPayment |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "VendorPayment_A_01")
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
    public string UpdateVendorPayment(VHMS.Entity.Billing.VendorPayment Objdata)
    {
        string sVendorPaymentId = string.Empty;
        string sException = string.Empty;
        bool bVendorPayment = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;
                bVendorPayment = VHMS.DataAccess.Billing.VendorPayment.UpdateVendorPayment(Objdata);
                if (bVendorPayment == true)
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
            sException = "VendorPayment UpdateVendorPayment |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "VendorPayment_U_01")
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
    public string DeleteVendorPayment(int ID)
    {
        string sVendorPaymentId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bVendorPayment = false;
        try
        {
            if (ValidateSession())
            {
                bVendorPayment = VHMS.DataAccess.Billing.VendorPayment.DeleteVendorPayment(ID);
                if (bVendorPayment == true)
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
            sException = "VendorPayment DeleteVendorPayment |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "VendorPayment_R_01" || ex.Message.ToString() == "VendorPayment_D_01")
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

    #region "Journal"
    public string GetJournal(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Journal> ObjList = new Collection<VHMS.Entity.Billing.Journal>();
                ObjList = VHMS.DataAccess.Billing.Journal.GetJournal(PublisherID, FK_FinancialYearID);
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
            sException = "Journal GetJournal |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }

    //public string GetJournalID(int PublisherID = 0)
    //{
    //    string sException = string.Empty;
    //    string sFileNames = string.Empty;
    //    JavaScriptSerializer jsObject = new JavaScriptSerializer();
    //    VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
    //    try
    //    {
    //        if (ValidateSession())
    //        {
    //            Collection<VHMS.Entity.Billing.Journal> ObjList = new Collection<VHMS.Entity.Billing.Journal>();
    //            ObjList = VHMS.DataAccess.Billing.Journal.GetJournalID(PublisherID);
    //            objResponse.Status = "Success";
    //            objResponse.Value = ObjList.Count > 0 ? jsObject.Serialize(ObjList) : "NoRecord";
    //        }
    //        else
    //        {
    //            objResponse.Status = "Error";
    //            objResponse.Value = "0";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        sException = "Journal GetJournal |" + ex.Message.ToString();
    //        Log.Write(sException);
    //        objResponse.Status = "Error";
    //        objResponse.Value = "Error";
    //    }
    //    return jsObject.Serialize(objResponse);
    //}
    public string SearchJournal(string ID = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Journal> ObjList = new Collection<VHMS.Entity.Billing.Journal>();
                ObjList = VHMS.DataAccess.Billing.Journal.SearchJournal(ID, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Journal GetJournal |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetJournalByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.Journal objJournal = new VHMS.Entity.Billing.Journal();
                objJournal = VHMS.DataAccess.Billing.Journal.GetJournalByID(ID, FK_FinancialYearID);
                if (objJournal.JournalID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objJournal);
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
            sException = "Journal GetJournalByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddJournal(VHMS.Entity.Billing.Journal Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iJournalId = 0;
        int iUserId = 0;
        int FK_FinancialYearID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                iJournalId = VHMS.DataAccess.Billing.Journal.AddJournal(Objdata);
                if (iJournalId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iJournalId.ToString();
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
            sException = "Journal AddJournal |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Journal_A_01")
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
    public string UpdateJournal(VHMS.Entity.Billing.Journal Objdata)
    {
        string sJournalId = string.Empty;
        string sException = string.Empty;
        bool bJournal = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                bJournal = VHMS.DataAccess.Billing.Journal.UpdateJournal(Objdata);
                if (bJournal == true)
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
            sException = "Journal UpdateJournal |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Journal_U_01")
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
    public string DeleteJournal(int ID)
    {
        string sJournalId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bJournal = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bJournal = VHMS.DataAccess.Billing.Journal.DeleteJournal(ID, iUserId);
                if (bJournal == true)
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
            sException = "Journal DeleteJournal |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Journal_R_01" || ex.Message.ToString() == "Journal_D_01")
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

    #region "BankEntry"
    public string GetBankEntry()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.BankEntry> ObjList = new Collection<VHMS.Entity.Billing.BankEntry>();
                ObjList = VHMS.DataAccess.Billing.BankEntry.GetBankEntry();
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
            sException = "VHMSService.Billing.BankEntry GetBankEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetBankEntryByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.BankEntry objBankEntry = new VHMS.Entity.Billing.BankEntry();
                objBankEntry = VHMS.DataAccess.Billing.BankEntry.GetBankEntryByID(ID);
                if (objBankEntry.BankEntryID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objBankEntry);
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
            sException = "VHMSService.Billing.BankEntry GetBankEntryByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddBankEntry(VHMS.Entity.Billing.BankEntry Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iBankEntryId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iBankEntryId = VHMS.DataAccess.Billing.BankEntry.AddBankEntry(Objdata);
                if (iBankEntryId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iBankEntryId.ToString();
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
            sException = "VHMSService.Billing.BankEntry AddBankEntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "BankEntry_A_01")
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
    public string UpdateBankEntry(VHMS.Entity.Billing.BankEntry Objdata)
    {
        string sBankEntryId = string.Empty;
        string sException = string.Empty;
        bool bBankEntry = false;
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
                bBankEntry = VHMS.DataAccess.Billing.BankEntry.UpdateBankEntry(Objdata);
                if (bBankEntry == true)
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
            sException = "VHMSService.Billing.BankEntry UpdateBankEntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "BankEntry_U_01")
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
    public string DeleteBankEntry(int ID)
    {
        string sBankEntryId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bBankEntry = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bBankEntry = VHMS.DataAccess.Billing.BankEntry.DeleteBankEntry(ID, UserID);
                if (bBankEntry == true)
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
            sException = "VHMSService.Billing.BankEntry DeleteBankEntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "BankEntry_R_01" || ex.Message.ToString() == "BankEntry_D_01")
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

    #region "ExchangeReceipt"
    public string GetExchangeReceipt()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.ExchangeReceipt> ObjList = new Collection<VHMS.Entity.ExchangeReceipt>();
                ObjList = VHMS.DataAccess.Billing.ExchangeReceipt.GetExchangeReceipt(FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "ExchangeReceipt GetExchangeReceipt |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetExchangeReceiptByStatus(string Status)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.ExchangeReceipt> ObjList = new Collection<VHMS.Entity.ExchangeReceipt>();
                ObjList = VHMS.DataAccess.Billing.ExchangeReceipt.GetExchangeReceiptByStatus(Status, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "ExchangeReceipt GetExchangeReceipt |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetExchangeReceiptByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.ExchangeReceipt objExchangeReceipt = new VHMS.Entity.ExchangeReceipt();
                objExchangeReceipt = VHMS.DataAccess.Billing.ExchangeReceipt.GetExchangeReceiptByID(ID, FK_FinancialYearID);
                if (objExchangeReceipt.ExchangeReceiptID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objExchangeReceipt);
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
            sException = "ExchangeReceipt GetExchangeReceiptByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddExchangeReceipt(VHMS.Entity.ExchangeReceipt Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iExchangeReceiptId = 0;
        int iUserId = 0;
        int FK_FinancialYearID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                iExchangeReceiptId = VHMS.DataAccess.Billing.ExchangeReceipt.AddExchangeReceipt(Objdata);
                if (iExchangeReceiptId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iExchangeReceiptId.ToString();
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
            sException = "ExchangeReceipt AddExchangeReceipt |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "ExchangeReceipt_A_01")
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
    public string UpdateExchangeReceipt(VHMS.Entity.ExchangeReceipt Objdata)
    {
        string sExchangeReceiptId = string.Empty;
        string sException = string.Empty;
        bool bExchangeReceipt = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            int FK_FinancialYearID = 0;

            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                bExchangeReceipt = VHMS.DataAccess.Billing.ExchangeReceipt.UpdateExchangeReceipt(Objdata);
                if (bExchangeReceipt == true)
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
            sException = "ExchangeReceipt UpdateExchangeReceipt |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "ExchangeReceipt_U_01")
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
    public string DeleteExchangeReceipt(int ID)
    {
        string sExchangeReceiptId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bExchangeReceipt = false;
        try
        {
            if (ValidateSession())
            {
                bExchangeReceipt = VHMS.DataAccess.Billing.ExchangeReceipt.DeleteExchangeReceipt(ID);
                if (bExchangeReceipt == true)
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
            sException = "ExchangeReceipt DeleteExchangeReceipt |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "ExchangeReceipt_R_01" || ex.Message.ToString() == "ExchangeReceipt_D_01")
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

    #region "Expense"
    public string GetExpense(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Expense> ObjList = new Collection<VHMS.Entity.Billing.Expense>();
                ObjList = VHMS.DataAccess.Billing.Expense.GetExpense(PublisherID, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Expense GetExpense |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }

    public string GetAdjustTDSExpense(int ipatientID = 0, int iTDSID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Expense> ObjList = new Collection<VHMS.Entity.Billing.Expense>();
                ObjList = VHMS.DataAccess.Billing.Expense.GetAdjustTDSExpense(ipatientID,  iTDSID, FK_FinancialYearID);
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
            sException = "Purchase GetPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }

    public string GetTDSExpense(int ipatientID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Expense> ObjList = new Collection<VHMS.Entity.Billing.Expense>();
                ObjList = VHMS.DataAccess.Billing.Expense.GetTDSExpense(ipatientID, FK_FinancialYearID);
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
            sException = "Purchase GetPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetPendingExpense(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Expense> ObjList = new Collection<VHMS.Entity.Billing.Expense>();
                ObjList = VHMS.DataAccess.Billing.Expense.GetPendingExpense(PublisherID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Purchase GetPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchExpense(string ID = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Expense> ObjList = new Collection<VHMS.Entity.Billing.Expense>();
                ObjList = VHMS.DataAccess.Billing.Expense.SearchExpense(ID, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Expense GetExpense |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetExpenseByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.Expense objExpense = new VHMS.Entity.Billing.Expense();
                objExpense = VHMS.DataAccess.Billing.Expense.GetExpenseByID(ID, FK_FinancialYearID);
                if (objExpense.ExpenseID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objExpense);
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
            sException = "Expense GetExpenseByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }


    public string GetTDSExpenseByID(int iExpenseID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.Expense objExpense = new VHMS.Entity.Billing.Expense();
                objExpense = VHMS.DataAccess.Billing.Expense.GetTDSExpenseByID(iExpenseID, FK_FinancialYearID);
                if (objExpense.ExpenseID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objExpense);
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
            sException = "Expense GetExpenseByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddExpense(VHMS.Entity.Billing.Expense Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iExpenseId = 0;
        int iUserId = 0;
        int FK_FinancialYearID = 0;
        try
        {

            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                iExpenseId = VHMS.DataAccess.Billing.Expense.AddExpense(Objdata);
                if (iExpenseId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iExpenseId.ToString();
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
            sException = "Expense AddExpense |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Expense_A_01")
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
    public string UpdateExpense(VHMS.Entity.Billing.Expense Objdata)
    {
        string sExpenseId = string.Empty;
        string sException = string.Empty;
        bool bExpense = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                bExpense = VHMS.DataAccess.Billing.Expense.UpdateExpense(Objdata);
                if (bExpense == true)
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
            sException = "Expense UpdateExpense |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Expense_U_01")
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
    public string DeleteExpense(int ID)
    {
        string sExpenseId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bExpense = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bExpense = VHMS.DataAccess.Billing.Expense.DeleteExpense(ID, iUserId);
                if (bExpense == true)
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
            sException = "Expense DeleteExpense |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Expense_R_01" || ex.Message.ToString() == "Expense_D_01")
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

    #region "LREntry"
    public string GetLREntry(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.LREntry> ObjList = new Collection<VHMS.Entity.Billing.LREntry>();
                ObjList = VHMS.DataAccess.Billing.LREntry.GetLREntry(PublisherID, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "LREntry GetLREntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchLREntry(string ID = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.LREntry> ObjList = new Collection<VHMS.Entity.Billing.LREntry>();
                ObjList = VHMS.DataAccess.Billing.LREntry.SearchLREntry(ID, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "LREntry GetLREntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetLREntryByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.LREntry objLREntry = new VHMS.Entity.Billing.LREntry();
                objLREntry = VHMS.DataAccess.Billing.LREntry.GetLREntryByID(ID, FK_FinancialYearID);
                if (objLREntry.LREntryID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objLREntry);
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
            sException = "LREntry GetLREntryByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetLREntrySalesID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.LREntry objLREntry = new VHMS.Entity.Billing.LREntry();
                objLREntry = VHMS.DataAccess.Billing.LREntry.GetLREntrySalesID(ID, FK_FinancialYearID);
                if (objLREntry.LREntryID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objLREntry);
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
            sException = "LREntry GetLREntryByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddLREntry(VHMS.Entity.Billing.LREntry Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iLREntryId = 0;
        int iUserId = 0;
        int FK_FinancialYearID = 0;
        try
        {


            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                iLREntryId = VHMS.DataAccess.Billing.LREntry.AddLREntry(Objdata);
                if (iLREntryId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iLREntryId.ToString();
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
            sException = "LREntry AddLREntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "LREntry_A_01")
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
    public string UpdateLREntry(VHMS.Entity.Billing.LREntry Objdata)
    {
        string sLREntryId = string.Empty;
        string sException = string.Empty;
        bool bLREntry = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                bLREntry = VHMS.DataAccess.Billing.LREntry.UpdateLREntry(Objdata);
                if (bLREntry == true)
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
            sException = "LREntry UpdateLREntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "LREntry_U_01")
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
    public string UpdateLREntryStatus(VHMS.Entity.Billing.LREntry Objdata)
    {
        string sLREntryId = string.Empty;
        string sException = string.Empty;
        bool bLREntry = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;
                bLREntry = VHMS.DataAccess.Billing.LREntry.UpdateLREntryStatus(Objdata);
                if (bLREntry == true)
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
            sException = "LREntry UpdateLREntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "LREntry_U_01")
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
    public string DeleteLREntry(int ID)
    {
        string sLREntryId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bLREntry = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bLREntry = VHMS.DataAccess.Billing.LREntry.DeleteLREntry(ID, iUserId);
                if (bLREntry == true)
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
            sException = "LREntry DeleteLREntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "LREntry_R_01" || ex.Message.ToString() == "LREntry_D_01")
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

    #region INLREntry
    public string GetINLREntry(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.INLREntry> ObjList = new Collection<VHMS.Entity.Billing.INLREntry>();
                ObjList = VHMS.DataAccess.Billing.INLREntry.GetINLREntry(PublisherID, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "LREntry GetLREntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchINLREntry(string ID = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.INLREntry> ObjList = new Collection<VHMS.Entity.Billing.INLREntry>();
                ObjList = VHMS.DataAccess.Billing.INLREntry.SearchINLREntry(ID, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "LREntry GetLREntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetINLREntryByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.INLREntry objINLREntry = new VHMS.Entity.Billing.INLREntry();
                objINLREntry = VHMS.DataAccess.Billing.INLREntry.GetINLREntryByID(ID, FK_FinancialYearID);
                if (objINLREntry.INLREntryID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objINLREntry);
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
            sException = "LREntry GetLREntryByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetINLREntrySalesID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.INLREntry objINLREntry = new VHMS.Entity.Billing.INLREntry();
                objINLREntry = VHMS.DataAccess.Billing.INLREntry.GetINLREntrySalesID(ID, FK_FinancialYearID);
                if (objINLREntry.INLREntryID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objINLREntry);
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
            sException = "LREntry GetLREntryByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddINLREntry(VHMS.Entity.Billing.INLREntry Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iLREntryId = 0;
        int iUserId = 0;
        int FK_FinancialYearID = 0;
        try
        {


            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                iLREntryId = VHMS.DataAccess.Billing.INLREntry.AddINLREntry(Objdata);
                if (iLREntryId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iLREntryId.ToString();
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
            sException = "LREntry AddINLREntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "LREntry_A_01")
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
    public string UpdateINLREntry(VHMS.Entity.Billing.INLREntry Objdata)
    {
        string sLREntryId = string.Empty;
        string sException = string.Empty;
        bool bLREntry = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                bLREntry = VHMS.DataAccess.Billing.INLREntry.UpdateINLREntry(Objdata);
                if (bLREntry == true)
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
            sException = "LREntry UpdateINLREntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "LREntry_U_01")
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
    public string UpdateINLREntryStatus(VHMS.Entity.Billing.INLREntry Objdata)
    {
        string sLREntryId = string.Empty;
        string sException = string.Empty;
        bool bLREntry = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;
                bLREntry = VHMS.DataAccess.Billing.INLREntry.UpdateINLREntryStatus(Objdata);
                if (bLREntry == true)
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
            sException = "LREntry UpdateINLREntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "LREntry_U_01")
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
    public string DeleteINLREntry(int ID)
    {
        string sLREntryId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bLREntry = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bLREntry = VHMS.DataAccess.Billing.INLREntry.DeleteINLREntry(ID, iUserId);
                if (bLREntry == true)
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
            sException = "LREntry DeleteINLREntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "LREntry_R_01" || ex.Message.ToString() == "LREntry_D_01")
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

    #region Others
    public string GetProductRate(int ID, string type, int SupplierID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Price objTax = new VHMS.Entity.Price();
                objTax = VHMS.DataAccess.Billing.Purchase.GetProductRate(ID, type, SupplierID);
                if (!string.IsNullOrEmpty(objTax.Type))
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objTax);
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
            sException = "VHMSService.Billing.Tax GetTaxByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetWholeSaleProductRate(int ID, string type, int SupplierID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Price objTax = new VHMS.Entity.Price();
                objTax = VHMS.DataAccess.Billing.Purchase.GetWholeSaleProductRate(ID, type, SupplierID);
                if (!string.IsNullOrEmpty(objTax.Type))
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objTax);
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
            sException = "VHMSService.Billing.Tax GetTaxByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetRetailProductRate(int ID, string type, int SupplierID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Price objTax = new VHMS.Entity.Price();
                objTax = VHMS.DataAccess.Billing.Purchase.GetRetailProductRate(ID, type, SupplierID);
                if (!string.IsNullOrEmpty(objTax.Type))
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objTax);
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
            sException = "VHMSService.Billing.Tax GetTaxByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetRateByProduct(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Price objTax = new VHMS.Entity.Price();
                objTax = VHMS.DataAccess.Billing.Purchase.GetRateByProduct(ID);
                if (!string.IsNullOrEmpty(objTax.Rate.ToString()))
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objTax);
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
            sException = "VHMSService.Billing.Tax GetTaxByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetProductDetails(int ID, int type, int SupplierID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.SalesEntryTransDetails> ObjList = new Collection<VHMS.Entity.Billing.SalesEntryTransDetails>();
                ObjList = VHMS.DataAccess.Billing.SalesEntry.GetProductDetails(ID, type, SupplierID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Receipt GetReceipt |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetProductDetailsBySMSCode(int ID, int type, int SupplierID, string iSMSCode = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.SalesEntryTransDetails> ObjList = new Collection<VHMS.Entity.Billing.SalesEntryTransDetails>();
                ObjList = VHMS.DataAccess.Billing.SalesEntry.GetProductDetailsBySMSCode(ID, type, SupplierID, iSMSCode);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Receipt GetReceipt |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetProductDetailsEstimate(int ID, int type, int SupplierID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.SalesEntryTransDetails> ObjList = new Collection<VHMS.Entity.Billing.SalesEntryTransDetails>();
                ObjList = VHMS.DataAccess.Billing.SalesEntry.GetProductDetailsEstimate(ID, type, SupplierID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Receipt GetReceipt |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetProductDetailsEstimateBySMSCoode(int ID, int type, int SupplierID, string iSMSCode = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.SalesEntryTransDetails> ObjList = new Collection<VHMS.Entity.Billing.SalesEntryTransDetails>();
                ObjList = VHMS.DataAccess.Billing.SalesEntry.GetProductDetailsEstimateBySMSCoode(ID, type, SupplierID, iSMSCode);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Receipt GetReceipt |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetNewProductDetails(int ID = 0, string code = null, int type = 0, int SupplierID = 0, int SalesEntryID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.SalesEntryTransDetails> ObjList = new Collection<VHMS.Entity.Billing.SalesEntryTransDetails>();
                ObjList = VHMS.DataAccess.Billing.SalesEntry.GetNewProductDetails(ID, code, type, SupplierID, SalesEntryID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Receipt GetReceipt |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetPurchaseNewProductDetails(int ID = 0, string code = null, int type = 0, int SupplierID = 0, int SalesEntryID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.PurchaseTransDetails> ObjList = new Collection<VHMS.Entity.Billing.PurchaseTransDetails>();
                ObjList = VHMS.DataAccess.Billing.Purchase.GetPurchaseNewProductDetails(ID, code, type, SupplierID, SalesEntryID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Receipt GetReceipt |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetProductDetailsPurchase(int ID, int type, int SupplierID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.PurchaseTransDetails> ObjList = new Collection<VHMS.Entity.Billing.PurchaseTransDetails>();
                ObjList = VHMS.DataAccess.Billing.Purchase.GetProductDetailsPurchase(ID, type, SupplierID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Receipt GetReceipt |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetCurrentStock(int ID = 0, int CategoryID = 0, int SubCategoryID = 0, int SupplierID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Price objTax = new VHMS.Entity.Price();
                objTax = VHMS.DataAccess.Billing.Purchase.GetCurrentStock(ID, CategoryID, SubCategoryID, SupplierID);
                if (!string.IsNullOrEmpty(objTax.Rate.ToString()))
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objTax);
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
            sException = "VHMSService.Billing.Tax GetTaxByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    #endregion

    #region "VENDORENTRY"
    public string GetVendorEntry(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.VendorEntry> ObjList = new Collection<VHMS.Entity.Billing.VendorEntry>();
                ObjList = VHMS.DataAccess.Billing.VendorEntry.GetVendorEntry(PublisherID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "VendorEntry GetVendorEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchVendorEntry(string ID = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.VendorEntry> ObjList = new Collection<VHMS.Entity.Billing.VendorEntry>();
                ObjList = VHMS.DataAccess.Billing.VendorEntry.SearchVendorEntry(ID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "VendorEntry GetVendorEntry |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetVendorEntryByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.VendorEntry objVendorEntry = new VHMS.Entity.Billing.VendorEntry();
                objVendorEntry = VHMS.DataAccess.Billing.VendorEntry.GetVendorEntryByID(ID);
                if (objVendorEntry.VendorEntryID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objVendorEntry);
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
            sException = "VendorEntry GetVendorEntryByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetVendorStockCheck(int iVendorID, int iWorkID, int iVendorEntryID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.VendorStockCheck objVendorEntry = new VHMS.Entity.Billing.VendorStockCheck();
                objVendorEntry = VHMS.DataAccess.Billing.VendorEntry.GetVendorStockCheck(iVendorID, iWorkID, iVendorEntryID);

                objResponse.Status = "Success";
                objResponse.Value = jsObject.Serialize(objVendorEntry);

            }
            else
            {
                objResponse.Status = "Error";
                objResponse.Value = "0";
            }
        }
        catch (Exception ex)
        {
            sException = "VendorEntry GetVendorEntryByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetVendorEntryByStatus(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.VendorEntry objVendorEntry = new VHMS.Entity.Billing.VendorEntry();
                objVendorEntry = VHMS.DataAccess.Billing.VendorEntry.GetVendorEntryByStatus(ID);
                if (objVendorEntry.VendorEntryID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objVendorEntry);
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
            sException = "VendorEntry GetVendorEntryByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetVendorEntryByOpeningQty(int ID, int iVendorEntryID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.VendorEntry objVendorEntry = new VHMS.Entity.Billing.VendorEntry();
                objVendorEntry = VHMS.DataAccess.Billing.VendorEntry.GetVendorEntryByOpeningQty(ID, iVendorEntryID);
                if (objVendorEntry.OpeningQty > 0 || objVendorEntry.OpeningBalance > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objVendorEntry);
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
            sException = "VendorEntry GetVendorEntryByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddVendorEntry(VHMS.Entity.Billing.VendorEntry Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iVendorEntryId = 0;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                iVendorEntryId = VHMS.DataAccess.Billing.VendorEntry.AddVendorEntry(Objdata);
                if (iVendorEntryId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iVendorEntryId.ToString();
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
            sException = "VendorEntry AddVendorEntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "VendorEntry_A_01")
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
    public string UpdateVendorEntry(VHMS.Entity.Billing.VendorEntry Objdata)
    {
        string sVendorEntryId = string.Empty;
        string sException = string.Empty;
        bool bVendorEntry = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId))
            {
                bVendorEntry = VHMS.DataAccess.Billing.VendorEntry.UpdateVendorEntry(Objdata);
                if (bVendorEntry == true)
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
            sException = "VendorEntry UpdateVendorEntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "VendorEntry_U_01")
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
    public string DeleteVendorEntry(int ID)
    {
        string sVendorEntryId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bVendorEntry = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bVendorEntry = VHMS.DataAccess.Billing.VendorEntry.DeleteVendorEntry(ID, iUserId);
                if (bVendorEntry == true)
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
            sException = "VendorEntry DeleteVendorEntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "VendorEntry_R_01" || ex.Message.ToString() == "VendorEntry_D_01")
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

    #region "ExpensePayment"
    public string GetExpensePayment(int Type, int iPartyID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.ExpensePayment> ObjList = new Collection<VHMS.Entity.Billing.ExpensePayment>();
                ObjList = VHMS.DataAccess.Billing.ExpensePayment.GetExpensePayment(Type, FK_FinancialYearID, iPartyID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "ExpensePayment GetExpensePayment |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetLastExpensePaymentDetails(int ID, int Type)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.ExpensePayment> ObjList = new Collection<VHMS.Entity.Billing.ExpensePayment>();
                ObjList = VHMS.DataAccess.Billing.ExpensePayment.GetLastExpensePaymentDetails(ID, Type, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "ExpensePayment GetExpensePayment |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetExpensePaymentByID(int ID, int Type)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.ExpensePayment objExpensePayment = new VHMS.Entity.Billing.ExpensePayment();
                objExpensePayment = VHMS.DataAccess.Billing.ExpensePayment.GetExpensePaymentByID(ID, Type, FK_FinancialYearID);
                if (objExpensePayment.ExpensePaymentID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objExpensePayment);
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
            sException = "ExpensePayment GetExpensePaymentByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddExpensePayment(VHMS.Entity.Billing.ExpensePayment Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iExpensePaymentId = 0;
        int iUserId = 0;
        int FK_FinancialYearID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                iExpensePaymentId = VHMS.DataAccess.Billing.ExpensePayment.AddExpensePayment(Objdata);
                if (iExpensePaymentId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iExpensePaymentId.ToString();
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
            sException = "ExpensePayment AddExpensePayment |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "ExpensePayment_A_01")
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
    public string UpdateExpensePayment(VHMS.Entity.Billing.ExpensePayment Objdata)
    {
        string sExpensePaymentId = string.Empty;
        string sException = string.Empty;
        bool bExpensePayment = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int iUserId = 0;
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.ModifiedBy = objUser;
                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;
                bExpensePayment = VHMS.DataAccess.Billing.ExpensePayment.UpdateExpensePayment(Objdata);
                if (bExpensePayment == true)
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
            sException = "ExpensePayment UpdateExpensePayment |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "ExpensePayment_U_01")
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
    public string DeleteExpensePayment(int ID)
    {
        string sExpensePaymentId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bExpensePayment = false;
        try
        {
            if (ValidateSession())
            {
                bExpensePayment = VHMS.DataAccess.Billing.ExpensePayment.DeleteExpensePayment(ID);
                if (bExpensePayment == true)
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
            sException = "ExpensePayment DeleteExpensePayment |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "ExpensePayment_R_01" || ex.Message.ToString() == "ExpensePayment_D_01")
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
    public string GetExpenseBalanceAmount(int ID, string Type)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Receipt objReceipt = new VHMS.Entity.Receipt();
                objReceipt = VHMS.DataAccess.Billing.ExpensePayment.GetExpenseBalanceAmount(ID, Type);
                if (objReceipt.OnAccount >= 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objReceipt);
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
            sException = "Receipt GetReceiptByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    #endregion

    #region "PurchaseDiscount"
    public string GetPurchaseDiscount(int PublisherID = 0, int iSupplierID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.PurchaseDiscount> ObjList = new Collection<VHMS.Entity.Billing.PurchaseDiscount>();
                ObjList = VHMS.DataAccess.Billing.PurchaseDiscount.GetPurchaseDiscount(PublisherID, iSupplierID, FK_FinancialYearID);
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
            sException = "PurchaseDiscount GetPurchaseDiscount |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTopPurchaseDiscount(int PublisherID = 0, int iSupplierID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.PurchaseDiscount> ObjList = new Collection<VHMS.Entity.Billing.PurchaseDiscount>();
                ObjList = VHMS.DataAccess.Billing.PurchaseDiscount.GetTopPurchaseDiscount(PublisherID, iSupplierID, FK_FinancialYearID);
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
            sException = "PurchaseDiscount GetPurchaseDiscount |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSearchPurchaseDiscount(string PublisherID, int iSupplierID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.PurchaseDiscount> ObjList = new Collection<VHMS.Entity.Billing.PurchaseDiscount>();
                ObjList = VHMS.DataAccess.Billing.PurchaseDiscount.GetSearchPurchaseDiscount(PublisherID, iSupplierID, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "PurchaseDiscount GetPurchaseDiscount |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddPurchaseDiscount(VHMS.Entity.Billing.PurchaseDiscount Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iPurchaseDiscountId = 0;
        int iUserId = 0;
        int FK_FinancialYearID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;
                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;
                iPurchaseDiscountId = VHMS.DataAccess.Billing.PurchaseDiscount.AddPurchaseDiscount(Objdata);
                if (iPurchaseDiscountId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iPurchaseDiscountId.ToString();
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
            sException = "PurchaseDiscount AddPurchaseDiscount |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "PurchaseDiscount_A_01")
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
    public string GetPurchaseDiscountByID(int ID, int Type)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.PurchaseDiscount objPurchaseDiscount = new VHMS.Entity.Billing.PurchaseDiscount();
                objPurchaseDiscount = VHMS.DataAccess.Billing.PurchaseDiscount.GetPurchaseDiscountByID(ID, Type, FK_FinancialYearID);
                if (objPurchaseDiscount.PurchaseDiscountID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objPurchaseDiscount);
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
            sException = "PurchaseDiscount GetPurchaseDiscountByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string UpdatePurchaseDiscount(VHMS.Entity.Billing.PurchaseDiscount Objdata)
    {
        string sPurchaseDiscountId = string.Empty;
        string sException = string.Empty;
        bool bPurchaseDiscount = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0; int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;
                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;
                bPurchaseDiscount = VHMS.DataAccess.Billing.PurchaseDiscount.UpdatePurchaseDiscount(Objdata);
                if (bPurchaseDiscount == true)
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
            sException = "PurchaseDiscount UpdatePurchaseDiscount |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "PurchaseDiscount_U_01")
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
    public string DeletePurchaseDiscount(int ID)
    {
        string sLREntryId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bLREntry = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bLREntry = VHMS.DataAccess.Billing.PurchaseDiscount.DeletePurchaseDiscount(ID, iUserId);
                if (bLREntry == true)
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
            sException = "LREntry DeleteLREntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "PurchaseDiscount_R_01" || ex.Message.ToString() == "PurchaseDiscount_D_01")
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

    #region "SalesDiscount"
    public string GetSalesDiscount(int PublisherID = 0, int iSupplierID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.SalesDiscount> ObjList = new Collection<VHMS.Entity.Billing.SalesDiscount>();
                ObjList = VHMS.DataAccess.Billing.SalesDiscount.GetSalesDiscount(PublisherID, iSupplierID, FK_FinancialYearID);
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
            sException = "SalesDiscount GetSalesDiscount |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTopSalesDiscount(int PublisherID = 0, int iSupplierID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.SalesDiscount> ObjList = new Collection<VHMS.Entity.Billing.SalesDiscount>();
                ObjList = VHMS.DataAccess.Billing.SalesDiscount.GetTopSalesDiscount(PublisherID, iSupplierID, FK_FinancialYearID);
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
            sException = "SalesDiscount GetTopSalesDiscount |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetSearchSalesDiscount(string PublisherID, int iSupplierID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.SalesDiscount> ObjList = new Collection<VHMS.Entity.Billing.SalesDiscount>();
                ObjList = VHMS.DataAccess.Billing.SalesDiscount.GetSearchSalesDiscount(PublisherID, iSupplierID, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "SalesDiscount GetSearchSalesDiscount |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddSalesDiscount(VHMS.Entity.Billing.SalesDiscount Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iSalesDiscountId = 0;
        int iUserId = 0;
        int FK_FinancialYearID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;
                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;
                iSalesDiscountId = VHMS.DataAccess.Billing.SalesDiscount.AddSalesDiscount(Objdata);
                if (iSalesDiscountId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iSalesDiscountId.ToString();
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
            sException = "SalesDiscount AddSalesDiscount |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "SalesDiscount_A_01")
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
    public string GetSalesDiscountByID(int ID, int Type)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.SalesDiscount objSalesDiscount = new VHMS.Entity.Billing.SalesDiscount();
                objSalesDiscount = VHMS.DataAccess.Billing.SalesDiscount.GetSalesDiscountByID(ID, Type, FK_FinancialYearID);
                if (objSalesDiscount.SalesDiscountID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objSalesDiscount);
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
            sException = "SalesDiscount GetSalesDiscountByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string UpdateSalesDiscount(VHMS.Entity.Billing.SalesDiscount Objdata)
    {
        string sSalesDiscountId = string.Empty;
        string sException = string.Empty;
        bool bSalesDiscount = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0; int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;
                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;
                bSalesDiscount = VHMS.DataAccess.Billing.SalesDiscount.UpdateSalesDiscount(Objdata);
                if (bSalesDiscount == true)
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
            sException = "SalesDiscount UpdateSalesDiscount |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "SalesDiscount_U_01")
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
    public string GetSalesDiscountBillNo(int iCustomerID = 0, int SalesReturnID = 0, int IsRetailBill = 1, int IsActive = 1)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.SalesEntry> ObjList = new Collection<VHMS.Entity.Billing.SalesEntry>();
                ObjList = VHMS.DataAccess.Billing.SalesEntry.GetSalesDiscountBillNo(iCustomerID, IsRetailBill, SalesReturnID, 0, IsActive);
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
            sException = "Purchase GetPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string DeleteSalesDiscount(int ID)
    {
        string sLREntryId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bLREntry = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bLREntry = VHMS.DataAccess.Billing.SalesDiscount.DeleteSalesDiscount(ID, iUserId);
                if (bLREntry == true)
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
            sException = "LREntry DeleteLREntry |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "SalesDiscount_R_01" || ex.Message.ToString() == "SalesDiscount_D_01")
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
    public string GetPendingSalesDiscountBillNo(int CustomerID = 0, int IsRetailBill = 1, int SalesReturnID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.SalesEntry> ObjList = new Collection<VHMS.Entity.Billing.SalesEntry>();
                ObjList = VHMS.DataAccess.Billing.SalesEntry.GetPendingSalesDiscountBillNo(CustomerID, IsRetailBill, SalesReturnID, FK_FinancialYearID);
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
            sException = "SalesEntry GetPurchase |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    #endregion

    #region "Outward"
    public string GetOutward(int PublisherID = 0, int IsRetail = 0, int IsYarnBill = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Outward> ObjList = new Collection<VHMS.Entity.Billing.Outward>();
                ObjList = VHMS.DataAccess.Billing.Outward.GetOutward(PublisherID, IsRetail, IsYarnBill, FK_FinancialYearID);
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
            sException = "Outward GetOutward |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTopOutward(int PublisherID = 0, int IsRetail = 0, int IsYarnBill = 0, int iCustomerID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Outward> ObjList = new Collection<VHMS.Entity.Billing.Outward>();
                ObjList = VHMS.DataAccess.Billing.Outward.GetTopOutward(PublisherID, IsRetail, IsYarnBill, iCustomerID, FK_FinancialYearID);
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
            sException = "Outward GetOutward |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchOutward(string ID = null, int IsRetail = 0, int IsYarnBill = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.Outward> ObjList = new Collection<VHMS.Entity.Billing.Outward>();
                ObjList = VHMS.DataAccess.Billing.Outward.SearchOutward(ID, IsRetail, IsYarnBill, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "Outward GetOutward |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetOutwardByID(int ID, int IsRetail = 0, int IsYarnBill = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.Outward objOutward = new VHMS.Entity.Billing.Outward();
                objOutward = VHMS.DataAccess.Billing.Outward.GetOutwardByID(ID, IsRetail, IsYarnBill, FK_FinancialYearID);
                if (objOutward.OutwardID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objOutward);
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
            sException = "Outward GetOutwardByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddOutward(VHMS.Entity.Billing.Outward Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iOutwardId = 0;
        int iUserId = 0;
        int FK_FinancialYearID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                iOutwardId = VHMS.DataAccess.Billing.Outward.AddOutward(Objdata);
                if (iOutwardId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iOutwardId.ToString();
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
            sException = "Outward AddOutward |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Outward_A_01")
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
    public string UpdateOutward(VHMS.Entity.Billing.Outward Objdata)
    {
        string sOutwardId = string.Empty;
        string sException = string.Empty;
        bool bOutward = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                bOutward = VHMS.DataAccess.Billing.Outward.UpdateOutward(Objdata);
                if (bOutward == true)
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
            sException = "Outward UpdateOutward |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Outward_U_01")
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
    public string DeleteOutward(int ID, string Reason)
    {
        string sOutwardId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bOutward = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bOutward = VHMS.DataAccess.Billing.Outward.DeleteOutward(ID, Reason, iUserId);
                if (bOutward == true)
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
            sException = "Outward DeleteOutward |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "Outward_R_01" || ex.Message.ToString() == "Outward_D_01")
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

    #region "RetailOutward"
    public string GetRetailOutward(int PublisherID = 0, int IsRetail = 0, int IsYarnBill = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.RetailOutward> ObjList = new Collection<VHMS.Entity.Billing.RetailOutward>();
                ObjList = VHMS.DataAccess.Billing.RetailOutward.GetRetailOutward(PublisherID, IsRetail, IsYarnBill, FK_FinancialYearID);
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
            sException = "RetailOutward GetRetailOutward |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTopRetailOutward(int PublisherID = 0, int IsRetail = 0, int IsYarnBill = 0, int iCustomerID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.RetailOutward> ObjList = new Collection<VHMS.Entity.Billing.RetailOutward>();
                ObjList = VHMS.DataAccess.Billing.RetailOutward.GetTopRetailOutward(PublisherID, IsRetail, IsYarnBill, iCustomerID, FK_FinancialYearID);
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
            sException = "RetailOutward GetRetailOutward |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchRetailOutward(string ID = null, int IsRetail = 0, int IsYarnBill = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.RetailOutward> ObjList = new Collection<VHMS.Entity.Billing.RetailOutward>();
                ObjList = VHMS.DataAccess.Billing.RetailOutward.SearchRetailOutward(ID, IsRetail, IsYarnBill, FK_FinancialYearID);
                objResponse.Status = "Success";
                jsObject.MaxJsonLength = Int32.MaxValue;
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
            sException = "RetailOutward GetRetailOutward |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetRetailOutwardByID(int ID, int IsRetail = 0, int IsYarnBill = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.RetailOutward objRetailOutward = new VHMS.Entity.Billing.RetailOutward();
                objRetailOutward = VHMS.DataAccess.Billing.RetailOutward.GetRetailOutwardByID(ID, IsRetail, IsYarnBill, FK_FinancialYearID);
                if (objRetailOutward.RetailOutwardID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objRetailOutward);
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
            sException = "RetailOutward GetRetailOutwardByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetLastRetailOutwardByID(int ID, int IsRetail = 0, int IsYarnBill = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.RetailOutward objRetailOutward = new VHMS.Entity.Billing.RetailOutward();
                objRetailOutward = VHMS.DataAccess.Billing.RetailOutward.GetLastRetailOutwardByID(ID, IsRetail, IsYarnBill, FK_FinancialYearID);
                if (objRetailOutward.RetailOutwardID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objRetailOutward);
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
            sException = "RetailOutward GetRetailOutwardByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddRetailOutward(VHMS.Entity.Billing.RetailOutward Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iRetailOutwardId = 0;
        int iUserId = 0;
        int FK_FinancialYearID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                iRetailOutwardId = VHMS.DataAccess.Billing.RetailOutward.AddRetailOutward(Objdata);
                if (iRetailOutwardId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iRetailOutwardId.ToString();
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
            sException = "RetailOutward AddRetailOutward |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "RetailOutward_A_01")
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
    public string UpdateRetailOutward(VHMS.Entity.Billing.RetailOutward Objdata)
    {
        string sRetailOutwardId = string.Empty;
        string sException = string.Empty;
        bool bRetailOutward = false;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                bRetailOutward = VHMS.DataAccess.Billing.RetailOutward.UpdateRetailOutward(Objdata);
                if (bRetailOutward == true)
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
            sException = "RetailOutward UpdateRetailOutward |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "RetailOutward_U_01")
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
    public string DeleteRetailOutward(int ID, string Reason)
    {
        string sRetailOutwardId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bRetailOutward = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bRetailOutward = VHMS.DataAccess.Billing.RetailOutward.DeleteRetailOutward(ID, Reason, iUserId);
                if (bRetailOutward == true)
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
            sException = "RetailOutward DeleteRetailOutward |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "RetailOutward_R_01" || ex.Message.ToString() == "RetailOutward_D_01")
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

    #region "PunchLog"
    public string GetPunchLog(int CountryID = 0, string FromDate = "", string ToDate = "", int iEmployeeID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.PunchLog> ObjList = new Collection<VHMS.Entity.PunchLog>();
                ObjList = VHMS.DataAccess.PunchLog.GetPunchLog(CountryID, FromDate, ToDate, iEmployeeID);
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
            sException = "PunchLog GetPunchLog |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetPunchLogByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.PunchLog objPunchLog = new VHMS.Entity.PunchLog();
                objPunchLog = VHMS.DataAccess.PunchLog.GetPunchLogByID(ID);
                if (objPunchLog.PunchLogID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objPunchLog);
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
            sException = "PunchLog GetPunchLogByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddPunchLog(VHMS.Entity.PunchLog Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iPurchaseId = 0;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;
                iPurchaseId = VHMS.DataAccess.PunchLog.AddPunchLog(Objdata);
                if (iPurchaseId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iPurchaseId.ToString();
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
            sException = "PunchLog AddPunchLog |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "PunchLogFinal_A_01")
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

    #endregion

    #region "PurchaseTDSPayment"
    public string GetPurchaseTDSPayment(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.PurchaseTDSPayment> ObjList = new Collection<VHMS.Entity.Billing.PurchaseTDSPayment>();
                ObjList = VHMS.DataAccess.Billing.PurchaseTDSPayment.GetPurchaseTDSPayment(PublisherID, FK_FinancialYearID);
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
            sException = "PurchaseTDSPayment GetPurchaseTDSPayment |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetTopPurchaseTDSPayment(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iUserId = 0;
        int iBranchID = 0;
        int FK_FinancialYearID = 0;
        try
        {

            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                if (iUserId <= 1)
                    iBranchID = 0;
                Collection<VHMS.Entity.Billing.PurchaseTDSPayment> ObjList = new Collection<VHMS.Entity.Billing.PurchaseTDSPayment>();
                ObjList = VHMS.DataAccess.Billing.PurchaseTDSPayment.GetTopPurchaseTDSPayment(PublisherID, iBranchID, FK_FinancialYearID);
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
            sException = "PurchaseTDSPayment GetPurchaseTDSPayment |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetPurchaseTDSPaymentID(int PublisherID = 0)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                Collection<VHMS.Entity.Billing.PurchaseTDSPayment> ObjList = new Collection<VHMS.Entity.Billing.PurchaseTDSPayment>();
                ObjList = VHMS.DataAccess.Billing.PurchaseTDSPayment.GetPurchaseTDSPaymentID(PublisherID, FK_FinancialYearID);
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
            sException = "PurchaseTDSPayment GetPurchaseTDSPayment |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string SearchPurchaseTDSPayment(string ID = null)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iUserId = 0;
        int iBranchID = 0;
        int FK_FinancialYearID = 0;
        try
        {


            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                if (iUserId <= 1)
                    iBranchID = 0;
                Collection<VHMS.Entity.Billing.PurchaseTDSPayment> ObjList = new Collection<VHMS.Entity.Billing.PurchaseTDSPayment>();
                ObjList = VHMS.DataAccess.Billing.PurchaseTDSPayment.SearchPurchaseTDSPayment(ID, iBranchID, FK_FinancialYearID);
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
            sException = "PurchaseTDSPayment GetPurchaseTDSPayment |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetPurchaseTDSPaymentByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.Billing.PurchaseTDSPayment objPurchaseTDSPayment = new VHMS.Entity.Billing.PurchaseTDSPayment();
                objPurchaseTDSPayment = VHMS.DataAccess.Billing.PurchaseTDSPayment.GetPurchaseTDSPaymentByID(ID, FK_FinancialYearID);
                if (objPurchaseTDSPayment.TDSPaymentID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objPurchaseTDSPayment);
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
            sException = "PurchaseTDSPayment GetPurchaseTDSPaymentByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddPurchaseTDSPayment(VHMS.Entity.Billing.PurchaseTDSPayment Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iPurchaseTDSPaymentId = 0;
        int iUserId = 0;
        int iBranchID = 0;
        int FK_FinancialYearID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserId;
                Objdata.CreatedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                iPurchaseTDSPaymentId = VHMS.DataAccess.Billing.PurchaseTDSPayment.AddPurchaseTDSPayment(Objdata);
                if (iPurchaseTDSPaymentId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iPurchaseTDSPaymentId.ToString();
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
            sException = "PurchaseTDSPayment AddPurchaseTDSPayment |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "PurchaseTDSPayment_A_01")
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
    public string UpdatePurchaseTDSPayment(VHMS.Entity.Billing.PurchaseTDSPayment Objdata)
    {
        string sPurchaseTDSPaymentId = string.Empty;
        string sException = string.Empty;
        bool bPurchaseTDSPayment = false;
        int iBranchID = 0;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            int UserId = 0;
            int FK_FinancialYearID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserId) && Int32.TryParse(HttpContext.Current.Session["BranchID"].ToString(), out iBranchID) && Int32.TryParse(HttpContext.Current.Session["FK_FinancialYearID"].ToString(), out FK_FinancialYearID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = UserId;
                Objdata.ModifiedBy = objUser;

                VHMS.Entity.FinancialYear objFinancialYear = new VHMS.Entity.FinancialYear();
                objFinancialYear.FinancialYearID = FK_FinancialYearID;
                Objdata.FinancialYear = objFinancialYear;

                bPurchaseTDSPayment = VHMS.DataAccess.Billing.PurchaseTDSPayment.UpdatePurchaseTDSPayment(Objdata);
                if (bPurchaseTDSPayment == true)
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
            sException = "PurchaseTDSPayment UpdatePurchaseTDSPayment |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "PurchaseTDSPayment_U_01")
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
    public string DeletePurchaseTDSPayment(int ID)
    {
        string sPurchaseTDSPaymentId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bPurchaseTDSPayment = false;
        int iUserId = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserId))
            {
                bPurchaseTDSPayment = VHMS.DataAccess.Billing.PurchaseTDSPayment.DeletePurchaseTDSPayment(ID);
                if (bPurchaseTDSPayment == true)
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
            sException = "PurchaseTDSPayment DeletePurchaseTDSPayment |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "PurchaseTDSPayment_R_01" || ex.Message.ToString() == "PurchaseTDSPayment_D_01")
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

    #region "HSNMaster"
    public string GetHSNMaster()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.HSNMaster> ObjList = new Collection<VHMS.Entity.Billing.HSNMaster>();
                ObjList = VHMS.DataAccess.Billing.HSNMaster.GetHSNMaster();
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
            sException = "VHMSService.Billing.HSNMaster GetHSNMaster |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetHSNMasterByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.HSNMaster objHSNMaster = new VHMS.Entity.Billing.HSNMaster();
                objHSNMaster = VHMS.DataAccess.Billing.HSNMaster.GetHSNMasterByID(ID);
                if (objHSNMaster.HSNMasterID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objHSNMaster);
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
            sException = "VHMSService.Billing.HSNMaster GetHSNMasterByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddHSNMaster(VHMS.Entity.Billing.HSNMaster Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iHSNMasterId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iHSNMasterId = VHMS.DataAccess.Billing.HSNMaster.AddHSNMaster(Objdata);
                if (iHSNMasterId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iHSNMasterId.ToString();
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
            sException = "VHMSService.Billing.HSNMaster AddHSNMaster |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "HSNMaster_A_01")
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
    public string UpdateHSNMaster(VHMS.Entity.Billing.HSNMaster Objdata)
    {
        string sHSNMasterId = string.Empty;
        string sException = string.Empty;
        bool bHSNMaster = false;
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
                bHSNMaster = VHMS.DataAccess.Billing.HSNMaster.UpdateHSNMaster(Objdata);
                if (bHSNMaster == true)
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
            sException = "VHMSService.Billing.HSNMaster UpdateHSNMaster |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "HSNMaster_U_01")
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
    public string DeleteHSNMaster(int ID)
    {
        string sHSNMasterId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bHSNMaster = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bHSNMaster = VHMS.DataAccess.Billing.HSNMaster.DeleteHSNMaster(ID, UserID);
                if (bHSNMaster == true)
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
            sException = "VHMSService.Billing.HSNMaster DeleteHSNMaster |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "HSNMaster_R_01" || ex.Message.ToString() == "HSNMaster_D_01")
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

    #region "BuyerContactType"
    public string GetBuyerContactType()
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                Collection<VHMS.Entity.Billing.BuyerContactType> ObjList = new Collection<VHMS.Entity.Billing.BuyerContactType>();
                ObjList = VHMS.DataAccess.Billing.BuyerContactType.GetBuyerContactType();
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
            sException = "VHMSService.Billing.BuyerContactType GetBuyerContactType |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string GetBuyerContactTypeByID(int ID)
    {
        string sException = string.Empty;
        string sFileNames = string.Empty;
        JavaScriptSerializer jsObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        try
        {
            if (ValidateSession())
            {
                VHMS.Entity.Billing.BuyerContactType objBuyerContactType = new VHMS.Entity.Billing.BuyerContactType();
                objBuyerContactType = VHMS.DataAccess.Billing.BuyerContactType.GetBuyerContactTypeByID(ID);
                if (objBuyerContactType.BuyerContactTypeID > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = jsObject.Serialize(objBuyerContactType);
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
            sException = "VHMSService.Billing.BuyerContactType GetBuyerContactTypeByID |" + ex.Message.ToString();
            Log.Write(sException);
            objResponse.Status = "Error";
            objResponse.Value = "Error";
        }
        return jsObject.Serialize(objResponse);
    }
    public string AddBuyerContactType(VHMS.Entity.Billing.BuyerContactType Objdata)
    {
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        int iBuyerContactTypeId = 0;
        int iUserID = 0;
        try
        {
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out iUserID))
            {
                VHMS.Entity.User objUser = new VHMS.Entity.User();
                objUser.UserID = iUserID;
                Objdata.CreatedBy = objUser;
                iBuyerContactTypeId = VHMS.DataAccess.Billing.BuyerContactType.AddBuyerContactType(Objdata);
                if (iBuyerContactTypeId > 0)
                {
                    objResponse.Status = "Success";
                    objResponse.Value = iBuyerContactTypeId.ToString();
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
            sException = "VHMSService.Billing.BuyerContactType AddBuyerContactType |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "BuyerContactType_A_01")
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
    public string UpdateBuyerContactType(VHMS.Entity.Billing.BuyerContactType Objdata)
    {
        string sBuyerContactTypeId = string.Empty;
        string sException = string.Empty;
        bool bBuyerContactType = false;
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
                bBuyerContactType = VHMS.DataAccess.Billing.BuyerContactType.UpdateBuyerContactType(Objdata);
                if (bBuyerContactType == true)
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
            sException = "VHMSService.Billing.BuyerContactType UpdateBuyerContactType |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "BuyerContactType_U_01")
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
    public string DeleteBuyerContactType(int ID)
    {
        string sBuyerContactTypeId = string.Empty;
        string sException = string.Empty;
        JavaScriptSerializer jsonObject = new JavaScriptSerializer();
        VHMS.Entity.Response objResponse = new VHMS.Entity.Response();
        bool bBuyerContactType = false;
        try
        {
            int UserID = 0;
            if (ValidateSession() && Int32.TryParse(HttpContext.Current.Session["UserID"].ToString(), out UserID))
            {
                bBuyerContactType = VHMS.DataAccess.Billing.BuyerContactType.DeleteBuyerContactType(ID, UserID);
                if (bBuyerContactType == true)
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
            sException = "VHMSService.Billing.BuyerContactType DeleteBuyerContactType |" + ex.Message.ToString();
            Log.Write(sException);
            if (ex.Message.ToString() == "BuyerContactType_R_01" || ex.Message.ToString() == "BuyerContactType_D_01")
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
}