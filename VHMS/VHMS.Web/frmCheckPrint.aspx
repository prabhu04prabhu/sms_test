<%@ Page Title="frmCheckPrint" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmCheckPrint.aspx.cs" Inherits="frmCheckPrint" %>

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
    <asp:HiddenField ID="hdnBillNo" runat="server" />

    <div class="container-wrapper">
        <section class="content-header">
            <h1>Cheque leaf
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Report</a></li>
                <li class="active">cheque leaf Report</li>
            </ol>
            <div class="pull-right">
            </div>
            <br />
            <br />
            <div class="box-body">
                <div class="table-responsive">
                    <table border="0" class="table table-striped table-bordered table-responsive dTableR">
                        <div class="box box-warning box-solid" id="divFilter">
                            <div class="box-header with-border">
                                <div class="box-title">
                                  Cheque Details
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="row">

                                    <div class="form-group col-md-2">
                                        <label>
                                            Ledger Name</label><span class="text-danger">*</span>
                                        <asp:TextBox ID="txtpay" TextMode="SingleLine" Width="205" Height="40" runat="server" autocomplete="off" ></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-2">
                                        <label>
                                            Cheque Date</label><span class="text-danger">*</span>
                                        <asp:TextBox ID="txtdate" CssClass="datetimepicker1" TextMode="SingleLine" Width="205" Height="40" runat="server" autocomplete="off" ></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label>
                                            Payment Amount</label><span class="text-danger">*</span>
                                        <asp:TextBox ID="txtamount" TextMode="SingleLine" Width="205" Height="40" runat="server" autocomplete="off" ></asp:TextBox>
                                    </div>
                                    <div class="pull-left">
                                        <asp:Button ID="btnPrint" runat="server" Text="Print Report" TabIndex="4"
                                            CssClass="btn btn-danger margin btn-sm" OnClick="btnPrint_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </table>
                </div>
            </div>
        </section>
    </div>

    <div>
        <CR:CrystalReportViewer ID="CRDischargeSummaryReport" ToolPanelView="None" runat="server"
            Width="100%" HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasExportButton="True"
            HasPrintButton="True" HasDrillUpButton="False" HasDrilldownTabs="False" HasRefreshButton="True" Style="margin-left: 100px;" />
    </div>
    <table>
    </table>


    <script>
        $(function () {
            $('.datetimepicker1').datetimepicker({
                format: 'DD/MM/YYYY'
            });
        });
    </script>
</asp:Content>



