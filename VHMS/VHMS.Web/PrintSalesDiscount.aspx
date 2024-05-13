<%@ Page Title="Print Sales Discount" Language="C#" MasterPageFile="~/VHMSReportPage.master"
    AutoEventWireup="true" CodeFile="PrintSalesDiscount.aspx.cs" Inherits="PrintSalesDiscount" %>

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
        .BigCheckBox input
        {
            width: 25px;
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <asp:HiddenField ID="hdnSalesDiscountID" runat="server" />
    <div class="container-wrapper hidden">
        <div class="content-header">
            <h1>
                Print Sales Discount
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Transaction</a></li>
                <li><a href="#">Sales</a></li>
                <li class="active">Print Sales Discount</li>
            </ol>
        </div>
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="checkbox col-md-4" style="margin-top: -38px; margin-left: 500px; font-size: 23px;">
                        <asp:CheckBox ID="chkPartyCode" runat="server" AutoPostBack="true" OnCheckedChanged="chkPartyCode_CheckedChanged"
                            Text="Party Code" CssClass="BigCheckBox" />
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
    <table>
    </table>
</asp:Content>
