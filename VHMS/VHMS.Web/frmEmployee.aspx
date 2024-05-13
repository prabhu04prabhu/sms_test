<%@ Page Title="Employee" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmEmployee.aspx.cs" Inherits="frmEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Employee
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Master</a></li>
                <li class="active">Employee</li>
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
                                            <th>Emp.ID
                                            </th>
                                            <th>Employee Name
                                            </th>
                                            <th>EmployeeCode
                                            </th>
                                            <th>PhoneNo1
                                            </th>
                                            <th>Gender
                                            </th>
                                            <th>Status
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
            <div class="modal fade" id="compose-modal" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group  col-md-6" id="divName">
                                    <label>
                                        Employee Name</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtName" placeholder="Please enter Employee Name"
                                        maxlength="150" tabindex="1" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-6" id="divEmployeeCode">
                                    <label>
                                        Employee Code</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtEmployeeCode" placeholder="Please enter Employee Code"
                                        maxlength="150" tabindex="1" autocomplete="off" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4" id="divDOB">
                                    <label>
                                        Date of Birth</label><span class="text-danger">*</span>

                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOPBillingDate"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <input type="text" class="form-control pull-right" tabindex="2" id="txtDOB" readonly="true" />
                                    </div>
                                </div>
                                <div class="form-group col-md-4" id="divPhoneNo1">
                                    <label>
                                        PhoneNo1</label><span class="text-danger">*</span>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-phone"></i></div>
                                        <input type="text" class="form-control" id="txtPhoneNo1" placeholder="Phone No" maxlength="13"
                                            tabindex="3" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                    </div>
                                </div>
                                <div class="form-group col-md-4" id="divPhoneNo2">
                                    <label>
                                        PhoneNo2</label>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-phone"></i></div>
                                        <input type="text" class="form-control" id="txtPhoneNo2" placeholder="Phone No" maxlength="13"
                                            tabindex="4" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group " id="divAddress">
                                <label>
                                    Address</label>
                                <textarea id="txtAddress" class="form-control" maxlength="255" tabindex="5" rows="3" aria-autocomplete="none"></textarea>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4" id="divDOj">
                                    <label>
                                        Date of Join</label><span class="text-danger">*</span>

                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOPBillingDate"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <input type="text" class="form-control pull-right" tabindex="6" id="txtDOJ" readonly="true" />
                                    </div>
                                </div>
                                <div class="form-group col-md-5" id="divIDProof">
                                    <label>
                                        ID Proof</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtIDProof" placeholder="Please enter IDProof"
                                        maxlength="150" tabindex="7" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3" id="divESLID">
                                    <label>
                                        ESL ID</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtESLID" placeholder="ESL ID"
                                        maxlength="12" tabindex="9" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-6" id="divBloodGroup">
                                    <label>
                                        Blood Group</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtBloodGroup" placeholder="Please enter BloodGroup"
                                        maxlength="150" tabindex="8" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-4" id="divCategory">
                                    <label>
                                        Customer Type</label><span class="text-danger">*</span>
                                    <select id="ddlGender" class="form-control" tabindex="9">
                                        <option value="Male" selected="selected">Male</option>
                                        <option value="Female">Female</option>
                                    </select>
                                </div>

                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" id="chkStatus" checked="checked" tabindex="10" />Active
                                    </label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-5">
                                    <label>
                                        Image 1</label>
                                    <input name="imagefile" type="file" id="imagefile" data-image-src="imgUpload1_view" accept="image/*" onchange="ResizeImage('imagefile');" />

                                    <img src="" id="imgUpload1_view" alt="" style="width: 141px;" />
                                </div>
                                <div class="form-group col-md-5">
                                    <label>
                                        Image 2</label>
                                    <input name="imagefile" type="file" id="imagefile2" data-image-src="imgUpload2_view" accept="image/*" onchange="ResizeImage('imagefile2');" />

                                    <img src="" id="imgUpload2_view" alt="" style="width: 141px;" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnSave" tabindex="11">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="submit" class="btn btn-info pull-left" id="btnUpdate" tabindex="12">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="13">
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
        var pageUrl = '<%=ResolveUrl("~/frmEmployee.aspx") %>';
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


            $("#txtDOB,#txtDOJ").attr("data-link-format", "dd/MM/yyyy");

            $("#txtDOB,#txtDOJ").datetimepicker({
                pickTime: false,
                useCurrent: true,
                maxDate: moment(),
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

            GetRecord();
        });

        $("#btnAddNew").click(function () {
            ClearFields();
            $("#hdnID").val("");
            $("#txtName").val("");
            $("#txtEmployeeCode").val("");
            $("#txtPhoneNo1").val("");
            $("#txtPhoneNo2").val("");
            $("#txtAddress").val("");
            $("#txtBloodGroup").val("");
            $("#txtIDProof").val("");
            $("#txtBillDate").val("");
            $("#txtDOJ").val("");
            $("#ddlGender").val("Male");

            $("#btnSave").show();
            $("#btnUpdate").hide();
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Employee");
            $('#compose-modal').modal({ show: true, backdrop: true });
            $("#txtName").focus();
            return false;
        });

        function saveimage(id) {
            pLoadingSetup(false);

            var images = $("#" + id + "").attr('src');
            var ImageSave = images.replace("data:image/jpeg;base64,", "");
            var submitval = JSON.stringify({ data: ImageSave });

            $.ajax({
                type: "POST",
                url: pageUrl + "/saveimage",
                data: submitval,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $get(id).src = "./" + r.d;
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
            pLoadingSetup(true);
        }

        $("#btnSave,#btnUpdate").click(function () {
            if (this.id == "btnSave") { if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
            else { if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

            if ($("#txtName").val().trim() == "" || $("#txtName").val().trim() == undefined) {
                $.jGrowl("Please enter Employee Name", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divName").addClass('has-error'); $("#txtName").focus(); return false;
            } else { $("#divName").removeClass('has-error'); }

            if ($("#txtDOB").val().trim() == "" || $("#txtDOB").val().trim() == undefined) {
                $.jGrowl("Please select Date of Birth Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divDOB").addClass('has-error'); $("#txtDOB").focus(); return false;
            }
            else { $("#divDOB").removeClass('has-error'); }


            if ($("#txtDOJ").val().trim() == "" || $("#txtDOJ").val().trim() == undefined) {
                $.jGrowl("Please select Date of Join Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divDOj").addClass('has-error'); $("#txtDOJ").focus(); return false;
            }
            else { $("#divDOj").removeClass('has-error'); }

            if ($("#txtPhoneNo1").val().trim() == "" || $("#txtPhoneNo1").val().trim() == undefined) {
                $.jGrowl("Please enter Employee Phone No1", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divPhoneNo1").addClass('has-error'); $("#txtPhoneNo1").focus(); return false;
            } else { $("#divPhoneNo1").removeClass('has-error'); }

            
            if ($("#txtESLID").val().trim() == "" || $("#txtESLID").val().trim() == undefined) {
                $.jGrowl("Please enter ESLID", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divESLID").addClass('has-error'); $("#txtESLID").focus(); return false;
            } else { $("#divESLID").removeClass('has-error'); }

            if ($("#txtAddress").val().trim() == "" || $("#txtAddress").val().trim() == undefined) {
                $.jGrowl("Please enter Address", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divAddress").addClass('has-error'); $("#txtAddress").focus(); return false;
            } else { $("#divAddress").removeClass('has-error'); }


            var Obj = new Object();

            Obj.EmployeeID = 0;
            Obj.EmployeeName = $("#txtName").val().trim();
            Obj.EmployeeCode = $("#txtEmployeeCode").val();
            Obj.sDob = $("#txtDOB").val();
            Obj.Address = $("#txtAddress").val();
            Obj.PhoneNo1 = $("#txtPhoneNo1").val();
            Obj.PhoneNo2 = $("#txtPhoneNo2").val();
            Obj.sDateofjoin = $("#txtDOJ").val();
            Obj.IDProof = $("#txtIDProof").val();
            Obj.ESLID = $("#txtESLID").val();
            console.log($("#ddlGender").val());
            Obj.Gender = $("#ddlGender").val();
            Obj.ProofImage1 = $("[id*=imgUpload1_view]").attr("src");;
            Obj.ProofImage2 = $("[id*=imgUpload2_view]").attr("src");;
            Obj.BloodGroup = $("#txtBloodGroup").val();
            Obj.IsActive = $("#chkStatus").is(':checked') ? "1" : "0";

            var sMethodName;
            if ($("#hdnID").val() > 0) {
                Obj.EmployeeID = $("#hdnID").val();
                sMethodName = "UpdateEmployee";
            }
            else { sMethodName = "AddEmployee"; }

            SaveandUpdateEmployee(Obj, sMethodName);

            return false;
        });

        $("#btnClose").click(function () {
            $('#compose-modal').modal('hide');
            return false;
        });

        function ClearFields() {
            $("#txtName").val("");
            $("#txtEmployeeCode").val("");
            $("#txtPhoneNo1").val("");
            $("#txtPhoneNo2").val("");
            $("#txtAddress").val("");
            $("#txtBloodGroup").val("");
            $("#txtIDProof").val("");
            $("#txtBillDate").val("");
            $("#txtDOJ").val("");
            $("#ddlGender").val("Male");
            $("#txtESLID").val("");
            $get("imgUpload1_view").src = "";
            $get("imgUpload2_view").src = "";
            $("[id*=imgUpload1_view]").css("visibility", "hidden");
            $("[id*=imgUpload2_view]").css("visibility", "hidden");
            $("#divName").removeClass('has-error');
            return false;
        }

        function GetRecord() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetEmployee",
                data: JSON.stringify({ CountryID: 0 }),
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

                                        var table = "<tr id='" + obj[index].EmployeeID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].EmployeeID + "</td>";
                                        table += "<td>" + obj[index].EmployeeName + "</td>";
                                        table += "<td>" + obj[index].EmployeeCode + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].PhoneNo1 + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Gender + "</td>";
                                        table += "<td class='hidden-xs'>" + TypeStatus + "</td>";

                                        if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].EmployeeID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionUpdate == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].EmployeeID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].EmployeeID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Employee");
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
                                    { "sWidth": "5%" },
                                    { "sWidth": "25%" },
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

        function SaveandUpdateEmployee(Obj, sMethodName) {
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

                                if (sMethodName == "AddEmployee") { $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                                else if (sMethodName == "UpdateEmployee") { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                                $('#compose-modal').modal('hide');
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Employee_A_01" || objResponse.Value == "Employee_U_01") {
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
                url: "WebServices/VHMSService.svc/GetEmployeeByID",
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

                                    $("#hdnID").val(obj.EmployeeID);
                                    $("#txtName").val(obj.EmployeeName);
                                    $("#txtEmployeeCode").val(obj.EmployeeCode);
                                    $("#txtDOB").val(obj.sDob);
                                    $("#txtAddress").val(obj.Address);
                                    $("#txtPhoneNo1").val(obj.PhoneNo1);
                                    $("#txtPhoneNo2").val(obj.PhoneNo2);
                                    $("#txtDOJ").val(obj.sDateofjoin);
                                    $("#txtIDProof").val(obj.IDProof);
                                    $("#txtESLID").val(obj.ESLID)
                                    $("#ddlGender").val(obj.Gender);
                                    $("#txtBloodGroup").val(obj.BloodGroup);
                                    $("#chkStatus").prop("checked", obj.IsActive ? true : false);

                                    $("[id*=imgUpload1_view]").css("visibility", "visible");
                                    $("[id*=imgUpload1_view]").attr("src", obj.ProofImage1);
                                    $("[id*=imgUpload2_view]").css("visibility", "visible");
                                    $("[id*=imgUpload2_view]").attr("src", obj.ProofImage2);

                                    $('#compose-modal').modal({ show: true, backdrop: true });
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit Employee");
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
                url: "WebServices/VHMSService.svc/DeleteEmployee",
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
                            else if (objResponse.Value == "Employee_R_01" || objResponse.Value == "Employee_D_01") {
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

        function ResizeImage(img_id) {

            var filesToUpload = document.getElementById(img_id).files;
            var file = filesToUpload[0];

            // Create an image
            var img = document.createElement("img");
            // Create a file reader
            var reader = new FileReader();
            // Set the image once loaded into file reader
            reader.onload = function (e) {
                //img.src = e.target.result;
                var img = new Image();

                img.src = this.result;

                setTimeout(function () {
                    var canvas = document.createElement("canvas");

                    var MAX_WIDTH = 1500;
                    var MAX_HEIGHT = 1000;
                    var width = img.width;
                    var height = img.height;

                    if (width > height) {
                        if (width > MAX_WIDTH) {
                            height *= MAX_WIDTH / width;
                            width = MAX_WIDTH;
                        }
                    } else {
                        if (height > MAX_HEIGHT) {
                            width *= MAX_HEIGHT / height;
                            height = MAX_HEIGHT;
                        }
                    }
                    canvas.width = width;
                    canvas.height = height;
                    var ctx = canvas.getContext("2d");
                    ctx.drawImage(img, 0, 0, width, height);
                    var dataurl = canvas.toDataURL("image/jpeg");
                    var image_view = $("#" + img_id).attr("data-image-src");
                    document.getElementById(image_view).src = dataurl;
                    $("#" + image_view).css({ "visibility": "visible", "display": "block" });
                    saveimage(image_view);
                }, 100);
            }
            // Load files into file reader
            reader.readAsDataURL(file);
        }
    </script>
</asp:Content>
