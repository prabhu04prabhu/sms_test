<%@ Page Title="Print SalesReturn Invoice" Language="C#" MasterPageFile="~/VHMSReportPage.master" AutoEventWireup="true" CodeFile="PrintSalesReturnInvoice.aspx.cs" Inherits="PrintSalesReturnInvoice" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <style type="text/css">
        #VHMSWebContent_CRDischargeSummaryReport__UI_mb, #VHMSWebContent_CRDischargeSummaryReport__UI_bc {
            height: inherit !important;
            top: 0px !important;
            left: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <asp:HiddenField ID="hdnPurchaseNo" runat="server" />
    <div class="container-wrapper hidden">
        <div class="content-header">
            <h1>Print SalesReturn Invoice
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Transaction</a></li>
                <li><a href="#">Sales</a></li>
                <li class="active">Print SalesReturn Invoice</li>
            </ol>
        </div>
        <div style="text-align: center;">
            <asp:Button ID="btnExportReport" runat="server" Text="Print Report" TabIndex="4"
                CssClass="btn btn-danger margin btn-sm" OnClick="btnPrint_Click" />
        </div>
    <CR:CrystalReportViewer ID="CRDischargeSummaryReport" ToolPanelView="None" runat="server"
        Width="100%" HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasExportButton="False"
        HasPrintButton="false" HasDrillUpButton="False" HasDrilldownTabs="False" HasRefreshButton="False" />
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



