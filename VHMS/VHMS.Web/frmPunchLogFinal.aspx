<%@ Page Title="PunchLogFinal" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmPunchLogFinal.aspx.cs" Inherits="frmPunchLogFinal" %>


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
            <h1>Punch Log Final
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Report</a></li>
                <li class="active">Punch Log Final</li>
            </ol>
            <div class="pull-right">
            </div>
            <br />
            <br />
            <section class="content">
                <table border="0" class="table table-striped table-bordered table-responsive dTableR">
                    <div class="box box-warning box-solid" id="divFilter">
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-2" id="divDOR">
                                    <label>
                                        From
                                    </label>
                                    <span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDOR"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtFromDate" Enabled="true" Width="100" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group col-md-2" id="divDOD">
                                    <label>
                                        To</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDOD"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtToDate" Enabled="true" Width="100" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnView" Text="View" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnView_Click" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divRecords">
                        <asp:GridView ID="gvPurchase" runat="server" Caption="PunchLogFinals" CaptionAlign="Top" CssClass="table table-striped table-bordered table-responsive dTableR"
                            AutoGenerateColumns="false" GridLines="None" DataKeyNames="PK_PunchLogID"
                            EmptyDataText="No Records Found" ShowFooter="true" AllowSorting="true">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Employee Name" ReadOnly="true" DataField="EmployeeName" />
                                <asp:BoundField HeaderText="Log Date" ReadOnly="true" DataField="LogDate" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField HeaderText="Log Time" ReadOnly="true" DataField="LogTime" />

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

            $("input[id*=txtFromDate]").attr("data-link-format", "dd/MM/yyyy");
            $('input[id*=txtFromDate]').datetimepicker({
                pickTime: false,
                useCurrent: true,
                maxDate: moment(),
                format: 'DD/MM/YYYY'
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
            var prtContent = document.getElementById('<%=   gvPurchase.ClientID   %>');
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
    <style>
        .footer {
            position: relative;
            height: 5em;
            margin-bottom: -5em;
            /* bottom:3px;
    z-index:05px;
    margin-top:-5em; */
            background-image: url('bg_footer111.gif');
            background-repeat: no-repeat;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            font-weight: bold;
            color: #FFFFFF;
            left: 5px;
            right: -5px;
            width: 982px;
        }
    </style>
    <script type="text/javascript">
        document.onkeydown = function () {
            if (event.keyCode == 113) {
                var myWindow = window.open("frmDPurchaseReport.aspx", "_self");
            }

        };
    </script>
</asp:Content>


