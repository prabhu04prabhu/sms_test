<%@ Page Title="Gift" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmGift.aspx.cs" Inherits="frmGift" %>


<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Gift
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Master</a></li>
                <li class="active">Gift</li>
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
                                            <th>Gift
                                            </th>
                                            <th class="hidden-xs">Gift Code
                                            </th>
                                            <th class="hidden-xs">From Amount
                                            </th>
                                            <th class="hidden-xs">To Amount
                                            </th>
                                            <th class="hidden-xs">Status
                                            </th>
                                            <th>View
                                            </th>
                                            <th>Edit
                                            </th>
                                            <th>Delete
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
                                <div class="form-group col-md-8" id="divName">
                                    <label>
                                        Gift Name</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtName" placeholder="Please enter Gift Name"
                                        maxlength="150" tabindex="1" autocomplete="off"/>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Gift Code</label>
                                    <input type="text" class="form-control" id="txtCode" placeholder="Please enter Gift Code"
                                        maxlength="50" tabindex="2" autocomplete="off"/>
                                </div>
                            </div>
                            <div class="row">
                                <%--<div class="form-group col-md-4" id="divDuration">
                                    <label>
                                        Duration</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtDuration" placeholder="Duration"
                                        maxlength="2" tabindex="3" onkeypress="return isNumberKey(event)" />
                                </div>--%>
                                <div class="form-group col-md-4" id="divfromAmount">
                                    <label>
                                        From Amount</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtFromAmount" placeholder="From Amount"
                                        maxlength="12" tabindex="3" onkeypress="return IsNumeric(event)" autocomplete="off"/>
                                </div>
                                <div class="form-group col-md-4" id="divToAmount">
                                    <label>
                                        To Amount</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtToAmount" placeholder="To Amount"
                                        maxlength="12" tabindex="4" onkeypress="return IsNumeric(event)" autocomplete="off"/>
                                </div>
                                <div class="checkbox col-md-4">
                                    <label>
                                        <input type="checkbox" id="chkStatus" checked="checked" tabindex="5" />Active
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnSave" tabindex="6">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="submit" class="btn btn-info pull-left" id="btnUpdate" tabindex="7">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="8">
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

            if (ActionUpdate != "1") {
                $("#btnUpdate").remove();
            }

            pLoadingSetup(false);
            pLoadingSetup(true);

            GetRecord();
        });

        $("#btnAddNew").click(function () {
            ClearFields();
            $("#hdnID").val("");
            $("#btnSave").show();
            $("#btnUpdate").hide();
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Gift");
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

            if ($("#txtFromAmount").val() == "" || $("#txtFromAmount").val() == undefined || $("#txtFromAmount").val() == null) {
                $.jGrowl("Please enter From Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divfromAmount").addClass('has-error'); $("#txtFromAmount").focus(); return false;
            } else { $("#divfromAmount").removeClass('has-error'); }

            if ($("#txtToAmount").val() == "" || $("#txtToAmount").val() == undefined || $("#txtToAmount").val() == null) {
                $.jGrowl("Please enter To Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divToAmount").addClass('has-error'); $("#txtToAmount").focus(); return false;
            } else { $("#divToAmount").removeClass('has-error'); }

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
            Obj.GiftID = 0;
            Obj.GiftName = $("#txtName").val().trim();
            Obj.GiftCode = $("#txtCode").val();
            Obj.FromAmount = $("#txtFromAmount").val();
            Obj.ToAmount = $("#txtToAmount").val();

            Obj.IsActive = $("#chkStatus").is(':checked') ? "1" : "0";

            var sMethodName;
            if ($("#hdnID").val() > 0) {
                Obj.GiftID = $("#hdnID").val();
                sMethodName = "UpdateGift";
            }
            else { sMethodName = "AddGift"; }

            SaveandUpdateGift(Obj, sMethodName);

            return false;
        });


        function ClearFields() {
            $("#txtName").val("");
            $("#txtCode").val("");
            $("#txtDuration").val(0);
            $("#txtToAmount").val(0);
            $("#txtFromAmount").val(0);
            //$("#txtBonusAmount").val(0);
            //$("#txtGrossAmount").val(0);
            $("#chkStatus").prop("checked", true);

            $("#divName").removeClass('has-error');
            return false;
        }

        function GetRecord() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetGift",
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

                                        var table = "<tr id='" + obj[index].GiftID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].GiftName + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].GiftCode + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].FromAmount + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].ToAmount + "</td>";
                                        table += "<td class='hidden-xs'>" + TypeStatus + "</td>";

                                        if (ActionView == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].GiftID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionUpdate == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].GiftID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }

                                        if (ActionDelete == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].GiftID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }

                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Gift");
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
                                    { "sWidth": "30%" },
                                    { "sWidth": "15%" },
                                    { "sWidth": "15%" },
                                    { "sWidth": "15%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "5%" },
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

        function SaveandUpdateGift(Obj, sMethodName) {
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

                                if (sMethodName == "AddGift")
                                { $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                                else if (sMethodName == "UpdateGift")
                                { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                                $('#compose-modal').modal('hide');
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Gift_A_01" || objResponse.Value == "Gift_U_01") {
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
                url: "WebServices/VHMSService.svc/GetGiftByID",
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

                                    $("#hdnID").val(obj.GiftID);
                                    $("#txtName").val(obj.GiftName);
                                    $("#txtCode").val(obj.GiftCode);
                                    $("#txtFromAmount").val(obj.FromAmount);
                                    $("#txtToAmount").val(obj.ToAmount);
                                    $("#chkStatus").prop("checked", obj.IsActive ? true : false);

                                    $('#compose-modal').modal({ show: true, backdrop: true });
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit Gift");
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
                url: "WebServices/VHMSService.svc/DeleteGift",
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
                            else if (objResponse.Value == "Gift_R_01" || objResponse.Value == "Gift_D_01") {
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


