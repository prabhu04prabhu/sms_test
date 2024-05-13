using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmBSupplier : BaseConfig
{
    protected string UploadFolderPath = "~/images/Documents/Supplier/";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) VHMSService.AddPageVisitLog();
    }
    protected void UploadedComplete(object sender, EventArgs e)
    {
        string imageName = Guid.NewGuid().ToString("N");
        //imgupload1.SaveAs(Server.MapPath(this.UploadFolderPath) + imageName + System.IO.Path.GetExtension(imgupload1.FileName));
        //Session["ProofPath"] = imageName + System.IO.Path.GetExtension(imgupload1.FileName);
    }

    [WebMethod(EnableSession = true)]
    public static string GetProofPath()
    {
        string url = HttpContext.Current.Session["ProofPath"].ToString();
        HttpContext.Current.Session["ProofPath"] = null;
        return url;
    }
    [WebMethod]
    public static string saveimage(string data)
    {
        byte[] imgarr = Convert.FromBase64String(data);
        string imageName = Guid.NewGuid().ToString("N") + ".png";
        String path = HttpContext.Current.Server.MapPath("~/images/Documents/Supplier/Images");
        string imgPath = Path.Combine(path, imageName);
        File.WriteAllBytes(imgPath, imgarr);
        return "images/Documents/Supplier/Images/" + imageName;
    }
}