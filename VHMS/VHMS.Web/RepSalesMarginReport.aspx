<%@ Page Title="Sales Margin With Return Report" Language="C#" MasterPageFile="~/VHMSReportPage.master"
    AutoEventWireup="true" CodeFile="RepSalesMarginReport.aspx.cs" Inherits="RepSalesMarginReport" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
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
               Sales Margin With Return Report
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Report</a></li>
                <li class="active">Sales Margin With Return Report</li>
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
                                <div class="form-group col-md-2" id="divDOB">
                                <label>
                                    From</label><span class="text-danger">*</span>
                                <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDOB"
                                    data-link-format="dd/MM/yyyy">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtDOB" AutoComplete="off" runat="server" Width="150" Height="30" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group col-md-2" id="divDOR">
                                <label>
                                    To</label><span class="text-danger">*</span>
                                <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDOR"
                                    data-link-format="dd/MM/yyyy">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtDOR" AutoComplete="off" runat="server" Width="150" Height="30"></asp:TextBox>
                                </div>
                            </div>
                                <div class="form-group col-md-2">
                                    <label>
                                        Supplier</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlSupplier" Width="200" Height="30" runat="server" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged"
                                        AutoPostBack="true" CssClass="select2">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-2">
                                    <label>
                                        Category</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlCategory" Width="200" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                        AutoPostBack="true" Height="30" runat="server" CssClass="select2">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-2">
                                    <label>
                                        Subcategory</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlSubcategory" Width="200" Height="30" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged"
                                        AutoPostBack="true" runat="server" CssClass="select2">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-2">
                                    <label>
                                        Product</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlProduct" Width="200" Height="30" runat="server" CssClass="select2">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-2">
                                    <label>
                                        Customer</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlCustomer" Width="200" Height="30" runat="server" CssClass="select2">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-2">
                                    <label>
                                        Report Type</label>
                                    <asp:DropDownList ID="ddluser" runat="server" data-placeholder="Select User" TabIndex="3"
                                        CssClass="select2">
                                         <asp:ListItem Value="Abstract YearWise" Text="Abstract YearWise"></asp:ListItem>
                                        <asp:ListItem Value="BillWise" Selected="True" Text="Bill Wise"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-2">
                                    <label>
                                        Type</label>
                                    <asp:DropDownList ID="ddlType" runat="server" data-placeholder="Select Type" TabIndex="3"
                                        CssClass="select2">
                                        <asp:ListItem Value="All" Text="All"></asp:ListItem>
                                        <asp:ListItem Value="Whole Sales" Selected="True" Text="Whole Sales"></asp:ListItem>
                                        <asp:ListItem Value="Retail Sales" Text="Retail Sales"></asp:ListItem>
                                        <asp:ListItem Value="Yarn Sales" Text="Yarn Sales"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                               <%-- <div class="checkbox col-md-2" style="margin-top: -41px; margin-left: 680px; font-size: 23px;">
                                    <asp:CheckBox ID="chkPartyCode" runat="server" AutoPostBack="true" Text="Tax"
                                        CssClass="BigCheckBox" Checked="true" Visible="false"/>
                                </div>--%>

                                 <div class="form-group col-md-2" id="divCode">
                                    <label>
                                        SMSCode/PartyCode</label><span class="text-danger">*</span>

                                    <asp:TextBox ID="txtCode" runat="server" placeholder="Code" class="form-control TRSearch"
                                        OnTextChanged="tbAccount_TextChanged" AutoPostBack="false"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-2">
                                    <asp:Button ID="btnSearchReport" runat="server" Text="View" TabIndex="4" CssClass="btn btn-primary margin btn-sm"
                                        OnClick="btnSearchReport_Click" />
                                    <asp:Button ID="btnExportReport" runat="server" Text="Print Report" TabIndex="4"
                                        CssClass="btn btn-danger margin btn-sm" OnClick="btnExportReport_Click" />
                                </div>
                            </div>
                        </div>
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
            $("input[id*=txtDOB]").attr("data-link-format", "dd/MM/yyyy");
            $('input[id*=txtDOB]').datetimepicker({
                pickTime: false,
                useCurrent: true,
                maxDate: moment(),
                format: 'DD/MM/YYYY'
            });

            $("input[id*=txtDOR]").attr("data-link-format", "dd/MM/yyyy");
            $('input[id*=txtDOR]').datetimepicker({
                pickTime: false,
                useCurrent: true,
                maxDate: moment(),
                format: 'DD/MM/YYYY'
            });

            pLoadingSetup(true);
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.select2').select2({ theme: 'bootstrap' });
            });
            $("[id$=txtCode]").change(function () {
                $("[id$=txtCode]").val(($("[id$=txtCode]").val().split('|')[0]).trim());
            });
            $("[id$=txtCode]").autocomplete({

                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/frmBarcode.aspx/GetSMSCode") %>',
                        data: "{ 'prefix': '" + request.term + "','SupplierID':0,'IsAll':'A'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item,
                                    val: item.split('|')[0]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                open: function () {
                    $("[id$=txtCode]").autocomplete("widget").css({
                        "width": ("600px"), "backgroundColor": ("#d7dde2")
                    });
                },
                select: function (e, i) {
                },
                minLength: 1
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
