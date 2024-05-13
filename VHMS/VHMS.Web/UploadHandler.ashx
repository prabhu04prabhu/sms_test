<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Web;
using System.IO;

public class UploadHandler : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        string path = context.Request["path"];
        if (path != null && path.Length > 0)
        {
            FileInfo fn = new FileInfo(path);

            string imageName = Guid.NewGuid().ToString("N");
            string extension = fn.Extension;
            fn.CopyTo(context.Server.MapPath("~/images/PatientPhotos/" + imageName + extension));
            context.Response.Write("~/images/PatientPhotos/" + imageName + extension);
        }
        else
        {
            context.Response.Write("");
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}