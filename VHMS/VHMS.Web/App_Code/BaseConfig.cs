using System;
using System.Web;
using System.Data;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

public class BaseConfig : System.Web.UI.Page
{
    public BaseConfig()
    {
        // TODO: Add constructor logic here
        base.Load += new EventHandler(BaseConfigPage_Load);
        base.PreInit += new EventHandler(BaseConfigPage_PreInit);
    }
    void BaseConfigPage_PreInit(object sender, EventArgs e)
    {
    }
    protected void BaseConfigPage_Load(System.Object sender, System.EventArgs e)
    {
        string[] strFrmName = null;
        string strfrm = string.Empty;
        if (Session["UserID"] == null)
        {
            Log.Write("BaseConfigPage_Load | UserID Session Expired");
            Response.Redirect("frmLogin.aspx", true);
            return;
        }
        strFrmName = Request.Url.AbsolutePath.Split('/');
        strfrm = (strFrmName[strFrmName.GetUpperBound(0)]);
        if (Session["RoleID"] != null)
        {
            if (UserPrivilage(Convert.ToInt32(Session["RoleID"]), strfrm) == false)
            {
                Log.Write("This User does not have permission to Access this application");
                Response.Redirect(strfrm != null ? strfrm != "frmDefault.aspx" ? "frmAccessDenied.aspx" : "frmLogin.aspx" : "frmLogin.aspx", true);
            }
        }
        else
        {
            Response.Redirect("frmLogin.aspx", true);
        }
    }
    public static string GetConnectionString(string sKey)
    {
        string strEncConnectionString = string.Empty;
        try
        {
            strEncConnectionString = System.Configuration.ConfigurationManager.AppSettings[sKey];
            //strEncConnectionString = GetDecrypt(CRYPTO_KEY, strEncConnectionString);
        }
        catch (Exception)
        {
            throw;
        }
        return strEncConnectionString;
    }
    public static bool ValidateSession()
    {
        bool status = false;
        if (HttpContext.Current.Session["UserID"] != null)
        {
            status = true;
        }
        else
        {
            Log.Write("ValidateSession | UserID Session Expired");
        }
        return status;
    }
    public static string GetFileHash(string path)
    {
        string hash = (string)HttpContext.Current.Cache["_hash_" + path];

        if (hash == null)
        {
            // Get the physical path of the file
            string file = HttpContext.Current.Server.MapPath(path);

            // Code for MD5 hashing omitted for brevity
            hash = GetMD5(file);

            // Insert the hash into the Cache, with a dependency on the underlying file
            HttpContext.Current.Cache.Insert("_hash_" + path, hash, new System.Web.Caching.CacheDependency(file));
        }

        return hash;
    }
    public static string GetMD5(string file)
    {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read, 8192);
        md5.ComputeHash(stream);
        stream.Close();
        byte[] hash = md5.Hash;
        StringBuilder sb = new StringBuilder();
        foreach (byte b in hash)
        {
            sb.Append(string.Format("{0:X2}", b));
        }
        return sb.ToString();
    }
    public bool UserPrivilage(int RoleID, string sPath)
    {
        bool bResult = false;

        try
        {
            Session["ActionAccess"] = null;
            Session["ActionView"] = null;
            Session["ActionAdd"] = null;
            Session["ActionUpdate"] = null;
            Session["ActionDelete"] = null;

            DataSet dsPrivilege = VHMS.DataAccess.RoleConfiguration.GetRoleConfigurationByRoleID(RoleID);
            if (dsPrivilege.Tables.Count > 0 && dsPrivilege.Tables[0].Rows.Count > 0)
            {
                var qPrivilege = (from t in dsPrivilege.Tables[0].AsEnumerable()
                                  select new
                                  {
                                      fd_roleconfiguration_id = t.Field<int>("PK_RoleConfigurationID"),
                                      fd_menu_id = t.Field<int>("FK_MenuID"),
                                      fd_module_id = t.Field<int>("FK_ModuleID"),
                                      fd_filename = t.Field<string>("FileName"),
                                      fd_IsAccess = t.Field<bool>("IsAccess"),
                                      fd_IsView = t.Field<bool>("IsView"),
                                      fd_IsAdd = t.Field<bool>("IsAdd"),
                                      fd_IsEdit = t.Field<bool>("IsEdit"),
                                      fd_IsDelete = t.Field<bool>("IsDelete")
                                  }).Where(t => t.fd_filename == sPath && t.fd_IsAccess == true);

                if (qPrivilege.Count() > 0)
                {
                    foreach (var qitem in qPrivilege)
                    {
                        if (qitem.fd_filename == sPath && qitem.fd_IsAccess == true)
                        {
                            if (Convert.ToInt32(Session["FK_FinancialYearID"]) == Convert.ToInt32(Session["Current_FinancialYear"]))
                            {
                                Session["ActionAccess"] = qitem.fd_IsAccess ? 1 : 0;
                                Session["ActionView"] = qitem.fd_IsView ? 1 : 0;
                                Session["ActionAdd"] = qitem.fd_IsAdd ? 1 : 0;
                                Session["ActionUpdate"] = qitem.fd_IsEdit ? 1 : 0;
                                Session["ActionDelete"] = qitem.fd_IsDelete ? 1 : 0;
                            }
                            else
                            {
                                Session["ActionAccess"] = qitem.fd_IsAccess ? 1 : 0;
                                Session["ActionView"] = qitem.fd_IsView ? 1 : 0;
                                Session["ActionAdd"] =  0;
                                Session["ActionUpdate"] =  0;
                                Session["ActionDelete"] =  0;
                            }
                            bResult = true;
                            break;
                        }
                    }
                }
            }

            if (sPath == "frmDefault.aspx")
                bResult = true;
        }
        catch (Exception Ex)
        { Log.Write("BaseConfig UserPrivilage | " + Ex.ToString()); }

        return bResult;
    }
}