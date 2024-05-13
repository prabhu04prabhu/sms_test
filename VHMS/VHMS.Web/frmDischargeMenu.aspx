<%@ Page Title="Discharge Menu" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmDischargeMenu.aspx.cs" Inherits="frmDischargeMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper">
        <section class="content-header">
        </section>
        <section class="content">
            <div class="nav-tabs-custom" id="tab-modal">
                <ul class="nav nav-tabs pull-right">
                    <li class="active"><a id="aHR" href="#HR" data-toggle="tab">Activity</a></li>
                    <li class="pull-left header"><i class="fa fa-medkit"></i>Discharge</li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="HR">
                        <div class="box">
                            <div class="box-header">
                                <div class="box-title"><i class="fa fa-newspaper-o"></i>&nbsp;&nbsp;Master</div>
                            </div>
                            <div class="box-body">
                                <div class="table-responsive">
                                    <div id="divDischargeMasterMenu">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box">
                            <div class="box-header">
                                <div class="box-title"><i class="fa fa-newspaper-o"></i>&nbsp;&nbsp;Transaction</div>
                            </div>
                            <div class="box-body">
                                <div class="table-responsive">
                                    <div id="divDischargeTransactionMenu">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box">
                            <div class="box-header">
                                <div class="box-title"><i class="fa fa-newspaper-o"></i>&nbsp;&nbsp;Report</div>
                            </div>
                            <div class="box-body">
                                <div class="table-responsive">
                                    <div id="divDischargeReportMenu">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            pLoadingSetup(false);

            pLoadingSetup(true);

            GeneraterDischargeMasterMenu();
            GeneraterDischargeTransactionMenu();
        });

        function GeneraterDischargeMasterMenu() {
            var sTable = "";
            var sCount = 1;
            var sColorCode = "";

            //sTable = "<table id='tblHREmployeeMenu' class='table no-margin table-hover'>";
            //sTable += "<tr><td><a href='#'>" + "Department" + "</a></td><td><i class='fa fa-chevron-circle-right text-yellow'></i></td>";
            //sTable += "<tr><td><a href='#'>" + "Diagnosis" + "</a></td><td><i class='fa fa-chevron-circle-right text-yellow'></i></td>";
            //sTable += "<tr><td><a href='#'>" + "Doctor" + "</a></td><td><i class='fa fa-chevron-circle-right text-yellow'></i></td>";
            //sTable += "<tr><td><a href='#'>" + "Drug" + "</a></td><td><i class='fa fa-chevron-circle-right text-yellow'></i></td>";
            //sTable += "<tr><td><a href='#'>" + "Specialization" + "</a></td><td><i class='fa fa-chevron-circle-right text-yellow'></i></td>";
            //sTable += "<tr><td><a href='#'>" + "Register" + "</a></td><td><i class='fa fa-chevron-circle-right text-yellow'></i></td>";
            //sTable += ('</table>');
            sTable = "<p></P><a href='frmDepartment.aspx' class='btn btn-app bg-purple'><i class='fa fa-hospital-o'></i> Department</a>";
            sTable += "<a href='frmDiagonsis.aspx' class='btn btn-app bg-navy'><i class='fa fa-stethoscope'></i> Diagnosis</a>";
            sTable += "<a href='frmDoctor.aspx' class='btn btn-app bg-orange'><span class='badge'>3</span><i class='fa fa-user-md'></i> Doctors</a>";
            sTable += "<a href='frmDrug.aspx' class='btn btn-app bg-maroon'><i class='fa fa-medkit'></i> Drug</a>";
            sTable += "<a href='frmSpecialization.aspx' class='btn btn-app bg-olive'><i class='fa fa-h-square'></i> Spcialization</a>";
            sTable += "<a href='#' class='btn btn-app bg-teal'><i class='fa fa-plus-square'></i> Register</a>";
            $("#divDischargeMasterMenu").empty();
            $("#divDischargeMasterMenu").html(sTable);

            return false;
        }
        function GeneraterDischargeTransactionMenu() {
            var sTable = "";
            var sCount = 1;
            var sColorCode = "";

            sTable = "<table id='tblHREmployeeMenu' class='table no-margin table-hover'>";
            sTable += "<tr><td><a href='#'>" + "Discharge Entry" + "</a></td><td><i class='fa fa-users text-yellow'></i></td>";
            sTable += "<tr><td><a href='#'>" + "Discharge Edit" + "</a></td><td><i class='fa fa-edit text-yellow'></i></td>";
            sTable += "<tr><td><a href='#'>" + "Discharge Reprint" + "</a></td><td><i class='fa fa-print text-yellow'></i></td>";

            sTable = "<p></P><a class='btn btn-app bg-green'><i class='fa fa-plus'></i>Discharge Entry</a>";
            sTable += "<a class='btn btn-app bg-blue'><i class='fa fa-edit'></i> Edit Discharge</a>";
            sTable += "<a class='btn btn-app bg-yellow'><i class='fa fa-print fa-3x'></i> Reprint</a>";
            sTable += ('</table>');

            $("#divDischargeTransactionMenu").empty();
            $("#divDischargeTransactionMenu").html(sTable);

            return false;
        }
    </script>
</asp:Content>

