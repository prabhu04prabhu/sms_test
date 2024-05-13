<%@ Page Title="FinancialYear" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmFinancialYear.aspx.cs" Inherits="frmFinancialYear" %>


<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>FinancialYear
            </h1>
            <div class="form-group  col-md-4" style="margin-left: 255px; margin-top: -30px;">
                    <label>
                        FinancialYear</label>
                    <select id="ddlFinancialYear" class="form-control select2" data-placeholder="Select FinancialYear" tabindex="-1"></select>
                </div>
             <div class="form-group  col-md-2" style="margin-left: 40%; margin-top: -50px;">
                <button type="button" style="background-color: #000000 !important;" class="btn btn-danger pull-right" id="btnView" tabindex="19">
                    Set</button>
            </div>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Master</a></li>
                <li class="active">FinancialYear</li>
            </ol>
            <div class="pull-right">
                <button id="btnAddNew" class="btn btn-info">
                    <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add New</button>
            </div>
            <br />
            <br />
        </section>
        <section class="content">
            <div class="row">
                <div class="col-xs-12">
                    <div class="box box-warning">
                        <div class="box-body">
                            <div class="table-responsive">
                                <table id="tblRecord" class="table table-striped table-bordered bg-info" width="100%">
                                    <thead>
                                        <tr>
                                            <th>SNo
                                            </th>
                                            <th>FinancialYear
                                            </th>
                                            <th class="hidden-xs">Financial Year From
                                            </th>
                                            <th class="hidden-xs">Financial Year To
                                            </th>
                                            <th class="hidden-xs">Financial Year Status
                                            </th>
                                            <th class="hidden-xs">Status
                                            </th>
                                            <th>View
                                            </th>
                                            <th>Edit
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody id="tblRecord_tbody">
                                    </tbody>
                                </table>
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
                            <div class="row">
                                <div class="form-group col-md-12" id="divName">
                                    <label>
                                        Title</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtName" placeholder="Please enter Title"
                                        maxlength="5" tabindex="1" autocomplete="off" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-5" id="divBillDate">
                                    <label>
                                        From  Date</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOPBillingDate"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <input type="text" class="form-control pull-right" tabindex="2" id="txtBillDate" readonly />
                                    </div>
                                </div>
                                <%--<div class="form-group col-md-4" id="divDuration">
                                    <label>
                                        Duration</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtDuration" placeholder="Duration"
                                        maxlength="2" tabindex="3" onkeypress="return isNumberKey(event)" />
                                </div>--%>
                                <div class="form-group col-md-5" id="divTODate">
                                    <label>
                                        TO  Date</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtTODate"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <input type="text" class="form-control pull-right" tabindex="3" id="txtTODate" readonly />
                                    </div>
                                </div>
                                <div class="checkbox col-md-2">
                                    <label>
                                        <input type="checkbox" id="chkStatus" checked="checked" tabindex="4" />Active
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnSave" tabindex="5">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="submit" class="btn btn-info pull-left" id="btnUpdate" tabindex="6">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="7">
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
            ActionFinancialYear = '<%=Session["FK_FinancialYearID"]%>';
            ActionCurrentFinancialYear = '<%=Session["Current_FinancialYear"]%>';
           
            $('input,select').keydown(function (event) { //event==Keyevent
                if (event.which == 13) {
                    var inputs = $(this).closest('form').find(':input:visible:not(disabled):not([readonly])');
                    inputs.eq(inputs.index(this) + 1).focus();
                    event.preventDefault(); //Disable standard Enterkey action

                }
            });
            $("#txtBillDate,#txtTODate").attr("data-link-format", "dd/MM/yyyy");

            $("#txtBillDate,#txtTODate").datetimepicker({
                pickTime: false,
                useCurrent: true,
                format: 'DD/MM/YYYY'
            });
        

            if (ActionAdd != "1") {
                $("#btnAddNew").remove();
                $("#btnSave").remove();
            }

            if (ActionUpdate != "1") {
                $("#btnUpdate").remove();
            }

            pLoadingSetup(false);
            pLoadingSetup(true);
            GetFinancialYear("ddlFinancialYear");
            GetRecord();
            $("#ddlFinancialYear").val(ActionFinancialYear).change();
        });

        $("#btnView").click(function () {
            SetSessionValue("FK_FinancialYearID", $("#ddlFinancialYear").val());
            SetSessionValue("Fin_Title", $("#ddlFinancialYear option:selected").text());
            $.jGrowl("Financial Year Changed Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
            if (ActionCurrentFinancialYear != $("#ddlFinancialYear").val()) {
                $(".fin-year-display").css("background-color", "#a30f42");
            }
            else {
                $(".fin-year-display").css("background-color", "#155388");
            }
            $("#fin_title").text("FY : " + $("#ddlFinancialYear option:selected").text());
            
        });


        function GetFinancialYear(ddlname) {
            var sControlName = "#" + ddlname;
            dProgress(true);
            $(sControlName).empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetFinancialYear",
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
                                        if (obj[index].Active)
                                            $(sControlName).append("<option value='" + obj[index].FinancialYearID + "'>" + obj[index].Title + "</option>");
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

        $("#btnAddNew").click(function () {
            ClearFields();
            $("#hdnID").val("");
            $("#btnSave").show();
            $("#btnUpdate").hide();
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add FinancialYear");
            $('#compose-modal').modal({ show: true, backdrop: true });
            $("#txtName").focus();
            return false;
        });
        //$("#txtDuration,#txtInstallmentAmount,#txtBonusAmount").change(function () {
        //    if ($("#txtDuration").val().trim() == "" || $("#txtDuration").val().trim() == undefined)
        //        $("#txtDuration").val(0);
        //    if ($("#txtInstallmentAmount").val().trim() == "" || $("#txtInstallmentAmount").val().trim() == undefined)
        //        $("#txtInstallmentAmount").val(0);
        //    if ($("#txtBonusAmount").val().trim() == "" || $("#txtBonusAmount").val().trim() == undefined)
        //        $("#txtBonusAmount").val(0);

        //    $("#txtTotalAmount").val((parseFloat($("#txtInstallmentAmount").val()) * parseFloat($("#txtDuration").val())).toFixed(2));
        //    var iAmt = (parseFloat($("#txtBonusAmount").val()) + parseFloat($("#txtTotalAmount").val()));
        //    $("#txtGrossAmount").val(iAmt.toFixed(2));
        //});


        //$("#txtDuration,#txtInstallmentAmount,#txtBonusAmount").change(function () {
        //    var Dur = 0;
        //    var Installamt = 0;
        //    var Bonus = 0;
        //    var totAmt = 0;
        //    var Grossamt = 0;

        //    if ($("#txtDuration").val() > 0)
        //        Dur = $("#txtDuration").val();

        //    if ($("#txtInstallmentAmount").val() > 0)
        //        Installamt = $("#txtInstallmentAmount").val();

        //    if ($("#txtBonusAmount").val() > 0)
        //        Bonus = $("#txtBonusAmount").val();
        //    totAmt = parseFloat(Dur) * parseFloat(Installamt);
        //    $("#txtTotalAmount").val(totAmt.toFixed(2));
        //    Grossamt = parseFloat(Bonus) + parseFloat(totAmt);
        //    $("#txtGrossAmount").val(Grossamt.toFixed(2));
        //});

        $("#btnClose").click(function () {
            $('#compose-modal').modal('hide');
            return false;
        });
        $("#btnSave,#btnUpdate").click(function () {
            if (this.id == "btnSave")
            { if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
            else { if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

            if ($("#txtName").val().trim() == "" || $("#txtName").val().trim() == undefined) {
                $.jGrowl("Please enter Scheme Name", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divName").addClass('has-error'); $("#txtName").focus(); return false;
            } else { $("#divName").removeClass('has-error'); }

            if ($("#txtBillDate").val().trim() == "" || $("#txtBillDate").val().trim() == undefined) {
                $.jGrowl("Please select from Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divBillDate").addClass('has-error'); $("#txtBillDate").focus(); return false;
            }
            else { $("#divBillDate").removeClass('has-error'); }

            if ($("#txtTODate").val().trim() == "" || $("#txtTODate").val().trim() == undefined) {
                $.jGrowl("Please select To Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divTODate").addClass('has-error'); $("#txtTODate").focus(); return false;
            }
            else { $("#divTODate").removeClass('has-error'); }


            //var FAmount = $("#txtFromAmount").val();
            //var ToAmount = $("#txtToAmount").val();
            //if ($("#txtFromAmount").val() > 0) {
            //    // if ($("#txtFromAmount").val() >= $("#txtToAmount").val()) {
            //    if (FAmount >= ToAmount) {
            //        $.jGrowl("Please Change FromAmount Greater than ToAmount ", { sticky: false, theme: 'warning', life: jGrowlLife });
            //        $("#divToAmount").addClass('has-error'); $("#txtToAmount").focus(); return false;
            //        //$.jGrowl("Please enter To Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
            //        //$("#txtToAmount").focus(); return;
            //    }
            //}

            var Obj = new Object();
            Obj.FinancialYearID = 0;
            Obj.Title = $("#txtName").val().trim();
            Obj.sYearFrom = $("#txtBillDate").val().trim();
            Obj.sYearTo = $("#txtTODate").val().trim();
            Obj.sInwardDate = "Open";
            Obj.Active = $("#chkStatus").is(':checked') ? "1" : "0";

            var sMethodName;
            if ($("#hdnID").val() > 0) {
                Obj.FinancialYearID = $("#hdnID").val();
                sMethodName = "UpdateFinancialYear";
               
            }
            else {
                
                sMethodName = "AddFinancialYear";
            }

            SaveandUpdateFinancialYear(Obj, sMethodName);

            return false;
        });


        function ClearFields() {
            $("#txtName").val("");
            $("#txtBillDate").val("");
            $("#txtTODate").val("");
            $("#chkStatus").prop("checked", true);

            $("#divName").removeClass('has-error');
            return false;
        }

        function GetRecord() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetFinancialYear",
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
                                        if (obj[index].Active == "1")
                                        { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                        else
                                        { TypeStatus = "<span class='label label-warning'>Inactive</span>"; }

                                        var table = "<tr id='" + obj[index].FinancialYearID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].Title + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].sYearFrom + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].sYearTo + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Status + "</td>";
                                        table += "<td class='hidden-xs'>" + TypeStatus + "</td>";

                                        if (ActionView == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].FinancialYearID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionUpdate == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].FinancialYearID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }

                                        //if (ActionDelete == "1")
                                        //{ table += "<td style='text-align:center;'><a href='#' id=" + obj[index].FinancialYearID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        //else
                                        //{ table += "<td></td>"; }

                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View FinancialYear");
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
                                    { "sWidth": "35%" },
                                    { "sWidth": "15%" },
                                    { "sWidth": "15%" },
                                    { "sWidth": "15%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "5%" },
                                   // { "sWidth": "5%" },
                                    { "sWidth": "5%" }
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

        function SaveandUpdateFinancialYear(Obj, sMethodName) {
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

                                if (sMethodName == "AddFinancialYear")
                                { $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                                else if (sMethodName == "UpdateFinancialYear")
                                { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                                $('#compose-modal').modal('hide');
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "FinancialYear_A_01" || objResponse.Value == "FinancialYear_U_01") {
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
                url: "WebServices/VHMSService.svc/GetFinancialYearByID",
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

                                    $("#hdnID").val(obj.FinancialYearID);
                                    $("#txtName").val(obj.Title);
                                    $("#txtBillDate").val(obj.sYearFrom);
                                    $("#txtTODate").val(obj.sYearTo);
                                    $("#chkStatus").prop("checked", obj.Active ? true : false);

                                    $('#compose-modal').modal({ show: true, backdrop: true });
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit FinancialYear");
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
                url: "WebServices/VHMSService.svc/DeleteFinancialYear",
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
                            else if (objResponse.Value == "FinancialYear_R_01" || objResponse.Value == "FinancialYear_D_01") {
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
</asp:Content>


