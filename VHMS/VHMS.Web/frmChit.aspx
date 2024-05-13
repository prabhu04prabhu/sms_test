<%@ Page Title="Scheme" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmChit.aspx.cs" Inherits="frmChit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Scheme
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Scheme</a></li>
                <li class="active">Scheme</li>
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
                                            <th>Scheme
                                            </th>
                                            <th class="hidden-xs">Code
                                            </th>
                                            <th class="hidden-xs">Duration
                                            </th>
                                           <%-- <th class="hidden-xs">Installment Amount
                                            </th>
                                            <th class="hidden-xs">Total Amount
                                            </th>
                                            <th class="hidden-xs">Bonus Amount
                                            </th>
                                            <th class="hidden-xs">GrandTotal
                                            </th>--%>
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
                                        Scheme</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtName" placeholder="Please enter Scheme Name"
                                        maxlength="150" tabindex="1" />
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Code</label>
                                    <input type="text" class="form-control" id="txtCode" placeholder="Please enter Code"
                                        maxlength="50" tabindex="2" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4" id="divDuration">
                                    <label>
                                        Duration</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtDuration" placeholder="Duration"
                                        maxlength="2" tabindex="3" onkeypress="return isNumberKey(event)" />
                                </div>
                                <div class="checkbox col-md-4">
                                    <label>
                                        <input type="checkbox" id="chkStatus" checked="checked" tabindex="8" />Active
                                    </label>
                                </div>
                               <%-- <div class="form-group col-md-4" id="divInstallmentAmount">
                                    <label>
                                        Installment Amount</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtInstallmentAmount" placeholder="Installment Amount"
                                        maxlength="12" tabindex="4" onkeypress="return IsNumeric(event)" />
                                </div>
                                <div class="form-group col-md-4" id="divTotalAmount">
                                    <label>
                                        Total Amount</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtTotalAmount" placeholder="Total Amount"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true"/>
                                </div>--%>
                            </div>
                          <%--  <div class="row">
                                <div class="form-group col-md-4" id="divBonusAmount">
                                    <label>
                                        Bonus Amount</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtBonusAmount" placeholder="Bonus Amount"
                                        maxlength="12" tabindex="6" onkeypress="return IsNumeric(event)" />
                                </div>
                                <div class="form-group col-md-4" id="divGrossAmount">
                                    <label>
                                        Gross Amount</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtGrossAmount" placeholder="Gross Amount"
                                        maxlength="12" tabindex="7" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                
                            </div>--%>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="button" class="btn btn-danger pull-left" id="btnClose" tabindex="11">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                            <button type="submit" class="btn btn-info pull-right" id="btnSave" tabindex="9">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="submit" class="btn btn-info pull-right" id="btnUpdate" tabindex="10">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
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
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Scheme");
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

            if ($("#txtDuration").val() <= 0 || $("#txtDuration").val() == "" || $("#txtDuration").val() == undefined || $("#txtDuration").val() == null) {
                $.jGrowl("Please enter Duration", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divAmount").addClass('has-error'); $("#txtDuration").focus(); return false;
            } else { $("#divAmount").removeClass('has-error'); }

            //if ($("#txtInstallmentAmount").val() == "" || $("#txtInstallmentAmount").val() == undefined || $("#txtInstallmentAmount").val() == null) {
            //    $.jGrowl("Please enter Installment Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
            //    $("#divInstallmentAmount").addClass('has-error'); $("#txtInstallmentAmount").focus(); return false;
            //} else { $("#divInstallmentAmount").removeClass('has-error'); }

            

            //if ($("#txtBonusAmount").val() == "" || $("#txtBonusAmount").val() == undefined || $("#txtBonusAmount").val() == null) {
            //    $.jGrowl("Please enter Bonus Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
            //    $("#divBonusAmount").addClass('has-error'); $("#txtBonusAmount").focus(); return false;
            //} else { $("#divBonusAmount").removeClass('has-error'); }

           
            var Obj = new Object();
            Obj.ChitID = 0;
            Obj.ChitName = $("#txtName").val().trim();
            Obj.ChitCode = $("#txtCode").val();
            Obj.Duration = $("#txtDuration").val();
            //Obj.InstallmentAmount = $("#txtInstallmentAmount").val();
            //Obj.TotalAmount = $("#txtTotalAmount").val();
            //Obj.BonusAmount = $("#txtBonusAmount").val();
            //Obj.GrandTotal = $("#txtGrossAmount").val();

            Obj.IsActive = $("#chkStatus").is(':checked') ? "1" : "0";

            var sMethodName;
            if ($("#hdnID").val() > 0) {
                Obj.ChitID = $("#hdnID").val();
                sMethodName = "UpdateChit";
            }
            else { sMethodName = "AddChit"; }

            SaveandUpdateChit(Obj, sMethodName);

            return false;
        });


        function ClearFields() {
            $("#txtName").val("");
            $("#txtCode").val("");
            $("#txtDuration").val(0);
            //$("#txtInstallmentAmount").val(0);
            //$("#txtTotalAmount").val(0);
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
                url: "WebServices/VHMSService.svc/GetChit",
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

                                        var table = "<tr id='" + obj[index].ChitID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].ChitName + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].ChitCode + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Duration + "</td>";
                                        //table += "<td class='hidden-xs'>" + obj[index].InstallmentAmount + "</td>";
                                        //table += "<td class='hidden-xs'>" + obj[index].TotalAmount + "</td>";
                                        //table += "<td class='hidden-xs'>" + obj[index].BonusAmount + "</td>";
                                        //table += "<td class='hidden-xs'>" + obj[index].GrandTotal + "</td>";
                                        table += "<td class='hidden-xs'>" + TypeStatus + "</td>";

                                        if (ActionView == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ChitID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionUpdate == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ChitID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }

                                        if (ActionDelete == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ChitID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }

                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Chit");
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
                                  { "sWidth": "0%" },
                                  { "sWidth": "0%" },
                                  //{ "sWidth": "0%" },
                                  //{ "sWidth": "0%" },
                                  //{ "sWidth": "0%" },
                                  //{ "sWidth": "0%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "5%" },
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

        function SaveandUpdateChit(Obj, sMethodName) {
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

                                if (sMethodName == "AddChit")
                                { $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                                else if (sMethodName == "UpdateChit")
                                { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                                $('#compose-modal').modal('hide');
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Chit_A_01" || objResponse.Value == "Chit_U_01") {
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
                                    $("#btnSave").hide();
                                    $("#btnUpdate").show();

                                    $("#hdnID").val(obj.ChitID);
                                    $("#txtName").val(obj.ChitName);
                                    $("#txtCode").val(obj.ChitCode);
                                    $("#txtDuration").val(obj.Duration);
                                    //$("#txtInstallmentAmount").val(obj.InstallmentAmount);
                                    //$("#txtTotalAmount").val(obj.TotalAmount);
                                    //$("#txtBonusAmount").val(obj.BonusAmount);
                                    //$("#txtGrossAmount").val(obj.GrandTotal);

                                    $("#chkStatus").prop("checked", obj.IsActive ? true : false);

                                    $('#compose-modal').modal({ show: true, backdrop: true });
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit Scheme");
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
                url: "WebServices/VHMSService.svc/DeleteChit",
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
                            else if (objResponse.Value == "Chit_R_01" || objResponse.Value == "Chit_D_01") {
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


