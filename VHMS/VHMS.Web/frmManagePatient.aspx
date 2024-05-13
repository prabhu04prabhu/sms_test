<%@ Page Title="Manage Patient" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmManagePatient.aspx.cs" Inherits="frmManagePatient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header" id="secHeader">
            <div id="divTitle">
                <h3>Patient
                </h3>
            </div>
            <ol class="breadcrumb pull-left hidden">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li class="active">Dashboard</li>
            </ol>
            <br />
            <div class="breadcrumb">
                <button id="btnAddNew" class="btn btn-info">
                    <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add New</button>
            </div>
        </section>
        <section class="content">
            <div class="nav-tabs-custom" id="divRecords">
                <ul class="nav nav-tabs">
                    <li class="active"><a id="aGeneral" href="#General" data-toggle="tab">Patients</a></li>
                    <li><a id="aSearchResult" href="#SearchResult" data-toggle="tab">SearchRecord</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="General">
                        <div class="box box-warning">
                            <div class="box-body">
                                <div class="table-responsive">
                                    <table id="tblRecord" class="table table-striped table-bordered bg-info" width="100%">
                                        <thead>
                                            <tr>
                                                <th>S.No</th>
                                                <th>OPD No</th>
                                                <th>Husband Name</th>
                                                <th>Wife Name</th>
                                                <th class="hidden-xs">Husband Mobile No</th>
                                                <th class="hidden-xs">Wife Mobile No</th>
                                                <th class="hidden-xs">Status</th>
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
                    <div class="tab-pane active" id="SearchResult">
                        <div class="box box-warning">
                            <div class="box-body">
                                <div class="form-group" id="divSearchaname">
                                    <label>
                                        Search Patient Records</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtSearchName" placeholder="Please enter Patient Name or number"
                                        maxlength="150" />
                                </div>

                                <div class="table-responsive">
                                    <table id="tblSearchResult" class="table table-striped table-bordered bg-info" width="100%">
                                       <thead>
                                            <tr>
                                                <th>S.No</th>
                                                <th>OPD No</th>
                                                <th>Husband Name</th>
                                                <th>Wife Name</th>
                                                <th class="hidden-xs">Husband Mobile No</th>
                                                <th class="hidden-xs">Wife Mobile No</th>
                                                <th class="hidden-xs">Status</th>
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
        </section>
    </div>
    <input type="hidden" id="hdnPatientID" />
    <script src="UserDefined_Js/Discharge/jManagePatient.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/Discharge/jManagePatient.js") %>" type="text/javascript"></script>

    <script type="text/javascript">
        var _CMActionAdd = '<%=Session["ActionAdd"]%>';
        var _CMActionUpdate = '<%=Session["ActionUpdate"]%>';
        var _CMActionDelete = '<%=Session["ActionDelete"]%>';
        var _CMActionView = '<%=Session["ActionView"]%>';

        $(document).ready(function () {
        });
    </script>
</asp:Content>

