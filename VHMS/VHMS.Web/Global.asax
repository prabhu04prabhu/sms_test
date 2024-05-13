<%@ Application Language="C#" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        try
        {
            VHMS.Entity.DBConnection.ConnectionString = CommonMethods.Security.Decrypt(BaseConfig.GetConnectionString("DBConnectionString"), true);
            VHMS.Entity.DBConnection.dbCon = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(VHMS.Entity.DBConnection.ConnectionString);

            VHMS.Entity.DBConnection.LogDBConnectionString = CommonMethods.Security.Decrypt(BaseConfig.GetConnectionString("LogDBConnectionString"), true);
            VHMS.Entity.DBConnection.dbLogCon = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(VHMS.Entity.DBConnection.LogDBConnectionString);

             VHMS.Entity.DBConnection.AttendanceDBConnectionString = CommonMethods.Security.Decrypt(BaseConfig.GetConnectionString("AttendanceDBConnectionString"), true);
            VHMS.Entity.DBConnection.dbAttendanceCon = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(VHMS.Entity.DBConnection.AttendanceDBConnectionString);

            System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(VHMS.Entity.DBConnection.ConnectionString);
            VHMS.Entity.DBConnection.DatabaseName = builder.InitialCatalog;
        }
        catch (Exception ex)
        {
            Log.Write("Application_Start | " + ex.ToString());
            HttpContext.Current.Response.Redirect("frmLogin.aspx");
        }
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
        //EventLogHistory.InsertLogHistory("Application", "End", 0, 1, false);
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
        Log.Write("Application_Error | " + HttpContext.Current.Server.GetLastError().ToString());
        HttpContext.Current.Response.Redirect("frmLogin.aspx");
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {

    }       
</script>
