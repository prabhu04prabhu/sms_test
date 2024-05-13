using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class frmLetterPad_old : BaseConfig
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) VHMSService.AddPageVisitLog();
        AddHeaderFooter();
    }


    protected void btntest_Click(object sender, EventArgs e)
    {
        string test = html_Description.Content;
        Response.Clear();
        divOPInvoice.InnerHtml = html_Description.Content;
        Response.ContentType = "text/html";
        var strBuilder = new StringBuilder();
        var strWriter = new StringWriter(strBuilder);
        var htmlWriter = new HtmlTextWriter(strWriter);
        var streamWriter = new StreamWriter(Response.OutputStream);
        var javascript = @"<script type='text/javascript'>window.onload = function() {setTimeout(function () { window.print(); }, 2000);}</script>";
        var stylecss = @"<link href='css/Print.css' rel='stylesheet type='text/css' /><link href='css/fonts/font-awesome.min.css' rel='stylesheet type='text/css' />";
        htmlWriter.Write(javascript);
        htmlWriter.Write(stylecss);
        divPdf.RenderControl(htmlWriter);
        streamWriter.Write(strBuilder.ToString());
        streamWriter.Flush();
        Response.End();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Response.Clear();
        divOPInvoice.InnerHtml = html_Description.Content;
        Response.ContentType = "text/html";
        var strBuilder = new StringBuilder();
        var strWriter = new StringWriter(strBuilder);
        var htmlWriter = new HtmlTextWriter(strWriter);
        var streamWriter = new StreamWriter(Response.OutputStream);
        var javascript = @"<script type='text/javascript'>window.onload = function() {setTimeout(function () { window.print(); }, 2000);}</script>";
        var stylecss = @"<link href='css/Print.css' rel='stylesheet type='text/css' /><link href='css/fonts/font-awesome.min.css' rel='stylesheet type='text/css' />";
        htmlWriter.Write(javascript);
        htmlWriter.Write(stylecss);
        divPdf.RenderControl(htmlWriter);
        streamWriter.Write(strBuilder.ToString());
        streamWriter.Flush();
        Response.End();
    }
    public void AddHeaderFooter()
    {
        VHMS.Entity.Company ObjList = new VHMS.Entity.Company();
        ObjList = VHMS.DAL.Company.GetCompany();
        string TableHTML = "";

        TableHTML += "<table cellpadding='0' cellspacing='0' style='width:100%;margin-left:0%'>";
        //TableHTML += "<tr valign='middle'><th rowspan='3' align='right' style='width:25%';background:white;><img src='images/smslogo.jpg' width='100%' alt=''/></th><th style='font-size:19px;text-align:center;background:white;'>" + ObjList.CompanyName + "</th></tr>";
        //TableHTML += "<tr><th style='text-align:center;background:white;'>" + ObjList.CompanyAddress + "</th></tr>";
        //    if (ObjList.PhoneNo2.Length > 2)
        //        TableHTML += "<tr><th style='text-align:center;background:white;'> Phone :" + ObjList.PhoneNo1 + "/" + ObjList.PhoneNo2 + "</th></tr>";
        //    else
        //        TableHTML += "<tr><th style='text-align:center;background:white;'> Phone :" + ObjList.PhoneNo1 + "</th></tr>";


        if (ObjList.PhoneNo2.Length <= 0)
            TableHTML += "<tr valign='middle'><th rowspan='3' align='right' style='padding-left:10px;width:10%'><img src='images/meenakchi-amman-1.jpg' width='100%' alt=''/></th><th rowspan='3' align='right' style='padding-left:10px;width:15%'><img src='images/5.png' width='100%' alt=''/></th><th style='font-size:19px;text-align:center;width: 48%;height: 1%;'>" + ObjList.CompanyName + "</th><th style='text-align:end;height: 1%;'> " + ObjList.PhoneNo1 + "</th></tr>";
        else
            TableHTML += "<tr valign='middle'><th rowspan='3' align='right' style='padding-left:10px;width:10%'><img src='images/meenakchi-amman-1.jpg' width='100%' alt=''/></th><th rowspan='3' align='right' style='padding-left:10px;width:15%'><img src='images/5.png' width='100%' alt=''/></th><th style='font-size:19px;text-align:center;width: 48%;height: 1%;'>" + ObjList.CompanyName + "</th><th style='text-align:end;height: 1%;'> " + ObjList.PhoneNo1 + " / " + ObjList.PhoneNo2 + "</th></tr>";

        TableHTML += "<tr><th style='text-align:center;height: 1%;' >Pure Handloom Silk Sarees Manufacturer</th><th></th></tr>";

        TableHTML += "<tr><th style='text-align:center;height: 1%;'>" + ObjList.CompanyAddress + "</th><th style='text-align:end;height: 1%;'>" + ObjList.CSTNo + " / Statecode: 33 </th></tr>";
        TableHTML += "<tr><th style='text-align:center;'Colspan='4'> Email : " + ObjList.Email + "</th></tr>";

        TableHTML += "</table><hr/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>";
        TableHTML += "<table>" + divOPInvoice.ClientID + "</table>";

        TableHTML += "<table cellpadding='0' cellspacing='0'><tr><td style='text-align:right;background:white;font-size:22px !important;'>For " + ObjList.CompanyName + "</td></tr></table>";
        html_Description.Content = TableHTML;


        //if (ObjList.PhoneNo2.Length < 0)
        //    TableHTML += "<tr valign='middle'><th rowspan='3' align='right' style='padding-left:25px;width:10%'><img src='images/meenakchi-amman-1.jpg' width='100%' alt=''/><th rowspan='3' align='right' style='padding-left:25px;width:15%'><img src='images/5.png' width='100%' alt=''/></th><th style='font-size:19px;text-align:center;width: 48%;height: 1%;'>" + ObjList.CompanyName + "</th><th style='text-align:end;height: 1%;'> " + ObjList.PhoneNo1 + "</th></tr>";
        //else
        //    TableHTML += "<tr valign='middle'><th rowspan='3' align='right' style='padding-left:25px;width:10%'><img src='images/meenakchi-amman-1.jpg' width='100%' alt=''/><th rowspan='3' align='right' style='padding-left:25px;width:15%'><img src='images/5.png' width='100%' alt=''/></th><th style='font-size:19px;text-align:center;width: 48%;height: 1%;'>" + ObjList.CompanyName + "</th><th style='text-align:end;height: 1%;'> " + ObjList.PhoneNo1 + " / " + ObjList.PhoneNo2 + "</th></tr>";

        //TableHTML += "<tr><th style='text-align:center;height: 1%;' >Pure Handloom Silk Sarees Manufacturer</th><th></th></tr>";

        //TableHTML += "<tr><th style='text-align:center;height: 1%;'>" + ObjList.CompanyAddress + "</th><th style='text-align:end;height: 1%;'>" + ObjList.CSTNo + " / State Code: 33 </th></tr>";
        //TableHTML += "<tr><th style='text-align:center;'Colspan='4'> Email : " + ObjList.Email + "</th></tr>";
        // }

        // TableHTML += "</table>";
    }
}