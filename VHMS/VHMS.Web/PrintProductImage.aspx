<%@ Page Title="Print Product Image" Language="C#" MasterPageFile="~/VHMSReportPage.master"
    AutoEventWireup="true" CodeFile="PrintProductImage.aspx.cs" Inherits="PrintProductImage" %>

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
    <asp:HiddenField ID="hdnBillNo" runat="server" />
    <div class="container-wrapper hidden">
        <div class="content-header">
            <h1>Print Product Image
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Reports</a></li>
                <li class="active">Print Product Image</li>
            </ol>
        </div>
        <div class="box box-warning box-solid" style="width: 98%; margin-left: 12px;" id="divFilter">
            <div class="box-header with-border">
                <div class="box-title">
                    Filter Options
                </div>
            </div>
            <div class="box-body">
                <div class="form-group col-md-2" style="margin-left: -5px;" id="divCategory">
                    <label>
                        Category</label><span class="text-danger">*</span>
                    <asp:DropDownList ID="ddlCategory" CssClass="select2" AutoPostBack="false" Style="width: 200px; height: 30px;"
                        runat="server">
                    </asp:DropDownList>
                </div>
                <div class="form-group col-md-2" style="margin-left: -5px;" id="divSubCategory">
                    <label>
                        SubCategory</label><span class="text-danger">*</span>
                    <asp:DropDownList ID="ddlSubCategory" CssClass="select2" AutoPostBack="false" Style="width: 200px; height: 30px;"
                        runat="server">
                    </asp:DropDownList>
                </div>
                <div class="form-group col-md-2" style="margin-left: -5px;" id="divSupplier">
                    <label>
                        Supplier</label><span class="text-danger">*</span>
                    <asp:DropDownList ID="ddlSupplier" CssClass="select2" AutoPostBack="false" Style="width: 200px; height: 30px;"
                        runat="server">
                    </asp:DropDownList>
                </div>
                <div class="form-group col-md-2" style="margin-left: -5px;" id="divProduct">
                    <label>
                        Product</label><span class="text-danger">*</span>
                    <asp:DropDownList ID="ddlProduct" CssClass="select2" AutoPostBack="false" Style="width: 200px; height: 30px;"
                        runat="server">
                    </asp:DropDownList>
                </div>
                <div class="form-group col-md-2" id="divProductType">
                    <label>
                        Product Type</label><span class="text-danger">*</span>
                    <asp:DropDownList ID="ddlProductType" CssClass="select2" AutoPostBack="false" Style="width: 200px; height: 30px;"
                        runat="server">
                    </asp:DropDownList>
                </div>
                <div class="form-group col-md-2" id="divCode">
                    <label>
                        SMSCode/PartyCode</label><span class="text-danger">*</span>
                    <asp:TextBox ID="txtCode" runat="server" placeholder="Code" class="form-control TRSearch"
                        OnTextChanged="tbAccount_TextChanged" AutoPostBack="false"></asp:TextBox>
                </div>
                <div class="form-group col-md-2" id="divStatus">
                    <label>
                        Order</label><span class="text-danger">*</span>
                    <asp:DropDownList ID="ddlOrder" Width="150" Height="30" runat="server">
                        <asp:ListItem Text="PartyCode" Value="PartyCode"></asp:ListItem>
                        <asp:ListItem Text="SMSCode" Value="SMSCode"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="checkbox col-md-2" style="font-size: 23px; margin-top: 22px;">
                    <asp:CheckBox ID="chkPartyCode" runat="server" AutoPostBack="false" Text="Party Code" CssClass="BigCheckBox" />
                </div>
                <div class="form-group col-md-1 btn-group" style="margin-top: 27px;">
                    <asp:Button ID="btnView" Text="View" CausesValidation="false" CssClass="btn btn-info"
                        OnClick="btnView_Click" runat="server" />
                </div>
                <div class="form-group col-md-1 btn-group" style="margin-top: 27px; margin-left: -38px">
                    <asp:Button ID="btnExportReport" Text="Print Report" CausesValidation="false" CssClass="btn btn-info"
                        OnClick="btnPrint_Click" runat="server" />
                </div>
            </div>
        </div>
        <CR:CrystalReportViewer ID="CRDischargeSummaryReport" ToolPanelView="None" runat="server"
            Width="100%" HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasExportButton="true"
            HasPrintButton="False" HasDrillUpButton="False" HasDrilldownTabs="False" HasRefreshButton="False" />
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            pLoadingSetup(false);

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
    <table>
    </table>
</asp:Content>
