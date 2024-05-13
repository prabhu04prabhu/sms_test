
<%@ Page Title="Print Retail sales Invoice" Language="C#" MasterPageFile="~/VHMSReportPage.master" AutoEventWireup="true" CodeFile="PrintRetailsalesEntry.aspx.cs" Inherits="PrintRetailsalesEntry" %>

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
            <h1>Print Retail sales Invoice
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Transaction</a></li>
                <li><a href="#">Sales</a></li>
                <li class="active">Print Retail sales Invoice</li>
            </ol>
        </div>
        <div style="margin-left: 25%">
            <asp:CheckBox ID="chkOriginal" runat="server" Style="display: none;" Text="Original"
                Checked="true" CssClass="BigCheckBox" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="chkDuplicate" runat="server" Text="MEMO" Style="display: none;"
                Checked="true" CssClass="BigCheckBox" />&nbsp;&nbsp;&nbsp;&nbsp;

           <div class="form-group col-md-2" style="margin-top: -24px;">
               <label>
                   Report Type</label>
               <asp:DropDownList ID="ddluser" runat="server" AutoPostBack="true" data-placeholder="Select User"  OnTextChanged="ddlPayment_SelectedIndexChanged"
                   TabIndex="3" CssClass="select2">
                   <asp:ListItem Value="Retail" Text="Retail"></asp:ListItem>
                   <asp:ListItem Value="MEMO" Text="MEMO"></asp:ListItem>
                   <asp:ListItem Value="Original" Text="Original"></asp:ListItem>
               </asp:DropDownList>
           </div>
            <asp:Button ID="btnExportReport" runat="server" Text="Print Report" TabIndex="4"
                CssClass="btn btn-danger margin btn-sm" OnClick="btnPrint_Click" />
            
        </div>

        <CR:CrystalReportViewer ID="CRDischargeSummaryReport" ToolPanelView="None" runat="server"
            Width="100%" HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasExportButton="False"
            HasPrintButton="false" HasDrillUpButton="False" HasDrilldownTabs="False" HasRefreshButton="False" />
        <CR:CrystalReportViewer ID="CRDuplicate" ToolPanelView="None" runat="server"
            Width="100%" HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasExportButton="True"
            HasPrintButton="True" HasDrillUpButton="False" HasDrilldownTabs="False" HasRefreshButton="True" />
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



