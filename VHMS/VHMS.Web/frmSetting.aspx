<%@ Page Title="Settings" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmSetting.aspx.cs" Inherits="frmSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Settings
            </h1>
        </section>
        <section class="content">
            <div class="nav-tabs-custom" id="tab-modal">
                <ul class="nav nav-tabs">
                    <li class="active"><a id="aGeneral" href="#General" data-toggle="tab">General</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="General">
                        <div class="box box-primary box-solid">
                            <div class="box-header">
                                Calculation Settings
                            </div>
                            <div class="box-body">
                                <div class="col-md-4">
                                    <div class="box box-info box-solid">
                                        <div class="box-header" style="text-align: center;">
                                            <div class="box-title">&nbsp;&nbsp;Maximum Discount %</div>
                                        </div>
                                        <div class="table-responsive">
                                            <div class="form-group col-md-6 col-sm-3" id="divMaxSalesDiscount" style="text-align: center;">
                                                <label>
                                                    Whole sales                                               
                                                </label>
                                                <span class="text-danger">*</span>
                                                <input type="text" class="form-control TRSearch" id="txtMaxSalesDiscount" placeholder="Maximum Sales Discount"
                                                    maxlength="12" tabindex="1" onkeypress="return IsNumeric(event)" />
                                            </div>
                                            <div class="form-group col-md-6 col-sm-3" id="divMaxDiscount" style="text-align: center;">
                                                <label>
                                                    Retail Sales</label><span class="text-danger">*</span>
                                                <input type="text" class="form-control TRSearch" id="txtMaxDiscount" placeholder="Maximum Discount Percent"
                                                    maxlength="12" tabindex="2" onkeypress="return IsNumeric(event)" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="box box-info box-solid">
                                        <div class="box-header" style="text-align: center;">
                                            <div class="box-title">&nbsp;&nbsp;Minimum Margin %</div>
                                        </div>
                                        <div class="table-responsive">

                                            <div class="form-group col-md-6 col-sm-3" id="divWholeSaleMinMargin" style="text-align: center;">
                                                <label>
                                                    Whole Sales
                                                </label>
                                                <span class="text-danger">*</span>
                                                <input type="text" class="form-control TRSearch" id="txtWholeSaleMinMargin" placeholder="Sales Whole Sales Min Margin"
                                                    maxlength="12" tabindex="3" onkeypress="return IsNumeric(event)" />
                                            </div>
                                            <div class="form-group col-md-6 col-sm-3" id="divRetailsMinMargin" style="text-align: center;">
                                                <label>
                                                    Retail Sales
                                                </label>
                                                <span class="text-danger">*</span>
                                                <input type="text" class="form-control TRSearch" id="txtRetailMinMargin" placeholder="Sales Retails Min Margin"
                                                    maxlength="12" tabindex="4" onkeypress="return IsNumeric(event)" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="box box-info box-solid">
                                        <div class="box-header" style="text-align: center;">
                                            <div class="box-title">&nbsp;&nbsp;Other</div>
                                        </div>
                                        <div class="table-responsive">
                                            <div class="form-group col-md-9 col-sm-2" id="divSalesTaxReturn" style="text-align: center;">
                                                <label>
                                                    Sales Tax Return Amount
                                                </label>
                                                <span class="text-danger">*</span>
                                                <input type="text" class="form-control TRSearch" id="txtSalesTaxReturn" placeholder="Sales Tax Return Amount"
                                                    maxlength="12" tabindex="5" onkeypress="return IsNumeric(event)" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box box-primary box-solid">
                            <div class="box-header">
                                Email Settings
                            </div>
                            <div class="box-body">
                                <div class="form-group col-md-2 col-sm-2" id="divHostName">
                                    <label>
                                        Host Name
                                    </label>
                                    <span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtHostName" placeholder="Host Name"
                                        maxlength="150" tabindex="6" />
                                </div>
                                <div class="form-group col-md-2 col-sm-2" id="divPort">
                                    <label>
                                        Port
                                    </label>
                                    <span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtPort" placeholder="Port"
                                        maxlength="50" tabindex="7" />
                                </div>
                                <div class="form-group col-md-2 col-sm-2" id="divEmailID">
                                    <label>
                                        Email ID
                                    </label>
                                    <span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtEmailID" placeholder="EmailID"
                                        maxlength="150" tabindex="8" />
                                </div>
                                <div class="form-group col-md-2 col-sm-2" id="divEmailPassword">
                                    <label>
                                        Email Password
                                    </label>
                                    <span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtEmailPassword" placeholder="Password"
                                        maxlength="50" tabindex="6" />
                                </div>
                                <div class="form-group col-md-2  col-sm-2">
                                    <div class="checkbox" style="margin-top: 30px">
                                        <label>
                                            <input type="checkbox" id="chkEnableSSL" tabindex="10" />Enable SSL
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group col-md-2  col-sm-2">
                                    <div class="checkbox" style="margin-top: 30px">
                                        <label>
                                            <input type="checkbox" id="chkDefaultCredentials" tabindex="11" />Default Credential
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box box-primary box-solid">
                            <div class="box-header">
                                SMS Settings
                            </div>
                            <div class="box-body">
                                <div class="form-group col-md-12" id="divAPILink">
                                    <label>
                                        API Link</label>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-link"></i></div>
                                        <input type="text" class="form-control" id="txtAPILink" placeholder="API Link" maxlength="1000" tabindex="12" />
                                    </div>
                                </div>

                                <div class="form-group col-md-3" id="divUserName">
                                    <label>
                                        User Name</label>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-user"></i></div>
                                        <input type="text" class="form-control" id="txtUserName" placeholder="User Name" maxlength="50" tabindex="13" />
                                    </div>
                                </div>
                                <div class="form-group col-md-3" id="divPassword">
                                    <label>
                                        Password</label>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-openid"></i></div>
                                        <input type="text" class="form-control" id="txtPassword" placeholder="Password" maxlength="50" tabindex="14" />
                                    </div>
                                </div>
                                <div class="form-group col-md-3" id="divSenderName">
                                    <label>
                                        Sender Name</label>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-user"></i></div>
                                        <input type="text" class="form-control" id="txtSenderName" placeholder="Sender Name" maxlength="100" tabindex="15" />
                                    </div>
                                </div>
                                <div class="form-group col-md-3">
                                    <div class="checkbox" style="margin-top: 30px">
                                        <label>
                                            <input type="checkbox" id="chkSMSSend" tabindex="16" />Send SMS
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box box-primary box-solid">
                            <div class="box-header">
                                Purchase Company Details
                            </div>
                            <div class="box-body">
                                <div class="form-group col-md-3" id="divMobileNo">
                                    <label>
                                        Mobile No</label>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-phone"></i></div>
                                        <input type="text" class="form-control" id="txtMobileNo" placeholder="Mobile No" maxlength="50" tabindex="17" />
                                    </div>
                                </div>
                                <div class="form-group col-md-3" id="divEmail">
                                    <label>
                                        Email</label>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-envelope"></i></div>
                                        <input type="text" class="form-control" id="txtEmail" placeholder="Email" maxlength="50" tabindex="18" />
                                    </div>
                                </div>
                                <div class="form-group col-md-5" id="divAddress">
                                    <label>
                                        Address</label>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-address-card"></i></div>
                                        <textarea id="txtAddress" class="form-control" maxlength="250" tabindex="19" rows="4" aria-autocomplete="none"></textarea>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="box box-primary box-solid">
                            <div class="box-header">
                                Account Settings
                            </div>
                            <div class="box-body">
                                <div class="col-md-4">
                                    <div class="box box-info box-solid">
                                        <div class="box-header" style="text-align: center;">
                                            <div class="box-title">&nbsp;&nbsp;Payment</div>
                                        </div>
                                        <div class="table-responsive">
                                            <div class="form-group col-md-6 col-sm-3" id="divPayment">
                                                <label>
                                                    Payment Mode                                              
                                                </label>
                                                <span class="text-danger">*</span>
                                                <select id="ddlPaymentMode" class="form-control" tabindex="20">
                                                    <option value="0" selected="selected">--Select--</option>
                                                    <option value="3">NEFT/RTGS</option>
                                                    <option value="4">IMPS</option>
                                                    <option value="1">Cash</option>
                                                    <option value="2">Cheque</option>
                                                    <option value="5">Others</option>
                                                </select>
                                            </div>
                                            <div class="form-group col-md-6 col-sm-3" id="divSelectAC">
                                                <label>
                                                    Select Ac</label><span class="text-danger">*</span>
                                                <select id="ddlPaymentAC" class="form-control select2" data-placeholder="Select Account" tabindex="21">
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="box box-info box-solid">
                                        <div class="box-header" style="text-align: center;">
                                            <div class="box-title">&nbsp;&nbsp;Receipt</div>
                                        </div>
                                        <div class="table-responsive">
                                            <div class="form-group col-md-6 col-sm-3" id="divReceipt">
                                                <label>
                                                    Receipt Mode                                              
                                                </label>
                                                <span class="text-danger">*</span>
                                                <select id="ddlReceiptMode" class="form-control" tabindex="22">
                                                    <option value="0" selected="selected">--Select--</option>
                                                    <option value="3">NEFT/RTGS</option>
                                                    <option value="5">IMPS</option>
                                                    <option value="1">Cash</option>
                                                    <option value="2">Cheque</option>
                                                    <option value="4">Others</option>
                                                </select>
                                            </div>
                                            <div class="form-group col-md-6 col-sm-3" id="divReceiptSelectAC">
                                                <label>
                                                    Select Ac</label><span class="text-danger">*</span>
                                                <select id="ddlReceiptPaymentAC" class="form-control select2" data-placeholder="Select Account" tabindex="23">
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="box box-info box-solid">
                                        <div class="box-header" style="text-align: center;">
                                            <div class="box-title">&nbsp;&nbsp;Retails Receipt</div>
                                        </div>
                                        <div class="table-responsive">
                                            <div class="form-group col-md-6 col-sm-3" id="divRetailsReceipt">
                                                <label>
                                                    Receipt Mode                                              
                                                </label>
                                                <span class="text-danger">*</span>
                                                <select id="ddlRetailsReceiptMode" class="form-control" tabindex="24">
                                                    <option value="0" selected="selected">--Select--</option>
                                                    <option value="3">NEFT/RTGS</option>
                                                    <option value="5">IMPS</option>
                                                    <option value="1">Cash</option>
                                                    <option value="2">Cheque</option>
                                                    <option value="4">Others</option>
                                                </select>
                                            </div>
                                            <div class="form-group col-md-6 col-sm-3" id="divRetailsReceiptSelectAC">
                                                <label>
                                                    Select Ac</label><span class="text-danger">*</span>
                                                <select id="ddlRetailsReceiptPaymentAC" class="form-control select2" data-placeholder="Select Account" tabindex="25">
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="box box-info box-solid">
                                        <div class="box-header" style="text-align: center;">
                                            <div class="box-title">&nbsp;&nbsp;Expense</div>
                                        </div>
                                        <div class="table-responsive">
                                            <div class="form-group col-md-6 col-sm-3" id="divExpensePayment">
                                                <label>
                                                    Payment Mode                                              
                                                </label>
                                                <span class="text-danger">*</span>
                                                <select id="ddlExpensePaymentMode" class="form-control" tabindex="26">
                                                    <option value="0" selected="selected">--Select--</option>
                                                    <option value="3">NEFT/RTGS</option>
                                                    <option value="4">IMPS</option>
                                                    <option value="1">Cash</option>
                                                    <option value="2">Cheque</option>
                                                    <option value="5">Others</option>
                                                </select>
                                            </div>
                                            <div class="form-group col-md-6 col-sm-3" id="divExpenseSelectAC">
                                                <label>
                                                    Select Ac</label><span class="text-danger">*</span>
                                                <select id="ddlExpensePaymentAC" class="form-control select2" data-placeholder="Select Account" tabindex="27">
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="box box-info box-solid">
                                        <div class="box-header" style="text-align: center;">
                                            <div class="box-title">&nbsp;&nbsp;Other Expense</div>
                                        </div>
                                        <div class="table-responsive">
                                            <div class="form-group col-md-6 col-sm-3" id="divOtherPayment">
                                                <label>
                                                    Payment Mode                                              
                                                </label>
                                                <span class="text-danger">*</span>
                                                <select id="ddlOtherReceiptMode" class="form-control" tabindex="28">
                                                    <option value="0" selected="selected">--Select--</option>
                                                    <option value="1">Cash</option>
                                                    <option value="2">Cheque</option>
                                                    <option value="3">NEFT/RTGS</option>
                                                    <option value="4">Others</option>
                                                </select>
                                            </div>
                                            <div class="form-group col-md-6 col-sm-3" id="divOtherSelectAC">
                                                <label>
                                                    Select Ac</label><span class="text-danger">*</span>
                                                <select id="ddlOtherPaymentAC" class="form-control select2" data-placeholder="Select Account" tabindex="29">
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="box box-primary box-solid">
                            <div class="box-header">
                                Retails Sales Company Details
                            </div>
                            <div class="box-body">
                                <div class="form-group col-md-2" id="divReatilsMobileNo">
                                    <label>
                                        Mobile No</label>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-phone"></i></div>
                                        <input type="text" class="form-control" id="txtReatilsMobileNo" placeholder="Mobile No" maxlength="50" tabindex="30" />
                                    </div>
                                </div>
                                <div class="form-group col-md-3" id="divReatilsEmail">
                                    <label>
                                        Email</label>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-envelope"></i></div>
                                        <input type="text" class="form-control" id="txtReatilsEmail" placeholder="Email" maxlength="50" tabindex="31" />
                                    </div>
                                </div>
                                <div class="form-group col-md-3" id="divReatilsGSTNo">
                                    <label>
                                        GSTIN No</label>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-sort-numeric-asc"></i></div>
                                        <input type="text" class="form-control" id="txtReatilsGSTNo" placeholder="GSTIN No" maxlength="50" tabindex="32" />
                                    </div>
                                </div>
                                <div class="form-group col-md-4" id="divReatilsAddress">
                                    <label>
                                        Address</label>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-address-card"></i></div>
                                        <textarea id="txtReatilsAddress" class="form-control" maxlength="250" tabindex="33" rows="4" aria-autocomplete="none"></textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button id="btnSaveChanges" type="button" class="btn btn-primary margin pull-right" tabindex="34">
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
            GetLedgerBank("ddlPaymentAC");
            GetLedgerBank("ddlReceiptPaymentAC");
            GetLedgerBank("ddlRetailsReceiptPaymentAC");
            GetLedgerBank("ddlExpensePaymentAC");
            GetLedgerBank("ddlOtherPaymentAC");
            GetCompanyInformation();

            pLoadingSetup(true);
        });


        function GetLedgerBank(ddlname) {
            var sControlName = "#" + ddlname;
            dProgress(true);
            $(sControlName).empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetLedgerBank",
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
                                    $(sControlName).append('<option value="' + '0' + '">' + '--Select--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive)
                                            $(sControlName).append("<option value='" + obj[index].LedgerID + "'>" + obj[index].LedgerName + "</option>");
                                    }
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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

            if ($("#txtMaxDiscount").val() == "" || $("#txtMaxDiscount").val() == "0" || $("#txtMaxDiscount").val() == undefined || $("#txtMaxDiscount").val() == null) {
                $.jGrowl("Please enter Maximum Retail Discount %", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divMaxDiscount").addClass('has-error'); $("#txtMaxDiscount").focus(); return false;
            } else { $("#divMaxDiscount").removeClass('has-error'); }

            if ($("#txtMaxSalesDiscount").val() == "" || $("#txtMaxSalesDiscount").val() == "0" || $("#txtMaxSalesDiscount").val() == undefined || $("#txtMaxSalesDiscount").val() == null) {
                $.jGrowl("Please enter Maximum WholeSale Discount %", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divMaxSalesDiscount").addClass('has-error'); $("#txtMaxSalesDiscount").focus(); return false;
            } else { $("#divMaxSalesDiscount").removeClass('has-error'); }

            if ($("#txtWholeSaleMinMargin").val() == "" || $("#txtWholeSaleMinMargin").val() == "0" || $("#txtWholeSaleMinMargin").val() == undefined || $("#txtWholeSaleMinMargin").val() == null) {
                $.jGrowl("Please enter Whole Sales Min Margin", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divWholeSaleMinMargin").addClass('has-error'); $("#txtWholeSaleMinMargin").focus(); return false;
            } else { $("#divWholeSaleMinMargin").removeClass('has-error'); }

            if ($("#txtRetailMinMargin").val() == "" || $("#txtRetailMinMargin").val() == "0" || $("#txtRetailMinMargin").val() == undefined || $("#txtRetailMinMargin").val() == null) {
                $.jGrowl("Please enter Retail Min Margin", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divRetailsMinMargin").addClass('has-error'); $("#txtRetailMinMargin").focus(); return false;
            } else { $("#divRetailsMinMargin").removeClass('has-error'); }

            if ($("#txtHostName").val() == "" || $("#txtHostName").val() == undefined || $("#txtHostName").val() == null) {
                $.jGrowl("Please enter Host Name", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divHostName").addClass('has-error'); $("#txtHostName").focus(); return false;
            } else { $("#divHostName").removeClass('has-error'); }

            if ($("#txtPort").val() == "" || $("#txtPort").val() == undefined || $("#txtPort").val() == null) {
                $.jGrowl("Please enter Port", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divPort").addClass('has-error'); $("#txtPort").focus(); return false;
            } else { $("#divPort").removeClass('has-error'); }

            if ($("#txtEmailID").val() == "" || $("#txtEmailID").val() == undefined || $("#txtEmailID").val() == null) {
                $.jGrowl("Please enter Email ID", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divEmailID").addClass('has-error'); $("#txtEmailID").focus(); return false;
            } else { $("#divEmailID").removeClass('has-error'); }



            if ($("#txtEmailPassword").val() == "" || $("#txtEmailPassword").val() == undefined || $("#txtEmailPassword").val() == null) {
                $.jGrowl("Please enter Email Password", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divEmailPassword").addClass('has-error'); $("#txtEmailPassword").focus(); return false;
            } else { $("#divEmailPassword").removeClass('has-error'); }

            if ($("#chkSMSSend").prop("checked") == true) {

                if ($("#txtAPILink").val().trim() == "" || $("#txtAPILink").val().trim() == undefined) {
                    $.jGrowl("Please enter API Link", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divAPILink").addClass('has-error'); $("#txtAPILink").focus();
                    return false;
                } else { $("#divAPILink").removeClass('has-error'); }
                if ($("#txtUserName").val().trim() == "" || $("#txtUserName").val().trim() == undefined) {
                    $.jGrowl("Please enter User Name", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divUserName").addClass('has-error'); $("#txtUserName").focus();
                    return false;
                } else { $("#divUserName").removeClass('has-error'); }
                if ($("#txtPassword").val().trim() == "" || $("#txtPassword").val().trim() == undefined) {
                    $.jGrowl("Please enter Password", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divPassword").addClass('has-error'); $("#txtPassword").focus();
                    return false;
                } else { $("#divPassword").removeClass('has-error'); }
                if ($("#txtSenderName").val().trim() == "" || $("#txtSenderName").val().trim() == undefined) {
                    $.jGrowl("Please enter Sender Name", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divSenderName").addClass('has-error'); $("#txtSenderName").focus();
                    return false;
                } else { $("#divSenderName").removeClass('has-error'); }
            }

            if ($("#txtMobileNo").val() == "" || $("#txtMobileNo").val() == undefined || $("#txtMobileNo").val() == null) {
                $.jGrowl("Please enter Mobile No", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divMobileNo").addClass('has-error'); $("#txtMobileNo").focus(); return false;
            } else { $("#divMobileNo").removeClass('has-error'); }

            if ($("#txtEmail").val() == "" || $("#txtEmail").val() == undefined || $("#txtEmail").val() == null) {
                $.jGrowl("Please enter Email", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divEmail").addClass('has-error'); $("#txtEmail").focus(); return false;
            } else { $("#divEmail").removeClass('has-error'); }

            if ($("#txtAddress").val() == "" || $("#txtAddress").val() == undefined || $("#txtAddress").val() == null) {
                $.jGrowl("Please enter Address", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divAddress").addClass('has-error'); $("#txtAddress").focus(); return false;
            } else { $("#divAddress").removeClass('has-error'); }

            if ( $("#ddlPaymentMode").val() == undefined || $("#ddlPaymentMode").val() == null) {
                $.jGrowl("Please select Payment Mode", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divPayment").addClass('has-error'); $("#ddlPaymentMode").focus(); return false;
            } else { $("#divPayment").removeClass('has-error'); }

            if ($("#divSelectAC").val() == undefined || $("#divSelectAC").val() == null) {
                $.jGrowl("Please select Payment Account", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divSelectAC").addClass('has-error'); $("#divSelectAC").focus(); return false;
            } else { $("#divSelectAC").removeClass('has-error'); }

            if ($("#ddlReceiptMode").val() == undefined || $("#ddlReceiptMode").val() == null) {
                $.jGrowl("Please select Receipt Mode", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divReceipt").addClass('has-error'); $("#ddlReceiptMode").focus(); return false;
            } else { $("#divReceipt").removeClass('has-error'); }

            if ( $("#divReceiptSelectAC").val() == undefined || $("#divReceiptSelectAC").val() == null) {
                $.jGrowl("Please select Receipt Account", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divReceiptSelectAC").addClass('has-error'); $("#divReceiptSelectAC").focus(); return false;
            } else { $("#divReceiptSelectAC").removeClass('has-error'); }

            if ( $("#ddlExpensePaymentMode").val() == undefined || $("#ddlExpensePaymentMode").val() == null) {
                $.jGrowl("Please select Expense Payment Mode", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divExpensePayment").addClass('has-error'); $("#ddlExpensePaymentMode").focus(); return false;
            } else { $("#divExpensePayment").removeClass('has-error'); }

            if ( $("#ddlExpensePaymentAC").val() == undefined || $("#ddlExpensePaymentAC").val() == null) {
                $.jGrowl("Please select Expense Account", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#ddlExpensePaymentMode").addClass('has-error'); $("#ddlExpensePaymentAC").focus(); return false;
            } else { $("#ddlExpensePaymentMode").removeClass('has-error'); }

            if ( $("#ddlOtherReceiptMode").val() == undefined || $("#ddlOtherReceiptMode").val() == null) {
                $.jGrowl("Please select Other Receipt Mode", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divOtherPayment").addClass('has-error'); $("#ddlOtherReceiptMode").focus(); return false;
            } else { $("#divOtherPayment").removeClass('has-error'); }

            if ($("#ddlOtherPaymentAC").val() == undefined || $("#ddlOtherPaymentAC").val() == null) {
                $.jGrowl("Please select Other Account", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divOtherSelectAC").addClass('has-error'); $("#ddlOtherPaymentAC").focus(); return false;
            } else { $("#divOtherSelectAC").removeClass('has-error'); }

            if ( $("#ddlRetailsReceiptMode").val() == undefined || $("#ddlRetailsReceiptMode").val() == null) {
                $.jGrowl("Please select Retails Receipt Mode", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divRetailsReceipt").addClass('has-error'); $("#ddlRetailsReceiptMode").focus(); return false;
            } else { $("#divRetailsReceipt").removeClass('has-error'); }

            if ( $("#ddlRetailsReceiptPaymentAC").val() == undefined || $("#ddlRetailsReceiptPaymentAC").val() == null) {
                $.jGrowl("Please select Retails Receipt Account", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divRetailsReceiptSelectAC").addClass('has-error'); $("#ddlRetailsReceiptPaymentAC").focus(); return false;
            } else { $("#divRetailsReceiptSelectAC").removeClass('has-error'); }


            if ($("#txtReatilsMobileNo").val() == "" || $("#txtReatilsMobileNo").val() == undefined || $("#txtReatilsMobileNo").val() == null) {
                $.jGrowl("Please enter Retails Sales Company Mobile No", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divReatilsMobileNo").addClass('has-error'); $("#txtReatilsMobileNo").focus(); return false;
            } else { $("#divReatilsMobileNo").removeClass('has-error'); }

            if ($("#txtReatilsEmail").val() == "" || $("#txtReatilsEmail").val() == undefined || $("#txtReatilsEmail").val() == null) {
                $.jGrowl("Please enter Retails Sales Company Email", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divReatilsEmail").addClass('has-error'); $("#txtReatilsEmail").focus(); return false;
            } else { $("#divReatilsEmail").removeClass('has-error'); }

            if ($("#txtReatilsGSTNo").val() == "" || $("#txtReatilsGSTNo").val() == undefined || $("#txtReatilsGSTNo").val() == null) {
                $.jGrowl("Please enter Retails Sales Company GSTIN No", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divReatilsGSTNo").addClass('has-error'); $("#txtReatilsGSTNo").focus(); return false;
            } else { $("#divReatilsGSTNo").removeClass('has-error'); }

            if ($("#txtReatilsAddress").val() == "" || $("#txtReatilsAddress").val() == undefined || $("#txtReatilsAddress").val() == null) {
                $.jGrowl("Please enter Retails Sales Company Address", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divReatilsAddress").addClass('has-error'); $("#txtReatilsAddress").focus(); return false;
            } else { $("#divReatilsAddress").removeClass('has-error'); }

            SaveandUpdateCompanyInformation();

            return false;
        });

        function ClearFields() {
            $("#txtMaxDiscount").val(0);
            $("#txtMaxSalesDiscount").val(0);
            $("#chkSMSSend").prop("checked", false);
            $("#txtPassword").val("");
            $("#txtSenderName").val("");
            $("#txtUserName").val("");
            $("#txtAPILink").val("");
            $("#txtMobileNo").val("");
            $("#txtEmail").val("");
            $("#txtReatilsMobileNo").val("");
            $("#txtReatilsEmail").val("");
            $("#txtReatilsGSTNo").val("");
            $("#txtReatilsAddress").val("");
            $("#txtAddress").val("Cash").change();
            $("#ddlPaymentMode").val("0").change();
            $("#ddlPaymentAC").val("Cash").change();
            $("#ddlRetailsReceiptMode").val("0").change();
            $("#ddlReceiptPaymentAC").val("Cash").change();
            $("#ddlRetailsReceiptMode").val("0").change();
            $("#ddlRetailsReceiptPaymentAC").val("Cash").change();
            $("#ddlExpensePaymentMode").val("0").change();
            $("#ddlExpensePaymentAC").val("Cash").change();
            $("#ddlOtherReceiptMode").val("0").change();
            $("#ddlOtherPaymentAC").val("Cash").change();

            $("#divMaxDiscount").removeClass('has-error');
            $("#divMaxSalesDiscount").removeClass('has-error');
            return false;
        }

        function SaveandUpdateCompanyInformation() {
            var Obj = new Object();
            Obj.MaxDiscountPercent = $("#txtMaxDiscount").val().trim();
            Obj.MaxSalesDiscountPercent = $("#txtMaxSalesDiscount").val().trim();
            Obj.RetailMinMargin = $("#txtRetailMinMargin").val().trim();
            Obj.WholeSaleMinMargin = $("#txtWholeSaleMinMargin").val().trim();
            Obj.SalesTaxAmount = $("#txtSalesTaxReturn").val().trim();
            Obj.SenderName = $("#txtSenderName").val().trim();
            Obj.APILink = $("#txtAPILink").val().trim();
            Obj.SMSUsername = $("#txtUserName").val().trim();
            Obj.SMSPassword = $("#txtPassword").val().trim();
            Obj.SendSMS = $("#chkSMSSend").is(':checked') ? "1" : "0";
            Obj.HostName = $("#txtHostName").val().trim();
            Obj.Port = $("#txtPort").val().trim();
            Obj.UserMailID = $("#txtEmailID").val().trim();
            Obj.UserMailPassword = $("#txtEmailPassword").val().trim();
            Obj.EnableSSL = $("#chkEnableSSL").is(':checked') ? "1" : "0";
            Obj.DefaultCrendentials = $("#chkDefaultCredentials").is(':checked') ? "1" : "0";
            Obj.MobileNo = $("#txtMobileNo").val().trim();
            Obj.ContactEmail = $("#txtEmail").val().trim();
            Obj.Address = $("#txtAddress").val().trim();
            Obj.Retails_Sales_MobileNo = $("#txtReatilsMobileNo").val().trim();
            Obj.Retails_Sales_Email = $("#txtReatilsEmail").val().trim();
            Obj.Retails_Sales_GSTNO = $("#txtReatilsGSTNo").val().trim();
            Obj.Retails_Sales_Address = $("#txtReatilsAddress").val().trim();

            var objPaymentBank = new Object();
            objPaymentBank.LedgerID = $("#ddlPaymentAC").val();
            Obj.PaymentBank = objPaymentBank;

            var objReceiptBank = new Object();
            objReceiptBank.LedgerID = $("#ddlReceiptPaymentAC").val();
            Obj.ReceiptBank = objReceiptBank;

            var objRetailsReceiptBank = new Object();
            objRetailsReceiptBank.LedgerID = $("#ddlRetailsReceiptPaymentAC").val();
            Obj.RetailReceiptBank = objRetailsReceiptBank;

            var objOtherExpenseBank = new Object();
            objOtherExpenseBank.LedgerID = $("#ddlOtherPaymentAC").val();
            Obj.OtherExpenseBank = objOtherExpenseBank;

            var objExpenseBank = new Object();
            objExpenseBank.LedgerID = $("#ddlExpensePaymentAC").val();
            Obj.ExpenseBank = objExpenseBank;

            Obj.ExpensePaymentModeID = $("#ddlExpensePaymentMode").val();
            Obj.OtherReceiptModeID = $("#ddlOtherReceiptMode").val();
            Obj.ReceiptModeID = $("#ddlReceiptMode").val();
            Obj.RetailReceiptModeID = $("#ddlRetailsReceiptMode").val();
            Obj.PaymentModeID = $("#ddlPaymentMode").val();

            sMethodName = "UpdateSettings";

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
                                if (sMethodName == "AddSettings") {
                                    $.jGrowl("Saved Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                                    $("#hdnCompanyID").val(objResponse.Value);
                                }
                                else if (sMethodName == "UpdateSettings") { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

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

                                    $("#txtMaxDiscount").val(obj.MaxDiscountPercent);
                                    $("#txtMaxSalesDiscount").val(obj.MaxSalesDiscount);
                                    $("#txtSalesTaxReturn").val(obj.SalesTaxAmount);
                                    $("#txtUserName").val(obj.SMSUsername);
                                    $("#txtPassword").val(obj.SMSPassword);
                                    $("#txtSenderName").val(obj.SenderName);
                                    $("#txtAPILink").val(obj.APILink);
                                    $("#txtRetailMinMargin").val(obj.RetailMinMargin);
                                    $("#txtWholeSaleMinMargin").val(obj.WholeSaleMinMargin);
                                    $("#chkSMSSend").prop("checked", obj.SendSMS ? true : false);
                                    $("#txtHostName").val(obj.HostName);
                                    $("#txtPort").val(obj.Port);
                                    $("#txtEmailID").val(obj.UserMailID);
                                    $("#txtMobileNo").val(obj.MobileNo);
                                    $("#txtEmail").val(obj.ContactEmail);
                                    $("#txtAddress").val(obj.Address);
                                    $("#txtEmailPassword").val(obj.UserMailPassword);
                                    $("#chkEnableSSL").prop("checked", obj.EnableSSL ? true : false);
                                    $("#chkDefaultCredentials").prop("checked", obj.DefaultCrendentials ? true : false);
                                    $("#ddlPaymentMode").val(obj.PaymentModeID).change();
                                    $("#ddlPaymentAC").val(obj.PaymentBank.LedgerID).change();
                                    $("#ddlReceiptMode").val(obj.ReceiptModeID).change();
                                    $("#ddlReceiptPaymentAC").val(obj.ReceiptBank.LedgerID).change();
                                    $("#ddlExpensePaymentMode").val(obj.ExpensePaymentModeID).change();
                                    $("#ddlExpensePaymentAC").val(obj.ExpenseBank.LedgerID).change();
                                    $("#ddlOtherReceiptMode").val(obj.OtherReceiptModeID).change();
                                    $("#ddlOtherPaymentAC").val(obj.OtherExpenseBank.LedgerID).change();
                                    $("#ddlRetailsReceiptMode").val(obj.RetailReceiptModeID).change();
                                    $("#ddlRetailsReceiptPaymentAC").val(obj.RetailReceiptBank.LedgerID).change();
                                    $("#txtReatilsMobileNo").val(obj.Retails_Sales_MobileNo);
                                    $("#txtReatilsEmail").val(obj.Retails_Sales_Email);
                                    $("#txtReatilsGSTNo").val(obj.Retails_Sales_GSTNO);
                                    $("#txtReatilsAddress").val(obj.Retails_Sales_Address);

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


