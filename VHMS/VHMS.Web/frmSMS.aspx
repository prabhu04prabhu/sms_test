<%@ Page Title="" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmSMS.aspx.cs" Inherits="frmSMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">

        <section class="content">
            <div class="modal fade" id="compose-modal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title">Compose SMS</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-md-12" id="divSMSType">
                                    <label>
                                        SMS Type</label><span class="text-danger">*</span>
                                    <select id="ddlSMSType" class="form-control" tabindex="1">
                                        <option value="All">All</option>
                                        <option value="Normal">Retail</option>
                                        <option value="Scheme">Wholesale</option>
                                        <option value="Individual Customer">Individual Customer</option>
                                    </select>
                                </div>
                                <div class="form-group col-md-12" id="divMobileNo">
                                    <label>
                                        Mobile No</label><span class="text-danger">*</span>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-mobile"></i></div>
                                        <input type="text" class="form-control" id="txtMobileNo" placeholder="Mobile No"
                                            maxlength="10" tabindex="2" onkeypress="return isNumberKey(event)" autocomplete="off"/>
                                    </div>
                                </div>
                                <div class="form-group col-md-12" id="divMessage">
                                    <label>
                                        Message</label><span class="text-danger">*</span>
                                    <textarea id="txtMessage" class="form-control" maxlength="2000" tabindex="3" rows="3" aria-autocomplete="none"></textarea>
                                </div>

                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnSend" tabindex="12">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Send</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="13">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnID" />
    <input type="hidden" id="hdRS" />
    <input type="hidden" id="hdnOpeningDate" />
    <input type="hidden" id="SdSMS" />
    <input type="hidden" id="SMSsendername" />
    <input type="hidden" id="SMSpassword" />
    <input type="hidden" id="SMSurl" />
    <input type="hidden" id="SMSusername" />
    <script type="text/javascript">
        $(document).ready(function () {
            ActionAdd = '<%=Session["ActionAdd"]%>';
            ActionUpdate = '<%=Session["ActionUpdate"]%>';
            ActionDelete = '<%=Session["ActionDelete"]%>';
            ActionView = '<%=Session["ActionView"]%>';

            var _SendSMS = '<%=Session["SendSMS"]%>';
            var _SMSpassword = '<%=Session["SMSPassword"]%>';
            var _SMSsendername = '<%=Session["SenderName"]%>';
            var _SMSurl = '<%=Session["APILink"]%>';
            var _SMSusername = '<%=Session["SMSUsername"]%>';
            
                $('input,select').keydown(function (event) { //event==Keyevent
                    if (event.which == 13) {
                        var inputs = $(this).closest('form').find(':input:visible:not(disabled):not([readonly])');
                        inputs.eq(inputs.index(this) + 1).focus();
                        event.preventDefault(); //Disable standard Enterkey action

                    }
                });

            $("#SdSMS").val(_SendSMS);
            $("#SMSsendername").val(_SMSsendername);
            $("#SMSpassword").val(_SMSpassword);
            $("#SMSurl").val(_SMSurl);
            $("#SMSusername").val(_SMSusername);
            $("#divMobileNo").hide();
            if (ActionAdd != "1") {
                $("#btnSend").remove();
            }

            $('#compose-modal').modal({ show: true, backdrop: true });
            pLoadingSetup(false);
            pLoadingSetup(true);

        });
        function GetSettings() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetSettings",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                                var obj = jQuery.parseJSON(objResponse.Value);
                                if (obj != null) {
                                    $("#txtDate").val(obj.sOpeningDate);
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                                dProgress(false);
                            }
                            else if (objResponse.Value == "Error") {
                                $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location("frmLogin.aspx");
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                            }
                        }
                    }
                    else {
                        $.jGrowl("Error Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                        dProgress(false);
                    }
                },
                error: function () {
                    $.jGrowl("Error Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    dProgress(false);
                }
            });
            return false;
        }

        function GetPassword(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetUserPassword",
                data: JSON.stringify({ ID: 0 }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                                var obj = jQuery.parseJSON(objResponse.Value);
                                if (obj != null) {
                                    $("#hdRS").val(obj.ConfirmPassword);
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                                dProgress(false);
                            }
                            else if (objResponse.Value == "Error") {
                                $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location("frmLogin.aspx");
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                            }
                        }
                    }
                    else {
                        $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                        dProgress(false);
                    }
                },
                error: function () {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    dProgress(false);
                }
            });
            return false;
        }

        $("#btnAddNew").click(function () {
           // ClearFields();
            $("#hdnID").val("");
            $("#btnSave").show();
            $("#btnUpdate").hide();
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Scheme Close");
            $('#compose-modal').modal({ show: true, backdrop: true });
            $("#txtAccountNo").focus();
            return false;
        });

        $("#ddlSMSType").change(function () {
            if ($("#ddlSMSType").val() == "Individual Customer")
                $("#divMobileNo").show();
            else
                $("#divMobileNo").hide();
        });

        $("#btnSend").click(function () {

            if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }

            if (confirm('Are you sure to Send SMS?')) {

            }
            else {
                return false;
            }
            if ($("#ddlSMSType").val() == "Individual Customer") {
                if ($("#txtMobileNo").val().trim() == "" || $("#txtMobileNo").val().trim() == undefined) {
                    $.jGrowl("Please enter Mobile No", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divMobileNo").addClass('has-error'); $("#txtMobileNo").focus(); return false;
                } else { $("#divMobileNo").removeClass('has-error'); }

                GetSMSRecord($("#txtMobileNo").val().trim(), $("#txtMessage").val().trim())
            }
            else {
                LoadCustomer($("#ddlSMSType").val());
            }



            var Obj = new Object();

            Obj.Message = $("#txtMessage").val();
            if ($("#ddlSMSType").val() == "Individual Customer")
                Obj.SendTo = $("#txtMobileNo").val();
            else
                Obj.SendTo = $("#ddlSMSType").val();

            var sMethodName;
            sMethodName = "AddSMSLog";

            SaveandUpdateRegister(Obj, sMethodName);

            return false;
        });

        function LoadCustomer(ddlname) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetCustomerByType",
                data: JSON.stringify({ ID: ddlname }),
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
                                    for (var index = 0; index < obj.length; index++) {
                                        GetSMSRecord(obj[index].MobileNo, $("#txtMessage").val().trim());
                                    }
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
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

        function SendSMS(MobileNo, APLUrl) {

            var url = APLUrl;
            $.ajax({
                type: 'GET',
                url: url,
                dataType: 'json',
                success: function (res) {
                    console.log(res);
                }
            });
        }

        function GetSMSRecord(iMobileNo, iMessage) {

            var APILink = $("#SMSurl").val();

            if (APILink.match(/#message#/))
                APILink = APILink.replace("#message#", iMessage);
            if (APILink.match(/#uname#/))
                APILink = APILink.replace("#uname#", $("#SMSusername").val());
            if (APILink.match(/#pwd#/))
                APILink = APILink.replace("#pwd#", $("#SMSpassword").val());
            if (APILink.match(/#mobile#/))
                APILink = APILink.replace("#mobile#", iMobileNo);
            if (APILink.match(/#sendername#/))
                APILink = APILink.replace("#sendername#", $("#SMSsendername").val());
            SendSMS(iMobileNo, APILink);
        }

        $("#btnClose").click(function () {
            $('#compose-modal').modal('hide');
            return false;
        });

        function SaveandUpdateRegister(Obj, sMethodName) {
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/" + sMethodName,
                data: JSON.stringify({ Objdata: Obj }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value > 0) {

                                if (sMethodName == "AddSMSLog")
                                { $.jGrowl("Send Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                                $("#btnClose").click();
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Register_A_01" || objResponse.Value == "Register_U_01") {
                                $.jGrowl(_CMAlreadyExits, { sticky: false, theme: 'danger', life: jGrowlLife });
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                        }
                    }
                    else {
                        $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }
    </script>
</asp:Content>



