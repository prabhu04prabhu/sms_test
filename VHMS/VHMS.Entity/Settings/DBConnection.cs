using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;

namespace VHMS.Entity
{
    public static class DBConnection
    {
        public static string ConnectionString { get; set; }
        public static Database dbCon { get; set; }
        public static string AttendanceDBConnectionString { get; set; }
        public static Database dbAttendanceCon { get; set; }
        public static string LogDBConnectionString { get; set; }
        public static Database dbLogCon { get; set; }
        public static DataSet DSForms { get; set; }
        public static DataSet DSFormPermission { get; set; }
        public static string ServerRootPath { get; set; }
        public const int ROWCOUNT = 100;
        public static string DatabaseName = string.Empty;      
    }

    public class Response
    {
        public string Status { get; set; }
        public string Value { get; set; }
        public string ADValueI { get; set; }
        public string ADValueII { get; set; }
        public string ADValueIII { get; set; }
    }

    public class SearchResult
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
    }
}
