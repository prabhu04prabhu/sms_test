<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/VHMSMasterPage.master" CodeFile="frmAttendanceLogFinalAuto.aspx.cs" Inherits="frmAttendanceLogFinalAuto" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">

    <div class="container-wrapper">
        <section class="content-header">
            <h1>Attendance Log Final
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Report</a></li>
                <li class="active">Attendance Log Final</li>
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
                                        To</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDOR"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtFromDate" Enabled="true" Width="100" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnView" Text="View" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnView_Click" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divRecords">
                        <asp:GridView ID="gvPurchase" runat="server" Caption="AttendanceLogFinals" CaptionAlign="Top" CssClass="table table-striped table-bordered table-responsive dTableR"
                            AutoGenerateColumns="false" GridLines="None" DataKeyNames="PK_AttendanceLogID"
                            EmptyDataText="No Records Found" ShowFooter="true" AllowSorting="true">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Employee Name" ReadOnly="true" DataField="EmployeeName" />
                                <asp:BoundField HeaderText="Punch Date" ReadOnly="true" DataField="PunchDate" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField HeaderText="InTime" ReadOnly="true" DataField="PunchInTime" />
                                <asp:BoundField HeaderText="Out Time" ReadOnly="true" DataField="PunchOutTime" />
                                <asp:BoundField HeaderText="Duration" ReadOnly="true" DataField="Duration" />
                                <asp:BoundField HeaderText="Late Minutes " ReadOnly="true" DataField="LateMinutes" />
                                <asp:BoundField HeaderText="Deduction Amount" ReadOnly="true" DataField="DeductionAmt" />
                                <asp:BoundField HeaderText="Special Status" ReadOnly="true" DataField="SpecialStatus" />
                                <asp:BoundField HeaderText="Status" ReadOnly="true" DataField="Status" />

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
</asp:Content>


