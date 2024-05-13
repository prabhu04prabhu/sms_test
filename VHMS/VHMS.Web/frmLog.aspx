<%@ Page Title="Page Visit Log" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmLog.aspx.cs" Inherits="frmLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li class="active">Page Visit Log</li>
            </ol>
            <br />
        </section>
        <section class="content">
            <div class="box box-danger">
                <div class="box-body">
                    <div class="table-responsive">
                        <div id="divVisitLog">
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            pLoadingSetup(false);
            pLoadingSetup(true);
            GetVisitLog();
        });

        function GetVisitLog() {
            dProgress(true);
            $("#divVisitLog").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetVisitLog",
                contentType: "application/json; charset=utf-8",
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
                                    var sColorCode = "bg-info";

                                    sTable = "<table id='tblMagazineList' class='table no-margin table-hover table-condensed'>";
                                    sTable += "<tr><th class='" + sColorCode + "'>S.No</th>";
                                    sTable += "<th class='" + sColorCode + "'>Date & Time</th>";
                                    sTable += "<th class='" + sColorCode + "'>IP Address</th>";
                                    sTable += "<th class='" + sColorCode + "'>User Name</th>";
                                    sTable += "<th class='" + sColorCode + "'>URL</th>";
                                    sTable += "</tr>";
                                    for (var index = 0; index < obj.length; index++) {
                                        sTable += "<tr><td id='" + (index + 1) + "' style='text-align:left;width:5%;'>" + (index + 1) + "</td>";
                                        sTable += "<td>" + obj[index].sVisitLogDateTime + "</td>";
                                        sTable += "<td>" + obj[index].IPAddress + "</td>";
                                        sTable += "<td>" + obj[index].UserName + "</td>";
                                        sTable += "<td>" + obj[index].URL + "</td>";
                                        sTable += "</tr>";
                                    }

                                    $("#divVisitLog").html(sTable);
                                    if (obj.length >= 13)
                                    { $("#divVisitLog").css({ 'height': '0px', 'min-height': '500px', 'overflow': 'auto' }); }
                                    else
                                    { $("#divVisitLog").css({ 'height': '', 'min-height': '' }); }
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#divVisitLog").empty();
                                $.jGrowl("No Records", { sticky: false, theme: 'warning', life: jGrowlLife });
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
                        $("#divVisitLog").empty();
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
    </script>
</asp:Content>
