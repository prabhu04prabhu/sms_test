<%@ Page Title="Scheme Close" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmChitCancel.aspx.cs" Inherits="frmChitCancel" %>


<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Scheme Close
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Master</a></li>
                <li class="active">Scheme Close</li>
            </ol>
            <div class="pull-right">
                <button id="btnAddNew" class="btn btn-info">
                    <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add New</button>
            </div>
            <br />
            <br />
        </section>
         <section class="content">
            <div class="nav-tabs-custom" id="divTab">
                <ul class="nav nav-tabs">
                    <li class="active"><a id="aGeneral" href="#General" data-toggle="tab">General</a></li>
                    <li><a id="aSearchResult" href="#SearchResult" data-toggle="tab">Search Result</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="General">
                        <div class="box box-warning">
                            <div class="box-body">
                                <div class="table-responsive">
                                   <table id="tblRecord" class="table table-striped table-bordered bg-info" width="100%">
                                    <thead>
                                        <tr>
                                            <th>SNo
                                            </th>
                                            <th>Account No
                                            </th>
                                            <th>Customer
                                            </th>
                                            <th class="hidden-xs">Scheme
                                            </th>
                                            <th class="hidden-xs">Employee
                                            </th>
                                            <th class="hidden-xs">Duration
                                            </th>
                                            <th class="hidden-xs">Amount
                                            </th>
                                            <th class="hidden-xs">Bonus Amount
                                            </th>
                                            <th></th>
                                            </tr>
                                    </thead>
                                    <tbody id="tblRecord_tbody">
                                    </tbody>
                                </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="SearchResult">
                        <div class="box box-warning">
                            <div class="box-body">
                                <div class="form-group col-md-4" id="divSearchaname">
                                    <label>
                                        Search records</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtSearchName" placeholder="Please enter search details"
                                        maxlength="150" />
                                </div>
                                <div class="form-group col-md-8"></div>
                                <div class="form-group col-md-12">
                                    <div class="table-responsive">
                                        <table id="tblSearchResult" class="table table-striped table-bordered bg-info" width="100%">
                                             <thead>
                                        <tr>
                                            <th>SNo
                                            </th>
                                            <th>Account No
                                            </th>
                                            <th>Customer
                                            </th>
                                            <th class="hidden-xs">Scheme
                                            </th>
                                            <th class="hidden-xs">Employee
                                            </th>
                                            <th class="hidden-xs">Duration
                                            </th>
                                            <th class="hidden-xs">Amount
                                            </th>
                                            <th class="hidden-xs">Bonus Amount
                                            </th>
                                            <th></th>
                                            </tr>
                                    </thead>
                                            <tbody id="tblSearchResult_tbody">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="compose-modal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-md-3" id="divAccountNo">
                                    <label>
                                        Account No</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtAccountNo" placeholder="Account No"
                                        maxlength="15" tabindex="1" value="0"  autocomplete="off"/>
                                </div>
                                <div class="form-group col-md-3" id="divDOB">
                                    <label>
                                        Date</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDate"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <input type="text" class="form-control pull-right" tabindex="2" id="txtDate" readonly="true" disabled="disabled" />
                                    </div>
                                </div>

                                <div class="form-group col-md-3" id="divCustomer">
                                    <label>
                                        Customer</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtCustomer" placeholder="Customer"
                                        maxlength="15" tabindex="-1" readonly="true" />
                                </div>

                                <div class="form-group col-md-3" id="divAddress">
                                    <label>
                                        Address</label><span class="text-danger">*</span>
                                    <textarea id="txtAddress" class="form-control" maxlength="250" tabindex="-1" rows="3" readonly="true"></textarea>
                                </div>
                                <div class="form-group col-md-3" id="divPhone">
                                    <label>
                                        Phone</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtPhone" placeholder="Phone"
                                        maxlength="15" tabindex="-1" readonly="true" />
                                </div>
                                <div class="form-group col-md-3" id="divChit">
                                    <label>
                                        Scheme</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtChit" placeholder="Scheme"
                                        maxlength="15" tabindex="-1" readonly="true" />
                                </div>
                                <div class="form-group col-md-3" id="divDuration">
                                    <label>
                                        Duration</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtDuration" placeholder="Duration"
                                        maxlength="2" tabindex="-1" readonly="true" onkeypress="return isNumberKey(event)" />
                                </div>
                                <div class="form-group col-md-3" id="divInstallmentAmount">
                                    <label>
                                        Installment Amount</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtInstallmentAmount" placeholder="Installment Amount"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-3" id="divTotalAmount">
                                    <label>
                                        Total Amount</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtTotalAmount" placeholder="Total Amount"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-3" id="divBonusAmount">
                                    <label>
                                        Bonus Amount</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtBonusAmount" placeholder="Bonus Amount"
                                        maxlength="12" tabindex="3" onkeypress="return IsNumeric(event)" />
                                </div>
                                <div class="form-group col-md-3" id="divGrossAmount">
                                    <label>
                                        Gross Amount</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtGrossAmount" placeholder="Gross Amount"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                 <div class="form-group col-md-3" id="divRenewalCount">
                                    <label>
                                        Paid Due</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtRenewalCount" placeholder="Paid Due"
                                        maxlength="2" tabindex="-1" onkeypress="return isNumberKey(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-3" id="divIDProof">
                                    <label>
                                        ID Proof
                                    </label>
                                    <span class="text-danger">*</span>
                                    <select id="ddlIDProof" class="form-control" tabindex="-1" disabled="disabled">
                                        <option value="Aadhar">Aadhar</option>
                                        <option value="Ration Card">Ration Card</option>
                                        <option value="Voter ID">Voter ID</option>
                                        <option value="Driving License">Driving License</option>
                                        <option value="PAN">PAN</option>
                                    </select>
                                </div>
                                <div class="form-group col-md-3" id="divIDNo" hidden="hidden">
                                    <label>
                                        ID No</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtIDNo" placeholder="ID No"
                                        maxlength="15" tabindex="-1" readonly="true" />

                                </div>
                                <div class="form-group col-md-3" id="divEmployee">
                                    <label>
                                        Employee</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtEmployee" placeholder="Employee"
                                        maxlength="15" tabindex="-1" readonly="true" />
                                </div>

                                <%--<div class="form-group col-md-12" id="divReason">
                                    <label>
                                        Reason for Cancellation</label>
                                    <textarea id="txtReason" class="form-control" maxlength="250" tabindex="3" rows="3"></textarea>
                                </div>--%>
                                <div class="form-group col-md-4" id="divOtherPassword">
                                    <label>
                                        Enter Special Password</label><span class="text-danger">*</span>
                                    <input type="password" class="form-control" id="txtOtherPassword" placeholder="Special Password" maxlength="512"
                                        tabindex="5" />
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Proof Image</label>
                                    <asp:Image ID="ProofimgCapture" runat="server" Style="visibility: hidden; width: 240px; height: 225px" />
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Register Front Image</label>
                                    <asp:Image ID="Register1Capture" runat="server" Style="visibility: hidden; width: 240px; height: 225px" />
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Register Back Image</label>
                                    <asp:Image ID="Register2Capture" runat="server" Style="visibility: hidden; width: 240px; height: 225px" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="button" class="btn btn-danger pull-left" id="btnClose" tabindex="13">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                            <button type="submit" class="btn btn-info pull-right" id="btnSave" tabindex="11">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="submit" class="btn btn-info pull-right" id="btnUpdate" tabindex="12">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnID" />
    <input type="hidden" id="hdRS" />
    <input type="hidden" id="hdnRegisterID" />
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

            $("#SdSMS").val(_SendSMS);
            $("#SMSsendername").val(_SMSsendername);
            $("#SMSpassword").val(_SMSpassword);
            $("#SMSurl").val(_SMSurl);
            $("#SMSusername").val(_SMSusername);

            if (ActionAdd != "1") {
                $("#btnAddNew").remove();
                $("#btnSave").remove();
            }

            if (ActionUpdate != "1") {
                $("#btnUpdate").remove();
            }

            $("#txtDate").attr("data-link-format", "dd/MM/yyyy");

            $("#txtDate").datetimepicker({
                pickTime: false,
                useCurrent: true,
                maxDate: moment(),
                format: 'DD/MM/YYYY'
            });

            pLoadingSetup(false);
            pLoadingSetup(true);
            //GetCustomer();
            //GetEmployee();
            //GetChit
            GetSettings();
            GetPassword();
            GetRecord();
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
                                    $("#hdnMaxDiscount").val(obj.MaxDiscountPercent);
                                    $("#hdnOpeningDate").val(obj.sOpeningDate);
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
        $("#txtAccountNo").change(function () {
            if ($("#txtAccountNo").val().length > 0)
                GetRegistereddetails($("#txtAccountNo").val());
        });

        function GetRegistereddetails(id) {
            var iactive = "CLOSE";
            if ($("#hdnID").val() > 0)
                iactive = "All";
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetRegisterByNo",
                data: JSON.stringify({ ID: id, IsActive: iactive }),
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
                                    $("#txtIDNo").val(obj.IDNo);
                                    $("#hdnID").val(obj.RegisterID);
                                    $("#txtAccountNo").val(obj.AccountNo);
                                    $("#hdnRegisterID").val(obj.RegisterID);
                                    $("#txtCustomer").val(obj.CustomerName);
                                    $("#txtChit").val(obj.Chit.ChitName);
                                    $("#txtRegisteredEmployee").val(obj.Employee.EmployeeName);
                                    $("#txtDuration").val(obj.Chit.Duration);
                                    $("#txtInstallmentAmount").val(obj.InstallmentAmount);
                                    $("#txtAddress").val(obj.Address);
                                    $("#txtRenewalCount").val(obj.RenewalCount);
                                    $("#txtPhone").val(obj.MobileNo);
                                    $("#txtEmployee").val(obj.Employee.EmployeeName);
                                    $("#ddlIDProof").val(obj.IDProof);
                                    $("#txtAmountPaid").val(obj.InstallmentAmount);
                                    $("[id*=ProofimgCapture]").css("visibility", "visible");
                                    $("[id*=ProofimgCapture]").attr("src", obj.ProofImage);
                                    $("[id*=Register1Capture]").css("visibility", "visible");
                                    $("[id*=Register1Capture]").attr("src", obj.RegisterImage1);
                                    $("[id*=Register2Capture]").css("visibility", "visible");
                                    $("[id*=Register2Capture]").attr("src", obj.RegisterImage2);
                                    $("#txtInstallmentAmount").change();
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

        $("#txtDuration,#txtInstallmentAmount,#txtBonusAmount").change(function () {
            var Dur = 0;
            var Installamt = 0;
            var Bonus = 0;
            var totAmt = 0;
            var Grossamt = 0;

            if ($("#txtDuration").val() > 0)
                Dur = $("#txtDuration").val();

            if ($("#txtInstallmentAmount").val() > 0)
                Installamt = $("#txtInstallmentAmount").val();

            if ($("#txtBonusAmount").val() > 0)
                Bonus = $("#txtBonusAmount").val();
            totAmt = parseFloat(Dur) * parseFloat(Installamt);
            $("#txtTotalAmount").val(totAmt.toFixed(2));
            Grossamt = parseFloat(Bonus) + parseFloat(totAmt);
            $("#txtGrossAmount").val(Grossamt.toFixed(2));
        });
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
            ClearFields();
            $("#hdnID").val("");
            $("#btnSave").show();
            $("#btnUpdate").hide();
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Scheme Close");
            $('#compose-modal').modal({ show: true, backdrop: true });
            $("#txtAccountNo").focus();
            return false;
        });

        $("#btnSave,#btnUpdate").click(function () {
            if (this.id == "btnSave")
            { if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
            else { if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

            //if ($("#txtReason").val().trim() == "" || $("#txtReason").val().trim() == undefined) {
            //    $.jGrowl("Please enter Reason", { sticky: false, theme: 'warning', life: jGrowlLife });
            //    $("#divReason").addClass('has-error'); $("#txtReason").focus(); return false;
            //} else { $("#divReason").removeClass('has-error'); }

            var d1 = Date.parse($("#hdnOpeningDate").val());
            var d2 = Date.parse($("#txtDate").val());
            if (d1 < d2) {
                $.jGrowl("Date not updated yet. Contact Administrator", { sticky: false, theme: 'warning', life: jGrowlLife });
                return false;
            }

            if ($("#hdnRegisterID").val().trim() == "" || $("#hdnRegisterID").val().trim() == undefined || $("#hdnRegisterID").val() <= 0) {
                $.jGrowl("Please enter Account No", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divAccountNo").addClass('has-error'); $("#hdnRegisterID").focus(); return false;
            } else { $("#divAccountNo").removeClass('has-error'); }

            if ($("#txtDate").val().trim() == "" || $("#txtDate").val().trim() == undefined) {
                $.jGrowl("Please select Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divDate").addClass('has-error'); $("#txtDate").focus(); return false;
            }
            else { $("#divDate").removeClass('has-error'); }

            if ($("#txtOtherPassword").val().trim() == "" || $("#txtOtherPassword").val().trim() == undefined || $("#txtOtherPassword").val().trim() != $("#hdRS").val()) {
                $.jGrowl("Please enter Valid Password", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divOtherPassword").addClass('has-error'); $("#txtOtherPassword").focus(); return false;
            } else { $("#divOtherPassword").removeClass('has-error'); }

            //if ($("#txtMobileNo").val().trim() == "" || $("#txtMobileNo").val().trim() == undefined) {
            //    $.jGrowl("Please enter Register", { sticky: false, theme: 'warning', life: jGrowlLife });
            //    $("#divMobileNo").addClass('has-error'); $("#txtMobileNo").focus(); return false;
            //} else { $("#divMobileNo").removeClass('has-error'); }

            var Obj = new Object();
            Obj.RegisterID = 0;
            Obj.ReasonforCancel = "";
            Obj.BonusAmount = $("#txtBonusAmount").val();
            Obj.RegisterID = $("#hdnID").val();

            var sMethodName;
            if ($("#hdnID").val() > 0) {
                Obj.RegisterID = $("#hdnID").val();
                sMethodName = "UpdateCancelledRegister";
            }
            else { sMethodName = "UpdateCancelledRegister"; }

            SaveandUpdateRegister(Obj, sMethodName);

            return false;
        });

        $("#btnClose").click(function () {
            $('#compose-modal').modal('hide');
            return false;
        });

        function ClearFields() {
            $("#txtIDNo").val("");
            $("#txtAccountNo").val("");
            //$("#ddlCustomer").val(null).change();
            //$("#ddlChit").val(null).change();
            //$("#ddlEmployee").val(null).change();
            $("#ddlIDProof").val(null).change();
            $("#txtDuration").val(0);
            $("#txtInstallmentAmount").val(0);
            $("#txtTotalAmount").val(0);
            $("#txtBonusAmount").val(0);
            $("#txtGrossAmount").val(0);
            $("#txtAddress").val("");
            $("#txtPhone").val("");
            $("#txtReason").val("");
            $("#txtOtherPassword").val("");
            $("#txtRenewalCount").val(0);
            var d = new Date().getDate();
            var m = new Date().getMonth() + 1; // JavaScript months are 0-11
            var y = new Date().getFullYear();
            $("#txtDate").val(d + "/" + m + "/" + y);
            return false;
        }

        function GetRecord() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetCancelledRegister",
                data: JSON.stringify({ RegisterID: 0 }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            var notetable = $("#tblRecord").dataTable();
                            notetable.fnDestroy();
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    $("#tblRecord_tbody").empty();
                                    var TypeStatus = "";
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive == "1")
                                        { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                        else
                                        { TypeStatus = "<span class='label label-warning'>Inactive</span>"; }

                                        var table = "<tr id='" + obj[index].RegisterID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].AccountNo + "</td>";
                                        table += "<td>" + obj[index].CustomerName + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Chit.ChitName + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Employee.EmployeeName + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Chit.Duration + "</td>";                                        
                                        table += "<td class='hidden-xs'>" + obj[index].ChitAmount + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].BonusAmount + "</td>";

                                        if (ActionView == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].RegisterID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        //if (ActionUpdate == "1")
                                        //{ table += "<td style='text-align:center;'><a href='#' id=" + obj[index].RegisterID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        //else
                                        //{ table += "<td></td>"; }

                                        //if (ActionDelete == "1")
                                        //{ table += "<td style='text-align:center;'><a href='#' id=" + obj[index].RegisterID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        //else
                                        //{ table += "<td></td>"; }
                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Register");
                                            $("#btnSave").hide();
                                            $("#btnUpdate").hide();
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Print").click(function () {
                                        var iRegisterID = parseInt($(this).parent().parent()[0].id);
                                        var iBarcode = $(this).attr('Barcode');
                                        $.cookie("Barcode", iBarcode);
                                        $.cookie("RegisterID", iRegisterID);
                                        window.location = "frmBarcode.aspx";
                                    });
                                    $(".Edit").click(function () {
                                        if (ActionUpdate == "1")
                                        { EditRecord($(this).parent().parent()[0].id); }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Delete").click(function () {
                                        if (ActionDelete == "1") {
                                            if (confirm('Are you sure to delete the selected record ?'))
                                            { DeleteRecord($(this).parent().parent()[0].id); }
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#tblRecord_tbody").empty();
                                dProgress(false);
                            }
                            $("#tblRecord").dataTable({
                                "bPaginate": true,
                                "bFilter": true,
                                "bSort": true,
                                "iDisplayLength": 25,
                                aoColumns: [
                                  { "sWidth": "5%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "5%" },
                                  //{ "sWidth": "3%" },
                                  //{ "sWidth": "3%" },
                                  { "sWidth": "3%" },
                                  { "sWidth": "3%" }
                                ]
                            });
                            $("#tblRecord_filter").addClass('pull-right');
                            $(".pagination").addClass('pull-right');
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location("frmLogin.aspx");
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                        }
                    }
                    else {
                        $("#tblRecord_tbody").empty();
                        dProgress(false);
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    dProgress(false);
                }
            });
            return false;
        }

        function GetSearchRecord(iDetails) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/SearchChitClosed",
                data: JSON.stringify({ ID: iDetails }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            var notetable = $("#tblSearchResult").dataTable();
                            notetable.fnDestroy();
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    $("#tblSearchResult_tbody").empty();
                                    var TypeStatus = "";
                                    for (var index = 0; index < obj.length; index++) {

                                        var table = "<tr id='" + obj[index].RegisterID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].AccountNo + "</td>";
                                        table += "<td>" + obj[index].CustomerName + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Chit.ChitName + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Employee.EmployeeName + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Chit.Duration + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].TotalAmount + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].BonusAmount + "</td>";

                                        if (ActionView == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].RegisterID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        //if (ActionUpdate == "1")
                                        //{ table += "<td style='text-align:center;'><a href='#' id=" + obj[index].RegisterID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        //else
                                        //{ table += "<td></td>"; }

                                        //if (ActionDelete == "1")
                                        //{ table += "<td style='text-align:center;'><a href='#' id=" + obj[index].RegisterID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        //else
                                        //{ table += "<td></td>"; }
                                        table += "</tr>";
                                        $("#tblSearchResult_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View ChitClosed");
                                            $("#btnSave").hide();
                                            $("#btnUpdate").hide();
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    //$(".Print").click(function () {
                                    //    var iRenewalID = parseInt($(this).parent().parent()[0].id);
                                    //    var iBarcode = $(this).attr('Barcode');
                                    //    $.cookie("Barcode", iBarcode);
                                    //    $.cookie("RenewalID", iRenewalID);
                                    //    window.location = "frmBarcode.aspx";
                                    //});
                                    $(".Edit").click(function () {
                                        if (ActionUpdate == "1")
                                        { EditRecord($(this).parent().parent()[0].id); }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Delete").click(function () {
                                        if (ActionDelete == "1") {
                                            if (confirm('Are you sure to delete the selected record ?'))
                                            { DeleteRecord($(this).parent().parent()[0].id); }
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#tblSearchResult_tbody").empty();
                                dProgress(false);
                            }
                            $("#tblSearchResult").dataTable({
                                "bPaginate": true,
                                "bFilter": true,
                                "bSort": true,
                                "iDisplayLength": 25,
                                aoColumns: [
                                  { "sWidth": "5%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "5%" },
                                  //{ "sWidth": "3%" },
                                  //{ "sWidth": "3%" },
                                  { "sWidth": "3%" },
                                  { "sWidth": "3%" }
                                ]
                            });
                            $("#tblSearchResult_filter").addClass('pull-right');
                            $(".pagination").addClass('pull-right');
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location("frmLogin.aspx");
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                        }
                    }
                    else {
                        $("#tblSearchResult_tbody").empty();
                        dProgress(false);
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    dProgress(false);
                }
            });
            return false;
        }

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
                                GetRecord();
                                ClearFields();
                                if (sMethodName == "AddRegister")
                                {
                                    $.jGrowl("Closed Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                                    //if ($("#SdSMS").val() == "True")
                                        //GetSMSRecord($("#hdnID").val(), "");
                                }
                                else if (sMethodName == "UpdateRegister")
                                { $.jGrowl("Closed Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                                $('#compose-modal').modal('hide');

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

        function GetSMSRecord(id, messageType) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetRegisterByID",
                data: JSON.stringify({ ID: id }),
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

                                    var SMSMsg = "Dear " + obj.CustomerName + ", Your " + obj.Chit.ChitName + " Scheme, Account No.: " + obj.AccountNo + " has closed - SVS Jewellers.";
                                    //SMSMsg = SMSMsg.replace("#jobcardno#", obj.JobCardNo);
                                    ////var test = ;
                                    //if (SMSMsg.match(/#brandmodel#/))
                                    //    SMSMsg = SMSMsg.replace("#brandmodel#", obj.Model.Brand.BrandName + " - " + obj.Model.ModelName);

                                    //var sComplaints = obj.sComplaints.split(',');
                                    //var Complaint = "";
                                    //for (var i = 0; i < sComplaints.length; i++)
                                    //    Complaint += CapitalizeFirstLetterEachWord(sComplaints[i]);
                                    ////if (SMSMsg.includes("#complaints#"))
                                    //if (SMSMsg.match(/#complaints#/))
                                    //    SMSMsg = SMSMsg.replace("#complaints#", Complaint);
                                    //if (SMSMsg.match(/#companyname#/))
                                    //    SMSMsg = SMSMsg.replace("#companyname#", CompanyName);
                                    //if (SMSMsg.match(/#phone#/))
                                    //    SMSMsg = SMSMsg.replace("#phone#", PhoneNo1);
                                    ////if (SMSMsg.includes("#servicecost#"))
                                    //if (SMSMsg.match(/#servicecost#/))
                                    //    SMSMsg = SMSMsg.replace("#servicecost#", obj.InvoiceAmount);
                                    ////var SMSUsername = SMS;
                                    ////var SMSPassword = Session["SMSPassword"];

                                    // $("#SdSMS").val(_SendSMS);

                                    // var test = $("#SdSMS").val();
                                    var APILink = $("#SMSurl").val();

                                    if (APILink.match(/#message#/))
                                        APILink = APILink.replace("#message#", SMSMsg);
                                    if (APILink.match(/#uname#/))
                                        APILink = APILink.replace("#uname#", $("#SMSusername").val());
                                    if (APILink.match(/#pwd#/))
                                        APILink = APILink.replace("#pwd#", $("#SMSpassword").val());
                                    if (APILink.match(/#mobile#/))
                                        APILink = APILink.replace("#mobile#", obj.MobileNo);
                                    if (APILink.match(/#sendername#/))
                                        APILink = APILink.replace("#sendername#", $("#SMSsendername").val());

                                    SendSMS(obj.MobileNo, APILink);
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
        function EditRecord(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetRegisterByID",
                data: JSON.stringify({ ID: id }),
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
                                    $("#btnSave").hide();
                                    $("#btnUpdate").show();

                                    $("#hdnID").val(obj.RegisterID);
                                    $("#txtDate").val(obj.sRegisterDate);
                                    $("#txtAccountNo").val(obj.AccountNo).change();
                                    $("#ddlChit").val(obj.Chit.ChitID).change();
                                    $("#ddlEmployee").val(obj.Employee.UserID).change();
                                    $("#ddlIDProof").val(obj.IDProof).change();
                                    $("#txtIDNo").val(obj.IDNo);
                                   
                                    $('#compose-modal').modal({ show: true, backdrop: true });
                                    $("[id*=ProofimgCapture]").css("visibility", "visible");
                                    $("[id*=ProofimgCapture]").attr("src", obj.ProofImage);
                                    $("[id*=Register1Capture]").css("visibility", "visible");
                                    $("[id*=Register1Capture]").attr("src", obj.RegisterImage1);
                                    $("[id*=Register2Capture]").css("visibility", "visible");
                                    $("[id*=Register2Capture]").attr("src", obj.RegisterImage2);
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit Scheme Close");
                                    GetRegistereddetails(obj.AccountNo);
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

        function DeleteRecord(id) {
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/DeleteRegister",
                data: JSON.stringify({ ID: id }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value > 0) {
                                $.jGrowl("Deleted Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                                ClearFields();
                                GetRecord();
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Register_R_01" || objResponse.Value == "Register_D_01") {
                                $.jGrowl(_CMDeleteError, { sticky: false, theme: 'danger', life: jGrowlLife });
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

        $("#txtSearchName").change(function () {

            if ($("#txtSearchName").val().trim() != "" || $("#txtSearchName").val().trim() != undefined) {
                var iDetails = $("#txtSearchName").val();
                GetSearchRecord(iDetails);
            }
            return false;
        });


        $("#aGeneral").click(function () {

            $("#SearchResult").hide();
            GetRecord();
        });

        $("#aSearchResult").click(function () {

            $("#SearchResult").show();

        });
    </script>
</asp:Content>







