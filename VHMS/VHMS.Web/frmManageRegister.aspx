<%@ Page Title="Register" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmManageRegister.aspx.cs" Inherits="frmManageRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Register
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Scheme</a></li>
                <li class="active">Register</li>
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
                                                 <th>Receipt No
                                                </th>
                                                <th>Customer
                                                </th>
                                                <th>Moblie No
                                                </th>
                                                <th class="hidden-xs">Scheme
                                                </th>
                                                <th class="hidden-xs">Employee Code
                                                </th>
                                                <th class="hidden-xs">Duration
                                                </th>
                                                <th class="hidden-xs">Installment Amount
                                                </th>
                                                <th class="hidden-xs">Total Amount
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
                                                <th>Account No
                                                </th>
                                                 <th>Receipt No
                                                </th>
                                                <th>Customer
                                                </th>
                                                <th>Moblie No
                                                </th>
                                                <th class="hidden-xs">Scheme
                                                </th>
                                                <th class="hidden-xs">Employee Code
                                                </th>
                                                <th class="hidden-xs">Duration
                                                </th>
                                                <th class="hidden-xs">Installment Amount
                                                </th>
                                                <th class="hidden-xs">Total Amount
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
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group col-md-8" id="divName">
                                <label>
                                    Customer</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtName" placeholder="Please enter Customer Name"
                                    maxlength="150" tabindex="1" />
                            </div>
                            <div class="form-group col-md-4">
                                <label>
                                    Code</label>
                                <input type="text" class="form-control" id="txtCode" placeholder="Please enter Code"
                                    maxlength="50" tabindex="2" />
                            </div>
                            <div class="form-group col-md-8">
                                <label>
                                    Address</label>
                                <textarea id="txtAddress" class="form-control" maxlength="250" tabindex="3" rows="3"></textarea>
                            </div>
                            <div class="form-group col-md-4" id="divDate">
                                <label>DOB</label>
                                <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDate"
                                    data-link-format="dd/MM/yyyy">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                    </div>
                                    <input type="text" class="form-control pull-right" tabindex="-1 " id="txtDate" readonly="true" />
                                </div>
                            </div>

                            <%--<div class="form-group col-md-12"">
                                <label>
                                    Customer Type</label>
                                <input type="text" class="form-control" id="txtCustomerType" placeholder="Please enter Customer Type"
                                    maxlength="50" tabindex="4" />
                            </div>--%>

                            <div class="form-group col-md-6" id="divMobileNo">
                                <label>
                                    MobileNo</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtMobileNo" placeholder="Please enter MobileNo"
                                    maxlength="10" tabindex="5" onkeypress="return isNumberKey(event)" />
                            </div>
                            <div class="form-group col-md-6">
                                <label>
                                    AlternateNo</label>
                                <input type="text" class="form-control" id="txtAlternateNo" placeholder="Please enter AlternateNo"
                                    maxlength="12" tabindex="6" onkeypress="return isNumberKey(event)" />
                            </div>
                            <div class="form-group col-md-6">
                                <label>
                                    Email</label>
                                <input type="text" class="form-control" id="txtEmail" placeholder="Please enter Email"
                                    maxlength="50" tabindex="7" />
                            </div>
                            <div class="form-group col-md-6">
                                <label>
                                    GSTNo</label>
                                <input type="text" class="form-control" id="txtGSTNo" placeholder="Please enter GSTNo"
                                    maxlength="50" tabindex="8" />
                            </div>
                            <div class="form-group col-md-12">
                                <label>
                                    <input type="checkbox" id="chkStatus" checked="checked" tabindex="10" />&nbsp &nbsp Active
                                </label>
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
                                    maxlength="150" tabindex="39"  autocomplete="off"/>
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
    <input type="hidden" id="hdnID" />
    <input type="hidden" id="HidImage" />
    <input type="hidden" id="hdRS" />
    <input type="hidden" id="hdnCustomerID" />
    <script src='<%=ResolveUrl("~/Webcam_Plugin/jquery.webcam.js") %>' type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            ActionAdd = '<%=Session["ActionAdd"]%>';
            ActionUpdate = '<%=Session["ActionUpdate"]%>';
            ActionDelete = '<%=Session["ActionDelete"]%>';
            ActionView = '<%=Session["ActionView"]%>';
            var iRegisterID = '<%=Session["RegisterID"]%>';
            if (ActionAdd != "1") {
                $("#btnAddNew").remove();
            }
            if (iRegisterID > 0) {
                ShowReceiptNo(iRegisterID);
            }

            pLoadingSetup(false);
            pLoadingSetup(true);
            $("#hdnID").val('<%=Session["RegisterID"]%>');
            SetSessionValue("RegisterID", "");
            if ($("#hdnID").val() != "")
                ShowReceiptNo($("#hdnID").val());
             GetPassword();
            GetRecord();
        });

        $("#btnAddNew").click(function () {
            SetSessionValue("RegisterID", "");
            var myWindow = window.open("frmRegister.aspx", "_self");
            return false;
        });

        function GetRecord() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetRegister",
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
                                        table += "<td>" + obj[index].ReceiptNo + "</td>";
                                        table += "<td>" + obj[index].CustomerName + "</td>";
                                        table += "<td>" + obj[index].MobileNo + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Chit.ChitName + "</td>";

                                        table += "<td class='hidden-xs'>" + obj[index].Employee.EmployeeCode + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Chit.Duration + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].InstallmentAmount + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].TotalAmount + "</td>";

                                        if (ActionView == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].RegisterID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionUpdate == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].RegisterID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }

                                        if (ActionDelete == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].RegisterID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }
                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            ViewRecord($(this).parent().parent()[0].id);
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
                                            {  $('#hdnDeleteRegisterID').val($(this).parent().parent()[0].id);
                                                $('#deletemodal').modal('show'); }
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
        function GetSearchRecord(iDetails) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/SearchRegister",
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
                                        if (obj[index].IsActive == "1")
                                        { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                        else
                                        { TypeStatus = "<span class='label label-warning'>Inactive</span>"; }

                                        var table = "<tr id='" + obj[index].RegisterID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].AccountNo + "</td>";
                                        table += "<td>" + obj[index].ReceiptNo + "</td>";
                                        table += "<td>" + obj[index].CustomerName + "</td>";
                                        table += "<td>" + obj[index].MobileNo + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Chit.ChitName + "</td>";

                                        table += "<td class='hidden-xs'>" + obj[index].Employee.EmployeeCode + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Chit.Duration + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].InstallmentAmount + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].TotalAmount + "</td>";

                                        if (ActionView == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].RegisterID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionUpdate == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].RegisterID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }

                                        if (ActionDelete == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].RegisterID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }
                                        table += "</tr>";
                                        $("#tblSearchResult_tbody").append(table);
                                    }
                                    $(".Print").click(function () {
                                        var iRegisterID = parseInt($(this).parent().parent()[0].id);
                                        var iBarcode = $(this).attr('Barcode');
                                        $.cookie("Barcode", iBarcode);
                                        $.cookie("RegisterID", iRegisterID);
                                        window.location = "frmBarcode.aspx";
                                    });
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            ViewRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Register");
                                            $("#btnSave").hide();
                                            $("#btnUpdate").hide();
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
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
                                            {  $('#hdnDeleteRegisterID').val($(this).parent().parent()[0].id);
                                                $('#deletemodal').modal('show'); }
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
                                  { "sWidth": "5%" },
                                  { "sWidth": "3%" },
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
            SetSessionValue("RegisterID", id);

            var myWindow = window.open("frmRegister.aspx", "_self");
            return false;
        }
        function ViewRecord(id) {
            SetSessionValue("RegisterID", id);
            SetSessionValue("isView", "View");
            var myWindow = window.open("frmRegister.aspx", "_self");
            return false;
        }
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
        $("#btnOK").click(function () {
            $('#Renewalmodal').modal('hide');
            SetSessionValue("RegisterID", "");
            return false;

        });

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
                               // ClearFields();
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
        $("#aGeneral").click(function () {
            $("#General").show();
            $("#SearchResult").hide();
            GetRecord();
        });

        $("#aSearchResult").click(function () {
            $("#General").hide();
            $("#SearchResult").show();
        
        });
    </script>

</asp:Content>






