using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.Security;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Drawing;
using System.Web.Services;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Text;


public partial class frmPatient : BaseConfig
{
    protected string UploadFolderPath = "~/images/PatientPhotos/";
    protected string UploadFolderName = "";

    //protected string imageName = Guid.NewGuid().ToString("N");
    //protected string HimageName = Guid.NewGuid().ToString("N");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            if (Request.InputStream.Length > 0)
            {
                using (StreamReader reader = new StreamReader(Request.InputStream))
                {
                    string hexString = Server.UrlEncode(reader.ReadToEnd());
                    string imageName = Guid.NewGuid().ToString("N");
                    string imagePath = string.Format("~/images/PatientPhotos/{0}.png", imageName);
                    File.WriteAllBytes(Server.MapPath(imagePath), ConvertHexToBytes(hexString));
                    Session["CapturedImage"] = ResolveUrl(imagePath);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "clientScript", "alert('"+ imagePath + "');", true);
                }
            }
        }

    }
    private static byte[] ConvertHexToBytes(string hex)
    {
        byte[] bytes = new byte[hex.Length / 2];
        for (int i = 0; i < hex.Length; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        }
        return bytes;
    }

    [WebMethod(EnableSession = true)]
    public static string GetCapturedImage()
    {
        string url = HttpContext.Current.Session["CapturedImage"].ToString();
        HttpContext.Current.Session["CapturedImage"] = null;
        return url;
    }

    [WebMethod(EnableSession = true)]
    public static string HGetCapturedImage()
    {
        string url = HttpContext.Current.Session["CapturedImage"].ToString();
        HttpContext.Current.Session["CapturedImage"] = null;
        return url;
    }

    //  [WebMethod(EnableSession = true)]
    //public void LoadStream()
    //{
    //    if (Request.InputStream.Length > 0)
    //    {
    //        using (StreamReader reader = new StreamReader(Request.InputStream))
    //        {
    //            string hexString = Server.UrlEncode(reader.ReadToEnd());
    //            string imageName = DateTime.Now.ToString("dd-MM-yy hh-mm-ss");

    //            string imagePath = string.Format("~/images/PatientPhotos/{0}.png", imageName);
    //            File.WriteAllBytes(Server.MapPath(imagePath), ConvertHexToBytes(hexString));
    //            Session["CapturedImage"] = ResolveUrl(imagePath);
    //        }
    //    }

    //}

    protected void AsyncFileUpload1_UploadedComplete(object sender, EventArgs e)
    {

    }
    protected void HProof_UploadedComplete(object sender, EventArgs e)
    {
        string imageName = Guid.NewGuid().ToString("N");
        //if (HttpContext.Current.Session["LastOPDNo"] != null)
        //    hidOPDNo.Value = HttpContext.Current.Session["LastOPDNo"].ToString();
        //else
        //    hidOPDNo.Value = "190001";
        //string filename = "HP" + hidOPDNo.Value + System.IO.Path.GetExtension(HProof.FileName);
       // File.Delete(Server.MapPath(Path.Combine("~/images/PatientPhotos/", imageName + System.IO.Path.GetExtension(HProof.FileName))));
        HProof.SaveAs(Server.MapPath(this.UploadFolderPath) + imageName + System.IO.Path.GetExtension(HProof.FileName));
        lblStatus1.Text = "Image Uploaded";
        Session["ProofPath"] = imageName + System.IO.Path.GetExtension(HProof.FileName);
    }
    protected void WPhoto_UploadedComplete(object sender, EventArgs e)
    {
    }
    protected void WProof_UploadedComplete(object sender, EventArgs e)
    {
        string imageName = Guid.NewGuid().ToString("N");
        //if (HttpContext.Current.Session["LastOPDNo"] != null)
        //    hidOPDNo.Value = HttpContext.Current.Session["LastOPDNo"].ToString();
        //else
        //    hidOPDNo.Value = "190001";
        //string filename = "WP" + hidOPDNo.Value + System.IO.Path.GetExtension(WProof.FileName);
       // File.Delete(Server.MapPath(Path.Combine("~/images/PatientPhotos/", imageName + System.IO.Path.GetExtension(HProof.FileName))));
        WProof.SaveAs(Server.MapPath(this.UploadFolderPath) + imageName + System.IO.Path.GetExtension(WProof.FileName));
        lblWProofStatus.Text = "Image Uploaded";
        Session["ProofPath"] = imageName + System.IO.Path.GetExtension(WProof.FileName);
    }

    [WebMethod(EnableSession = true)]
    public static string GetProofPath()
    {
        string url = HttpContext.Current.Session["ProofPath"].ToString();
        HttpContext.Current.Session["ProofPath"] = null;
        return url;
    }

}