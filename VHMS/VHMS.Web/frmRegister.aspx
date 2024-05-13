<%@ Page Title="Register" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmRegister.aspx.cs" Inherits="frmRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header" id="secHeader">
            <div id="divTitle">
                <h3>New Register
                </h3>
            </div>
            <br />

        </section>
        <section class="content">
            <div class="box box-info box-solid" id="divPatient">
                <div class="box-header with-border">
                    <div class="modal-title"></div>
                    <div class="box-tools pull-right">
                        <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="row">
                       
                        <div class="form-group col-md-2" id="divDOB">
                            <label>
                                Date</label><span class="text-danger">*</span>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="2" id="txtDate" />
                            </div>
                        </div>

                        <div class="form-group col-md-2" id="divOPDNo">
                            <label>
                                Search by Moblie No</label><span class="text-danger">*</span>
                            <input type="text" class="form-control" id="txtCustomerCode" placeholder=""
                                maxlength="10" tabindex="3" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-3" id="divCustomer">
                            <label>
                                Customer</label><span class="text-danger">*</span>
                            <input type="text" class="form-control" id="txtName" placeholder="Customer Name"
                                maxlength="500" tabindex="4" />
                        </div>

                        <div class="form-group col-md-2" id="divAddress">
                            <label>
                                Address</label><span class="text-danger">*</span>
                            <textarea id="txtAddress" class="form-control" maxlength="250" tabindex="5" rows="3"></textarea>
                        </div>
                        <div class="form-group col-md-2" id="divPhone">
                            <label>
                                Phone</label><span class="text-danger">*</span>
                            <input type="text" class="form-control" id="txtPhone" placeholder="Phone"
                                maxlength="10" tabindex="6" onkeypress="return isNumberKey(event)" />
                        </div>
                        <div class="form-group col-md-1" id="divDate">
                            <label>DOB</label>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDate"
                                data-link-format="dd/MM/yyyy">

                                <input type="text" class="form-control pull-right" tabindex="-1 " id="txtDOB" readonly="true" />
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="form-group col-md-2" id="divChit">
                            <label>
                                Scheme</label><span class="text-danger">*</span>
                            <select id="ddlChit" class="form-control" tabindex="7">
                                <option selected="selected" value="0">--Select Scheme--</option>
                            </select>
                        </div>
                        <div class="form-group col-md-2" id="divDuration">
                            <label>
                                Duration</label><span class="text-danger">*</span>
                            <input type="text" class="form-control TRSearch" id="txtDuration" placeholder="Duration"
                                maxlength="2" tabindex="-1" onkeypress="return isNumberKey(event)" readonly="true" />
                        </div>
                        <div class="form-group col-md-2" id="divInstallmentAmount">
                            <label>
                                Installment Amount</label><span class="text-danger">*</span>
                            <input type="text" class="form-control TRSearch" id="txtInstallmentAmount" placeholder="Installment Amount"
                                maxlength="12" tabindex="8" onkeypress="return IsNumeric(event)" />
                        </div>
                        <%-- </div>
                    <div class="row">--%>
                        <div class="form-group col-md-2" id="divTotalAmount">
                            <label>
                                Total Amount</label><span class="text-danger">*</span>
                            <input type="text" class="form-control TRSearch" id="txtTotalAmount" placeholder="Total Amount"
                                maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                        </div>
                        <div class="form-group col-md-2" id="divBonusAmount" hidden="hidden">
                            <label>
                                Bonus Amount</label><span class="text-danger">*</span>
                            <input type="text" class="form-control TRSearch" id="txtBonusAmount" placeholder="Bonus Amount"
                                maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                        </div>
                        <div class="form-group col-md-2" id="divGrossAmount" hidden="hidden">
                            <label>
                                Gross Amount</label><span class="text-danger">*</span>
                            <input type="text" class="form-control TRSearch" id="txtGrossAmount" placeholder="Gross Amount"
                                maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                        </div>
                        <div class="form-group col-md-2" id="divIDProof">
                            <label>
                                ID Proof
                            </label>
                            <span class="text-danger">*</span>
                            <select id="ddlIDProof" class="form-control" tabindex="12">
                                <option value="Aadhar">Aadhar</option>
                                <option value="Ration Card">Ration Card</option>
                                <option value="Voter ID">Voter ID</option>
                                <option value="Driving License">Driving License</option>
                                <option value="PAN">PAN</option>
                            </select>
                        </div>
                        <div class="form-group col-md-2" id="divEmployee">
                            <label>
                                Employee Code</label><span class="text-danger">*</span>
                            <select id="ddlEmployee" class="form-control" tabindex="14">
                                <option selected="selected" value="0">--Select Employee--</option>
                            </select>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-3">
                            <label>Live Camera</label>
                            <div id="webcam"></div>
                        </div>
                        <div class="form-group col-md-2">
                            <label>ID Proof Front Page</label>
                            <asp:Image ID="ProofimgCapture" runat="server" Style="visibility: hidden; width: 300px; height: 225px" />
                            <asp:Button ID="btnProofCapture" Text="Capture" runat="server" OnClientClick="return Capture();" /><span id="ProofStatus"></span>
                        </div>
                        <div class="form-group col-md-2">
                            <label>ID Proof Back Page</label>
                            <asp:Image ID="ProofBackimgCapture" runat="server" Style="visibility: hidden; width: 300px; height: 225px" />
                            <asp:Button ID="btnProofBackCapture" Text="Capture" runat="server" OnClientClick="return Capture3();" /><span id="ProofBackStatus"></span>
                        </div>
                        <div class="form-group col-md-2">
                            <label>Application Front Page</label>
                            <asp:Image ID="Register1Capture" runat="server" Style="visibility: hidden; width: 300px; height: 225px" />
                            <asp:Button ID="btnRegister1Capture" Text="Capture" runat="server" OnClientClick="return Capture1();" /><span id="Register1Status"></span>
                        </div>
                        <div class="form-group col-md-3">
                            <label>Application Back Page</label>
                            <asp:Image ID="Register2Capture" runat="server" Style="visibility: hidden; width: 300px; height: 225px" />
                            <asp:Button ID="btnRegister2Capture" Text="Capture" runat="server" OnClientClick="return Capture2();" /><span id="Register2Status"></span>
                        </div>
                    </div>

                    <div class="row">
                        <div class="form-group col-md-3" id="divIDNo" hidden="hidden">
                            <label>
                                ID No</label>
                            <input type="text" class="form-control" id="txtIDNo" placeholder="ID No"
                                maxlength="13" tabindex="13" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer clearfix">
                    <button type="button" class="btn btn-danger pull-left" id="btnClose" tabindex="15">
                        <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                    <button type="submit" class="btn btn-info pull-right" id="btnSave" tabindex="16">
                        <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                    <button type="submit" class="btn btn-info pull-right" id="btnUpdate" tabindex="17">
                        <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
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
                            <div class="form-group  col-md-3">
                                <label>
                                    Account No</label><span class="text-danger">*</span>
                            </div>
                            <div class="form-group  col-md-3" id="divAccount">
                                <input type="text" class="form-control" id="txtAccNo" placeholder=""
                                    maxlength="150" tabindex="-1" readonly="true" />
                            </div>
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
                                &nbsp;&nbsp;
                                OK</button>
                        </div>
                    </div>
                </div>
            </div>

        </section>
    </div>
    <input type="hidden" id="hdnID" />
    <input type="hidden" id="HidImage" />
    <input type="hidden" id="hdnCustomerID" />
    <input type="hidden" id="hdnOpeningDate" />
    <input type="hidden" id="SdSMS" />
    <input type="hidden" id="SMSsendername" />
    <input type="hidden" id="SMSpassword" />
    <input type="hidden" id="SMSurl" />
    <input type="hidden" id="SMSusername" />
    <script src='<%=ResolveUrl("~/Webcam_Plugin/jquery.webcam.js") %>' type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
           <%-- ActionAdd = '<%=Session["ActionAdd"]%>';
            ActionUpdate = '<%=Session["ActionUpdate"]%>';
            ActionDelete = '<%=Session["ActionDelete"]%>';
            ActionView = '<%=Session["ActionView"]%>';--%>

            var ActionAdd = 1;
            var ActionUpdate = 1;
            var ActionDelete = 1;
            var ActionView = 1;

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

            $("#hdnID").val('<%=Session["RegisterID"]%>');
            var isView = '<%=Session["isView"]%>';

            if (isView == "View") {
                $("#btnSave").remove();
                $("#btnUpdate").remove();
            }
            if (ActionAdd != "1") {
                $("#btnSave").remove();
            }

            if (ActionUpdate != "1") {
                $("#btnUpdate").remove();
            }
            //$(".decimal").inputmask("decimal", { digits: 2, radixPoint: "." });
            $("#txtDate").attr("data-link-format", "dd/MM/yyyy");
            $("#divPatient").show();
            $("#txtDate").datetimepicker({
                pickTime: false,
                useCurrent: true,
                maxDate: moment(),
                format: 'DD/MM/YYYY'
            });

            $("#txtDOB").attr("data-link-format", "dd/MM/yyyy");
            $("#txtDOB").datetimepicker({
                pickTime: false,
                useCurrent: true,
                maxDate: moment(),
                format: 'DD/MM/YYYY'
            });
            pLoadingSetup(false);
            pLoadingSetup(true);
            GetEmployee();
            GetChit();
            GetSettings();
            if ($("#hdnID").val() == "")
                AddNewRegister();
            else
                EditRecord($("#hdnID").val())
            SetSessionValue("RegisterID", "");
            SetSessionValue("isView", "");
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
        function AddNewRegister() {
            $("#divPatient").show();
            document.title = "New Register";

            $("#hdnID").val("");
            $("#btnSave").show();
            $("#btnUpdate").hide();
            $("#txtDate").focus();
            $("#txtCustomerCode").focus();
            $(".divTitle").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Register");
            ClearFields();

            //var test = $("#LastOPDNo").val();
            //test = (test.slice(3));
            //$("#LastOPDNo").val(dformat);
            SetSessionValue("LastOPDNo", $("#LastOPDNo").val());
            return false;
        };

        $("#ddlChit").change(function () {
            if ($("#ddlChit").val() > 0)
                GetChitDetails($("#ddlChit").val());
            $("#txtDuration").change();
        });

        $("#txtCustomerCode").blur(function () {
            if ($("#txtCustomerCode").val().length > 0) {
                GetCustomerDetails($("#txtCustomerCode").val());
            }
        });

        $("#txtDuration,#txtInstallmentAmount").change(function () {
            var Dur = 0;
            var Installamt = 0;
            var Bonus = 0;
            var totAmt = 0;
            var Grossamt = 0;

            if ($("#txtDuration").val() > 0)
                Dur = $("#txtDuration").val();

            if ($("#txtInstallmentAmount").val() > 0)
                Installamt = $("#txtInstallmentAmount").val();

            totAmt = parseFloat(Dur) * parseFloat(Installamt);
            $("#txtTotalAmount").val(totAmt.toFixed(2));

        });

        function GetCustomerDetails(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetCustomerByCode",
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
                                    $("#txtName").val(obj.CustomerName);
                                    $("#hdnCustomerID").val(obj.CustomerID);
                                    $("#txtPhone").val(obj.MobileNo);
                                    $("#txtAddress").val(obj.Address);
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

        function GetChitDetails(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetChitByID",
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
                                    $("#txtDuration").val(obj.Duration);
                                    //$("#txtInstallmentAmount").val(obj.InstallmentAmount);
                                    //$("#txtTotalAmount").val(obj.TotalAmount);
                                    //$("#txtBonusAmount").val(obj.BonusAmount);
                                    //$("#txtGrossAmount").val(obj.GrandTotal);
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
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Register");
            $('#compose-modal').modal({ show: true, backdrop: true });
            $("#txtDate").focus();
            $("#txtCustomerCode").focus();
            return false;
        });
        function GetCustomer() {
            $("#ddlCustomer").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetCustomer",
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
                                    //$("#ddlCategory").append('<option value="' + '0' + '">' + '--Select State--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive == "1")
                                            $("#ddlCustomer").append('<option value=' + obj[index].CustomerID + ' >' + obj[index].CustomerName + '-' + obj[index].MobileNo + '</option>');
                                    }
                                }
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlCustomer").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $("#ddlCustomer").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }
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
                                    //$("#ddlCategory").append('<option value="' + '0' + '">' + '--Select State--' + '</option>');
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
        function GetChit() {
            $("#ddlChit").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetChit",
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
                                    //$("#ddlCategory").append('<option value="' + '0' + '">' + '--Select State--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive == "1")
                                            $("#ddlChit").append('<option value=' + obj[index].ChitID + ' >' + obj[index].ChitName + '</option>');
                                    }
                                }
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlChit").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $("#ddlChit").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }

        $("#btnSave,#btnUpdate").click(function () {
            //if (this.id == "btnSave")
            //{ if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
            //else { if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

            if (this.id == "btnSave") {
                if (confirm('Are you sure to Save?')) { }
                else { return false; }
            }
            else if (this.id == "btnUpdate") {
                if (confirm('Are you sure to Update?')) { }
                else { return false; }
            }

            if ($("#txtName").val().trim() == "" || $("#txtName").val().trim() == undefined) {
                $.jGrowl("Please enter Name", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divName").addClass('has-error'); $("#txtName").focus(); return false;
            } else { $("#divName").removeClass('has-error'); }

            if ($("#txtDate").val().trim() == "" || $("#txtDate").val().trim() == undefined) {
                $.jGrowl("Please select Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divDate").addClass('has-error'); $("#txtDate").focus(); return false;
            }
            else { $("#divDate").removeClass('has-error'); }

            var d1 = Date.parse($("#hdnOpeningDate").val());
            var d2 = Date.parse($("#txtDate").val());
            if (d1 < d2) {
                $.jGrowl("Date not updated yet. Contact Administrator", { sticky: false, theme: 'warning', life: jGrowlLife });
                return false;
            }

            if ($("#txtPhone").val().trim() == "" || $("#txtPhone").val().trim() == undefined || $("#txtPhone").val().length != 10) {
                $.jGrowl("Please enter Mobile No", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divPhone").addClass('has-error'); $("#txtPhone").focus(); return false;
            } else { $("#divPhone").removeClass('has-error'); }

            if ($("#txtAddress").val().trim() == "" || $("#txtAddress").val().trim() == undefined) {
                $.jGrowl("Please enter Address", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divAddress").addClass('has-error'); $("#txtAddress").focus(); return false;
            } else { $("#divAddress").removeClass('has-error'); }

            //if ($("#ddlCustomer").val() == "0" || $("#ddlCustomer").val() == undefined) {
            //    $.jGrowl("Please Select Customer", { sticky: false, theme: 'warning', life: jGrowlLife });
            //    $("#divCustomer").addClass('has-error'); $("#ddlCustomer").focus(); return false;
            //} else { $("#divCustomer").removeClass('has-error'); }

            if ($("#txtInstallmentAmount").val() == "" || $("#txtInstallmentAmount").val() == "0" || $("#txtInstallmentAmount").val() == undefined || $("#txtInstallmentAmount").val() == null) {
                $.jGrowl("Please enter Installment Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divInstallmentAmount").addClass('has-error'); $("#txtInstallmentAmount").focus(); return false;
            } else { $("#divInstallmentAmount").removeClass('has-error'); }

            if ($("#ddlChit").val() == "0" || $("#ddlChit").val() == undefined) {
                $.jGrowl("Please Select Scheme", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divChit").addClass('has-error'); $("#ddlChit").focus(); return false;
            } else { $("#divChit").removeClass('has-error'); }

            if ($("#ddlIDProof").val() == "0" || $("#ddlIDProof").val() == undefined || $("#ddlIDProof").val() == "") {
                $.jGrowl("Please Select ID Proof", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divIDProof").addClass('has-error'); $("#ddlIDProof").focus(); return false;
            } else { $("#divIDProof").removeClass('has-error'); }

            if ($("#ddlEmployee").val() == "0" || $("#ddlEmployee").val() == undefined) {
                $.jGrowl("Please Select Employee", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divEmployee").addClass('has-error'); $("#ddlEmployee").focus(); return false;
            } else { $("#divEmployee").removeClass('has-error'); }

            if ($("#txtInstallmentAmount").val() < 1000) {
                $.jGrowl("Please enter Installment Amount Greater than 999", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divInstallmentAmount").addClass('has-error'); $("#txtInstallmentAmount").focus(); return false;
            } else { $("#divInstallmentAmount").removeClass('has-error'); }


            var Obj = new Object();
            Obj.RegisterID = 0;
            Obj.sRegisterDate = $("#txtDate").val();

            var ObjCustomer = new Object();
            ObjCustomer.CustomerID = $("#hdnCustomerID").val();
            Obj.Customer = ObjCustomer;

            var ObjEmployee = new Object();
            ObjEmployee.UserID = $("#ddlEmployee").val();
            Obj.Employee = ObjEmployee;

            //var ObjBranch = new Object();
            //ObjBranch.BranchID = $("#ddlBranch").val();
            //Obj.Branch = ObjBranch;

            var ObjChit = new Object();
            ObjChit.ChitID = $("#ddlChit").val();
            Obj.Chit = ObjChit;

            Obj.IDProof = $("#ddlIDProof").val();
            Obj.IDNo = $("#txtIDNo").val();
            Obj.CustomerName = $("#txtName").val();
            Obj.MobileNo = $("#txtPhone").val();
            Obj.Address = $("#txtAddress").val();
            Obj.TotalAmount = $("#txtTotalAmount").val();
            Obj.InstallmentAmount = $("#txtInstallmentAmount").val();
            Obj.DOB = $("#txtDOB").val();
            Obj.ProofImage = $("[id*=ProofimgCapture]").attr("src");
            Obj.ProofBackImage = $("[id*=ProofBackimgCapture]").attr("src");
            Obj.RegisterImage1 = $("[id*=Register1Capture]").attr("src");
            Obj.RegisterImage2 = $("[id*=Register2Capture]").attr("src");

            var sMethodName;
            if ($("#hdnID").val() > 0) {
                Obj.RegisterID = $("#hdnID").val();
                sMethodName = "UpdateRegister";

            }
            else { sMethodName = "AddRegister"; }

            SaveandUpdateRegister(Obj, sMethodName);

            return false;
        });

        $("#btnClose").click(function () {
            SetSessionValue("RegisterID", "");
            var myWindow = window.open("frmManageRegister.aspx", "_self");
        });

        function ClearFields() {
            $("#txtIDNo").val("");
            $("#hdnCustomerID").val(0);
            $("#txtAccountNo").val("");
            $("#ddlCustomer").val(null).change();
            $("#ddlChit").val(null).change();
            $("#ddlEmployee").val(null).change();
            $("#ddlIDProof").val(null).change();
            $("#txtDuration").val(0);
            $("#txtInstallmentAmount").val(0);
            $("#txtTotalAmount").val(0);
            $("#txtBonusAmount").val(0);
            $("#txtGrossAmount").val(0);
            $("#txtAddress").val("");
            $("#txtPhone").val("");
            $("#txtCustomerCode").val("");
            $("#txtAddress").val("");
            $("#txtName").val("");
            $("#txtPhone").val("");
            $("#divCustomer").removeClass('has-error');
            $("#divIDProof").removeClass('has-error');
            $("#divEmployee").removeClass('has-error');
            $("#divScheme").removeClass('has-error');
            $("#ProofimgCapture").attr("src", "");
            $("#ProofBackimgCapture").attr("src", "");
            $("#Register1Capture").attr("src", "");
            $("#Register2Capture").attr("src", "");
            $("#btnSave").show();
            $("#btnUpdate").hide();
            $("#txtDOB").val();
            var d = new Date().getDate();
            var m = new Date().getMonth() + 1; // JavaScript months are 0-11
            var y = new Date().getFullYear();
            $("#txtDate").val(d + "/" + m + "/" + y);
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
                                ClearFields();

                                if (sMethodName == "AddRegister") {
                                    $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                                    SetSessionValue("RegisterID", objResponse.Value);
                                    if ($("#SdSMS").val() == "True")
                                        GetSMSRecord(objResponse.Value, "");
                                    SetSessionValue("RegisterID", objResponse.Value);
                                    var myWindow = window.open("frmManageRegister.aspx", "_self");
                                    // ShowReceiptNo(objResponse.Value);
                                }
                                else if (sMethodName == "UpdateRegister") {
                                    $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                                    SetSessionValue("RegisterID", "");
                                    var myWindow = window.open("frmManageRegister.aspx", "_self");
                                }

                                var myWindow = window.open("frmManageRegister.aspx", "_self");
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
                                    $("#txtAccountNo").val(obj.AccountNo);
                                    $("#hdnCustomerID").val(obj.Customer.CustomerID);
                                    //$("#ddlCustomer").val(obj.Customer.CustomerID).change();
                                    $("#ddlChit").val(obj.Chit.ChitID).change();
                                    $("#ddlEmployee").val(obj.Employee.UserID).change();
                                    $("#ddlIDProof").val(obj.IDProof).change();
                                    $("#txtIDNo").val(obj.IDNo);
                                    $("#txtName").val(obj.CustomerName);
                                    $("#txtPhone").val(obj.MobileNo);
                                    $("#txtAddress").val(obj.Address);
                                    $("#txtTotalAmount").val(obj.TotalAmount);
                                    $("#txtDOB").val(obj.DOB);
                                    $("#ddlBranch").val(obj.Branch.BranchID).change();
                                    var BranchID = obj.Branch.BranchID;
                                    $("#txtInstallmentAmount").val(obj.InstallmentAmount);
                                    $("[id*=ProofimgCapture]").css("visibility", "visible");
                                    $("[id*=ProofimgCapture]").attr("src", obj.ProofImage);
                                    $("[id*=ProofBackimgCapture]").css("visibility", "visible");
                                    $("[id*=ProofBackimgCapture]").attr("src", obj.ProofBackImage);
                                    $("[id*=Register1Capture]").css("visibility", "visible");
                                    $("[id*=Register1Capture]").attr("src", obj.RegisterImage1);
                                    $("[id*=Register2Capture]").css("visibility", "visible");
                                    $("[id*=Register2Capture]").attr("src", obj.RegisterImage2);
                                    $(".divTitle").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Update Register");
                                    //$('#compose-modal').modal({ show: true, backdrop: true });
                                    //$(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit Register");
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
            // $('#Renewalmodal').modal('hide');
            //return false;
            //SetSessionValue("RegisterID", "");
            var myWindow = window.open("frmManageRegister.aspx", "_self");
            // var myWindow = window.open("frmManageRegister.aspx", "_self");
        });
        function ShowReceiptNo(id) {
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
                                    $("#txtReceiptNo").val(obj.ReceiptNo);
                                    $("#txtAccNo").val(obj.AccountNo);
                                    $('#Renewalmodal').modal({ show: true, backdrop: true });

                                    $(".modal-title").html("<i class='fa fa-info-circle'></i>&nbsp;&nbsp;Register Details");
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

                                    var SMSMsg = "Dear " + obj.CustomerName + ", You are registered in " + obj.Chit.ChitName + " Scheme. Your Account No.: " + obj.AccountNo + ", Receipt No.: " + obj.ReceiptNo + " Paid Amount : Rs." + obj.InstallmentAmount + "- SVS Jewellers.";
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
                                var myWindow = window.open("frmManagePatient.aspx", "_self");
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
    </script>

    <script type="text/javascript">
        var pageUrl = '<%=ResolveUrl("~/frmRegister.aspx") %>';
        $(function () {
            jQuery("#webcam").webcam({
                width: 320,
                height: 240,
                mode: "save",
                swffile: '<%=ResolveUrl("~/Webcam_Plugin/jscam.swf") %>',
                debug: function (type, status) {
                    //$('#camStatus').append(type + ": " + status + '<br /><br />');
                },
                onSave: function (data) {
                    if ($("#HidImage").val() == "ProofImage") {
                        $.ajax({
                            type: "POST",
                            url: pageUrl + "/GetProofCapturedImage",
                            data: '',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (r) {
                                $("[id*=ProofimgCapture]").css("visibility", "visible");
                                $("[id*=ProofimgCapture]").attr("src", r.d);
                                //$("#hdnWPhoto").val(r.d)
                            },
                            failure: function (response) {
                                alert(response.d);
                            }
                        });
                    }
                    else if ($("#HidImage").val() == "ProofBackImage") {
                        $.ajax({
                            type: "POST",
                            url: pageUrl + "/GetProofBackCapturedImage",
                            data: '',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (r) {
                                $("[id*=ProofBackimgCapture]").css("visibility", "visible");
                                $("[id*=ProofBackimgCapture]").attr("src", r.d);
                                //$("#hdnWPhoto").val(r.d)
                            },
                            failure: function (response) {
                                alert(response.d);
                            }
                        });
                    }
                    else if ($("#HidImage").val() == "Register1Image") {
                        $.ajax({
                            type: "POST",
                            url: pageUrl + "/GetRegister1CapturedImage",
                            data: '',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (r) {
                                $("[id*=Register1Capture]").css("visibility", "visible");
                                $("[id*=Register1Capture]").attr("src", r.d);
                                //$("#hdnWPhoto").val(r.d)
                            },
                            failure: function (response) {
                                alert(response.d);
                            }
                        });
                    }
                    else {
                        $.ajax({
                            type: "POST",
                            url: pageUrl + "/GetRegister2CapturedImage",
                            data: '',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (r) {
                                $("[id*=Register2Capture]").css("visibility", "visible");
                                $("[id*=Register2Capture]").attr("src", r.d);
                                //$("#hdnHImage").val(r.d)
                            },
                            failure: function (response) {
                                alert(response.d);
                            }
                        });
                    }
                },
                onCapture: function () {
                    webcam.save(pageUrl);
                }
            });
        });
        function Capture() {
            $("#HidImage").val("ProofImage");
            webcam.capture();
            return false;
        }
        function Capture3() {
            $("#HidImage").val("ProofBackImage");
            webcam.capture();
            return false;
        }
        function Capture1() {
            $("#HidImage").val("Register1Image");
            webcam.capture();

            return false;
        }
        function Capture2() {
            $("#HidImage").val("Register2Image");
            webcam.capture();

            return false;
        }
        function uploadStarted() {

        }
    </script>

</asp:Content>





