using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.IO;


public partial class frmProduct1 : BaseConfig
{
    protected string UploadFolderPath = "~/images/ProductImages/";
    protected string UploadFolderName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            //if (Request.InputStream.Length > 0)
            //{
            //    using (StreamReader reader = new StreamReader(Request.InputStream))
            //    {
            //        string hexString = Server.UrlEncode(reader.ReadToEnd());
            //        string imageName = Guid.NewGuid().ToString("N");
            //        string imagePath = string.Format("~/images/ProductImages/{0}.png", imageName);
            //        File.WriteAllBytes(Server.MapPath(imagePath), ConvertHexToBytes(hexString));
            //        Session["CapturedImage"] = ResolveUrl(imagePath);
            //        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "clientScript", "alert('" + imagePath + "');", true);
            //    }
            //}
        }
    }

    protected void UploadedComplete(object sender, EventArgs e)
    {
        string imageName = Guid.NewGuid().ToString("N");
        imgbtn1.SaveAs(Server.MapPath(this.UploadFolderPath) + imageName + System.IO.Path.GetExtension(imgbtn1.FileName));
        Session["CapturedImage"] = imageName + System.IO.Path.GetExtension(imgbtn1.FileName);
    }

    [WebMethod(EnableSession = true)]
    public static string GetProofPath()
    {
        string url = HttpContext.Current.Session["CapturedImage"].ToString();
        HttpContext.Current.Session["CapturedImage"] = null;
        return url;
    }

    protected void UploadedComplete1(object sender, EventArgs e)
    {
        string imageName = Guid.NewGuid().ToString("N");
        imgbtn2.SaveAs(Server.MapPath(this.UploadFolderPath) + imageName + System.IO.Path.GetExtension(imgbtn2.FileName));
        Session["ProofPath1"] = imageName + System.IO.Path.GetExtension(imgbtn2.FileName);
    }   

    [WebMethod(EnableSession = true)]
    public static string GetProofPath1()
    {
        string url = HttpContext.Current.Session["ProofPath1"].ToString();
        HttpContext.Current.Session["ProofPath1"] = null;
        return url;
    }

    [WebMethod(EnableSession = true)]
    public static string GetProofPath2()
    {
        string url = HttpContext.Current.Session["ProofPath2"].ToString();
        HttpContext.Current.Session["ProofPath2"] = null;
        return url;
    }

    protected void UploadedComplete2(object sender, EventArgs e)
    {
        string imageName = Guid.NewGuid().ToString("N");
        imgbtn3.SaveAs(Server.MapPath(this.UploadFolderPath) + imageName + System.IO.Path.GetExtension(imgbtn3.FileName));
        Session["ProofPath2"] = imageName + System.IO.Path.GetExtension(imgbtn3.FileName);
    }

    //private static byte[] ConvertHexToBytes(string hex)
    //{
    //    byte[] bytes = new byte[hex.Length / 2];
    //    for (int i = 0; i < hex.Length; i += 2)
    //    {
    //        bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
    //    }
    //    return bytes;
    //}

    //[WebMethod(EnableSession = true)]
    //public static string GetImage()
    //{
    //    string url = HttpContext.Current.Session["CapturedImage"].ToString();
    //    HttpContext.Current.Session["CapturedImage"] = null;
    //    return url;
    //}

}