using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
public partial class frmPurchaseApprove : BaseConfig
{
    protected string UploadFolderPath = "~/images/Documents/Purchase/";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) VHMSService.AddPageVisitLog();
    }
    protected void UploadedComplete(object sender, EventArgs e)
    {
        string imageName = Guid.NewGuid().ToString("N");
        imgupload1.SaveAs(Server.MapPath(this.UploadFolderPath) + imageName + System.IO.Path.GetExtension(imgupload1.FileName));
        Session["ProofPath"] = imageName + System.IO.Path.GetExtension(imgupload1.FileName);
    }

    [WebMethod(EnableSession = true)]
    public static string GetProofPath()
    {
        string url = HttpContext.Current.Session["ProofPath"].ToString();
        HttpContext.Current.Session["ProofPath"] = null;
        return url;
    }
}