<%@ Page Title="Op Billing report" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmOPReport.aspx.cs" Inherits="frmOPReport" %>


<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <style>
        div.dt-buttons {
            float: right !important;
            margin-left: 10px !important;
        }
    </style>
    <%--<style>
   .table-bordered>thead>tr>th, .table-bordered>tbody>tr>th, .table-bordered>tfoot>tr>th, .table-bordered>thead>tr>td, .table-bordered>tbody>tr>td, .table-bordered>tfoot>tr>td{
    border: 1px solid #c3b8b8 !important; 
    }
    </style>--%>

    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.1.0/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.10/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.1.0/css/select.dataTables.min.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>OP Billing Report
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Report</a></li>
                <li class="active">OP Billing Report</li>
            </ol>
            <div class="pull-right">
                <button id="btnAddNew" style="display: none;" class="btn btn-info">
                    <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add New</button>
            </div>
            <br />
            <br />
        </section>
        <section class="content">
            <div class="box box-warning box-solid" id="divFilter">
                <div class="box-header with-border">
                    <div class="box-title">
                        Filter Options
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="form-group col-md-2 col-sm-4" id="divDOB">
                            <label>
                                From</label><span class="text-danger">*</span>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDOB"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="1" id="txtDOB" readonly />
                            </div>
                        </div>
                        <div class="form-group col-md-2" id="divDOR">
                            <label>
                                TO</label><span class="text-danger">*</span>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDOR"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="2" id="txtDOR" readonly />
                            </div>
                        </div>
                        <div class="form-group col-md-2" id="divPatient">
                            <label>
                                Patient</label><span class="text-danger">*</span>
                            <select id="ddlPatient" class="form-control" tabindex="3">
                            </select>
                        </div>
                        <div class="form-group col-md-2" id="divDoctor">
                            <label>
                                Doctor</label><span class="text-danger">*</span>
                            <select id="ddlDoctor" class="form-control" tabindex="4">
                            </select>
                        </div>
                        <div class="form-group col-md-2" id="divDescription">
                            <label>
                                Category </label><span class="text-danger">*</span>
                            <select id="ddlDescription" class="form-control" tabindex="5">
                            </select>
                        </div>
                        <div class="form-group col-md-2" id="divUser">
                            <label>
                                User</label><span class="text-danger">*</span>
                            <select id="ddlUser" class="form-control" tabindex="6">
                            </select>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" id="divRecords">
                <div class="col-xs-12">
                    <div class="box box-warning">
                        <div class="box-body">
                            <div class="table-responsive">
                                <table id="tblRecord" class="table table-condensed table-striped table-bordered" width="100%">
                                    <thead>
                                        <tr class="bg-primary">
                                            <th>Sl.No
                                            </th>
                                            <th>Bill NO#
                                            </th>
                                            <th>Date
                                            </th>
                                            <th>Patient Name
                                            </th>
                                            <th>Patient Phone
                                            </th>
                                            <th>Doctor
                                            </th>
                                            <th>Description
                                            </th>
                                            <th>Amount
                                            </th>
                                            <th>Created By
                                            </th>
                                             <th>Status
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
            <div class="box sticky" id="divHeader">
                <div class="box-body">
                    <div class="table-responsive">
                        <div id="divJobCardInfo">
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>

    <input type="hidden" id="hdnID" />

    <%--<script type="text/javascript" src="https://datatables.net/release-datatables/media/js/jquery.dataTables.js"></script>--%>
    <%--<script type="text/javascript" src="https://datatables.net/release-datatables/extensions/TableTools/js/dataTables.tableTools.js"></script>--%>
    <%-- <script type="text/javascript" src="https://cdn.datatables.net/tabletools/2.2.4/js/dataTables.tableTools.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.4/js/dataTables.buttons.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.4/js/buttons.flash.min.js"></script>--%>
    <%--<script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.36/build/pdfmake.min.js"></script>--%>
    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/vfs_fonts.js"></script>--%>
    <%--<script src="https://cdn.datatables.net/buttons/1.5.4/js/buttons.html5.min.js"></script>--%>
     <%--<script type="text/javascript" src="https://cdn.datatables.net/buttons/1.5.4/js/buttons.print.min.js"></script>--%>
    <script type="text/javascript" src="JS/jquery.dataTables.js"></script>
    <script src="JS/dataTables.tableTools.js"></script>
    <script src="JS/dataTables.buttons.min.js"></script>
    <script src="JS/jszip.min.js"></script>
    <script src="JS/buttons.flash.min.js"></script>
    <script src="JS/vfs_fonts.js"></script>
     <script src="JS/pdfmake.min.js"></script>
    <script src="JS/buttons.html5.min.js"></script>
    <script type="text/javascript" src="JS/buttons.print.min.js"></script>
<%--     <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.min.css"/>
	<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.5.4/css/buttons.dataTables.min.css"/>--%>
  
   <link rel="stylesheet" type="text/css" href="css/jquery.dataTables.min.css"/>
	<link rel="stylesheet" type="text/css" href="css/buttons.dataTables.min.css"/>

    <script type="text/javascript">

        var pageUrl = '<%=ResolveUrl("~/CS.aspx") %>';

        $(document).ready(function () {


            $("#txtDOB,#txtDOR").attr("data-link-format", "dd/MM/yyyy");
            $("#txtDOB,#txtDOR").datetimepicker({
                pickTime: false,
                useCurrent: true,
                format: 'DD/MM/YYYY'
            });
            pLoadingSetup(false);

            var objFilter = new Object();
            GetDoctorList("ddlDoctor");
          //  GetPatientList("ddlPatient");
            GetDescriptionList("ddlDescription");
            GetUserList("ddlUser");
           // GetRecord(objFilter);
            $("#divPatient").hide();
            pLoadingSetup(true);
            $("#txtDOB").focus();
            $("#txtDOR").focus();
            $("#ddlPatient").focus();

            $('#tblRecord').DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ]
            });

            var objFilter = new Object();
           // objFilter.PatientID = $('#ddlPatient').val();
            objFilter.PatientID = 0;
            objFilter.DoctorID = $('#ddlDoctor').val();
            objFilter.DescriptionID = $('#ddlDescription').val();
            objFilter.UserID = $('#ddlUser').val();
            objFilter.DateTo = $('#txtDOR').val();
            objFilter.DateFrom = $('#txtDOB').val();
            GetRecord(objFilter);
        });
        $('#txtDOB,#ddlDescription,#txtDOR,#ddlDoctor,#ddlPatient,#ddlUser').change(function () {
            var objFilter = new Object();
            objFilter.PatientID = 0;
            objFilter.DoctorID = $('#ddlDoctor').val();
            objFilter.DescriptionID = $('#ddlDescription').val();
            objFilter.UserID = $('#ddlUser').val();
            objFilter.DateTo = $('#txtDOR').val();
            objFilter.DateFrom = $('#txtDOB').val();
            GetRecord(objFilter);
        });
        function GetUserList(ddlname) {
            var sControlName = "#" + ddlname;
            dProgress(true);
            $(sControlName).empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetUser",
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
                                    $(sControlName).append('<option value="' + '0' + '">' + 'ALL' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive)
                                            $(sControlName).append("<option value='" + obj[index].UserID + "'>" + obj[index].UserName + "</option>");
                                    }
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $(sControlName).append('<option value="' + '0' + '">' + 'ALL' + '</option>');
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
                        $(sControlName).append('<option value="' + '0' + '">' + 'ALL' + '</option>');
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
        function GetDescriptionList(ddlname) {
            var sControlName = "#" + ddlname;
            dProgress(true);
            $(sControlName).empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetDescriptionCategory",
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
                                    $(sControlName).append('<option value="' + '0' + '">' + 'ALL' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive)
                                            $(sControlName).append("<option value='" + obj[index].DescriptionCategoryID + "'>" + obj[index].DescriptionCategoryName + "</option>");
                                    }
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $(sControlName).append('<option value="' + '0' + '">' + 'ALL' + '</option>');
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
                        $(sControlName).append('<option value="' + '0' + '">' + 'ALL' + '</option>');
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
        function GetDoctorList(ddlname) {
            var sControlName = "#" + ddlname;
            dProgress(true);
            $(sControlName).empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetDoctor",
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
                                    $(sControlName).append('<option value="' + '0' + '">' + 'ALL' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive)
                                            $(sControlName).append("<option value='" + obj[index].DoctorID + "'>" + obj[index].DoctorName + "</option>");
                                    }
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $(sControlName).append('<option value="' + '0' + '">' + 'ALL' + '</option>');
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
                        $(sControlName).append('<option value="' + '0' + '">' + 'ALL' + '</option>');
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
        function GetPatientList(ddlname) {
            var sControlName = "#" + ddlname;
            dProgress(true);
            $(sControlName).empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetpatientName",
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
                                    $(sControlName).append('<option value="' + '0' + '">' + 'ALL' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive)
                                            $(sControlName).append("<option value='" + obj[index].patientID + "'>" + obj[index].WName + "</option>");
                                    }
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $(sControlName).append('<option value="' + '0' + '">' + 'ALL' + '</option>');
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
                        $(sControlName).append('<option value="' + '0' + '">' + 'ALL' + '</option>');
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
        function GetRecord(objFilter) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetOPBillingReport",
                data: JSON.stringify({ oJobCardFilter: objFilter }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            var notetable = $("#tblRecord").dataTable();
                            notetable.fnDestroy();
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                var amt = 0;
                                var OPDID = 0;
                                if (obj.length > 0) {
                                    $("#tblRecord_tbody").empty();
                                    var TypeStatus = "";
                                    for (var index = 0; index < obj.length; index++) {
                                       
                                        var table = "<tr id='" + obj[index].OPID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].BillNo + "</td>";
                                        table += "<td>" + obj[index].sBillDate + "</td>";
                                        table += "<td>" + obj[index].Patient.WName + "</td>";
                                        table += "<td>" + obj[index].Patient.WMobileNo + "</td>";
                                        table += "<td>" + obj[index].Doctor.DoctorName + "</td>";
                                        table += "<td>" + obj[index].DescriptionName + "</td>";
                                       
                                        table += "<td>" + (parseFloat(obj[index].Subtotal) - parseFloat(obj[index].DiscountAmount)) + "</td>";

                                        

                                        OPDID = (index + 2);
                                        table += "<td>" + obj[index].CUserName + "</td>";

                                        if (obj[index].IsCancelled == "0")
                                        {
                                            amt = amt + obj[index].Subtotal - parseFloat(obj[index].DiscountAmount);
                                            table += "<td></td>";
                                        }
                                        else
                                        { table += "<td>Cancelled</td>"; }

                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }

                                    var table1 = "<tr id='" + OPDID + "'>";
                                    table1 += "<td>" + OPDID + " </td>";
                                    table1 += "<td> </td>";
                                    table1 += "<td> </td>";
                                    table1 += "<td> </td>";
                                    table1 += "<td> </td>";
                                    table1 += "<td> </td>";

                                    table1 += "<td><b>Total:</b></td>";
                                    table1 += "<td><b>" + amt + "</b></td>";
                                    table1 += "<td> </td>";
                                    table1 += "<td> </td>";
                                    table1 += "</tr>";
                                    $("#tblRecord_tbody").append(table1);

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
                                  { "sWidth": "1%" },
                                   { "sWidth": "0%" },
                                { "sWidth": "0%" }
                                ],
                                dom: 'Blfrtip',
                                buttons: [
                               //{
                               //    extend: 'csv',
                               //    footer: false

                               //},
                               //{
                               //    extend: 'excel',
                               //    footer: false
                               //}
                               //,
                               //{
                               //    extend: 'pdf',
                               //    footer: false
                               //},
                               // {
                               //     extend: 'print'
                                    
                               // }
                               'csv', 'excel', 'pdf', 'print'

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
    </script>
</asp:Content>

