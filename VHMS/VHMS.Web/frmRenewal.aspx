<%@ Page Title="Renewal" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmRenewal.aspx.cs" Inherits="frmRenewal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Renewal
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Master</a></li>
                <li class="active">Renewal</li>
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
                                                <th>Renewal No
                                                </th>
                                                <th>Account No
                                                </th>
                                                <th>Date
                                                </th>
                                                <th class="hidden-xs">Customer
                                                </th>
                                                <th class="hidden-xs">Scheme
                                                </th>
                                                <th class="hidden-xs">Employee Code
                                                </th>
                                                <th class="hidden-xs">Duration
                                                </th>
                                                <th class="hidden-xs">Net Amount
                                                </th>
                                                <th class="hidden-xs">Amount Paid
                                                </th>
                                                <th></th>
                                                <th></th>
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
                                                    <th>Renewal No
                                                    </th>
                                                    <th>Account No
                                                    </th>
                                                    <th>Date
                                                    </th>
                                                    <th class="hidden-xs">Customer
                                                    </th>
                                                    <th class="hidden-xs">Scheme
                                                    </th>
                                                    <th class="hidden-xs">Employee Code
                                                    </th>
                                                    <th class="hidden-xs">Duration
                                                    </th>
                                                    <th class="hidden-xs">Net Amount
                                                    </th>
                                                    <th class="hidden-xs">Amount Paid
                                                    </th>
                                                    <th></th>
                                                    <th></th>
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
                                        Enter Account No</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtAccountNo" placeholder="Account No"
                                        maxlength="15" tabindex="1" value="0" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3" id="divDOB">
                                    <label>
                                        Renewal Date</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDate"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <input type="text" class="form-control pull-right" tabindex="2" id="txtDate" />
                                    </div>
                                </div>
                                <div class="form-group col-md-3" id="divRenewalNo" hidden="hidden">
                                    <label>
                                        Renewal No</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtRenewalNo" placeholder="Renewal No"
                                        maxlength="15" tabindex="-1" value="0" readonly="true" />
                                </div>
                                <div class="form-group col-md-3" id="divCustomer">
                                    <label>
                                        Customer</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtCustomer" placeholder="Customer"
                                        maxlength="2" tabindex="-1" readonly="true" />
                                </div>
                                <div class="form-group col-md-3" id="divAddress">
                                    <label>
                                        Address</label><span class="text-danger">*</span>
                                    <textarea id="txtAddress" class="form-control" maxlength="250" tabindex="-1" rows="3" readonly="true"></textarea>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-3" id="divPhone">
                                    <label>
                                        Phone</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtPhone" placeholder="Phone"
                                        maxlength="15" tabindex="-1" readonly="true" />
                                </div>
                                <div class="form-group col-md-3" id="divChit">
                                    <label>
                                        Scheme</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtChit" placeholder="Scheme"
                                        maxlength="2" tabindex="-1" readonly="true" />
                                </div>
                                <div class="form-group col-md-3" id="divDuration">
                                    <label>
                                        Duration</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtDuration" placeholder="Duration"
                                        maxlength="2" tabindex="-1" onkeypress="return isNumberKey(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-3" id="divRenewalCount">
                                    <label>
                                        Paid Due</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtRenewalCount" placeholder="Paid due"
                                        maxlength="2" tabindex="-1" onkeypress="return isNumberKey(event)" readonly="true" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-3" id="divInstallmentAmount" hidden="hidden">
                                    <label>
                                        Installment Amount</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtInstallmentAmount" placeholder="Installment Amount"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-3" id="divTotalAmount" hidden="hidden">
                                    <label>
                                        Total Amount</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtTotalAmount" placeholder="Total Amount"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-3" id="divBonusAmount" hidden="hidden">
                                    <label>
                                        Bonus Amount</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtBonusAmount" placeholder="Bonus Amount"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-3" id="divGrossAmount" hidden="hidden">
                                    <label>
                                        Gross Amount</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtGrossAmount" placeholder="Gross Amount"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>

                                <div class="form-group col-md-3" id="divIDProof" hidden="hidden">
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
                                <div class="form-group col-md-3" id="divRegisteredEmployee">
                                    <label>
                                        Registered Employee</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtRegisteredEmployee" placeholder="Employee"
                                        maxlength="12" tabindex="-1" readonly="true" />

                                </div>
                                <div class="form-group col-md-3" id="divAmountPaid">
                                    <label>
                                        Amount Paid</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtAmountPaid" placeholder="0"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-3" id="divEmployee">
                                    <label>
                                        Collected Employee Code</label><span class="text-danger">*</span>
                                    <select id="ddlEmployee" class="form-control" tabindex="4">
                                        <option selected="selected" value="0">--Select Employee--</option>
                                    </select>
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
            <div class="modal fade" id="Renewalmodal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group  col-md-1"></div>
                            <div class="form-group  col-md-3">
                                <label>
                                    Receipt No</label><span class="text-danger">*</span>
                            </div>
                            <div class="form-group  col-md-3" id="divReason">
                                <input type="text" class="form-control" id="txtReceiptNo" placeholder=""
                                    maxlength="150" tabindex="-1" readonly="true" />
                            </div>

                        </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-right" id="btnOK" tabindex="21">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                OK</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="deletemodal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title">Delete Record</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group  col-md-1"></div>
                            <div class="form-group  col-md-4">
                                <label>
                                    Special Password</label><span class="text-danger">*</span>
                            </div>
                            <input type="hidden" id="hdnDeleteRegisterID" />
                            <div class="form-group  col-md-6" id="divOtherPassword">
                                <input type="text" class="form-control" id="txtOtherPassword" placeholder=""
                                    maxlength="150" tabindex="39" autocomplete="off"/>
                            </div>

                        </div>
                        <div class="modal-footer clearfix">
                            <button type="button" class="btn btn-danger pull-left" id="btncancel" tabindex="40">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                            <button type="submit" class="btn btn-info pull-right" id="btndelete" tabindex="41">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                OK</button>

                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnRegisterID" />
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
            GetPassword();
            GetEmployee();
            //GetChit();
            GetSettings();
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
            var iactive = "Open";
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
                                    $("#txtAccountNo").val(obj.AccountNo);
                                    $("#hdnRegisterID").val(obj.RegisterID);
                                    $("#txtCustomer").val(obj.CustomerName);
                                    $("#txtChit").val(obj.Chit.ChitName);
                                    $("#txtRegisteredEmployee").val(obj.Employee.EmployeeName);
                                    // $("#ddlIDProof").val(obj.IDProof).change();
                                    $("#txtDuration").val(obj.Chit.Duration);
                                    $("#txtInstallmentAmount").val(obj.InstallmentAmount);
                                    //$("#txtTotalAmount").val(obj.Chit.TotalAmount);
                                    //$("#txtBonusAmount").val(obj.Chit.ChitName);
                                    //$("#txtGrossAmount").val(obj.Chit.GrandTotal);
                                    $("#txtAddress").val(obj.Address);
                                    $("#txtRenewalCount").val(obj.RenewalCount);
                                    $("#txtPhone").val(obj.MobileNo);
                                    $("#txtAmountPaid").val(obj.InstallmentAmount);

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
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Renewal");
            $('#compose-modal').modal({ show: true, backdrop: true });
            $("#txtDate").focus();
            $("#txtAccountNo").focus();
            return false;
        });
        function GetEmployee() {
            $("#ddlEmployee").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/Getuser",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ CountryID: 0 }),
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    $("#ddlCategory").append('<option value="' + '0' + '">' + '--Select State--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive == "1")
                                            $("#ddlEmployee").append('<option value=' + obj[index].UserID + ' >' + obj[index].EmployeeCode + '</option>');
                                    }
                                }
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlEmployee").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                            }
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
                        $("#ddlEmployee").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }

        $("#btnSave,#btnUpdate").click(function () {
            if (this.id == "btnSave") { if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
            else { if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

            //if ($("#txtName").val().trim() == "" || $("#txtName").val().trim() == undefined) {
            //    $.jGrowl("Please enter Renewal", { sticky: false, theme: 'warning', life: jGrowlLife });
            //    $("#divName").addClass('has-error'); $("#txtName").focus(); return false;
            //} else { $("#divName").removeClass('has-error'); }

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

            if ($("#ddlEmployee").val() == "0" || $("#ddlEmployee").val() == undefined) {
                $.jGrowl("Please Select Employee", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divEmployee").addClass('has-error'); $("#ddlEmployee").focus(); return false;
            } else { $("#divEmployee").removeClass('has-error'); }

            if ($("#txtAmountPaid").val() == "" || $("#txtAmountPaid").val() == "0" || $("#txtAmountPaid").val() == undefined || $("#txtAmountPaid").val() == null) {
                $.jGrowl("Please enter Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divAmountPaid").addClass('has-error'); $("#txtAmountPaid").focus(); return false;
            } else { $("#divAmountPaid").removeClass('has-error'); }

            //if ($("#txtAmountPaid").val() < 1000) {
            //    $.jGrowl("Please enter Amount greater than 999", { sticky: false, theme: 'warning', life: jGrowlLife });
            //    $("#divAmountPaid").addClass('has-error'); $("#txtAmountPaid").focus(); return false;
            //} else { $("#divAmountPaid").removeClass('has-error'); }

            if ($("#txtDate").val().trim() == "" || $("#txtDate").val().trim() == undefined) {
                $.jGrowl("Please select Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divDate").addClass('has-error'); $("#txtDate").focus(); return false;
            }
            else { $("#divDate").removeClass('has-error'); }
            var Obj = new Object();
            Obj.RenewalID = 0;
            Obj.sRenewalDate = $("#txtDate").val();

            var ObjRegister = new Object();
            ObjRegister.RegisterID = $("#hdnRegisterID").val();
            Obj.Register = ObjRegister;

            var ObjEmployee = new Object();
            ObjEmployee.UserID = $("#ddlEmployee").val();
            Obj.Employee = ObjEmployee;

            Obj.Status = "Closed";
            Obj.AmountPaid = $("#txtAmountPaid").val();

            var sMethodName;
            if ($("#hdnID").val() > 0) {
                Obj.RenewalID = $("#hdnID").val();
                sMethodName = "UpdateRenewal";
            }
            else {
                sMethodName = "AddRenewal";
            }

            SaveandUpdateRenewal(Obj, sMethodName);

            return false;
        });

        $("#btnClose").click(function () {
            $('#compose-modal').modal('hide');
            return false;
        });

        function ClearFields() {
            $("#txtIDNo").val("");
            $("#txtAccountNo").val("");
            $("#txtAmountPaid").val(0);
            $("#txtCustomer").val("");
            $("#txtChit").val("");
            $("#txtRegisteredEmployee").val("");
            $("#ddlEmployee").val(null).change();
            $("#ddlIDProof").val(null).change();
            $("#txtDuration").val(0);
            $("#txtInstallmentAmount").val(0);
            $("#txtTotalAmount").val(0);
            $("#txtBonusAmount").val(0);
            $("#txtGrossAmount").val(0);
            $("#txtAddress").val("");
            $("#txtPhone").val("");
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
                url: "WebServices/VHMSService.svc/GetRenewal",
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

                                        var table = "<tr id='" + obj[index].RenewalID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].RenewalNo + "</td>";
                                        table += "<td>" + obj[index].Register.AccountNo + "</td>";
                                        table += "<td>" + obj[index].sRenewalDate + "</td>";
                                        table += "<td>" + obj[index].Register.Customer.CustomerName + "</td>";

                                        table += "<td>" + obj[index].Register.Chit.ChitName + "</td>";
                                        table += "<td>" + obj[index].REmployee.EmployeeCode + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Register.Chit.Duration + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Register.Chit.InstallmentAmount + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].AmountPaid + "</td>";

                                        if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].RenewalID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionUpdate == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].RenewalID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].RenewalID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else { table += "<td></td>"; }
                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Renewal");
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
                                        if (ActionUpdate == "1") { EditRecord($(this).parent().parent()[0].id); }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Delete").click(function () {
                                        if (ActionDelete == "1") {
                                            if (confirm('Are you sure to delete the selected record ?')) {
                                                $('#hdnDeleteRegisterID').val($(this).parent().parent()[0].id);
                                                $('#deletemodal').modal('show');
                                            }
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
                                    { "sWidth": "0%" },
                                    { "sWidth": "0%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "3%" },
                                    { "sWidth": "3%" },
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
                url: "WebServices/VHMSService.svc/SearchRenewal",
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

                                        var table = "<tr id='" + obj[index].RenewalID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].RenewalNo + "</td>";
                                        table += "<td>" + obj[index].Register.AccountNo + "</td>";
                                        table += "<td>" + obj[index].sRenewalDate + "</td>";
                                        table += "<td>" + obj[index].Register.Customer.CustomerName + "</td>";

                                        table += "<td>" + obj[index].Register.Chit.ChitName + "</td>";
                                        table += "<td>" + obj[index].REmployee.EmployeeCode + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Register.Chit.Duration + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Register.Chit.InstallmentAmount + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].AmountPaid + "</td>";

                                        if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].RenewalID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionUpdate == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].RenewalID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].RenewalID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else { table += "<td></td>"; }
                                        table += "</tr>";
                                        $("#tblSearchResult_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Renewal");
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
                                        if (ActionUpdate == "1") { EditRecord($(this).parent().parent()[0].id); }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Delete").click(function () {
                                        if (ActionDelete == "1") {
                                            if (confirm('Are you sure to delete the selected record ?')) {
                                                $('#hdnDeleteRegisterID').val($(this).parent().parent()[0].id);
                                                $('#deletemodal').modal('show');
                                            }
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
                                    { "sWidth": "0%" },
                                    { "sWidth": "0%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "3%" },
                                    { "sWidth": "3%" },
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

        function SaveandUpdateRenewal(Obj, sMethodName) {
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


                                if (sMethodName == "AddRenewal") {
                                    $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                                    if ($("#SdSMS").val() == "True")
                                        GetSMSRecord(objResponse.Value, "");
                                    ShowReceiptNo(objResponse.Value);
                                }
                                else if (sMethodName == "UpdateRenewal") { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                                ClearFields();
                                GetRecord();

                                $('#compose-modal').modal('hide');
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Renewal_A_01" || objResponse.Value == "Renewal_U_01") {
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
        function GetPassword() {
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
        function ShowReceiptNo(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetRenewalByID",
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
                                    $("#txtReceiptNo").val(obj.RenewalNo);
                                    $('#Renewalmodal').modal({ show: true, backdrop: true });
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Receipt Details");
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

        $("#txtSearchName").change(function () {

            if ($("#txtSearchName").val().trim() != "" || $("#txtSearchName").val().trim() != undefined) {
                var iDetails = $("#txtSearchName").val();
                GetSearchRecord(iDetails);
            }
            return false;
        });

        function GetSMSRecord(id, messageType) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetRenewalByID",
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

                                    var SMSMsg = "Dear " + obj.Register.Customer.CustomerName + ", Your Receipt No : " + obj.RenewalNo + " Scheme. Your Register No.: " + obj.Register.AccountNo + ", Installation Amount : Rs." + obj.AmountPaid + ". Total Amount Paid : Rs." + obj.Register.ChitAmount + "- SVS Jewellers.";
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
                                    var APILink = $("#SMSurl").val();

                                    if (APILink.match(/#message#/))
                                        APILink = APILink.replace("#message#", SMSMsg);
                                    if (APILink.match(/#uname#/))
                                        APILink = APILink.replace("#uname#", $("#SMSusername").val());
                                    if (APILink.match(/#pwd#/))
                                        APILink = APILink.replace("#pwd#", $("#SMSpassword").val());
                                    if (APILink.match(/#mobile#/))
                                        APILink = APILink.replace("#mobile#", obj.Register.Customer.MobileNo);
                                    if (APILink.match(/#sendername#/))
                                        APILink = APILink.replace("#sendername#", $("#SMSsendername").val());

                                    SendSMS(obj.Register.Customer.MobileNo, APILink);
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

        $("#btnOK").click(function () {
            $('#Renewalmodal').modal('hide');
            return false;
        });

        $("#btndelete").click(function () {
            if ($("#txtOtherPassword").val().trim() == "" || $("#txtOtherPassword").val().trim() == undefined || $("#txtOtherPassword").val().trim() != $("#hdRS").val()) {
                $.jGrowl("Please enter Valid Password", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divOtherPassword").addClass('has-error'); $("#txtOtherPassword").focus(); return false;
            } else { $("#divOtherPassword").removeClass('has-error'); }

            DeleteRecord($('#hdnDeleteRegisterID').val());
            $('#txtOtherPassword').val("");
            $('#deletemodal').modal('hide');
            return false;
        });

        $("#btncancel").click(function () {
            $('#txtOtherPassword').val("");
            $('#deletemodal').modal('hide');
            return false;
        });
        function EditRecord(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetRenewalByID",
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

                                    $("#hdnID").val(obj.RenewalID);
                                    $("#txtDate").val(obj.sRenewalDate);
                                    $("#txtAccountNo").val(obj.Register.AccountNo).change();
                                    $("#ddlAmountPaid").val(obj.AmountPaid);
                                    $("#ddlEmployee").val(obj.REmployee.UserID);
                                    $("#txtRenewalNo").val(obj.RenewalNo);
                                    $('#compose-modal').modal({ show: true, backdrop: true });
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit Renewal");
                                    GetRegistereddetails(obj.Register.AccountNo);
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
                url: "WebServices/VHMSService.svc/DeleteRenewal",
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
                            else if (objResponse.Value == "Renewal_R_01" || objResponse.Value == "Renewal_D_01") {
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
        $("#aGeneral").click(function () {

            $("#SearchResult").hide();
            GetRecord();
        });

        $("#aSearchResult").click(function () {

            $("#SearchResult").show();

        });
    </script>
</asp:Content>







