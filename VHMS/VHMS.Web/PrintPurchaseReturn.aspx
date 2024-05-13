<%@ Page Title="Print Purchase Return" Language="C#" MasterPageFile="~/VHMSReportPage.master"
    AutoEventWireup="true" CodeFile="PrintPurchaseReturn.aspx.cs" Inherits="PrintPurchaseReturn" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <style type="text/css">
        #VHMSWebContent_CRDischargeSummaryReport__UI_mb, #VHMSWebContent_CRDischargeSummaryReport__UI_bc {
            height: inherit !important;
            top: 0px !important;
            left: 0px !important;
        }

        .BigCheckBox input {
            width: 25px;
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <asp:HiddenField ID="hdnPurchaseReturn" runat="server" />
    <div class="container-wrapper hidden">
        <div class="content-header">
            <h1>Print Purchase Return
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Transaction</a></li>
                <li><a href="#">Purchase</a></li>
                <li class="active">Print Purchase Return</li>
            </ol>
        </div>
        <div style="margin-left: 25%">
           <%-- <asp:CheckBox ID="chkOriginal" runat="server" Text="Original"
                Checked="true" CssClass="BigCheckBox" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="chkTransport" runat="server" Text="Transport"
                Checked="true" CssClass="BigCheckBox" />--%>
            <asp:CheckBox ID="chkPartyCode" runat="server" AutoPostBack="true" OnCheckedChanged="chkPartyCode_CheckedChanged"
                Text="Party Code" CssClass="BigCheckBox" />
        <%--    <asp:Button ID="btnExportReport" runat="server" Text="Print Report" TabIndex="4"
                CssClass="btn btn-danger margin btn-sm"  />   --%>
                 </div>
                    <CR:CrystalReportViewer ID="CRDischargeSummaryReport" ToolPanelView="None" runat="server"
                        Width="100%" HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasExportButton="True"
                        HasPrintButton="True" HasDrillUpButton="False" HasDrilldownTabs="False" HasRefreshButton="True" Style="margin-left: 100px;" />
                    <CR:CrystalReportViewer ID="CRTransport" ToolPanelView="None" runat="server"
                        Width="100%" HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasExportButton="True"
                        HasPrintButton="True" HasDrillUpButton="False" HasDrilldownTabs="False" HasRefreshButton="True" Style="margin-left: 100px;" />
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
