using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmPurchaseReturn : BaseConfig
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) VHMSService.AddPageVisitLog();
    }
    [WebMethod]
    public static string saveimage(string data)
    {
        byte[] imgarr = Convert.FromBase64String(data);
        string imageName = Guid.NewGuid().ToString("N") + ".png";
        String path = HttpContext.Current.Server.MapPath("~/images/Documents/PurchaseReturn");
        string imgPath = Path.Combine(path, imageName);
        File.WriteAllBytes(imgPath, imgarr);
        return "images/Documents/PurchaseReturn/" + imageName;
    }
}