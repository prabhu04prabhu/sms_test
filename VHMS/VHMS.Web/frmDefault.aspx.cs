using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VHMS.DataAccess;
using System.Text;
public partial class frmDefault : BaseConfig
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) VHMSService.AddPageVisitLog();
      //  GetRecentAdmission();        
    }
    private void GetRecentAdmission()
    {
        try
        {
            int RoleID = 0, UserID = 0;
            StringBuilder sbRecentAdmission = new StringBuilder();

            if (HttpContext.Current.Session["RoleID"] != null) RoleID = Convert.ToInt32(HttpContext.Current.Session["RoleID"].ToString());
            if (HttpContext.Current.Session["UserID"] != null) UserID = Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString());

            DataSet dsData = new DataSet();
            dsData = Framework.GetRecentAdmission(RoleID == 1 ? 0 : UserID);

            if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                var qRecentAdmissionData = (from t in dsData.Tables[0].AsEnumerable()
                                            select new
                                            {
                                                fd_AdmissionID = t.Field<Int32>("PK_AdmissionID"),
                                                fd_AdmissionNo = t.Field<string>("AdmissionNo"),
                                                fd_UHIDNo = t.Field<string>("UHIDNo"),
                                                fd_RoomNo = t.Field<string>("RoomNo"),
                                                fd_PatientName = t.Field<string>("PatientName"),
                                                fd_PatientAge = t.Field<byte>("PatientAge"),
                                                fd_PatientSex = t.Field<byte>("PatientSex"),
                                                fd_DateofAdmission = t.Field<DateTime>("DateofAdmission"),
                                                fd_PrimaryConsultant = t.Field<string>("PrimaryConsultant"),
                                                fd_SummaryType = t.Field<string>("SummaryType")
                                            });

                sbRecentAdmission.Append("<div class='table-responsive'><table id='tblRecentAdmission' class='table no-margin table-condensed table-striped'>");
                sbRecentAdmission.Append("<thead><tr class='bg-light-blue-active color-palette'><th>S.No</th>");
                sbRecentAdmission.Append("<th>Admission No</th>");
                sbRecentAdmission.Append("<th>UHID #</th>");
                sbRecentAdmission.Append("<th>Room No</th>");
                sbRecentAdmission.Append("<th>Patient Name</th>");
                sbRecentAdmission.Append("<th>Age</th>");
                sbRecentAdmission.Append("<th>Gender</th>");
                sbRecentAdmission.Append("<th>Primary Consultant</th>");
                sbRecentAdmission.Append("<th>Admission Date</th>");
                sbRecentAdmission.Append("<th>Time</th>");
                sbRecentAdmission.Append("<th>Summary Type</th>");
                sbRecentAdmission.Append("</tr></thead><tbody>");

                int SNo = 1;
                foreach (var qRecord in qRecentAdmissionData)
                {
                    sbRecentAdmission.Append("<tr id=" + qRecord.fd_AdmissionID + ">");
                    sbRecentAdmission.Append("<td>" + SNo.ToString() + "</td>");
                    sbRecentAdmission.Append("<td>" + qRecord.fd_AdmissionNo + "</td>");
                    sbRecentAdmission.Append("<td>" + qRecord.fd_UHIDNo + "</td>");
                    sbRecentAdmission.Append("<td>" + qRecord.fd_RoomNo + "</td>");
                    sbRecentAdmission.Append("<td>" + qRecord.fd_PatientName + "</td>");
                    sbRecentAdmission.Append("<td>" + qRecord.fd_PatientAge.ToString() + "</td>");
                    sbRecentAdmission.Append("<td>" + (qRecord.fd_PatientSex == 1 ? "M" : "F") + "</td>");
                    sbRecentAdmission.Append("<td>" + qRecord.fd_PrimaryConsultant + "</td>");
                    if (qRecord.fd_DateofAdmission.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        sbRecentAdmission.Append("<td>" + qRecord.fd_DateofAdmission.ToString("dd/MM/yyyy") + "</td>");
                        sbRecentAdmission.Append("<td>" + qRecord.fd_DateofAdmission.ToString("HH:mm") + "</td>");
                    }
                    else
                    {
                        sbRecentAdmission.Append("<td></td>");
                        sbRecentAdmission.Append("<td></td>");
                    }
                    sbRecentAdmission.Append("<td>" + qRecord.fd_SummaryType + "</td>");
                    sbRecentAdmission.Append("</tr>");
                    SNo++;
                }

                if (qRecentAdmissionData.Count() > 0) sbRecentAdmission.Append("<tr></tr><tr></tr>");
                sbRecentAdmission.Append("</tbody></table></div>");
                //divRecentAdmission.InnerHtml = sbRecentAdmission.ToString();
            }
        }
        catch (Exception ex)
        { Log.Write("frmDefault GetRecentAdmission | " + ex.ToString()); }
    }
}