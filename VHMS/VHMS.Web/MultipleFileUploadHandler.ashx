<%@ WebHandler Language="C#" Class="FileUploadHandler" %>

using System;
using System.Web;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;

public class FileUploadHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");

        if (context.Request.Files.Count > 0)
        {
            try
            {
                HttpFileCollection files = context.Request.Files;
                string fname = "", ifilename = "";
                FileUpload fp;
                Collection<FileUpload> objList = new Collection<FileUpload>();
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    string imageName = Guid.NewGuid().ToString("N");
                    fname = context.Server.MapPath("./images/ProductImages/" + imageName + file.FileName);
                    file.SaveAs(fname);
                    ifilename = "./images/ProductImages/" + imageName + file.FileName;

                    fp = new FileUpload();
                    fp.filename = file.FileName;
                    fp.filepath = ifilename;
                    objList.Add(fp);
                }
                JavaScriptSerializer jsObject = new JavaScriptSerializer();
                context.Response.ContentType = "text/plain";
                var lists = jsObject.Serialize(objList);
                context.Response.Write(jsObject.Serialize(objList));
            }
            catch(Exception ex)
            {
                ex.ToString();
            }

        }

    }
    public class FileUpload
    {
        public string filename { get; set; }
        public string filepath { get; set; }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}