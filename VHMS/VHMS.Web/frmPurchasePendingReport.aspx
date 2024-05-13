<%@ Page Title="Purchase Pending Report" Language="C#" MasterPageFile="~/VHMSReportPage.master"
    AutoEventWireup="true" CodeFile="frmPurchasePendingReport.aspx.cs" Inherits="frmPurchasePendingReport" %>

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
        
        .BigCheckBox input
        {
            width: 25px;
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <asp:HiddenField ID="hdnAdmissionID" runat="server" />
    <div class="container-wrapper hidden">
        <div class="content-header">
            <h1>
                Purchase Pending Report
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Report</a></li>
                <li class="active">Purchase Pending Report</li>
            </ol>
        </div>
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="box box-warning box-solid" id="divFilter">
                        <div class="box-header with-border">
                            <div class="box-title">
                                Filter Options
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-2" id="Branch">
                                    <label>
                                        Supplier</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlCustomer" CssClass="select2" Width="200" Height="30" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-2" id="Div1">
                                    <label>
                                        Due Days</label><span class="text-danger">*</span>
                                    <asp:TextBox ID="txtDueDays" AutoComplete="off" Text="0" runat="server" Width="150"
                                        Height="30"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-2">
                                    <asp:Button ID="btnSearchReport" runat="server" Text="View" TabIndex="4" CssClass="btn btn-primary margin btn-sm"
                                        OnClick="btnView_Click" />
                                    <asp:Button ID="btnExportReport" runat="server" Text="Print Report" TabIndex="4"
                                        CssClass="btn btn-danger margin btn-sm" OnClick="btnExportReport_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <CR:CrystalReportViewer ID="CRDischargeSummaryReport" ToolPanelView="None" runat="server"
                        Width="100%" HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasExportButton="True"
                        HasPrintButton="True" HasDrillUpButton="False" HasDrilldownTabs="False" HasRefreshButton="True" />
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
        .MyCalendar
        {
            background-color: white !important;
        }
    </style>
</asp:Content>
