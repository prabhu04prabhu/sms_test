using System;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.IO;
using System.Xml;
using System.Data;
using System.Globalization;

public class Log
{
    public static void Write(string errorMessage)
    {
        string exception = string.Empty;
        try
        {
            LogEntry log = new LogEntry();
            log.EventId = 2;
            log.Message = errorMessage;
            Logger.Write(log);

        }
        catch (Exception Ex)
        {
            exception = "Log Write |" + Ex.Message.ToString();
        }
    }
}
