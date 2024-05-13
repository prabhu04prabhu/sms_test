using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using System.Collections;

public partial class frmAccessDenied : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) VHMSService.AddPageVisitLog();
    }

    private void ForInterview()
    {
        Collection<VHMS.Entity.Calendar> oCalendar = new Collection<VHMS.Entity.Calendar>();
        
    }
} 