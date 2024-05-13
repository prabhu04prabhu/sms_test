<%@ Page Title="" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmVisitCustomerReport.aspx.cs" Inherits="frmVisitCustomerReport" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <style>
        @media (max-width:600px)
        {
            .fixed .main-header, .content-header > h1, .content-header > .breadcrumb, .content-header br
            {
                display: none;
                visibility: hidden;
            }
        
            .fixed .content-wrapper
            {
                padding-top: 0px;
            }
        
        
            .box.box-warning.box-solid
            {
                height: auto !important;
            }
        
            .box-body .btn-group
            {
                margin: 0px !important;
                padding: 0px !important;
            }
        
            .box-body .btn-info
            {
                margin-left: 15px !important;
                float: left;
                padding: 5px 0px;
                width: 58px;
            }
        
        }

        .lblTotal{
            text-align:right;
            font-weight:bold;
            display:block;
            font-size:16px;
        }
          .table-responsive
        {
            overflow: hidden;
            overflow-x: scroll;
            -webkit-overflow-scrolling: touch;
        }
        .MyCalendar
        {
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
            <h1> Visit Customer Report
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Report</a></li>
                <li class="active">Visit Customer Report</li>
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
                                        <asp:TextBox ID="txtDOB" AutoComplete="off" runat="server"  Style="width: 165px; Height:30px;" ></asp:TextBox>
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
                                        <asp:TextBox ID="txtDOR" AutoComplete="off" runat="server"  Style="width: 165px; Height:30px;" ></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDOR"
                                            CssClass="MyCalendar" Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                    </div>
                                </div>
                                <div class="form-group col-md-2" style="margin-left: -5px;" id="divBranch">
                                    <label>
                                        Branch</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlBranch" OnTextChanged="ddlPayment_SelectedIndexChanged"  Style="width: 200px; Height:30px;" AutoPostBack="true"
                                        runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-2" style="margin-left: -5px;" id="divEmployeeCode">
                                    <label>
                                        EmployeeCode</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlEmployeeCode" Style="width: 200px; Height:30px;" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <%--</div>--%>
                                <%--<div class="row">--%>
                               <%-- <div class="form-group col-md-8">&nbsp;</div>--%>
                                <div class="form-group col-md-1 btn-group">
                                    <asp:Button ID="btnView" Text="View" CausesValidation="false"    CssClass="btn btn-info" OnClick="btnView_Click" runat="server" style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;"/>
                                </div>
                                <div class="form-group col-md-1 btn-group">
                                    <asp:Button ID="btnprint" Text="print" CausesValidation="false"  CssClass="btn btn-info" OnClientClick="doPrint();return false;" runat="server" style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;"/>
                                </div>
                                 <div class="form-group col-md-1 btn-group">
                                    <asp:Button ID="btnExcel" Text="Excel" CausesValidation="false"   CssClass="btn btn-info" OnClick="btnExcel_Click" runat="server" style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;"/>
                                </div>
                                 <div class="form-group col-md-1 btn-group">
                                    <asp:Button ID="btnPDF" Text="PDF" CausesValidation="false"  CssClass="btn btn-info" OnClick="btnPDF_Click" runat="server" style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;"/>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row table-responsive" id="divRecords">
                        <asp:GridView ID="gvRegister"  runat="server" Caption="Visit Customer Reports" CaptionAlign="Top" CssClass="table table-striped table-bordered table-responsive dTableR"
                            AutoGenerateColumns="false" GridLines="None" DataKeyNames="PK_CustomerID"
                            EmptyDataText="No Records Found" OnRowDataBound="GridView2_RowDataBound" ShowFooter="false"  AllowSorting="true" OnDataBound="OnDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Date" ReadOnly="true" DataField="CreatedOn" DataFormatString="{0:dd/MM/yyyy}"/>
                                 <asp:BoundField HeaderText="Time" ReadOnly="true" DataField="createdTime"/>
                                <asp:BoundField HeaderText="Customer Name" ReadOnly="true" DataField="CustomerName" />
                                <asp:BoundField HeaderText="Mobile No." ReadOnly="true" DataField="MobileNo" />
                                <asp:BoundField HeaderText="Address" ReadOnly="true" DataField="Address" />
                                <asp:BoundField HeaderText="Employee" ReadOnly="true" DataField="EmployeeCode" />                                
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
           
        });
    </script>
    <style>
        .MyCalendar {
            background-color: white !important;
        }
    
    </style>
    <script>
        function doPrint() {
            var prtContent = document.getElementById('<%=   gvRegister.ClientID   %>');
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



