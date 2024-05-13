<%@ Page Title="Stock Summary Report" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmStockSummary.aspx.cs" Inherits="frmStockSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <%--<style>
        div.dt-buttons {
            float: right !important;
            margin-left: 10px !important;
        }
    </style>

    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.1.0/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.10/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.1.0/css/select.dataTables.min.css" type="text/css" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">

    <div class="container-wrapper">
        <section class="content-header">
            <h1>Stock Summary Report
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Report</a></li>
                <li class="active">Stock Summary Report</li>
            </ol>
            <div class="pull-right">
            </div>
            <br />
            <br />
            <section class="content">

                <table border="0" class="table table-striped table-bordered table-responsive dTableR">
                    <div class="box box-warning box-solid" id="divFilter">

                        <div class="box-header with-border">
                            <div class="box-title">
                                Filter Options
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-2" id="divCategory">
                                    <label>Category </label>
                                    <span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlCategory" CssClass="select2" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true" Width="200" Height="30" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-2" id="divSubCategory">
                                    <label>SubCategory </label>
                                    <span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlSubCategory" CssClass="select2" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged" AutoPostBack="true" Width="200" Height="30" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-3" style="margin-left: -5px;" id="divSupplier">
                                    <label>Supplier</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlSupplier" CssClass="select2" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged" AutoPostBack="true" Style="width: 200px; height: 30px;" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-3" id="divProduct">
                                    <label>Product</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlProduct" CssClass="select2" Width="200" Height="30" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-2" id="divProductType">
                                    <label>Product Type</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlProductType" CssClass="select2" Width="200" Height="30" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-2" id="divUnit">
                                    <label>Unit</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlUnit" CssClass="select2" Width="200" Height="30" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-2">
                                    <label>
                                        Report Type</label>
                                    <asp:DropDownList ID="ddluser" runat="server" data-placeholder="Select User"
                                        TabIndex="3" CssClass="select2">
                                        <asp:ListItem Value="Without Zero Stock" Text="Without Zero Stock"></asp:ListItem>                                        
                                        <asp:ListItem Value="Zero Stock" Text="Zero Stock"></asp:ListItem>       
                                        <asp:ListItem Value="All" Text="All"></asp:ListItem>
                                        <asp:ListItem Value="Negative Stock" Text="Negative Stock"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-2" id="divCode">
                                    <label>
                                        SMSCode/PartyCode</label><span class="text-danger">*</span>

                                    <asp:TextBox ID="txtCode" runat="server" placeholder="Code" class="form-control TRSearch"
                                        OnTextChanged="tbAccount_TextChanged" AutoPostBack="false"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="btnView1" Text="View" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnView1_Click" runat="server" />
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="btnprint" Text="print" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClientClick="doPrint(); return false;" runat="server" />
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="btnAddNew1" Text="Excel" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnAddNew1_Click" runat="server" />
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="btnPDF1" Text="PDF" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnPDF1_Click" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </table>


                <div class="row" id="divRecords1">
                    <asp:GridView ID="GVSummary" runat="server" Caption="StockSummary Reports" CaptionAlign="Top" CssClass="table table-striped table-bordered table-responsive dTableR"
                        AutoGenerateColumns="false" GridLines="None" DataKeyNames="FK_ProductID"
                        EmptyDataText="No Records Found" OnRowDataBound="GridView2_RowDataBound" ShowFooter="true" AllowSorting="true">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Category" ReadOnly="true" DataField="CategoryName" />
                            <asp:BoundField HeaderText="SubCategory" ReadOnly="true" DataField="SubCategoryName" />
                            <asp:BoundField HeaderText="Supplier" ReadOnly="true" DataField="SupplierName" />
                            <asp:BoundField HeaderText="Party Code" ReadOnly="true" DataField="ProductCode" />
                            <asp:BoundField HeaderText="SMSCode" ReadOnly="true" DataField="SMSCode" />
                            <asp:BoundField HeaderText="Product" ReadOnly="true" DataField="ProductName" />
                            <asp:BoundField HeaderText="ProductType Name" ReadOnly="true" DataField="ProductTypeName" />
                            <asp:BoundField HeaderText="DesignNo" ReadOnly="true" DataField="DesignNo" />
                            <asp:BoundField HeaderText="HSNCode" ReadOnly="true" DataField="HSNCode" />
                            <asp:BoundField HeaderText="Quantity" ReadOnly="true" DataField="Quantity" DataFormatString="{0:0.##}" />
                             <asp:BoundField HeaderText="Purchase Price" ReadOnly="true" DataField="PurchasePrice" />
                              <asp:BoundField HeaderText="Total Price" ReadOnly="true" DataField="Totalvalue" DataFormatString="{0:0.00}"/>
                            <asp:BoundField HeaderText="Minimum Stock" ReadOnly="true" DataField="MinimumStock" />
                        </Columns>
                        <PagerStyle CssClass="cssPager" HorizontalAlign="Center" />
                    </asp:GridView>
                </div>
            </section>
        </section>
    </div>
    <script type="text/javascript">
</script>
    <script type="text/javascript">
        $(document).ready(function () {
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
        .MyCalendar {
            background-color: white !important;
        }
    </style>
    <script>
        function doPrint() {
            var prtContent = document.getElementById('<%=    GVSummary.ClientID   %>');
            prtContent.border = 1; //set no border here
            var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
            WinPrint.document.write(prtContent.outerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
    </script>
    <style>
        .table-bordered > thead > tr > th,
        .table-bordered > tbody > tr > th,
        .table-bordered > tfoot > tr > th,
        .table-bordered > thead > tr > td,
        .table-bordered > tbody > tr > td,
        .table-bordered > tfoot > tr > td {
            border: 1px solid #000 !important;
        }
    </style>
</asp:Content>


