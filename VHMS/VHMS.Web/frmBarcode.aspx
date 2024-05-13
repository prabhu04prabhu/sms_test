<%@ Page Title="Barcode" Language="C#" MasterPageFile="~/VHMSReportPage.master" AutoEventWireup="true"
    CodeFile="frmBarcode.aspx.cs" Inherits="frmBarcode" %>

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
    <asp:HiddenField ID="hdnPatientID" runat="server" />
    <div class="container-wrapper hidden">
        <div class="content-header">
            <h1>Barcode
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Master</a></li>
                <li class="active">Barcode</li>
            </ol>
            <br />
            <br />
            <div class="input-group-append">
                <div class="row">
                    <div class="form-group col-md-2">
                        <label>
                            Barcode</label>
                        <asp:TextBox ID="txtBarcode" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-2">
                        <label>
                            SMS Code</label>
                        <asp:TextBox ID="txtSMSCode" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-1">
                        <label>
                            Count</label>
                        <asp:TextBox ID="txtCount" CssClass="form-control" runat="server" Text="1"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-2">
                        <label>
                            Type</label>
                        <asp:DropDownList ID="ddlType" CssClass="form-control" runat="server">
                            <asp:ListItem Value="SMSCodeOnly">SMS Code Only</asp:ListItem>
                            <asp:ListItem Value="SMSreader"  Selected="True">SMS reader</asp:ListItem>
                            <asp:ListItem Value="BarReader">Bar Reader</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group col-md-2">
                        <label>
                            Price</label>
                        <asp:DropDownList ID="ddlRate" CssClass="form-control" runat="server">
                            <asp:ListItem Value="Retail"  Selected="True">Retail Rate</asp:ListItem>
                            <asp:ListItem Value="WholeSale">WholeSale Rate</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group col-md-1" style="display: none;">
                        <asp:Button ID="btnGenrate" CssClass="btn btn-outline-primary Suggestions" runat="server"
                            Text="Generate" OnClick="btnGenrate_Click" />
                    </div>
                    <div class="form-group col-md-1">
                        <asp:Button ID="btnPrint" CssClass="btn btn-outline-primary Suggestions" runat="server"
                            Text="Print" OnClick="btnPrint_Click" />
                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>
        <div class="form-group col-md-12">
            <asp:Button ID="btnPrintAll" CssClass="btn btn-outline-primary Suggestions" runat="server"
                Text="Print All" OnClick="btnPrintAll_Click" />
        </div>
        <div class="form-group col-md-12">
            <asp:GridView ID="GridView1" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                runat="server" AutoGenerateColumns="false" OnRowDataBound="OnRowDataBound" OnRowCommand="RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="S.No">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SMSCode" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:TextBox ID="txtSMSCode" runat="server" Text='<%# Eval("SMSCode") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Barcode" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:TextBox ID="txtBarcode" runat="server" Text='<%# Eval("Barcode") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("Quantity") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="70">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" Width="25" ToolTip="Print Record" CommandName="Print"
                                CommandArgument='<%# Eval("PK_PurchaseTransID") %>' runat="server" Text="Print" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <CR:CrystalReportViewer ID="CRDischargeSummaryReport" ToolPanelView="None" runat="server"
            Width="100%" HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasExportButton="False"
            HasPrintButton="false" HasDrillUpButton="False" HasDrilldownTabs="False" HasRefreshButton="False" />
        <%--<div class="content" id="imageOutput" style="margin-left: 200px">
            <div class="row">
                <div class="form-group col-md-2">
                    <label style="margin-left: 4%; font-size:11px; font-family:arial;">SMS</label>
                    <asp:Panel ID="PlaceHolder1" runat="server">
                    </asp:Panel>
                    <asp:Label ID="lblBarcode" runat="server" Text="" style="margin-left: 10%; font-size:12px;  font-family:arial;"></asp:Label>
                </div>
            </div>
        </div>--%>
    </div>
    <script type="text/javascript">
        var pageUrl = '<%=ResolveUrl("~/frmBarcode.aspx") %>';
        $(document).ready(function () {

            $('input,select').keydown(function (event) { //event==Keyevent
                if (event.which == 13) {
                    var inputs = $(this).closest('form').find(':input:visible:not(disabled):not([readonly])');
                    inputs.eq(inputs.index(this) + 1).focus();
                    event.preventDefault(); //Disable standard Enterkey action

                }
            });

            pLoadingSetup(false);

            pLoadingSetup(true);
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.select2').select2({ theme: 'bootstrap' });
            });
            $("[id$=txtBarcode]").change(function () {
                $("[id$=txtBarcode]").val(($("[id$=txtBarcode]").val().split('|')[0]).trim());
            });
            $("[id$=txtBarcode]").autocomplete({

                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/frmBarcode.aspx/GetBarcodeList") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
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
                    $("[id$=txtBarcode]").autocomplete("widget").css({
                        "width": ("200px"), "backgroundColor": ("#eac9c2"), "-webkit-text-fill-color": ("#000")
                    });
                },
                select: function (e, i) {
                },
                minLength: 1
            });

            $("[id$=txtSMSCode]").change(function () {
                $("[id$=txtSMSCode]").val(($("[id$=txtSMSCode]").val().split('|')[0]).trim());
            });
            $("[id$=txtSMSCode]").autocomplete({

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
                    $("[id$=txtSMSCode]").autocomplete("widget").css({
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
        .Suggestions {
            margin-top: 25px;
        }
    </style>
</asp:Content>
