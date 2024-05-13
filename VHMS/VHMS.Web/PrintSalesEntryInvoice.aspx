<%@ Page Title="Print Sales Invoice" Language="C#" MasterPageFile="~/VHMSReportPage.master"
    AutoEventWireup="true" CodeFile="PrintSalesEntryInvoice.aspx.cs" Inherits="PrintSalesEntryInvoice" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <style type="text/css">
        #VHMSWebContent_CRDischargeSummaryReport__UI_mb, #VHMSWebContent_CRDischargeSummaryReport__UI_bc {
            height: inherit !important;
            top: 0px !important;
            left: 0px !important;
        }
           .chkBigCheckBox input
        {
            width: 25px;
            height: 25px;
        }
        .BigCheckBox input {
            width: 15px;
            height: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <asp:HiddenField ID="hdnBillNo" runat="server" />
    <div class="container-wrapper hidden">
        <div class="content-header">
            <h1>Print Sales Invoice
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Transaction</a></li>
                <li><a href="#">Sales</a></li>
                <li class="active">Print Sales Invoice</li>
            </ol>
        </div>
        <div style="margin-left: 25%">
            <asp:CheckBox ID="chkOriginal" runat="server" Text="Original"
                Checked="true" CssClass="BigCheckBox" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="chkDuplicate" runat="server" Text="Duplicate"
                Checked="true" CssClass="BigCheckBox" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="chkTransport" runat="server"  Text="Transport"
                Checked="true" CssClass="BigCheckBox" />
            <asp:Button ID="btnExportReport" runat="server" Text="Print Report" TabIndex="4"
                CssClass="btn btn-danger margin btn-sm" OnClick="btnPrint_Click" />

             <asp:CheckBox ID="chkPartyCode" runat="server" style="font-size: x-large;" AutoPostBack="true" Text="SMS Code"  OnCheckedChanged="chkPartyCode_CheckedChanged"
                        CssClass="chkBigCheckBox    " />
        </div>
        <CR:CrystalReportViewer ID="CRDischargeSummaryReport" ToolPanelView="None" runat="server"
            Width="100%" HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasExportButton="True"
            HasPrintButton="True" HasDrillUpButton="False" HasDrilldownTabs="False" HasRefreshButton="True" style="margin-left: 100px;" />
        <CR:CrystalReportViewer ID="CRDuplicate" ToolPanelView="None" runat="server"
            Width="100%" HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasExportButton="True"
            HasPrintButton="True" HasDrillUpButton="False" HasDrilldownTabs="False" HasRefreshButton="True" style="margin-left: 100px;" />
        <CR:CrystalReportViewer ID="CRTransport" ToolPanelView="None" runat="server"
            Width="100%" HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasExportButton="True"
            HasPrintButton="True" HasDrillUpButton="False" HasDrilldownTabs="False" HasRefreshButton="True" style="margin-left: 100px;" />
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
