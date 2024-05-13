<%@ Page Title="Closing Report" Language="C#" MasterPageFile="~/VHMSMasterPage.master" CodeFile="frmclosedReport.aspx.cs" Inherits="frmclosedReport" %>

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
            <h1>Closing Report
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Report</a></li>
                <li class="active">Closing Report</li>
            </ol>
            <div class="pull-right">
            </div>
            <br />
            <br />
            <section class="content">
                <table border="0" class="table table-striped table-bordered table-responsive dTableR">
                    <div class="box box-warning box-solid" style="WIDTH: 101%; height: 164px;" id="divFilter">
                        <div class="box-header with-border">
                            <div class="box-title">
                                Filter Options
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-2 col-sm-4" id="divDOB">
                                    <label>
                                        From</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDOB"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtDOB" AutoComplete="off" runat="server" Style="width: 165px; Height:30px;" ></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDOB"
                                            CssClass="MyCalendar" Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                    </div>
                                </div>
                                <div class="form-group col-md-2" style="margin-left:-5px;" id="divDOR">
                                    <label>
                                        To</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P"  data-link-field="txtDOR"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtDOR" AutoComplete="off" runat="server" Style="width: 165px; Height:30px;" ></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDOR"
                                            CssClass="MyCalendar" Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                    </div>
                                </div>
                              <%--  <div class="form-group col-md-2" style="margin-left: -5px;" id="divCustomerName">
                                    <label>
                                        CustomerName</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlCustomerName" Style="width: 200px; Height:30px;"
                                         runat="server">
                                    </asp:DropDownList>
                                </div>--%>
                                 <div class="form-group col-md-2" id="divBarcode">
                                    <label>
                                        Mobile No</label><span class="text-danger">*</span>
                                     <asp:TextBox ID="txtMobile" AutoComplete="off" runat="server"  Style="width: 200px; Height:30px;" ></asp:TextBox>
                                </div>
                                 <%-- <div class="form-group col-md-2" id="divAccountNo">
                                    <label>
                                        Account No</label>
                                     <asp:TextBox ID="txtAccountNo" runat="server"  Style="width: 200px; Height:30px;" ></asp:TextBox>
                                </div>--%>
                                <div class="form-group col-md-2" style="margin-left: -5px;" id="Scheme">
                                    <label>
                                        Scheme</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlScheme" AutoPostBack="true" Style="width: 200px; Height:30px;" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-2" style="margin-left: -5px;" id="divBranch">
                                    <label>
                                     Branch</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlBranch" OnTextChanged="ddlPayment_SelectedIndexChanged" Style="width: 200px; Height:30px;" AutoPostBack="true"
                                         runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-2" style="margin-left: -5px;" id="divEmployeeCode">
                                    <label>
                                        EmployeeCode</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlEmployeeCode" Style="width: 200px; Height:30px;" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="btnView" Text="View" CausesValidation="false" Style="margin-top: 0px; padding-top: 4px; margin-left: 931px;"  CssClass="btn btn-info" OnClick="btnView1_Click" runat="server" />
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="btnprint" Text="print" CausesValidation="false" Style="margin-top: -1px; padding-top: 4px;margin-left: 908px;" CssClass="btn btn-info" OnClientClick="doPrint()" runat="server" />
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="btnExcel" Text="Excel" CausesValidation="false" Style="margin-top: -1px; padding-top: 4px; margin-left: 884px;" CssClass="btn btn-info" OnClick="btnExcel_Click" runat="server" />
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="btnPDF" Text="PDF" CausesValidation="false" Style="margin-top: 0px; padding-top: 4px; margin-left: 866px;" CssClass="btn btn-info" OnClick="btnPDF_Click" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row table-responsive" id="divRecords">
                        <asp:Label ID="lblTotal" CssClass="lblTotal" runat="server" />
                        <asp:GridView ID="gvClosing" runat="server"  Caption="Closed Reports" CaptionAlign="Top" CssClass="table table-striped table-bordered table-responsive dTableR"
                            AutoGenerateColumns="false" GridLines="None" DataKeyNames="PK_RegisterID"
                            EmptyDataText="No Records Found" OnRowDataBound="GridView2_RowDataBound" ShowFooter="true"  OnDataBound="OnDataBound"  AllowSorting="true">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Account No" ReadOnly="true" DataField="AccountNo" />
                                <asp:BoundField HeaderText="Closed Date" ReadOnly="true" DataField="CancelledDate" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField HeaderText="Customer Name" ReadOnly="true" DataField="CustomerName" />
                                <asp:BoundField HeaderText="Customer PhoneNo" ReadOnly="true" DataField="MobileNo" />
                                <asp:BoundField HeaderText="EmployeeName" ReadOnly="true" DataField="EmployeeName" />
                                 <asp:BoundField HeaderText="Branch Name" ReadOnly="true" DataField="BranchName" />
                                <asp:BoundField HeaderText="Scheme" ReadOnly="true" DataField="ChitName" />
                                <asp:BoundField HeaderText="Duration" ReadOnly="true" DataField="Duration" />
                                <asp:BoundField HeaderText="Installment Amount" ReadOnly="true" DataField="InstallmentAmount" />
                               <asp:BoundField HeaderText="Total Amount" ReadOnly="true" DataField="TotalAmount" />
                            </Columns>
                            <PagerStyle CssClass="cssPager" HorizontalAlign="Center" />
                        </asp:GridView>
                    </div>
                </table>
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

        .lblTotal{
            text-align:right;
            font-weight:bold;
            display:block;
            font-size:16px;
        }
    </style>
    <script>
        function doPrint() {
            var prtContent = document.getElementById('<%=   gvClosing.ClientID   %>');
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


