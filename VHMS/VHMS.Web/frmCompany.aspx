<%@ Page Title="Company" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmCompany.aspx.cs" Inherits="frmCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Company
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
                            <div class="form-group col-md-8 col-sm-8" id="divName">
                                <label>
                                    Name</label><span class="text-danger">*</span>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-user"></i></div>
                                    <input type="text" class="form-control" id="txtName" placeholder="Please enter Company Name"
                                        maxlength="255" tabindex="1" />
                                </div>
                            </div>
                            <div class="form-group col-md-4 col-sm-4" id="divCode">
                                <label>
                                    Code</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtCode" placeholder="Please enter Company Code"
                                    maxlength="10" tabindex="2" />
                            </div>
                            <div class="form-group col-md-12 col-sm-12" id="divAddress">
                                <label>
                                    Address</label><span class="text-danger">*</span>
                                <textarea id="txtAddress" class="form-control" maxlength="255" tabindex="3" rows="4"></textarea>
                            </div>
                            <div class="form-group col-md-3 col-sm-3" id="divContactPerson">
                                <label>
                                    Contact Person</label>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-user"></i></div>
                                    <input type="text" class="form-control" id="txtContactPerson" placeholder="Please enter Contact Person"
                                        maxlength="255" tabindex="4" />
                                </div>
                            </div>
                            <div class="form-group col-md-3 col-sm-3" id="divContactNo">
                                <label>
                                    Contact No</label>
                                <input type="text" class="form-control" id="txtContactNo" placeholder="Please enter Contact No"
                                    maxlength="13" tabindex="5" />
                            </div>
                            <div class="form-group col-md-3 col-sm-3" id="divPhoneNo1">
                                <label>
                                    Phone No 1</label><span class="text-danger">*</span>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-phone"></i></div>
                                    <input type="text" class="form-control" id="txtPhoneNo1" placeholder="Phone No" maxlength="13"
                                        tabindex="6" />
                                </div>
                            </div>
                            <div class="form-group col-md-3 col-sm-3" id="divPhoneNo2">
                                <label>
                                    Phone No 2</label>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-phone"></i></div>
                                    <input type="text" class="form-control" id="txtPhoneNo2" placeholder="Phone No" maxlength="13"
                                        tabindex="7" />
                                </div>
                            </div>
                            <div class="form-group col-md-3 col-sm-3" id="divLandline">
                                <label>
                                    LandLine</label>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-phone"></i></div>
                                    <input type="text" class="form-control" id="txtLandline" placeholder="Landline" maxlength="13"
                                        tabindex="8" />
                                </div>
                            </div>
                            <div class="form-group col-md-3 col-sm-3" id="divFax">
                                <label>
                                    Fax</label>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-fax"></i></div>
                                    <input type="text" class="form-control" id="txtFax" placeholder="Fax" maxlength="20"
                                        tabindex="9" />
                                </div>
                            </div>
                            <div class="form-group col-md-3 col-sm-3" id="divTINNo" style="display: none">
                                <label>
                                    TIN No</label>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-sort-numeric-asc"></i></div>
                                    <input type="text" class="form-control" id="txtTINNo" placeholder="TIN No" maxlength="20" tabindex="10" />
                                </div>
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
                            <div class="form-group col-md-3 col-sm-3" id="divWebSite">
                                <label>
                                    Website</label>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-internet-explorer"></i></div>
                                    <input type="text" class="form-control" id="txtWebSite" placeholder="Website" maxlength="150" tabindex="13" />
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
                            <div class="form-group col-md-3 col-sm-3" id="divFinancialYear">
                                <label>
                                    FinancialYear</label>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-phone"></i></div>
                                    <input type="text" class="form-control" id="txtFinancialYear" placeholder="Landline" maxlength="5"
                                        tabindex="18" />
                                </div>
                            </div>
                            <div class="form-group col-md-3 col-sm-3" id="divCINNo">
                                <label>
                                    CIN No</label>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-sort-numeric-asc"></i></div>
                                    <input type="text" class="form-control" id="txtCINNo" style="text-transform: uppercase" placeholder="CIN No" maxlength="50" tabindex="19" />
                                </div>
                            </div>
                            <div class="form-group col-md-3 col-sm-3" id="divIEC ">
                                <label>
                                    IEC No</label>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-sort-numeric-asc"></i></div>
                                    <input type="text" class="form-control" id="txtIEC" style="text-transform: uppercase" placeholder="IEC Number" maxlength="500" tabindex="19" />
                                </div>
                            </div>
                            <div class="form-group col-md-6" id="divTerms">
                                <label>
                                    Terms and Conditions:</label>
                                <textarea id="txtTerms" class="form-control" maxlength="500" tabindex="19" rows="7" aria-autocomplete="none"></textarea>
                            </div>
                            <div class="form-group col-md-6" id="divNotes">
                                <label>
                                    Company Notes:</label>
                                <textarea id="txtNotes" class="form-control" maxlength="500" tabindex="19" rows="7" aria-autocomplete="none"></textarea>
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
    <input type="hidden" id="hdnCompanyID" />
    <script type="text/javascript">
        $(document).ready(function () {
            ActionAdd = '<%=Session["ActionAdd"]%>';
            ActionUpdate = '<%=Session["ActionUpdate"]%>';
            ActionDelete = '<%=Session["ActionDelete"]%>';
            ActionView = '<%=Session["ActionView"]%>';

            pLoadingSetup(false);
            GetCompanyInformation();
            pLoadingSetup(true);
        });


        //$("#txtEmail").change(function(){
        //    if ($("#txtEmail").val().length > 2) {
        //        var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        //        if (!regex.test($("#txtEmail").val)) {
        //            $.jGrowl("Please enter valid Email", { sticky: false, theme: 'warning', life: jGrowlLife });
        //            $("#divEmail").addClass('has-error'); $("#txtEmail").focus(); return false;
        //        } else { $("#divEmail").removeClass('has-error'); }
        //        }
        //})


        $("#btnSaveChanges").click(function () {
            if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }

            if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }

            if ($("#txtName").val().trim() == "" || $("#txtName").val().trim() == undefined) {
                $.jGrowl("Please enter Company Name", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divName").addClass('has-error'); $("#txtName").focus(); return false;
            } else { $("#divName").removeClass('has-error'); }

            if ($("#txtCode").val().trim() == "" || $("#txtCode").val().trim() == undefined) {
                $.jGrowl("Please enter Code", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divCode").addClass('has-error'); $("#txtCode").focus(); return false;
            } else { $("#divCode").removeClass('has-error'); }

            if ($("#txtAddress").val().trim() == "" || $("#txtAddress").val().trim() == undefined) {
                $.jGrowl("Please enter Address", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divAddress").addClass('has-error'); $("#txtAddress").focus(); return false;
            } else { $("#divAddress").removeClass('has-error'); }

            if ($("#txtPhoneNo1").val().trim() == "" || $("#txtPhoneNo1").val().trim() == undefined) {
                $.jGrowl("Please enter Phone No", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divPhoneNo1").addClass('has-error'); $("#txtPhoneNo1").focus(); return false;
            } else { $("#divPhoneNo1").removeClass('has-error'); }

            if ($("#txtEmail").val().length > 0) {
                var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (!regex.test($("#txtEmail").val())) {
                    $.jGrowl("Please enter valid email", { sticky: false, theme: 'warning', life: jGrowlLife });
                    return false;
                }
            }

            if ($("#txtFinancialYear").val().trim() == "" || $("#txtFinancialYear").val().trim() == undefined) {
                $.jGrowl("Please enter FinancialYear", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divFinancialYear").addClass('has-error'); $("#txtFinancialYear").focus();
                return false;
            } else { $("#divFinancialYear").removeClass('has-error'); }


            SaveandUpdateCompanyInformation();

            return false;
        });

        function ClearFields() {
            $("#txtName").val("");
            $("#txtCode").val("");
            $("#txtAddress").val("");
            $("#txtContactPerson").val("");
            $("#txtContactNo").val("");
            $("#txtPhoneNo1").val("");
            $("#txtPhoneNo2").val("");
            $("#txtLandline").val("");
            $("#txtFax").val("");
            $("#txtIEC").val("");
            $("#txtCSTNo").val("");
            $("#txtEmail").val("");
            $("#txtWebSite").val("");
            $("#txtFinancialYear").val("");
            $("#txtCINNo").val("");
            $("#txtTerms").val("");
            $("#txtNotes").val("");
            // $("#txtIEC").val("");

            $("#divName").removeClass('has-error');
            $("#divCode").removeClass('has-error');
            $("#divAddress").removeClass('has-error');
            $("#divPhoneNo1").removeClass('has-error');
            return false;
        }

        function SaveandUpdateCompanyInformation() {
            var Obj = new Object();
            Obj.CompanyID = 0;
            Obj.CompanyName = $("#txtName").val().trim();
            Obj.CompanyCode = $("#txtCode").val().trim();
            Obj.CompanyAddress = $("#txtAddress").val().trim();
            Obj.ContactPerson = $("#txtContactPerson").val().trim();
            Obj.ContactNo = $("#txtContactNo").val().trim();
            Obj.PhoneNo1 = $("#txtPhoneNo1").val().trim();
            Obj.PhoneNo2 = $("#txtPhoneNo2").val().trim();
            Obj.LandLine = $("#txtLandline").val().trim();
            Obj.Fax = $("#txtFax").val().trim();
            Obj.Email = $("#txtEmail").val().trim();
            Obj.WebSite = $("#txtWebSite").val().trim();
            Obj.TINNo = $("#txtIEC").val().trim();
            Obj.CSTNo = $("#txtCSTNo").val().trim().toUpperCase();
            Obj.FinancialYear = $("#txtFinancialYear").val().trim();
            Obj.CINNo = $("#txtCINNo").val().trim();
            //  Obj.IEC = $("#txtIEC").val().trim();
            Obj.BankName = $("#txtBankName").val().trim();
            Obj.BranchName = $("#txtBranchName").val().trim();
            Obj.AccountNo = $("#txtAccountNo").val().trim();
            Obj.IFSCCode = $("#txtIFSCCode").val().trim();
            Obj.Tream = $("#txtTerms").val();
            Obj.CompanyNote = $("#txtNotes").val();

            if ($("#hdnCompanyID").val() > 0) {
                Obj.CompanyID = $("#hdnCompanyID").val();
                sMethodName = "UpdateCompany";
            }
            else { sMethodName = "AddCompany"; }

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
                                if (sMethodName == "AddCompany") {
                                    $.jGrowl("Saved Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                                    $("#hdnCompanyID").val(objResponse.Value);
                                }
                                else if (sMethodName == "UpdateCompany") { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                                GetCompanyInformation();
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Company_A_01") {
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

        function GetCompanyInformation() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetCompany",
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

                                    $("#hdnCompanyID").val(obj.CompanyID);
                                    $("#txtName").val(obj.CompanyName);
                                    $("#txtCode").val(obj.CompanyCode);
                                    $("#txtAddress").val(obj.CompanyAddress);
                                    $("#txtContactPerson").val(obj.ContactPerson);
                                    $("#txtContactNo").val(obj.ContactNo);
                                    $("#txtPhoneNo1").val(obj.PhoneNo1);
                                    $("#txtPhoneNo2").val(obj.PhoneNo2);
                                    $("#txtLandline").val(obj.LandLine);
                                    $("#txtFax").val(obj.Fax);
                                    $("#txtEmail").val(obj.Email);
                                    $("#txtWebSite").val(obj.WebSite);
                                    $("#txtIEC").val(obj.TINNo);
                                    $("#txtCSTNo").val(obj.CSTNo);
                                    $("#txtBankName").val(obj.BankName);
                                    $("#txtBranchName").val(obj.BranchName);
                                    $("#txtAccountNo").val(obj.AccountNo);
                                    $("#txtIFSCCode").val(obj.IFSCCode);
                                    $("#txtFinancialYear").val(obj.FinancialYear);
                                    $("#txtCINNo").val(obj.CINNo);
                                    $("#txtTerms").val(obj.Tream);
                                    $("#txtNotes").val(obj.CompanyNote);
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


