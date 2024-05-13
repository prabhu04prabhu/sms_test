<%@ Page Title="Sales Tax Return" Language="C#" MasterPageFile="~/VHMSReportPage.master"
    AutoEventWireup="true" CodeFile="RepSalesTaxReturnReport.aspx.cs" Inherits="RepSalesTaxReturnReport" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <style type="text/css">
        #VHMSWebContent_CRDischargeSummaryReport__UI_mb, #VHMSWebContent_CRDischargeSummaryReport__UI_bc
        {
            height: inherit !important;
            top: 0px !important;
            left: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <asp:HiddenField ID="hdnAdmissionID" runat="server" />
    <div class="container-wrapper hidden">
        <div class="content-header">
            <h1>
                Sales Tax Return Report
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Report</a></li>
                <li class="active">Sales Tax Return Report</li>
            </ol>
        </div>
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                <ContentTemplate>
                        <label>
                            To</label><span class="text-danger">*</span>
                        <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDOR"
                            data-link-format="dd/MM/yyyy">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                            </div>
                            <asp:TextBox ID="txtDOR" AutoComplete="off" runat="server" Style="width: 165px; height: 30px;"></asp:TextBox>
                            <ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" targetcontrolid="txtDOR"
                                cssclass="MyCalendar" format="dd/MM/yyyy" popupbuttonid="Image1" />
                        </div>
                    <div class="margin">
                        <asp:Button ID="btnSearchReport" runat="server" Text="View" TabIndex="4" CssClass="btn btn-primary margin btn-sm"
                            OnClick="btnSearchReport_Click" />
                        <asp:Button ID="btnExportReport" runat="server" Text="Print Report" TabIndex="4"
                            CssClass="btn btn-danger margin btn-sm" OnClick="btnExportReport_Click" />
                    </div>
                    <CR:CrystalReportViewer ID="CRDischargeSummaryReport" ToolPanelView="None" runat="server"
                        Width="100%" HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasExportButton="False"
                        HasPrintButton="false" HasDrillUpButton="False" HasDrilldownTabs="False" HasRefreshButton="False" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            pLoadingSetup(false);

            pLoadingSetup(true);
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.select2').select2({ theme: 'bootstrap' });
            });
        });

    </script>
      <style>
        .MyCalendar {
            background-color: white !important;
        }
    
    </style>
</asp:Content>
