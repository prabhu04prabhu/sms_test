<%@ Page Title="Retail Customer" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmRetailCustomer.aspx.cs" Inherits="frmRetailCustomer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Retail Customer
            </h1>
            <div class="form-group  col-md-4" style="margin-left: 255px; margin-top: -34px;">
                <label>
                    Customer Name</label>
                <select id="ddlCategoryName" class="form-control select2" data-placeholder="Select Category Name" tabindex="-1"></select>
            </div>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Master</a></li>
                <li class="active">Retail Customer</li>
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
                                                <th>Customer
                                                </th>
                                                <th class="hidden-xs">CustomerType
                                                </th>
                                                <th class="hidden-xs">City
                                                </th>
                                                <th class="hidden-xs">Area
                                                </th>
                                                <th class="hidden-xs">Address
                                                </th>
                                                <th class="hidden-xs">MobileNo
                                                </th>
                                                <th class="hidden-xs">AlternateNo
                                                </th>
                                                <th class="hidden-xs">Status
                                                </th>
                                                <th></th>
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
                                                    <th>Customer
                                                    </th>
                                                    <th class="hidden-xs">CustomerType
                                                    </th>
                                                    <th class="hidden-xs">City
                                                    </th>
                                                    <th class="hidden-xs">Area
                                                    </th>
                                                    <th class="hidden-xs">Address
                                                    </th>
                                                    <th class="hidden-xs">MobileNo
                                                    </th>
                                                    <th class="hidden-xs">AlternateNo
                                                    </th>
                                                    <th class="hidden-xs">Status
                                                    </th>
                                                    <th></th>
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
            <div class="modal fade" id="compose-modal" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body col-md-12">
                            <div class="form-group col-md-8" id="divName">
                                <label>
                                    Customer</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtName" style="text-transform: uppercase" placeholder="Please enter Customer Name"
                                    maxlength="150" tabindex="1" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-4" id="divMobileNo">
                                <label>
                                    Mobile No</label>
                                <input type="text" class="form-control" id="txtMobileNo" placeholder="Please enter MobileNo"
                                    maxlength="10" tabindex="2" onkeypress="return isNumberKey(event)" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-4">
                                <label>
                                    Alternate No</label>
                                <input type="text" class="form-control" id="txtAlternateNo" placeholder="Please enter AlternateNo"
                                    maxlength="12" tabindex="3" onkeypress="return isNumberKey(event)" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-4">
                                <label>
                                    Customer Type</label><span class="text-danger">*</span>
                                <select id="ddlCustomerType" class="form-control select2" data-placeholder="Select Customer Type" tabindex="4">
                                </select>
                            </div>
                            <div class="form-group col-md-4" id="divDate">
                                <label>DOB</label>
                                <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDate"
                                    data-link-format="dd/MM/yyyy">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                    </div>
                                    <input type="text" class="form-control pull-right" tabindex="5 " id="txtDate" readonly="true" />
                                </div>
                            </div>
                            <div class="form-group col-md-3" style="display: none">
                                <label>
                                    Code</label>
                                <input type="text" class="form-control" id="txtCode" placeholder="Please enter Code"
                                    maxlength="50" tabindex="500" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-12">
                                <label>
                                    Address</label>
                                <textarea id="txtAddress" class="form-control" maxlength="250" tabindex="6" rows="3" aria-autocomplete="none"></textarea>
                            </div>
                            <%--   <div class="form-group col-md-6">
                                <label>
                                    Shipping  Address</label>
                                <textarea id="txtShippinAddress" class="form-control" maxlength="250" tabindex="6" rows="3"></textarea>
                            </div>--%>
                            <div class="form-group col-md-5">
                                <label>
                                    Email</label>
                                <input type="text" class="form-control" id="txtEmail" placeholder="Please enter Email"
                                    maxlength="50" tabindex="7" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-5">
                                <label>
                                    GSTNo</label>
                                <input type="text" class="form-control" id="txtGSTNo" placeholder="Please enter GSTNo"
                                    maxlength="15" tabindex="8" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-2" id="divDays">
                                <label>
                                    Due Days</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtDays" placeholder="Please enter Days"
                                    maxlength="12" tabindex="14" onkeypress="return isNumberKey(event)" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-4" id="divState">
                                <label>
                                    State</label><span class="text-danger">*</span>
                                <select id="ddlState" class="form-control select2" data-placeholder="Select State" tabindex="9">
                                </select>
                            </div>
                            <div class="form-group col-md-4" id="divCity">
                                <label>
                                    City</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtCity" placeholder="Please enter City"
                                    maxlength="12" tabindex="10" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-4" id="divArea">
                                <label>
                                    Area</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtArea" placeholder="Please enter Area"
                                    maxlength="120" tabindex="11" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-2">
                                <label>
                                    Pincode</label>
                                <input type="text" class="form-control" id="txtPincode" placeholder="Pincode"
                                    maxlength="6" tabindex="12" onkeypress="return isNumberKey(event)" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-2" id="divDiscountPercent">
                                <label>
                                    Discount%</label>
                                <input type="text" class="form-control" id="txtDiscountPercent" placeholder="Please enter Default DiscountPercent"
                                    maxlength="12" tabindex="13" onkeypress="return isNumberKey(event)" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-4" id="divLimitSales">
                                <label>
                                    Limit Sales Amount</label>
                                <input type="text" class="form-control" id="txtlimitsalesAmount" placeholder="Please enter limitSalesAmount"
                                    maxlength="12" tabindex="14" onkeypress="return isNumberKey(event)" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-4" id="divAddress1" style="display: none;">
                                <label>
                                    Comments</label>
                                <textarea id="txtComments" class="form-control" maxlength="255" tabindex="-1" rows="3" aria-autocomplete="none"></textarea>
                            </div>

                            <div class="form-group col-md-2">
                                <label>
                                    <input type="checkbox" id="chkStatus" checked="checked" tabindex="15" />&nbsp &nbsp Active
                                </label>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnSave" tabindex="16">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="submit" class="btn btn-info pull-left" id="btnUpdate" tabindex="17">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="18">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnID" />

    <script type="text/javascript">
        $(document).ready(function () {
            ActionAdd = '<%=Session["ActionAdd"]%>';
            ActionUpdate = '<%=Session["ActionUpdate"]%>';
            ActionDelete = '<%=Session["ActionDelete"]%>';
            ActionView = '<%=Session["ActionView"]%>';

            $('input,select').keydown(function (event) { //event==Keyevent
                if (event.which == 13) {
                    var inputs = $(this).closest('form').find(':input:visible:not(disabled):not([readonly])');
                    inputs.eq(inputs.index(this) + 1).focus();
                    event.preventDefault(); //Disable standard Enterkey action

                }
            });

            if (ActionAdd != "1") {
                $("#btnAddNew").remove();
                $("#btnSave").remove();
            }

            $("#txtDate").attr("data-link-format", "dd/MM/yyyy");

            $("#txtDate").datetimepicker({
                pickTime: false,
                useCurrent: true,
                maxDate: moment(),
                format: 'DD/MM/YYYY'
            });
            if (ActionUpdate != "1") {
                $("#btnUpdate").remove();
            }

            pLoadingSetup(false);
            pLoadingSetup(true);
            GetCustomerType();
            GetStateList();
            GetCustomerList("ddlCategoryName");
            GetRecord();
        });

        $("#btnAddNew").click(function () {
            ClearFields();
            $("#hdnID").val("");
            $("#btnSave").show();
            $("#btnUpdate").hide();
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Customer");
            $('#compose-modal').modal({ show: true, backdrop: true });
            $("#txtName").focus();
            $("#txtComments").val("");
            return false;
        });
        function IsEmail(email) {
            var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!regex.test(email)) {
                return false;
            } else {
                return true;
            }
        }


        $("#ddlCategoryName").change(function () {
            GetRecord();
        });
        function GetCustomerList(ddlname) {
            var sControlName = "#" + ddlname;
            dProgress(true);
            $(sControlName).empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetTopCustomerType",
                data: JSON.stringify({ Type: 'Retail' }),
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

                                    $(sControlName).append('<option value="' + '0' + '">' + '--All--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        $(sControlName).append('<option value=' + obj[index].CustomerID + ' >' + obj[index].CustomerName + '</option>');
                                    }
                                }
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }

        function GetStateList() {
            $("#ddlState").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetState",
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
                                    //$("#ddlState").append('<option value="' + '0' + '">' + '--Select State--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        $("#ddlState").append('<option value=' + obj[index].StateID + ' >' + obj[index].StateName + '</option>');
                                    }
                                }
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlState").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $("#ddlState").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }

        function GetCustomerType() {
            $("#ddlCustomerType").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetCustomertypeName",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ TypeNames: "Retail" }),
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    //$("#ddlState").append('<option value="' + '0' + '">' + '--Select State--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        $("#ddlCustomerType").append('<option value=' + obj[index].CustomertypeID + ' >' + obj[index].CustomerTypeName + '</option>');
                                    }
                                }
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlCustomerType").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $("#ddlCustomerType").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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

            if ($("#txtName").val().trim() == "" || $("#txtName").val().trim() == undefined) {
                $.jGrowl("Please enter Customer", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divName").addClass('has-error'); $("#txtName").focus(); return false;
            } else { $("#divName").removeClass('has-error'); }

            //if ($("#txtMobileNo").val().trim() == "" || $("#txtMobileNo").val().trim() == undefined || $("#txtMobileNo").val().length != 10) {
            //    $.jGrowl("Please enter valid Mobile No", { sticky: false, theme: 'warning', life: jGrowlLife });
            //    $("#divMobileNo").addClass('has-error'); $("#txtMobileNo").focus(); return false;
            //} else { $("#divMobileNo").removeClass('has-error'); }

            if ($("#txtEmail").val().trim() != "" && $("#txtEmail").val().trim() != undefined) {
                if (IsEmail($("#txtEmail").val()) == false) {
                    $.jGrowl("Please enter Valid Email", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#txtEmail").focus(); return false;
                }
            }

            if ($("#ddlCustomerType").val() == "0" || $("#ddlCustomerType").val() == undefined) {
                $.jGrowl("Please select Customer type", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divCustomerType").addClass('has-error'); $("#ddlCustomerType").focus(); return false;
            } else { $("#divCustomerType").removeClass('has-error'); }

            if ($("#ddlState").val() == "0" || $("#ddlState").val() == undefined) {
                $.jGrowl("Please Select State", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divState").addClass('has-error'); $("#ddlState").focus(); return false;
            } else { $("#divState").removeClass('has-error'); }

            if ($("#txtCity").val().trim() == "" || $("#txtCity").val().trim() == undefined) {
                $.jGrowl("Please enter City", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divCity").addClass('has-error'); $("#txtCity").focus(); return false;
            } else { $("#divCity").removeClass('has-error'); }

            if ($("#txtArea").val().trim() == "" || $("#txtArea").val().trim() == undefined) {
                $.jGrowl("Please enter Area", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divArea").addClass('has-error'); $("#txtArea").focus(); return false;
            } else { $("#divArea").removeClass('has-error'); }

            if ($("#txtDiscountPercent").val().trim() == "" || $("#txtDiscountPercent").val().trim() == undefined) {
                $.jGrowl("Please enter Default DiscountPercent", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divDiscountPercent").addClass('has-error'); $("#txtDiscountPercent").focus(); return false;
            } else { $("#divDiscountPercent").removeClass('has-error'); }

            //if ($("#txtlimitsalesAmount").val().trim() == "" || $("#txtlimitsalesAmount").val().trim() == undefined) {
            //    $.jGrowl("Please enter Limit Sales Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
            //    $("#divLimitSales").addClass('has-error'); $("#txtlimitsalesAmount").focus(); return false;
            //} else { $("#divLimitSales").removeClass('has-error'); }

            var Obj = new Object();
            Obj.CustomerID = 0;
            Obj.CustomerName = $("#txtName").val().trim().toUpperCase();
            Obj.CustomerCode = $("#txtCode").val();
            Obj.CustomerType = $("#ddlCustomerType option:selected").text();
            Obj.Address = $("#txtAddress").val();
            Obj.AlternateNo = $("#txtAlternateNo").val();
            Obj.Email = $("#txtEmail").val();
            Obj.DOB = $("#txtDate").val();
            Obj.MobileNo = $("#txtMobileNo").val();
            Obj.WhatsAppNo = "";
            Obj.GSTNo = $("#txtGSTNo").val();
            Obj.Comments = $("#txtComments").val().trim();
            //Obj.Days = $("#txtDays").val();
            if ($("#txtDays").val().length > 0)
                Obj.Days = $("#txtDays").val();
            else
                Obj.Days = 0;
            Obj.IsActive = $("#chkStatus").is(':checked') ? "1" : "0";
            Obj.Pincode = $("#txtPincode").val();
            //Obj.Days = 0;
            Obj.City = $("#txtCity").val();
            Obj.Area = $("#txtArea").val();
            Obj.Default_DiscountPercent = $("#txtDiscountPercent").val();
            var Limit_SalesAmount = parseFloat($("#txtDisPer").val());
            if (isNaN(Limit_SalesAmount)) Limit_SalesAmount = 0;
            Obj.Limit_SalesAmount = Limit_SalesAmount;
            Obj.MinDueDays = 0;
            Obj.MaxDueDays = 0;
            var ObjState = new Object();
            ObjState.StateID = $("#ddlState").val();
            Obj.State = ObjState;
            Obj.ShippingAddress = [];
            var ObjCustomerType = new Object();
            ObjCustomerType.CustomertypeID = $("#ddlCustomerType").val();
            Obj.CustomerTypes = ObjCustomerType;

            var ObjTransport = new Object();
            ObjTransport.TransportID = 0;
            Obj.Transport = ObjTransport;

            var sMethodName;
            if ($("#hdnID").val() > 0) {
                Obj.CustomerID = $("#hdnID").val();
                sMethodName = "UpdateCustomer";
            }
            else { sMethodName = "AddCustomer"; }

            SaveandUpdateCustomer(Obj, sMethodName);

            return false;
        });

        $("#btnClose").click(function () {
            $('#compose-modal').modal('hide');
            return false;
        });

        function ClearFields() {
            $("#txtName").val("");
            $("#txtCode").val("");
            $("#txtMobileNo").val("");
            $("#ddlCustomerType").val(null).change();
            $("#txtAddress").val("");
            $("#txtAlternateNo").val("");
            $("#txtEmail").val("");
            $("#txtGSTNo").val("");
            $("#txtDays").val("");
            $("#txtDate").val("");
            $("#chkStatus").prop("checked", true);
            $("#ddlState").val(null).change();
            $("#txtCity").val("");
            $("#txtArea").val("");
            $("#txtPincode").val("");
            $("#txtDiscountPercent").val("0");
            $("#txtlimitsalesAmount").val("0");
            $("#divName").removeClass('has-error');
            return false;
        }

        function GetRecord() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetTopCustomer",
                data: JSON.stringify({ CustomerID: $("#ddlCategoryName").val(), Type: 'Retail' }),
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
                                        if (obj[index].IsActive == "1") { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                        else { TypeStatus = "<span class='label label-warning'>Inactive</span>"; }

                                        var table = "<tr id='" + obj[index].CustomerID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].CustomerName + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].CustomerType + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].City + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Area + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Address + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].MobileNo + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].AlternateNo + "</td>";
                                        table += "<td class='hidden-xs'>" + TypeStatus + "</td>";

                                        if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].CustomerID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionUpdate == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].CustomerID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].CustomerID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        table += "<td style='text-align:center; color: darkviolet;'><a href='#' id=hid" + obj[index].CustomerID + " class='Address' style='color:black;' Accountno='" + obj[index].CustomerID + "' title='click here to Customer Ledger'><i class='fa fa-address-card' style='color: darkviolet;'></i></a></td>";

                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Customer");
                                            $("#btnSave").hide();
                                            $("#btnUpdate").hide();
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Edit").click(function () {
                                        if (ActionUpdate == "1") { EditRecord($(this).parent().parent()[0].id); }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Address").click(function () {
                                        SetSessionValue("SalesID", $(this).attr('Accountno'));
                                        SetSessionValue("Table", "Customer");
                                        var myWindow = window.open("frmAddressLabel.aspx", "MsgWindow");
                                    });
                                    $(".Delete").click(function () {
                                        if (ActionDelete == "1") {
                                            if (confirm('Are you sure to delete the selected record ?')) { DeleteRecord($(this).parent().parent()[0].id); }
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
                                    { "sWidth": "31%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "20%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "5%" },
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

        function SaveandUpdateCustomer(Obj, sMethodName) {
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
                                GetRecord();

                                if (sMethodName == "AddCustomer") { $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                                else if (sMethodName == "UpdateCustomer") { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                                $('#compose-modal').modal('hide');
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Customer_A_01" || objResponse.Value == "Customer_U_01") {
                                $.jGrowl("Mobile No. already Exist", { sticky: false, theme: 'danger', life: jGrowlLife });
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

        function EditRecord(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetCustomerByID",
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

                                    $("#hdnID").val(obj.CustomerID);
                                    $("#txtName").val(obj.CustomerName);
                                    $("#txtCode").val(obj.CustomerCode);
                                    $("#txtAddress").val(obj.Address);
                                    $("#txtMobileNo").val(obj.MobileNo);
                                    $("#txtAlternateNo").val(obj.AlternateNo);
                                    $("#txtEmail").val(obj.Email);
                                    $("#txtGSTNo").val(obj.GSTNo);
                                    $("#txtDays").val(obj.Days);
                                    $("#txtDate").val(obj.DOB);
                                    $("#ddlCustomerType").val(obj.CustomerType).change();
                                    $("#chkStatus").prop("checked", obj.IsActive ? true : false);
                                    $("#txtCity").val(obj.City);
                                    $("#txtArea").val(obj.Area);
                                    $("#txtPincode").val(obj.Pincode);
                                    $("#txtShippinAddress").val(obj.Shipping_Address);
                                    $("#txtDiscountPercent").val(obj.Default_DiscountPercent);
                                    $("#txtlimitsalesAmount").val(obj.Limit_SalesAmount);
                                    $("#txtComments").val(obj.Comments);
                                    $("#ddlState").val(obj.State.StateID).change();
                                    $("#ddlCustomerType").val(obj.CustomerTypes.CustomertypeID).change();

                                    $('#compose-modal').modal({ show: true, backdrop: true });
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit Customer");
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


        $("#txtSearchName").change(function () {

            if ($("#txtSearchName").val().trim() != "" || $("#txtSearchName").val().trim() != undefined) {
                var iDetails = $("#txtSearchName").val();
                GetSearchRecord(iDetails);
            }
            return false;
        });

        function DeleteRecord(id) {
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/DeleteCustomer",
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
                            else if (objResponse.Value == "Customer_R_01" || objResponse.Value == "Customer_D_01") {
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

        function GetSearchRecord(iDetails) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/SearchCustomer",
                data: JSON.stringify({ ID: iDetails, Type: 'Retail' }),
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
                                        if (obj[index].IsActive == "1") { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                        else { TypeStatus = "<span class='label label-warning'>Inactive</span>"; }

                                        var table = "<tr id='" + obj[index].CustomerID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].CustomerName + "</td>";
                                        //table += "<td class='hidden-xs'>" + obj[index].CustomerCode + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].CustomerType + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].City + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Area + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Address + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].MobileNo + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].AlternateNo + "</td>";
                                        table += "<td class='hidden-xs'>" + TypeStatus + "</td>";

                                        if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].CustomerID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionUpdate == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].CustomerID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].CustomerID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        table += "<td style='text-align:center; color: darkviolet;'><a href='#' id=hid" + obj[index].CustomerID + " class='Address' style='color:black;' Accountno='" + obj[index].CustomerID + "' title='click here to Customer Ledger'><i class='fa fa-address-card' style='color: darkviolet;'></i></a></td>";

                                        table += "</tr>";
                                        $("#tblSearchResult_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Customer");
                                            $("#btnSave").hide();
                                            $("#btnUpdate").hide();
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Edit").click(function () {
                                        if (ActionUpdate == "1") { EditRecord($(this).parent().parent()[0].id); }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Address").click(function () {
                                        SetSessionValue("SalesID", $(this).attr('Accountno'));
                                        SetSessionValue("Table", "Customer");
                                        var myWindow = window.open("frmAddressLabel.aspx", "MsgWindow");
                                    });
                                    $(".Delete").click(function () {
                                        if (ActionDelete == "1") {
                                            if (confirm('Are you sure to delete the selected record ?')) { DeleteRecord($(this).parent().parent()[0].id); }
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
                                    { "sWidth": "31%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "20%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "5%" },
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

        $("#aGeneral").click(function () {
            $("#SearchResult").hide();
            GetRecord();
        });

        $("#aSearchResult").click(function () {
            $("#SearchResult").show();
        });
    </script>
</asp:Content>



