<%@ Page Title="Branch Master" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmBranch.aspx.cs" Inherits="frmBranch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Branch
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Master</a></li>
                <li class="active">Branch</li>
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
                                            <th>Branch
                                            </th>
                                            <th class="hidden-xs">ContactPerson
                                            </th>
                                            <th class="hidden-xs">Address
                                            </th>
                                            <th class="hidden-xs">MobileNo
                                            </th>
                                            <th class="hidden-xs">Landline
                                            </th>                                           
                                            <th class="hidden-xs">Status
                                            </th>
                                            <th>
                                            </th>
                                            <th>
                                            </th>
                                            <th>
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
                            <div class="form-group col-md-9" id="divName">
                                <label>
                                    Branch</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtName" placeholder="Please enter Branch Name"
                                    maxlength="150" tabindex="1" />
                            </div>
                            <div class="form-group col-md-3">
                                <label>
                                    Code</label>
                                <input type="text" class="form-control" id="txtCode" placeholder="Please enter Code"
                                    maxlength="50" tabindex="2" />
                            </div>
                            <div class="form-group col-md-12">
                                <label>
                                    Address</label>
                                <textarea id="txtAddress" class="form-control" maxlength="250" tabindex="3" rows="3"></textarea>
                            </div>
                            <div class="form-group col-md-12"">
                                <label>
                                    Contact Person</label>
                                <input type="text" class="form-control" id="txtContactPerson" placeholder="Please enter Contact Person"
                                    maxlength="50" tabindex="4" />
                            </div>
                            
                            <div class="form-group col-md-6"  id="divMobileNo">
                                <label>
                                    MobileNo</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtMobileNo" placeholder="Please enter MobileNo"
                                    maxlength="10" tabindex="5" onkeypress="return isNumberKey(event)" />
                            </div>
                            <div class="form-group col-md-6">
                                <label>
                                    Landline</label>
                                <input type="text" class="form-control" id="txtLandline" placeholder="Please enter Landline"
                                    maxlength="12" tabindex="6"  onkeypress="return isNumberKey(event)"  />
                            </div>
                            <div class="form-group col-md-6">
                                <label>
                                    Email</label>
                                <input type="text" class="form-control" id="txtEmail" placeholder="Please enter Email"
                                    maxlength="50" tabindex="7" />
                            </div>
                            <div class="form-group col-md-6">
                                <label>
                                    Website</label>
                                <input type="text" class="form-control" id="txtWebsite" placeholder="Please enter Website"
                                    maxlength="50" tabindex="8" />
                            </div>
                             <div class="form-group col-md-6" id="divRegion">
                                <label>
                                    Region</label><span class="text-danger">*</span>
                                <select id="ddlRegion" class="form-control select2" data-placeholder="Select Region" tabindex="9">
                            </select>
                            </div>
                             <div class="form-group col-md-6" id="divZone">
                                <label>
                                    Zone</label><span class="text-danger">*</span>
                                <select id="ddlZone" class="form-control select2" data-placeholder="Select Zone" tabindex="10">
                            </select>
                            </div>
                            <div class="form-group col-md-6"">
                                <label>
                                    <input type="checkbox" id="chkHeadOffice" checked="checked" tabindex="11" />&nbsp &nbsp Sales Branch
                                </label>
                            </div>
                            <div class="form-group col-md-6"">
                                <label>
                                    <input type="checkbox" id="chkStatus" checked="checked" tabindex="12" />&nbsp &nbsp Active
                                </label>
                            </div>
                         </div>                        
                        <div class="modal-footer clearfix">
                            <button type="button" class="btn btn-danger pull-left" id="btnClose" tabindex="15">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                            <button type="submit" class="btn btn-info pull-right" id="btnSave" tabindex="13">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="submit" class="btn btn-info pull-right" id="btnUpdate" tabindex="14">
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
            GetRegionList();
            GetRecord();
        });
        $("#ddlRegion").change(function () {
            if ($("#ddlRegion").val() > 0) {
                GetZoneList();
            }
        });
        function GetZoneList() {
            var regionid = $("#ddlRegion").val();
            dProgress(true);
            $("#ddlZone").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetZone",
                data: JSON.stringify({ izoneid: 0, iregionid: regionid }),
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
                                    $("#ddlZone").append('<option value="' + '0' + '">' + '--Select--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive)
                                            $("#ddlZone").append('<option value=' + obj[index].ZoneID + ' >' + obj[index].ZoneName + '</option>');
                                    }
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlZone").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $("#ddlZone").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Branch");
            $('#compose-modal').modal({ show: true, backdrop: true });
            $("#txtName").focus();
            return false;
        });
        function GetRegionList() {
            dProgress(true);
            $("#ddlRegion").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetRegion",
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
                                    $("#ddlRegion").append('<option value="' + '0' + '">' + '--Select--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive)
                                            $("#ddlRegion").append('<option value=' + obj[index].RegionID + ' >' + obj[index].RegionName + '</option>');
                                    }
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlRegion").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $("#ddlRegion").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
        function IsEmail(email) {
            var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!regex.test(email)) {
                return false;
            } else {
                return true;
            }
        }
        $("#btnSave,#btnUpdate").click(function () {
            if (this.id == "btnSave") { if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
            else { if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

            if ($("#txtName").val().trim() == "" || $("#txtName").val().trim() == undefined) {
                $.jGrowl("Please enter Branch", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divName").addClass('has-error'); $("#txtName").focus(); return false;
            } else { $("#divName").removeClass('has-error'); }

            if ($("#txtMobileNo").val().trim() == "" || $("#txtMobileNo").val().trim() == undefined || $("#txtMobileNo").val().length != 10) {
                $.jGrowl("Please enter valid Mobile No", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divMobileNo").addClass('has-error'); $("#txtMobileNo").focus(); return false;
            } else { $("#divMobileNo").removeClass('has-error'); }

            if ($("#txtEmail").val().trim() != "" && $("#txtEmail").val().trim() != undefined) {
                if (IsEmail($("#txtEmail").val()) == false) {
                    $.jGrowl("Please enter Valid Email", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#txtEmail").focus(); return false;
                }
            }

            if ($("#ddlRegion").val() == "0" || $("#ddlRegion").val() == undefined || $("#ddlRegion").val() == null) {
                $.jGrowl("Please select Region", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divRegion").addClass('has-error'); $("#ddlRegion").focus(); return false;
            } else { $("#divRegion").removeClass('has-error'); }

            if ($("#ddlZone").val() == "0" || $("#ddlZone").val() == undefined || $("#ddlZone").val() == null) {
                $.jGrowl("Please select Zone", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divZone").addClass('has-error'); $("#ddlZone").focus(); return false;
            } else { $("#divZone").removeClass('has-error'); }

            var Obj = new Object();
            Obj.BranchID = 0;
            Obj.BranchName = $("#txtName").val().trim();
            Obj.BranchCode = $("#txtCode").val();
            Obj.ContactPerson = $("#txtContactPerson").val();
            Obj.Address = $("#txtAddress").val();
            Obj.Landline = $("#txtLandline").val();
            Obj.Email = $("#txtEmail").val();
            Obj.MobileNo = $("#txtMobileNo").val();

            ObjRegion = new Object();
            ObjRegion.RegionID = $("#ddlRegion").val();
            Obj.Region = ObjRegion;

            ObjZone = new Object();
            ObjZone.ZoneID = $("#ddlZone").val();
            Obj.Zone = ObjZone;

            Obj.Website = $("#txtWebsite").val();
            Obj.IsActive = $("#chkStatus").is(':checked') ? "1" : "0";
            Obj.HeadOfficeFlag = $("#chkHeadOffice").is(':checked') ? "1" : "0";
            var sMethodName;
            if ($("#hdnID").val() > 0) {
                Obj.BranchID = $("#hdnID").val();
                sMethodName = "UpdateBranch";
            }
            else { sMethodName = "AddBranch"; }

            SaveandUpdateBranch(Obj, sMethodName);

            return false;
        });

        $("#btnClose").click(function () {
            $('#compose-modal').modal('hide');
            return false;
        });

        function ClearFields() {
            $("#txtName").val("");
            $("#txtCode").val("");
            $("#ddlRegion").val(0);
            $("#ddlZone").val(0);
            $("#txtMobileNo").val("");
            $("#txtContactPerson").val("");
            $("#txtAddress").val("");
            $("#txtLandline").val("");
            $("#txtEmail").val("");
            $("#txtWebsite").val("");
            $("#chkHeadOffice").prop("checked", false);
            $("#chkStatus").prop("checked", true);
            $("#divName").removeClass('has-error');
            return false;
        }

        function GetRecord() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetBranch",
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

                                        var table = "<tr id='" + obj[index].BranchID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].BranchName + "</td>";
                                        //table += "<td class='hidden-xs'>" + obj[index].BranchCode + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].ContactPerson + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Address + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].MobileNo + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Landline + "</td>";
                                        table += "<td class='hidden-xs'>" + TypeStatus + "</td>";

                                        if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].BranchID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionUpdate == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].BranchID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].BranchID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Branch");
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
                                    { "sWidth": "0%" },
                                    { "sWidth": "0%" },
                                    { "sWidth": "0%" },
                                    { "sWidth": "0%" },
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

        function SaveandUpdateBranch(Obj, sMethodName) {
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

                                if (sMethodName == "AddBranch") { $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                                else if (sMethodName == "UpdateBranch") { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                                $('#compose-modal').modal('hide');
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Branch_A_01" || objResponse.Value == "Branch_U_01") {
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
                url: "WebServices/VHMSService.svc/GetBranchByID",
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

                                    $("#hdnID").val(obj.BranchID);
                                    $("#txtName").val(obj.BranchName);
                                    $("#txtCode").val(obj.BranchCode);
                                    $("#txtAddress").val(obj.Address);
                                    $("#txtMobileNo").val(obj.MobileNo);
                                    $("#txtLandline").val(obj.Landline);
                                    $("#ddlRegion").val(obj.Region.RegionID).change();
                                    $("#ddlZone").val(obj.Zone.ZoneID).change();
                                    $("#txtEmail").val(obj.Email);
                                    $("#txtWebsite").val(obj.Website);
                                    $("#txtContactPerson").val(obj.ContactPerson);
                                    $("#chkHeadOffice").prop("checked", obj.HeadOfficeFlag ? true : false);
                                    $("#chkStatus").prop("checked", obj.IsActive ? true : false);

                                    $('#compose-modal').modal({ show: true, backdrop: true });
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit Branch");
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
                url: "WebServices/VHMSService.svc/DeleteBranch",
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
                            else if (objResponse.Value == "Branch_R_01" || objResponse.Value == "Branch_D_01") {
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


