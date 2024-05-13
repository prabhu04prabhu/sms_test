using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmReceipt : BaseConfig
{
    protected string UploadFolderPath = "~/images/Documents/Receipt/";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) VHMSService.AddPageVisitLog();
    }
    protected void UploadedComplete(object sender, EventArgs e)
    {
        string imageName = Guid.NewGuid().ToString("N");
        imgUpload1.SaveAs(Server.MapPath(this.UploadFolderPath) + imageName + System.IO.Path.GetExtension(imgUpload1.FileName));
        Session["ProofPath"] = imageName + System.IO.Path.GetExtension(imgUpload1.FileName);
    }

    [WebMethod(EnableSession = true)]
    public static string GetProofPath()
    {
        string url = HttpContext.Current.Session["ProofPath"].ToString();
        HttpContext.Current.Session["ProofPath"] = null;
        return url;
    }

}