<%@ Page Title="Exchange Report" Language="C#" MasterPageFile="~/VHMSMasterPage.master" CodeFile="frmExchangeReport.aspx.cs" Inherits="frmExchangeReport" %>


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
            <h1>Exchange Report
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Report</a></li>
                <li class="active">Exchange Report</li>
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
                                <div class="form-group col-md-2 col-sm-4" id="divDOB">
                                    <label>
                                        From</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDOB"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtDOB" AutoComplete="off" runat="server" Width="150" Height="30" ></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDOB"
                                            CssClass="MyCalendar" Format="dd/MM/yyyy" PopupButtonID="Image1" />
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
                                        <asp:TextBox ID="txtDOR" AutoComplete="off" runat="server" Width="150" Height="30" ></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDOR"
                                            CssClass="MyCalendar" Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                    </div>
                                </div>
                               <%-- <div class="form-group col-md-2" id="divCustomerName">
                                    <label>
                                        CustomerName</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlCustomerName"  Width="200" Height="30"
                                         runat="server">
                                    </asp:DropDownList>
                                </div>--%>
                                <div class="form-group col-md-2" id="divBarcode">
                                    <label>
                                        Mobile No</label><span class="text-danger">*</span>
                                     <asp:TextBox ID="txtMobile" AutoComplete="off" runat="server"  Style="width: 200px; Height:30px;" ></asp:TextBox>
                                </div>
                                <div class="form-group col-md-2" id="Branch">
                                    <label>
                                        Branch</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlBranch" AutoPostBack="true" Width="200" Height="30" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="btnView" Text="View" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnView_Click" runat="server" />
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="btnprint" Text="print" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClientClick="doPrint(); return false;" runat="server" />
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="btnExcel" Text="Excel" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnExcel_Click" runat="server" />
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="btnPDF" Text="PDF" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnPDF_Click" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divRecords">
                      
                        <asp:GridView ID="gvExchange" runat="server"  Caption="Exchange Reports" CaptionAlign="Top" CssClass="table table-striped table-bordered table-responsive dTableR"
                            AutoGenerateColumns="false" GridLines="None" DataKeyNames="PK_ExchangeID"
                            EmptyDataText="No Records Found" AllowSorting="true" OnRowDataBound="GridView2_RowDataBound" ShowFooter="true">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="ExchangeNo" ReadOnly="true" DataField="ExchangeNo" />
                                <asp:BoundField HeaderText="ExchangeDate" ReadOnly="true" DataField="ExchangeDate"  DataFormatString="{0:dd/MM/yyyy}"  />
                                <asp:BoundField HeaderText="CustomerName" ReadOnly="true" DataField="CustomerName" />
                                <asp:BoundField HeaderText="CustomerPhoneNo" ReadOnly="true" DataField="MobileNo" />
                                <asp:BoundField HeaderText="BranchName" ReadOnly="true" DataField="BranchName"  />
                                <asp:BoundField HeaderText="TaxAmount" ReadOnly="true" DataField="TaxAmount" />
                                <%--<asp:BoundField HeaderText="NetAmount" ReadOnly="true" DataField="NetAmount" />--%>
                          <asp:TemplateField HeaderText="NetAmount ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNetAmount" runat="server" Style="width: 10px;" Text='<%# ""+Eval("NetAmount").ToString()%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                   <%-- <FooterTemplate>
                                        <asp:Label ID="lblTotalAmount1" runat="server" />
                                        <asp:Label ID="lblTotalAmount" runat="server" />
                                    </FooterTemplate>--%>
                                </asp:TemplateField>
                            
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
    </style>
    <script>
        function doPrint() {
            var prtContent = document.getElementById('<%=   gvExchange.ClientID   %>');
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


