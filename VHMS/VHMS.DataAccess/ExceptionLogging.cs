using System.IO;
using System;
using System.Reflection;
using System.Web;

public static class ExceptionLogging
{

    private static String ErrorlineNo, Errormsg, extype, exurl, hostIp, ErrorLocation, HostAdd;

    public static void SendErrorToText(Exception ex)
    {
        var line = Environment.NewLine + Environment.NewLine;

        ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
        Errormsg = ex.GetType().Name.ToString();
        extype = ex.GetType().ToString();
        ErrorLocation = ex.Message.ToString() + " " + ex.StackTrace.ToString();


        var m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        String path = HttpContext.Current.Server.MapPath("~/Log");
        try
        {
            using (StreamWriter w = File.AppendText(path + "\\" + "Errorlog.txt"))
            {
                w.Write("\r\nLog Entry : ");
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
                w.WriteLine("----");
                w.WriteLine("  {0}", ErrorLocation);
                w.WriteLine("-------------------------------");
            }
        }
        catch (Exception e)
        {
        }
    }
}