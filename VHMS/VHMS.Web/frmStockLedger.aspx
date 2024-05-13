<%@ Page Title="Stock Ledger Report" Language="C#" AutoEventWireup="true" MasterPageFile="~/VHMSReportPage.master"
    CodeFile="frmStockLedger.aspx.cs" Inherits="frmStockLedger" %>

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
    <link href="css/print.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper">
        <section class="content-header">
            <h1>Stock Ledger Report
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Report</a></li>
                <li class="active">Stock Ledger Report</li>
            </ol>
            <div class="pull-right">
            </div>
            <br />
            <br />
            <section class="content">
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
                                    <asp:TextBox ID="txtDOB" AutoComplete="off" runat="server" Width="150" Height="30"></asp:TextBox>
                                  <%--  <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDOB"
                                        CssClass="MyCalendar" Format="dd/MM/yyyy" PopupButtonID="Image1" />--%>
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
                                    <asp:TextBox ID="txtDOR" AutoComplete="off" runat="server" Width="150" Height="30"></asp:TextBox>
                                    <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDOR"
                                        CssClass="MyCalendar" Format="dd/MM/yyyy" PopupButtonID="Image1" />--%>
                                </div>
                            </div>
                            <div class="form-group col-md-2" id="divCode">
                                <label>
                                    SMSCode/PartyCode</label><span class="text-danger">*</span>

                                <asp:TextBox ID="txtCode" runat="server" placeholder="Code" class="form-control TRSearch"
                                    OnTextChanged="tbAccount_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-2" id="Branch">
                                <label>
                                    product</label><span class="text-danger">*</span>
                                <asp:DropDownList ID="ddlCustomer" Width="200" Height="30" runat="server" CssClass="select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-1">
                                <asp:Button ID="btnView" Text="View" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnView_Click" runat="server" />
                            </div>
                            <div class="form-group col-md-1">
                                <asp:Button ID="btnprint" Text="print" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClientClick="doPrint()" runat="server" />
                            </div>
                            <div class="form-group col-md-1">
                                <asp:Button ID="btnExcel" Text="Excel" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnExcel_Click" runat="server" />
                            </div>
                            <div class="form-group col-md-1" style="margin-left: -150px; display: none;">
                                <asp:Button ID="btnPDF" Text="PDF" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnPDF_Click" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="content">
                    <div id="divPdf" runat="server" style="text-align: left;">
                        <div style="background-color: white;" id="divOPInvoice1" runat="server">
                        </div>
                    </div>
                </div>

            </section>
        </section>
    </div>
    <script type="text/javascript">
</script>
    <script type="text/javascript">
        $(document).ready(function () {
            pLoadingSetup(false);

            $("input[id*=txtDOB]").attr("data-link-format", "dd/MM/yyyy");
            $('input[id*=txtDOB]').datetimepicker({
                pickTime: false,
                useCurrent: true,
                maxDate: moment(),
                format: 'DD/MM/YYYY'
            });

            $("input[id*=txtDOR]").attr("data-link-format", "dd/MM/yyyy");
            $('input[id*=txtDOR]').datetimepicker({
                pickTime: false,
                useCurrent: true,
                maxDate: moment(),
                format: 'DD/MM/YYYY'
            });

            pLoadingSetup(true);
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.select2').select2({ theme: 'bootstrap' });
            });

            $('input,select').keydown(function (event) { //event==Keyevent
                if (event.which == 13) {
                    var inputs = $(this).closest('form').find(':input:visible');
                    inputs.eq(inputs.index(this) + 1).focus();
                    event.preventDefault(); //Disable standard Enterkey action

                }
            });

            $("[id$=txtCode]").change(function () {
                $("[id$=txtCode]").val(($("[id$=txtCode]").val().split('|')[0].trim()));
            });
            $("[id$=txtCode]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/frmBarcode.aspx/GetBarcodeCode") %>',
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
                        "width": ("800px"), "backgroundColor": ("#d7dde2")
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
   <%-- <script>
        function doPrint() {
            var prtContent = document.getElementById('<%=   divPdf.ClientID   %>');
            prtContent.border = 1; //set no border here
            var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1,border=1');
            WinPrint.document.write('<link rel="stylesheet" href="css/print.css" type="text/css" />');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }

    </script>--%>
    <script>
        function doPrint() {
            var panel = document.getElementById("<%=divPdf.ClientID %>");
            var printWindow = window.open('', '', '');
            printWindow.border = 1;
            printWindow.document.write('<html><head><title>Print Invoice</title>');

            // Make sure the relative URL to the stylesheet works:
            printWindow.document.write('<base href="' + location.origin + location.pathname + '">');

            // Add the stylesheet link and inline styles to the new document:
            printWindow.document.write('<link rel="stylesheet" href="css/print.css">');
            printWindow.document.write('<style type="text/css">print.css{width: 100%;}</style>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');

            setTimeout(function () {
                printWindow.print();
            }, 10);
            return false;
        }
        printWindow.document.close();

        </script>
</asp:Content>
