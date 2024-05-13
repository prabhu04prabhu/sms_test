using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Collections.ObjectModel;
using System.Net.Mail;
using CrystalDecisions.Shared;

public partial class frmINLREntry : BaseConfig
{
    protected string UploadFolderPath = "~/images/Documents/LR/";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) VHMSService.AddPageVisitLog();
    }
    [WebMethod]
    public static string saveimage(string data)
    {
        byte[] imgarr = Convert.FromBase64String(data);
        string imageName = Guid.NewGuid().ToString("N") + ".png";
        String path = HttpContext.Current.Server.MapPath("~/images/Documents/LR");
        string imgPath = Path.Combine(path, imageName);
        File.WriteAllBytes(imgPath, imgarr);
        return "images/Documents/LR/" + imageName;
    }

    [WebMethod(EnableSession = true)]
    public static string GetProofPath()
    {
        string url = HttpContext.Current.Session["ProofPath"].ToString();
        HttpContext.Current.Session["ProofPath"] = null;
        return url;
    }

    [WebMethod(EnableSession = true)]
    public static string SendEmail(int ID)
    {
        VHMS.Entity.Billing.LREntry objLREntry = new VHMS.Entity.Billing.LREntry();
        objLREntry = VHMS.DataAccess.Billing.LREntry.GetLREntryByID(ID);

        VHMS.Entity.Settings objSettings = new VHMS.Entity.Settings();
        objSettings = VHMS.DataAccess.Settings.GetSettings();
        if (objLREntry.Customer.Email.Length > 5)
        {
            String strToEmail = "", strToDisplayname;
            String Subject = "";
            strToEmail = objLREntry.Customer.Email;
            strToDisplayname = objLREntry.Customer.CustomerName;

            Subject = "LR No : " + objLREntry.LREntryNo + "";

            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            mail.From = new MailAddress(objSettings.UserMailID, objSettings.CompanyName);
            mail.To.Add(new MailAddress(strToEmail, ""));
            mail.Subject = Subject;
            string str = null;


            str = "<html><body>"
                    + "<div style='width: 600px; border: 1px #dceaf5 solid; padding: 15px; margin-left: 100px;'>"
                    + "<table rules='all' width='100%' cellpadding='10' style='color: #444444; border: 0px; font-size: 11pt; line-height: 20px; font-family: proxima_nova,Open Sans,Lucida Grande,Segoe UI,Arial,Verdana,Lucida Sans Unicode,Tahoma,Sans Serif;'>"
                    + "<tr><td colspan='7' style='color: #444444; border: 0px; font-size: 11pt; line-height: 20px; font-family: proxima_nova,Open Sans,Lucida Grande,Segoe UI,Arial,Verdana,Lucida Sans Unicode,Tahoma,Sans Serif;'>LR No        : " + objLREntry.LREntryNo + " </td></tr>"
                    + "<tr><td colspan='7' style='color: #444444; border: 0px; font-size: 11pt; line-height: 20px; font-family: proxima_nova,Open Sans,Lucida Grande,Segoe UI,Arial,Verdana,Lucida Sans Unicode,Tahoma,Sans Serif;'>Date         : " + objLREntry.LREntryDate.ToString("dd/MM/yyyy") + " </td></tr>"
                    + "<tr><td colspan='7' style='color: #444444; border: 0px; font-size: 11pt; line-height: 20px; font-family: proxima_nova,Open Sans,Lucida Grande,Segoe UI,Arial,Verdana,Lucida Sans Unicode,Tahoma,Sans Serif;'>Invoice Date : " + objLREntry.InvoiceDate.ToString("dd/MM/yyyy") + " </td></tr>"
                    + "<tr><td colspan='7' style='color: #444444; border: 0px; font-size: 11pt; line-height: 20px; font-family: proxima_nova,Open Sans,Lucida Grande,Segoe UI,Arial,Verdana,Lucida Sans Unicode,Tahoma,Sans Serif;'>Invoice No   : " + objLREntry.InvoiceNo + " </td></tr>"
                    + "<tr><td colspan='7' style='color: #444444; border: 0px; font-size: 11pt; line-height: 20px; font-family: proxima_nova,Open Sans,Lucida Grande,Segoe UI,Arial,Verdana,Lucida Sans Unicode,Tahoma,Sans Serif;'>Transport    :" + objLREntry.Transport + "</td></tr>"
                    + "<tr><td colspan='7' style='color: #444444; border: 0px; font-size: 11pt; line-height: 20px; font-family: proxima_nova,Open Sans,Lucida Grande,Segoe UI,Arial,Verdana,Lucida Sans Unicode,Tahoma,Sans Serif;'>Vehicle No   :    " + objLREntry.VehicleNo + "</td></tr>"
                    + "<tr><td colspan='7' style='color: #444444; border: 0px; font-size: 11pt; line-height: 20px; font-family: proxima_nova,Open Sans,Lucida Grande,Segoe UI,Arial,Verdana,Lucida Sans Unicode,Tahoma,Sans Serif;'>AWB No       :" + objLREntry.AWBNo + "</td></tr>";

            str += "<tr><td colspan='7' style='color: #444444; border: 0px; font-size: 11pt; line-height: 20px; font-family: proxima_nova,Open Sans,Lucida Grande,Segoe UI,Arial,Verdana,Lucida Sans Unicode,Tahoma,Sans Serif;'>"
              + "Sincerely,</td></tr>"
               + "<tr><td colspan='7' style='color: #444444; border: 0px; font-size: 11pt; line-height: 20px; font-family: proxima_nova,Open Sans,Lucida Grande,Segoe UI,Arial,Verdana,Lucida Sans Unicode,Tahoma,Sans Serif;'>"
              + objSettings.CompanyName + "</td></tr></table></div></body></html>";

            mail.Body = str;
            mail.IsBodyHtml = true;
            mail.Attachments.Add(new Attachment(objLREntry.DocumentPath));
            smtp.Host = objSettings.HostName;
            smtp.Port = 587;
            smtp.UseDefaultCredentials = objSettings.DefaultCrendentials;
            smtp.Credentials = new System.Net.NetworkCredential(objSettings.UserMailID, objSettings.UserMailPassword);
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.EnableSsl = objSettings.EnableSSL;
            smtp.Timeout = 20000;
            try
            {
                // Send the mail message
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                mail.Dispose();
            }


            
        }
        return "test";
    }

}