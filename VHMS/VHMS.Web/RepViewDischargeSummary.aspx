<%@ Page Title="Discharge Summary Report" Language="C#" MasterPageFile="~/VHMSReportPage.master"
    AutoEventWireup="true" CodeFile="RepViewDischargeSummary.aspx.cs" Inherits="RepViewDischargeSummary" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <style type="text/css">
        #VHMSWebContent_CRDischargeSummaryReport__UI_mb, #VHMSWebContent_CRDischargeSummaryReport__UI_bc
        {
            height: inherit !important;
            top: 0px !important;
            left: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <asp:HiddenField ID="hdnAdmissionID" runat="server" />
    <asp:HiddenField ID="hdnAdmissionNo" runat="server" />
    <div class="container-wrapper hidden">
        <div class="content-header">
            <h1>
                Discharge Summary Report
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Report</a></li>
                <li class="active">Discharge Summary</li>
            </ol>
        </div>
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="margin">
                        <asp:Button ID="btnExportReport" runat="server" Text="Print Report" TabIndex="4"
                            CssClass="btn btn-danger margin btn-sm" OnClick="btnExportReport_Click" />
                    </div>
                    <div class="box box-warning box-solid hidden" id="divDischargeEdit">
                        <div class="box-header with-border">
                            <div class="box-title">
                                <i class="fa fa-print"></i>&nbsp;&nbsp;Discharge Report
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-4" id="divSearchAdmissionNo">
                                    <label>
                                        Search Admission No</label><span class="text-danger">*</span>
                                    <div class="input-group">
                                        <input class="form-control TRSearch" id="txtSearchAdmissionNo" maxlength="10" tabindex="6"
                                            placeholder="Please enter Admission No, Patient Name, IP No, Room No" type="text" />
                                        <span class="input-group-btn">
                                            <button class="btn btn-success btn-flat" id="btnSearch" type="button">
                                                Go!</button>
                                        </span>
                                    </div>
                                </div>
                                <div class="form-group col-md-4 margin">
                                    <button type="button" class="btn btn-danger btn-sm margin" title="Clear filter" id="btnClearSearch"
                                        tabindex="6">
                                        <i class="fa fa-undo"></i>
                                    </button>
                                </div>
                                <div class="form-group col-md-3 hidden">
                                    <div class="margin">
                                        <asp:Button ID="btnSearchReport" runat="server" Text="Print Summary" TabIndex="4"
                                            CssClass="btn btn-primary margin btn-sm" OnClick="btnSearchReport_Click" />
                                    </div>
                                </div>
                                <div class="form-group col-md-3">
                                </div>
                            </div>
                            <div class="table-responsive">
                                <div id="divAdmissionResult">
                                </div>
                            </div>
                        </div>
                    </div>                   
                    <CR:CrystalReportViewer ID="CRDischargeSummaryReport" ToolPanelView="None" runat="server"
                        Width="100%" HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasExportButton="False"
                        HasPrintButton="false" HasDrillUpButton="False" HasDrilldownTabs="False" HasRefreshButton="False" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            pLoadingSetup(false);

            pLoadingSetup(true);
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.select2').select2({ theme: 'bootstrap' });
            });
        });

        $("#VHMSWebContent_btnSearchReport").click(function () {
            if ($("#VHMSWebContent_hdnAdmissionID").val() == null || $("#VHMSWebContent_hdnAdmissionID").val() == 0 || $("#VHMSWebContent_hdnAdmissionID").val() == undefined) {
                $.jGrowl("Please select Admission No", { sticky: false, theme: 'warning', life: jGrowlLife });
                return false;
            }
        });

        $("#txtSearchAdmissionNo").keyup(function () {
            var sKey = $("#txtSearchAdmissionNo").val().trim();
            GetAdmissionDetails(sKey);
        });

        $("#btnSearch").click(function () {
            var sKey = $("#txtSearchAdmissionNo").val().trim();
            GetAdmissionDetails(sKey);
            return false;
        });

        function GetAdmissionDetails(sKey) {
            //dProgress(true);
            $("#divAdmissionResult").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetAdmission",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ key: sKey, RowCount: 100 }),
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    var sTable = "";
                                    var sCount = 1;
                                    var sColorCode = "bg-info"; //"bg-teal-active color-palette";

                                    sTable = "<table id='tblAdmissionList' class='table no-margin table-hover'>";
                                    sTable += "<tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
                                    sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'></th>";
                                    sTable += "<th class='" + sColorCode + "'>Admission No</th>";
                                    sTable += "<th class='" + sColorCode + "'>UHID #</th>";
                                    sTable += "<th class='" + sColorCode + "'>Room No</th>";
                                    sTable += "<th class='" + sColorCode + "'>Patient Name</th>";
                                    sTable += "<th class='" + sColorCode + "'>Age</th>";
                                    sTable += "<th class='" + sColorCode + "'>Gender</th>";
                                    sTable += "<th class='" + sColorCode + "'>Primary Consultant</th>";
                                    sTable += "<th class='" + sColorCode + "'>Admission Dt</th>";
                                    sTable += "<th class='" + sColorCode + "'>Time</th>";
                                    sTable += "</tr>";
                                    for (var index = 0; index < obj.length; index++) {
                                        sTable += "<tr><td id='" + (index + 1) + "' style='text-align:left;width:3%;'>" + (index + 1) + "</td>";
                                        sTable += "<td style='text-align:center;'><a href='#' AdmissionID=" + obj[index].AdmissionID + " class='PreviewReport' title='Click here to Preview Report'><i class='fa fa-lg fa-print text-blue'/></a></td>";
                                        sTable += "<td>" + obj[index].AdmissionNo + "</td>";
                                        sTable += "<td>" + obj[index].UHIDNo + "</td>";
                                        sTable += "<td>" + obj[index].RoomNo + "</td>";
                                        sTable += "<td>" + obj[index].PatientName + "</td>";
                                        sTable += "<td>" + obj[index].PatientAge + "</td>";
                                        sTable += "<td>" + (obj[index].PatientSex == 1 ? "M" : obj[index].PatientSex == 2 ? "F" : "T") + "</td>";
                                        sTable += "<td>" + obj[index].PrimaryConsultant + "</td>";
                                        sTable += "<td>" + obj[index].sDateofAdmissionDate + "</td>";
                                        sTable += "<td>" + obj[index].sDateofAdmissionTime + "</td>";
                                        sTable += "</tr>";
                                    }
                                    sTable += "</table>";
                                    $("#divAdmissionResult").html(sTable);

                                    if (obj.length >= 10)
                                    { $("#divAdmissionResult").css({ 'height': '0px', 'min-height': '400px', 'overflow': 'auto' }); }
                                    else
                                    { $("#divAdmissionResult").css({ 'height': '', 'min-height': '' }); }

                                    $(".PreviewReport").click(function () {
                                        var AdmissionID = $(this).attr('AdmissionID');
                                        $("#VHMSWebContent_hdnAdmissionID").val(AdmissionID);

                                        $("#VHMSWebContent_btnSearchReport").click();
                                    });
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#divAdmissionResult").empty();
                                //$.jGrowl("No Records", { sticky: false, theme: 'warning', life: jGrowlLife });
                                dProgress(false);
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                            dProgress(false);
                        }
                    }
                    else {
                        $("#divAdmissionResult").empty();
                        dProgress(false);
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                    dProgress(false);
                }
            });
            return false;
        }
        $("#btnClearSearch").click(function () {
            $("#divAdmissionResult").empty();
            $("#txtSearchAdmissionNo").val("");
            $("#txtSearchAdmissionNo").focus();
            $("#divAdmissionResult").css({ 'height': '', 'min-height': '' });
            return false;
        });

    </script>
</asp:Content>
