<%@ Page Title="Patient Report" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="false" CodeFile="frmPatientDetails.aspx.cs" Inherits="frmPatientDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <style>
        div.dt-buttons {
            float: right !important;
            margin-left: 10px !important;
        }
    </style>

    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.1.0/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.10/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.1.0/css/select.dataTables.min.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Patient Report
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Report</a></li>
                <li class="active">Patient Report</li>
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
                                <input type="text" class="form-control pull-right" tabindex="1" id="txtDOB"  readonly />
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
                        <div class="form-group col-md-2" id="divCategory">
                                        <label>
                                            Category</label><span class="text-danger">*</span>
                                        <select id="ddlCategory" class="form-control" tabindex="3">
                                            <option selected="selected" value="All">All</option>
                                            <option value="Fertility">Fertility</option>
                                            <option value="Gynaecology">Gynaecology</option>
                                            <option value="ANC">ANC</option>
                                        </select>
                                    </div>
                        <div class="form-group col-md-3" id="divHRefer">
                            <label>
                                Referred by</label><span class="text-danger">*</span>
                            <select id="ddlHRefer" class="form-control" tabindex="4">
                                <option selected="selected" value="All">All</option>
                                <option value="Media">Media</option>
                                <option value="Internet">Internet</option>
                                <option value="Doctor">Doctor</option>
                                <option value="Others">Others</option>
                            </select>                          
                        </div>                        
                    </div>
                </div>
            </div>
            <div class="row" id="divRecords">
                 <asp:GridView ID="gvProductMas" runat="server" CssClass="table table-striped table-bordered dTableR"
                            AutoGenerateColumns="false"  GridLines="None" DataKeyNames="ProductID"
                            AllowPaging="true" PageSize="10" OnPageIndexChanging="gvProductMas_PageIndexChanging"
                            EmptyDataText="No Records Found" AllowSorting="true">
                            <Columns>
                                <%--<asp:BoundField HeaderText="Sl.No" ReadOnly="true" DataField="ProductModelNo" />--%>
                                <asp:BoundField HeaderText="OPD NO#" ReadOnly="true" DataField="OPDNo" />
                                <asp:BoundField HeaderText="Category" ReadOnly="true" DataField="Category" />
                                <asp:BoundField HeaderText="Patient Name" ReadOnly="true" DataField="WName" />
                                <asp:BoundField HeaderText="Husband Name" ReadOnly="true" DataField="HName" />
                                <asp:BoundField HeaderText="Phone" ReadOnly="true" DataField="WMobileNo" />
                                <asp:BoundField HeaderText="Address" ReadOnly="true" DataField="WAddress" />
                                  <asp:BoundField HeaderText="Email" ReadOnly="true" DataField="WEmail" />
                                  <asp:BoundField HeaderText="Referred BY" ReadOnly="true" DataField="WReferredBy" />
                                <asp:BoundField HeaderText="Reference details" ReadOnly="true" DataField="WReferredDetails" />
                                  <asp:BoundField HeaderText="Doctor Mobile No" ReadOnly="true" DataField="RefDoctorMobileNo" />
                            </Columns>
                            <PagerStyle CssClass="cssPager" HorizontalAlign="Center" />
                        </asp:GridView>
               <%-- <div class="col-xs-12">
                    <div class="box box-warning">
                        <div class="box-body">
                            <div class="table-responsive">
                                <table id="tblRecord" class="table table-condensed table-striped table-bordered" width="100%">
                                    <thead>
                                        <tr class="bg-primary">
                                            <th>Sl.No
                                            </th>
                                            <th>OPD NO#
                                            </th>
                                            <th>Category
                                            </th>
                                            <th>Patient Name
                                            </th>
                                            <th>Husband Name
                                            </th>
                                            <th>Phone
                                            </th>
                                            <th>Address
                                            </th>
                                            <th class="hidden-xs">Email
                                            </th>
                                            <th class="hidden-xs">Referred BY
                                            </th>
                                            <th class="hidden-xs">Reference details
                                            </th>
                                             <th class="hidden-xs">Doctor Mobile No
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody id="tblRecord_tbody">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>--%>
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
    <script src="https://cdn.datatables.net/buttons/1.1.0/js/dataTables.buttons.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/2.5.0/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/vfs_fonts.js"></script>--%>
    <%--<script src="https://cdn.datatables.net/buttons/1.1.0/js/buttons.html5.min.js"></script>--%>
    <%--<script src="JS/buttons.html5.min.js"></script>--%>


    <script type="text/javascript" src="JS/jquery.dataTables.js"></script>
    <script src="JS/dataTables.tableTools.js"></script>
    <script src="JS/dataTables.buttons.min.js"></script>
    <script src="JS/jszip.min.js"></script>
    <script src="JS/buttons.flash.min.js"></script>
    <script src="JS/vfs_fonts.js"></script>
     <script src="JS/pdfmake.min.js"></script>
    <script src="JS/buttons.html5.min.js"></script>
    <script type="text/javascript" src="JS/buttons.print.min.js"></script>
  
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
           
           // GetRecord(objFilter);
           
            pLoadingSetup(true);
            $("#txtDOB").focus();
            $("#txtDOR").focus();
            $("#ddlCategory").focus();
        });
        $('#txtDOB,#ddlHRefer,#txtDOR,#ddlCategory').change(function () {
            var objFilter = new Object();
            objFilter.Category = $('#ddlCategory').val();
            objFilter.ReferredBy = $('#ddlHRefer').val();
            objFilter.DateTo = $('#txtDOR').val();
            objFilter.DateFrom = $('#txtDOB').val();
            // GetRecord(objFilter);

            $.ajax({
                type: "POST",
                url: pageUrl + "/LoadReport",
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                   
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        function GetRecord(objFilter) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetPatientDetails",
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
                                if (obj.length > 0) {
                                    $("#tblRecord_tbody").empty();
                                    var TypeStatus = "";
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive == "1")
                                        { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                        else
                                        { TypeStatus = "<span class='label label-warning'>Inactive</span>"; }
                                        var table = "<tr id='" + obj[index].PatientID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].OPDNo + "</td>";
                                        table += "<td>" + obj[index].Category + "</td>";
                                        table += "<td>" + obj[index].WName + "</td>";
                                        table += "<td>" + obj[index].HName + "</td>";
                                        table += "<td>" + obj[index].WMobileNo + "</td>";
                                        table += "<td>" + obj[index].WAddress + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].WEmail + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].WReferredBy + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].WReferredDetails + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].RefDoctorMobileNo + "</td>";

                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }
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
                                  { "sWidth": "1%" },
                                  { "sWidth": "1%" },
                                  { "sWidth": "1%" }
                                ],
                                dom: 'Blfrtip',
                                buttons: [
                                     'csv', 'excel', 'pdf', 'print'
                               //{
                               //    extend: 'csv',
                               //    footer: false

                               //},
                               //{
                               //    extend: 'excel',
                               //    footer: false
                               //}
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


