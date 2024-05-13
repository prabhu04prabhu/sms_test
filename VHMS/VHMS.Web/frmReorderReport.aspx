<%@ Page Title="Reorder Report" Language="C#" AutoEventWireup="true" MasterPageFile="~/VHMSMasterPage.master" CodeFile="frmReorderReport.aspx.cs" Inherits="frmReorderReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <style>
        @media (max-width:600px) {
            .fixed .main-header, .content-header > h1, .content-header > .breadcrumb, .content-header br {
                display: none;
                visibility: hidden;
            }

            .fixed .content-wrapper {
                padding-top: 0px;
            }


            .box.box-warning.box-solid {
                height: auto !important;
            }

            .box-body .btn-group {
                margin: 0px !important;
                padding: 0px !important;
            }

            .box-body .btn-info {
                margin-left: 15px !important;
                float: left;
                padding: 5px 0px;
                width: 58px;
            }
        }

        .lblTotal {
            text-align: right;
            font-weight: bold;
            display: block;
            font-size: 16px;
        }

        .table-responsive {
            overflow: hidden;
            overflow-x: scroll;
            -webkit-overflow-scrolling: touch;
        }

        .MyCalendar {
            z-index: 100000;
        }
    </style>
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
            <h1>Reorder Report
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Master</a></li>
                <li class="active">Reorder Report</li>
            </ol>
            <div class="pull-right">
            </div>
            <br />
            <br />
            <section class="content">
                <div class="box box-warning box-solid" style="width: 100%;" id="divFilter">
                    <div class="box-header with-border">
                        <div class="box-title">
                            Filter Options
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="row">

                            <div class="form-group col-md-2" style="margin-left: -5px;" id="divCategory">
                                <label>
                                    Category</label><span class="text-danger">*</span>
                                <asp:DropDownList ID="ddlCategory"  CssClass="select2" AutoPostBack="false" Style="width: 200px; height: 30px;" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-md-2" style="margin-left: -5px;" id="divSubCategory">
                                <label>
                                    SubCategory</label><span class="text-danger">*</span>
                                <asp:DropDownList ID="ddlSubCategory"  CssClass="select2" AutoPostBack="false" Style="width: 200px; height: 30px;" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-md-2" style="margin-left: -5px;" id="divSupplier">
                                <label>
                                    Supplier</label><span class="text-danger">*</span>
                                <asp:DropDownList ID="ddlSupplier"  CssClass="select2" AutoPostBack="false" Style="width: 200px; height: 30px;" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-md-1 btn-group" style="margin-top: 20px;">
                                <asp:Button ID="btnView" Text="View" CausesValidation="false" CssClass="btn btn-info" OnClick="btnView_Click" runat="server" />
                            </div>
                            <div class="form-group col-md-1 btn-group" style="margin-top: 20px;">
                                <asp:Button ID="btnprint" Text="print" CausesValidation="false" CssClass="btn btn-info" OnClientClick="doPrint();return false;" runat="server" />
                            </div>
                            <div class="form-group col-md-1 btn-group" style="margin-top: 20px;">
                                <asp:Button ID="btnExcel" Text="Excel" CausesValidation="false" CssClass="btn btn-info" OnClick="btnExcel_Click" runat="server" />
                            </div>
                            <div class="form-group col-md-1 btn-group" style="margin-top: 20px;">
                                <asp:Button ID="btnPDF" Text="PDF" CausesValidation="false" CssClass="btn btn-info" OnClick="btnPDF_Click" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row table-responsive" id="divRecords">
                    <asp:Label ID="lblTotal" CssClass="lblTotal" runat="server" />
                    <asp:GridView ID="gvReceipt" runat="server" Caption="Reorder Report" CaptionAlign="Top" CssClass="table table-striped table-bordered table-responsive dTableR"
                        AutoGenerateColumns="false" GridLines="None" DataKeyNames="PK_ProductID"
                        EmptyDataText="No Records Found" ShowFooter="false" AllowSorting="true">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Party Code" ReadOnly="true" DataField="ProductCode" />
                            <asp:BoundField HeaderText="SMSCode" ReadOnly="true" DataField="SMSCode" />
                            <asp:BoundField HeaderText="Product" ReadOnly="true" DataField="ProductName" />
                            <asp:BoundField HeaderText="Supplier" ReadOnly="true" DataField="SupplierName" />
                            <asp:BoundField HeaderText="Minimum Stock" ReadOnly="true" DataField="MinimumStock" />
                            <asp:BoundField HeaderText="AvailableQty" ReadOnly="true" DataField="AvailableQty" />
                            <%-- <asp:TemplateField HeaderText="Amount Paid">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmountPaid" runat="server" Style="width: 10px;" Text='<%# ""+Eval("AmountPaid").ToString()%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
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
            //$("#txtDOB,#txtDOR").attr("data-link-format", "dd/MM/yyyy");
            //$("#txtDOB,#txtDOR").datetimepicker({
            //    pickTime: false,
            //    useCurrent: true,
            //    format: 'DD/MM/YYYY'
            //});
            $('input,select').keydown(function (event) { //event==Keyevent
                if (event.which == 13) {
                    var inputs = $(this).closest('form').find(':input:visible:not(disabled):not([readonly])');
                    inputs.eq(inputs.index(this) + 1).focus();
                    event.preventDefault(); //Disable standard Enterkey action

                }
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
            var prtContent = document.getElementById('<%=   gvReceipt.ClientID   %>');
            prtContent.border = 1; //set no border here
            var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,border=1,status=0,resizable=1');
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

