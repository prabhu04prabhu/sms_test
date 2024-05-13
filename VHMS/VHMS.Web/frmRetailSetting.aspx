<%@ Page Title="Retail Setting" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmRetailSetting.aspx.cs" Inherits="frmRetailSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Retail Setting
            </h1>
        </section>
        <section class="content">
            <div class="nav-tabs-custom" id="tab-modal">
                <ul class="nav nav-tabs">
                    <li class="active"><a id="aGeneral" href="#General" data-toggle="tab">General</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="General">
                        <div class="row">
                            <div class="form-group col-md-3 col-sm-3" id="divContactNo">
                                <label>
                                    Mobile No</label>
                                <input type="text" class="form-control" id="txtContactNo" placeholder="Please enter Contact No"
                                    maxlength="13" tabindex="5" />
                            </div>
                            <div class="form-group col-md-3 col-sm-3" id="divCSTNo">
                                <label>
                                    GSTIN No</label>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-sort-numeric-asc"></i></div>
                                    <input type="text" class="form-control" id="txtCSTNo" style="text-transform: uppercase" placeholder="CST No" maxlength="20" tabindex="11" />
                                </div>
                            </div>
                            <div class="form-group col-md-3 col-sm-3" id="divEmail">
                                <label>
                                    Email</label>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-envelope"></i></div>
                                    <input type="text" class="form-control" id="txtEmail" placeholder="Email" maxlength="50" tabindex="12" />
                                </div>
                            </div>
                            <div class="form-group col-md-3 col-sm-3" id="divBankName">
                                <label>
                                    Bank Name</label>
                                <input type="text" class="form-control TRSearch" id="txtBankName" placeholder="Bank Name"
                                    maxlength="500" tabindex="14" />
                            </div>
                            <div class="form-group col-md-3 col-sm-3" id="divBranch Name">
                                <label>
                                    Branch Name</label>
                                <input type="text" class="form-control" id="txtBranchName" placeholder="Branch Name" maxlength="500"
                                    tabindex="15" />
                            </div>
                            <div class="form-group col-md-3 col-sm-3" id="divAccountNo">
                                <label>
                                    Account No</label>
                                <input type="text" class="form-control" id="txtAccountNo" placeholder="Account No" maxlength="25"
                                    tabindex="16" />
                            </div>
                            <div class="form-group col-md-3 col-sm-3" id="divIFSCCode">
                                <label>
                                    IFSC Code</label>
                                <input type="text" class="form-control" id="txtIFSCCode" placeholder="IFSC Code" maxlength="20"
                                    tabindex="17" />
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button id="btnSaveChanges" type="button" class="btn btn-primary margin pull-right" tabindex="20">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save Changes</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnSettingID" />
    <script type="text/javascript">
        $(document).ready(function () {
            ActionAdd = '<%=Session["ActionAdd"]%>';
            ActionUpdate = '<%=Session["ActionUpdate"]%>';
            ActionDelete = '<%=Session["ActionDelete"]%>';
            ActionView = '<%=Session["ActionView"]%>';

            pLoadingSetup(false);
            GetSettingInformation();
            pLoadingSetup(true);
        });

        $("#btnSaveChanges").click(function () {
            if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }

            if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }

            if ($("#txtEmail").val().length > 0) {
                var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (!regex.test($("#txtEmail").val())) {
                    $.jGrowl("Please enter valid email", { sticky: false, theme: 'warning', life: jGrowlLife });
                    return false;
                }
            }
            SaveandUpdateSettingInformation();

            return false;
        });

        function ClearFields() {
          
            $("#txtContactNo").val("");
            
            $("#txtCSTNo").val("");
            $("#txtEmail").val("");
            $("#txtWebSite").val("");
            $("#txtCINNo").val("");
            $("#divName").removeClass('has-error');
            $("#divCode").removeClass('has-error');
            $("#divAddress").removeClass('has-error');
            $("#divPhoneNo1").removeClass('has-error');
            return false;
        }

        function SaveandUpdateSettingInformation() {
            var Obj = new Object();
            Obj.SettingID = 0;
            Obj.ContactNo = $("#txtContactNo").val().trim();
            Obj.Email = $("#txtEmail").val().trim();
            Obj.CSTNo = $("#txtCSTNo").val().trim().toUpperCase();
            Obj.BankName = $("#txtBankName").val().trim();
            Obj.BranchName = $("#txtBranchName").val().trim();
            Obj.AccountNo = $("#txtAccountNo").val().trim();
            Obj.IFSCCode = $("#txtIFSCCode").val().trim();
        
            if ($("#hdnSettingID").val() > 0) {
                Obj.SettingID = $("#hdnSettingID").val();
                sMethodName = "UpdateSettings";
            }
            else { sMethodName = "AddSettings"; }

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
                                ClearFields();
                                if (sMethodName == "AddSetting") {
                                    $.jGrowl("Saved Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                                    $("#hdnSettingID").val(objResponse.Value);
                                }
                                else if (sMethodName == "UpdateSettings") { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                                GetSettingInformation();
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Setting_A_01") {
                                $.jGrowl("Name Already Exists", { sticky: false, theme: 'danger', life: jGrowlLife });
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

        function GetSettingInformation() {
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
                                    ClearFields();

                                    $("#hdnSettingID").val(obj.SettingID);
                                    $("#txtContactNo").val(obj.ContactNo);
                                    $("#txtEmail").val(obj.Email);
                                    $("#txtCSTNo").val(obj.CSTNo);
                                    $("#txtBankName").val(obj.BankName);
                                    $("#txtBranchName").val(obj.BranchName);
                                    $("#txtAccountNo").val(obj.AccountNo);
                                    $("#txtIFSCCode").val(obj.IFSCCode);
                                    dProgress(false);
                                }
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
                }
            });
            return false;
        }
    </script>
</asp:Content>


