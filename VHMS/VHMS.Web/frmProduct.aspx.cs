using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.IO;

public partial class frmProduct : BaseConfig
{
    protected string UploadFolderPath = "~/images/ProductImages/";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) VHMSService.AddPageVisitLog();
    }

    [WebMethod]
    public static string saveimage(string data)
    {
        byte[] imgarr = Convert.FromBase64String(data);
        string imageName = Guid.NewGuid().ToString("N") + ".png";
        String path = HttpContext.Current.Server.MapPath("~/images/ProductImages");
        string imgPath = Path.Combine(path, imageName);
        File.WriteAllBytes(imgPath, imgarr);
        return "images/ProductImages/" + imageName;
    }

    [WebMethod]
    public static string[] GetBarcode(string prefix)
    {
        List<string> customers = new List<string>();
        customers = VHMS.DataAccess.Master.Product.GetProductNameList(prefix);
        return customers.ToArray();
    }
}