<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/VHMSMasterPage.master" CodeFile="frmAuditorBillReports.aspx.cs" Inherits="frmAuditorBillReports" %>

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

        .BigCheckBox input {
            width: 20px;
            height: 20px;
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
            <h1>AuditorBill Report
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Auditor Bill</a></li>
                <li class="active">AuditorBill Report</li>
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
                            <div class="form-group col-md-2 col-sm-4" style="margin-left: -5px;" id="divDOB">
                                <label>
                                    From</label><span class="text-danger">*</span>
                                <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDOB"
                                    data-link-format="dd/MM/yyyy">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtDOB" AutoComplete="off" runat="server" Style="width: 165px; height: 30px;"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDOB"
                                        CssClass="MyCalendar" Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </div>
                            </div>
                            <div class="form-group col-md-2" style="margin-left: -5px;" id="divDOR">
                                <label>
                                    To</label><span class="text-danger">*</span>
                             
                                <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDOR"
                                    data-link-format="dd/MM/yyyy">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtDOR" AutoComplete="off" runat="server" Style="width: 165px; height: 30px;"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDOR"
                                        CssClass="MyCalendar" Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </div>
                                  
                            </div>
                             
                            <div class="form-group col-md-2" style="margin-left: -5px;" id="divBranch">
                                <label>
                                    Branch</label><span class="text-danger">*</span>
                                <asp:DropDownList ID="ddlBranch" OnTextChanged="ddlPayment_SelectedIndexChanged" Style="width: 200px; height: 30px;" AutoPostBack="true"
                                    runat="server">
                                </asp:DropDownList>
                                 <asp:label id="OriginalBillDate" runat="server" Style="color: red;"></asp:label>
                            </div>
                             <div class="form-group col-md-1" id="divBarcode">
                                    <label>
                                        Last BillNo.</label><span class="text-danger">*</span>
                                     <asp:TextBox ID="txtLastBillNo" AutoComplete="off" runat="server"  Style="width: 100px; Height:30px;" ></asp:TextBox>
                                 <asp:label id="OriginalBillNo" runat="server" Style="color: red;"></asp:label>
                                </div>
                            <%--<div class="form-group col-md-2" style="margin-left: -5px;" id="divEmployeeCode">
                                    <label>
                                        EmployeeCode</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlEmployeeCode" Style="width: 200px; Height:30px;" runat="server">
                                    </asp:DropDownList>
                                </div>--%>
                            <div class="form-group col-md-1 btn-group">
                                <asp:Button ID="btnView" Text="View" CausesValidation="false" CssClass="btn btn-info" OnClick="btnView_Click" runat="server" Style="margin-top: 22px;" />
                            </div>
                           <%-- <div class="form-group col-md-1 btn-group">
                                <asp:Button ID="btnprint" Text="print" CausesValidation="false" CssClass="btn btn-info" OnClientClick="doPrint();return false;" runat="server" Style="margin-top: 22px;"  />
                            </div>--%>
                            <div class="form-group col-md-1 btn-group">
                                <asp:Button ID="btnExcel" Text="Excel" CausesValidation="false" CssClass="btn btn-info" OnClick="btnExcel_Click" runat="server" Style="margin-top: 22px;"  />
                            </div>
                            <div class="form-group col-md-1 btn-group">
                                <asp:Button ID="btnPDF" Text="PDF" CausesValidation="false" CssClass="btn btn-info" OnClick="btnPDF_Click" runat="server" Style="margin-top: 22px;" />
                            </div>
                            <div class="form-group col-md-1 btn-group">
                                <asp:Button ID="btnSelectAll" Text="Select All" CausesValidation="false" CssClass="btn btn-info" OnClick="btnSelectAll_Click" runat="server" Style="margin-top: 22px;" />
                            </div>
                            <div class="form-group col-md-1 btn-group">
                                <asp:Button ID="btnUnSelect" Text="UnSelect All" CausesValidation="false" CssClass="btn btn-info" OnClick="btnUnSelect_Click" runat="server" Style="margin-top: 22px;" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row table-responsive" id="divRecords">
                    <asp:Label ID="lblTotal" CssClass="lblTotal" runat="server" Style="margin-right:100px;" />
                    <asp:GridView ID="gvRenewal" runat="server" Caption="Auditor Bill Reports" CaptionAlign="Top" CssClass="table table-striped table-bordered table-responsive dTableR"
                        AutoGenerateColumns="false" GridLines="None" DataKeyNames="PK_SalesID"
                        EmptyDataText="No Records Found" OnRowDataBound="GridView2_RowDataBound" ShowFooter="true" AllowSorting="true" OnDataBound="OnDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="BillNo" ReadOnly="true" DataField="InvoiceNo" />
                           
                            <asp:BoundField HeaderText="Date" ReadOnly="true" DataField="InvoiceDate" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField HeaderText="Gross Amount" ReadOnly="true" DataField="GrossAmount" />
                            <asp:BoundField HeaderText="Gst Tax 1.5%" ReadOnly="true" DataField="CGSTAmount" />
                           <asp:BoundField HeaderText="Gst Tax 1.5%" ReadOnly="true" DataField="SGSTAmount" />
                             <asp:BoundField HeaderText="Total Amount" ReadOnly="true" DataField="Total_Amount" />
                             
                            <asp:TemplateField  HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="SelectChkBox" runat="server" OnCheckedChanged="SelectChkBox_CheckChanged" AutoPostBack="true" CssClass="BigCheckBox"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:BoundField HeaderText="Reference ID" ReadOnly="true" DataField="PK_SalesID" />
                            <asp:TemplateField HeaderText="ISGSTBill" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="IsGSTBill" Visible="false" runat="server" Text='<%# Eval("IsGSTBill") %>'></asp:Label>
                                    </ItemTemplate>
                            </asp:TemplateField>
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
        });
    </script>
    <style>
        .MyCalendar {
            background-color: white !important;
        }
    </style>
    <script>
        <%--function doPrint() {
            var prtContent = document.getElementById('<%=   gvRenewal.ClientID   %>');
            prtContent.border = 1; //set no border here
            var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,border=1,status=0,resizable=1');
            WinPrint.document.write(prtContent.outerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }--%>
      
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

